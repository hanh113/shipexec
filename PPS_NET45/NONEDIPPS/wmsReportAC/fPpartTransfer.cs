using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace wmsReportAC
{
    public partial class fPpartTransfer : Form
    {
        public int H = 0;
        public int W = 0;
        public fPpartTransfer()
        {
            InitializeComponent();
        }

        private void fPpartTransfer_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Maximized;
            initGroupBox();
            initLocationFrom();
            initLocationTo();

        }
        private void initGroupBox()
        {
            W = Screen.PrimaryScreen.Bounds.Width;
          
               W = Convert.ToInt32(W * 0.5);
            
        
            this.grpFrom.Width =W;
        }

        private void initLocationFrom()
        {
            string strSql = "select distinct a.trolley_line_no id, a.trolley_line_no name "
                        + " from NONEDIPPS.t_trolley_line_info a "
                        + "  where(trolley_no, sides_no, level_no, seq_no) in "
                        + "        (select distinct trolley_no, sides_no, level_no, seq_no "
                        + "           from NONEDIPPS.t_trolley_sn_status  "
                        + "              where trolley_no <> 'ICT-00-00-000' ) "
                        + " order by id asc";
            WMSBLL wb = new WMSBLL();
            wb.fillCmb(strSql, "", cmbLocationFrom);
        }
        private void initLocationTo()
        {
            string strSql = "select distinct a.trolley_line_no id, a.trolley_line_no name "
                        + "   from NONEDIPPS.t_trolley_line_info a "
                        + "   where trolley_no in (select trolley_no from NONEDIPPS.t_location_trolley) "
                        + "     and trolley_no not in('ICT-00-00-000','ICT-00-00-SYS')"
                        + " order by id asc";
            WMSBLL wb = new WMSBLL();
            wb.fillCmb(strSql, "", cmbLocationTo);
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

        private void cmbLocationFrom_SelectedIndexChanged(object sender, EventArgs e)
        {

            WMSBLL wb = new WMSBLL();
            wb.ShowStockInfo(cmbLocationFrom.Text, dgvFindResultFrom);
        }

        private void cmbLocationTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            WMSBLL wb = new WMSBLL();
            wb.ShowStockInfo(cmbLocationTo.Text, dgvFindResultTo);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ShowMsg("", 0);
            if (dgvFindResultFrom.RowCount == 0)
            {
                ShowMsg("先选择车行号，点击查询", 0);
                return;
            }
            if (cmbLocationFrom.Text.Equals(cmbLocationTo.Text))
            {
                ShowMsg("相同位置不需要转移", 0);
                return;
            }
            cmbLocationFrom.Enabled = false;
            cmbLocationTo.Enabled = false;
            btnStart.Enabled = false;
            btnSearch.Enabled = false;
            btnEnd.Enabled = true;

            txtSN.Enabled = true;
            txtSN.Focus();
            txtSN.SelectAll();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            cmbLocationFrom.Enabled = true;
            cmbLocationTo.Enabled = true;
            btnStart.Enabled = true;
            btnSearch.Enabled = true;
            btnEnd.Enabled = false;

            txtSN.Text = "";
            txtSN.Enabled = false;
            ShowMsg("", -1);
        }
        private void txtSN_KeyDown(object sender, KeyEventArgs e)
        {
            string strCSN = txtSN.Text;
           
            if (e.KeyCode != Keys.Enter)
            { return; }
            WMSBLL plb = new WMSBLL();
            strCSN = plb.DelPrefixCartonSN(strCSN);

            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strCSN))
            {
                txtSN.SelectAll();
                txtSN.Focus();
                return;
            }
            //锁定源和目的储位
            if (rdoLockFromTo.Checked == true)
            {
                #region
                if (dgvFindResultFrom.RowCount == 0)
                {
                    ShowMsg("源车行号下无记录，无法转出", 0);
                    return;
                }
                if (cmbLocationFrom.Text.Equals(cmbLocationTo.Text))
                {
                    ShowMsg("相同位置不需要转移", 0);
                    return;
                }
                string strResult = string.Empty;
                string strResulterrmsg = string.Empty;
                WMSBLL wb = new WMSBLL();
                strResult = wb.WMSPpartTrans(cmbLocationFrom.Text, cmbLocationTo.Text, strCSN, out strResulterrmsg);

                if (strResult.Equals("OK"))
                {
                    wb.ShowStockInfo2(cmbLocationFrom.Text, dgvFindResultFrom, strCSN);
                    wb.ShowStockInfo2(cmbLocationTo.Text, dgvFindResultTo, strCSN);

                    //showdgvNoColor(strCSN, dgvFindResultTo);
                    LibHelperAC.MediasHelper.PlaySoundAsyncByOk();
                    ShowMsg("OK", -1);
                }
                else
                {
                    LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                    ShowMsg(strResulterrmsg, 0);
                }
                txtSN.Text = "";
                txtSN.Focus();
                txtSN.SelectAll();
                #endregion
            }
            //忽略源储位，锁定目的储位
            else if (rdoLockTo.Checked == true)
            {
                #region
                string strResult = string.Empty;
                string strCarlineno = string.Empty;
                string strResulterrmsg = string.Empty;
                WMSBLL wb = new WMSBLL();
                strResult = wb.GetCarlinenoByCSN(strCSN, out strCarlineno, out strResulterrmsg);
                if (strResult.Equals("OK"))
                {
                    cmbLocationFrom.Text = strCarlineno;

                    wb.ShowStockInfo2(cmbLocationFrom.Text, dgvFindResultFrom, strCSN);
                    showdgvNoColor(strCSN, dgvFindResultFrom);


                    string strResult2 = string.Empty;
                    string strResulterrmsg2 = string.Empty;
                    strResult2 = wb.WMSPpartTrans(strCarlineno, cmbLocationTo.Text, strCSN, out strResulterrmsg2);

                    if (strResult2.Equals("OK"))
                    {
                        wb.ShowStockInfo2(cmbLocationFrom.Text, dgvFindResultFrom, strCSN);
                        wb.ShowStockInfo2(cmbLocationTo.Text, dgvFindResultTo, strCSN);

                        //showdgvNoColor(strCSN, dgvFindResultTo);
                        LibHelperAC.MediasHelper.PlaySoundAsyncByOk();
                        ShowMsg("OK", -1);
                    }
                    else
                    {
                        LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                        ShowMsg(strResulterrmsg2, 0);
                    }
                }
                else
                {
                    LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                    ShowMsg(strResulterrmsg, 0);
                }
                txtSN.Text = "";
                txtSN.Focus();
                txtSN.SelectAll();
                #endregion
            }
            //建议储位
            else if (rdoAdvise.Checked == true)
            {
                #region
                //GetCarlinenoByAdvise(string incsn, out string carlineno)

                #region
                string strResult = string.Empty;
                string strCarlineno = string.Empty;
                string strResulterrmsg = string.Empty;
                WMSBLL wb = new WMSBLL();
                strResult = wb.GetCarlinenoByCSN(strCSN, out strCarlineno, out strResulterrmsg);
                if (strResult.Equals("OK"))
                {
                    cmbLocationFrom.Text = strCarlineno;

                    wb.ShowStockInfo2(cmbLocationFrom.Text, dgvFindResultFrom, strCSN);
                    showdgvNoColor(strCSN, dgvFindResultFrom);

                    string strAdviseCarlineno = string.Empty;
                    wb.GetCarlinenoByAdvise(strCSN, out strAdviseCarlineno);

                    if (string.IsNullOrEmpty(strAdviseCarlineno))
                    {
                        LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                        ShowMsg("无建议储位", 0);
                    }
                    else
                    {
                        cmbLocationTo.Text = strAdviseCarlineno;
                        wb.ShowStockInfo2(strAdviseCarlineno, dgvFindResultTo, strCSN);
                        DialogResult strResultA = MessageBox.Show("获得车行号:" + strAdviseCarlineno + ",是否开始转移作业？", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (strResultA == DialogResult.No)
                        {
                            return;
                        }
                        string strResult2 = string.Empty;
                        string strResulterrmsg2 = string.Empty;
                        strResult2 = wb.WMSPpartTrans(strCarlineno, strAdviseCarlineno, strCSN, out strResulterrmsg2);

                        if (strResult2.Equals("OK"))
                        {
                            wb.ShowStockInfo2(strCarlineno, dgvFindResultFrom, strCSN);
                            wb.ShowStockInfo2(strAdviseCarlineno, dgvFindResultTo, strCSN);

                            //showdgvNoColor(strCSN, dgvFindResultTo);
                            LibHelperAC.MediasHelper.PlaySoundAsyncByOk();
                            ShowMsg("OK", -1);
                        }
                        else
                        {
                            LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                            ShowMsg(strResulterrmsg2, 0);
                        }

                    }

                   
                }
                else
                {
                    LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                    ShowMsg(strResulterrmsg, 0);
                }
                txtSN.Text = "";
                txtSN.Focus();
                txtSN.SelectAll();
                #endregion

                #endregion
            }
            //查询
            else if (rdoQuery.Checked == true)
            {
                #region
                string strResult = string.Empty;
                string strCarlineno = string.Empty;
                string strResulterrmsg = string.Empty;
                WMSBLL wb = new WMSBLL();
                strResult = wb.GetCarlinenoByCSN(strCSN, out   strCarlineno, out  strResulterrmsg);
                if (strResult.Equals("OK"))
                {
                    cmbLocationFrom.Text = strCarlineno;
                    wb.ShowStockInfo2(cmbLocationFrom.Text, dgvFindResultFrom, strCSN);
                    showdgvNoColor(strCSN, dgvFindResultFrom);
                    LibHelperAC.MediasHelper.PlaySoundAsyncByOk();
                    ShowMsg("OK", -1);
                }
                else
                {
                    LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                    ShowMsg(strResulterrmsg, 0);
                }
                txtSN.Text = "";
                txtSN.Focus();
                txtSN.SelectAll();

                #endregion
            }
            //锁定源和目的储位
            else if (rdoCheck.Checked == true)
            {
                #region
                //GetCarlinenoByAdvise(string incsn, out string carlineno)

                #region
                string strResult = string.Empty;
                string strCarlineno = string.Empty;
                string strResulterrmsg = string.Empty;
                WMSBLL wb = new WMSBLL();
                strResult = wb.GetCarlinenoByCSN(strCSN, out strCarlineno, out strResulterrmsg);
                if (strResult.Equals("OK"))
                {

                    if (strCarlineno.Equals(cmbLocationFrom.Text))
                    {
                        LibHelperAC.MediasHelper.PlaySoundAsyncByOk();
                        showdgvNoColor(strCSN, dgvFindResultFrom);
                        ShowMsg("OK", -1);
                    }
                    else
                    {
                        LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                        ShowMsg("当前车行号与序号车行号【"+ strCarlineno + "】不匹配", 0);
                    } 

                }
                else
                {
                    LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                    ShowMsg(strResulterrmsg, 0);
                }
                txtSN.Text = "";
                txtSN.Focus();
                txtSN.SelectAll();
                #endregion

                #endregion
            }

        }

        private void showdgvNoColor(string incsn,DataGridView dgv)
        {
            #region
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                //dgv.Rows[i].HeaderCell.Value = (i + 1).ToString();
                ////按条件设置每一行的颜色属性
                //this.dgvNo.Rows[i].Cells[0].Style.ForeColor = Color.Blue;
                if (dgv.Rows[i].Cells["CUSTOM_SN"].Value.ToString().Contains(incsn))
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    return;
                }

            }
            #endregion
        }

        public DialogResult ShowMsg(string strTxt, int strType)
        {
            TextMsg.Text = strTxt;
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
