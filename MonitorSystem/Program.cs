using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitorSystem
{
    static class Program
    {
        /// <summary>
        /// �D���x���ε{��DOS��l��
        /// </summary>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        public static extern void FreeConsole();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AllocConsole();
            Application.Run(new FmMain());
        }
    }
}
