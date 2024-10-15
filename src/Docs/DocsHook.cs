using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.Ast;
using ICSharpCode.NRefactory.CSharp;

using Mono.Cecil;
using Mono.Cecil.Cil;
using Oxide.Patcher.Common;
using Oxide.Patcher.Hooks;

namespace Oxide.Patcher.Docs
{
    public class DocsHook
    {
        public HookType Type { get; set; }
        public string Name { get; set; }
        public string HookName { get; set; }
        public string HookDescription { get; set; }
        public Dictionary<string, string> HookParameters { get; set; }
        public string ReturnType { get; set; }
        public ReturnBehavior ReturnBehavior { get; set; } = ReturnBehavior.Continue;
        public string TargetType { get; set; }
        public string Category { get; set; }
        public DocsMethodData MethodData { get; set; }
        public string CodeAfterInjection { get; set; }

        private readonly string _targetDirectory;

        public DocsHook(Hook hook, MethodDefinition methodDef, string targetDirectory)
        {
            if (IsNeverCalledInPlugin(hook.HookName))
            {
                throw new NotSupportedException("This hook is never called in a plugin");
            }

            _targetDirectory = targetDirectory;

            switch (hook)
            {
                case Simple simpleHook:
                    Type = HookType.Simple;
                    ReturnBehavior = simpleHook.ReturnBehavior;
                    HookParameters = GetHookArguments(simpleHook, methodDef);
                    ReturnType = GetReturnType(simpleHook, methodDef);
                    break;

                case Modify modifyHook:
                    Type = HookType.Modify;
                    break;

                default:
                    throw new NotSupportedException("This hook type is not supported");
            }

            Name = hook.Name;
            HookName = hook.HookName;
            HookDescription = hook.HookDescription;
            TargetType = hook.TypeName;
            Category = hook.HookCategory;

            MethodData = new DocsMethodData(methodDef);

            string methodSourceCode = GetSourceCode(methodDef);

            methodDef.Body = null;

            string[] lines = Regex.Split(methodSourceCode, "\r\n|\r|\n");

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                if (!line.Contains($"Interface.CallHook(\"{hook.HookName}\""))
                {
                    continue;
                }

                int startIndex = i - 5 < 0 ? 0 : i - 5;
                int endIndex = i + 6;

                StringBuilder sb = new StringBuilder();

                if (startIndex > 0)
                {
                    sb.AppendLine("//---");
                }

                for (int x = startIndex; x < endIndex && x < lines.Length; x++)
                {
                    sb.AppendLine(lines[x]);
                }

                if (endIndex < lines.Length - 1)
                {
                    sb.AppendLine("//---");
                }

                CodeAfterInjection = sb.ToString();

                break;
            }
        }

        private Dictionary<string, string> GetHookArguments(Simple hook, MethodDefinition method)
        {
            Dictionary<string, string> hookArguments = new Dictionary<string, string>();

            switch (hook?.ArgumentBehavior)
            {
                case ArgumentBehavior.All:
                {
                    AddThisArg(hook, hookArguments);
                    AddMethodArgs(method, hookArguments);

                    break;
                }

                case ArgumentBehavior.JustThis:
                {
                    AddThisArg(hook, hookArguments);
                    break;
                }

                case ArgumentBehavior.JustParams:
                {
                    AddMethodArgs(method, hookArguments);
                    break;
                }

                case ArgumentBehavior.UseArgumentString:
                {
                    string[] args = Utility.ParseArgumentString(hook.ArgumentString, out string returnValue);

                    foreach (string argument in args)
                    {
                        string typeName = Utility.TransformType(GetArgStringType(argument, method, out string argName));

                        //TODO: think of a better way to handle if there are two args that have the same name
                        if (hookArguments.ContainsKey(argName))
                        {
                            string newArgName = argName;

                            int index = 2;
                            while (hookArguments.ContainsKey(newArgName))
                            {
                                newArgName = $"{argName}{index}";
                                index++;
                            }

                            hookArguments.Add(newArgName, typeName);

                            continue;
                        }

                        hookArguments.Add(argName, typeName);
                    }

                    break;
                }
            }

            return hookArguments;
        }

