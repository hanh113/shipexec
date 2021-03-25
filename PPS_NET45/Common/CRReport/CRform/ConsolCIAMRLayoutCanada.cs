using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRReport.CRfrom
{
    public partial class ConsolCIAMRLayoutCanada
    {
        public string diskCompletePath = ""; //全局变量返回pdf路径
        public string diskCompelteSerialPath = "";
        public ConsolCIAMRLayoutCanada(string acDn, bool print)
        {
            Initialize(acDn, print);
        }
        private void Initialize(string shipID, bool print)
        {
            //初始化 DataSet
            DataSet ds = new DataSet();

            //AmrDsShippingLabel
            string headerSql = @"SELECT * FROM WMUSER.AC_AMR_DS_CCI_CA_HEADER@dgedi T0 WHERE T0.SHIPMENT_ID = '" + shipID + "'";

            string lineSql = @"SELECT * FROM WMUSER.AC_AMR_DS_CCI_CA_LINE@dgedi T0 WHERE T0.SHIPMENT_ID = '" + shipID + "'";
            string serialSql = @"SELECT DISTINCT b.AC_PN, b.MATE_DESC, a.Serial_Number
                                  FROM PPSUSER.G_DS_SCANDATA_DETAIL_T     a,
                                       WMUSER.AC_AMR_DS_CCI_CA_LINE@DGEDI b
                                 where a.SHIPMENT_ID = '" + shipID + "'"
                                + @"AND a.mpn = b.AC_PN AND a.SHIPMENT_ID = b.SHIPMENT_ID ORDER BY  b.AC_PN ,a.Serial_Number";
            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            ds.Tables.Add(Util.getDataTaleC(serialSql, "Serial"));
            string filePath = "";
            string filePath2 = "";
            filePath = Application.StartupPath + "\\PDF\\" + shipID + "CCICAN.pdf";
            filePath2 = Application.StartupPath + "\\PDF\\" + shipID + "CCICANSERIAL.pdf";
            diskCompletePath = filePath; //全局变量返回pdf路径
            diskCompelteSerialPath = filePath2;
            if (print)
            {
                Util.CreateDataTable(Constant.CCILayoutCAN_URL, ds);
                Util.CreateDataTable(Constant.CCILayoutCANSerial_URL, ds);
            }
            else
            {
                //CCILayoutCANSerial_URL
                Util.exportCRPDFAndSendEmail(Constant.CCILayoutCAN_URL, ds, filePath);
                Util.exportCRPDFAndSendEmail(Constant.CCILayoutCANSerial_URL, ds, filePath2);
            }


        }
    }
}
