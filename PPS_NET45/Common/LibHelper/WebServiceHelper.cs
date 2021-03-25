using LibHelper.SaveDsDeliNote;
using LibHelper.ToEdiByInfoInPick;
using LibHelper.UpsHawb;
using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace LibHelper
{
    public class WebServiceHelper
    {

        /// <summary>
        /// 获取UPS Hawb值
        /// </summary>
        /// <param name="strCarrierCoder">类型</param>
        /// <param name="strParceAccount">账号</param>
        /// <param name="strServiceLevel">服务等级</param>
        /// <returns>Hawb值</returns>
        public static string GetUpsHawb(string strCarrierCoder, string strParceAccount, string strServiceLevel)
        {
            try
            {
                ICT_WSoagetUPS_HAWB_ws UpsHawb = new ICT_WSoagetUPS_HAWB_ws();
                //UpsHawb upsHawb = new LibHelper.UpsHawb();
                //UpsHawb.carrierCode = strCarrierCoder;
                //UpsHawb.parcelAccount = "XUPSC";
                //UpsHawb.serviceLevel = "strServiceLevel";
                return UpsHawb.getUPS_HAWB_ws(strCarrierCoder, strParceAccount, strServiceLevel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 保存shipmetnId,DN到ICT系统
        /// </summary>
        /// <param name="shipmentId"></param>
        /// <param name="dn"></param>
        public static void SaveIctDsDeliNote(string shipmentId, string dn)
        {
            try
            {
                ICT_WSppsDSsaveDS_DELI_NOTE_G_EMEIA saveIctDeliNote = new ICT_WSppsDSsaveDS_DELI_NOTE_G_EMEIA();
                saveIctDeliNote.Timeout = 2000;
                saveIctDeliNote.saveDS_DELI_NOTE_G_EMEIA(shipmentId, dn);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static bool ToEdiByInfoInPick(string shipmentId,out string errorMessage)
        {
            try
            {
                string sql = @" SELECT region,type 
                                  FROM ppsuser.g_ds_shimment_base_t
                                 WHERE shipment_id = :ShipmentId";
                object[][] objParams = new object[1][];
                objParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
                
                DataSet dtShipment = ClientUtils.ExecuteSQL(sql, objParams);
                string message = string.Empty;
                string resultMessage = string.Empty;
                errorMessage = string.Empty;
                if (dtShipment != null && dtShipment.Tables[0].Rows.Count > 0)
                {
                    ICT_WSppsDSgenShippingDocLabel toEdiByInfoInPick = new ICT_WSppsDSgenShippingDocLabel();
                    resultMessage = toEdiByInfoInPick.genShippingDocLabel_ws(shipmentId, dtShipment.Tables[0].Rows[0][0].ToString(), dtShipment.Tables[0].Rows[0][1].ToString(), out message);
                    if(resultMessage.ToUpper() == "SUCCESS" || message.ToUpper() == "SUCCESS")
                    {
                        return true;
                    }
                    else
                    {
                        resultMessage = "失败:" + resultMessage;
                        errorMessage = resultMessage;
                        return false;
                    }
                }
                return true;
                
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message.ToString();
                return false;
            }
        }
    }
}
