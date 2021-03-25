using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRReport.CRfrom
{
    public partial class ConsolCIAMRLayoutBrazilForm
    {
        public string diskCompletePath = ""; //全局变量返回pdf路径
        public string diskCompelteSerialPath = "";
        public ConsolCIAMRLayoutBrazilForm(string shipID, bool print)
        {
            Initialize(shipID, print);
        }

        private void Initialize(string shipID, bool print)
        {
            //初始化 DataSet
            DataSet ds = new DataSet();

            //AmrDsShippingLabel
            string headerSql = @"SELECT * FROM WMUSER.AC_AMR_DS_CCI_BZ_HEADER@dgedi T0 WHERE T0.SHIPMENT_ID = '" + shipID + "'";

            string lineSql = @"SELECT * FROM WMUSER.AC_AMR_DS_CCI_BZ_LINE@dgedi T0 WHERE T0.SHIPMENT_ID = '" + shipID + "'";
            string serialSql = @"SELECT DISTINCT b.AC_PN, b.MATE_DESC, a.Serial_Number
                                  FROM PPSUSER.G_DS_SCANDATA_DETAIL_T     a,
                                       WMUSER.AC_AMR_DS_CCI_BZ_LINE@DGEDI b
                                 where a.SHIPMENT_ID = '" + shipID + "'"
                    + @"AND a.mpn = b.AC_PN AND a.SHIPMENT_ID = b.SHIPMENT_ID ORDER BY  b.AC_PN ,a.Serial_Number";
            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            ds.Tables.Add(Util.getDataTaleC(serialSql, "Serial"));
            string filePath = "";
            string filePath2 = "";
            filePath = Application.StartupPath + "\\PDF\\" + shipID + "CCIBZ.pdf";
            filePath2 = Application.StartupPath + "\\PDF\\" + shipID + "CCIBZSERIAL.pdf";
            diskCompletePath = filePath; //全局变量返回pdf路径
            diskCompelteSerialPath = filePath2;
            if (print)
            {
                Util.CreateDataTable(Constant.CCILayoutBZ_URL, ds);
                Util.CreateDataTable(Constant.CCILayoutBZSERIAL_URL, ds);
            }
            else
            {
                Util.exportCRPDFAndSendEmail(Constant.CCILayoutBZ_URL, ds, filePath);
                Util.exportCRPDFAndSendEmail(Constant.CCILayoutBZSERIAL_URL, ds, filePath2);
            }


        }
    }
}
