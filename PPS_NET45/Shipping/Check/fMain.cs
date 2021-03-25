using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Check.Core;
using Check.Entitys;
using System.IO;
using Check.Utils;
using ClientUtilsDll;
using System.Text.RegularExpressions;
using LibHelper;
using Oracle.ManagedDataAccess.Client;


/*
 add by wangdunyang  20181018
     
     */
namespace Check
{
    public partial class fMain : Form
    {
        public string strEmptyCarton = string.Empty;
        private Controller controller;
        private bool isMix = false;//区分 mix and No_mix
        private bool isDSPacShipLable = false;//区分ds_pac是否打印label
        private bool isDSPacDeliveryNote = false;//区分 DS 是否打印deliveryNote
        private PrintLabel printLabel;
        private ShipmentInfo shipmentInfo;
        //private AutoSizeFormClass autoSizeForm;

        private bool isPopLock = true;
        private string strUnLockPWD = "UnLock123";

        public fMain()
        {
            InitializeComponent();
            controller = new Controller();
            printLabel = new PrintLabel();
            shipmentInfo = new ShipmentInfo();
            //autoSizeForm = new AutoSizeFormClass();
        }
        private void fMain_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.lblTitel.Text = "CHECK" + "(Ver:" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + ")";
        }
        private void fMain_SizeChanged(object sender, EventArgs e)
        {
            //autoSizeForm.controlAutoSize(this);
        }
        private void txtShipmentId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string shipmentId = this.txtShipmentId.Text.ToUpper().Trim();
                shipmentInfo = null;
                this.labFinish.Visible = false;
                this.lblMixPallet.Visible = false;
                if (string.IsNullOrEmpty(shipmentId))
                {
                    Show_Message("没有输入集货单号，请输入！", 0);
                    return;
                }
                ExecuteResult exeRes = new ExecuteResult();
                DataTable dt = new DataTable();
                exeRes = controller.getShipMentInfoByshipmentId(shipmentId);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    this.lblCarrierName.Text = dt.Rows[0]["carrierName"].ToString();
                    this.lblPOE.Text = dt.Rows[0]["POE"].ToString();
                    this.lblShipmentType.Text = dt.Rows[0]["shipmentType"].ToString();
                    this.lblType.Text = dt.Rows[0]["type_"].ToString();

