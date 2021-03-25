using ClientUtilsDll;
using LibHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shipment
{
    public class ShipmentBll
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
            ShipmentDal shipmentDal = new ShipmentDal();
            DataSet dataSet = shipmentDal.GetCarList(startDate, endDate);
            //HYQ：下面赋值有问题 开始运行时 就跑了2次， 第二次报错了。
            //DataTable dataTable = new DataTable();
            //dataTable.Clear();
            //dataTable = dataSet.Tables[0];

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
        public DataTable GetShipmentInfoDataTable(string carNo, string shipmentid)
        {
            if (string.IsNullOrEmpty(carNo) || string.IsNullOrEmpty(shipmentid)) { return null; }
            ShipmentDal shipmentDal = new ShipmentDal();
            DataSet dataSet = shipmentDal.GetShipmentInfoDataTable(carNo, shipmentid);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public DataTable GetShipmentAlreadyInfoDataTable(string carNo, string shipmentid)
        {
            if (string.IsNullOrEmpty(carNo) || string.IsNullOrEmpty(shipmentid)) { return null; }
            ShipmentDal shipmentDal = new ShipmentDal();
            DataSet dataSet = shipmentDal.GetShipmentAlearyInfoDataTable(carNo, shipmentid);
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
        public bool ScanPalletNo(DataTable dataTable, string palletNo, string strCarNO, out string strMessage, out int ScanRowNum, string strLocalMACADDRESS)
        {
            ScanRowNum = 0;
            strMessage = string.Empty;
            bool isExistPallet = false;
            int rowNum = 0;
            ShipmentDal shipmentDal = new ShipmentDal();

            //判断是否已经扫描 ，通过查shipment_flag
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
                if (palletNo == dataRow["pallet_no"].ToString())//存在栈板号
                {
                    if (!dataRow["sncount"].ToString().Equals(dataRow["qty"].ToString()))
                    {
                        strMessage = "栈板内CSN 序号站别存在非[W4]，异常";
                        return false;
                    }
                    if (string.IsNullOrEmpty(dataRow["car_no"].ToString()))
                    {
                        strMessage = "栈板号对应货运车牌号没有OMS维护，异常";
                        return false;
                    }


                    //增加关务临时更改车防呆
                    DataTable dtTrmp22 = shipmentDal.GetPalletCarInfoScan(palletNo.Trim());
                    if (dtTrmp22.Rows.Count != 1)
                    {
                        strMessage = "栈板维护车号对应异常";
                        return false;
                    }
                    if (dtTrmp22.Rows[0]["CAR_NO"].ToString().Trim() != strCarNO)
                    {
                        strMessage = "车号异常，请检查是否关务有车更新！";
                        return false;
                    }


                    ScanRowNum = rowNum;
                    isExistPallet = true;

                    //HYQ：更新pallet表标记

                    // shipmentDal.UpdatePalletFlagandWC(palletNo);
                    //shipmentDal.UpdatePalletFlagandWC(palletNo);
                    string strResult = string.Empty;
                    string strResultMsg = string.Empty;
                    strResult = shipmentDal.UpdatePalletFlagandWCBySP(palletNo, out strResultMsg);
                    if (strResult.Equals("NG"))
                    {
                        return false;
                    }
                    //HYQ：insert log
                    string errmsg = string.Empty;
                    ShipmentDal sd = new ShipmentDal();
                    string strRB = sd.PPSInsertWorkLogByProcedure(palletNo, "SHIPMENT", strLocalMACADDRESS, out errmsg);
                    if (strRB.Equals("NG"))
                    {
                        return false;
                    }


                    //获取未扫描栈板数 
                    string shipmentId = dataRow["shipment_id"].ToString();
                    int remainNum = shipmentDal.GetShipmentRemainNum(shipmentId);
                    int remainNumCar = shipmentDal.GetCarRemainNum(palletNo);
                    if (remainNum <= 0)
                    {
                        //HYQ:20181214 需要增加update shipment_info status`

                        shipmentDal.UpdateShipmentStatus(shipmentId);

                        //HYQ: 这里可以打印水晶报表了 handoverManifest  20191010 CarBillList
                        if (remainNumCar <= 0)
                        {
                            CRReport.CRMain cr = new CRReport.CRMain();
                            //20191010 CarBillList
                            //cr.HanDoveMan2(shipmentId, true, true);
                            cr.HanDoveMan2(shipmentId, true, true, "", "", "");
                            ClientUtils.WritePrintLabelLog("Loading Truck Manifest", "", palletNo, LocalHelper.getMacAddr_Local());

                            //还有没装完的给个提示
                            if (GetCombineCarInfo(strCarNO, shipmentId))
                            {
                                MessageBox.Show(string.Format("此车:{0} 还存在AC的货未装完，请注意装车！".TP(), strCarNO));
                            }
                        }
                        ////更新DDLine状态
                        //shipmentDal.UpdateDDLineStatus(shipmentId);
                        ////更新DNLine状态
                        //shipmentDal.UpdateDNLineStatus(shipmentId);
                        ////更新WeightList标识
                        //shipmentDal.UpdateWeightListFlag(shipmentId);
                        ////调用上传EDI界面
                        //CallOtherWindow(shipmentId);
                        return true;
                    }
                    else
                    {
                        //MessageBox.Show("最后的节点,不是想要的测试结果");
                        //return false;
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
        /// 看是否有AC和EDI维护在同一辆车上
        /// </summary>
        private bool GetCombineCarInfo(string carno, string strShipmentID)
        {
            ShipmentDal dal = new ShipmentDal();
            return dal.GetCombineCarInfo(carno, strShipmentID);
        }

        /// <summary>
        /// 调用EDI上传界面
        /// </summary>
        //public static void CallOtherWindow(string strShipmentId)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Shipment已经完成扫描，是否上传EDI?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //        {
        //            Sentdirectedi.fMain sendEdi = new Sentdirectedi.fMain();
        //            sendEdi.StrInShipmentId = strShipmentId;
        //            sendEdi.ShowDialog();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("调用上传EDI程序集失败" + ex);
        //    }
        //}

        public string PPSInsertWorkLogBy(string insn, string inwc, string macaddress, out string errmsg)
        {

            errmsg = string.Empty;
            ShipmentDal sd = new ShipmentDal();
            string strRB = sd.PPSInsertWorkLogByProcedure(insn, inwc, macaddress, out errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }


        public bool CheckShipmentIDHold(string insn, out string errmsg)
        {
            //insn 是PACKPALLETNO
            ShipmentDal sd = new ShipmentDal();
            string strRB = sd.CheckShipmentIDHoldByProcedure(insn, out errmsg);
            if (strRB.Equals("NG"))
            {
                return false;
            }
            errmsg = "OK";
            return true;
        }

        public void fillCmb(string strSQL, string colName, ComboBox cmb)
        {

            DataSet dts = ClientUtils.ExecuteSQL(strSQL);
            if (dts.Tables[0].Rows.Count > 0)
            {
                cmb.DataSource = dts.Tables[0];
                cmb.ValueMember = "id";
                cmb.DisplayMember = "name";
            }
            else
            {
                cmb.Items.Clear();
            }
        }


        public DataTable GetCarInfoDataTable(string starttime, string endtime, string truckno)
        {
            if (string.IsNullOrEmpty(truckno)) { return null; }
            ShipmentDal shipmentDal = new ShipmentDal();
            DataSet dataSet = shipmentDal.GetCarInfoDataTablebySQL(starttime, endtime, truckno);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }


        public bool getPalletsQtyByTruck(string starttime, string endtime, string truckno, out string allreturnlist, out string errmsg)
        {
            ShipmentDal sd = new ShipmentDal();
            string strRB = sd.getPalletsQtyByTruckByProcedure(starttime, endtime, truckno, out allreturnlist, out errmsg);
            if (strRB.Equals("NG"))
            {
                return false;
            }
            errmsg = "OK";
            return true;
        }

        public bool InertTruckConfirm(string starttime, string endtime, string truckno, string whoconfirm, string passw, out string errmsg)
        {
            ShipmentDal sd = new ShipmentDal();
            string strRB = sd.TruckConfirmByProcedure(starttime, endtime, truckno, whoconfirm, passw, out errmsg);
            if (strRB.Equals("NG"))
            {
                return false;
            }
            errmsg = "OK";
            return true;
        }

        public string GetNoCarPalletCount(string strsid)
        {
            if (string.IsNullOrEmpty(strsid)) { return null; }
            ShipmentDal shipmentDal = new ShipmentDal();
            DataSet dataSet = shipmentDal.GetNoCarPalletCountbySQL(strsid);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return "";
            }
            else
            {
                return dataSet.Tables[0].Rows[0]["palletcount"].ToString();
            }
        }

        public DataTable GetToDayShipmentByCar(string carNo)
        {
            DataTable dt = new DataTable();
            if (!String.IsNullOrWhiteSpace(carNo))
                dt = new ShipmentDal().GetToDayShipmentByCar(carNo);
            return dt;
        }

        public bool IsValidPalletLoadTruck(string palletNo, out string msg)
        {
            msg = "";
            ShipmentDal dal = new ShipmentDal();
            int c = dal.GetPalletBeforeLoad(palletNo);
            var res = c > 0;
            if (res)
                msg = palletNo + "栈板不是优先的栈板";
            return !res;
        }
    }

}

