using GibddApp.Db;
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

            bool isPrivilegesLoaded = false;
            do
            {
                using (var loginForm = new LoginForm())
                {
                    var dr = loginForm.ShowDialog();
                    if (dr != DialogResult.OK)
                        return;
                }

                isPrivilegesLoaded = PrivilegesLoad();

            } while (!isPrivilegesLoaded);

            Application.Run(new MainForm());
        }

        private static bool PrivilegesLoad()
        {
            var repository = new Repository();
            var userPrivileges = repository.GetCurrentUserPrivileges();
            if ((userPrivileges?.Count ?? 0) == 0)
                return false;

            foreach(var item in userPrivileges!)
            {
                if (!LoginInfo.Privileges.ContainsKey(item.TableName))
                    LoginInfo.Privileges[item.TableName] = new HashSet<string>();

                LoginInfo.Privileges[item.TableName].Add(item.Privilege);
            }

            if (LoginInfo.Login == "SYSDBA")
                LoginInfo.IsSysDba = true;

            return true;
        }
    }
}