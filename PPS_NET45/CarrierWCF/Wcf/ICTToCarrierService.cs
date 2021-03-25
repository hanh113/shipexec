namespace CarrierWCF.Wcf
{
    using CarrierWCF.Core;
    using CarrierWCF.Entity;
    using CarrierWCF.Model;
    using CarrierWCF.Models;
    using ClientUtilsDll;
    using DBTools;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OperationWCF;
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using ExecutionResult = DBTools.ExecutionResult;
    using Packages = Models.Packages;

    public class ICTToCarrierService : HttpHosting, IICTToCarrierService
    {
        private corebridge core;
        private DBTools.ExecutionResult exeRes;
        private const int LOOP_COUT = 3;

        public ICTToCarrierService()
        {
            ClientUtils.ServerUrl = "http://10.171.16.201:8090/WCF_RemoteObject";
        }
        private T callAPI<T>(string Url, object data)
        {
            HttpResponseMessage result = HttpClientExtensions.PostAsJsonAsync<object>(new HttpClient(), Url, data).Result;
            HttpStatusCode statusCode = result.StatusCode;
            if (statusCode == HttpStatusCode.OK)
            {
                return HttpContentExtensions.ReadAsAsync<T>(result.Content).Result;
            }
            if (statusCode != HttpStatusCode.BadRequest)
            {
                throw new Exception(result.StatusCode.ToString());
            }
            JObject obj2 = HttpContentExtensions.ReadAsAsync<JObject>(result.Content).Result;
            throw new Exception(result.StatusCode.ToString() + "\t" + obj2["Message"].ToString());
        }


        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }
        /// <summary>
        /// 1 carton = 1 request
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string ShipDevTest(string data)
        {
            this.core = new corebridge();
            exeRes = new DBTools.ExecutionResult { Status = true, Message = "OK" };
            ShipModel model = new ShipModel();
            ShipOutputModel model2 = new ShipOutputModel();
            try
            {
                model = JsonConvert.DeserializeObject<ShipModel>(data);
                model2 = this.callAPI<ShipOutputModel>(ServiceUrl + "/Ship", JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(model)).ToObject<object>());
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            finally
            {
                if (exeRes.Message == "OK")
                    exeRes.Message = JsonConvert.SerializeObject(model2);
            }
            return exeRes.Message;
        }

        public string Ship(string data)
        {
            int loop = 0;
            string res = "";
            ShipModel model = new ShipModel();
            try
            {
                model = JsonConvert.DeserializeObject<ShipModel>(data);
                do
                {
                    try
                    {
                        loop++;
                        res = DefaultShip(model);
                        if (res == "OK")
                            break; //if no exeception then break call api
                    }
                    catch (Exception ex)
                    {
                        res = ex.Message;
                    }
                    finally
                    {
                        if (loop >= 3 && res != "OK")
                        {
                            //send mail
                            SendMailAlert(model.ShipmentRequest.Packages[0].MiscReference5, res);
                        }
                    }
                } while (loop < LOOP_COUT);
            }
            catch (Exception ex)
            {
                res = ex.Message;
            }
            return res;
        }

        //public string DefaultShip(string data)
        public string DefaultShip(ShipModel model)
        {
            this.core = new corebridge();
            exeRes = new DBTools.ExecutionResult { Status = true, Message = "OK" };
            //ShipModel model = new ShipModel();
            ShipOutputModel model2 = new ShipOutputModel();

            try
            {
                bool isDuplicate = false;
                //model = JsonConvert.DeserializeObject<ShipModel>(data);
                model2 = this.callAPI<ShipOutputModel>(ServiceUrl + "/Ship", JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(model)).ToObject<object>());
                if (!model2.ErrorCode.Equals(0))
                {
                    exeRes.Status = false;
                    exeRes.Message = string.Format("NG:{0} : {1}", model2.ErrorCode, model2.ErrorMessage);
                }
                if (!model2.ShipmentResponse.PackageDefaults.ErrorCode.Equals(0) || !this.exeRes.Status)
                {
                    if (model2.ShipmentResponse.PackageDefaults.ErrorCode == 1001 && model2.ShipmentResponse.PackageDefaults.ErrorMessage.Contains("Duplicate Tracking Number"))
                    {
                        isDuplicate = true;
                        var objSearch = JsonConvert.DeserializeObject<ExecutionResult>(GetGlbMSNByTrackingNo(model.ShipmentRequest.Packages[0].TrackingNumber));
                        if (objSearch.Status)
                        {
                            var rawObj = new UPSRawDataEntity()
                            {
                                CARTON_NO = model.ShipmentRequest.Packages[0].MiscReference5,
                                TRACKING_NO = model.ShipmentRequest.Packages[0].TrackingNumber,
                                GLOBALMSN = objSearch.Anything.ToString(),
                            };
                            var reReq = RePrint(JsonConvert.SerializeObject(rawObj));
                            if (reReq != "OK")
                            {
                                exeRes.Status = false;
                                exeRes.Message = reReq;
                            }
                        }
                        else
                        {
                            exeRes.Status = false;
                            exeRes.Message = objSearch.Message;
                        }
                    }
                    else
                    {
                        exeRes.Status = false;
                        exeRes.Message = string.Format("NG:SHIP_PackageDefaults {0} : {1}", model2.ShipmentResponse.PackageDefaults.ErrorCode, model2.ShipmentResponse.PackageDefaults.ErrorMessage);
                    }
                }
                if (exeRes.Status && !isDuplicate)
                {
                    foreach (Package package in model2.ShipmentResponse.Packages)
                    {
                        int errorCode = package.ErrorCode;
                        if (!errorCode.Equals(0))
                        {
                            exeRes.Status = false;
                            exeRes.Message = string.Format("NG:Packages {0} : {1}", package.ErrorCode, package.ErrorMessage);
                        }
                        foreach (var doc in package.Documents)
                        {
                            //如果没有获取到label document data直接回滚掉ups 中数据，并返回报错信息
                            if (!doc.ErrorCode.Equals(0))
                            {
                                Void(JsonConvert.SerializeObject(new VoidRequestModel
                                {
                                    ClientAccessCredentials = new ClientAccessCredentials(),
                                    UserContext = new UserContext(),
                                    GlobalMsns = new int[] { package.GlobalMsn }
                                }));
                                exeRes.Status = false;
                                exeRes.Message = string.Format("NG:Document {0} : has no label data", doc.ErrorCode);
                            }
                        }
                        if (package.Documents[0].ErrorCode.Equals(0))
                        {
                            var rawObj = new UPSRawDataEntity();
                            rawObj.GLOBALMSN = package.GlobalMsn.ToString();
                            rawObj.CARTON_NO = model.ShipmentRequest.Packages.Where(x => x.TrackingNumber == package.TrackingNumber).FirstOrDefault().MiscReference5;
                            rawObj.TRACKING_NO = package.TrackingNumber;
                            rawObj.DELIVERY_NO = model.ShipmentRequest.PackageDefaults.ShipperReference;
                            rawObj.RAWDATA = package.Documents[0].RawData[0];
                            exeRes = this.core.InsertRawData(rawObj);
                        }
                    }

                    if (exeRes.Status)
                    {
                        exeRes = this.core.InsertResponseData(model, model2);
                    }
                }
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            finally
            {
                this.core.WriteLog(JsonConvert.SerializeObject(model), "SHIP", exeRes.Status, exeRes.Status ? JsonConvert.SerializeObject(model2) : exeRes.Message,
                    "ICT-UPS", model.ShipmentRequest.PackageDefaults.ShipperReference, model.ShipmentRequest.Packages[0].MiscReference5);
                //this.core.WriteLog(data, "SHIP", exeRes.Status, exeRes.Status ? JsonConvert.SerializeObject(model2) : exeRes.Message, "ICT-UPS", model.ShipmentRequest.PackageDefaults.ShipperReference);
            }
            return exeRes.Message;
        }

        public string Void(string data)
        {
            this.core = new corebridge();
            exeRes = new ExecutionResult { Status = true, Message = "OK" };
            VoidResponseModel Res = new VoidResponseModel();
            var objTemp = new VoidRequestModel();
            try
            {
                objTemp = JsonConvert.DeserializeObject<VoidRequestModel>(data);
                objTemp.ClientAccessCredentials = new ClientAccessCredentials();
                objTemp.UserContext = new UserContext();
                JObject obj2 = JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(objTemp));
                Res = this.callAPI<VoidResponseModel>(ServiceUrl + "/voidpackages", obj2.ToObject<object>());
                if (!Res.ErrorCode.Equals(0))
                {
                    exeRes.Status = false;
                    exeRes.Message = string.Format("NG:{0} : {1}", Res.ErrorCode, "Cancel SN Fail!");
                }
                if (exeRes.Status)
                {
                    foreach (var item in Res.Packages)
                    {
                        if (!item.ErrorCode.Equals(0))
                        {
                            exeRes.Status = false;
                            exeRes.Message = string.Format("NG:Packages {0} : {1}", item.ErrorCode, " Fail!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            finally
            {
                this.core.WriteLog(JsonConvert.SerializeObject(objTemp), "VOID", exeRes.Status, exeRes.Status ? JsonConvert.SerializeObject(Res) : exeRes.Message, "ICT-UPS", "");
            }
            return exeRes.Message;
        }

        public static string ServiceUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["UPSUrl"];
                // return ConfigurationManager.AppSettings["https://webservice.uat.apple.shipexec.com/ShippingService.svc/rest"];
            }
        }

        public string GetGlbMSNByTrackingNo(string tracingNo)
        {
            this.core = new corebridge();
            exeRes = new ExecutionResult { Status = true, Message = "OK" };
            SearchRequestModel Mdel = new SearchRequestModel()
            {
                ClientAccessCredentials = new ClientAccessCredentials(),
                UserContext = new UserContext(),
                SearchCriteria = new SearchCriteria()
                {
                    Skip = "0",
                    Take = "10",
                    WhereClauses = new WhereClauses[1],
                    OrderByClauses = new OrderByClauses[1]
                }
            };
            Mdel.SearchCriteria.WhereClauses[0] = new WhereClauses()
            {
                FieldName = "TrackingNumber",
                FieldValue = tracingNo,
                Operator = "0"
            };

            Mdel.SearchCriteria.OrderByClauses[0] = new OrderByClauses()
            {
                FieldName = "GlobalMsn",
                Direction = "desc"
            };
            SearchOutputModel Mdel2 = new SearchOutputModel();
            try
            {
                Mdel2 = this.callAPI<SearchOutputModel>(ServiceUrl + "/SearchPackageHistory", JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(Mdel)).ToObject<object>());
                if (!Mdel2.ErrorCode.Equals(0))
                {
                    exeRes.Status = false;
                    exeRes.Message = string.Format("NG:{0} : {1}", Mdel2.ErrorCode, "Search ERROR");
                }
                if (exeRes.Status)
                {
                    if (!Mdel2.Packages[0].ErrorCode.Equals(0))
                    {
                        exeRes.Status = false;
                        exeRes.Message = string.Format("NG:Packages {0} : {1}", Mdel2.Packages[0].ErrorCode, Mdel2.Packages[0].ErrorMessage);
                    }
                    else
                    {
                        exeRes.Anything = Mdel2.Packages[0].GlobalMsn;
                    }
                }
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = string.Format("NG:{0} : {1}", "UPS_SEARCH", ex.Message);
            }
            finally
            {
                this.core.WriteLog(JsonConvert.SerializeObject(Mdel), "UPS_SEARCH", exeRes.Status, exeRes.Status ? JsonConvert.SerializeObject(Mdel2) : exeRes.Message, "ICT-UPS", "");
            }
            return JsonConvert.SerializeObject(exeRes);
        }
        public string RePrint(string data)
        {
            this.core = new corebridge();
            exeRes = new ExecutionResult { Status = true, Message = "OK" };
            RePrintResponseModel Mdel2 = new RePrintResponseModel();
            RePrintReqUPSModel Mdel = new RePrintReqUPSModel();
            try
            {
                var rawOjb = JsonConvert.DeserializeObject<UPSRawDataEntity>(data);
                Mdel.PrintConfiguration = new PrintConfiguration()
                { GlobalMsn = rawOjb.GLOBALMSN };
                Mdel.ClientAccessCredentials = new ClientAccessCredentials();
                Mdel.UserContext = new UserContext();
                Mdel2 = this.callAPI<RePrintResponseModel>(ServiceUrl + "/Print", JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(Mdel)).ToObject<object>());
                if (!Mdel2.ErrorCode.Equals(0))
                {
                    exeRes.Status = false;
                    exeRes.Message = string.Format("NG:{0} : {1}", Mdel2.ErrorCode, Mdel2.ErrorMessage);
                }
                if (exeRes.Status)
                {
                    rawOjb.RAWDATA = Mdel2.DocumentResponses[0].RawData[0];
                    this.core.InsertRawData(rawOjb);
                }
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = string.Format("NG:{0} : {1}", "UPS_REPRINT", ex.Message);
            }
            finally
            {
                this.core.WriteLog(JsonConvert.SerializeObject(Mdel), "UPS_REPRINT", exeRes.Status, exeRes.Status ? JsonConvert.SerializeObject(Mdel2) : exeRes.Message, "ICT-UPS", "");
            }
            return exeRes.Message;
        }

        public async void SendMailAlert(string carton, string msg)
        {
            await System.Threading.Tasks.Task.Run(() => this.core.InsertMailTest(carton, msg));
        }
    }
}

