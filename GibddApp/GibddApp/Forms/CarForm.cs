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
                ("Gosno", "Гос. номер", false, true),
                ("Brand", "Марка", false, false),
                ("Model", "Модель", false, false),
                ("YearCar", "Год выпуска", false, false),
                ("Color", "Цвет", false, false),
                ("License", "N вод. уд-я", true, false)
            })
        {
            Text = "Автомобили";
        }

        protected override void LoadData()
        {
            var cars = Repository.GetCars();
            Data = new BindingList<Car>(cars);
        }

        protected override void LoadLinkedData()
        {
            var drivers = Repository.GetDrivers().Select(c => c.License).ToList();
            var licenseColumn = dataGridView.Columns["License"] as DataGridViewComboBoxColumn;
            licenseColumn.DataSource = drivers;

            dataGridView.DefaultValuesNeeded += (object? sender, DataGridViewRowEventArgs e) =>
            {                
                e.Row.Cells[licenseColumn.Name].Value = drivers[0];
            };
        }
    }
}
