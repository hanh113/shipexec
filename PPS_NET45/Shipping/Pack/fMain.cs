using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Packingparcel.Core;
using Packingparcel.Entitys;
using Packingparcel.Utils;
using LibHelper;
using CRReport.CRfrom;
using System.Threading;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using ClientUtilsDll;
using System.Text.RegularExpressions;
//create by wangdunyang 20180928
namespace Packingparcel
{
    public partial class fMain : Form
    {
        private Controller controller;
        private bool isMix = false;
        private bool isCartonType = false;
        private CreateBarcodeForShippingLabel createBarcodes;
        private ShipmentInfo shipmentInfo;
        private double saveDays = 90;
        public static string Printer = "";

        private string strLocalMACADDRESS = string.Empty;
        private bool isFirst = true;
        public fMain()
        {
            InitializeComponent();
            controller = new Controller();
            createBarcodes = new CreateBarcodeForShippingLabel();
            shipmentInfo = new ShipmentInfo();
        }
        private void fMain_Load(object sender, EventArgs e)
        {
            //show出当前程式的版本
            this.txtTitel.Text = "PACK" + "(Ver:" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + ")";
            this.Text = "PACK" + "(Ver:" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + ")";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //删除TransFile日志
            DeleteTransFileLog();
            strLocalMACADDRESS = LibHelper.LocalHelper.getMacAddr_Local();
        }

        private void DeleteTransFileLog()
        {
            try
            {
                string logFilePath = Path.Combine(Application.StartupPath, "deleteTransFilelog.txt");
                string dayLog = "";
                if (!File.Exists(logFilePath))
                {
                    FileStream fs = File.Create(logFilePath);
                }
                else
                {
                    dayLog = File.ReadAllText(logFilePath).Trim();
                }
                string nowLog = DateTime.Now.ToString("yyyyMMdd");
                if (nowLog == dayLog)
                {
                    return;
                }
                string filePath = Path.Combine(Application.StartupPath, "ALL_CARRIER");
                DeleteFile(filePath);
                File.WriteAllText(logFilePath, nowLog);
            }
            catch { }
        }

        private void DeleteFile(string filePath)
        {
            if (!Directory.Exists(filePath))
            { Directory.CreateDirectory(filePath); }
            string[] sonFiles = Directory.GetFiles(filePath);
            foreach (string sf in sonFiles)
            {
                if (DateTime.Compare(File.GetLastWriteTime(sf).AddDays(saveDays), DateTime.Now) <= 0)
                {
                    File.Delete(sf);
                }
            }
            string[] sonDirecs = Directory.GetDirectories(filePath);
            foreach (string s in sonDirecs)
            {
                DeleteFile(s);
            }
            string[] fileLeft = Directory.GetFiles(filePath);
            string[] sonLeft = Directory.GetDirectories(filePath);
            if ((sonLeft.Length == 0) && (fileLeft.Length == 0))
            {
                Directory.Delete(filePath);
            }
        }

        private void fMain_Paint(object sender, PaintEventArgs e)
        {
            this.txtShipmentId.Focus();
            this.txtShipmentId.SelectAll();
        }

        private void fMain_SizeChanged(object sender, EventArgs e)
        {
            // autoSizeFormClass.controlAutoSize(this);
        }

