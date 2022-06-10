using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPUCreaker
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string intro = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if(args.Length > 1)
            {

            }
            else
            {
                if (File.Exists(intro + "Info") && File.Exists(intro + "STARTUP"))
                {
                    RegistryKey key2 = Registry.LocalMachine.OpenSubKey("HARDWARE", true).OpenSubKey("DESCRIPTION", true).OpenSubKey("System", true).OpenSubKey("CentralProcessor", true).OpenSubKey("0", true);
                    key2.SetValue("ProcessorNameString", File.ReadAllText(intro + "Info"));
                    return;
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GC.Collect();
            Application.Run(new Form1());
        }
    }
}
