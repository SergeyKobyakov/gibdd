using GibddApp.Db;
using GibddApp.Models;
using System.ComponentModel;

namespace GibddApp.Forms
{
    internal class UserForm: FormBase
    {
        public UserForm(): base(            
            Tables.Driver,
            new[] 
            {
                ("UserName", "Имя пользователя", false, true),
                ("IsAdmin", "Является администратором", false, true),
                ("Password", "Пароль", false, true)
            })
        {
            Text = "Пользователи";                 
        }

        protected override void LoadData()
        {
            var allUserPriveleges = Repository.GetAllUserPrivileges();
            var users = allUserPriveleges.DistinctBy(c => c.UserName)
                .Select(i => new User { UserName = i.UserName })
                .ToList();

            foreach(var user in users)
            {
                var userPriveleges = allUserPriveleges.Where(p => p.UserName == user.UserName)
                    .Select(i => i.Privilege)
                    .ToHashSet();

                user.IsAdmin = userPriveleges.Contains("S")
                    && userPriveleges.Contains("U")
                    && userPriveleges.Contains("D")
                    && userPriveleges.Contains("R")
                    && userPriveleges.Contains("I");
            }

            Data = new BindingList<User>(users);     
        }

        protected override void AddRow(int rowIndex, int columnIndex)
        {
            dataGridView[columnIndex, rowIndex].Value = dataGridView[columnIndex, rowIndex].EditedFormattedValue;

            var newUser = (User)Data[rowIndex];

            if (string.IsNullOrWhiteSpace(newUser.UserName) || string.IsNullOrWhiteSpace(newUser.Password))
            {
                Messages.ShowErrorMessage("Нужно заполнить имя и пароль пользователя");

                if (Data.Count >= rowIndex + 1)
                    Data.RemoveAt(rowIndex);

                return;
            }

            if (!Repository.CreateUser(newUser))
            {
                if (Data.Count >= rowIndex + 1)
                    Data.RemoveAt(rowIndex);                
            };

            RowCount++;
        }

        protected override void DeleteRow(int rowIndex)
        {
            if (Repository.DropUser((User)Data[rowIndex]))
                RowCount--;
        }
    }
}