        private void txtShipmentId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string shipMentId = this.txtShipmentId.Text.ToUpper().Trim();
                if (string.IsNullOrEmpty(shipMentId))
                {
                    Show_Message("集货单号未输入，请检查！", 0);
                    MediasHelper.PlaySoundAsyncByNg();
                    return;
                }
                shipmentInfo = null;//将shipmentInfo重置
                this.labFinish.Visible = false;
                ExecuteResult exeReS = new ExecuteResult();
                DataTable dt = new DataTable();
                // check shipmentId 状态
                exeReS = controller.checkShipmentIdStatusByShipmentId(shipMentId);
                if (exeReS.Status)
                {
                    exeReS = controller.getShipMentInfoByshipmentId(shipMentId);
                    if (exeReS.Status)
                    {
                        dt = (DataTable)exeReS.Anything;
                        this.lblCarrierName.Text = dt.Rows[0]["Carrier"].ToString();
                        this.lblPOE.Text = dt.Rows[0]["POE"].ToString();
                        this.lblShipmentType.Text = dt.Rows[0]["shipment_type"].ToString();
                        this.lblType.Text = dt.Rows[0]["type"].ToString();
                        //this.lblSecurity.Text = dt.Rows[0]["security"].ToString();
                        //获取全局 shipmentInfo信息
                        shipmentInfo = new ShipmentInfo
                        {
                            Region = dt.Rows[0]["region"].ToString(),
                            CarrierName = dt.Rows[0]["Carrier"].ToString(),
                            ShipmentType = dt.Rows[0]["shipment_type"].ToString(),
                            ShipmentId = shipMentId,
                            TYPE = dt.Rows[0]["type"].ToString(),
                            CarrierCode = dt.Rows[0]["carrier_code"].ToString(),
                            ServiceLevel = dt.Rows[0]["service_level"].ToString()
                        };
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            this.dgvShipmentInfo.Rows.Add();
                            this.dgvShipmentInfo.Rows[i].Cells[0].Value = dt.Rows[i]["shipmentId"].ToString();
                            this.dgvShipmentInfo.Rows[i].Cells[1].Value = dt.Rows[i]["Carrier"].ToString();
                            this.dgvShipmentInfo.Rows[i].Cells[2].Value = dt.Rows[i]["POE"].ToString();
                            this.dgvShipmentInfo.Rows[i].Cells[3].Value = dt.Rows[i]["region"].ToString();
                            this.dgvShipmentInfo.Rows[i].Cells[4].Value = dt.Rows[i]["palletNo"].ToString();
                            this.dgvShipmentInfo.Rows[i].Cells[5].Value = dt.Rows[i]["palletType"].ToString();
                            this.dgvShipmentInfo.Rows[i].Cells[6].Value = dt.Rows[i]["ictPn_"].ToString();
                            this.dgvShipmentInfo.Rows[i].Cells[7].Value = dt.Rows[i]["cartonQty"].ToString();
                            this.dgvShipmentInfo.Rows[i].Cells[8].Value = dt.Rows[i]["alreadyPickCartonQty"].ToString();
                            this.dgvShipmentInfo.Rows[i].Cells[9].Value = dt.Rows[i]["alreadyPackCartonQty"].ToString();
                            this.dgvShipmentInfo.Rows[i].Cells[10].Value = dt.Rows[i]["status"].ToString();
                            if (dt.Rows[i]["status"].ToString().Equals("已完成"))
                            {
                                this.dgvShipmentInfo.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                            }
                            if (int.Parse(dt.Rows[i]["alreadyPackCartonQty"].ToString()) > 0 && int.Parse(dt.Rows[i]["alreadyPackCartonQty"].ToString()) < int.Parse(dt.Rows[i]["cartonQty"].ToString()))
                            {
                                this.dgvShipmentInfo.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                            }
                        }
                        exeReS = controller.CheckSIDLineControl(shipMentId, strLocalMACADDRESS);
                        if (!exeReS.Status)
                        {
                            this.txtShipmentId.Focus();
                            this.txtShipmentId.SelectAll();
                            MediasHelper.PlaySoundAsyncByNg();
                            Show_Message(exeReS.Message, 0);
                            return;
                        }

                        Show_Message("集货箱号正确，请刷栈板号！", 1);
                        this.txtShipmentId.Enabled = false;
                        this.txtPickPalletNo.Enabled = true;
                        this.txtPickPalletNo.Focus();
                        this.txtPickPalletNo.SelectAll();
                        MediasHelper.PlaySoundAsyncByOk();
                        //增加提示声音
                        if (shipmentInfo.TYPE == "PARCEL")
                        {
                            if (Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*UPS\w*") || Regex.IsMatch(shipmentInfo.CarrierName, @"\w*UPS\w*"))
                            {
                                this.labFinish.Visible = true;
                                MediasHelper.PlaySoundAsyncByUPS();
                            }
                            else if (Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*FED\w*") || Regex.IsMatch(shipmentInfo.CarrierName, @"\w*FED\w*"))
                            {
                                this.labFinish.Visible = true;
                                MediasHelper.PlaySoundAsyncByFEDEX();
                            }
                            else if (Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*DHL\w*") || Regex.IsMatch(shipmentInfo.CarrierName, @"\w*DHL\w*"))
                            {
                                if (shipmentInfo.ServiceLevel.Equals("BBX") || shipmentInfo.ServiceLevel.Equals("EXPRESS") || shipmentInfo.ServiceLevel.Equals("WPX"))
                                {
                                    this.labFinish.Visible = true;
                                    MediasHelper.PlaySoundAsyncByDHL();
                                }
                            }
                            else if (Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*SF\w*") || Regex.IsMatch(shipmentInfo.CarrierName, @"\w*SF\w*"))
                            {
                                if (controller.GetSFShiptoCNTYCODE(shipMentId))
                                {
                                    this.labFinish.Visible = true;
                                    MediasHelper.PlaySoundAsyncBySF();
                                }
                            }
                        }
                    }
                    else
                    {
                        this.txtShipmentId.Focus();
                        this.txtShipmentId.SelectAll();
                        MediasHelper.PlaySoundAsyncByNg();
                        Show_Message(exeReS.Message, 0);
                        return;
                    }
                }
                else
                {
                    this.txtShipmentId.Focus();
                    this.txtShipmentId.SelectAll();
                    MediasHelper.PlaySoundAsyncByNg();
                    Show_Message(exeReS.Message, 0);
                    return;
                }
            }
        }

        private void txtPickPalletNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.isMix = false;//状态重置 
                isCartonType = false;

                string shipMentId = this.txtShipmentId.Text.ToUpper().Trim();
                if (string.IsNullOrEmpty(shipMentId))
                {
                    Show_Message("集货单号未输入，请检查！", 0);
                    MediasHelper.PlaySoundAsyncByNg();
                    return;
                }

                string pickPalletNo = this.txtPickPalletNo.Text.ToUpper().Trim();
                if (string.IsNullOrEmpty(pickPalletNo))
                {
                    Show_Message("Pick栈板号信息未输入，请检查！", 0);
                    MediasHelper.PlaySoundAsyncByNg();
                    return;
                }
                if (pickPalletNo.Equals("UNDO"))
                {
                    resetAllStatus();
                    Show_Message("状态已经重置！", 1);
                    return;
                }
                ExecuteResult exeRes = new ExecuteResult();
                DataTable dt = new DataTable();
                #region 20190117  kuilin  增加显示QAhold 数量  BY  pickPalletNo
                exeRes = controller.checkQHQtyByPickPalletNo(pickPalletNo);
                if (!exeRes.Status)
                {
                    MessageBox.Show(exeRes.Message);
                }
                #endregion
                exeRes = controller.getPalletPickInfoByPickPalletNo(pickPalletNo, shipMentId);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    string isMixOrNoMix = dt.Rows[0]["palletType"].ToString();
                    string snType = dt.Rows[0]["sn_type"].ToString();
                    this.lblSecurity.Text = dt.Rows[0]["SECURITY"].ToString();
                    if (isMixOrNoMix.Equals("MIX"))
                    {
                        isMix = true;
                    }
                    if (snType.Equals("C"))
                    {
                        isCartonType = true;
                    }
                    this.lblisMix.Text = isMixOrNoMix;
                    this.lblNeedPackBoxQty.Text = dt.Rows[0]["pack_carton"].ToString() + @"/" + dt.Rows[0]["carton_qty"].ToString();
                    this.lblRemark.Text = dt.Rows[0]["REMARK"].ToString() + dt.Rows[0]["SERVICE_LEVEL"].ToString();
                    this.txtPickPalletNo.Enabled = false;
                    this.txtCartonno.Enabled = true;
                    this.txtCartonno.Focus();
                    this.txtCartonno.SelectAll();
                    this.btnStart.Enabled = false;
                    MediasHelper.PlaySoundAsyncByOk();
                    Show_Message("Pick栈板号输入正确，请开始作业！", 1);
                    //增加提示声音
                    if (shipmentInfo.TYPE == "PARCEL")
                    {
                        if (Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*UPS\w*") || Regex.IsMatch(shipmentInfo.CarrierName, @"\w*UPS\w*"))
                        {
                            MediasHelper.PlaySoundAsyncByUPS();
                        }
                        else if (Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*FED\w*") || Regex.IsMatch(shipmentInfo.CarrierName, @"\w*FED\w*"))
                        {
                            MediasHelper.PlaySoundAsyncByFEDEX();
                        }
                        else if (Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*DHL\w*") || Regex.IsMatch(shipmentInfo.CarrierName, @"\w*DHL\w*"))
                        {
                            if (shipmentInfo.ServiceLevel.Equals("BBX") || shipmentInfo.ServiceLevel.Equals("EXPRESS") || shipmentInfo.ServiceLevel.Equals("WPX"))
                            {
                                MediasHelper.PlaySoundAsyncByDHL();
                            }
                        }
                        else if (Regex.IsMatch(shipmentInfo.CarrierCode, @"\w*SF\w*") || Regex.IsMatch(shipmentInfo.CarrierName, @"\w*SF\w*"))
                        {
                            if (controller.GetSFShiptoCNTYCODE(shipMentId))
                            {
                                MediasHelper.PlaySoundAsyncBySF();
                            }
                        }
                    }
                }
                else
                {
                    Show_Message(exeRes.Message, 0);
                    this.txtPickPalletNo.Focus();
                    this.txtPickPalletNo.SelectAll();
                    MediasHelper.PlaySoundAsyncByNg();
                    return;
                }
            }
        }

        private void txtCartonno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DateTime dt1 = DateTime.Now;
                string inputData = this.txtCartonno.Text.ToUpper().Trim();
                this.txtCartonno.Enabled = false;
                if (this.lstCreateTXT.Items.Count > 0)
                {
                    this.lstCreateTXT.Items.Clear();
                }
                this.btnStart.Enabled = true;
                if (string.IsNullOrEmpty(inputData))
                {
                    Show_Message("未输入信息，请检查！", 0);
                    MediasHelper.PlaySoundAsyncByNg();
                    return;
                }
                if (inputData.Equals("UNDO"))
                {
                    resetAllStatus();
                    Show_Message("状态已经重置！", 1);
                    return;
                }
                if (inputData.Length == 20 && inputData.Substring(0, 2).Equals("00"))
                {
                    inputData = inputData.Substring(2, 18);
                }
                if (inputData.StartsWith("3S"))
                {
                    inputData = inputData.Substring(2);
                }
                if (inputData.StartsWith("S"))
                {
                    inputData = inputData.Substring(1);
                }
                string pickPalletNo = this.txtPickPalletNo.Text.ToUpper().Trim();
                string shipmentId = this.txtShipmentId.Text.ToUpper().Trim();
                ExecuteResult exeRes = new ExecuteResult();

                if (isFirst)
                {
                    exeRes = controller.CheckSIDLineControl(shipmentId, strLocalMACADDRESS);
                    if (!exeRes.Status)
                    {
                        this.txtShipmentId.Focus();
                        this.txtShipmentId.SelectAll();
                        MediasHelper.PlaySoundAsyncByNg();
                        Show_Message(exeRes.Message, 0);
                        return;
                    }
                    isFirst = false;
                }

                DataTable dt = new DataTable();
                List<string> cartonNoList = new List<string>();
                string deliveryNo = "";
                string lineItem = "";
                string inputType = "";
                //mix 不允许输入pick栈板号
                if (isMix)
                {
                    exeRes = controller.judgeInputDataType(inputData);
                    if (exeRes.Status)
                    {
                        inputType = (string)exeRes.Anything;
                        if (inputType.Equals("PICKPALLETNO"))
                        {
                            Show_Message("Mix 不可以输入Pick栈板号，请输入箱号或者流水号！", 0);
                            this.txtCartonno.Enabled = true;
                            this.txtCartonno.Focus();
                            this.txtCartonno.SelectAll();
                            MediasHelper.PlaySoundAsyncByNg();
                            return;
                        }
                    }
                }
                if (isCartonType)
                {
                    exeRes = controller.judgeInputDataType(inputData);
                    if (exeRes.Status)
                    {
                        inputType = (string)exeRes.Anything;
                        if (!inputType.Equals("CARTONNO"))
                        {
                            Show_Message("PICK站别是按箱号作业，Pack请依旧按箱号作业！", 0);
                            this.txtCartonno.Enabled = true;
                            this.txtCartonno.Focus();
                            this.txtCartonno.SelectAll();
                            MediasHelper.PlaySoundAsyncByNg();
                            return;
                        }
                    }
                }
                else
                {
                    if (ChkInputCartonNo.Checked)
                    {
                        exeRes = controller.judgeInputDataType(inputData);
                        if (exeRes.Status)
                        {
                            inputType = (string)exeRes.Anything;
                            if (!inputType.Equals("CARTONNO"))
                            {
                                Show_Message("默认选择箱号输入，若取消，请将箱号钩掉！", 0);
                                this.txtCartonno.Enabled = true;
                                this.txtCartonno.Focus();
                                this.txtCartonno.SelectAll();
                                MediasHelper.PlaySoundAsyncByNg();
                                return;
                            }
                        }
                    }
                }

                #region   检查输入机器
                exeRes = controller.checkSnStatus(inputData, pickPalletNo);
                if (exeRes.Status)
                {
                    #region  将输入的信息转为箱号
                    exeRes = controller.getAllCartonNo(inputData);
                    cartonNoList = (List<string>)exeRes.Anything;
                    #endregion

                    #region 获取DN信息
                    exeRes = controller.queryPalletOrderInfoByCartonNo(cartonNoList[0], pickPalletNo.Substring(2), false);
                    if (exeRes.Status)
                    {
                        this.dgvOrderInfo.Rows.Clear();
                        dt = (DataTable)exeRes.Anything;
                        deliveryNo = dt.Rows[0]["delivery_no"].ToString();
                        lineItem = dt.Rows[0]["line_item"].ToString();
                        #region   show cartonNostatus                  
                        exeRes = controller.getCartonStatusByInputData(inputData);
                        if (exeRes.Status)
                        {
                            dt = (DataTable)exeRes.Anything;
                            this.dgvShowBoxStatus.Rows.Clear();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                this.dgvShowBoxStatus.Rows.Add();
                                this.dgvShowBoxStatus.Rows[i].Cells[0].Value = dt.Rows[i]["cartonNo"].ToString();
                                this.dgvShowBoxStatus.Rows[i].Cells[1].Value = dt.Rows[i]["partNo"].ToString();
                                this.dgvShowBoxStatus.Rows[i].Cells[2].Value = dt.Rows[i]["cartonQty"].ToString();
                            }
                        }
                        else
                        {
                            Show_Message(exeRes.Message, 0);
                            this.txtCartonno.Enabled = true;
                            MediasHelper.PlaySoundAsyncByNg();
                            return;
                        }
                        #endregion

                        #endregion

                        #region  过站   
                        foreach (string cartonNo in cartonNoList)
                        {
                            //if ups, deliveryNo set = empty
                            ExecuteResult exeRes_passStationPro = controller.packingPassStationPro(shipmentId, cartonNo, pickPalletNo, deliveryNo, LocalHelper.getMacAddr_Local());
                            if (!exeRes_passStationPro.Status)
                            {
                                Show_Message("此箱：" + cartonNo + exeRes_passStationPro.Message, 0);
                                this.txtCartonno.Enabled = true;
                                this.txtCartonno.Focus();
                                this.txtCartonno.SelectAll();
                                MediasHelper.PlaySoundAsyncByNg();
                                return;
                            }
                        }
                        #endregion
                        #region  水晶报表打印
                        exeRes = controller.printCrystalReportAllLogic(cartonNoList[0], shipmentInfo, deliveryNo, lineItem, false, pickPalletNo.Substring(2));
                        if (exeRes.Status)
                        {
                            this.lstCreateTXT.Items.Add(exeRes.Message);
                        }
                        else
                        {
                            this.lstCreateTXT.Items.Add(exeRes.Message);
                        }

                        #endregion
                        #region  label打印
                        //DateTime startPrint = DateTime.Now;
                        exeRes = controller.printAllLabelLogic(isMix, cartonNoList[0], shipmentInfo);
                        if (!exeRes.Status)
                        {
                            this.lstCreateTXT.Items.Add(exeRes.Message);
                        }
                        else
                        {
                            this.lstCreateTXT.Items.Add(exeRes.Message);
                        }
                        //TimeSpan  tsResult =  DateTime.Now - startPrint;
                        //this.responsetime_LB.Text = tsResult.Seconds.ToString()+"-";
                        #endregion
                        #region  打印DeliveryNote label
                        exeRes = controller.printDeliveryNoteLabel(cartonNoList[0], deliveryNo, lineItem, shipmentInfo, false);
                        if (exeRes.Status)
                        {
                            this.lstCreateTXT.Items.Add(exeRes.Message);
                        }
                        else
                        {
                            this.lstCreateTXT.Items.Add(exeRes.Message);
                        }
                        #endregion
                        #region 打印GS1-label    nomix and cs1Flag为‘Y’   
                        if (!isMix)
                        {
                            if (controller.isPrintForNoMix(cartonNoList[0]))
                            {
                                exeRes = controller.printGS1LabelLogic(shipmentInfo, cartonNoList[0]);
                                if (exeRes.Status)
                                {
                                    this.lstCreateTXT.Items.Add(exeRes.Message);
                                }
                                else
                                {
                                    this.lstCreateTXT.Items.Add(exeRes.Message);
                                }
                            }
                        }
                        #endregion
                        #region   生成 File 
                        exeRes = controller.printAllCarrierLableLogic(shipmentInfo, cartonNoList[0]);
                        if (exeRes.Status)
                        {
                            this.lstCreateTXT.Items.Add(exeRes.Message);
                        }
                        else
                        {
                            this.lstCreateTXT.Items.Add(exeRes.Message);
                        }
                        #endregion
                        #region afterPassStation orderInfo数据
                        exeRes = controller.queryPalletOrderInfoByCartonNo(cartonNoList[0], pickPalletNo.Substring(2), true);
                        if (exeRes.Status)
                        {
                            this.dgvOrderInfo.Rows.Clear();
                            dt = (DataTable)exeRes.Anything;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                this.dgvOrderInfo.Rows.Add();
                                this.dgvOrderInfo.Rows[i].Cells[0].Value = dt.Rows[i]["delivery_no"].ToString();
                                this.dgvOrderInfo.Rows[i].Cells[1].Value = dt.Rows[i]["ictpn"].ToString();
                                this.dgvOrderInfo.Rows[i].Cells[2].Value = dt.Rows[i]["totalcartonQty"].ToString();
                                this.dgvOrderInfo.Rows[i].Cells[3].Value = dt.Rows[i]["totalQty"].ToString();
                                this.dgvOrderInfo.Rows[i].Cells[4].Value = dt.Rows[i]["assign_carton"].ToString();
                                this.dgvOrderInfo.Rows[i].Cells[5].Value = dt.Rows[i]["assign_qty"].ToString();
                                this.dgvOrderInfo.Rows[i].Cells[6].Value = dt.Rows[i]["pack_qty"].ToString();
                                this.dgvOrderInfo.Rows[i].Cells[7].Value = dt.Rows[i]["pack_carton"].ToString();
                                this.dgvOrderInfo.Rows[i].Cells[8].Value = dt.Rows[i]["status_"].ToString();
                            }
                        }
                        #endregion
                        #region show dn_Lb   已经pack 和 需要pack
                        exeRes = controller.queryOrderInfoByDn(deliveryNo, shipmentId);
                        if (exeRes.Status)
                        {
                            dt = (DataTable)exeRes.Anything;
                            this.txtDeliveryNo.Text = deliveryNo;
                            this.lblDNSumQty.Text = dt.Rows[0]["packQty"].ToString() + @"/" + dt.Rows[0]["cartonQty"].ToString();
                        }
                        else
                        {
                            Show_Message(exeRes.Message, 0);
                            this.txtCartonno.Enabled = true;
                            MediasHelper.PlaySoundAsyncByNg();
                            return;
                        }
                        //  test_thread
                        exeRes = controller.getPalletPickInfoByPickPalletNo(pickPalletNo, shipmentId);
                        if (exeRes.Status)
                        {
                            dt = (DataTable)exeRes.Anything;
                            this.lblNeedPackBoxQty.Text = dt.Rows[0]["pack_carton"].ToString() + @"/" + dt.Rows[0]["carton_qty"].ToString();
                        }
                        #endregion
                        #region show shipmentInfo DGV
                        exeRes = controller.getShipMentInfoByshipmentId(shipmentId);
                        if (exeRes.Status)
                        {
                            dt = (DataTable)exeRes.Anything;
                            this.dgvShipmentInfo.Rows.Clear();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                this.dgvShipmentInfo.Rows.Add();
                                this.dgvShipmentInfo.Rows[i].Cells[0].Value = dt.Rows[i]["shipmentId"].ToString();
                                this.dgvShipmentInfo.Rows[i].Cells[1].Value = dt.Rows[i]["Carrier"].ToString();
                                this.dgvShipmentInfo.Rows[i].Cells[2].Value = dt.Rows[i]["POE"].ToString();
                                this.dgvShipmentInfo.Rows[i].Cells[3].Value = dt.Rows[i]["region"].ToString();
                                this.dgvShipmentInfo.Rows[i].Cells[4].Value = dt.Rows[i]["palletNo"].ToString();
                                this.dgvShipmentInfo.Rows[i].Cells[5].Value = dt.Rows[i]["palletType"].ToString();
                                this.dgvShipmentInfo.Rows[i].Cells[6].Value = dt.Rows[i]["ictPn_"].ToString();
                                this.dgvShipmentInfo.Rows[i].Cells[7].Value = dt.Rows[i]["cartonQty"].ToString();
                                this.dgvShipmentInfo.Rows[i].Cells[8].Value = dt.Rows[i]["alreadyPickCartonQty"].ToString();
                                this.dgvShipmentInfo.Rows[i].Cells[9].Value = dt.Rows[i]["alreadyPackCartonQty"].ToString();
                                this.dgvShipmentInfo.Rows[i].Cells[10].Value = dt.Rows[i]["status"].ToString();
                                if (dt.Rows[i]["status"].ToString().Equals("已完成"))
                                {
                                    this.dgvShipmentInfo.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                                }
                                if (int.Parse(dt.Rows[i]["alreadyPackCartonQty"].ToString()) > 0 && int.Parse(dt.Rows[i]["alreadyPackCartonQty"].ToString()) < int.Parse(dt.Rows[i]["cartonQty"].ToString()))
                                {
                                    this.dgvShipmentInfo.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                                }
                            }
                        }
                        #endregion
                        DateTime dt2 = DateTime.Now;
                        TimeSpan ts = dt2 - dt1;
                        this.responsetime_LB.Text = ts.Seconds.ToString();//响应时间计算
                        #region  跳转控制
                        if (controller.isFinishWorkByShipMentId(shipmentId))
                        {
                            resetAllStatus();
                            // Show_Message("此集货单号已做完,请刷下一个！", 0);
                            return;
                        }
                        if (controller.isFinishWorkByPickPalletNo(pickPalletNo))
                        {
                            this.txtCartonno.Enabled = false;
                            this.txtPickPalletNo.Enabled = true;
                            this.txtPickPalletNo.Focus();
                            this.txtPickPalletNo.SelectAll();
                            returnToPickPalletNo();
                            //Show_Message("Ok,此Pick栈板已做完，请刷下一个Pick栈板！", 1);
                            return;
                        }
                        this.txtCartonno.Enabled = true;
                        this.txtCartonno.Focus();
                        this.txtCartonno.SelectAll();
                        MediasHelper.PlaySoundAsyncByOk();
                        Show_Message("Ok,请继续刷下一台！", 1);
                        #endregion
                    }
                    else
                    {
                        Show_Message(exeRes.Message, 0);
                        this.txtCartonno.Enabled = true;
                        this.txtCartonno.Focus();
                        this.txtCartonno.SelectAll();
                        MediasHelper.PlaySoundAsyncByNg();
                        return;
                    }
                }
                else
                {
                    Show_Message(exeRes.Message, 0);
                    this.txtCartonno.Enabled = true;
                    this.txtCartonno.Focus();
                    this.txtCartonno.SelectAll();
                    MediasHelper.PlaySoundAsyncByNg();
                    return;
                }
                #endregion

            }
        }
        private void returnToPickPalletNo()
        {
            this.txtCartonno.Clear();
            this.txtDeliveryNo.Clear();
            this.lblDNSumQty.Text = @"0/0";
            this.dgvOrderInfo.Rows.Clear();
            this.dgvShowBoxStatus.Rows.Clear();
            this.lblNeedPackBoxQty.Text = @"0/0";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.txtCartonno.Enabled = true;
            this.txtCartonno.Focus();
            this.txtCartonno.SelectAll();

        }
        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.txtCartonno.Enabled = false;
            this.txtShipmentId.Enabled = true;
            this.txtShipmentId.Focus();
            this.txtShipmentId.SelectAll();
        }

        private void btnReprint_Click(object sender, EventArgs e)
        {
            fCheck fc = new fCheck();
            if (fc.ShowDialog() == DialogResult.OK)
            {
                Reprint rp = new Reprint();
                rp.ShowDialog();
            }
            else
            {
                Show_Message("账号或密码错误，请检查！", 0);
                return;
            }
        }

        private void btnResetStatus_Click(object sender, EventArgs e)
        {
            resetAllStatus();
        }
        private void resetAllStatus()//重置所有的状态
        {
            //public
            this.isMix = false;
            shipmentInfo = null;
            //txt
            this.txtShipmentId.Enabled = true;
            this.txtShipmentId.Focus();
            this.txtShipmentId.SelectAll();
            this.txtPickPalletNo.Clear();
            this.txtPickPalletNo.Enabled = false;
            this.txtCartonno.Clear();
            this.txtCartonno.Enabled = false;
            this.txtDeliveryNo.Text = "";
            //  this.createTxt_TB.Text = "";

            //lbl
            this.lblCarrierName.Text = "____";
            this.lblPOE.Text = "____";
            this.lblisMix.Text = "-----";
            this.lblType.Text = "PARCEL";
            this.lblShipmentType.Text = "DIRECT";
            this.lblRemark.Text = "REMARK";
            this.lblDNSumQty.Text = "0/0";
            this.lblNeedPackBoxQty.Text = "0/0";
            this.lblSecurity.Text = "";
            //dgv
            this.dgvShipmentInfo.Rows.Clear();
            this.dgvOrderInfo.Rows.Clear();
            this.dgvShowBoxStatus.Rows.Clear();

        }

        private void btnCreateDHLFile_Click(object sender, EventArgs e)
        {
            DHL_Mother dhlMother = new DHL_Mother();
            dhlMother.Show();
        }

        private void lstCreateTXT_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                e.DrawBackground();
                Brush mybsh = Brushes.Black;
                // 判断是什么类型的标签
                if (lstCreateTXT.Items[e.Index].ToString().StartsWith("OK_"))
                {
                    mybsh = Brushes.Green;
                }
                else if (lstCreateTXT.Items[e.Index].ToString().StartsWith("NG_"))
                {
                    mybsh = Brushes.Red;
                }
                // 焦点框
                e.DrawFocusRectangle();
                //文本 
                e.Graphics.DrawString(lstCreateTXT.Items[e.Index].ToString(), e.Font, mybsh, e.Bounds, StringFormat.GenericDefault);
            }
        }
        private void Show_Message(string msg, int type)
        {
            lblMessage.Text = msg.TP();
            switch (type)
            {
                case 0: //error
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.BackColor = Color.Yellow;
                    break;
                case 1:
                    lblMessage.ForeColor = Color.Blue;
                    lblMessage.BackColor = Color.White;
                    break;
                default:
                    lblMessage.ForeColor = Color.Black;
                    lblMessage.BackColor = Color.White;
                    break;
            }
        }

        private void btnPrinter_Click(object sender, EventArgs e)
        {
            Printer printer = new Printer();
            printer.Show();
        }
    }
}
