using System.ComponentModel;

namespace GibddApp.Forms
{
    public partial class FormBase : Form
    {
        public FormBase()
        {
            InitializeComponent();

            dataGridView.DataSource = LoadData();
            DataGridSetup();
        }

        protected virtual IBindingList? LoadData()
        {
            return null;
        }

        protected virtual void DataGridSetup()
        {

        }

        private void driverDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }        
    }
}
