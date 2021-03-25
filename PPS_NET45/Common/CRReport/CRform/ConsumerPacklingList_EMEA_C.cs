using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class ConsumerPacklingList_EMEA_C
    {
        public ConsumerPacklingList_EMEA_C(String acDn)
        {
            setDataSoure(acDn);
        }

        private void setDataSoure(String acDn)
        {
            string strSql = @"SELECT * FROM WMUSER.AC_DS_EMEA_DELN_HEADER_C@dgedi t0,WMUSER.AC_DS_EMEA_DELN_LINE_C@dgedi t1 WHERE t0.AC_DN = '" + acDn + "'  AND t0.AC_DN = t1.AC_DN";
            DataSet action = new DataSet();
            action.Tables.Add(Util.getDataTaleC(strSql, "ConsumerPacking"));
            Util.CreateDataTable(Constant.ConsumerPacklingList_EMEA_C_URL, action);
        }
    }
}
