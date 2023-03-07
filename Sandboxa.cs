using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Runtime.CompilerServices;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Sandboxa
{
    public class Sandboxa : MarshalByRefObject
    {

        public static void Main(string [] args)
        {
            Program program = new Program();
            program.consoleAppDomain(args);
        }
        public void ExecuteUntrustedCode(string assemblyName, string typeName, string entryPoint)
        {

            Sandboxa sandboxa = new Sandboxa();
            //.WriteLine(sandboxa.pathToUntrusted);
            //Console.WriteLine(assemblyName + "\n" + typeName + " \n" + entryPoint);

            //foreach (object obj in parameters)
            //{
            //    Console.WriteLine("Parameter: " + obj.ToString());
            //}
            MethodInfo target = Assembly.Load(assemblyName).EntryPoint;
            try
            {
                Object retVal = target.Invoke(null, null);

            }
            //catch (Exception ex)
            //{
            //    new PermissionSet(PermissionState.Unrestricted).Assert();
            //    if (ex.ToString().Contains("System.Security.Permissions.FileIOPermission"))
            //    {
            //        Console.WriteLine("File access permission required. Use required permission flags or sandboxa /? for manual.");
            //        MessageBox.Show("File access permission required. Use required permission flags or sandboxa /? for manual.");
            //    }
            //    //else if (ex.ToString().Contains("Request for the permission of type 'System.Net.WebPermission"))
            //    //{
            //    //    Console.WriteLine("Network permission required. Please use required permission flags.");
            //    //    MessageBox.Show("Network permission required. Please use required permission flags.");
            //    //}
            //    else if (ex.ToString().Contains("Request for the permission of type 'System.Security.Permissions.UIPermission"))
            //    {
            //        Console.WriteLine("UI permission required. Please use required permission flags.");
            //        MessageBox.Show("UI permission required. Please use required permission flags.");
            //    }
            //    else
            //    {
            //        Console.WriteLine("SecurityException caught:\n{0}", ex.ToString());
            //        //Console.WriteLine("Permission Denied. Please use required permission flags.");
            //        CodeAccessPermission.RevertAssert();

            //    }
            //}

            catch (Exception ex)
            {
                Console.WriteLine("SecurityException caught:\n{0}", ex.ToString());
            }
        }
    }
}


