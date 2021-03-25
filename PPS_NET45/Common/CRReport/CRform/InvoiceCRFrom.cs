using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class InvoiceCRFrom
    {
        public InvoiceCRFrom(string invoice)
        {
            Initialize(invoice);
        }

        private void Initialize(string invoice)
        {
            DataSet ds = new DataSet();
            string strSql = "SELECT * FROM WMUSER.AC_FD_APAC_CI_HEADER@dgedi t0,WMUSER.AC_FD_APAC_CI_LINE@dgedi t1 WHERE t0.INVOICE_NO = '" + invoice + "'  AND t0.INVOICE_NO = t1.INVOICE_NO";
            ds.Tables.Add(Util.getDataTaleC(strSql, "AC_FD_APAC_CI"));
            Util.CreateDataTable(Constant.INVOICE_URL, ds);
        }
    }
}
