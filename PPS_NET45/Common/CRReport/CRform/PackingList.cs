using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class PackingList
    {
        public PackingList(string invoiceNo)
        {
            setDataSoure(invoiceNo);
        }

        private void setDataSoure(String invoiceNo)
        {
            string strSql = @"SELECT
                             T0.INVOICE_NO,
                             T0.PRINT_DATE,
                             T0.SHIPPER_NAME,
                             T0.SHIPPER_ADD1,
                             T0.SHIPPER_ADD2,
                             T0.BT_NAME,
                             T0.BT_ADD1,
                             T0.BT_ADD2,
                             T0.ST_NAME,
                             T0.SHIPTO_ADDR,
                             T0.PAY_TERM,
                             T0.SALE_TERM,
                             T0.PORT_EXP,
                             T0.FINAL_DEST,
                             T0.HAWB,
                             T0.PS_DATE,
                             T0.NET_WET,
                             T0.VOLUMN,
                             T0.PAL_CNT,
                             T0.CTN_CNT,
                             T0.GRO_WET,
                             T1.INVOICE_NO,
                             T1.REF_NUM,
                             T1.AC_PO_LINE,
                             T1.AC_PN,
                             T1.AC_PN_DESC,
                             T1.DELI_NUM,
                             T1.NET_WET,
                             T1.MODEL_NO,
                             T1.COO,
                             T1.QUANTITY
                            FROM
                             WMUSER.AC_FD_APAC_PL_HEADER@dgedi T0
                            CROSS JOIN WMUSER.AC_FD_APAC_PL_LINE@dgedi T1
                            WHERE
                             T0.INVOICE_NO = T1.INVOICE_NO
                            AND T0.INVOICE_NO = '" + invoiceNo + "'";
            // DataTable action = GetData.getDatatable(strSql);
            DataSet action = new DataSet();
            action.Tables.Add(Util.getDataTaleC(strSql, "AC_FD_APAC_PL"));
            Util.CreateDataTable(Constant.packingList_URL, action);
        }
    }
}
