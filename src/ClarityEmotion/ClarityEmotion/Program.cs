using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarityEmotion
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Clarity.DLL.Winmm.timeBeginPeriod(1);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

           
            Clarity.ClarityLog.Init(Clarity.EClarityLogLevel.ALL, Clarity.EClarityLogMode.Window | Clarity.EClarityLogMode.Console);            
            Application.Run(new MainForm());
        }
    }
}
