using Oxide.Patcher.Hooks;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Oxide.Patcher
{
    internal static class Program
    {
        public static Project PatchProject;

        // defines for commandline output
        [DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int dwProcessId);

        private const int ATTACH_PARENT_PROCESS = -1;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            AppDomain.CurrentDomain.AssemblyResolve += (sender, args1) =>
            {
                string resourceName = $"Oxide.Patcher.Dependencies.{new AssemblyName(args1.Name).Name}.dll";
                if (resourceName.Contains("resources.dll"))
                {
                    return null;
                }

                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    byte[] assemblyData = new byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };

            if (args.Length == 0 || !Array.Exists(args, x => x == "-c"))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new PatcherForm());
                return;
            }

            RunHeadless(args);
        }

        private static void RunHeadless(string[] args)
        {
            string fileName = "Rust.opj";
            bool unflagAll = false;
            string targetOverride = string.Empty;

            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    // redirect console output to parent process;
                    // must be before any calls to Console.WriteLine()
                    AttachConsole(ATTACH_PARENT_PROCESS);
                    break;
            }

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];

                if (arg.Contains("-unflag"))
                {
                    unflagAll = true;
                    continue;
                }

                if (!arg.StartsWith("-") && arg.EndsWith(".opj"))
                {
                    fileName = arg;
                    continue;
                }

                if (arg.Contains("-p"))
                {
                    try
                    {
                        string nextArg = args[i + 1];
                        if (nextArg.StartsWith("-"))
                        {
                            throw new Exception();
                        }

                        if (!nextArg.EndsWith(".opj"))
                        {
                            targetOverride = nextArg;
                            i++;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("ERROR: -p requires a file path.");
                        return;
                    }

                    continue;
                }

                Console.WriteLine($"ERROR: Unknown or invalid option: {arg}");
                return;
            }

            if (!string.IsNullOrEmpty(targetOverride) && !Directory.Exists(targetOverride))
            {
                Console.WriteLine($"{targetOverride} does not exist!");
                return;
            }

            if (!File.Exists(fileName))
            {
                Console.WriteLine($"{fileName} does not exist!");
                return;
            }

            PatchProject = Project.Load(fileName, targetOverride);

            if (unflagAll)
            {
                UnflagAll(PatchProject, fileName);
            }

            try
            {
                Patching.Patcher patcher = new Patching.Patcher(PatchProject, true);
                patcher.Patch();
            }
            catch (Exception e)
            {
                Console.WriteLine("There was an error while patching: {0}", e);
            }

            Console.WriteLine("Press Enter to continue...");
        }

        private static void UnflagAll(Project project, string filename)
        {
            bool updated = false;
            foreach (Hook hook in project.Manifests.SelectMany(m => m.Hooks))
            {
                if (!hook.Flagged)
                {
                    continue;
                }

                hook.Flagged = false;
                updated = true;

                Console.WriteLine($"Hook '{hook.HookName}' has been unflagged.");
            }

            if (updated)
            {
                project.Save(filename);
            }
        }
    }
}
