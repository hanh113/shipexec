using SajetClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClientUtilsDll;

namespace WMSTransIN
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " (" + SajetCommon.g_sFileVersion + ")";
            labShow.Text = labShow.Text + " (" + SajetCommon.g_sFileVersion + ")";

            this.WindowState = FormWindowState.Maximized;

            //填充仓库信息
            string strSql = string.Format(@"
                select a.warehouse_id id, a.warehouse_no name
                  from ppsuser.wms_warehouse a
                 where a.enabled = 'Y'
                   and a.warehouse_id in ('2019010200317')
                 order by a.warehouse_no
                ");

            fillCmb(strSql, "warehouse_No", cmbWH);
        }

        private void cmbWH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbWH.SelectedValue.GetType().ToString() == "System.Data.DataRowView")
            { return; }
            string strSql = string.Format(@"
                 select distinct a.location_id id, location_no name
                             from ppsuser.wms_location a
                            where a.warehouse_id = '{0}'
                              and a.location_no like 'E3FG%' 
                            order by a.location_no"
                         , cmbWH.SelectedValue);
            fillCmb(strSql, "location_name", cmbLocation);


        }
        private void fillCmb(string strSQL, string colName, ComboBox cmb)
        {

            DataSet dts = ClientUtils.ExecuteSQL(strSQL);
            if (dts.Tables[0].Rows.Count > 0)
            {
                cmb.DataSource = dts.Tables[0];
                cmb.ValueMember = "id";
                cmb.DisplayMember = "name";

            }
            else
            {
                cmb.Items.Clear();
            }
            selecttxtCarton();
        }

        private void selecttxtCarton()
        {
            txtCarton.SelectAll();
            txtCarton.Focus();
        }

        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowMsg("", -1);
            if (cmbLocation.SelectedValue.GetType().ToString() == "System.Data.DataRowView")
            { return; }
            dgvLocation.DataSource = null;
            dgvLocation.DataSource = findData();

            selecttxtCarton();
        }

        private DataTable findData()
        {

            int iPara = 0;                              //变量项次
            string strSql;                              //SQL字符串变量
            object[][] sqlparams = new object[0][];     //查询条件传参数组
            bool isInput = false;                       //是否有输入查询条件
            DataTable dt = new DataTable();              //按查询条件，查出数据源


            #region
            strSql = "select location_no  Location,pallet_no 栈板号,part_no 料号, sum(cartonqty)箱数, sum(qty) 数量, sum(qhcartonqty) QHold影响的箱数, sum(qhqty) QHold数量 " +
                              " from ppsuser.t_location  where  1=1 ";


            //组合输入查询条件，过滤数据源
            //仓库有输入值时，添加查询条件变量
            if (cmbWH.Text.Trim() != "")
            {
                isInput = true;
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                strSql += " and warehouse_id = :warehouse";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "warehouse", cmbWH.SelectedValue };
                iPara = iPara + 1;
            }

            //储位有输入值时，添加查询条件变量
            if (cmbLocation.Text.Trim() != "")
            {
                isInput = true;
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                strSql += " and location_no = :location";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "location", cmbLocation.Text.Trim() };
                iPara = iPara + 1;
            }

            strSql += " group by location_no,pallet_no, part_no having sum(qty) > 0 and sum(cartonqty)>0 order by location_no,pallet_no, part_no";


            //判断是否有输入查询条件，调用不同的类型
            if (isInput)
            {
                dt = ClientUtils.ExecuteSQL(strSql, sqlparams).Tables[0];
            }
            else
            {
                dt = ClientUtils.ExecuteSQL(strSql).Tables[0];
            }
            #endregion


            return dt;
        }

        private void txtCarton_KeyDown(object sender, KeyEventArgs e)
        {
            //可扫描序号/箱号
            string strCarton = txtCarton.Text.Trim();
            WMSTransINBLL wb = new WMSTransINBLL();
            strCarton = wb.DelPrefixCartonSN(strCarton);

            if (e.KeyCode != Keys.Enter)
            { return; }
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strCarton))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("输入的SN/Carton不能为空！", 0);
                selecttxtCarton();
                return;
            }

            string strInSnType = string.Empty;

            if (rdoCarton.Checked)
            { strInSnType = "CARTON"; }
            else
            { strInSnType = "PALLET"; }

            DataTable dtCarton = wb.GetSNInfoDataTable(strCarton, strInSnType);
            if (dtCarton != null && dtCarton.Rows.Count > 0)
            {

                dgvCarton.DataSource = dtCarton;
                dgvCarton.AutoResizeColumns();
            }
            else
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                ShowMsg("NG,SN信息不存在", 0);
                selecttxtCarton();
                return;
            }


            // 开始处理
            string errorMessage = string.Empty;
            string strResult = wb.ExecuteWMSTransIN(strCarton, this.cmbLocation.SelectedValue.ToString(), strInSnType, out errorMessage);
            //MessageBox.Show( cmbLocation.SelectedValue.ToString());
            if (!strResult.StartsWith("OK"))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                ShowMsg(errorMessage, 0);
                selecttxtCarton();
                return;
            }
            else if (chkPrint.Checked && errorMessage.Contains("FULL"))
            {
                WHPalletLabel wp = new WHPalletLabel();
                if (wp.PrintWHPalletLabel(strCarton))
                {
                    ShowMsg("打印OK", -1);
                    LibHelper.MediasHelper.PlaySoundAsyncByOk();
                }
                else
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    ShowMsg("打印FAIL", 0);
                }
                dgvLocation.DataSource = null;
                dgvLocation.DataSource = findData();
                dgvCarton.DataSource = null;
                dgvCarton.DataSource = wb.GetSNInfoDataTable(strCarton, strInSnType);
                cmbWH.Enabled = true;
                cmbLocation.Enabled = true;
                txtCarton.Enabled = false;
                btnStart.Enabled = true;
                btnEnd.Enabled = false;
                return;
            }

            if (chkPrint.Checked && strInSnType.Equals("PALLET"))
            {
                WHPalletLabel wp = new WHPalletLabel();
                if (wp.PrintWHPalletLabel(strCarton))
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByOk();
                    ShowMsg("打印OK", -1);

                }
                else
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    ShowMsg("打印FAIL", 0);
                }
                dgvLocation.DataSource = null;
                dgvLocation.DataSource = findData();
                dgvCarton.DataSource = null;
                dgvCarton.DataSource = wb.GetSNInfoDataTable(strCarton, strInSnType);
                cmbWH.Enabled = true;
                cmbLocation.Enabled = true;
                txtCarton.Enabled = false;
                btnStart.Enabled = true;
                btnEnd.Enabled = false;
                return;
            }



            LibHelper.MediasHelper.PlaySoundAsyncByOk();
            ShowMsg("OK", -1);
            dgvLocation.DataSource = null;
            dgvLocation.DataSource = findData();
            dgvCarton.DataSource = null;
            dgvCarton.DataSource = wb.GetSNInfoDataTable(strCarton, strInSnType);
            selecttxtCarton();

        }


        private void btnStart_Click(object sender, EventArgs e)
        {

            string strLocation = cmbLocation.Text;
            if (string.IsNullOrEmpty(strLocation))
            {
                ShowMsg("不得输入空白储位", 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                return;
            }


            for (int i = 0; i < cmbLocation.Items.Count; i++)
            {

                if (strLocation == cmbLocation.GetItemText(cmbLocation.Items[i]))
                {
                    cmbLocation.SelectedIndex = i;
                    cmbWH.Enabled = false;
                    cmbLocation.Enabled = false;
                    txtCarton.Enabled = true;
                    btnStart.Enabled = false;
                    btnEnd.Enabled = true;
                    selecttxtCarton();
                    return;
                }

                if ((i == cmbLocation.Items.Count - 1) && !cmbLocation.GetItemText(cmbLocation.Items[i]).Equals(strLocation))
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    cmbLocation.SelectAll();
                    cmbLocation.Focus();
                    ShowMsg("输入的储位无效", 0);
                    return;
                }

            }

        }

        private void btnRePrint_Click(object sender, EventArgs e)
        {
            TextMsg.Text = "";
            TextMsg.BackColor = Color.Blue;
            rePrintLabel pr = new rePrintLabel();
            pr.ShowDialog();
            //fCheck fcheck = new fCheck();
            //if (fcheck.ShowDialog() != DialogResult.OK)
            //{
            //    ShowMsg("账号权限不正确", 0);
            //    return;
            //}
            //else
            //{
            //    rePrintLabel pr = new rePrintLabel();
            //    pr.ShowDialog();
            //}
        }

        private void rdoPallet_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPallet.Checked) { txtCarton.Enabled = true; }
            else { txtCarton.Enabled = false; }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (dgvCarton.RowCount > 0)
            {
                string strSN = dgvCarton.Rows[0].Cells["Carton_no"].Value.ToString();
                WHPalletLabel wp = new WHPalletLabel();
                if (wp.PrintWHPalletLabel(strSN))
                {
                    TextMsg.Text = "打印OK";
                }
                else
                {
                    TextMsg.Text = "打印FAIL";
                }

            }
            cmbWH.Enabled = true;
            cmbLocation.Enabled = true;
            txtCarton.Enabled = false;
            btnStart.Enabled = true;
            btnEnd.Enabled = false;
        }

        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string strLocation = cmbLocation.Text;
                if (string.IsNullOrEmpty(strLocation))
                {
                    ShowMsg("不得输入空白储位", 0);
                    return;
                }
                //foreach (System.Data.DataRowView dr in cmbLocation.Items)
                //{
                //    //string id = dr["id"].ToString();
                //    if (strLocation == dr["name"].ToString())
                //    {
                //        MessageBox.Show(dr["id"].ToString());
                //    }
                //}

                for (int i = 0; i < cmbLocation.Items.Count; i++)
                {

                    if (strLocation == cmbLocation.GetItemText(cmbLocation.Items[i]))
                    {
                        cmbLocation.SelectedIndex = i;
                        //MessageBox.Show(cmbLocation.SelectedValue.ToString());
                        return;
                    }

                    if ((i == cmbLocation.Items.Count - 1) && !cmbLocation.GetItemText(cmbLocation.Items[i]).Equals(strLocation))
                    {
                        LibHelper.MediasHelper.PlaySoundAsyncByNg();
                        cmbLocation.SelectAll();
                        cmbLocation.Focus();
                        ShowMsg("输入的储位无效", 0);
                    }

                }

                //for (int i = 0; i < comboBox1.Items.Count; i++)
                //{
                //    comboBox1.SelectedIndex = i;
                //    string value = comboBox1.SelectedValue.ToString();
                //}

            }
        }
        public DialogResult ShowMsg(string strTxt, int strType)
        {
            TextMsg.Text = strTxt.TP();
            switch (strType)
            {
                case 0: //Error    
                    TextMsg.ForeColor = Color.Red;
                    TextMsg.BackColor = Color.Blue;
                    return DialogResult.None;
                case 1: //Warning                        
                    TextMsg.ForeColor = Color.Blue;
                    TextMsg.BackColor = Color.FromArgb(255, 255, 128);
                    return DialogResult.None;
                case 2: //Confirm
                    return MessageBox.Show(strTxt, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    TextMsg.ForeColor = Color.White;
                    TextMsg.BackColor = Color.Blue;
                    return DialogResult.None;
            }
        }
    }
}