                    //this.lblSecurity.Text = dt.Rows[0]["security"].ToString();
                    //this.type_LB.Text = dt.Rows[0]["region"].ToString(); 
                    //获取全局 shipmentInfo信息
                    shipmentInfo = new ShipmentInfo
                    {
                        TYPE = dt.Rows[0]["type_"].ToString(),
                        ShipmentType = dt.Rows[0]["shipmentType"].ToString(),
                        Region = dt.Rows[0]["region"].ToString(),
                        CarrierCode = dt.Rows[0]["carrier_code"].ToString(),
                        CarrierName = dt.Rows[0]["carrierName"].ToString(),
                        Security = dt.Rows[0]["security"].ToString(),
                        ServiceLevel = dt.Rows[0]["service_level"].ToString()
                    };
                    Show_Message(exeRes.Message, 1);
                    this.txtShipmentId.Enabled = false;
                    this.txtPickPalletNo.Enabled = true;
                    this.txtPickPalletNo.Focus();
                    this.txtPickPalletNo.SelectAll();
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
                            if (controller.GetSFShiptoCNTYCODE(shipmentId))
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
                    Show_Message(exeRes.Message, 0);
                    return;
                }
            }
        }
        private void resetAllFormStatus()
        {
            //txt
            this.txtShipmentId.Enabled = true;
            this.txtShipmentId.Clear();
            this.txtShipmentId.Focus();
            this.txtShipmentId.SelectAll();
            this.txtPickPalletNo.Clear();
            this.txtTrackingNo.Clear();
            this.txtCartonNo.Clear();
            this.txtCartonNo.Enabled = false;
            this.txtGS1SSCC.Clear();
            this.txtGS1SSCC.Enabled = false;
            this.txtDNPO.Clear();
            this.txtTrackingNo.Enabled = false;
            this.txtPickPalletNo.Enabled = false;
            this.txtDNDN.Clear();
            //lbl
            this.lblCarrierName.Text = "____";
            this.lblPOE.Text = "____";
            this.lblShipmentType.Text = "DIRECT";
            this.lblPalletSize.Text = "";
            this.lblType.Text = "PARCEL";
            this.lblisMix.Text = "-----";
            this.lblNeedCheckBoxQty.Text = "0/0";
            this.lblSecurity.Text = "";
            this.lblSDN.Visible = false;
            this.lblSpecialDN.Text = "0/0";
            this.lblSpecialDN.Visible = false;
            this.labFinish.Visible = false;
            this.lblMixPallet.Visible = false;
            //dgv
            this.dgvPalletInfo.Rows.Clear();
            this.dgvResult.Rows.Clear();
            //public
            this.isMix = false;
            this.isDSPacDeliveryNote = false;
            this.isDSPacShipLable = false;
        }
        private void txtPickPalletNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                string pickPalletNo = this.txtPickPalletNo.Text.ToUpper().Trim();
                if (string.IsNullOrEmpty(pickPalletNo))
                {
                    Show_Message("没有输入Pick栈板号，请输入！", 0);
                    return;
                }
                isMix = false;
                isDSPacShipLable = false;
                string shipmentId = this.txtShipmentId.Text.ToUpper().Trim();
                ExecuteResult exeRes = new ExecuteResult();
                DataTable dt = new DataTable();
                exeRes = controller.getPickPalletInfoByPickPalletNoAndShipmentId(pickPalletNo, shipmentId);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    this.dgvPalletInfo.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.dgvPalletInfo.Rows.Add();
                        this.dgvPalletInfo.Rows[i].Cells[0].Value = dt.Rows[i]["palletNo"].ToString();
                        this.dgvPalletInfo.Rows[i].Cells[1].Value = dt.Rows[i]["ictPn_"].ToString();
                        this.dgvPalletInfo.Rows[i].Cells[2].Value = dt.Rows[i]["cartonQty"].ToString();
                        this.dgvPalletInfo.Rows[i].Cells[3].Value = dt.Rows[i]["totalQty"].ToString();
                        this.dgvPalletInfo.Rows[i].Cells[4].Value = dt.Rows[i]["packQty"].ToString();
                        this.dgvPalletInfo.Rows[i].Cells[5].Value = dt.Rows[i]["packCarton"].ToString();
                        this.dgvPalletInfo.Rows[i].Cells[6].Value = dt.Rows[i]["alreadyCheckCartonQty"].ToString();
                        this.dgvPalletInfo.Rows[i].Cells[7].Value = dt.Rows[i]["checkStatus"].ToString();
                        strEmptyCarton = dt.Rows[0]["empty_carton"].ToString().Trim();
                        if (dt.Rows[i]["checkStatus"].ToString().Equals("已完成"))
                        {
                            if (controller.GetPACGS1003MultiLine(shipmentId, pickPalletNo.Substring(2)))
                            {
                                lblMixPallet.Visible = true;
                            }
                            Show_Message("此Pick栈板已Check完毕，请刷下一个Pick栈板！", 1);
                            this.txtPickPalletNo.Focus();
                            this.txtPickPalletNo.SelectAll();
                            return;
                        }
                        if (int.Parse(dt.Rows[i]["alreadyCheckCartonQty"].ToString()) == int.Parse(dt.Rows[i]["cartonQty"].ToString()))
                        {
                            this.dgvPalletInfo.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                        }
                        if (int.Parse(dt.Rows[i]["alreadyCheckCartonQty"].ToString()) > 0 && int.Parse(dt.Rows[i]["alreadyCheckCartonQty"].ToString()) < int.Parse(dt.Rows[i]["cartonQty"].ToString()))
                        {
                            this.dgvPalletInfo.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        //totalCartonQty += int.Parse(dt.Rows[i]["cartonQty"].ToString());
                    }
                    if (controller.GetPACGS1003MultiLine(shipmentId, pickPalletNo.Substring(2)))
                    {
                        lblMixPallet.Visible = true;
                    }
                    this.lblisMix.Text = dt.Rows[0]["palletType"].ToString();
                    this.lblPalletSize.Text = dt.Rows[0]["remark"].ToString();
                    this.lblSecurity.Text = dt.Rows[0]["SECURITY"].ToString().Trim();
                    if (this.lblisMix.Text.Trim().Equals("MIX"))
                    {
                        isMix = true;
                    }
                    exeRes = controller.totalAllCartonQtyByPickPalletNo(pickPalletNo);
                    this.lblNeedCheckBoxQty.Text = (string)exeRes.Anything;
                    Show_Message(exeRes.Message, 1);
                    /*
                     针对PAC（亚太地区）可能会不打印shippingLabel
                     */
                    this.txtPickPalletNo.Enabled = false;
                    if (shipmentInfo.Region.Equals("PAC") && shipmentInfo.ShipmentType.Equals("DS"))
                    {
                        if (shipmentInfo.TYPE.Equals("BULK"))
                        {
                            if (!controller.isExistShippingLabelForPAC(pickPalletNo, shipmentInfo.TYPE))
                            {
                                isDSPacShipLable = true;
                                this.txtCartonNo.Enabled = true;
                                this.txtCartonNo.Focus();
                                this.txtCartonNo.SelectAll();
                                return;
                            }
                        }
                    }
                    this.txtTrackingNo.Enabled = true;
                    this.txtTrackingNo.Focus();
                    this.txtTrackingNo.SelectAll();

                }
                else
                {
                    this.txtPickPalletNo.Focus();
                    this.txtPickPalletNo.SelectAll();
                    Show_Message(exeRes.Message, 0);
                    return;
                }
            }
        }
        private void txtTrackingNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DateTime t_startTime = DateTime.Now;
                string trackingNo = this.txtTrackingNo.Text.ToUpper().Trim();
                string shipmentId = this.txtShipmentId.Text.ToUpper().Trim();
                if (this.txtTrackingNo.Text.ToUpper().Trim().Length == 20 && this.txtTrackingNo.Text.ToUpper().Trim().StartsWith("00"))
                {
                    trackingNo = trackingNo.Substring(2);
                }
                //增加YMT前后A判断
                if (this.txtTrackingNo.Text.ToUpper().Trim().Length == 14 && this.txtTrackingNo.Text.ToUpper().Trim().StartsWith("A") && this.txtTrackingNo.Text.ToUpper().Trim().EndsWith("A"))
                {
                    trackingNo = trackingNo.Substring(1, trackingNo.Length - 2);
                    this.txtTrackingNo.Text = trackingNo;
                }
                if (string.IsNullOrEmpty(trackingNo))
                {
                    Show_Message("未输入TrackingNo，请输入！", 0);
                    return;
                }
                /*
                 增加检查sscc是否和pick栈板link关系
                 */
                string pickPalletNo = this.txtPickPalletNo.Text.ToUpper().Trim();
                ExecuteResult exeRes = new ExecuteResult();
                exeRes = controller.check_isLinkPickPalletNo_Pro(shipmentId, trackingNo, pickPalletNo);
                //exeRes = controller.checkIsLinkPallet(shipmentInfo,trackingNo,pickPalletNo);
                if (exeRes.Status)
                {
                    this.txtCartonNo.Enabled = true;
                    this.txtCartonNo.Focus();
                    this.txtCartonNo.SelectAll();
                    Show_Message("TrackingNo输入OK，请刷箱号(CartonNo)!", 1);
                    this.txtTrackingNo.Enabled = false;
                }
                else
                {
                    Show_Message(exeRes.Message, 0);
                    if (this.isPopLock)
                    {
                        ShowLockForm(this.strUnLockPWD);
                        txtTrackingNo.Text = "";
                        txtCartonNo.Text = "";
                        txtDNDN.Text = "";
                        txtDNPO.Text = "";
                    }
                    this.txtTrackingNo.Focus();
                    this.txtTrackingNo.SelectAll();
                }
                DateTime endTime = DateTime.Now;
                TimeSpan diff = endTime - t_startTime;
                checkPrintLogAdd("SSCC输入框用时", "", t_startTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), diff.TotalSeconds.ToString());
            }

        }
        private void txtCartonNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DateTime snC_startTime = DateTime.Now;
                string cartonNo = this.txtCartonNo.Text.ToUpper().Trim();
                if (string.IsNullOrEmpty(cartonNo))
                {
                    Show_Message("SNCarton未输入，请输入！", 0);
                    return;
                }
                if (cartonNo.Length == 20 && cartonNo.Substring(0, 2).Equals("00"))
                {
                    cartonNo = cartonNo.Substring(2, 18);
                }
                if (cartonNo.StartsWith("3S"))
                {
                    cartonNo = cartonNo.Substring(2);
                }
                if (cartonNo.StartsWith("S"))
                {
                    cartonNo = cartonNo.Substring(1);
                }
                string sscc = this.txtTrackingNo.Text.ToUpper().Trim();
                string shipmentId = this.txtShipmentId.Text.Trim().ToUpper();
                string pickPalletNo = this.txtPickPalletNo.Text.Trim().ToUpper();
                if (sscc.ToUpper().Trim().Length == 20 && sscc.ToUpper().Trim().StartsWith("00"))
                {
                    sscc = sscc.Substring(2);
                }

                lblSDN.Visible = false;
                lblSpecialDN.Text = "0/0";
                lblSpecialDN.Visible = false;


                List<string> labelContentList = new List<string>();
                ExecuteResult exeRes = new ExecuteResult();
                DataTable dt = new DataTable();
                //增加检查  此sscc是否link cartonNo        
                exeRes = controller.check_isLinkCartonNo_Pro(shipmentId, sscc, cartonNo, isDSPacShipLable ? "1" : "0");
                //exeRes = controller.checkIsLinkCarton(shipmentInfo,sscc,cartonNo,isDSPacShipLable);
                if (!exeRes.Status)
                {
                    Show_Message(exeRes.Message, 0);
                    if (this.isPopLock)
                    {
                        ShowLockForm(this.strUnLockPWD);
                        txtTrackingNo.Text = "";
                        txtGS1SSCC.Text = "";
                        txtCartonNo.Text = "";
                        txtDNDN.Text = "";
                        txtDNPO.Text = "";
                    }
                    if (isDSPacShipLable)
                    {
                        this.txtPickPalletNo.Enabled = true;
                        this.txtPickPalletNo.Focus();
                        this.txtPickPalletNo.SelectAll();
                    }
                    else
                    {
                        this.txtTrackingNo.Enabled = true;
                        this.txtTrackingNo.Focus();
                        this.txtTrackingNo.SelectAll();
                        this.txtGS1SSCC.Enabled = false;
                        this.txtCartonNo.Enabled = false;
                    }
                    return;
                }
                if (controller.isJumpPackingList(cartonNo, isMix, shipmentInfo))//检查是否需要刷packingList
                {
                    this.txtDNPO.Enabled = true;
                    this.txtDNPO.Focus();
                    this.txtDNPO.SelectAll();
                    this.txtCartonNo.Enabled = false;
                    Show_Message("CartonNo输入完毕，请刷 packingList 条码！", 1);
                    existsFile();
                    return;
                }
                else
                {
                    //检查是否check deliveryNote
                    if (controller.isJumpDeliveryNoteTB(cartonNo, shipmentInfo, isMix))
                    {
                        this.txtDNDN.Enabled = true;
                        this.txtDNDN.Focus();
                        this.txtDNDN.SelectAll();
                        this.txtCartonNo.Enabled = false;
                        this.isDSPacDeliveryNote = true;
                        existsFile();
                        return;
                    }
                    //检查是否有GS1 
                    //if (controller.bGS1Flag(pickPalletNo) && !sscc.Equals(strGS1))
                    if (controller.bGS1Flag(pickPalletNo))
                    {
                        this.txtGS1SSCC.Enabled = true;
                        this.txtGS1SSCC.Focus();
                        this.txtGS1SSCC.SelectAll();
                        Show_Message("CartonNo输入OK，请刷GS1 Label !", 1);
                        this.txtTrackingNo.Enabled = false;
                        this.txtCartonNo.Enabled = false;
                        return;
                    }

                    exeRes = controller.checkPassStationPro(shipmentId, pickPalletNo, sscc, cartonNo, "", isMix, LibHelper.LocalHelper.getMacAddr_Local(), isDSPacShipLable ? "1" : "0", isDSPacDeliveryNote ? "1" : "0", "");//过站
                    if (!exeRes.Status)
                    {
                        Show_Message(exeRes.Message, 0);
                        if (this.isPopLock)
                        {
                            ShowLockForm(this.strUnLockPWD);
                            txtTrackingNo.Text = "";
                            txtGS1SSCC.Text = "";
                            txtCartonNo.Text = "";
                            txtDNDN.Text = "";
                            txtDNPO.Text = "";
                        }
                        if (isDSPacShipLable)
                        {
                            this.txtCartonNo.Enabled = true;
                            this.txtCartonNo.Focus();
                            this.txtCartonNo.SelectAll();
                        }
                        else
                        {
                            this.txtCartonNo.Enabled = false;
                            this.txtTrackingNo.Enabled = true;
                            this.txtTrackingNo.Focus();
                            this.txtTrackingNo.SelectAll();
                        }
                        return;
                    }
                    updatePalletInfo(pickPalletNo, shipmentId);


                    #region  showResult
                    exeRes = controller.getShowResultDGV(cartonNo, isMix);
                    this.dgvResult.Rows.Clear();
                    if (exeRes.Status)
                    {
                        dt = (DataTable)exeRes.Anything;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            this.dgvResult.Rows.Add();
                            this.dgvResult.Rows[i].Cells[0].Value = dt.Rows[i]["pallet_no"].ToString();
                            this.dgvResult.Rows[i].Cells[1].Value = dt.Rows[i]["sscc"].ToString();
                            this.dgvResult.Rows[i].Cells[2].Value = dt.Rows[i]["carton_no"].ToString();
                            this.dgvResult.Rows[i].Cells[3].Value = dt.Rows[i]["delivery_no"].ToString();
                            this.dgvResult.Rows[i].Cells[4].Value = dt.Rows[i]["checkResult"].ToString();
                        }
                    }

                    string strSDNstatus = string.Empty;
                    string strSDNmsg = string.Empty;
                    controller.checkSpecialDNbySP(cartonNo, out strSDNstatus, out strSDNmsg);
                    if (strSDNmsg.Equals("OK"))
                    {
                        MessageBox.Show("此箱号属于特殊DN，请检查特殊标签".TL());
                        lblSDN.Visible = true;
                        lblSpecialDN.Text = strSDNstatus;
                        lblSpecialDN.Visible = true;
                    }

                    #endregion
                    #region   label 打印

                    if (controller.isPrintLabel(pickPalletNo.Substring(2)))
                    {
                        labelContentList.Add(shipmentId + @"|" + pickPalletNo.Substring(2) + @"|" + this.lblSecurity.Text.Trim() + @"|" + strEmptyCarton + @"|");
                        DateTime dtStart = DateTime.Now;
                        exeRes = printLabel.printLableForModifyVersion("SH_PalletIDLabel", labelContentList, 1);
                        if (!exeRes.Status)
                        {
                            Show_Message(exeRes.Message, 0);
                        }
                        else
                        {
                            ClientUtils.WritePrintLabelLog("Check Pallet Label", "", pickPalletNo, LocalHelper.getMacAddr_Local());
                        }
                        DateTime endTime = DateTime.Now;
                        TimeSpan ts = endTime - dtStart;
                        this.lblTotalTime.Text = ts.TotalSeconds.ToString() + "-s";
                        checkPrintLogAdd("箱号输入框用时", "箱号输入框用时-打印方法", dtStart.ToString("yyyy-MM-dd HH:mm:ss.fff"), endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), ts.TotalSeconds.ToString());
                    }

                    #endregion
                    txtTrackingNo.Text = "";
                    txtCartonNo.Text = "";
                    txtGS1SSCC.Text = "";
                    txtDNDN.Text = "";
                    txtDNPO.Text = "";
                    #region 检查此Pick栈板是否作业完毕
                    if (controller.isCheckshipmentIdFinishWorkByCartonNo(cartonNo))
                    {
                        Show_Message("此集货单号：" + shipmentId + " 已作业完毕,请刷下一个集货单号！", 1);
                        this.txtShipmentId.Enabled = true;
                        this.txtShipmentId.Focus();
                        this.txtShipmentId.SelectAll();
                        this.txtCartonNo.Enabled = false;
                        resetAllFormStatus();
                        return;
                    }
                    if (controller.isFinishWorkByCartonNo(cartonNo))
                    {
                        Show_Message("此Pick栈板：" + pickPalletNo + " 已作业完毕,请刷下一个Pick栈板！", 1);
                        this.txtPickPalletNo.Enabled = true;
                        this.txtPickPalletNo.Focus();
                        this.txtPickPalletNo.SelectAll();
                        this.txtCartonNo.Enabled = false;
                        this.isDSPacShipLable = false;
                        this.isDSPacDeliveryNote = false;
                        return;
                    }
                    Show_Message("此箱：" + cartonNo + " 已作业完毕，请刷下一箱！", 1);

                    if (isDSPacShipLable)
                    {
                        this.txtCartonNo.Enabled = true;
                        this.txtCartonNo.Focus();
                        this.txtCartonNo.SelectAll();
                    }
                    else
                    {
                        this.txtCartonNo.Enabled = false;
                        this.txtTrackingNo.Enabled = true;
                        this.txtTrackingNo.Focus();
                        this.txtTrackingNo.SelectAll();
                    }
                    //this.txtCartonNo.Enabled = false;
                    //    this.txtTrackingNo.Enabled = true;
                    //    this.txtTrackingNo.Focus();
                    //    this.txtTrackingNo.SelectAll();
                    #endregion
                    DateTime snC_endTime = DateTime.Now;
                    TimeSpan diff = snC_endTime - snC_startTime;
                    checkPrintLogAdd("箱号输入框用时", "总时间", snC_startTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), snC_endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), diff.TotalSeconds.ToString());
                }
            }
        }
        private void checkPrintLogAdd(string functionName, string fileContent, string startTime, string endTime, string diffTime)
        {
            string startPath = Application.StartupPath;
            string logPath = startPath + @"\CheckPrintLog";
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            string txtFilePath = logPath + @"\" + DateTime.Now.ToString("yyyyMMdd") + @".txt";
            FileStream fileStream = new FileStream(txtFilePath, FileMode.Append, FileAccess.Write);
            string allFileContent = "----------------" + functionName + "----------------" + Environment.NewLine + fileContent + Environment.NewLine + "开始时间：" + startTime + Environment.NewLine + "间隔(" + diffTime + ")" + Environment.NewLine + "结束时间：" + endTime + Environment.NewLine + "----------------" + functionName + "----------------";
            StreamWriter writer = null;
            try
            {
                using (writer = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    writer.WriteLine(allFileContent);
                    writer.Flush();
                    writer.Close();
                }
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
        private void updatePalletInfo(string pickPalletNo, string shipmentId)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            //int totalCartonQty = 0;
            exeRes = controller.getPickPalletInfoByPickPalletNoAndShipmentId(pickPalletNo, shipmentId);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                this.dgvPalletInfo.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.dgvPalletInfo.Rows.Add();
                    this.dgvPalletInfo.Rows[i].Cells[0].Value = dt.Rows[i]["palletNo"].ToString();
                    this.dgvPalletInfo.Rows[i].Cells[1].Value = dt.Rows[i]["ictPn_"].ToString();
                    this.dgvPalletInfo.Rows[i].Cells[2].Value = dt.Rows[i]["cartonQty"].ToString();
                    this.dgvPalletInfo.Rows[i].Cells[3].Value = dt.Rows[i]["totalQty"].ToString();
                    this.dgvPalletInfo.Rows[i].Cells[4].Value = dt.Rows[i]["packQty"].ToString();
                    this.dgvPalletInfo.Rows[i].Cells[5].Value = dt.Rows[i]["packCarton"].ToString();
                    this.dgvPalletInfo.Rows[i].Cells[6].Value = dt.Rows[i]["alreadyCheckCartonQty"].ToString();
                    this.dgvPalletInfo.Rows[i].Cells[7].Value = dt.Rows[i]["checkStatus"].ToString();
                    if (int.Parse(dt.Rows[i]["alreadyCheckCartonQty"].ToString()) == int.Parse(dt.Rows[i]["cartonQty"].ToString()))
                    {
                        this.dgvPalletInfo.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    }
                    if (int.Parse(dt.Rows[i]["alreadyCheckCartonQty"].ToString()) > 0 && int.Parse(dt.Rows[i]["alreadyCheckCartonQty"].ToString()) < int.Parse(dt.Rows[i]["cartonQty"].ToString()))
                    {
                        this.dgvPalletInfo.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    //totalCartonQty += int.Parse(dt.Rows[i]["cartonQty"].ToString());
                }
                exeRes = controller.totalAllCartonQtyByPickPalletNo(pickPalletNo);
                this.lblNeedCheckBoxQty.Text = (string)exeRes.Anything;
            }
        }
        private void txtDNPO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DateTime dnpo_startTime = DateTime.Now;
                string deliveryNo = this.txtDNPO.Text.ToUpper().Trim();
                if (string.IsNullOrEmpty(deliveryNo))
                {
                    Show_Message("DN/PO信息未输入，请输入！", 0);
                    return;
                }
                string sscc = this.txtTrackingNo.Text.ToUpper().Trim();
                if (this.txtTrackingNo.Text.ToUpper().Trim().StartsWith("00") && this.txtTrackingNo.Text.ToUpper().Trim().Length == 20)
                {
                    sscc = sscc.Substring(2);
                }
                string cartonNo = this.txtCartonNo.Text.ToUpper().Trim();
                if (cartonNo.Length == 20 && cartonNo.Substring(0, 2).Equals("00"))
                {
                    cartonNo = cartonNo.Substring(2, 18);
                }
                if (cartonNo.StartsWith("3S"))
                {
                    cartonNo = cartonNo.Substring(2);
                }
                if (cartonNo.StartsWith("S"))
                {
                    cartonNo = cartonNo.Substring(1);
                }
                string shipmentId = this.txtShipmentId.Text.ToUpper().Trim();
                string pickPalletNo = this.txtPickPalletNo.Text.ToUpper().Trim();
                List<string> labelContentList = new List<string>();
                ExecuteResult exeRes = new ExecuteResult();
                DataTable dt = new DataTable();
                if (controller.isJumpDeliveryNoteTB(cartonNo, shipmentInfo, isMix))
                {
                    this.txtDNDN.Enabled = true;
                    this.txtDNDN.Focus();
                    this.txtDNDN.SelectAll();
                    this.txtDNPO.Enabled = false;
                    this.isDSPacDeliveryNote = true;
                    existsFile();
                    return;
                }
                //检查是否有GS1
                if (controller.bGS1Flag(pickPalletNo))
                {
                    this.txtGS1SSCC.Enabled = true;
                    this.txtGS1SSCC.Focus();
                    this.txtGS1SSCC.SelectAll();
                    Show_Message("DN/PO输入OK，请刷GS1 Label !", 1);
                    this.txtDNPO.Enabled = false;
                    return;
                }
                exeRes = controller.checkPassStationPro(shipmentId, pickPalletNo, sscc, cartonNo, deliveryNo, isMix, LibHelper.LocalHelper.getMacAddr_Local(), isDSPacShipLable ? "1" : "0", isDSPacDeliveryNote ? "1" : "0", "");
                if (!exeRes.Status)
                {
                    Show_Message(exeRes.Message, 0);
                    if (this.isPopLock)
                    {
                        ShowLockForm(this.strUnLockPWD);
                        txtTrackingNo.Text = "";
                        txtCartonNo.Text = "";
                        txtDNDN.Text = "";
                        txtDNPO.Text = "";
                    }
                    this.txtDNPO.Clear();
                    this.txtDNPO.Enabled = false;
                    this.txtTrackingNo.Enabled = true;
                    this.txtTrackingNo.Focus();
                    this.txtTrackingNo.SelectAll();
                    return;
                }
                updatePalletInfo(pickPalletNo, shipmentId);
                #region  showResult
                exeRes = controller.getShowResultDGV(cartonNo, isMix);
                this.dgvResult.Rows.Clear();
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.dgvResult.Rows.Add();
                        this.dgvResult.Rows[i].Cells[0].Value = dt.Rows[i]["pallet_no"].ToString();
                        this.dgvResult.Rows[i].Cells[1].Value = dt.Rows[i]["sscc"].ToString();
                        this.dgvResult.Rows[i].Cells[2].Value = dt.Rows[i]["carton_no"].ToString();
                        this.dgvResult.Rows[i].Cells[3].Value = dt.Rows[i]["delivery_no"].ToString();
                        this.dgvResult.Rows[i].Cells[4].Value = dt.Rows[i]["checkResult"].ToString();
                    }
                }
                string strSDNstatus = string.Empty;
                string strSDNmsg = string.Empty;
                controller.checkSpecialDNbySP(cartonNo, out strSDNstatus, out strSDNmsg);
                if (strSDNmsg.Equals("OK"))
                {
                    MessageBox.Show("此箱号属于特殊DN，请检查特殊标签".TL());
                    lblSDN.Visible = true;
                    lblSpecialDN.Text = strSDNstatus;
                    lblSpecialDN.Visible = true;
                }
                #endregion
                #region  打印label

                if (controller.isPrintLabel(pickPalletNo.Substring(2)))
                {
                    labelContentList.Add(shipmentId + @"|" + pickPalletNo.Substring(2) + @"|" + this.lblSecurity.Text.Trim() + @"|" + strEmptyCarton + @"|");
                    DateTime dtStart = DateTime.Now;
                    exeRes = printLabel.printLableForModifyVersion("SH_PalletIDLabel", labelContentList, 1);
                    if (!exeRes.Status)
                    {
                        Show_Message(exeRes.Message, 0);
                    }
                    else
                    {
                        ClientUtils.WritePrintLabelLog("Check Pallet Label", "", pickPalletNo, LocalHelper.getMacAddr_Local());
                    }
                    DateTime endTime = DateTime.Now;
                    TimeSpan ts = endTime - dtStart;
                    this.lblTotalTime.Text = ts.TotalSeconds.ToString() + "-s";
                    checkPrintLogAdd("DN输入框用时", "DN输入框用时-打印方法", dtStart.ToString("yyyy-MM-dd HH:mm:ss.fff"), endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), ts.TotalSeconds.ToString());
                }

                #endregion
                txtTrackingNo.Text = "";
                txtCartonNo.Text = "";
                txtDNDN.Text = "";
                txtDNPO.Text = "";
                #region 检查此Pick栈板是否作业完毕
                if (controller.isCheckshipmentIdFinishWorkByCartonNo(cartonNo))
                {
                    Show_Message("此集货单号：" + shipmentId + " 已作业完毕,请刷下一个集货单号！", 1);
                    this.txtShipmentId.Enabled = true;
                    this.txtShipmentId.Focus();
                    this.txtShipmentId.SelectAll();
                    this.txtDNPO.Enabled = false;
                    this.isDSPacShipLable = false;
                    this.isDSPacDeliveryNote = false;
                    return;
                }
                if (controller.isFinishWorkByCartonNo(cartonNo))
                {
                    Show_Message("此Pick栈板：" + pickPalletNo + " 已作业完毕,请刷下一个Pick栈板！", 1);
                    this.txtPickPalletNo.Enabled = true;
                    this.txtPickPalletNo.Focus();
                    this.txtPickPalletNo.SelectAll();
                    this.txtDNPO.Enabled = false;
                    this.isDSPacShipLable = false;
                    this.isDSPacDeliveryNote = false;
                    return;
                }
                Show_Message("此箱：" + cartonNo + " 已作业完毕，请刷下一箱！", 1);
                this.txtTrackingNo.Enabled = true;
                this.txtTrackingNo.Focus();
                this.txtTrackingNo.SelectAll();
                this.txtDNPO.Enabled = false;
                #endregion
                DateTime dnpo_endTime = DateTime.Now;
                TimeSpan dnpoDiff = dnpo_endTime - dnpo_startTime;
                checkPrintLogAdd("DN输入框用时", "DN输入框用时-", dnpo_startTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), dnpo_endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), dnpoDiff.TotalSeconds.ToString());
            }
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
                Show_Message("账号密码错误，请检查！", 0);
                return;
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            resetAllFormStatus();
            Show_Message("已重置窗口！", 1);
        }
        private void txtDNDN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DateTime deNote_startTime = DateTime.Now;
                string deliveryNote = this.txtDNDN.Text.ToUpper().Trim();
                if (string.IsNullOrEmpty(deliveryNote))
                {
                    Show_Message("DN/PO信息未输入，请输入！", 0);
                    return;
                }
                string sscc = this.txtTrackingNo.Text.ToUpper().Trim();
                if (this.txtTrackingNo.Text.ToUpper().Trim().StartsWith("00") && this.txtTrackingNo.Text.ToUpper().Trim().Length == 20)
                {
                    sscc = sscc.Substring(2);
                }
                string cartonNo = this.txtCartonNo.Text.ToUpper().Trim();
                if (cartonNo.Length == 20 && cartonNo.Substring(0, 2).Equals("00"))
                {
                    cartonNo = cartonNo.Substring(2, 18);
                }
                if (cartonNo.StartsWith("3S"))
                {
                    cartonNo = cartonNo.Substring(2);
                }
                if (cartonNo.StartsWith("S"))
                {
                    cartonNo = cartonNo.Substring(1);
                }
                string shipmentId = this.txtShipmentId.Text.ToUpper().Trim();
                string pickPalletNo = this.txtPickPalletNo.Text.ToUpper().Trim();
                string deliveryNo = this.txtDNPO.Text.ToUpper().Trim();
                //检查是否有GS1
                if (controller.bGS1Flag(pickPalletNo))
                {
                    this.txtGS1SSCC.Enabled = true;
                    this.txtGS1SSCC.Focus();
                    this.txtGS1SSCC.SelectAll();
                    Show_Message("DN/PO输入OK，请刷GS1 Label !", 1);
                    this.txtDNDN.Enabled = false;
                    return;
                }
                List<string> labelContentList = new List<string>();
                ExecuteResult exeRes = new ExecuteResult();
                DataTable dt = new DataTable();
                exeRes = controller.checkPassStationPro(shipmentId, pickPalletNo, sscc, cartonNo, deliveryNo, isMix, LibHelper.LocalHelper.getMacAddr_Local(), isDSPacShipLable ? "1" : "0", isDSPacDeliveryNote ? "1" : "0", deliveryNote);
                if (!exeRes.Status)
                {
                    Show_Message(exeRes.Message, 0);
                    if (this.isPopLock)
                    {
                        ShowLockForm(this.strUnLockPWD);
                        txtTrackingNo.Text = "";
                        txtCartonNo.Text = "";
                        txtDNDN.Text = "";
                        txtDNPO.Text = "";
                    }

                    this.txtTrackingNo.Enabled = true;
                    this.txtTrackingNo.Focus();
                    this.txtTrackingNo.SelectAll();
                    this.txtDNDN.Clear();
                    this.isDSPacDeliveryNote = false;
                    this.txtDNDN.Enabled = false;
                    return;
                }
                updatePalletInfo(pickPalletNo, shipmentId);
                #region  showResult
                exeRes = controller.getShowResultDGV(cartonNo, isMix);
                this.dgvResult.Rows.Clear();
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.dgvResult.Rows.Add();
                        this.dgvResult.Rows[i].Cells[0].Value = dt.Rows[i]["pallet_no"].ToString();
                        this.dgvResult.Rows[i].Cells[1].Value = dt.Rows[i]["sscc"].ToString();
                        this.dgvResult.Rows[i].Cells[2].Value = dt.Rows[i]["carton_no"].ToString();
                        this.dgvResult.Rows[i].Cells[3].Value = dt.Rows[i]["delivery_no"].ToString();
                        this.dgvResult.Rows[i].Cells[4].Value = dt.Rows[i]["checkResult"].ToString();
                    }
                }
                string strSDNstatus = string.Empty;
                string strSDNmsg = string.Empty;
                controller.checkSpecialDNbySP(cartonNo, out strSDNstatus, out strSDNmsg);
                if (strSDNmsg.Equals("OK"))
                {
                    MessageBox.Show("此箱号属于特殊DN，请检查特殊标签".TL());
                    lblSDN.Visible = true;
                    lblSpecialDN.Text = strSDNstatus;
                    lblSpecialDN.Visible = true;
                }
                #endregion
                #region  打印label

                if (controller.isPrintLabel(pickPalletNo.Substring(2)))
                {
                    labelContentList.Add(shipmentId + @"|" + pickPalletNo.Substring(2) + @"|" + this.lblSecurity.Text.Trim() + @"|" + strEmptyCarton + @"|");
                    DateTime dtStart = DateTime.Now;
                    exeRes = printLabel.printLableForModifyVersion("SH_PalletIDLabel", labelContentList, 1);
                    if (!exeRes.Status)
                    {
                        Show_Message(exeRes.Message, 0);
                    }
                    else
                    {
                        ClientUtils.WritePrintLabelLog("Check Pallet Label", "", pickPalletNo, LocalHelper.getMacAddr_Local());
                    }
                    DateTime endTime = DateTime.Now;
                    TimeSpan ts = endTime - dtStart;
                    this.lblTotalTime.Text = ts.TotalSeconds.ToString() + "-s";
                    checkPrintLogAdd("DeliveryNote输入框用时", "DeliveryNote输入框-打印方法", dtStart.ToString("yyyy-MM-dd HH:mm:ss.fff"), endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), ts.TotalSeconds.ToString());
                }

                #endregion
                txtTrackingNo.Text = "";
                txtCartonNo.Text = "";
                txtDNDN.Text = "";
                txtDNPO.Text = "";
                #region 检查此Pick栈板是否作业完毕
                if (controller.isCheckshipmentIdFinishWorkByCartonNo(cartonNo))
                {
                    Show_Message("此集货单号：" + shipmentId + " 已作业完毕,请刷下一个集货单号！", 1);
                    this.txtShipmentId.Enabled = true;
                    this.txtShipmentId.Focus();
                    this.txtShipmentId.SelectAll();
                    this.txtDNDN.Clear();
                    this.txtDNDN.Enabled = false;
                    this.isDSPacShipLable = false;
                    this.isDSPacDeliveryNote = false;
                    return;
                }
                if (controller.isFinishWorkByCartonNo(cartonNo))
                {
                    Show_Message("此Pick栈板：" + pickPalletNo + " 已作业完毕,请刷下一个Pick栈板！", 1);
                    this.txtPickPalletNo.Enabled = true;
                    this.txtPickPalletNo.Focus();
                    this.txtPickPalletNo.SelectAll();
                    this.txtDNDN.Clear();
                    this.txtDNDN.Enabled = false;
                    this.isDSPacShipLable = false;
                    this.isDSPacDeliveryNote = false;
                    return;
                }
                Show_Message("此箱：" + cartonNo + " 已作业完毕，请刷下一箱！", 1);
                this.txtTrackingNo.Enabled = true;
                this.txtTrackingNo.Focus();
                this.txtTrackingNo.SelectAll();
                this.txtDNDN.Clear();
                this.txtDNDN.Enabled = false;
                this.isDSPacDeliveryNote = false;
                #endregion
                DateTime deNote_endTime = DateTime.Now;
                TimeSpan deNoteSp = deNote_endTime - deNote_startTime;
                checkPrintLogAdd("DeliveryNote输入框用时", "DeliveryNote输入框-", deNote_startTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), deNote_endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), deNoteSp.TotalSeconds.ToString());
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

        private void ShowLockForm(string strPW)
        {
            FrmUnLock frm = new FrmUnLock(strPW);
            frm.ShowDialog();
        }

        /// <summary>
        /// 有文件声音
        /// </summary>
        public void existsFile()
        {
            LibHelper.MediasHelper.PlaySoundAsyncByCheckDNPO();
        }
        private void txtGS1SSCC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DateTime snC_startTime = DateTime.Now;
                string cartonNo = this.txtCartonNo.Text.ToUpper().Trim();
                if (cartonNo.Length == 20 && cartonNo.Substring(0, 2).Equals("00"))
                {
                    cartonNo = cartonNo.Substring(2, 18);
                }
                if (cartonNo.StartsWith("3S"))
                {
                    cartonNo = cartonNo.Substring(2);
                }
                if (cartonNo.StartsWith("S"))
                {
                    cartonNo = cartonNo.Substring(1);
                }
                string shipmentId = this.txtShipmentId.Text.ToUpper().Trim();
                string pickPalletNo = this.txtPickPalletNo.Text.ToUpper().Trim();
                string sscc = this.txtTrackingNo.Text.ToUpper().Trim();
                List<string> labelContentList = new List<string>();
                ExecuteResult exeRes = new ExecuteResult();
                DataTable dt = new DataTable();
                if (this.txtTrackingNo.Text.ToUpper().Trim().StartsWith("00") && this.txtTrackingNo.Text.ToUpper().Trim().Length == 20)
                {
                    sscc = sscc.Substring(2);
                }
                string Gs1sscc = this.txtGS1SSCC.Text.ToUpper().Trim();
                if (this.txtGS1SSCC.Text.ToUpper().Trim().StartsWith("00") && this.txtGS1SSCC.Text.ToUpper().Trim().Length == 20)
                {
                    Gs1sscc = Gs1sscc.Substring(2);
                }
                string strGS1Sscc = GetGS1SscCByCarton(cartonNo);
                if (!Gs1sscc.Equals(strGS1Sscc))
                {
                    Show_Message("SSCC 检查失败 ,请重新确认", 0);
                    this.txtGS1SSCC.Focus();
                    this.txtGS1SSCC.SelectAll();
                    return;
                }
                string deliveryNo = !string.IsNullOrEmpty(this.txtDNPO.Text.ToUpper().Trim()) ? this.txtDNPO.Text.ToUpper().Trim() : string.IsNullOrEmpty(this.txtDNDN.Text.ToUpper().Trim()) ? "" : this.txtDNDN.Text.ToUpper().Trim();
                exeRes = controller.checkPassStationPro(shipmentId, pickPalletNo, sscc, cartonNo, deliveryNo, isMix, LibHelper.LocalHelper.getMacAddr_Local(), isDSPacShipLable ? "1" : "0", isDSPacDeliveryNote ? "1" : "0", string.IsNullOrEmpty(this.txtDNDN.Text.ToUpper().Trim()) ? "" : this.txtDNDN.Text.ToUpper().Trim());//过站
                if (!exeRes.Status)
                {
                    Show_Message(exeRes.Message, 0);
                    if (this.isPopLock)
                    {
                        ShowLockForm(this.strUnLockPWD);
                        txtTrackingNo.Text = "";
                        txtGS1SSCC.Text = "";
                        txtCartonNo.Text = "";
                        txtDNDN.Text = "";
                        txtDNPO.Text = "";
                    }
                    if (isDSPacShipLable)
                    {
                        this.txtCartonNo.Enabled = true;
                        this.txtCartonNo.Focus();
                        this.txtCartonNo.SelectAll();
                    }
                    else
                    {
                        this.txtCartonNo.Enabled = false;
                        this.txtTrackingNo.Enabled = true;
                        this.txtTrackingNo.Focus();
                        this.txtTrackingNo.SelectAll();
                    }
                    return;
                }
                updatePalletInfo(pickPalletNo, shipmentId);


                #region  showResult
                exeRes = controller.getShowResultDGV(cartonNo, isMix);
                this.dgvResult.Rows.Clear();
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.dgvResult.Rows.Add();
                        this.dgvResult.Rows[i].Cells[0].Value = dt.Rows[i]["pallet_no"].ToString();
                        this.dgvResult.Rows[i].Cells[1].Value = dt.Rows[i]["sscc"].ToString();
                        this.dgvResult.Rows[i].Cells[2].Value = dt.Rows[i]["carton_no"].ToString();
                        this.dgvResult.Rows[i].Cells[3].Value = dt.Rows[i]["delivery_no"].ToString();
                        this.dgvResult.Rows[i].Cells[4].Value = dt.Rows[i]["checkResult"].ToString();
                    }
                }

                string strSDNstatus = string.Empty;
                string strSDNmsg = string.Empty;
                controller.checkSpecialDNbySP(cartonNo, out strSDNstatus, out strSDNmsg);
                if (strSDNmsg.Equals("OK"))
                {
                    MessageBox.Show("此箱号属于特殊DN，请检查特殊标签".TL());
                    lblSDN.Visible = true;
                    lblSpecialDN.Text = strSDNstatus;
                    lblSpecialDN.Visible = true;
                }

                #endregion
                #region   label 打印

                if (controller.isPrintLabel(pickPalletNo.Substring(2)))
                {
                    labelContentList.Add(shipmentId + @"|" + pickPalletNo.Substring(2) + @"|" + this.lblSecurity.Text.Trim() + @"|" + strEmptyCarton + @"|");
                    DateTime dtStart = DateTime.Now;
                    exeRes = printLabel.printLableForModifyVersion("SH_PalletIDLabel", labelContentList, 1);
                    if (!exeRes.Status)
                    {
                        Show_Message(exeRes.Message, 0);
                    }
                    else
                    {
                        ClientUtils.WritePrintLabelLog("Check Pallet Label", "", pickPalletNo, LocalHelper.getMacAddr_Local());
                    }
                    DateTime endTime = DateTime.Now;
                    TimeSpan ts = endTime - dtStart;
                    this.lblTotalTime.Text = ts.TotalSeconds.ToString() + "-s";
                    checkPrintLogAdd("箱号输入框用时", "箱号输入框用时-打印方法", dtStart.ToString("yyyy-MM-dd HH:mm:ss.fff"), endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), ts.TotalSeconds.ToString());
                }

                #endregion
                txtTrackingNo.Text = "";
                txtCartonNo.Text = "";
                txtGS1SSCC.Text = "";
                txtDNDN.Text = "";
                txtDNPO.Text = "";
                txtGS1SSCC.Enabled = false;
                #region 检查此Pick栈板是否作业完毕
                if (controller.isCheckshipmentIdFinishWorkByCartonNo(cartonNo))
                {
                    Show_Message("此集货单号：" + shipmentId + " 已作业完毕,请刷下一个集货单号！", 1);
                    this.txtShipmentId.Enabled = true;
                    this.txtShipmentId.Focus();
                    this.txtShipmentId.SelectAll();
                    this.txtCartonNo.Enabled = false;
                    resetAllFormStatus();
                    return;
                }
                if (controller.isFinishWorkByCartonNo(cartonNo))
                {
                    Show_Message("此Pick栈板：" + pickPalletNo + " 已作业完毕,请刷下一个Pick栈板！", 1);
                    this.txtPickPalletNo.Enabled = true;
                    this.txtPickPalletNo.Focus();
                    this.txtPickPalletNo.SelectAll();
                    this.txtCartonNo.Enabled = false;
                    this.isDSPacShipLable = false;
                    this.isDSPacDeliveryNote = false;
                    return;
                }
                Show_Message("此箱：" + cartonNo + " 已作业完毕，请刷下一箱！", 1);

                if (isDSPacShipLable)
                {
                    this.txtCartonNo.Enabled = true;
                    this.txtCartonNo.Focus();
                    this.txtCartonNo.SelectAll();
                }
                else
                {
                    this.txtCartonNo.Enabled = false;
                    this.txtTrackingNo.Enabled = true;
                    this.txtTrackingNo.Focus();
                    this.txtTrackingNo.SelectAll();
                }
                #endregion
                DateTime snC_endTime = DateTime.Now;
                TimeSpan diff = snC_endTime - snC_startTime;
                checkPrintLogAdd("箱号输入框用时", "总时间", snC_startTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), snC_endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), diff.TotalSeconds.ToString());
            }
        }

        private string GetGS1SscCByCarton(string cartonNo)
        {
            try
            {
                string sql = @"select distinct a.sscc
                      from ppsuser.t_shipment_pallet a, ppsuser.t_sn_status b
                     where a.pallet_no = b.pack_pallet_no
                       and b.carton_no = :varCarton";
                DataTable dt = ClientUtils.ExecuteSQL(sql, new object[1][] { new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varCarton", cartonNo } }).Tables[0];
                return dt.Rows[0]["sscc"].ToString() ?? "";
            }
            catch { return ""; }

        }
    }
}
