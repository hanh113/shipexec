using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace CheckAC.Dao
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
                                            tsi.carrier_code 
                                        from NONEDIPPS.t_shipment_info tsi
                                        where tsi.shipment_id = '{0}'", shipMentId);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public DataTable fd_checkIsLinkPallletNo(string trackingNo, string pickPalletNo)
        {
            string sql = @"     select tss.serial_number
                                                from NONEDIPPS.t_sn_status tss
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
                                      FROM NONEDIPPS.T_SN_STATUS TSS
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
                                      FROM NONEDIPPS.T_SN_STATUS TSS
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
                                      FROM NONEDIPPS.T_SN_STATUS TSS
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
                                                from NONEDIPPS.t_sn_status tss
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
                                      FROM NONEDIPPS.T_SN_STATUS TSS
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
                                      FROM NONEDIPPS.T_SN_STATUS TSS
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
                                      FROM NONEDIPPS.T_SN_STATUS TSS
                                     WHERE instr(:TrackingNo,substr(TSS.TRACKING_NO,1,9))>0
                                       AND TSS.Carton_No = :CartonNo";
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "TrackingNo", trackingNo };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
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
                                                  from NONEDIPPS.t_sn_status tss
                                                 where tss.pack_pallet_no = tpp.pallet_no
                                                   and tss.part_no = tspp.ictpn
                                                   and tss.check_time is not null
                                                   and tss.carton_no is not null) +
                                               (select count(*)
                                                  from NONEDIPPS.t_sn_status tss
                                                 where tss.pack_pallet_no = tpp.pallet_no
                                                   and tss.part_no = tspp.ictpn
                                                   and tss.check_time is not null
                                                   and tss.carton_no is null) as alreadyCheckCartonQty,
                                                   (SELECT DISTINCT VMI.REMARK
                                                      FROM NONEDIPPS.t_pallet_pick     tpp,
                                                           NONEDIPPS.VW_MPN_INFO       VMI,
                                                           NONEDIPPS.T_SHIPMENT_PALLET TSP
                                                     WHERE TPP.ICTPN = VMI.ICTPARTNO
                                                       AND TPP.PALLET_NO = TSP.PALLET_NO
                                                       AND TSP.PACK_CODE = VMI.PACKCODE
                                                       AND TPP.PICK_PALLET_NO = '{0}') as remark,
                                                       DECODE(UPPER(TSP.SECURITY),'BASIC','低','MEDIUM','中','HIGH','高') AS SECURITY
                                          from NONEDIPPS.t_shipment_pallet_part tspp,
                                               NONEDIPPS.t_pallet_pick          tpp,
                                               NONEDIPPS.t_shipment_pallet      tsp,
                                               NONEDIPPS.t_shipment_info        tsi
                                         where tspp.pallet_no = tpp.pallet_no
                                           and tspp.ictpn = tpp.ictpn
                                           and tsp.pallet_no = tspp.pallet_no
                                           and tsp.shipment_id = tsi.shipment_id
                                           and tpp.qty <> 0
                                           and tpp.pick_pallet_no = '{0}'
                                           and tsi.shipment_id = '{1}'", pickPalletNo, shipmentid);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }

        public DataTable GetFinishiInfo(string pickPalletNo)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "pickPalletNo", pickPalletNo };
            return ClientUtils.ExecuteSQL(@"SELECT A.PALLET_NO 
FROM NONEDIPPS.T_SHIPMENT_PALLET a WHERE a.CARTON_QTY>a.PICK_CARTON
AND a.PALLET_NO=substr(:pickPalletNo,3) ", sqlparams).Tables[0];
        }

        public DataTable isPrintLabel(string palletNo)
        {
            //string sql = string.Format(@" select decode(sign(TSP.CARTON_QTY -
            //                                (SELECT COUNT(DISTINCT TSS.CARTON_NO)
            //                                    FROM NONEDIPPS.T_SN_STATUS TSS
            //                                WHERE TSS.PACK_PALLET_NO = '{0}')),
            //                            0,
            //                            'OK',
            //                            1,
            //                            'ERROR',
            //                            -1,
            //                            'ERROR') AS CHECKRESULT
            //                from NONEDIPPS.t_shipment_pallet tsp
            //            where tsp.pallet_no = '{0}' ", palletNo);
            string sql = string.Format(@" SELECT tsp.*
                                          FROM NONEDIPPS.T_SHIPMENT_PALLET TSP
                                         WHERE TSP.CHECK_RESULT = 'PASS'
                                           AND TSP.PALLET_NO ='{0}' ", palletNo);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public DataTable totalAllCartonQtyByPickPalletNo(string pickPalletNo)
        {
            string sql = string.Format(@"SELECT T1.CHECKEDCARTONQTY || CHR(47) || TOTALCARTONQTY AS T_STR
                                              FROM (select count(distinct tss.carton_no) as checkedCartonQty
                                                      from NONEDIPPS.t_sn_status tss
                                                     where tss.pack_pallet_no = substr('{0}', 3)
                                                       and tss.check_time is not null) T1,
                                                   (SELECT TSP.CARTON_QTY AS TOTALCARTONQTY
                                                      FROM NONEDIPPS.T_SHIPMENT_PALLET TSP
                                                     WHERE TSP.PALLET_NO = substr('{0}', 3)) T2 ", pickPalletNo);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public DataTable getT940InfoByDeliveryNo(string cartonNo)
        {
            string sql = @"SELECT DISTINCT T9U.SHIPCNTYCODE, T9U.CUSTOMERGROUP
                              FROM NONEDIPPS.T_SN_STATUS TSS, NONEDIPPS.T_940_UNICODE T9U
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
                                               from NONEDIPPS.t_sn_status tss
                                              where tss.pick_pallet_no =
                                                    (select distinct tss.pick_pallet_no
                                                       from NONEDIPPS.t_sn_status tss
                                                      where tss.carton_no = '{0}')) t1,            
                                            (select count(*) as checkStationQty
                                               from NONEDIPPS.t_sn_status tss
                                              where tss.pick_pallet_no =
                                                    (select distinct tss.pick_pallet_no
                                                       from NONEDIPPS.t_sn_status tss
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
                                                from NONEDIPPS.t_sn_status tss,NONEDIPPS.t_shipment_info  tsi
                                                where 
                                                tss.shipment_id = tsi.shipment_id
                                                and  tss.shipment_id =
                                                    (select distinct tss.shipment_id
                                                        from NONEDIPPS.t_sn_status tss
                                                        where tss.carton_no = '{0}')) t1,
            
                                            (select count(*) as checkShipmentIsFinishWorkQty
                                                from NONEDIPPS.t_sn_status tss,NONEDIPPS.t_shipment_info  tsi
                                                where 
                                                tss.shipment_id = tsi.shipment_id
                                                and  tss.shipment_id =
                                                    (select distinct tss.shipment_id
                                                        from NONEDIPPS.t_sn_status tss
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
                       from NONEDIPPS.t_sn_status    tss,
                            NONEDIPPS.t_process_info tpci
                      where tss.wc = tpci.inwc
                        and tss.carton_no='{0}'
                        AND TSS.WC<>'W0') -
                    (select tpci.sequence
                       from NONEDIPPS.t_process_info tpci
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
            string sql = @"   SELECT DISTINCT TSS.SHIPMENT_ID, TSS.PICK_PALLET_NO,TSS.CHECK_TIME
                               FROM NONEDIPPS.T_SN_STATUS TSS
                               WHERE TSS.CARTON_NO = :CartonNo";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }

        public DataTable isCartonNoLinkSSCC(string cartonNo, string sscc)
        {
            string sql = @"   SELECT COUNT(*)  as countR
                              FROM NONEDIPPS.T_SN_STATUS TSS
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
              from NONEDIPPS.t_sn_status       tss,
                   NONEDIPPS.t_shipment_pallet tsp,
                   NONEDIPPS.t_pallet_order    tpo
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
              from NONEDIPPS.t_sn_status       tss,
                   NONEDIPPS.t_shipment_pallet tsp,
                   NONEDIPPS.t_pallet_order    tpo
             where tss.pack_pallet_no = tsp.pallet_no
               and tpo.pallet_no = tss.pack_pallet_no
               and tss.part_no = tpo.ictpn
               and tss.delivery_no = tpo.delivery_no             
               and tss.pick_pallet_no = (select distinct tss.pick_pallet_no  from  NONEDIPPS.t_sn_status tss where  tss.carton_no = '{0}')
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
                              FROM NONEDIPPS.T_SN_STATUS TSS
                              WHERE TSS.CARTON_NO ='{0}'";
            }
            else
            {
                resultSql = @" SELECT MIN(TSS1.BOX_NO) AS BOXNO
                               FROM NONEDIPPS.T_SN_STATUS TSS1, NONEDIPPS.T_SN_STATUS TSS
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
FROM NONEDIPPS.T_MES_PACKINGLIST b INNER JOIN NONEDIPPS.VW_PERSON_DN_INFO c
ON b.DN_NO = c.DN_NO
WHERE b.DN_NO IN(
SELECT DISTINCT a.DELIVERY_NO FROM NONEDIPPS.T_SN_STATUS a WHERE a.CARTON_NO = :CartonNo)", sqlparams).Tables[0];
        }

        public DataTable GetPPartPrintCarton(string cartonNo)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(@"
SELECT DISTINCT a.CARTON_NO FROM NONEDIPPS.T_SN_STATUS a WHERE a.CARTON_NO=:CartonNo OR a.CUSTOMER_SN=:CartonNo "
                        , sqlparams).Tables[0];
        }

        public DataTable isJumpPackingListForDSConditionByT940UnicodeInfo(string region, string customerGroup, string msgFlag, string gpFlag)
        {
            string sql = @"      SELECT ow.*
                                  FROM NONEDIOMS.oms_ww ow
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
                                  FROM NONEDIPPS.T_940_UNICODE T9U,
                                       NONEDIPPS.T_SN_STATUS   TSS
                                 WHERE T9U.DELIVERYNO = TSS.DELIVERY_NO
                                   AND TSS.CARTON_NO =:CartonNo ";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable isPickPalletNoLinkRelationBySSCCAndPickPalletNo(string sscc, string pickPalletNo)
        {
            string sql = string.Format(@" select count(*) as item_
                                          from NONEDIPPS.t_sn_status tss
                                         where (tss.sscc ='{0}' or tss.tracking_no='{0}')
                                           and tss.pick_pallet_no ='{1}' ", sscc, pickPalletNo);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }



        public DataTable isExistShippingLabelForPAC(string pickPalletNo, string shipInfoType)
        {
            try
            {
                string sql = @"    SELECT count(*) as checkCount
                                 FROM NONEDIPPS.T_940_UNICODE   T9U,
                                      NONEDIOMS.oms_lmd          ol,
                                      NONEDIOMS.oms_lmd_overview olo,
                                      NONEDIPPS.t_sn_status tss
                                where NONEDIPPS.t_newtrim_function(t9u.deliverytype) = ol.dntype
                                  and NONEDIPPS.t_newtrim_function(t9u.shipcntycode) = ol.country
                                  and NONEDIPPS.t_newtrim_function(t9u.saleorgcode) = ol.salesorg
                                  and NONEDIPPS.t_newtrim_function(ol.sccode) = olo.sccode
                                  and t9u.deliveryno = tss.delivery_no
                                  and t9u.custdelitem = tss.line_item
                                  and olo.lmdmode = :ShipInfoType
                                  and olo.document = 'ShippingLabel'
                                  and olo.createlmd = 'Y'
                                  and NONEDIPPS.t_newtrim_function(t9u.region) = 'PAC'
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
                                 FROM NONEDIPPS.T_940_UNICODE   T9U,
                                      NONEDIOMS.oms_lmd          ol,
                                      NONEDIOMS.oms_lmd_overview olo,
                                      NONEDIPPS.t_sn_status tss
                                where NONEDIPPS.t_newtrim_function(t9u.deliverytype) = ol.dntype
                                  and NONEDIPPS.t_newtrim_function(t9u.shipcntycode) = ol.country
                                  and NONEDIPPS.t_newtrim_function(t9u.saleorgcode) = ol.salesorg
                                  and NONEDIPPS.t_newtrim_function(ol.sccode) = olo.sccode
                                  and t9u.deliveryno = tss.delivery_no
                                  and t9u.custdelitem = tss.line_item
                                  and olo.lmdmode = :ShipInfoType
                                  and olo.document = 'DeliveryNote'
                                  and olo.item = 'Shipper'
                                  and olo.createlmd = 'Y'
                                  and NONEDIPPS.t_newtrim_function(t9u.region) = 'PAC'
                                  and tss.carton_no = :CartonNo";
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipInfoType", shipInfoType };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }

        public void AddStationTimeLogSQL(string workStation, string workType, DateTime dtStart, DateTime dtEnd, double seconds)
        {
            object[][] sqlparams = new object[5][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "WORK_STATION", workStation };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "SCANTYPE_DESC", workType };
            sqlparams[2] = new object[] { ParameterDirection.Input, OracleDbType.Date, "START_TIME", dtStart };
            sqlparams[3] = new object[] { ParameterDirection.Input, OracleDbType.Date, "END_TIME", dtEnd };
            sqlparams[4] = new object[] { ParameterDirection.Input, OracleDbType.Int32, "USE_SECOND", seconds };
            object[] objResult = ClientUtils.ExecuteObjectSQL(@"INSERT INTO NONEDIPPS.T_PRINTTIME_LOG(WORK_STATION,SCANTYPE_DESC,START_TIME,END_TIME,USE_SECOND) VALUES(:WORK_STATION,:SCANTYPE_DESC,:START_TIME,:END_TIME,:USE_SECOND)", sqlparams);
            if (objResult[0].ToString().Trim() != "0")
            {
                throw new Exception(objResult[1].ToString().Trim());
            }
        }

        public DataTable GetAddressInfo(string strCarton)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CARTON_NO", strCarton };
//            return ClientUtils.ExecuteSQL(@"SELECT DISTINCT c.SHIPTONAME1,c.SHIPTOADDRESS1,c.SHIPTOCITY,c.SHIPTOSTATE,c.SHIPTOZIP,c.SHIPTOCOUNTRY,
//(SELECT x.SHIPPERNAME FROM NONEDIPPS.T_SHIPPER x WHERE rownum<=1) AS  SUPPLIER_NAME1,
//(SELECT y.SHIPPERADDRESS1 || ', ' || y.SHIPPERADDRESS2 || ', ' || y.SHIPPERCITY || ', ' || y.SHIPPERCOUNTRY FROM NONEDIPPS.T_SHIPPER y WHERE rownum<=1) AS  SUPPLIER_NAME2,
//(SELECT z.SHIPPERCOUNTRY FROM NONEDIPPS.T_SHIPPER z WHERE rownum<=1) AS  SUPPLIER_NAME3
//FROM NONEDIPPS.T_SN_STATUS a INNER JOIN NONEDIPPS.T_ORDER_INFO b ON a.SHIPMENT_ID=b.SHIPMENT_ID
//AND a.PART_NO=b.ICTPN AND a.DELIVERY_NO=b.DELIVERY_NO AND a.LINE_ITEM=b.LINE_ITEM
//INNER JOIN NONEDIPPS.T_FD_ORDER_DETAIL c ON b.DELIVERY_NO=c.AC_PO AND b.LINE_ITEM=c.AC_PO_LINE AND b.FREIGHTORDER=c.FREIGHTORDER
//WHERE a.CARTON_NO=:CARTON_NO ", sqlparams).Tables[0];

            return ClientUtils.ExecuteSQL(@"SELECT DISTINCT c.SHIPTONAME1,c.SHIPTOADDRESS1,c.SHIPTOCITY,c.SHIPTOSTATE,c.SHIPTOZIP,c.SHIPTOCOUNTRY,
(SELECT x.SHIPPERNAME FROM NONEDIPPS.T_SHIPPER x WHERE rownum<=1) AS  SUPPLIER_NAME1,
(SELECT y.SHIPPERADDRESS1 || ', ' || y.SHIPPERADDRESS2 || ', ' || y.SHIPPERCITY || ', ' || y.SHIPPERCOUNTRY FROM NONEDIPPS.T_SHIPPER y WHERE rownum<=1) AS  SUPPLIER_NAME2,
(SELECT z.SHIPPERCOUNTRY FROM NONEDIPPS.T_SHIPPER z WHERE rownum<=1) AS  SUPPLIER_NAME3
FROM NONEDIPPS.T_SN_STATUS a INNER JOIN NONEDIPPS.T_ORDER_INFO b ON a.SHIPMENT_ID=b.SHIPMENT_ID
AND a.PART_NO=b.ICTPN AND a.DELIVERY_NO=b.DELIVERY_NO AND a.LINE_ITEM=b.LINE_ITEM
INNER JOIN nonedioms.vwtshiporder c ON b.DELIVERY_NO=c.AC_PO AND b.LINE_ITEM=c.AC_PO_LINE
WHERE a.CARTON_NO=:CARTON_NO ", sqlparams).Tables[0];

        }

        public DataTable GetModelType(string strCarton)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CARTON_NO", strCarton };
            return ClientUtils.ExecuteSQL(@"SELECT DISTINCT A.CUSTMODEL 
FROM NONEDIOMS.OMS_PARTMAPPING A WHERE A.PART IN
(SELECT DISTINCT B.PART_NO FROM NONEDIPPS.T_SN_STATUS B WHERE B.CARTON_NO=:CARTON_NO)
 ", sqlparams).Tables[0];
        }

        public DataTable GetBasicInfoForPrint(string basicKey)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PARA_TYPE", basicKey };
            return ClientUtils.ExecuteSQL(@"SELECT count(*) from ppsuser.t_basicparameter_info where ENABLED = 'Y' and PARA_TYPE=:PARA_TYPE", sqlparams).Tables[0];
        }
    }
}
