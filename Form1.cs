using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPUCreaker
{
    public partial class Form1 : Form
    {
        private static string intro = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public Form1()
        {
            InitializeComponent();
            if (File.Exists(intro + "STARTUP")) button2.Text = "关闭自起";
            GC.Collect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                RegistryKey key2 = Registry.LocalMachine.OpenSubKey("HARDWARE", true).OpenSubKey("DESCRIPTION", true).OpenSubKey("System", true).OpenSubKey("CentralProcessor", true).OpenSubKey("0", true);
                File.WriteAllText(intro + "backup",key2.GetValue("ProcessorNameString").ToString());
                key2.SetValue("ProcessorNameString", textBox1.Text);
                File.WriteAllText(intro + "Info", textBox1.Text);
                MessageBox.Show("成功!", "Tips 提示", MessageBoxButtons.OK, MessageBoxIcon.Information) ;
            }
            catch (Exception) { MessageBox.Show("更改失败!", "Tips 提示", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
            GC.Collect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!File.Exists(intro + "STARTUP")) { Utils.RegisterAutoStartUP("cpucreaker", GetType().Assembly.Location); File.Create(intro + "STARTUP"); MessageBox.Show("成功!", "Tips 提示", MessageBoxButtons.OK, MessageBoxIcon.Information); button2.Text = "关闭自起"; }
            else { Utils.CancelAutoStartUP("cpucreaker");File.Delete(intro + "STARTUP"); MessageBox.Show("成功!", "Tips 提示", MessageBoxButtons.OK, MessageBoxIcon.Information); button2.Text = "开机自起"; }
            GC.Collect();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try { 
            MessageBox.Show("这会将你的CPU型号更改成你修改之前的值!", "Tips 提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RegistryKey key2 = Registry.LocalMachine.OpenSubKey("HARDWARE", true).OpenSubKey("DESCRIPTION", true).OpenSubKey("System", true).OpenSubKey("CentralProcessor", true).OpenSubKey("0", true);
            key2.SetValue("ProcessorNameString", File.ReadAllText(intro + "backup"));
                MessageBox.Show("成功!", "Tips 提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception) { MessageBox.Show("更改失败!", "Tips 提示", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
        }
    }

    class Utils
    {
        /// <summary>
        /// 注册开机自启动
        /// </summary>
        /// <param name="KeyName"></param>
        /// <param name="ButtinPath"></param>
        public static void RegisterAutoStartUP(string KeyName, string ButtinPath)
        {

            using (RegistryKey rk = Registry.LocalMachine)
            {
                RegistryKey runKey = rk.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (runKey.Equals(null))
                {
                    runKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                }

                try
                {
                    runKey.SetValue(KeyName, ButtinPath);
                    runKey.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    runKey.Close();
                }

            }
        }

        /// <summary>
        /// 删除开机自启动 
        /// </summary>
        /// <param name="ButtinPath">执行文件路径</param>
        public static void CancelAutoStartUP(string KeyName)//
        {
            using (RegistryKey reg = Registry.LocalMachine)
            {
                RegistryKey runKey = reg.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (runKey.Equals(null))
                {
                    runKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                }
                try
                {
                    runKey.DeleteValue(KeyName, true);
                    runKey.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    runKey.Close();
                }
            }
        }
    }
}
