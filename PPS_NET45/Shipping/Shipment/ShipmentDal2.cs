using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace Shipment
{
    public class ShipmentDal2
    {
        /// <summary>
        /// 获取shipmentId的称重栈板数量
        /// </summary>
        /// <param name="shipmentId"></param>
        /// <returns></returns>
        public int GetWeightNumByShipmnetStat(string shipmentId)
        {
            string sql = @"SELECT *
                             FROM ppsuser.g_ds_weight_t 
                            WHERE shipment_id = :shipmentId";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "shipmentId", shipmentId };
            int rowCount = ClientUtils.RowCount(sql, parameterArray);
            return rowCount;
        }

        /// <summary>
        /// 获取shiment单的所有车牌列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetCarList(string startDate, string endDate)
        {
            //HYQ: 这里改下 ，下拉选项改为 Tuck+shipmentid
            string sql = @"SELECT DISTINCT 
                                case 
                                  when truck_no='' then 'notruck'||shipment_id
                                    when truck_no is null then 'notruck'||shipment_id
                                else truck_no||shipment_id
                                end   new_truck_no
                             FROM ppsuser.t_shipment_pallet  
                            WHERE to_date(cdt) >=  to_date( :startDate ,'YYYY-MM-DD')
                              AND to_date(cdt) <= to_date( :endDate, 'YYYY-MM-DD')";
            object[][] parameterArray = new object[2][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "startDate", startDate };
            parameterArray[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "endDate", endDate };
            DataSet dataSet = ClientUtils.ExecuteSQL(sql, parameterArray);
            return dataSet;
        }

        /// <summary>
        /// 获取时间段、车牌号的shipment信息
        /// </summary>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="carNo">车牌号</param>
        /// <returns></returns>
        public DataSet GetShipmentInfoDataTable(string startDate, string endDate, string carNo)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(carNo))
            {
                sql = @"SELECT rownum, t.*  FROM ( 
                           select a.car_no,          --车牌号
                                  a.carrier_name,    --Carrier
                                  a.shipment_id,     --shipment_id
                                  c.wharf_location,  --码头储位
                                  s.end_palletno,    --栈板号
                                  s.qty,             --栈板货物数量
                                  s.shipment_flag--状态表中的FLAG
                             from PPSUSER.G_DS_SHIMMENT_BASE_T a
                           --left join ppsuser.g_ds_carinfo_t b on b.carrier = a.carrier_name and a.car_no = b.car_no
                             left join  ppsuser.g_ds_weightlist_t c on a.shipment_id = c.shipment_id
                             left join  ppsuser.g_ds_pallet_t s on a.shipment_id = s.shipment_id  
                            where a.car_no is null
                              -- and to_date(a.shipping_time) between to_date( :startDate ,'YYYY-MM-DD') and to_date( :endDate ,'YYYY-MM-DD')
                             and to_date(a.shipping_time) >=  to_date( :startDate ,'YYYY-MM-DD')
                             and to_date(a.shipping_time) <=  to_date( :endDate ,'YYYY-MM-DD')
                             and s.shipment_flag is null 
                             and s.end_palletno is not null
                            group by a.car_no,a.carrier_name,a.shipment_id,c.wharf_location,s.end_palletno,s.qty,s.shipment_flag 
                            order by shipment_flag desc ,end_palletno asc 
                         ) t ";
                object[][] parameterArray = new object[2][];
                parameterArray[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "startDate", startDate };
                parameterArray[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "endDate", endDate };
                DataSet dataSet = ClientUtils.ExecuteSQL(sql, parameterArray);
                return dataSet;
            }
            else
            {
                sql = @"SELECT rownum, t.*  FROM ( 
                           select a.car_no,          --车牌号
                                  a.carrier_name,    --Carrier
                                  a.shipment_id,     --shipment_id
                                  c.wharf_location,  --码头储位
                                  s.end_palletno,    --栈板号
                                  s.qty,             --栈板货物数量
                                  s.shipment_flag--状态表中的FLAG
                             from PPSUSER.G_DS_SHIMMENT_BASE_T a
                           --left join ppsuser.g_ds_carinfo_t b on b.carrier = a.carrier_name and a.car_no = b.car_no
                             left join  ppsuser.g_ds_weightlist_t c on a.shipment_id = c.shipment_id
                             left join  ppsuser.g_ds_pallet_t s on a.shipment_id = s.shipment_id  
                            where a.car_no = :carNo
                              -- and to_date(a.shipping_time) between to_date( :startDate ,'YYYY-MM-DD') and to_date( :endDate ,'YYYY-MM-DD')
                             and to_date(a.shipping_time) >=  to_date( :startDate ,'YYYY-MM-DD')
                             and to_date(a.shipping_time) <=  to_date( :endDate ,'YYYY-MM-DD')
                             and s.shipment_flag is null
                             and s.end_palletno is not null
                            group by a.car_no,a.carrier_name,a.shipment_id,c.wharf_location,s.end_palletno,s.qty,s.shipment_flag 
                            order by shipment_flag desc ,end_palletno asc) t
                            ";
                object[][] parameterArray = new object[3][];
                parameterArray[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "carNo", carNo };
                parameterArray[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "startDate", startDate };
                parameterArray[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "endDate", endDate };
                DataSet dataSet = ClientUtils.ExecuteSQL(sql, parameterArray);
                return dataSet;
            }
        }

        /// <summary>
        /// 获取时间段、车牌号的shipment信息
        /// </summary>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="carNo">车牌号</param>
        /// <returns></returns>
        public DataSet GetShipmentAlearyInfoDataTable(string startDate, string endDate, string carNo)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(carNo))
            {
                sql = @"SELECT rownum, t.*  FROM ( 
                           select --a.car_no,          --车牌号
                                  a.carrier_name,    --Carrier
                                  a.shipment_id,     --shipment_id
                                --c.wharf_location,  --码头储位
                                  s.end_palletno,     --栈板号
                                  s.qty             --栈板货物数量
                                --s.shipment_flag--状态表中的FLAG
                             from PPSUSER.G_DS_SHIMMENT_BASE_T a
                           --left join ppsuser.g_ds_carinfo_t b on b.carrier = a.carrier_name and a.car_no = b.car_no
                             left join  ppsuser.g_ds_weightlist_t c on a.shipment_id = c.shipment_id
                             left join  ppsuser.g_ds_pallet_t s on a.shipment_id = s.shipment_id  
                            where a.car_no is null
                              -- and to_date(a.shipping_time) between to_date( :startDate ,'YYYY-MM-DD') and to_date( :endDate ,'YYYY-MM-DD')
                             and to_date(a.shipping_time) >=  to_date( :startDate ,'YYYY-MM-DD')
                             and to_date(a.shipping_time) <=  to_date( :endDate ,'YYYY-MM-DD')
                             and s.shipment_flag = '1'
                             and s.end_palletno is not null
                            group by a.car_no,a.carrier_name,a.shipment_id,c.wharf_location,s.end_palletno,s.qty,s.shipment_flag 
                            order by end_palletno asc 
                        ) t";
                object[][] parameterArray = new object[2][];
                parameterArray[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "startDate", startDate };
                parameterArray[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "endDate", endDate };
                DataSet dataSet = ClientUtils.ExecuteSQL(sql, parameterArray);
                return dataSet;
            }
            else
            {
                sql = @"SELECT rownum, t.*  FROM ( 
                           select --a.car_no,          --车牌号
                                  a.carrier_name,    --Carrier
                                  a.shipment_id,     --shipment_id
                                --c.wharf_location,  --码头储位
                                  s.end_palletno,    --栈板号
                                  s.qty             --栈板货物数量
                                --s.shipment_flag--状态表中的FLAG
                             from PPSUSER.G_DS_SHIMMENT_BASE_T a
                           --left join ppsuser.g_ds_carinfo_t b on b.carrier = a.carrier_name and a.car_no = b.car_no
                             left join  ppsuser.g_ds_weightlist_t c on a.shipment_id = c.shipment_id
                             left join  ppsuser.g_ds_pallet_t s on a.shipment_id = s.shipment_id  
                            where a.car_no = :carNo
                              -- and to_date(a.shipping_time) between to_date( :startDate ,'YYYY-MM-DD') and to_date( :endDate ,'YYYY-MM-DD')
                              and to_date(a.shipping_time) >=  to_date( :startDate ,'YYYY-MM-DD')
                              and to_date(a.shipping_time) <=  to_date( :endDate ,'YYYY-MM-DD')
                              and s.shipment_flag = '1'
                              and s.end_palletno is not null
                             group by a.car_no,a.carrier_name,a.shipment_id,c.wharf_location,s.end_palletno,s.qty,s.shipment_flag 
                             order by end_palletno asc 
                       ) t ";
                object[][] parameterArray = new object[3][];
                parameterArray[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "carNo", carNo };
                parameterArray[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "startDate", startDate };
                parameterArray[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "endDate", endDate };
                DataSet dataSet = ClientUtils.ExecuteSQL(sql, parameterArray);
                return dataSet;
            }
        }

        /// <summary>
        /// 判断pallet是否已经扫描
        /// </summary>
        /// <param name="palletNo">栈板号</param>
        /// <returns>true:已经扫描, false:未扫描</returns>
        public bool IsExistPalletFlagScan(string palletNo)
        {
            string sql = @"SELECT * 
                             FROM ppsuser.g_ds_pallet_t 
                            WHERE shipment_flag = '1' AND  end_palletno = :palletNo";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "palletNo", palletNo };
            int count = ClientUtils.RowCount(sql, parameterArray);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新pallet表标记为已经扫
        /// </summary>
        /// <param name="palletNo">栈板号</param>
        public void UpdatePalletFlag(string palletNo)
        {
            string sql = @"UPDATE ppsuser.g_ds_pallet_t 
                              SET shipment_flag = '1' 
                            WHERE end_palletno =:palletNo";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "palletNo", palletNo };
            ClientUtils.ExecuteSQL(sql, parameterArray);
        }

        /// <summary>
        /// 获取shipment未扫描的栈板数
        /// </summary>
        /// <param name="shipmentId"></param>
        /// <returns>未扫描栈板数</returns>
        public int GetShipmentRemainNum(string shipmentId)
        {
            string sql = @"SELECT * 
                             FROM ppsuser.g_ds_shimment_base_t  A
                             LEFT JOIN ppsuser.g_ds_pallet_t b 
                               ON A.shipment_id = b.shipment_id 
                            WHERE b.shipment_flag IS NULL 
                              AND A.shipment_id = :shipmentid";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "shipmentid", shipmentId };
            return ClientUtils.RowCount(sql, parameterArray);
        }

        /// <summary>
        /// 更新DDline状态为Handover
        /// </summary>
        /// <param name="shipmentId"></param>
        public void UpdateDDLineStatus(string shipmentId)
        {
            string sql = @"UPDATE ppsuser.g_ds_shipment_ddline_t SET status = 'Handover'
                            WHERE shipment_id = :shipmentid";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "shipmentid", shipmentId };
            ClientUtils.ExecuteSQL(sql, parameterArray);
        }

        /// <summary>
        /// 更新DNline状态为Handover
        /// </summary>
        /// <param name="shipmentId"></param>
        public void UpdateDNLineStatus(string shipmentId)
        {
            string sql = @"UPDATE ppsuser.g_ds_shipment_dnline_t SET status = 'Handover'
                            WHERE shipment_id = :shipmentid";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "shipmentid", shipmentId };
            ClientUtils.ExecuteSQL(sql, parameterArray);
        }

        /// <summary>
        /// 更新WeihtList标识
        /// </summary>
        /// <param name="shipmentId"></param>
        public void UpdateWeightListFlag(string shipmentId)
        {
            string sql = @"UPDATE ppsuser.g_ds_weightlist_t SET Flag = '1'
                            WHERE shipment_id = :shipmentid";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "shipmentid", shipmentId };
            ClientUtils.ExecuteSQL(sql, parameterArray);
        }
    }
}
