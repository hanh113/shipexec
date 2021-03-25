using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;
namespace Check.Dao
{
    class DataGetWay
    {
        public DataGetWay()
        {

        }
        public DataTable getShipMentInfoByshipmentId(string shipMentId)
        {
            string sql = string.Format(@"   select tsi.poe,
                                            decode(upper(tsi.security),'BASIC','低','MEDIUM','中','HIGH','高') as security,
                                            tsi.region,
                                            tsi.carrier_name  as carrierName,
                                            tsi.shipment_type as shipmentType,
                                            tsi.type  as type_ ,
                                            tsi.carrier_code ,
                                            tsi.service_level 
                                        from ppsuser.t_shipment_info tsi
                                        where tsi.shipment_id = '{0}'", shipMentId);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public DataTable fd_checkIsLinkPallletNo(string trackingNo, string pickPalletNo)
        {
            string sql = @"     select tss.serial_number
                                                from ppsuser.t_sn_status tss
                                               where TSS.SSCC =:TrackingNo
                                                 and tss.pick_pallet_no =:PickPalletNo";
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "TrackingNo", trackingNo };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PickPalletNo", pickPalletNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable ds_checkIsLinkPallletNoUpsYmtCnpl(string trackingNo, string pickPalletNo)
        {
            string sql = @"        SELECT tss.serial_number
                                      FROM PPSUSER.T_SN_STATUS TSS
                                     WHERE TSS.TRACKING_NO = :TrackingNo
                                       AND TSS.PICK_PALLET_NO = :PickPalletNo";
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "TrackingNo", trackingNo };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PickPalletNo", pickPalletNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable ds_checkIsLinkPallletNoDhlSf(string trackingNo, string pickPalletNo)
        {
            string sql = @"        SELECT tss.serial_number
                                      FROM PPSUSER.T_SN_STATUS TSS
                                     WHERE TSS.BABYTRACKING_NO = :TrackingNo
                                       AND TSS.PICK_PALLET_NO = :PickPalletNo";
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "TrackingNo", trackingNo };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PickPalletNo", pickPalletNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable ds_checkIsLinkPallletNoTnt(string trackingNo, string pickPalletNo)
        {
            string sql = @"        SELECT tss.serial_number
                                      FROM PPSUSER.T_SN_STATUS TSS
                                     WHERE instr(:TrackingNo,substr(TSS.TRACKING_NO,1,9))>0
                                       AND TSS.PICK_PALLET_NO = :PickPalletNo";
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "TrackingNo", trackingNo };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PickPalletNo", pickPalletNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }



        public DataTable fd_checkIsLinkCartonNo(string trackingNo, string cartonNo)
        {
            string sql = @"     select tss.serial_number
                                                from ppsuser.t_sn_status tss
                                               where TSS.SSCC = :TrackingNo
                                                 and tss.Carton_No = :CartonNo";
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "TrackingNo", trackingNo };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable ds_checkIsLinkCartonNoNoUpsYmtCnpl(string trackingNo, string cartonNo)
        {
            string sql = @"        SELECT tss.serial_number
                                      FROM PPSUSER.T_SN_STATUS TSS
                                     WHERE TSS.TRACKING_NO = :TrackingNo
                                       AND TSS.Carton_No = :CartonNo";
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "TrackingNo", trackingNo };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable ds_checkIsLinkCartonNoDhlSf(string trackingNo, string cartonNo)
        {
            string sql = @"        SELECT tss.serial_number
                                      FROM PPSUSER.T_SN_STATUS TSS
                                     WHERE TSS.BABYTRACKING_NO = :TrackingNo
                                       AND TSS.Carton_No = :CartonNo";
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "TrackingNo", trackingNo };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public DataTable ds_checkIsLinkCartonNoTnt(string trackingNo, string cartonNo)
        {
            string sql = @"        SELECT tss.serial_number
                                      FROM PPSUSER.T_SN_STATUS TSS
                                     WHERE instr(:TrackingNo,substr(TSS.TRACKING_NO,1,9))>0
                                       AND TSS.Carton_No = :CartonNo";
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "TrackingNo", trackingNo };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }

        public DataTable GetSFShiptoCNTYCODE(string shipMentId)
        {
            string sql = string.Format(@"SELECT DISTINCT f.SHIPCNTYCODE FROM ppsuser.T_940_UNICODE f WHERE f.DELIVERYNO IN(
SELECT a.DELIVERY_NO FROM ppsuser.T_ORDER_INFO a WHERE a.SHIPMENT_ID='{0}')
AND (f.SHIPCNTYCODE='HK' OR f.SHIPCNTYCODE='TW')
                    ", shipMentId);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }

        public DataTable getPickPalletInfoByPickPalletNoAndShipmentId(string pickPalletNo, string shipmentid)
        {
            string sql = string.Format(@"  select decode(tsp.pallet_type, '001', 'NO_MIX', 'MIX') as palletType,
                                               tspp.pallet_no as palletNo,
                                               tspp.ictpn ictPn_,
                                               TSPP.QTY totalQty,
                                               TSPP.CARTON_QTY cartonQty,
                                               tspp.pack_qty packQty,
                                               tspp.pack_carton packCarton,
                                               decode(tsp.check_result, 'PASS', '已完成', 'Check中') checkStatus,
                                               tpp.pick_pallet_no,
                                               (select count(distinct tss.carton_no)
                                                  from ppsuser.t_sn_status tss
                                                 where tss.pack_pallet_no = tpp.pallet_no
                                                   and tss.part_no = tspp.ictpn
                                                   and tss.check_time is not null
                                                   and tss.carton_no is not null) +
                                               (select count(*)
                                                  from ppsuser.t_sn_status tss
                                                 where tss.pack_pallet_no = tpp.pallet_no
                                                   and tss.part_no = tspp.ictpn
                                                   and tss.check_time is not null
                                                   and tss.carton_no is null) as alreadyCheckCartonQty,
                                                   (select distinct case
                                                              when a.region = 'EMEIA' and
                                                                   (a.carrier_code like '%DHL%' or
                                                                   a.carrier_name like '%DHL%') and
                                                                   a.service_level = 'WPX' then
                                                               vmi.remark || 'WPX'
                                                              else
                                                               vmi.remark
                                                            end as remark
                                              from ppsuser.t_pallet_pick     tpp,
                                                   ppsuser.vw_mpn_info       vmi,
                                                   ppsuser.t_shipment_pallet tsp, ppsuser.t_shipment_info a
                                             where tpp.ictpn = vmi.ictpartno
                                               and tpp.pallet_no = tsp.pallet_no
                                               and tsp.pack_code = vmi.packcode
                                               and tsp.shipment_id = a.shipment_id
                                               and tpp.pick_pallet_no = '{0}') as remark,
                                                       --DECODE(UPPER(TSP.SECURITY),'BASIC','低','MEDIUM','中','HIGH','高') AS SECURITY
                                                      UPPER(TSP.SECURITY) AS SECURITY,
                                               tsp.empty_carton
                                          from ppsuser.t_shipment_pallet_part tspp,
                                               ppsuser.t_pallet_pick          tpp,
                                               ppsuser.t_shipment_pallet      tsp,
                                               ppsuser.t_shipment_info        tsi
                                         where tspp.pallet_no = tpp.pallet_no
                                           and tspp.ictpn = tpp.ictpn
                                           and tsp.pallet_no = tspp.pallet_no
                                           and tsp.shipment_id = tsi.shipment_id
                                           and tpp.qty <> 0
                                           and tpp.pick_pallet_no = '{0}'
                                           and tsi.shipment_id = '{1}'", pickPalletNo, shipmentid);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }


        public DataTable isPrintLabel(string palletNo)
        {
            //string sql = string.Format(@" select decode(sign(TSP.CARTON_QTY -
            //                                (SELECT COUNT(DISTINCT TSS.CARTON_NO)
            //                                    FROM PPSUSER.T_SN_STATUS TSS
            //                                WHERE TSS.PACK_PALLET_NO = '{0}')),
            //                            0,
            //                            'OK',
            //                            1,
            //                            'ERROR',
            //                            -1,
            //                            'ERROR') AS CHECKRESULT
            //                from ppsuser.t_shipment_pallet tsp
            //            where tsp.pallet_no = '{0}' ", palletNo);
            string sql = string.Format(@" SELECT tsp.*
                                          FROM PPSUSER.T_SHIPMENT_PALLET TSP
                                         WHERE TSP.CHECK_RESULT = 'PASS'
                                           AND TSP.PALLET_NO ='{0}' ", palletNo);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public DataTable totalAllCartonQtyByPickPalletNo(string pickPalletNo)
        {
            string sql = string.Format(@"SELECT T1.CHECKEDCARTONQTY || CHR(47) || TOTALCARTONQTY AS T_STR
                                              FROM (select count(distinct tss.carton_no) as checkedCartonQty
                                                      from ppsuser.t_sn_status tss
                                                     where tss.pack_pallet_no = substr('{0}', 3)
                                                       and tss.check_time is not null) T1,
                                                   (SELECT TSP.CARTON_QTY AS TOTALCARTONQTY
                                                      FROM PPSUSER.T_SHIPMENT_PALLET TSP
                                                     WHERE TSP.PALLET_NO = substr('{0}', 3)) T2 ", pickPalletNo);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public DataTable getT940InfoByDeliveryNo(string cartonNo)
        {
            string sql = @"SELECT DISTINCT T9U.SHIPCNTYCODE, T9U.CUSTOMERGROUP
                              FROM PPSUSER.T_SN_STATUS TSS, PPSUSER.T_940_UNICODE T9U
                             WHERE TSS.DELIVERY_NO = T9U.DELIVERYNO
                               AND TSS.LINE_ITEM = T9U.CUSTDELITEM
                               AND TSS.CARTON_NO = :CartonNo";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable isFinishWorkByCartonNo(string cartonNo)
        {
            string sql = string.Format(@" 
                                       select t1.*, t2.*
                                       from (select count(*) as totalQty
                                               from ppsuser.t_sn_status tss
                                              where tss.pick_pallet_no =
                                                    (select distinct tss.pick_pallet_no
                                                       from ppsuser.t_sn_status tss
                                                      where tss.carton_no = '{0}')) t1,            
                                            (select count(*) as checkStationQty
                                               from ppsuser.t_sn_status tss
                                              where tss.pick_pallet_no =
                                                    (select distinct tss.pick_pallet_no
                                                       from ppsuser.t_sn_status tss
                                                      where tss.carton_no = '{0}')
                                                and tss.check_time is not null) t2
                                      where t1.totalQty = t2.checkStationQty ", cartonNo);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }

        public DataTable isCheckshipmentIdFinishWorkByCartonNo(string cartonNo)
        {
            string sql = string.Format(@" 
                                       select t1.*, t2.*
                                        from (select distinct tsi.qty as totalQty
                                                from ppsuser.t_sn_status tss,ppsuser.t_shipment_info  tsi
                                                where 
                                                tss.shipment_id = tsi.shipment_id
                                                and  tss.shipment_id =
                                                    (select distinct tss.shipment_id
                                                        from ppsuser.t_sn_status tss
                                                        where tss.carton_no = '{0}')) t1,
            
                                            (select count(*) as checkShipmentIsFinishWorkQty
                                                from ppsuser.t_sn_status tss,ppsuser.t_shipment_info  tsi
                                                where 
                                                tss.shipment_id = tsi.shipment_id
                                                and  tss.shipment_id =
                                                    (select distinct tss.shipment_id
                                                        from ppsuser.t_sn_status tss
                                                        where tss.carton_no = '{0}')
                                                and tss.check_time is not null) t2
                                        where t1.totalQty = t2.checkShipmentIsFinishWorkQty", cartonNo);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }

        public DataTable isReprint(string cartonNo)
        {
            string currentStation = "W3";//当前站别
            string sql = string.Format(@" 
                     select decode(sign((select distinct tpci.sequence
                       from ppsuser.t_sn_status    tss,
                            ppsuser.t_process_info tpci
                      where tss.wc = tpci.inwc
                        and tss.carton_no='{0}'
                        AND TSS.WC<>'W0') -
                    (select tpci.sequence
                       from ppsuser.t_process_info tpci
                      where tpci.inwc = '{1}')),
                        0,
                        'OK',
                        1,
                        'OK',
                        'FAIL') as checkResult
            from dual", cartonNo, currentStation);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }

        public DataTable getReprintInfoByCartonNo(string cartonNo)
        {
            string sql = @"   SELECT DISTINCT TSS.SHIPMENT_ID, TSS.PICK_PALLET_NO
                               FROM PPSUSER.T_SN_STATUS TSS
                               WHERE TSS.CARTON_NO = :CartonNo";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }

        public DataTable isCartonNoLinkSSCC(string cartonNo, string sscc)
        {
            string sql = @"   SELECT COUNT(*)  as countR
                              FROM PPSUSER.T_SN_STATUS TSS
                              WHERE TSS.CARTON_NO = :CartonNo
                                AND (TSS.SSCC = :Sscc or tss.tracking_no=:Sscc)";
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Sscc", sscc };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }

        public DataTable getShowResultDGV_Mix(string cartonNo)
        {
            string sql_Mix = string.Format(@"select tss.carton_no,
                   tsp.pallet_no,
                   tss.sscc,
                   decode(tsp.check_result,'PASS','已完成','进行中') as checkResult,
                   tpo.delivery_no
              from ppsuser.t_sn_status       tss,
                   ppsuser.t_shipment_pallet tsp,
                   ppsuser.t_pallet_order    tpo
             where tss.pack_pallet_no = tsp.pallet_no
               and tpo.pallet_no = tss.pack_pallet_no
               and tss.part_no = tpo.ictpn
               and tss.delivery_no = tpo.delivery_no
               and tss.carton_no = '{0}'              
             group by tss.carton_no,
                      tsp.pallet_no,
                      tss.sscc,
                      tsp.check_result,
                      tpo.delivery_no ", cartonNo);

            return ClientUtils.ExecuteSQL(sql_Mix).Tables[0];
        }
        public DataTable getShowResultDGV_No_Mix(string cartonNo)
        {

            string sql_No_Mix = string.Format(@" select tss.carton_no,
                   tsp.pallet_no,
                   tss.sscc,
                   decode(tsp.check_result,'PASS','已完成','进行中') as checkResult,
                   tpo.delivery_no
              from ppsuser.t_sn_status       tss,
                   ppsuser.t_shipment_pallet tsp,
                   ppsuser.t_pallet_order    tpo
             where tss.pack_pallet_no = tsp.pallet_no
               and tpo.pallet_no = tss.pack_pallet_no
               and tss.part_no = tpo.ictpn
               and tss.delivery_no = tpo.delivery_no             
               and tss.pick_pallet_no = (select distinct tss.pick_pallet_no  from  ppsuser.t_sn_status tss where  tss.carton_no = '{0}')
             group by tss.carton_no,
                      tsp.pallet_no,
                      tss.sscc,
                      tsp.check_result,
                      tpo.delivery_no ", cartonNo);
            return ClientUtils.ExecuteSQL(sql_No_Mix).Tables[0];
        }


        public DataTable isJumpPackingListForDN_NO1ByCartonNo(string CartonNo, bool isMix)
        {
            string resultSql = "";
            if (isMix)
            {
                resultSql = @"SELECT TSS.BOX_NO AS BOXNO
                              FROM PPSUSER.T_SN_STATUS TSS
                              WHERE TSS.CARTON_NO ='{0}'";
            }
            else
            {
                resultSql = @" SELECT MIN(TSS1.BOX_NO) AS BOXNO
                               FROM PPSUSER.T_SN_STATUS TSS1, PPSUSER.T_SN_STATUS TSS
                               WHERE TSS1.DELIVERY_NO = TSS.DELIVERY_NO
                               and   tss1.pick_pallet_no = tss.pick_pallet_no
                               AND TSS.CARTON_NO = '{0}'";
            }
            string sql = string.Format(@resultSql, CartonNo);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }

        public DataTable GetPPartDNInfo(string cartonNo)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(@"SELECT b.DN_NO,b.PRINT_CARTON 
FROM PPSUSER.T_MES_PACKINGLIST b INNER JOIN PPSUSER.VW_PERSON_DN_INFO c
ON b.DN_NO = c.DN_NO
WHERE b.DN_NO IN(
SELECT DISTINCT a.DELIVERY_NO FROM ppsuser.T_SN_STATUS a WHERE a.CARTON_NO = :CartonNo)", sqlparams).Tables[0];
        }

        public DataTable GetPPartPrintCarton(string cartonNo)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(@"
SELECT DISTINCT a.CARTON_NO FROM ppsuser.T_SN_STATUS a WHERE a.CARTON_NO=:CartonNo OR a.CUSTOMER_SN=:CartonNo "
                        , sqlparams).Tables[0];
        }

        public DataTable isJumpPackingListForDSConditionByT940UnicodeInfo(string region, string customerGroup, string msgFlag, string gpFlag)
        {
            string sql = @"      SELECT ow.*
                                  FROM pptest.oms_ww ow
                                 where ow.region =:Region
                                   and ow.customergroup = :CustOmerGroup 
                                   and (ow.msgflag = :MsgFlag or ow.msgflag = 'ALL')
                                   and (ow.gpflag = :GpFlag or ow.gpflag = 'ALL')";
            object[][] sqlparams = new object[4][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Region", region };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CustOmerGroup", customerGroup };
            sqlparams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "MsgFlag", msgFlag };
            sqlparams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "GpFlag", gpFlag };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable getT940UnicodeInfoByCartonNo(string cartonNo)
        {
            string sql = @"       SELECT DISTINCT T9U.*
                                  FROM PPSUSER.T_940_UNICODE T9U,
                                       PPSUSER.T_SN_STATUS   TSS
                                 WHERE T9U.DELIVERYNO = TSS.DELIVERY_NO
                                   AND TSS.CARTON_NO =:CartonNo ";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable isPickPalletNoLinkRelationBySSCCAndPickPalletNo(string sscc, string pickPalletNo)
        {
            string sql = string.Format(@" select count(*) as item_
                                          from ppsuser.t_sn_status tss
                                         where (tss.sscc ='{0}' or tss.tracking_no='{0}')
                                           and tss.pick_pallet_no ='{1}' ", sscc, pickPalletNo);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }



        public DataTable isExistShippingLabelForPAC(string pickPalletNo, string shipInfoType)
        {
            try
            {
                string sql = @"    SELECT count(*) as checkCount
                                 FROM PPSUSER.T_940_UNICODE   T9U,
                                      pptest.oms_lmd          ol,
                                      pptest.oms_lmd_overview olo,
                                      ppsuser.t_sn_status tss
                                where  t9u.deliverytype  = ol.dntype
                                  and  t9u.shipcntycode  = ol.country
                                  and  t9u.saleorgcode = ol.salesorg
                                  and  ol.sccode  = olo.sccode
                                  and t9u.deliveryno = tss.delivery_no
                                  and t9u.custdelitem = tss.line_item
                                  and olo.lmdmode = :ShipInfoType
                                  and olo.document = 'ShippingLabel'
                                  and olo.createlmd = 'Y'
                                  and  t9u.region = 'PAC'
                                  and tss.pick_pallet_no = :PickPalletNo";
                object[][] sqlparams = new object[2][];
                sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PickPalletNo", pickPalletNo };
                sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipInfoType", shipInfoType };
                return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable isJumpDeliveryNoteTB(string cartonNo, string shipInfoType)
        {
            string sql = @"    SELECT count(*)  checkCount
                                 FROM PPSUSER.T_940_UNICODE   T9U,
                                      pptest.oms_lmd          ol,
                                      pptest.oms_lmd_overview olo,
                                      ppsuser.t_sn_status tss
                                where  t9u.deliverytype  = ol.dntype
                                  and  t9u.shipcntycode  = ol.country
                                  and  t9u.saleorgcode  = ol.salesorg
                                  and  ol.sccode  = olo.sccode
                                  and t9u.deliveryno = tss.delivery_no
                                  and t9u.custdelitem = tss.line_item
                                  and olo.lmdmode = :ShipInfoType
                                  and olo.document = 'DeliveryNote'
                                  and olo.item = 'Shipper'
                                  and olo.createlmd = 'Y'
                                  and  t9u.region  = 'PAC'
                                  and tss.carton_no = :CartonNo";
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipInfoType", shipInfoType };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable GetPACGS1003MultiLineBySQL(string strSID, string strPallet)
        {
            string sql = string.Format(@"select b.shipment_id,
                                               b.pallet_no,
                                               a.gs1flag,
                                               count(distinct a.deliveryno || a.custdelitem) dnlineqty
                                          from ppsuser.t_940_unicode a
                                          join ppsuser.t_pallet_order b
                                            on a.deliveryno = b.delivery_no
                                           and a.custdelitem = b.line_item
                                          join ppsuser.t_shipment_info c
                                            on b.shipment_id = c.shipment_id
                                         where a.gs1flag = '003'
                                           and c.region = 'PAC'
                                           and b.shipment_id = '{0}'
                                           and b.pallet_no = '{1}'
                                         group by b.shipment_id, b.pallet_no, a.gs1flag
                                        having count(distinct a.deliveryno || a.custdelitem) > 1
                    ", strSID, strPallet);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
    }
}
