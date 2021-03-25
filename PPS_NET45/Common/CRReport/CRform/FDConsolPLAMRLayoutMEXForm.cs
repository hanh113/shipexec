using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRReport.CRfrom
{
    public partial class FDConsolPLAMRLayoutMEXForm
    {
        public string diskCompletePath = "";
        public FDConsolPLAMRLayoutMEXForm(string shipID, bool print)
        {
            Initialize(shipID, print);
        }
        public void Initialize(string shipID, bool print)
        {
            DataSet ds = new DataSet();
            string headerSql = @"select distinct a.SHIPID_TT,
                                a.SUP_NAME1,
                                a.SUP_NAME2,
                                a.SUP_ADDR1,
                                a.SUP_ADDR2,
                                a.SUP_ADDR3,
                                a.SUP_TEL,
                                a.ST_NAME1,
                                a.ST_NAME2,
                                a.ST_ADDR1,
                                a.ST_ADDR2,
                                a.ST_ADDR3,
                                a.ST_ADDR4,
                                a.ST_ADDR5,
                                a.POE,
                                b.HAWB,
                                c.invoiceno as INVOICE,
                                b.carr_code  ,
                                b.ship_per as SHIP_CONDI_DESC,
                                a.PORT_EXP,
                                a.SO_NAME1,
                                a.SO_NAME2,
                                a.SO_ADDR1,
                                a.SO_ADDR2,
                                a.SO_ADDR3,
                                a.SO_ADDR4,
                                a.SO_ADDR5,
                                a.SHIP_TYPE,
                                a.DN_COUNT,
                                a.REMARK,
                                a.DECLAR           
                                from WMUSER.AC_AMR_FD_CPL_MEX_HEADER@dgedi a,
                                wmuser.ac_fd_amr_pl_header@dgedi      b,
                                ppsuser.g_shipping_detail_t           c
                                where a.shipid_tt = '" + shipID + "' "
                                + " and a.shipid_tt = c.so_no "
                                + " and b.shipid_tt = a.shipid_tt ";
            string palletSql = @"select count( distinct mix_pallets) as palletsNo 
                                from ppsuser.g_shipping_detail_t 
                                where so_no = '" + shipID + "'";
            string cartonSql = @"select sum(CQTY) Total_carton
                                from ppsuser.g_shipping_detail_t
                                where so_no = '" + shipID + "'";
            string qtyAndWgSql = @"select sum(a.qty) qty,
                                    sum(d.product_weight * a.qty) as net_weight,
                                    sum(d.rough_weight * a.qty) as gross_weight
                                    from ppsuser.g_shipping_detail_t a,
                                    ppsuser.g_ds_partinfo_t     d,
                                    sajet.sys_part              e
                                    where a.so_no = '" + shipID + "' "
                                    + " and a.part_id = e.part_id "
                                    + " and e.part_no = d.ictpn "
                                    + " GROUP by a.so_no";
            string lineSql = @"select t0.so_no as SHIPID_TT,
                                t1.LINE, 
                                t1.COO, 
                                t1.AC_PN, 
                                t1.MATE_DESC, 
                                t0.mpn, 
                                sum(t0.qty) as qty,
 							    sum(d.product_weight * t0.qty) as net_weight,               
                                sum(d.rough_weight * t0.qty) as gross_weight
                                from ppsuser.g_shipping_detail        t0,
                                WMUSER.AC_AMR_FD_CPL_MEX_LINE@dgedi t1,
                                ppsuser.g_ds_partinfo_t     d,
                                sajet.sys_part              e
                                where t0.so_no = '" + shipID + "'"
                                + " and t0.so_no = t1.SHIPID_TT(+) and t0.mpn = t1.AC_PN(+) "
                                + " and t0.part_id = e.part_id and e.part_no = d.ictpn "
                                + " group by t0.so_no,t1.LINE, t1.COO, t1.AC_PN, t1.MATE_DESC, t0.mpn order by t1.LINE ,t1.AC_PN";
            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            ds.Tables.Add(Util.getDataTaleC(qtyAndWgSql, "QtyAndWg"));
            ds.Tables.Add(Util.getDataTaleC(palletSql, "Pallets"));
            ds.Tables.Add(Util.getDataTaleC(cartonSql, "Cartons"));

            string tmp = "";
            tmp = Application.StartupPath + "\\PDF\\" + shipID + "FDConsolPLAMRMexico.pdf";
            diskCompletePath = tmp; //全局变量返回pdf路径
            if (print)
            {
                Util.CreateDataTable(Constant.FDAMRCPLUSAndPeru_URL, ds);
            }
            else
            {
                Util.printPDFCrystalReportV2(Constant.FDAMRCPLUSAndPeru_URL, ds, tmp);
            }
        }
    }
}