        private string GetReturnType(Simple hook, MethodDefinition method)
        {
            string returnType = "";
            try
            {
                switch (hook?.ReturnBehavior)
                {
                    case ReturnBehavior.Continue:
                        returnType = "void";
                        break;

                    case ReturnBehavior.ExitWhenNonNull:
                        if (hook?.Signature.ReturnType == "System.Void") returnType = "object";
                        else returnType = Utility.TransformType(hook?.Signature.ReturnType);
                        break;

                    case ReturnBehavior.ExitWhenValidType:
                        if (hook?.Signature.ReturnType == "System.Void") returnType = "object";
                        else returnType = Utility.TransformType(hook?.Signature.ReturnType);
                        break;

                    case ReturnBehavior.ModifyRefArg:
                        if (hook?.Signature.ReturnType == "System.Void") returnType = "object";
                        else returnType = Utility.TransformType(hook?.Signature.ReturnType);  
                        break;

                    case ReturnBehavior.UseArgumentString:
                        string[] args = Utility.ParseArgumentString(hook.ArgumentString, out string returnValue);
                        returnType = Utility.TransformType(GetArgStringType(returnValue, method, out string argName));
                        break;
                }                
            } catch { returnType = "ERROR"; }

            return returnType;
        }

        //Doesn't work if I use the Decompiler class so just do this for now
        private static string GetSourceCode(MethodDefinition methodDefinition)
        {
            DecompilerSettings settings = new DecompilerSettings { UsingDeclarations = false };
            DecompilerContext context = new DecompilerContext(methodDefinition.Module)
            {
                CurrentType = methodDefinition.DeclaringType,
                Settings = settings
            };

            AstBuilder astBuilder = new AstBuilder(context);
            astBuilder.AddMethod(methodDefinition);
            PlainTextOutput textOutput = new PlainTextOutput();
            astBuilder.GenerateCode(textOutput);
            return textOutput.ToString();
        }

        #region -Arg Helpers-

        private string GetArgStringType(string arg, MethodDefinition method, out string argName)
        {
            string firstArg = arg.ToLowerInvariant();

            string[] target = null;
            if (!string.IsNullOrEmpty(firstArg) && arg.Contains("."))
            {
                string[] split = arg.Split('.');
                firstArg = split[0];
                target = split.Skip(1).ToArray();
            }

            if ((firstArg.StartsWith("l") || firstArg.StartsWith("v")) && int.TryParse(firstArg.Substring(1), out int index))
            {
                VariableDefinition variable = method.Body.Variables[index];
                TypeReference variableType = variable.VariableType;

                if (target != null && GetMember(method, variableType.Resolve(), target, out TypeDefinition finalType))
                {
                    argName = target[target.Length - 1];
                    return finalType.Name;
                }

                argName = GetLocalVariableName(index, method);
                return variableType is ByReferenceType byRefType
                           ? byRefType.ElementType.Name
                           : variableType.Name;
            }

            if ((firstArg.StartsWith("a") || firstArg.StartsWith("p")) && int.TryParse(firstArg.Substring(1), out index))
            {
                ParameterDefinition parameter = method.Parameters[index];
                TypeReference parameterType = parameter.ParameterType;

                if (target != null && GetMember(method, parameterType.Resolve(), target, out TypeDefinition finalType))
                {
                    argName = target[target.Length - 1];
                    return finalType.Name;
                }

                argName = parameter.Name;
                return parameter.ParameterType.Name;
            }

            if (firstArg.StartsWith("r") && int.TryParse(firstArg.Substring(1), out index) &&
                method.Body.Instructions[index - 1].Operand is MethodDefinition storedMethod)
            {
                TypeDefinition returnType = storedMethod.DeclaringType;
                string typeName = returnType.Name;

                char firstChar = char.ToLower(typeName[0]);
                argName = $"{firstChar}{typeName.Substring(1)}";
                return returnType.Name;
            }

            if (firstArg == "this")
            {
                if (target != null && GetMember(method, method.DeclaringType, target, out TypeDefinition finalType))
                {
                    argName = target[target.Length - 1];
                    return finalType.Name;
                }

                argName = "instance";
                return method.DeclaringType.Name;
            }

            argName = "Unknown";
            return "Unknown";
        }

        private string GetLocalVariableName(int index, MethodDefinition method)
        {
            DecompilerContext context = new DecompilerContext(method.Module)
            {
                CurrentType = method.DeclaringType,
            };

            AstBuilder astBuilder = new AstBuilder(context);
            astBuilder.AddMethod(method);

            MethodDeclaration methodDeclaration = astBuilder.SyntaxTree.Members.First() as MethodDeclaration;
            if (methodDeclaration == null)
            {
                return $"V_{index}";
            }

            int varsFound = 0;
            foreach (Statement statement in methodDeclaration.Body.Statements)
            {
                if (statement is VariableDeclarationStatement varDeclaration)
                {
                    if (varsFound != index)
                    {
                        varsFound++;
                        continue;
                    }

                    string identifier = GetIdentifier(varDeclaration.Children);
                    if (!string.IsNullOrEmpty(identifier))
                    {
                        return identifier;
                    }
                }

                if (statement is ExpressionStatement expressionStatement)
                {
                    if (varsFound != index)
                    {
                        varsFound++;
                        continue;
                    }

                    string identifier = GetIdentifier(expressionStatement.Children);
                    if (!string.IsNullOrEmpty(identifier))
                    {
                        return identifier;
                    }
                }
            }

            return $"V_{index}";
        }

