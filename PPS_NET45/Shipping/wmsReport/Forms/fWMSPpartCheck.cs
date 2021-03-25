using ClientUtilsDll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace wmsReport
{
    public partial class fWMSPpartCheck : Form
    {
        public fWMSPpartCheck()
        {
            InitializeComponent();
            ClientUtils.runBackground((Form)this);
        }
        
        private void fWMSPpartCheck_Load(object sender, EventArgs e)
        {
            initCMB();
        }

        private void initCMB()
        {
            string strSql = "select distinct a.trolley_no id, a.trolley_no name "
                        + "  from ppsuser.t_trolley_sn_status a "
                        + " order by id asc";
            WMSBLL wb = new WMSBLL();
            wb.fillCmb(strSql, "", cmbLocation);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbLocation.Text))
            {
                ShowMsg("请选择合适的车行号",0);
            }

            //
            btnSearch.Enabled = false;

            WMSBLL wb = new WMSBLL();
            wb.ShowStockInfo(cmbLocation.Text, dgvFindResult);


            btnSearch.Enabled = true;
        }

  
        private void btnStart_Click(object sender, EventArgs e)
        {
            ShowMsg("", 0);
            if (dgvFindResult.RowCount == 0)
            {
                ShowMsg("先选择车行号，点击查询", 0);
                return;
            }
            cmbLocation.Enabled = false;
            btnStart.Enabled = false;
            btnSearch.Enabled = false;
            btnEnd.Enabled = true;

            txtSN.Enabled = true;
            txtSN.Focus();
            txtSN.SelectAll();
           
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            cmbLocation.Enabled = true;
            btnStart.Enabled = true;
            btnSearch.Enabled = true;
            btnEnd.Enabled = false;

            txtSN.Text = "";
            txtSN.Enabled = false;
            ShowMsg("",-1);
        }

        private void txtSN_KeyDown(object sender, KeyEventArgs e)
        {
            string strCSN = txtSN.Text;
            if(strCSN.StartsWith("3S"))
            { strCSN = strCSN.Substring(2); }

            if (e.KeyCode != Keys.Enter)
            { return; }
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strCSN))
            {
                txtSN.SelectAll();
                txtSN.Focus();
                return;
            }
            //调用一个SP 更新PointNO，

            string strResult = string.Empty;
            string strResulterrmsg = string.Empty;
            WMSBLL wb = new WMSBLL();
            strResult = wb.PPSInsertWorkLogBy( cmbLocation.Text, strCSN, out strResulterrmsg);
           
            if (strResult.Equals("OK"))
            {
                wb.ShowStockInfo(cmbLocation.Text, dgvFindResult);

                showdgvNoColor(strCSN);
                LibHelper.MediasHelper.PlaySoundAsyncByOk();
                ShowMsg("CHECK OK", -1);
            }
            else
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                ShowMsg(strResulterrmsg, 0);
            }

            txtSN.Text = "";
            txtSN.Focus();
            txtSN.SelectAll();

        }

        private void showdgvNoColor(string incsn)
        {
            #region
            for (int i = 0; i < dgvFindResult.Rows.Count; i++)
            {
                this.dgvFindResult.Rows[i].HeaderCell.Value = (i + 1).ToString();
                ////按条件设置每一行的颜色属性
                //this.dgvNo.Rows[i].Cells[0].Style.ForeColor = Color.Blue;
                if (dgvFindResult.Rows[i].Cells["CUSTOM_SN"].Value.ToString().Contains(incsn))
                {
                    this.dgvFindResult.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    return;
                }
               
            }
            #endregion
        }
        public DialogResult ShowMsg(string strTxt, int strType)
        {
            TextMsg.Text = strTxt.TP();
            switch (strType)
            {
                case 0: //Error                
                    TextMsg.ForeColor = Color.Red;
                    TextMsg.BackColor = Color.Silver;
                    return DialogResult.None;
                case 1: //Warning                        
                    TextMsg.ForeColor = Color.Blue;
                    TextMsg.BackColor = Color.FromArgb(255, 255, 128);
                    return DialogResult.None;
                case 2: //Confirm
                    return MessageBox.Show(strTxt, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    TextMsg.ForeColor = Color.Green;
                    TextMsg.BackColor = Color.White;
                    return DialogResult.None;
            }
        }
    }
}
