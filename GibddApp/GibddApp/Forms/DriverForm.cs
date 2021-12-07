using GibddApp.Db;
using GibddApp.Db.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GibddApp.Forms
{
    internal class DriverForm: FormBase
    {
        public DriverForm()
        {
            Text = "Driver";
        }

        protected override IBindingList? LoadData()
        {
            using (var db = new GibddDbContext())
            {
                var drivers = db.Drivers.OrderBy(c => c.License).ToList();
                return new BindingList<Driver>(drivers);
            }
        }

        protected override void DataGridSetup()
        {
            dataGridView.Columns["License"].ReadOnly = true;
            dataGridView.Columns["License"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView.Columns["FIO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView.Columns["ADRES"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
