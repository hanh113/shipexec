using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibHelperAC;
using Oracle.ManagedDataAccess.Client;

namespace StockInAC
{
    public partial class fMain : Form
    {
        private CommonSQL common = new CommonSQL();
        private string strLocationID = string.Empty;

        public fMain()
        {
            InitializeComponent();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " (" + SajetCommon.g_sFileVersion + ")";
            labShow.Text = labShow.Text + " (" + SajetCommon.g_sFileVersion + ")";

            this.WindowState = FormWindowState.Maximized;

            this.txtCarton.Enabled = false;
            this.txtLocation.Enabled = true;
            this.txtLocation.SelectAll();
            this.txtLocation.Focus();
            ShowMsg("", -1);
        }

        private void txtLocation_KeyDown(object sender, KeyEventArgs e)
        {
            dtgrvStockin.DataSource = null;
            ShowMsg("", -1);
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            if (string.IsNullOrEmpty(this.txtLocation.Text.Trim()))
            {
                return;
            }
            try
            {
                //箱号格式处理
                string inputData = this.txtLocation.Text.ToUpper().Trim();
                this.txtLocation.Text = inputData;
                DataTable dtTemp = common.GetLocationInfo(this.txtLocation.Text);
                if ((dtTemp == null) || (dtTemp.Rows.Count <= 0))
                {
                    ShowMsg("储位号不存在或未启用", 0);
                    MediasHelper.PlaySoundAsyncByNg();
                    this.txtLocation.SelectAll();
                    this.txtLocation.Focus();
                    return;
                }
                strLocationID = dtTemp.Rows[0]["LOCATION_ID"].ToString().Trim();
                dtgrvStockin.DataSource = StockInfor(txtLocation.Text);
                ShowMsg("储位扫描OK,请扫描需要入库的中箱号", -1);
                MediasHelper.PlaySoundAsyncByOk();
                this.txtLocation.Enabled = false;
                this.txtCarton.Enabled = true;
                this.txtCarton.SelectAll();
                this.txtCarton.Focus();
                
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message, 0);
                MediasHelper.PlaySoundAsyncByNg();
                this.txtLocation.SelectAll();
                this.txtLocation.Focus();
            }
        }

        private void txtCarton_KeyDown(object sender, KeyEventArgs e)
        {
            //dtgrvStockin.DataSource = null;
            ShowMsg("", -1);
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            if (string.IsNullOrEmpty(this.txtCarton.Text.Trim()))
            {
                return;
            }
            if (this.radSN.Checked)
            {
                try
                {
                    //箱号格式处理
                    string inputData = this.txtCarton.Text.ToUpper().Trim();
                    if (inputData.Length == 20 && inputData.Substring(0, 2).Equals("00"))
                    {
                        inputData = inputData.Substring(2, 18);
                    }
                    this.txtCarton.Text = inputData;
                    DataTable dtPallet = common.StockInByPallet(this.strLocationID, this.txtCarton.Text);
                    if ((dtPallet == null) || (dtPallet.Rows.Count <= 0))
                    {
                        throw new Exception("入库异常,请联系IT-PPS处理!");
                    }
                    if (dtPallet.Rows[0]["varRetCode"].ToString().Trim() != "0")
                    {
                        ShowMsg(dtPallet.Rows[0]["varRetMsg"].ToString().Trim(), 0);
                        MediasHelper.PlaySoundAsyncByNg();
                        this.txtCarton.SelectAll();
                        this.txtCarton.Focus();
                    }
                    else
                    {
                        ShowMsg("扫描OK,请继续扫描下一箱!", -1);
                        MediasHelper.PlaySoundAsyncByOk();
                        this.txtCarton.Text = "";
                        this.txtCarton.SelectAll();
                        this.txtCarton.Focus();
                    }
                }
                catch (Exception ex)
                {
                    ShowMsg(ex.Message, 0);
                    MediasHelper.PlaySoundAsyncByNg();
                    this.txtCarton.SelectAll();
                    this.txtCarton.Focus();
                }
            }
            else
            {
                if (this.chkCarton.Checked)
                {
                    if (string.IsNullOrEmpty(this.txtPre.Text.Trim()))
                    {
                        ShowMsg("请输入检查箱号前缀!", 0);
                        MediasHelper.PlaySoundAsyncByNg();
                        this.txtPre.SelectAll();
                        this.txtPre.Focus();
                        return;
                    }
                    string strPre = this.txtPre.Text.Trim().ToUpper();
                    string strCartonU = this.txtCarton.Text.Trim().ToUpper();
                    if (!strCartonU.StartsWith(strPre))
                    {
                        ShowMsg("箱号前缀不符合,请检查!", 0);
                        MediasHelper.PlaySoundAsyncByNg();
                        this.txtCarton.SelectAll();
                        this.txtCarton.Focus();
                        return;
                    }
                }
                ShowMsg("箱号OK,请扫描料号", -1);
                MediasHelper.PlaySoundAsyncByOk();
                this.txtCarton.Enabled = false;
                this.txtPart.Enabled = true;
                this.txtPart.SelectAll();
                this.txtPart.Focus();
            }
            dtgrvStockin.DataSource = null;
            dtgrvStockin.DataSource = StockInfor(txtLocation.Text);
        }

