using ClientUtilsDll;
using ClientUtilsDll.Forms;
using Stock_Out.Core;
using Stock_Out.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_Out
{
    public partial class fMain : PPSForm
    {
        #region fields & properties
        private corebridge core;
        private ExecutionResult exeRes;
        // private string strUser;
        #endregion

        #region construe method
        public fMain()
        {
            this.prgTitle.Text = "STOCK_OUT   ver:" + this.ProductVersion;
            core = new corebridge();
            exeRes = new ExecutionResult();
            //  strUser = ClientUtils.fLoginUser;
            InitializeComponent();
        }
        #endregion

        #region form events
        private void setupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //待考虑是否添加站别 线别选项
            //fSetup fSetup = new fSetup();
            //fSetup.ShowDialog();
        }

        private void tbScan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            exeRes = core.Excute(tbScan.Text.Trim());
            if (exeRes.Status)
            {
                dgvInfo.DataSource = (DataTable)exeRes.Anything;
                lbQty.Text = ((DataTable)exeRes.Anything).Rows.Count.ToString();
                ShowMSG(exeRes.Message, 4);
            }
            else
            {
                RefreshForm();
                ShowMSG(exeRes.Message, 0);
            }
            tbScan.Focus();
            tbScan.SelectAll();
        }

        private void btnReprint_Click(object sender, EventArgs e)
        {
            fReprint fReprint = new fReprint();
            fReprint.ShowDialog();
        }
        #endregion

        #region inner method
        public void RefreshForm()
        {
            DataTable dtdgv = (DataTable)dgvInfo.DataSource;
            if (dtdgv != null)
            {
                dtdgv.Rows.Clear();
                dgvInfo.DataSource = dtdgv;
            }

            ShowMSG("Message", 4);
            lbQty.Text = "0";
        }
        #endregion


    }
}
