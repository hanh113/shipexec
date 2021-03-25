using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wmsReport.DataGetWays;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using wmsReport.Entitys;
using CRReport.CRfrom;
using System.IO;
using System.Windows.Forms;
using System.Net.Http;
using System.Configuration;
using System.Net;
using Newtonsoft.Json.Linq;
using wmsReport.WebApi;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace wmsReport.Core
{
    class Controller
    {
        private GetDatas getDatas;
        public Controller()
        {
            getDatas = new GetDatas();
        }
        public ExecuteResult queryShipmentInfoByShipmentId(string shipmentId, bool isQueryQAHold)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = getDatas.queryShipmentInfoByShipmentId(shipmentId, isQueryQAHold);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此集货单号：" + shipmentId + " 查询不到信息！";
            }
            return exeRes;
        }

        public ExecuteResult getTransferLableContentStr(string locationNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = getDatas.getTransferLableContent(locationNo);
            string allWorkStr = locationNo + "|";
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    string palletNo = dt.Rows[i]["pallet_no"].ToString();
                    string partNo = dt.Rows[i]["part_no"].ToString();
                    string MPN_ = dt.Rows[i]["MPN"].ToString();
                    string perQty = dt.Rows[i]["PER_QTY"].ToString();
                    allWorkStr += palletNo + "|" + MPN_ + "|" + partNo + "|" + perQty + "|";
                }
                exeRes.Anything = allWorkStr;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此储位:" + locationNo + "无资料，请检查！";
            }
            return exeRes;
        }

        public ExecuteResult queryCartonInfoByCartonNo(string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = getDatas.queryCartonInfoByCartonNo(cartonNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此箱号：" + cartonNo + " 查询不到信息！";
            }
            return exeRes;
        }

        public ExecuteResult locationNoTransformLocationIdByLocationNo(string locationNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = getDatas.locationNoTransformLocationIdByLocationNo(locationNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此储位号：" + locationNo + " 查询不到信息！";
            }
            return exeRes;
        }


        public ExecuteResult queryPalletNoInfoByPackPalletNo(string packPalletNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = getDatas.queryPalletNoInfoByPackPalletNo(packPalletNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此栈板号：" + packPalletNo + " 查询不到信息！";
            }
            return exeRes;
        }
        public ExecuteResult queryShipmentIdInfoByShipmentTime(string startTime, string endTime, string shipMentId, string shipment_type, string shipment_region)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = getDatas.queryShipmentIdInfoByShipmentTime(startTime, endTime, shipMentId, shipment_type, shipment_region);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
                exeRes.Message = "信息查询成功,共计" + dt.Rows.Count + "条！";
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "查询不到信息！";
            }
            return exeRes;
        }
        public ExecuteResult queryMoreInfoByMultiOption(string Mes_pallet, string pps_pallet, string pick_pallet, string deliveryNo, string locationNo, string ictPn, string cartonNo, string ssccCode, string shipmentId)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = getDatas.queryMoreInfoByMultiOption(Mes_pallet, pps_pallet, pick_pallet, deliveryNo, locationNo, ictPn, cartonNo, ssccCode, shipmentId);
            if (dt != null && dt.Rows.Count > 0)
            {

                exeRes.Anything = dt;
                exeRes.Message = "查询成功，共计" + dt.Rows.Count + "条！";
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "查询无资料,请检查！";
            }
            return exeRes;
        }

        public ExecuteResult queryDnInfoByShipmentId(string shipmentId)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = getDatas.queryDnInfoByShipmentId(shipmentId);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "查询失败-没有过站记录！";
            }
            return exeRes;
        }

        public ExecuteResult getPassStationLogByCarton(string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = getDatas.getPassStationLogByCarton(cartonNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
                exeRes.Message = "查询成功-过站记录！";
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "查询失败-没有过站记录！";
            }
            return exeRes;
        }
        public ExecuteResult queryQAholdCartonsByShipmentId(string shipmentId)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = getDatas.queryQAholdCartonsByShipmentId(shipmentId);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "";
            }
            return exeRes;
        }
        public ExecuteResult checkIsPrintDeliveryNote(string closeTime)
        {
            if (File.Exists(Application.StartupPath + @"\pdf\dn_log.txt"))
            {
                File.Delete(Application.StartupPath + @"\pdf\dn_log.txt");
            }
            ExecuteResult exeRes = new ExecuteResult();
            int count = 0;
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            List<ShipmentInfo> shipmentInfoList = new List<ShipmentInfo>();
            exeRes = getAllShipmentIdByCloseTime(closeTime);
            if (exeRes.Status)
            {
                shipmentInfoList = (List<ShipmentInfo>)exeRes.Anything;
                for (int i = 0; i < shipmentInfoList.Count; i++)
                {
                    if (shipmentInfoList[i].TYPE.Equals("PARCEL"))
                    {
                        dt = getDatas.getAllDeliveryNoteByShipmentId(shipmentInfoList[i].ShipmentId);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                string dn = dt.Rows[j]["DELIVERY_NO"].ToString();
                                string lineItem = dt.Rows[j]["line_item"].ToString();
                                dt2 = getDatas.getT940InfoByDeliveryNo(dn);
                                if (dt2 != null && dt2.Rows.Count > 0)
                                {
                                    string shipCnCode = dt2.Rows[0]["SHIPCNTYCODE"].ToString();
                                    string custGroup = dt2.Rows[0]["CUSTOMERGROUP"].ToString();
                                    if (shipCnCode.Equals("JP"))
                                    {
                                        exeRes = isPrintDeliveryNoteByDN_And_ShipOfType(dn, lineItem, shipmentInfoList[i].TYPE);
                                        if (!exeRes.Status)
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        if (custGroup.Equals("IN") || custGroup.Equals("RK") || custGroup.Equals("RW"))
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            exeRes = isPrintDeliveryNoteByDN_And_ShipOfType(dn, lineItem, shipmentInfoList[i].TYPE);
                                            if (!exeRes.Status)
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                }
                                string startPath = Application.StartupPath;
                                string secPathDir = startPath + @"\PDF\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + shipmentInfoList[i].CarrierCode + @"\" + shipmentInfoList[i].ShipmentId;
                                //+ @"\" + dn + ".pdf"
                                if (!Directory.Exists(secPathDir))
                                {
                                    Directory.CreateDirectory(secPathDir);
                                }
                                string PDFFilePath = secPathDir + @"\" + dn + ".pdf";
                                new D1_DeliveryNote(dn, lineItem, shipmentInfoList[i].ShipmentId, true, PDFFilePath);
                                count++;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        dt = getDatas.getAllDeliveryNoteByShipmentId(shipmentInfoList[i].ShipmentId);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                string dn = dt.Rows[j]["DELIVERY_NO"].ToString();
                                string lineItem = dt.Rows[j]["line_item"].ToString();
                                exeRes = isPrintDeliveryNoteByDN_And_ShipOfType(dn, lineItem, shipmentInfoList[i].TYPE);
                                if (!exeRes.Status)
                                {
                                    continue;
                                }
                                string startPath = Application.StartupPath;
                                string secPathDir = startPath + @"\PDF\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + shipmentInfoList[i].CarrierCode + @"\" + shipmentInfoList[i].ShipmentId;
                                //+ @"\" + dn + ".pdf"
                                if (!Directory.Exists(secPathDir))
                                {
                                    Directory.CreateDirectory(secPathDir);
                                }
                                string PDFFilePath = secPathDir + @"\" + dn + ".pdf";
                                new D1_DeliveryNote(dn, lineItem, shipmentInfoList[i].ShipmentId, true, PDFFilePath);
                                count++;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            if (count > 0)
            {
                exeRes.Message = "成功生成" + count + "条!";
            }
            else
            {
                exeRes.Message = "没有PDF 产生！";
                exeRes.Status = false;
            }
            return exeRes;
        }

        public ExecuteResult changeLocation(string[] inputData)
        {
            ExecuteResult exeRes = new ExecuteResult();
            try
            {
                //获取连线地址
                string sql = @"select para_value from t_basicparameter_info where para_type='TOPSTAR' and enabled='Y'";
                string url = ClientUtils.ExecuteSQL(sql).Tables[0].Rows[0]["para_value"].ToString();
                //尝试发送出库单请求
                string ret =  CallApi.postApi<string>("api/TopStar/PostOrderToTopStar", JsonConvert.SerializeObject(new OrderModel { TrayCodes = inputData }), url);
                //解析接收结果
                StockRes stockRes = JsonConvert.DeserializeObject<StockRes>(ret);
                exeRes.Status = stockRes.result.Equals(1);
                exeRes.Message = stockRes.message;
            }
            catch (Exception e)
            {
                exeRes.Status = false;
                exeRes.Message = string.Format("此：{0} 信息发送出库单！{1}", string.Join(",", inputData), e.Message);
            }
            return exeRes;
        }

        public ExecuteResult isPrintDeliveryNoteByDN_And_ShipOfType(string deliveryNote, string lineItem, string shipOfType)
        {
            DataTable dt = new DataTable();
            ExecuteResult exeRes = new ExecuteResult();
            dt = getDatas.isPrintDeliveryNoteByDN_And_ShipOfType(deliveryNote, lineItem, shipOfType);
            if (dt.Rows[0]["ALL_COUNT"].ToString().Equals("0"))
            {
                exeRes.Status = false;
            }
            return exeRes;
        }
        public ExecuteResult getAllShipmentIdByCloseTime(string closeTime)
        {
            ExecuteResult exeRes = new ExecuteResult();
            List<ShipmentInfo> shipmentInfoList = new List<ShipmentInfo>();
            DataTable dt = new DataTable();
            dt = getDatas.getAllShipmentIdByCloseTime(closeTime);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ShipmentInfo shipmentInfo = new ShipmentInfo
                    {
                        Region = dt.Rows[i]["region"].ToString(),
                        ShipmentType = dt.Rows[i]["shipment_type"].ToString(),
                        ShipmentId = dt.Rows[i]["SHIPMENT_ID"].ToString(),
                        TYPE = dt.Rows[i]["type"].ToString(),
                        CarrierCode = dt.Rows[i]["carrier_code"].ToString()
                    };
                    shipmentInfoList.Add(shipmentInfo);
                }
                exeRes.Anything = shipmentInfoList;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此closeTime：" + closeTime + " 查询不到信息！";
            }
            return exeRes;
        }

        public ExecuteResult getAllLocationInfo(bool isType)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = getDatas.getAllLocationInfo(isType);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
                exeRes.Message = "所有储位查询成功！";
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "所有储位查询失败！";
            }
            return exeRes;
        }


        public ExecuteResult getLocationInformationBylocationId(string locationId)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            dt = getDatas.getLocationInformationBylocationId(locationId);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
                exeRes.Message = "储位详细信息查询成功！";
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "储位详细信息查询失败！";
                //comment by wenxing 2021-2-5
                //dt1 = getDatas.getLocationInforBySYSlocationId(locationId);
                //if (dt1 != null && dt1.Rows.Count > 0)
                //{
                //    exeRes.Anything = dt1;
                //    exeRes.Message = "SYS储位详细信息查询成功！";
                //}
                //else
                //{
                //    exeRes.Status = false;
                //    exeRes.Message = "储位详细信息查询无资料！";
                //}    
            }
            return exeRes;
        }

        public ExecuteResult getInformationByInputData(string InputData)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = getDatas.getInformationByInputData(InputData);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
                exeRes.Message = "查询成功！";
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "查询失败！";
            }
            return exeRes;
        }

        public ExecuteResult checkIsTransfer(string inputData, string orgLocationNo, string tarLocationNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            object[][] procParams = new object[4][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "INPUTDATA", inputData };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "orgLocationNo", orgLocationNo };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "tarLocationNo", tarLocationNo };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "TMES", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.t_check_is_transfer", procParams);
            if (ds.Tables[0].Rows[0]["TMES"].ToString().Equals("OK"))
            {

            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = ds.Tables[0].Rows[0]["TMES"].ToString();
            }
            return exeRes;
        }
        public ExecuteResult fastSearchByCartonNo(string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            object[][] procParams = new object[4][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "V_CARTON_NO", cartonNo };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RET_TIER", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RET_CODE", "" };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RET_MSG", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.t_fast_search", procParams);
            if (ds.Tables[0].Rows[0]["RET_CODE"].ToString().Equals("1"))
            {
                dt = ds.Tables[0];
                exeRes.Anything = dt;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = ds.Tables[0].Rows[0]["RET_MSG"].ToString();
            }
            return exeRes;
        }
        public ExecuteResult changeLocation(string inputData, string orgLocationNo, string tarLocationNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            object[][] procParams = new object[4][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inputData", inputData };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "orgLocationNo", orgLocationNo };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "tarLocationNo", tarLocationNo };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "TMES", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.t_change_location", procParams);
            if (ds.Tables[0].Rows[0]["TMES"].ToString().Equals("OK"))
            {

            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = ds.Tables[0].Rows[0]["TMES"].ToString();
            }
            return exeRes;
        }
        public ExecuteResult ZFPassStation(string tt_id, string tt_order, string tt_ictpn, string tt_location, string tt_carton)
        {
            ExecuteResult exeRes = new ExecuteResult();
            object[][] procParams = new object[7][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "TT_ID", tt_id };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "TT_ORDER", tt_order };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "TT_ICTPN", tt_ictpn };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "TT_LOCATION", tt_location };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "TT_CARTON", tt_carton };
            procParams[5] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "TMES", "" };
            procParams[6] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "C_MSG", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.T_ZF_PASS_STATION", procParams);
            if (ds.Tables[0].Rows[0]["TMES"].ToString().Equals("OK"))
            {
                exeRes.Anything = ds.Tables[0];
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = ds.Tables[0].Rows[0]["TMES"].ToString();
            }
            return exeRes;
        }
        public ExecuteResult insertTTOrderInfo(string tt_order, string ictPn, int totalQty, string remark, string empNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            object[][] procParams = new object[16][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "TT_ORDER", tt_order };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ICTPN", ictPn };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "TOTALQTY", totalQty };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "REMARK_INFO", remark };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "EMP_NO", empNo };
            procParams[5] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "TMES", "" };
            procParams[6] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "O_ID", "" };
            procParams[7] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "O_TT_NO", "" };
            procParams[8] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "O_ICTPN", "" };
            procParams[9] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "O_TOTALQTY", "" };
            procParams[10] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "O_OUT_QTY", "" };
            procParams[11] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "O_REMARK", "" };
            procParams[12] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "O_CDT", "" };
            procParams[13] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "O_CREATE_EMP", "" };
            procParams[14] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "O_UDT", "" };
            procParams[15] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "O_UPDATE_EMP", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.t_insert_ZF_info", procParams);
            if (ds.Tables[0].Rows[0]["TMES"].ToString().Equals("OK"))
            {
                exeRes.Anything = ds.Tables[0];
                exeRes.Message = "提交成功！";
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = ds.Tables[0].Rows[0]["TMES"].ToString();
            }
            return exeRes;
        }

        public ExecuteResult getTTOrderInfoByConditions(string tt_order, string ictPn, string start_time, string end_time)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = getDatas.getTTOrderInfoByConditions(tt_order, ictPn, start_time, end_time);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
                exeRes.Message = "查询成功！";
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "查询失败！";
            }
            return exeRes;
        }

        public ExecuteResult checkIsExistIctPnOnLocationByLocationNoAndIctPn(string ictPn, string locationNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            exeRes = getDatas.checkIsExistIctPnOnLocationByLocationNoAndIctPn(ictPn, locationNo);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                if (dt.Rows.Count > 0 && dt != null)
                {
                    exeRes.Message = "库位输入正确，请输入箱号！";
                }
                else
                {
                    exeRes.Status = false;
                    exeRes.Message = "此库位没有料号-" + ictPn + " ，请检查，输入储位号是否正确！";
                }
            }
            return exeRes;
        }
        public ExecuteResult checkIsExistCartonNoIsLinkLocationNO(string cartonNo, string locationNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            exeRes = getDatas.checkIsExistCartonNoIsLinkLocationNO(cartonNo, locationNo);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                if (dt.Rows.Count > 0 && dt != null)
                {

                }
                else
                {
                    exeRes.Status = false;
                    exeRes.Message = "此箱号不在 " + locationNo + " 上，请检查，输入箱号是否正确！";
                }
            }
            return exeRes;
        }


        public ExecuteResult checkIsExistCartonNoIsLinkIctPn(string cartonNo, string ictPn)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            exeRes = getDatas.checkIsExistCartonNoIsLinkIctPn(cartonNo, ictPn);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                if (dt.Rows.Count > 0 && dt != null)
                {

                }
                else
                {
                    exeRes.Status = false;
                    exeRes.Message = "此箱号-料号不是 " + ictPn + " 料号，请检查，输入箱号是否正确！";
                }
            }
            return exeRes;
        }
    }
}
