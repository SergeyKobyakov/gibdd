using GibddApp.Db;
using GibddApp.Db.Models;
using System.ComponentModel;

namespace GibddApp.Forms
{
    internal class CarForm: FormBase
    {
        public CarForm(): base(            
            new[] {
                "Gosno",
                "Brand",
                "Model",
                "YearCar",
                "Color",
                "License"
            },
            new[]
            {
                "Gosno"
            })
        {
            Text = "Car";
        }

        protected override IBindingList LoadData()
        {
            using (var db = new GibddDbContext())
            {
                var cars = db.Cars.OrderBy(c => c.Gosno).ToList();
                return new BindingList<Car>(cars);
            }
        }        
    }
}
