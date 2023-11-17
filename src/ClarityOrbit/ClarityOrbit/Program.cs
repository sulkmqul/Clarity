namespace ClarityOrbit
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();
                Application.Run(new MainForm());
                
            }

            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"ClarityOrbit fatal error\n{ex.Message}", "ClarityOrbit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}