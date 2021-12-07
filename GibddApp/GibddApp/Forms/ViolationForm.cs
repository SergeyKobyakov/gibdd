using GibddApp.Db;
using GibddApp.Db.Models;
using System.ComponentModel;

namespace GibddApp.Forms
{
    internal class ViolationForm : FormBase
    {
        public ViolationForm()
        {
            Text = "Violation";
        }

        protected override IBindingList? LoadData()
        {
            using (var db = new GibddDbContext())
            {
                var violation = db.Violations.OrderBy(c => c.CodeVio).ToList();
                return new BindingList<Violation>(violation);
            }
        }

        protected override void DataGridSetup()
        {
            dataGridView.Columns["CodeVio"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView.Columns["CodeVio"].DisplayIndex = 0;

            dataGridView.Columns["NameVio"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView.Columns["NameVio"].DisplayIndex = 1;

            dataGridView.Columns["Article"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView.Columns["Article"].DisplayIndex = 2;

            dataGridView.Columns["Sanction"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["Sanction"].DisplayIndex = 3;
        }
    }
}
