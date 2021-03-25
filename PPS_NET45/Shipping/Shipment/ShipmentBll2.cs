using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shipment
{
    public class ShipmentBll2
    {
        /// <summary>
        /// 获取称重站点的百分比
        /// </summary>
        /// <param name="palletNum"></param>
        /// <param name="shipmentId"></param>
        /// <returns></returns>
        public static string GetWeightNumByShipmnetStat(int palletNum, string shipmentId)
        {
            string progressPercenStr = string.Empty;
            ShipmentDal shipmentDal = new ShipmentDal();
            int weightScanNum = shipmentDal.GetWeightNumByShipmnetStat(shipmentId);

            if (weightScanNum <= 0)
            {
                progressPercenStr = "0/0%";
            }
            else
            {
                float tmp = (float)weightScanNum / (float)palletNum;
                int percen = (int)(tmp * 100);
                progressPercenStr = weightScanNum + "/" + percen + "%";
            }
            return progressPercenStr;
        }

        /// <summary>
        /// 获取车牌列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetCarList(string startDate, string endDate)
        {
            ShipmentDal2 shipmentDal2 = new ShipmentDal2();
            DataSet dataSet = shipmentDal2.GetCarList(startDate, endDate);
            DataTable dataTable = dataSet.Tables[0];
            if (dataTable == null || dataTable.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataTable;
            }
        }

        /// <summary>
        /// 获取时间段、车牌号的shipment信息
        /// </summary>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="carNo">车牌号</param>
        /// <returns></returns>
        public DataTable GetShipmentInfoDataTable(string startDate, string endDate, string carNo)
        {
            ShipmentDal shipmentDal = new ShipmentDal();
            DataSet dataSet = shipmentDal.GetShipmentInfoDataTable(startDate, endDate, carNo);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public DataTable GetShipmentAlreadyInfoDataTable(string startDate, string endDate, string carNo)
        {
            ShipmentDal shipmentDal = new ShipmentDal();
            DataSet dataSet = shipmentDal.GetShipmentAlearyInfoDataTable(startDate, endDate, carNo);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }


        /// <summary>
        /// 扫描栈板号
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="shipmentId"></param>
        /// <param name="palletNo"></param>
        /// <param name="strMessage">false:返回消息</param>
        /// <param name="ScanRowNum">匹配的行号</param>
        /// <returns></returns>
        public bool ScanPalletNo(DataTable dataTable, string palletNo, out string strMessage, out int ScanRowNum)
        {
            ScanRowNum = 0;
            strMessage = string.Empty;
            bool isExistPallet = false;
            int rowNum = 0;
            ShipmentDal shipmentDal = new ShipmentDal();
            //判断是否已经扫描
            bool isExistScan = shipmentDal.IsExistPalletFlagScan(palletNo);
            if (isExistScan)
            {
                ScanRowNum = 0;
                strMessage = "栈板号已扫描";
                return false;
            }
            foreach (DataRow dataRow in dataTable.Rows)
            {
                rowNum++;
                if (palletNo == dataRow["end_palletno"].ToString())//存在栈板号
                {
                    ScanRowNum = rowNum;
                    isExistPallet = true;

                    //更新pallet表标记
                    shipmentDal.UpdatePalletFlag(palletNo);
                    //获取未扫描栈板数 
                    string shipmentId = dataRow["shipment_id"].ToString();
                    int remainNum = shipmentDal.GetShipmentRemainNum(shipmentId);
                    if (remainNum <= 0)
                    {
                        //更新DDLine状态
                        shipmentDal.UpdateDDLineStatus(shipmentId);
                        //更新DNLine状态
                        shipmentDal.UpdateDNLineStatus(shipmentId);
                        //更新WeightList标识
                        shipmentDal.UpdateWeightListFlag(shipmentId);
                        //调用上传EDI界面
                        CallOtherWindow(shipmentId);
                        return true;
                    }
                }
            }
            if (!isExistPallet)
            {
                strMessage = "栈板号不存在";
            }
            return isExistPallet;
        }

        /// <summary>
        /// 调用EDI上传界面
        /// </summary>
        public static void CallOtherWindow(string strShipmentId)
        {
            try
            {
                if (MessageBox.Show("Shipment已经完成扫描，是否上传EDI?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Sentdirectedi.fMain sendEdi = new Sentdirectedi.fMain();
                    sendEdi.StrInShipmentId = strShipmentId;
                    sendEdi.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("调用上传EDI程序集失败");
            }
        }
    }
}
