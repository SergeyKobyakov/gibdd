using GibddApp.Db;
using GibddApp.Db.Models;
using System.ComponentModel;

namespace GibddApp.Forms
{
    internal class ProtocolForm: FormBase
    {
        public ProtocolForm()
        {
            Text = "Protocol";
        }

        protected override IBindingList? LoadData()
        {
            using (var db = new GibddDbContext())
            {
                var protocol = db.Protocols.OrderBy(c => c.NoProtocol).ToList();
                return new BindingList<Protocol>(protocol);
            }
        }

        protected override void DataGridSetup()
        {
            dataGridView.Columns["NoProtocol"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView.Columns["NoProtocol"].DisplayIndex = 0;

            dataGridView.Columns["CodeVio"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView.Columns["CodeVio"].DisplayIndex = 1;

            dataGridView.Columns["DateVio"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView.Columns["DateVio"].DisplayIndex = 2;

            dataGridView.Columns["TimeVio"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView.Columns["TimeVio"].DisplayIndex = 3;

            dataGridView.Columns["License"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["License"].DisplayIndex = 4;
        }
    }
}
