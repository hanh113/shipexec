using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ReverseAC
{
    public class ReverseBll
    {
        /// <summary>
        /// 检查shipmentId/carton是否hold住了
        /// </summary>
        /// <param name="shipmentIdOrCartonId"></param>
        /// <param name="outMessage">错误消息提示</param>
        /// <returns>hold:True, unHold:false</returns>
        public static bool CheckHold(string shipmentIdOrPalletOrOtherSN, out string errorMessage)
        {
            errorMessage = string.Empty;
            bool returnFlag = false;
            ReverseDal reservelDal = new ReverseDal();
            //检查这个Shipment 是否属于Hold 状态 只有做
            int shipmentHoldCount = reservelDal.CheckShipmentHold(shipmentIdOrPalletOrOtherSN);
            if (shipmentHoldCount > 0)
            {
                errorMessage = "发现被Hold的集货单!";
                returnFlag = true;
            }
            int otherSN = reservelDal.CheckOtherSnHold(shipmentIdOrPalletOrOtherSN);
            if (otherSN > 0)
            {
                errorMessage = "发现被Hold产品:" + shipmentIdOrPalletOrOtherSN;
                returnFlag = true;
            }
            //int cartonInPalletOfCartonHoldCount = reservelDal.CheckCartonInPalletOfCartonHold(shipmentIdOrPalletOrOtherSN);
            //if (cartonInPalletOfCartonHoldCount > 0)
            //{
            //    errorMessage = "发现Carton的Pallet内有Hold产品！";
            //    returnFlag = true;
            //}
            //int cartonInPalletCount = reservelDal.CheckCartonInPalletHold(shipmentIdOrPalletOrOtherSN);
            //if (cartonInPalletCount > 0)
            //{
            //    errorMessage = "发现Pallet内有Hold产品！";
            //    returnFlag = true;
            //}
            ////真实Pallet检查
            //if (shipmentIdOrPalletOrOtherSN.StartsWith("H"))
            //{
            //    int cartonInReadPalletCount = reservelDal.CheckCartonInReadPalletHold(shipmentIdOrPalletOrOtherSN);
            //    if (cartonInReadPalletCount > 0)
            //    {
            //        errorMessage = "发现Pallet内有Hold产品！";
            //        returnFlag = true;
            //    }
            //}
            ////检查Shipment内是否有Hold产品
            //int cartonInShipmentCount = reservelDal.CheckCartonInShipmentHold(shipmentIdOrPalletOrOtherSN) ;
            //if (cartonInShipmentCount > 0)
            //{
            //    errorMessage = "发现Shipment内有Hold产品！";
            //    returnFlag = true;
            //}
            if (returnFlag)
            {
                LibHelperAC.MediasHelper.PlaySoundAsyncByHold();
            }
            return returnFlag;
        }

        public static bool CheckHold(string Sno,string Type, out string errorMessage)
        {



            errorMessage = string.Empty;
            //bool returnFlag = false;
            ReverseDal reservelDal = new ReverseDal();
            errorMessage = reservelDal.CheckHoldByProcedure(Sno, Type, out errorMessage);
            
            if (!errorMessage.Equals("OK"))
            {
                LibHelperAC.MediasHelper.PlaySoundAsyncByHold();
                return false;
            }
            errorMessage = "OK";
            return true;
            
        }

        /// <summary>
        /// 获取所有Hold的信息
        /// </summary>
        public DataTable GetHoldByAll()
        {
            ReverseDal reservelDal = new ReverseDal();
            DataTable dataTable = reservelDal.GetAllHoldInfo();
            return dataTable;
        }

        /// <summary>
        /// UnHold选中了的行项目
        /// </summary>
        /// <param name="holdInfo">选中了的行项目</param>
        /// <param name="outMessageStr">错误消息</param>
        /// <returns>True:UnHold成功,False:UnHold失败</returns>
        public bool SetSelectedUnHold(DataTable holdInfo, out string outMessageStr)
        {
            bool flag = true;
            outMessageStr = string.Empty;
            DataRow[] drInfoArry = holdInfo.Select("Sel = True");
            if (drInfoArry.Count() > 0)
            {
                foreach (DataRow drInfo in drInfoArry)
                {
                    if (drInfo["Status"].ToString() == "Hold")
                    {
                        //产品Hold
                        SetProductUnHold(drInfo, out outMessageStr);
                        if (!flag)
                        {
                            break;
                        }
                    }
                    else
                    {
                        //shipment Hold
                        flag = SetShipmentUnHold(drInfo, out outMessageStr);
                        if (!flag)
                        {
                            break;
                        }
                    }
                }
            }
            return flag;
        }

        /// <summary>
        /// 获取ShipmentId的Info内容
        /// </summary>
        /// <param name="shipmentId"></param>
        /// <returns></returns>
        public DataTable GetInfoByShipmentId(string shipmentId)
        {
            ReverseDal reservelDal = new ReverseDal();
            DataTable dataTable = reservelDal.GetInfoByShipmentId(shipmentId);
            return dataTable;
        }

        /// <summary>
        /// 获取CartonNo的Info内容
        /// </summary>
        /// <param name="cartonNo"></param>
        /// <returns></returns>
        public DataTable GetInfoByCartonNo(string cartonNo)
        {
            ReverseDal reservelDal = new ReverseDal();
            DataTable dataTable = reservelDal.GetInfoByCartonNo(cartonNo);
            return dataTable;
        }

        /// <summary>
        /// UnShip 选中的shipment/pallet
        /// </summary>
        /// <param name="infoDataTable"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool SetSelectedUnShip(DataTable infoDataTable, out string errorMessage)
        {
            errorMessage = string.Empty;
            bool flag = true;
            ReverseDal reverseDal = new ReverseDal();
            //删除已经包含整单的Carton出货
            List<string> motherShipmenIdList = (from d in infoDataTable.AsEnumerable()
                                               .Where(p => p.Field<string>("SEL") == "True"
                                                        && p.Field<string>("CARTON_NO") == null)
                                                select d.Field<string>("SHIPMENT_ID")).ToList();
            foreach (string motherShipmentId in motherShipmenIdList)
            {
                for (int i = infoDataTable.Rows.Count - 1; i >= 0; i--)
                {
                    if (infoDataTable.Rows[i]["SHIPMENT_ID"].ToString() == motherShipmentId &&
                        !string.IsNullOrEmpty(infoDataTable.Rows[i]["CARTON_NO"].ToString()))
                    {
                        infoDataTable.Rows.RemoveAt(i);
                    }
                }
            }
            //获取选中的shipmentId
            List<string> shipmentIdList = (from d in infoDataTable.AsEnumerable()
                                          .Where(p => p.Field<string>("SEL") == "True")
                                           select d.Field<string>("SHIPMENT_ID")
                                           ).Distinct()
                                            .ToList();
            //检查shipment是否已经上传
            for (int i = 0; i < shipmentIdList.Count(); i++)
            {
                int rowCount = reverseDal.GetUpdateEdiCount(shipmentIdList[i]);
                if (rowCount > 0)
                {
                    flag = false;
                    errorMessage = shipmentIdList[i] + "已经上传EDI，无法取消出货";
                    return flag;
                }
            }
            //整单 UnShip
            DataRow[] unShipArry = infoDataTable.Select("SEL = True AND CARTON_NO is null");
            foreach (DataRow dr in unShipArry)
            {
                if (string.IsNullOrEmpty(dr["Carton_No"].ToString()))
                {
                    UnShipByShipmentId(dr["Shipment_Id"].ToString(), reverseDal);
                }
                //else
                //{
                //    //
                //}
            }
            //整栈板 UnShip
            //List<string> palletNoList = (from d in infoDataTable.AsEnumerable()
            //                             .Where(p => p.Field<string>("SEL") == "True"
            //                                      && p.Field<string>("CARTON_NO") != null) 
            //                                    select d.Field<string>("Pallet_No")).ToList();
            //List<string> unShipPalletNoList = palletNoList.Distinct().ToList();
            var palletNoList = (from d in infoDataTable.AsEnumerable()
                              .Where(p => p.Field<string>("SEL") == "True"
                                       && p.Field<string>("CARTON_NO") != null)
                                select new
                                {
                                    shipmentId = d.Field<string>("SHIPMENT_ID"),
                                    palletNo = d.Field<string>("PALLET_NO")
                                })
                               .Distinct()
                               .ToList();
            foreach (var palletNo in palletNoList)
            {
                //通过pallet获取shipmentId 
                UnShipByPallet(palletNo.shipmentId.ToString(), palletNo.palletNo.ToString(), reverseDal);
            }
            return true;
        }

        /// <summary>
        /// 通过物料获取箱号
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public DataTable GetCartonByMaterial(string material)
        {
            ReverseDal reverseDal = new ReverseDal();
            return reverseDal.GetCartonByMaterial(material);
        }
        /// <summary>
        /// 通过箱号获取箱号
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public DataTable GetCartonByCarton(string material)
        {
            ReverseDal reverseDal = new ReverseDal();
            return reverseDal.GetCartonByCarton(material);
        }

        /// <summary>
        /// 更新储位
        /// </summary>
        /// <param name="carton"></param>
        /// <param name="location"></param>
        public bool UpdateLocation(string carton, string location, out string errorMessage)
        {
            //Location储位
            string holdLocation = "MES测试";
            ReverseDal reverseDal = new ReverseDal();
            errorMessage = string.Empty;
            bool flag = true;
            //检查是否Hold了
            int holdCount = reverseDal.CheckOtherSnHold(carton);
            if (holdCount > 0 && location != holdLocation)
            {
                errorMessage = "只能移入[Hold]储位";
                return false;
            }
            else if (holdCount <= 0 && location == holdLocation)
            {
                errorMessage = "正常产品不能移入[Hold]储位";
                return false;
            }
            DataTable dataTable = reverseDal.GetLocationNoByLocationName(location);
            string locationId = string.Empty;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                locationId = dataTable.Rows[0][1].ToString();
            }
            reverseDal.UpdateLocationByCartonNo(carton, locationId);
            return flag;
        }

        /// <summary>
        /// 检查是否在正常单据中
        /// </summary>
        /// <param name="carton"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool CheckCartonAlready(string carton, out string errorMessage)
        {
            //检查是否在正常单据中
            ReverseDal reverseDal = new ReverseDal();
            errorMessage = "";
            int rowCount = reverseDal.CheckAlready(carton);
            if (rowCount > 0)
            {
                errorMessage = "非UnShip产品无法转移储位";
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 检查箱号与仓库是否匹配
        /// </summary>
        /// <param name="carton">箱号</param>
        /// <param name="location">储位</param>
        /// <param name="errorMessage">错误信息</param>
        /// <returns>True:匹配,False:不匹配</returns>
        public bool CheckCartonLocation(string carton, string location, out string errorMessage)
        {
            //Location储位
            string holdLocation = "MES测试";
            ReverseDal reverseDal = new ReverseDal();
            errorMessage = string.Empty;
            bool flag = true;
            //检查是否Hold了
            int holdCount = reverseDal.CheckOtherSnHold(carton);
            if (holdCount > 0 && location != holdLocation)
            {
                errorMessage = "只能移入[Hold]储位";
                return false;
            }
            else if (holdCount <= 0 && location == holdLocation)
            {
                errorMessage = "正常产品不能移入[Hold]储位";
                return false;
            }
            return flag;
        }

        public bool AddDgvCartonInfo(string cartonNo, ref DataTable dgvCartonInfo, out string erroerMessage)
        {
            erroerMessage = string.Empty;
            int rowCount = 0;
            //检查是否已经扫描
            if (dgvCartonInfo != null && dgvCartonInfo.Rows.Count > 0)
            {
                int duplicateCount = dgvCartonInfo.Select("CARTON_NO = '" + cartonNo + "'").Count();
                if (duplicateCount > 0)
                {
                    erroerMessage = "已经扫描";
                    return false;
                }
                rowCount = dgvCartonInfo.Rows.Count;
            }
            else
            {
                dgvCartonInfo = new DataTable();
                dgvCartonInfo.Columns.Add("ROWNUM");
                dgvCartonInfo.Columns.Add("PART_NO");
                dgvCartonInfo.Columns.Add("Carton_No");
                dgvCartonInfo.Columns.Add("RC_NO");
            }
            //检查箱号是否存在
            DataTable dtCartonInfo = GetCartonByCarton(cartonNo);
            if (dtCartonInfo == null || dtCartonInfo.Rows.Count <= 0)
            {
                erroerMessage = "箱号不存在";
                return false;
            }
            //插入列
            int rowSeq = rowCount + 1;
            DataRow dr = dtCartonInfo.Rows[0];
            dr[0] = rowSeq;
            dgvCartonInfo.Rows.Add(dr.ItemArray);
            return true;
        }

        /// <summary>
        /// 检查储位是否存在
        /// </summary>
        /// <param name="location">储位</param>
        /// <param name="errorMessage">错误消息</param>
        /// <returns>True:存在，False:不存在</returns>
        public bool CheckLocationExist(string location, out string errorMessage)
        {
            errorMessage = string.Empty;
            ReverseDal reverseDal = new ReverseDal();
            DataTable dataTable = reverseDal.GetLocationNoByLocationName(location);
            if (dataTable == null || dataTable.Rows.Count <= 0)
            {
                errorMessage = "储位不存在";
                return false;
            }
            else
            {
                return true;
            }
        }

        #region Function
        /// <summary>
        /// UnHold Shipment
        /// </summary>
        /// <param name="drInfo">UnHold的内容</param>
        /// <param name="outMessageStr">错误消息</param>
        /// <returns>True:UnHold成功,False:UnHold失败</returns>
        private bool SetShipmentUnHold(DataRow drInfo, out string outMessageStr)
        {
            bool flag = true;
            outMessageStr = string.Empty;
            string shipemtnId = drInfo["SHIPMENT_ID"].ToString();
            try
            {
                ReverseDal reserveDal = new ReverseDal();
                reserveDal.SetShipmentUnHold(shipemtnId);
            }
            catch
            {
                outMessageStr = string.Format("UnHold {0} 失败", drInfo["Shipment_id"].ToString());
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// UnHold Product
        /// </summary>
        /// <param name="drInfo">UnHold的内容</param>
        /// <param name="outMessageStr">错误消息</param>
        /// <returns>True:UnHold成功,False:UnHold失败</returns>
        private bool SetProductUnHold(DataRow drInfo, out string outMessageStr)
        {
            bool flag = true;
            outMessageStr = string.Empty;
            string shipemtnId = drInfo["SHIPMENT_ID"].ToString();
            string cartonNo = drInfo["CARTON_NO"].ToString();
            try
            {
                ReverseDal reserveDal = new ReverseDal();
                reserveDal.SetProductUnHold(shipemtnId, cartonNo);
            }
            catch
            {
                outMessageStr = string.Format("UnHold {0} {1} 失败", shipemtnId, cartonNo);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 整单取消出货
        /// </summary>
        /// <param name="shipmentId"></param>
        /// <param name="reverseDal"></param>
        /// <returns></returns>
        private bool UnShipByShipmentId(string shipmentId, ReverseDal reverseDal)
        {
            //shiment站点
            reverseDal.UpdatePalletFlagByShipmentId(shipmentId);
            reverseDal.UpdateDdLine(shipmentId);
            reverseDal.UpdateDnLineByShpment(shipmentId);
            //称重站点
            reverseDal.DelWeight(shipmentId);
            reverseDal.DelWeightList(shipmentId);
            //检查站点
            //reverseDal.UpdateCheckFlagByShipmentId(shipmentId);

            //Pack站点
            reverseDal.DelPackPrintRecord(shipmentId);
            reverseDal.UpdateSnStatus(shipmentId);
            reverseDal.DelScanDetail(shipmentId);
            reverseDal.DelScan(shipmentId);
            reverseDal.DelLpsLine(shipmentId);
            reverseDal.UpdatePalletScanQTY(shipmentId);
            reverseDal.UpdatePickScanQty(shipmentId);
            //Pick站点
            reverseDal.UpdatePickQtyAndLineNo(shipmentId);
            reverseDal.DelPickList(shipmentId);
            return true;
        }

        private bool UnShipByPallet(string shipmentId, string palletNo, ReverseDal reverseDal)
        {
            //获取栈板的Carton数量
            int GetPalletCartonCount = reverseDal.GetPalletCartonCount(shipmentId, palletNo);
            //shipment站点
            reverseDal.UpdatePalletFlagByPalletNo(palletNo);
            reverseDal.UpdateDdLine(shipmentId);
            reverseDal.UpdateDnLineByShpment(shipmentId);
            //称重站点
            reverseDal.DelWeight(shipmentId, palletNo);
            reverseDal.DelWeightList(shipmentId, palletNo);
            //Pack站点
            reverseDal.UpdatePackPrintRecord(shipmentId, palletNo);
            reverseDal.UpdateSnStatus(shipmentId, palletNo);
            reverseDal.DelLpsLine(shipmentId, palletNo);
            reverseDal.DelScanDetail(shipmentId, palletNo);
            reverseDal.DelScan(shipmentId, palletNo);
            reverseDal.UpdatePallet(palletNo);
            //Pick站点
            reverseDal.UpdatePickQtyAndLineNo(shipmentId, palletNo);
            reverseDal.DelPickList(shipmentId, palletNo);
            return true;
        }
        #endregion
    }
}