        public DataTable StockInfor(string strLocationNo)
        {
               string sql = @"   SELECT t.location_no,
                     t.pallet_no,
                     t.carton_no,
                     t.part_no,
                     t.cdt,
                     COUNT (serial_number) qty
                FROM nonedipps.t_sn_status t
               WHERE t.location_no = :location_no
            GROUP BY t.location_no,
                     t.pallet_no,
                     t.carton_no,
                     t.cdt,
                     t.part_no order by t.cdt";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "location_no", strLocationNo };
            DataTable dt = new DataTable();
            dt= ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
            return dt;

        }

        public DialogResult ShowMsg(string strTxt, int strType)
        {
            txtMsg.Text = strTxt;
            switch (strType)
            {
                case 0: //Error    
                    txtMsg.ForeColor = Color.Red;
                    txtMsg.BackColor = Color.Blue;
                    return DialogResult.None;
                case 1: //Warning                        
                    txtMsg.ForeColor = Color.Blue;
                    txtMsg.BackColor = Color.FromArgb(255, 255, 128);
                    return DialogResult.None;
                case 2: //Confirm
                    return MessageBox.Show(strTxt, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    txtMsg.ForeColor = Color.White;
                    txtMsg.BackColor = Color.Blue;
                    return DialogResult.None;
            }
        }

        private void radSN_CheckedChanged(object sender, EventArgs e)
        {
            if (radSN.Checked)
            {
                this.txtLocation.Text = "";
                this.txtLocation.Enabled = true;
                this.txtLocation.Focus();
                this.txtCarton.Text = "";
                this.txtCarton.Enabled = false;
                this.labPart.Visible = false;
                this.txtPart.Text = "";
                this.txtPart.Enabled = false;
                this.txtPart.Visible = false;
                this.chkCarton.Visible = false;
                this.txtPre.Visible = false;
            }
            else
            {
                this.txtLocation.Text = "";
                this.txtLocation.Enabled = true;
                this.txtLocation.Focus();
                this.txtCarton.Text = "";
                this.txtCarton.Enabled = false;
                this.labPart.Visible = true;
                this.txtPart.Text = "";
                this.txtPart.Enabled = false;
                this.txtPart.Visible = true;
                this.chkCarton.Visible = true;
                this.txtPre.Visible = true;
            }
        }

        private void txtPart_KeyDown(object sender, KeyEventArgs e)
        {
            ShowMsg("", -1);
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            if (string.IsNullOrEmpty(this.txtPart.Text.Trim()))
            {
                return;
            }
            try
            {
                DataTable dtPart = common.StockInByCartonAndPart(this.strLocationID, this.txtLocation.Text.Trim().ToUpper(), this.txtCarton.Text.Trim(), this.txtPart.Text.Trim().ToUpper());
                if ((dtPart == null) || (dtPart.Rows.Count <= 0))
                {
                    throw new Exception("入库异常,请联系IT-PPS处理!");
                }
                if (dtPart.Rows[0]["varRetCode"].ToString().Trim() != "0")
                {
                    ShowMsg(dtPart.Rows[0]["varRetMsg"].ToString().Trim(), 0);
                    MediasHelper.PlaySoundAsyncByNg();
                    this.txtCarton.Enabled = true;
                    this.txtPart.Enabled = false;
                    this.txtPart.Text = "";
                    this.txtCarton.Text = "";
                    this.txtCarton.SelectAll();
                    this.txtCarton.Focus();
                }
                else
                {
                    ShowMsg("扫描OK,请继续扫描下一箱!", -1);
                    MediasHelper.PlaySoundAsyncByOk();
                    this.txtPart.Text = "";
                    this.txtCarton.Text = "";
                    this.txtPart.Enabled = false;
                    this.txtCarton.Enabled = true;
                    this.txtCarton.SelectAll();
                    this.txtCarton.Focus();
                }
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message, 0);
                MediasHelper.PlaySoundAsyncByNg();
                this.txtCarton.SelectAll();
                this.txtCarton.Focus();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {

            btnReset.Enabled = false;
            try
            {
                txtLocation.Text = string.Empty;
                txtCarton.Text = string.Empty;
                txtLocation.Enabled = true;
                txtCarton.Enabled = false;
                txtLocation.Focus();
                dtgrvStockin.DataSource = null;
             
                this.ShowMsg("清空成功", -1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            btnReset.Enabled = true;
        }
    }
}
