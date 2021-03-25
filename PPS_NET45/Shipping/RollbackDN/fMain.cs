using ClientUtilsDll;
using SajetClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RollbackDN
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }
        private Int32 g_curRow = -1;    //当前选中行号
        private void fMainNew_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " (" + SajetCommon.g_sFileVersion + ")";
            labShow.Text = labShow.Text + " (" + SajetCommon.g_sFileVersion + ")";
            DateTime dateTimeNow = DateTime.Now;
            //dt_start.Value = new DateTime(dateTimeNow.Year, dateTimeNow.Month - 2, 1);
            dt_start.Value = dateTimeNow.AddDays(-1);
            dt_end.Value = dateTimeNow.AddDays(1);
            this.WindowState = FormWindowState.Maximized;

            string strResult = string.Empty;
            string strOutparavalue = string.Empty;
            string strResulterrmsg = string.Empty;
            RollbackBll pb = new RollbackBll();
            strResult = pb.GetDBType("DB_TYPE", out strOutparavalue, out strResulterrmsg);
            if (strResult.Equals("OK") && strOutparavalue.Equals("TEST"))
            {
                btnTest.Visible = true;
                btnTest.Enabled = true;
            }
            else
            {
                btnTest.Visible = false;
                btnTest.Enabled = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ShowMsg("", -1);
            btnSearch.Enabled = false;

            showDNList();

            btnSearch.Enabled = true;
        }
        private void showDNList()
        {
            string strStartDay = dt_start.Value.ToString("yyyy-MM-dd");
            string strEndDay = dt_end.Value.ToString("yyyy-MM-dd");
            dgvDN.DataSource = null;
            RollbackBll rb = new RollbackBll();
            DataTable dtDNList = rb.GetZCDNListDataTable(strStartDay, strEndDay);
            if (dtDNList == null || dtDNList.Rows.Count == 0)
            {
                ShowMsg("NG，查不资料！", 0);
            }
            else
            {
                dgvDN.DataSource = dtDNList;
            }
        }


        private DialogResult ShowMsg(string strTxt, int strType)
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
                    TextMsg.BackColor = Color.Blue;
                    return DialogResult.None;
            }
        }

        private void dgvDN_SelectionChanged(object sender, EventArgs e)
        {
            reflashlabel();
        }
        private void reflashlabel()
        {
            Int32 rowIndex = 0;
            try
            {
                rowIndex = dgvDN.CurrentRow.Index;
                //rowIndex = dgvDN.CurrentCell.RowIndex;
            }
            catch (Exception)
            {
                return;
            }
            if (dgvDN.CurrentRow.Index >= 0)
            {
                //1.1 同一行，则返回
                if (g_curRow == rowIndex)
                    return;
                g_curRow = rowIndex;

                txtSmId.Text = dgvDN.Rows[rowIndex].Cells["SHIPMENT_ID"].Value.ToString();
                txtGroup.Text = dgvDN.Rows[rowIndex].Cells["GROUP_CODE"].Value.ToString();
                txtDNSelect.Text = dgvDN.Rows[rowIndex].Cells["DELIVERY_NO"].Value.ToString();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtSmId.Text) || string.IsNullOrEmpty(txtDNSelect.Text))
            {
                ShowMsg("NG，请选择合适的DN再开始检查", 0);
                return;
            }
            string strSID = txtSmId.Text;
            string strDN = txtDNSelect.Text;

            btnStart.Enabled = false;


            string strResult = string.Empty;
            string strResulterrmsg = string.Empty;
            string strGroupcode = string.Empty;
            RollbackBll pb = new RollbackBll();
            strResult = pb.CheckDNGroupCodetoBackUP(strSID, strDN, out strGroupcode, out strResulterrmsg);
            if (strResult.Equals("NG"))
            {
                ShowMsg(strResulterrmsg, 0);
                btnStart.Enabled = true;
                return;
            }
            //刷新dgvDN list
            showDNList();
            //选择groupcode 最后一个
            if (dgvDN.RowCount == 0)
            {
                ShowMsg("NG，查不资料！", 0);
                return;
            }
            else
            {
                bool isfirst = true;
                for (int i = 0; i < dgvDN.RowCount - 1; i++)
                {
                    if (dgvDN.Rows[i].Cells["GROUP_CODE"].Value.ToString().Equals(strGroupcode))
                    {
                        dgvDN.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        if (isfirst)
                        {
                            dgvDN.Rows[i].Selected = true;
                            dgvDN.FirstDisplayedScrollingRowIndex = i;
                            txtSmId.Text = dgvDN.Rows[i].Cells["SHIPMENT_ID"].Value.ToString();
                            txtGroup.Text = dgvDN.Rows[i].Cells["GROUP_CODE"].Value.ToString();
                            txtDNSelect.Text = dgvDN.Rows[i].Cells["DELIVERY_NO"].Value.ToString();
                            isfirst = false;
                        }
                    }
                }
            }

            //显示dgv  需ZC箱号 

            showNeedZCCartonList(strGroupcode);

            //显示dgv 已经ZC箱号

            showZCOKCartonList(strGroupcode);
            //
            //显示多余PICK箱数

            showZCNoPpartPickCarton(strGroupcode);


            if (dgvPackCarton.Rows.Count == 1 && dgvPick.Rows.Count == 1)
            {
                ShowMsg("还原结束，转IT处理", 0);
                btnStart.Enabled = true;
                return;
            }


            txtPackCarton.Enabled = true;
            txtPackCarton.SelectAll();
            txtPackCarton.Focus();



            btnStart.Enabled = true;

        }
        private void showNeedZCCartonList(string strGroupcode)
        {

            dgvPackCarton.DataSource = null;
            dgvPackCarton.Rows.Clear();
            RollbackBll rb = new RollbackBll();
            DataTable dt = rb.showNeedZCCartonListDataTable(strGroupcode);
            if (dt == null || dt.Rows.Count == 0)
            {
                //ShowMsg("NG，查不资料！", 0);
            }
            else
            {
                //dgvPackCarton.DataSource = dt;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dgvPackCarton.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    //累加序号
                    dr.HeaderCell.Value = (i + 1).ToString();
                    dr.Cells[0].Value = dt.Rows[i]["PALLET_NO"].ToString();
                    dr.Cells[1].Value = dt.Rows[i]["DELIVERY_NO"].ToString();
                    dr.Cells[2].Value = dt.Rows[i]["CARTON_NO"].ToString();

                    try
                    {
                        dgvPackCarton.Invoke((MethodInvoker)delegate ()
                        {
                            dgvPackCarton.Rows.Add(dr);
                        });
                    }
                    catch (Exception e1)
                    {
                        ShowMsg(e1.ToString(), 0);
                        return;
                    }
                }
            }
        }
        //showZCOKCartonList
        private void showZCOKCartonList(string strGroupcode)
        {

            dgvCartonZCOK.DataSource = null;
            dgvCartonZCOK.Rows.Clear();
            RollbackBll rb = new RollbackBll();
            DataTable dt = rb.showZCOKCartonListDataTable(strGroupcode);
            if (dt == null || dt.Rows.Count == 0)
            {
                //ShowMsg("NG，查不资料！", 0);
            }
            else
            {
                //dgvCartonZCOK.DataSource = dt;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dgvCartonZCOK.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    //累加序号
                    dr.HeaderCell.Value = (i + 1).ToString();
                    dr.Cells[0].Value = dt.Rows[i]["CARTON_NO"].ToString();
                    try
                    {
                        dgvCartonZCOK.Invoke((MethodInvoker)delegate ()
                        {
                            dgvCartonZCOK.Rows.Add(dr);
                        });
                    }
                    catch (Exception e1)
                    {
                        ShowMsg(e1.ToString(), 0);
                        return;
                    }
                }
            }
        }


        //showZCNoPpartPickCartonDataTable
        private void showZCNoPpartPickCarton(string strGroupcode)
        {

            dgvPick.DataSource = null;
            dgvPick.Rows.Clear();
            RollbackBll rb = new RollbackBll();
            DataTable dt = rb.showZCNoPpartPickCartonDataTable(strGroupcode);
            if (dt == null || dt.Rows.Count == 0)
            {
                //ShowMsg("NG，查不资料！", 0);
            }
            else
            {
                //dgvCartonZCOK.DataSource = dt;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dgvPick.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    //累加序号
                    dr.HeaderCell.Value = (i + 1).ToString();
                    dr.Cells[0].Value = dt.Rows[i]["PALLET_NO2"].ToString();
                    dr.Cells[0].Value = dt.Rows[i]["ICTPN"].ToString();
                    dr.Cells[0].Value = dt.Rows[i]["ZCCARTONCOUNT"].ToString();
                    try
                    {
                        dgvPick.Invoke((MethodInvoker)delegate ()
                        {
                            dgvPick.Rows.Add(dr);
                        });
                    }
                    catch (Exception e1)
                    {
                        ShowMsg(e1.ToString(), 0);
                        return;
                    }
                }
            }
        }

        private void reflashform()
        {
            reflashlabel();

        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            btnEnd.Enabled = false;
            txtPackCarton.Enabled = false;



            btnEnd.Enabled = true;
        }

        private void dt_start_ValueChanged(object sender, EventArgs e)
        {
            initForm();
        }

        private void dt_end_ValueChanged(object sender, EventArgs e)
        {
            initForm();
        }

        private void initForm()
        {
            dgvDN.DataSource = null;
            txtSmId.Text = "";
            txtGroup.Text = "";
            txtDNSelect.Text = "";
        }

        private void txtPackCarton_KeyDown(object sender, KeyEventArgs e)
        {
            //可扫描箱号/栈板号进行Pick
            string strCarton = txtPackCarton.Text.Trim();

            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            ShowMsg("", -1);


            if (string.IsNullOrEmpty(strCarton))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("输入的Carton不能为空！", 0);
                txtPackCarton.SelectAll();
                txtPackCarton.Focus();
                return;
            }

            RollbackBll plb = new RollbackBll();
            strCarton = plb.DelPrefixCartonSN(strCarton);

            string strGroupcode = txtGroup.Text;
            if (string.IsNullOrEmpty(strGroupcode))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("组号不能为空！", 0);
                txtPackCarton.SelectAll();
                txtPackCarton.Focus();
                return;
            }


            RollbackBll rb = new RollbackBll();
            string strResult = string.Empty;
            string errorMessage = string.Empty;
            strResult = rb.RBSNbyDN(strCarton, strGroupcode, out errorMessage);
            if (strResult.Contains("OK"))
            {

                ShowMsg(errorMessage, -1);
                txtPackCarton.Text = "";
                txtPackCarton.Focus();
            }
            else if (strResult.Contains("NG"))
            {
                ShowMsg(errorMessage, 0);
                txtPackCarton.SelectAll();
                txtPackCarton.Focus();

            }

            else
            {
                ShowMsg("检查CARTONNO获得特殊异常", 0);
                txtPackCarton.SelectAll();
                txtPackCarton.Focus();
            }
            //刷新界面

            //显示dgv  需ZC箱号 

            showNeedZCCartonList(strGroupcode);

            //显示dgv 已经ZC箱号

            showZCOKCartonList(strGroupcode);
            //
            //显示多余PICK箱数

            showZCNoPpartPickCarton(strGroupcode);


            if (dgvPackCarton.Rows.Count == 1 && dgvPick.Rows.Count == 1)
            {
                ShowMsg("还原结束，转IT处理", 0);
                btnStart.Enabled = true;
            }

        }

        private void btnCancelSID_Click(object sender, EventArgs e)
        {
            CancelSID zcs = new CancelSID();
            zcs.ShowDialog();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            ZCshipmentid zcs = new ZCshipmentid();
            zcs.ShowDialog();
        }
    }
}
