using ClientUtilsDll;
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

namespace cWHConfig.Forms
{
    public partial class fDetails : Form
    {
        #region 字段属性
        baseinfo bean;
        corebridge core;
        ExecutionResult exeRes;
        private string MainCombType;
        #endregion

        #region 构造函数
        public fDetails(baseinfo _baseinfo)
        {
            exeRes = new ExecutionResult();
            bean = _baseinfo;
            MainCombType = bean.CombType;
            core = new corebridge();
            InitializeComponent();
        }

        #endregion

        #region 窗体事件
        private void fDetails_Load(object sender, EventArgs e)
        {
            Initial_Form();//初始化窗体
            //控件赋值
            SetValue();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            exeRes = core.InsertData(MainCombType,
                string.IsNullOrEmpty(txtStoreNo.Text) ? cbStoreNo.Text : txtStoreNo.Text.Trim(),
               string.IsNullOrEmpty(txtWareHouseNo.Text) ? cbWareHouseNo.Text : txtWareHouseNo.Text.Trim(),
               string.IsNullOrEmpty(txtLocationNo.Text) ? cbLocationNo.Text : txtLocationNo.Text.Trim());
            MessageBox.Show(exeRes.Message);
            if (exeRes.Status)
            {
                DialogResult = DialogResult.OK;
            }
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cbStoreNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            bean.sStoreNo = cbStoreNo.Text;
            dataTable = core.GetWHbyStore(bean);
            if (dataTable.Rows.Count > 0)
            {
                cbWareHouseNo.Items.Clear();
                cbWareHouseNo.Items.AddRange(dataTable.AsEnumerable().Select(d => d.Field<string>("WAREHOUSE_NO")).ToArray());
                cbWareHouseNo.SelectedIndex = 0;
            }
        }
        #endregion

        #region 私有方法
        private void SetValue()
        {
            DataTable dt = new DataTable();

            //根据前台选择有效性进行筛选
            if (MainCombType.Equals("WAREHOUSE") || MainCombType.Equals("LOCATION"))
            {
                //查询仓库信息赋值到comb中
                bean.CombType = "STORE";
                dt = core.GetDataByType(bean);
                if (dt.Rows.Count > 0)
                {
                    cbStoreNo.Items.Clear();
                    cbStoreNo.Items.AddRange(dt.AsEnumerable().Select(d => d.Field<string>("STORE_NO")).ToArray());
                    cbStoreNo.SelectedIndex = 0;
                }

            }
        }

        private void Initial_Form()
        {
            switch (MainCombType)
            {
                case "STORE":
                    cbStoreNo.Visible = false;
                    lbWarehouseNo.Visible = false;
                    txtWareHouseNo.Visible = false;
                    cbWareHouseNo.Visible = false;
                    lbLocationNo.Visible = false;
                    txtLocationNo.Visible = false;
                    cbLocationNo.Visible = false;
                    break;
                case "WAREHOUSE":
                    txtStoreNo.Visible = false;
                    cbWareHouseNo.Visible = false;
                    lbLocationNo.Visible = false;
                    txtLocationNo.Visible = false;
                    cbLocationNo.Visible = false;
                    break;
                case "LOCATION":
                    txtStoreNo.Visible = false;
                    txtWareHouseNo.Visible = false;
                    cbLocationNo.Visible = false;
                    break;
                default:
                    break;
            }
        }

        #endregion 

    }
}
