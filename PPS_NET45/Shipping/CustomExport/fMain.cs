using SajetClass;
using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using ClientUtilsDll;
using CRReport.CRfrom;

namespace CustomExport
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }
        private string g_SQL;
        private CommonSQL common = new CommonSQL();
        private Dictionary<string, string> dicShipmentDN = new Dictionary<string, string>();
        /// <summary>
        /// 打印数量
        /// </summary>
        private int printCount = 0;
        private bool isCryPallet = false;
        /// <summary>
        /// Bartender打印PalletLoadingSheet
        /// </summary>
        private PalletSheetlabel palletLable = new PalletSheetlabel();

        private void fMain_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " (" + SajetCommon.g_sFileVersion + ")";
            labShow.Text = labShow.Text + " (" + SajetCommon.g_sFileVersion + ")";
            //DateTime dateTimeNow = DateTime.Now;
            //dt_start.Value = new DateTime(dateTimeNow.Year, dateTimeNow.Month - 2, 1);
            //dt_close.Value = dateTimeNow.AddDays(-1);
            //dt_end.Value = dateTimeNow.AddDays(1);
            this.dt_close.Value = DateTime.Now;
            this.WindowState = FormWindowState.Maximized;

            this.cmbPDF.SelectedIndex = 0;
            this.cmbREGION.SelectedIndex = 0;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                btnSearch.Enabled = false;
                object[][] sqlparams = new object[0][];     //查询条件传参数组
                string strSQL = createSQL(out sqlparams);
                DataTable db = ClientUtils.ExecuteSQL(strSQL, sqlparams).Tables[0];
                //dgvNo.Rows.Clear();
                dgvNo.DataSource = null;
                refreshCmbbox();
                if (db.Rows.Count > 0)
                {
                    dgvNo.DataSource = db;
                    for (int i = 0; i < db.Rows.Count; i++)
                    {
                        try
                        {
                            dgvNo.Invoke((MethodInvoker)delegate ()
                            {
                                if (!cmbSmid.Items.Contains(db.Rows[i]["SHIPMENT_ID"].ToString()))
                                {
                                    cmbSmid.Items.Add(db.Rows[i]["SHIPMENT_ID"].ToString());
                                }
                                if (!cmbPOE.Items.Contains(db.Rows[i]["POE"].ToString()) && !string.IsNullOrEmpty(db.Rows[i]["POE"].ToString()))
                                {
                                    cmbPOE.Items.Add(db.Rows[i]["POE"].ToString());
                                }
                                if (!cmbCarrier.Items.Contains(db.Rows[i]["CARRIER_NAME"].ToString()) && !string.IsNullOrEmpty(db.Rows[i]["CARRIER_NAME"].ToString()))
                                {
                                    cmbCarrier.Items.Add(db.Rows[i]["CARRIER_NAME"].ToString());
                                }
                            });
                        }
                        catch (Exception e1)
                        {
                            ShowMsg(e1.ToString(), 0);
                            return;
                        }
                    }
                    ShowMsg("", -1);
                }
                else
                {
                    ShowMsg("查询无资料", 0);
                }
                btnSearch.Enabled = true;
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message, 0);
                return;
            }
        }
        private string createSQL(out object[][] sqlparams)
        {
            if (cmbPDF.Text.Equals("PALLETLOADINGSHEET"))
            {
                g_SQL = string.Format(@"
                        select a.shipment_id,
                               b.pallet_no,
                               a.carrier_name,
                               a.carrier_code,
                               a.poe,
                               a.hawb,
                               a.shipment_type,
                               a.region,
                               a.qty,
                               a.carton_qty,
                               a.shipping_time,
                               a.transport,
                               a.status,
                               a.origion,
                               a.invtype
                        from ppsuser.t_shipment_info a 
                        join ppsuser.t_shipment_pallet b
                             on a.shipment_id =b.shipment_id
                        --where  a.status not in('WS','SF','IN') 
                        where  a.status='UF'
                        and 1=1
                        ");
            }
            else if (this.cmbPDF.Text.Trim() == "HANDOVERMANIFEST")
            {
                g_SQL = string.Format(@"
                        select a.shipment_id,
                               a.carrier_name,
                               a.carrier_code,
                               a.poe,
                               a.hawb,
                               a.shipment_type,
                               a.region,
                               a.qty,
                               a.carton_qty,
                               a.shipping_time,
                               a.transport,
                               a.status,
                               a.origion,
                               a.invtype
                        from ppsuser.t_shipment_info a 
                        --where  a.status not in('WS','SF','IN') 
                        where  a.status='UF'
                        and 1=1
                        ");
            }
            else if (this.cmbPDF.Text.Trim() == "DELIVERYNOTE")
            {
                g_SQL = string.Format(@"
                        SELECT DISTINCT A.SHIPMENT_ID,C.DELIVERY_NO,C.LINE_ITEM,B.PALLET_NO,A.REGION,A.POE,A.SHIPMENT_TYPE,
A.TYPE,A.TRANSPORT,A.CARRIER_CODE,A.CARRIER_NAME,
DECODE(B.PALLET_TYPE,'001','NO_MIX','999','MIX') as PALLETTYPE
FROM PPSUSER.T_SHIPMENT_INFO A INNER JOIN PPSUSER.T_SHIPMENT_PALLET B ON A.SHIPMENT_ID=B.SHIPMENT_ID
INNER JOIN PPSUSER.T_PALLET_ORDER C ON A.SHIPMENT_ID=C.SHIPMENT_ID AND B.PALLET_NO=C.PALLET_NO
WHERE 1=1
                        ");
            }
            else if (this.cmbPDF.Text.Trim() == "PACKINGLIST")
            {
                g_SQL = string.Format(@"
                        SELECT DISTINCT A.SHIPMENT_ID,C.DELIVERY_NO,C.LINE_ITEM,B.PALLET_NO,A.REGION,A.POE,A.SHIPMENT_TYPE,
A.TYPE,A.TRANSPORT,A.CARRIER_CODE,A.CARRIER_NAME,
DECODE(B.PALLET_TYPE,'001','NO_MIX','999','MIX') as PALLETTYPE
FROM PPSUSER.T_SHIPMENT_INFO A INNER JOIN PPSUSER.T_SHIPMENT_PALLET B ON A.SHIPMENT_ID=B.SHIPMENT_ID
INNER JOIN PPSUSER.T_PALLET_ORDER C ON A.SHIPMENT_ID=C.SHIPMENT_ID AND B.PALLET_NO=C.PALLET_NO
WHERE 1=1
                        ");
            }
            else
            {
                throw new Exception("未知的档案类型,请检查...".TL());
            }

            //组合输入查询条件，过滤数据源
            //出货类型查询条件
            sqlparams = new object[0][];
            if (chkDS.Checked && chkFD.Checked)
            {
                g_SQL += " and a.SHIPMENT_TYPE in ('FD','DS')";
            }
            else if (chkDS.Checked && !chkFD.Checked)
            {
                g_SQL += " and a.SHIPMENT_TYPE in ('DS')";
            }
            else
            {
                g_SQL += " and a.SHIPMENT_TYPE in ('FD')";
            }

            //Close时间

            g_SQL += string.Format(" and to_char(a.close_time,'YYYY-MM-DD')='{0}'", this.dt_close.Value.ToString("yyyy-MM-dd"));

            int iPara = 0;                              //变量项次

            //集货单号查询条件
            if (cmbSmid.Text.Trim() != "" && cmbSmid.Text.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and a.Shipment_ID = :shipmentID ";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentID", cmbSmid.Text };
                iPara = iPara + 1;
            }

            //货代查询条件
            if (cmbCarrier.Text.Trim() != "" && cmbCarrier.Text.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and a.CARRIER_NAME = :carrier";
                //g_SQL += " and  carrier_code in ( select carriercode from PPTEST.OMS_CARRIER_TRACKING_PREFIX where scaccode = :carrier )";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "carrier", cmbCarrier.Text };
                iPara = iPara + 1;
            }

            //港口查询条件
            if (cmbPOE.Text.Trim() != "" && cmbPOE.Text.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and a.POE = :poe";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "poe", cmbPOE.Text };
                iPara = iPara + 1;
            }

            //状态查询条件// 这里有个状态 查不到的 QH
            //if (cmbSTATUS.Text.Trim() != "" && cmbSTATUS.Text.Trim() != "-ALL-")
            //{
            //    string strStatus = cmbSTATUS.Text.Trim();
            //    if (strStatus.Contains("WP-"))
            //    { strStatus = "WP"; }
            //    else if (strStatus.Contains("IP-"))
            //    { strStatus = "IP"; }
            //    else if (strStatus.Contains("FP-"))
            //    { strStatus = "FP"; }
            //    else if (strStatus.Contains("LF-"))
            //    { strStatus = "LF"; }
            //    else if (strStatus.Contains("UF-"))
            //    { strStatus = "UF"; }
            //    else if (strStatus.Contains("CP-"))
            //    { strStatus = "CP"; }
            //    else if (strStatus.Contains("HO-"))
            //    { strStatus = "HO"; }

            //    Array.Resize(ref sqlparams, sqlparams.Length + 1);
            //    g_SQL += " and Status = :Status";
            //    sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Status", strStatus };
            //    iPara = iPara + 1;
            //}

            //if (cmbSTATUS.Text.Contains("ALL") || cmbSTATUS.Text.Contains("UF-"))
            //{
            //    //出货开始日期
            //    Array.Resize(ref sqlparams, sqlparams.Length + 1);
            //    g_SQL += " and SHIPPING_TIME >= :shipmentTime";
            //    sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleType.DateTime, "shipmentTime", dt_close.Value };
            //    iPara = iPara + 1;

            //    //出货结束日期
            //    Array.Resize(ref sqlparams, sqlparams.Length + 1);
            //    g_SQL += " and SHIPPING_TIME <= :shipmentTime2";
            //    sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleType.DateTime, "shipmentTime2", dt_end.Value };
            //    iPara = iPara + 1;
            //}

            //地区查询条件
            if (cmbREGION.Text.Trim() != "" && cmbREGION.Text.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and a.Region = :Region";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Region", cmbREGION.Text };
                iPara = iPara + 1;
            }
            //添加排序
            if (cmbPDF.Text.Equals("PALLETLOADINGSHEET"))
            {
                g_SQL += " order by a.shipment_id,b.pallet_no asc  ";
            }
            else if (this.cmbPDF.Text.Trim() == "HANDOVERMANIFEST")
            {
                g_SQL += " order by a.shipment_id  asc  ";
            }
            else if (this.cmbPDF.Text.Trim() == "DELIVERYNOTE")
            {
                g_SQL += " ORDER BY A.SHIPMENT_ID,C.DELIVERY_NO,C.LINE_ITEM,B.PALLET_NO ASC  ";
            }
            else if (this.cmbPDF.Text.Trim() == "PACKINGLIST")
            {
                g_SQL += " ORDER BY A.SHIPMENT_ID,C.DELIVERY_NO,C.LINE_ITEM,B.PALLET_NO ASC  ";
            }

            //g_SQL = g_SQL.Replace("\r\n", " ");
            return g_SQL;
        }
        private void refreshCmbbox()
        {
            cmbSmid.Items.Clear();
            cmbSmid.Items.Add("-ALL-");

            //cmbREGION.Items.Clear();
            //cmbREGION.Items.Add("-ALL-");

            //cmbPOE.DataSource = null;
            cmbPOE.Items.Clear();
            cmbPOE.Items.Add("-ALL-");

            //cmbCarrier.DataSource = null;
            cmbCarrier.Items.Clear();
            cmbCarrier.Items.Add("-ALL-");
        }

        //public void formControl()
        //{
        //    if (cmbSTATUS.Text.Contains("ALL") || cmbSTATUS.Text.Contains("UF-"))
        //    {
        //        lblClose.Visible = true;
        //        lblEnd.Visible = true;
        //        dt_close.Visible = true;
        //        dt_end.Visible = true;
        //    }
        //    else
        //    {
        //        lblClose.Visible = false;
        //        lblEnd.Visible = false;
        //        dt_close.Visible = false;
        //        dt_end.Visible = false;
        //    }
        //}

        private void chkFD_CheckedChanged(object sender, EventArgs e)
        {
            dgvNo.DataSource = null;
            if (!chkFD.Checked && !chkDS.Checked)
            {
                chkDS.Checked = true;
            }
        }

        private void chkDS_CheckedChanged(object sender, EventArgs e)
        {
            dgvNo.DataSource = null;
            if (!chkDS.Checked && !chkFD.Checked)
            {
                chkFD.Checked = true;
            }
        }

        private void cmbSmid_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvNo.DataSource = null;
        }

        private void cmbREGION_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvNo.DataSource = null;
        }

        private void cmbPOE_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvNo.DataSource = null;
        }

        private void cmbCarrier_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvNo.DataSource = null;
        }

        //private void cmbSTATUS_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    dgvNo.DataSource = null;
        //    formControl();
        //}

        private void dt_close_ValueChanged(object sender, EventArgs e)
        {
            dgvNo.DataSource = null;
        }

        //private void dt_end_ValueChanged(object sender, EventArgs e)
        //{
        //    dgvNo.DataSource = null;
        //}

        public DialogResult ShowMsg(string strTxt, int strType)
        {
            TextMsg.Text = strTxt.TP();
            switch (strType)
            {
                case 0: //Error    
                    TextMsg.ForeColor = Color.Red;
                    TextMsg.BackColor = Color.Blue;
                    return DialogResult.None;
                case 1: //Warning                        
                    TextMsg.ForeColor = Color.Blue;
                    TextMsg.BackColor = Color.FromArgb(255, 255, 128);
                    return DialogResult.None;
                case 2: //Confirm
                    return MessageBox.Show(strTxt, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    TextMsg.ForeColor = Color.White;
                    TextMsg.BackColor = Color.Blue;
                    return DialogResult.None;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            ShowMsg("", -1);

            if (dgvNo.RowCount == 0)
            {
                ShowMsg("需查询到集货单", 0);
                return;
            }

            try
            {
                dicShipmentDN = new Dictionary<string, string>();
                printCount = 0;
                if (cmbPDF.Text.Equals("HANDOVERMANIFEST"))
                {
                    #region HANDOVERMANIFEST
                    for (int i = 0; i < dgvNo.RowCount; i++)
                    {
                        string strshipmentid = dgvNo.Rows[i].Cells["SHIPMENT_ID"].Value.ToString();
                        string strRegion = dgvNo.Rows[i].Cells["REGION"].Value.ToString().Trim();
                        string strCarrierCode = dgvNo.Rows[i].Cells["CARRIER_CODE"].Value.ToString().Trim();
                        string strPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "PDF\\" + strRegion + "\\" + strCarrierCode + "\\" + this.dt_close.Value.ToString("yyyyMMdd") + "\\" + "HANDOVERMANIFEST" + "\\");
                        if (!Directory.Exists(strPath))//如果路径不存在
                        {
                            Directory.CreateDirectory(strPath);//创建一个路径的文件夹
                        }
                        strPath = Path.Combine(strPath, strshipmentid + ".pdf");

                        CustomExportBLL ceb = new CustomExportBLL();
                        //如果此集货单的所有的栈板都称重完成，且是PAC的，就打印Handover manifest

                        string strResultShipment = string.Empty;
                        string strResultRegion = string.Empty;
                        ceb.CheckShipmentWeightStatus(strshipmentid, out strResultRegion, out strResultShipment);

                        if (strResultShipment.Equals("OK"))
                        {
                            //OK  说明集货单所有栈板都称重完成，且是PAC
                            string errormsg2 = string.Empty;

                            CRReport.CRMain cr = new CRReport.CRMain();
                            if (strResultRegion.Equals("PAC"))
                            {
                                cr.HanDoveMan(strshipmentid, false, false, strPath);
                            }
                            else
                            {
                                cr.HanDoveMan2(strshipmentid, false, false, "WEIGHT", strPath);
                            }
                            printCount = printCount + 1;
                        }

                    }

                    //CRReport.CRMain cr = new CRReport.CRMain();
                    //cr.HanDoveMan(strshipmentid, false, false);


                    //ShowMsg("OK", -1);
                    #endregion
                }
                //"CHANNEL_PACKLIST",
                //"CONSUMER_PACKLIST",
                //"DELIVERYNOTE",
                //"PALLETLOADINGSHEET",
                //"HANDOVERMANIFEST"});
                else if (cmbPDF.Text.Equals("PALLETLOADINGSHEET"))
                {
                    #region PALLETLOADINGSHEET

                    //SAWB 20190729003196 //NORMAL 20190807003985 //
                    //string strPalletNo = "20190729003196";
                    for (int i = 0; i < dgvNo.RowCount; i++)
                    {
                        string strPalletNo = dgvNo.Rows[i].Cells["PALLET_NO"].Value.ToString();
                        string strRegion = dgvNo.Rows[i].Cells["REGION"].Value.ToString().Trim();
                        string strCarrierCode = dgvNo.Rows[i].Cells["CARRIER_CODE"].Value.ToString().Trim();
                        string strPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "PDF\\" + strRegion + "\\" + strCarrierCode + "\\" + this.dt_close.Value.ToString("yyyyMMdd") + "\\" + "PALLETLOADINGSHEET" + "\\");
                        strPath = Path.Combine(strPath, strPalletNo + ".pdf");
                        if (isCryPallet)
                        {
                            CRReport.CRMain cr = new CRReport.CRMain();
                            cr.PalletLoadingSheet(strPalletNo, false, false, strPath);
                        }
                        else
                        {
                            if (!palletLable.PrintPalletLabel(strPalletNo, 10, "ALL"))
                            {
                                throw new Exception("打印连接出了问题".TL());
                            }
                        }
                        printCount = printCount + 1;
                    }

                    //ShowMsg("OK", -1);
                    #endregion
                }
                else if (this.cmbPDF.Text.Trim() == "PACKINGLIST")
                {
                    #region PACKINGLIST
                    for (int i = 0; i < this.dgvNo.RowCount; i++)
                    {
                        string strRegion = dgvNo.Rows[i].Cells["REGION"].Value.ToString().Trim();
                        string strShipmentType = dgvNo.Rows[i].Cells["SHIPMENT_TYPE"].Value.ToString().Trim();
                        string strShipmentID = dgvNo.Rows[i].Cells["SHIPMENT_ID"].Value.ToString().Trim();
                        string strType = dgvNo.Rows[i].Cells["TYPE"].Value.ToString().Trim();
                        string strDeliveryNo = dgvNo.Rows[i].Cells["DELIVERY_NO"].Value.ToString().Trim();
                        string strLineItem = dgvNo.Rows[i].Cells["LINE_ITEM"].Value.ToString().Trim();
                        string strCarrierCode = dgvNo.Rows[i].Cells["CARRIER_CODE"].Value.ToString().Trim();
                        if (dicShipmentDN.ContainsKey(strShipmentID + strDeliveryNo))
                        {
                            continue;
                        }
                        else
                        {
                            dicShipmentDN.Add(strShipmentID + strDeliveryNo, strShipmentID + strDeliveryNo);
                        }
                        string strPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "PDF\\" + strRegion + "\\" + strCarrierCode + "\\" + this.dt_close.Value.ToString("yyyyMMdd") + "\\" + "PACKINGLIST" + "\\");
                        strPath = Path.Combine(strPath, strShipmentID + "_" + strDeliveryNo + ".pdf");

                        if (strShipmentType == "FD")
                        {
                            if (strRegion == "AMR")
                            {
                                #region FD AMR
                                CheckOmsBucketDocIsExist("AMR", "Hub Packing List");
                                new FDHubGlobalLayoutForm(strDeliveryNo, strShipmentID, strPath, false);
                                printCount = printCount + 1;
                                continue;
                                #endregion
                            }
                            else
                            {
                                continue;
                            }
                            
                        }
                        else
                        {
                            T940UnicodeInfo t940UnicodeInfo = GetT940UnicodeInfoByDeliveryNoAndLineItem(strDeliveryNo, strLineItem);
                            if (t940UnicodeInfo == null)
                            {
                                continue;
                            }
                            else
                            {
                                DataTable dtTempPackList = common.JudgeCrystalReportByCondition(t940UnicodeInfo.Region, t940UnicodeInfo.CustomerGroup, t940UnicodeInfo.MsgFlag, t940UnicodeInfo.GpFlag);
                                if ((dtTempPackList != null) && (dtTempPackList.Rows.Count > 0))
                                {
                                    string documentName = dtTempPackList.Rows[0]["documentname"].ToString().Trim();
                                    if (documentName.Equals("ConsumerPL"))
                                    {
                                        #region ConsumerPL
                                        if (strRegion == "AMR")
                                        {
                                            CheckOmsBucketDocIsExist("AMR", "Consumer Packing List");
                                            if (t940UnicodeInfo.ShipCntyCode.Equals("MX"))
                                            {
                                                new Ltr_PkList_MEX(strDeliveryNo, strLineItem, strShipmentID, false, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                            else if (t940UnicodeInfo.ShipCntyCode.Equals("CA"))
                                            {
                                                new ConsumerPkListBilingualCanadaForm(strDeliveryNo, strLineItem, strShipmentID, false, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                            else
                                            {
                                                new P1ConsumerPackingListGlobal(strDeliveryNo, strLineItem, strShipmentID, false, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                        }
                                        else if (strRegion == "PAC")
                                        {
                                            CheckOmsBucketDocIsExist("PAC", "Consumer Packing List");
                                            if (t940UnicodeInfo.ShipCntyCode.Equals("TW"))
                                            {
                                                new P1ConsumerPackingListTW(strDeliveryNo, strLineItem, strShipmentID, false, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                            else if (t940UnicodeInfo.ShipCntyCode.Equals("CN"))
                                            {
                                                new P1ConsumerPackingListChina(strDeliveryNo, strLineItem, strShipmentID, false, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                            else if (t940UnicodeInfo.ShipCntyCode.Equals("JP"))
                                            {
                                                new P1ConsumerPackingListJP(strDeliveryNo, strLineItem, strShipmentID, false, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                            else if (t940UnicodeInfo.ShipCntyCode.Equals("KR"))
                                            {
                                                new ConsumerPackingListKorea(strDeliveryNo, strLineItem, strShipmentID, false, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                            else if (t940UnicodeInfo.ShipCntyCode.Equals("HK"))
                                            {
                                                new P1ConsumerPackingListHK(strDeliveryNo, strLineItem, strShipmentID, false, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                            else if (t940UnicodeInfo.ShipCntyCode.Equals("TH"))
                                            {
                                                new ThaiLandPKFormcs(strDeliveryNo, strLineItem, strShipmentID, false, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                            else
                                            {
                                                new P1ConsumerPackingListGlobal(strDeliveryNo, strLineItem, strShipmentID, false, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region 非ConsumerPL
                                        if (strRegion == "AMR")
                                        {
                                            CheckOmsBucketDocIsExist("AMR", "Channel Packing List");
                                            if (t940UnicodeInfo.ShipCntyCode.Equals("US"))
                                            {
                                                new ChannelPackingListUS(strDeliveryNo, strLineItem, strShipmentID, false, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                            else if (t940UnicodeInfo.ShipCntyCode.Equals("MX"))
                                            {
                                                new MexicoChannelPackingList(strDeliveryNo, strLineItem, strShipmentID, false, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                            else if (t940UnicodeInfo.ShipCntyCode.Equals("CA"))
                                            {
                                                new ChannelPackingListCanada(strDeliveryNo, strLineItem, strShipmentID, false, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                            else if (t940UnicodeInfo.ShipCntyCode.Equals("CO"))
                                            {
                                                new ChannelColumbiaForm(strDeliveryNo, strLineItem, strShipmentID, false, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                            else if (t940UnicodeInfo.ShipCntyCode.Equals("BR"))
                                            {
                                                new ChannelPackingListBrazil(strDeliveryNo, strLineItem, strShipmentID, false, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                            else if (t940UnicodeInfo.ShipCntyCode.Equals("CL"))
                                            {
                                                new ChileChannelPackingList(strDeliveryNo, strLineItem, strShipmentID, false, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                            else
                                            {
                                                new ChannelPackingListPeru(strDeliveryNo, strLineItem, strShipmentID, false, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if (this.cmbPDF.Text.Trim() == "DELIVERYNOTE")
                {
                    #region DELIVERYNOTE

                    for (int i = 0; i < this.dgvNo.RowCount; i++)
                    {
                        string strRegion = dgvNo.Rows[i].Cells["REGION"].Value.ToString().Trim();
                        if (strRegion == "AMR")
                        {
                            continue;
                        }
                        string strShipmentType = dgvNo.Rows[i].Cells["SHIPMENT_TYPE"].Value.ToString().Trim();
                        if (strShipmentType == "FD")
                        {
                            continue;
                        }
                        string strShipmentID = dgvNo.Rows[i].Cells["SHIPMENT_ID"].Value.ToString().Trim();
                        string strType = dgvNo.Rows[i].Cells["TYPE"].Value.ToString().Trim();
                        string strDeliveryNo = dgvNo.Rows[i].Cells["DELIVERY_NO"].Value.ToString().Trim();
                        string strLineItem = dgvNo.Rows[i].Cells["LINE_ITEM"].Value.ToString().Trim();
                        string strPalletNo = dgvNo.Rows[i].Cells["PALLET_NO"].Value.ToString().Trim();
                        string strCarrierCode = dgvNo.Rows[i].Cells["CARRIER_CODE"].Value.ToString().Trim();
                        if (dicShipmentDN.ContainsKey(strShipmentID + strDeliveryNo))
                        {
                            continue;
                        }
                        else
                        {
                            dicShipmentDN.Add(strShipmentID + strDeliveryNo, strShipmentID + strDeliveryNo);
                        }
                        string strPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "PDF\\" + strRegion + "\\" + strCarrierCode + "\\" + this.dt_close.Value.ToString("yyyyMMdd") + "\\" + "DELIVERYNOTE" + "\\");
                        strPath = Path.Combine(strPath, strShipmentID + "_" + strDeliveryNo + ".pdf");
                       
                        if (strRegion == "PAC")
                        {
                            #region DELIVERYNOTE PAC
                            CheckOmsBucketDocIsExist("PAC", "Delivery Note");
                            if (strType == "PARCEL")
                            {
                                DataTable dtTempT90DeliveryNote = common.GetDeliveryNoteT90Info(strDeliveryNo);
                                if ((dtTempT90DeliveryNote != null) && (dtTempT90DeliveryNote.Rows.Count > 0))
                                {
                                    string shipCnCode = dtTempT90DeliveryNote.Rows[0]["SHIPCNTYCODE"].ToString();
                                    string custGroup = dtTempT90DeliveryNote.Rows[0]["CUSTOMERGROUP"].ToString();
                                    if (shipCnCode.Equals("JP"))
                                    {
                                        DataTable dtTempPACDeliverNoteLabel1 = common.GetPrintPACDeliveryNoteLabel(strShipmentID, strType);
                                        if ((dtTempPACDeliverNoteLabel1 != null) && (dtTempPACDeliverNoteLabel1.Rows.Count > 0))
                                        {
                                            new D1_DeliveryNote(strDeliveryNo, strLineItem, strShipmentID, true, strPath);
                                            printCount = printCount + 1;
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        if (custGroup.Equals("IN") || custGroup.Equals("RK") || custGroup.Equals("RW"))
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            DataTable dtTempPACDeliverNoteLabel2 = common.GetPrintPACDeliveryNoteLabel(strShipmentID, strType);
                                            if ((dtTempPACDeliverNoteLabel2 != null) && (dtTempPACDeliverNoteLabel2.Rows.Count > 0))
                                            {
                                                new D1_DeliveryNote(strDeliveryNo, strLineItem, strShipmentID, true, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    throw new Exception("T940Unicode没有获得资料,请联系IT-PPS!".TL());
                                }
                            }
                            else
                            {
                                DataTable dtTempPACDeliverNoteLabel3 = common.GetPrintPACDeliveryNoteLabel(strShipmentID, strType);
                                if ((dtTempPACDeliverNoteLabel3 != null) && (dtTempPACDeliverNoteLabel3.Rows.Count > 0))
                                {
                                    new D1_DeliveryNote(strDeliveryNo, strLineItem, strShipmentID, true, strPath);
                                    printCount = printCount + 1;
                                    continue;
                                }
                            }
                            #endregion
                        }



                        else if (strRegion == "EMEIA")
                        {
                            #region  DELIVERYNOTE EMEIA
                            CheckOmsBucketDocIsExist("EMEIA", "Channel Delivery Note");
                            T940UnicodeInfo t940UnicodeInfo = GetT940UnicodeInfoByDeliveryNoAndLineItem(strDeliveryNo, strLineItem);
                            if (t940UnicodeInfo == null)
                            {
                                continue;
                            }
                            else
                            {
                                DataTable dtTempPackList = common.JudgeCrystalReportByCondition(t940UnicodeInfo.Region, t940UnicodeInfo.CustomerGroup, t940UnicodeInfo.MsgFlag, t940UnicodeInfo.GpFlag);
                                if ((dtTempPackList != null) && (dtTempPackList.Rows.Count > 0))
                                {
                                    string documentName = dtTempPackList.Rows[0]["documentname"].ToString().Trim();
                                    if (documentName.Equals("ConsumerPL"))
                                    {
                                        if(strRegion == "EMEIA")
                                        {
                                            CheckOmsBucketDocIsExist("EMEIA", "Consumer Packing List");
                                            new ConsumerPackingList_EMEA_G(strDeliveryNo, strLineItem, strShipmentID, false, strPath);
                                            printCount = printCount + 1;
                                            continue;
                                        }
                                        continue;
                                    }
                                    else
                                    {
                                        if (t940UnicodeInfo.ShipCntyCode.Equals("RU") || t940UnicodeInfo.ShipCntyCode.Equals("AE") || t940UnicodeInfo.ShipCntyCode.Equals("TR"))
                                        {
                                            if (isMultiOrSignleCustSoNoByDeliveryNo(strDeliveryNo))
                                            {
                                                new EMEIA_BUY_DELIVERYNOTE(strDeliveryNo, strPalletNo, true, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                            else
                                            {
                                                new EMEIA_SELL_DELIVERYNOTE(strDeliveryNo, strPalletNo,false, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            if (isMultiOrSignleCustSoNoByDeliveryNo(strDeliveryNo))
                                            {
                                                new EMEIA_MUTIL_DELIVERYNOTE(strDeliveryNo, strPalletNo, false, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                            else
                                            {
                                                new DeliveryNoteHForm(strDeliveryNo, strPalletNo, false, strPath);
                                                printCount = printCount + 1;
                                                continue;
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
                else
                {
                    throw new Exception("未知的档案类型,请检查...".TL());
                }
                ShowMsg("成功生成" + printCount + "条!", -1);
            }
            catch(Exception ex)
            {
                ShowMsg(ex.Message, 0);
                return;
            }
        }

        private void CheckOmsBucketDocIsExist(string region, string documentName)
        {
            DataTable dt = common.checkOmsBucketDocIsExist(region, documentName);
            if ((dt == null) || (dt.Rows.Count <= 0))
            {
                throw new Exception("NG_此Region:" + region + " 对应的-" + documentName + " BucketType 维护异常！".TP());
            }
        }

        private T940UnicodeInfo GetT940UnicodeInfoByDeliveryNoAndLineItem(string DeliveryNo, string LineItem)
        {
            DataTable dtT90UniCode = common.GetT940UnicodeInfoByDeliveryNoAndLineItem(DeliveryNo);
            if ((dtT90UniCode != null) && (dtT90UniCode.Rows.Count > 0))
            {
                string customerGroup_D = dtT90UniCode.Rows[0]["customergroup"].ToString().Trim();
                if (!(customerGroup_D.Equals("RK") || customerGroup_D.Equals("RW") || customerGroup_D.Equals("IN") || customerGroup_D.Equals("RR")))
                {
                    customerGroup_D = "OTHER";
                }
                string t9uMsgFlag = "N";
                string t9uGbFlag = "N";
                for (int i = 0; i < dtT90UniCode.Rows.Count; i++)
                {
                    if (!dtT90UniCode.Rows[i]["msgflag"].ToString().Trim().Equals(t9uMsgFlag))
                    {
                        t9uMsgFlag = dtT90UniCode.Rows[i]["msgflag"].ToString().Trim();
                        break;
                    }
                }
                for (int i = 0; i < dtT90UniCode.Rows.Count; i++)
                {
                    if (!dtT90UniCode.Rows[i]["gpflag"].ToString().Trim().Equals(t9uGbFlag))
                    {
                        t9uGbFlag = dtT90UniCode.Rows[i]["gpflag"].ToString().Trim();
                        break;
                    }
                }
                return new T940UnicodeInfo
                {
                    Region = dtT90UniCode.Rows[0]["region"].ToString().Trim(),
                    MsgFlag = t9uMsgFlag,
                    GpFlag = t9uGbFlag,
                    CustomerGroup = customerGroup_D,
                    DeliveryNo = DeliveryNo,
                    LineItem = LineItem,
                    ShipCntyCode = dtT90UniCode.Rows[0]["shipcntycode"].ToString().Trim()
                };
            }
            else
            {
                throw new Exception("T940Unicode没有获得资料,请联系IT-PPS!".TL());
            }
        }

        private bool isMultiOrSignleCustSoNoByDeliveryNo(string deliveryNo)
        {
            bool flag = false;
            DataTable dt = new DataTable();
            dt = common.isMultiOrSignleCustSoNoByDeliveryNo(deliveryNo);
            if (Convert.ToInt32(dt.Rows[0]["checkCustSo"].ToString()) > 1)
            {
                flag = true;
            }
            return flag;
        }

        private void cmbPDF_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvNo.DataSource = null;
        }

        private void btnInit_Click(object sender, EventArgs e)
        {

        }
    }
}
