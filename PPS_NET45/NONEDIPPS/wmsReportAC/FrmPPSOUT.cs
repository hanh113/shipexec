using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibHelperAC;

namespace wmsReportAC
{
    public partial class FrmPPSOUT : Form
    {
        public FrmPPSOUT()
        {
            InitializeComponent();
        }

        private void FrmPPSOUT_Load(object sender, EventArgs e)
        {
            this.txtCarton.Enabled = true;
            this.txtCarton.Visible = true;
            this.txtCarton.Focus();
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

        private void txtCarton_KeyDown(object sender, KeyEventArgs e)
        {
            ShowMsg("", -1);
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            if (string.IsNullOrEmpty(this.txtCarton.Text.Trim()))
            {
                return;
            }
            try
            {
                //箱号格式处理
                string inputData = this.txtCarton.Text.ToUpper().Trim();
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
                this.txtCarton.Text = inputData;

                WMSDLL wmsdll = new WMSDLL();
                string strReturnMsg = wmsdll.AppleCareOut(this.txtCarton.Text);
                if (strReturnMsg == "OK")
                {
                    ShowMsg("出库OK", -1);
                    MediasHelper.PlaySoundAsyncByOk();
                    this.txtCarton.SelectAll();
                    this.txtCarton.Focus();
                }
                else
                {
                    ShowMsg(strReturnMsg, 0);
                    MediasHelper.PlaySoundAsyncByNg();
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
    }
}
