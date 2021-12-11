using GibddApp.Db;
using GibddApp.Db.Models;
using System.ComponentModel;

namespace GibddApp.Forms
{
    internal class CarForm: FormBase
    {
        public CarForm(): base(     
            Tables.Car,
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
            Text = "Автомобили";
        }

        protected override IBindingList LoadData()
        {
            if (!LoginInfo.CanSelect(TableName))
                return new BindingList<Car>();

            var cars = Repository.GetCars();
            return new BindingList<Car>(cars);
        }        
    }
}
