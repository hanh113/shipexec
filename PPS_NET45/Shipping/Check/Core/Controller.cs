using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Check.Entitys;
using Check.Dao;
using System.Text.RegularExpressions;

namespace Check.Core
{
    class Controller
    {
        private DataGetWay dataGetWay;
        public Controller()
        {
            dataGetWay = new DataGetWay();
        }
        public ExecuteResult getShipMentInfoByshipmentId(string shipmentId)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = dataGetWay.getShipMentInfoByshipmentId(shipmentId);
            if (dt.Rows.Count > 0)
            {
                exeRes.Status = true;
                exeRes.Message = "集货单号正确，请输入Pick栈板号！";
                exeRes.Anything = dt;
            }
            else
            {

                exeRes.Status = false;
                exeRes.Message = "此集货单号:" + shipmentId + " 查询不到信息，请检查是否正确！";
            }
            return exeRes;
        }
        public ExecuteResult checkIsLinkPallet(ShipmentInfo shipmentInfo, string trackingNo, string pickPalletNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            //Regex.IsMatch(shipmentInfo.CarrierName, @"\w*UPS\w*")
            if (shipmentInfo.ShipmentType.Equals("FD"))
            {
                dt = dataGetWay.fd_checkIsLinkPallletNo(trackingNo, pickPalletNo);
                if (dt != null && dt.Rows.Count > 0)
                {
                    exeRes.Status = true;
                }
                else
                {
                    exeRes.Status = false;
                    exeRes.Message = "此 SSCC：" + trackingNo + "不属于此PickPalletNo,请检查输入是否正确！";
                }
            }
            else
            {
                if (shipmentInfo.TYPE.Equals("PARCEL"))
                {
                    if (Regex.IsMatch(shipmentInfo.CarrierName, @"\w*UPS\w*") || Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*UPS\w*"))
                    {
                        dt = dataGetWay.ds_checkIsLinkPallletNoUpsYmtCnpl(trackingNo, pickPalletNo);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            exeRes.Status = true;
                        }
                        else
                        {
                            exeRes.Status = false;
                            exeRes.Message = "此 TrackingNo：" + trackingNo + "不属于此PickPalletNo,请检查输入是否正确！";
                        }
                    }
                    else if (Regex.IsMatch(shipmentInfo.CarrierName, @"\w*YMT\w*") || Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*YMT\w*"))
                    {
                        dt = dataGetWay.ds_checkIsLinkPallletNoUpsYmtCnpl(trackingNo, pickPalletNo);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            exeRes.Status = true;
                        }
                        else
                        {
                            exeRes.Status = false;
                            exeRes.Message = "此 TrackingNo：" + trackingNo + "不属于此PickPalletNo,请检查输入是否正确！";
                        }
                    }
                    else if (Regex.IsMatch(shipmentInfo.CarrierName, @"\w*CNPL\w*") || Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*CNPL\w*"))
                    {
                        dt = dataGetWay.ds_checkIsLinkPallletNoUpsYmtCnpl(trackingNo, pickPalletNo);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            exeRes.Status = true;
                        }
                        else
                        {
                            exeRes.Status = false;
                            exeRes.Message = "此 TrackingNo：" + trackingNo + "不属于此PickPalletNo,请检查输入是否正确！";
                        }
                    }
                    else if (Regex.IsMatch(shipmentInfo.CarrierName, @"\w*SF\w*") || Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*SF\w*"))
                    {
                        dt = dataGetWay.ds_checkIsLinkPallletNoDhlSf(trackingNo, pickPalletNo);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            exeRes.Status = true;
                        }
                        else
                        {
                            exeRes.Status = false;
                            exeRes.Message = "此 TrackingNo：" + trackingNo + "不属于此PickPalletNo,请检查输入是否正确！";
                        }
                    }
                    else if (Regex.IsMatch(shipmentInfo.CarrierName, @"\w*DHL\w*") || Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*DHL\w*"))
                    {
                        dt = dataGetWay.ds_checkIsLinkPallletNoDhlSf(trackingNo.Substring(1), pickPalletNo);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            exeRes.Status = true;
                        }
                        else
                        {
                            exeRes.Status = false;
                            exeRes.Message = "此 TrackingNo：" + trackingNo + "不属于此PickPalletNo,请检查输入是否正确！";
                        }

                    }
                    else if (Regex.IsMatch(shipmentInfo.CarrierName, @"\w*TNT\w*") || Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*TNT\w*"))
                    {
                        dt = dataGetWay.ds_checkIsLinkPallletNoTnt(trackingNo, pickPalletNo);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            exeRes.Status = true;
                        }
                        else
                        {
                            exeRes.Status = false;
                            exeRes.Message = "此 TrackingNo：" + trackingNo + "不属于此PickPalletNo,请检查输入是否正确！";
                        }
                    }
                    else
                    {
                        exeRes.Status = false;
                        exeRes.Message = "PARCEL-未知carrier：" + shipmentInfo.CarrierName + ",请联系IT-PPS! ";
                    }
                }
                else
                {
                    dt = dataGetWay.fd_checkIsLinkPallletNo(trackingNo, pickPalletNo);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        exeRes.Status = true;
                    }
                    else
                    {
                        exeRes.Status = false;
                        exeRes.Message = "此 SSCC：" + trackingNo + "不属于此PickPalletNo,请检查输入是否正确！";
                    }
                }
            }
            return exeRes;
        }

        public bool GetSFShiptoCNTYCODE(string shipMentId)
        {
            DataTable dtTemp = dataGetWay.GetSFShiptoCNTYCODE(shipMentId);
            if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ExecuteResult checkIsLinkCarton(ShipmentInfo shipmentInfo, string trackingNo, string cartonNo, bool isDsPACShippingLabel)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            //Regex.IsMatch(shipmentInfo.CarrierName, @"\w*UPS\w*")
            if (shipmentInfo.ShipmentType.Equals("FD"))
            {
                dt = dataGetWay.fd_checkIsLinkCartonNo(trackingNo, cartonNo);
                if (dt != null && dt.Rows.Count > 0)
                {
                    exeRes.Status = true;
                }
                else
                {
                    exeRes.Status = false;
                    exeRes.Message = "此箱号：" + cartonNo + "和TrackingNo不符合,请检查输入是否正确！";
                }
            }
            else
            {
                if (shipmentInfo.TYPE.Equals("PARCEL"))
                {
                    if (Regex.IsMatch(shipmentInfo.CarrierName, @"\w*UPS\w*") || Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*UPS\w*"))
                    {
                        dt = dataGetWay.ds_checkIsLinkCartonNoNoUpsYmtCnpl(trackingNo, cartonNo);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            exeRes.Status = true;
                        }
                        else
                        {
                            exeRes.Status = false;
                            exeRes.Message = "此箱号：" + cartonNo + "和TrackingNo不符合,请检查输入是否正确！";
                        }
                    }
                    else if (Regex.IsMatch(shipmentInfo.CarrierName, @"\w*YMT\w*") || Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*YMT\w*"))
                    {
                        dt = dataGetWay.ds_checkIsLinkCartonNoNoUpsYmtCnpl(trackingNo, cartonNo);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            exeRes.Status = true;
                        }
                        else
                        {
                            exeRes.Status = false;
                            exeRes.Message = "此箱号：" + cartonNo + "和TrackingNo不符合,请检查输入是否正确！";
                        }
                    }
                    else if (Regex.IsMatch(shipmentInfo.CarrierName, @"\w*CNPL\w*") || Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*CNPL\w*"))
                    {
                        dt = dataGetWay.ds_checkIsLinkCartonNoNoUpsYmtCnpl(trackingNo, cartonNo);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            exeRes.Status = true;
                        }
                        else
                        {
                            exeRes.Status = false;
                            exeRes.Message = "此箱号：" + cartonNo + "和TrackingNo不符合,请检查输入是否正确！";
                        }
                    }
                    else if (Regex.IsMatch(shipmentInfo.CarrierName, @"\w*SF\w*") || Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*SF\w*"))
                    {
                        dt = dataGetWay.ds_checkIsLinkCartonNoDhlSf(trackingNo, cartonNo);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            exeRes.Status = true;
                        }
                        else
                        {
                            exeRes.Status = false;
                            exeRes.Message = "此箱号：" + cartonNo + "和TrackingNo不符合,请检查输入是否正确！";
                        }
                    }
                    else if (Regex.IsMatch(shipmentInfo.CarrierName, @"\w*DHL\w*") || Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*DHL\w*"))
                    {
                        dt = dataGetWay.ds_checkIsLinkCartonNoDhlSf(trackingNo.Substring(1), cartonNo);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            exeRes.Status = true;
                        }
                        else
                        {
                            exeRes.Status = false;
                            exeRes.Message = "此箱号：" + cartonNo + "和TrackingNo不符合,请检查输入是否正确！";
                        }
                    }
                    else if (Regex.IsMatch(shipmentInfo.CarrierName, @"\w*TNT\w*") || Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*TNT\w*"))
                    {
                        dt = dataGetWay.ds_checkIsLinkCartonNoTnt(trackingNo.Substring(1), cartonNo);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            exeRes.Status = true;
                        }
                        else
                        {
                            exeRes.Status = false;
                            exeRes.Message = "此箱号：" + cartonNo + "和TrackingNo不符合,请检查输入是否正确！";
                        }
                    }
                    else
                    {
                        exeRes.Status = false;
                        exeRes.Message = "PARCEL-未知carrier：" + shipmentInfo.CarrierName + ",请联系IT-PPS! ";
                    }
                }
                else
                {
                    if (isDsPACShippingLabel)
                    {
                        exeRes.Status = true;
                        return exeRes;
                    }
                    dt = dataGetWay.fd_checkIsLinkCartonNo(trackingNo, cartonNo);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        exeRes.Status = true;
                    }
                    else
                    {
                        exeRes.Status = false;
                        exeRes.Message = "此箱号：" + cartonNo + "和TrackingNo不符合,请检查输入是否正确！";
                    }
                }
            }
            return exeRes;
        }
        public ExecuteResult getPickPalletInfoByPickPalletNoAndShipmentId(string pickPalletNo, string shipmentId)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = dataGetWay.getPickPalletInfoByPickPalletNoAndShipmentId(pickPalletNo, shipmentId);
            if (dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
                exeRes.Message = "Pick栈板号检查正确，请刷TrackingNo！";
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "Pick栈板号检查错误，请输入正确Pick栈板号！";
            }
            return exeRes;
        }

        public ExecuteResult checkPassStationPro(string shipmentId, string pickPalletNo, string sscc, string cartonNo, string deliveryNO, bool isMix, string mac_address, string isDSPacShipLable, string isDSPacDeliveryNote, string deliveryNote)
        {
            ExecuteResult exeRes = new ExecuteResult();
            object[][] procParams = new object[11][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "SHIPMENTID", shipmentId };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PICKPALLETNO", pickPalletNo };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ssccCode", sscc };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "cartonNo", cartonNo };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "DELIVERYNO", deliveryNO };
            procParams[5] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "isMix", isMix ? "1" : "0" };
            procParams[6] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "MAC_ADDRESS", mac_address };
            procParams[7] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "isDSPacShipLable", isDSPacShipLable };
            procParams[8] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "isDSPacDeliveryNote", isDSPacDeliveryNote };
            procParams[9] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "deliveryNote", deliveryNote };
            procParams[10] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "TMES", "" };
            DataSet ds = ClientUtils.ExecuteProc("ppsuser.t_check_pass_Station", procParams);
            if (ds.Tables[0].Rows[0]["TMES"].ToString().Equals("OK"))
            {
                exeRes.Status = true;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = ds.Tables[0].Rows[0]["TMES"].ToString();
            }
            return exeRes;
        }
        internal bool bGS1Flag(string pickPalletNo)
        {
            bool blGs1 = false;
            try
            {
                string sql = @"select count(*) as flag
                                  from ppsuser.t_shipment_pallet
                                 where pallet_no = substr(:varPickPallet, 3)
                                   and gs1flag = 'Y'";
                object[][] sqlParams = new object[1][];
                sqlParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varPickPallet", pickPalletNo };
                DataTable dt = ClientUtils.ExecuteSQL(sql, sqlParams).Tables[0];

                if (int.Parse(dt.Rows[0]["flag"].ToString()) > 0)
                    blGs1 = true;

            }
            catch (Exception ex)
            {
                throw;
            }
            return blGs1;
        }

        public ExecuteResult check_isLinkPickPalletNo_Pro(string shipmentId, string trackingNo, string pickPalletNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            object[][] procParams = new object[4][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "SHIPMENTID", shipmentId };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "trackingNo", trackingNo };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "pickPalletNo", pickPalletNo };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "TMES", "" };
            DataSet ds = ClientUtils.ExecuteProc("ppsuser.t_is_link_pickpalletNo_check", procParams);
            if (ds.Tables[0].Rows[0]["TMES"].ToString().Equals("OK"))
            {
                exeRes.Status = true;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = ds.Tables[0].Rows[0]["TMES"].ToString();
            }
            return exeRes;
        }

        public ExecuteResult check_isLinkCartonNo_Pro(string shipmentId, string trackingNo, string cartonNo, string isDSPACLABEL)
        {
            ExecuteResult exeRes = new ExecuteResult();
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "SHIPMENTID", shipmentId };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "trackingNo", trackingNo };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "cartonNo", cartonNo };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "isDSPACSHIPLABEL", isDSPACLABEL };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "TMES", "" };
            DataSet ds = ClientUtils.ExecuteProc("ppsuser.t_is_link_cartonno_check", procParams);
            if (ds.Tables[0].Rows[0]["TMES"].ToString().Equals("OK"))
            {
                exeRes.Status = true;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = ds.Tables[0].Rows[0]["TMES"].ToString();
            }
            return exeRes;
        }
        public ExecuteResult check_isLinkCartonNo_Pro_v2(string shipmentId, string trackingNo, string cartonNo, string isDSPACLABEL, string isMix, out string vRetCode, out string vRetDN)
        {
            ExecuteResult exeRes = new ExecuteResult();
            object[][] procParams = new object[8][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "SHIPMENTID", shipmentId };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "trackingNo", trackingNo };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "cartonNo", cartonNo };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "isDSPACSHIPLABEL", isDSPACLABEL };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "isMix", isMix };
            procParams[5] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "TMES", "" };
            procParams[6] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RetCode", "" };
            procParams[7] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RetDN", "" };
            DataSet ds = ClientUtils.ExecuteProc("ppsuser.t_is_link_cartonno_check_V2", procParams);
            if (ds.Tables[0].Rows[0]["TMES"].ToString().Equals("OK"))
            {
                exeRes.Status = true;

            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = ds.Tables[0].Rows[0]["TMES"].ToString();
            }
            vRetCode = ds.Tables[0].Rows[0]["RetCode"].ToString();
            vRetDN = ds.Tables[0].Rows[0]["RetDN"].ToString();

            return exeRes;
        }


        //public ExecuteResult isCartonNoLinkSSCC(string cartonNo,string  sscc)
        //{
        //    ExecuteResult exeRes = new ExecuteResult();
        //    DataTable dt = new DataTable();
        //    dt = dataGetWay.isCartonNoLinkSSCC(cartonNo,sscc);
        //    if (dt.Rows[0]["countR"].ToString().Equals("0"))
        //    {
        //        exeRes.Status = false;
        //        exeRes.Message = "箱号和TrackingNo不符合，请检查！";
        //    }
        //    return exeRes;
        //}

        public bool isJumpPackingList(string cartonNo, bool isMix, ShipmentInfo shipmentInfo)
        {
            /*
                  针对PAC（亚太地区）可能会不打印packingList
             */

            bool flag = false;
            DataTable dt = new DataTable();
            ExecuteResult exeRes = new ExecuteResult();
            T940UnicodeInfo t940Info = new T940UnicodeInfo();
            if (shipmentInfo.ShipmentType.Equals("FD"))
            {
                if (!shipmentInfo.Region.Equals("AMR"))
                {
                    return false;
                }
            }
            else
            {
                exeRes = getT940UnicodeInfoByCartonNo(cartonNo);
                if (exeRes.Status)
                {
                    t940Info = (T940UnicodeInfo)exeRes.Anything;
                    dt = dataGetWay.isJumpPackingListForDSConditionByT940UnicodeInfo(t940Info.region, t940Info.customerGroup, t940Info.msgFlag, t940Info.gpFlag);
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            dt = dataGetWay.GetPPartDNInfo(cartonNo);
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                //说明DN已在产线打印,判断是否对应打印的箱号
                dt = dataGetWay.GetPPartPrintCarton(dt.Rows[0]["PRINT_CARTON"].ToString().Trim());
                if (cartonNo.Trim().ToUpper() == dt.Rows[0]["CARTON_NO"].ToString().Trim().ToUpper())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            dt = dataGetWay.isJumpPackingListForDN_NO1ByCartonNo(cartonNo, isMix);
            if (dt.Rows[0]["BOXNO"].ToString().Equals("1"))
            {
                flag = true;
            }
            else
            {
                return false;
            }
            return flag;
        }
        public bool isJumpDeliveryNoteTB(string cartonNo, ShipmentInfo shipmentInfo, bool isMix)
        {
            bool flag = false;
            DataTable dt = new DataTable();
            if (shipmentInfo.TYPE.Equals("PARCEL"))
            {
                dt = dataGetWay.getT940InfoByDeliveryNo(cartonNo);
                if (dt.Rows.Count > 0 && dt != null)
                {
                    string shipCnCode = dt.Rows[0]["SHIPCNTYCODE"].ToString();
                    string custGroup = dt.Rows[0]["CUSTOMERGROUP"].ToString();
                    if (shipCnCode.Equals("JP"))
                    {
                        //对YMT JP 地区取消deliverynote modify 2020-1-14
                        if (Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*YMT\w*") ||
                            Regex.IsMatch(shipmentInfo.CarrierName, @"\w*YMT\w*"))
                        {
                            return false;
                        }
                        dt = dataGetWay.isJumpDeliveryNoteTB(cartonNo, shipmentInfo.TYPE);
                        if (!dt.Rows[0]["checkCount"].ToString().Equals("0") && dt != null)
                        {
                            flag = true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (custGroup.Equals("IN") || custGroup.Equals("RK") || custGroup.Equals("RW"))
                        {
                            return false;
                        }
                        else
                        {
                            dt = dataGetWay.isJumpDeliveryNoteTB(cartonNo, shipmentInfo.TYPE);
                            if (!dt.Rows[0]["checkCount"].ToString().Equals("0") && dt != null)
                            {
                                flag = true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            else
            {

                dt = dataGetWay.isJumpDeliveryNoteTB(cartonNo, shipmentInfo.TYPE);
                if (!dt.Rows[0]["checkCount"].ToString().Equals("0") && dt != null)
                {
                    flag = true;
                }
                else
                {
                    return false;
                }
            }




            dt = dataGetWay.isJumpPackingListForDN_NO1ByCartonNo(cartonNo, isMix);
            if (dt.Rows[0]["BOXNO"].ToString().Equals("1"))
            {
                flag = true;
            }
            else
            {
                return false;
            }
            return flag;
        }
        public ExecuteResult getT940UnicodeInfoByCartonNo(string cartonNo)
        {
            DataTable dt = new DataTable();
            ExecuteResult exeRes = new ExecuteResult();
            dt = dataGetWay.getT940UnicodeInfoByCartonNo(cartonNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                string customerGroup_D = dt.Rows[0]["customergroup"].ToString().Trim();
                if (!(customerGroup_D.Equals("RK") || customerGroup_D.Equals("RW") || customerGroup_D.Equals("IN") || customerGroup_D.Equals("RR")))
                {
                    customerGroup_D = "OTHER";
                }
                string t9uMsgFlag = "N";//user:weifeng   940中多个lineItem，只要有一个Y，就是Y
                string t9uGbFlag = "N";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!dt.Rows[i]["msgflag"].ToString().Trim().Equals(t9uMsgFlag))
                    {
                        t9uMsgFlag = dt.Rows[i]["msgflag"].ToString().Trim();
                        break;
                    }
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!dt.Rows[i]["gpflag"].ToString().Trim().Equals(t9uGbFlag))
                    {
                        t9uGbFlag = dt.Rows[i]["gpflag"].ToString().Trim();
                        break;
                    }
                }
                exeRes.Anything = new T940UnicodeInfo
                {
                    region = dt.Rows[0]["region"].ToString().Trim(),
                    msgFlag = t9uMsgFlag,
                    gpFlag = t9uGbFlag,
                    customerGroup = customerGroup_D
                };
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "T940Unicode没有获得资料，请联系IT-PPS!";
            }
            return exeRes;
        }



        public bool isReprint(string cartonNo)
        {
            bool flag = false;
            DataTable dt = new DataTable();
            dt = dataGetWay.isReprint(cartonNo);
            if (dt.Rows.Count > 0 && dt != null)
            {
                if (dt.Rows[0]["checkResult"].ToString().Equals("OK"))
                {
                    flag = true;
                }
            }
            return flag;
        }
        public bool isExistShippingLabelForPAC(string pickPalletNo, string shipInfoType)
        {
            bool flag = false;
            DataTable dt = new DataTable();
            dt = dataGetWay.isExistShippingLabelForPAC(pickPalletNo, shipInfoType);
            if (!dt.Rows[0]["checkCount"].ToString().Equals("0") && dt != null)
            {
                flag = true;
            }
            return flag;
        }

        public ExecuteResult getReprintInfoByCartonNo(string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = dataGetWay.getReprintInfoByCartonNo(cartonNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
            }
            else
            {
                exeRes.Message = "此箱号：" + cartonNo + " 获取信息异常！";
                exeRes.Status = false;
            }
            return exeRes;
        }

        public bool isPrintLabel(string palletNo)
        {
            DataTable dt = new DataTable();
            bool flag = false;
            dt = dataGetWay.isPrintLabel(palletNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }


        public bool isFinishWorkByCartonNo(string cartonNo)
        {
            DataTable dt = new DataTable();
            bool flag = false;
            dt = dataGetWay.isFinishWorkByCartonNo(cartonNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }

        public bool isCheckshipmentIdFinishWorkByCartonNo(string cartonNo)
        {
            DataTable dt = new DataTable();
            bool flag = false;
            dt = dataGetWay.isCheckshipmentIdFinishWorkByCartonNo(cartonNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }
        public ExecuteResult getShowResultDGV(string cartonNo, bool isMix)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            if (isMix)
            {
                dt = dataGetWay.getShowResultDGV_Mix(cartonNo);
            }
            else
            {
                dt = dataGetWay.getShowResultDGV_No_Mix(cartonNo);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "获取showResultDGV信息失败,请联系IT-PPS！";
            }
            return exeRes;
        }
        public ExecuteResult totalAllCartonQtyByPickPalletNo(string pickPalletNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = dataGetWay.totalAllCartonQtyByPickPalletNo(pickPalletNo);
            exeRes.Anything = dt.Rows[0]["T_STR"].ToString();
            return exeRes;
        }

        //public ExecuteResult isPickPalletNoLinkRelationBySSCCAndPickPalletNo(string  sscc ,string pickPalletNo)
        //{
        //    ExecuteResult exeRes = new ExecuteResult();
        //    DataTable dt = new DataTable();
        //    string checkResult = "";
        //    dt = dataGetWay.isPickPalletNoLinkRelationBySSCCAndPickPalletNo(sscc,pickPalletNo);
        //    if (dt  !=  null && dt.Rows.Count >0)
        //    {
        //      checkResult =   dt.Rows[0]["item_"].ToString();
        //        if (checkResult.Equals("0"))
        //        {
        //            exeRes.Status = false;
        //            exeRes.Message = "此 SSCC："+sscc+"不属于此PickPalletNo,请检查输入是否正确！";
        //        }
        //    }
        //    return exeRes;
        //}


        public void checkSpecialDNbySP(string strCarton, out string strDNStatud, out string strMsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incarton", strCarton };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outstatus", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("ppsuser.sp_check_specialdnstatus", procParams);
            strDNStatud = ds.Tables[0].Rows[0]["outstatus"].ToString();
            strMsg = ds.Tables[0].Rows[0]["errmsg"].ToString();

        }
        public bool GetPACGS1003MultiLine(string strSID, string strPallet)
        {
            DataTable dtTemp = dataGetWay.GetPACGS1003MultiLineBySQL(strSID, strPallet);
            if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
