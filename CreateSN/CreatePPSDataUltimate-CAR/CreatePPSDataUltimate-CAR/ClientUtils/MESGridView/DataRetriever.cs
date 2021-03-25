using ClientUtilsDll;
using System;
using System.Collections;
using System.Data;
using System.Text;

namespace MESGridView
{
	public class DataRetriever : IDataPageRetriever
	{
		private string sSQL;

		private object[][] Params;

		private object[] param;

		private int rowCountValue = -1;

		private DataColumnCollection columnsValue;

		private string commaSeparatedListOfColumnNamesValue = null;

		public DataColumnCollection Columns
		{
			get
			{
				DataColumnCollection dataColumnCollection;
				if (this.columnsValue == null)
				{
					DataTable dataTable = new DataTable();
					dataTable = (this.param != null ? ClientUtils.Columns(this.sSQL, this.param) : ClientUtils.Columns(this.sSQL, this.Params));
					this.columnsValue = dataTable.Columns;
					dataColumnCollection = this.columnsValue;
				}
				else
				{
					dataColumnCollection = this.columnsValue;
				}
				return dataColumnCollection;
			}
		}

		private string CommaSeparatedListOfColumnNames
		{
			get
			{
				string str;
				if (this.commaSeparatedListOfColumnNamesValue == null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					bool flag = true;
					foreach (DataColumn column in this.Columns)
					{
						if (!flag)
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append(column.ColumnName);
						flag = false;
					}
					this.commaSeparatedListOfColumnNamesValue = stringBuilder.ToString();
					str = this.commaSeparatedListOfColumnNamesValue;
				}
				else
				{
					str = this.commaSeparatedListOfColumnNamesValue;
				}
				return str;
			}
		}

		public int RowCount
		{
			get
			{
				int num;
				if (this.rowCountValue == -1)
				{
					num = (this.param != null ? ClientUtils.RowCount(this.sSQL, this.param) : ClientUtils.RowCount(this.sSQL, this.Params));
				}
				else
				{
					num = this.rowCountValue;
				}
				return num;
			}
		}

		public DataRetriever(string sSQL, object[][] Params)
		{
			this.sSQL = sSQL;
			this.Params = Params;
		}

		public DataRetriever(string sSQL, object[] param)
		{
			this.sSQL = sSQL;
			this.param = this.Params;
		}

		public DataTable SupplyPageOfData(int lowerPageBoundary, int rowsPerPage)
		{
            DataTable dataTable;
			dataTable = (this.param != null ? ClientUtils.SupplyPageOfData(this.sSQL, this.param, lowerPageBoundary, rowsPerPage) : ClientUtils.SupplyPageOfData(this.sSQL, this.Params, lowerPageBoundary, rowsPerPage));
			return dataTable;
		}

        public DataTable SupplyPageOfData(int lowerPageBoundary, int rowsPerPage, ref DataTable dt2)
        {
            //DataTable dataTable;
            //dataTable = (this.param != null ? ClientUtils.SupplyPageOfData(this.sSQL, this.param, lowerPageBoundary, rowsPerPage, ref this.rowCountValue, ref dt2) : ClientUtils.SupplyPageOfData(this.sSQL, this.Params, lowerPageBoundary, rowsPerPage, ref this.rowCountValue, ref dt2));
            //this.columnsValue = dataTable.Columns;
            return null;
        }
    }
}