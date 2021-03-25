using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibHelper;

namespace wmsReport
{
    public partial class FrmEartipsStockIn : Form
    {
        private CommonSQL common = new CommonSQL();
        private string strLocationID = string.Empty;

        public FrmEartipsStockIn()
        {
            InitializeComponent();
            ClientUtils.runBackground((Form)this);
        }

        private void FrmEartipsStockIn_Load(object sender, EventArgs e)
        {
            this.txtLocation.Text = "";
            this.txtCartonNo.Text = "";
            this.txtLocation.Enabled = true;
            this.txtCartonNo.Enabled = false;
            this.txtLocation.Focus();
        }

        private void txtLocation_KeyDown(object sender, KeyEventArgs e)
        {
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
                ShowMsg("储位扫描OK,请扫描需要入库的外箱号", -1);
                MediasHelper.PlaySoundAsyncByOk();
                this.txtLocation.Enabled = false;
                this.txtCartonNo.Enabled = true;
                this.txtCartonNo.SelectAll();
                this.txtCartonNo.Focus();
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message, 0);
                MediasHelper.PlaySoundAsyncByNg();
                this.txtLocation.SelectAll();
                this.txtLocation.Focus();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.txtLocation.Text = "";
            this.txtCartonNo.Text = "";
            this.txtLocation.Enabled = true;
            this.txtCartonNo.Enabled = false;
            this.txtLocation.Focus();
        }

        private void txtCartonNo_KeyDown(object sender, KeyEventArgs e)
        {
            ShowMsg("", -1);
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            if (string.IsNullOrEmpty(this.txtCartonNo.Text.Trim()))
            {
                return;
            }
            try
            {
                string inputData = this.txtCartonNo.Text.ToUpper().Trim();
                this.txtCartonNo.Text = DelPrefixCartonSN(inputData);
                DataTable dtTemp = common.GetPPSCartonInfo(this.txtCartonNo.Text);
                if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
                {
                    ShowMsg("该外箱号已有入库数据,请检查", 0);
                    MediasHelper.PlaySoundAsyncByNg();
                    this.txtCartonNo.SelectAll();
                    this.txtCartonNo.Focus();
                    return;
                }
                dtTemp = common.GetMESCartonInfo(this.txtCartonNo.Text);
                if ((dtTemp == null) || (dtTemp.Rows.Count <= 0))
                {
                    ShowMsg("该外箱号MES中无数据,请检查", 0);
                    MediasHelper.PlaySoundAsyncByNg();
                    this.txtCartonNo.SelectAll();
                    this.txtCartonNo.Focus();
                    return;
                }
                string strReturnMsg = common.CartonStockInLocation(this.txtCartonNo.Text, this.strLocationID);
                if (strReturnMsg == "OK")
                {
                    ShowMsg("入库OK", -1);
                    MediasHelper.PlaySoundAsyncByOk();
                    this.txtCartonNo.Text = "";
                    this.txtCartonNo.Focus();
                }
                else
                {
                    ShowMsg(strReturnMsg, 0);
                    MediasHelper.PlaySoundAsyncByNg();
                    this.txtCartonNo.SelectAll();
                    this.txtCartonNo.Focus();
                }
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message, 0);
                MediasHelper.PlaySoundAsyncByNg();
                this.txtCartonNo.SelectAll();
                this.txtCartonNo.Focus();
            }
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

        private string DelPrefixCartonSN(string insn)
        {
            if (insn.Length == 20 && insn.Substring(0, 2).Equals("00"))
            { insn = insn.Substring(2); }
            else if (insn.StartsWith("3S"))
            { insn = insn.Substring(2); }
            else if (insn.StartsWith("S"))
            { insn = insn.Substring(1); }
            return insn;
        }
    }
}
