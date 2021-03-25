using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class DeliveryNoteHForm
    {
        public DeliveryNoteHForm(string deliveryNo, string palletNo)
        {
            setDataSoure(deliveryNo, palletNo, true, "");
        }

        public DeliveryNoteHForm(string deliveryNo, string palletNo, bool print, string strPath)
        {
            setDataSoure(deliveryNo, palletNo, print, strPath);
        }

        private void setDataSoure(String deliveryNo, string palletNo, bool print, string strPath)
        {
            DataSet action = new DataSet();
            string HeadLineSql = string.Format(@"SELECT distinct (SELECT tesh.shipperaddress1
                                                       FROM PPSUSER.T_EMEIA_SHIPPER TESH
                                                      WHERE TESH.SHIPCNTYCODE =
                                                            DECODE(T9U.SHIPCNTYCODE,
                                                                   'SA',
                                                                   T9U.SHIPCNTYCODE,
                                                                   'IN',
                                                                   T9U.SHIPCNTYCODE,
                                                                   'Default')) as Shipper1,
                                                    (SELECT tesh.shipperaddress2
                                                       FROM PPSUSER.T_EMEIA_SHIPPER TESH
                                                      WHERE TESH.SHIPCNTYCODE =
                                                            DECODE(T9U.SHIPCNTYCODE,
                                                                   'SA',
                                                                   T9U.SHIPCNTYCODE,
                                                                   'IN',
                                                                   T9U.SHIPCNTYCODE,
                                                                   'Default')) as Shipper2,
                                                    (SELECT tesh.shipperaddress3
                                                       FROM PPSUSER.T_EMEIA_SHIPPER TESH
                                                      WHERE TESH.SHIPCNTYCODE =
                                                            DECODE(T9U.SHIPCNTYCODE,
                                                                   'SA',
                                                                   T9U.SHIPCNTYCODE,
                                                                   'IN',
                                                                   T9U.SHIPCNTYCODE,
                                                                   'Default')) as Shipper3,
                                                    (SELECT tesh.shipperaddress4
                                                       FROM PPSUSER.T_EMEIA_SHIPPER TESH
                                                      WHERE TESH.SHIPCNTYCODE =
                                                            DECODE(T9U.SHIPCNTYCODE,
                                                                   'SA',
                                                                   T9U.SHIPCNTYCODE,
                                                                   'IN',
                                                                   T9U.SHIPCNTYCODE,
                                                                   'Default')) as Shipper4,
                                                    (SELECT tesh.shipperaddress5
                                                       FROM PPSUSER.T_EMEIA_SHIPPER TESH
                                                      WHERE TESH.SHIPCNTYCODE =
                                                            DECODE(T9U.SHIPCNTYCODE,
                                                                   'SA',
                                                                   T9U.SHIPCNTYCODE,
                                                                   'IN',
                                                                   T9U.SHIPCNTYCODE,
                                                                   'Default')) as Shipper5,
                                                    t9u.deliveryno,
                                                    t9u.soldtoname,
                                                    t9u.soldtocompany,
                                                    t9u.soldtoaddress,
                                                    t9u.soldtoaddress2,
                                                    t9u.soldtoaddress3,
                                                    t9u.soldtocity,
                                                    t9u.soldtozip,
                                                    --t9u.soldtoaddress4,
                                                    decode(instr(t9u.SOLDTOREGIONDESC,'='),0,t9u.SOLDTOREGIONDESC,substr(t9u.SOLDTOREGIONDESC,instr(t9u.SOLDTOREGIONDESC,'=')+1)) as soldtoaddress4,
                                                    t9u.soldcntycode,
                                                    tsi.hawb,
                                                    to_char(tsi.shipping_time,'dd/Mon/yy','NLS_DATE_LANGUAGE=AMERICAN') AS SHIPDATE,
                                                    decode(substr(t9u.shiproute, 1, 3),
                                                           'PST',
                                                           'Postal',
                                                           'Standard Shipping Method') as ShipVia,
                                                    tsi.transport as PerMode,
                                                    '' as FreightTerm,
                                                    (SELECT SUM(TOI.QTY)
                                                       FROM PPSUSER.T_ORDER_INFO TOI
                                                      WHERE TOI.DELIVERY_NO = T9U.DELIVERYNO) AS TotalQty,
                                                    case
                                                      when (SELECT COUNT(DISTINCT T9U1.CUSTSONO)
                                                              FROM PPSUSER.T_940_UNICODE T9U1
                                                             WHERE T9U1.DELIVERYNO = T9U.DELIVERYNO) > 1 then
                                                       ''
                                                      else
                                                       (SELECT DISTINCT T9U1.CUSTSONO as SoldOrderNo
                                                          FROM PPSUSER.T_940_UNICODE T9U1
                                                         WHERE T9U1.DELIVERYNO = T9U.DELIVERYNO)
                                                    end AS SOLDORDERNO,
                
                                                    case
                                                      when (SELECT COUNT(DISTINCT T9U1.ITEMCUSTPO)
                                                              FROM PPSUSER.T_940_UNICODE T9U1
                                                             WHERE T9U1.DELIVERYNO = T9U.DELIVERYNO) > 1 then
                                                       ''
                                                      else
                                                       (SELECT DISTINCT CAST(T9U1.ITEMCUSTPO AS VARCHAR2(50)) as PurOrderNo
                                                          FROM PPSUSER.T_940_UNICODE T9U1
                                                         WHERE T9U1.DELIVERYNO = T9U.DELIVERYNO)
                                                    end AS PURORDERNO,
                                                    t9u.weborderno,
                                                    (SELECT SUM(TOI2.CARTON_QTY)
                                                       FROM PPSUSER.T_ORDER_INFO TOI2
                                                      WHERE TOI2.DELIVERY_NO = T9U.DELIVERYNO) AS TOTALCARTONQTY,
                                                    (select round(sum(t1.totalWeight), 2)
                                                       from (select TOI.ICTPN,
                                                                    TOI.DELIVERY_NO,
                                                                    vmi.FGWEIGHTKG * SUM(TOI.QTY) as totalWeight
                                                               from PPSUSER.T_ORDER_INFO TOI,
                                                                    PPTEST.vw_mpn_info   vmi
                                                              where TOI.ICTPN = vmi.ICTPARTNO
                                                                and TOI.DELIVERY_NO = '{0}'
                                                              group by TOI.ICTPN, TOI.DELIVERY_NO, vmi.FGWEIGHTKG) t1) AS TOTALNETWEIGHT,
                                                    (select round(sum(t1.totalWeight), 2)
                                                       from (select TOI.ICTPN,
                                                                    TOI.DELIVERY_NO,
                                                                    vmi.GROSSWEIGHTKG * SUM(TOI.CARTON_QTY) as totalWeight
                                                               from PPSUSER.T_ORDER_INFO TOI,
                                                                    PPTEST.vw_mpn_info   vmi
                                                              where TOI.ICTPN = vmi.ICTPARTNO
                                                                and TOI.DELIVERY_NO = '{0}'
                                                              group by TOI.ICTPN,
                                                                       TOI.DELIVERY_NO,
                                                                       vmi.GROSSWEIGHTKG) t1) AS TOTALGROSSWT,
                                                    (SELECT COUNT(DISTINCT TPO.PALLET_NO)
                                                       FROM PPSUSER.T_PALLET_ORDER TPO
                                                      WHERE TPO.DELIVERY_NO = T9U.DELIVERYNO) as totalpalletqty,
                                                    t9u.shiptoname,
                                                    t9u.shiptocompany,
                                                    t9u.shiptoaddress,
                                                    t9u.shiptoaddress2,
                                                    t9u.shiptoaddress3,
                                                    t9u.shiptocity,
                                                    t9u.shiptozip,
                                                    --t9u.shiptoaddress4,
                                                    decode(instr(t9u.regiondesc,'='),0,t9u.regiondesc,substr(t9u.regiondesc,instr(t9u.regiondesc,'=')+1)) as shiptoaddress4,
                                                    t9u.shipcntycode,
                                                    decode((SELECT DISTINCT TSP.PALLET_TYPE
                                                             FROM PPSUSER.T_SHIPMENT_PALLET TSP
                                                            WHERE TSP.PALLET_NO = '{1}'),
                                                           '999',
                                                           '',
                                                           '001',
                                                           (SELECT DISTINCT TSP.SSCC
                                                              FROM PPSUSER.T_SHIPMENT_PALLET TSP
                                                             WHERE TSP.PALLET_NO = '{1}')) as PalletSscc,
                                                    ppsuser.t_calculate_emeia_dent(tsi.shipment_id,
                                                                                   t9u.deliveryno,
                                                                                   '{1}') as palletCount,
                
                                                    T9U.NTECCE || T9U.CUSTSHIPINST || T9U.NTEWHI ||
                                                    T9U.EXCUSTNOTE || T9U.NTEGSI || T9U.NEBACUSTTEXT1 ||
                                                    T9U.NEBACUSTTEXT2 || T9U.NEBACUSTTEXT3 || T9U.NEBACUSTTEXT4 ||
                                                    T9U.NEBACUSTTEXT5 || T9U.Nebacusttext6 || T9U.NTEPCS ||
                                                    T9U.NTETRA || T9U.NTEOTH AS HeaderText
                                      FROM PPSUSER.T_940_UNICODE   T9U,
                                           ppsuser.t_order_info    toi,
                                           ppsuser.t_shipment_info tsi
                                     WHERE t9u.deliveryno = toi.delivery_no
                                       and toi.shipment_id = tsi.shipment_id
                                       and T9U.DELIVERYNO = '{0}'
                                    ", deliveryNo, palletNo);
            //       string MixCooFlag = ClientUtils.ExecuteSQL(string.Format(@" select coo  from ppsuser.t_shipment_pallet
            //where pallet_no = '{0}' ", palletNo)).Tables[0].Rows[0][0].ToString();
            //       //修改COO取值逻辑 edit by franky 2020/3/13
            //       string lineSql = MixCooFlag.Equals("MIX") ?
            //                                          string.Format(@"SELECT LINEITEM,
            //                                                              ppsuser.f_transform_coo(T.COO, 1) AS COUNTRYCODE,
            //                                                              UPC,
            //                                                              MPN,
            //                                                              PRODESC,
            //                                                              SUM(COO_QTY) AS PERLINEITEMQTY,
            //                                                              FGWEIGHTKG AS PERLINEITEMNETWT,
            //                                                              GROSSWEIGHTKG AS PERLINEITEMGROSSWT,
            //                                                              ItemText
            //                                                         FROM (SELECT TOC.COO,
            //                                                                      TOI.LINE_ITEM AS LINEITEM,
            //                                                                      TOI.MPN,
            //                                                                      decode(VMI.JAN_CODE, '', VMI.UPC_CODE, VMI.JAN_CODE) UPC,
            //                                                                      PPSUSER.T_SHOW_NPI(T9U.DELIVERYNO,
            //                                                                                         T9U.CUSTDELITEM,
            //                                                                                         'Channel Delivery Note') as PRODESC,
            //                                                                      TOC.COO_QTY,
            //                                                                      VMI.FGWEIGHTKG,
            //                                                                      VMI.GROSSWEIGHTKG,
            //                                                                      T9U.shipinstruct || T9U.exitemnote || T9U.euitemtext1 ||
            //                                                                      T9U.euitemtext2 || T9U.euitemtext3 || T9U.euitemtext4 ||
            //                                                                      T9U.euitemtext5 || T9U.euitemtext6 || T9U.euitemtext7 ||
            //                                                                      T9U.euitemtext8 || T9U.euitemtext9 || T9U.euitemtext10 as ItemText
            //                                                                 FROM PPSUSER.T_ORDER_INFO   TOI,
            //                                                                      PPTEST.VW_MPN_INFO     VMI,
            //                                                                      PPSUSER.T_940_UNICODE  T9U,
            //                                                                      PPSUSER.T_ORDER_COO TOC
            //                                                                WHERE TOI.ICTPN = VMI.ICTPARTNO
            //                                                                  AND TOI.LINE_ITEM = T9U.CUSTDELITEM
            //                                                                  AND TOI.DELIVERY_NO = T9U.DELIVERYNO
            //                                                                  AND TOI.DELIVERY_NO = TOC.DN_NO
            //                                                                  AND TOI.LINE_ITEM = TOC.DN_LINE
            //                                                                  AND TOI.ICTPN = TOC.PART_NO
            //                                                              AND TOI.DELIVERY_NO = '{0}' ) T
            //                                                        GROUP BY LINEITEM,
            //                                                                 MPN,
            //                                                                 UPC,
            //                                                                 PRODESC,
            //                                                                 FGWEIGHTKG,
            //                                                                 COO,
            //                                                                 GROSSWEIGHTKG,
            //                                                                 ItemText
            //                                                        ORDER BY LINEITEM ASC", deliveryNo) :
            string lineSql = string.Format(@" SELECT LINEITEM,
                                                                   ppsuser.f_transform_coo(T.COO, 1) AS COUNTRYCODE,
                                                                   UPC,
                                                                   MPN,
                                                                   PRODESC,
                                                                   SUM(QTY) AS PERLINEITEMQTY,
                                                                   FGWEIGHTKG AS PERLINEITEMNETWT,
                                                                   GROSSWEIGHTKG AS PERLINEITEMGROSSWT,
                                                                   ItemText
                                                              FROM (SELECT TPO.COO,
                                                                           TOI.LINE_ITEM AS LINEITEM,
                                                                           TOI.MPN,
                                                                           decode(VMI.JAN_CODE, '', VMI.UPC_CODE, VMI.JAN_CODE) UPC,
                                                                           PPSUSER.T_SHOW_NPI(T9U.DELIVERYNO,
                                                                                              T9U.CUSTDELITEM,
                                                                                              'Channel Delivery Note') as PRODESC,
                                                                           TPO.ASSIGN_QTY AS QTY,
                                                                           VMI.FGWEIGHTKG,
                                                                           VMI.GROSSWEIGHTKG,
                                                                           T9U.shipinstruct || T9U.exitemnote || T9U.euitemtext1 ||
                                                                           T9U.euitemtext2 || T9U.euitemtext3 || T9U.euitemtext4 ||
                                                                           T9U.euitemtext5 || T9U.euitemtext6 || T9U.euitemtext7 ||
                                                                           T9U.euitemtext8 || T9U.euitemtext9 || T9U.euitemtext10 as ItemText
                                                                      FROM PPSUSER.T_ORDER_INFO   TOI,
                                                                           PPTEST.VW_MPN_INFO     VMI,
                                                                           PPSUSER.T_940_UNICODE  T9U,
                                                                           PPSUSER.T_PALLET_ORDER TPO
                                                                     WHERE TOI.ICTPN = VMI.ICTPARTNO
                                                                       AND TOI.LINE_ITEM = T9U.CUSTDELITEM
                                                                       AND TOI.DELIVERY_NO = T9U.DELIVERYNO
                                                                       AND TOI.DELIVERY_NO = TPO.DELIVERY_NO
                                                                       AND TOI.LINE_ITEM = TPO.LINE_ITEM
                                                                       AND TOI.ICTPN = TPO.ICTPN
                                                                       AND TOI.DELIVERY_NO = '{0}') T
                                                             GROUP BY LINEITEM,
                                                                      MPN,
                                                                      UPC,
                                                                      PRODESC,
                                                                      FGWEIGHTKG,
                                                                      COO,
                                                                      GROSSWEIGHTKG,
                                                                      ItemText
                                                             ORDER BY LINEITEM ASC
                                                            ", deliveryNo);
            //string MlineSql = string.Format(@"SELECT LINEITEM,
            //                             --  'China' AS COUNTRYCODE,
            //                                ppsuser.getcoobykp(T.ICTPN, 1) AS COUNTRYCODE,
            //                               UPC,
            //                               MPN,
            //                               PRODESC,
            //                               SUM(QTY) AS PERLINEITEMQTY,
            //                               FGWEIGHTKG AS PERLINEITEMNETWT,
            //                               GROSSWEIGHTKG AS PERLINEITEMGROSSWT,
            //                               ItemText
            //                          FROM (SELECT TOI.ICTPN,
            //                                    TOI.LINE_ITEM AS LINEITEM,
            //                                       TOI.MPN,
            //                                       decode(VMI.JAN_CODE, '', VMI.UPC_CODE, VMI.JAN_CODE) UPC,
            //                                       PPSUSER.T_SHOW_NPI(T9U.DELIVERYNO,T9U.CUSTDELITEM,'Channel Delivery Note') as PRODESC,
            //                                       toi.qty,
            //                                       VMI.FGWEIGHTKG,
            //                                       VMI.GROSSWEIGHTKG,
            //                                       T9U.shipinstruct || T9U.exitemnote || T9U.euitemtext1 ||
            //                                       T9U.euitemtext2 || T9U.euitemtext3 || T9U.euitemtext4 ||
            //                                       T9U.euitemtext5 || T9U.euitemtext6 || T9U.euitemtext7 ||
            //                                       T9U.euitemtext8 || T9U.euitemtext9 || T9U.euitemtext10 as ItemText
            //                                  FROM PPSUSER.T_ORDER_INFO  TOI,
            //                                       PPTEST.VW_MPN_INFO    VMI,
            //                                       PPSUSER.T_940_UNICODE T9U
            //                                 WHERE TOI.ICTPN = VMI.ICTPARTNO
            //                                   AND TOI.LINE_ITEM = T9U.CUSTDELITEM
            //                                   AND TOI.DELIVERY_NO = T9U.DELIVERYNO
            //                                   AND TOI.DELIVERY_NO = '{0}') T
            //                         GROUP BY LINEITEM, MPN, UPC, PRODESC, FGWEIGHTKG, ICTPN,GROSSWEIGHTKG, ItemText ORDER BY LINEITEM ASC", deliveryNo);
            action.Tables.Add(Util.getDataTaleC(HeadLineSql, "HeadLine"));
            action.Tables.Add(Util.getDataTaleC(lineSql, "M_line"));
            if (print)
            {
                Util.createRpt(Constant.EMEIA_SIGNLE_DeliveryNote_URL, action);
            }
            else
            {
                Util.printPDFCrystalReportV2(Constant.EMEIA_SIGNLE_DeliveryNote_URL_ByGW, action, strPath);
            }
        }

        public DeliveryNoteHForm(string deliveryNo, string palletNo, string strCrystalFullPath, bool isCustom, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            Initialize2(deliveryNo, palletNo, strCrystalFullPath, isCustom, isPDF, strPDFPath, nCopies, isOnlyGetDS, out dtCrystal, out serverFullLabelName);
        }
        private void Initialize2(string deliveryNo, string palletNo, string strCrystalFullPath, bool isCustom, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            dtCrystal = null;
            serverFullLabelName = string.Empty;
            //其他情况都是默认传进来的路径+***.rpt

            DataSet ds = new DataSet();
            #region HeadLineSql
            string HeadLineSql = string.Format(@"SELECT distinct (SELECT tesh.shipperaddress1
                                                       FROM PPSUSER.T_EMEIA_SHIPPER TESH
                                                      WHERE TESH.SHIPCNTYCODE =
                                                            DECODE(T9U.SHIPCNTYCODE,
                                                                   'SA',
                                                                   T9U.SHIPCNTYCODE,
                                                                   'IN',
                                                                   T9U.SHIPCNTYCODE,
                                                                   'Default')) as Shipper1,
                                                    (SELECT tesh.shipperaddress2
                                                       FROM PPSUSER.T_EMEIA_SHIPPER TESH
                                                      WHERE TESH.SHIPCNTYCODE =
                                                            DECODE(T9U.SHIPCNTYCODE,
                                                                   'SA',
                                                                   T9U.SHIPCNTYCODE,
                                                                   'IN',
                                                                   T9U.SHIPCNTYCODE,
                                                                   'Default')) as Shipper2,
                                                    (SELECT tesh.shipperaddress3
                                                       FROM PPSUSER.T_EMEIA_SHIPPER TESH
                                                      WHERE TESH.SHIPCNTYCODE =
                                                            DECODE(T9U.SHIPCNTYCODE,
                                                                   'SA',
                                                                   T9U.SHIPCNTYCODE,
                                                                   'IN',
                                                                   T9U.SHIPCNTYCODE,
                                                                   'Default')) as Shipper3,
                                                    (SELECT tesh.shipperaddress4
                                                       FROM PPSUSER.T_EMEIA_SHIPPER TESH
                                                      WHERE TESH.SHIPCNTYCODE =
                                                            DECODE(T9U.SHIPCNTYCODE,
                                                                   'SA',
                                                                   T9U.SHIPCNTYCODE,
                                                                   'IN',
                                                                   T9U.SHIPCNTYCODE,
                                                                   'Default')) as Shipper4,
                                                    (SELECT tesh.shipperaddress5
                                                       FROM PPSUSER.T_EMEIA_SHIPPER TESH
                                                      WHERE TESH.SHIPCNTYCODE =
                                                            DECODE(T9U.SHIPCNTYCODE,
                                                                   'SA',
                                                                   T9U.SHIPCNTYCODE,
                                                                   'IN',
                                                                   T9U.SHIPCNTYCODE,
                                                                   'Default')) as Shipper5,
                                                    t9u.deliveryno,
                                                    t9u.soldtoname,
                                                    t9u.soldtocompany,
                                                    t9u.soldtoaddress,
                                                    t9u.soldtoaddress2,
                                                    t9u.soldtoaddress3,
                                                    t9u.soldtocity,
                                                    t9u.soldtozip,
                                                    --t9u.soldtoaddress4,
                                                    decode(instr(t9u.SOLDTOREGIONDESC,'='),0,t9u.SOLDTOREGIONDESC,substr(t9u.SOLDTOREGIONDESC,instr(t9u.SOLDTOREGIONDESC,'=')+1)) as soldtoaddress4,
                                                    t9u.soldcntycode,
                                                    tsi.hawb,
                                                    to_char(tsi.shipping_time,'dd/Mon/yy','NLS_DATE_LANGUAGE=AMERICAN') AS SHIPDATE,
                                                    decode(substr(t9u.shiproute, 1, 3),
                                                           'PST',
                                                           'Postal',
                                                           'Standard Shipping Method') as ShipVia,
                                                    tsi.transport as PerMode,
                                                    '' as FreightTerm,
                                                    (SELECT SUM(TOI.QTY)
                                                       FROM PPSUSER.T_ORDER_INFO TOI
                                                      WHERE TOI.DELIVERY_NO = T9U.DELIVERYNO) AS TotalQty,
                                                    case
                                                      when (SELECT COUNT(DISTINCT T9U1.CUSTSONO)
                                                              FROM PPSUSER.T_940_UNICODE T9U1
                                                             WHERE T9U1.DELIVERYNO = T9U.DELIVERYNO) > 1 then
                                                       ''
                                                      else
                                                       (SELECT DISTINCT T9U1.CUSTSONO as SoldOrderNo
                                                          FROM PPSUSER.T_940_UNICODE T9U1
                                                         WHERE T9U1.DELIVERYNO = T9U.DELIVERYNO)
                                                    end AS SOLDORDERNO,
                
                                                    case
                                                      when (SELECT COUNT(DISTINCT T9U1.ITEMCUSTPO)
                                                              FROM PPSUSER.T_940_UNICODE T9U1
                                                             WHERE T9U1.DELIVERYNO = T9U.DELIVERYNO) > 1 then
                                                       ''
                                                      else
                                                       (SELECT DISTINCT CAST(T9U1.ITEMCUSTPO AS VARCHAR2(50)) as PurOrderNo
                                                          FROM PPSUSER.T_940_UNICODE T9U1
                                                         WHERE T9U1.DELIVERYNO = T9U.DELIVERYNO)
                                                    end AS PURORDERNO,
                                                    t9u.weborderno,
                                                    (SELECT SUM(TOI2.CARTON_QTY)
                                                       FROM PPSUSER.T_ORDER_INFO TOI2
                                                      WHERE TOI2.DELIVERY_NO = T9U.DELIVERYNO) AS TOTALCARTONQTY,
                                                    (select round(sum(t1.totalWeight), 2)
                                                       from (select TOI.ICTPN,
                                                                    TOI.DELIVERY_NO,
                                                                    vmi.FGWEIGHTKG * SUM(TOI.QTY) as totalWeight
                                                               from PPSUSER.T_ORDER_INFO TOI,
                                                                    PPTEST.vw_mpn_info   vmi
                                                              where TOI.ICTPN = vmi.ICTPARTNO
                                                                and TOI.DELIVERY_NO = '{0}'
                                                              group by TOI.ICTPN, TOI.DELIVERY_NO, vmi.FGWEIGHTKG) t1) AS TOTALNETWEIGHT,
                                                    (select round(sum(t1.totalWeight), 2)
                                                       from (select TOI.ICTPN,
                                                                    TOI.DELIVERY_NO,
                                                                    vmi.GROSSWEIGHTKG * SUM(TOI.CARTON_QTY) as totalWeight
                                                               from PPSUSER.T_ORDER_INFO TOI,
                                                                    PPTEST.vw_mpn_info   vmi
                                                              where TOI.ICTPN = vmi.ICTPARTNO
                                                                and TOI.DELIVERY_NO = '{0}'
                                                              group by TOI.ICTPN,
                                                                       TOI.DELIVERY_NO,
                                                                       vmi.GROSSWEIGHTKG) t1) AS TOTALGROSSWT,
                                                    (SELECT COUNT(DISTINCT TPO.PALLET_NO)
                                                       FROM PPSUSER.T_PALLET_ORDER TPO
                                                      WHERE TPO.DELIVERY_NO = T9U.DELIVERYNO) as totalpalletqty,
                                                    t9u.shiptoname,
                                                    t9u.shiptocompany,
                                                    t9u.shiptoaddress,
                                                    t9u.shiptoaddress2,
                                                    t9u.shiptoaddress3,
                                                    t9u.shiptocity,
                                                    t9u.shiptozip,
                                                    --t9u.shiptoaddress4,
                                                    decode(instr(t9u.regiondesc,'='),0,t9u.regiondesc,substr(t9u.regiondesc,instr(t9u.regiondesc,'=')+1)) as shiptoaddress4,
                                                    t9u.shipcntycode,
                                                    decode((SELECT DISTINCT TSP.PALLET_TYPE
                                                             FROM PPSUSER.T_SHIPMENT_PALLET TSP
                                                            WHERE TSP.PALLET_NO = '{1}'),
                                                           '999',
                                                           '',
                                                           '001',
                                                           (SELECT DISTINCT TSP.SSCC
                                                              FROM PPSUSER.T_SHIPMENT_PALLET TSP
                                                             WHERE TSP.PALLET_NO = '{1}')) as PalletSscc,
                                                    ppsuser.t_calculate_emeia_dent(tsi.shipment_id,
                                                                                   t9u.deliveryno,
                                                                                   '{1}') as palletCount,
                
                                                    T9U.NTECCE || T9U.CUSTSHIPINST || T9U.NTEWHI ||
                                                    T9U.EXCUSTNOTE || T9U.NTEGSI || T9U.NEBACUSTTEXT1 ||
                                                    T9U.NEBACUSTTEXT2 || T9U.NEBACUSTTEXT3 || T9U.NEBACUSTTEXT4 ||
                                                    T9U.NEBACUSTTEXT5 || T9U.Nebacusttext6 || T9U.NTEPCS ||
                                                    T9U.NTETRA || T9U.NTEOTH AS HeaderText
                                      FROM PPSUSER.T_940_UNICODE   T9U,
                                           ppsuser.t_order_info    toi,
                                           ppsuser.t_shipment_info tsi
                                     WHERE t9u.deliveryno = toi.delivery_no
                                       and toi.shipment_id = tsi.shipment_id
                                       and T9U.DELIVERYNO = '{0}'
                                    ", deliveryNo, palletNo);
            #endregion
            #region MlineSql
            //       string MixCooFlag = ClientUtils.ExecuteSQL(string.Format(@" select coo  from ppsuser.t_shipment_pallet
            //where pallet_no = '{0}' ", palletNo)).Tables[0].Rows[0][0].ToString();
            //       //修改COO取值逻辑 edit by franky 2020/3/13
            //       string lineSql = MixCooFlag.Equals("MIX") ?
            //                                          string.Format(@"SELECT LINEITEM,
            //                                                              ppsuser.f_transform_coo(T.COO, 1) AS COUNTRYCODE,
            //                                                              UPC,
            //                                                              MPN,
            //                                                              PRODESC,
            //                                                              SUM(COO_QTY) AS PERLINEITEMQTY,
            //                                                              FGWEIGHTKG AS PERLINEITEMNETWT,
            //                                                              GROSSWEIGHTKG AS PERLINEITEMGROSSWT,
            //                                                              ItemText
            //                                                         FROM (SELECT TOC.COO,
            //                                                                      TOI.LINE_ITEM AS LINEITEM,
            //                                                                      TOI.MPN,
            //                                                                      decode(VMI.JAN_CODE, '', VMI.UPC_CODE, VMI.JAN_CODE) UPC,
            //                                                                      PPSUSER.T_SHOW_NPI(T9U.DELIVERYNO,
            //                                                                                         T9U.CUSTDELITEM,
            //                                                                                         'Channel Delivery Note') as PRODESC,
            //                                                                      TOC.COO_QTY,
            //                                                                      VMI.FGWEIGHTKG,
            //                                                                      VMI.GROSSWEIGHTKG,
            //                                                                      T9U.shipinstruct || T9U.exitemnote || T9U.euitemtext1 ||
            //                                                                      T9U.euitemtext2 || T9U.euitemtext3 || T9U.euitemtext4 ||
            //                                                                      T9U.euitemtext5 || T9U.euitemtext6 || T9U.euitemtext7 ||
            //                                                                      T9U.euitemtext8 || T9U.euitemtext9 || T9U.euitemtext10 as ItemText
            //                                                                 FROM PPSUSER.T_ORDER_INFO   TOI,
            //                                                                      PPTEST.VW_MPN_INFO     VMI,
            //                                                                      PPSUSER.T_940_UNICODE  T9U,
            //                                                                      PPSUSER.T_ORDER_COO TOC
            //                                                                WHERE TOI.ICTPN = VMI.ICTPARTNO
            //                                                                  AND TOI.LINE_ITEM = T9U.CUSTDELITEM
            //                                                                  AND TOI.DELIVERY_NO = T9U.DELIVERYNO
            //                                                                  AND TOI.DELIVERY_NO = TOC.DN_NO
            //                                                                  AND TOI.LINE_ITEM = TOC.DN_LINE
            //                                                                  AND TOI.ICTPN = TOC.PART_NO
            //                                                              AND TOI.DELIVERY_NO = '{0}' ) T
            //                                                        GROUP BY LINEITEM,
            //                                                                 MPN,
            //                                                                 UPC,
            //                                                                 PRODESC,
            //                                                                 FGWEIGHTKG,
            //                                                                 COO,
            //                                                                 GROSSWEIGHTKG,
            //                                                                 ItemText
            //                                                        ORDER BY LINEITEM ASC", deliveryNo) :
            string lineSql = string.Format(@" SELECT LINEITEM,
                                                                   ppsuser.f_transform_coo(T.COO, 1) AS COUNTRYCODE,
                                                                   UPC,
                                                                   MPN,
                                                                   PRODESC,
                                                                   SUM(QTY) AS PERLINEITEMQTY,
                                                                   FGWEIGHTKG AS PERLINEITEMNETWT,
                                                                   GROSSWEIGHTKG AS PERLINEITEMGROSSWT,
                                                                   ItemText
                                                              FROM (SELECT TPO.COO,
                                                                           TOI.LINE_ITEM AS LINEITEM,
                                                                           TOI.MPN,
                                                                           decode(VMI.JAN_CODE, '', VMI.UPC_CODE, VMI.JAN_CODE) UPC,
                                                                           PPSUSER.T_SHOW_NPI(T9U.DELIVERYNO,
                                                                                              T9U.CUSTDELITEM,
                                                                                              'Channel Delivery Note') as PRODESC,
                                                                           TPO.ASSIGN_QTY AS QTY,
                                                                           VMI.FGWEIGHTKG,
                                                                           VMI.GROSSWEIGHTKG,
                                                                           T9U.shipinstruct || T9U.exitemnote || T9U.euitemtext1 ||
                                                                           T9U.euitemtext2 || T9U.euitemtext3 || T9U.euitemtext4 ||
                                                                           T9U.euitemtext5 || T9U.euitemtext6 || T9U.euitemtext7 ||
                                                                           T9U.euitemtext8 || T9U.euitemtext9 || T9U.euitemtext10 as ItemText
                                                                      FROM PPSUSER.T_ORDER_INFO   TOI,
                                                                           PPTEST.VW_MPN_INFO     VMI,
                                                                           PPSUSER.T_940_UNICODE  T9U,
                                                                           PPSUSER.T_PALLET_ORDER TPO
                                                                     WHERE TOI.ICTPN = VMI.ICTPARTNO
                                                                       AND TOI.LINE_ITEM = T9U.CUSTDELITEM
                                                                       AND TOI.DELIVERY_NO = T9U.DELIVERYNO
                                                                       AND TOI.DELIVERY_NO = TPO.DELIVERY_NO
                                                                       AND TOI.LINE_ITEM = TPO.LINE_ITEM
                                                                       AND TOI.ICTPN = TPO.ICTPN
                                                                       AND TOI.DELIVERY_NO = '{0}') T
                                                             GROUP BY LINEITEM,
                                                                      MPN,
                                                                      UPC,
                                                                      PRODESC,
                                                                      FGWEIGHTKG,
                                                                      COO,
                                                                      GROSSWEIGHTKG,
                                                                      ItemText
                                                             ORDER BY LINEITEM ASC
                                                            ", deliveryNo);
            #endregion

            ds.Tables.Add(Util.getDataTaleC(HeadLineSql, "HeadLine"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "M_line"));


            if (string.IsNullOrEmpty(strCrystalFullPath))
            {
                if (!isCustom) //不是关务用默认值
                {
                    strCrystalFullPath = Constant.EMEIA_SIGNLE_DeliveryNote_URL;
                }
                else//isCustom如果是关务的是用宋体的模板 
                {
                    strCrystalFullPath = Constant.EMEIA_SIGNLE_DeliveryNote_URL_ByGW;
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
