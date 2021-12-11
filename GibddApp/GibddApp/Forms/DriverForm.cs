using GibddApp.Db;
using GibddApp.Db.Models;
using System.ComponentModel;

namespace GibddApp.Forms
{
    internal class DriverForm: FormBase
    {
        public DriverForm(): base(            
            new[] 
            {
                "License",
                "Fio",
                "Adres"
            },
            new[] 
            {
                "License"
            })
        {
            Text = "Driver";
        }

        protected override IBindingList LoadData()
        {
            var drivers = Repository.GetDrivers();
            return new BindingList<Driver>(drivers);
        }        
    }
}
