using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Forms;

namespace Sandboxa
{
    public class Program : Sandboxa
    {
        // Retrieve the selected items from the CheckedListBox control
        public List<string> selectedItems { get; set; } = new List<string>();
        //const string pathToUntrusted = @"..\..\..\UntrustedCode\bin\Debug";
        public string pathToUntrusted { get; set; }
        public string untrustedAssembly { get; set; }
        static string exePath { get; set; }
        static string exeName { get; set; }

        static string untrustedClass;
        static string entryPoint;

        Object[] parameters = { };
        public static string[] args { get; set; }

        public void consoleAppDomain( string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UI());
            if (args.Length == 0)
            {
                Console.WriteLine("\nFor more information on how to use, type Sanboxa /? or Sandboxa Start. to launch GUI.");
                return;

            }
            else if (args[0].Contains("?"))
            {

                //Console.WriteLine("\nSandboxa Help Screen\n---------------------------------------------------------------\nBy default all permissions are disabled. Use flags as required.\n\nRead/Write Access:         -f\nNetwork Access:            -n\nExecution Access:          -e\nUI Access:                 -ui\nFile Manager:              -fm\nClipboard Access:              -cb\n\nUsage 1: sandboxa <filepath> <filename> <flag>\nYou can also combine flags.");
                Console.WriteLine("\nSandboxa Help Screen\n---------------------------------------------------------------\nBy default all permissions are disabled. Use flags as required.\nPermissions\t\t\tFlags\t\t\tDescription\n");
                Console.WriteLine("Execution Access\t\t-f\t\t\tgrant program permission to run on your pc");
                Console.WriteLine("File Access\t\t\t-n\t\t\tgrant permission to give access to read and write to your files");
                Console.WriteLine("Network Access\t\t\t-n\t\t\tgrant network/internet access");
                Console.WriteLine("User Interface Access\t\t-ui\t\t\tgrant permission to launch a GUI app");
                Console.WriteLine("File dialog Access\t\t-fd\t\t\tgrant permission to open a file dialog");
                Console.WriteLine("Reflection Access\t\t-r\t\t\tgrant permission to enable reflection");
                Console.WriteLine("Environment Access\t\t-env\t\t\tgrant permission to edit environment variables");
                Console.WriteLine("Strong Name Access\t\t-sn\t\t\tgrant permission for Strong Naming");
                Console.WriteLine("Isolated Storage Access\t\t-is\t\t\tgrant permission for Isolated Storage Access");
                Console.WriteLine("\nUsage: sandboxa <assemblypath> <assemblyname> <flags>");
            }
            else if (args[0].ToLower().Contains("start."))
            {
                UI form = new UI();
                form.ShowDialog();

            }
            else if (args.Length > 1)
            {
                exePath = args[0];
                exeName = args[1];
                string flags = string.Join("", args.Skip(2));

                Sandboxa sandboxa = new Sandboxa();
                string pathToUntrusted = exePath;
                string untrustedAssembly = exeName;

                AppDomainSetup adSetup = new AppDomainSetup();
                adSetup.ApplicationBase = Path.GetFullPath(pathToUntrusted);
                //Console.WriteLine(pathToUntrusted);

                PermissionSet permSet = new PermissionSet(PermissionState.None);

                if (flags.Contains("e"))
                {
                    permSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
                    permSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.AllFlags));
                }
                if (flags.Contains("f"))
                {
                    permSet.AddPermission(new FileIOPermission((PermissionState)FileIOPermissionAccess.Read));
                }
                if (flags.Contains("n"))
                {
                    permSet.AddPermission(new WebPermission(PermissionState.Unrestricted));
                }
                if (flags.Contains("ui"))
                {
                    permSet.AddPermission(new UIPermission(PermissionState.Unrestricted));
                    permSet.AddPermission(new UIPermission(UIPermissionWindow.SafeTopLevelWindows));
                }
                if (flags.Contains("fd"))
                {
                    permSet.AddPermission(new FileDialogPermission(FileDialogPermissionAccess.OpenSave));
                }
                if (flags.Contains("r"))
                {
                    permSet.AddPermission(new ReflectionPermission(PermissionState.Unrestricted));
                }
                if (flags.Contains("env"))
                {
                    permSet.AddPermission(new EnvironmentPermission((PermissionState)EnvironmentPermissionAccess.Read));
                }
                if (flags.Contains("sn"))
                {
                    permSet.AddPermission(new StrongNameIdentityPermission(PermissionState.Unrestricted));
                }
                if (flags.Contains("is"))
                {
                    permSet.AddPermission(new IsolatedStorageFilePermission(PermissionState.Unrestricted));
                }
                StrongName fullTrustAssembly = typeof(Sandboxa).Assembly.Evidence.GetHostEvidence<StrongName>();

