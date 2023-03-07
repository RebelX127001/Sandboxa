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
        //private static Object[] parameters = { 45 };
        public static string[] args { get; set; }

        public void consoleAppDomain( string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("\nFor more information on how to use, type Sanboxa /? or Sandboxa Start. to launch GUI.");
                return;

            }
            else if (args[0].Contains("?"))
            {

                Console.WriteLine("Sandboxa Help Screen\n---------------------------------------------------------------\nBy default all permissions are disabled. Use flags as required.\n\nRead/Write Access:         -f\nNetwork Access:            -n\nExecution Access:          -e\nUI Access:                 -ui\nFile Manager:              -fm\nClipboard Access:              -cb\n\nUsage 1: sandboxa <filepath> <filename> <flag>\nYou can also combine flags.");

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
                }
                if (flags.Contains("fm"))
                {

                    permSet.AddPermission(new FileDialogPermission(FileDialogPermissionAccess.Open));
                }
                if (flags.Contains("fm2"))
                {
                    permSet.AddPermission(new FileDialogPermission(FileDialogPermissionAccess.Save));
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
                    newDomainInstance.ExecuteUntrustedCode(untrustedAssembly, untrustedClass, entryPoint);
                }
                catch (FileLoadException ex)
                {
                    Console.WriteLine("Execution permission required. Please use required permission flags.");
                    Console.WriteLine(ex);

                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine("Program not found. Please check and correct.");
                    Console.WriteLine(ex);
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
                    case "execution":
                        permSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
                        break;
                    case "file":
                        permSet.AddPermission(new FileIOPermission((PermissionState)FileIOPermissionAccess.Read));
                        break;
                    case "network":
                        permSet.AddPermission(new WebPermission(PermissionState.Unrestricted));
                        break;
                    case "user interface":
                        permSet.AddPermission(new UIPermission(PermissionState.Unrestricted));
                        break;
                    case "file dialog":
                        permSet.AddPermission(new FileDialogPermission(FileDialogPermissionAccess.OpenSave));
                        break;
                    //case "fm2":
                    //    permSet.AddPermission(new FileDialogPermission(FileDialogPermissionAccess.Save));
                    //    break;
                    default:
                        // Handle invalid items
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
                newDomainInstance.ExecuteUntrustedCode(untrustedAssembly, untrustedClass, entryPoint);
            }
            catch (FileLoadException ex)
            {
                Console.WriteLine("Execution permission required. Please use required permission flags.");
                MessageBox.Show("Execution permission required. Please use required permission flags.");

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Program not found. Please check and correct.");
                MessageBox.Show("Program not found. Please check and correct.");
            }
        }

    }
}
