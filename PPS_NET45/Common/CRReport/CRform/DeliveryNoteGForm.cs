using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRReport.CRfrom
{
    public partial class DeliveryNoteGForm
    {
        public DeliveryNoteGForm(string shipmentId, string acDn, bool print)
        {
            setDataSoure(shipmentId, acDn, print);

        }

        private void setDataSoure(string shipmentId, string acDn, bool print)
        {

            string strHeaderSql = @"SELECT * FROM WMUSER.AC_EMEIA_DS_DELN_HEADER_G@dgedi t0 WHERE t0.AC_DN = '" + acDn + "'";
            string ssccSql = @"SELECT T0.SSCC18 FROM WMUSER.AC_EMEIA_DS_DELN_PAL_G@dgedi T0 WHERE T0.SHIPMENT_ID = '" + shipmentId + "' AND t0.AC_DN = '" + acDn + "' GROUP BY T0.SSCC18";
            DataTable ssccDT = Util.getDataTaleC(ssccSql, "SSCC");
            int ssccCount = ssccDT.Rows.Count;
            if (ssccCount == 1)
            {
                string sscc18 = ssccDT.Rows[0][0].ToString();
                string strPalSql = @"SELECT '" + ssccCount + "' as SSCC_COUNT,'" + ssccCount + "' as SSCC_I,t0.* FROM WMUSER.AC_EMEIA_DS_DELN_PAL_G@dgedi t0 WHERE t0.AC_DN = '" + acDn + "' AND t0.SSCC18 = '" + sscc18 + "'";
                DataSet action = new DataSet();
                action.Tables.Add(Util.getDataTaleC(strHeaderSql, "AC_EMIEA_DS_DELN_HEADER_G"));
                action.Tables.Add(Util.getDataTaleC(strPalSql, "AC_EMIEA_DS_DELN_PAL_G"));
                string filePath = "";
                filePath = Application.StartupPath + "\\PDF\\" + acDn + "ssccCount" + ssccCount + ".pdf";
                if (print)
                {
                    Util.CreateDataTable(Constant.DeliveryNoteG_URL, action);
                }
                else
                {
                    Util.printPDFCrystalReportV2(Constant.DeliveryNoteG_URL, action, filePath);
                }
            }
            else
            {
                for (int i = 0; i < ssccCount; i++)
                {
                    if (i == ssccCount - 1)
                    {
                        int sscc_i = i + 1;
                        string sscc18 = ssccDT.Rows[i][0].ToString();
                        string strPalSql = @"SELECT '" + ssccCount + "' as SSCC_COUNT,'" + sscc_i + "' as SSCC_I,t0.* FROM WMUSER.AC_EMEIA_DS_DELN_PAL_G@dgedi t0 WHERE t0.AC_DN = '" + acDn + "' AND t0.SSCC18 = '" + sscc18 + "'";
                        DataSet action = new DataSet();
                        action.Tables.Add(Util.getDataTaleC(strHeaderSql, "AC_EMIEA_DS_DELN_HEADER_G"));
                        action.Tables.Add(Util.getDataTaleC(strPalSql, "AC_EMIEA_DS_DELN_PAL_G"));
                        string filePath = "";
                        filePath = Application.StartupPath + "\\PDF\\" + acDn + "ssccCount" + ssccCount + ".pdf";
                        if (print)
                        {
                            Util.CreateDataTable(Constant.DeliveryNoteG_URL, action);
                        }
                        else
                        {
                            Util.printPDFCrystalReportV2(Constant.DeliveryNoteG_URL, action, filePath);
                        }
                    }
                    else
                    {
                        int sscc_i = i + 1;
                        string sscc18 = ssccDT.Rows[i][0].ToString();
                        string strPalSql = @"SELECT '" + ssccCount + "' as SSCC_COUNT,' " + sscc_i + "' as SSCC_I,t0.* FROM WMUSER.AC_EMEIA_DS_DELN_PAL_G@dgedi t0 WHERE t0.AC_DN = '" + acDn + "' AND t0.SSCC18 = '" + sscc18 + "'";
                        DataSet action = new DataSet();
                        action.Tables.Add(Util.getDataTaleC(strHeaderSql, "AC_EMIEA_DS_DELN_HEADER_G"));
                        action.Tables.Add(Util.getDataTaleC(strPalSql, "AC_EMIEA_DS_DELN_PAL_G"));
                        string filePath = "";
                        filePath = Application.StartupPath + "\\PDF\\" + acDn + "ssccCount" + ssccCount + ".pdf";
                        if (print)
                        {
                            Util.CreateDataTable(Constant.DeliveryNoteG_HEADER_URL, action);

                        }
                        else
                        {
                            Util.printPDFCrystalReportV2(Constant.DeliveryNoteG_HEADER_URL, action, filePath);
                        }
                    }
                }
            }
        }
    }
}
