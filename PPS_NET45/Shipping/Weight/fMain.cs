using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Reverse;
using System.Text.RegularExpressions;
using System.IO.Ports;
using SajetClass;
using ClientUtilsDll.Forms;
using ClientUtilsDll;
using LibHelper;

namespace Weight
{
    public partial class fMain : Form
    {
        [DllImport("kernel32.dll")]
        public static extern int WinExec(string exeName, int operType);

        DataTable DtResult = new DataTable();
        //称重重量
        string StrWeight = string.Empty;

        string strLocalMACADDRESS = string.Empty;
        private SerialPort serialPort = null; //new SerialPort();
        /// <summary>
        /// 是否用水晶报表打印
        /// </summary>
        private bool isPrintCry = false;
        public fMain()
        {
            InitializeComponent();
        }
        private void fMain_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " (" + SajetCommon.g_sFileVersion + ")";
            labShow.Text = labShow.Text + " (" + SajetCommon.g_sFileVersion + ")";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            strLocalMACADDRESS = LibHelper.LocalHelper.getMacAddr_Local();
            //rdoWagonBalance.CheckedChanged -= new System.EventHandler(rdoWagonBalance_CheckedChanged);
            WeightBll wb = new WeightBll();
            int BalanceType = wb.getLocalBalance();
            if (BalanceType == 1)
            {
                rdoWagonBalance.Checked = true;
                rdoBluetooth.Checked = false;
                rdoCOM.Checked = false;
                rdoCOMBluetooth.Checked = false;
            }
            else if (BalanceType == 2)
            {
                rdoWagonBalance.Checked = false;
                rdoBluetooth.Checked = true;
                rdoCOM.Checked = false;
                rdoCOMBluetooth.Checked = false;
            }
            else if (BalanceType == 3)
            {
                rdoWagonBalance.Checked = false;
                rdoBluetooth.Checked = false;
                rdoCOM.Checked = true;
                rdoCOMBluetooth.Checked = false;
                InitSerialPort("COM4");

            }
            else if (BalanceType == 4)
            {
                rdoWagonBalance.Checked = false;
                rdoBluetooth.Checked = false;
                rdoCOM.Checked = false;
                rdoCOMBluetooth.Checked = true;
                InitSerialPort("COM3");
            }
            //rdoWagonBalance.CheckedChanged += new System.EventHandler(rdoWagonBalance_CheckedChanged);

