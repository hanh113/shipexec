using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class AmrDsShippingLabel940From
    {
        public AmrDsShippingLabel940From(String acDn)
        {
            Initialize(acDn);
        }

        private void Initialize(String acDn)
        {
            //初始化 DataSet
            DataSet ds = new DataSet();

            //AmrDsShippingLabel
            string herderSql = @"SELECT * FROM WMUSER.AC_AMR_DS_SHIP_LBL_HEADER@dgedi T0 WHERE T0.AC_DN = '" + acDn+"'";
            string lineSql = @"SELECT * FROM WMUSER.AC_AMR_DS_SHIP_LBL_LINE@dgedi T0 WHERE T0.AC_DN = '" + acDn + "'";
            string itemSql = @"SELECT * FROM WMUSER.AC_AMR_DS_SHIP_LBL_ITEM@dgedi T0 WHERE T0.AC_DN = '" + acDn + "'";


            ds.Tables.Add(Util.getDataTaleC(herderSql, "AC_AMR_DS_SHIP_LBL_HEADER"));

            ds.Tables.Add(Util.getDataTaleC(lineSql, "AC_AMR_DS_SHIP_LBL_LINE"));

            ds.Tables.Add(Util.getDataTaleC(itemSql, "AC_AMR_DS_SHIP_LBL_ITEM"));

            // ds.Tables.Add(Util.getDataTale(strSql, "AC_AMR_DS_SHIP_LBL",""));

            Util.CreateDataTable(
            Constant.AmrDsShippingLabel940_URL,
            ds);

           
        }

    }
}
