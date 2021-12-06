using GibddApp.Db;
using Microsoft.Extensions.DependencyInjection;

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
            var services = new ServiceCollection();
            
            using (var db = new GibddDbContext("database=localhost:/database/GIBDD.fdb;user=sysdba;password=000000"))
            {
                var drivers = db.Drivers.ToList();
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}