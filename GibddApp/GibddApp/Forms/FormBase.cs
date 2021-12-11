using GibddApp.Db;
using System.ComponentModel;

namespace GibddApp.Forms
{
    internal partial class FormBase : Form
    {
        protected readonly Repository Repository = new Repository();

        protected readonly string TableName;

        private HashSet<int> PrimaryKeyColumnIndexes = new();
        private int rowCount = 0;
        protected IBindingList? Data;

        public FormBase(string tableName, string[] columnNames, string[] primaryKeyColumnNames)
        {
            InitializeComponent();

            TableName = tableName;

            Data = LoadData();

            dataGridView.DataSource = Data;
            rowCount = Data?.Count ?? 0;

            dataGridView.AutoGenerateColumns = false;
            
            SetupColumns(columnNames);
            AddPrimaryKeyColumnIndexes(primaryKeyColumnNames);
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

        #region DatabaseUpdate
        protected virtual void UpdateEntity(int changedRowIndex)
        {
            Repository.Update(Data[changedRowIndex]);
        }

        protected virtual void AddNewEntity(int addedRowIndex)
        {
            Repository.Add(Data[addedRowIndex]);
        }

        protected virtual void DeleteEntity(int deletedRowIndex)
        {
            Repository.Delete(Data[deletedRowIndex]);
        }        
        #endregion

        #region Events
        private void driverDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == rowCount)
                return;

            UpdateEntity(e.RowIndex);
        }

        private void dataGridView_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (Data.Count == rowCount || e.RowIndex < rowCount - 1)                
                return;

            AddNewEntity(e.RowIndex);
            rowCount++;
        }

        private void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < rowCount && PrimaryKeyColumnIndexes.Contains(e.ColumnIndex))
                e.Cancel = true;
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DeleteEntity(e.Row.Index);
            rowCount--;
        }
        #endregion

        #region SetupColumns
        private void AddPrimaryKeyColumnIndexes(string[] columnNames)
        {
            foreach (var columnName in columnNames)
                PrimaryKeyColumnIndexes.Add(dataGridView.Columns[columnName].Index);
        }

        protected void SetupColumns(string[] columnNames)
        {
            for (var i = 0; i < columnNames.Length; i++)
            {
                var column = dataGridView.Columns[columnNames[i]];

                column.AutoSizeMode = i < columnNames.Length - 1
                    ? DataGridViewAutoSizeColumnMode.DisplayedCells
                    : DataGridViewAutoSizeColumnMode.Fill;

                column.DisplayIndex = i;
            }            
        }
        #endregion
    }
}
