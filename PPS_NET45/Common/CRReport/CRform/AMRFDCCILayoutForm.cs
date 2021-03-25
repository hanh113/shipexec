using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class AMRFDCCILayoutForm
    {
        public string diskCompletePath = "";
        public AMRFDCCILayoutForm(string ict_dn, string diskPath, bool print)
        {
          //  InitializeComponent();
            string querySQL = @"select distinct a.invoiceno from ppsuser.g_shipping_detail_t a where a.so_no = '" + ict_dn + "'";
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
            Initialize(invoiceNumber, diskPath, ict_dn, print);
        }

        public void Initialize(string invoiceNumber, string diskPath, string ict_dn, bool print)
        {
            DataSet ds = new DataSet();
            string headerSql = @"select distinct a.SHIPID_TT,
                                a.SUP_NAME1,
                                a.SUP_NAME2,
                                a.SUP_ADDR1,
                                a.SUP_ADDR2,
                                a.SUP_ADDR3,
                                a.SUP_TEL,
                                a.SO_NAME1,
                                a.SO_ADDR1,
                                a.SO_ADDR2,
                                a.SO_ADDR3,
                                a.SO_ADDR4,
                                a.SO_ADDR5,
                                a.POE,
                                b.carr_code  ,
                                b.ship_per as SHIP_CONDI_CODE,     
                                a.PORT_EXP, 
                                a.PAY_TERM,
                                a.INCOTERM,
                                c.invoiceno as INVOICE_NO,
                                b.HAWB,
                                a.SEL_NAME1,
                                a.SEL_NAME2,
                                a.SEL_ADDR1,
                                a.SEL_ADDR2,
                                a.SEL_ADDR3,
                                a.SEL_ADDR4,
                                a.SEL_ADDR5,
                                a.ST_NAME1,
                                a.ST_NAME2,
                                a.ST_ADDR1,
                                a.ST_ADDR2,
                                a.ST_ADDR3,
                                a.ST_ADDR4,
                                a.ST_ADDR5,
                                a.REMARK,
                                a.DECLAR           
                                from WMUSER.AC_AMR_FD_CCI_HEADER@dgedi a,
                                wmuser.ac_fd_amr_pl_header@dgedi      b,
                                ppsuser.g_shipping_detail_t           c
                                where a.shipid_tt = '" + ict_dn + "' "
                                + " and a.shipid_tt = c.so_no "
                                + " and b.shipid_tt = a.shipid_tt ";
            string lineSql = @"select t0.SHIPID_TT,
                                t0.CCI_LINE,
                                t0.COO,
                                t1.PO_NO AS FD_PO,
                                t0.AC_PN,
                                t0.MATE_DESC,
                                t0.AC_MODEL,
                                sum(a.PRODUCT_WEIGHT * t1.qty) as Net_Weight,
                                sum(a.ROUGH_WEIGHT * t1.qty) as Gross_Weight,
                                sum(t1.qty) qty,
                                t0.UNIT_PRICE,
                                sum(t1.qty * t0.UNIT_PRICE) extended
                                FROM (select DISTINCT * from WMUSER.AC_AMR_FD_CCI_LINE@dgedi) t0,
                                PPSUSER.g_shipping_detail_t t1,
                                ppsuser.g_ds_partinfo_t     a,
                                sajet.sys_part              b
                                where t0.SHIPID_TT = '" + ict_dn + "'"
                                + @"and t0.SHIPID_TT = t1.so_no
                                and t0.AC_PN = t1.mpn
                                and t1.part_id = b.part_id
                                and a.ictpn = b.part_no
                                group by t0.SHIPID_TT,
                                t0.CCI_LINE,
                                t0.COO,
                                t1.PO_NO,
                                t0.AC_PN,
                                t0.MATE_DESC,
                                t0.AC_MODEL,
                                t0.UNIT_PRICE 
                                order by t0.CCI_LINE, t0.AC_PN";
            

            string cartonSql = @"SELECT SUM(CQTY)total_cartons FROM PPSUSER.G_SHIPPING_DETAIL_T WHERE SO_NO= '" + ict_dn + "'";

            string palletSql = @"select count( distinct mix_pallets) as pallets 
                                from ppsuser.g_shipping_detail_t 
                                where so_no = '" + ict_dn + "'";
            string shipDateSql = @"select distinct ship_date from ppsuser.g_shipping_detail_t where so_no = '" + ict_dn + "'";
            string allPalletWgSql = @"select SUM(NVL(STANDARD_WEIGHT,0)) ALLPALLET_WEIGHT
                                        from ppsuser.g_palletedi_info A
                                        where A.SSCC IN (SELECT DISTINCT SSCC FROM PPSUSER.G_SHIPPING_DETAIL_T WHERE SO_NO='" + ict_dn + "')";
            DataTable dtHeader = Util.getDataTaleC(headerSql, "Header");
            string HAWB = "";
            string currentDate = "";
            currentDate = DateTime.Now.ToString("yyyyMMdd");
            if (dtHeader != null && dtHeader.Rows.Count > 0)
            {
                HAWB = dtHeader.Rows[0][1].ToString();
            }
            ds.Tables.Add(dtHeader);
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            ds.Tables.Add(Util.getDataTaleC(cartonSql, "Cartons"));
            ds.Tables.Add(Util.getDataTaleC(palletSql, "Pallets"));
            ds.Tables.Add(Util.getDataTaleC(shipDateSql, "ShipDate"));
            ds.Tables.Add(Util.getDataTaleC(allPalletWgSql, "AllPalletWg"));
            string filePath = "";
            filePath = diskPath + currentDate + " " + HAWB + " Import Customs Clearance Doc.CCI.pdf";
            diskCompletePath = filePath;
            if (print)
            {
              Util.CreateDataTable(Constant.FDAMRCCILayout_URL, ds);
            }
            else
            {
                Util.printPDFCrystalReportV2(Constant.FDAMRCCILayout_URL, ds, filePath);
            }
        }
    }
}