        private string GetIdentifier(IEnumerable<AstNode> children)
        {
            foreach (AstNode child in children)
            {
                if (child is Identifier identifier)
                {
                    return identifier.Name;
                }

                if (child is IdentifierExpression identifierExpression)
                {
                    return identifierExpression.Identifier;
                }

                if (child is VariableInitializer initializer)
                {
                    return GetIdentifier(initializer.Children);
                }
            }

            return null;
        }

        private bool GetMember(MethodDefinition originalMethod, TypeDefinition currentArg, string[] target, out TypeDefinition finalType)
        {
            finalType = null;
            if (currentArg == null || target == null || target.Length == 0)
            {
                return false;
            }

            int i;
            TypeDefinition arg = currentArg;
            for (i = 0; i < target.Length; i++)
            {
                if (GetMember(originalMethod, ref arg, target[i]))
                {
                    continue;
                }

                return false;
            }

            finalType = arg;
            return i >= 1;
        }

        private bool GetMember(MethodDefinition originalMethod, ref TypeDefinition currentArg, string target)
        {
            if (currentArg == null || string.IsNullOrEmpty(target))
            {
                return false;
            }

            while (currentArg != null)
            {
                if (target.Contains('('))
                {
                    string[] methodName = target.Split('(');
                    string[] args = methodName[1].TrimEnd(')').Split(',').Where(x => !string.IsNullOrEmpty(x)).ToArray();

                    MethodDefinition method = currentArg.Methods.FirstOrDefault(m => m.Name == methodName[0] && m.Parameters.Count == args.Length);
                    if (method != null)
                    {
                        if (method.IsGenericInstance || method.HasGenericParameters || method.Parameters.Count > 0)
                        {
                            return false;
                        }

                        currentArg = method.ReturnType.Resolve();

                        return true;
                    }
                }

                if (currentArg.IsClass && currentArg.HasFields)
                {
                    foreach (FieldDefinition field in currentArg.Fields)
                    {
                        if (!string.Equals(field.Name, target, StringComparison.CurrentCultureIgnoreCase))
                        {
                            continue;
                        }

                        currentArg = field.FieldType.Resolve();

                        return true;
                    }
                }

                if (currentArg.HasProperties)
                {
                    foreach (PropertyDefinition property in currentArg.Properties)
                    {
                        if (!string.Equals(property.Name, target, StringComparison.CurrentCultureIgnoreCase))
                        {
                            continue;
                        }

                        currentArg = property.PropertyType.Resolve();

                        return true;
                    }
                }

                if (currentArg.HasInterfaces)
                {
                    foreach (TypeReference interfaceType in currentArg.Interfaces)
                    {
                        TypeDefinition previous = currentArg;
                        currentArg = interfaceType.Resolve();

                        if (GetMember(originalMethod, ref currentArg, target))
                        {
                            return true;
                        }

                        currentArg = previous;
                    }
                }

                if (currentArg.BaseType != null && originalMethod.Module.Assembly != currentArg.BaseType.Module.Assembly)
                {
                    TypeReference baseType = currentArg.BaseType;
                    string scopeName = baseType.Scope.Name;

                    AssemblyDefinition baseTypeAssembly = AssemblyDefinition.ReadAssembly($"{_targetDirectory}\\{scopeName}{(scopeName.EndsWith(".dll") ? "" : ".dll")}");

                    currentArg = baseTypeAssembly.MainModule.Types.Single(x => x.FullName == baseType.FullName);
                }
                else
                {
                    currentArg = currentArg.BaseType?.Resolve();
                }
            }

            return false;
        }

        #endregion

        private void AddThisArg(Hook hook, Dictionary<string, string> dict)
        {
            TypeDefinition type = DocsGenerator.AssemblyLoader.GetType(hook.AssemblyName, hook.TypeName);
            if (type == null)
            {
                return;
            }

            dict.Add("instance", Utility.TransformType(type.Name));
        }

        private void AddMethodArgs(MethodDefinition method, Dictionary<string, string> dict)
        {
            foreach (ParameterDefinition parameter in method.Parameters)
            {
                string parameterName = parameter.Name;

                if (parameterName == "instance" && dict.ContainsKey("instance"))
                {
                    string parameterTypeName = parameter.ParameterType.Name;
                    parameterName = $"{char.ToLower(parameterTypeName[0])}{parameterTypeName.Substring(1)}";
                }

                dict.Add(parameterName, Utility.TransformType(parameter.ParameterType.Name));
            }
        }

        private static bool IsNeverCalledInPlugin(string hook)
        {
            return hook == "InitLogging";
        }

        public enum HookType
        {
            Simple,
            Modify
        }
    }
}
