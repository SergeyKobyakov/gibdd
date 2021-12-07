using GibddApp.Db;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace GibddApp.Forms
{
    public partial class FormBase : Form
    {
        private HashSet<int> PrimaryKeyColumnIndexes = new();
        private int rowCount = 0;
        protected IBindingList Data;

        public FormBase(string[] columnNames, string[] primaryKeyColumnNames)
        {
            InitializeComponent();

            Data = LoadData();
            dataGridView.DataSource = Data;
            rowCount = Data?.Count ?? 0;

            dataGridView.AutoGenerateColumns = false;
            
            SetUpColumns(columnNames);
            AddPrimaryKeyColumnIndexes(primaryKeyColumnNames);
        }
        
        protected virtual IBindingList LoadData()
        {
            return new BindingList<object>();
        }

        #region DatabaseUpdate
        protected virtual void UpdateEntity(int changedRowIndex)
        {
            using (var db = new GibddDbContext())
            {
                db.Entry(Data[changedRowIndex]).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        protected virtual void AddNewEntity(int addedRowIndex)
        {
            using (var db = new GibddDbContext())
            {
                db.Add(Data[addedRowIndex]);
                SaveChanges(db);
            }
        }

        protected virtual void DeleteEntity(int deletedRowIndex)
        {
            using (var db = new GibddDbContext())
            {
                db.Remove(Data[deletedRowIndex]);
                SaveChanges(db);
            }
        }

        private void SaveChanges(GibddDbContext db)
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка обновления базы данных");
            }
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

        protected void SetUpColumns(string[] columnNames)
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
