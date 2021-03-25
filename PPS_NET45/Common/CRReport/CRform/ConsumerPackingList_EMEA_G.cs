using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class ConsumerPackingList_EMEA_G
    {
        public ConsumerPackingList_EMEA_G(string deliveryNo, string lineItem, string shipmentId)
        {
            setDataSoure(deliveryNo, lineItem, shipmentId, true, "");
        }

        public ConsumerPackingList_EMEA_G(string deliveryNo, string lineItem, string shipmentId, bool print, string strPath)
        {
            setDataSoure(deliveryNo, lineItem, shipmentId, print, strPath);
        }

        public ConsumerPackingList_EMEA_G(string deliveryNo, string lineItem)
        {
            setDataSoure2(deliveryNo, lineItem);
        }

        private void setDataSoure(string deliveryNo, string lineItem, string shipmentId, bool print, string strPath)
        {
            string headerSql = string.Format(@"select distinct decode(t9u.shipcntycode,
                                                               'FR',
                                                               T9U.SHIPTOCOMPANY,
                                                               'DE',
                                                               T9U.SHIPTOCOMPANY,
                                                               T9U.SHIPTONAME) as ST_NAME,
                                                            decode(t9u.shipcntycode,
                                                               'FR',
                                                               T9U.SHIPTONAME,
                                                               'DE',
                                                               T9U.SHIPTONAME,
                                                               T9U.SHIPTOCOMPANY)      AS  ST_COMPANY,
                                                        t9u.ShipToAddress as ST_ADDR1,
                                                        t9u.Shiptoaddress2 as ST_ADDR2,
                                                        t9u.Shiptoaddress3 as ST_ADDR3,
                                                        DECODE(T9U.SHIPCNTYCODE,'GB',t9u.ShiptoCity,'IE',t9u.ShiptoCity,t9u.ShiptoZip ||','||t9u.ShiptoCity) as ST_CITY,
                                                        DECODE(T9U.SHIPCNTYCODE,'GB',t9u.ShiptoZip,'') as ST_POSTAL,
                                                        DECODE(T9U.SHIPCNTYCODE,'GB',t9u.ShiptoAddress4,'IE',t9u.ShiptoAddress4,'') as ST_DISTRICT,
                                                        t9u.ShipToCountry as ST_COUNTRY,
                                                        '' as SHIP_FROM,
                                                        t9u.SoldToName as SO_NAME,
                                                        T9U.SOLDTOCOMPANY AS SO_COMPANY,
                                                        t9u.DELIVERYNO as AC_DN,
                                                        t9u.WEBORDERNO as WO_NUM,
                                                        TO_CHAR(to_date(t9u.orderdate, 'yyyyMMdd'),
                                                                'dd MON yyyy',
                                                                'NLS_DATE_LANGUAGE=american') as REQ_SHIP_DATE,
                                                        (select TO_CHAR(tsi.shipping_time,
                                                                        'dd MON yyyy',
                                                                        'NLS_DATE_LANGUAGE=american')
                                                           from ppsuser.t_shipment_info tsi
                                                          where tsi.shipment_id = '{2}') as SHIP_DATE,
                                                        decode(substr(t9u.shiproute, 1, 3),
                                                               'PST',
                                                               'Postal',
                                                               'Standard Shipping Method Only') as SHIP_VIA,
                                                        (select sum(toi.qty)
                                                           from ppsuser.t_order_info toi
                                                          where toi.delivery_no = '{0}'
                                                            and toi.shipment_id = '{2}') as TOT_QTY,
                                                        (select round(sum(t1.totalWeight), 2)
                                                           from (select TOI.ICTPN,
                                                                        TOI.DELIVERY_NO,
                                                                        vmi.GROSSWEIGHTKG * SUM(TOI.CARTON_QTY) as totalWeight
                                                                   from PPSUSER.T_ORDER_INFO TOI,
                                                                        pptest.vw_mpn_info   vmi
                                                                  where TOI.ICTPN = vmi.ICTPARTNO
                                                                    and TOI.DELIVERY_NO = '{0}'
                                                                    and toi.shipment_id = '{2}'
                                                                  group by TOI.ICTPN,
                                                                           TOI.DELIVERY_NO,
                                                                           vmi.GROSSWEIGHTKG) t1) as TOT_GW,
                                                        SUBSTR(T9U.euheadtext1, 4) as PKLST_TEXT1,
                                                        SUBSTR(T9U.euheadtext2, 4) as PKLST_TEXT2,
                                                        SUBSTR(T9U.euheadtext3, 4) as PKLST_TEXT3,
                                                        SUBSTR(T9U.euheadtext4, 4) as PKLST_TEXT4,
                                                        SUBSTR(T9U.euheadtext5, 4) as PKLST_TEXT5,
                                                        SUBSTR(T9U.euheadtext6, 4) as PKLST_TEXT6,
                                                        SUBSTR(T9U.euheadtext7, 4) as PKLST_TEXT7,
                                                        SUBSTR(T9U.euheadtext8, 4) as PKLST_TEXT8
                                          from ppsuser.t_940_unicode t9u
                                         where t9u.deliveryno = '{0}'
                                           and t9u.custdelitem = '{1}'", deliveryNo, lineItem, shipmentId);
            string lineSql = string.Format(@"   SELECT T.DELIVERY_NO as AC_DN,
                                                   T.LINE_ITEM as AC_DN_LINE,
                                                   T.MPN as AC_PN,
                                                   MATE_DESC,
                                                   T.msg1 as GIFT_MSG1,
                                                   T.msg2 as GIFT_MSG2,
                                                   T.msg3 as GIFT_MSG3,
                                                   T.msg4 as GIFT_MSG4,
                                                   T.msg5 as GIFT_MSG5,
                                                   '' as GIFT_MSG6,
                                                   '' as GIFT_MSG7,
                                                   sum(qty) as QTY,
                                                   GIFT_MSG8
                                              FROM (SELECT TOI.DELIVERY_NO,
                                                           TOI.LINE_ITEM,
                                                           TOI.MPN,
                                                           PPSUSER.T_SHOW_NPI(T9U.DELIVERYNO,T9U.CUSTDELITEM,'Consumer Packing List') as MATE_DESC,
                                                           t9u.msg1,
                                                           t9u.msg2,
                                                           t9u.msg3,
                                                           t9u.msg4,
                                                           t9u.msg5,
                                                           DECODE(t9u.MSG1||t9u.MSG2||t9u.MSG3||t9u.MSG4||t9u.MSG5,'','','Gift Message') AS GIFT_MSG8,
                                                           toi.qty
                                                      FROM PPSUSER.T_ORDER_INFO  TOI,
                                                           PPTEST.VW_MPN_INFO    VMI,
                                                           PPSUSER.T_940_UNICODE T9U
                                                     WHERE TOI.ICTPN =VMI.ICTPARTNO
                                                       AND TOI.LINE_ITEM =T9U.CUSTDELITEM
                                                       AND TOI.DELIVERY_NO =T9U.DELIVERYNO
                                                       AND TOI.DELIVERY_NO ='{0}'
                                                       AND TOI.SHIPMENT_ID = '{1}') T
                                             GROUP BY T.DELIVERY_NO,
                                                      T.LINE_ITEM,
                                                      T.MPN,
                                                      MATE_DESC,
                                                      GIFT_MSG8,
                                                      T.msg1,
                                                      T.msg2,
                                                      T.msg3,
                                                      T.msg4,
                                                      T.msg5 
                                                      order by LINE_ITEM", deliveryNo, shipmentId);
            DataSet action = new DataSet();
            action.Tables.Add(Util.getDataTaleC(headerSql, "AC_DS_EMEA_CSPL_HEADER"));
            action.Tables.Add(Util.getDataTaleC(lineSql, "AC_DS_EMEA_CSPL_LINE"));
            if (print)
            {
                Util.CreateDataTable(Constant.ConsumerPackingList_EMEA_G_URL, action);
            }
            else
            {
                Util.printPDFCrystalReportV2(Constant.ConsumerPackingList_EMEA_G_URL_ByGW, action, strPath);
            }
        }

        private void setDataSoure2(string deliveryNo, string lineItem)
        {
            string headerSql = string.Format(@"select distinct decode(t9u.shipcntycode,
                                                               'FR',
                                                               T9U.SHIPTOCOMPANY,
                                                               'DE',
                                                               T9U.SHIPTOCOMPANY,
                                                               T9U.SHIPTONAME) as ST_NAME,
                                                            decode(t9u.shipcntycode,
                                                               'FR',
                                                               T9U.SHIPTONAME,
                                                               'DE',
                                                               T9U.SHIPTONAME,
                                                               T9U.SHIPTOCOMPANY)      AS  ST_COMPANY,
                                                        t9u.ShipToAddress as ST_ADDR1,
                                                        t9u.Shiptoaddress2 as ST_ADDR2,
                                                        t9u.Shiptoaddress3 as ST_ADDR3,
                                                        DECODE(T9U.SHIPCNTYCODE,'GB',t9u.ShiptoCity,'IE',t9u.ShiptoCity,t9u.ShiptoZip ||','||t9u.ShiptoCity) as ST_CITY,
                                                        DECODE(T9U.SHIPCNTYCODE,'GB',t9u.ShiptoZip,'') as ST_POSTAL,
                                                        DECODE(T9U.SHIPCNTYCODE,'GB',t9u.ShiptoAddress4,'IE',t9u.ShiptoAddress4,'') as ST_DISTRICT,
                                                        t9u.ShipToCountry as ST_COUNTRY,
                                                        '' as SHIP_FROM,
                                                        t9u.SoldToName as SO_NAME,
                                                        T9U.SOLDTOCOMPANY AS SO_COMPANY,
                                                        t9u.DELIVERYNO as AC_DN,
                                                        t9u.WEBORDERNO as WO_NUM,
                                                        TO_CHAR(to_date(t9u.orderdate, 'yyyyMMdd'),
                                                                'dd MON yyyy',
                                                                'NLS_DATE_LANGUAGE=american') as REQ_SHIP_DATE,
                                                        (SELECT TO_CHAR(max(omm.shipdate),'dd MON yyyy','NLS_DATE_LANGUAGE=american') 
                                                 FROM pptest.oms_940_m omm WHERE omm.ac_dn='{0}') as SHIP_DATE,
                                                        decode(substr(t9u.shiproute, 1, 3),
                                                               'PST',
                                                               'Postal',
                                                               'Standard Shipping Method Only') as SHIP_VIA,
                                                        (SELECT SUM(TOI_sum.PCS_QTY)
                                                           FROM PPSUSER.VW_PERSON_DN_INFO TOI_sum
                                                          WHERE toi_sum.DN_NO = '{0}'
                                                          ) TOT_QTY,
                                                       (   select round(sum(t1.totalWeight), 2)
                                                     from (select bb.ICT_PARTNO,
                                                                  bb.DN_NO,
                                                                  vmi.Grossweightkgp  * SUM(bb.PCS_QTY) as totalWeight
                                                             from PPSUSER.VW_PERSON_DN_INFO  bb, pptest.OMS_PARTMAPPING vmi
                                                            where bb.ICT_PARTNO = vmi.PART
                                                              and bb.DN_NO = '{0}'
                                                            group by bb.ICT_PARTNO, bb.DN_NO, vmi.Grossweightkgp) t1) TOT_GW,
                                                        SUBSTR(T9U.euheadtext1, 4) as PKLST_TEXT1,
                                                        SUBSTR(T9U.euheadtext2, 4) as PKLST_TEXT2,
                                                        SUBSTR(T9U.euheadtext3, 4) as PKLST_TEXT3,
                                                        SUBSTR(T9U.euheadtext4, 4) as PKLST_TEXT4,
                                                        SUBSTR(T9U.euheadtext5, 4) as PKLST_TEXT5,
                                                        SUBSTR(T9U.euheadtext6, 4) as PKLST_TEXT6,
                                                        SUBSTR(T9U.euheadtext7, 4) as PKLST_TEXT7,
                                                        SUBSTR(T9U.euheadtext8, 4) as PKLST_TEXT8
                                          from ppsuser.t_940_unicode t9u
                                         where t9u.deliveryno = '{0}'
                                           and t9u.custdelitem = '{1}'", deliveryNo, lineItem);
            string lineSql = string.Format(@"   SELECT T.DELIVERYNO as AC_DN,
                                                   T.CUSTDELITEM as AC_DN_LINE,
                                                   T.MPN as AC_PN,
                                                   MATE_DESC,
                                                   T.msg1 as GIFT_MSG1,
                                                   T.msg2 as GIFT_MSG2,
                                                   T.msg3 as GIFT_MSG3,
                                                   T.msg4 as GIFT_MSG4,
                                                   T.msg5 as GIFT_MSG5,
                                                   '' as GIFT_MSG6,
                                                   '' as GIFT_MSG7,
                                                   sum(PCS_QTY) as QTY,
                                                   GIFT_MSG8
                                              FROM (SELECT t9u.DELIVERYNO,
                                                           t9u.CUSTDELITEM,
                                                           t9u.MPN,
                                                           PPSUSER.T_SHOW_NPI_MES(T9U.DELIVERYNO,T9U.CUSTDELITEM,'Consumer Packing List',T9U.MPN) as MATE_DESC,
                                                           t9u.msg1,
                                                           t9u.msg2,
                                                           t9u.msg3,
                                                           t9u.msg4,
                                                           t9u.msg5,
                                                           DECODE(t9u.MSG1||t9u.MSG2||t9u.MSG3||t9u.MSG4||t9u.MSG5,'','','Gift Message') AS GIFT_MSG8,
                                                           bb.PCS_QTY
                                                      FROM PPSUSER.VW_PERSON_DN_INFO  bb,
                                                           PPTEST.VW_MPN_INFO    VMI,
                                                           PPSUSER.T_940_UNICODE T9U
                                                     WHERE bb.ICT_PARTNO = VMI.ICTPARTNO
                                                          AND bb.DN_LINE = T9U.CUSTDELITEM
                                                          AND bb.DN_NO = T9U.DELIVERYNO
                                                          AND bb.DN_NO = '{0}') T
                                             GROUP BY T.DELIVERYNO,
                                                      T.CUSTDELITEM,
                                                      T.MPN,
                                                      MATE_DESC,
                                                      GIFT_MSG8,
                                                      T.msg1,
                                                      T.msg2,
                                                      T.msg3,
                                                      T.msg4,
                                                      T.msg5 
                                                      order by CUSTDELITEM", deliveryNo);
            DataSet action = new DataSet();
            action.Tables.Add(Util.getDataTaleC(headerSql, "AC_DS_EMEA_CSPL_HEADER"));
            action.Tables.Add(Util.getDataTaleC(lineSql, "AC_DS_EMEA_CSPL_LINE"));
            Util.CreateDataTable(Constant.ConsumerPackingList_EMEA_G_URL, action);
        }

        public ConsumerPackingList_EMEA_G(string deliveryNo, string lineItem, string shipmentId, string strCrystalFullPath, bool isCustom, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            setDataSoure(deliveryNo, lineItem, shipmentId, strCrystalFullPath, isCustom, isPDF, strPDFPath, nCopies, isOnlyGetDS, out dtCrystal, out serverFullLabelName);
        }

        private void setDataSoure(string deliveryNo, string lineItem, string shipmentId, string strCrystalFullPath, bool isCustom, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            dtCrystal = null;
            serverFullLabelName = string.Empty;
            DataSet ds = new DataSet();
            #region headerSql
            string headerSql = string.Format(@"select distinct decode(t9u.shipcntycode,
                                                               'FR',
                                                               T9U.SHIPTOCOMPANY,
                                                               'DE',
                                                               T9U.SHIPTOCOMPANY,
                                                               T9U.SHIPTONAME) as ST_NAME,
                                                            decode(t9u.shipcntycode,
                                                               'FR',
                                                               T9U.SHIPTONAME,
                                                               'DE',
                                                               T9U.SHIPTONAME,
                                                               T9U.SHIPTOCOMPANY)      AS  ST_COMPANY,
                                                        t9u.ShipToAddress as ST_ADDR1,
                                                        t9u.Shiptoaddress2 as ST_ADDR2,
                                                        t9u.Shiptoaddress3 as ST_ADDR3,
                                                        DECODE(T9U.SHIPCNTYCODE,'GB',t9u.ShiptoCity,'IE',t9u.ShiptoCity,t9u.ShiptoZip ||','||t9u.ShiptoCity) as ST_CITY,
                                                        DECODE(T9U.SHIPCNTYCODE,'GB',t9u.ShiptoZip,'') as ST_POSTAL,
                                                        DECODE(T9U.SHIPCNTYCODE,'GB',t9u.ShiptoAddress4,'IE',t9u.ShiptoAddress4,'') as ST_DISTRICT,
                                                        t9u.ShipToCountry as ST_COUNTRY,
                                                        '' as SHIP_FROM,
                                                        t9u.SoldToName as SO_NAME,
                                                        T9U.SOLDTOCOMPANY AS SO_COMPANY,
                                                        t9u.DELIVERYNO as AC_DN,
                                                        t9u.WEBORDERNO as WO_NUM,
                                                        TO_CHAR(to_date(t9u.orderdate, 'yyyyMMdd'),
                                                                'dd MON yyyy',
                                                                'NLS_DATE_LANGUAGE=american') as REQ_SHIP_DATE,
                                                        (select TO_CHAR(tsi.shipping_time,
                                                                        'dd MON yyyy',
                                                                        'NLS_DATE_LANGUAGE=american')
                                                           from ppsuser.t_shipment_info tsi
                                                          where tsi.shipment_id = '{2}') as SHIP_DATE,
                                                        decode(substr(t9u.shiproute, 1, 3),
                                                               'PST',
                                                               'Postal',
                                                               'Standard Shipping Method Only') as SHIP_VIA,
                                                        (select sum(toi.qty)
                                                           from ppsuser.t_order_info toi
                                                          where toi.delivery_no = '{0}'
                                                            and toi.shipment_id = '{2}') as TOT_QTY,
                                                        (select round(sum(t1.totalWeight), 2)
                                                           from (select TOI.ICTPN,
                                                                        TOI.DELIVERY_NO,
                                                                        vmi.GROSSWEIGHTKG * SUM(TOI.CARTON_QTY) as totalWeight
                                                                   from PPSUSER.T_ORDER_INFO TOI,
                                                                        pptest.vw_mpn_info   vmi
                                                                  where TOI.ICTPN = vmi.ICTPARTNO
                                                                    and TOI.DELIVERY_NO = '{0}'
                                                                    and toi.shipment_id = '{2}'
                                                                  group by TOI.ICTPN,
                                                                           TOI.DELIVERY_NO,
                                                                           vmi.GROSSWEIGHTKG) t1) as TOT_GW,
                                                        SUBSTR(T9U.euheadtext1, 4) as PKLST_TEXT1,
                                                        SUBSTR(T9U.euheadtext2, 4) as PKLST_TEXT2,
                                                        SUBSTR(T9U.euheadtext3, 4) as PKLST_TEXT3,
                                                        SUBSTR(T9U.euheadtext4, 4) as PKLST_TEXT4,
                                                        SUBSTR(T9U.euheadtext5, 4) as PKLST_TEXT5,
                                                        SUBSTR(T9U.euheadtext6, 4) as PKLST_TEXT6,
                                                        SUBSTR(T9U.euheadtext7, 4) as PKLST_TEXT7,
                                                        SUBSTR(T9U.euheadtext8, 4) as PKLST_TEXT8
                                          from ppsuser.t_940_unicode t9u
                                         where t9u.deliveryno = '{0}'
                                           and t9u.custdelitem = '{1}'", deliveryNo, lineItem, shipmentId);
            #endregion
            #region lineSql
            string lineSql = string.Format(@"   SELECT T.DELIVERY_NO as AC_DN,
                                                   T.LINE_ITEM as AC_DN_LINE,
                                                   T.MPN as AC_PN,
                                                   MATE_DESC,
                                                   T.msg1 as GIFT_MSG1,
                                                   T.msg2 as GIFT_MSG2,
                                                   T.msg3 as GIFT_MSG3,
                                                   T.msg4 as GIFT_MSG4,
                                                   T.msg5 as GIFT_MSG5,
                                                   '' as GIFT_MSG6,
                                                   '' as GIFT_MSG7,
                                                   sum(qty) as QTY,
                                                   GIFT_MSG8
                                              FROM (SELECT TOI.DELIVERY_NO,
                                                           TOI.LINE_ITEM,
                                                           TOI.MPN,
                                                           PPSUSER.T_SHOW_NPI(T9U.DELIVERYNO,T9U.CUSTDELITEM,'Consumer Packing List') as MATE_DESC,
                                                           t9u.msg1,
                                                           t9u.msg2,
                                                           t9u.msg3,
                                                           t9u.msg4,
                                                           t9u.msg5,
                                                           DECODE(t9u.MSG1||t9u.MSG2||t9u.MSG3||t9u.MSG4||t9u.MSG5,'','','Gift Message') AS GIFT_MSG8,
                                                           toi.qty
                                                      FROM PPSUSER.T_ORDER_INFO  TOI,
                                                           PPTEST.VW_MPN_INFO    VMI,
                                                           PPSUSER.T_940_UNICODE T9U
                                                     WHERE TOI.ICTPN =VMI.ICTPARTNO
                                                       AND TOI.LINE_ITEM =T9U.CUSTDELITEM
                                                       AND TOI.DELIVERY_NO =T9U.DELIVERYNO
                                                       AND TOI.DELIVERY_NO ='{0}'
                                                       AND TOI.SHIPMENT_ID = '{1}') T
                                             GROUP BY T.DELIVERY_NO,
                                                      T.LINE_ITEM,
                                                      T.MPN,
                                                      MATE_DESC,
                                                      GIFT_MSG8,
                                                      T.msg1,
                                                      T.msg2,
                                                      T.msg3,
                                                      T.msg4,
                                                      T.msg5 
                                                      order by LINE_ITEM", deliveryNo, shipmentId);
            #endregion

            ds.Tables.Add(Util.getDataTaleC(headerSql, "AC_DS_EMEA_CSPL_HEADER"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "AC_DS_EMEA_CSPL_LINE"));

            if (string.IsNullOrEmpty(strCrystalFullPath))
            {
                if (!isCustom) //不是关务用默认值
                {
                    strCrystalFullPath = Constant.ConsumerPackingList_EMEA_G_URL;
                }
                else//isCustom如果是关务的是用宋体的模板 
                {
                    strCrystalFullPath = Constant.ConsumerPackingList_EMEA_G_URL_ByGW;
                }
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
