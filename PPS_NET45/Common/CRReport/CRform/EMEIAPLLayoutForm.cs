using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class EMEIAPLLayoutForm
    {
        public string diskCompletePath = ""; //全局变量返回pdf路径
        public EMEIAPLLayoutForm(string so_no, string diskPath, bool print, string countryCode)
        {
            string querySQL = @"select distinct a.invoiceno from ppsuser.g_shipping_detail_t a where a.so_no = '" + so_no + "'";
            string invoiceNumber = "";
            DataSet invoiceDS = ClientUtils.ExecuteSQL(querySQL);
            if (invoiceDS != null && invoiceDS.Tables.Count > 0)
            {
                DataTable invoiceDt = ClientUtils.ExecuteSQL(querySQL).Tables[0];
                if (invoiceDt != null && invoiceDt.Rows.Count > 0)
                {
                    invoiceNumber = invoiceDt.Rows[0][0].ToString();
                }
            }
            Initialize(invoiceNumber, so_no, diskPath, print, countryCode);
        }

        private void Initialize(string invoiceNumber, string so_no, string diskPath, bool print, string countryCode)
        {
            //初始化 DataSet
            DataSet ds = new DataSet();

            //AmrDsShippingLabel
            string headerSql = @"SELECT * FROM WMUSER.AC_FD_EMEIA_PL_HEADER@dgedi T0 WHERE T0.INVOICE_NO = '" + invoiceNumber + "'";
            string lineSql = @"SELECT F.PO_NO,
                                F.MPN,
                                F.PALLET,
                                F.QTY / T.sn_qty cartons,
                                '' AS scopeCartons,
                                F.QTY,
                                M.product_weight * F.qty AS net_weight,
                                F.GROSS_WEIGHT,
                                '' AS start_carton,
                                P.AC_PN_DESC
                                FROM (SELECT D.PO_NO,
                                D.LINE_ITEM,
                                D.PART_NO,
                                D.MPN,
                                SUM(QTY) qty,
                                SUM(WEIGHT) gross_weight,
                                D.PALLET
                                FROM (SELECT A.SSCC,
                                A.PO_NO,
                                A.LINE_ITEM,
                                E.PART_NO,
                                A.MPN,
                                SUM(A.QTY) QTY,
                                ROUND(SUM(A.QTY) / C.QTY * B.STANDARD_WEIGHT, 2) WEIGHT,
                                REPLACE(G.pallet, 'OF', '/') pallet
                                FROM PPSUSER.G_SHIPPING_DETAIL_T A
                                LEFT JOIN ppsuser.g_shipping_pallet_label_print G
                                ON A.SSCC = G.SSCC
                                AND A.PALLET_ID = G.PALLET_NO,
                                PPSUSER.G_PALLETEDI_INFO B, SAJET.SYS_PART E,
                                (SELECT SSCC, SUM(QTY) QTY
                                FROM PPSUSER.G_SHIPPING_DETAIL_T
                                WHERE SO_NO = '" + so_no + "'"
                                + @"GROUP BY SSCC) C
                                WHERE A.SO_NO = '" + so_no + "'"
                                + @"AND A.SO_NO = B.SO_NO
                                AND A.PART_ID = E.PART_ID
                                AND A.SSCC = B.SSCC
                                AND A.SSCC = C.SSCC
                                GROUP BY A.SSCC,
                                A.PO_NO,
                                A.LINE_ITEM,
                                E.PART_NO,
                                A.MPN,
                                C.QTY,
                                B.STANDARD_WEIGHT,
                                G.pallet) D
                                GROUP BY D.PO_NO, D.LINE_ITEM, D.PART_NO, D.MPN, D.PALLET) F
                                LEFT JOIN WMUSER.AC_FD_EMEIA_PL_LINE@dgedi p
                                ON F.PO_NO = P.AC_PO
                                AND F.LINE_ITEM = P.AC_PO_LINE
                                AND F.MPN = P.AC_PN
                                LEFT JOIN ppsuser.g_ds_partinfo_t M
                                ON F.PART_NO = M.ICTPN
                                LEFT JOIN PPSUSER.G_DS_PACKINFO_T T
                                ON M.PACK_CODE = T.PACK_CODE ORDER BY F.PO_NO,F.MPN";
            string shipDateSql = @"SELECT distinct ship_date FROM PPSUSER.G_SHIPPING_DETAIL_T WHERE invoiceno = '" + invoiceNumber + "'";

            string emptyCaronsSql = @"SELECT SUM(EMPTY_CARTONS) as EMPTY_CARTONS
                                        FROM ppsuser.g_shipping_pallet_label_print
                                        WHERE SSCC IN (SELECT SSCC
                                        FROM PPSUSER.G_SHIPPING_DETAIL_T
                                        WHERE SO_NO = '" + so_no + "')";
            string actualCartonsSql = @"select SUM(CQTY) as ACTUAL_CARTONS
                                        from ppsuser.g_shipping_detail_t
                                        where SO_NO = '" + so_no + "'";


            string filePath = "";
            string currentDate = "";
            string HAWB = "";
            currentDate = DateTime.Now.ToString("yyyyMMdd");
            DataTable headerDt = Util.getDataTaleC(headerSql, "Header");
            DataTable lineDt = Util.getDataTaleC(lineSql, "Line");
            DataTable emptyCartonDt = Util.getDataTaleC(emptyCaronsSql, "EmptyCartons");


            int endCartons = 0;
            int startCartons = 1;
            int totalQty = 0;
            int totalEmptyCartons = 0;
            int countedEmptyQty = 0;

            if (emptyCartonDt != null & emptyCartonDt.Rows.Count > 0)
            {
                totalEmptyCartons = Convert.ToInt32(emptyCartonDt.Rows[0][0].ToString());
            }
            for (int i = 0; i < lineDt.Rows.Count; i++)
            {
                totalQty = totalQty + Convert.ToInt32(lineDt.Rows[i][5].ToString());
            }
            for (int i = 0; i < lineDt.Rows.Count; i++)
            {
                endCartons = endCartons + Convert.ToInt32(lineDt.Rows[i][3].ToString());
                lineDt.Rows[i][4] = startCartons + "-" + endCartons;
                startCartons = startCartons + Convert.ToInt32(lineDt.Rows[i][3].ToString());

                if (i == lineDt.Rows.Count)
                {
                    lineDt.Rows[i][8] = totalEmptyCartons - countedEmptyQty;
                }
                int lineEmptyCartons = Convert.ToInt32(Convert.ToDouble(lineDt.Rows[i][5].ToString()) / totalQty * totalEmptyCartons);
                lineDt.Rows[i][8] = lineEmptyCartons;
                countedEmptyQty = countedEmptyQty + lineEmptyCartons;

            }

            if (headerDt != null && headerDt.Rows.Count > 0)
            {
                HAWB = headerDt.Rows[0][1].ToString();
            }
            filePath = diskPath + currentDate + " " + HAWB + " Import Customs Clearance Doc.CPL.pdf";
            diskCompletePath = filePath; //全局变量返回pdf路径
            string reportPath = "";
            if ("AE".Equals(countryCode))
            {
                reportPath = Constant.EMEIAPLLayoutAE_URL;
            }
            else if ("IT".Equals(countryCode))
            {
                reportPath = Constant.EMEIAPLLayoutIT_URL;
            }
            else
            {
                reportPath = Constant.EMEIAPLLayout_URL;
            }

            ds.Tables.Add(headerDt);
            ds.Tables.Add(lineDt);
            ds.Tables.Add(Util.getDataTaleC(shipDateSql, "ShipDate"));
            ds.Tables.Add(emptyCartonDt);
            ds.Tables.Add(Util.getDataTaleC(actualCartonsSql, "ActualCartons"));

            if (print)
            {
                Util.CreateDataTable(reportPath, ds);
            }
            else
            {
                Util.printPDFCrystalReportV2(reportPath, ds, filePath);
            }
        }
    }
}
