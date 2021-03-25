using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;

namespace ReverseAC
{
    public class ReverseDal
    {
        #region 检查shipmentId是否hold
        /// <summary>
        /// 检查shipmentId是否hold
        /// </summary>
        /// <param name="shipmentId"></param>
        /// <returns>hold的数量</returns>
        public int CheckShipmentHold(string shipmentId)
        {
            string sql = @"SELECT * FROM NONEDIPPS.edi940_header A 
                             LEFT JOIN NONEDIPPS.g_ds_shipment_dn_t b 
                               ON A.ac_dn = b.dn WHERE b.shipment_id = :shipmentId 
                              AND trans_flag IN ('H','Z')";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentId", shipmentId };
            return ClientUtils.RowCount(sql, parameterArray);
        }
        #endregion

        #region 检查palletNo,cartonId,SN是否Hold
        /// <summary>
        /// 检查palletNo,cartonId,SN是否Hold
        /// </summary>
        /// <param name="otherSn"></param>
        /// <returns>hold的数量</returns>
        public int CheckOtherSnHold(string otherSn)
        {
            string sql = @"SELECT * FROM NONEDIPPS.g_sn_status 
                            WHERE serial_number = :othersn 
                               OR pallet_no = :othersn 
                               OR carton_no = :othersn
                              AND hold_flag = 'Y' ";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "othersn", otherSn };
            return ClientUtils.RowCount(sql, parameterArray);
        }
        #endregion
        #region 检查carton的pallet内是否有Carton Hold
        /// <summary>
        /// 检查carton的pallet内是否有Carton Hold
        /// </summary>
        /// <param name="otherSn"></param>
        /// <returns>hold的数量</returns>
        public int CheckCartonInPalletOfCartonHold(string cartonNo)
        {
            string sql = @"SELECT * 
                             FROM NONEDIPPS.g_sn_status 
                            WHERE hold_flag = 'Y' 
                              AND carton_no IN (
                                    SELECT inputdata 
                                      FROM NONEDIPPS.g_ds_pickinglist_t 
                                     WHERE pallet_no IN(
                                             SELECT pallet_no 
                                               FROM NONEDIPPS.g_ds_pickinglist_t 
                                              WHERE inputdata = :CartonNo
                                           )
                                  )";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.RowCount(sql, parameterArray);
        }
        #endregion
        #region 检查pallet内有没有Hold的Carton
        /// <summary>
        /// 检查pallet内有没有Hold的Carton
        /// </summary>
        /// <param name="otherSn"></param>
        /// <returns>hold的数量</returns>
        public int CheckCartonInPalletHold(string palletNo)
        {
            string sql = @"SELECT * 
                             FROM NONEDIPPS.g_sn_status 
                            WHERE hold_flag = 'Y' 
                              AND  carton_no IN (
                                     SELECT inputdata 
                                       FROM NONEDIPPS.g_ds_pickinglist_t 
                                      WHERE pallet_no = :PalletNo 
                                   ) ";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PalletNo", palletNo };
            return ClientUtils.RowCount(sql, parameterArray);
        }
        #endregion

        #region 检查真实的Pallet有没有Hold产品

        /// <summary>
        /// 检查真实的Pallet有没有Hold产品
        /// </summary>
        /// <param name="palletNo"></param>
        /// <returns></returns>
        public int CheckCartonInReadPalletHold(string palletNo)
        {
            string sql = @"SELECT * 
                             FROM NONEDIPPS.g_sn_status 
                            WHERE hold_flag = 'Y' 
                              AND carton_no IN (
                                     SELECT inputdata 
                                       FROM NONEDIPPS.g_ds_pickinglist_t a
                                       LEFT JOIN NONEDIPPS.g_ds_pallet_t b 
                                        on a.pallet_no = b.pallet_no 
                                      WHERE b.end_palletno = :PalletNo 
                                   ) ";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PalletNo", palletNo };
            return ClientUtils.RowCount(sql, parameterArray);
        }
        #endregion

        #region 检查Shipment下是否有Hold箱号
        /// <summary>
        /// 检查Shipment下是否有Hold箱号
        /// </summary>
        /// <param name="shipmentId"></param>
        /// <returns></returns>
        public int CheckCartonInShipmentHold(string shipmentId)
        {
            string sql = @"SELECT * 
                             FROM NONEDIPPS.g_ds_pickinglist_t A
                             JOIN NONEDIPPS.g_sn_status b ON A.inputdata = b.carton_no
                            WHERE b.hold_flag = 'Y' 
                              AND shipment_id = :ShipmentID";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentID", shipmentId };
            return ClientUtils.RowCount(sql, parameterArray);
        } 
        #endregion

        public int CheckAlready(string cartonNo)
        {
            string sql = @"SELECT * 
                             FROM NONEDIPPS.g_ds_pickinglist_t 
                            WHERE inputdata = :CartonNo";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            int rowCount =  ClientUtils.RowCount(sql, parameterArray);
            return rowCount;
        }

        #region 获取所有Hold的信息
        /// <summary>
        /// 获取所有Hold的信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllHoldInfo()
        {
            string strSql = @"SELECT ROWNUM , D.* FROM (
                                SELECT * FROM (
                                     SELECT DISTINCT
                                            'False' AS SEL
                                           ,hawb
                                           ,shipment_id
                                           ,'' AS pallet_no
                                           ,'' AS carton_no
                                           ,'' AS materialno
                                           ,CASE WHEN trans_flag = 'H' THEN 'Lock' 
                                                 WHEN trans_flag = 'Z' THEN 'Zero Confirm' 
                                             END AS status
                                       FROM NONEDIPPS.edi940_header A 
                                       LEFT JOIN NONEDIPPS.g_ds_shipment_dn_t b 
                                              ON A.ac_dn = b.dn 
                                      WHERE trans_flag IN ('H','Z')
                                    UNION ALL 
                                     SELECT  DISTINCT 
                                            'False' AS sel
                                            ,c.hawb  
                                            ,b.shipment_id 
                                            ,b.pallet_no 
                                            ,a.carton_no 
                                            ,d.part_no AS materialno
                                            ,'Hold' AS status
                                       FROM NONEDIPPS.g_sn_status a
                                            JOIN NONEDIPPS.g_ds_pickinglist_t b 
                                              ON A.carton_no = b.inputdata 
                                       LEFT JOIN NONEDIPPS.g_ds_shimment_base_t c
                                              ON b.shipment_id = C.shipment_id
                                       LEFT JOIN sajet.sys_part d ON a.part_id =d.part_id
                                      WHERE hold_flag = 'Y'
                                ) ORDER BY hawb 
                              ) D ";
            DataSet dataSet = ClientUtils.ExecuteSQL(strSql);
            if (dataSet != null)
            {
                return dataSet.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 更新shipemtn UnHold标记
        /// <summary>
        /// 更新shipemtn UnHold标记
        /// </summary>
        /// <param name="shipmentId"></param>
        public void SetShipmentUnHold(string shipmentId)
        {
            string sql = @"UPDATE NONEDIPPS.edi940_header 
                              SET trans_flag ='N' 
                            WHERE trans_flag IN ('H','Z') 
                              AND ac_dn IN (
                                     SELECT dn 
                                       FROM NONEDIPPS.g_ds_shipment_dn_t 
                                      WHERE shipment_id = :ShipmentId
                                   )";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            ClientUtils.ExecuteSQL(sql, parameterArray);
        }
        #endregion

        #region 更新Porduct UnHold标记
        /// <summary>
        /// 更新Porduct UnHold标记
        /// </summary>
        /// <param name="shipmentId">集货单号</param>
        /// <param name="cartonNo">箱号</param>
        public void SetProductUnHold(string shipmentId, string cartonNo)
        {
            string sql = @"UPDATE NONEDIPPS.g_sn_status 
                              SET hold_flag = 'N' 
                            WHERE carton_no = :CartonNo ";
            object[][] parameterArray = new object[1][];
             parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            ClientUtils.ExecuteSQL(sql, parameterArray);
        }
        #endregion

        #region 获取所有ShipmentId的信息(包含箱)
        /// <summary>
        /// 获取所有ShipmentId的信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetInfoByShipmentId(string shipmentId)
        {
            string strSql = @"SELECT ROWNUM , D.* FROM (
                                SELECT * FROM (
                                     SELECT DISTINCT
                                            'False' AS SEL
                                           ,hawb
                                           ,shipment_id
                                           ,'' AS pallet_no
                                           ,'' AS carton_no
                                           ,'' AS materialno
                                           ,'' AS status
                                       FROM NONEDIPPS.edi940_header A 
                                       LEFT JOIN NONEDIPPS.g_ds_shipment_dn_t b 
                                              ON A.ac_dn = b.dn 
                                      WHERE shipment_id =  :ShipmentId 
                                    UNION ALL 
                                     SELECT DISTINCT 
                                            'False' AS SEL
                                           ,c.hawb  
                                           ,b.shipment_id 
                                           ,b.pallet_no 
                                           ,a.carton_no 
                                           ,d.part_no AS materialno
                                           ,CASE hold_flag WHEN 'N' THEN ' '
                                            WHEN 'Y' THEN 'Hold' END AS status
                                       FROM NONEDIPPS.g_sn_status a
                                            JOIN NONEDIPPS.g_ds_pickinglist_t b 
                                              ON A.carton_no = b.inputdata 
                                       LEFT JOIN NONEDIPPS.g_ds_shimment_base_t c
                                              ON b.shipment_id = C.shipment_id
                                       LEFT JOIN sajet.sys_part d 
                                              ON a.part_id =d.part_id
                                      WHERE b.shipment_id = :ShipmentId 
                                ) ORDER BY hawb,status desc,pallet_no asc
                              ) D ";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            DataSet dataSet = ClientUtils.ExecuteSQL(strSql, parameterArray);
            if (dataSet != null)
            {
                return dataSet.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 获取Carton的信息
        /// <summary>
        /// 获取Carton的信息
        /// </summary>
        /// <param name="cartonNo"></param>
        /// <returns></returns>
        public DataTable GetInfoByCartonNo(string cartonNo)
        {
            string strSql = @"SELECT ROWNUM , D.* FROM (
                                     SELECT DISTINCT 
                                            'False' AS SEL
                                           ,c.hawb  
                                           ,c.shipment_id 
                                           ,b.pallet_no 
                                           ,a.carton_no 
                                           ,b.ictpn AS materialno
                                           ,'' AS status
                                       FROM NONEDIPPS.g_sn_status A
                                       LEFT JOIN NONEDIPPS.g_ds_scandata_detail_t b 
                                              ON A.serial_number = b.serial_number AND  A.carton_no = b.carton_no
                                            JOIN NONEDIPPS.g_ds_scandata_t C 
                                              ON b.dn = C.dn 
                                             AND b.dn_line = C.dn_line
                                      WHERE a.carton_no = :Carton_no
                              ) D ";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Carton_no", cartonNo };
            DataSet dataSet = ClientUtils.ExecuteSQL(strSql, parameterArray);
            if (dataSet != null)
            {
                return dataSet.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Module:Reverse 
        #region 获取ShipmentId 上传EDI的数量
        /// <summary>
        /// 获取ShipmentId 上传EDI的数量
        /// </summary>
        /// <param name="shipmentId"></param>
        /// <returns>上传EDI的数量</returns>
        public int GetUpdateEdiCount(string shipmentId)
        {
            string strSql = @"SELECT * 
                                FROM NONEDIPPS.g_ds_shimment_base_t 
                               WHERE edi_flag = 'Y' 
                                 AND shipment_id = :ShipmentId ";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            int rowCount = ClientUtils.RowCount(strSql, parameterArray);
            return rowCount;
        }
        #endregion 

        /// <summary>
        /// 更新栈板号出货标记
        /// </summary>
        /// <param name="shipmentId"></param>
        public void UpdatePalletFlagByShipmentId(string shipmentId)
        {
            string strSql = @"UPDATE NONEDIPPS.g_ds_pallet_t 
                                 SET  shipment_flag = '' 
                               WHERE shipment_id = :ShipmentId ";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }
        /// <summary>
        /// 更新栈板号出货标记
        /// </summary>
        /// <param name="shipmentId"></param>
        public void UpdatePalletFlagByPalletNo(string palletNo)
        {
            string strSql = @"UPDATE NONEDIPPS.g_ds_pallet_t 
                                 SET  shipment_flag = '' 
                               WHERE pallet_no = :PalletNo ";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PalletNo", palletNo };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }


        /// <summary>
        /// 更新shipment ddline状态标记
        /// </summary>
        /// <param name="shipmentId"></param>
        public void UpdateDdLine(string shipmentId)
        {
            string strSql = @"UPDATE NONEDIPPS.g_ds_shipment_ddline_t 
                                 SET status = 'INPROCESS' 
                               WHERE shipment_id = :ShipmentId ";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }

        /// <summary>
        /// 更新shipment ddline状态标记
        /// </summary>
        /// <param name="shipmentId"></param>
        public void UpdateDnLineByShpment(string shipmentId)
        {
            string strSql = @"UPDATE NONEDIPPS.g_ds_shipment_dnline_t 
                                 SET status = 'INPROCESS' 
                               WHERE shipment_id = :ShipmentId ";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }

        /// <summary>
        /// 删除称重记录
        /// </summary>
        /// <param name="shipmentId"></param>
        public void DelWeight(string shipmentId)
        {
            string strSql = @"DELETE NONEDIPPS.g_ds_weight_t 
                               WHERE shipment_id = :shipmentid ";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }
        /// <summary>
        /// 删除称重记录
        /// </summary>
        /// <param name="shipmentId"></param>
        public void DelWeight(string shipmentId, string palletNo)
        {
            string strSql = @"DELETE NONEDIPPS.g_ds_weight_t 
                               WHERE shipment_id = :ShipmentId 
                                 AND pallet_no = :PalletNo";
            object[][] parameterArray = new object[2][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            parameterArray[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PalletNo", palletNo };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }

        /// <summary>
        /// 删除称重列表记录
        /// </summary>
        /// <param name="shipmentId"></param>
        public void DelWeightList(string shipmentId)
        {
            string strSql = @"DELETE NONEDIPPS.g_ds_weightlist_t 
                               WHERE shipment_id = :shipmentid ";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }
        public void DelWeightList(string shipmentId, string palletNo)
        {
            string strSql = @"DELETE NONEDIPPS.g_ds_weightlist_t 
                               WHERE shipment_id = :ShipmentId 
                                 AND pallet_no = :PalletNo";
            object[][] parameterArray = new object[2][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            parameterArray[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PalletNo", palletNo };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }

        /// <summary>
        /// 更新扫描标记
        /// </summary>
        /// <param name="shipmentId"></param>
        public void UpdateCheckFlagByShipmentId(string shipmentId)
        {
            string strSql = @"UPDATE NONEDIPPS.g_ds_scandata_t 
                                 SET check_flag = ''
                               WHERE shipment_id = :ShipmentId ";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }
        //删除shipping label 记录
        public void DelPackPrintRecord(string shipmentId)
        {
            string strSql = @"DELETE NONEDIPPS.g_ds_pack_shipping_label 
                               WHERE shipment_id = :shipmentid ";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }
        public void UpdatePackPrintRecord(string shipmentId, string palletNo)
        {
            string strSql = @"UPDATE NONEDIPPS.g_ds_pack_shipping_label 
                                SET unship_flag = 'Y'
                              WHERE shipment_id = :ShipmentId 
                                AND pallet_no = :PalletNo ";
            object[][] parameterArray = new object[2][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            parameterArray[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PalletNo", palletNo };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }

        public void DelScanDetail(string shipmentId)
        {
            string strSql = @"DELETE NONEDIPPS.g_ds_scandata_detail_t 
                               WHERE shipment_id = :shipmentid ";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }
        public void DelScanDetail(string shipmentId, string palletNo)
        {
            string strSql = @"DELETE NONEDIPPS.g_ds_scandata_detail_t 
                               WHERE shipment_id = :ShipmentId 
                                 AND pallet_no   = :PalletNo";
            object[][] parameterArray = new object[2][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            parameterArray[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PalletNo", palletNo };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }

        public void DelScan(string shipmentId)
        {
            string strSql = @"DELETE NONEDIPPS.g_ds_scandata_t 
                               WHERE shipment_id = :shipmentid ";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }
        public void DelScan(string shipmentId, string palletNo)
        {
            string strSql = @"DELETE NONEDIPPS.g_ds_scandata_t 
                               WHERE shipment_id = :ShipmentId 
                                 AND pallet_no = :PalletNo";
            object[][] parameterArray = new object[2][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            parameterArray[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PalletNo", palletNo };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }

        public void DelLpsLine(string shipmentId)
        {
            string strSql = @"DELETE NONEDIPPS.ict_lps_line 
                               WHERE mother_child_tag = 'C'
                                 AND shipment_id = :ShipmentId";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }
        public void DelLpsLine(string shipmentId, string palletNo)
        {
            string strSql = @"DELETE NONEDIPPS.ict_lps_line 
                               WHERE mother_child_tag = 'C'
                                 AND carton_id IN (
                                       SELECT carton_no 
                                         FROM NONEDIPPS.g_ds_scandata_detail_t 
                                        WHERE shipment_id = :ShipmentId
                                          AND pallet_no = :PalletNo )";
            object[][] parameterArray = new object[2][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            parameterArray[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PalletNo", palletNo };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipmentId"></param>
        public void UpdatePalletScanQTY(string shipmentId)
        {
            string strSql = @"UPDATE NONEDIPPS.g_ds_pallet_t 
                                 SET scan_qty = '0' 
                                    ,sscc = '' 
                                    ,end_palletno= ''
                               WHERE shipment_id = :ShipmentId ";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }
        public void UpdatePallet(string palletNo)
        {
            string strSql = @"UPDATE NONEDIPPS.g_ds_pallet_t
                                 SET total_cartons=0
                                    ,empty_cartons =0
                                    ,length = 0 
                                    ,width = 0 
                                    ,height = 0
                                    ,other_weight = 0 
                                    ,p_flag = '0'
                                    ,sscc = '' 
                                    ,end_palletno = ''
                                    ,scan_qty = 0
                               WHERE pallet_no = :PalletNo";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PalletNo", palletNo };
            ClientUtils.ExecuteSQL(strSql, parameterArray);

        }

        public void UpdatePickScanQty(string shipmentId)
        {
            string strSql = @"UPDATE NONEDIPPS.g_ds_pick_t 
                                 SET scan_qty = '0' 
                               WHERE shipment_id = :ShipmentId ";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }

        public void UpdateSnStatus(string shipmentId)
        {
            string strSql = @"UPDATE NONEDIPPS.g_sn_status A 
                                 SET A.shipping_id = '0'
                                    ,A.flag='0'
                                    ,A.pps_shipment = ''
                                    ,A.rc_no = 'UnShip Location'
                               WHERE A.carton_no
                                  IN( SELECT inputdata 
                                        FROM NONEDIPPS.g_ds_pickinglist_t b
                                       WHERE b.shipment_id = :ShipmentId )";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }
        public void UpdateSnStatus(string shipmentId, string palletNo)
        {
            string strSql = @"UPDATE NONEDIPPS.g_sn_status 
                                 SET shipping_id = '0'
                                    ,flag='0'
                                    ,pps_shipment = ''
                                    ,RC_NO = 'UnShip Location'
                               WHERE carton_no IN (
                                        SELECT carton_no 
                                          FROM NONEDIPPS.g_ds_scandata_detail_t 
                                         WHERE shipment_id = :ShipmentId
                                           AND pallet_no = :PalletNo 
                                     )";
            object[][] parameterArray = new object[2][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            parameterArray[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PalletNo", palletNo };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }

        public void UpdatePickQtyAndLineNo(string shipmentId)
        {
            string strSql = @"UPDATE NONEDIPPS.g_ds_pick_t 
                                 SET pick_qty = '0'
                                    ,scan_qty = '0'
                                    ,line_no = ''
                               WHERE shipment_id = :ShipmentId ";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }
        public void UpdatePickQtyAndLineNo(string shipmentId, string palletNo)
        {
            string strSql = @"UPDATE NONEDIPPS.g_ds_pick_t 
                                 SET pick_qty = '0'
                                    ,scan_qty = '0'
                                    ,line_no = ''
                               WHERE shipment_id = :ShipmentId 
                                 AND pallet_no = :PalletNo";
            object[][] parameterArray = new object[2][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            parameterArray[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PalletNo", palletNo };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }

        public void DelPickList(string shipmentId)
        {
            string strSql = @"DELETE NONEDIPPS.g_ds_pickinglist_t 
                               WHERE shipment_id = :ShipmentId ";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }
        public void DelPickList(string shipmentId, string palletNo)
        {
            string strSql = @"DELETE NONEDIPPS.g_ds_pickinglist_t 
                               WHERE shipment_id = :shipmentid 
                                 AND pallet_no = :PalletNo";
            object[][] parameterArray = new object[2][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            parameterArray[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PalletNo", palletNo };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }

        public int GetPalletCartonCount(string shipmentId, string palletNo)
        {
            string strSql = @"SELECT * 
                                FROM NONEDIPPS.g_ds_scandata_detail_t 
                               WHERE shipment_id = :shipmentid 
                                 AND pallet_no = :palletno";
            object[][] parameterArray = new object[2][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PalletNo", palletNo };
            return ClientUtils.RowCount(strSql, parameterArray);
        }
        #endregion

        public DataTable GetCartonByMaterial(string material)
        {
            string strSql = @"SELECT ROWNUM ,T.* 
                                FROM (
                                     SELECT DISTINCT b.part_no
                                                    ,A.carton_no
                                                    ,A.rc_no
                                       FROM NONEDIPPS.g_sn_status A 
                                       JOIN sajet.sys_part b 
                                         ON A.part_id =b.part_id 
                                WHERE part_no = :Material ) T
";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Material", material };
            DataSet dsCartonInfo = ClientUtils.ExecuteSQL(strSql, parameterArray);
            if (dsCartonInfo != null || dsCartonInfo.Tables[0].Rows.Count > 0)
            {
                return dsCartonInfo.Tables[0];
            }
            else
            {
                return null;
            }
        }
        public DataTable GetCartonByCarton(string cartonNo)
        {
            string strSql = @"SELECT ROWNUM,T.* FROM(
                                      SELECT DISTINCT  b.part_no
                                                      ,A.carton_no
                                                      ,c.location_name rc_no
                                        FROM NONEDIPPS.g_sn_status A 
                                        JOIN sajet.sys_part b 
                                          ON A.part_id =b.part_id 
                                        LEFT JOIN sajet.WMS_LOCATION c
                                          ON a.rc_no = TO_CHAR(C.LOCATION_ID)
                               WHERE carton_no = :Carton ) T ";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Carton", cartonNo };
            DataSet dsCartonInfo = ClientUtils.ExecuteSQL(strSql, parameterArray);
            if (dsCartonInfo != null || dsCartonInfo.Tables[0].Rows.Count > 0)
            {
                return dsCartonInfo.Tables[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 更新储位
        /// </summary>
        /// <param name="cartonNo"></param>
        /// <param name="location"></param>
        public void UpdateLocationByCartonNo(string cartonNo,string location)
        {
            string strSql = @"UPDATE NONEDIPPS.g_sn_status 
                                 SET rc_no = :Location 
                               WHERE carton_no = :CartonNo";
            object[][] parameterArray = new object[2][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Location", location };
            parameterArray[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            ClientUtils.ExecuteSQL(strSql, parameterArray);
        }

        public DataTable GetLocationNoByLocationName(string location)
        {
            string strSql = @"SELECT location_no
                                    ,location_id
                                FROM sajet.wms_location 
                               WHERE location_name = :LocationName";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "LocationName", location };
             
            DataSet dataSet =  ClientUtils.ExecuteSQL(strSql, parameterArray);
            if (dataSet != null || dataSet.Tables[0].Rows.Count > 0)
            {
                return dataSet.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #region 检查Check Hold
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sno"></param>
        /// <param name="Type"></param>
        /// <param name="RetMsg"></param>
        /// <returns></returns>
        public string  CheckHoldByProcedure(string Sno, string Type, out string RetMsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "InputSno", Sno };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "InputType", Type };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RetMsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.T_Check_Hold", procParams);
            RetMsg = ds.Tables[0].Rows[0]["RetMsg"].ToString();
            if (RetMsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return RetMsg;
            }

        }
        #endregion

    }
}
