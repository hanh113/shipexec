using ClientUtilsDll;
using ClientUtilsDll.Forms;
using Split.Beans;
using Split.Core.Common;
using Split.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Split
{
    public partial class fMain : PPSForm
    {
        #region fields & properties
        private CommonProcessExcute core;
        private ExecutionResult exeRes;
        private baseinfo bean;
        #endregion

        #region construe method
        public fMain()
        {
            core = new CommonProcessExcute();
            exeRes = new ExecutionResult();
            bean = new baseinfo();
            InitializeComponent();
        }
        #endregion

        #region FormEvents
        private void setupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fSetup fSetup = new fSetup();
            fSetup.ShowDialog();
            if (string.IsNullOrEmpty(SetupInfo.strLine) || string.IsNullOrEmpty(SetupInfo.strStation))
            {
                ShowMSG("Chose the Line and  Station by Setup Button first. ", 0);
            }
            else
            {
                this.prgTitle.Text = string.Format("Line: {0}  Station: {1} ver:{2}", SetupInfo.strLine, SetupInfo.strStation, this.ProductVersion);
                ShowMSG("Input the Mo Number pls. ", 3);
            }
            RefreshData();
        }

        private void tbMo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            if (!string.IsNullOrEmpty(SetupInfo.strLine) && !string.IsNullOrEmpty(SetupInfo.strStation))
            {
                this.prgTitle.Text = string.Format("Line: {0}  Station: {1} ver:{2}", SetupInfo.strLine, SetupInfo.strStation, this.ProductVersion);
            }
            //string strKeypart; DataTable dtBom;
            bean = new baseinfo();
            bean.strMo = tbMo.Text.Trim();
            exeRes = core.GetMoInfo(bean);
            if (exeRes.Status)
            {
                dgvBom.DataSource = bean.dtBom;
                lbPart.Text = bean.strKeyPart;
                tbSN.Enabled = true;
                tbMo.Enabled = false;
                ShowMSG("Scan the SN pls.", 3);
            }
            else
            {
                RefreshData();
                ShowMSG(exeRes.Message, 0);
            }
        }
        private void fMain_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SetupInfo.strLine) && !string.IsNullOrEmpty(SetupInfo.strStation))
            {
                this.prgTitle.Text = string.Format("Line: {0}  Station: {1} ver:{2}", SetupInfo.strLine, SetupInfo.strStation, this.ProductVersion);
            }
        }
        private void tbSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            if (!string.IsNullOrEmpty(SetupInfo.strLine) && !string.IsNullOrEmpty(SetupInfo.strStation))
            {
                this.prgTitle.Text = string.Format("Line: {0}  Station: {1} ver:{2}", SetupInfo.strLine, SetupInfo.strStation, this.ProductVersion);
            }
            if (tbSN.Text.Trim().ToUpper().Equals("UNDO"))
            {
                RefreshData(); return;
            }
            bean = new baseinfo { strMo = tbMo.Text.Trim(), strSN = tbSN.Text.Trim() };
            exeRes = core.ExecuteBySN(bean);
            if (exeRes.Status)
            {
                dgvDetail.DataSource = bean.dtSnDetail;
                lbQty.Text = bean.strSplitQty;
                //刷新工单信息
                exeRes = core.GetMoInfo(bean);
                if (exeRes.Status)
                {
                    dgvBom.DataSource = bean.dtBom;
                }
                tbSN.SelectAll();
                tbSN.Focus();
                ShowMSG("Scan the next one pls.", 3);
            }
            else
            {
                DataTable dtDetail = (DataTable)dgvDetail.DataSource;
                if (dtDetail != null)
                {
                    dtDetail.Rows.Clear();
                    dgvDetail.DataSource = dtDetail;
                }
                lbQty.Text = "0";
                tbSN.SelectAll();
                tbSN.Focus();
                ShowMSG(exeRes.Message, 0);
            }
        }
        #endregion

        #region inner method
        private void RefreshData()
        {
            DataTable dtBom = (DataTable)dgvBom.DataSource;
            if (dtBom != null)
            {
                dtBom.Rows.Clear();
                dgvBom.DataSource = dtBom;
            }
            DataTable dtDetail = (DataTable)dgvDetail.DataSource;
            if (dtDetail != null)
            {
                dtDetail.Rows.Clear();
                dgvDetail.DataSource = dtDetail;
            }
            lbPart.Text = "N/A";
            lbQty.Text = "0";
            tbSN.Text = "";
            tbSN.Enabled = false;
            tbMo.Enabled = true;
            tbMo.SelectAll();
            tbMo.Focus();
        }

        #endregion


    }
}
