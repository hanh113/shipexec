using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class ConsolCIAMRLayoutUSAndPeru
    {
        public string diskCompletePath = ""; //全局变量返回pdf路径
        public string diskCompelteSerialPath = "";
        public ConsolCIAMRLayoutUSAndPeru(string shipID, string diskPath, bool print)
        {
            Initialize(shipID, diskPath, print);
        }

        private void Initialize(string shipID, string diskPath, bool print)
        {
            //初始化 DataSet
            DataSet ds = new DataSet();
            //AmrDsShippingLabel
            string headerSql = @"SELECT * FROM WMUSER.AC_AMR_DS_CCI_US_PR_HEADER@dgedi T0 WHERE T0.SHIPMENT_ID = '" + shipID + "'";

            string lineSql = @"SELECT * FROM WMUSER.AC_AMR_DS_CCI_US_PR_LINE@dgedi T0 WHERE T0.SHIPMENT_ID = '" + shipID + "'";
            string serialSql = @"SELECT DISTINCT b.AC_PN, b.MATE_DESC, a.Serial_Number
                                  FROM PPSUSER.G_DS_SCANDATA_DETAIL_T     a,
                                       WMUSER.AC_AMR_DS_CCI_US_PR_LINE@DGEDI b
                                 where a.SHIPMENT_ID = '" + shipID + "'"
                                + @"AND a.mpn = b.AC_PN AND a.SHIPMENT_ID = b.SHIPMENT_ID ORDER BY  b.AC_PN ,a.Serial_Number";
            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            ds.Tables.Add(Util.getDataTaleC(serialSql, "Serial"));
            string filePath = "";
            string filePath2 = "";
            filePath = diskPath + "_CCI_AMR_US" + shipID + ".pdf";
            filePath2 = diskPath + "_CCI_AMR_US_SERIAL" + shipID + ".pdf";
            diskCompletePath = filePath; //全局变量返回pdf路径
            diskCompelteSerialPath = filePath2;
            if (print)
            {
                Util.CreateDataTable(Constant.CCILayoutUSAndPeru_URL, ds);
                Util.CreateDataTable(Constant.CCILayoutUSAndPeru_URL, ds);
            }
            else
            {
                Util.exportCRPDFAndSendEmail(Constant.CCILayoutUSAndPeru_URL, ds, filePath);
                Util.exportCRPDFAndSendEmail(Constant.CCILayoutUSAndPeruSerial_URL, ds, filePath2);
            }
        }
    }
}
