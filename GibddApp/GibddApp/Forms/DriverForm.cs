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
                ("License", "N вод. уд-я"),
                ("Fio", "ФИО"),
                ("Adres", "Адрес")
            },
            new[] 
            {
                "License"
            })
        {
            Text = "Водители";
        }

        protected override IBindingList LoadData()
        {
            var drivers = Repository.GetDrivers();
            return new BindingList<Driver>(drivers);
        }        
    }
}
