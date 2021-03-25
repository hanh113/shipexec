using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class P1ConsumerPackingListTW 
    {
        /**
         * Summary:Crystal Report function 
         * ParameterList:①.acDn:query table index  ②.print：judge print Crystal Report or export Crsytal Report for PDF
         *                 */
        public P1ConsumerPackingListTW(string deliveryNo, string lineItem, string shipmentId, bool print)
        {
            Initialize(deliveryNo, lineItem, shipmentId, print, "");
        }

        public P1ConsumerPackingListTW(string deliveryNo, string lineItem, string shipmentId, bool print, string strPath)
        {
            Initialize(deliveryNo, lineItem, shipmentId, print, strPath);
        }

        public void Initialize(string deliveryNo, string lineItem, string shipmentId, bool print, string strPath)
        {
            DataSet ds = new DataSet();
            string headerSql = string.Format(@"select  t9u.ShiptoName as ST_NAME,
                                                       t9u.ShipToAddress as ST_ADDR1,
                                                       t9u.SHIPTOADDRESS2 as ST_ADDR2,
                                                       decode(instr(t9u.ShipToAddress4, '=', 1, 1),
                                                              0,
                                                              t9u.ShipToAddress4,
                                                              substr(t9u.ShipToAddress4,
                                                                     instr(t9u.ShipToAddress4, '=', 1, 1) + 1)) as ST_ADDR3,
                                                       t9u.ShiptoCity as ST_CITY,
                                                       t9u.ShipToZip as ST_POSTAL,
                                                       t9u.ShipToState as ST_PROVINCE,
                                                       t9u.ShipToCountry as ST_COUNTRY,
                                                       '' as SHIP_FROM,
                                                       t9u.SoldToName as SO_NAME,
                                                       t9u.DELIVERYNO as AC_DN,
                                                       t9u.WEBORDERNO as WO_NUM," +
                                                        "TO_CHAR(TO_DATE(t9u.ORDERDATE, 'yyyyMMdd'), 'YYYY\"年\" MM\"月\"dd\"日\"') as AC_PO_DATE, " +
                                                        " (select TO_CHAR(tsi.shipping_time,'YYYY\"年\" MM\"月\"dd\"日\"')from ppsuser.t_shipment_info tsi where tsi.shipment_id = '{2}') as SHIP_DATE,"
                                                       + @"'' as SHIP_VIA,
                                                       (select sum(toi.qty)
                                                          from ppsuser.t_order_info toi
                                                         where toi.delivery_no = '{0}'
                                                         and   toi.shipment_id = '{2}') as TOT_QTY,
                                                       (select round(sum(t1.totalWeight), 2)
                                                          from (select TOI.ICTPN,
                                                                       TOI.DELIVERY_NO,
                                                                       vmi.GROSSWEIGHTKG * SUM(TOI.CARTON_QTY) as totalWeight
                                                                  from PPSUSER.T_ORDER_INFO TOI, PPTEST.vw_mpn_info vmi
                                                                 where TOI.ICTPN = vmi.ICTPARTNO
                                                                   and TOI.DELIVERY_NO = '{0}'
                                                                   and toi.shipment_id = '{2}'
                                                                 group by TOI.ICTPN, TOI.DELIVERY_NO, vmi.GROSSWEIGHTKG) t1) as TOT_GW,
                                                       SUBSTR(T9U.euheadtext1,4)  as PKLST_TEXT1,
                                                SUBSTR(T9U.euheadtext2,4)  as PKLST_TEXT2,
                                                SUBSTR(T9U.euheadtext3,4)  as PKLST_TEXT3,
                                                SUBSTR(T9U.euheadtext4,4)  as PKLST_TEXT4,
                                                SUBSTR(T9U.euheadtext5,4)  as PKLST_TEXT5,
                                                SUBSTR(T9U.euheadtext6,4)  as PKLST_TEXT6,
                                                SUBSTR(T9U.euheadtext7,4)  as PKLST_TEXT7,
                                                SUBSTR(T9U.euheadtext8,4)  as PKLST_TEXT8
                                                  from ppsuser.t_940_unicode t9u
                                                 where t9u.deliveryno = '{0}'
                                                   and t9u.custdelitem = '{1}'", deliveryNo, lineItem, shipmentId);
            string lineSql = string.Format(@"SELECT T.DELIVERY_NO as AC_DN,
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
                                                            DECODE(t9u.MSG1 || t9u.MSG2 || t9u.MSG3 || t9u.MSG4 ||
                                                                t9u.MSG5,
                                                                '',
                                                                '',
                                                                '送禮祝賀詞') AS GIFT_MSG8,
                                                            toi.qty
                                                    FROM PPSUSER.T_ORDER_INFO  TOI,
                                                            PPTEST.VW_MPN_INFO    VMI,
                                                            PPSUSER.T_940_UNICODE T9U
                                                    WHERE TOI.ICTPN = VMI.ICTPARTNO
                                                        AND TOI.LINE_ITEM = T9U.CUSTDELITEM
                                                        AND TOI.DELIVERY_NO = T9U.DELIVERYNO
                                                        AND TOI.DELIVERY_NO = '{0}'
                                                        and toi.shipment_id = '{1}') T
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
            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            if (print)
            {
                Util.CreateDataTable(Constant.P1ConsumerPackingListTW_URL, ds);
            }
            else
            {
                Util.printPDFCrystalReportV2(Constant.P1ConsumerPackingListTW_URL_ByGW, ds, strPath);
            }


        }

        public P1ConsumerPackingListTW(string deliveryNo, string lineItem, string shipmentId, string strCrystalFullPath, bool isCustom, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            Initialize(deliveryNo, lineItem, shipmentId, strCrystalFullPath, isCustom, isPDF, strPDFPath, nCopies, isOnlyGetDS, out dtCrystal, out serverFullLabelName);
        }

        public void Initialize(string deliveryNo, string lineItem, string shipmentId, string strCrystalFullPath, bool isCustom, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            dtCrystal = null;
            serverFullLabelName = string.Empty;
            DataSet ds = new DataSet();
            #region headerSql
            string headerSql = string.Format(@"select  t9u.ShiptoName as ST_NAME,
                                                       t9u.ShipToAddress as ST_ADDR1,
                                                       t9u.SHIPTOADDRESS2 as ST_ADDR2,
                                                       decode(instr(t9u.ShipToAddress4, '=', 1, 1),
                                                              0,
                                                              t9u.ShipToAddress4,
                                                              substr(t9u.ShipToAddress4,
                                                                     instr(t9u.ShipToAddress4, '=', 1, 1) + 1)) as ST_ADDR3,
                                                       t9u.ShiptoCity as ST_CITY,
                                                       t9u.ShipToZip as ST_POSTAL,
                                                       t9u.ShipToState as ST_PROVINCE,
                                                       t9u.ShipToCountry as ST_COUNTRY,
                                                       '' as SHIP_FROM,
                                                       t9u.SoldToName as SO_NAME,
                                                       t9u.DELIVERYNO as AC_DN,
                                                       t9u.WEBORDERNO as WO_NUM," +
                                                        "TO_CHAR(TO_DATE(t9u.ORDERDATE, 'yyyyMMdd'), 'YYYY\"年\" MM\"月\"dd\"日\"') as AC_PO_DATE, " +
                                                        " (select TO_CHAR(tsi.shipping_time,'YYYY\"年\" MM\"月\"dd\"日\"')from ppsuser.t_shipment_info tsi where tsi.shipment_id = '{2}') as SHIP_DATE,"
                                                       + @"'' as SHIP_VIA,
                                                       (select sum(toi.qty)
                                                          from ppsuser.t_order_info toi
                                                         where toi.delivery_no = '{0}'
                                                         and   toi.shipment_id = '{2}') as TOT_QTY,
                                                       (select round(sum(t1.totalWeight), 2)
                                                          from (select TOI.ICTPN,
                                                                       TOI.DELIVERY_NO,
                                                                       vmi.GROSSWEIGHTKG * SUM(TOI.CARTON_QTY) as totalWeight
                                                                  from PPSUSER.T_ORDER_INFO TOI, PPTEST.vw_mpn_info vmi
                                                                 where TOI.ICTPN = vmi.ICTPARTNO
                                                                   and TOI.DELIVERY_NO = '{0}'
                                                                   and toi.shipment_id = '{2}'
                                                                 group by TOI.ICTPN, TOI.DELIVERY_NO, vmi.GROSSWEIGHTKG) t1) as TOT_GW,
                                                       SUBSTR(T9U.euheadtext1,4)  as PKLST_TEXT1,
                                                SUBSTR(T9U.euheadtext2,4)  as PKLST_TEXT2,
                                                SUBSTR(T9U.euheadtext3,4)  as PKLST_TEXT3,
                                                SUBSTR(T9U.euheadtext4,4)  as PKLST_TEXT4,
                                                SUBSTR(T9U.euheadtext5,4)  as PKLST_TEXT5,
                                                SUBSTR(T9U.euheadtext6,4)  as PKLST_TEXT6,
                                                SUBSTR(T9U.euheadtext7,4)  as PKLST_TEXT7,
                                                SUBSTR(T9U.euheadtext8,4)  as PKLST_TEXT8
                                                  from ppsuser.t_940_unicode t9u
                                                 where t9u.deliveryno = '{0}'
                                                   and t9u.custdelitem = '{1}'", deliveryNo, lineItem, shipmentId);
            #endregion
            #region lineSql
            string lineSql = string.Format(@"SELECT T.DELIVERY_NO as AC_DN,
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
                                                            DECODE(t9u.MSG1 || t9u.MSG2 || t9u.MSG3 || t9u.MSG4 ||
                                                                t9u.MSG5,
                                                                '',
                                                                '',
                                                                '送禮祝賀詞') AS GIFT_MSG8,
                                                            toi.qty
                                                    FROM PPSUSER.T_ORDER_INFO  TOI,
                                                            PPTEST.VW_MPN_INFO    VMI,
                                                            PPSUSER.T_940_UNICODE T9U
                                                    WHERE TOI.ICTPN = VMI.ICTPARTNO
                                                        AND TOI.LINE_ITEM = T9U.CUSTDELITEM
                                                        AND TOI.DELIVERY_NO = T9U.DELIVERYNO
                                                        AND TOI.DELIVERY_NO = '{0}'
                                                        and toi.shipment_id = '{1}') T
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
            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));

            if (string.IsNullOrEmpty(strCrystalFullPath))
            {
                if (!isCustom) //不是关务用默认值
                {
                    strCrystalFullPath = Constant.P1ConsumerPackingListTW_URL;
                }
                else//isCustom如果是关务的是用宋体的模板 
                {
                    strCrystalFullPath = Constant.P1ConsumerPackingListTW_URL_ByGW;
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
        public P1ConsumerPackingListTW(string deliveryNo, string lineItem)
        {
            Initialize2(deliveryNo, lineItem);
        }

        public void Initialize2(string deliveryNo, string lineItem)
        {
            DataSet ds = new DataSet();
            string headerSql = string.Format(@"select  t9u.ShiptoName as ST_NAME,
                                                       t9u.ShipToAddress as ST_ADDR1,
                                                       t9u.SHIPTOADDRESS2 as ST_ADDR2,
                                                       decode(instr(t9u.ShipToAddress4, '=', 1, 1),
                                                              0,
                                                              t9u.ShipToAddress4,
                                                              substr(t9u.ShipToAddress4,
                                                                     instr(t9u.ShipToAddress4, '=', 1, 1) + 1)) as ST_ADDR3,
                                                       t9u.ShiptoCity as ST_CITY,
                                                       t9u.ShipToZip as ST_POSTAL,
                                                       t9u.ShipToState as ST_PROVINCE,
                                                       t9u.ShipToCountry as ST_COUNTRY,
                                                       '' as SHIP_FROM,
                                                       t9u.SoldToName as SO_NAME,
                                                       t9u.DELIVERYNO as AC_DN,
                                                       t9u.WEBORDERNO as WO_NUM," +
                                                        "TO_CHAR(TO_DATE(t9u.ORDERDATE, 'yyyyMMdd'), 'YYYY\"年\" MM\"月\"dd\"日\"') as AC_PO_DATE, " +
                                                        " (SELECT TO_CHAR(max(omm.shipdate),'YYYY\"年\" MM\"月\"dd\"日\"') FROM pptest.oms_940_m omm WHERE omm.ac_dn='{0}') as SHIP_DATE,"
                                                       + @"'' as SHIP_VIA,
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
                                                       SUBSTR(T9U.euheadtext1,4)  as PKLST_TEXT1,
                                                SUBSTR(T9U.euheadtext2,4)  as PKLST_TEXT2,
                                                SUBSTR(T9U.euheadtext3,4)  as PKLST_TEXT3,
                                                SUBSTR(T9U.euheadtext4,4)  as PKLST_TEXT4,
                                                SUBSTR(T9U.euheadtext5,4)  as PKLST_TEXT5,
                                                SUBSTR(T9U.euheadtext6,4)  as PKLST_TEXT6,
                                                SUBSTR(T9U.euheadtext7,4)  as PKLST_TEXT7,
                                                SUBSTR(T9U.euheadtext8,4)  as PKLST_TEXT8
                                                  from ppsuser.t_940_unicode t9u
                                                 where t9u.deliveryno = '{0}'
                                                   and t9u.custdelitem = '{1}'", deliveryNo, lineItem);
            string lineSql = string.Format(@"SELECT T.DELIVERYNO as AC_DN,
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
                                                    FROM (SELECT T9U.DELIVERYNO,
                                                            T9U.CUSTDELITEM,
                                                            T9U.MPN,
                                                            PPSUSER.T_SHOW_NPI_MES(T9U.DELIVERYNO,T9U.CUSTDELITEM,'Consumer Packing List',T9U.MPN) as MATE_DESC,
                                                            t9u.msg1,
                                                            t9u.msg2,
                                                            t9u.msg3,
                                                            t9u.msg4,
                                                            t9u.msg5,
                                                            DECODE(t9u.MSG1 || t9u.MSG2 || t9u.MSG3 || t9u.MSG4 ||
                                                                t9u.MSG5,
                                                                '',
                                                                '',
                                                                '送禮祝賀詞') AS GIFT_MSG8,
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
                                                    order by CUSTDELITEM ", deliveryNo);
            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            Util.CreateDataTable(Constant.P1ConsumerPackingListTW_URL, ds);
        }
    }
}
