using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace wmsReport.Utils
{
    class TransferDataTable
    {
        public static DataTable RowsToCol(DataTable DT)
        {
            try
            {
                int rowCount = DT.Rows.Count;
                int columnsCount = DT.Columns.Count;
                DataTable result = new DataTable();
                DataTable RowsDT = new DataTable();
                DataTable COLSDT = new DataTable();
                for (int i = 0; i < rowCount; i++)
                {
                    result.Columns.Add(DT.Rows[i][1].ToString());
                    RowsDT.Columns.Add(DT.Rows[i][1].ToString());
                    COLSDT.Columns.Add(DT.Rows[i][1].ToString());
                }
                string[] RowsName = new string[columnsCount];
                for (int i = 0; i < columnsCount; i++)
                {
                    RowsName[i] = DT.Columns[i].ColumnName.ToString();
                }
                for (int rowsi = 0; rowsi < RowsName.Length; rowsi++)
                {
                    RowsDT.Rows.Add(new string[] { RowsName[rowsi] });
                }
                //行转列的核心部分
                for (int columnsi = 0; columnsi < columnsCount; columnsi++)
                {
                    DataRow dr = COLSDT.NewRow();
                    for (int rowj = 0; rowj < rowCount; rowj++)
                    {
                        dr[rowj] = DT.Rows[rowj][columnsi].ToString();
                    }
                    COLSDT.Rows.Add(dr);
                }

                for (int columnsi = 0; columnsi < columnsCount; columnsi++)
                {
                    DataRow resultdr = result.NewRow();
                    for (int rowj = 0; rowj < rowCount; rowj++)
                    {
                        if (rowj == 0)
                        {
                            resultdr[rowj] = RowsDT.Rows[columnsi][0].ToString();
                        }
                        else
                        {
                            resultdr[rowj] = COLSDT.Rows[columnsi][rowj].ToString();
                        }
                    }
                    result.Rows.Add(resultdr);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

    }
}
