using CarrierWCF.Data;
using CarrierWCF.Entity;
using CarrierWCF.Model;
using CarrierWCF.Models;
using DBTools;
using DBTools.Connection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierWCF.Core
{
    public class corebridge
    {
        dataGateWay dgw;
        ExecutionResult exeRes;
        DBTransaction dbtrans;
        public corebridge()
        {
        }
        public static string DBAddr
        {
            get
            {
                return string.Format("{0};user id ={1};password = {2};", ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString, "ppsuser", DecodeBase64(ConfigurationManager.AppSettings["DBPwd"]));
            }
        }
        public static string DecodeBase64(string code)
        {
            string str;
            byte[] numArray = Convert.FromBase64String(code);
            try
            {
                str = Encoding.GetEncoding("utf-8").GetString(numArray);
            }
            catch
            {
                str = code;
            }
            return str;
        }
        public void WriteLog(string Origin_data, string interfaceName, bool boolRes, string strRes, string owner, string action_name, string carton_id = "")
        {
            dgw = new dataGateWay(DBAddr);
            dgw.WriteLog(Origin_data, interfaceName, boolRes, strRes, owner, action_name, carton_id);
        }

        public ExecutionResult InsertResponseData(ShipModel shipModel, ShipOutputModel shipOutputModel)
        {
            exeRes = new ExecutionResult();
            dbtrans = new DBTransaction(DBAddr);
            dgw = new dataGateWay();
            try
            {
                dbtrans.BeginTransaction();
                //写入总表
                exeRes = dgw.InsertDefaults(shipModel, shipOutputModel, dbtrans);
                if (exeRes.Status)
                {
                    //写入明细表
                    foreach (var packageItem in shipOutputModel.ShipmentResponse.Packages)
                    {
                        exeRes = dgw.InsertDetails(shipModel, packageItem, dbtrans);
                        if (!exeRes.Status)
                            break;
                    }
                }
                if (exeRes.Status)
                    dbtrans.Commit();
                else
                    dbtrans.Rollback();
            }
            catch (Exception ex)
            {
                dbtrans.Rollback();
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            finally
            {
                dbtrans.EndTransaction();
            }
            return exeRes;
        }

        public ExecutionResult InsertRawData(UPSRawDataEntity rawObj)
        {
            exeRes = new ExecutionResult();
            dbtrans = new DBTransaction(DBAddr);
            dgw = new dataGateWay();
            try
            {
                dbtrans.BeginTransaction();
                //写入总表
                exeRes = dgw.InsertRawData(rawObj, dbtrans);
                if (exeRes.Status)
                    dbtrans.Commit();
                else
                    dbtrans.Rollback();
            }
            catch (Exception ex)
            {
                dbtrans.Rollback();
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            finally
            {
                dbtrans.EndTransaction();
            }
            return exeRes;
        }
        public ExecutionResult InsertMailTest(string carton, string msg)
        {
            exeRes = new ExecutionResult();
            dbtrans = new DBTransaction(DBAddr);
            dgw = new dataGateWay();
            try
            {
                dbtrans.BeginTransaction();
                //写入总表
                ExecutionResult exeRes = new ExecutionResult();
                var dbparam = new DBParameter();
                string sql = @"INSERT into PPSUSER.T_TEMP
                            select :CARTON_NO,:CREATE_DATE, :DESCRIPTION from dual";
                dbparam.Add("CARTON_NO", System.Data.OracleClient.OracleType.VarChar, carton);
                dbparam.Add("DESCRIPTION", System.Data.OracleClient.OracleType.Clob, msg);
                dbparam.Add("CREATE_DATE", System.Data.OracleClient.OracleType.VarChar, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                exeRes = dbtrans.ExecuteUpdate(sql, dbparam.GetParameters());

                if (exeRes.Status)
                    dbtrans.Commit();
                else
                    dbtrans.Rollback();
            }
            catch (Exception ex)
            {
                dbtrans.Rollback();
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            finally
            {
                dbtrans.EndTransaction();
            }
            return exeRes;
        }
        internal string gettest()
        {
            dgw = new dataGateWay(DBAddr);
            return dgw.gettest();
        }
    }


}
