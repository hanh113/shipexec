using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class EMEIAInvoiceLayoutForm
    {
        public string diskCompletePath = ""; //全局变量返回pdf路径
        public EMEIAInvoiceLayoutForm(string so_no, string diskPath, bool print, string countryCode)
        {
            string querySQL = @"select a.invoiceno from ppsuser.g_shipping_detail_t a where a.so_no = '" + so_no + "'";
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
            Initialize(invoiceNumber, diskPath, print, countryCode);
        }

        private void Initialize(string invoiceNumber, string diskPath, bool print, string countryCode)
        {
            //初始化 DataSet
            DataSet ds = new DataSet();

            //AmrDsShippingLabel
            string headerSql = @"SELECT * FROM WMUSER.AC_FD_EMEIA_CI_HEADER@dgedi T0 WHERE T0.INVOICE_NO = '" + invoiceNumber + "'";
            string lineSql = @"SELECT T.S_COUNTRY_CODE,      
                                SUM(a.PRODUCT_WEIGHT * t1.qty) AS Net_Weight,
                                SUM(a.ROUGH_WEIGHT * t1.qty) AS Gross_Weight,       
                                t0.INVOICE_NO,       
                                t0.AC_PO,       
                                t0.AC_PO_LINE,      
                                t0.AC_PN,       
                                t0.AC_PN_DESC,       
                                SUM(t1.qty) qty,       
                                t0.UNIT_PRICE,       
                                SUM(t1.qty * t0.UNIT_PRICE) extended
                                FROM (SELECT distinct *
                                from WMUSER.AC_FD_EMEIA_CI_LINE@dgedi
                                where INVOICE_NO = '" + invoiceNumber + "') t0, "
                                + @" (SELECT DISTINCT AC_PO, S_COUNTRY_CODE FROM wmuser.ac_850_po_header) t,       
                                PPSUSER.g_shipping_detail_t t1,       
                                ppsuser.g_ds_partinfo_t a,       
                                sajet.sys_part b
                                WHERE
                                t0.INVOICE_NO = t1.invoiceno
                                AND t0.AC_PO = t1.po_no
                                AND t0.AC_PO_LINE = t1.line_item
                                AND t0.AC_PN = t1.mpn
                                AND t1.part_id = b.part_id
                                AND a.ictpn = b.part_no
                                AND T.AC_PO = t1.po_no
                                GROUP BY t.s_country_code,          
                                t0.INVOICE_NO,         
                                t0.AC_PO,          
                                t0.AC_PO_LINE,          
                                t0.AC_PN,          
                                t0.AC_PN_DESC,          
                                t0.UNIT_PRICE
                                ORDER BY t0.AC_PO,          
                                t0.AC_PO_LINE,          
                                t0.INVOICE_NO,          
                                t0.AC_PO,          
                                t0.AC_PN 
                                ";
            string shipDateSql = @"SELECT distinct ship_date FROM PPSUSER.G_SHIPPING_DETAIL_T WHERE invoiceno = '" + invoiceNumber + "'";
            string HAWB = "";
            string currentDate = "";
            currentDate = DateTime.Now.ToString("yyyyMMdd");
            DataTable headerDt = Util.getDataTaleC(headerSql, "Header");
            string reportPath = "";
            if ("AE".Equals(countryCode))
            {
                reportPath = Constant.EMEIAInvoiceLayoutAE_URL;
            }
            else
            {
                reportPath = Constant.EMEIAInvoiceLayout_URL;
            }
            if (headerDt != null && headerDt.Rows.Count > 0)
            {
                HAWB = headerDt.Rows[0][1].ToString();
            }
            ds.Tables.Add(headerDt);
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            ds.Tables.Add(Util.getDataTaleC(shipDateSql, "ShipDate"));
            String filePath = "";
            filePath = diskPath + currentDate + " " + HAWB + " Import Customs Clearance Doc.CCI.pdf";
            diskCompletePath = filePath;
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
