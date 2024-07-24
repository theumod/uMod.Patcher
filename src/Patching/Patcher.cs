using Mono.Cecil;
using Oxide.Patcher.Fields;
using Oxide.Patcher.Hooks;
using Oxide.Patcher.Modifiers;
using Oxide.Patcher.Patching.OxideDefinitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Oxide.Patcher.Common;
using FieldAttributes = Mono.Cecil.FieldAttributes;
using MethodAttributes = Mono.Cecil.MethodAttributes;
using TypeAttributes = Mono.Cecil.TypeAttributes;

namespace Oxide.Patcher.Patching
{
    /// <summary>
    /// Responsible for performing the actual patch process
    /// </summary>
    public class Patcher
    {
        /// <summary>
        /// Gets the project that this patcher will patch
        /// </summary>
        public Project PatchProject { get; }

        /// <summary>
        /// Is this a Console or Window Patcher?
        /// </summary>
        public bool IsConsole { get; }

        /// <summary>
        /// Called when a log message has been written
        /// </summary>
        public event Action<string> OnLogMessage;

        /// <summary>
        /// Directory where the patcher resides
        /// </summary>
        public string Directory { get; }

        /// <summary>
        /// Initializes a new instance of the Patcher class
        /// </summary>
        /// <param name="patchproject"></param>
        /// <param name="console"></param>
        public Patcher(Project patchproject, bool console = false)
        {
            PatchProject = patchproject;
            IsConsole = console;
            Directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        /// <summary>
        /// Gets the correct Assembly FilePath
        /// </summary>
        /// <param name="assemblyname"></param>
        /// <param name="original"></param>
        private string GetAssemblyFilename(string assemblyname, bool original)
        {
            if (original)
            {
                return Path.Combine(PatchProject.TargetDirectory, Path.GetFileNameWithoutExtension(assemblyname) + "_Original" + Path.GetExtension(assemblyname));
            }

            return Path.Combine(PatchProject.TargetDirectory, assemblyname);
        }

        /// <summary>
        /// Logs to console / window output and to log file
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        internal void Log(string format, params object[] args)
        {
            string line = string.Format(format, args);
            WriteToLog(line);
            if (IsConsole)
            {
                Console.WriteLine(line);
            }
            else
            {
                OnLogMessage?.Invoke(line);
            }
        }

        /// <summary>
        /// Writes text to log file
        /// </summary>
        /// <param name="line"></param>
        private void WriteToLog(string line)
        {
            using (StreamWriter file = new StreamWriter(Path.Combine(Directory, "log.txt"), true))
            {
                file.WriteLine(line);
            }
        }

        public Dictionary<string, AssemblyDefinition> Patch(bool save = true, bool applyModifiers = true)
        {
            Dictionary<string, AssemblyDefinition> patchedAssemblies = new Dictionary<string, AssemblyDefinition>();

            if (PatchProject == null)
            {
                return patchedAssemblies;
            }

            ReaderParameters readerParams = new ReaderParameters
            {
                AssemblyResolver = new PatcherAssemblyResolver(PatchProject.TargetDirectory)
            };

            DateTime now = DateTime.Now;
            WriteToLog("----------------------------------------");
            WriteToLog($"{now.ToShortDateString()} {now:hh:mm:ss tt zzz}");
            WriteToLog("----------------------------------------");

            // First pass, perform injections in all assemblies that may be referenced in the second pass
            foreach (Manifest manifest in PatchProject.Manifests)
            {
                // Get the assembly filename
                string filename = GetAssemblyFilename(manifest.AssemblyName, true);
                if (!File.Exists(filename))
                {
                    if (IsConsole)
                    {
                        filename = GetAssemblyFilename(manifest.AssemblyName, false);
                        if (!File.Exists(filename))
                        {
                            WriteToLog($"Failed to locate target assembly {manifest.AssemblyName}");
                            throw new FileNotFoundException($"Failed to locate target assembly {manifest.AssemblyName}", filename);
                        }

                        File.Copy(filename, Path.GetFileNameWithoutExtension(filename) + "_Original" + Path.GetExtension(filename), true);
                        filename = Path.GetFileNameWithoutExtension(filename) + "_Original" + Path.GetExtension(filename);
                    }
                    else
                    {
                        WriteToLog($"Failed to locate target assembly {manifest.AssemblyName}");
                        throw new FileNotFoundException($"Failed to locate target assembly {manifest.AssemblyName}", filename);
                    }
                }

                // Load it
                Log("Loading assembly {0}", manifest.AssemblyName);
                AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(filename, readerParams);

                InjectCustomFields(manifest.Fields, assembly);

                // Save it
                Log("First pass saving assembly {0}", manifest.AssemblyName);
                patchedAssemblies[manifest.AssemblyName] = assembly;
            }

            // Second pass, can use newly injected functionality
            foreach (Manifest manifest in PatchProject.Manifests)
            {
                // Load assembly
                Log("Loading assembly {0}", manifest.AssemblyName);
                if (!patchedAssemblies.TryGetValue(manifest.AssemblyName, out AssemblyDefinition assembly))
                {
                    continue;
                }

                List<Hook> baseHooks = manifest.Hooks.Where(hook => hook.BaseHook != null).Select(x => x.BaseHook).ToList();
                Dictionary<Hook, Hook> cloneHooks = manifest.Hooks.Where(hook => hook.BaseHook != null).ToDictionary(hook => hook.BaseHook);

                // Loop each hook
                foreach (Hook hook in manifest.Hooks)
                {
                    InjectHook(hook, assembly, baseHooks, cloneHooks);
                }

                // Loop each access modifier
                if (applyModifiers)
                {
                    foreach (Modifier modifier in manifest.Modifiers)
                    {
                        ApplyModifier(modifier, assembly);
                    }
                }

                if (save)
                {
                    Log("Saving assembly {0}", manifest.AssemblyName);
                    assembly.Write(GetAssemblyFilename(manifest.AssemblyName, false));
                }
            }

            return patchedAssemblies;
        }

        public AssemblyDefinition PatchSingleAssembly(string assemblyName)
        {
            if (!assemblyName.EndsWith(".dll"))
            {
                assemblyName += ".dll";
            }

            ReaderParameters readerParams = new ReaderParameters
            {
                AssemblyResolver = new PatcherAssemblyResolver(PatchProject.TargetDirectory)
            };

            foreach (Manifest manifest in PatchProject.Manifests)
            {
                if (manifest.AssemblyName != assemblyName)
                {
                    continue;
                }

                // Get the assembly filename
                string filename = GetAssemblyFilename(manifest.AssemblyName, true);

                // Load it
                Log("Loading assembly {0}", manifest.AssemblyName);
                AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(filename, readerParams);

                InjectCustomFields(manifest.Fields, assembly);

                return assembly;
            }

            return null;
        }

        private void InjectHook(Hook hook, AssemblyDefinition assemblyDefinition, List<Hook> baseHooks, Dictionary<Hook, Hook> cloneHooks)
        {
            bool cloneFlagged = cloneHooks.TryGetValue(hook, out Hook cloneHook) && cloneHook.Flagged;

            if (baseHooks.Contains(hook) && !hook.Flagged && !cloneFlagged)
            {
                return;
            }

            // Check if it's flagged
            if (hook.BaseHook != null && hook.BaseHook.Flagged)
            {
                Log("Ignored hook {0} as its base hook {1} is flagged", hook.Name, hook.BaseHook.Name);
                return;
            }

            if (hook.Flagged)
            {
                // Log
                Log("Ignored hook {0} as it is flagged", hook.Name);
                return;
            }

            // Locate the method
            MethodDefinition method = GetMethodDefinition(assemblyDefinition, hook.TypeName, hook.Signature);

            // Let the hook do it's work
            ILWeaver weaver = new ILWeaver(method.Body, method.Module);

            try
            {
                // Apply
                if (hook.PreparePatch(method, weaver, this) && hook.ApplyPatch(method, weaver, this))
                {
                    weaver.Apply(method.Body);

                    Hook baseHook = hook;
                    if (baseHook.BaseHook != null)
                    {
                        List<string> patchedHooks = new List<string> { hook.Name };
                        while (baseHook.BaseHook != null)
                        {
                            baseHook = baseHook.BaseHook;
                            patchedHooks.Add(baseHook.Name);
                        }

                        patchedHooks.Reverse();
                        Log("Applied hooks {0} to {1}::{2}", string.Join(", ", patchedHooks), baseHook.TypeName, baseHook.Signature.Name);
                    }
                    else
                    {
                        Log("Applied hook {0} to {1}::{2}", hook.Name, hook.TypeName, hook.Signature.Name);
                    }
                }
                else
                {
                    Log("Failed to apply hook {0}, invalid injection index specified!", hook.Name);
                    hook.Flagged = true;
                }
            }
            catch (Exception ex)
            {
                Log("Failed to apply hook {0}", hook.Name);
                Log(ex.ToString());
            }
        }

        private void InjectCustomFields(IEnumerable<Field> fields, AssemblyDefinition assemblyDefinition)
        {
            // Loop each additional field
            foreach (Field field in fields)
            {
                if (field.Flagged)
                {
                    Log($"Ignored adding field {field.TypeName}::{field.Name} as it is flagged");
                    continue;
                }

                if (string.IsNullOrEmpty(field.FieldType))
                {
                    Log($"Ignored adding field {field.TypeName}::{field.Name} as it has no target type");
                    continue;
                }

                if (!OxideDefinitions.OxideDefinitions.TryParseType(field.FieldType, out OxideTypeDefinition def, out string error))
                {
                    Log($"Failed to add field {field.TypeName}::{field.Name}, {error}");
                    continue;
                }

                FieldDefinition newField = new FieldDefinition(field.Name,
                    FieldAttributes.Public | FieldAttributes.NotSerialized,
                    assemblyDefinition.MainModule.Import(def.GetTypeReference()));

                TypeDefinition target = assemblyDefinition.MainModule.GetType(field.TypeName);
                target.Fields.Add(newField);

                Log($"Applied new field {field.Name} to {field.TypeName}");
            }
        }

        private void ApplyModifier(Modifier modifier, AssemblyDefinition assembly)
        {
            if (modifier.Flagged)
            {
                // Log
                Log($"Ignored modifier changes to {modifier.Name} as it is flagged");
                return;
            }

            switch (modifier.Type)
            {
                case ModifierType.Field:
                {
                    FieldDefinition field = GetFieldDefinition(assembly, modifier.TypeName, modifier.Signature);

                    if (modifier.Signature.Exposure[0] != modifier.TargetExposure[0])
                    {
                        ReplaceExposure(modifier.TargetExposure[0], field);
                    }

                    switch (modifier.TargetExposure.Length)
                    {
                        case 1 when field.IsStatic:
                        {
                            field.Attributes -= FieldAttributes.Static;

                            break;
                        }

                        case 2 when !field.IsStatic:
                        {
                            field.Attributes |= FieldAttributes.Static;

                            break;
                        }
                    }

                    Log($"Applied modifier changes to field {modifier.TypeName}::{modifier.Name}");

                    break;
                }

                case ModifierType.Method:
                {
                    MethodDefinition method = GetMethodDefinition(assembly, modifier.TypeName, modifier.Signature);

                    if (modifier.Signature.Exposure[0] != modifier.TargetExposure[0])
                    {
                        ReplaceExposure(modifier.TargetExposure[0], method);
                    }

                    switch (modifier.TargetExposure.Length)
                    {
                        case 1:
                            if (method.IsStatic)
                            {
                                method.Attributes -= MethodAttributes.Static;
                            }

                            break;

                        case 2:
                            if (!method.IsStatic)
                            {
                                method.Attributes |= MethodAttributes.Static;
                            }

                            break;
                    }

                    Log($"Applied modifier changes to method {modifier.TypeName}::{modifier.Signature.Name}");

                    break;
                }

                case ModifierType.Property:
                {
                    PropertyDefinition property =
                        GetPropertyDefinition(assembly, modifier.TypeName, modifier.Signature);

                    MethodDefinition getMethod = property.GetMethod;

                    if (getMethod != null && modifier.Signature.Exposure[0] != modifier.TargetExposure[0])
                    {
                        ReplaceExposure(modifier.TargetExposure[0], getMethod);
                    }

                    MethodDefinition setMethod = property.SetMethod;

                    if (setMethod != null && modifier.Signature.Exposure[1] != modifier.TargetExposure[1])
                    {
                        ReplaceExposure(modifier.TargetExposure[1], setMethod);
                    }

                    switch (modifier.TargetExposure.Length)
                    {
                        case 1:
                            if (getMethod != null && getMethod.IsStatic)
                            {
                                getMethod.Attributes -= MethodAttributes.Static;
                            }

                            if (setMethod != null && setMethod.IsStatic)
                            {
                                setMethod.Attributes -= MethodAttributes.Static;
                            }

                            break;

                        case 2:
                            if (getMethod != null && setMethod == null && !getMethod.IsStatic)
                            {
                                getMethod.Attributes |= MethodAttributes.Static;
                            }

                            break;

                        case 3:
                            if (getMethod != null && !getMethod.IsStatic)
                            {
                                getMethod.Attributes |= MethodAttributes.Static;
                            }

                            if (setMethod != null && !setMethod.IsStatic)
                            {
                                setMethod.Attributes |= MethodAttributes.Static;
                            }

                            break;
                    }

                    Log($"Applied modifier changes to property {modifier.TypeName}::{modifier.Name}");

                    break;
                }

                case ModifierType.Type:
                {
                    TypeDefinition typeDef = GetTypeDefinition(assembly, modifier.TypeName);

                    if (modifier.Signature.Exposure[0] != modifier.TargetExposure[0])
                    {
                        ReplaceExposure(modifier.TargetExposure[0], typeDef);
                    }

                    switch (modifier.TargetExposure.Length)
                    {
                        case 1:
                            if (typeDef.IsAbstract && typeDef.IsSealed)
                            {
                                typeDef.Attributes -= TypeAttributes.Abstract | TypeAttributes.Sealed;
                            }

                            break;

                        case 2:
                            if (!typeDef.IsAbstract && !typeDef.IsSealed)
                            {
                                typeDef.Attributes |= TypeAttributes.Abstract | TypeAttributes.Sealed;
                            }

                            break;
                    }

                    Log($"Applied modifier changes to type {modifier.TypeName}");

                    break;
                }
            }
        }

        private void ReplaceExposure(Exposure targetExposure, FieldDefinition field)
        {
            switch (targetExposure)
            {
                case Exposure.Private:
                    field.IsPrivate = true;
                    break;

                case Exposure.Protected:
                    field.IsFamily = true;
                    break;

                case Exposure.Public:
                    field.IsPublic = true;
                    break;

                case Exposure.Internal:
                    field.IsAssembly = true;
                    break;
            }
        }

        private static void ReplaceExposure(Exposure targetExposure, MethodDefinition method)
        {
            switch (targetExposure)
            {
                case Exposure.Private:
                    method.IsPrivate = true;
                    break;

                case Exposure.Protected:
                    method.IsFamily = true;
                    break;

                case Exposure.Public:
                    method.IsPublic = true;
                    break;

                case Exposure.Internal:
                    method.IsAssembly = true;
                    break;
            }
        }

        private static void ReplaceExposure(Exposure targetExposure, TypeDefinition typeDefinition)
        {
            switch (targetExposure)
            {
                case Exposure.Private:
                {
                    if (typeDefinition.IsNested)
                    {
                        typeDefinition.IsNestedPrivate = true;
                    }
                    else
                    {
                        typeDefinition.IsNotPublic = true;
                    }

                    break;
                }

                case Exposure.Public:
                {
                    if (typeDefinition.IsNested)
                    {
                        typeDefinition.IsNestedPublic = true;
                    }
                    else
                    {
                        typeDefinition.IsPublic = true;
                    }

                    break;
                }
            }
        }

        private MethodDefinition GetMethodDefinition(AssemblyDefinition assemblyDefinition, string typeName, MethodSignature signature)
        {
            try
            {
                TypeDefinition type = assemblyDefinition.Modules
                                                        .SelectMany(m => m.GetTypes())
                                                        .Single(t => t.FullName == typeName);

                return type.Methods.Single(m => Utility.GetMethodSignature(m).Equals(signature));
            }
            catch
            {
                string message = $"Failed to locate method {typeName}::{signature.Name} in assembly {assemblyDefinition.FullName}";

                WriteToLog(message);
                throw new Exception(message);
            }
        }

        private MethodDefinition GetMethodDefinition(AssemblyDefinition assemblyDefinition, string typeName, ModifierSignature signature)
        {
            try
            {
                TypeDefinition type = assemblyDefinition.Modules
                                                        .SelectMany(m => m.GetTypes())
                                                        .Single(t => t.FullName == typeName);

                return type.Methods.Single(m => Utility.GetModifierSignature(m).Equals(signature));
            }
            catch
            {
                string message = $"Failed to locate method {typeName}::{signature.Name} in assembly {assemblyDefinition.FullName}";

                WriteToLog(message);
                throw new Exception(message);
            }
        }

        private FieldDefinition GetFieldDefinition(AssemblyDefinition assembly, string typeName, ModifierSignature signature)
        {
            try
            {
                TypeDefinition type = assembly.Modules.SelectMany(m => m.GetTypes()).Single(t => t.FullName == typeName);
                return type.Fields.Single(m => Utility.GetModifierSignature(m).Equals(signature));
            }
            catch (Exception)
            {
                string message = $"Failed to locate field {typeName}::{signature.Name} in assembly {assembly.FullName}";

                WriteToLog(message);
                throw new Exception(message);
            }
        }

        private PropertyDefinition GetPropertyDefinition(AssemblyDefinition assembly, string typeName, ModifierSignature signature)
        {
            try
            {
                TypeDefinition type = assembly.Modules.SelectMany(m => m.GetTypes()).Single(t => t.FullName == typeName);
                return type.Properties.Single(m => Utility.GetModifierSignature(m).Equals(signature));
            }
            catch (Exception)
            {
                string message = $"Failed to locate property {typeName}::{signature.Name} in assembly {assembly.FullName}";

                WriteToLog(message);
                throw new Exception(message);
            }
        }

        private TypeDefinition GetTypeDefinition(AssemblyDefinition assembly, string typeName)
        {
            try
            {
                return assembly.Modules.SelectMany(m => m.GetTypes()).Single(t => t.FullName == typeName);
            }
            catch (Exception)
            {
                string message = $"Failed to locate type {typeName} in assembly {assembly.FullName}";

                WriteToLog(message);
                throw new Exception(message);
            }
        }
    }
}
