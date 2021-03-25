using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class HanManCRFrom
    {
        public HanManCRFrom(String acDn)
        {
            InitializeC(acDn);
        }

        private void InitializeC(String acDn)
        {
            //初始化 DataSet
            DataSet ds = new DataSet();

            //AcFdHome
            string strSql = @"SELECT * FROM WMUSER.AC_FD_HOMA_HEADER@dgedi T0 WHERE T0.ICT_DN = '" + acDn + "'";

            //total_qty_PO
            string qtySql = @"select sum(t.qty),count(distinct t.po_no),t.ship_date  
                            from ppsuser.g_shipping_detail_t@ksppsa t
                            where t.so_no = '" + acDn + "' GROUP BY  t.ship_date ";

            //line
            string line = @"SELECT  a.so_no,E.HAWB,a.po_no,a.mpn,a.sscc,c.pallet_no,a.qty FROM ppsuser.g_shipping_detail_t@ksppsa a ,ppsuser.g_shipping_pallet_label_print@ksppsa c ,(SELECT DISTINCT HAWB,SO_NO FROM ppsuser.g_shipping_detail WHERE SO_NO= '" + acDn + "') E where a.so_no in ( '" + acDn + "') and a.invoiceno = c.invoice and a.sscc = c.sscc AND A.SO_NO=E.SO_NO";



            //栈板数
            string palletedi = @"select count(1) AS COUNT 
                                        from 
                                        (
                                        select distinct t.sscc
                                        from  ppsuser.g_shipping_detail_t t
                                        where t.so_no = '" + acDn + "'group by t.SSCC) a";


            //箱数量
            string total_cartons = @" select sum(cqty) as total_cartons
                                    from ppsuser.g_shipping_detail_t
                                    where so_no = '" + acDn + "'";

            ds.Tables.Add(Util.getDataTaleC(strSql, "AcFdHome"));
            ds.Tables.Add(Util.getDataTaleC(qtySql, "total_qty_PO"));
            ds.Tables.Add(Util.getDataTaleC(line, "line"));
            ds.Tables.Add(Util.getDataTaleC(palletedi, "栈板数"));
            ds.Tables.Add(Util.getDataTaleC(total_cartons, "箱数"));
            Util.CreateDataTable(Constant.HandoverManifest_URL, ds);
        }
    }
}
