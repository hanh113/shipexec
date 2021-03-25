using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRReport.CRfrom
{
    public partial class P2_ConsolPackingList
    {
        public string completeDiskPath = "";
        public P2_ConsolPackingList(string shipID, bool print)
        {
            InitializeC(shipID, print);
        }


        private void InitializeC(String shipID, bool print)
        {
            //初始化 DataSet
            DataSet ds = new DataSet();

            string headerSql = @"SELECT * FROM WMUSER.AC_APAC_DS_CPL_HEADER@dgedi T0 WHERE T0.SHIPMENT_ID = '" + shipID + "'";
            string lineSql = @"SELECT * FROM WMUSER.AC_APAC_DS_CPL_LINE@dgedi T0 WHERE T0.SHIPMENT_ID = '" + shipID + "'";
            string PalletSql = @"select COUNT(DISTINCT PALLET_NO) AS PALLET_NO  from ppsuser.g_ds_scandata_t where SHIPMENT_ID = '" + shipID + "'";

            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            ds.Tables.Add(Util.getDataTaleC(PalletSql, "Pallet"));

            string tmp = "";
            tmp = Application.StartupPath + "\\PDF\\" + shipID + "P2C.pdf";
            completeDiskPath = tmp;
            if (print)
            {
                Util.CreateDataTable(Constant.P2ConsolPackingList_URL, ds);
            }
            else
            {
                Util.exportCRPDFAndSendEmail(Constant.P2ConsolPackingList_URL, ds, tmp);
            }
        }

        //Summary： export PDF for Crystal Report 
        private void exportPDFCR(string acDn, DataSet ds, string diskPath)
        {
            List<string> filePath = new List<string>();
            string tmp = diskPath + acDn + "P2C.pdf";
            filePath.Add(tmp);
            Util.printPDFCrystalReportV2(Constant.P2ConsolPackingList_URL, ds, tmp);
            //SendEmail.Send(filePath);
        }
    }
}
