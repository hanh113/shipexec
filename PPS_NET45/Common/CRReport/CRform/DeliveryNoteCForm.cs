using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class DeliveryNoteCForm
    {
        public DeliveryNoteCForm(string acDn)
        {
            setDataSoure(acDn);
        }

        private void setDataSoure(String acDn)
        {
            string strSql = @"SELECT * FROM WMUSER.AC_DS_EMEA_DELN_HEADER@dgedi t0,WMUSER.AC_DS_EMEA_DELN_LINE@dgedi t1 WHERE t0.AC_DN = '" + acDn + "'  AND t0.AC_DN = t1.AC_DN";
            DataSet action = new DataSet();
            action.Tables.Add(Util.getDataTaleC(strSql, "DeliveryTable"));
            Util.CreateDataTable(Constant.DeliveryNoteC_URL, action);
        }
    }
}
