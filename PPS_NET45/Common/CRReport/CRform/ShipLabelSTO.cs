using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class ShipLabelSTO
    {
        public ShipLabelSTO(String ac_dn)
        {
            InitializeC(ac_dn);
        }
        private void InitializeC(String acDn)
        {
            //初始化 DataSet
            DataSet ds = new DataSet();
            //AcFdHome
            string headerSql = @"SELECT * FROM WMUSER.AC_EMEIA_DS_SHIP_LBL_HEADER@dgedi T0 WHERE T0.AC_DN = '" + acDn + "'";
            //total_qty_PO
            string lineSql = @"SELECT * FROM WMUSER.AC_EMEIA_DS_SHIP_LBL_LINE@dgedi T0 WHERE T0.AC_DN = '" + acDn + "'";
            //line
            string itemSql = @"SELECT * FROM WMUSER.AC_EMEIA_DS_SHIP_LBL_ITEM@dgedi T0 WHERE T0.AC_DN = '" + acDn + "'";


            ds.Tables.Add(getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(getDataTaleC(lineSql, "Line"));
            ds.Tables.Add(getDataTaleC(itemSql, "Item"));
            Util.CreateDataTable(Constant.ShipLabelSTO_URL, ds);
        }

        private DataTable getDataTaleC(string strsql, string tableName)
        {
            DataTable action = null;
            DataSet ds = ClientUtils.ExecuteSQL(strsql);
            action = ds.Tables[0].Copy();
            if (action != null) action.TableName = tableName;
            return action;

        }
    }
}
