using GibddApp.Db;
using System.ComponentModel;

namespace GibddApp.Forms
{
    internal partial class FormBase : Form
    {
        protected readonly Repository Repository = new Repository();
        protected readonly string TableName;
        protected int RowCount = 0;

        private HashSet<int> ReadOnlyKeyColumnIndexes = new();
        
        protected IBindingList Data;

        private object previousValue = null;

        public FormBase(string tableName, (string, string)[] columnNames, string[] readOnlyColumnNames)
        {
            InitializeComponent();

            TableName = tableName;

            Data = LoadData();

            dataGridView.DataSource = Data;
            RowCount = Data?.Count ?? 0;

            dataGridView.AutoGenerateColumns = false;
            
            SetupColumns(columnNames);
            AddReadOnlyKeyColumnIndexes(readOnlyColumnNames);
            SetupRights();
        }
        
        protected virtual IBindingList LoadData()
        {
            return new BindingList<object>();
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
            if (e.RowIndex < RowCount && ReadOnlyKeyColumnIndexes.Contains(e.ColumnIndex))
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
        private void AddReadOnlyKeyColumnIndexes(string[] columnNames)
        {
            foreach (var columnName in columnNames)
                ReadOnlyKeyColumnIndexes.Add(dataGridView.Columns[columnName].Index);
        }

        protected void SetupColumns((string, string)[] columnNames)
        {
            for (var i = 0; i < columnNames.Length; i++)
            {
                var column = dataGridView.Columns[columnNames[i].Item1];
                column.HeaderText = columnNames[i].Item2;

                column.AutoSizeMode = i < columnNames.Length - 1
                    ? DataGridViewAutoSizeColumnMode.DisplayedCells
                    : DataGridViewAutoSizeColumnMode.Fill;

                column.DisplayIndex = i;
            }            
        }
        #endregion
    }
}
