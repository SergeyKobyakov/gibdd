using System.ComponentModel;

namespace GibddApp.Forms.Driver
{
    public partial class DataView : Form
    {
        public DataView(IBindingList bindingList, string formText)
        {
            InitializeComponent();

            dataGridView.DataSource = bindingList;
            Text = formText;
        }        
    }
}
