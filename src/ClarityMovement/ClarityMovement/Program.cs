namespace ClarityMovement
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            ApplicationConfiguration.Initialize();
            Clarity.ClarityLog.Init(Clarity.EClarityLogLevel.Debug, Clarity.EClarityLogMode.Console);
            Application.Run(new MainForm());
        }
    }
}