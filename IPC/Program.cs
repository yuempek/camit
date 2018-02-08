using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Win32;
using System.IO;
using System.Runtime.InteropServices;

namespace IPC
{
    static class Program
    {
        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

        public static Mutex WriterMutex;
        public static List<Thread> Threads = new List<Thread>();
        public static String FileExtention = ".ipc";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                // Association();
                WriterMutex = new Mutex();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (args.Length == 0) Application.Run(new MainForm());
                else Application.Run(new MainForm(args[0]));
                WriterMutex.Close();
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                try 
	            {
                    FileStream fs = new FileStream(Path.Combine(Application.StartupPath, "logs.txt"), FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(e.Source);
                    sw.WriteLine(e.Message);
                    sw.WriteLine(e.StackTrace);
                    sw.WriteLine("#######################################################################");
                    sw.Close();
                    fs.Close();
	            }
	            finally
	            {
	            }
                
            }
        }

        public static void Association() 
        {
            //if (!IsAssociated(FileExtention)) 
            {
                SetAssociation(FileExtention, Path.GetFileNameWithoutExtension(Application.ExecutablePath) + FileExtention, Application.ExecutablePath, "List of the CAM-it cameras.");
            }
        }

        public static bool IsAssociated(string Extension) 
        {
            return (Registry.ClassesRoot.OpenSubKey(Extension) != null);
        }

        public static void RemoveAssociation(string Extension, string KeyName, string OpenWith, string FileDescription)
        {
           // CurrentUser = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\.ucs", true);
           // CurrentUser.DeleteSubKey("UserChoice", false);
           // CurrentUser.Close();
           // SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
        }

        public static void SetAssociation(string Extension, string KeyName, string OpenWith, string FileDescription)
        {
            RegistryKey BaseKey = Registry.ClassesRoot.CreateSubKey(Extension);
            RegistryKey OpenMethod = Registry.ClassesRoot.CreateSubKey(KeyName);
            RegistryKey Shell = OpenMethod.CreateSubKey("Shell");
            RegistryKey CurrentUser = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\" + Extension);

            BaseKey.SetValue("", KeyName);

            OpenMethod.SetValue("", FileDescription);
            OpenMethod.CreateSubKey("DefaultIcon").SetValue("", "\"" + OpenWith + "\",0");
            
            //Shell.CreateSubKey("edit").CreateSubKey("command").SetValue("", "\"" + OpenWith + "\"" + " \"%1\"");
            Shell.CreateSubKey("open").CreateSubKey("command").SetValue("", "\"" + OpenWith + "\"" + " \"%1\"");

            CurrentUser.CreateSubKey("UserChoice");
            CurrentUser = CurrentUser.OpenSubKey("UserChoice", RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);
            CurrentUser.SetValue("Progid", KeyName, RegistryValueKind.String);

            BaseKey.Close();
            OpenMethod.Close();
            Shell.Close();
            CurrentUser.Close();

            SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
        }
    }
}