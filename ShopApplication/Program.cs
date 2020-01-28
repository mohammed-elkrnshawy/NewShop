using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (ProcessorID()== ReadFile())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Shared_Login_Form());
            }
            else
            {
                MessageBox.Show("من فضلك تأكد من الجهاز");
            }
           
        }

        private static string ProcessorID()
        {
            String cpuid = "";
            ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select ProcessorID From Win32_processor");
            ManagementObjectCollection mbsList = mbs.Get();
            foreach (ManagementObject mo in mbsList)
            {
                cpuid = mo["ProcessorID"].ToString();
            }

            return cpuid;
        }

        private static string ReadFile()
        {
            string textFile = Application.StartupPath + @"\VIP.txt";
            string fileText = "";
            fileText = File.ReadAllText(textFile);
            return fileText;
        }
    }
}
