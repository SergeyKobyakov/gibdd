using GibddApp.Db;
using GibddApp.Db.Models;
using System.ComponentModel;

namespace GibddApp.Forms
{
    internal class DriverForm: FormBase
    {
        public DriverForm(): base(            
            Tables.Driver,
            new[] 
            {
                ("License", "N вод. уд-я", false, true),
                ("Fio", "ФИО", false, false),
                ("Adres", "Адрес", false, false)
            })
        {
            Text = "Водители";
        }

        protected override void LoadData()
        {
            var drivers = Repository.GetDrivers();
            Data = new BindingList<Driver>(drivers);
        }        
    }
}
