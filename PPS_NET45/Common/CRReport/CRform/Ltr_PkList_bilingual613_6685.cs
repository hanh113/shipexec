using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class Ltr_PkList_bilingual613_6685 
    {
        public Ltr_PkList_bilingual613_6685(string acDn)
        {
            setDataSoure(acDn);
        }

        private void setDataSoure(string acDn)
        {
            DataSet ds = new DataSet();      //AC_DS_AMR_CA_LT_CSPL_HEADER
            string headerSql = @"SELECT * FROM WMUSER.AC_DS_AMR_CA_LT_CSPL_HEADER@dgedi WHERE AC_DN = '" + acDn + "'";
            string lineSql = @"SELECT * FROM WMUSER.AC_DS_AMR_CA_LT_CSPL_LINE@dgedi  WHERE AC_DN = '" + acDn + "'";

            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            Util.CreateDataTable(Constant.Ltr_PkList_bilingual613_6685_URL, ds);
        }
    }
}