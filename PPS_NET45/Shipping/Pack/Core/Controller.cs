using CRReport.CRfrom;
using LibHelper;
using Oracle.ManagedDataAccess.Client;
using Packingparcel.Dao;
using Packingparcel.Entitys;
using Packingparcel.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Packingparcel.Wcf;
using Newtonsoft.Json;
using Spire.Pdf;
using System.Drawing.Printing;
using System.Text;

namespace Packingparcel.Core
{
    class Controller
    {
        private SelectData selectData;
        private PrintLabel printLabel;
        public Controller()
        {
            selectData = new SelectData();
            printLabel = new PrintLabel();
        }

        public ExecuteResult getShipMentInfoByshipmentId(string shipMentId)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = selectData.getShipMentInfoByshipmentId(shipMentId);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此集货箱号:" + shipMentId + " 查不到资料！";
            }
            return exeRes;
        }
        public ExecuteResult createUpsFile(string cartonNo, ShipmentInfo shipmentInfo)
        {
            /*
             * 文件路径：
             MESCLIENT\ALL_CARRIER\UPS_CARRIER\UPS_CARRIER_BACKUP\DATENOW\SHIPMENTID\UPSCODE--自己备份
                                              \DATENOW\UPSCODE                         --客户读取，并删除
             */
            ExecuteResult exeRes = new ExecuteResult();
            string UPS_TRACKING_NO = "";
            string upsFileContent = "";
            string upsBackUpTotalPath = "";
            //      string upsTotalPath = "";
            try
            {
                string startPath = Application.StartupPath + @"\ALL_CARRIER\UPS_CARRIER";
                if (!Directory.Exists(startPath))
                {
                    Directory.CreateDirectory(startPath);
                }
                string secBackUpDir = startPath + @"\UPS_CARRIER_BACKUP\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + shipmentInfo.ShipmentId;//备份dir
                if (!Directory.Exists(secBackUpDir))
                {
                    Directory.CreateDirectory(secBackUpDir);
                }
                exeRes = getUpsCodeByCartonNo(cartonNo);
                if (!exeRes.Status)
                {
                    return exeRes;
                }
                UPS_TRACKING_NO = (string)exeRes.Anything;
                //判断新旧UPS交互方式 Franky
                string sql = @"select count(*) as flag from ppsuser.t_basicparameter_info where para_type='UPS_URL' and enabled='Y'";
                if (Convert.ToInt32(ClientUtils.ExecuteSQL(sql).Tables[0].Rows[0]["flag"].ToString()) > 0)
                {
                    string Result = "NG";
                    upsBackUpTotalPath = secBackUpDir + @"\" + UPS_TRACKING_NO + ".pdf";
                    //先检查是否为补印状态：db中有之前获取到label data 有则默认为补印状态
                    DataTable dtLabel = ClientUtils.ExecuteSQL(string.Format(@"select  count(1) from ppsuser.t_ups_rawdata a where a.carton_no='{0}' and a.rawdata is not null", cartonNo)).Tables[0];
                    if (Convert.ToInt32(dtLabel.Rows[0][0].ToString()) > 0)
                    //有数据则是补印状态
                    {
                        Result = "OK";
                    }
                    else
                    {
                        #region UPS新交互方式 请求wcf 获取label data
                        string ppsURL = "";
                        string msg = "";
                        var res = selectData.GetDBTypeBySP("ICTSerivce_URL", out ppsURL, out msg);

                        if (!String.IsNullOrWhiteSpace(ppsURL))
                        {
                            CarrierWCF.Wcf.IICTToCarrierService WS = OperationWCF.HttpChannel.Get<CarrierWCF.Wcf.IICTToCarrierService>(ppsURL);
                            //UpsWcf.ICTToCarrierService UpsService = new UpsWcf.ICTToCarrierService();
                            ShipRequestModel shipRequest = new ShipRequestModel();
                            //exeRes = getRequestData(cartonNo, shipmentInfo.Region, out shipRequest);getRequestShipexec
                            exeRes = getRequestShipexec(cartonNo, shipmentInfo.Region, out shipRequest);
                            if (!exeRes.Status)
                            {
                                return exeRes;
                            }
                            Result = WS.Ship(JsonConvert.SerializeObject(shipRequest));
                        }
                        else
                        {
                            exeRes.Status = false;
                            exeRes.Message = "NG_UPS ShipExec配置有异常，请再检查！";
                        }

                        #endregion
                    }

                    if (!Result.StartsWith("NG"))//交互结果成功
                    {
                        #region 从DB中拉取标签数据列印
                        string labeldata = ClientUtils.ExecuteSQL(string.Format(@"select rawdata from ppsuser.t_ups_rawdata a where a.carton_no='{0}' ", cartonNo)).Tables[0].Rows[0][0].ToString();
                        using (var stream = new System.IO.FileStream(upsBackUpTotalPath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write))
                        {
                            //byte[] data = Convert.FromBase64String(labeldata);
                            //string ZPLString = Encoding.UTF8.GetString(data);

                            if (string.IsNullOrEmpty(fMain.Printer))
                            {
                                fMain.Printer = this.GetUPSPrinter();
                            }

                            if (!string.IsNullOrEmpty(fMain.Printer))
                            {
                                RawPrinterHelper.SendStringToPrinter(fMain.Printer, labeldata);
                            }
                            else
                            {
                                exeRes.Status = false;
                                exeRes.Message = "NG_UPS Carrier Label 未指定打印机";
                            }
                        }
                    }
                    else
                    {
                        exeRes.Status = false;
                        exeRes.Message = Result;
                    }
                    #endregion
                }
                else
                {
                    #region UPS 旧交互方式 抛转文件产生transin file
                    exeRes = getUpsInfoByCartonNo(cartonNo, shipmentInfo.Region);
                    if (!exeRes.Status)
                    {
                        return exeRes;
                    }
                    upsFileContent = (string)exeRes.Anything;
                    upsBackUpTotalPath = secBackUpDir + @"\" + UPS_TRACKING_NO + ".ups";

                    string ftpFlag = "N";
                    string msg = "";
                    string ftpFlagCheck = selectData.GetDBTypeBySP("FTP_UPS", out ftpFlag, out msg);
                    if (ftpFlag == "Y")
                    {
                        string xmlMsg = String.Empty;
                        var list = GetFilePathByCarrierFTP("UPS", out xmlMsg);
                        if (xmlMsg.IndexOf("NG") >= 0)
                        {
                            exeRes.Status = false;
                            exeRes.Message = xmlMsg;
                            return exeRes;
                        }
                        else
                            exeRes = WriteAndReadUtil.writeToByFilePathAndFileContentFTP(upsBackUpTotalPath, list, upsFileContent);
                    }
                    else
                    {
                        // upsTotalPath = secDir + @"\" + UPS_TRACKING_NO + ".ups";
                        //add carrier multi-Net Disk by Franky 2019年11月11日 
                        List<string> lisFilePath = this.GetFilePathByCarrier("UPS");
                        if (lisFilePath.Count < 1)
                        {
                            exeRes.Status = false;
                            exeRes.Message = "未维护货代标签地址网盘！";
                            return exeRes;
                        }
                        else
                        {
                            for (int i = 0; i < lisFilePath.Count; i++)
                            {
                                lisFilePath[i] = lisFilePath[i] + @"\" + UPS_TRACKING_NO + ".ups";
                            }
                        }
                        exeRes = WriteAndReadUtil.writeToByFilePathAndFileContent(upsBackUpTotalPath, lisFilePath, upsFileContent);
                    }
                    #endregion
                }

                if (!exeRes.Status)
                {
                    return exeRes;
                }
                exeRes.Message = "OK_UPS货代已经打印！";
            }
            catch (Exception e)
            {
                exeRes.Status = false;
                exeRes.Message = e.Message;
            }
            return exeRes;
        }
        public ExecuteResult createFedFile(string cartonNo, ShipmentInfo shipmentInfo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            string fedFileContent = "";
            string fedBackUpTotalPath = "";
            string fedTotalPath = "";
            try
            {
                string startPath = Application.StartupPath + @"\ALL_CARRIER\FEDEX_CARRIER";
                if (!Directory.Exists(startPath))
                {
                    Directory.CreateDirectory(startPath);
                }
                string secBackUpDir = startPath + @"\FEDEX_CARRIER_BACKUP\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + shipmentInfo.ShipmentId;//备份dir
                if (!Directory.Exists(secBackUpDir))
                {
                    Directory.CreateDirectory(secBackUpDir);
                }
                //   string secDir = @"X:";
                exeRes = getFedInfoByCartonNo(cartonNo);
                if (!exeRes.Status)
                {
                    return exeRes;
                }
                fedFileContent = (string)exeRes.Anything;
                fedBackUpTotalPath = secBackUpDir + @"\" + cartonNo + ".in";
                // fedTotalPath = secDir + @"\" + cartonNo + ".in";

                string ftpFlag = "N";
                string msg = "";
                string ftpFlagCheck = selectData.GetDBTypeBySP("FTP_FEDEX", out ftpFlag, out msg);
                if (ftpFlag == "Y")
                {
                    string xmlMsg = String.Empty;
                    var list = GetFilePathByCarrierFTP("FEDEX", out xmlMsg);
                    if (xmlMsg.IndexOf("NG") >= 0)
                    {
                        exeRes.Status = false;
                        exeRes.Message = xmlMsg;
                        return exeRes;
                    }
                    else
                        exeRes = WriteAndReadUtil.writeToByFilePathAndFileContentFTP(fedBackUpTotalPath, list, fedFileContent);
                }
                else
                {
                    List<string> lisFilePath = this.GetFilePathByCarrier("FEDEX");
                    if (lisFilePath.Count < 1)
                    {
                        exeRes.Status = false;
                        exeRes.Message = "未维护货代标签地址网盘！";
                        return exeRes;
                    }
                    else
                    {
                        for (int i = 0; i < lisFilePath.Count; i++)
                        {
                            lisFilePath[i] = lisFilePath[i] + @"\" + cartonNo + ".in";
                        }
                    }
                    exeRes = WriteAndReadUtil.writeToByFilePathAndFileContent(fedBackUpTotalPath, lisFilePath, fedFileContent);
                    //exeRes = WriteAndReadUtil.writeToByFilePathAndFileContent(fedBackUpTotalPath, fedTotalPath, fedFileContent);
                }
                if (!exeRes.Status)
                {
                    return exeRes;
                }
                exeRes.Message = "OK_FEDEX货代已经打印！";
            }
            catch (Exception e)
            {
                exeRes.Status = false;
                exeRes.Message = e.Message;
            }
            return exeRes;
        }

        public bool GetSFShiptoCNTYCODE(string shipMentId)
        {
            DataTable dtTemp = selectData.GetSFShiptoCNTYCODE(shipMentId);
            if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public ExecuteResult CheckSIDLineControl(string strSID, string strComputer)
        {
            //ppsuser.sp_pack_checksidline(insid          in varchar2,
            //                                            incomputername in varchar2,
            //                                            errmsg         out varchar2) as
            ExecuteResult exeRes = new ExecuteResult();
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insid", strSID };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incomputername", strComputer };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            //DataSet ds = ClientUtils.ExecuteProc("ppsuser.sp_pack_checksidline", procParams);
            DataSet ds = new DataSet();
            try
            {
                ds = ClientUtils.ExecuteProc("PPSUSER.sp_pack_checksidline", procParams);
            }
            catch (Exception e)
            {
                exeRes.Status = false;
                exeRes.Message = e.ToString();
                return exeRes;
            }
            if (ds.Tables[0].Rows[0]["errmsg"].ToString().Equals("OK"))
            {
                exeRes.Status = true;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = ds.Tables[0].Rows[0]["errmsg"].ToString();
            }
            return exeRes;
        }
        public ExecuteResult getRequestData(string cartonNo, string region, out ShipRequestModel shipRequest)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            shipRequest = new ShipRequestModel();
            //仿照原本获取transinfile 查询逻辑 修改个别字段后查询sql
            dt = selectData.getUpsInfoByCartonNo(cartonNo, region);
            if (dt != null && dt.Rows.Count > 0)
            {
                PackageDefaults packageDefaults = new PackageDefaults
                {
                    Service = new Service(),
                    Consignee = new Consignee //收件人
                    {
                        Address1 = dt.Rows[0]["ST_ADDR1"].ToString(),
                        City = dt.Rows[0]["SHIPTOCITY"].ToString(),
                        Company = dt.Rows[0]["SHIPTOCOMPANY"].ToString(),
                        Contact = dt.Rows[0]["SHIPTONAME"].ToString(),
                        Country = dt.Rows[0]["SHIPCNTYCODE"].ToString(),
                        Phone = dt.Rows[0]["SHIPTOCONTTEL"].ToString(),
                        PostalCode = dt.Rows[0]["SHIPTOZIP"].ToString(),
                        //RegionDesc  如果有 = 号码, 那么取 = 号之前的
                        StateProvince = dt.Rows[0]["REGIONDESC"].ToString().Contains("=") ? dt.Rows[0]["REGIONDESC"].ToString().Split('=')[0] : dt.Rows[0]["REGIONDESC"].ToString()
                    },
                    #region 待取消--测试时使用写死测试数据  UNKNOW
                    //ImporterOfRecord = new ImporterOfRecord
                    //{
                    //    Account = "X3332V",
                    //    Address1 = "1 Infinite Loop 3-IE",
                    //    City = "Cupertino",
                    //    Company = "Apple Computer Inc",
                    //    Contact = "c/o Customs Clearance",
                    //    Country = "US",
                    //    PostalCode = "95014",
                    //    StateProvince = "CA",
                    //    TaxId = "94-240411000"
                    //},
                    //ReturnAddress = new ReturnAddress
                    //{
                    //    Address1 = "558 ALD BLVD",
                    //    City = "Mt. Juliet",
                    //    Company = "A.I.",
                    //    Country = "US",
                    //    PostalCode = "37122",
                    //    StateProvince = "TN"
                    //},
                    //ThirdPartyBilling = "1",
                    //ThirdPartyBillingAddress = new ThirdPartyBillingAddress
                    //{
                    //    Account = "100Y00",
                    //    Address1 = "5505 W Parmer Lane, Bldg 4",
                    //    Address2 = "MS:186-LPM",
                    //    City = "Austin",
                    //    Company = "APPLE",
                    //    Contact = "C/O Freight Payment",
                    //    Country = "US",
                    //    PostalCode = "78727-4021",
                    //    StateProvince = "TX"
                    //},
                    #endregion
                    Shipdate = new Shipdate // SHIP TIME 
                    {
                        Year = dt.Rows[0]["SHIPDATE"].ToString().Split('/')[0],
                        Month = dt.Rows[0]["SHIPDATE"].ToString().Split('/')[1],
                        Day = dt.Rows[0]["SHIPDATE"].ToString().Split('/')[2]
                    },
                    Shipper = "AAPL_123456",// dt.Rows[0]["SHIPER_CORP_NAME"].ToString(), 报错临时写定值
                    ShipperReference = dt.Rows[0]["DELIVERY_NO"].ToString(),//DN
                    ConsigneeReference = dt.Rows[0]["WEBORDERNO"].ToString()//WEB ORDER NO 
                };
                CommodityContents contents = new CommodityContents
                {
                    Description = "ACCESSORY",
                    OriginCountry = "IRELAND",//"Vietnam",//报错写死
                    ProductCode = "",
                    Quantity = dt.Rows[0]["PERCARTONQTY"].ToString(),
                    QuantityUnitMeasure = "PCS",
                    UnitValue = new UnitValue
                    {
                        Amount = dt.Rows[0]["SHIPMENT_TOTAL_VALUE"].ToString(),//总价钱
                        Currency = "USD"
                    },
                    UnitWeight = new UnitWeight
                    {
                        Amount = dt.Rows[0]["DN_TOTAL_WEIGHT"].ToString(),//单箱重量
                        Units = "KGS"
                    }
                };
                Packages packages = new Packages
                {
                    Description = "ACCESSORY",
                    CommodityContents = new CommodityContents[] { contents },
                    MiscReference1 = dt.Rows[0]["AC_PN"].ToString(),
                    MiscReference2 = dt.Rows[0]["SSCC"].ToString(),
                    MiscReference3 = dt.Rows[0]["SAWB"].ToString(),
                    TrackingNumber = dt.Rows[0]["TRACKING_NO"].ToString(),
                    Weight = new Weight
                    {
                        Amount = dt.Rows[0]["DN_TOTAL_WEIGHT"].ToString(),//单箱重量
                        Units = "KG"
                    },
                    WorldEaseCode = dt.Rows[0]["HAWB"].ToString(),
                    WorldEaseFlag = "1"
                };
                shipRequest = new ShipRequestModel
                {
                    ClientAccessCredentials = new ClientAccessCredentials(),
                    UserContext = new UserContext(),
                    ShipmentRequest = new ShipmentRequest
                    {
                        PackageDefaults = packageDefaults,
                        Packages = new Packages[] { packages }
                    }
                };
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "未能查询到Ups信息，请联系IT-PPS!";
            }
            return exeRes;
        }


        /// <summary>
        /// 根据货代查询目的路径
        /// </summary>
        /// <param name="strCarrier">货代名称</param>
        /// <returns>路径数组</returns>
        private List<string> GetFilePathByCarrier(string strCarrier)
        {
            string strSQL = string.Format(@"
                                           select a.transfile_path
                                          from ppsuser.t_transfilepath_info a
                                         where a.carrier_abbr = '{0}'
                                            ", strCarrier);
            DataTable dt = ClientUtils.ExecuteSQL(strSQL).Tables[0];
            return dt.AsEnumerable().Select(d => d.Field<string>("transfile_path")).ToList();
        }
        private List<string> GetFilePathByCarrierFTP(string strCarrier, out string msg)
        {
            List<string> lst = new List<string>();
            msg = "OK";
            try
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar.ToString() + "PPS.xml");
                lst = dataSet.Tables["FTP_CARRIER"].AsEnumerable().Where(x => x.Field<string>("CARRIER_ABBR") == strCarrier)
                                                                    .Select(d => d.Field<string>("IP")).ToList();
            }
            catch
            {
                msg = "NG-" + strCarrier + " FTP配置有异常，请在PPS.xml文件检查！";
            }
            return lst;
        }

        public ExecuteResult createTransInFile(string cartonNo, string shipmentId)
        {
            /*
             * 文件路径：
             MESCLIENT\ALL_CARRIER\TNT_CARRIER\TNT_CARRIER_BACKUP\DATENOW\SHIPMENTID\tntCode(tss-trackingNo)--自己备份
                                              \DATENOW\tntCode                         --客户读取，并删除
             */
            ExecuteResult exeRes = new ExecuteResult();
            string tntCode = "";
            string transInFileContent = "";
            string tntBackUpTotalPath = "";
            //  string tntTotalPath = "";
            try
            {
                string startPath = Application.StartupPath + @"\ALL_CARRIER\TNT_CARRIER";
                if (!Directory.Exists(startPath))
                {
                    Directory.CreateDirectory(startPath);
                }
                string secBackUpDir = startPath + @"\TNT_CARRIER_BACKUP\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + shipmentId;//备份dir
                if (!Directory.Exists(secBackUpDir))
                {
                    Directory.CreateDirectory(secBackUpDir);
                }
                //string secDir = startPath + @"\" + DateTime.Now.ToString("yyyyMMdd");//备份dir
                //if (!Directory.Exists(secDir))
                //{
                //    Directory.CreateDirectory(secDir);
                //}
                //string secDir = @"T:";
                exeRes = getUpsCodeByCartonNo(cartonNo);
                if (!exeRes.Status)
                {
                    return exeRes;
                }
                tntCode = (string)exeRes.Anything;
                exeRes = getTransInFileInfo(cartonNo);
                if (!exeRes.Status)
                {
                    return exeRes;
                }
                transInFileContent = (string)exeRes.Anything;
                tntBackUpTotalPath = secBackUpDir + @"\" + cartonNo + "_" + tntCode + ".txt";

                string ftpFlag = "N";
                string msg = "";
                string ftpFlagCheck = selectData.GetDBTypeBySP("FTP_TNT", out ftpFlag, out msg);
                if (ftpFlag == "Y")
                {
                    string xmlMsg = String.Empty;
                    var list = GetFilePathByCarrierFTP("TNT", out xmlMsg);
                    if (xmlMsg.IndexOf("NG") >= 0)
                    {
                        exeRes.Status = false;
                        exeRes.Message = xmlMsg;
                        return exeRes;
                    }
                    else
                        exeRes = WriteAndReadUtil.writeToByFilePathAndFileContentFTP(tntBackUpTotalPath, list, transInFileContent);
                }
                else
                {
                    //  tntTotalPath = secDir + @"\" + cartonNo + "_" + tntCode + ".txt";
                    //add carrier multi-Net Disk by Franky 2019年11月11日 
                    List<string> lisFilePath = this.GetFilePathByCarrier("TNT");
                    if (lisFilePath.Count < 1)
                    {
                        exeRes.Status = false;
                        exeRes.Message = "未维护货代标签地址网盘！";
                        return exeRes;
                    }
                    else
                    {
                        for (int i = 0; i < lisFilePath.Count; i++)
                        {
                            lisFilePath[i] = lisFilePath[i] + @"\" + cartonNo + "_" + tntCode + ".txt";
                        }
                    }
                    //  exeRes = WriteAndReadUtil.writeToByFilePathAndFileContent(tntBackUpTotalPath, tntTotalPath, transInFileContent);
                    exeRes = WriteAndReadUtil.writeToByFilePathAndFileContent(tntBackUpTotalPath, lisFilePath, transInFileContent);
                }
                if (!exeRes.Status)
                {
                    return exeRes;
                }
                exeRes.Message = "OK_TNT货代已经打印！";
            }
            catch (Exception e)
            {
                exeRes.Status = false;
                exeRes.Message = e.Message;
            }
            return exeRes;
        }
        public ExecuteResult createImportWPXFile(string cartonNo, string shipmentId)
        {
            /*
             * 文件路径：
             MESCLIENT\ALL_CARRIER\WPXFILE\BACKUP_WPXFILE\DATENOW\SHIPMENTID\CARTONNO--自己备份
                                  \DATENOW\CARTONNO                         --客户读取，并删除
             */
            ExecuteResult exeRes = new ExecuteResult();
            string ImportWPXFileContent = "";
            string wpxBackUpTotalPath = "";
            // string wpxTotalPath = "";
            try
            {
                string startPath = Application.StartupPath + @"\ALL_CARRIER\DHL_WPX_CARRIER";
                if (!Directory.Exists(startPath))
                {
                    Directory.CreateDirectory(startPath);
                }
                string secBackUpDir = startPath + @"\DHL_WPX_CARRIER_BACKUP\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + shipmentId;//备份dir
                if (!Directory.Exists(secBackUpDir))
                {
                    Directory.CreateDirectory(secBackUpDir);
                }
                //string secDir = startPath + @"\" + DateTime.Now.ToString("yyyyMMdd");//备份dir
                //if (!Directory.Exists(secDir))
                //{
                //    Directory.CreateDirectory(secDir);
                //}
                //   string secDir = @"L:";
                exeRes = getSps203ImportWpx(cartonNo);
                if (!exeRes.Status)
                {
                    return exeRes;
                }
                ImportWPXFileContent = (string)exeRes.Anything;
                wpxBackUpTotalPath = secBackUpDir + @"\" + cartonNo + ".sps";
                //wpxTotalPath = secDir + @"\" + cartonNo + ".sps";

                string ftpFlag = "N";
                string msg = "";
                string ftpFlagCheck = selectData.GetDBTypeBySP("FTP_DHL", out ftpFlag, out msg);
                if (ftpFlag == "Y")
                {
                    string xmlMsg = String.Empty;
                    var list = GetFilePathByCarrierFTP("DHL", out xmlMsg);
                    if (xmlMsg.IndexOf("NG") >= 0)
                    {
                        exeRes.Status = false;
                        exeRes.Message = xmlMsg;
                        return exeRes;
                    }
                    else
                        exeRes = WriteAndReadUtil.writeToByFilePathAndFileContentFTP(wpxBackUpTotalPath, list, ImportWPXFileContent);
                }
                else
                {
                    //add carrier multi-Net Disk by Franky 2019年11月11日 
                    List<string> lisFilePath = this.GetFilePathByCarrier("DHL");
                    if (lisFilePath.Count < 1)
                    {
                        exeRes.Status = false;
                        exeRes.Message = "未维护货代标签地址网盘！";
                        return exeRes;
                    }
                    else
                    {
                        for (int i = 0; i < lisFilePath.Count; i++)
                        {
                            lisFilePath[i] = lisFilePath[i] + @"\" + cartonNo + ".sps";
                        }
                    }
                    //  exeRes = WriteAndReadUtil.writeToByFilePathAndFileContent(wpxBackUpTotalPath, wpxTotalPath, ImportWPXFileContent;
                    exeRes = WriteAndReadUtil.writeToByFilePathAndFileContent(wpxBackUpTotalPath, lisFilePath, ImportWPXFileContent);
                }
                if (!exeRes.Status)
                {
                    return exeRes;
                }
                exeRes.Message = "OK_DHL_WPX货代已经打印！";
            }
            catch (Exception e)
            {
                exeRes.Status = false;
                exeRes.Message = e.Message;
            }
            return exeRes;
        }

        public ExecuteResult createYMTInfoFile(string cartonNo, string shipmentId)
        {
            /*
             * 文件路径：
             MESCLIENT\YMTFILE\BACKUP_YMTFILE\DATENOW\SHIPMENTID\CARTONNO--自己备份
                                  \DATENOW\CARTONNO                         --客户读取，并删除
             */
            ExecuteResult exeRes = new ExecuteResult();
            string YMTFILEContent = "";
            string YMTFILEBackUpTotalPath = "";
            //string YMTFILETotalPath = "";
            try
            {
                string startPath = Application.StartupPath + @"\ALL_CARRIER\YMT_CARRIER";
                if (!Directory.Exists(startPath))
                {
                    Directory.CreateDirectory(startPath);
                }
                string secBackUpDir = startPath + @"\YMT_CARRIER_BACKUP\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + shipmentId;//备份dir
                if (!Directory.Exists(secBackUpDir))
                {
                    Directory.CreateDirectory(secBackUpDir);
                }
                //string secDir = startPath + @"\" + DateTime.Now.ToString("yyyyMMdd");//备份dir
                //if (!Directory.Exists(secDir))
                //{
                //    Directory.CreateDirectory(secDir);
                //}
                //  string secDir = @"Y:";
                exeRes = getYMT_File_Info_ByCartonNo(cartonNo);
                if (!exeRes.Status)
                {
                    return exeRes;
                }
                YMTFILEContent = (string)exeRes.Anything;
                YMTFILEBackUpTotalPath = secBackUpDir + @"\" + cartonNo + ".dat";
                //     YMTFILETotalPath = secDir + @"\" + cartonNo + ".dat";

                string ftpFlag = "N";
                string msg = "";
                string ftpFlagCheck = selectData.GetDBTypeBySP("FTP_YMT", out ftpFlag, out msg);
                if (ftpFlag == "Y")
                {
                    string xmlMsg = String.Empty;
                    var list = GetFilePathByCarrierFTP("YMT", out xmlMsg);
                    if (xmlMsg.IndexOf("NG") >= 0)
                    {
                        exeRes.Status = false;
                        exeRes.Message = xmlMsg;
                        return exeRes;
                    }
                    else
                        exeRes = WriteAndReadUtil.writeToByFilePathAndFileContentFTP(YMTFILEBackUpTotalPath, list, YMTFILEContent);
                }
                else
                {
                    //add carrier multi-Net Disk by Franky 2019年11月11日 
                    List<string> lisFilePath = this.GetFilePathByCarrier("YMT");
                    if (lisFilePath.Count < 1)
                    {
                        exeRes.Status = false;
                        exeRes.Message = "未维护货代标签地址网盘！";
                        return exeRes;
                    }
                    else
                    {
                        for (int i = 0; i < lisFilePath.Count; i++)
                        {
                            lisFilePath[i] = lisFilePath[i] + @"\" + cartonNo + ".dat";
                        }
                    }
                    //exeRes = WriteAndReadUtil.writeToByFilePathAndFileContent(YMTFILEBackUpTotalPath, YMTFILETotalPath, YMTFILEContent);
                    exeRes = WriteAndReadUtil.writeToByFilePathAndFileContent(YMTFILEBackUpTotalPath, lisFilePath, YMTFILEContent);
                }
                if (!exeRes.Status)
                {
                    return exeRes;
                }
                exeRes.Message = "OK_YMT_货代已经打印！";
            }
            catch (Exception e)
            {
                exeRes.Status = false;
                exeRes.Message = e.Message;
            }
            return exeRes;
        }
        public ExecuteResult createAPPLEECXFile(string cartonNo, string shipmentId)
        {
            /*
             * 文件路径：
             MESCLIENT\ALL_CARRIER\APPLEECX\BACKUP_APPLEECX\DATENOW\SHIPMENTID\CARTONNO--自己备份
                               \DATENOW\CARTONNO                         --客户读取，并删除
             */
            ExecuteResult exeRes = new ExecuteResult();
            string appleEcxFileContent = "";
            string ecxBackUpTotalPath = "";
            //string ecxTotalPath = "";
            try
            {
                string startPath = Application.StartupPath + @"\ALL_CARRIER\DHL_ECX_CARRIER";
                if (!Directory.Exists(startPath))
                {
                    Directory.CreateDirectory(startPath);
                }
                string secBackUpDir = startPath + @"\DHL_ECX_CARRIER_BACKUP\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + shipmentId;//备份dir
                if (!Directory.Exists(secBackUpDir))
                {
                    Directory.CreateDirectory(secBackUpDir);
                }
                //string secDir = startPath + @"\" + DateTime.Now.ToString("yyyyMMdd");//备份dir
                //if (!Directory.Exists(secDir))
                //{
                //    Directory.CreateDirectory(secDir);
                //}
                //     string secDir = @"L:";
                exeRes = getSPSForAppleECXFileInfo(cartonNo);
                if (!exeRes.Status)
                {
                    return exeRes;
                }
                appleEcxFileContent = (string)exeRes.Anything;
                ecxBackUpTotalPath = secBackUpDir + @"\" + cartonNo + ".sps";
                //   ecxTotalPath = secDir + @"\" + cartonNo + ".sps";

                string ftpFlag = "N";
                string msg = "";
                string ftpFlagCheck = selectData.GetDBTypeBySP("FTP_DHL", out ftpFlag, out msg);
                if (ftpFlag == "Y")
                {
                    string xmlMsg = String.Empty;
                    var list = GetFilePathByCarrierFTP("DHL", out xmlMsg);
                    if (xmlMsg.IndexOf("NG") >= 0)
                    {
                        exeRes.Status = false;
                        exeRes.Message = xmlMsg;
                        return exeRes;
                    }
                    else
                        exeRes = WriteAndReadUtil.writeToByFilePathAndFileContentFTP(ecxBackUpTotalPath, list, appleEcxFileContent);
                }
                else
                {
                    //add carrier multi-Net Disk by Franky 2019年11月11日 
                    List<string> lisFilePath = this.GetFilePathByCarrier("DHL");
                    if (lisFilePath.Count < 1)
                    {
                        exeRes.Status = false;
                        exeRes.Message = "未维护货代标签地址网盘！";
                        return exeRes;
                    }
                    else
                    {
                        for (int i = 0; i < lisFilePath.Count; i++)
                        {
                            lisFilePath[i] = lisFilePath[i] + @"\" + cartonNo + ".sps";
                        }
                    }
                    //exeRes = WriteAndReadUtil.writeToByFilePathAndFileContent(ecxBackUpTotalPath, ecxTotalPath, appleEcxFileContent);
                    exeRes = WriteAndReadUtil.writeToByFilePathAndFileContent(ecxBackUpTotalPath, lisFilePath, appleEcxFileContent);
                }
                if (!exeRes.Status)
                {
                    return exeRes;
                }
                exeRes.Message = "OK_DHL_ECX货代已经打印！";
            }
            catch (Exception e)
            {
                exeRes.Status = false;
                exeRes.Message = e.Message;
            }
            return exeRes;
        }
        public ExecuteResult createTransInFileCNPLFileByCartonNo(string cartonNo, string shipmentId)
        {
            /*
             * 文件路径：
             MESCLIENT\TRANSINFILECOD\BACKUP_TRANSINFILECOD\DATENOW\SHIPMENTID\CARTONNO--自己备份
                                     \DATENOW\CARTONNO                         --客户读取，并删除
             */
            ExecuteResult exeRes = new ExecuteResult();
            string TRANSINFILECODFileContent = "";
            string TRANSINFILECODBackUpTotalPath = "";
            //  string TRANSINFILECODTotalPath = "";
            try
            {
                string startPath = Application.StartupPath + @"\ALL_CARRIER\CNPL_CARRIER";
                if (!Directory.Exists(startPath))
                {
                    Directory.CreateDirectory(startPath);
                }
                string secBackUpDir = startPath + @"\CNPL_CARRIER_BACKUP\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + shipmentId;//备份dir
                if (!Directory.Exists(secBackUpDir))
                {
                    Directory.CreateDirectory(secBackUpDir);
                }
                //string secDir = startPath + @"\" + DateTime.Now.ToString("yyyyMMdd");//备份dir
                //if (!Directory.Exists(secDir))
                //{
                //    Directory.CreateDirectory(secDir);
                //}
                //  string secDir = @"P:";
                exeRes = getTransInFileCNPLInfoStringByCartonNo(cartonNo);
                if (!exeRes.Status)
                {
                    return exeRes;
                }
                TRANSINFILECODFileContent = (string)exeRes.Anything;
                TRANSINFILECODBackUpTotalPath = secBackUpDir + @"\" + cartonNo + ".txt";
                //    TRANSINFILECODTotalPath = secDir + @"\" + cartonNo + ".txt";

                string ftpFlag = "N";
                string msg = "";
                string ftpFlagCheck = selectData.GetDBTypeBySP("FTP_CNPL", out ftpFlag, out msg);
                if (ftpFlag == "Y")
                {
                    string xmlMsg = String.Empty;
                    var list = GetFilePathByCarrierFTP("CNPL", out xmlMsg);
                    if (xmlMsg.IndexOf("NG") >= 0)
                    {
                        exeRes.Status = false;
                        exeRes.Message = xmlMsg;
                        return exeRes;
                    }
                    else
                        exeRes = WriteAndReadUtil.writeToByFilePathAndFileContentFTP(TRANSINFILECODBackUpTotalPath, list, TRANSINFILECODFileContent);
                }
                else
                {
                    //add carrier multi-Net Disk by Franky 2019年11月11日 
                    List<string> lisFilePath = this.GetFilePathByCarrier("CNPL");
                    if (lisFilePath.Count < 1)
                    {
                        exeRes.Status = false;
                        exeRes.Message = "未维护货代标签地址网盘！";
                        return exeRes;
                    }
                    else
                    {
                        for (int i = 0; i < lisFilePath.Count; i++)
                        {
                            lisFilePath[i] = lisFilePath[i] + @"\" + cartonNo + ".txt";
                        }
                    }
                    //    exeRes = WriteAndReadUtil.writeToByFilePathAndFileContent(TRANSINFILECODBackUpTotalPath, TRANSINFILECODTotalPath, TRANSINFILECODFileContent);
                    exeRes = WriteAndReadUtil.writeToByFilePathAndFileContent(TRANSINFILECODBackUpTotalPath, lisFilePath, TRANSINFILECODFileContent);
                }
                if (!exeRes.Status)
                {
                    return exeRes;
                }
                exeRes.Message = "OK_CNPL_货代已经打印！";
            }
            catch (Exception e)
            {
                exeRes.Status = false;
                exeRes.Message = e.Message;
            }
            return exeRes;
        }
        public ExecuteResult createBBX_BABYFile(string cartonNo, string shipmentId, string region)
        {
            /*
             * 文件路径：
             MESCLIENT\BBX_BABY\BACKUP_BBX_BABY\DATENOW\SHIPMENTID\CARTONNO--自己备份
                               \DATENOW\CARTONNO                         --客户读取，并删除
             */
            ExecuteResult exeRes = new ExecuteResult();
            string bbx_baby_FileContent = "";
            string bbx_baby_BackUpTotalPath = "";
            //     string bbx_baby_TotalPath = "";
            try
            {
                string startPath = Application.StartupPath + @"\ALL_CARRIER\DHL_BBX_CARRIER";
                if (!Directory.Exists(startPath))
                {
                    Directory.CreateDirectory(startPath);
                }
                string secBackUpDir = startPath + @"\DHL_BBX_CARRIER_BACKUP\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + shipmentId;//备份dir
                if (!Directory.Exists(secBackUpDir))
                {
                    Directory.CreateDirectory(secBackUpDir);
                }
                //string secDir = startPath + @"\" + DateTime.Now.ToString("yyyyMMdd");//备份dir
                //if (!Directory.Exists(secDir))
                //{
                //    Directory.CreateDirectory(secDir);
                //}
                //string secDir = @"L:";
                exeRes = getBBXBabyFile(cartonNo, region);
                if (!exeRes.Status)
                {
                    return exeRes;
                }
                bbx_baby_FileContent = (string)exeRes.Anything;
                bbx_baby_BackUpTotalPath = secBackUpDir + @"\" + cartonNo + ".sps";
                //   bbx_baby_TotalPath = secDir + @"\" + cartonNo + ".sps";

                string ftpFlag = "N";
                string msg = "";
                string ftpFlagCheck = selectData.GetDBTypeBySP("FTP_DHL", out ftpFlag, out msg);
                if (ftpFlag == "Y")
                {
                    string xmlMsg = String.Empty;
                    var list = GetFilePathByCarrierFTP("DHL", out xmlMsg);
                    if (xmlMsg.IndexOf("NG") >= 0)
                    {
                        exeRes.Status = false;
                        exeRes.Message = xmlMsg;
                        return exeRes;
                    }
                    else
                        exeRes = WriteAndReadUtil.writeToByFilePathAndFileContentFTP(bbx_baby_BackUpTotalPath, list, bbx_baby_FileContent);
                }
                else
                {
                    //add carrier multi-Net Disk by Franky 2019年11月11日 
                    List<string> lisFilePath = this.GetFilePathByCarrier("DHL");
                    if (lisFilePath.Count < 1)
                    {
                        exeRes.Status = false;
                        exeRes.Message = "未维护货代标签地址网盘！";
                        return exeRes;
                    }
                    else
                    {
                        for (int i = 0; i < lisFilePath.Count; i++)
                        {
                            lisFilePath[i] = lisFilePath[i] + @"\" + cartonNo + ".sps";
                        }
                    }
                    //exeRes = WriteAndReadUtil.writeToByFilePathAndFileContent(bbx_baby_BackUpTotalPath, bbx_baby_TotalPath, bbx_baby_FileContent);
                    exeRes = WriteAndReadUtil.writeToByFilePathAndFileContent(bbx_baby_BackUpTotalPath, lisFilePath, bbx_baby_FileContent);
                }
                if (!exeRes.Status)
                {
                    return exeRes;
                }
                exeRes.Message = "OK_DHL_BBX货代已经打印,注意生成Mother_label！";
            }
            catch (Exception e)
            {
                exeRes.Status = false;
                exeRes.Message = e.Message;
            }
            return exeRes;
        }

        public ExecuteResult createBBX_Mother_File(string shipmentId, string region)
        {
            /*
             * 文件路径：
             MESCLIENT\ALL_CARRIER\BBX_Mother\BACKUP_BBX_Mother\DATENOW\SHIPMENTID--自己备份
                                 \DATENOW\SHIPMENTID                         --客户读取，并删除
             */
            ExecuteResult exeRes = new ExecuteResult();
            string bbx_mother_FileContent = "";
            string bbx_mother_BackUpTotalPath = "";
            //      string bbx_mother_TotalPath = "";
            try
            {
                string startPath = Application.StartupPath + @"\ALL_CARRIER\DHL_BBX_Mother\" + region + @"\";
                if (!Directory.Exists(startPath))
                {
                    Directory.CreateDirectory(startPath);
                }
                string secBackUpDir = startPath + @"\ALL_CARRIER\DHL_BBX_Mother_BACKUP\" + region + @"\" + DateTime.Now.ToString("yyyyMMdd");//备份dir
                if (!Directory.Exists(secBackUpDir))
                {
                    Directory.CreateDirectory(secBackUpDir);
                }
                //string secDir = startPath + @"\" + DateTime.Now.ToString("yyyyMMdd");//备份dir
                //if (!Directory.Exists(secDir))
                //{
                //    Directory.CreateDirectory(secDir);
                //}
                //  string secDir = @"L:";
                exeRes = getSPSImportBBXMotherFile(shipmentId, region);
                if (!exeRes.Status)
                {
                    return exeRes;
                }
                bbx_mother_FileContent = (string)exeRes.Anything;
                bbx_mother_BackUpTotalPath = secBackUpDir + @"\" + shipmentId + ".sps";
                //    bbx_mother_TotalPath = secDir + @"\" + shipmentId + ".sps";

                string ftpFlag = "N";
                string msg = "";
                string ftpFlagCheck = selectData.GetDBTypeBySP("FTP_DHL", out ftpFlag, out msg);
                if (ftpFlag == "Y")
                {
                    string xmlMsg = String.Empty;
                    var list = GetFilePathByCarrierFTP("DHL", out xmlMsg);
                    if (xmlMsg.IndexOf("NG") >= 0)
                    {
                        exeRes.Status = false;
                        exeRes.Message = xmlMsg;
                        return exeRes;
                    }
                    else
                        exeRes = WriteAndReadUtil.writeToByFilePathAndFileContentFTP(bbx_mother_BackUpTotalPath, list, bbx_mother_FileContent);
                }
                else
                {
                    //add carrier multi-Net Disk by Franky 2019年11月11日 
                    List<string> lisFilePath = this.GetFilePathByCarrier("DHL");
                    if (lisFilePath.Count < 1)
                    {
                        exeRes.Status = false;
                        exeRes.Message = "未维护货代标签地址网盘！";
                        return exeRes;
                    }
                    else
                    {
                        for (int i = 0; i < lisFilePath.Count; i++)
                        {
                            lisFilePath[i] = lisFilePath[i] + @"\" + shipmentId + ".sps";
                        }
                    }
                    //exeRes = WriteAndReadUtil.writeToByFilePathAndFileContent(bbx_mother_BackUpTotalPath, bbx_mother_TotalPath, bbx_mother_FileContent);
                    exeRes = WriteAndReadUtil.writeToByFilePathAndFileContent(bbx_mother_BackUpTotalPath, lisFilePath, bbx_mother_FileContent);
                }
                if (!exeRes.Status)
                {
                    return exeRes;
                }
            }
            catch (Exception e)
            {
                exeRes.Status = false;
                exeRes.Message = e.Message;
            }
            return exeRes;
        }



        public ExecuteResult create_SF_file(string cartonNo, string shipmentId)//顺丰
        {
            /*
             * 文件路径：
             MESCLIENT\SF\BACKUP_SF\DATENOW\SHIPMENTID\CARTONNO--自己备份
                               \DATENOW\CARTONNO                         --客户读取，并删除
             */
            ExecuteResult exeRes = new ExecuteResult();
            string SF_FileContent = "";
            string SF_BackUpTotalPath = "";
            //    string SF_TotalPath = "";
            try
            {
                string startPath = Application.StartupPath + @"\ALL_CARRIER\SF_CARRIER";
                if (!Directory.Exists(startPath))
                {
                    Directory.CreateDirectory(startPath);
                }
                string secBackUpDir = startPath + @"\SF_CARRIER_BACKUP\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + shipmentId;//备份dir
                if (!Directory.Exists(secBackUpDir))
                {
                    Directory.CreateDirectory(secBackUpDir);
                }
                //string secDir = startPath + @"\" + DateTime.Now.ToString("yyyyMMdd");//备份dir
                //if (!Directory.Exists(secDir))
                //{
                //    Directory.CreateDirectory(secDir);
                //}
                // string secDir = @"S:";
                exeRes = getSF_File_InfoByCartonNo(cartonNo);
                if (!exeRes.Status)
                {
                    return exeRes;
                }
                SF_FileContent = (string)exeRes.Anything;
                SF_BackUpTotalPath = secBackUpDir + @"\" + cartonNo + ".txt";
                //  SF_TotalPath = secDir + @"\" + cartonNo + ".txt";

                string ftpFlag = "N";
                string msg = "";
                string ftpFlagCheck = selectData.GetDBTypeBySP("FTP_SF", out ftpFlag, out msg);
                if (ftpFlag == "Y")
                {
                    string xmlMsg = String.Empty;
                    var list = GetFilePathByCarrierFTP("SF", out xmlMsg);
                    if (xmlMsg.IndexOf("NG") >= 0)
                    {
                        exeRes.Status = false;
                        exeRes.Message = xmlMsg;
                        return exeRes;
                    }
                    else
                        exeRes = WriteAndReadUtil.writeToByFilePathAndFileContentFTP(SF_BackUpTotalPath, list, SF_FileContent);
                }
                else
                {
                    //add carrier multi-Net Disk by Franky 2019年11月11日 
                    List<string> lisFilePath = this.GetFilePathByCarrier("SF");
                    if (lisFilePath.Count < 1)
                    {
                        exeRes.Status = false;
                        exeRes.Message = "未维护货代标签地址网盘！";
                        return exeRes;
                    }
                    else
                    {
                        for (int i = 0; i < lisFilePath.Count; i++)
                        {
                            lisFilePath[i] = lisFilePath[i] + @"\" + cartonNo + ".txt";
                        }
                    }
                    //   exeRes = WriteAndReadUtil.writeToByFilePathAndFileContent(SF_BackUpTotalPath, SF_TotalPath, SF_FileContent);
                    exeRes = WriteAndReadUtil.writeToByFilePathAndFileContent(SF_BackUpTotalPath, lisFilePath, SF_FileContent);
                }
                if (!exeRes.Status)
                {
                    return exeRes;
                }
                exeRes.Message = "OK_SF_(顺丰)货代已经打印！";
            }
            catch (Exception e)
            {
                exeRes.Status = false;
                exeRes.Message = e.Message;
            }
            return exeRes;
        }

        public ExecuteResult getFedInfoByCartonNo(string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            StringBuilder fedFileStr = new StringBuilder();
            dt = selectData.getFedInfoByCartonNo(cartonNo);
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                fedFileStr.AppendLine("0,\"" + dt.Rows[0]["TransactionType"].ToString() + "\"");
                fedFileStr.AppendLine("1,\"" + dt.Rows[0]["TransactionID"].ToString() + "\"");
                fedFileStr.AppendLine("4,\"" + dt.Rows[0]["SenderCompany"].ToString() + "\"");
                fedFileStr.AppendLine("5,\"" + dt.Rows[0]["SenderAddress1"].ToString() + "\"");
                fedFileStr.AppendLine("6,\"" + dt.Rows[0]["SenderAddress2"].ToString() + "\"");
                fedFileStr.AppendLine("7,\"" + dt.Rows[0]["SenderCity"].ToString() + "\"");
                fedFileStr.AppendLine("8,\"" + dt.Rows[0]["SenderState"].ToString() + "\"");
                fedFileStr.AppendLine("9,\"" + dt.Rows[0]["SenderPostalCode"].ToString() + "\"");
                fedFileStr.AppendLine("10,\"" + dt.Rows[0]["SenderAccountNumber"].ToString() + "\"");
                fedFileStr.AppendLine("11,\"" + dt.Rows[0]["RecipientCompany"].ToString() + "\"");
                fedFileStr.AppendLine("12,\"" + dt.Rows[0]["RecipientContactName"].ToString() + "\"");
                fedFileStr.AppendLine("13,\"" + dt.Rows[0]["RecipientAddress1"].ToString() + "\"");
                fedFileStr.AppendLine("14,\"" + dt.Rows[0]["RecipientAddress2"].ToString() + "\"");
                fedFileStr.AppendLine("15,\"" + dt.Rows[0]["RecipientCity"].ToString() + "\"");
                fedFileStr.AppendLine("16,\"" + dt.Rows[0]["RecipientState"].ToString() + "\"");
                fedFileStr.AppendLine("17,\"" + dt.Rows[0]["RecipientPostalCode"].ToString() + "\"");
                fedFileStr.AppendLine("18,\"" + dt.Rows[0]["RecipientPhone"].ToString() + "\"");
                fedFileStr.AppendLine("19,\"" + dt.Rows[0]["RecipientBusinessCode"].ToString() + "\"");
                fedFileStr.AppendLine("20,\"" + dt.Rows[0]["PayAccountNumber"].ToString() + "\"");
                fedFileStr.AppendLine("22,\"" + dt.Rows[0]["PS2ServiceType"].ToString() + "\"");
                fedFileStr.AppendLine("23,\"" + dt.Rows[0]["PaymentCode"].ToString() + "\"");
                fedFileStr.AppendLine("24,\"" + dt.Rows[0]["ShipDate"].ToString() + "\"");
                fedFileStr.AppendLine("25,\"" + dt.Rows[0]["ReferenceNotes"].ToString() + "\"");
                fedFileStr.AppendLine("29,\"" + dt.Rows[0]["MasterTrackingNumber"].ToString() + "\"");
                fedFileStr.AppendLine("50,\"" + dt.Rows[0]["RecipientCountryCode"].ToString() + "\"");
                fedFileStr.AppendLine("57,\"" + dt.Rows[0]["PackageHeight"].ToString() + "\"");
                fedFileStr.AppendLine("58,\"" + dt.Rows[0]["PackageWidth"].ToString() + "\"");
                fedFileStr.AppendLine("59,\"" + dt.Rows[0]["PackageLength"].ToString() + "\"");
                fedFileStr.AppendLine("68,\"" + dt.Rows[0]["CurrencyType"].ToString() + "\"");
                fedFileStr.AppendLine("69,\"" + dt.Rows[0]["CarriageValue"].ToString() + "\"");
                fedFileStr.AppendLine("70,\"" + dt.Rows[0]["DutyPaymentType"].ToString() + "\"");
                fedFileStr.AppendLine("71,\"" + dt.Rows[0]["DutyPayerAccountNumber"].ToString() + "\"");
                fedFileStr.AppendLine("72,\"" + dt.Rows[0]["TermsOfSale"].ToString() + "\"");
                fedFileStr.AppendLine("75,\"" + dt.Rows[0]["WeightType"].ToString() + "\"");
                fedFileStr.AppendLine("78,\"" + dt.Rows[0]["CommodityCustomsValue"].ToString() + "\"");
                fedFileStr.AppendLine("79,\"" + dt.Rows[0]["CommodityDescriptionLine"].ToString() + "\"");
                fedFileStr.AppendLine("80,\"" + dt.Rows[0]["CountryofManufacture"].ToString() + "\"");
                fedFileStr.AppendLine("88,\"" + dt.Rows[0]["CommodityDescriptionLine2"].ToString() + "\"");
                fedFileStr.AppendLine("97,\"" + dt.Rows[0]["CommodityDescriptionLine3"].ToString() + "\"");
                fedFileStr.AppendLine("113,\"" + dt.Rows[0]["CommercialInvoiceFlag"].ToString() + "\"");
                fedFileStr.AppendLine("114,\"" + dt.Rows[0]["TrackingNumberofCrn"].ToString() + "\"");
                fedFileStr.AppendLine("116,\"" + dt.Rows[0]["totalNumberofPackages"].ToString() + "\"");
                fedFileStr.AppendLine("117,\"" + dt.Rows[0]["SenderCountryCode"].ToString() + "\"");
                fedFileStr.AppendLine("183,\"" + dt.Rows[0]["SenderPhoneNumber"].ToString() + "\"");
                fedFileStr.AppendLine("627,\"" + dt.Rows[0]["ServiceType"].ToString() + "\"");
                fedFileStr.AppendLine("700,\"" + dt.Rows[0]["ReturnContactName"].ToString() + "\"");
                fedFileStr.AppendLine("701,\"" + dt.Rows[0]["ReturnCompany"].ToString() + "\"");
                fedFileStr.AppendLine("702,\"" + dt.Rows[0]["ReturnAddress1"].ToString() + "\"");
                fedFileStr.AppendLine("703,\"" + dt.Rows[0]["ReturnAddress2"].ToString() + "\"");
                fedFileStr.AppendLine("709,\"" + dt.Rows[0]["CustomerPurchaseOrderNo"].ToString() + "\"");
                fedFileStr.AppendLine("711,\"" + dt.Rows[0]["CartonNo"].ToString() + "\"");
                fedFileStr.AppendLine("712,\"" + dt.Rows[0]["CartonCount"].ToString() + "\"");
                fedFileStr.AppendLine("713,\"" + dt.Rows[0]["CustomBarcode"].ToString() + "\"");
                fedFileStr.AppendLine("714,\"" + dt.Rows[0]["SalesOrderNo"].ToString() + "\"");
                fedFileStr.AppendLine("715,\"" + dt.Rows[0]["WebOrderNo"].ToString() + "\"");
                fedFileStr.AppendLine("716,\"" + dt.Rows[0]["deliveryno"].ToString() + "\"");
                fedFileStr.AppendLine("717,\"" + dt.Rows[0]["ShipDate2"].ToString() + "\"");
                fedFileStr.AppendLine("718,\"" + dt.Rows[0]["PartNo"].ToString() + "\"");
                fedFileStr.AppendLine("719,\"" + dt.Rows[0]["DescriptionLine1"].ToString() + "\"");
                fedFileStr.AppendLine("720,\"" + dt.Rows[0]["DescriptionLine2"].ToString() + "\"");
                fedFileStr.AppendLine("721,\"" + dt.Rows[0]["ShippingInstruction"].ToString() + "\"");
                fedFileStr.AppendLine("722,\"" + dt.Rows[0]["LineItemNumber"].ToString() + "\"");
                fedFileStr.AppendLine("723,\"" + dt.Rows[0]["QuantityShipped"].ToString() + "\"");
                fedFileStr.AppendLine("724,\"" + dt.Rows[0]["QuantityOrdered"].ToString() + "\"");
                fedFileStr.AppendLine("726,\"" + dt.Rows[0]["ProductModelLine1"].ToString() + "\"");
                fedFileStr.AppendLine("728,\"" + dt.Rows[0]["District"].ToString() + "\"");
                fedFileStr.AppendLine("1116,\"" + dt.Rows[0]["DimUnit"].ToString() + "\"");
                fedFileStr.AppendLine("1226,\"" + dt.Rows[0]["SaturdayDeliveryFlag"].ToString() + "\"");
                fedFileStr.AppendLine("1274,\"" + dt.Rows[0]["ServiceType2"].ToString() + "\"");
                fedFileStr.AppendLine("1670,\"" + dt.Rows[0]["PackageWeight"].ToString() + "\"");
                exeRes.Anything = fedFileStr.ToString().Trim();
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "未能查询到FedEx信息，请联系IT-PPS!";
            }
            return exeRes;
        }

        public string handleStrForTNT(string colResult, int len_)
        {
            string return_str = "";
            if (colResult.Length > len_)
            {
                return_str = colResult.Substring(0, len_);
            }
            else
            {
                return_str = colResult.PadRight(len_, ' ');
            }
            return return_str;
        }

        public ExecuteResult getTransInFileInfo(string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            string transInFile = "";
            dt = selectData.getTransInFileInfo(cartonNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                transInFile += handleStrForTNT(dt.Rows[0]["ConsignmentNumber"].ToString(), 15);
                transInFile += handleStrForTNT(dt.Rows[0]["SenderAccountNumber"].ToString(), 9);
                transInFile += handleStrForTNT(dt.Rows[0]["SenderName"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["SenderAddress1"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["SenderAddress2"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["SenderAddress3"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["SenderPostcode"].ToString(), 9);
                transInFile += handleStrForTNT(dt.Rows[0]["SenderCity"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["SenderProvince"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["SenderCountry"].ToString(), 3);
                transInFile += handleStrForTNT(dt.Rows[0]["SenderVATNumber"].ToString(), 20);
                transInFile += handleStrForTNT(dt.Rows[0]["SenderTelephoneArea"].ToString(), 7);
                transInFile += handleStrForTNT(dt.Rows[0]["SenderTelephoneNumber"].ToString(), 9);
                transInFile += handleStrForTNT(dt.Rows[0]["SenderContactName"].ToString(), 22);
                transInFile += handleStrForTNT(dt.Rows[0]["PickupName"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["PickupAddress1"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["PickupAddress2"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["PickupAddress3"].ToString(), 35);

                transInFile += handleStrForTNT(dt.Rows[0]["PickupPostcode"].ToString(), 9);
                transInFile += handleStrForTNT(dt.Rows[0]["PickupCity"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["PickupProvince"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["PickupCountry"].ToString(), 3);
                transInFile += handleStrForTNT(dt.Rows[0]["PickupTelephone1"].ToString(), 7);
                transInFile += handleStrForTNT(dt.Rows[0]["PickupTelephone2"].ToString(), 9);
                transInFile += handleStrForTNT(dt.Rows[0]["PickupContactName"].ToString(), 22);
                transInFile += handleStrForTNT(dt.Rows[0]["ReceiverAccountNumber"].ToString(), 9);
                transInFile += handleStrForTNT(dt.Rows[0]["ReceiverName"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["ReceiverAddress1"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["ReceiverAddress2"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["ReceiverAddress3"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["ReceiverPostcode"].ToString(), 9);
                transInFile += handleStrForTNT(dt.Rows[0]["ReceiverCity"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["ReceiverProvince"].ToString(), 35);

                transInFile += handleStrForTNT(dt.Rows[0]["ReceiverCountry"].ToString(), 3);
                transInFile += handleStrForTNT(dt.Rows[0]["ReceiverTelephone1"].ToString(), 7);
                transInFile += handleStrForTNT(dt.Rows[0]["ReceiverTelephone2"].ToString(), 9);
                transInFile += handleStrForTNT(dt.Rows[0]["ReceiverContactName"].ToString(), 22);
                transInFile += handleStrForTNT(dt.Rows[0]["DeliveryName"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["DeliveryAddress1"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["DeliveryAddress2"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["DeliveryAddress3"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["DeliveryPostcode"].ToString(), 9);
                transInFile += handleStrForTNT(dt.Rows[0]["DeliveryCity"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["DeliveryProvince"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["DeliveryCountry"].ToString(), 3);
                transInFile += handleStrForTNT(dt.Rows[0]["DeliveryTelephone1"].ToString(), 7);

                transInFile += handleStrForTNT(dt.Rows[0]["DeliveryTelephone2"].ToString(), 9);
                transInFile += handleStrForTNT(dt.Rows[0]["DeliveryContactName"].ToString(), 22);
                transInFile += dt.Rows[0]["ServicecodeAppleRoute"].ToString().PadLeft(3, ' ');
                transInFile += handleStrForTNT(dt.Rows[0]["SenderReceiverpays"].ToString(), 3);
                transInFile += handleStrForTNT(dt.Rows[0]["Serviceoption1"].ToString(), 3);
                transInFile += handleStrForTNT(dt.Rows[0]["Serviceoption2"].ToString(), 3);
                transInFile += handleStrForTNT(dt.Rows[0]["Serviceoption3"].ToString(), 3);
                transInFile += handleStrForTNT(dt.Rows[0]["Serviceoption4"].ToString(), 3);
                transInFile += dt.Rows[0]["Numberofparcels"].ToString();
                transInFile += dt.Rows[0]["Sequencenumber"].ToString();
                transInFile += handleStrForTNT(dt.Rows[0]["Typeofpacking"].ToString(), 20);
                transInFile += handleStrForTNT(dt.Rows[0]["Marksofpacking"].ToString(), 10);
                transInFile += handleStrForTNT(dt.Rows[0]["Height_"].ToString(), 3);
                transInFile += handleStrForTNT(dt.Rows[0]["Width_"].ToString(), 3);
                transInFile += handleStrForTNT(dt.Rows[0]["Length_"].ToString(), 3);
                transInFile += handleStrForTNT(dt.Rows[0]["Volumeintegerm3"].ToString(), 3);
                transInFile += handleStrForTNT(dt.Rows[0]["Volumedecimalsm3"].ToString(), 3);
                transInFile += handleStrForTNT(dt.Rows[0]["Grossweightkg"].ToString(), 4);
                transInFile += handleStrForTNT(dt.Rows[0]["Grossweightgrams"].ToString(), 3);
                transInFile += handleStrForTNT(dt.Rows[0]["IncreasedCMRliability"].ToString(), 3);
                transInFile += handleStrForTNT(dt.Rows[0]["Insurancevalueintege"].ToString(), 11);
                transInFile += handleStrForTNT(dt.Rows[0]["Insurancevaluedecima"].ToString(), 2);
                transInFile += handleStrForTNT(dt.Rows[0]["Insurancecurrency"].ToString(), 3);
                transInFile += handleStrForTNT(dt.Rows[0]["InvoicevalueInteger"].ToString(), 11);
                transInFile += handleStrForTNT(dt.Rows[0]["Invoicevaluedecimal"].ToString(), 2);
                transInFile += handleStrForTNT(dt.Rows[0]["Invoicecurrency"].ToString(), 3);
                transInFile += handleStrForTNT(dt.Rows[0]["Samplevalueint"].ToString(), 11);
                transInFile += handleStrForTNT(dt.Rows[0]["Samplecurrencydec"].ToString(), 2);
                transInFile += handleStrForTNT(dt.Rows[0]["Samplecurrency"].ToString(), 3);
                transInFile += handleStrForTNT(dt.Rows[0]["Receiverreference"].ToString(), 24);
                transInFile += handleStrForTNT(dt.Rows[0]["Senderreference"].ToString(), 24);
                transInFile += handleStrForTNT(dt.Rows[0]["Sendersignatory"].ToString(), 22);

                transInFile += handleStrForTNT(dt.Rows[0]["SpecialInstructions1"].ToString(), 30);
                transInFile += handleStrForTNT(dt.Rows[0]["SpecialInstructions2"].ToString(), 30);
                transInFile += handleStrForTNT(dt.Rows[0]["Collectiondatecc"].ToString(), 2);
                transInFile += handleStrForTNT(dt.Rows[0]["Collectiondateyy"].ToString(), 2);
                transInFile += handleStrForTNT(dt.Rows[0]["Collectiondatemm"].ToString(), 2);
                transInFile += handleStrForTNT(dt.Rows[0]["Collectiondatedd"].ToString(), 2);

                transInFile += handleStrForTNT(dt.Rows[0]["Collectiontime"].ToString(), 4);

                transInFile += handleStrForTNT(dt.Rows[0]["Articledescription"].ToString(), 30);
                transInFile += handleStrForTNT(dt.Rows[0]["BTNnumber"].ToString(), 25);
                transInFile += handleStrForTNT(dt.Rows[0]["Numberofarticles"].ToString(), 4);
                transInFile += handleStrForTNT(dt.Rows[0]["Hazardousclass"].ToString(), 3);

                transInFile += handleStrForTNT(dt.Rows[0]["Tradestatus"].ToString(), 4);
                transInFile += handleStrForTNT(dt.Rows[0]["Printerportnumber"].ToString(), 2);
                transInFile += handleStrForTNT(dt.Rows[0]["Divisioncode"].ToString(), 1);

                transInFile += handleStrForTNT(dt.Rows[0]["Sendingdepot"].ToString(), 5);
                transInFile += handleStrForTNT(dt.Rows[0]["Larosedepot"].ToString(), 5);
                transInFile += handleStrForTNT(dt.Rows[0]["Endpiecenumber"].ToString(), 3);

                transInFile += handleStrForTNT(dt.Rows[0]["Retaincode"].ToString(), 1);
                transInFile += handleStrForTNT(dt.Rows[0]["Actioncode"].ToString(), 1);
                transInFile += handleStrForTNT(dt.Rows[0]["Returncode"].ToString(), 5);

                transInFile += handleStrForTNT(dt.Rows[0]["Printeraddress"].ToString(), 50);
                transInFile += handleStrForTNT(dt.Rows[0]["Printerdriver"].ToString(), 6);
                transInFile += handleStrForTNT(dt.Rows[0]["Weighttypetext"].ToString(), 3);
                transInFile += handleStrForTNT(dt.Rows[0]["Dimensiontypetext"].ToString(), 3);
                transInFile += handleStrForTNT(dt.Rows[0]["Volumetypetext"].ToString(), 3);

                transInFile += handleStrForTNT(dt.Rows[0]["AppleLabelRemarks"].ToString(), 35);
                transInFile += handleStrForTNT(dt.Rows[0]["AppleUUIReference"].ToString(), 23);
                transInFile += handleStrForTNT(dt.Rows[0]["AppleSortText"].ToString(), 6);
                transInFile += handleStrForTNT(dt.Rows[0]["RoadAirIndicator"].ToString(), 1);
                transInFile += handleStrForTNT(dt.Rows[0]["TradeStatusValue"].ToString(), 2);

                transInFile += handleStrForTNT(dt.Rows[0]["AppleDCReference"].ToString(), 20);
                transInFile += handleStrForTNT(dt.Rows[0]["WebOrderNumber"].ToString(), 16);
                transInFile += handleStrForTNT(dt.Rows[0]["CoDIndicator"].ToString(), 1);
                transInFile += handleStrForTNT(dt.Rows[0]["Filler"].ToString(), 4);
                transInFile += handleStrForTNT(dt.Rows[0]["AppleShippingCondition"].ToString(), 5);
                transInFile += handleStrForTNT(dt.Rows[0]["AppleStore"].ToString(), 8);
                transInFile += handleStrForTNT(dt.Rows[0]["ReturnsConsignment"].ToString(), 15);
                transInFile += handleStrForTNT(dt.Rows[0]["ReturnsDispatchRefApple"].ToString(), 24);

                transInFile += handleStrForTNT(dt.Rows[0]["ReturnsRMARefApple"].ToString(), 24);
                transInFile += handleStrForTNT(dt.Rows[0]["ReturnsCustomerRefTNT"].ToString(), 24);
                transInFile += handleStrForTNT(dt.Rows[0]["ReturnsPrinterAddress"].ToString(), 50);
                transInFile += handleStrForTNT(dt.Rows[0]["ReturnsAppleRoute"].ToString(), 3);
                transInFile += handleStrForTNT(dt.Rows[0]["ReturnsShipCond"].ToString(), 5);
                exeRes.Anything = transInFile;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此箱号： " + cartonNo + "未查询到信息！";
            }
            return exeRes;
        }

        public ExecuteResult getYMT_File_Info_ByCartonNo(string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            string allStr = "";
            dt = selectData.getYMT_File_Info_ByCartonNo(cartonNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count; ++i)
                {
                    allStr = allStr + "\"" + dt.Rows[0][i].ToString() + "\",";
                }
                allStr = allStr.Substring(0, allStr.LastIndexOf(","));
                exeRes.Anything = allStr;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此箱号:" + cartonNo + " 无法获取对应的YMTinfo资料，请联系IT-PPS！";
            }
            return exeRes;
        }
        public ExecuteResult getUpsInfoByCartonNo(string cartonNo, string region)
        //SAWB_SHIPMENTID 目前只针对region=AMR 
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            string upsFileStr = "";
            string ST_PROVINCE = "";
            dt = selectData.getUpsInfoByCartonNo(cartonNo, region);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count; ++i)
                {
                    if ((i + 1) == 22)//ST_PROVINCE --RegionDesc  如果有 = 号码, 那么取 = 号之前的
                    {
                        ST_PROVINCE = dt.Rows[0][i].ToString();
                        if (ST_PROVINCE.Contains("="))
                        {
                            ST_PROVINCE = ST_PROVINCE.Split('=')[0];
                            upsFileStr += (i + 1).ToString() + "," + ST_PROVINCE + Environment.NewLine;
                        }
                        else
                        {
                            upsFileStr += (i + 1).ToString() + "," + dt.Rows[0][i].ToString() + Environment.NewLine;
                        }
                    }
                    else
                    {
                        upsFileStr += (i + 1).ToString() + "," + dt.Rows[0][i].ToString() + Environment.NewLine;
                    }

                }
                exeRes.Anything = upsFileStr.Trim();
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "未能查询到Ups信息，请联系IT-PPS!";
            }
            return exeRes;
        }



        public ExecuteResult checkShipmentIdStatusByShipmentId(string shipmentId)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = selectData.checkShipmentIdStatusByShipmentId(shipmentId);
            if (dt != null && dt.Rows.Count > 0)
            {
                string status = dt.Rows[0]["status"].ToString();
                switch (status)
                {
                    case "FP":
                    case "LF":
                    case "UF":
                        exeRes.Status = false;
                        exeRes.Message = "此集货单号:" + shipmentId + " 已做完，请检查！";
                        break;
                    case "QH":
                        exeRes.Status = false;
                        exeRes.Message = "此集货单号:" + shipmentId + " 已被QAHold，请检查！";
                        break;
                    default:
                        exeRes.Status = true;
                        break;
                }
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此集货单号：" + shipmentId + " 对应shipmentInfo没有资料,请检查输入集货单号是否正确！";
            }
            return exeRes;
        }
        public ExecuteResult getPalletPickInfoByPickPalletNo(string pickPalletNo, string shipMentId)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = selectData.getPalletPickInfoByPickPalletNo(pickPalletNo, shipMentId);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此栈板号: " + pickPalletNo + " 查不到信息，请检查！";
            }
            return exeRes;
        }
        public ExecuteResult getUpsCodeByCartonNo(string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = selectData.getUpsCodeByCartonNo(cartonNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt.Rows[0]["tracking_no"].ToString();
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此箱号: " + cartonNo + " 查不到UPSCODE信息，请检查！";
            }
            return exeRes;
        }
        public ExecuteResult checkSnStatus(string inputData, string pickPallletNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inputData", inputData };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "pickPalletNo", pickPallletNo };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "TMES", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.t_pack_checkSnStatus", procParams);
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
        public ExecuteResult judgeInputDataType(string inputData)
        {
            ExecuteResult exeRes = new ExecuteResult();
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inputData", inputData };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "isType", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.t_judge_input_type", procParams);
            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isType"].ToString()))
            {
                exeRes.Anything = ds.Tables[0].Rows[0]["isType"].ToString();
            }
            else
            {
                exeRes.Status = false;
            }
            return exeRes;
        }

        public ExecuteResult getCartonStatusByInputData(string inputData)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = selectData.getCartonStatusByInputData(inputData);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "根据inputData信息获取箱子信息失败，请联系IT-PPS!";
            }
            return exeRes;
        }



        public ExecuteResult getAllCartonNo(string inputData)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            List<string> cartonNolist = new List<string>();
            dt = selectData.inputDataConvertCartonNo(inputData);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    cartonNolist.Add(dt.Rows[i]["cartonNo"].ToString());
                }
                exeRes.Anything = cartonNolist;
                exeRes.Message = "信息查询成功！";
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此：" + inputData + " 查询不到资料，请检查！";
            }
            return exeRes;
        }



        public ExecuteResult queryPalletOrderInfoByCartonNo(string cartonNo, string palletNo, bool isAfterData)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            exeRes = isPpartByCartonNo(cartonNo);
            if (exeRes.Status)
            {
                dt = selectData.getPpartDnInfoByCartonNo(cartonNo);
                if (dt != null && dt.Rows.Count > 0)
                {
                    exeRes.Anything = dt;
                    exeRes.Message = "信息查询成功！";
                }
                else
                {
                    exeRes.Status = false;
                    exeRes.Message = "此箱号：" + cartonNo + "查询不到资料，请检查！";
                }
            }
            else
            {
                dt = selectData.queryPalletOrderInfoByCartonNo(cartonNo, palletNo, isAfterData);
                if (dt != null && dt.Rows.Count > 0)
                {
                    exeRes.Status = true;
                    exeRes.Anything = dt;
                    exeRes.Message = "信息查询成功！";
                }
                else
                {
                    exeRes.Status = false;
                    exeRes.Message = "此箱号：" + cartonNo + "查询不到资料，请检查！";
                }
            }

            return exeRes;
        }
        public ExecuteResult isPpartByCartonNo(string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = selectData.isPpartByCartonNo(cartonNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
                exeRes.Message = "信息查询成功！";
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此箱号：" + cartonNo + "查询不到资料，请检查！";
            }
            return exeRes;
        }




        public ExecuteResult getT940UnicodeInfoByDeliveryNoAndLineItem(string deliveryNo, string lineItem)
        {
            DataTable dt = new DataTable();
            ExecuteResult exeRes = new ExecuteResult();
            dt = selectData.getT940UnicodeInfoByDeliveryNoAndLineItem(deliveryNo);
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
                    customerGroup = customerGroup_D,
                    deliveryNo = deliveryNo,
                    lineItem = lineItem,
                    ShipCntyCode = dt.Rows[0]["shipcntycode"].ToString().Trim()
                };
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "T940Unicode没有获得资料，请联系IT-PPS!";
            }
            return exeRes;
        }



        /// <summary>
        /// 将A中数据添加到B中
        /// </summary>
        /// <param name="getDataDt">A dataTable</param>
        /// <param name="dgvDt">B dataTable</param>
        /// <returns></returns>
        private DataTable addRowData(DataTable getDataDt, DataTable dgvDt)
        {
            DataTable dt = new DataTable();
            if (getDataDt != null && getDataDt.Rows.Count > 0)//将取到值添加到dgv中
            {
                DataRow drcalc;
                foreach (DataRow dr in getDataDt.Rows)
                {
                    drcalc = dgvDt.NewRow();
                    drcalc.ItemArray = dr.ItemArray;
                    dgvDt.Rows.Add(drcalc);
                }
            }
            dt = dgvDt;
            return dt;
        }
        /// <summary>
        /// 返回label前半部分值，已拼接好string形式返回
        /// </summary>
        /// <returns></returns>
        public ExecuteResult getFrontPartWorkingString(bool isMix, ShipmentInfo shipmentInfo, string cartonNo)
        {
            string lastLabelContent = "";
            DataTable dt = new DataTable();
            ExecuteResult exeRes = new ExecuteResult();
            //区分DS / FD
            if (shipmentInfo.ShipmentType.Equals("FD"))
            {
                dt = selectData.getPrintInfoByShipmentIdAndCartonNo_FD(shipmentInfo.ShipmentId, cartonNo);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string carrierName = dt.Rows[0]["SCACCODE"].ToString();//
                    string coc = dt.Rows[0]["POE"].ToString();//POE
                    string region = dt.Rows[0]["region"].ToString();//REGION
                    string hawb = dt.Rows[0]["HAWB"].ToString();//
                    string po = dt.Rows[0]["DELIVERY_NO"].ToString();//DELIVERYNO
                    string origin = dt.Rows[0]["ORIGION"].ToString();//固定值
                    string INVOICENO = hawb;//
                    string cartons = "";// 格式：最小箱号-最大箱号/DELIVERYNO-对应的一共有多少箱
                    exeRes = getCartonsOfPrintLabelInfoByCartonNo(cartonNo, shipmentInfo.ShipmentId, isMix);
                    if (exeRes.Status)
                    {
                        cartons = (string)exeRes.Anything;
                    }
                    else
                    {
                        return exeRes;
                    }
                    string shipId = dt.Rows[0]["shipid"].ToString();//
                    string shipMentDate = DateTime.Parse(dt.Rows[0]["shipping_time"].ToString()).ToString(@"MM/dd/yyyy");// 格式为--MM/DD/YYYY
                    string tel = dt.Rows[0]["shiptotelephone"].ToString();//SHIPTOTELEPHONE
                    string sscc = "";//如果是NO Mix 那么是栈板的SSCCCode反正则是Carton 的SSCCC
                    exeRes = getSsccOfPrintLabelInfoByCartonNo(cartonNo, isMix);
                    if (exeRes.Status)
                    {
                        sscc = (string)exeRes.Anything;
                    }
                    else
                    {
                        return exeRes;
                    }
                    string returnToName1 = dt.Rows[0]["returntoname1"].ToString();
                    string returnToName2 = dt.Rows[0]["returntoname2"].ToString();
                    string returnToAddress1 = dt.Rows[0]["returnToAddress1"].ToString();
                    string returnToAddress2 = dt.Rows[0]["returnToAddress2"].ToString();
                    string returnToAddress3 = dt.Rows[0]["returnToAddress3"].ToString();
                    string returnToAddress4 = dt.Rows[0]["returnToAddress4"].ToString();
                    string returntocity = dt.Rows[0]["returntocity"].ToString();
                    string returntostate = dt.Rows[0]["returntostate"].ToString();
                    string returntozip = dt.Rows[0]["returntozip"].ToString();//
                    string returntocountry = dt.Rows[0]["returntocountry"].ToString();//

                    //string return_To1 = returnToName1 + returnToName2;
                    //string return_To2 = returnToAddress1 + returnToAddress2 + returnToAddress3 + returnToAddress4;
                    //string return_To3 = returntocity + " " + returntostate + " " + returntozip;
                    //string return_To4 = returntocountry;
                    string allReturnInfo = calculateShiptoORReturnTo(returnToName1, returnToName2, returnToAddress1, returnToAddress2, returnToAddress3, returnToAddress4, returntocity + " " + returntostate + " " + returntozip, returntocountry);

                    string shiptoname1 = dt.Rows[0]["shiptoname1"].ToString();
                    string shiptoname2 = dt.Rows[0]["shiptoname2"].ToString();
                    string shiptoaddress1 = dt.Rows[0]["shiptoaddress1"].ToString();
                    string shiptoaddress2 = dt.Rows[0]["shiptoaddress2"].ToString();
                    string shiptoaddress3 = dt.Rows[0]["shiptoaddress3"].ToString();
                    string shiptoaddress4 = dt.Rows[0]["shiptoaddress4"].ToString();
                    string shiptocity = dt.Rows[0]["shiptocity"].ToString();
                    string shiptostate = dt.Rows[0]["shiptostate"].ToString();
                    string shiptozip = dt.Rows[0]["shiptozip"].ToString();
                    string shiptocountry = dt.Rows[0]["shiptocountry"].ToString();
                    string ctry = coc;//来源不明，为空
                    string allShipinfo = calculateShiptoORReturnTo(shiptoname1, shiptoname2, shiptoaddress1, shiptoaddress2, shiptoaddress3, shiptoaddress4, shiptocity + " " + shiptostate + " " + shiptozip, shiptocountry);

                    /*
                     * for  gu  test
                        A.I.  
                        C/O IML
                        LAGO ZURICH 245 ED PRESA FALCON P7
                        MEXICO METROPOLITANA
                        DF
                        05349
                        Mexico                    
                     */
                    //allShipinfo = @"APPLE OPERATIONS MEXICO S.A. DE C.V|RODOLFO DE LA FUENTE GUTIERREZ|Ave.Las Lomas esq.Ave. Industrial N|ENTRE CONVENTO ACOLMAN Y AV BARRIOS|PONIENTE N.3515 LOCAL 42,43,44|COL ANTIGUA MINA LA TOTOLOAPAN|Huixquilucan, Estado de Mexico|DF|52763|Mexico|";
                    lastLabelContent = carrierName + "|" + coc + "|" + ctry + "|" + hawb + "|" + po + "|" + origin + "|" + INVOICENO + "|" + cartons + "|" + shipId + "|" + shipMentDate + "|" + tel + "|" + sscc + "|"
                                       + allReturnInfo + allShipinfo;
                    //if (shipmentInfo.Region.Equals("EMEIA") && shipmentInfo.CarrierName.Equals("KNBULK")) --add  weifeng 20190813
                    if (shipmentInfo.Region.Equals("EMEIA") && (shipmentInfo.CarrierCode.Equals("1060020795") || shipmentInfo.CarrierCode.Equals("1060029822")))
                    {
                        dt = selectData.getKnboxNoOfPrintLabelInfoByCartonNo(cartonNo);
                        string knboxNo = dt.Rows[0]["KNBOXNO"].ToString();
                        lastLabelContent = knboxNo + "|" + lastLabelContent;
                    }
                    exeRes.Anything = lastLabelContent;
                }
                else
                {
                    exeRes.Status = false;
                    exeRes.Message = "无法获取打印信息，请联系IT-PPS!";
                }
            }
            else
            {
                if (shipmentInfo.TYPE.Equals("PARCEL"))//DS此类型不打印shiplabel，产生transInfile
                {
                    exeRes.Status = false;
                    exeRes.Message = "出货PARCEL类型，无需打印shippingLabel！";
                    return exeRes;
                }
                switch (shipmentInfo.Region)
                {
                    case "PAC":
                        exeRes = getFrontPartWorkingStringOfPAC(shipmentInfo, cartonNo, isMix);
                        break;
                    case "AMR":
                        exeRes = getFrontPartWorkingStringOfAMR(shipmentInfo, cartonNo, isMix);
                        break;
                    case "EMEIA":
                        exeRes = getFrontPartWorkingStringOfEMEIA(shipmentInfo, cartonNo, isMix);
                        break;
                    default:
                        break;
                }
            }
            return exeRes;
        }

        #region 因为DS部分与FD不共用shiplabel取值不同
        //美国段AMR
        private ExecuteResult getFrontPartWorkingStringOfAMR(ShipmentInfo shipmentInfo, string cartonNo, bool isMix)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            string lastLabelContent = "";
            dt = selectData.getPrintInfoByShipmentIdAndCartonNo_DS_AMR(cartonNo);
            if (dt.Rows[0]["Tmes"].ToString().Equals("OK"))
            {
                string tel = dt.Rows[0]["tel"].ToString();
                string returnto1 = dt.Rows[0]["returnto1"].ToString().Trim();
                string returnto2 = dt.Rows[0]["returnto2"].ToString().Trim();
                string returnto3 = dt.Rows[0]["returnto3"].ToString().Trim();
                string returnto4 = dt.Rows[0]["returnto4"].ToString().Trim();
                string returnto5 = dt.Rows[0]["returnto5"].ToString().Trim();
                string returnto6 = dt.Rows[0]["returnto6"].ToString().Trim();
                string returnto7 = dt.Rows[0]["returnto7"].ToString().Trim();
                string returnto8 = dt.Rows[0]["returnto8"].ToString().Trim();
                string allReturnTo = calculateShiptoORReturnTo(returnto1, returnto2, returnto3, returnto4, returnto5, returnto6, returnto7, returnto8);
                string shipto1 = dt.Rows[0]["shipto_name"].ToString().Trim();
                string shipto2 = dt.Rows[0]["shipto_company"].ToString().Trim();
                string shipto3 = dt.Rows[0]["shipto_address"].ToString().Trim();
                string shipto4 = dt.Rows[0]["shipto_address2"].ToString().Trim();
                string shipto5 = dt.Rows[0]["shipto_address3"].ToString().Trim();
                string shipto6 = dt.Rows[0]["shipto_address4"].ToString().Trim();
                string shipto7 = dt.Rows[0]["shipto_address5"].ToString().Trim();
                string shipto8 = dt.Rows[0]["shipto_country"].ToString().Trim();
                string allShipto = calculateShiptoORReturnTo(shipto1, shipto2, shipto3, shipto4, shipto5, shipto6, shipto7, shipto8);
                string carrierName = dt.Rows[0]["CARRIERNAME"].ToString().Trim();
                string origin = dt.Rows[0]["O_RIGION"].ToString().Trim();
                string coc = dt.Rows[0]["P_OE"].ToString().Trim();
                string shipDate = DateTime.Parse(dt.Rows[0]["shippingTime"].ToString().Trim()).ToString(@"MM/dd/yyyy");
                string cartons = "";
                exeRes = getCartonsOfPrintLabelInfoByCartonNo(cartonNo, shipmentInfo.ShipmentId, isMix);
                if (exeRes.Status)
                {
                    cartons = (string)exeRes.Anything;
                }
                else
                {
                    return exeRes;
                }
                string HAWB = dt.Rows[0]["H_awb"].ToString().Trim();
                string saleOrder = dt.Rows[0]["SaleOrder"].ToString().Trim();
                string deliveryNo = dt.Rows[0]["dn"].ToString().Trim();
                string web = dt.Rows[0]["web_Order_NO"].ToString().Trim();
                string po = dt.Rows[0]["custpo_no"].ToString().Replace(" ", "").Replace(@"\r\n", "").Trim();
                string sscc = "";
                /*
                DS_AMR--  CARRIERNAME|COC|SALEORDER|HAWB|PO|ORIGIN|DELIVERY_NO|CARTONS|WEB|SHIP_DATE|tel|SSCC|              
                 */
                exeRes = getSsccOfPrintLabelInfoByCartonNo(cartonNo, isMix);
                if (exeRes.Status)
                {
                    sscc = (string)exeRes.Anything;
                }
                else
                {
                    return exeRes;
                }
                lastLabelContent = carrierName + "|" + coc + "|" + saleOrder + "|" + HAWB + "|" + po + "|" + origin + "|" + deliveryNo + "|" +
                                   cartons + "|" + web + "|" + shipDate + "|" + tel + "|" + sscc + "|" + allReturnTo + allShipto;
                exeRes.Anything = lastLabelContent;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "无法获取打印信息，请联系IT-PPS!";
            }
            return exeRes;
        }


        #region  PAC(亚太段字符串)
        private ExecuteResult getFrontPartWorkingStringOfPAC(ShipmentInfo shipmentInfo, string cartonNo, bool isMix)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            string lastLableContent = "";
            string allReturnTo = "";
            //首先判断是否打印亚太shippinglabel
            if (!isPrintPACShippingLabelByCartonNoAndShipInfoType(cartonNo, shipmentInfo.TYPE).Status)
            {
                exeRes.Message = "此箱不需要打印ShippingLabel！";
                exeRes.Status = false;
                return exeRes;
            }
            exeRes = getPACReturnInfoByCartonNo(cartonNo, shipmentInfo.TYPE);
            if (exeRes.Status)
            {
                allReturnTo = calculateShiptoORReturnTo((string[])exeRes.Anything);
            }
            else
            {
                return exeRes;
            }
            dt = selectData.getPrintInfoOfPACByCartonNO(cartonNo);
            if (dt.Rows.Count > 0 && dt != null)
            {
                string carrierName = dt.Rows[0]["carrier"].ToString().Trim();
                //coc   = poe,ctry
                string coc = dt.Rows[0]["poe"].ToString().Trim();
                string hawb = dt.Rows[0]["hawb"].ToString().Trim();
                string dn = dt.Rows[0]["delivery_no"].ToString().Trim();
                //针对po修改了逻辑 BY dunyang
                //string po = calculatePAC_PoString(dt.Rows[0]["delivery_no"].ToString().Trim()) ;//Multiple PO               
                exeRes = calculatePacPoPro(cartonNo);
                if (!exeRes.Status)
                {
                    return exeRes;
                }
                string po = (string)exeRes.Anything;
                string origin = dt.Rows[0]["ORIGIN"].ToString().Trim();
                string invoiceno = dt.Rows[0]["INVOICENO"].ToString().Trim();
                string shipId = dt.Rows[0]["SHIPID"].ToString().Trim();
                string shippingtime = dt.Rows[0]["shipDate"].ToString().Trim();
                string tel = dt.Rows[0]["tel"].ToString().Trim();
                string salesOrder = dt.Rows[0]["SALESORDER"].ToString().Trim();
                string webOrder = dt.Rows[0]["WEBORDER"].ToString().Trim();
                string delDate = dt.Rows[0]["DEL_DATE"].ToString().Trim();
                string ctry = coc;
                string sscc = "";
                string cartons = "";
                exeRes = getCartonsOfPrintLabelInfoByCartonNo(cartonNo, shipmentInfo.ShipmentId, isMix);
                if (exeRes.Status)
                {
                    cartons = (string)exeRes.Anything;
                }
                else
                {
                    return exeRes;
                }
                exeRes = getSsccOfPrintLabelInfoByCartonNo(cartonNo, isMix);
                if (exeRes.Status)
                {
                    sscc = (string)exeRes.Anything;
                }
                else
                {
                    return exeRes;
                }
                string allShipTo = "";
                string shipToName = dt.Rows[0]["shiptoname"].ToString().Trim();
                string shipToName2 = dt.Rows[0]["shiptocompany"].ToString().Trim();
                string shipToAddress = dt.Rows[0]["shiptoaddress"].ToString().Trim();
                string shipToAddress2 = dt.Rows[0]["shiptoaddress2"].ToString().Trim();
                string shipToAddress3 = dt.Rows[0]["shiptoaddress3"].ToString().Trim();
                string shipToAddress4 = dt.Rows[0]["shiptoaddress4"].ToString().Trim();
                string shipToCity = dt.Rows[0]["shiptocity"].ToString().Trim();
                string shipToState = dt.Rows[0]["shiptostate"].ToString().Trim();
                string shipToZip = dt.Rows[0]["shiptozip"].ToString().Trim();
                string shipToConuntry = dt.Rows[0]["shiptocountry"].ToString().Trim();
                allShipTo = calculateShiptoORReturnTo(shipToName, shipToName2, shipToAddress, shipToAddress2, shipToAddress3, shipToAddress4, shipToCity + " " + shipToState + " " + shipToZip, shipToConuntry);
                allShipTo = splitOver33Length(allShipTo, 30);
                //PAC--CARRIER|COC|CTRY|HAWB|PO|ORIGIN|INVOICENO|CARTONS|SHIPID|SHIP_DATE|TEL|SSCC|salesOrder|webOrder|
                //     CARRIER|COC|CTRY|HAWB|PO|DN|ORIGIN|INVOICENO|CARTONS|SHIPID|SHIP_DATE|TEL|SSCC|SALESORDER|WEBORDER|
                lastLableContent = carrierName + "|" + coc + "|" + ctry + "|" + hawb + "|" + po + "|" + dn + "|" + origin + "|" + invoiceno + "|" + cartons + "|" + shipId + "|" + shippingtime + "|" + tel + "|" + sscc + "|" + salesOrder + "|" + webOrder + "|" + delDate + "|" + allReturnTo + allShipTo;
                exeRes.Anything = lastLableContent;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "获取打印亚太shippingLabel信息失败，请联系IT-ppsTeam！";
            }
            return exeRes;
        }
        #endregion

        private string calculatePAC_PoString(string deliveryNo)
        {
            /*
             T_940_UNICODE--itemcustpoline  此栏位 
             去除重复 rows>1，poValue = ‘Multiple PO’    
             rows=1，poValue = ‘itemcustpoline’      
             */
            DataTable dt = new DataTable();
            string poValue = "";
            dt = selectData.calculatePAC_PoString(deliveryNo);
            if (dt.Rows.Count > 0 && dt != null)
            {
                if (dt.Rows.Count > 1)
                {
                    poValue = "Multiple PO";
                }
                else
                {
                    poValue = dt.Rows[0]["ITEMCUSTPOLINE"].ToString();
                }
            }
            return poValue;
        }




        #region  EMEIA(欧洲段字符串)
        private ExecuteResult getFrontPartWorkingStringOfEMEIA(ShipmentInfo shipmentInfo, string cartonNo, bool isMix)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            string lastLableContent = "";
            string allReturnTo = "";
            exeRes = getDSEMEIAALLReturnInfoByCartonNo(cartonNo);
            allReturnTo = (string)exeRes.Anything;
            dt = selectData.getPrintInfoOfPACByCartonNO(cartonNo);
            if (dt.Rows.Count > 0 && dt != null)
            {
                string carrierName = dt.Rows[0]["carrier"].ToString().Trim();
                //coc   = poe,ctry
                string coc = dt.Rows[0]["poe"].ToString().Trim();
                string portType = "";
                if (coc.Length == 2)
                {
                    portType = "COC";
                    if (coc.Equals("SA"))
                    {
                        portType = "POE";
                        coc = dt.Rows[0]["PORTOFENTRY"].ToString().Trim();
                    }
                }
                else
                {
                    portType = "POE";
                }
                string hawb = dt.Rows[0]["hawb"].ToString().Trim();
                string dn = dt.Rows[0]["delivery_no"].ToString().Trim();
                string origin = dt.Rows[0]["ORIGIN"].ToString().Trim();
                string invoiceno = dt.Rows[0]["INVOICENO"].ToString().Trim();
                // string shipId = dt.Rows[0]["SHIPID"].ToString().Trim();
                string shippingtime = dt.Rows[0]["shipDate"].ToString().Trim();
                string tel = dt.Rows[0]["tel"].ToString().Trim();
                if (string.IsNullOrEmpty(tel))//只针对EMEIA
                {
                    tel = "00000000";
                }
                string salesOrder = dt.Rows[0]["SALESORDER"].ToString().Trim();
                string webOrder = dt.Rows[0]["WEBORDER"].ToString().Trim();
                string po = dt.Rows[0]["custpono"].ToString().Trim();
                string ctry = dt.Rows[0]["ctry"].ToString().Trim();
                string shipPlant = dt.Rows[0]["shipplant"].ToString().Trim();
                string sscc = "";
                string cartons = "";
                string uuid = (string)getEMEIA_UUIDByCartonNo(cartonNo, shipPlant, isMix).Anything;
                exeRes = getCartonsOfPrintLabelInfoByCartonNo(cartonNo, shipmentInfo.ShipmentId, isMix);
                if (exeRes.Status)
                {
                    cartons = (string)exeRes.Anything;
                }
                else
                {
                    return exeRes;
                }
                exeRes = getSsccOfPrintLabelInfoByCartonNo(cartonNo, isMix);
                if (exeRes.Status)
                {
                    sscc = (string)exeRes.Anything;
                }
                else
                {
                    return exeRes;
                }
                string allShipTo = "";
                string shipToName = dt.Rows[0]["shiptoname"].ToString().Trim();
                string shipToName2 = dt.Rows[0]["shiptocompany"].ToString().Trim();
                string shipToAddress = dt.Rows[0]["shiptoaddress"].ToString().Trim();
                string shipToAddress2 = dt.Rows[0]["shiptoaddress2"].ToString().Trim();
                string shipToAddress3 = dt.Rows[0]["shiptoaddress3"].ToString().Trim();
                string shipToAddress4 = dt.Rows[0]["shiptoaddress4"].ToString().Trim();
                string shipToCity = dt.Rows[0]["shiptocity"].ToString().Trim();
                string shipToState = dt.Rows[0]["shiptostate"].ToString().Trim();
                string shipToZip = dt.Rows[0]["shiptozip"].ToString().Trim();
                string shipToConuntry = dt.Rows[0]["shiptocountry"].ToString().Trim();
                allShipTo = calculateShiptoORReturnTo(shipToName, shipToName2, shipToAddress, shipToAddress2, shipToAddress3, shipToAddress4, shipToCity + " " + shipToState + " " + shipToZip, shipToConuntry);
                allShipTo = splitOver33Length(allShipTo);
                //PAC--CARRIER|COC|CTRY|HAWB|PO|ORIGIN|INVOICENO|CARTONS|SHIPID|SHIP_DATE|TEL|SSCC|salesOrder|webOrder|
                lastLableContent = carrierName + "|" + coc + "|" + ctry + "|" + hawb + "|" + po + "|" + dn + "|" + origin + "|" + invoiceno + "|" + cartons + "|" + uuid + "|" + shippingtime + "|" + tel + "|" + sscc + "|" + salesOrder + "|" + webOrder + "|" + portType + "|" + allReturnTo + allShipTo;
                if (shipmentInfo.Region.Equals("EMEIA") && (shipmentInfo.CarrierCode.Equals("1060020795") || shipmentInfo.CarrierCode.Equals("1060029822")))
                {
                    dt = selectData.getKnboxNoOfPrintLabelInfoByCartonNo(cartonNo);
                    string knboxNo = dt.Rows[0]["KNBOXNO"].ToString();
                    lastLableContent = knboxNo + "|" + lastLableContent;
                }
                exeRes.Anything = lastLableContent;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "获取打印亚太shippingLabel信息失败，请联系IT-ppsTeam！";
            }

            return exeRes;
        }
        #endregion
        #endregion


        private string splitOver33Length(string handleStr, int subLen = 45)
        {
            string[] handleStrArray = handleStr.Split('|');
            List<string> listStr = new List<string>();
            List<string> addList = new List<string>();
            for (int i = 0; i < handleStrArray.Length; ++i)
            {
                if (handleStrArray[i].Length > subLen)
                {
                    string strTemp = handleStrArray[i].Substring(0, subLen + 1);
                    int splitDefault = subLen;
                    if ((strTemp.Substring(subLen) != " "))
                    {
                        int splitLocationA = strTemp.LastIndexOf(" ");
                        if (splitLocationA > 0)
                        {
                            splitDefault = splitLocationA;
                        }
                    }
                    listStr.Add(handleStrArray[i].Substring(0, splitDefault));
                    listStr.Add(handleStrArray[i].Substring(splitDefault).Trim());
                    continue;
                }
                listStr.Add(handleStrArray[i]);
            }
            if (listStr.Count > 8)
            {
                for (int i = 0; i < 8; ++i)
                {
                    addList.Add(listStr[i]);
                }
            }
            return calculateShiptoORReturnTo(addList.ToArray());
        }
        private ExecuteResult getEMEIA_UUIDByCartonNo(string cartonNo, string shipPlant, bool isMix)
        {
            /*
             当region=EMEIA，并为mix时，查询一箱是否有1ps，若为1ps，打印uuicode，否则为-‘0000000000000000000000’（length=22）
             */
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            string uuid = "0000000000000000000000";
            List<string> listStr = new List<string>();
            dt = selectData.getEMEIA_UUIDByCartonNo(cartonNo);
            if (shipPlant.Contains("MIT"))//weifeng-->增加打印uuicode打印规则
            {
                if (isMix)
                {

                    if (dt.Rows.Count == 1)
                    {
                        uuid = dt.Rows[0]["uuicode"].ToString();
                    }
                    else
                    {
                        //print multi MPN label
                        exeRes = getAllUUICodeByCartonNo(cartonNo);
                        if (exeRes.Status)
                        {
                            listStr.Add((string)exeRes.Anything);
                            exeRes = printLabel.printLableForModifyVersion("EMEIA_UUICODE", listStr, 1);
                            if (!exeRes.Status)
                            {
                                return exeRes;
                            }
                        }
                        else
                        {
                            return exeRes;
                        }
                    }
                }

                else
                {
                    //for nomix  EMEIA_UUICODE
                    if (dt.Rows.Count == 1)
                    {
                        uuid = dt.Rows[0]["uuicode"].ToString();
                    }
                    else
                    {
                        //一个pack栈板，一箱多pis打印uuicodeLabel
                        if (getPackPalletNoQtyByCartonNo(cartonNo))
                        {
                            exeRes = getAllUUICodeByCartonNo(cartonNo);
                            if (exeRes.Status)
                            {
                                listStr.Add((string)exeRes.Anything);
                                exeRes = printLabel.printLableForModifyVersion("EMEIA_UUICODE", listStr, 1);
                                if (!exeRes.Status)
                                {
                                    return exeRes;
                                }
                            }
                            else
                            {
                                return exeRes;
                            }
                        }
                    }
                }
            }
            exeRes.Anything = uuid;
            return exeRes;
        }
        public bool getPackPalletNoQtyByCartonNo(string cartonNo)
        {
            bool flag = false;
            DataTable dt = new DataTable();
            dt = selectData.calPackPalletNoQtyByCartonNo(cartonNo);
            if (dt.Rows[0]["CARTONQTY"].ToString().Equals("1"))
            {
                flag = true;
            }
            return flag;
        }
        public ExecuteResult getAllUUICodeByCartonNo(string cartonNo)
        {

            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            string allWorkingStr = "";
            dt = selectData.getAllUUICodeByCartonNo(cartonNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    allWorkingStr += dt.Rows[i]["UUICODE"].ToString() + "|";
                }
                exeRes.Anything = cartonNo + "|" + allWorkingStr;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此箱号:" + cartonNo + "未能获取到UUICODE，请检查！";
            }
            return exeRes;
        }
        private ExecuteResult getEMEIAReturnInfoLogic_NotMit(string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = selectData.getPrintInfoReturnInfo_EMEIA_ORES_ByCartonNo(cartonNo);
            if (dt.Rows.Count <= 0 || dt == null)
            {
                dt = selectData.getPrintInfoReturnInfo_EMEIA_ORED_ByCartonNo(cartonNo);
                if (dt.Rows.Count <= 0 || dt == null)
                {
                    dt = selectData.getPrintInfoReturnInfo_EMEIA_OTHER();
                }
            }
            string returnAddress1 = dt.Rows[0]["returnaddress1"].ToString();
            string returnAddress2 = dt.Rows[0]["returnaddress2"].ToString();
            string returnAddress3 = dt.Rows[0]["returnaddress3"].ToString();
            string returnAddress4 = dt.Rows[0]["returnaddress4"].ToString();
            string returnAddress5 = dt.Rows[0]["returnaddress5"].ToString();
            string returnAddress6 = dt.Rows[0]["returnaddress6"].ToString();
            string returnAddress7 = dt.Rows[0]["returnaddress7"].ToString();
            string returnAddress8 = dt.Rows[0]["returnaddress8"].ToString();
            exeRes.Anything = calculateShiptoORReturnTo(returnAddress1, returnAddress2, returnAddress3, returnAddress4, returnAddress5, returnAddress6, returnAddress7, returnAddress8);
            return exeRes;
        }
        public ExecuteResult getDSEMEIAALLReturnInfoByCartonNo(string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            if (isEMEIAMITRETURN(cartonNo))
            {
                exeRes = getEMEIA_MIT_RETURN_INFO(cartonNo);
            }
            else
            {
                exeRes = getEMEIAReturnInfoLogic_NotMit(cartonNo);
            }
            return exeRes;
        }
        public ExecuteResult getEMEIA_MIT_RETURN_INFO(string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = selectData.getEMEIA_MIT_RETURNINFO();
            if (dt != null && dt.Rows.Count > 0)
            {
                string[] returninfos = new string[8];
                for (int i = 0; i < returninfos.Length; i++)
                {
                    returninfos[i] = "";
                }
                returninfos[0] = dt.Rows[0]["RU_2_NAME"].ToString();
                string afterReturns = dt.Rows[0]["RU_2_ADDRESS"].ToString();
                for (int j = 0; j < afterReturns.Split(',').Length; j++)
                {
                    returninfos[j + 1] = afterReturns.Split(',')[j];
                }
                exeRes.Anything = calculateShiptoORReturnTo(returninfos);
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "获得EMEIA-MIT return信息失败，查看是否维护MIT return信息！";
            }
            return exeRes;
        }
        private bool isEMEIAMITRETURN(string cartonNo)
        {
            bool flag = false;
            DataTable dt = new DataTable();
            dt = selectData.isEMEIA_MIT(cartonNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["SHIPPLANT"].ToString().Equals("MIT"))
                {
                    flag = true;
                }
            }
            return flag;
        }
        private ExecuteResult getPACReturnInfoByCartonNo(string cartonNo, string shipInfoType)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = selectData.getPACReturnInfoByCartonNoAndShipInfoType(cartonNo, shipInfoType);
            if (dt.Rows.Count > 0 && dt != null)
            {
                exeRes.Anything = makeupPACReturnInfoByDataContent(dt.Rows[0]["datacontent"].ToString());
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此箱号：" + cartonNo + " 无法获得DS_PACReturn 信息,请联系PPS-team！";

            }
            return exeRes;
        }

        private string[] makeupPACReturnInfoByDataContent(string dataContent)
        {
            string[] allReturnInfo = new string[8];
            string[] dataContentArr = dataContent.Replace("\r", "").Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < allReturnInfo.Length; i++)
            {
                if (i >= dataContentArr.Length)
                {
                    allReturnInfo[i] = "";
                }
                else
                {
                    allReturnInfo[i] = dataContentArr[i];
                }
            }
            return allReturnInfo;
        }
        private string calculateShiptoORReturnTo(params string[] arrSort)
        {
            string temp = "";
            for (int i = 0; i < arrSort.Length; i++)
            {
                for (int j = i + 1; j < arrSort.Length; j++)
                {
                    if (string.IsNullOrEmpty(arrSort[i]))
                    {
                        temp = arrSort[j];
                        arrSort[j] = arrSort[i];
                        arrSort[i] = temp;
                    }
                }
            }
            temp = "";
            for (int i = 0; i < arrSort.Length; ++i)
            {
                temp += arrSort[i] + "|";
            }
            return temp;
        }

        /// <summary>
        /// 根据对应的region和carrierName  来转换对应的label
        /// </summary>
        /// <param name="region"></param>
        /// <param name="carrierName"></param>
        /// <returns></returns>
        private string getLabelName(ShipmentInfo shipmentInfo)
        {
            string labelName = "";
            switch (shipmentInfo.Region)
            {
                case "PAC":
                    labelName = "SH_ASIA";
                    if (shipmentInfo.ShipmentType.Equals("DS"))
                    {
                        labelName = "SH_DS_ASIA";
                    }
                    break;
                case "EMEIA":
                    if (shipmentInfo.ShipmentType.Equals("DS"))
                    {
                        if (shipmentInfo.CarrierCode.Equals("1060020795") || shipmentInfo.CarrierCode.Equals("1060029822"))
                        {
                            labelName = "SH_DS_EUROPEAN_KN";
                        }
                        else
                        {
                            labelName = "SH_DS_EUROPEAN_HAWB";
                        }
                    }
                    else
                    {
                        if (shipmentInfo.CarrierCode.Equals("1060020795") || shipmentInfo.CarrierCode.Equals("1060029822"))
                        {
                            labelName = "SH_EUROPEAN_KN";
                        }
                        else
                        {
                            labelName = "SH_EUROPEAN_HAWB";
                        }
                    }
                    break;
                case "AMR":
                    if (shipmentInfo.ShipmentType.Equals("DS") && shipmentInfo.TYPE.Equals("BULK"))
                    {
                        labelName = "SH_DS_AMR";
                    }
                    else
                    {
                        labelName = "SH_USA";
                    }
                    break;
                default:
                    labelName = "";
                    break;
            }
            return labelName;
        }
        /// <summary>
        /// label  打印逻辑
        /// </summary>
        /// <param name="isMix"></param>
        /// <param name="shipmentId"></param>
        /// <param name="cartonNo"></param>
        /// <param name="shipmentInfo"></param>
        public ExecuteResult printAllLabelLogic(bool isMix, string cartonNo, ShipmentInfo shipmentInfo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            List<string> labelContentList = new List<string>();
            if (isMix)
            {
                exeRes = getFullWorkingString(shipmentInfo, cartonNo, isMix);
                if (exeRes.Status)
                {
                    labelContentList = (List<string>)exeRes.Anything;
                    exeRes = printLabel.printLableForModifyVersion(getLabelName(shipmentInfo), labelContentList, 1);
                    ClientUtils.WritePrintLabelLog("SHIPPING LABEL", cartonNo, "", LocalHelper.getMacAddr_Local());
                    //PrintLog("SHIPPING LABEL");
                }
            }
            else
            {
                if (isPrintForNoMix(cartonNo))
                {
                    exeRes = getFullWorkingString(shipmentInfo, cartonNo, isMix);
                    if (exeRes.Status)
                    {
                        labelContentList = (List<string>)exeRes.Anything;
                        exeRes = printLabel.printLableForModifyVersion(getLabelName(shipmentInfo), labelContentList, 1);
                        //PrintLog("SHIPPING LABEL");
                        ClientUtils.WritePrintLabelLog("SHIPPING LABEL", cartonNo, "", LocalHelper.getMacAddr_Local());
                    }
                }
            }
            return exeRes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ExecuteResult printGS1LabelLogic(ShipmentInfo shipmentInfo, string cartonNo)
        {
            DataTable dt = new DataTable();
            ExecuteResult exeRes = new ExecuteResult();
            List<string> allWorkingStrList = new List<string>();
            dt = selectData.getGS1LabelInfo(cartonNo);
            var ctrCOOPrint = new List<string>();
            if (dt != null && dt.Rows.Count > 0)
            {
                //multi coo gs1 label 需要根据coo具体数量进行打印  add by franky 2020年3月16日
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[0]["gs1flag"].ToString().Equals("Y"))
                    {
                        allWorkingStrList = new List<string>();
                        string modifyQty = "";
                        string custPart = dt.Rows[i]["CUSTPART"].ToString();
                        string modelNo = dt.Rows[i]["MODELNO"].ToString();
                        string gtin = dt.Rows[i]["gtin"].ToString().PadLeft(14, '0');//左填充  14位   填充为0
                        string ssccCode = dt.Rows[i]["sscc"].ToString();
                        string qty = dt.Rows[i]["qty"].ToString();
                        string coo = dt.Rows[i]["coo"].ToString();
                        string qrCOO = dt.Rows[i]["QRCOO"].ToString(); //add by wenxing 20201117
                        // add  coo column by Franky 2020/3/3
                        if (qty.Length % 2 == 0)
                        {
                            modifyQty = qty;
                        }
                        else
                        {
                            modifyQty = "0" + qty;
                        }
                        string sccount = "(02)" + gtin + "(37)" + modifyQty;
                        string sccount_bar = "02" + gtin + "37" + modifyQty;
                        if (isMultiMpnForGs1ByCartonNo(cartonNo))
                        {
                            if (ctrCOOPrint.Exists(x => x == coo)) continue;
                            var allWorkingString = ssccCode + "|" + coo + "|";
                            if (IsGs1Coo())//check gs1 QR-code coo flag by wenxing 2020-11-25
                                allWorkingString += qrCOO + "|";
                            allWorkingStrList.Add(allWorkingString);
                            exeRes = printLabel.printLableForModifyVersion("GS1_multi", allWorkingStrList, 1);//multi MPn
                            ctrCOOPrint.Add(coo);
                        }
                        else
                        {
                            //string allWorkingString = custPart + "|" + modelNo + "|" + gtin + "|" + ssccCode + "|" + qty + "|" + sccount + "|" + sccount_bar + "|" + modifyQty + "|" + coo + "|";
                            string allWorkingString = custPart + "|" + modelNo + "|" + gtin + "|" + ssccCode + "|" + qty + "|" + sccount + "|" + sccount_bar + "|" + modifyQty + "|" + coo + "|";
                            if (IsGs1Coo())//check gs1 QR-code coo flag by wenxing 2020-11-25
                                allWorkingString += qrCOO + "|";
                            allWorkingStrList.Add(allWorkingString);
                            exeRes = printLabel.printLableForModifyVersion("GS1PalletsheetLabel", allWorkingStrList, 1);//signle MPN
                        }
                        if (!exeRes.Status)
                        {
                            return exeRes;
                        }
                        exeRes.Message = "OK_GS1_label打印成功！";
                        MediasHelper.PlaySoundAsyncByGS1();
                        //PrintLog("GS1 LABEL");
                        ClientUtils.WritePrintLabelLog("GS1 LABEL", cartonNo, "", LocalHelper.getMacAddr_Local());
                    }
                    else
                    {
                        //不需要打印GS1label
                        exeRes.Status = false;
                        exeRes.Message = "NG_无需打印GS1label！";
                        return exeRes;
                    }
                }
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "NG_获取GS1_Label信息错误，请联系IT-PPSTEAM!";
            }
            return exeRes;
        }

        private void PrintLog(string strLabelType)
        {
            try
            {
                // string strMacAddr = LocalHelper.getMacAddr_Local();
                //  string strIpAddr = ClientUtils.IPAddressV4;
                //  string strEmpNo = ClientUtils.fLoginUser;
                string sSQL = @"    INSERT INTO PPSUSER.T_PRINT_LABEL_LOG
                                                (LABEL_NAME ,--标签类型
                                                LABEL_CONTENT,--IP 地址
                                                MAC_ADDRESS,--MAC地址
                                                CREATE_TIME,--补印时间
                                                REMARK --作业工号)
                                                VALUES
                                                (
                                               :varLabelType,
                                                :varIpAddr,
                                                :varMacAddr,
                                                sysdate,
                                                :varEmp
                                                )";
                ClientUtils.ExecuteSQL(sSQL, new object[4][] {
                    new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varLabelType", strLabelType },
                    new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varIpAddr", ClientUtils.IPAddressV4 },
                    new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varMacAddr", LocalHelper.getMacAddr_Local() },
                    new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varEmp", ClientUtils.fLoginUser } });
            }
            catch { }
        }
        public bool IsGs1Coo()
        {

            bool flag = false;
            DataTable dt = new DataTable();
            dt = selectData.IsGs1Coo();
            if (Convert.ToInt32(dt.Rows[0]["checkGs1Coo"].ToString()) >= 1)
            {
                flag = true;
            }
            return flag;
        }
        public bool isMultiMpnForGs1ByCartonNo(string cartonNo)
        {
            bool flag = false;
            DataTable dt = new DataTable();
            dt = selectData.isMultiMpnForGs1ByCartonNo(cartonNo);
            if (Convert.ToInt32(dt.Rows[0]["checkMpn"].ToString()) > 1)
            {
                flag = true;
            }
            return flag;
        }
        public ExecuteResult getLatterPartOfWorkingString(bool isMix, string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            if (isMix)
            {
                dt = selectData.getLatterPartOfLabelPrintInfo_mix(cartonNo);
            }
            else
            {
                dt = selectData.getLatterPartOfLabelPrintInfo_noMix(cartonNo);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此箱号：" + cartonNo + " 无法获取itemLine，料号，数量，请联系IT-PPS！";
            }
            return exeRes;
        }

        public ExecuteResult printDeliveryNoteLabel(string cartonNo, string deliveryNo, string lineItem, ShipmentInfo shipmentInfo, bool isReprint)
        {
            ExecuteResult exeRes = new ExecuteResult();
            if (shipmentInfo.TYPE.Equals("PARCEL"))
            {
                DataTable dt = new DataTable();
                dt = selectData.getT940InfoByDeliveryNo(deliveryNo);
                if (dt.Rows.Count > 0 && dt != null)
                {
                    string shipCnCode = dt.Rows[0]["SHIPCNTYCODE"].ToString();
                    string custGroup = dt.Rows[0]["CUSTOMERGROUP"].ToString();
                    if (shipCnCode.Equals("JP"))
                    {
                        //针对YMT 出 JP 不出实物的DeliveryNote了 MODIFY 2020-1-14
                        if (Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*YMT\w*") || Regex.IsMatch(shipmentInfo.CarrierName, @"\w*YMT\w*"))
                        {
                            exeRes.Status = false;
                            exeRes.Message = "NG_无需打印DeliveryNote!";
                            return exeRes;
                        }
                        exeRes = isPrintDeliveryNoteLableByCartonNoAndShipInfoType(cartonNo, shipmentInfo.TYPE);
                        if (!exeRes.Status)
                        {
                            return exeRes;
                        }
                    }
                    else
                    {
                        if (custGroup.Equals("IN") || custGroup.Equals("RK") || custGroup.Equals("RW"))
                        {
                            exeRes.Status = false;
                            exeRes.Message = "NG_无需打印DeliveryNote!";
                            return exeRes;
                        }
                        else
                        {
                            exeRes = isPrintDeliveryNoteLableByCartonNoAndShipInfoType(cartonNo, shipmentInfo.TYPE);
                            if (!exeRes.Status)
                            {
                                return exeRes;
                            }
                        }
                    }
                }
            }
            else
            {
                exeRes = isPrintDeliveryNoteLableByCartonNoAndShipInfoType(cartonNo, shipmentInfo.TYPE);
                if (!exeRes.Status)
                {
                    return exeRes;
                }
            }
            exeRes = checkOmsBucketDocIsExist("PAC", "Delivery Note");
            if (!exeRes.Status)
            {
                return exeRes;
            }
            if (isPrintCrt(cartonNo, isReprint))
            {
                try
                {
                    new D1_DeliveryNote2(deliveryNo, lineItem, shipmentInfo.ShipmentId, false, Application.StartupPath + "\\PDF\\");
                    MediasHelper.PlaySoundAsyncByDeliveryNote();
                    exeRes.Message = "OK_DeliveryNo_Label！";
                    //PrintLog("DELIVERY NOTE");
                    ClientUtils.WritePrintLabelLog("DELIVERY NOTE", cartonNo, "", LocalHelper.getMacAddr_Local());
                }
                catch (Exception ex)
                {
                    exeRes.Status = false;
                    exeRes.Message = "NG_" + ex.Message;
                }
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "NG_无需打印DELIVERYNOTE!";
            }
            return exeRes;
        }
        private ExecuteResult isPrintDeliveryNoteLableByCartonNoAndShipInfoType(string cartonNo, string shipInfoType)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = selectData.isPrintDeliveryNoteLableByCartonNoAndShipInfoType(cartonNo, shipInfoType);
            if (dt.Rows.Count > 0 && dt != null)
            {
                if (dt.Rows[0]["checkCount"].ToString().Equals("0"))
                {
                    exeRes.Status = false;
                    exeRes.Message = "NG_无需打印DeliveryNote_Label!";
                }
            }
            return exeRes;
        }

        public bool isReprint(string cartonNo)
        {
            bool flag = false;
            DataTable dt = new DataTable();
            dt = selectData.isReprint(cartonNo);
            if (dt.Rows.Count > 0 && dt != null)
            {
                if (dt.Rows[0]["checkResult"].ToString().Equals("OK"))
                {
                    flag = true;
                }
            }
            return flag;
        }

        public ExecuteResult getReprintLabelFormInfo(string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = selectData.getReprintLabelFormInfo(cartonNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此箱号：" + cartonNo + " 查询不到信息，请检查箱号是否正确！";
            }
            return exeRes;
        }

        public ExecuteResult getFullWorkingString(ShipmentInfo shipmentInfo, string cartonNo, bool isMix)
        {
            List<string> labelContentList = new List<string>();
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            string frontPartWorkingString = "";
            int perNum = 3;
            exeRes = getFrontPartWorkingString(isMix, shipmentInfo, cartonNo);
            if (exeRes.Status)
            {
                frontPartWorkingString = (string)exeRes.Anything;
                exeRes = getLatterPartOfWorkingString(isMix, cartonNo);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    //逢三加一张
                    //if (shipmentInfo.ShipmentType.Equals("DS"))
                    //{

                    //comment by wenxing 20200915
                    //if (shipmentInfo.Region.Equals("EMEIA") && (shipmentInfo.CarrierCode.Equals("1060020795") || shipmentInfo.CarrierCode.Equals("1060029822")))
                    //{
                    //    //KN
                    //    perNum = 1;
                    //}
                    //}
                    //else
                    //{
                    //    if (shipmentInfo.Region.Equals("EMEIA") &&  shipmentInfo.CarrierName.Equals("KNBULK"))
                    //    {
                    //        //KN
                    //        perNum = 1;
                    //    }
                    //}
                    labelContentList = calculateLatterPartNoInfo(perNum, frontPartWorkingString, dt);
                    exeRes.Anything = labelContentList;
                }
                else
                {
                    return exeRes;
                }
            }
            else
            {
                return exeRes;
            }
            return exeRes;
        }



        public List<string> calculateLatterPartNoInfo(int perNum, string frontPartWorkingString, DataTable dt)
        {
            List<string> latterPartWorkingStringList = new List<string>();
            string perLatterPartWorkingString = "";
            if (dt.Rows.Count > perNum)
            {
                for (int j = 0; j < dt.Rows.Count; ++j)
                {
                    string lineItem = dt.Rows[j]["lineItem"].ToString();
                    string partNo = dt.Rows[j]["mpn"].ToString();
                    string qty = dt.Rows[j]["qty"].ToString();
                    perLatterPartWorkingString += lineItem + "|" + partNo + "|" + qty + "|";
                    if ((j + 1) % perNum == 0)
                    {
                        latterPartWorkingStringList.Add(frontPartWorkingString + perLatterPartWorkingString);
                        perLatterPartWorkingString = "";
                    }
                    if (j == dt.Rows.Count - 1)
                    {
                        if (!(dt.Rows.Count % perNum == 0))
                        {
                            latterPartWorkingStringList.Add(frontPartWorkingString + perLatterPartWorkingString);
                            perLatterPartWorkingString = "";
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < dt.Rows.Count; ++j)
                {
                    string lineItem = dt.Rows[j]["lineItem"].ToString();
                    string partNo = dt.Rows[j]["mpn"].ToString();
                    string qty = dt.Rows[j]["qty"].ToString();
                    perLatterPartWorkingString += lineItem + "|" + partNo + "|" + qty + "|";
                }
                latterPartWorkingStringList.Add(frontPartWorkingString + perLatterPartWorkingString);
            }
            return latterPartWorkingStringList;
        }



        public bool isPrintForNoMix(string cartonNo)
        {
            bool flag = false;
            DataTable dt = new DataTable();
            dt = selectData.isPrintForNoMix(cartonNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }
        public ExecuteResult getCartonsOfPrintLabelInfoByCartonNo(string cartonNo, string shipmentId, bool isMix)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            if (isMix)
            {
                dt = selectData.getCartonsOfPrintLabelInfoByCartonNo_MIX(cartonNo, shipmentId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string totalQty = dt.Rows[0]["totalQty"].ToString();
                    string cartonQty = dt.Rows[0]["cartonQty"].ToString();
                    exeRes.Anything = cartonQty + @"/" + totalQty;
                }
                else
                {
                    exeRes.Status = false;
                    exeRes.Message = "此箱:" + cartonNo + " 在orderInfo对应无资料，请联系IT-PPS！";
                }
            }
            else
            {
                dt = selectData.getCartonsOfPrintLabelInfoByCartonNo_No_MIX(cartonNo, shipmentId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string totalQty = dt.Rows[0]["totalQty"].ToString();
                    string startBoxNo = dt.Rows[0]["startBoxNo"].ToString();
                    string endBoxNo = dt.Rows[0]["endBoxNo"].ToString();
                    exeRes.Anything = startBoxNo + @"-" + endBoxNo + @"/" + totalQty;
                }
                else
                {
                    exeRes.Status = false;
                    exeRes.Message = "此箱:" + cartonNo + " 在orderInfo对应无资料，请联系IT-PPS！";
                }
            }

            return exeRes;
        }

        public bool isPrintCrt(string inputData, bool isReprint, string strDNNo)
        {
            DataTable dt = new DataTable();
            bool flag = false;
            dt = selectData.isPrintCrt(inputData);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!isReprint)
                {
                    if (!string.IsNullOrEmpty(strDNNo))
                    {
                        //存在LOG则不打印
                        DataTable dtTempDN = selectData.GetMESPrintDN(strDNNo);
                        //if ((dtTempDN != null) || (dtTempDN.Rows.Count > 0))
                        if (dtTempDN.Rows.Count > 0)
                        {
                            return false;
                        }
                    }
                    string boxNo = dt.Rows[0]["BOXNO"].ToString();
                    if (boxNo.Equals("1"))
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = true;
                }
            }
            return flag;
        }

        public bool isPrintCrt(string inputData, bool isReprint)
        {
            DataTable dt = new DataTable();
            bool flag = false;
            dt = selectData.isPrintCrt(inputData);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!isReprint)
                {
                    string boxNo = dt.Rows[0]["BOXNO"].ToString();
                    if (boxNo.Equals("1"))
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = true;
                }
            }
            return flag;
        }

        public bool isFinishWorkByPickPalletNo(string pickPalletNo)
        {
            DataTable dt = new DataTable();
            bool flag = false;
            dt = selectData.isFinishWorkByPickPalletNo(pickPalletNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }
        public bool isFinishWorkByShipMentId(string shipmentId)
        {
            DataTable dt = new DataTable();
            bool flag = false;
            dt = selectData.isFinishWorkByShipMentId(shipmentId);
            if (dt != null && dt.Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }

        public ExecuteResult getSsccOfPrintLabelInfoByCartonNo(string cartonNo, bool isMix)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            string sscc = "";
            dt = selectData.getSsccOfPrintLabelInfoByCartonNo(cartonNo, isMix);
            if (dt != null && dt.Rows.Count > 0)
            {
                sscc = dt.Rows[0]["sscc"].ToString();
                exeRes.Anything = sscc;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此箱:" + cartonNo + " 没有SSCC，请联系IT-PPS！";
            }
            return exeRes;
        }
        public ExecuteResult getKnboxNoOfPrintLabelInfoByCartonNo(string cartonNo, bool isMix)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            string sscc = "";
            dt = selectData.getSsccOfPrintLabelInfoByCartonNo(cartonNo, isMix);
            if (dt != null && dt.Rows.Count > 0)
            {
                sscc = dt.Rows[0]["sscc"].ToString();
                exeRes.Anything = sscc;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此箱:" + cartonNo + " 没有SSCC，请联系IT-PPS！";
            }
            return exeRes;
        }




        public ExecuteResult queryOrderInfoByDn(string dn, string shipmentId)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = selectData.queryOrderInfoByDn(dn, shipmentId);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此:" + dn + " 查询不到信息！";
            }
            return exeRes;
        }
        public ExecuteResult checkQHQtyByPickPalletNo(string pickPalletNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "InputSno", pickPalletNo };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "InputType", "PICKPALLETNO" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RetMsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("ppsuser.T_Check_Hold", procParams);
            if (ds.Tables[0].Rows[0]["RetMsg"].ToString().Equals("OK"))
            {
                exeRes.Status = true;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = ds.Tables[0].Rows[0]["RetMsg"].ToString();
            }
            return exeRes;
        }

        public ExecuteResult calculatePacPoPro(string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CARTONNO", cartonNo };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "PO_NO", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RE_MSG", "" };
            DataSet ds = ClientUtils.ExecuteProc("ppsuser.t_calculate_pac_po", procParams);
            if (ds.Tables[0].Rows[0]["RE_MSG"].ToString().Equals("OK"))
            {
                exeRes.Anything = ds.Tables[0].Rows[0]["PO_NO"].ToString();
                exeRes.Status = true;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = ds.Tables[0].Rows[0]["RE_MSG"].ToString();
            }
            return exeRes;
        }

        public ExecuteResult packingPassStationPro(string shipmentId, string cartonNo, string pickPalletNo, string deliveryNo, string macAddress)
        {
            ExecuteResult exeRes = new ExecuteResult();
            object[][] procParams = new object[6][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "SHIPMENTID", shipmentId };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CARTONNO", cartonNo };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PICKPALLETNO", pickPalletNo };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "deliveryNo", deliveryNo };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "mac_address", macAddress };
            procParams[5] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "TMES", "" };
            DataSet ds = ClientUtils.ExecuteProc("ppsuser.t_packing_pass_Station2", procParams);
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

        public ExecuteResult printCrystalReportAllLogic(string inputData, ShipmentInfo shipmentInfo, string deliveryNo, string lineItem, bool isReprint, string palletNo)
        {
            //printCrystalReportAllLogic(cartonNo, shipmentInfo, deliveryNo, lineItem, true, packPalletNo)
            ExecuteResult exeRes = new ExecuteResult();
            if (isPrintCrt(inputData, isReprint, deliveryNo))
            {
                if (shipmentInfo.ShipmentType.Equals("FD"))
                {
                    switch (shipmentInfo.Region)
                    {
                        case "AMR":
                            exeRes = checkOmsBucketDocIsExist("AMR", "Hub Packing List");
                            if (!exeRes.Status)
                            {
                                return exeRes;
                            }
                            FDHubGlobalLayoutForm crtPrint = new FDHubGlobalLayoutForm(deliveryNo, shipmentInfo.ShipmentId, @"D:\", true);
                            //DataSet dtCrystal = new DataSet();
                            //string serverLabelName1 = string.Empty;
                            //FDHubGlobalLayoutForm crtPrint = new FDHubGlobalLayoutForm(deliveryNo, shipmentInfo.ShipmentId, "", false, false, "", 1, false, out dtCrystal, out serverLabelName1);
                            //PrintLog("PACKING LIST");
                            ClientUtils.WritePrintLabelLog("PACKING LIST", "", palletNo, LocalHelper.getMacAddr_Local());
                            MediasHelper.PlaySoundAsyncByPackList();
                            exeRes.Message = "OK_FD-PackingList(AMR-全球)！";
                            break;
                        default:
                            exeRes.Status = false;
                            exeRes.Message = "NG_FD-此地区:" + shipmentInfo.Region + "无需打印PackingList！";
                            if (isReprint)
                            {
                                exeRes.Message = "NG_没有PackingList标签需要打印！";
                            }
                            break;
                    }
                }
                else
                {
                    T940UnicodeInfo t940Unicode = new T940UnicodeInfo();
                    exeRes = getT940UnicodeInfoByDeliveryNoAndLineItem(deliveryNo, lineItem);
                    if (exeRes.Status)
                    {
                        t940Unicode = (T940UnicodeInfo)exeRes.Anything;
                        exeRes = judgeCrystalReportByCondition(t940Unicode, shipmentInfo.ShipmentId, palletNo);
                        if (exeRes.Status)
                        {
                            MediasHelper.PlaySoundAsyncByPackList();
                        }
                    }
                }
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "NG_无需打印PackingList！";
            }
            return exeRes;
        }
        public ExecuteResult checkOmsBucketDocIsExist(string region, string documentName)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = selectData.checkOmsBucketDocIsExist(region, documentName);
            if (dt != null)
            {
                if (dt.Rows[0]["checkCount"].ToString().Equals("0"))
                {
                    exeRes.Status = false;
                    exeRes.Message = "NG_此Region:" + region + " 对应的-" + documentName + " BucketType 维护异常！";
                }
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "NG_此Region:" + region + " 对应的-" + documentName + " BucketType 维护异常！";
            }
            return exeRes;
        }
        public ExecuteResult getReprintInfoByCartonNo(string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = selectData.getReprintInfoByCartonNo(cartonNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt;
            }
            else
            {
                exeRes.Message = "此箱号：" + cartonNo + "查询不到信息！";
                exeRes.Status = false;
            }
            return exeRes;
        }
        private ExecuteResult judgeCrystalReportByCondition(T940UnicodeInfo t940UnicodeInfo, string shipmentId, string palletNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = selectData.judgeCrystalReportByCondition(t940UnicodeInfo.region, t940UnicodeInfo.customerGroup, t940UnicodeInfo.msgFlag, t940UnicodeInfo.gpFlag);
            if (dt.Rows.Count > 0 && dt != null)
            {
                string documentName = dt.Rows[0]["documentname"].ToString().Trim();
                if (documentName.Equals("ConsumerPL"))
                {
                    switch (t940UnicodeInfo.region)
                    {
                        case "AMR":
                            exeRes = checkOmsBucketDocIsExist("AMR", "Consumer Packing List");
                            if (!exeRes.Status)
                            {
                                return exeRes;
                            }
                            //--US   --CA  cnanada  --MX   --co   -- Br --pe  -- cl
                            if (t940UnicodeInfo.ShipCntyCode.Equals("MX"))
                            {
                                //已ok--墨西哥
                                new Ltr_PkList_MEX(t940UnicodeInfo.deliveryNo, t940UnicodeInfo.lineItem, shipmentId, true, Application.StartupPath + "\\PDF\\");
                                exeRes.Message = "OK_DS-PackingList-ConsumerPL(AMR-墨西哥)！";
                            }
                            else if (t940UnicodeInfo.ShipCntyCode.Equals("CA"))
                            {
                                //已ok--加拿大
                                new ConsumerPkListBilingualCanadaForm(t940UnicodeInfo.deliveryNo, t940UnicodeInfo.lineItem, shipmentId, true);
                                exeRes.Message = "OK_DS-PackingList-ConsumerPL(AMR-加拿大)！";
                            }
                            else
                            {
                                //已OK--AMR_All
                                new P1ConsumerPackingListGlobal(t940UnicodeInfo.deliveryNo, t940UnicodeInfo.lineItem, shipmentId, true);
                                exeRes.Message = "OK_DS-PackingList-ConsumerPL(AMR-全球)！";
                            }
                            //PrintLog("PACKING LIST");
                            ClientUtils.WritePrintLabelLog("PACKING LIST", "", palletNo, LocalHelper.getMacAddr_Local());
                            break;
                        case "EMEIA":
                            //已OK -- 欧洲
                            exeRes = checkOmsBucketDocIsExist("EMEIA", "Consumer Packing List");
                            if (!exeRes.Status)
                            {
                                return exeRes;
                            }
                            new ConsumerPackingList_EMEA_G(t940UnicodeInfo.deliveryNo, t940UnicodeInfo.lineItem, shipmentId);  //参照  AMR_All 
                            exeRes.Message = "OK_DS-PackingList-ConsumerPL(EMEIA-欧洲)！";
                            //PrintLog("PACKING LIST");
                            ClientUtils.WritePrintLabelLog("PACKING LIST", "", palletNo, LocalHelper.getMacAddr_Local());
                            break;
                        case "PAC":
                            exeRes = checkOmsBucketDocIsExist("PAC", "Consumer Packing List");
                            if (!exeRes.Status)
                            {
                                return exeRes;
                            }
                            if (t940UnicodeInfo.ShipCntyCode.Equals("TW"))
                            {
                                new P1ConsumerPackingListTW(t940UnicodeInfo.deliveryNo, t940UnicodeInfo.lineItem, shipmentId, true);
                                exeRes.Message = "OK_DS-PackingList-ConsumerPL(PAC-中国台湾)！";
                            }
                            else if (t940UnicodeInfo.ShipCntyCode.Equals("CN"))
                            {
                                new P1ConsumerPackingListChina(t940UnicodeInfo.deliveryNo, t940UnicodeInfo.lineItem, shipmentId, true);
                                exeRes.Message = "OK_DS-PackingList-ConsumerPL(PAC-中国大陆)！";
                            }
                            else if (t940UnicodeInfo.ShipCntyCode.Equals("JP"))
                            {
                                new P1ConsumerPackingListJP(t940UnicodeInfo.deliveryNo, t940UnicodeInfo.lineItem, shipmentId, true);
                                exeRes.Message = "OK_DS-PackingList-ConsumerPL(PAC-日本)！";
                            }
                            else if (t940UnicodeInfo.ShipCntyCode.Equals("KR"))
                            {
                                new ConsumerPackingListKorea(t940UnicodeInfo.deliveryNo, t940UnicodeInfo.lineItem, shipmentId, true, Application.StartupPath + "\\PDF\\");
                                exeRes.Message = "OK_DS-PackingList-ConsumerPL(PAC-韩国)已经打印！";
                            }
                            else if (t940UnicodeInfo.ShipCntyCode.Equals("HK"))//香港
                            {
                                new P1ConsumerPackingListHK(t940UnicodeInfo.deliveryNo, t940UnicodeInfo.lineItem, shipmentId, true);
                                exeRes.Message = "OK_DS-PackingList-ConsumerPL(PAC-中国香港)！";
                            }
                            else if (t940UnicodeInfo.ShipCntyCode.Equals("TH"))//泰国
                            {
                                new ThaiLandPKFormcs(t940UnicodeInfo.deliveryNo, t940UnicodeInfo.lineItem, shipmentId, true);
                                exeRes.Message = "OK_DS-PackingList-ConsumerPL(PAC-泰国)！";
                            }
                            else
                            {
                                new P1ConsumerPackingListGlobal(t940UnicodeInfo.deliveryNo, t940UnicodeInfo.lineItem, shipmentId, true);
                                exeRes.Message = "OK_DS-PackingList-ConsumerPL(PAC-全球)！";
                            }
                            //PrintLog("PACKING LIST");
                            ClientUtils.WritePrintLabelLog("PACKING LIST", "", palletNo, LocalHelper.getMacAddr_Local());
                            break;
                        default:
                            exeRes.Message = "NG_DS:" + t940UnicodeInfo.region + " 未知地区，请确认信息是否维护！";
                            exeRes.Status = false;
                            break;
                    }
                }
                else
                {
                    switch (t940UnicodeInfo.region)
                    {
                        case "AMR":
                            exeRes = checkOmsBucketDocIsExist("AMR", "Channel Packing List");
                            if (!exeRes.Status)
                            {
                                return exeRes;
                            }
                            if (t940UnicodeInfo.ShipCntyCode.Equals("US"))//美国
                            {
                                new ChannelPackingListUS(t940UnicodeInfo.deliveryNo, t940UnicodeInfo.lineItem, shipmentId, true, Application.StartupPath + "\\PDF\\");
                                exeRes.Message = "OK_DS-PackingList-Channel(AMR-美国)！";
                            }
                            else if (t940UnicodeInfo.ShipCntyCode.Equals("MX"))//墨西哥
                            {
                                new MexicoChannelPackingList(t940UnicodeInfo.deliveryNo, t940UnicodeInfo.lineItem, shipmentId, true, Application.StartupPath + "\\PDF\\");
                                exeRes.Message = "OK_DS-PackingList-Channel(AMR-墨西哥)！";
                            }
                            else if (t940UnicodeInfo.ShipCntyCode.Equals("CA"))//加拿大
                            {
                                new ChannelPackingListCanada(t940UnicodeInfo.deliveryNo, t940UnicodeInfo.lineItem, shipmentId, true, Application.StartupPath + "\\PDF\\");
                                exeRes.Message = "OK_DS-PackingList-Channel(AMR-加拿大)！";
                            }
                            else if (t940UnicodeInfo.ShipCntyCode.Equals("CO"))//哥伦比亚
                            {
                                new ChannelColumbiaForm(t940UnicodeInfo.deliveryNo, t940UnicodeInfo.lineItem, shipmentId, true, Application.StartupPath + "\\PDF\\");
                                exeRes.Message = "OK_DS-PackingList-Channel(AMR-哥伦比亚)！";
                            }
                            else if (t940UnicodeInfo.ShipCntyCode.Equals("BR"))//巴西
                            {
                                new ChannelPackingListBrazil(t940UnicodeInfo.deliveryNo, t940UnicodeInfo.lineItem, shipmentId, true, Application.StartupPath + "\\PDF\\");
                                exeRes.Message = "OK_DS-PackingList-Channel(AMR-巴西)！";
                            }
                            else if (t940UnicodeInfo.ShipCntyCode.Equals("CL"))//智利
                            {
                                new ChileChannelPackingList(t940UnicodeInfo.deliveryNo, t940UnicodeInfo.lineItem, shipmentId, true, Application.StartupPath + "\\PDF\\");
                                exeRes.Message = "OK_DS-PackingList-Channel(AMR-智利)！";
                            }
                            else //if (t940UnicodeInfo.ShipCntyCode.Equals("PE"))//秘鲁
                            {
                                new ChannelPackingListPeru(t940UnicodeInfo.deliveryNo, t940UnicodeInfo.lineItem, shipmentId, true, Application.StartupPath + "\\PDF\\");
                                exeRes.Message = "OK_DS-PackingList-Channel(AMR-秘鲁)！";
                            }
                            //PrintLog("PACKING LIST");
                            ClientUtils.WritePrintLabelLog("PACKING LIST", "", palletNo, LocalHelper.getMacAddr_Local());
                            break;
                        case "EMEIA":
                            exeRes = checkOmsBucketDocIsExist("EMEIA", "Channel Delivery Note");
                            if (!exeRes.Status)
                            {
                                return exeRes;
                            }
                            if (t940UnicodeInfo.ShipCntyCode.Equals("RU") || t940UnicodeInfo.ShipCntyCode.Equals("AE") || t940UnicodeInfo.ShipCntyCode.Equals("TR"))
                            {
                                if (isMultiOrSignleCustSoNoByDeliveryNo(t940UnicodeInfo.deliveryNo))
                                {
                                    new EMEIA_BUY_DELIVERYNOTE(t940UnicodeInfo.deliveryNo, palletNo, false, "");
                                    exeRes.Message = "OK_DS-PackingList-DeliveryNote(EMEIA-BUY_Multip)！";
                                }
                                else
                                {
                                    new EMEIA_SELL_DELIVERYNOTE(t940UnicodeInfo.deliveryNo, palletNo);
                                    exeRes.Message = "OK_DS-PackingList-DeliveryNote(EMEIA-BUY_Single)！";
                                }
                            }
                            else
                            {
                                if (isMultiOrSignleCustSoNoByDeliveryNo(t940UnicodeInfo.deliveryNo))
                                {

                                    new EMEIA_MUTIL_DELIVERYNOTE(t940UnicodeInfo.deliveryNo, palletNo);
                                    exeRes.Message = "OK_DS-PackingList-DeliveryNote(EMEIA-SELL_Multip)！";
                                }
                                else
                                {
                                    new DeliveryNoteHForm(t940UnicodeInfo.deliveryNo, palletNo);
                                    exeRes.Message = "OK_DS-PackingList-DeliveryNote(EMEIA-SELL_Single)！";
                                }
                            }
                            //PrintLog("PACKING LIST");
                            ClientUtils.WritePrintLabelLog("PACKING LIST", "", palletNo, LocalHelper.getMacAddr_Local());
                            break;
                        default:
                            exeRes.Message = "NG_DS:" + t940UnicodeInfo.region + " 未知国别，请确认信息是否维护！";
                            exeRes.Status = false;
                            break;
                    }
                }
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "NG_无需打印PackingList！";
            }
            return exeRes;
        }
        private bool isMultiOrSignleCustSoNoByDeliveryNo(string deliveryNo)
        {
            bool flag = false;
            DataTable dt = new DataTable();
            dt = selectData.isMultiOrSignleCustSoNoByDeliveryNo(deliveryNo);
            if (Convert.ToInt32(dt.Rows[0]["checkCustSo"].ToString()) > 1)
            {
                flag = true;
            }
            return flag;
        }
        public ExecuteResult getSps203ImportWpx(string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = selectData.getSps203ImportWpx(cartonNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                string headLine = dt.Rows[0][0].ToString() + "|" + dt.Rows[0][1].ToString();
                string secLine = "";
                for (int i = 2; i < dt.Columns.Count; ++i)
                {
                    secLine += dt.Rows[0][i] + "|";
                }
                exeRes.Anything = headLine + Environment.NewLine + secLine;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此箱号：" + cartonNo + "没有获取到对应的sps203importWpx File，请联系IT-PPS! ";
            }
            return exeRes;
        }
        public ExecuteResult getSPSForAppleECXFileInfo(string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = selectData.getSPSForAppleECXFileInfo(cartonNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                string headLine = dt.Rows[0][0].ToString() + "|" + dt.Rows[0][1].ToString();
                string secLine = "";
                for (int i = 2; i < dt.Columns.Count; ++i)
                {
                    secLine += dt.Rows[0][i] + "|";
                }
                exeRes.Anything = headLine + Environment.NewLine + secLine;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此箱号：" + cartonNo + "没有获取到对应的SPSForAppleECXFileInfo File，请联系IT-PPS! ";
            }
            return exeRes;
        }

        public ExecuteResult getSPSImportBBXMotherFile(string shipmentId, string type)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            if (type.Equals("EMEIA"))//区分EMEIA  PAC
            {
                dt = selectData.getSPSImportBBXMotherForEMEA(shipmentId);
            }
            else
            {
                dt = selectData.getSPSImportBBXMotherForPAC(shipmentId);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                string headLine = dt.Rows[0][0].ToString() + "|" + dt.Rows[0][1].ToString();
                string secLine = "";
                for (int i = 2; i < dt.Columns.Count; ++i)
                {
                    secLine += dt.Rows[0][i] + "|";
                }
                exeRes.Anything = headLine + Environment.NewLine + secLine;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此集货单号：" + shipmentId + "没有获取到对应的SPSImportBBXMotherFile，请联系IT-PPS! ";
            }
            return exeRes;
        }

        public ExecuteResult getBBXBabyFile(string cartonNo, string type)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            if (type.Equals("EMEIA"))//区分EMEA  PAC
            {
                dt = selectData.getBBXBabyFileForEMEA(cartonNo);
            }
            else
            {
                dt = selectData.getBBXBabyFileForPAC(cartonNo);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                string headLine = dt.Rows[0][0].ToString() + "|" + dt.Rows[0][1].ToString();
                string secLine = "";
                for (int i = 2; i < dt.Columns.Count; ++i)
                {
                    secLine += dt.Rows[0][i] + "|";
                }
                exeRes.Anything = headLine + Environment.NewLine + secLine;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此箱号：" + cartonNo + "没有获取到对应的BBXBabyFile，请联系IT-PPS! ";
            }
            return exeRes;
        }


        public ExecuteResult getDHL_MotherFileShipmentIdsByShipmentTime(string shipmentTime)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            List<string> shipmentIds = new List<string>();
            dt = selectData.getDHL_MotherFileShipmentIdsByShipmentTime(shipmentTime);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    shipmentIds.Add(dt.Rows[i]["shipment_id"].ToString());
                }
                exeRes.Anything = shipmentIds;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "选取时间，无对应BBXMotherFile文件！";
            }
            return exeRes;
        }



        public ExecuteResult getShipmentInfoOfRegionByshipmentId(string shipmentId)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = selectData.getShipmentInfoOfRegionByshipmentId(shipmentId);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Anything = dt.Rows[0]["region"].ToString();
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此集货单号：" + shipmentId + " 没有查询到资料！";
            }
            return exeRes;
        }


        public ExecuteResult getTransInFileCNPLInfoStringByCartonNo(string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            string allStr = "";
            dt = selectData.getTransInFileCNPLInfoByCartonNo(cartonNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count; ++i)
                {
                    allStr += dt.Rows[0][i].ToString() + "|";
                }
                exeRes.Anything = allStr;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此箱号:" + cartonNo + " 没有对应的TransInCNPLfile资料！";
            }
            return exeRes;
        }


        public ExecuteResult getSF_File_InfoByCartonNo(string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            string allStr = "";
            dt = selectData.getSF_File_InfoByCartonNo(cartonNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count; ++i)
                {
                    allStr += dt.Rows[0][i].ToString() + "|";
                }
                exeRes.Anything = allStr.Substring(0, allStr.LastIndexOf("|"));
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "此箱号：" + cartonNo + " 对应的SF_File信息，查询不到，请联系IT-PPS!";
            }
            return exeRes;
        }

        public ExecuteResult isPrintPACShippingLabelByCartonNoAndShipInfoType(string cartonNo, string shipInfoType)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = selectData.isPrintPACShippingLabelByCartonNoAndShipInfoType(cartonNo, shipInfoType);
            string checkCount = dt.Rows[0]["checkCount"].ToString();
            if (checkCount.Equals("0"))
            {
                exeRes.Status = false;
            }
            return exeRes;
        }
        public bool isShipmentFinishByShipmentId(string shipmentId)
        {
            bool flag = false;
            DataTable dt = new DataTable();
            dt = selectData.isShipmentFinishByShipmentId(shipmentId);
            if (dt != null && dt.Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }
        public ExecuteResult printAllCarrierLableLogic(ShipmentInfo shipmentInfo, string cartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            if (shipmentInfo.TYPE.Equals("PARCEL"))
            {
                if (Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*UPS\w*") || Regex.IsMatch(shipmentInfo.CarrierName, @"\w*UPS\w*"))
                {
                    exeRes = createUpsFile(cartonNo, shipmentInfo);
                    //PrintLog("UPS CARRIER");
                    ClientUtils.WritePrintLabelLog("UPS CARRIER", cartonNo, "", LocalHelper.getMacAddr_Local());
                }
                //增加FedEX代码 Add By KyLinQiu 20190808
                else if (Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*FED\w*") || Regex.IsMatch(shipmentInfo.CarrierName, @"\w*FED\w*"))
                {
                    exeRes = createFedFile(cartonNo, shipmentInfo);
                    //PrintLog("FEDEX CARRIER");
                    ClientUtils.WritePrintLabelLog("FEDEX CARRIER", cartonNo, "", LocalHelper.getMacAddr_Local());
                }
                else if (Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*TNT\w*") || Regex.IsMatch(shipmentInfo.CarrierName, @"\w*TNT\w*"))
                {
                    exeRes = createTransInFile(cartonNo, shipmentInfo.ShipmentId);
                    //PrintLog("TNT CARRIER");
                    ClientUtils.WritePrintLabelLog("TNT CARRIER", cartonNo, "", LocalHelper.getMacAddr_Local());
                }
                else if (Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*DHL\w*") || Regex.IsMatch(shipmentInfo.CarrierName, @"\w*DHL\w*"))//file
                {
                    //switch (shipmentInfo.ServiceLevel)
                    //{
                    //    case "WPX":
                    //        exeRes = createImportWPXFile(cartonNo, shipmentInfo.ShipmentId);
                    //        break;
                    //    case "ECX":
                    //        exeRes = createAPPLEECXFile(cartonNo, shipmentInfo.ShipmentId);
                    //        break;
                    //    case "BBX":
                    //        exeRes = createBBX_BABYFile(cartonNo, shipmentInfo.ShipmentId, shipmentInfo.Region);
                    //        break;
                    //}

                    if (shipmentInfo.ServiceLevel.Equals("WPX"))
                    {
                        exeRes = createImportWPXFile(cartonNo, shipmentInfo.ShipmentId);
                    }
                    else if (shipmentInfo.ServiceLevel.Equals("ECX"))
                    {
                        exeRes = createAPPLEECXFile(cartonNo, shipmentInfo.ShipmentId);
                    }
                    else if (shipmentInfo.ServiceLevel.Equals("BBX") || shipmentInfo.ServiceLevel.Equals("EXPRESS"))//add BY benson
                    {
                        exeRes = createBBX_BABYFile(cartonNo, shipmentInfo.ShipmentId, shipmentInfo.Region);
                        if (isShipmentFinishByShipmentId(shipmentInfo.ShipmentId))
                        {
                            exeRes = createBBX_Mother_File(shipmentInfo.ShipmentId, shipmentInfo.Region);
                        }
                    }
                    else
                    {
                        exeRes.Status = false;
                        exeRes.Message = "NG_" + shipmentInfo.ServiceLevel + "未知ServiceLevel信息，请检查！";
                    }
                    //PrintLog("DHL CARRIER");
                    ClientUtils.WritePrintLabelLog("DHL CARRIER", cartonNo, "", LocalHelper.getMacAddr_Local());
                }
                else if (Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*CNPL\w*") || Regex.IsMatch(shipmentInfo.CarrierName, @"\w*CNPL\w*"))
                {
                    exeRes = createTransInFileCNPLFileByCartonNo(cartonNo, shipmentInfo.ShipmentId);
                    //PrintLog("EMS CARRIER");
                    ClientUtils.WritePrintLabelLog("EMS CARRIER", cartonNo, "", LocalHelper.getMacAddr_Local());
                }
                else if (Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*SF\w*") || Regex.IsMatch(shipmentInfo.CarrierName, @"\w*SF\w*"))
                {
                    exeRes = create_SF_file(cartonNo, shipmentInfo.ShipmentId);
                    //PrintLog("SF CARRIER");
                    ClientUtils.WritePrintLabelLog("SF CARRIER", cartonNo, "", LocalHelper.getMacAddr_Local());
                }
                else if (Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*YMT\w*") || Regex.IsMatch(shipmentInfo.CarrierName, @"\w*YMT\w*"))
                {
                    exeRes = createYMTInfoFile(cartonNo, shipmentInfo.ShipmentId);
                    //PrintLog("YMT CARRIER");
                    ClientUtils.WritePrintLabelLog("YMT CARRIER", cartonNo, "", LocalHelper.getMacAddr_Local());
                }
            }
            else
            {
                exeRes.Message = "NG_不是 PARCEL，无需打印！";
                exeRes.Status = false;
            }
            return exeRes;
        }


        public ExecuteResult PPSCheckReprintRoleBySP(string inempno, string inwc)
        {
            ExecuteResult exeRes = new ExecuteResult();
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inempno", inempno };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inwc", inwc };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds = new DataSet();
            try
            {
                ds = ClientUtils.ExecuteProc("PPSUSER.SP_PPS_CHECKREPRINTROLE", procParams);
            }
            catch (Exception e)
            {
                exeRes.Status = false;
                exeRes.Message = e.ToString();
                return exeRes;
            }
            if (ds.Tables[0].Rows[0]["errmsg"].ToString().Equals("OK"))
            {
                exeRes.Status = true;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = ds.Tables[0].Rows[0]["errmsg"].ToString();
            }
            return exeRes;
        }

        public ExecuteResult PPInsertRePrintLogBySP(string strstation, string strlabelname, string strsn, string strppsempno, string strppsempname, string strreprintempno, string strreprintempname, string strreprintlogintime, string strcomputername, string strmac, string strremark)
        {
            ExecuteResult exeRes = new ExecuteResult();
            object[][] procParams = new object[12][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "instation", strstation };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inlabelname", strlabelname };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", strsn };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inppsempno", strppsempno };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inppsempname", strppsempname };
            procParams[5] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inreprintempno", strreprintempno };
            procParams[6] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inreprintempname", strreprintempname };
            procParams[7] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inreprintlogintime", strreprintlogintime };
            procParams[8] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incomputername", strcomputername };
            procParams[9] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inmac", strmac };
            procParams[10] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inremark", strremark };
            procParams[11] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds = new DataSet();
            try
            {
                ds = ClientUtils.ExecuteProc("PPSUSER.sp_pps_inreparintlog", procParams);
            }
            catch (Exception e)
            {
                exeRes.Status = false;
                exeRes.Message = e.ToString();
                return exeRes;
            }
            if (ds.Tables[0].Rows[0]["errmsg"].ToString().Equals("OK"))
            {
                exeRes.Status = true;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = ds.Tables[0].Rows[0]["errmsg"].ToString();
            }
            return exeRes;
        }

        public ExecuteResult CheckReprintCartonBySP(string strEmpNo, string strWC, string strCartonNo)
        {
            ExecuteResult exeRes = new ExecuteResult();
            object[][] procParams = new object[4][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inempno", strEmpNo };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inwc", strWC };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incarton", strCartonNo };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };


            DataSet ds = new DataSet();
            try
            {
                ds = ClientUtils.ExecuteProc("PPSUSER.sp_pack_checkreprintcarton", procParams);
            }
            catch (Exception e)
            {
                exeRes.Status = false;
                exeRes.Message = e.ToString();
                return exeRes;
            }
            if (ds.Tables[0].Rows[0]["errmsg"].ToString().Equals("OK"))
            {
                exeRes.Status = true;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = ds.Tables[0].Rows[0]["errmsg"].ToString();
            }
            return exeRes;
        }
        public string GetUPSPrinter()
        {
            string printerName = null;
            try
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar.ToString() + "PPS.xml");
                printerName = dataSet.Tables["LC_UPSPrinter"].AsEnumerable().Select(x => x.Field<string>("Value")).FirstOrDefault();
                if (printerName == null)
                {
                    Printer printer = new Printer();
                    printer.ShowDialog();
                }
            }
            catch
            {
                Printer printer = new Printer();
                printer.ShowDialog();
            }
            return printerName ?? fMain.Printer;
        }
        public ExecuteResult getRequestShipexec(string cartonNo, string region, out ShipRequestModel shipRequest)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            shipRequest = new ShipRequestModel();
            //仿照原本获取transinfile 查询逻辑 修改个别字段后查询sql
            //dt = selectData.getUpsInfoByCartonNo(cartonNo, region);
            dt = selectData.getShipexecInfoByCartonNo(cartonNo, region);
            if (dt != null && dt.Rows.Count > 0)
            {
                PackageDefaults packageDefaults = new PackageDefaults
                {
                    Service = new Service()
                    {
                        Symbol = dt.Rows[0]["SERVICELEVELID"].ToString()
                    },
                    Consignee = new Consignee //收件人
                    {
                        Address1 = dt.Rows[0]["ST_ADDR1"].ToString(),
                        City = dt.Rows[0]["SHIPTOCITY"].ToString(),
                        Company = dt.Rows[0]["SHIPTOCOMPANY"].ToString(),
                        Contact = dt.Rows[0]["SHIPTONAME"].ToString(),
                        Country = dt.Rows[0]["SHIPCNTYCODE"].ToString(),
                        Phone = dt.Rows[0]["SHIPTOCONTTEL"].ToString(),
                        PostalCode = dt.Rows[0]["SHIPTOZIP"].ToString(),
                        //RegionDesc  如果有 = 号码, 那么取 = 号之前的
                        StateProvince = dt.Rows[0]["REGIONDESC"].ToString().Contains("=") ? dt.Rows[0]["REGIONDESC"].ToString().Split('=')[0] : dt.Rows[0]["REGIONDESC"].ToString()
                    },
                    #region 待取消--测试时使用写死测试数据  UNKNOW
                    //comment by wenxing 2021-3-20
                    //ImporterOfRecord = new ImporterOfRecord
                    //{
                    //    Account = "X3332V",
                    //    Address1 = "1 Infinite Loop 3-IE",
                    //    City = "Cupertino",
                    //    Company = "Apple Computer Inc",
                    //    Contact = "c/o Customs Clearance",
                    //    Country = "US",
                    //    PostalCode = "95014",
                    //    StateProvince = "CA",
                    //    TaxId = "94-240411000"
                    //},
                    //ReturnAddress = new ReturnAddress
                    //{
                    //    Address1 = "558 ALD BLVD",
                    //    City = "Mt. Juliet",
                    //    Company = "A.I.",
                    //    Country = "US",
                    //    PostalCode = "37122",
                    //    StateProvince = "TN"
                    //},
                    //ThirdPartyBilling = "1",
                    //ThirdPartyBillingAddress = new ThirdPartyBillingAddress
                    //{
                    //    Account = "100Y00",
                    //    Address1 = "5505 W Parmer Lane, Bldg 4",
                    //    Address2 = "MS:186-LPM",
                    //    City = "Austin",
                    //    Company = "APPLE",
                    //    Contact = "C/O Freight Payment",
                    //    Country = "US",
                    //    PostalCode = "78727-4021",
                    //    StateProvince = "TX"
                    //},
                    #endregion
                    Shipdate = new Shipdate // SHIP TIME 
                    {
                        Year = dt.Rows[0]["SHIPDATE"].ToString().Split('/')[0],
                        Month = dt.Rows[0]["SHIPDATE"].ToString().Split('/')[1],
                        Day = dt.Rows[0]["SHIPDATE"].ToString().Split('/')[2]
                    },
                    //Shipper = "AAPL_ICT_KS_TEST",// dt.Rows[0]["SHIPER_CORP_NAME"].ToString(), 报错临时写定值
                    Shipper = "AAPL_" + dt.Rows[0]["parcelaccountnumber"].ToString(),
                    ShipperReference = dt.Rows[0]["DELIVERY_NO"].ToString(),//DN
                    ConsigneeReference = dt.Rows[0]["CUSTSONO"].ToString()
                };
                CommodityContents contents = new CommodityContents
                {
                    Description = "ELECTRONIC PRODUCTS",
                    OriginCountry = dt.Rows[0]["OriginCountry"].ToString(),//COO
                    ProductCode = dt.Rows[0]["AC_PN"].ToString(),//no 35 in transin-file
                    Quantity = dt.Rows[0]["PERCARTONQTY"].ToString(),
                    QuantityUnitMeasure = "PCS",
                    UnitValue = new UnitValue
                    {
                        Amount = dt.Rows[0]["SHIPMENT_TOTAL_VALUE"].ToString(),//总价钱
                        Currency = "USD"
                    },
                    UnitWeight = new UnitWeight
                    {
                        Amount = dt.Rows[0]["WEIGHT_UNIT"].ToString(),
                        Units = "KGS"
                    }
                };
                Packages packages = new Packages
                {
                    CarrierInstructions = dt.Rows[0]["Delivery_Instruction"].ToString(),
                    Description = "ACCESSORY",
                    CommodityContents = new CommodityContents[] { contents },
                    MiscReference1 = dt.Rows[0]["CUSTPONO"].ToString(),
                    MiscReference2 = dt.Rows[0]["WEBORDERNO"].ToString(),
                    MiscReference3 = dt.Rows[0]["CUSTDELITEM"].ToString(),
                    MiscReference4 = dt.Rows[0]["AC_PN"].ToString(),
                    MiscReference5 = dt.Rows[0]["CARTON_NO"].ToString(),
                    MiscReference6 = dt.Rows[0]["SSCC"].ToString(),
                    MiscReference7 = dt.Rows[0]["SHIPMENT_TRACKING"].ToString(),
                    MiscReference8 = dt.Rows[0]["SHIPMENTREACKING"].ToString(),
                    MiscReference10 = dt.Rows[0]["TOTAL_WEIGHT"].ToString() + " KG",//固定是KG
                    MiscReference11 = dt.Rows[0]["CARTON_SEQUNECE"].ToString(),
                    MiscReference12 = dt.Rows[0]["CARTON_COUNT"].ToString(),
                    MiscReference14 = dt.Rows[0]["SAWB"].ToString(),
                    MiscReference15 = dt.Rows[0]["HAWB"].ToString(),
                    TrackingNumber = dt.Rows[0]["TRACKING_NO"].ToString(),
                    Weight = new Weight
                    {
                        Amount = dt.Rows[0]["DN_TOTAL_WEIGHT"].ToString(),//单箱重量 no 27 in transinfile
                        //Amount = dt.Rows[0]["TOTAL_WEIGHT"].ToString(),//total weight of DN
                        Units = "KG"
                    },
                    WorldEaseCode = dt.Rows[0]["SHIPMENTREACKING"].ToString(),
                    WorldEaseFlag = "1"
                };
                shipRequest = new ShipRequestModel
                {
                    ClientAccessCredentials = new ClientAccessCredentials(),
                    UserContext = new UserContext(),
                    ShipmentRequest = new ShipmentRequest
                    {
                        PackageDefaults = packageDefaults,
                        Packages = new Packages[] { packages }
                    }
                };
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "未能查询到Ups信息，请联系IT-PPS!";
            }
            return exeRes;
        }

    }
}
