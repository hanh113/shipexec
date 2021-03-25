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
    public partial class fMain2 : Form
    {
        public fMain2()
        {
            InitializeComponent();
        }
        private void fMain_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " (" + SajetCommon.g_sFileVersion + ")";
            labShow.Text = labShow.Text + " (" + SajetCommon.g_sFileVersion + ")";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

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
        //private string g_SQL;
        //private Int32 g_curRow = -1;    //当前选中行号
        //private string g_partNo = "";   //当前选中的料号

        private string g_sUserID = ClientUtils.UserPara1;
        private string g_sUserNo = ClientUtils.fLoginUser;
        private string g_sProgram = ClientUtils.fProgramName;
        private string g_sFunction = ClientUtils.fFunctionName;
        private string g_sExeName = ClientUtils.fCurrentProject;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ShowMsg("", -1);
            string strDN = txtDN.Text.Trim();
            //check
            string strcheck = string.Empty;
            if (string.IsNullOrEmpty(strDN))
            {
                //ShowMsg("DN为空",0);
                return;
            }
            RollbackBll rb = new RollbackBll();
            DataTable dtDN = rb.GetDNInfoDataTable(strDN);
            if (dtDN != null && dtDN.Rows.Count > 0)
            {

                dgvDN.DataSource = dtDN;
                dgvDN.AutoResizeColumns();
            }
            else
            {
                LibHelper.MediasHelper.PlaySoundAsyncByHold();
                ShowMsg("NG,未找到DN信息或不能ZC", 0);
                return;
            }

            DataTable dtPICK = rb.GetPICKInfoDataTable(strDN);
            if (dtPICK != null && dtPICK.Rows.Count > 0)
            {

                dgvPick.DataSource = dtPICK;
                dgvPick.AutoResizeColumns();
            }
            else
            {
                LibHelper.MediasHelper.PlaySoundAsyncByHold();
                ShowMsg("NG,未找到DN对应集货单信息", 0);
                return;
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

        private void btnTest_Click(object sender, EventArgs e)
        {
            ZCshipmentid zcs = new ZCshipmentid();
            zcs.ShowDialog();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ShowMsg("", -1);

            string strDN = txtDN.Text;

            //检查dgv数据,
            if (dgvDN.RowCount == 0 || dgvPick.RowCount == 0)
            {
                ShowMsg("请先查找合适的DN，才能开始作业。", 0);
                return;
            }


            //HYQ:这里有个问题：查询后txtDN 能改，不点查询 直接点开始作业。

            if (!dgvDN.Rows[0].Cells["DELIVERY_NO"].Value.ToString().Equals(strDN) || !dgvPick.Rows[0].Cells["DELIVERY_NO"].Value.ToString().Equals(strDN))
            {
                ShowMsg("输入的DN变更后，需点查询按钮，再点开始作业。", 0);
                return;
            }


            //1.检查DN的状态
            //check
            string strcheck = string.Empty;
            //CheckDNtoShipmentID(string shipmentid, out string dntype, out string errmsg)
            RollbackBll rb = new RollbackBll();
            string dnType = string.Empty;
            string errorMessage = string.Empty;
            strcheck = rb.CheckDNtoShipmentID(strDN, out dnType, out errorMessage);


            if (!strcheck.Equals("OK"))
            {
                ShowMsg(errorMessage, 0);
                return;
            }

            //2. 提示是否 锁定shipmentID

            lblDNmodel.Text = "";
            lblDNmodel.Text = dnType;
            if (dnType.Equals("A0"))
            {
                DialogResult result = ShowMsg("这是多DN对应一个ShipmentID的模式，确定开始还原DN吗？", 2);
                if (result == DialogResult.No) { return; }
            }
            if (dnType.Equals("A1"))
            {
                DialogResult result = ShowMsg("这是多DN对应一个ShipmentID的模式，确定继续还原DN吗？", 2);
                if (result == DialogResult.No) { return; }
            }

            if (dnType.Equals("C0"))
            {
                DialogResult result = ShowMsg("这是一个DN对应一个ShipmentID的模式，确定开始还原DN吗？", 2);
                if (result == DialogResult.No) { return; }
            }
            if (dnType.Equals("C1"))
            {
                DialogResult result = ShowMsg("这是一个DN对应一个ShipmentID的模式，确定继续还原DN吗？", 2);
                if (result == DialogResult.No) { return; }
            }

            if (dnType.Equals("E"))
            {
                DialogResult result = ShowMsg("这个DN还未开始作业,无需刷入序号,是否让系统自动处理？", 2);
                if (result == DialogResult.No) { return; }
            }
            //锁定未完成的shipmentid， PPS系统不能再作业此shipmentid。

            //Lock
            string strlock = string.Empty;
            errorMessage = string.Empty;
            strlock = rb.LockDNtoShipmentID(strDN, out errorMessage);


            if (!strlock.Equals("OK"))
            {
                ShowMsg(errorMessage, 0);
                return;
            }
            DataTable dtCarton = rb.GetDN_ZCStatusDataTable(strDN);
            if (dtCarton != null && dtCarton.Rows.Count > 0)
            {

                dgvPackCarton.DataSource = dtCarton;
                dgvPackCarton.AutoResizeColumns();
            }
            //检查OK锁定界面
            lockform();

            //可以开始刷序号还原。
        }

        private void btnEnd_Click(object sender, EventArgs e)

        {
            ShowMsg("", -1);
            dgvDN.DataSource = null;
            dgvPackCarton.DataSource = null;
            dgvPick.DataSource = null;
            //解锁界面
            unlockform();

        }

        private void lockform()
        {
            txtDN.Enabled = false;
            btnSearch.Enabled = false;
            txtPackCarton.Enabled = true;
            //txtPickCarton.Enabled = true;
            txtPackCarton.Focus();

        }

        private void unlockform()
        {
            ShowMsg("", -1);
            txtDN.Enabled = true;
            btnSearch.Enabled = true;
            txtPackCarton.Enabled = false;
            //txtPickCarton.Enabled = false;
            txtDN.Focus();
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

            string strDN = txtDN.Text.Trim();

            if (!dgvDN.Rows[0].Cells["DELIVERY_NO"].Value.ToString().Equals(strDN) || !dgvPick.Rows[0].Cells["DELIVERY_NO"].Value.ToString().Equals(strDN))
            {
                ShowMsg("输入的DN变更后，需点查询按钮，再点开始作业。", 0);
                return;
            }
            RollbackBll plb = new RollbackBll();
            strCarton = plb.DelPrefixCartonSN(strCarton);


            RollbackBll rb = new RollbackBll();
            string strResult = string.Empty;
            string errorMessage = string.Empty;
            strResult = rb.RBSNbyDN(strCarton, strDN, out errorMessage);
            if (strResult.Contains("OK"))
            {

                ShowMsg(errorMessage, 0);
                txtPackCarton.Text = "";
                txtPackCarton.Focus();
            }
            else if (strResult.Contains("NG"))
            {
                ShowMsg(errorMessage, 0);
                txtPackCarton.SelectAll();
                txtPackCarton.Focus();

            }
            else if (strResult.Contains("FINISH"))
            {
                ShowMsg(errorMessage, 0);
            }
            else
            {
                ShowMsg("检查CARTONNO获得特殊异常", 0);
                txtPackCarton.SelectAll();
                txtPackCarton.Focus();
            }
            //刷新界面

            DataTable dtCarton = rb.GetDN_ZCStatusDataTable(strDN);
            if (dtCarton != null && dtCarton.Rows.Count > 0)
            {

                dgvPackCarton.DataSource = dtCarton;
                dgvPackCarton.AutoResizeColumns();
            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            fMain fmn = new fMain();
            fmn.ShowDialog();
        }
    }
}
