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
            using (var db = new GibddDbContext())
            {
                var drivers = db.Drivers.OrderBy(c => c.License).ToList();                
                return new BindingList<Driver>(drivers);
            }
        }        
    }
}
