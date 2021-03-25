using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class FDAPACShippingInoviceForm
    {
        public string diskCompletePath = "";
        public FDAPACShippingInoviceForm(string so_no, string diskPath, bool print)
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
            Initialize(invoiceNumber, so_no, diskPath, print);
        }


        private void Initialize(string invoiceNumber, string so_no, string diskPath, bool print)
        {
            //初始化 DataSet
            DataSet ds = new DataSet();
            //AmrDsShippingLabel
            string headerSql = @"SELECT * FROM WMUSER.AC_FD_APAC_CI_HEADER@dgedi T0 WHERE T0.INVOICE_NO = '" + invoiceNumber + "'";
            string lineSql = @"select distinct p.AC_PN_DESC,             
                                p.REF_NUM,
                                p.AC_PO_LINE,
                                p.AC_PN,                
                                p.DELI_NUM,                
                                p.MODEL_NO,               
                                p.UNIT_PRICE,               
                                p.COO,               
                                a.po_no,               
                                a.mpn,               
                                sum(a.qty) qty,                
                                sum(a.qty * p.UNIT_PRICE) extended,                
                                sum(d.product_weight * a.qty) as net_weight,               
                                sum(d.rough_weight * a.qty) as gross_weight
                                from ppsuser.g_shipping_detail_t a,      
                                WMUSER.AC_FD_APAC_CI_LINE@dgedi p,     
                                ppsuser.g_ds_partinfo_t d,   
                                sajet.sys_part e
                                where a.so_no = '" + so_no + "' "
                                + " and a.line_item = p.ac_po_line "
                                + " and a.invoiceno = p.invoice_no "
                                + " and a.po_no = p.REF_NUM "
                                + " and a.so_no = p.DELI_NUM "
                                + " and a.part_id = e.part_id "
                                + " and e.part_no = d.ictpn "
                                + " group by p.AC_PN_DESC, "
                                + " p.REF_NUM, "
                                + " p.AC_PO_LINE, "
                                + " p.AC_PN, "
                                + " p.DELI_NUM, "
                                + " p.MODEL_NO, "
                                + " p.UNIT_PRICE, "
                                + " p.COO, "
                                + " a.po_no, "
                                + " a.mpn "
                                + " order by a.po_no,p.AC_PO_LINE, a.mpn";

            //获得Header Information
            string headerDoc = @"select a.invoice_no,a.hawb,a.carr_code,a.ship_per,a.st_addr,b.ac_po,b.ac_pn,b.ac_pn_desc,sum(b.qty) as qty,a.st_name
                                    from wmuser.AC_FD_AMR_PL_HEADER@dgedi a,
                                    wmuser.AC_FD_AMR_PL_LINE@dgedi b
                                    where   
                                    a.invoice_no = '" + invoiceNumber + "' and a.invoice_no = b.invoice_no group by  a.invoice_no,a.hawb,a.hawb,a.carr_code,a.ship_per,a.st_addr,b.ac_po,b.ac_pn,b.ac_pn_desc,b.qty,a.st_name ";

            //获得Gross Weight,Volume
            string weightAndVolume = @"select sum(a.allpallet_weight) as Gross_Weight, sum(a.Volume) as volume
                                        from (select distinct t.so_no,
                                        t.allpallet_weight,
                                        t.pallet_length * t.pallet_width * t.pallet_height /
                                        1000000 as Volume
                                        from ppsuser.g_palletedi_info t, ppsuser.g_shipping_detail_t b
                                        where t.so_no = '" + so_no + "'"
                                        + @"and t.so_no = b.so_no
                                        and t.sscc = b.sscc) a
                                        group by a.so_no";

            string packageSql = @"select sum(aa.qty / dd.sn_qty) no_package
                                from ppsuser.g_shipping_detail aa,
                                sajet.sys_part            bb,
                                ppsuser.g_ds_partinfo_t   cc,
                                ppsuser.g_ds_packinfo_t   dd
                                where aa.so_no = '" + so_no
                                + "'and aa.part_id = bb.part_id and bb.part_no = cc.ictpn and cc.pack_code = dd.pack_code";
            string shipDateSql = @"SELECT distinct ship_date FROM PPSUSER.G_SHIPPING_DETAIL_T WHERE invoiceno = '" + invoiceNumber + "' and so_no = '" + so_no + "'";
            string allPalletWgSql = @"select SUM(NVL(STANDARD_WEIGHT,0)) ALLPALLET_WEIGHT
                                        from ppsuser.g_palletedi_info A
                                        where A.SSCC IN (SELECT DISTINCT SSCC FROM PPSUSER.G_SHIPPING_DETAIL_T WHERE SO_NO='" + so_no + "')";
            string HAWB = "";
            string currentDate = "";
            String filePath = "";
            currentDate = DateTime.Now.ToString("yyyyMMdd");
            DataTable headerDt = Util.getDataTaleC(headerSql, "Header");
            if (headerDt != null && headerDt.Rows.Count > 0)
            {

                HAWB = headerDt.Rows[0][14].ToString();
            }
            filePath = diskPath + currentDate + " " + HAWB + "Import Customs Clearance Doc.CCI.pdf";
            diskCompletePath = filePath;
            ds.Tables.Add(headerDt);
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            ds.Tables.Add(Util.getDataTaleC(weightAndVolume, "weightAndVolume"));
            ds.Tables.Add(Util.getDataTaleC(packageSql, "Package"));
            ds.Tables.Add(Util.getDataTaleC(shipDateSql, "ShipDate"));
            ds.Tables.Add(Util.getDataTaleC(allPalletWgSql, "AllPalletWg"));

            if (print)
            {
                Util.CreateDataTable(Constant.FDAPACShippingInvoice_URL, ds);
            }
            else
            {
                Util.printPDFCrystalReportV2(Constant.FDAPACShippingInvoice_URL, ds, filePath);
            }
        }
    }
}
