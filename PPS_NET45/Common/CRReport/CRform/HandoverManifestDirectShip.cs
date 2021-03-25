using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRReport.CRfrom
{
    public partial class HandoverManifestDirectShip
    {
        public HandoverManifestDirectShip(string shipmentId, bool PRINTERorPDF, bool ISFIRST)
        {
            setDataSoure(shipmentId, PRINTERorPDF, ISFIRST, "");
        }

        public HandoverManifestDirectShip(string shipmentId, bool PRINTERorPDF, bool ISFIRST, string strPath)
        {
            setDataSoure(shipmentId, PRINTERorPDF, ISFIRST, strPath);
        }

        private void setDataSoure(String shipmentId, bool PRINTERorPDF, bool ISFIRST, string strPath)
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

            string lineSql = string.Format(@"select a.shipment_id,
                                                       '--' mawb,
                                                       b.hawb,
                                                       decode(b.shipment_type, 'FD', '', a.delivery_no) delivery_no,
                                                       a.delivery_no deliveryno,
                                                       a.line_item ac_dn_line,
                                                       a.mpn part_no,
                                                       a.assign_qty shipment_qty,
                                                       '--' psi_required,
                                                       c.pallet_no pallet_id,
                                                       a.ictpn ictpn
                                                  from ppsuser.t_pallet_order    a,
                                                       ppsuser.t_shipment_info   b,
                                                       ppsuser.t_shipment_pallet c
                                                 where a.shipment_id = b.shipment_id
                                                   and a.pallet_no = c.pallet_no
                                                   and a.shipment_id = c.shipment_id
                                                   and b.shipment_id = '{0}'
                                                 order by c.pallet_no asc ,a.delivery_no asc", shipmentId);

            string lineSql2 = string.Format(@"select aa.shipment_id,
                                                   aa.mawb,
                                                   aa.hawb,
                                                   '' delivery_no,
                                                   '' deliveryno,
                                                   '' ac_dn_line,
                                                   aa.part_no,
                                                   sum(aa.assign_qty) shipment_qty,
                                                   aa.psi_required,
                                                   aa.pallet_id,
                                                   '' ictpn
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
                                             order by aa.pallet_id asc, aa.part_no asc
                                            ", shipmentId);


            string palletNoSql = string.Format("SELECT SHIPMENT_ID, count(distinct pallet_no) AS PALLET_QTY "
                                           + "  FROM ppsuser.t_shipment_pallet "
                                           + "  where shipment_id = '{0}' "
                                           + "  group by shipment_id", shipmentId);

            string strSIDinfo = string.Format(@"select a.region || a.type typeregion
                                                  from ppsuser.t_shipment_info a
                                                 where a.shipment_id = '{0}'", shipmentId);
            DataTable dtsid = ClientUtils.ExecuteSQL(strSIDinfo).Tables[0];

            ds.Tables.Clear();

            if (dtsid.Rows[0]["typeregion"].ToString().Equals("PACBULK"))
            {
                ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
                ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
                ds.Tables.Add(Util.getDataTaleC(palletNoSql, "PalletNo"));
            }
            else
            {
                ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
                ds.Tables.Add(Util.getDataTaleC(lineSql2, "Line"));
                ds.Tables.Add(Util.getDataTaleC(palletNoSql, "PalletNo"));
            }
            if (PRINTERorPDF)
            {

                //MessageBox.Show("正常水晶报表打印");  一样要打印PDF做记录
                if (ISFIRST)
                {
                    Util.CreateDataTableADDcount(Constant.HandoverManifestDirectShip_URL, ds, 2);

                }
                else
                {
                    Util.CreateDataTableADDcount(Constant.HandoverManifestDirectShip_URL, ds, 1);
                }


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
                Util.printPDFCrystalReportV2(Constant.HandoverManifestDirectShip_URL, ds, completeDiskPath);

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
                Util.printPDFCrystalReportV2(Constant.HandoverManifestDirectShip_URL, ds, completeDiskPath);

                /**Summary:printPDFCrystalReport此方法用于将CR报表导出为PDF，并保存在指定的文件中
    *Paramater List:  ①.Crystal Report 报表rpt文件路径（在Constant中）  ②.获取的Dataset 
    *                 ③.保存PDF文件的路径
    *Returns :   void     ---By Lk 2018/07/08  **/

            }

        }


        //EOS
        public HandoverManifestDirectShip(string strSID, string strCrystalFullPath, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            setDataSoure2(strSID, strCrystalFullPath, isPDF, strPDFPath, nCopies, isOnlyGetDS, out dtCrystal, out serverFullLabelName);
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
            string lineSql = string.Format(@"select a.shipment_id,
                                                       '--' mawb,
                                                       b.hawb,
                                                       decode(b.shipment_type, 'FD', '', a.delivery_no) delivery_no,
                                                       a.delivery_no deliveryno,
                                                       a.line_item ac_dn_line,
                                                       a.mpn part_no,
                                                       a.assign_qty shipment_qty,
                                                       '--' psi_required,
                                                       c.pallet_no pallet_id,
                                                       a.ictpn ictpn
                                                  from ppsuser.t_pallet_order    a,
                                                       ppsuser.t_shipment_info   b,
                                                       ppsuser.t_shipment_pallet c
                                                 where a.shipment_id = b.shipment_id
                                                   and a.pallet_no = c.pallet_no
                                                   and a.shipment_id = c.shipment_id
                                                   and b.shipment_id = '{0}'
                                                 order by c.pallet_no asc ,a.delivery_no asc", shipmentId);
            #endregion
            # region  lineSql2
            string lineSql2 = string.Format(@"select aa.shipment_id,
                                                   aa.mawb,
                                                   aa.hawb,
                                                   '' delivery_no,
                                                   '' deliveryno,
                                                   '' ac_dn_line,
                                                   aa.part_no,
                                                   sum(aa.assign_qty) shipment_qty,
                                                   aa.psi_required,
                                                   aa.pallet_id,
                                                   '' ictpn
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
                                             order by aa.pallet_id asc, aa.part_no asc
                                            ", shipmentId);

            #endregion

            string palletNoSql = string.Format("SELECT SHIPMENT_ID, count(distinct pallet_no) AS PALLET_QTY "
                                           + "  FROM ppsuser.t_shipment_pallet "
                                           + "  where shipment_id = '{0}' "
                                           + "  group by shipment_id", shipmentId);

            string strSIDinfo = string.Format(@"select a.region || a.type typeregion
                                                  from ppsuser.t_shipment_info a
                                                 where a.shipment_id = '{0}'", shipmentId);
            DataTable dtsid = ClientUtils.ExecuteSQL(strSIDinfo).Tables[0];

            ds.Tables.Clear();


            if (dtsid.Rows[0]["typeregion"].ToString().Equals("PACBULK"))
            {
                ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
                ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
                ds.Tables.Add(Util.getDataTaleC(palletNoSql, "PalletNo"));
            }
            else
            {
                ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
                ds.Tables.Add(Util.getDataTaleC(lineSql2, "Line"));
                ds.Tables.Add(Util.getDataTaleC(palletNoSql, "PalletNo"));
            }


            if (string.IsNullOrEmpty(strCrystalFullPath))
            {
                strCrystalFullPath = Constant.HandoverManifestDirectShip_URL;
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
