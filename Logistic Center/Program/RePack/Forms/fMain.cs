using ClientUtilsDll;
using ClientUtilsDll.Forms;
using Repack.Beans;
using RePack.Beans;
using RePack.Core.Common;
using RePack.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RePack
{
    public partial class fMain : PPSForm
    {
        #region fields & properties
        private CommonProcessExcute core;
        private ExecutionResult exeRes;
        private baseinfo bean;
        public static bool bType;
        #endregion

        #region construe method
        public fMain()
        {
            exeRes = new ExecutionResult();
            core = new CommonProcessExcute();
            bean = new baseinfo();
            InitializeComponent();
        }
        #endregion

        #region Form events

        private void btnClosePallet_Click(object sender, EventArgs e)
        {
            bType = false;
            fClosePallet fClosePallet = string.IsNullOrEmpty(tbPallet.Text.Trim()) ? new fClosePallet() : new fClosePallet(tbPallet.Text.Trim());
            fClosePallet.ShowDialog();
            if (bType)
            {
                tbPallet.Text = string.Empty;
                //  RefreshData();
            }
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SetupInfo.strLine) && !string.IsNullOrEmpty(SetupInfo.strStation))
            {
                this.prgTitle.Text = string.Format("Line: {0}  Station: {1} ver:{2}", SetupInfo.strLine, SetupInfo.strStation, this.ProductVersion);
            }
        }
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
        }

        private void tbMo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            if (!string.IsNullOrEmpty(SetupInfo.strLine) && !string.IsNullOrEmpty(SetupInfo.strStation))
            {
                this.prgTitle.Text = string.Format("Line: {0}  Station: {1} ver:{2}", SetupInfo.strLine, SetupInfo.strStation, this.ProductVersion);
            }
            bean.strMo = tbMo.Text.Trim();
            exeRes = core.GetMoInfo(bean);
            if (exeRes.Status)
            {
                dgvMoInfo.DataSource = bean.dtMo;
                tbPallet.Enabled = true;
                cbAuto.Enabled = true;
                cbAuto.Checked = false;
                tbMo.Enabled = false;
                ShowMSG("Chose Auto Pallet No or Manual Input Pallet No.", 3);
            }
            else
            {
                RefreshData();
                ShowMSG(exeRes.Message, 0);
            }
        }

        private void cbAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAuto.Checked)
            {
                //自动获取栈板号
                tbPallet.Text = "";
                tbPallet.Enabled = false;
                cbAuto.Enabled = false;
                tbSN.Text = "";
                tbSN.Enabled = true;
                tbSN.Focus();
            }
            else
            {
                //检查栈板
                tbPallet_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            }
        }

        private void tbPallet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            bean.strPallet = tbPallet.Text.Trim().ToUpper();
            if (bean.strPallet.Equals("UNDO"))
            {
                RefreshData(); return;
            }
            exeRes = core.CheckPalletNo(bean);
            if (exeRes.Status)
            {
                tbPallet.Text = bean.strPallet;
                tbPallet.Enabled = false;
                cbAuto.Enabled = false;
                tbSN.Text = "";
                tbSN.Enabled = true;
                tbSN.Focus();
                ShowMSG("Scan Sn pls.", 3);
            }
            else
            {
                tbPallet.SelectAll();
                tbPallet.Focus();
                cbAuto.Enabled = true;
                ShowMSG(exeRes.Message, 0);
            }
        }

        private void tbSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (tbSN.Text.Trim().ToUpper().Equals("UNDO"))
            {
                RefreshData(); return;
            }
            //重新取值
            bean.bAutoFlag = cbAuto.Checked;
            bean.strPallet = tbPallet.Text;
            bean.strSN = tbSN.Text.Trim().ToUpper();
            exeRes = core.CheckSN(bean);
            if (exeRes.Status)
            {
                dgvPalletInfo.DataSource = bean.dtSnDetail;
                lbQty.Text = bean.dtSnDetail.Rows.Count.ToString();

                tbPallet.Text = bean.strPallet;
                tbSN.SelectAll();
                tbSN.Focus();
                ShowMSG("Scan the next carton.", 3);
                PrintCartonLabel(bean.dtCartonLabel);
            }
            else
            {
                DataTable dtDetail = (DataTable)dgvPalletInfo.DataSource;
                if (dtDetail != null)
                {
                    dtDetail.Rows.Clear();
                    dgvPalletInfo.DataSource = dtDetail;
                }
                lbQty.Text = "0";
                tbSN.SelectAll();
                tbSN.Focus();
                ShowMSG(exeRes.Message, 0);
            }
        }



        private void btnPrint_Click(object sender, EventArgs e)
        {

            fReprint fReprint = string.IsNullOrEmpty(tbPallet.Text.Trim()) ? new fReprint() : new fReprint(tbPallet.Text.Trim());
            fReprint.ShowDialog();
        }
        #endregion

        #region inner method
        private void RefreshData()
        {
            DataTable dtMo = (DataTable)dgvMoInfo.DataSource;
            if (dtMo != null)
            {
                dtMo.Rows.Clear();
                dgvMoInfo.DataSource = dtMo;
            }
            DataTable dtDetail = (DataTable)dgvPalletInfo.DataSource;
            if (dtDetail != null)
            {
                dtDetail.Rows.Clear();
                dgvPalletInfo.DataSource = dtDetail;
            }
            lbQty.Text = "0";
            tbSN.Text = "";
            tbSN.Enabled = false;
            tbPallet.Text = "";
            tbPallet.Enabled = false;
            tbMo.Enabled = true;
            tbMo.SelectAll();
            tbMo.Focus();
            cbAuto.Enabled = false;
        }

        public  void PrintCartonLabel(DataTable dtCartonInfo)
        {
            try
            {
                if (dtCartonInfo is null)
                {
                    this.ShowMSG("Get Carton Label Params Fail", 0);
                    return;
                }
                string labelparams = string.Format("UPC|PART_NO|SN|MODEL\r\n{0}|{1}|{2}|{3}", dtCartonInfo.Rows[0]["upc_code"].ToString(), dtCartonInfo.Rows[0]["custpart"].ToString(), dtCartonInfo.Rows[0]["carton_no"].ToString(), dtCartonInfo.Rows[0]["custpartmodel"].ToString());
                string strLstPath = System.Windows.Forms.Application.StartupPath + @"\Repack\Label\SingleCartonLabel.lst";
                if (File.Exists(strLstPath))
                {
                    File.Delete(strLstPath);
                }
                File.AppendAllText(strLstPath, labelparams, Encoding.Default);
                string strBtwPath = System.Windows.Forms.Application.StartupPath + @"\Repack\Label\SingleCartonLabel.btw";
                ClientUtils.PrintBartender(strBtwPath, strLstPath);

                ShowMSG("Print Success", 3);
            }
            catch (Exception ex)
            {
                ShowMSG(ex.Message, 0);
            }
        }
        #endregion

    }
}
