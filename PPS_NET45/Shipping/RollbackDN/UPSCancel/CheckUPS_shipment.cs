using Newtonsoft.Json;
using OperationWCF;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace RollbackDN.UPSCancel
{
    class CheckUPS_shipment
    {

        public string UPSCheck(string shipmentId)
        {
            string msg = "";
            string sql = @"select * from t_shipment_info"
                           + "    where shipment_type='DS' " +
                           "and type='PARCEL' and carrier_name like '%UPS%' "
                           + " and shipment_id=:shipment_id";
            object[][] para = new object[1][];
            para[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipment_id", shipmentId };
            DataTable dt = new DataTable();
            dt = ClientUtils.ExecuteSQL(sql, para).Tables[0];
            if (dt.Rows.Count > 0)
            {
                msg = "OK";
            }
            return msg;
        }
        public string SendShipmentCancel(string shipmentId)
        {
            string msg = "";
            try
            {
                string linksrv = "";
                ICTConnectionDB cndb = new ICTConnectionDB();
                linksrv = cndb.IctUrlFromDB();
                List<int> ls = new List<int>();
                string sql = @"SELECT DISTINCT globalmsn from PPSUSER.T_UPS_RAWDATA
                                where tracking_no in
                                (SELECT distinct T.TRACKING_NO trackingNo
                                                          FROM ppsuser.t_allo_trackingno t
                                                         WHERE     t.shipment_id IN (SELECT shipment_id
                                                                   FROM ppsuser.t_shipment_info
                                                              WHERE  carrier_name LIKE '%UPS%'
                                                                    AND TYPE = 'PARCEL')
                                AND shipment_id = :shipment_id)";
                object[][] para = new object[1][];
                para[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipment_id", shipmentId };
                DataTable dt = new DataTable();
                dt = ClientUtils.ExecuteSQL(sql, para).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    msg = "在Database未找到 此集货单之globalmsn";
                }
                else
                {
                    ls = dt.AsEnumerable().Select(x => int.Parse(x["globalmsn"].ToString())).ToList();
                    string data = JsonConvert.SerializeObject(new { GlobalMsns = ls });
                    if (ls.Count == 0)
                    {
                        msg = "未找到此集货单号之 GlobalMsns 号!";
                    }
                    else
                    {
                        CarrierWCF.Wcf.IICTToCarrierService WS = HttpChannel.Get<CarrierWCF.Wcf.IICTToCarrierService>(linksrv);
                        string dataout = WS.Void(data);
                        msg = dataout;
                    }
                }
            }
            catch (Exception ex1)
            {
                msg = ex1.Message;
            }
            return msg;
        }
        public string SendShipmentCancel(List<int> lstGlbMSN)
        {
            string msg = "";
            try
            {
                ICTConnectionDB cndb = new ICTConnectionDB();
                string linksrv = cndb.IctUrlFromDB();
                if (lstGlbMSN.Count == 0)
                    msg = "在Database未找到 此集货单之globalmsn";
                else
                {
                    CarrierWCF.Wcf.IICTToCarrierService WS = HttpChannel.Get<CarrierWCF.Wcf.IICTToCarrierService>(linksrv);
                    msg = WS.Void(JsonConvert.SerializeObject(new { GlobalMsns = lstGlbMSN }));
                }
            }
            catch (Exception ex1)
            {
                msg = ex1.Message;
            }
            return msg;
        }
        public bool CheckUPSEnable()
        {
            //string msg = "";
            string sql = @"select ENABLED from PPSUSER.T_BASICPARAMETER_INFO where para_type='UPS_URL'  and ENABLED='Y'";
            DataTable dt = new DataTable();
            dt = ClientUtils.ExecuteSQL(sql).Tables[0];
            //if (dt.Rows.Count > 0)
            //{ 
            //    msg = "尚未设定UPS Carrier开关";
            //}
            //else
            //{
            //    msg = dt.Rows[0]["ENABLED"].ToString();
            //}
            return (dt.Rows.Count > 0);
        }
    }
}
