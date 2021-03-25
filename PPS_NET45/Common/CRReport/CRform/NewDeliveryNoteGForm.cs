using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRReport.CRfrom
{
    public partial class NewDeliveryNoteGForm
    {
        public string competeDiskPath = "";
        public NewDeliveryNoteGForm(string shipID, string ac_Dn, bool print)
        {
            Initialize(shipID, ac_Dn, print);
        }

        public void Initialize(string shipID, string ac_Dn, bool print)
        {
            DataSet ds = new DataSet();
            string HeaderSql = @"select * from WMUSER.AC_EMEIA_DS_DELN_HEADER_G@dgedi where ac_dn = '" + ac_Dn + "'";
            string LineSql = @"select * from WMUSER.AC_EMEIA_DS_DELN_LINE_G@dgedi where ac_dn = '" + ac_Dn + "' ORDER BY AC_DN_LINE ASC ";
            string PalletSql = @"select COUNT(DISTINCT PALLET_NO) AS PALLET_NO  from ppsuser.g_ds_scandata_t where SHIPMENT_ID = '" + shipID + "'";
            ds.Tables.Add(Util.getDataTaleC(HeaderSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(LineSql, "Line"));
            ds.Tables.Add(Util.getDataTaleC(PalletSql, "Pallet"));

            string filePath = "";
            filePath = Application.StartupPath + "\\PDF\\" + ac_Dn + "DeliveryNoteG.pdf";
            competeDiskPath = filePath;
            if (print)
            {
                Util.CreateDataTable(Constant.NewDeliveryNote_URL, ds);
            }
            else
            {
                Util.printPDFCrystalReportV2(Constant.NewDeliveryNote_URL, ds, filePath);
            }
        }
    }
}
