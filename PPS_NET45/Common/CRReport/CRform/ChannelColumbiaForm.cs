using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class ChannelColumbiaForm
    {
        public string completeDiskPath = "";
        public ChannelColumbiaForm(string acDn, string lineItem, string shipmentId, bool print, string diskPath)
        {
            Initialize(acDn, lineItem, shipmentId, print, diskPath);
        }

        private void Initialize(string acDn, string lineItem, string shipmentId, bool print, string diskPath)
        {
            //初始化 DataSet
            DataSet ds = new DataSet();

            string headerSql = string.Format(@"select distinct b.shipment_id SHIPMENT_ID,
                                                                a.deliveryno AC_DN,
                                                                c.suppliername as SUP_NAME,
                                                                c.supplieraddress1 as SUP_ADDR1,
                                                                c.supplieraddress2 as SUP_ADDR2,
                                                                c.supplieraddress3 as SUP_ADDR3,
                                                                c.supplieraddress4 as SUP_ADDR4,
                                                                c.supplieraddress5 as SUP_ADDR5,
                                                                a.soldtoname as SO_NAME,
                                                                a.soldtocompany as SO_COMPANY,
                                                                a.soldtoaddress as SO_ADDR1,
                                                                a.soldtoaddress2 as SO_ADDR2,
                                                                a.soldtoaddress3 as SO_ADDR3,
                                                                a.soldtoaddress4 as SO_ADDR4,
                                                                a.soldtocity ||' '|| (CASE
                                                                    WHEN INSTR(A.SOLDTOREGIONDESC, '=') > 0 THEN
                                                                    SUBSTR(CAST(A.SOLDTOREGIONDESC AS VARCHAR2(30)),
                                                                            1,
                                                                            INSTR(A.SOLDTOREGIONDESC, '=') - 1)||' '
                                                                    ELSE
                                                                    ' '
                                                                END) || DECODE(CAST(A.SOLDCNTYCODE AS VARCHAR2(30)),
                                                                                'US',
                                                                                SUBSTR(A.SOLDTOZIP, 1, 5),
                                                                                A.SOLDTOZIP) as SO_ADDR5,
                                                                a.soldcntycode as SO_COUNTRY_CODE,
                                                                a.ntecce||a.custshipinst||a.ntewhi||a.excustnote||a.ntegsi||a.nebacusttext1||a.nebacusttext2||a.nebacusttext3||a.nebacusttext4||a.nebacusttext5||a.nebacusttext6||a.ntepcs||a.ntetra||a.nteoth  as SPC_INST, --后期再确定
                                                                a.custsono as AC_SO,
                                                                 CASE
                                                                  WHEN (SELECT COUNT(*)
                                                                          FROM PPSUSER.T_SHIPMENT_SAWB TSSA
                                                                         WHERE TSSA.SAWB_SHIPMENT_ID = '{2}') > 0 THEN
                                                                   (SELECT distinct TSI.HAWB
                                                                      FROM PPSUSER.t_Order_Info    toi,
                                                                           ppsuser.t_shipment_info tsi
                                                                     where TSI.SHIPMENT_ID = TOI.SHIPMENT_ID
                                                                       AND toi.delivery_no = '{0}'
                                                                       and toi.shipment_id in
                                                                           (SELECT DISTINCT TSSA.SHIPMENT_ID
                                                                              FROM PPSUSER.T_SHIPMENT_SAWB TSSA
                                                                             WHERE TSSA.SAWB_SHIPMENT_ID = '{2}'))
                                                                  ELSE
                                                                   b.hawb
                                                                END as HAWB,
                                                                CASE
                                                                  WHEN (SELECT COUNT(*)
                                                                          FROM PPSUSER.T_SHIPMENT_SAWB TSSA
                                                                         WHERE TSSA.SAWB_SHIPMENT_ID = '{2}') > 0 THEN
                                                                   (SELECT distinct OCTP.SCACCODE
                                                                      FROM PPSUSER.t_Order_Info               toi,
                                                                           ppsuser.t_shipment_info            tsi,
                                                                           PPTEST.OMS_CARRIER_TRACKING_PREFIX OCTP
                                                                     where TSI.SHIPMENT_ID = TOI.SHIPMENT_ID
                                                                       AND TSI.CARRIER_CODE = OCTP.CARRIERCODE
                                                                       AND toi.delivery_no = '{0}'
                                                                       and toi.shipment_id in
                                                                           (SELECT DISTINCT TSSA.SHIPMENT_ID
                                                                              FROM PPSUSER.T_SHIPMENT_SAWB TSSA
                                                                             WHERE TSSA.SAWB_SHIPMENT_ID = '{2}'))
                                                                  ELSE
                                                                   OCTP.SCACCODE
                                                                END as CARR_NAME,
                                                                b.transport as PER_MODE,
                                                                'DAP' as FR_TERM, --US 的没有
                                                                (SELECT SUM(TOI.QTY)
                                                                    FROM PPSUSER.T_ORDER_INFO TOI
                                                                    WHERE TOI.DELIVERY_NO = A.DELIVERYNO
                                                                    and toi.shipment_id = '{2}') as TOT_QTY, --整个DN 的Piece  T_ORDER_INFO 里面Qty的Sum
                                                                to_char(tsi.shipping_time, 'yyyy/MM/dd') as SHIP_DATE,
                                                                a.shiptoname as ST_NAME,
                                                                a.shiptocompany as ST_COMPANY,
                                                                a.shiptoaddress as ST_ADDR1,
                                                                a.shiptoaddress2 as ST_ADDR2,
                                                                a.shiptoaddress3 as ST_ADDR3,
                                                                a.shiptoaddress4 as ST_ADDR4,
                                                                a.shiptocity||' ' || (CASE
                                                                    WHEN INSTR(A.REGIONDESC, '=') > 0 THEN
                                                                    SUBSTR(CAST(A.REGIONDESC AS VARCHAR2(30)),
                                                                            1,
                                                                            INSTR(A.REGIONDESC, '=') - 1)||' '
                                                                    ELSE
                                                                    ' '
                                                                END) || DECODE(CAST(A.SHIPCNTYCODE AS VARCHAR2(30)),
                                                                                'US',
                                                                                SUBSTR(A.shiptozip, 1, 5),
                                                                                A.shiptozip) as ST_ADDR5,
                                                                a.shipcntycode as ST_COUNTRY_CODE,
                                                                a.custpono AS AC_ECPON,
                                                                A.WEBORDERNO AS WO_NUM,    ---WEBORDER change to WEBORDERNO by Jammy --20200730
                                                                --Return 的信息参考pptest.oms_return_amr 根据Shipcntycode,shiptoZip
                                                               o.packinglistname as RET_NAME1,
                                                             --  o.shiplabelname as RET_NAME1, -- wenxing 20201022: change to packinglistname
                                                               o.address as RET_NAME2,
                                                               O.DISTRICT AS RET_ADDR1,   
                                                               o.city || ',' || o.state || ' ' || o.postalcode as RET_ADDR2,
                                                               O.COUNTRYCODE AS RET_ADDR3,
                                                                '' RET_ADDR4,
                                                                '' RET_ADDR5,
                                                                (select round(sum(t1.totalWeight), 2)
                                                                    from (select TOI.ICTPN,
                                                                                TOI.DELIVERY_NO,
                                                                                vmi.FGWEIGHTKG * SUM(TOI.QTY) as totalWeight
                                                                            from PPSUSER.T_ORDER_INFO TOI,
                                                                                pptest.vw_mpn_info   vmi
                                                                            where TOI.ICTPN = vmi.ICTPARTNO
                                                                            and TOI.DELIVERY_NO = '{0}'
                                                                            and toi.shipment_id = '{2}'
                                                                            group by TOI.ICTPN, TOI.DELIVERY_NO, vmi.FGWEIGHTKG) t1) TOT_NW, --sum(FGWEIGHTKG) pptest.vw_mpn_info                
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
                                                                                    vmi.GROSSWEIGHTKG) t1) TOT_GW, --sum(Grossweight)pptest.vw_mpn_info
                                                                (select sum(toi.carton_qty)
                                                                    from ppsuser.t_order_info toi
                                                                    where toi.delivery_no = b.delivery_no
                                                                    and toi.shipment_id = '{2}') TOT_CTN, --Sum(Cartons)   
                                                                a.ShipToContTel as RECE_C_PHONE --不清楚
                             from  ppsuser.t_940_unicode   a,
                                   ppsuser.t_order_info    b,
                                   ppsuser.t_shipment_info  tsi,
                                   pptest.oms_supplier_amr c,
                                   pptest.oms_return_amr   o,
                                   pptest.oms_carrier_tracking_prefix octp
                             where TRIM(a.deliveryno) = TRIM(b.delivery_no)
                               and tsi.shipment_id = b.shipment_id
                               and TRIM(a.custdelitem)= b.line_item
                               and tsi.carrier_code = octp.carriercode
                               and TRIM(a.shipcntycode) = c.shiptocntycode
                               and trim(a.shipcntycode) = o.shiptocntycode                             
                               and trim(a.deliveryno) = '{0}' 
                               and trim(a.custdelitem) ='{1}'
                               and b.shipment_id = '{2}'", acDn, lineItem, shipmentId);
            //           string MixCooFlag = ClientUtils.ExecuteSQL(string.Format(@" select coo  from ppsuser.t_shipment_pallet
            //    where shipment_id = '{0}'", shipmentId)).Tables[0].Rows[0][0].ToString();
            //           //修改COO取值逻辑 edit by franky 2020/3/13
            //           string lineSql = MixCooFlag.Equals("MIX") ?
            //                                              string.Format(@"SELECT DISTINCT T.SHIPMENT_ID AS SHIPMENT_ID,
            //               T.DELIVERY_NO as AC_DN,
            //               T.LINE_ITEM as AC_DN_LINE,
            //               'Assembled in ' || ppsuser.f_transform_coo(T.COO, 1) as COO,
            //               T.MPN as AC_PN,
            //               UPC_JAN,
            //               MATE_DESC,
            //               sum(COO_Qty) as QTY,
            //               ROUND(SUM(FG_WEIGHT), 2) as NETW,
            //               ROUND(SUM(GROSS_WEIGHT), 2) AS GROW
            // FROM (SELECT TOI.SHIPMENT_ID,
            //              TOI.DELIVERY_NO,
            //              TOI.LINE_ITEM,
            //              TOI.MPN,
            //              decode(VMI.JAN_CODE, '', VMI.UPC_CODE, VMI.JAN_CODE) UPC_JAN,
            //              ppsuser.t_show_npi(t9u.deliveryno,
            //                                 T9U.CUSTDELITEM,
            //                                 'Channel Packing List') as MATE_DESC,
            //              TOC.COO_Qty,
            //              TOC.COO_Qty * VMI.FGWEIGHTKG AS FG_WEIGHT,
            //              TOC.COO_QTY / OPM.PACKQTY * VMI.GROSSWEIGHTKG AS GROSS_WEIGHT,
            //              TOC.COO
            //         FROM PPSUSER.T_ORDER_INFO   TOI,
            //              PPTEST.VW_MPN_INFO     VMI,
            //              PPSUSER.T_940_UNICODE  T9U,
            //              PPSUSER.T_ORDER_COO    TOC,
            //              PPTEST.OMS_PARTMAPPING OPM
            //        WHERE TOI.ICTPN = VMI.ICTPARTNO
            //          AND TOI.ICTPN = OPM.PART
            //          AND TOI.LINE_ITEM = T9U.CUSTDELITEM
            //          AND TOI.DELIVERY_NO = T9U.DELIVERYNO
            //          AND TOI.DELIVERY_NO = TOC.DN_NO
            //          AND TOI.LINE_ITEM = TOC.DN_LINE
            //          AND TOI.ICTPN = TOC.PART_NO
            //          AND TOI.DELIVERY_NO = '{0}'
            //          AND TOI.SHIPMENT_ID = '{1}') T
            //GROUP BY T.SHIPMENT_ID,
            //         T.DELIVERY_NO,
            //         T.LINE_ITEM,
            //         T.MPN,
            //         T.COO,
            //         UPC_JAN,
            //         MATE_DESC
            //order by T.LINE_ITEM", acDn, shipmentId) :
            string lineSql = string.Format(@"SELECT DISTINCT T.SHIPMENT_ID AS SHIPMENT_ID,
                                                                        T.DELIVERY_NO as AC_DN,
                                                                        T.LINE_ITEM as AC_DN_LINE,
                                                                        'Assembled in ' || ppsuser.f_transform_coo(T.COO, 1) as COO,
                                                                        T.MPN as AC_PN,
                                                                        UPC_JAN,
                                                                        MATE_DESC,
                                                                        sum(qty) as QTY,
                                                                        ROUND(SUM(FG_WEIGHT), 2) as NETW,
                                                                        ROUND(SUM(GROSS_WEIGHT), 2) AS GROW
                                                          FROM (SELECT TOI.SHIPMENT_ID,
                                                                       TOI.DELIVERY_NO,
                                                                       TOI.LINE_ITEM,
                                                                       TOI.MPN,
                                                                       decode(VMI.JAN_CODE, '', VMI.UPC_CODE, VMI.JAN_CODE) UPC_JAN,
                                                                       ppsuser.t_show_npi(t9u.deliveryno,
                                                                                          T9U.CUSTDELITEM,
                                                                                          'Channel Packing List') as MATE_DESC,
                                                                       TPO.ASSIGN_QTY AS QTY,
                                                                       TPO.ASSIGN_QTY * VMI.FGWEIGHTKG AS FG_WEIGHT,
                                                                       TPO.ASSIGN_CARTON * VMI.GROSSWEIGHTKG AS GROSS_WEIGHT,
                                                                       TPO.COO
                                                                  FROM PPSUSER.T_ORDER_INFO   TOI,
                                                                       PPTEST.VW_MPN_INFO     VMI,
                                                                       PPSUSER.T_940_UNICODE  T9U,
                                                                       PPSUSER.T_PALLET_ORDER TPO
                                                                 WHERE TOI.ICTPN = VMI.ICTPARTNO
                                                                   AND TOI.LINE_ITEM = T9U.CUSTDELITEM
                                                                   AND TOI.DELIVERY_NO = T9U.DELIVERYNO
                                                                   AND TOI.DELIVERY_NO = TPO.DELIVERY_NO
                                                                   AND TOI.LINE_ITEM = TPO.LINE_ITEM
                                                                   AND TOI.ICTPN=TPO.ICTPN
                                                                  AND TOI.SHIPMENT_ID=TPO.SHIPMENT_ID
                                                                   AND TOI.DELIVERY_NO = '{0}'
                                                                   AND TOI.SHIPMENT_ID = '{1}') T
                                                         GROUP BY T.SHIPMENT_ID,
                                                                  T.DELIVERY_NO,
                                                                  T.LINE_ITEM,
                                                                  T.MPN,
                                                                  T.COO,
                                                                  UPC_JAN,
                                                                  MATE_DESC
                                                         order by T.LINE_ITEM", acDn, shipmentId);
            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));

            if (print)
            {
                Util.CreateDataTable(
                Constant.ColumbiaChannelPK_URL,
                ds);
            }
            else
            {
                Util.printPDFCrystalReportV2(Constant.ColumbiaChannelPK_URL_ByGW, ds, diskPath);
            }
        }

        public ChannelColumbiaForm(string acDn, string lineItem, string shipmentId, string strCrystalFullPath, bool isCustom, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            Initialize2(acDn, lineItem, shipmentId, strCrystalFullPath, isCustom, isPDF, strPDFPath, nCopies, isOnlyGetDS, out dtCrystal, out serverFullLabelName);
        }
        private void Initialize2(string acDn, string lineItem, string shipmentId, string strCrystalFullPath, bool isCustom, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            dtCrystal = null;
            serverFullLabelName = string.Empty;
            //初始化 DataSet
            DataSet ds = new DataSet();
            #region headerSql
            string headerSql = string.Format(@"select distinct b.shipment_id SHIPMENT_ID,
                                                                a.deliveryno AC_DN,
                                                                c.suppliername as SUP_NAME,
                                                                c.supplieraddress1 as SUP_ADDR1,
                                                                c.supplieraddress2 as SUP_ADDR2,
                                                                c.supplieraddress3 as SUP_ADDR3,
                                                                c.supplieraddress4 as SUP_ADDR4,
                                                                c.supplieraddress5 as SUP_ADDR5,
                                                                a.soldtoname as SO_NAME,
                                                                a.soldtocompany as SO_COMPANY,
                                                                a.soldtoaddress as SO_ADDR1,
                                                                a.soldtoaddress2 as SO_ADDR2,
                                                                a.soldtoaddress3 as SO_ADDR3,
                                                                a.soldtoaddress4 as SO_ADDR4,
                                                                a.soldtocity ||' '|| (CASE
                                                                    WHEN INSTR(A.SOLDTOREGIONDESC, '=') > 0 THEN
                                                                    SUBSTR(CAST(A.SOLDTOREGIONDESC AS VARCHAR2(30)),
                                                                            1,
                                                                            INSTR(A.SOLDTOREGIONDESC, '=') - 1)||' '
                                                                    ELSE
                                                                    ' '
                                                                END) || DECODE(CAST(A.SOLDCNTYCODE AS VARCHAR2(30)),
                                                                                'US',
                                                                                SUBSTR(A.SOLDTOZIP, 1, 5),
                                                                                A.SOLDTOZIP) as SO_ADDR5,
                                                                a.soldcntycode as SO_COUNTRY_CODE,
                                                                a.ntecce||a.custshipinst||a.ntewhi||a.excustnote||a.ntegsi||a.nebacusttext1||a.nebacusttext2||a.nebacusttext3||a.nebacusttext4||a.nebacusttext5||a.nebacusttext6||a.ntepcs||a.ntetra||a.nteoth  as SPC_INST, --后期再确定
                                                                a.custsono as AC_SO,
                                                                 CASE
                                                                  WHEN (SELECT COUNT(*)
                                                                          FROM PPSUSER.T_SHIPMENT_SAWB TSSA
                                                                         WHERE TSSA.SAWB_SHIPMENT_ID = '{2}') > 0 THEN
                                                                   (SELECT distinct TSI.HAWB
                                                                      FROM PPSUSER.t_Order_Info    toi,
                                                                           ppsuser.t_shipment_info tsi
                                                                     where TSI.SHIPMENT_ID = TOI.SHIPMENT_ID
                                                                       AND toi.delivery_no = '{0}'
                                                                       and toi.shipment_id in
                                                                           (SELECT DISTINCT TSSA.SHIPMENT_ID
                                                                              FROM PPSUSER.T_SHIPMENT_SAWB TSSA
                                                                             WHERE TSSA.SAWB_SHIPMENT_ID = '{2}'))
                                                                  ELSE
                                                                   b.hawb
                                                                END as HAWB,
                                                                CASE
                                                                  WHEN (SELECT COUNT(*)
                                                                          FROM PPSUSER.T_SHIPMENT_SAWB TSSA
                                                                         WHERE TSSA.SAWB_SHIPMENT_ID = '{2}') > 0 THEN
                                                                   (SELECT distinct OCTP.SCACCODE
                                                                      FROM PPSUSER.t_Order_Info               toi,
                                                                           ppsuser.t_shipment_info            tsi,
                                                                           PPTEST.OMS_CARRIER_TRACKING_PREFIX OCTP
                                                                     where TSI.SHIPMENT_ID = TOI.SHIPMENT_ID
                                                                       AND TSI.CARRIER_CODE = OCTP.CARRIERCODE
                                                                       AND toi.delivery_no = '{0}'
                                                                       and toi.shipment_id in
                                                                           (SELECT DISTINCT TSSA.SHIPMENT_ID
                                                                              FROM PPSUSER.T_SHIPMENT_SAWB TSSA
                                                                             WHERE TSSA.SAWB_SHIPMENT_ID = '{2}'))
                                                                  ELSE
                                                                   OCTP.SCACCODE
                                                                END as CARR_NAME,
                                                                b.transport as PER_MODE,
                                                                'DAP' as FR_TERM, --US 的没有
                                                                (SELECT SUM(TOI.QTY)
                                                                    FROM PPSUSER.T_ORDER_INFO TOI
                                                                    WHERE TOI.DELIVERY_NO = A.DELIVERYNO
                                                                    and toi.shipment_id = '{2}') as TOT_QTY, --整个DN 的Piece  T_ORDER_INFO 里面Qty的Sum
                                                                to_char(tsi.shipping_time, 'yyyy/MM/dd') as SHIP_DATE,
                                                                a.shiptoname as ST_NAME,
                                                                a.shiptocompany as ST_COMPANY,
                                                                a.shiptoaddress as ST_ADDR1,
                                                                a.shiptoaddress2 as ST_ADDR2,
                                                                a.shiptoaddress3 as ST_ADDR3,
                                                                a.shiptoaddress4 as ST_ADDR4,
                                                                a.shiptocity||' ' || (CASE
                                                                    WHEN INSTR(A.REGIONDESC, '=') > 0 THEN
                                                                    SUBSTR(CAST(A.REGIONDESC AS VARCHAR2(30)),
                                                                            1,
                                                                            INSTR(A.REGIONDESC, '=') - 1)||' '
                                                                    ELSE
                                                                    ' '
                                                                END) || DECODE(CAST(A.SHIPCNTYCODE AS VARCHAR2(30)),
                                                                                'US',
                                                                                SUBSTR(A.shiptozip, 1, 5),
                                                                                A.shiptozip) as ST_ADDR5,
                                                                a.shipcntycode as ST_COUNTRY_CODE,
                                                                a.custpono AS AC_ECPON,
                                                                A.WEBORDERNO AS WO_NUM,    ---WEBORDER change to WEBORDERNO by Jammy --20200730
                                                                --Return 的信息参考pptest.oms_return_amr 根据Shipcntycode,shiptoZip
                                                               o.packinglistname as RET_NAME1,
                                                              -- o.shiplabelname as RET_NAME1, -- wenxing 20201022: change to packinglistname
                                                               o.address as RET_NAME2,
                                                               O.DISTRICT AS RET_ADDR1,   
                                                               o.city || ',' || o.state || ' ' || o.postalcode as RET_ADDR2,
                                                               O.COUNTRYCODE AS RET_ADDR3,
                                                                '' RET_ADDR4,
                                                                '' RET_ADDR5,
                                                                (select round(sum(t1.totalWeight), 2)
                                                                    from (select TOI.ICTPN,
                                                                                TOI.DELIVERY_NO,
                                                                                vmi.FGWEIGHTKG * SUM(TOI.QTY) as totalWeight
                                                                            from PPSUSER.T_ORDER_INFO TOI,
                                                                                pptest.vw_mpn_info   vmi
                                                                            where TOI.ICTPN = vmi.ICTPARTNO
                                                                            and TOI.DELIVERY_NO = '{0}'
                                                                            and toi.shipment_id = '{2}'
                                                                            group by TOI.ICTPN, TOI.DELIVERY_NO, vmi.FGWEIGHTKG) t1) TOT_NW, --sum(FGWEIGHTKG) pptest.vw_mpn_info                
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
                                                                                    vmi.GROSSWEIGHTKG) t1) TOT_GW, --sum(Grossweight)pptest.vw_mpn_info
                                                                (select sum(toi.carton_qty)
                                                                    from ppsuser.t_order_info toi
                                                                    where toi.delivery_no = b.delivery_no
                                                                    and toi.shipment_id = '{2}') TOT_CTN, --Sum(Cartons)   
                                                                a.ShipToContTel as RECE_C_PHONE --不清楚
                             from  ppsuser.t_940_unicode   a,
                                   ppsuser.t_order_info    b,
                                   ppsuser.t_shipment_info  tsi,
                                   pptest.oms_supplier_amr c,
                                   pptest.oms_return_amr   o,
                                   pptest.oms_carrier_tracking_prefix octp
                             where TRIM(a.deliveryno) = TRIM(b.delivery_no)
                               and tsi.shipment_id = b.shipment_id
                               and TRIM(a.custdelitem)= b.line_item
                               and tsi.carrier_code = octp.carriercode
                               and TRIM(a.shipcntycode) = c.shiptocntycode
                               and trim(a.shipcntycode) = o.shiptocntycode                             
                               and trim(a.deliveryno) = '{0}' 
                               and trim(a.custdelitem) ='{1}'
                               and b.shipment_id = '{2}'", acDn, lineItem, shipmentId);
            #endregion
            #region lineSql
            //       string MixCooFlag = ClientUtils.ExecuteSQL(string.Format(@" select coo  from ppsuser.t_shipment_pallet
            //where shipment_id = '{0}'", shipmentId)).Tables[0].Rows[0][0].ToString();
            string lineSql = string.Format(@"SELECT DISTINCT T.SHIPMENT_ID AS SHIPMENT_ID,
                                                                        T.DELIVERY_NO as AC_DN,
                                                                        T.LINE_ITEM as AC_DN_LINE,
                                                                        'Assembled in ' || ppsuser.f_transform_coo(T.COO, 1) as COO,
                                                                        T.MPN as AC_PN,
                                                                        UPC_JAN,
                                                                        MATE_DESC,
                                                                        sum(qty) as QTY,
                                                                        ROUND(SUM(FG_WEIGHT), 2) as NETW,
                                                                        ROUND(SUM(GROSS_WEIGHT), 2) AS GROW
                                                          FROM (SELECT TOI.SHIPMENT_ID,
                                                                       TOI.DELIVERY_NO,
                                                                       TOI.LINE_ITEM,
                                                                       TOI.MPN,
                                                                       decode(VMI.JAN_CODE, '', VMI.UPC_CODE, VMI.JAN_CODE) UPC_JAN,
                                                                       ppsuser.t_show_npi(t9u.deliveryno,
                                                                                          T9U.CUSTDELITEM,
                                                                                          'Channel Packing List') as MATE_DESC,
                                                                       TPO.ASSIGN_QTY AS QTY,
                                                                       TPO.ASSIGN_QTY * VMI.FGWEIGHTKG AS FG_WEIGHT,
                                                                       TPO.ASSIGN_CARTON * VMI.GROSSWEIGHTKG AS GROSS_WEIGHT,
                                                                       TPO.COO
                                                                  FROM PPSUSER.T_ORDER_INFO   TOI,
                                                                       PPTEST.VW_MPN_INFO     VMI,
                                                                       PPSUSER.T_940_UNICODE  T9U,
                                                                       PPSUSER.T_PALLET_ORDER TPO
                                                                 WHERE TOI.ICTPN = VMI.ICTPARTNO
                                                                   AND TOI.LINE_ITEM = T9U.CUSTDELITEM
                                                                   AND TOI.DELIVERY_NO = T9U.DELIVERYNO
                                                                   AND TOI.DELIVERY_NO = TPO.DELIVERY_NO
                                                                   AND TOI.LINE_ITEM = TPO.LINE_ITEM
                                                                   AND TOI.ICTPN=TPO.ICTPN
                                                                  AND TOI.SHIPMENT_ID=TPO.SHIPMENT_ID
                                                                   AND TOI.DELIVERY_NO = '{0}'
                                                                   AND TOI.SHIPMENT_ID = '{1}') T
                                                         GROUP BY T.SHIPMENT_ID,
                                                                  T.DELIVERY_NO,
                                                                  T.LINE_ITEM,
                                                                  T.MPN,
                                                                  T.COO,
                                                                  UPC_JAN,
                                                                  MATE_DESC
                                                         order by T.LINE_ITEM", acDn, shipmentId);
            //修改COO取值逻辑 edit by franky 2020/3/13
            #region
            //               MixCooFlag.Equals("MIX") ?
            //                                              string.Format(@"SELECT DISTINCT T.SHIPMENT_ID AS SHIPMENT_ID,
            //               T.DELIVERY_NO as AC_DN,
            //               T.LINE_ITEM as AC_DN_LINE,
            //               'Assembled in ' || ppsuser.f_transform_coo(T.COO, 1) as COO,
            //               T.MPN as AC_PN,
            //               UPC_JAN,
            //               MATE_DESC,
            //               sum(COO_Qty) as QTY,
            //               ROUND(SUM(FG_WEIGHT), 2) as NETW,
            //               ROUND(SUM(GROSS_WEIGHT), 2) AS GROW
            // FROM (SELECT TOI.SHIPMENT_ID,
            //              TOI.DELIVERY_NO,
            //              TOI.LINE_ITEM,
            //              TOI.MPN,
            //              decode(VMI.JAN_CODE, '', VMI.UPC_CODE, VMI.JAN_CODE) UPC_JAN,
            //              ppsuser.t_show_npi(t9u.deliveryno,
            //                                 T9U.CUSTDELITEM,
            //                                 'Channel Packing List') as MATE_DESC,
            //              TOC.COO_Qty,
            //              TOC.COO_Qty * VMI.FGWEIGHTKG AS FG_WEIGHT,
            //              TOC.COO_QTY / OPM.PACKQTY * VMI.GROSSWEIGHTKG AS GROSS_WEIGHT,
            //              TOC.COO
            //         FROM PPSUSER.T_ORDER_INFO   TOI,
            //              PPTEST.VW_MPN_INFO     VMI,
            //              PPSUSER.T_940_UNICODE  T9U,
            //              PPSUSER.T_ORDER_COO    TOC,
            //              PPTEST.OMS_PARTMAPPING OPM
            //        WHERE TOI.ICTPN = VMI.ICTPARTNO
            //          AND TOI.ICTPN = OPM.PART
            //          AND TOI.LINE_ITEM = T9U.CUSTDELITEM
            //          AND TOI.DELIVERY_NO = T9U.DELIVERYNO
            //          AND TOI.DELIVERY_NO = TOC.DN_NO
            //          AND TOI.LINE_ITEM = TOC.DN_LINE
            //          AND TOI.ICTPN = TOC.PART_NO
            //          AND TOI.DELIVERY_NO = '{0}'
            //          AND TOI.SHIPMENT_ID = '{1}') T
            //GROUP BY T.SHIPMENT_ID,
            //         T.DELIVERY_NO,
            //         T.LINE_ITEM,
            //         T.MPN,
            //         T.COO,
            //         UPC_JAN,
            //         MATE_DESC
            //order by T.LINE_ITEM", acDn, shipmentId) :

            //修改COO取值逻辑 edit by franky 2020/1/21
            //string lineSql = string.Format(@" SELECT DISTINCT  T.SHIPMENT_ID AS SHIPMENT_ID,
            //                                      T.DELIVERY_NO as AC_DN,
            //                                      T.LINE_ITEM as AC_DN_LINE,
            //                                     -- 'Assembled in Vietnam' COO,
            //                                    'Assembled in ' || ppsuser.getcoobykp(T.ICTPN,-1) as  COO,
            //                                      T.MPN as AC_PN,
            //                                      UPC_JAN,
            //                                      MATE_DESC,
            //                                      sum(qty) as QTY,         
            //                                      ROUND(SUM(FG_WEIGHT),2) as NETW,
            //                                      ROUND(SUM(GROSS_WEIGHT),2) AS GROW
            //                                 FROM (SELECT TOI.SHIPMENT_ID,
            //                                              TOI.DELIVERY_NO,
            //                                              TOI.LINE_ITEM,
            //                                              TOI.MPN,
            //                                              decode(VMI.JAN_CODE, '', VMI.UPC_CODE, VMI.JAN_CODE) UPC_JAN,
            //                                              ppsuser.t_show_npi(t9u.deliveryno,T9U.CUSTDELITEM,'Channel Packing List') as MATE_DESC,
            //                                              toi.qty,
            //                                              TOI.Qty * VMI.FGWEIGHTKG AS FG_WEIGHT,
            //                                              TOI.CARTON_QTY * VMI.GROSSWEIGHTKG AS GROSS_WEIGHT,
            //                                              TOI.ICTPN
            //                                         FROM PPSUSER.T_ORDER_INFO  TOI,
            //                                              PPTEST.VW_MPN_INFO    VMI,
            //                                              PPSUSER.T_940_UNICODE T9U
            //                                        WHERE TOI.ICTPN = VMI.ICTPARTNO
            //                                          AND TOI.LINE_ITEM = T9U.CUSTDELITEM
            //                                          AND TOI.DELIVERY_NO = T9U.DELIVERYNO
            //                                          AND TOI.DELIVERY_NO = '{0}'
            //                                          AND TOI.SHIPMENT_ID='{1}') T
            //                                GROUP BY T.SHIPMENT_ID,
            //                                         T.DELIVERY_NO,
            //                                         T.LINE_ITEM,
            //                                         T.MPN,
            //                                         T.ICTPN,
            //                                         UPC_JAN,
            //                                         MATE_DESC
            //                                order by  T.LINE_ITEM", acDn, shipmentId);
            #endregion
            #endregion
            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));



            if (string.IsNullOrEmpty(strCrystalFullPath))
            {
                if (!isCustom) //不是关务用默认值
                {
                    strCrystalFullPath = Constant.ColumbiaChannelPK_URL;
                }
                else//isCustom如果是关务的是用宋体的模板 
                {
                    strCrystalFullPath = Constant.ColumbiaChannelPK_URL;
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
