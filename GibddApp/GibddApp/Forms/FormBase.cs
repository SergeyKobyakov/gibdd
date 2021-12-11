using GibddApp.Db;
using System.ComponentModel;

namespace GibddApp.Forms
{
    internal partial class FormBase : Form
    {
        protected readonly List<DataGridViewColumn> Columns;
        protected readonly Repository Repository = new Repository();
        protected readonly string TableName;
        protected int RowCount = 0;

        private HashSet<string> ReadOnlyColumnNames = new();
        
        protected IBindingList Data;

        private object previousValue = null;

        public FormBase(string tableName, 
            (string name, string header, bool linked, bool readOnly)[] columns)
        {
            InitializeComponent();
            dataGridView.AutoGenerateColumns = false;

            TableName = tableName;
            AddColumns(columns);
            
            LoadData();
            LoadLinkedData();
            
            dataGridView.DataSource = Data;
            RowCount = Data?.Count ?? 0;            
                        
            SetupRights();
        }
        
        protected virtual void LoadData()
        {            
        }

        protected virtual void LoadLinkedData()
        {
        }

        private void SetupRights()
        {
            if (!LoginInfo.CanUpdate(TableName))
                dataGridView.ReadOnly = true;

            if (!LoginInfo.CanInsert(TableName))
                dataGridView.AllowUserToAddRows = false;

            if (!LoginInfo.CanDelete(TableName))
                dataGridView.AllowUserToDeleteRows = false;
        }

        #region Events
        private void driverDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == RowCount)
                return;

            if (e.RowIndex >= 0 && !Repository.Update(Data[e.RowIndex]) && previousValue != null)
            {
                dataGridView.CurrentCell.Value = previousValue;
            }
        }

        private void dataGridView_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (Data.Count == RowCount || e.RowIndex < RowCount - 1)                
                return;
            
            AddRow(e.RowIndex, e.ColumnIndex);            
        }

        private void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < RowCount && ReadOnlyColumnNames.Contains(
                dataGridView.Columns[e.ColumnIndex].Name))
                e.Cancel = true;

            previousValue = dataGridView.CurrentCell.Value;
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row != null)
            {
                DeleteRow(e.Row.Index);
            }
        }

        protected virtual void AddRow(int rowIndex, int columnIndex)
        {
            dataGridView[columnIndex, rowIndex].Value = dataGridView[columnIndex, rowIndex].EditedFormattedValue;

            if (!Repository.Add(Data[rowIndex]))
            {
                if (Data.Count >= rowIndex + 1)
                    Data.RemoveAt(rowIndex);

                return;
            }

            RowCount++;
        }

        protected virtual void DeleteRow(int rowIndex)
        {
            Repository.Delete(Data[rowIndex]);
            RowCount--;
        }

        #endregion

        #region SetupColumns
        protected void AddColumns((string name, string header, bool linked, bool readOnly)[] columnsData)
        {
            DataGridViewColumn column = null;

            foreach(var columnData in columnsData)
            {
                column = columnData.linked
                ? new DataGridViewComboBoxColumn()
                : new DataGridViewTextBoxColumn();

                column.Name = columnData.name;
                column.DataPropertyName = columnData.name;
                column.HeaderText = columnData.header;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                dataGridView.Columns.Add(column);

                if (columnData.readOnly)
                    ReadOnlyColumnNames.Add(columnData.name);
            }            
        }
        #endregion
    }
}
