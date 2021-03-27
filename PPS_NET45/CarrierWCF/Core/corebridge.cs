using CarrierWCF.Data;
using CarrierWCF.Entity;
using CarrierWCF.Model;
using CarrierWCF.Models;
using DBTools;
using DBTools.Connection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarrierWCF.Core
{
    public class corebridge
    {
        dataGateWay dgw;
        ExecutionResult exeRes;
        DBTransaction dbtrans;
        private static ClientAccessCredentials CLIENT_ACCESS { set; get; }
        private static UserContext USER_CONTEXT { set; get; }
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
        public ExecutionResult SendMail(string carton, string msg)
        {
            exeRes = new ExecutionResult();
            dbtrans = new DBTransaction(DBAddr);
            dgw = new dataGateWay();
            try
            {
                dbtrans.BeginTransaction();
                exeRes = dgw.GetParameter(dbtrans, "UPS_ALERT");
                //exeRes = dgw.GetListAlertMail(dbtrans);
                var ds = exeRes.Anything as DataSet;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    SendMail(new MailInformation()
                    {
                        Body = "箱号: " + carton + "\n" + "MESSAGE: " + msg,
                        CodePage = 0,
                        IsBodyHtml = false,
                        Subject = "UPS SHIPEXEC ERROR",
                        Object = new MailTo()
                        {
                            ToAddresses = ds.Tables[0].Rows[0]["PARA_VALUE"].ToString().Split('#').Select(x => { return x + "@luxshare-ict.com"; }).ToArray()
                        }
                    });
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
        internal string gettest()
        {
            dgw = new dataGateWay(DBAddr);
            return dgw.gettest();
        }

        public static void SendMail(MailInformation mi)
        {
            HttpClient client = new HttpClient();
            //string email = "http://10.54.20.41:81/emailvn/";
            string email = ConfigurationManager.AppSettings["MAIL_API"].ToString();
            client.BaseAddress = new Uri(email);

            var result = client.PostAsJsonAsync("api/SendMail", mi).Result;
            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    break;
                case HttpStatusCode.BadRequest:
                    var jobj = result.Content.ReadAsAsync<JObject>().Result;
                    throw new Exception(result.StatusCode.ToString() + "\t" + jobj["Message"].ToString());
                default:
                    throw new Exception(result.StatusCode.ToString());
            }
        }

        public static ClientAccessCredentials GetClientAccess()
        {
            if (CLIENT_ACCESS != null)
                return CLIENT_ACCESS;
            else
            {
                var _dbtrans = new DBTransaction(DBAddr);
                var _dgw = new dataGateWay();
                try
                {
                    _dbtrans.BeginTransaction();
                    ExecutionResult exeRes = _dgw.GetParameter(_dbtrans, "UPS_ACCESS");
                    var ds = exeRes.Anything as DataSet;
                    CLIENT_ACCESS = JsonConvert.DeserializeObject<ClientAccessCredentials>(ds.Tables[0].Rows[0]["PARA_VALUE"].ToString());
                    if (exeRes.Status)
                        _dbtrans.Commit();
                    else
                        _dbtrans.Rollback();
                }
                catch (Exception ex)
                {
                    _dbtrans.Rollback();
                    return null;
                }
                finally
                {
                    _dbtrans.EndTransaction();
                }
                return CLIENT_ACCESS;
            }
        }

        public static UserContext GetUserContext()
        {
            if (USER_CONTEXT != null)
                return USER_CONTEXT;
            else
            {
                var _dbtrans = new DBTransaction(DBAddr);
                var _dgw = new dataGateWay();
                try
                {
                    _dbtrans.BeginTransaction();
                    ExecutionResult exeRes =  _dgw.GetParameter(_dbtrans, "UPS_CONTEXT");
                    var ds = exeRes.Anything as DataSet;
                    USER_CONTEXT = JsonConvert.DeserializeObject<UserContext>(ds.Tables[0].Rows[0]["PARA_VALUE"].ToString());
                    if (exeRes.Status)
                        _dbtrans.Commit();
                    else
                        _dbtrans.Rollback();
                }
                catch
                {
                    _dbtrans.Rollback();
                    return null;
                }
                finally
                {
                    _dbtrans.EndTransaction();
                }
                return USER_CONTEXT;
            }
        }
    }


}
