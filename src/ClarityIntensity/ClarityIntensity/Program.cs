using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity;

namespace ClarityIntensity
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());

            /*
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            MainForm f = new MainForm();
            ClarityLoop.Run(f, () =>
            {
                
            });*/

            MainForm f = new MainForm();
            Clarity.Engine.ClarityEngine.Init(f, @"cs.xml");

            Clarity.Engine.ClarityEngine.Run(new IntensityPlugin());

        }
    }
}
