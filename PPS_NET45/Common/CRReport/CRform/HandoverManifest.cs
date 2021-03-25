using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRReport.CRfrom
{
    public partial class HandoverManifest
    {
        public HandoverManifest(string shipmentId, bool PRINTERorPDF, bool ISFIRST)
        {
            setDataSoure3(shipmentId, PRINTERorPDF, ISFIRST);
        }
        public HandoverManifest(string shipmentId, string strpalletno, bool PRINTERorPDF, bool ISFIRST, string strPath, string strnull, string strnull2)
        {
            //CarBillList
            setDataSoure4(shipmentId, strpalletno, PRINTERorPDF, ISFIRST, strPath);
        }
        public HandoverManifest(string shipmentId, bool PRINTERorPDF, bool ISFIRST, string strPath, string strnull, string strnull2)
        {
            //CarBillList
            setDataSoure3(shipmentId, PRINTERorPDF, ISFIRST, strPath);
        }

        public HandoverManifest(string shipmentId, bool PRINTERorPDF, bool ISFIRST, string strStation)
        {
            setDataSoure2(shipmentId, PRINTERorPDF, ISFIRST, strStation, "");
            //司机提货清单 签字在页脚 strStation=SHIPMENT用
            //public static string HandoverManifest_URL = BASE_RUL + "HandoverManifestNEW.rpt";
            //Handover Manifest 签字在报表尾部 strStation=WEIGHT用
            //public static string HandoverManifest_URL2 = BASE_RUL + "HandoverManifestNEW2.rpt";
        }

        public HandoverManifest(string shipmentId, bool PRINTERorPDF, bool ISFIRST, string strStation, string strPath)
        {
            setDataSoure2(shipmentId, PRINTERorPDF, ISFIRST, strStation, strPath);
            //司机提货清单 签字在页脚 strStation=SHIPMENT用
            //public static string HandoverManifest_URL = BASE_RUL + "HandoverManifestNEW.rpt";
            //Handover Manifest 签字在报表尾部 strStation=WEIGHT用
            //public static string HandoverManifest_URL2 = BASE_RUL + "HandoverManifestNEW2.rpt";
        }

        private void setDataSoure(String shipmentId, bool PRINTERorPDF, bool ISFIRST)
        {
            //初始化 DataSet
            DataSet ds = new DataSet();
            //HYQ：目前用这个版本 
            string headerSql = string.Format("select b.SHIPMENT_ID, "
                                             + "     case when b.shipment_type = 'DS' and b.region ='PAC' then "
                                             + "    ( select min(n902) from pptest.oms_940_m_n9   "
                                             + "                    where n901='4B' and   ac_dn in (select delivery_no from ppsuser.t_order_info where shipment_id='{0}') )   "
                                             + "            else    '--' end  SHIP_POINT, "
                                             + "                case "
                                             + "                  when b.shipment_type = 'DS' then "
                                             + "                    b.carrier_code"
                                             + "                else "
                                             + "          (SELECT distinct SCACCODE "
                                             + "             FROM PPTEST.OMS_CARRIER_TRACKING_PREFIX c "
                                             + "            where trim(c.carriercode) = b.carrier_code "
                                             + "              and c.ShipMode = b.transport "
                                             + "              and c.isdisabled = '0' "
                                             + "              and c.type = 'HAWB')"
                                             + "            end CARRIER_NAME, "
                                             + "           case when b.shipment_type='DS' and  b.region='EMEIA' and b.poe='SA' then "
                                             + "                   (select  distinct  PORTOFENTRY  "
                                             + "                    from ppsuser.t_940_unicode  "
                                             + "                    where  (deliveryno,custdelitem) in  "
                                             + "                        (select delivery_no,line_item from ppsuser.t_order_info where shipment_id ='{0}'))  "
                                             + "                      else  b.poe end POE_COC, "
                                             + "          b.shipping_time SHIP_DATE, "
                                             + "          (select count(c.DELIVERY_NO) "
                                             + "             from(select distinct(DELIVERY_NO) "
                                             + "                     from ppsuser.t_order_info a "
                                             + "                    where a.SHIPMENT_ID = '{1}') c) TOTAL_DN, "
                                             + "          b.QTY TOTAL_QTY, "
                                             + "          b.CARTON_QTY CARTON_QTY "
                                             + "     from ppsuser.t_shipment_info b "
                                             + "    where b.SHIPMENT_ID = '{2}' ", shipmentId, shipmentId, shipmentId);

            string lineSql = string.Format(@"select aa.shipment_id,
                                                   aa.mawb,
                                                   aa.hawb,
                                                   aa.part_no,
                                                   aa.psi_required,
                                                   aa.pallet_id,
                                                   sum(aa.assign_qty) shipment_qty
                                              from (select a.shipment_id,
                                                           '--' mawb,
                                                           b.hawb,
                                                           decode(b.shipment_type, 'FD', '', a.delivery_no) delivery_no,
                                                           a.delivery_no deliveryno,
                                                           a.line_item ac_dn_line,
                                                           a.mpn part_no,
                                                           a.assign_qty,
                                                           '--' psi_required,
                                                           c.pallet_no pallet_id,
                                                           a.ictpn ictpn
                                                      from ppsuser.t_pallet_order    a,
                                                           ppsuser.t_shipment_info   b,
                                                           ppsuser.t_shipment_pallet c
                                                     where a.shipment_id = b.shipment_id
                                                       and a.pallet_no = c.pallet_no
                                                       and a.shipment_id = c.shipment_id
                                                       and b.shipment_id = '{0}') aa
                                             group by aa.shipment_id,
                                                      aa.mawb,
                                                      aa.hawb,
                                                      aa.part_no,
                                                      aa.psi_required,
                                                      aa.pallet_id
                                             order by aa.pallet_id asc,aa.part_no asc
                                            ", shipmentId);

            string palletNoSql = string.Format("SELECT SHIPMENT_ID, count(distinct pallet_no) AS PALLET_QTY "
                                           + "  FROM ppsuser.t_shipment_pallet "
                                           + "  where shipment_id = '{0}' "
                                           + "  group by shipment_id", shipmentId);

            ds.Tables.Clear();


            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            ds.Tables.Add(Util.getDataTaleC(palletNoSql, "PalletNo"));

            if (PRINTERorPDF)
            {
                //MessageBox.Show("正常水晶报表打印");  一样要打印PDF做记录
                if (ISFIRST)
                {
                    Util.CreateDataTableADDcount(Constant.HandoverManifest_URL, ds, 2);
                }
                else
                {
                    Util.CreateDataTableADDcount(Constant.HandoverManifest_URL, ds, 1);
                }

                //MessageBox.Show("PDF打印");
                string completeDiskPath = "";
                DateTime d = DateTime.Now;
                if (ISFIRST)
                {
                    completeDiskPath = Application.StartupPath + "\\PDF\\" + "HM_" + shipmentId + "_" + d.ToString("yyyyMMddHHmmss") + ".pdf";
                }
                else
                {
                    completeDiskPath = Application.StartupPath + "\\PDF\\" + "HMRE_" + shipmentId + "_" + d.ToString("yyyyMMddHHmmss") + ".pdf";
                }
                Util.printPDFCrystalReportV2(Constant.HandoverManifest_URL, ds, completeDiskPath);

                /**Summary:printPDFCrystalReport此方法用于将CR报表导出为PDF，并保存在指定的文件中
                *Paramater List:  ①.Crystal Report 报表rpt文件路径（在Constant中）  ②.获取的Dataset 
                *                 ③.保存PDF文件的路径
                *Returns :   void     ---By Lk 2018/07/08  **/
            }
            else
            {
                //MessageBox.Show("PDF打印");
                string completeDiskPath = "";
                DateTime d = DateTime.Now;
                completeDiskPath = Application.StartupPath + "\\PDF\\" + "HM_" + shipmentId + "_" + d.ToString("yyyyMMddHHmmss") + ".pdf";
                Util.printPDFCrystalReportV2(Constant.HandoverManifest_URL, ds, completeDiskPath);

                /**Summary:printPDFCrystalReport此方法用于将CR报表导出为PDF，并保存在指定的文件中
    *Paramater List:  ①.Crystal Report 报表rpt文件路径（在Constant中）  ②.获取的Dataset 
    *                 ③.保存PDF文件的路径
    *Returns :   void     ---By Lk 2018/07/08  **/
            }

        }

        private void setDataSoure3(String shipmentId, bool PRINTERorPDF, bool ISFIRST)
        {
            DataSet ds = new DataSet();
            string lineSql = string.Format(@"
                SELECT d.HAWB,f.MPN AS PART_NO,f.PALLET_NO AS PALLET_ID,sum(f.ASSIGN_QTY) AS SHIPMENT_QTY
                FROM pptest.oms_load_car a 
                INNER JOIN ppsuser.T_SHIPMENT_INFO d ON a.SHIPMENT_ID=d.SHIPMENT_ID
                INNER JOIN ppsuser.T_SHIPMENT_PALLET e ON a.SHIPMENT_ID=e.SHIPMENT_ID AND a.pallet_no=e.pallet_no
                INNER JOIN ppsuser.T_PALLET_ORDER f ON a.SHIPMENT_ID=f.SHIPMENT_ID AND a.pallet_no=f.PALLET_NO
                WHERE (a.car_no,TRUNC(a.expectedtime)) IN
                (
                SELECT DISTINCT b.car_no,TRUNC(c.SHIPPING_TIME) 
                FROM pptest.oms_load_car b INNER JOIN ppsuser.T_SHIPMENT_INFO c ON b.SHIPMENT_ID=c.SHIPMENT_ID
                INNER JOIN ppsuser.T_SHIPMENT_PALLET c ON b.SHIPMENT_ID=c.SHIPMENT_ID
                WHERE c.SHIPMENT_ID='{0}' OR c.PALLET_NO='{0}' OR c.REAL_PALLET_NO='{0}'
                )
                GROUP BY d.HAWB,f.MPN,f.PALLET_NO
                ORDER BY d.HAWB,f.PALLET_NO,f.MPN ASC
                ", shipmentId);
            string headerSql = string.Format(@"
              select to_char(max(a.expectedtime), 'YYYY/MM/DD') as shipdate,
       listagg(count(distinct a.pallet_no), ',') WITHIN GROUP(ORDER BY a.car_no) as totalpallets,
       listagg(sum(e.qty), ',') WITHIN GROUP(ORDER BY a.car_no) as totalqty,       
       listagg(a.car_no, ',') WITHIN GROUP(ORDER BY a.car_no) as carno,
       listagg(a.drivername, ',') WITHIN GROUP(ORDER BY a.car_no) as drivername,
       listagg(a.driverphone, ',') WITHIN GROUP(ORDER BY a.car_no) as drivertel
                  from pptest.oms_load_car a
                 inner join ppsuser.t_shipment_pallet e
                    on a.shipment_id = e.shipment_id
                   and a.pallet_no = e.pallet_no
                 where (a.car_no, trunc(a.expectedtime)) in
                       (select distinct b.car_no, trunc(c.shipping_time)
                          from pptest.oms_load_car b
                         inner join ppsuser.t_shipment_info c
                            on b.shipment_id = c.shipment_id
                         inner join ppsuser.t_shipment_pallet c
                            on b.shipment_id = c.shipment_id
                         where c.shipment_id = '{0}'
                            or c.pallet_no = '{0}'
                            or c.real_pallet_no = '{0}')
              group by a.expectedtime, a.car_no, a.drivername, a.driverphone
                ", shipmentId);
            //string headerSql = string.Format(@"
            //    SELECT to_char(max(a.expectedtime),'YYYY/MM/DD') AS SHIPDATE,count(DISTINCT a.pallet_no) AS TOTALPALLETS,
            //    sum(e.qty) AS TOTALQTY,max(a.car_no) AS CARNO,max(a.drivername) AS DRIVERNAME,max(driverphone) AS DRIVERTEL
            //    FROM pptest.oms_load_car a 
            //    INNER JOIN ppsuser.T_SHIPMENT_PALLET e ON a.SHIPMENT_ID=e.SHIPMENT_ID AND a.pallet_no=e.pallet_no
            //    WHERE (a.car_no,TRUNC(a.expectedtime)) IN
            //    (
            //    SELECT DISTINCT b.car_no,TRUNC(c.SHIPPING_TIME) 
            //    FROM pptest.oms_load_car b INNER JOIN ppsuser.T_SHIPMENT_INFO c ON b.SHIPMENT_ID=c.SHIPMENT_ID
            //    INNER JOIN ppsuser.T_SHIPMENT_PALLET c ON b.SHIPMENT_ID=c.SHIPMENT_ID
            //    WHERE c.SHIPMENT_ID='{0}' OR c.PALLET_NO='{0}' OR c.REAL_PALLET_NO='{0}'
            //    )
            //    ", shipmentId);
            ds.Tables.Clear();
            DataTable dtHeader = Util.getDataTaleC(headerSql, "Header");
            if ((dtHeader == null) || (dtHeader.Rows.Count <= 0))
            {
                throw new Exception("未维护装车信息或装车日期与集货单出货日期不符,请检查！");
            }
            DataTable dtLine = Util.getDataTaleC(lineSql, "Line");
            if ((dtLine == null) || (dtLine.Rows.Count <= 0))
            {
                throw new Exception("未维护装车信息或装车日期与集货单出货日期不符,请检查！");
            }
            ds.Tables.Add(dtHeader);
            ds.Tables.Add(dtLine);

            //MessageBox.Show("正常水晶报表打印");  一样要打印PDF做记录
            if (ISFIRST)
            {
                Util.CreateDataTableADDcount(Constant.CarManifest_URL, ds, 2);
            }
            else
            {
                Util.CreateDataTableADDcount(Constant.CarManifest_URL, ds, 1);
            }
        }


        private void setDataSoure3(String shipmentId, bool PRINTERorPDF, bool ISFIRST, string strPath)
        {
            DataSet ds = new DataSet();
            #region linesql
            string lineSql = string.Format(@"
                                            select d.hawb,
                                   f.mpn as part_no,
                                   f.pallet_no as pallet_id,
                                   sum(f.assign_qty) as shipment_qty
                              from pptest.oms_load_car a
                             inner join ppsuser.t_shipment_info d
                                on a.shipment_id = d.shipment_id
                             inner join ppsuser.t_shipment_pallet e
                                on a.shipment_id = e.shipment_id
                               and a.pallet_no = e.pallet_no
                             inner join ppsuser.t_pallet_order f
                                on a.shipment_id = f.shipment_id
                               and a.pallet_no = f.pallet_no
                             where (a.car_no, trunc(a.expectedtime)) in
                                   (select distinct b.car_no, trunc(c.shipping_time)
                                      from pptest.oms_load_car b
                                     inner join ppsuser.t_shipment_info c
                                        on b.shipment_id = c.shipment_id
                                     inner join ppsuser.t_shipment_pallet c
                                        on b.shipment_id = c.shipment_id
                                     where c.shipment_id = '{0}'
                                        or c.pallet_no = '{0}'
                                        or c.real_pallet_no = '{0}')
                             group by d.hawb, f.mpn, f.pallet_no
                             order by d.hawb, f.pallet_no, f.mpn asc
                                            ", shipmentId);
            #endregion

            #region  headerSql
            string headerSql = string.Format(@"
              select to_char(max(a.expectedtime), 'YYYY/MM/DD') as shipdate,
       listagg(count(distinct a.pallet_no), ',') WITHIN GROUP(ORDER BY a.car_no) as totalpallets,
       listagg(sum(e.qty), ',') WITHIN GROUP(ORDER BY a.car_no) as totalqty,       
       listagg(a.car_no, ',') WITHIN GROUP(ORDER BY a.car_no) as carno,
       listagg(a.drivername, ',') WITHIN GROUP(ORDER BY a.car_no) as drivername,
       listagg(a.driverphone, ',') WITHIN GROUP(ORDER BY a.car_no) as drivertel
                  from pptest.oms_load_car a
                 inner join ppsuser.t_shipment_pallet e
                    on a.shipment_id = e.shipment_id
                   and a.pallet_no = e.pallet_no
                 where (a.car_no, trunc(a.expectedtime)) in
                       (select distinct b.car_no, trunc(c.shipping_time)
                          from pptest.oms_load_car b
                         inner join ppsuser.t_shipment_info c
                            on b.shipment_id = c.shipment_id
                         inner join ppsuser.t_shipment_pallet c
                            on b.shipment_id = c.shipment_id
                         where c.shipment_id = '{0}'
                            or c.pallet_no = '{0}'
                            or c.real_pallet_no = '{0}')
              group by a.expectedtime, a.car_no, a.drivername, a.driverphone
                ", shipmentId);
            #endregion

            ds.Tables.Clear();

            DataTable dtHeader = Util.getDataTaleC(headerSql, "Header");
            if ((dtHeader == null) || (dtHeader.Rows.Count <= 0))
            {
                throw new Exception("未维护装车信息或装车日期与集货单出货日期不符,请检查！");
            }
            DataTable dtLine = Util.getDataTaleC(lineSql, "Line");
            if ((dtLine == null) || (dtLine.Rows.Count <= 0))
            {
                throw new Exception("未维护装车信息或装车日期与集货单出货日期不符,请检查！");
            }
            ds.Tables.Add(dtHeader);
            ds.Tables.Add(dtLine);
            //ds.WriteXmlSchema(@"C:\aa.xml");
            string strCRReportName = string.Empty;
            strCRReportName = Constant.CarManifest_URL;
            int iisfirst = 0;
            if (ISFIRST)
            {
                iisfirst = 2;
            }
            else
            {
                iisfirst = 1;
            }

            if (PRINTERorPDF)
            {

                Util.CreateDataTableADDcount(strCRReportName, ds, iisfirst);
                string completeDiskPath = "";
                DateTime d = DateTime.Now;
                if (ISFIRST)
                {
                    completeDiskPath = Application.StartupPath + "\\PDF\\" + "CBL_" + shipmentId + "_" + d.ToString("yyyyMMddHHmmss") + ".pdf";
                }
                else
                {
                    completeDiskPath = Application.StartupPath + "\\PDF\\" + "CBLRE_" + shipmentId + "_" + d.ToString("yyyyMMddHHmmss") + ".pdf";
                }
                if (!string.IsNullOrEmpty(strPath))
                {
                    completeDiskPath = strPath;
                }
                Util.printPDFCrystalReportV2(strCRReportName, ds, completeDiskPath);

                /**Summary:printPDFCrystalReport此方法用于将CR报表导出为PDF，并保存在指定的文件中
                *Paramater List:  ①.Crystal Report 报表rpt文件路径（在Constant中）  ②.获取的Dataset 
                *                 ③.保存PDF文件的路径
                *Returns :   void     ---By Lk 2018/07/08  **/
            }
            else
            {
                //MessageBox.Show("PDF打印");
                string completeDiskPath = "";
                DateTime d = DateTime.Now;
                completeDiskPath = Application.StartupPath + "\\PDF\\" + "CBL_" + shipmentId + "_" + d.ToString("yyyyMMddHHmmss") + ".pdf";
                if (!string.IsNullOrEmpty(strPath))
                {
                    completeDiskPath = strPath;
                }
                Util.printPDFCrystalReportV2(strCRReportName, ds, completeDiskPath);

                /**Summary:printPDFCrystalReport此方法用于将CR报表导出为PDF，并保存在指定的文件中
                *Paramater List:  ①.Crystal Report 报表rpt文件路径（在Constant中）  ②.获取的Dataset 
                *                 ③.保存PDF文件的路径
                *Returns :   void     ---By Lk 2018/07/08  **/

            }
        }

        private void setDataSoure2(String shipmentId, bool PRINTERorPDF, bool ISFIRST, string strStation, string strPath)
        {
            //初始化 DataSet
            DataSet ds = new DataSet();
            //HYQ：目前用这个版本 
            string headerSql = string.Format("select b.SHIPMENT_ID, "
                                             + "     case when b.shipment_type = 'DS' and b.region ='PAC' then "
                                             + "    ( select min(n902) from pptest.oms_940_m_n9   "
                                             + "                    where n901='4B' and   ac_dn in (select delivery_no from ppsuser.t_order_info where shipment_id='{0}') )   "
                                             + "            else    '--' end  SHIP_POINT, "
                                             + "                case "
                                             + "                  when b.shipment_type = 'DS' then "
                                             + "                    b.carrier_code"
                                             + "                else "
                                             + "          (SELECT distinct SCACCODE "
                                             + "             FROM PPTEST.OMS_CARRIER_TRACKING_PREFIX c "
                                             + "            where trim(c.carriercode) = b.carrier_code "
                                             + "              and c.ShipMode = b.transport "
                                             + "              and c.isdisabled = '0' "
                                             + "              and c.type = 'HAWB')"
                                             + "            end CARRIER_NAME, "
                                             + "           case when b.shipment_type='DS' and  b.region='EMEIA' and b.poe='SA' then "
                                             + "                   (select  distinct  PORTOFENTRY  "
                                             + "                    from ppsuser.t_940_unicode  "
                                             + "                    where  (deliveryno,custdelitem) in  "
                                             + "                        (select delivery_no,line_item from ppsuser.t_order_info where shipment_id ='{0}'))  "
                                             + "                      else  b.poe end POE_COC, "
                                             + "          b.shipping_time SHIP_DATE, "
                                             + "          (select count(c.DELIVERY_NO) "
                                             + "             from(select distinct(DELIVERY_NO) "
                                             + "                     from ppsuser.t_order_info a "
                                             + "                    where a.SHIPMENT_ID = '{1}') c) TOTAL_DN, "
                                             + "          b.QTY TOTAL_QTY, "
                                             + "          b.CARTON_QTY CARTON_QTY "
                                             + "     from ppsuser.t_shipment_info b "
                                             + "    where b.SHIPMENT_ID = '{2}' ", shipmentId, shipmentId, shipmentId);

            string lineSql = string.Format(@"select aa.shipment_id,
                                                   aa.mawb,
                                                   aa.hawb,
                                                   aa.part_no,
                                                   aa.psi_required,
                                                   aa.pallet_id,
                                                   sum(aa.assign_qty) shipment_qty
                                              from (select a.shipment_id,
                                                           '--' mawb,
                                                           b.hawb,
                                                           decode(b.shipment_type, 'FD', '', a.delivery_no) delivery_no,
                                                           a.delivery_no deliveryno,
                                                           a.line_item ac_dn_line,
                                                           a.mpn part_no,
                                                           a.assign_qty,
                                                           '--' psi_required,
                                                           c.pallet_no pallet_id,
                                                           a.ictpn ictpn
                                                      from ppsuser.t_pallet_order    a,
                                                           ppsuser.t_shipment_info   b,
                                                           ppsuser.t_shipment_pallet c
                                                     where a.shipment_id = b.shipment_id
                                                       and a.pallet_no = c.pallet_no
                                                       and a.shipment_id = c.shipment_id
                                                       and b.shipment_id = '{0}') aa
                                             group by aa.shipment_id,
                                                      aa.mawb,
                                                      aa.hawb,
                                                      aa.part_no,
                                                      aa.psi_required,
                                                      aa.pallet_id
                                             order by aa.pallet_id asc,aa.part_no asc
                                            ", shipmentId);

            string palletNoSql = string.Format("SELECT SHIPMENT_ID, count(distinct pallet_no) AS PALLET_QTY "
                                           + "  FROM ppsuser.t_shipment_pallet "
                                           + "  where shipment_id = '{0}' "
                                           + "  group by shipment_id", shipmentId);

            ds.Tables.Clear();

            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            ds.Tables.Add(Util.getDataTaleC(palletNoSql, "PalletNo"));


            //司机提货清单 签字在页脚 strStation=SHIPMENT用
            //public static string HandoverManifest_URL = BASE_RUL + "HandoverManifestNEW.rpt";
            //Handover Manifest 签字在报表尾部 strStation=WEIGHT用
            //public static string HandoverManifest_URL2 = BASE_RUL + "HandoverManifestNEW2.rpt";

            string strCRReportName = string.Empty;
            if (strStation.Equals("WEIGHT"))
            {
                //只有称重站会用这个DATA
                strCRReportName = Constant.HandoverManifest_URL2;
            }
            else//Shipment 或者旧方法使用
            {
                //strCRReportName = Constant.HandoverManifest_URL;
            }
            if (PRINTERorPDF)
            {
                //MessageBox.Show("正常水晶报表打印");  一样要打印PDF做记录
                if (ISFIRST)
                {
                    Util.CreateDataTableADDcount(strCRReportName, ds, 2);
                }
                else
                {
                    Util.CreateDataTableADDcount(strCRReportName, ds, 1);
                }

                //MessageBox.Show("PDF打印");
                string completeDiskPath = "";
                DateTime d = DateTime.Now;
                if (ISFIRST)
                {
                    completeDiskPath = Application.StartupPath + "\\PDF\\" + "HM_" + shipmentId + "_" + d.ToString("yyyyMMddHHmmss") + ".pdf";
                }
                else
                {
                    completeDiskPath = Application.StartupPath + "\\PDF\\" + "HMRE_" + shipmentId + "_" + d.ToString("yyyyMMddHHmmss") + ".pdf";
                }
                Util.printPDFCrystalReportV2(strCRReportName, ds, completeDiskPath);

                /**Summary:printPDFCrystalReport此方法用于将CR报表导出为PDF，并保存在指定的文件中
                *Paramater List:  ①.Crystal Report 报表rpt文件路径（在Constant中）  ②.获取的Dataset 
                *                 ③.保存PDF文件的路径
                *Returns :   void     ---By Lk 2018/07/08  **/

            }
            else
            {
                string completeDiskPath = "";
                DateTime d = DateTime.Now;
                completeDiskPath = Application.StartupPath + "\\PDF\\" + "HM_" + shipmentId + "_" + d.ToString("yyyyMMddHHmmss") + ".pdf";
                if (!string.IsNullOrEmpty(strPath))
                {
                    completeDiskPath = strPath;
                }
                Util.printPDFCrystalReportV2(strCRReportName, ds, completeDiskPath);

            }

        }
        private void setDataSoure4(string shipmentId, string strpalletno, bool PRINTERorPDF, bool ISFIRST, string strPath)
        {
            DataSet ds = new DataSet();
            #region linesql
            string lineSql = string.Format(@"
                                            select d.hawb,
                                   f.mpn as part_no,
                                   f.pallet_no as pallet_id,
                                   sum(f.assign_qty) as shipment_qty
                              from pptest.oms_load_car a
                             inner join ppsuser.t_shipment_info d
                                on a.shipment_id = d.shipment_id
                             inner join ppsuser.t_shipment_pallet e
                                on a.shipment_id = e.shipment_id
                               and a.pallet_no = e.pallet_no
                             inner join ppsuser.t_pallet_order f
                                on a.shipment_id = f.shipment_id
                               and a.pallet_no = f.pallet_no
                             where (a.car_no, trunc(a.expectedtime)) in
                                   (select distinct b.car_no, trunc(y.shipping_time)
                                      from pptest.oms_load_car b
                                     inner join ppsuser.t_shipment_info y
                                        on b.shipment_id = y.shipment_id
                                        and trunc(b.expectedtime) =trunc(y.shipping_time)
                                     inner join ppsuser.t_shipment_pallet c
                                        on b.shipment_id = c.shipment_id
                                        and b.pallet_no =c.pallet_no
                                     where c.shipment_id = '{0}'
                                        and ( c.pallet_no = '{1}'
                                        or c.real_pallet_no = '{2}'))
                             group by d.hawb, f.mpn, f.pallet_no
                             order by d.hawb, f.pallet_no, f.mpn asc
                                            ", shipmentId, strpalletno, strpalletno);
            #endregion

            #region  headerSql
            string headerSql = string.Format(@"
                select to_char(max(a.expectedtime), 'YYYY/MM/DD') as shipdate,
                       count(distinct a.pallet_no) as totalpallets,
                       sum(e.qty) as totalqty,
                       max(a.car_no) as carno,
                       max(a.drivername) as drivername,
                       max(driverphone) as drivertel
                  from pptest.oms_load_car a
                 inner join ppsuser.t_shipment_pallet e
                    on a.shipment_id = e.shipment_id
                   and a.pallet_no = e.pallet_no
                 where (a.car_no, trunc(a.expectedtime)) in
                       (select distinct b.car_no, trunc(y.shipping_time)
                          from pptest.oms_load_car b
                         inner join ppsuser.t_shipment_info y
                            on b.shipment_id = y.shipment_id
                            and trunc(b.expectedtime) =trunc(y.shipping_time)
                         inner join ppsuser.t_shipment_pallet c
                            on b.shipment_id = c.shipment_id
                            and b.pallet_no =c.pallet_no
                         where c.shipment_id = '{0}'
                                        and ( c.pallet_no = '{1}'
                                        or c.real_pallet_no = '{2}'))
                ", shipmentId, strpalletno, strpalletno);
            #endregion

            ds.Tables.Clear();
            try
            {
                DataTable dtHeader = Util.getDataTaleC(headerSql, "Header");
                if ((dtHeader == null) || (dtHeader.Rows.Count <= 0))
                {
                    throw new Exception("未维护装车信息或装车日期与集货单出货日期不符,请检查！");
                }
                DataTable dtLine = Util.getDataTaleC(lineSql, "Line");
                if ((dtLine == null) || (dtLine.Rows.Count <= 0))
                {
                    throw new Exception("未维护装车信息或装车日期与集货单出货日期不符,请检查！");
                }
                ds.Tables.Add(dtHeader);
                ds.Tables.Add(dtLine);
                //ds.WriteXmlSchema(@"C:\aa.xml");
            }
            catch (Exception exc)
            {
                MessageBox.Show("水晶报表的数据产生出了问题" + exc.ToString());
                return;
            }
            string strCRReportName = string.Empty;
            strCRReportName = Constant.CarManifest_URL;
            int iisfirst = 0;
            if (ISFIRST)
            {
                iisfirst = 2;
            }
            else
            {
                iisfirst = 1;
            }

            if (PRINTERorPDF)
            {
                try
                {
                    Util.CreateDataTableADDcount(
                                                            strCRReportName,
                                                            ds, iisfirst);
                }
                catch (Exception e)
                {
                    MessageBox.Show("水晶报表打印出了问题" + e.ToString());
                }

                try
                {
                    string completeDiskPath = "";
                    DateTime d = DateTime.Now;
                    if (ISFIRST)
                    {
                        completeDiskPath = "D:\\MES_CLIENT\\PDF\\" + "CBL_" + shipmentId + "_" + d.ToString("yyyyMMddHHmmss") + ".pdf";
                    }
                    else
                    {
                        completeDiskPath = "D:\\MES_CLIENT\\PDF\\" + "CBLRE_" + shipmentId + "_" + d.ToString("yyyyMMddHHmmss") + ".pdf";
                    }
                    if (!string.IsNullOrEmpty(strPath))
                    {
                        completeDiskPath = strPath;
                    }
                    Util.printPDFCrystalReportV2(strCRReportName, ds, completeDiskPath);

                    /**Summary:printPDFCrystalReport此方法用于将CR报表导出为PDF，并保存在指定的文件中
                    *Paramater List:  ①.Crystal Report 报表rpt文件路径（在Constant中）  ②.获取的Dataset 
                    *                 ③.保存PDF文件的路径
                    *Returns :   void     ---By Lk 2018/07/08  **/
                }
                catch (Exception e)
                {
                    MessageBox.Show("水晶报表产生PDF出了问题" + e.ToString());
                }
            }
            else
            {
                try
                {
                    //MessageBox.Show("PDF打印");
                    string completeDiskPath = "";
                    DateTime d = DateTime.Now;
                    completeDiskPath = "D:\\MES_CLIENT\\PDF\\" + "CBL_" + shipmentId + "_" + d.ToString("yyyyMMddHHmmss") + ".pdf";
                    if (!string.IsNullOrEmpty(strPath))
                    {
                        completeDiskPath = strPath;
                    }
                    Util.printPDFCrystalReportV2(strCRReportName, ds, completeDiskPath);

                    /**Summary:printPDFCrystalReport此方法用于将CR报表导出为PDF，并保存在指定的文件中
                    *Paramater List:  ①.Crystal Report 报表rpt文件路径（在Constant中）  ②.获取的Dataset 
                    *                 ③.保存PDF文件的路径
                    *Returns :   void     ---By Lk 2018/07/08  **/
                }
                catch (Exception e)
                {
                    MessageBox.Show("水晶报表产生PDF出了问题" + e.ToString());
                }
            }
        }
        //EOS
        public HandoverManifest(string strSID, string strCrystalFullPath, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            setDataSoure2(strSID, strCrystalFullPath, isPDF, strPDFPath, nCopies, isOnlyGetDS, out dtCrystal, out serverFullLabelName);
            //司机提货清单 签字在页脚 strStation=SHIPMENT用
            //public static string HandoverManifest_URL = BASE_RUL + "HandoverManifestNEW.rpt";
            //Handover Manifest 签字在报表尾部 strStation=WEIGHT用
            //public static string HandoverManifest_URL2 = BASE_RUL + "HandoverManifestNEW2.rpt";
        }
        //EOS
        private void setDataSoure2(string shipmentId, string strCrystalFullPath, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            //初始化 DataSet
            DataSet ds = new DataSet();
            //HYQ：目前用这个版本 
            #region headerSql
            string headerSql = string.Format("select b.SHIPMENT_ID, "
                                             + "     case when b.shipment_type = 'DS' and b.region ='PAC' then "
                                             + "    ( select min(n902) from pptest.oms_940_m_n9   "
                                             + "                    where n901='4B' and   ac_dn in (select delivery_no from ppsuser.t_order_info where shipment_id='{0}') )   "
                                             + "            else    '--' end  SHIP_POINT, "
                                             + "                case "
                                             + "                  when b.shipment_type = 'DS' then "
                                             + "                    b.carrier_code"
                                             + "                else "
                                             + "          (SELECT distinct SCACCODE "
                                             + "             FROM PPTEST.OMS_CARRIER_TRACKING_PREFIX c "
                                             + "            where trim(c.carriercode) = b.carrier_code "
                                             + "              and c.ShipMode = b.transport "
                                             + "              and c.isdisabled = '0' "
                                             + "              and c.type = 'HAWB')"
                                             + "            end CARRIER_NAME, "
                                             + "           case when b.shipment_type='DS' and  b.region='EMEIA' and b.poe='SA' then "
                                             + "                   (select  distinct  PORTOFENTRY  "
                                             + "                    from ppsuser.t_940_unicode  "
                                             + "                    where  (deliveryno,custdelitem) in  "
                                             + "                        (select delivery_no,line_item from ppsuser.t_order_info where shipment_id ='{0}'))  "
                                             + "                      else  b.poe end POE_COC, "
                                             + "          b.shipping_time SHIP_DATE, "
                                             + "          (select count(c.DELIVERY_NO) "
                                             + "             from(select distinct(DELIVERY_NO) "
                                             + "                     from ppsuser.t_order_info a "
                                             + "                    where a.SHIPMENT_ID = '{1}') c) TOTAL_DN, "
                                             + "          b.QTY TOTAL_QTY, "
                                             + "          b.CARTON_QTY CARTON_QTY "
                                             + "     from ppsuser.t_shipment_info b "
                                             + "    where b.SHIPMENT_ID = '{2}' ", shipmentId, shipmentId, shipmentId);
            #endregion
            #region lineSql
            string lineSql = string.Format(@"select aa.shipment_id,
                                                   aa.mawb,
                                                   aa.hawb,
                                                   aa.part_no,
                                                   aa.psi_required,
                                                   aa.pallet_id,
                                                   sum(aa.assign_qty) shipment_qty
                                              from (select a.shipment_id,
                                                           '--' mawb,
                                                           b.hawb,
                                                           decode(b.shipment_type, 'FD', '', a.delivery_no) delivery_no,
                                                           a.delivery_no deliveryno,
                                                           a.line_item ac_dn_line,
                                                           a.mpn part_no,
                                                           a.assign_qty,
                                                           '--' psi_required,
                                                           c.pallet_no pallet_id,
                                                           a.ictpn ictpn
                                                      from ppsuser.t_pallet_order    a,
                                                           ppsuser.t_shipment_info   b,
                                                           ppsuser.t_shipment_pallet c
                                                     where a.shipment_id = b.shipment_id
                                                       and a.pallet_no = c.pallet_no
                                                       and a.shipment_id = c.shipment_id
                                                       and b.shipment_id = '{0}') aa
                                             group by aa.shipment_id,
                                                      aa.mawb,
                                                      aa.hawb,
                                                      aa.part_no,
                                                      aa.psi_required,
                                                      aa.pallet_id
                                             order by aa.pallet_id asc,aa.part_no asc
                                            ", shipmentId);
            #endregion
            string palletNoSql = string.Format("SELECT SHIPMENT_ID, count(distinct pallet_no) AS PALLET_QTY "
                                           + "  FROM ppsuser.t_shipment_pallet "
                                           + "  where shipment_id = '{0}' "
                                           + "  group by shipment_id", shipmentId);

            ds.Tables.Clear();

            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            ds.Tables.Add(Util.getDataTaleC(palletNoSql, "PalletNo"));


            if (string.IsNullOrEmpty(strCrystalFullPath))
            {
                strCrystalFullPath = Constant.HandoverManifest_URL2;
            }

            dtCrystal = ds;

            if (!isOnlyGetDS)
            {
                if (isPDF)
                {
                    if (string.IsNullOrEmpty(strPDFPath))
                    {
                        strPDFPath = AppDomain.CurrentDomain.BaseDirectory + @"\PDF\CR_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".PDF";
                    }
                    Util.printPDFCrystalReportV3(strCrystalFullPath, ds, strPDFPath, out serverFullLabelName);
                }
                else
                {
                    Util.CreateDataTableADDcount(strCrystalFullPath, ds, nCopies, out serverFullLabelName);
                }
            }
            else
            {
                string reportPath = Util.checkCRReportVersion(strCrystalFullPath, out serverFullLabelName);
            }
        }
    }
}
