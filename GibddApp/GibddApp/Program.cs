using GibddApp.Forms;

namespace GibddApp
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

            using (var loginForm = new LoginForm())
            {
                var dr = loginForm.ShowDialog();
                if (dr != DialogResult.OK)
                    return;
            }                

            Application.Run(new MainForm());
        }
    }
}