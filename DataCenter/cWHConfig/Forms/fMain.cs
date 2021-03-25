using ClientUtilsDll;
using ClientUtilsDll.Forms;
using cWHConfig.Bean;
using cWHConfig.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/*
* combShow:
*   Enabled
*   Disabled
*   All
* combType:
*   WareHouse
*   Zone
*   Location
*/
namespace cWHConfig
{
    public partial class fMain : PPSForm
    {
        #region 字段属性
        baseinfo bean;
        corebridge core;
        ExecutionResult exeRes;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        #endregion

        #region 构造函数
        public fMain()
        {
            InitializeComponent();
            bean = new baseinfo();
            core = new corebridge();
            exeRes = new ExecutionResult();
        }
        #endregion

        #region 窗体事件
        private void fMain_Load(object sender, EventArgs e)
        {
            prgTitle.Text = string.Format("{0}:{1}", bean.g_sFunction, bean.g_sVersion);
            combShow.SelectedIndex = 0;
            combType.SelectedIndex = 0;
            bean.CombType = combType.Text;
            bean.Enabled = combShow.SelectedIndex;
            dgvAll.Focus();
            SuccessMSG("OK");
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            bean.CombType = combType.Text;
            Forms.fDetails fDetails = new Forms.fDetails(bean);
            //fDetails.Show(); 
            if (fDetails.ShowDialog() == DialogResult.OK)
            {
                combType_SelectedIndexChanged(sender, e);
            }
        }

        public void combType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bean.CombType = combType.Text;
            dgvAll.DataSource = core.GetDataByType(bean);
            if (dgvAll.Rows.Count > 0)
            {
                //   dgvAll.Rows[0].Selected = true;
                dgvAll_CellClick(dgvAll, new DataGridViewCellEventArgs(0, 0));
            }
            combFilter.Items.Clear();
            switch (bean.CombType)
            {
                case "STORE":
                    combFilter.Items.Add("STORE_NO");
                    break;
                case "WAREHOUSE":
                    combFilter.Items.AddRange(new string[] { "STORE_NO", "WAREHOUSE_NO" });
                    break;
                case "LOCATION":
                    combFilter.Items.AddRange(new string[] { "LOCATION_NO", "WAREHOUSE_NO" });
                    break;
                default:
                    break;
            }
            SuccessMSG("OK");
        }

        private void combShow_SelectedIndexChanged(object sender, EventArgs e)
        {
             
            btnDelete.Visible = (combShow.SelectedIndex == 1);
            btnDisabled.Visible = (combShow.SelectedIndex == 0);
            btnEnabled.Visible = (combShow.SelectedIndex == 1);
            bean.Enabled = combShow.SelectedIndex;
            bean.CombType = combType.Text;
            dgvAll.DataSource = core.GetDataByType(bean);
            if (dgvAll.Rows.Count > 0)
            {
                //dgvAll.Rows[0].Selected = true;
                dgvAll_CellClick(dgvAll, new DataGridViewCellEventArgs(0, 0));
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            bean.CombType = combType.Text;
            Forms.Modify fModify = new Forms.Modify(bean);
            if (fModify.ShowDialog() == DialogResult.OK)
            {
                combType_SelectedIndexChanged(sender, e);
            }
        }

        private void dgvAll_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                bean.CombType = combType.Text;
                switch (bean.CombType)
                {
                    case "STORE":
                        bean.sStoreNo = dgvAll.Rows[e.RowIndex].Cells["STORE_NO"].Value.ToString();
                        break;
                    case "WAREHOUSE":
                        bean.sStoreNo = dgvAll.Rows[e.RowIndex].Cells["STORE_NO"].Value.ToString();
                        bean.sWareHouse = dgvAll.Rows[e.RowIndex].Cells["WAREHOUSE_NO"].Value.ToString();
                        break;
                    case "LOCATION":
                        bean.sLocation = dgvAll.Rows[e.RowIndex].Cells["LOCATION_NO"].Value.ToString();
                        bean.sWareHouse = dgvAll.Rows[e.RowIndex].Cells["WAREHOUSE_NO"].Value.ToString();
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnDisabled_Click(object sender, EventArgs e)
        {
            bean.sFlag = "N";// disabled
            exeRes = core.UpdateData(bean.CombType, bean.sStoreNo, bean.sWareHouse, "", bean.sLocation, bean.sFlag);
            combType_SelectedIndexChanged(sender, e);
        }

        private void btnEnabled_Click(object sender, EventArgs e)
        {
            bean.sFlag = "Y";// enabled
            exeRes = core.UpdateData(bean.CombType, bean.sStoreNo, bean.sWareHouse, "", bean.sLocation, bean.sFlag);
            combType_SelectedIndexChanged(sender, e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bean.sFlag = "D";// Deleted
            exeRes = core.UpdateData(bean.CombType, bean.sStoreNo, bean.sWareHouse, "", bean.sLocation, bean.sFlag);
            combType_SelectedIndexChanged(sender, e);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = "xls";
                saveFileDialog.Filter = "All Files(*.xls)|*.xls";
                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                string sFileName = saveFileDialog.FileName;

                ExportExcel.CreateExcel Export = new ExportExcel.CreateExcel(sFileName);
                if (sender == btnExport)
                    Export.ExportToExcel(dgvAll);
                SuccessMSG("导出Excel 成功");
            }
            catch (Exception ex)
            {
                ErrorMSG("导出失败：" + ex.Message);
            }
        }

        private void tbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Return)
                return;
            if (!string.IsNullOrEmpty(combFilter.Text) && !string.IsNullOrEmpty(tbFilter.Text.Trim()))
            {
                bean.CombFilter = combFilter.Text;
                bean.FilterText = tbFilter.Text.Trim();
                dgvAll.DataSource = core.GetDataByType(bean);
                bean.CombFilter = null;//查询后置空
            }
        }
        #endregion

    }
}