            txtPalletNo.Focus();
            txtPalletNo.Select();
            //txtWeight.ReadOnly = true;
        }
        /// <summary>
        /// 点击回车或者扫描枪自带回车输入，开始检查栈板号。 
        /// </summary>
        private void txtPalletNo_KeyDown(object sender, KeyEventArgs e)
        {
            this.txtWeight.Text = "";
            string StrPalletNo = txtPalletNo.Text.Trim().ToUpper();
            txtStandard.Text = "";
            txtUpperWeight.Text = "";
            txtLowerWeight.Text = "";
            lbl_WeightValue.Text = "0.00";
            lblRegion.Text = "";
            lblSecurity.Text = "";
            dgvShipment.DataSource = null;

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (string.IsNullOrEmpty(StrPalletNo))
                {
                    return;
                }
                if (StrPalletNo.StartsWith("=") || StrPalletNo.StartsWith("G.W") || StrPalletNo.StartsWith("N.W") || StrPalletNo.StartsWith("T.W") || StrPalletNo.StartsWith("NO"))
                {
                    txtPalletNo.Text = "";
                    return;
                }
                else if (StrPalletNo.StartsWith("NO.") || StrPalletNo.StartsWith("G  +") || StrPalletNo.StartsWith("N  +") || StrPalletNo.StartsWith("T  +") || StrPalletNo.StartsWith("PT +") || StrPalletNo.StartsWith("U/W") || StrPalletNo.StartsWith("Q"))
                {
                    txtPalletNo.Text = "";
                    return;
                }
                txtMessage.BackColor = Color.Blue;
                txtMessage.Text = "";
                // HYQ：20181116
                //添加QHold 检查
                //ReverseBll.CheckHold(string Sno, string Type, out string errorMessage)
                //Type有: 'SHIPMENT', 'PICKPALLETNO', 'PACKPALLETNO', 'SERIALNUMBER'
                #region
                string errorMessage = "";
                bool CheckHoldOK = ReverseBll.CheckHold(StrPalletNo, "PACKPALLETNO", out errorMessage);
                if (!CheckHoldOK)
                {
                    ShowMsg(errorMessage, 0);
                    txtPalletNo.Text = "";
                    txtPalletNo.Focus();
                    return;
                }
                #endregion

                /// <summary>
                /// HYQ：检查站别
                /// </summary>
                /// 
                string strSQL = string.Format(@"
                                    select a.customer_sn,
                                           a.part_no,
                                           a.wc wc,
                                           a.pack_pallet_no,
                                           b.pallet_no,
                                           b.weight,
                                           b.shipment_id,
                                           c.region,
                                           b.SECURITY
                                      from ppsuser.t_shipment_pallet b
                                      join ppsuser.t_sn_status a
                                        on a.pack_pallet_no = b.pallet_no
                                      join ppsuser.t_shipment_info c
                                        on b.shipment_id = c.shipment_id
                                     where b.pallet_no = '{0}'", StrPalletNo);

                DataSet sDataSet = ClientUtils.ExecuteSQL(strSQL);
                dgvPalletinfo.Visible = false;
                if (sDataSet.Tables[0].Rows.Count > 0)
                {
                    #region
                    string weightStationCheck = string.Empty;
                    //string currentPalletWeight = "";
                    this.dgvPalletinfo.Rows.Clear();
                    this.dgvWeightInfo.Rows.Clear();
                    int j = 0;
                    /// HYQ：检查站别WC
                    for (int i = 0; i < sDataSet.Tables[0].Rows.Count; i++)
                    {
                        string weightStation = sDataSet.Tables[0].Rows[i]["wc"].ToString();
                        weightStation = weightStation.Trim();
                        string shipmentid = sDataSet.Tables[0].Rows[i]["shipment_id"].ToString();
                        shipmentid = shipmentid.Trim();

                        if (i == 0)
                        {
                            lblRegion.Text = sDataSet.Tables[0].Rows[i]["region"].ToString();
                            lblSecurity.Text = sDataSet.Tables[0].Rows[i]["SECURITY"].ToString();
                        }

                        if (!weightStation.Equals("W3"))
                        {
                            dgvPalletinfo.Visible = true;
                            j = this.dgvPalletinfo.Rows.Add();
                            this.dgvPalletinfo.Rows[j].Cells[0].Value = sDataSet.Tables[0].Rows[i]["customer_sn"].ToString();
                            this.dgvPalletinfo.Rows[j].Cells[1].Value = sDataSet.Tables[0].Rows[i]["part_no"].ToString();
                            this.dgvPalletinfo.Rows[j].Cells[2].Value = sDataSet.Tables[0].Rows[i]["wc"].ToString();
                            this.dgvPalletinfo.Rows[j].Cells[2].Style.BackColor = Color.Red;
                            this.dgvPalletinfo.Rows[j].Cells[3].Value = sDataSet.Tables[0].Rows[i]["pack_pallet_no"].ToString();
                            this.dgvPalletinfo.Rows[j].Cells[4].Value = sDataSet.Tables[0].Rows[i]["pallet_no"].ToString();
                            this.dgvPalletinfo.Rows[j].Cells[5].Value = sDataSet.Tables[0].Rows[i]["weight"].ToString();
                            this.dgvPalletinfo.Rows[j].Cells[6].Value = sDataSet.Tables[0].Rows[i]["shipment_id"].ToString();
                            //a.customer_sn,a.part_no ,a.wc ,a.pack_pallet_no ,b.pallet_no ,b.weight,b.shipment_id
                            weightStationCheck = "存在异常站别";

                        }

                        //if (sDataSet.Tables[0].Rows[i]["WEIGHT"].ToString().Trim() != null)
                        //{
                        //    currentPalletWeight = sDataSet.Tables[0].Rows[i]["WEIGHT"].ToString();
                        //}

                    }

                    if (weightStationCheck.Equals("存在异常站别"))
                    {
                        //MessageBox.Show("输入的栈板号: " + StrPalletNo + "中存在序号站别异常,不能做weight作业");
                        string errmessage = "输入的栈板号: " + StrPalletNo + "中存在序号站别异常,不能做weight作业";
                        Show_Message(errmessage, 0);
                        txtPalletNo.Text = "";
                        return;
                    }
                    #endregion
                    else
                    {
                        /// <summary>
                        /// HYQ：获取标准重量  ppsuser.SP_WEIGHT_CHECKPALLETSTATUS(
                        /// packpalletno in  varchar2,
                        /// standardwight out varchar2,
                        /// errmsg out varchar2
                        /// )
                        /// </summary>

                        string Strstandardwight = string.Empty;
                        object[][] procParams = new object[3][];
                        string errormsg = "";
                        procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "packpalletno", StrPalletNo };
                        procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "standardwight", Strstandardwight };
                        procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", errormsg };
                        DataTable dt = new DataTable();
                        try
                        {
                            dt = ClientUtils.ExecuteProc("ppsuser.SP_WEIGHT_CHECKPALLETSTATUS", procParams).Tables[0];
                        }
                        catch (Exception e1)
                        {
                            Show_Message(e1.ToString(), 0);
                            return;
                        }
                        #region
                        if (dt.Rows[0]["errmsg"].ToString().Contains("OK"))
                        {
                            txtStandard.Text = dt.Rows[0]["standardwight"].ToString();
                            txtPalletNo.ReadOnly = true;

                            WeightBll wb1 = new WeightBll();
                            wb1.ShowShipmentPalletInfo(StrPalletNo, dgvShipment);
                            //20200415增加SSCC18 检查
                            //检查是否有SSCC18的箱号

                            bool IsExistSSCC18Label = wb1.CheckExistSSCC18Label(StrPalletNo);

                            if (IsExistSSCC18Label)
                            {
                                txtSSCC.Text = "";
                                txtSSCC.Enabled = true;
                                txtSSCC.ReadOnly = false;
                                txtSSCC.Focus();
                                txtSSCC.SelectAll();

                            }
                            else
                            {
                                Show_Message("栈板序号检查PASS，请将栈板放在称上，进行称重......", -1);
                                txtWeight.Text = "";
                                txtWeight.Enabled = true;
                                txtWeight.ReadOnly = false;
                                txtWeight.Focus();
                                txtWeight.SelectAll();
                                txtSSCC.Enabled = false;

                                if (rdoCOM.Checked)
                                {
                                    OpenSerialPortTransDataToTxtWeight("COM4");
                                }
                                else if (rdoCOMBluetooth.Checked)
                                {
                                    OpenSerialPortTransDataToTxtWeight("COM3");
                                }
                            }
                        }
                        else if (dt.Rows[0]["errmsg"].ToString().Contains("NG"))
                        {
                            Show_Message(dt.Rows[0]["errmsg"].ToString(), 0);
                            txtPalletNo.Text = "";
                            txtPalletNo.Focus();
                            return;
                        }
                        else
                        {
                            Show_Message("检查PACKPALLETNO获得特殊异常", 0);
                            txtPalletNo.Text = "";
                            txtPalletNo.Focus();
                            return;
                        }

                    }
                    #endregion

                }
                else
                {
                    if (!string.IsNullOrEmpty(StrPalletNo))
                    {
                        string errmessage = "输入的栈板号: " + StrPalletNo + ",无栈板相关信息!";
                        txtPalletNo.SelectAll();
                        txtPalletNo.Focus();
                        Show_Message(errmessage, 0);
                    }
                    else
                    {
                        txtPalletNo.Focus();
                        txtMessage.Text = "";
                        txtMessage.BackColor = Color.Blue;
                    }
                }
                //HYQ :检查站别过了， 鼠标光标转到称重的txtweight 的TextBox，转到称重的处理。
            }
        }
        /// <summary>
        /// HYQ：插入栈板的称重记录
        /// </summary>
        /// 
        private void insertPalletWeightLog(string shipmentid, string palletno, double weightvalue, string staweight, string dvalueweight, double upperweight, double lowerweight, string percentdec, string pass)
        {
            try
            {
                //HYQ：先分3个方法写update update insert，后面给位一个SP处理
                //insert into ppsuser.t_pallet_weight_log
                //values(shipmentid, palletno, weight, staweight, dvalueweight, upperweight, lowerweight, percentdec, cdt,"1")
                //values('ADSS_20180821_00001', '201808000005674', '102.02', '100', 2.02, 103, 97, 3, sysdate)
                string stringupdateweight = @"insert into ppsuser.t_pallet_weight_log 
                (shipment_id,pallet_no,weight,standard_weight,dvalue_weight,upper_weight,lower_weight,per_devweight,cdt,pass)            
                values(:shipment_id , :pallet_no , :weight , :sta_weight , :dvalue_weight , :upper_weight , :lower_weight, :percent_dec , sysdate , :ifpass)";

                object[][] Param = new object[9][];
                Param[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "weight", weightvalue };
                Param[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PALLET_NO", palletno };
                Param[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipment_id", shipmentid };
                Param[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "sta_weight", staweight };
                Param[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "dvalue_weight", dvalueweight };
                Param[5] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "upper_weight", upperweight };
                Param[6] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "lower_weight", lowerweight };
                Param[7] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "percent_dec", percentdec };
                Param[8] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ifpass", pass };
                ClientUtils.ExecuteSQL(stringupdateweight.ToString(), Param);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                LibHelper.MediasHelper.PlaySoundAsyncByConts();
                lbl_WeightValue.Text = "";
                InitializeComponent();
                Show_Message("数据库连接处理insertPalletWeightLog异常，请检查网络!" + ex, 0);
                return;
            }
        }

        public DialogResult ShowMsg(string strText, int intType)
        {
            txtMessage.Text = strText.TP();
            switch (intType)
            {
                case 0: //Error                
                    txtMessage.ForeColor = Color.Red;
                    txtMessage.BackColor = Color.Silver;
                    return DialogResult.None;
                case 1: //Warning                        
                    txtMessage.ForeColor = Color.Blue;
                    txtMessage.BackColor = Color.FromArgb(255, 255, 128);
                    return DialogResult.None;
                case 2: //Confirm
                    return MessageBox.Show(strText, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    txtMessage.ForeColor = Color.Green;
                    txtMessage.BackColor = Color.White;
                    return DialogResult.None;
            }
        }


        public string getWeight(string weight)
        {
            //地磅格式处理：
            // ==================
            //G.W.:    50.5kg
            //N.W.:    50.5kg
            //T.W.:     0.0kg
            //NO. :      0000
            string weight1 = weight.ToUpper().Trim();
            if (weight1.Contains("KG"))
            {
                Regex regex = new Regex("G.W.:(?<weight>[\\s\\S]*?)KG", RegexOptions.IgnoreCase);
                MatchCollection matchCollection = regex.Matches(weight1);
                if (matchCollection.Count > 0)
                {
                    Match matchItem = matchCollection[0];
                    string weight2 = matchItem.Groups["weight"].Value;
                    weight2 = weight2.Trim();
                    return weight2;
                }
                else
                {
                    weight1 = "0";
                    return weight1;
                }
            }
            return weight1;
        }
        public string getCOMWeight(string weight)
        {
            //NO.        0
            //G + 75.5  kg
            //N + 75.5  kg
            //T + 0.0  kg
            //PT + 0.0  kg
            //U / W        0  kg
            //Q          0 pcs

            string weight1 = weight.ToUpper().Trim();
            if (weight1.Contains(@"N  +"))
            {
                Regex regex = new Regex("N  \\+(?<weight>[\\s\\S]*?)KG", RegexOptions.IgnoreCase);
                MatchCollection matchCollection = regex.Matches(weight1);
                if (matchCollection.Count > 0)
                {
                    Match matchItem = matchCollection[0];
                    string weight2 = matchItem.Groups["weight"].Value.Trim();
                    weight2 = weight2.Trim();
                    return weight2;
                }
                else
                {
                    weight1 = "0";
                    return weight1;
                }
            }
            return weight1;
        }

        public string getCOMBluetoothWeight(string weight)
        {
            //ST,NT,=   71.5kg
            //string weight1 = weight.ToUpper().Replace("ST,NT,+", "").Replace("KG", "").Trim();
            string[] strArray = weight.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i].ToUpper().Contains("KG"))
                {
                    return strArray[i].ToUpper().Replace("KG", "").Trim();
                }
            }
            return "0";
        }

        /// <summary>
        /// 刷新当前明细中的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvWeightInfo_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //显示在HeaderCell上
            for (int i = 0; i < this.dgvWeightInfo.Rows.Count; i++)
            {
                DataGridViewRow r = this.dgvWeightInfo.Rows[i];
                r.HeaderCell.Value = string.Format("{0}", i + 1);
            }
            this.dgvWeightInfo.Refresh();
        }

        private void btn_RePrint_Click(object sender, EventArgs e)
        {
            txtMessage.Text = "";
            txtMessage.BackColor = Color.Blue;
            fCheck fcheck = new fCheck();
            if (fcheck.ShowDialog() != DialogResult.OK)
            {
                ShowMsg("账号权限不正确", 0);
                return;
            }
            else
            {
                fWeightPrint pr = new fWeightPrint();
                pr.ShowDialog();
            }
        }


        /// <summary>
        /// HYQ：依据标准重量textbox 的值，以及偏差，自动显示上下限值。
        /// 如果以后上下限值有获取方法，这部分取消。
        /// </summary>
        private void txtStandard_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtStandard.Text != "")
                {
                    double intsun = Convert.ToDouble(txtStandard.Text);
                    //计算上限和下限重量 
                    if (intsun > 0)
                    {
                        txtUpperWeight.Text = (intsun + intsun * 0.03).ToString("f3");
                        txtLowerWeight.Text = (intsun - intsun * 0.03).ToString("f3");
                    }
                    txtMessage.BackColor = Color.Blue;
                    txtMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                txtStandard.Text = "";
                txtUpperWeight.Text = "";
                txtLowerWeight.Text = "";
                Show_Message(ex.ToString(), 0);
                return;
            }
        }

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPalletNo.Text.Trim()))
            {
                this.txtWeight.Text = "";
            }
            if (string.IsNullOrEmpty(this.txtWeight.Text.Trim()))
            {
                return;
            }
            string StrWeight = string.Empty;
            lbl_WeightValue.Text = "0.00";

            int linesum = txtWeight.Lines.Count();
            if (rdoWagonBalance.Checked)
            {
                if (linesum == 21 || linesum == 19 || linesum == 20)
                {
                    #region
                    string strContext = this.txtWeight.Text;
                    string[] str = this.txtWeight.Lines;
                    for (int i = 0; i < str.Length; i++)
                    {
                        //Regex.Replace(input, "\s", "");
                        if (str[i].Contains("G.W"))      //统计有效行数
                        {
                            StrWeight = getWeight(str[i]);
                            /// <summary>
                            ///HYQ：获得实际重量 比对 重量范围
                            /// </summary>
                            lbl_WeightValue.Text = StrWeight;

                            ComWeightSub();

                        }


                    }
                    txtSSCC.Enabled = false;
                    txtSSCC.Text = "";
                    txtWeight.Enabled = false;
                    txtWeight.Text = "";
                    txtWeight.ReadOnly = true; ;
                    txtPalletNo.Enabled = true;
                    txtPalletNo.ReadOnly = false;
                    txtPalletNo.Text = "";
                    txtPalletNo.Focus();
                    txtPalletNo.SelectAll();
                    //txtPalletNo.Text = "";
                    //txtWeight.Text = "";
                    //txtPalletNo.ReadOnly = false;
                    //txtPalletNo.Focus();
                    #endregion
                }
            }
            else if (rdoBluetooth.Checked)
            {
                if (linesum == 2)
                {
                    #region

                    string strContext = this.txtWeight.Text;
                    string[] str = this.txtWeight.Lines;
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str[i].Trim().Length > 0)
                        {
                            StrWeight = str[i].Trim();
                            lbl_WeightValue.Text = StrWeight;
                            ComWeightSub();

                        }


                    }
                    txtSSCC.Enabled = false;
                    txtSSCC.Text = "";
                    txtWeight.Enabled = false;
                    txtWeight.Text = "";
                    txtWeight.ReadOnly = true; ;
                    txtPalletNo.Enabled = true;
                    txtPalletNo.ReadOnly = false;
                    txtPalletNo.Text = "";
                    txtPalletNo.Focus();
                    txtPalletNo.SelectAll();
                    //txtPalletNo.Text = "";
                    //txtWeight.Text = "";
                    //txtPalletNo.ReadOnly = false;
                    //txtPalletNo.Focus();
                    #endregion
                }
            }

            else if (rdoCOM.Checked)
            {
                #region
                string strContext = this.txtWeight.Text.ToUpper();
                if (!strContext.ToUpper().Contains("PCS"))
                {
                    return;
                }
                string[] str = this.txtWeight.Lines;
                for (int i = 0; i < str.Length; i++)
                {
                    //Regex.Replace(input, "\s", "");
                    if (str[i].Contains(@"N  +"))      //统计有效行数
                    {
                        StrWeight = getCOMWeight(str[i]);
                        lbl_WeightValue.Text = StrWeight;
                        ComWeightSub();

                    }


                }
                txtSSCC.Enabled = false;
                txtSSCC.Text = "";
                txtWeight.Enabled = false;
                txtWeight.Text = "";
                txtWeight.ReadOnly = true; ;
                txtPalletNo.Enabled = true;
                txtPalletNo.ReadOnly = false;
                txtPalletNo.Text = "";
                txtPalletNo.Focus();
                txtPalletNo.SelectAll();
                //txtPalletNo.Text = "";
                //txtWeight.Text = "";
                //txtPalletNo.ReadOnly = false;
                //txtPalletNo.Focus();
                #endregion
            }
            else if (rdoCOMBluetooth.Checked)
            {
                string strContext = this.txtWeight.Text.ToUpper();
                if (!strContext.ToUpper().Contains("KG"))
                {
                    return;
                }
                string[] str = this.txtWeight.Lines;
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i].ToUpper().Contains(@"KG"))      //统计有效行数
                    {
                        StrWeight = getCOMBluetoothWeight(str[i]);
                        lbl_WeightValue.Text = StrWeight;
                        ComWeightSub();
                        break;
                    }
                }
                txtSSCC.Enabled = false;
                txtSSCC.Text = "";
                txtWeight.Enabled = false;
                txtWeight.Text = "";
                txtWeight.ReadOnly = true; ;
                txtPalletNo.Enabled = true;
                txtPalletNo.ReadOnly = false;
                txtPalletNo.Text = "";
                txtPalletNo.Focus();
                txtPalletNo.SelectAll();
                //txtPalletNo.Text = "";
                //txtWeight.Text = "";
                //txtPalletNo.ReadOnly = false;
                //txtPalletNo.Focus();
            }
        }
        private void ComWeightSub()
        {

            CloseSerialPort();
            string StrPalletNo = txtPalletNo.Text.Trim();
            //string strSQL = @"select a.customer_sn,a.pack_pallet_no,b.pallet_no,b.shipment_id from ppsuser.t_sn_status a  join  ppsuser.t_shipment_pallet b on a.pack_pallet_no =b.pallet_no where b.pallet_no='" + StrPalletNo + "'";

            string strSQL = string.Format(@"
                                select a.customer_sn, a.pack_pallet_no, b.pallet_no, b.shipment_id
                                  from ppsuser.t_sn_status a
                                  join ppsuser.t_shipment_pallet b
                                    on a.pack_pallet_no = b.pallet_no
                                 where b.pallet_no = '{0}'
                                ", StrPalletNo);


            DataSet sDataSet = ClientUtils.ExecuteSQL(strSQL);
            dgvPalletinfo.Visible = false;
            if (sDataSet.Tables[0].Rows.Count > 0)
            {
                string shipmenid2 = sDataSet.Tables[0].Rows[0]["shipment_id"].ToString();
                try
                {
                    double WeightValue = Convert.ToDouble(lbl_WeightValue.Text);
                    double UpperWeight = Convert.ToDouble(txtUpperWeight.Text);
                    double LowerWeight = Convert.ToDouble(txtLowerWeight.Text);
                    string weightcvalue = Math.Abs(WeightValue - Convert.ToDouble(txtStandard.Text)).ToString("f3");

                    object[][] procParams = new object[9][];
                    string errormsg = "";

                    procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "packpalletno", StrPalletNo };
                    procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "standardwight", txtStandard.Text };
                    procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Double, "weightvalue", WeightValue };
                    procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "dvalueweight", weightcvalue };
                    procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Double, "upperweight", UpperWeight };
                    procParams[5] = new object[] { ParameterDirection.Input, OracleDbType.Double, "lowerweight", LowerWeight };
                    procParams[6] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "percentdec", txtDeviation.Text };
                    procParams[8] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", errormsg };
                    DataTable dt = new DataTable();


                    if ((WeightValue <= UpperWeight) && (LowerWeight <= WeightValue))
                    {
                        #region

                        dgvWeightInfo.ColumnCount = 6;
                        dgvWeightInfo.Rows.Add();
                        dgvWeightInfo.Columns[0].HeaderText = "ShipMent_id";
                        dgvWeightInfo.Columns[1].HeaderText = "栈板号";
                        dgvWeightInfo.Columns[2].HeaderText = "标准重量";
                        dgvWeightInfo.Columns[3].HeaderText = "实际重量";
                        dgvWeightInfo.Columns[4].HeaderText = "差异值";
                        dgvWeightInfo.Columns[5].HeaderText = @"差异(%)";
                        dgvWeightInfo.Rows.Insert(0, shipmenid2, StrPalletNo, txtStandard.Text, WeightValue, weightcvalue, (Convert.ToDouble(weightcvalue) / Convert.ToDouble(txtStandard.Text) * 100).ToString("f2"));

                        /// <summary>
                        ///HYQ： 更新记录，跳站,打印标签
                        /// </summary>
                        /// 

                        procParams[7] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "pass", "1" };
                        try
                        {
                            dt = ClientUtils.ExecuteProc("ppsuser.SP_WEIGHT_INSERTPALLETWEIGHT", procParams).Tables[0];
                        }
                        catch (Exception e1)
                        {
                            Show_Message(e1.ToString(), 0);
                            return;
                        }

                        if (dt.Rows[0]["errmsg"].ToString().Contains("OK"))
                        {
                            //HYQ: 获取打印清单数据
                            DSPalletSheetlabel_multi dspslm = new DSPalletSheetlabel_multi();
                            int labellistrows = 8;
                            string pages = "ALL";
                            if (isPrintCry)
                            {
                                CRReport.CRMain cr = new CRReport.CRMain();
                                cr.PalletLoadingSheet(StrPalletNo, true, false, "");
                            }
                            else
                            {
                                if (dspslm.PrintPalletLabel(StrPalletNo, labellistrows, pages))
                                {
                                    lbl_WeightValue.Text = "0.00";
                                    Show_Message("打印OK", -1);
                                }
                                else
                                {
                                    lbl_WeightValue.Text = "0.00";
                                    Show_Message("打印连接出了问题", 0);
                                }
                            }

                            WeightBll pb1 = new WeightBll();
                            //如果此集货单的所有的栈板都称重完成，且是PAC的，就打印Handover manifest

                            string strResultShipment = string.Empty;
                            string strResultRegion = string.Empty;
                            pb1.CheckShipmentWeightStatus(StrPalletNo, out strResultRegion, out strResultShipment);
                            if (strResultShipment.Equals("OK"))
                            {
                                //OK  说明集货单所有栈板都称重完成，
                                string errormsg2 = string.Empty;

                                string strsShipmentID = pb1.changeSNtoShipmentID(StrPalletNo, out errormsg2);
                                if (errormsg2.Equals("OK"))
                                {
                                    CRReport.CRMain cr = new CRReport.CRMain();
                                    if (strResultRegion.Equals("PAC"))
                                    {
                                        cr.HanDoveMan(strsShipmentID, true, true);
                                    }
                                    else
                                    {
                                        cr.HanDoveMan2(strsShipmentID, true, true, "WEIGHT");
                                    }

                                }

                            }


                            string strResultInsertLog = string.Empty;
                            string strResulterrmsg = string.Empty;

                            strResultInsertLog = pb1.PPSInsertWorkLogBy(StrPalletNo, "WEIGHT", strLocalMACADDRESS, out strResulterrmsg);
                            ClientUtils.WritePrintLabelLog("Pallet Loading Sheet", "", StrPalletNo, LocalHelper.getMacAddr_Local());

                        }
                        else if (dt.Rows[0]["errmsg"].ToString().Contains("NG"))
                        {
                            Show_Message(dt.Rows[0]["errmsg"].ToString(), 0);
                            lbl_WeightValue.Text = "0.00";
                            txtWeight.Text = "";
                            txtWeight.Focus();
                            return;
                        }
                        else
                        {
                            Show_Message("检查PACKPALLETNO获得特殊异常", 0);
                            lbl_WeightValue.Text = "0.00";
                            InitializeComponent();
                            return;
                        }
                        //updateWeightWC(StrPalletNo, "W4");
                        // //insertPalletWeightLog(shipmenid2, StrPalletNo,WeightValue,txtStandard.Text, weightcvalue, UpperWeight, LowerWeight,txtDeviation.Text,"1");
                        // //HYQ：原先insert是在 update Palletweight之后的，把这称重提前，是为了计算REAL_PALLET_NO
                        // //也就是打印在PalletLoadingsheet 上的PalletID。
                        // insertPalletWeightLog(shipmenid2, StrPalletNo, WeightValue, txtStandard.Text, weightcvalue, UpperWeight, LowerWeight, txtDeviation.Text, "1");
                        // updatePalletWeight(StrPalletNo, lbl_WeightValue.Text);
                        #endregion
                    }
                    else
                    {
                        insertPalletWeightLog(shipmenid2, StrPalletNo, WeightValue, txtStandard.Text, weightcvalue, UpperWeight, LowerWeight, txtDeviation.Text, "0");
                        LibHelper.MediasHelper.PlaySoundAsyncByNg();
                        Show_Message("重量超出范围，请重新刷栈板作业", 0);
                        txtWeight.Text = "";
                        txtPalletNo.SelectAll();
                        txtPalletNo.Focus();

                    }

                }
                catch (Exception ex)
                {
                    lbl_WeightValue.Text = "";
                    InitializeComponent();
                    Show_Message(ex.ToString(), 0);

                }
            }
            //if sdataset row <0
            else
            {
                //异常处理， 初始化。
                string errmessage = "输入的栈板号: " + StrPalletNo + ",无栈板相关信息1!";
                Show_Message(errmessage, 0);
            }
        }

        /// <summary>
        /// HYQ:之前高手写的,正则表达式 将称的数据直接转为数值
        /// </summary>
        #region
        //Double douWeight = 0;
        //if (StrWeight.IndexOf("G.W") >= 0)
        //{
        //    StrWeight = getWeight(StrWeight);
        //}
        //else if (Double.TryParse(StrWeight, out douWeight))
        //{
        //    StrWeight = getWeight(StrWeight);
        //}
        //else
        //{
        //    txtWeight.Text = "";
        //    StrWeight = "";
        //    string errmessage = "输入无法解析的重量值。";
        //    Show_Message(errmessage, 0);
        //    return;
        //}
        #endregion

        private void rdoWagonBalance_CheckedChanged(object sender, EventArgs e)
        {
            setLocalBalance();

        }

        private void rdoBluetooth_CheckedChanged(object sender, EventArgs e)
        {
            setLocalBalance();
        }

        private void rdoCOM_CheckedChanged(object sender, EventArgs e)
        {
            setLocalBalance();
        }

        private void rdoCOMBluetooth_CheckedChanged(object sender, EventArgs e)
        {
            setLocalBalance();
        }
        private void setLocalBalance()
        {
            int BalanceType = 0;
            if (rdoWagonBalance.Checked)
            {

                BalanceType = 1;
                CloseSerialPort();
            }
            else if (rdoBluetooth.Checked)
            {
                BalanceType = 2;
                CloseSerialPort();
            }
            else if (rdoCOM.Checked)
            {
                BalanceType = 3;
                InitSerialPort("COM4");
            }
            else if (rdoCOMBluetooth.Checked)
            {
                BalanceType = 4;
                InitSerialPort("COM3");
            }
            WeightBll wb = new WeightBll();
            wb.initLocalBalance(BalanceType);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {

                serialPort.DataReceived += new SerialDataReceivedEventHandler(CommDataReceived); //设置数据接收事件（监听）
                if (!serialPort.IsOpen)
                {
                    //serialPort.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InitSerialPort(string strComPort)
        {
            try
            {
                if ((this.serialPort == null) || (this.serialPort.PortName != strComPort))
                {
                    this.serialPort = new SerialPort();
                    serialPort.PortName = strComPort; //通信端口
                    serialPort.BaudRate = 9600; //串行波特率
                    serialPort.DataBits = 8; //每个字节的标准数据位长度
                    serialPort.StopBits = StopBits.Two; //设置每个字节的标准停止位数
                    serialPort.Parity = Parity.None; //设置奇偶校验检查协议
                    serialPort.Encoding = System.Text.Encoding.GetEncoding("GB2312");
                    serialPort.ReceivedBytesThreshold = 1;
                    serialPort.DataReceived += new SerialDataReceivedEventHandler(CommDataReceived); //设置数据接收事件（监听）
                }
            }
            catch (Exception)
            {
                Show_Message("初始化COM4错误!", -1);
            }

        }
        private void CloseSerialPort()
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    //serialPort.Close();
                }
            }
            catch (Exception)
            {
            }

        }

        private void OpenSerialPortTransDataToTxtWeight(string strCOMPort)
        {
            try
            {
                //serialPort.DataReceived += new SerialDataReceivedEventHandler(CommDataReceived); //设置数据接收事件（监听）
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }
            }
            catch (Exception e)
            {
                Show_Message(string.Format("获得{0}数据错误!", strCOMPort) + e.ToString(), -1);
            }
        }
        public void CommDataReceived(Object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //定义一个字段，来保存串口传来的信息。
                string str = "";
                int len = serialPort.BytesToRead;
                Byte[] readBuffer = new Byte[len];
                serialPort.Read(readBuffer, 0, len);
                str = Encoding.Default.GetString(readBuffer);
                this.txtWeight.Invoke(new Action(() =>
                {
                    txtWeight.Text += str;
                }));
                //serialPort.DiscardInBuffer();  //清空接收缓冲区     
            }
            catch (Exception ex)
            {
                serialPort.Close();
                MessageBox.Show(ex.Message);

            }
        }

        /// <summary>
        /// 输入后，弹出窗口状态信息
        /// </summary>
        private void Show_Message(string msg, int type)
        {
            txtMessage.Text = msg.TP();
            switch (type)
            {
                case 0: //error
                    txtMessage.ForeColor = Color.Red;
                    txtMessage.BackColor = Color.Yellow;
                    break;
                case 1:
                    //LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    txtMessage.ForeColor = Color.Blue;
                    txtMessage.BackColor = Color.White;
                    break;
                default:
                    txtMessage.ForeColor = Color.Black;
                    txtMessage.BackColor = Color.White;
                    break;
            }
        }

        private void btn_RePrintHM_Click(object sender, EventArgs e)
        {
            txtMessage.Text = "";
            txtMessage.BackColor = Color.Blue;
            fCheck fcheck = new fCheck();
            if (fcheck.ShowDialog() != DialogResult.OK)
            {
                ShowMsg("账号权限不正确", 0);
                return;
            }
            else
            {
                rePrintReport pr = new rePrintReport();
                pr.ShowDialog();
            }
        }

        private void txtSSCC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                string StrPalletNo = txtPalletNo.Text.Trim().ToUpper();
                string strSSCC = txtSSCC.Text.Trim().ToUpper();
                WeightBll wb = new WeightBll();
                strSSCC = wb.DelPrefixCartonSN(strSSCC);
                if (string.IsNullOrEmpty(strSSCC) || string.IsNullOrEmpty(StrPalletNo))
                {
                    return;
                }
                bool IsMatch = wb.CheckPalletandSsccMatch(StrPalletNo, strSSCC);
                if (IsMatch)
                {
                    Show_Message("栈板序号检查PASS，请将栈板放在称上，进行称重......", -1);
                    txtWeight.Text = "";
                    txtWeight.Enabled = true;
                    txtWeight.ReadOnly = false;
                    txtSSCC.Enabled = false;
                    txtSSCC.ReadOnly = true;
                    txtPalletNo.Enabled = false;
                    txtPalletNo.ReadOnly = true;
                    txtWeight.Focus();
                    txtWeight.SelectAll();
                    if (rdoCOM.Checked)
                    {
                        OpenSerialPortTransDataToTxtWeight("COM4");
                    }
                    else if (rdoCOMBluetooth.Checked)
                    {
                        OpenSerialPortTransDataToTxtWeight("COM3");
                    }
                }
                else
                {
                    txtPalletNo.Text = "";
                    txtPalletNo.ReadOnly = false;
                    txtPalletNo.Focus();
                    txtPalletNo.SelectAll();
                    txtSSCC.Text = "";
                    txtSSCC.Enabled = false;
                    txtSSCC.ReadOnly = true;
                    Show_Message("栈板号:" + StrPalletNo + "与SSCC号:" + strSSCC + "不匹配", -1);
                }
            }

        }
    }
}




