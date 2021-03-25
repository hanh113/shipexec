using ClientUtilsDll;
using ClientUtilsDll.Forms;
using Stock_In.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_In
{
    public partial class fMain : PPSForm
    {
        #region fields&properties
        private corebridge core;
        private ExecutionResult exeRes;
        #endregion
        #region construe method
        public fMain()
        {
            core = new corebridge();
            exeRes = new ExecutionResult();
            InitializeComponent();
            this.prgTitle.Text = "STOCK_IN   ver:" + this.ProductVersion;
        }
        #endregion

        #region Form events
        private void fMain_Load(object sender, EventArgs e)
        {
            exeRes = core.GetWarehouseInfo();
            if (exeRes.Status)
            {
                // string[] arrLine = ((DataTable)exeRes.Anything).AsEnumerable().Select(x => x.Field<string>("WAREHOUSE_NO")).ToArray();
                //cbWarehouse.Items.AddRange(arrLine);
                cbWarehouse.DataSource = (DataTable)exeRes.Anything;
                cbWarehouse.DisplayMember = "WAREHOUSE_NO";
                cbWarehouse.ValueMember = "WAREHOUSE_ID";
                this.cbWarehouse.SelectedIndex = -1;
                ShowMSG("Chose WareHouse Info First.", 3);
            }
            else
            {
                ShowMSG(exeRes.Message, 0);
            }
        }


        private void cbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbWarehouse.SelectedIndex < 0 || string.IsNullOrEmpty(this.cbWarehouse.ValueMember.ToString())) return;
            exeRes = core.GetLocatioinByWH(this.cbWarehouse.SelectedValue.ToString());
            if (exeRes.Status)
            {
                cbLocation.DataSource = (DataTable)exeRes.Anything;
                cbLocation.DisplayMember = "LOCATION_NO";
                cbLocation.ValueMember = "LOCATION_ID";
                this.cbLocation.SelectedIndex = -1;
                ShowMSG("Scan the Pallet No pls.", 3);
            }
            else
            {
                ShowMSG(exeRes.Message, 0);
            }
        }

        private void tbPallet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || string.IsNullOrEmpty(cbLocation.SelectedValue.ToString()))
                return;
            exeRes = core.GetPalletInfo(tbPallet.Text.Trim().ToUpper());
            if (exeRes.Status)
            {
                lbQty.Text = ((DataTable)exeRes.Anything).Rows.Count.ToString();
                dgvInfo.DataSource = (DataTable)exeRes.Anything;
            }
            else
            {
                RefreshData();
                ShowMSG(exeRes.Message, 0);
            }
            btnStock.Enabled = exeRes.Status;
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbPallet.Text.Trim().ToUpper()) || string.IsNullOrEmpty(cbLocation.SelectedValue.ToString()))
                return;
            RefreshData();
            //入库动作
            exeRes = core.Stock_IN(cbLocation.SelectedValue.ToString(), tbPallet.Text.Trim().ToUpper());
            if (exeRes.Status)
                dgvInfo.DataSource = (DataTable)exeRes.Anything;
            ShowMSG(exeRes.Message, exeRes.Status ? 3 : 0);
            btnStock.Enabled = !exeRes.Status;
        }

        #endregion
        #region inner method
        private void RefreshData()
        {

            DataTable dtDetail = (DataTable)dgvInfo.DataSource;
            if (dtDetail != null)
            {
                dtDetail.Rows.Clear();
                dgvInfo.DataSource = dtDetail;
            }
            lbQty.Text = "0";
            tbPallet.SelectAll();
            tbPallet.Focus();
        }

        #endregion
    }
}
