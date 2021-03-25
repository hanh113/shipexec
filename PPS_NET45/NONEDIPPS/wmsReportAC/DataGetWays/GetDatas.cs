using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;
namespace wmsReportAC.DataGetWays
{
    class GetDatas
    {
        public DataTable queryShipmentInfoByShipmentId(string shipmentId, bool isQueryQAHold)
        {
            //当订单（shipmentiD）下来的时候，就已经确定装在哪辆车上了
            string addQAHold = " AND GSS.HOLD_FLAG = 'Y'";
            if (!isQueryQAHold)
            {
                addQAHold = "";
            }
            string sql = @"     SELECT DISTINCT TSI.SHIPMENT_ID AS 集货单号,
                                    to_char(tsi.shipping_time,'yyyy-MM-dd') as 出货时间,
                                    TSI.SHIPMENT_TYPE as 出货类型,
                                    TSI.REGION AS 国别,
                                    tsi.qty as 总数量,
                                    tsi.carton_qty as 总箱数,
                                    OLC.CAR_NO AS 车牌号,
                                    DECODE(OLC.ISLOAD, '1', '已装车', '未装车') AS 是否装车,
                                    tss.carton_no as 箱号,
                                        DECODE(     TSS.WC,
                                                    'W0',
                                                    '库位中',
                                                    'W1',
                                                    '已PICK',
                                                    'W2',
                                                    'PACK',
                                                    'W3',
                                                    'CHECK',
                                                    'W4',
                                                    'WEIGHT',
                                                    'W5',
                                                    'SHIPMENT',
                                                    'W6',
                                                    'UPLOAD 856',
                                                    '站别不明') as 站别
                        FROM NONEDIPPS.T_SN_STATUS     TSS,
                            NONEDIPPS.T_SHIPMENT_INFO TSI,
                            NONEDIOMS.OMS_LOAD_CAR     OLC,
                            NONEDIPPS.G_SN_STATUS  GSS
                        WHERE TSS.SHIPMENT_ID = TSI.SHIPMENT_ID
                        AND OLC.SHIPMENT_ID = TSS.SHIPMENT_ID
                        AND GSS.SERIAL_NUMBER = TSS.SERIAL_NUMBER " + addQAHold +
                    @"                     
                        AND TSI.SHIPMENT_ID = :ShipmentId
                        order by  tss.carton_no";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable queryCartonInfoByCartonNo(string cartonNo)
        {
            string sql = @"    SELECT tss.carton_no,
                               TSS.SERIAL_NUMBER as 流水号,
                               TSS.PART_NO as 料号,
                               DECODE(TSS.WC,
                                      'W0',
                                      '库位中',
                                      'W1',
                                      '已PICK',
                                      'W2',
                                      'PACK',
                                      'W3',
                                      'CHECK',
                                      'W4',
                                      'WEIGHT',
                                      'W5',
                                      'SHIPMENT',
                                      'W6',
                                      'UPLOAD 856',
                                      'ZF',
                                      '杂项出货'
                                      '站别不明') as 站别,
                                      decode(gss.hold_flag,'Y','YES','N','NO','') as Hold状态,
                               tss.pick_pallet_no as Pick栈板号,
                               tss.pack_pallet_no as 栈板号,
                               tss.shipment_id as 集货单号,
                               tss.delivery_no as DN号,
                               tss.line_item as lineItem,
                               tss.sscc
                          FROM NONEDIPPS.T_SN_STATUS TSS,NONEDIPPS.g_sn_status  gss
                         WHERE 
                         tss.serial_number = gss.serial_number
                         and TSS.CARTON_NO = :CartonNo
                         order by tss.serial_number";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable queryPalletNoInfoByPackPalletNo(string packPalletNo)
        {
            string sql = @"    SELECT distinct TSS.PALLET_NO AS 栈板号,
                                     TSS.CARTON_NO as 箱号,
                                     DECODE(TSS.WC,
                                            'W0',
                                            '库位中',
                                            'W1',
                                            '已PICK',
                                            'W2',
                                            'PACK',
                                            'W3',
                                            'CHECK',
                                            'W4',
                                            'WEIGHT',
                                            'W5',
                                            'SHIPMENT',
                                            'W6',
                                            'UPLOAD 856',
                                            '站别不明') as 站别,
                                     tss.shipment_id as 集货单号,
                                     TSS.DELIVERY_NO AS DN号,
                                     TSS.LINE_ITEM AS LineItem,
                                     tss.part_no as 料号,
                                     tss.sscc,
                                     tss.pick_pallet_no as Pick栈板号
                       FROM NONEDIPPS.T_SN_STATUS TSS
                      WHERE TSS.PACK_PALLET_NO = :PackPalletNo
                      order by tss.carton_no";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PackPalletNo", packPalletNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }

        public DataTable queryShipmentIdInfoByShipmentTime(string startTime, string endTime, string shipMentId, string shipment_type, string shipping_region)
        {
            int countIndex = 0;
            object[][] sqlparams = new object[countIndex][];
            bool isInput = false;
            string addSql = @"";
            if (!(string.IsNullOrEmpty(startTime) && string.IsNullOrEmpty(endTime)))
            {
                countIndex += 2;
                Array.Resize(ref sqlparams, sqlparams.Length + 2);
                addSql += @"AND tsi.shipping_time between
                               to_date(:StartTime,
                                       'yyyy-mm-dd hh24:mi:ss') and
                               to_date(:EndTime,
                                       'yyyy-mm-dd hh24:mi:ss') ";
                sqlparams[countIndex - 2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "StartTime", startTime };
                sqlparams[countIndex - 1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "EndTime", endTime };
                isInput = true;
            }
            if (!(string.IsNullOrEmpty(shipMentId)))
            {
                addSql += @" AND TSI.SHIPMENT_ID=:ShipMentId ";
                countIndex += 1;
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                sqlparams[countIndex - 1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipMentId", shipMentId };
                isInput = true;
            }
            if (!(string.IsNullOrEmpty(shipment_type)))
            {
                addSql += @" AND  TSI.SHIPMENT_TYPE = :Shipment_type ";
                countIndex += 1;
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                sqlparams[countIndex - 1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Shipment_type", shipment_type };
                isInput = true;
            }
            if (!(string.IsNullOrEmpty(shipping_region)))
            {
                addSql += @" AND  TSI.REGION = :Shipping_region ";
                countIndex += 1;
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                sqlparams[countIndex - 1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Shipping_region", shipping_region };
                isInput = true;
            }
            string sql = @"     SELECT  tsi.shipment_id as 集货单号,
                                        tsi.shipment_type as 出货方式,
                                        tsi.region as 国家,
                                        tsi.carrier_name as 货代,
                                        tsi.poe as 港口,
                                        tsi.type,
                                        tsi.qty as 总数量,
                                        tsi.carton_qty as 总箱数,
                                        tsi.pack_qty as 已Pack数量,
                                        tsi.pack_carton_qty as 已Pack箱数,
                                        to_char(tsi.shipping_time, 'yyyy-MM-dd') as 出货时间,
                                        DECODE(QA_TABLE.TOTALCARTON,
                                               NULL,
                                               '0',
                                               '',
                                               '0'," +
                                               "QA_TABLE.TOTALCARTON) || '/' || TSI.CARTON_QTY AS \"QH箱数 / 总箱数\" " +
                                   @"FROM NONEDIPPS.T_SHIPMENT_INFO TSI
                                   LEFT JOIN (SELECT TPO.SHIPMENT_ID, COUNT(DISTINCT TSS.CARTON_NO) AS TOTALCARTON
                                              FROM NONEDIPPS.T_SN_STATUS TSS, NONEDIPPS.T_PALLET_ORDER TPO
                                             WHERE substr(TSS.PICK_PALLET_NO, 3) = TPO.PALLET_NO
                                               AND TO_CHAR(TSS.SERIAL_NUMBER) IN
                                                   (SELECT TO_CHAR(HSS.SERIAL_NUMBER)
                                                      FROM sajet.g_SN_STATUS HSS
                                                     where hss.hold_flag = 'Y'
                                                       and TO_CHAR(hss.SERIAL_NUMBER) in
                                                           (select TO_CHAR(A.SERIAL_NUMBER)
                                                              from NONEDIPPS.t_sn_status a
                                                             where a.wc = 'W0'))
                                             GROUP BY TPO.SHIPMENT_ID) QA_TABLE
                                     ON TSI.SHIPMENT_ID = QA_TABLE.SHIPMENT_ID
                                  WHERE 1 = 1" +
                                  addSql
                                  + "ORDER BY tsi.shipping_time DESC";
            if (isInput)
            {
                return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
            }
            else
            {
                return ClientUtils.ExecuteSQL(sql).Tables[0];
            }
        }

        public DataTable getTTOrderInfoByConditions(string tt_order,string ictPn,string start_time,string end_time)
        {
            int countIndex = 0;
            object[][] sqlparams = new object[countIndex][];
            bool isInput = false;
            string addSql = @"";
            if (!(string.IsNullOrEmpty(tt_order)))
            {
                addSql += @" AND    THM.TT_NO =:Tt_order ";
                countIndex += 1;
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                sqlparams[countIndex - 1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Tt_order", tt_order };
                isInput = true;
            }
            if (!(string.IsNullOrEmpty(ictPn)))
            {
                addSql += @" AND    THM.ICTPN  = :IctPn ";
                countIndex += 1;
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                sqlparams[countIndex - 1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "IctPn", ictPn };
                isInput = true;
            }
            if (!(string.IsNullOrEmpty(start_time) && string.IsNullOrEmpty(end_time)))
            {
                addSql += @" AND    THM.CDT >= TO_DATE(:Start_time,'yyyyMMdd')
                             AND    THM.CDT <= TO_DATE(:End_time,'yyyyMMdd')";
                countIndex += 2;
                Array.Resize(ref sqlparams, sqlparams.Length + 2);
                sqlparams[countIndex - 1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Start_time", start_time };
                sqlparams[countIndex - 2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "End_time", end_time };
                isInput = true;
            }
            string sql = @"     SELECT THM.ID as 序号,
                                       THM.TT_NO as TT_单号,
                                       THM.ICTPN as 料号,
                                       THM.TOTAL_QTY as 总量,
                                       THM.OUT_QTY as 已出数量,
                                       THM.REMARK as 备注,
                                       to_char(THM.CDT, 'YYYY-MM-DD') AS 创建时间,
                                       THM.CREATE_EMP_NO AS 创建者
                                  FROM NONEDIPPS.T_OTHERS_M THM
                                  WHERE  1 = 1" +
                                  addSql
                                  + " ORDER BY THM.CDT DESC";
            if (isInput)
            {
                return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
            }
            else
            {
                return ClientUtils.ExecuteSQL(sql).Tables[0];
            }
        }

        public DataTable queryMoreInfoByMultiOption(string Mes_pallet, string pps_pallet, string pick_pallet, string deliveryNo, string locationNo, string ictPn, string cartonNo, string ssccCode, string shipmentId)
        {
            int countIndex = 0;
            object[][] sqlparams = new object[countIndex][];
            bool isInput = false;
            string addSql = @"";
            if (!(string.IsNullOrEmpty(shipmentId)))
            {
                countIndex += 1;
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                addSql += @"AND tss.shipment_id = :ShipmentId";
                sqlparams[countIndex - 1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
                isInput = true;
            }
            if (!(string.IsNullOrEmpty(Mes_pallet)))
            {
                countIndex += 1;
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                addSql += @"AND TSS.PALLET_NO = :Mes_pallet";
                sqlparams[countIndex - 1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Mes_pallet", Mes_pallet };
                isInput = true;
            }
            if (!(string.IsNullOrEmpty(pps_pallet)))
            {
                addSql += @" AND  TSS.PACK_PALLET_NO= :Pps_pallet";
                countIndex += 1;
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                sqlparams[countIndex - 1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Pps_pallet", pps_pallet };
                isInput = true;
            }
            if (!(string.IsNullOrEmpty(pick_pallet)))
            {
                addSql += @" AND  TSS.PICK_PALLET_NO = :Pick_pallet ";
                countIndex += 1;
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                sqlparams[countIndex - 1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Pick_pallet", pick_pallet };
                isInput = true;
            }
            if (!(string.IsNullOrEmpty(deliveryNo)))
            {
                addSql += @" AND  TSS.DELIVERY_NO=:DeliveryNo ";
                countIndex += 1;
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                sqlparams[countIndex - 1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "DeliveryNo", deliveryNo };
                isInput = true;
            }

            if (!(string.IsNullOrEmpty(locationNo)))
            {
                addSql += @" AND  TSS.LOCATION_NO=:LocationNo 
                             AND  TSS.WC <> 'W0'";
                countIndex += 1;
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                sqlparams[countIndex - 1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "LocationNo", locationNo };
                isInput = true;
            }
            if (!(string.IsNullOrEmpty(ictPn)))
            {
                addSql += @" AND  TSS.PART_NO=:IctPn ";
                countIndex += 1;
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                sqlparams[countIndex - 1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "IctPn", ictPn };
                isInput = true;
            }
            if (!(string.IsNullOrEmpty(cartonNo)))
            {
                addSql += @" AND  TSS.CARTON_NO=:CartonNo ";
                countIndex += 1;
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                sqlparams[countIndex - 1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
                isInput = true;
            }
            if (!(string.IsNullOrEmpty(ssccCode)))
            {
                addSql += @" AND  TSS.SSCC=: SsccCode";
                countIndex += 1;
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                sqlparams[countIndex - 1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "SsccCode", ssccCode };
                isInput = true;
            }
            if (string.IsNullOrEmpty(Mes_pallet) &&
                string.IsNullOrEmpty(pick_pallet) &&
                string.IsNullOrEmpty(pps_pallet) &&
                string.IsNullOrEmpty(deliveryNo) &&
                string.IsNullOrEmpty(locationNo) &&
                string.IsNullOrEmpty(cartonNo) &&
                string.IsNullOrEmpty(ssccCode) &&
                string.IsNullOrEmpty(ictPn))
            {
                addSql += @" AND  ROWNUM <=5000";
            }
            string sql = @"      SELECT distinct   TSS.PALLET_NO AS MES_栈板号,
                                                   tss.location_no  as  储位号,
                                                   TSS.PACK_PALLET_NO AS PPS_栈板号,
                                     tss.pick_pallet_no as Pick栈板号,                                   
                                     TSS.CARTON_NO as 箱号,
                                     TSS.BOX_NO as 序号,
                                     DECODE(TSS.WC,
                                            'W0',
                                            '库位中',
                                            'W1',
                                            '已PICK',
                                            'W2',
                                            'PACK',
                                            'W3',
                                            'CHECK',
                                            'W4',
                                            'WEIGHT',
                                            'W5',
                                            'SHIPMENT',
                                            'W6',
                                            'UPLOAD 856',
                                            'ZF',
                                            '杂项出货',
                                            '站别不明') as 站别,
                                     tss.shipment_id as 集货单号,
                                     TSS.DELIVERY_NO AS DN号,
                                     TSS.LINE_ITEM AS LineItem,
                                     tss.part_no as 料号,
                                     tss.sscc
                       FROM NONEDIPPS.T_SN_STATUS TSS
                      WHERE 
                      1=1" + addSql + @" order by tss.carton_no";
            if (isInput)
            {
                return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
            }
            else
            {
                return ClientUtils.ExecuteSQL(sql).Tables[0];
            }
        }


        public DataTable queryQAholdCartonsByShipmentId(string shipmentId)
        {
            //string sql = @"     SELECT DISTINCT TSS.CARTON_NO AS CartonNo,
            //                    TSS.PART_NO as PartNO,
            //                    substr(tss.pick_pallet_no, 3) as PalletNo
            //                  FROM NONEDIPPS.T_SN_STATUS TSS, NONEDIPPS.T_PALLET_ORDER TPO
            //                 WHERE SUBSTR(TSS.PICK_PALLET_NO, 3) = TPO.PALLET_NO
            //                   AND TSS.SERIAL_NUMBER IN
            //                       (SELECT HSS.SERIAL_NUMBER
            //                          FROM sajet.g_SN_STATUS HSS
            //                         WHERE HSS.HOLD_FLAG = 'Y')
            //                   AND TPO.SHIPMENT_ID = :ShipmentId
            //                 order by PalletNo ";
            string sql = @"  SELECT DISTINCT TSS.CARTON_NO AS CartonNo,
                                 TSS.PART_NO as PartNO,
                                 substr(tss.pick_pallet_no, 3) as PalletNo
                               FROM NONEDIPPS.T_SN_STATUS TSS, NONEDIPPS.T_PALLET_ORDER TPO
                              WHERE SUBSTR(TSS.PICK_PALLET_NO, 3) = TPO.PALLET_NO
                                AND TSS.SERIAL_NUMBER IN
                                    (SELECT HSS.SERIAL_NUMBER
                                       FROM sajet.g_SN_STATUS HSS
                                      WHERE HSS.HOLD_FLAG = 'Y'
                                        and TO_CHAR(hss.serial_number) in
                                            ((select TO_CHAR(A.SERIAL_NUMBER)
                                               from NONEDIPPS.t_sn_status a
                                              where a.wc = 'W0')))
                                AND TPO.SHIPMENT_ID = :ShipmentId
                              order by PalletNo";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public ExecuteResult checkIsExistIctPnOnLocationByLocationNoAndIctPn(string ictPn,string locationNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            try
            {
                string sql = @" SELECT tl.location_no AS 储位号,
                                   tl.pallet_no AS 栈板号,
                                   tl.qty AS 数量,
                                   tl.cartonqty AS 箱数,
                                   to_char(tl.udt, 'yyyy-MM-dd') AS 更新时间,
                                   tl.qhcartonqty AS QHHold箱数,
                                   tl.qhqty AS QHHold数量,
                                   tl.part_no AS 料号
                              FROM NONEDIPPS.T_LOCATION TL
                             WHERE TL.LOCATION_NO = :LocationNo
                               AND TL.PART_NO = :IctPn";
                object[][] sqlparams = new object[2][];
                sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "IctPn", ictPn };
                sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "LocationNo", locationNo };
                exeRes.Anything =  ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
            }
            catch (Exception  ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            return exeRes;
        }

        public ExecuteResult checkIsExistCartonNoIsLinkIctPn(string ictPn,string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            try
            {
                string sql = @" SELECT TSS.*  FROM  NONEDIPPS.T_SN_STATUS  TSS
                                WHERE  TSS.PART_NO=:IctPn
                                AND    TSS.CARTON_NO=:CartonNo";
                object[][] sqlparams = new object[2][];
                sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "IctPn", ictPn };
                sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
                exeRes.Anything = ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            return exeRes;
        }
        public ExecuteResult checkIsExistCartonNoIsLinkLocationNO(string locationNo, string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            try
            {
                string sql = @" SELECT TSS.*  FROM  NONEDIPPS.T_SN_STATUS  TSS
                                WHERE  TSS.LOCATION_NO=:LocationNo
                                AND    TSS.CARTON_NO=:CartonNo";
                object[][] sqlparams = new object[2][];
                sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "LocationNo", locationNo };
                sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
                exeRes.Anything = ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            return exeRes;
        }

        public DataTable getPassStationLogByCarton(string cartonNo)
        {
            try
            {
                string sql = string.Format("  SELECT T.* FROM (SELECT T1.*  FROM  (SELECT TPSL.IN_STATION as \"站别\", " +
                                                           " TPSL.CARTON_NO as \"箱号\", " +
                                                           " decode(TPSL.STATUS, '1', '成功', '0', '失败') \"过站状态\", " +
                                                           " TO_CHAR(TPSL.CDT,'yyyy-MM-dd HH24:mi:ss') as \"过站时间\", " +
                                                           "decode(TPSL.ISAVAILABLE, '1', '有效', '0', '无效') \"是否有效\"," +
                                                           @"TPSL.MAC
                                                      FROM NONEDIPPS.T_PASSSTATION_LOG TPSL
                                                     where tpsl.carton_no = :CartonNo
                                                     ORDER BY TPSL.CDT)T1
                                                    union
                                                    SELECT T2.*  FROM  (SELECT DISTINCT " + " TPSL.IN_STATION as \"站别\", " +
                                                            " '{0}' as \"箱号\", " +
                                                            " decode(TPSL.STATUS, '1', '成功', '0', '失败') \"过站状态\", " +
                                                            " TO_CHAR(TPSL.CDT,'yyyy-MM-dd HH24:mi:ss') as \"过站时间\", " +
                                                            "decode(TPSL.ISAVAILABLE, '1', '有效', '0', '无效') \"是否有效\"," +
                                                       @"TPSL.MAC
                                                           FROM NONEDIPPS.T_SN_STATUS       TSS,
                                                           NONEDIPPS.T_SN_STATUS       TSS1,
                                                           NONEDIPPS.T_PASSSTATION_LOG TPSL,
                                                           NONEDIPPS.T_SHIPMENT_PALLET  TSP
                                                     WHERE TSS.PICK_PALLET_NO = TSS1.PICK_PALLET_NO
                                                       AND SUBSTR(TSS.PICK_PALLET_NO,3) =TSP.PALLET_NO 
                                                       AND TSS.CARTON_NO = TPSL.CARTON_NO
                                                       AND TSP.PALLET_TYPE='001'
                                                       AND TPSL.IN_STATION='CHECK'
                                                       AND TSS1.CARTON_NO =:CartonNo
                                                       ORDER BY TPSL.CDT ASC)T2)T" +
                                                       " ORDER BY \"过站时间\" ", cartonNo);
                object[][] sqlparams = new object[1][];
                sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
                return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
            } catch (Exception ex) {
                return null;
            }
        }
        public DataTable getAllLocationInfo(bool isType)
        {
            /*
             * 当A->>>B 转移机器：
             1.A需要有机器，则 tl.pallet_no  is  null
             2.sys(系统库位)不允许人为操作转入机器，此库位专门有后台数据操作使用，则B库位不等于SYS   
             WHERE WL.WAREHOUSE_ID in ('2017070400015', '2019010200317','2017112100017', '2017112100018')          
             */
            string addSql = @"  union
                                SELECT DISTINCT wl.location_id, WL.location_no
                                  FROM NONEDIPPS.WMS_LOCATION WL
                                  LEFT JOIN NONEDIPPS.T_LOCATION TL
                                    ON TL.LOCATION_ID = WL.LOCATION_ID
                                 WHERE WL.WAREHOUSE_ID in (SELECT WAREHOUSE_ID FROM SAJET.WMS_WAREHOUSE WHERE ENABLED='Y')
                                   AND WL.Location_No = 'SYS'";
            string isTransferSql = "AND TL.PALLET_NO IS NOT NULL";
            if (isType)
            {
                addSql = "";
                isTransferSql = "";
            }
            string sSQL = string.Empty;
            sSQL = @"        SELECT DISTINCT wl.location_id, WL.location_no
                              FROM NONEDIPPS.WMS_LOCATION WL
                              LEFT JOIN NONEDIPPS.T_LOCATION TL
                                ON TL.LOCATION_ID = WL.LOCATION_ID
                             WHERE WL.WAREHOUSE_ID in (SELECT WAREHOUSE_ID FROM SAJET.WMS_WAREHOUSE WHERE ENABLED='Y')
                               AND WL.Location_No <> 'SYS' " + isTransferSql + addSql;
            //object[][] sqlparams = new object[1][];
            //sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Condition_", condition_ };
            return ClientUtils.ExecuteSQL(sSQL).Tables[0];
        }

        public DataTable queryDnInfoByShipmentId(string shipmentId)
        {
            string sqlstr = @"  SELECT DISTINCT TOI.DELIVERY_NO
                                  FROM NONEDIPPS.T_ORDER_INFO TOI
                                 WHERE TOI.SHIPMENT_ID =:ShipmentId  ";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            return ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];
        }
        public DataTable getLocationInformationBylocationId(string locationId)
        {
            string sqlstr = @"  SELECT      WL.Location_No as 储位名,
                                            tl.pallet_no     as 栈板号,
                                            tl.qty           as 总数量,
                                            tl.cartonqty     as 总箱数,
                                            tl.part_no       as 料号,
                                            tl.qhqty         as QAHold数量,
                                            tl.qhcartonqty   as QAHold箱数
                                            FROM NONEDIPPS.WMS_LOCATION WL
                                            LEFT JOIN NONEDIPPS.T_LOCATION TL
                                            ON TL.LOCATION_ID = WL.LOCATION_ID
                                            WHERE WL.WAREHOUSE_ID in (SELECT WAREHOUSE_ID FROM SAJET.WMS_WAREHOUSE WHERE ENABLED='Y')
                                            AND  WL.Location_id = :LocationId  ";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "LocationId", locationId };
            return ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];
        }
        public DataTable getTransferLableContent(string locationNo)
        {
            string sql = @"
            SELECT tl.pallet_no, tl.part_no, vmi.MPN, sum(tl.qty) AS  PER_QTY
              FROM NONEDIPPS.T_LOCATION TL, NONEDIOMS.vw_mpn_info vmi
             WHERE tl.part_no = vmi.ICTPARTNO
              AND TL.LOCATION_NO=:LocationNo
             GROUP BY tl.pallet_no, tl.part_no, vmi.MPN
             ";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "LocationNo", locationNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable getInformationByInputData(string inputData)
        {
            string sql = @"select tss.serial_number as 流水号,
                           tss.customer_sn as 客户料号,
                           tss.pallet_no as 栈板号,
                           tss.carton_no as 箱号,
                           tss.location_no as 目标储位,
                           TSS.PART_NO as 料号
                      from NONEDIPPS.t_sn_status tss
                     where (tss.serial_number = :InputData or
                                tss.carton_no = :InputData or
                                tss.pallet_no = :InputData) ";
            object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "InputData", inputData } };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }

        public DataTable locationNoTransformLocationIdByLocationNo(string locationNo)
        {
            string sqlstr = @"  SELECT  WL.LOCATION_ID  FROM   NONEDIPPS.WMS_LOCATION  WL  WHERE   WL.LOCATION_NO = :LocationNo ";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "LocationNo", locationNo };
            return ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];
        }

        public DataTable isPrintDeliveryNoteByDN_And_ShipOfType(string deliveryNote, string lineItem, string shipOfType)
        {
            string sql = @"     SELECT COUNT(*) ALL_COUNT
                                  FROM NONEDIPPS.T_940_UNICODE   T9U,
                                       NONEDIOMS.oms_lmd          ol,
                                       NONEDIOMS.oms_lmd_overview olo
                                 where NONEDIPPS.t_newtrim_function(t9u.deliverytype) = ol.dntype
                                   and NONEDIPPS.t_newtrim_function(t9u.shipcntycode) = ol.country
                                   and NONEDIPPS.t_newtrim_function(t9u.saleorgcode) = ol.salesorg
                                   and NONEDIPPS.t_newtrim_function(ol.sccode) = olo.sccode
                                   and olo.lmdmode = :ShipOfType
                                   and olo.document = 'DeliveryNote'
                                   and olo.item = 'Shipper'
                                   and olo.createlmd = 'Y'
                                   and NONEDIPPS.t_newtrim_function(t9u.region) = 'PAC'
                                   and t9u.deliveryno = :DeliveryNote
                                   and t9u.custdelitem = :LineItem";
            object[][] sqlparams = new object[3][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "DeliveryNote", deliveryNote };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "LineItem", lineItem };
            sqlparams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipOfType", shipOfType };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }

        public DataTable getT940InfoByDeliveryNo(string deliveryNo)
        {
            string sql = @"SELECT T9U.SHIPCNTYCODE, T9U.CUSTOMERGROUP
                            FROM NONEDIPPS.T_940_UNICODE T9U
                           WHERE T9U.DELIVERYNO = :DeliveryNo";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "DeliveryNo", deliveryNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }


        public DataTable getAllShipmentIdByCloseTime(string closeTime)
        {
            string sql = @"   SELECT  TSI.*
                              FROM NONEDIPPS.T_SHIPMENT_INFO TSI
                            WHERE TSI.REGION = 'PAC'
                              AND TSI.SHIPMENT_TYPE = 'DS'
                              AND TO_CHAR(TSI.CLOSE_TIME, 'YYYYMMDD') = :CloseTime";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CloseTime", closeTime };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable getAllDeliveryNoteByShipmentId(string shipmentId)
        {
            string sql = @"    SELECT DISTINCT TOI.DELIVERY_NO,toi.line_item
                               FROM NONEDIPPS.T_ORDER_INFO TOI
                              WHERE TOI.SHIPMENT_ID = :ShipmentId";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
    }
}
