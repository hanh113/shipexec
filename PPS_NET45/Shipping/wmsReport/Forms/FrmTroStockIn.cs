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
    public partial class FrmTroStockIn : Form
    {
        private CommonSQL common = new CommonSQL();
        private string strLocationID = string.Empty;

        public FrmTroStockIn()
        {
            InitializeComponent();
            ClientUtils.runBackground((Form)this);
        }

        private void FrmTroStockIn_Load(object sender, EventArgs e)
        {
            this.txtLocation.Enabled = true;
            this.txtTroNo.Enabled = false;
            this.txtLocation.Text = "";
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
                dtTemp = common.GetTLocation(this.txtLocation.Text);
                if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
                {
                    ShowMsg("储位号已被占用,请检查", 0);
                    MediasHelper.PlaySoundAsyncByNg();
                    this.txtLocation.SelectAll();
                    this.txtLocation.Focus();
                    return;
                }
                ShowMsg("储位扫描OK,请扫描需要入库的精钢车号", -1);
                MediasHelper.PlaySoundAsyncByOk();
                this.txtLocation.Enabled = false;
                this.txtTroNo.Enabled = true;
                this.txtTroNo.SelectAll();
                this.txtTroNo.Focus();
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message, 0);
                MediasHelper.PlaySoundAsyncByNg();
                this.txtLocation.SelectAll();
                this.txtLocation.Focus();
            }
        }

        private void txtTroNo_KeyDown(object sender, KeyEventArgs e)
        {
            ShowMsg("", -1);
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            if (string.IsNullOrEmpty(this.txtTroNo.Text.Trim()))
            {
                return;
            }
            try
            {
                string inputData = this.txtTroNo.Text.ToUpper().Trim();
                this.txtTroNo.Text = inputData;
                DataTable dtTemp = common.GetPPSStockInfo(this.txtTroNo.Text);
                if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
                {
                    ShowMsg("该精钢车已有入库数据,请检查", 0);
                    MediasHelper.PlaySoundAsyncByNg();
                    this.txtTroNo.SelectAll();
                    this.txtTroNo.Focus();
                    return;
                }
                dtTemp = common.GetMESStockInfo(this.txtTroNo.Text);
                if ((dtTemp == null) || (dtTemp.Rows.Count <= 0))
                {
                    ShowMsg("该精钢车MES中无数据,请检查", 0);
                    MediasHelper.PlaySoundAsyncByNg();
                    this.txtTroNo.SelectAll();
                    this.txtTroNo.Focus();
                    return;
                }
                string strReturnMsg = common.TroStockInLocation(dtTemp.Rows[0]["PALLET_NO"].ToString().Trim(), this.strLocationID);
                if (strReturnMsg == "OK")
                {
                    ShowMsg("入库OK", -1);
                    MediasHelper.PlaySoundAsyncByOk();
                    this.txtTroNo.Enabled = false;
                    this.txtLocation.Enabled = true;
                    this.txtLocation.SelectAll();
                    this.txtLocation.Focus();
                }
                else
                {
                    ShowMsg(strReturnMsg, 0);
                    MediasHelper.PlaySoundAsyncByNg();
                    this.txtTroNo.SelectAll();
                    this.txtTroNo.Focus();
                }
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message, 0);
                MediasHelper.PlaySoundAsyncByNg();
                this.txtTroNo.SelectAll();
                this.txtTroNo.Focus();
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
    }
}
