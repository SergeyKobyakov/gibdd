using GibddApp.Db;
using GibddApp.Db.Models;
using System.ComponentModel;

namespace GibddApp.Forms
{
    internal class ProtocolForm : FormBase
    {
        public ProtocolForm() : base(         
            Tables.Protocol,
            new[]
            {
                ("NoProtocol", "N протокола", false, true),
                ("CodeVio", "Код нарушения", true, false),
                ("DateVio", "Дата нарушения", false, false),
                ("TimeVio", "Время нарушения", false, false),
                ("License", "N вод. уд-я", true, false)
            })
        {
            Text = "Протоколы";
        }

        protected override void LoadData()
        {
            var protocols = Repository.GetProtocols();
            Data = new BindingList<Protocol>(protocols);
        }

        protected override void LoadLinkedData()
        {
            var violations = Repository.GetViolations().Select(c => c.CodeVio).ToList();
            var codeVioColumn = dataGridView.Columns["CodeVio"] as DataGridViewComboBoxColumn;
            codeVioColumn.DataSource = violations;
            
            var drivers = Repository.GetDrivers().Select(c => c.License).ToList();
            var licenseColumn = dataGridView.Columns["License"] as DataGridViewComboBoxColumn;
            licenseColumn.DataSource = drivers;

            dataGridView.DefaultValuesNeeded += (object? sender, DataGridViewRowEventArgs e) =>
            {
                e.Row.Cells[codeVioColumn.Name].Value = violations[0];
                e.Row.Cells[licenseColumn.Name].Value = drivers[0];
            };
        }
    }
}
