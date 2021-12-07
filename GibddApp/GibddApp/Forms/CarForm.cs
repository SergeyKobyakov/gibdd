using GibddApp.Db;
using GibddApp.Db.Models;
using System.ComponentModel;

namespace GibddApp.Forms
{
    internal class CarForm: FormBase
    {
        public CarForm()
        {
            Text = "Car";
        }

        protected override IBindingList? LoadData()
        {
            using (var db = new GibddDbContext())
            {
                var cars = db.Cars.OrderBy(c => c.Gosno).ToList();
                return new BindingList<Car>(cars);
            }
        }

        protected override void DataGridSetup()
        {
            dataGridView.Columns["Gosno"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView.Columns["Gosno"].DisplayIndex = 0;

            dataGridView.Columns["Brand"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView.Columns["Brand"].DisplayIndex = 1;

            dataGridView.Columns["Model"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView.Columns["Model"].DisplayIndex = 2;

            dataGridView.Columns["YearCar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView.Columns["YearCar"].DisplayIndex = 3;

            dataGridView.Columns["Color"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView.Columns["Color"].DisplayIndex = 4;

            dataGridView.Columns["License"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["License"].DisplayIndex = 5;
        }
    }
}
