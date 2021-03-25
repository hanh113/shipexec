using ClientUtilsDll;
using Oracle.ManagedDataAccess.Client;
using SajetClass;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NPIPickListAC
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        string StrData;
        //保存查询出来的PickList用于过滤
        string StrIniFile = Application.StartupPath + "\\sajet.ini";
        private string g_sUserNo = ClientUtils.fLoginUser;
        private string g_sUserID = ClientUtils.UserPara1;
        private string g_ServerIP = ClientUtils.url;
        public int H = 0;

        string strSTATUS = string.Empty;
        string strStime = string.Empty;
        string strEtime = string.Empty;

        private Int32 g_curRow = -1;

        NPIPickListACBLL wb = new NPIPickListACBLL();

        /// <summary>
        /// 设置文本框美式键盘
        /// </summary>
        private void SeachTxt()
        {
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    c.ImeMode = System.Windows.Forms.ImeMode.Disable;
                }
                if (c is ComboBox)
                {
                    c.ImeMode = System.Windows.Forms.ImeMode.Disable;
                }
            }
        }
        private void fMain_Load(object sender, EventArgs e)
        {
            //HYQ:fLoad()  设定了panel2.Size
            fLoad();
            this.Text = this.Text + " (" + SajetCommon.g_sFileVersion + ")";
            labShow.Text = labShow.Text + " (" + SajetCommon.g_sFileVersion + ")";
            SeachTxt();
            DateTime dateTimeNow = DateTime.Now;
            //dt_start.Value = new DateTime(dateTimeNow.Year, dateTimeNow.Month - 2, 1);
            dt_start.Value = dateTimeNow.AddDays(-1);
            dt_end.Value = dateTimeNow.AddDays(1);
            this.WindowState = FormWindowState.Maximized;
            SajetInifile sajetInifile1 = new SajetInifile();
            StrData = sajetInifile1.ReadIniFile(StrIniFile, "System", "Data", "Prod").ToLower();



            //填充单号状态信息
            string strSql2 = string.Format(@"
                select 'ALL' id ,'ALL' name from dual
                union 
                select 'WP' id ,'WP-未开始' name from dual
                union 
                select 'IP' id ,'IP-作业中' name from dual
                union 
                select 'FP' id ,'FP-已完成' name from dual
                union 
                select 'CP' id ,'CP-CANCEL' name from dual
                union
                select 'HO' id ,'HO-HOLD' name from dual
                ");
            fillCmb(strSql2, "", cmbSTATUS);

        }

        private void fLoad()
        {
            H = Screen.PrimaryScreen.Bounds.Height;
            if (H >= 1080)
            {
                H = Convert.ToInt32(H * 0.30);
            }
            else
            {
                H = Convert.ToInt32(H * 0.22);

            }
            this.panel2.Size = new System.Drawing.Size(1300, H);
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            string strSID = string.Empty;
            strSID = cmbSID.Text;
            strSTATUS = cmbSTATUS.SelectedValue.ToString();
            strStime = dt_start.Value.ToString("yyyy-MM-dd");
            string strEtime = dt_end.Value.ToString("yyyy-MM-dd");
            btnSearch.Enabled = false;

            dgvNo.DataSource = null;
            dgvNo.Rows.Clear();
            DataTable dtSAPIDList = wb.GetNPISIDDataTable(strSID, strSTATUS, strStime, strEtime);
            dgvNo.DataSource = dtSAPIDList;
            refreshCmbbox();
            ShowMsg("",0);
            btnSearch.Enabled = true;
        }

        private void refreshCmbbox()
        {
            cmbSID.Items.Clear();
            cmbSID.Items.Add("ALL");
            //cmbRegion.Items.Clear();
            //cmbRegion.Items.Add("-ALL-");

            for (int i = 0; i < dgvNo.RowCount; i++)
            {
                string strSIDStatus = dgvNo.Rows[i].Cells["status"].Value.ToString();
                string strSID = dgvNo.Rows[i].Cells["shipment_id"].Value.ToString();
                if (strSIDStatus.Contains("WP"))
                {
                    dgvNo.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                else if (strSIDStatus.Contains("IP"))
                {
                    dgvNo.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else if (strSIDStatus.Contains("FP"))
                {
                    dgvNo.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }
                if (!cmbSID.Items.Contains(strSID))
                {
                    cmbSID.Items.Add(strSID);
                }
            }

        }


        private void fillCmb(string strSQL, string colName, ComboBox cmb)
        {

            DataSet dts = ClientUtils.ExecuteSQL(strSQL);
            if (dts.Tables[0].Rows.Count > 0)
            {

                //return DtbPickList;
                //绑定单据号
                //List<string> shipmentList = (from d in dts.Tables[0].AsEnumerable()
                //                             select d.Field<string>(colName)
                //                           ).Distinct()
                //                           .ToList();
                //shipmentList.Add("");
                //shipmentList.Sort();
                cmb.DataSource = dts.Tables[0];
                cmb.ValueMember = "id";
                cmb.DisplayMember = "name";

            }
            else
            {
                cmb.Items.Clear();
            }
            //selecttxtCarton();
        }

        private void dgvNo_SelectionChanged(object sender, EventArgs e)
        {
            dgvPickNO.DataSource = null;
            dgvStock.DataSource = null;

            //集货清单选中的事件
            //1. 更新库存清单显示逻辑
            //2. 更新需作业集货单号、栈板号等显示内容
            Int32 rowIndex = 0;
            try
            {
                rowIndex = dgvNo.CurrentRow.Index;
                //rowIndex = dgvNo.CurrentCell.RowIndex;
            }
            catch (Exception)
            {
                return;
            }
            if (dgvNo.CurrentRow.Index >= 0)
            {
                //1.1 同一行，则返回
                //if (g_curRow == rowIndex)
                //    return;
                g_curRow = rowIndex;

                string strSID = dgvNo.Rows[g_curRow].Cells["shipment_id"].Value.ToString();

                txtSID.Text = strSID;
                ShowMsg("",0);
                //2 更新作业显示内容
                //pb.ShowStockInfo(setPart, dgvStock);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            TextMsg.Text = "";

            //1. 检查
            //获得电脑名
            string localHostname = "";
            try
            {
                localHostname = System.Environment.MachineName;
            }
            catch (Exception ex)
            {
                ShowMsg("获取电脑名异常" + ex.ToString(), 0);
                return;
            }
            //检查必须要选择一个集货单号
            if (string.IsNullOrEmpty(txtSID.Text.Trim()))
            {
                ShowMsg("请选择一个需作业的SAP单号。", 0);
                return;
            }

            string strSID = txtSID.Text;
            string strResultOUT = string.Empty;
            string strRB = wb.NPICheckSID(strSID, localHostname, out strResultOUT);
            if (strRB.Equals("OK"))
            {
                txtCarton.Enabled = false;
                txtCarton.Focus();
            }
            else if (strRB.Equals("NG"))
            {

                ShowMsg(strResultOUT, 0);
                return;
            }
            else if (strRB.Equals("WA"))
            {
                DialogResult strResult = MessageBox.Show(strResultOUT.Substring(3) + ",是否继续作业？", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (strResult == DialogResult.No)
                {
                    return;
                }
            }



            //给dgvPickNO写值, 刷新界面
            refresh_dgvPickNew(strSID);

            /// 2.锁定程式界面上半部分的功能>> 按钮失效;datagriadview 选择index 事件失效;...
            btnSearch.Enabled = false;
            btnClsFace.Enabled = false;
            btnReprint.Enabled = false;
            this.dgvNo.SelectionChanged -= new System.EventHandler(this.dgvNo_SelectionChanged);

            txtPalletNO.Text = "";
            txtCarton.Enabled = true;
            txtCarton.Text = "";
            txtCarton.SelectAll();
            txtCarton.Focus();
            /// 3.【结束作业】 按钮启用。
            btnStart.Enabled = false;
            btnEnd.Enabled = true;



        }

        private void refresh_dgvPickNew(string strSID )
        {
            dgvPickNO.DataSource = null;
            dgvPickNO.Rows.Clear();
            DataTable pickdt = wb.GetSIDLINEINFO(strSID);
            int sumsapqty = 0;
            int sumpickqty = 0;
            if (pickdt.Rows.Count > 0)
            {
                for (int i = 0; i < pickdt.Rows.Count; i++)
                {
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dgvPickNO.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    dr.HeaderCell.Value = (i + 1).ToString();
                    dr.Cells[0].Value = pickdt.Rows[i]["shipment_id"].ToString();
                    dr.Cells[1].Value = pickdt.Rows[i]["delivery_no"].ToString();
                    dr.Cells[2].Value = pickdt.Rows[i]["line_item"].ToString();
                    dr.Cells[3].Value = pickdt.Rows[i]["mpn"].ToString();
                    dr.Cells[4].Value = pickdt.Rows[i]["ictpn"].ToString();
                    dr.Cells[5].Value = pickdt.Rows[i]["status"].ToString();
                    dr.Cells[6].Value = pickdt.Rows[i]["qty"].ToString();
                    dr.Cells[7].Value = pickdt.Rows[i]["carton_qty"].ToString();
                    dr.Cells[8].Value = pickdt.Rows[i]["pack_qty"].ToString();
                    dr.Cells[9].Value = pickdt.Rows[i]["pack_carton_qty"].ToString();
                    if (pickdt.Rows[i]["STATUS"].ToString().Contains("WP"))
                    {
                        dr.DefaultCellStyle.BackColor = Color.White;
                    }
                    else if (pickdt.Rows[i]["STATUS"].ToString().Contains("IP"))
                    {
                        dr.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else if (pickdt.Rows[i]["STATUS"].ToString().Contains("FP"))
                    {
                        dr.DefaultCellStyle.BackColor = Color.Green;
                    }
                    try
                    {
                        dgvPickNO.Invoke((MethodInvoker)delegate ()
                        {
                            dgvPickNO.Rows.Add(dr);

                        });
                    }
                    catch (Exception e1)
                    {
                        ShowMsg(e1.ToString(), 0);
                        return;
                    }
                    sumsapqty += Convert.ToInt32(dr.Cells["QTY"].Value);
                    sumpickqty += Convert.ToInt32(dr.Cells["pack_qty"].Value);
                }
                labqty.Text = sumpickqty.ToString() + "/" + sumsapqty.ToString();
                labqty.Refresh();
            }
            else
            {
                labqty.Text = "00/00";
            }
        }



        

        private void txtCarton_KeyDown(object sender, KeyEventArgs e)
        {
            DateTime dateStart = DateTime.Now;
            //可扫描序号/箱号/栈板号进行Pick
            string strCarton = txtCarton.Text.Trim();
            strCarton = wb.DelPrefixCartonSN(strCarton);

            if (e.KeyCode != Keys.Enter)
            { return; }
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strCarton))
            {
                //LibHelper.MediasHelper.PlaySoundAsyncByNg();
                //this.ShowMsg("输入的/PalletID/SN/Carton不能为空！", 0);
                txtCarton.SelectAll();
                txtCarton.Focus();
                return;
            }
            DataTable dt0 = wb.GetSNInfoDataTableBLL(strCarton);
            if (dt0 == null)
            {
                LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                ShowMsg("输入非法无效的序号或者箱号，不做统计。", 0);
                txtCarton.Text = "";
                txtCarton.Focus();
                return;
            }


            TimeSpan ts0 = DateTime.Now.Subtract(dateStart).Duration();
            string strTest = dateStart.ToString("HHmmss.fff") + "执行insertpickpallet前" + DateTime.Now.ToString("HHmmss.fff") + "|" + ts0.Seconds.ToString() + "*" + ts0.Milliseconds.ToString();

            string strSID = txtSID.Text.Trim();
            string strPickSapNoA = txtPalletNO.Text.Trim();
            string strPickSapNO = "";
            string strLBL = "";
            string strResult = string.Empty;
            string strRetMsg = string.Empty;
            
            strResult =wb.NPIPICKInsertCarton(strPickSapNoA, out strPickSapNO, strSID, strCarton, g_sUserNo, out strRetMsg, out strLBL);

            txtPalletNO.Text = strPickSapNO;
            txtPalletNO.Refresh();
            TimeSpan ts1 = DateTime.Now.Subtract(dateStart).Duration();
            strTest += "\r\n执行insertpickpallet后" + DateTime.Now.ToString("HHmmss.fff") + "|" + ts1.Seconds.ToString() + "*" + ts1.Milliseconds.ToString();
            string strF = string.Empty;
            #region
            if (strResult.StartsWith("OK"))
            {
                this.labqty.Text = strLBL;
                labqty.Refresh();
                Ok();
                txtCarton.Focus();
                btnStart.Enabled = false;
            }
            else if (strResult.Contains("NG"))
            {
                ShowMsg(strRetMsg, 0);
                Ng();
                txtCarton.SelectAll();
                btnStart.Enabled = false;
                txtCarton.Focus();
                return;
            }
            else if (strResult.Contains("FINISH"))
            {
                strF = "F";
                labqty.Text = strLBL;
                labqty.Refresh();
                TimeSpan ts2 = DateTime.Now.Subtract(dateStart).Duration();
                strTest += "\r\n执行打印前" + DateTime.Now.ToString("HHmmss.fff") + "|" + ts2.Seconds.ToString() + "*" + ts2.Milliseconds.ToString();

                
                string strPickpalletno = txtPalletNO.Text.Trim();
                if (string.IsNullOrEmpty(strPickpalletno))
                {
                    ShowMsg("PickPalletNO 不能为空", 0);
                    return;
                }
                PickPalletLabel ppl = new PickPalletLabel();
                if (ppl.PrintPickPalletLabel_new(strPickpalletno))
                {
                    ShowMsg("打印OK", -1);


                }
                else
                {
                    ShowMsg("打印连接出了问题", 0);
                }

                Ok();
                //调用反馈SAP的接口
                #region

                string strCheckerrmsg = string.Empty;
                string strTTURL = string.Empty;
                string strResult2 = string.Empty;

                NPIPickListACBLL ub = new NPIPickListACBLL();
                strResult2 = ub.NPIPPSCheckWEBLOG(strSID, out strTTURL, out strCheckerrmsg);

                if (strResult2.StartsWith("NG"))
                {
                    ShowMsg(strResult2, 0);
                    return;
                }
                if (strCheckerrmsg.StartsWith("NG"))
                {
                    ShowMsg(strCheckerrmsg, 0);
                    return;
                }
                if (strResult2.Contains("UPLOAD TIPTOP") || strCheckerrmsg.Contains("已经传过"))
                {
                    //跳对话框
                    fCheck fcheck = new fCheck();
                    if (fcheck.ShowDialog() != DialogResult.OK)
                    {
                        ShowMsg("账号权限不正确", 0);
                        return;
                    }
                }
                string strResult0 = string.Empty;
                string strResultOut = string.Empty;
                strResult0 = ub.AfterEdiHttpPostWebService(g_ServerIP, strTTURL, strSID,out strResultOut);
                
                if (strResult0.Equals("OK"))
                {
                    ShowMsg(strResultOut, -1);
                    endprintaction();
                }
                else
                {
                    ShowMsg(strResultOut, 0);
                    txtCarton.Text = "";
                    btnStart.Enabled = false;
                    return;
                }
               
                #endregion
                TimeSpan ts3 = DateTime.Now.Subtract(dateStart).Duration();
                strTest += "\r\n执行打印后" + DateTime.Now.ToString("HHmmss.fff") + "|" + ts3.Seconds.ToString() + "*" + ts3.Milliseconds.ToString();

            }
            else
            {
                ShowMsg("检查SN或者CARTONNO获得特殊异常", 0);
                txtCarton.Text = "";
                return;
            }
            #endregion
            refresh_dgvPickNew(strSID);

            txtCarton.Text = "";
            txtCarton.Focus();
            TimeSpan ts4 = DateTime.Now.Subtract(dateStart).Duration();
            strTest += "\r\n执行SP后界面刷新" + DateTime.Now.ToString("HHmmss.fff") + "|" + ts4.Seconds.ToString() + "*" + ts4.Milliseconds.ToString();
            string strStartupPath = System.Windows.Forms.Application.StartupPath;
            string strLabelPath = strStartupPath + @"\Shipping\Label";
            string str8 = Path.GetFullPath(strLabelPath) + @"\" + strCarton + DateTime.Now.ToString("yyyyMMddHHmmssms") + ".txt";

        }
        private void endprintaction()
        {
            /// 3.锁定程式界面上半部分的功能>> 按钮失效;datagriadview 选择index 事件失效;...
            btnSearch.Enabled = true;
            btnClsFace.Enabled = true;
            btnReprint.Enabled = true;
            this.dgvNo.SelectionChanged += new System.EventHandler(this.dgvNo_SelectionChanged);
            txtCarton.Enabled = false;
            txtCarton.Text = "";
            txtPalletNO.Text = "";
            /// 4.【开始作业】 按钮启用。
            btnStart.Enabled = true;
            btnEnd.Enabled = false;
        }
        /// <summary>
        /// 错误声音
        /// </summary>
        public void Ng()
        {
            LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
        }

        /// <summary>
        /// 正确声音
        /// </summary>
        public void Ok()
        {
            LibHelperAC.MediasHelper.PlaySoundAsyncByOk();
        }

        /// <summary>
        /// Message 信息
        /// </summary>
        public DialogResult ShowMsg(string strTxt, int strType)
        {
            TextMsg.Text = strTxt;
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

        private void btnClsFace_Click(object sender, EventArgs e)
        {
            btnClsFace.Enabled = false;
            try
            {
                btnStart.Enabled = true;
                btnReprint.Enabled = true;
                clearCmb();
                clearDgv();
                clearTxt();
                labqty.Text = "00/00";
                this.ShowMsg("清空成功", -1);
                Ok();
            }
            catch (Exception ex)
            {
                Ng();
                MessageBox.Show(ex.Message);
            }
            btnClsFace.Enabled = true;
        }

        private void clearCmb()
        {
            //this.dt_start.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss");
            //this.dt_end.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            cmbSID.Text = "-ALL-";
            //cmbCarrier2.Text = "";
        }
        /// <summary>
        /// dgv清空
        /// </summary>
        private void clearDgv()
        {
            this.dgvNo.DataSource = null;
            this.dgvStock.DataSource = null;
            this.dgvPickNO.DataSource = null;
            dgvNo.Rows.Clear();
            dgvStock.Rows.Clear();
            dgvPickNO.Rows.Clear();
            //DataTable dt = (DataTable)dgvPick.DataSource;
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    dt.Clear();
            //    dgvPick.DataSource = dt;
            //}
        }

        /// <summary>
        /// txt清空
        /// </summary>
        private void clearTxt()
        {
            //txtCountry.Text = string.Empty;
            //txtPoe.Text = string.Empty;
            //txtRegion.Text = string.Empty;
            txtSID.Text = string.Empty;
            txtCarton.Text = string.Empty;
            txtPalletNO.Text = string.Empty;
            txtSID.ReadOnly = true;
            //txtRegion.ReadOnly = true;
            //txtCountry.ReadOnly = true;
            //txtPoe.ReadOnly = true;
            txtSID.BackColor = System.Drawing.Color.Silver;
            //txtRegion.BackColor = System.Drawing.Color.Silver;
            //txtCountry.BackColor = System.Drawing.Color.Silver;
            //txtPoe.BackColor = System.Drawing.Color.Silver;
            txtCarton.BackColor = System.Drawing.SystemColors.Window;
            //txtLOCATION_NO.BackColor = System.Drawing.SystemColors.Window;
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            string strSID =txtSID.Text.Trim();
            string strPickSapNo = txtPalletNO.Text.Trim();

            if (!string.IsNullOrEmpty(strPickSapNo))
            {
                if (strPickSapNo.Substring(1, 1).Equals("99"))
                {
                    //这么写不好，再改改。
                    ShowMsg("99号pickpallet,必须拣货完成自动打印", 0);
                    return;
                }

                /// 2.解锁电脑名与栈板的绑定。 改成SP nonedipps.SP_PICK_UNLOCKCOMPUTERNAME(palletno  in varchar2,timelimit in varchar2,errmsg    out varchar2)
                //--timelimit 如果是N，就是没有时间限制，栈板号来了就能解除电脑， 
                //--          如果是Y，就是有时间限制， UDT 在当前时间点 前一天 或者一小时 才能解锁电脑。

               
            }

            string strRetMsg = string.Empty;
            string strResult = wb.NPIPICKUnlockComputer(strSID,  out strRetMsg);
            object[][] procParams = new object[3][];
            
            if (strResult.Contains("OK"))
            {
                txtCarton.Enabled = false;
                ShowMsg("", 0);
                txtCarton.Focus();
            }
            else if (strResult.Contains("NG"))
            {
                ShowMsg(strRetMsg, 0);
                return;
            }
            else
            {
                ShowMsg("处理palletno遇到特殊异常", 0);
                return;
            }
            endprintaction();
        }

        private void btnTEST_Click(object sender, EventArgs e)
        {
            string strSID = "ITTEST001";
            string strCheckerrmsg = string.Empty;
            string strTTURL = string.Empty;
            string strResult2 = string.Empty;

            NPIPickListACBLL ub = new NPIPickListACBLL();
            strResult2 = ub.NPIPPSCheckWEBLOG(strSID, out strTTURL, out strCheckerrmsg);

            if (strResult2.StartsWith("NG"))
            {
                ShowMsg(strResult2, 0);
                return;
            }
            if (strCheckerrmsg.StartsWith("NG"))
            {
                ShowMsg(strCheckerrmsg, 0);
                return;
            }
            if (strResult2.Contains("UPLOAD TIPTOP") || strCheckerrmsg.Contains("已经传过"))
            {
                //跳对话框
                fCheck fcheck = new fCheck();
                if (fcheck.ShowDialog() != DialogResult.OK)
                {
                    ShowMsg("账号权限不正确", 0);
                    return;
                }
            }
            string strResult0 = string.Empty;
            string strResultOut = string.Empty;
            strResult0 = ub.AfterEdiHttpPostWebService(g_ServerIP, strTTURL, strSID, out strResultOut);

            if (strResult0.Equals("OK"))
            {
                ShowMsg(strResultOut, -1);
                endprintaction();
            }
            else
            {
                ShowMsg(strResultOut, 0);
                return;
            }


        }

        private void dgvPickNO_SelectionChanged(object sender, EventArgs e)
        {
            
            //1. 更新库存清单显示逻辑
            Int32 rowIndex = 0;
            try
            {
                rowIndex = dgvPickNO.CurrentRow.Index;
                //rowIndex = dgvNo.CurrentCell.RowIndex;
            }
            catch (Exception)
            {
                return;
            }
            if (dgvPickNO.CurrentRow.Index >= 0)
            {
                //1.1 同一行，则返回
                //if (g_curRow == rowIndex)
                //    return;
                 g_curRow = rowIndex;
                dgvStock.DataSource = null;
                dgvStock.Rows.Clear();
                string strSID = dgvPickNO.Rows[g_curRow].Cells["shipment_id"].Value.ToString();
                string strPartNo= dgvPickNO.Rows[g_curRow].Cells["ictpn"].Value.ToString();
                //2 更新作业显示内容
                dgvStock.DataSource=  wb.ShowStockInfo(strPartNo);
            }
        }

        private void btnReprint_Click(object sender, EventArgs e)
        {
            TextMsg.Text = "";
            TextMsg.BackColor = Color.Blue;
            fCheck fcheck = new fCheck();
            if (fcheck.ShowDialog() != DialogResult.OK)
            {
                ShowMsg("账号权限不正确", 0);
                return;
            }
            else
            {
                rePrintLabel pr = new rePrintLabel();
                pr.ShowDialog();
            }
        }
    }
}
