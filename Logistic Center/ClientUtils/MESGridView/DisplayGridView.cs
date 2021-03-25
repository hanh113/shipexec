using ClientUtilsDll;
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace MESGridView
{
    public class DisplayGridView
    {
        private int irowPerPage = 500;

        public DisplayGridView()
        {
        }

        public DataGridView GetGridView(DataGridView dataGridView, string sSQL, out Cache memoryCache)
        {
            DataGridView dataGridView1;
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            this.GetPerPage();
            try
            {
                DataRetriever dataRetriever = new DataRetriever(sSQL, (object[][])null);
               	memoryCache = new Cache(dataRetriever, this.irowPerPage);
                foreach (DataColumn column in dataRetriever.Columns)
                {
                    dataGridView.Columns.Add(column.ColumnName, column.ColumnName);
                    dataGridView.Columns[dataGridView.Columns.Count - 1].ValueType = column.DataType;
                }
                dataGridView.RowCount = dataRetriever.RowCount;
                dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                dataGridView1 = dataGridView;
            }
            catch
            {
                memoryCache = null;
                dataGridView1 = null;
            }
            return dataGridView1;
        }

        public void GetGridView(DataGridView dataGridView, string sSQL, object[] param, out Cache memoryCache)
        {
            this.GetGridView(dataGridView, sSQL, (object[][])param[0], out memoryCache);
        }

        public void GetGridView(DataGridView dataGridView, string sSQL, object[][] Params, out Cache memoryCache)
        {
            this.GetPerPage();
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            try
            {
                DataRetriever dataRetriever = new DataRetriever(sSQL, Params);
                memoryCache = new Cache(dataRetriever, this.irowPerPage);
                foreach (DataColumn column in dataRetriever.Columns)
                {
                    dataGridView.Columns.Add(column.ColumnName, column.ColumnName);
                    dataGridView.Columns[dataGridView.Columns.Count - 1].ValueType = column.DataType;
                }
                dataGridView.RowCount = dataRetriever.RowCount;
                dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            }
            catch
            {
                memoryCache = null;
            }
        }

        private void GetPerPage()
        {
            DataSet dataSet = ClientUtils.ExecuteSQL("SELECT PARAM_VALUE FROM SAJET.SYS_BASE_PARAM WHERE PROGRAM = 'ALL' AND PARAM_NAME = 'MES DATA GRID PER PAGE' AND ROWNUM = 1 ");
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                this.irowPerPage = int.Parse(dataSet.Tables[0].Rows[0][0].ToString());
            }
        }
    }
}