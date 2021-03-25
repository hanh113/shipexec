using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CheckAC.Core;
using CheckAC.Entitys;
using System.IO;
using CheckAC.Utils;

namespace CheckAC
{
    public partial class fMain : Form
    {
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

        private void txtShipmentId_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //DateTime t_startTime = DateTime.Now;
                    string shipmentId = this.txtShipmentId.Text.ToUpper().Trim();
                    shipmentInfo = null;
                    if (string.IsNullOrEmpty(shipmentId))
                    {
                        Show_Message("没有输入集货单号，请输入！", 0);
                        this.txtShipmentId.Focus();
                        LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
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
                            Security = dt.Rows[0]["security"].ToString()
                        };
                        Show_Message(exeRes.Message, 1);
                        this.txtShipmentId.Enabled = false;
                        this.txtPickPalletNo.Enabled = true;
                        this.txtPickPalletNo.Focus();
                        this.txtPickPalletNo.SelectAll();
                        //DateTime endTime = DateTime.Now;
                        //TimeSpan diff = endTime - t_startTime;
                        //RecordTimeLog("CHECK", "SCAN SHIPMENTID", t_startTime, endTime, diff.TotalSeconds);
                    }
                    else
                    {
                        this.txtShipmentId.Focus();
                        this.txtShipmentId.SelectAll();
                        Show_Message(exeRes.Message, 0);
                        LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                this.txtShipmentId.Focus();
                this.txtShipmentId.SelectAll();
                Show_Message(ex.Message, 0);
                LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                return;
            }
        }


        private void Show_Message(string msg, int type)
        {
            lblMessage.Text = msg;
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
            //dgv
            this.dgvPalletInfo.Rows.Clear();
            this.dgvResult.Rows.Clear();
            //public
            this.isMix = false;
            this.isDSPacDeliveryNote = false;
            this.isDSPacShipLable = false;
        }
        private void fMain_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.lblTitel.Text = "CHECK" + "(Ver:" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + ")";
        }

        private void txtPickPalletNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //DateTime t_startTime = DateTime.Now;
                string pickPalletNo = this.txtPickPalletNo.Text.ToUpper().Trim();
                if (string.IsNullOrEmpty(pickPalletNo))
                {
                    Show_Message("没有输入Pick栈板号，请输入！", 0);
                    this.txtPickPalletNo.Focus();
                    LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
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
                    //增加是否PICK完检查,否则不让进行CHECK
                    DataTable dtTempPick = controller.GetFinishiInfo(pickPalletNo);
                    if ((dtTempPick != null) && (dtTempPick.Rows.Count > 0))
                    {
                        Show_Message("该Pick栈板号对应栈板号未Pick完成,Check！", 0);
                        LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                        this.txtPickPalletNo.SelectAll();
                        this.txtPickPalletNo.Focus();
                        return;
                    }
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
                        if (dt.Rows[i]["checkStatus"].ToString().Equals("已完成"))
                        {
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
                    this.lblisMix.Text = dt.Rows[0]["palletType"].ToString();
                    this.lblPalletSize.Text = dt.Rows[0]["remark"].ToString().Trim();
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

                    #region Remark
                    //if (shipmentInfo.Region.Equals("PAC") && shipmentInfo.ShipmentType.Equals("DS"))
                    //{
                    //    if (shipmentInfo.TYPE.Equals("BULK"))
                    //    {
                    //        if (!controller.isExistShippingLabelForPAC(pickPalletNo, shipmentInfo.TYPE))
                    //        {
                    //            isDSPacShipLable = true;
                    //            this.txtCartonNo.Enabled = true;
                    //            this.txtCartonNo.Focus();
                    //            this.txtCartonNo.SelectAll();
                    //            //DateTime endTime1 = DateTime.Now;
                    //            //TimeSpan diff1 = endTime1 - t_startTime;
                    //            //RecordTimeLog("CHECK", "SCAN PICKPALLETNO", t_startTime, endTime1, diff1.TotalSeconds);
                    //            return;
                    //        }
                    //    }
                    //}
                    //this.txtTrackingNo.Enabled = true;
                    //this.txtTrackingNo.Focus();
                    //this.txtTrackingNo.SelectAll();
                    //DateTime endTime = DateTime.Now;
                    //TimeSpan diff = endTime - t_startTime;
                    //RecordTimeLog("CHECK", "SCAN PICKPALLETNO", t_startTime, endTime, diff.TotalSeconds); 
                    #endregion

                    this.txtCartonNo.Enabled = true;
                    this.txtCartonNo.Focus();
                    this.txtCartonNo.SelectAll();
                }
                else
                {
                    this.txtPickPalletNo.Focus();
                    this.txtPickPalletNo.SelectAll();
                    Show_Message(exeRes.Message, 0);
                    LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                    return;
                }
            }
        }

        private void txtTrackingNo_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    DateTime t_startTime = DateTime.Now;
            //    string trackingNo = this.txtTrackingNo.Text.ToUpper().Trim();
            //    string shipmentId = this.txtShipmentId.Text.ToUpper().Trim();
            //    if (this.txtTrackingNo.Text.ToUpper().Trim().Length == 20 && this.txtTrackingNo.Text.ToUpper().Trim().StartsWith("00"))
            //    {
            //        trackingNo = trackingNo.Substring(2);
            //    }
            //    //增加YMT前后A判断
            //    if (this.txtTrackingNo.Text.ToUpper().Trim().Length == 14 && this.txtTrackingNo.Text.ToUpper().Trim().StartsWith("A") && this.txtTrackingNo.Text.ToUpper().Trim().EndsWith("A"))
            //    {
            //        trackingNo = trackingNo.Substring(1, trackingNo.Length - 2);
            //        this.txtTrackingNo.Text = trackingNo;
            //    }
            //    if (string.IsNullOrEmpty(trackingNo))
            //    {
            //        Show_Message("未输入TrackingNo，请输入！", 0);
            //        return;
            //    }
            //    /*
            //     增加检查sscc是否和pick栈板link关系
            //     */
            //    string pickPalletNo = this.txtPickPalletNo.Text.ToUpper().Trim();
            //    ExecuteResult exeRes = new ExecuteResult();
            //    exeRes = controller.check_isLinkPickPalletNo_Pro(shipmentId, trackingNo, pickPalletNo);
            //    //exeRes = controller.checkIsLinkPallet(shipmentInfo,trackingNo,pickPalletNo);
            //    if (exeRes.Status)
            //    {
            //        this.txtCartonNo.Enabled = true;
            //        this.txtCartonNo.Focus();
            //        this.txtCartonNo.SelectAll();
            //        Show_Message("TrackingNo输入OK，请刷箱号(CartonNo)!", 1);
            //        this.txtTrackingNo.Enabled = false;
            //    }
            //    else
            //    {
            //        Show_Message(exeRes.Message, 0);
            //        if (this.isPopLock)
            //        {
            //            ShowLockForm(this.strUnLockPWD);
            //        }
            //        this.txtTrackingNo.Focus();
            //        this.txtTrackingNo.SelectAll();
            //    }
            //    DateTime endTime = DateTime.Now;
            //    TimeSpan diff = endTime - t_startTime;
            //    checkPrintLogAdd("SSCC输入框用时", "", t_startTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), diff.TotalSeconds.ToString());
            //    //RecordTimeLog("CHECK", "SCAN TRACKINGNO", t_startTime, endTime, diff.TotalSeconds);
            //}

        }

        private void txtCartonNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //DateTime snC_startTime = DateTime.Now;
                string cartonNo = this.txtCartonNo.Text.ToUpper().Trim();
                if (string.IsNullOrEmpty(cartonNo))
                {
                    Show_Message("SNCarton未输入，请输入！", 0);
                    this.txtCartonNo.Focus();
                    LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                    return;
                }
                if (cartonNo.Length == 20 && cartonNo.Substring(0, 2).Equals("00"))
                {
                    cartonNo = cartonNo.Substring(2, 18);
                }
                //if (cartonNo.StartsWith("3S"))
                //{
                //    cartonNo = cartonNo.Substring(2);
                //}
                //if (cartonNo.StartsWith("S"))
                //{
                //    cartonNo = cartonNo.Substring(1);
                //}
                string sscc = this.txtTrackingNo.Text.ToUpper().Trim();
                string shipmentId = this.txtShipmentId.Text.Trim().ToUpper();
                string pickPalletNo = this.txtPickPalletNo.Text.Trim().ToUpper();
                if (sscc.ToUpper().Trim().Length == 20 && sscc.ToUpper().Trim().StartsWith("00"))
                {
                    sscc = sscc.Substring(2);
                }
                List<string> labelContentList = new List<string>();
                ExecuteResult exeRes = new ExecuteResult();
                DataTable dt = new DataTable();

                #region Remark
                ////增加检查  此sscc是否link cartonNo        
                //exeRes = controller.check_isLinkCartonNo_Pro(shipmentId, sscc, cartonNo, isDSPacShipLable ? "1" : "0");
                ////exeRes = controller.checkIsLinkCarton(shipmentInfo,sscc,cartonNo,isDSPacShipLable);
                //if (!exeRes.Status)
                //{
                //    Show_Message(exeRes.Message, 0);
                //    LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                //    if (this.isPopLock)
                //    {
                //        ShowLockForm(this.strUnLockPWD);
                //    }
                //    #region Remark
                //    //if (isDSPacShipLable)
                //    //{
                //    //    this.txtPickPalletNo.Enabled = true;
                //    //    this.txtPickPalletNo.Focus();
                //    //    this.txtPickPalletNo.SelectAll();
                //    //}
                //    //else
                //    //{
                //    //    this.txtTrackingNo.Enabled = true;
                //    //    this.txtTrackingNo.Focus();
                //    //    this.txtTrackingNo.SelectAll();
                //    //} 
                //    #endregion
                //    this.txtCartonNo.Enabled = true;
                //    this.txtCartonNo.Focus();
                //    this.txtCartonNo.SelectAll();
                //    return;
                //} 
                #endregion

                #region Remark
                //if (controller.isJumpPackingList(cartonNo, isMix, shipmentInfo))//检查是否需要刷packingList
                //{
                //    this.txtDNPO.Enabled = true;
                //    this.txtDNPO.Focus();
                //    this.txtDNPO.SelectAll();
                //    this.txtCartonNo.Enabled = false;
                //    Show_Message("CartonNo输入完毕，请刷 packingList 条码！", 1);
                //    DateTime endTime1 = DateTime.Now;
                //    TimeSpan diff1 = endTime1 - snC_startTime;
                //    //RecordTimeLog("CHECK", "SCAN CARTONNO", snC_startTime, endTime1, diff1.TotalSeconds);
                //    return;
                //}
                //else
                //{
                //    //检查是否check deliveryNote
                //    if (controller.isJumpDeliveryNoteTB(cartonNo, shipmentInfo, isMix))
                //    {
                //        this.txtDNDN.Enabled = true;
                //        this.txtDNDN.Focus();
                //        this.txtDNDN.SelectAll();
                //        this.txtCartonNo.Enabled = false;
                //        this.isDSPacDeliveryNote = true;
                //        DateTime endTime2 = DateTime.Now;
                //        TimeSpan diff2 = endTime2 - snC_startTime;
                //        //RecordTimeLog("CHECK", "SCAN CARTONNO", snC_startTime, endTime2, diff2.TotalSeconds);
                //        return;
                //    }
                //    exeRes = controller.checkPassStationPro(shipmentId, pickPalletNo, sscc, cartonNo, "", isMix, LibHelperAC.LocalHelper.getMacAddr_Local(), isDSPacShipLable ? "1" : "0", isDSPacDeliveryNote ? "1" : "0", "");//过站
                //    if (!exeRes.Status)
                //    {
                //        Show_Message(exeRes.Message, 0);
                //        if (this.isPopLock)
                //        {
                //            ShowLockForm(this.strUnLockPWD);
                //        }
                //        if (isDSPacShipLable)
                //        {
                //            this.txtCartonNo.Enabled = true;
                //            this.txtCartonNo.Focus();
                //            this.txtCartonNo.SelectAll();
                //        }
                //        else
                //        {
                //            this.txtCartonNo.Enabled = false;
                //            this.txtTrackingNo.Enabled = true;
                //            this.txtTrackingNo.Focus();
                //            this.txtTrackingNo.SelectAll();
                //        }
                //        return;
                //    }
                //    updatePalletInfo(pickPalletNo, shipmentId);
                //    #region  showResult
                //    exeRes = controller.getShowResultDGV(cartonNo, isMix);
                //    this.dgvResult.Rows.Clear();
                //    if (exeRes.Status)
                //    {
                //        dt = (DataTable)exeRes.Anything;
                //        for (int i = 0; i < dt.Rows.Count; i++)
                //        {
                //            this.dgvResult.Rows.Add();
                //            this.dgvResult.Rows[i].Cells[0].Value = dt.Rows[i]["pallet_no"].ToString();
                //            this.dgvResult.Rows[i].Cells[1].Value = dt.Rows[i]["sscc"].ToString();
                //            this.dgvResult.Rows[i].Cells[2].Value = dt.Rows[i]["carton_no"].ToString();
                //            this.dgvResult.Rows[i].Cells[3].Value = dt.Rows[i]["delivery_no"].ToString();
                //            this.dgvResult.Rows[i].Cells[4].Value = dt.Rows[i]["checkResult"].ToString();
                //        }
                //    }

                //    #endregion
                //    #region   label 打印

                //    if (controller.isPrintLabel(pickPalletNo.Substring(2)))
                //    {
                //        labelContentList.Add(shipmentId + @"|" + pickPalletNo.Substring(2) + @"|" + this.lblSecurity.Text.Trim() + @"|");
                //        DateTime dtStart = DateTime.Now;
                //        exeRes = printLabel.printLableForModifyVersion("SH_PalletIDLabel", labelContentList, 1);
                //        if (!exeRes.Status)
                //        {
                //            Show_Message(exeRes.Message, 0);
                //        }
                //        DateTime endTime = DateTime.Now;
                //        TimeSpan ts = endTime - dtStart;
                //        this.lblTotalTime.Text = ts.TotalSeconds.ToString() + "-s";
                //        checkPrintLogAdd("箱号输入框用时", "箱号输入框用时-打印方法", dtStart.ToString("yyyy-MM-dd HH:mm:ss.fff"), endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), ts.TotalSeconds.ToString());
                //        //RecordTimeLog("CHECK", "PRINT CHECKLABEL", snC_startTime, endTime, ts.TotalSeconds);
                //    }

                //    #endregion
                //    #region 检查此Pick栈板是否作业完毕
                //    if (controller.isCheckshipmentIdFinishWorkByCartonNo(cartonNo))
                //    {
                //        Show_Message("此集货单号：" + shipmentId + " 已作业完毕,请刷下一个集货单号！", 1);
                //        this.txtShipmentId.Enabled = true;
                //        this.txtShipmentId.Focus();
                //        this.txtShipmentId.SelectAll();
                //        this.txtCartonNo.Enabled = false;
                //        resetAllFormStatus();
                //        return;
                //    }
                //    if (controller.isFinishWorkByCartonNo(cartonNo))
                //    {
                //        Show_Message("此Pick栈板：" + pickPalletNo + " 已作业完毕,请刷下一个Pick栈板！", 1);
                //        this.txtPickPalletNo.Enabled = true;
                //        this.txtPickPalletNo.Focus();
                //        this.txtPickPalletNo.SelectAll();
                //        this.txtCartonNo.Enabled = false;
                //        this.isDSPacShipLable = false;
                //        this.isDSPacDeliveryNote = false;
                //        return;
                //    }
                //    Show_Message("此箱：" + cartonNo + " 已作业完毕，请刷下一箱！", 1);
                //    if (isDSPacShipLable)
                //    {
                //        this.txtCartonNo.Enabled = true;
                //        this.txtCartonNo.Focus();
                //        this.txtCartonNo.SelectAll();
                //    }
                //    else
                //    {
                //        this.txtCartonNo.Enabled = false;
                //        this.txtTrackingNo.Enabled = true;
                //        this.txtTrackingNo.Focus();
                //        this.txtTrackingNo.SelectAll();
                //    }
                //    #endregion
                //    //DateTime snC_endTime = DateTime.Now;
                //    //TimeSpan diff = snC_endTime - snC_startTime;
                //    //checkPrintLogAdd("箱号输入框用时", "总时间", snC_startTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), snC_endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), diff.TotalSeconds.ToString());
                //    //RecordTimeLog("CHECK", "SCAN CARTONNO", snC_startTime, snC_endTime, diff.TotalSeconds);
                //} 
                #endregion

                exeRes = controller.checkPassStationPro(shipmentId, pickPalletNo, sscc, cartonNo, "", isMix, LibHelperAC.LocalHelper.getMacAddr_Local(), isDSPacShipLable ? "1" : "0", isDSPacDeliveryNote ? "1" : "0", "");//过站
                if (!exeRes.Status)
                {
                    Show_Message(exeRes.Message, 0);
                    LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                    if (this.isPopLock)
                    {
                        ShowLockForm(this.strUnLockPWD);
                    }
                    #region Remark
                    //if (isDSPacShipLable)
                    //{
                    //    this.txtCartonNo.Enabled = true;
                    //    this.txtCartonNo.Focus();
                    //    this.txtCartonNo.SelectAll();
                    //}
                    //else
                    //{
                    //    this.txtCartonNo.Enabled = false;
                    //    this.txtTrackingNo.Enabled = true;
                    //    this.txtTrackingNo.Focus();
                    //    this.txtTrackingNo.SelectAll();
                    //} 
                    #endregion
                    this.txtCartonNo.Enabled = true;
                    this.txtCartonNo.Focus();
                    this.txtCartonNo.SelectAll();
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

                #endregion

                #region   label 打印
                //if (controller.isPrintLabel(pickPalletNo.Substring(2)))
                //{
                //    labelContentList.Add(shipmentId + @"|" + pickPalletNo.Substring(2) + @"|" + this.lblSecurity.Text.Trim() + @"|");
                //    DateTime dtStart = DateTime.Now;
                //    exeRes = printLabel.printLableForModifyVersion("SH_PalletIDLabel", labelContentList, 1);
                //    if (!exeRes.Status)
                //    {
                //        Show_Message(exeRes.Message, 0);
                //    }
                //    DateTime endTime = DateTime.Now;
                //    TimeSpan ts = endTime - dtStart;
                //    this.lblTotalTime.Text = ts.TotalSeconds.ToString() + "-s";
                //    //checkPrintLogAdd("箱号输入框用时", "箱号输入框用时-打印方法", dtStart.ToString("yyyy-MM-dd HH:mm:ss.fff"), endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), ts.TotalSeconds.ToString());
                //    //RecordTimeLog("CHECK", "PRINT CHECKLABEL", snC_startTime, endTime, ts.TotalSeconds);
                //}
                #endregion

                //check before printlabel modify by wenxing 2020-08-31
                if (controller.PrintLabel("AC_Check_Carton"))
                    try
                    {
                        //增加CUSTMODEL为CASE的时候才需要打印这个LABEL
                        DataTable dtTemp = controller.GetModelType(cartonNo);
                        if ((dtTemp == null) || (dtTemp.Rows.Count <= 0))
                        {
                            throw new Exception("未找到料号包规信息,请联系IT-PPS处理!");
                        }
                        if (dtTemp.Rows[0]["CUSTMODEL"].ToString().Trim().ToUpper() == "CASE")
                        {
                            dtTemp = controller.GetAddressInfo(cartonNo);
                            if ((dtTemp == null) || (dtTemp.Rows.Count <= 0))
                            {
                                throw new Exception("未找到PO相关信息,请联系IT-PPS处理!");
                            }
                            if (dtTemp.Rows.Count > 1)
                            {
                                throw new Exception("同一箱号对应PO出货地址不一致,请检查!");
                            }
                            string strSUPPLIER_NAME1 = dtTemp.Rows[0]["SUPPLIER_NAME1"].ToString().Trim();
                            string strSUPPLIER_NAME2 = dtTemp.Rows[0]["SUPPLIER_NAME2"].ToString().Trim();
                            string strSUPPLIER_NAME3 = dtTemp.Rows[0]["SUPPLIER_NAME3"].ToString().Trim();
                            string strSUPPLIER_ADDRESS1 = dtTemp.Rows[0]["SHIPTONAME1"].ToString().Trim();
                            string strSUPPLIER_ADDRESS2 = dtTemp.Rows[0]["SHIPTOADDRESS1"].ToString().Trim();
                            string strSHIPINFO = dtTemp.Rows[0]["SHIPTOCITY"].ToString().Trim();
                            string strSHIPTOSTATE = dtTemp.Rows[0]["SHIPTOSTATE"].ToString().Trim();
                            string strSHIPTOZIP = dtTemp.Rows[0]["SHIPTOZIP"].ToString().Trim();
                            string strSHIPTOCOUNTRY = dtTemp.Rows[0]["SHIPTOCOUNTRY"].ToString().Trim();
                            if (!string.IsNullOrEmpty(strSHIPTOSTATE))
                            {
                                strSHIPINFO = strSHIPINFO + "," + strSHIPTOSTATE;
                            }
                            if (!string.IsNullOrEmpty(strSHIPTOZIP))
                            {
                                strSHIPINFO = strSHIPINFO + "," + strSHIPTOZIP;
                            }
                            if (!string.IsNullOrEmpty(strSHIPTOCOUNTRY))
                            {
                                strSHIPINFO = strSHIPINFO + "," + strSHIPTOCOUNTRY;
                            }
                            labelContentList.Add("\"" + strSUPPLIER_NAME1 + "\"," + "\"" + strSUPPLIER_NAME2 + "\"," + "\"" + strSUPPLIER_NAME3 + "\"," + "\"" + strSUPPLIER_ADDRESS1 + "\"," + "\"" + strSUPPLIER_ADDRESS2 + "\"," + "\"" + strSHIPINFO + "\"");
                            printLabel.printLableForModifyVersion("CartonAddress.btw", labelContentList, 1);
                        }
                    }
                    catch (Exception ex)
                    {
                        Show_Message(ex.Message, 0);
                        this.txtCartonNo.Focus();
                        this.txtCartonNo.SelectAll();
                        return;
                    }

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

                #region Remark
                //if (isDSPacShipLable)
                //{
                //    this.txtCartonNo.Enabled = true;
                //    this.txtCartonNo.Focus();
                //    this.txtCartonNo.SelectAll();
                //}
                //else
                //{
                //    this.txtCartonNo.Enabled = false;
                //    this.txtTrackingNo.Enabled = true;
                //    this.txtTrackingNo.Focus();
                //    this.txtTrackingNo.SelectAll();
                //} 
                #endregion

                this.txtCartonNo.Enabled = true;
                this.txtCartonNo.Focus();
                this.txtCartonNo.SelectAll();
            }
        }
        private void checkPrintLogAdd(string functionName, string fileContent, string startTime, string endTime, string diffTime)
        {
            //string startPath = Application.StartupPath;
            //string logPath = startPath + @"\CheckPrintLog";
            //if (!Directory.Exists(logPath))
            //{
            //    Directory.CreateDirectory(logPath);
            //}
            //string txtFilePath = logPath + @"\" + DateTime.Now.ToString("yyyyMMdd") + @".txt";
            //FileStream fileStream = new FileStream(txtFilePath, FileMode.Append, FileAccess.Write);
            //string allFileContent = "----------------" + functionName + "----------------" + Environment.NewLine + fileContent + Environment.NewLine + "开始时间：" + startTime + Environment.NewLine + "间隔(" + diffTime + ")" + Environment.NewLine + "结束时间：" + endTime + Environment.NewLine + "----------------" + functionName + "----------------";
            //StreamWriter writer = null;
            //try
            //{
            //    using (writer = new StreamWriter(fileStream, Encoding.UTF8))
            //    {
            //        writer.WriteLine(allFileContent);
            //        writer.Flush();
            //        writer.Close();
            //    }
            //}
            //finally
            //{
            //    if (writer != null)
            //    {
            //        writer.Close();
            //    }
            //}
        }

        #region Remark
        //private void RecordTimeLog(string workStation,string workType,DateTime dtStart,DateTime dtEnd,double seconds)
        //{
        //    try
        //    {
        //        controller.AddStationTimeLog(workStation, workType, dtStart, dtEnd, seconds);
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //} 
        #endregion

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
            //if (e.KeyCode == Keys.Enter)
            //{
            //    DateTime dnpo_startTime = DateTime.Now;
            //    string deliveryNo = this.txtDNPO.Text.ToUpper().Trim();
            //    if (string.IsNullOrEmpty(deliveryNo))
            //    {
            //        Show_Message("DN/PO信息未输入，请输入！", 0);
            //        return;
            //    }
            //    string sscc = this.txtTrackingNo.Text.ToUpper().Trim();
            //    if (this.txtTrackingNo.Text.ToUpper().Trim().StartsWith("00") && this.txtTrackingNo.Text.ToUpper().Trim().Length == 20)
            //    {
            //        sscc = sscc.Substring(2);
            //    }
            //    string cartonNo = this.txtCartonNo.Text.ToUpper().Trim();
            //    if (cartonNo.Length == 20 && cartonNo.Substring(0, 2).Equals("00"))
            //    {
            //        cartonNo = cartonNo.Substring(2, 18);
            //    }
            //    if (cartonNo.StartsWith("3S"))
            //    {
            //        cartonNo = cartonNo.Substring(2);
            //    }
            //    if (cartonNo.StartsWith("S"))
            //    {
            //        cartonNo = cartonNo.Substring(1);
            //    }
            //    string shipmentId = this.txtShipmentId.Text.ToUpper().Trim();
            //    string pickPalletNo = this.txtPickPalletNo.Text.ToUpper().Trim();
            //    List<string> labelContentList = new List<string>();
            //    ExecuteResult exeRes = new ExecuteResult();
            //    DataTable dt = new DataTable();
            //    if (controller.isJumpDeliveryNoteTB(cartonNo, shipmentInfo, isMix))
            //    {
            //        this.txtDNDN.Enabled = true;
            //        this.txtDNDN.Focus();
            //        this.txtDNDN.SelectAll();
            //        this.txtDNPO.Enabled = false;
            //        this.isDSPacDeliveryNote = true;
            //        return;
            //    }
            //    exeRes = controller.checkPassStationPro(shipmentId, pickPalletNo, sscc, cartonNo, deliveryNo, isMix, LibHelperAC.LocalHelper.getMacAddr_Local(), isDSPacShipLable ? "1" : "0", isDSPacDeliveryNote ? "1" : "0", "");
            //    if (!exeRes.Status)
            //    {
            //        Show_Message(exeRes.Message, 0);
            //        if (this.isPopLock)
            //        {
            //            ShowLockForm(this.strUnLockPWD);
            //        }
            //        this.txtDNPO.Clear();
            //        this.txtDNPO.Enabled = false;
            //        this.txtTrackingNo.Enabled = true;
            //        this.txtTrackingNo.Focus();
            //        this.txtTrackingNo.SelectAll();
            //        return;
            //    }
            //    updatePalletInfo(pickPalletNo, shipmentId);
            //    #region  showResult
            //    exeRes = controller.getShowResultDGV(cartonNo, isMix);
            //    this.dgvResult.Rows.Clear();
            //    if (exeRes.Status)
            //    {
            //        dt = (DataTable)exeRes.Anything;
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            this.dgvResult.Rows.Add();
            //            this.dgvResult.Rows[i].Cells[0].Value = dt.Rows[i]["pallet_no"].ToString();
            //            this.dgvResult.Rows[i].Cells[1].Value = dt.Rows[i]["sscc"].ToString();
            //            this.dgvResult.Rows[i].Cells[2].Value = dt.Rows[i]["carton_no"].ToString();
            //            this.dgvResult.Rows[i].Cells[3].Value = dt.Rows[i]["delivery_no"].ToString();
            //            this.dgvResult.Rows[i].Cells[4].Value = dt.Rows[i]["checkResult"].ToString();
            //        }
            //    }

            //    #endregion
            //    #region  打印label

            //    if (controller.isPrintLabel(pickPalletNo.Substring(2)))
            //    {
            //        labelContentList.Add(shipmentId + @"|" + pickPalletNo.Substring(2) + @"|" + this.lblSecurity.Text.Trim() + @"|");
            //        DateTime dtStart = DateTime.Now;
            //        exeRes = printLabel.printLableForModifyVersion("SH_PalletIDLabel", labelContentList, 1);
            //        if (!exeRes.Status)
            //        {
            //            Show_Message(exeRes.Message, 0);
            //        }
            //        DateTime endTime = DateTime.Now;
            //        TimeSpan ts = endTime - dtStart;
            //        this.lblTotalTime.Text = ts.TotalSeconds.ToString() + "-s";
            //        checkPrintLogAdd("DN输入框用时", "DN输入框用时-打印方法", dtStart.ToString("yyyy-MM-dd HH:mm:ss.fff"), endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), ts.TotalSeconds.ToString());
            //        //RecordTimeLog("CHECK", "PRINT CHECKLABEL", dtStart, endTime, ts.TotalSeconds);
            //    }

            //    #endregion
            //    #region 检查此Pick栈板是否作业完毕
            //    if (controller.isCheckshipmentIdFinishWorkByCartonNo(cartonNo))
            //    {
            //        Show_Message("此集货单号：" + shipmentId + " 已作业完毕,请刷下一个集货单号！", 1);
            //        this.txtShipmentId.Enabled = true;
            //        this.txtShipmentId.Focus();
            //        this.txtShipmentId.SelectAll();
            //        this.txtDNPO.Enabled = false;
            //        this.isDSPacShipLable = false;
            //        this.isDSPacDeliveryNote = false;
            //        return;
            //    }
            //    if (controller.isFinishWorkByCartonNo(cartonNo))
            //    {
            //        Show_Message("此Pick栈板：" + pickPalletNo + " 已作业完毕,请刷下一个Pick栈板！", 1);
            //        this.txtPickPalletNo.Enabled = true;
            //        this.txtPickPalletNo.Focus();
            //        this.txtPickPalletNo.SelectAll();
            //        this.txtDNPO.Enabled = false;
            //        this.isDSPacShipLable = false;
            //        this.isDSPacDeliveryNote = false;
            //        return;
            //    }
            //    Show_Message("此箱：" + cartonNo + " 已作业完毕，请刷下一箱！", 1);
            //    this.txtTrackingNo.Enabled = true;
            //    this.txtTrackingNo.Focus();
            //    this.txtTrackingNo.SelectAll();
            //    this.txtDNPO.Enabled = false;
            //    #endregion
            //    DateTime dnpo_endTime = DateTime.Now;
            //    TimeSpan dnpoDiff = dnpo_endTime - dnpo_startTime;
            //    checkPrintLogAdd("DN输入框用时", "DN输入框用时-", dnpo_startTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), dnpo_endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), dnpoDiff.TotalSeconds.ToString());
            //    //RecordTimeLog("CHECK", "PRINT SCAN PACKINGLIST", dnpo_startTime, dnpo_endTime, dnpoDiff.TotalSeconds);
            //}
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

        private void fMain_SizeChanged(object sender, EventArgs e)
        {
            //autoSizeForm.controlAutoSize(this);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            resetAllFormStatus();
            Show_Message("已重置窗口！", 1);
        }

        private void txtDNDN_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    DateTime deNote_startTime = DateTime.Now;
            //    string deliveryNote = this.txtDNDN.Text.ToUpper().Trim();
            //    if (string.IsNullOrEmpty(deliveryNote))
            //    {
            //        Show_Message("DN/PO信息未输入，请输入！", 0);
            //        return;
            //    }
            //    string sscc = this.txtTrackingNo.Text.ToUpper().Trim();
            //    if (this.txtTrackingNo.Text.ToUpper().Trim().StartsWith("00") && this.txtTrackingNo.Text.ToUpper().Trim().Length == 20)
            //    {
            //        sscc = sscc.Substring(2);
            //    }
            //    string cartonNo = this.txtCartonNo.Text.ToUpper().Trim();
            //    if (cartonNo.Length == 20 && cartonNo.Substring(0, 2).Equals("00"))
            //    {
            //        cartonNo = cartonNo.Substring(2, 18);
            //    }
            //    if (cartonNo.StartsWith("3S"))
            //    {
            //        cartonNo = cartonNo.Substring(2);
            //    }
            //    if (cartonNo.StartsWith("S"))
            //    {
            //        cartonNo = cartonNo.Substring(1);
            //    }
            //    string shipmentId = this.txtShipmentId.Text.ToUpper().Trim();
            //    string pickPalletNo = this.txtPickPalletNo.Text.ToUpper().Trim();
            //    string deliveryNo = this.txtDNPO.Text.ToUpper().Trim();
            //    List<string> labelContentList = new List<string>();
            //    ExecuteResult exeRes = new ExecuteResult();
            //    DataTable dt = new DataTable();
            //    exeRes = controller.checkPassStationPro(shipmentId, pickPalletNo, sscc, cartonNo, deliveryNo, isMix, LibHelperAC.LocalHelper.getMacAddr_Local(), isDSPacShipLable ? "1" : "0", isDSPacDeliveryNote ? "1" : "0", deliveryNote);
            //    if (!exeRes.Status)
            //    {
            //        Show_Message(exeRes.Message, 0);
            //        if (this.isPopLock)
            //        {
            //            ShowLockForm(this.strUnLockPWD);
            //        }
            //        this.txtTrackingNo.Enabled = true;
            //        this.txtTrackingNo.Focus();
            //        this.txtTrackingNo.SelectAll();
            //        this.txtDNDN.Clear();
            //        this.isDSPacDeliveryNote = false;
            //        this.txtDNDN.Enabled = false;
            //        return;
            //    }
            //    updatePalletInfo(pickPalletNo, shipmentId);
            //    #region  showResult
            //    exeRes = controller.getShowResultDGV(cartonNo, isMix);
            //    this.dgvResult.Rows.Clear();
            //    if (exeRes.Status)
            //    {
            //        dt = (DataTable)exeRes.Anything;
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            this.dgvResult.Rows.Add();
            //            this.dgvResult.Rows[i].Cells[0].Value = dt.Rows[i]["pallet_no"].ToString();
            //            this.dgvResult.Rows[i].Cells[1].Value = dt.Rows[i]["sscc"].ToString();
            //            this.dgvResult.Rows[i].Cells[2].Value = dt.Rows[i]["carton_no"].ToString();
            //            this.dgvResult.Rows[i].Cells[3].Value = dt.Rows[i]["delivery_no"].ToString();
            //            this.dgvResult.Rows[i].Cells[4].Value = dt.Rows[i]["checkResult"].ToString();
            //        }
            //    }

            //    #endregion
            //    #region  打印label

            //    if (controller.isPrintLabel(pickPalletNo.Substring(2)))
            //    {
            //        labelContentList.Add(shipmentId + @"|" + pickPalletNo.Substring(2) + @"|" + this.lblSecurity.Text.Trim() + @"|");
            //        DateTime dtStart = DateTime.Now;
            //        exeRes = printLabel.printLableForModifyVersion("SH_PalletIDLabel", labelContentList, 1);
            //        if (!exeRes.Status)
            //        {
            //            Show_Message(exeRes.Message, 0);
            //        }
            //        DateTime endTime = DateTime.Now;
            //        TimeSpan ts = endTime - dtStart;
            //        this.lblTotalTime.Text = ts.TotalSeconds.ToString() + "-s";
            //        checkPrintLogAdd("DeliveryNote输入框用时", "DeliveryNote输入框-打印方法", dtStart.ToString("yyyy-MM-dd HH:mm:ss.fff"), endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), ts.TotalSeconds.ToString());
            //        //RecordTimeLog("CHECK", "SCAN CARTONNO", dtStart, endTime, ts.TotalSeconds);
            //    }

            //    #endregion
            //    #region 检查此Pick栈板是否作业完毕
            //    if (controller.isCheckshipmentIdFinishWorkByCartonNo(cartonNo))
            //    {
            //        Show_Message("此集货单号：" + shipmentId + " 已作业完毕,请刷下一个集货单号！", 1);
            //        this.txtShipmentId.Enabled = true;
            //        this.txtShipmentId.Focus();
            //        this.txtShipmentId.SelectAll();
            //        this.txtDNDN.Clear();
            //        this.txtDNDN.Enabled = false;
            //        this.isDSPacShipLable = false;
            //        this.isDSPacDeliveryNote = false;
            //        return;
            //    }
            //    if (controller.isFinishWorkByCartonNo(cartonNo))
            //    {
            //        Show_Message("此Pick栈板：" + pickPalletNo + " 已作业完毕,请刷下一个Pick栈板！", 1);
            //        this.txtPickPalletNo.Enabled = true;
            //        this.txtPickPalletNo.Focus();
            //        this.txtPickPalletNo.SelectAll();
            //        this.txtDNDN.Clear();
            //        this.txtDNDN.Enabled = false;
            //        this.isDSPacShipLable = false;
            //        this.isDSPacDeliveryNote = false;
            //        return;
            //    }
            //    Show_Message("此箱：" + cartonNo + " 已作业完毕，请刷下一箱！", 1);
            //    this.txtTrackingNo.Enabled = true;
            //    this.txtTrackingNo.Focus();
            //    this.txtTrackingNo.SelectAll();
            //    this.txtDNDN.Clear();
            //    this.txtDNDN.Enabled = false;
            //    this.isDSPacDeliveryNote = false;
            //    #endregion
            //    DateTime deNote_endTime = DateTime.Now;
            //    TimeSpan deNoteSp = deNote_endTime - deNote_startTime;
            //    checkPrintLogAdd("DeliveryNote输入框用时", "DeliveryNote输入框-", deNote_startTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), deNote_endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), deNoteSp.TotalSeconds.ToString());
            //    //RecordTimeLog("CHECK", "SCAN DELIVERYNOTE", deNote_startTime, deNote_endTime, deNoteSp.TotalSeconds);
            //}
        }

        private void ShowLockForm(string strPW)
        {
            FrmUnLock frm = new FrmUnLock(strPW);
            frm.ShowDialog();
        }
    }
}
