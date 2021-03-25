using CarrierWCF.Entity;
using CarrierWCF.Model;
using CarrierWCF.Models;
using DBTools;
using DBTools.Connection;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierWCF.Data
{
    public class dataGateWay
    {
        DBParameter dbparam;
        DBExecute dbTool;
        public dataGateWay(string dbConn)
        {
            dbTool = new DBExecute(dbConn);
        }
        public dataGateWay()
        {
        }
        /// <summary>
        /// transfer log
        /// </summary>
        /// <param name="Origin_data">源数据</param>
        /// <param name="interfaceName">接口名称</param>
        /// <param name="boolRes">交互结果</param>
        /// <param name="strRes">交互信息</param>
        /// <param name="owner">方向</param>
        public void WriteLog(string Origin_data, string interfaceName, bool boolRes, string strRes, string owner, string action_name, string carton_id = "")
        {
            dbparam = new DBParameter();
            string sql = @"insert into ppsuser.i_interface_log 
                                (Origin_data,interface_name,status,result_message,create_date,owner,action_name,CARTON_ID)
                                 values
                                 (:Origin_data,:interface_name,:status,:result_message,sysdate,:owner,:action_name,:CARTON_ID)";
            dbparam.Add("Origin_data", OracleType.Clob, Origin_data);
            dbparam.Add("interface_name", OracleType.VarChar, interfaceName);
            dbparam.Add("status", OracleType.VarChar, boolRes);
            dbparam.Add("result_message", OracleType.Clob, strRes);
            dbparam.Add("owner", OracleType.VarChar, owner);
            dbparam.Add("action_name", OracleType.VarChar, action_name);
            dbparam.Add("CARTON_ID", OracleType.VarChar, carton_id);
            dbTool.ExecuteUpdate(sql, dbparam.GetParameters());
        }

        internal ExecutionResult InsertDefaults(ShipModel shipModel, ShipOutputModel shipOutputModel, DBTransaction dbtrans)
        {
            ExecutionResult exeRes = new ExecutionResult();
            dbparam = new DBParameter();
            string sql = @"merge into ppsuser.t_ups_packagedefaults a
                                    using (select :vDELIVERY_NO           as DELIVERY_NO,
                                                  :vARRIVETIME            as ARRIVETIME,
                                                  :vMANIFESTID            as MANIFESTID,
                                                  :vRATEDSERVICE_SYMBOL   as RATEDSERVICE_SYMBOL,
                                                  :vRATEDSERVICE_NAME     as RATEDSERVICE_NAME,
                                                  :vSERVICE_SYMBOL        as SERVICE_SYMBOL,
                                                  :vSERVICE_NAME          as SERVICE_NAME,
                                                  :vSHIPPEDSERVICE_SYMBOL as SHIPPEDSERVICE_SYMBOL,
                                                  :vSHIPPEDSERVICE_NAME   as SHIPPEDSERVICE_NAME,
                                                  :vTIMEINTRANSITDAYS     as TIMEINTRANSITDAYS,
                                                  :vBASECHARGE_CURRENCY   as BASECHARGE_CURRENCY,
                                                  :vBUNDLEIDLIST          as BUNDLEIDLIST,
                                                  :vTIMEINTRANSIT         as TIMEINTRANSIT,
                                                  :vTOTAL_CURRENCY        as TOTAL_CURRENCY
                                             from dual) b
                                    on (a.delivery_no = b.delivery_no)
                                    when matched then
                                      update set a.create_date = sysdate
                                    when not matched then
                                      insert
                                        (DELIVERY_NO,
                                         ARRIVETIME,
                                         MANIFESTID,
                                         RATEDSERVICE_SYMBOL,
                                         RATEDSERVICE_NAME,
                                         SERVICE_SYMBOL,
                                         SERVICE_NAME,
                                         SHIPPEDSERVICE_SYMBOL,
                                         SHIPPEDSERVICE_NAME,
                                         TIMEINTRANSITDAYS,
                                         BASECHARGE_CURRENCY,
                                         BUNDLEIDLIST,
                                         TIMEINTRANSIT,
                                         TOTAL_CURRENCY)
                                      values
                                        (b.DELIVERY_NO,
                                         b.ARRIVETIME,
                                         b.MANIFESTID,
                                         b.RATEDSERVICE_SYMBOL,
                                         b.RATEDSERVICE_NAME,
                                         b.SERVICE_SYMBOL,
                                         b.SERVICE_NAME,
                                         b.SHIPPEDSERVICE_SYMBOL,
                                         b.SHIPPEDSERVICE_NAME,
                                         b.TIMEINTRANSITDAYS,
                                         b.BASECHARGE_CURRENCY,
                                         b.BUNDLEIDLIST,
                                         b.TIMEINTRANSIT,
                                         b.TOTAL_CURRENCY)";
            dbparam.Add("vDELIVERY_NO", OracleType.VarChar, shipModel.ShipmentRequest.PackageDefaults.ShipperReference);
            dbparam.Add("vARRIVETIME", OracleType.VarChar, shipOutputModel.ShipmentResponse.PackageDefaults.ArriveTime);
            dbparam.Add("vMANIFESTID", OracleType.VarChar, shipOutputModel.ShipmentResponse.PackageDefaults.ManifestId);
            dbparam.Add("vRATEDSERVICE_SYMBOL", OracleType.VarChar, shipOutputModel.ShipmentResponse.PackageDefaults.RatedService.Symbol);
            dbparam.Add("vRATEDSERVICE_NAME", OracleType.VarChar, shipOutputModel.ShipmentResponse.PackageDefaults.RatedService.Name);
            dbparam.Add("vSERVICE_SYMBOL", OracleType.VarChar, shipOutputModel.ShipmentResponse.PackageDefaults.Service.Symbol);
            dbparam.Add("vSERVICE_NAME", OracleType.VarChar, shipOutputModel.ShipmentResponse.PackageDefaults.Service.Name);
            dbparam.Add("vSHIPPEDSERVICE_SYMBOL", OracleType.VarChar, shipOutputModel.ShipmentResponse.PackageDefaults.ShippedService.Symbol);
            dbparam.Add("vSHIPPEDSERVICE_NAME", OracleType.VarChar, shipOutputModel.ShipmentResponse.PackageDefaults.ShippedService.Name);
            dbparam.Add("vTIMEINTRANSITDAYS", OracleType.VarChar, shipOutputModel.ShipmentResponse.PackageDefaults.TimeInTransitDays);
            dbparam.Add("vBASECHARGE_CURRENCY", OracleType.VarChar, shipOutputModel.ShipmentResponse.PackageDefaults.BaseCharge.Currency);
            dbparam.Add("vBUNDLEIDLIST", OracleType.VarChar, string.Join(",", shipOutputModel.ShipmentResponse.PackageDefaults.BundleIdList));
            dbparam.Add("vTIMEINTRANSIT", OracleType.VarChar, shipOutputModel.ShipmentResponse.PackageDefaults.TimeInTransit);
            dbparam.Add("vTOTAL_CURRENCY", OracleType.VarChar, shipOutputModel.ShipmentResponse.PackageDefaults.Total.Currency);
            exeRes = dbtrans.ExecuteUpdate(sql, dbparam.GetParameters());
            return exeRes;
        }

        internal string gettest()
        {
            string sql = @"selecT result_message from i_interface_log where
 interface_name = 'SHIP' and status = 'True' and
   create_date = to_date('2020/2/18 17:11:26', 'yyyy/MM/dd hh24:mi:ss')";
            return dbTool.ExecuteQueryStr(sql);
        }

        internal ExecutionResult InsertDetails(ShipModel shipModel, Package package, DBTransaction dbtrans)
        {
            ExecutionResult exeRes = new ExecutionResult();
            dbparam = new DBParameter();
            string sql = @"update ppsuser.t_ups_packages
                                       set (WAYBILLBOLNUMBER,
                                            GLOBALBUNDLEID,
                                            GLOBALMSN,
                                            SHIPID,
                                            TIMEINTRANSITDAYS,
                                            BUNDLEID,
                                            MAXICODE,
                                            MSN,
                                            NOFNSEQUENCEBUNDLE,
                                            ROUTINGCODE,
                                            PACKAGELISTID,
                                            WORLDEASEID,
                                            RATEDWEIGHT_AMOUNT,
                                            RATEDWEIGHT_UNITS,
                                            TIMEINTRANSIT,
                                            ZONE,
                                            labeldata,
                                            ISUSED,
                                            USED_TIME) =
                                           (select  :WAYBILLBOLNUMBER, :GLOBALBUNDLEID, :GLOBALMSN, :SHIPID,
                                            :TIMEINTRANSITDAYS, :BUNDLEID, :MAXICODE, :MSN, :NOFNSEQUENCEBUNDLE,
                                            :ROUTINGCODE, :PACKAGELISTID, :WORLDEASEID, :RATEDWEIGHT_AMOUNT,
                                            :RATEDWEIGHT_UNITS, :TIMEINTRANSIT, :ZONE, :LabelData,'Y',SYSDATE  from dual)
                                     where delivery_no = :DELIVERY_NO
                                       and trackingnumber = :TRACKINGNUMBER
                                    ";
            dbparam.Add("DELIVERY_NO", OracleType.VarChar, shipModel.ShipmentRequest.PackageDefaults.ShipperReference);
            dbparam.Add("TRACKINGNUMBER", OracleType.VarChar, package.TrackingNumber);
            dbparam.Add("WAYBILLBOLNUMBER", OracleType.VarChar, package.WaybillBolNumber);
            dbparam.Add("GLOBALBUNDLEID", OracleType.VarChar, package.GlobalBundleId);
            dbparam.Add("GLOBALMSN", OracleType.VarChar, package.GlobalMsn);
            dbparam.Add("SHIPID", OracleType.VarChar, package.ShipId);
            dbparam.Add("TIMEINTRANSITDAYS", OracleType.VarChar, package.TimeInTransitDays);
            dbparam.Add("BUNDLEID", OracleType.VarChar, package.BundleId);
            dbparam.Add("MAXICODE", OracleType.VarChar, string.Join(",", package.Maxicode));//数组转string
            dbparam.Add("MSN", OracleType.VarChar, package.Msn);
            dbparam.Add("NOFNSEQUENCEBUNDLE", OracleType.VarChar, package.NofnSequenceBundle);
            dbparam.Add("ROUTINGCODE", OracleType.VarChar, package.RoutingCode);
            dbparam.Add("PACKAGELISTID", OracleType.VarChar, package.PackageListId);
            dbparam.Add("WORLDEASEID", OracleType.VarChar, package.WorldEaseId);
            dbparam.Add("RATEDWEIGHT_AMOUNT", OracleType.VarChar, package.RatedWeight.Amount);
            dbparam.Add("RATEDWEIGHT_UNITS", OracleType.VarChar, package.RatedWeight.Units);
            dbparam.Add("TIMEINTRANSIT", OracleType.VarChar, package.TimeInTransit);
            dbparam.Add("ZONE", OracleType.VarChar, package.Zone);
            dbparam.Add("LabelData", OracleType.Clob, package.Documents[0].RawData[0].ToString());
            exeRes = dbtrans.ExecuteUpdate(sql, dbparam.GetParameters());
            return exeRes;
        }

        public ExecutionResult InsertRawData(UPSRawDataEntity rawObj, DBTransaction dbtrans)
        {
            //ExecutionResult exeRes = new ExecutionResult();
            //string errormsg = "";
            //object[][] procParams = new object[6][];
            //procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incartonno", rawObj.CARTON_NO };
            //procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "intrackingno", rawObj.TRACKING_NO };
            //procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inglobalmsn", rawObj.GLOBALMSN };
            //procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Clob, "inrawdata", rawObj.RAWDATA };
            //procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "deliveryno", rawObj.DELIVERY_NO };
            //procParams[5] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", errormsg };
            //DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_UPS_INSERTRAWDATA", procParams);
            //exeRes.Message = ds.Tables[0].Rows[0]["errmsg"].ToString();
            //exeRes.Status = exeRes.Message == "OK";
            //return exeRes;

            ExecutionResult exeRes = new ExecutionResult();
            exeRes.Status = true;
            dbparam = new DBParameter();
//delete PPSUSER.T_UPS_RAWDATA where CARTON_NO = :CARTON_NO;
            string sql = @"INSERT into PPSUSER.T_UPS_RAWDATA
                        (CARTON_NO,TRACKING_NO, GLOBALMSN, RAWDATA,DELIVERY_NO)
                        VALUES(:CARTON_NO,:TRACKING_NO,:GLOBALMSN,:RAWDATA,:DELIVERY_NO)";
            dbparam.Add("CARTON_NO", OracleType.VarChar, rawObj.CARTON_NO);
            dbparam.Add("TRACKING_NO", OracleType.VarChar, rawObj.TRACKING_NO);
            dbparam.Add("GLOBALMSN", OracleType.VarChar, rawObj.GLOBALMSN);
            dbparam.Add("RAWDATA", OracleType.VarChar, rawObj.RAWDATA);
            dbparam.Add("DELIVERY_NO", OracleType.VarChar, rawObj.DELIVERY_NO);

            exeRes = dbtrans.ExecuteUpdate(sql, dbparam.GetParameters());
            return exeRes;
        }
    }
}