                AppDomain newDomain = AppDomain.CreateDomain("Sandbox", null, adSetup, permSet, fullTrustAssembly);

                ObjectHandle handle = Activator.CreateInstanceFrom(
                    newDomain, typeof(Sandboxa).Assembly.ManifestModule.FullyQualifiedName,
                    typeof(Sandboxa).FullName
                    );
                Sandboxa newDomainInstance = (Sandboxa)handle.Unwrap();
                try
                {
                    //Console.WriteLine(parameters.GetType().ToString());
                    newDomainInstance.ExecuteUntrustedCode(untrustedAssembly, untrustedClass, entryPoint, parameters);
                }
                catch (FileLoadException ex)
                {
                    Console.WriteLine("Execution permission required. Please use required permission flags.");
                    MessageBox.Show("Execution permission required. Please use required permission flags.");
                    //Console.WriteLine(ex);

                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine("Program not found. Please check and correct.");
                    MessageBox.Show("Program not found. Please check and correct.");
                    //Console.WriteLine(ex);
                }

                catch (Exception ex)
                {
                    Console.WriteLine("SecurityException caught:\n{0}", ex.ToString());
                    MessageBox.Show("SecurityException caught:\n{0}", ex.ToString());
                }
            }
            else
            {
                Console.WriteLine("\nFor more information on how to use, type Sanboxa /? or Sandboxa Start. to launch GUI.");
                return;
            }
        }

        public void UiAppDomain(string pathToUntrusted, string untrustedAssembly, string untrustedClass, string entryPoint)
        {
            //Sandboxa sandboxa = new Sandboxa();
            //string pathToUntrusted = sandboxa.pathToUntrusted;
            //string untrustedAssembly = sandboxa.untrustedAssembly;

            AppDomainSetup adSetup = new AppDomainSetup();
            adSetup.ApplicationBase = Path.GetFullPath(pathToUntrusted);
            //Console.WriteLine(pathToUntrusted);

            PermissionSet permSet = new PermissionSet(PermissionState.None);

            // Loop through the selected items and add the corresponding permission to the permission set
            foreach (var item in selectedItems)
            {
                //Console.WriteLine(item);
                switch (item.ToLower())
                {
                    case "execution access":
                        permSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
                        break;
                    case "file access":
                        permSet.AddPermission(new FileIOPermission((PermissionState)FileIOPermissionAccess.Read));
                        break;
                    case "network access":
                        permSet.AddPermission(new WebPermission(PermissionState.Unrestricted));
                        break;
                    case "user interface access":
                        permSet.AddPermission(new UIPermission(PermissionState.Unrestricted));
                        permSet.AddPermission(new UIPermission(UIPermissionWindow.SafeTopLevelWindows));
                        break;
                    case "file dialog access":
                        permSet.AddPermission(new FileDialogPermission(FileDialogPermissionAccess.OpenSave));
                        break;
                    case "reflection access":
                        permSet.AddPermission(new ReflectionPermission(PermissionState.Unrestricted));
                        break;
                    case "environment access":
                        permSet.AddPermission(new EnvironmentPermission((PermissionState)EnvironmentPermissionAccess.Read));
                        break;
                    case "strong name access":
                        permSet.AddPermission(new StrongNameIdentityPermission(PermissionState.Unrestricted));
                        break;
                    case "isolated storage access":
                        permSet.AddPermission(new IsolatedStorageFilePermission(PermissionState.Unrestricted));
                        break;
                    //case "fm2":
                    //    permSet.AddPermission(new FileDialogPermission(FileDialogPermissionAccess.Save));
                    //    break;
                    default:
                        break;
                }
            }

            StrongName fullTrustAssembly = typeof(Sandboxa).Assembly.Evidence.GetHostEvidence<StrongName>();

            AppDomain newDomain = AppDomain.CreateDomain("Sandbox", null, adSetup, permSet, fullTrustAssembly);

            ObjectHandle handle = Activator.CreateInstanceFrom(
                newDomain, typeof(Sandboxa).Assembly.ManifestModule.FullyQualifiedName,
                typeof(Sandboxa).FullName
                );
            Sandboxa newDomainInstance = (Sandboxa)handle.Unwrap();
            try
            {
                newDomainInstance.ExecuteUntrustedCode(untrustedAssembly, untrustedClass, entryPoint, parameters);
            }
            catch (FileLoadException ex)
            {
                Console.WriteLine("Execution permission required. Please use required permission flags.");
                MessageBox.Show("Execution permission required. Please use required permission flags.");

            }
            catch (FileNotFoundException ex)
            {
                //Console.WriteLine(ex);
                Console.WriteLine("Program not found. Please check and correct.");
                MessageBox.Show("Program not found. Please check and correct.");
            }
        }

    }
}
