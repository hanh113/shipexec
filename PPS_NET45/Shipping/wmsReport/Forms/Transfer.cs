using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.IO;
using wmsReport.Core;
using ClientUtilsDll;
/*
add by dunyang_wang 20180917
*/
namespace wmsReport
{
    public partial class Transfer : Form
    {
        private PrintLabel printLabel = new PrintLabel();
        private Controller controller;
        public int H = 0;
        public int W = 0;
        public Transfer()
        {
            InitializeComponent();
            //固定位置
            controller = new Controller();
            this.Location = new Point(System.Windows.Forms.SystemInformation.WorkingArea.Width - this.Size.Width, 0);
            ClientUtils.runBackground((Form)this);
        }

        private void Transfer_Load(object sender, EventArgs e)
        {
            this.chkQHold.Checked = true;//默认检查QAhold
            this.chkPrint.Checked = true;//默认打标签
            initGroupBox();
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }
        private void initGroupBox()
        {
            W = Screen.PrimaryScreen.Bounds.Width;
            W = Convert.ToInt32(W * 0.5);

            H = Screen.PrimaryScreen.Bounds.Height;
            H = Convert.ToInt32(H * 0.4);
            this.groupBox1.Width = W;
            this.panel4.Width = W;
            this.panel1.Height = H;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.cmbLocationFrom.Enabled = false;
            this.cmbLocationTo.Enabled = false;
            this.txtCarton.Enabled = true;
            this.txtCarton.Focus();
            this.txtCarton.SelectAll();
        }

        private void resetAllStatus()
        {
            this.cmbLocationFrom.Enabled = true;
            this.cmbLocationFrom.Text = string.Empty;
            this.cmbLocationFrom.DataSource = null;
            this.dgvLocationFrom.DataSource = null;
            this.cmbLocationTo.Enabled = true;
            this.cmbLocationTo.Text = string.Empty;
            this.cmbLocationTo.DataSource = null;
            this.dgvLocationTo.DataSource = null;
            this.txtCarton.Enabled = false;
            this.txtCarton.Clear();
            this.dgvCarton.DataSource = null;
            this.Message_LB.Text = "Message";
        }

        private void Show_Message(string msg, int type)
        {
            Message_LB.Text = msg.TP();
            switch (type)
            {
                case 0: //error
                    Message_LB.ForeColor = Color.Red;
                    Message_LB.BackColor = Color.Yellow;
                    break;
                case 1:
                    Message_LB.ForeColor = Color.Blue;
                    Message_LB.BackColor = Color.White;
                    break;
                default:
                    Message_LB.ForeColor = Color.Black;
                    Message_LB.BackColor = Color.White;
                    break;
            }
        }
        private void cmbLocationTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orgLocationNo = this.cmbLocationTo.Text.ToUpper().Trim();
            if (string.IsNullOrEmpty(orgLocationNo))
            {
                Show_Message("储位的信息，不可为空！", 0);
                return;
            }
            string locationId = "";
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            exeRes = controller.locationNoTransformLocationIdByLocationNo(orgLocationNo);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                locationId = dt.Rows[0]["location_id"].ToString();
                exeRes = controller.getLocationInformationBylocationId(locationId);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    this.dgvLocationTo.DataSource = dt;
                    Show_Message(exeRes.Message, 1);
                }
                else
                {
                    Show_Message(exeRes.Message, 0);
                    return;
                }
            }
            else
            {
                Show_Message(exeRes.Message, 0);
                return;
            }
        }
        private void resetAll()
        {
            this.cmbLocationFrom.Enabled = true;
            this.cmbLocationFrom.Text = "";
            this.cmbLocationTo.Enabled = true;
            this.cmbLocationTo.Text = "";
            this.dgvLocationTo.DataSource = null;
            this.dgvLocationFrom.DataSource = null;
            this.txtCarton.Clear();
            this.txtCarton.Enabled = false;
            this.dgvCarton.DataSource = null;
        }
        private void txtCarton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string orgLocationNo = this.cmbLocationFrom.Text.Trim().ToUpper();
                string tarLocationNo = this.cmbLocationTo.Text.Trim().ToUpper();
                string inputData = this.txtCarton.Text.Trim().ToUpper();
                if (inputData.StartsWith("3S"))
                {
                    inputData = inputData.Substring(2);
                }
                if (inputData.StartsWith("S"))
                {
                    inputData = inputData.Substring(1);
                }
                if (inputData.Length == 20 && inputData.Substring(0, 2).Equals("00"))
                {
                    inputData = inputData.Substring(2);
                }
                if (string.IsNullOrEmpty(inputData) || string.IsNullOrEmpty(orgLocationNo) || string.IsNullOrEmpty(tarLocationNo))
                {
                    Show_Message("输入信息或原储位和目标储位没有输入信息，请检查！", 0);
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    return;
                }

                this.dgvCarton.DataSource = null;
                ExecuteResult exeRes = new ExecuteResult();
                DataTable dt = new DataTable();
                #region  判断是否能够转移储位     
                exeRes = controller.checkIsTransfer(inputData, orgLocationNo, tarLocationNo);
                if (!exeRes.Status)
                {
                    this.txtCarton.Focus();
                    this.txtCarton.SelectAll();
                    Show_Message(exeRes.Message, 0);
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    return;
                }
                #endregion
                #region //增加判断是否有QAhold机器，并且是否可以转移
                if (chkQHold.Checked)
                {
                    string errorMes = "";
                    if (!Reverse.ReverseBll.CheckHold(inputData, "SERIALNUMBER", out errorMes))
                    {
                        Show_Message("刷入:" + inputData + "中有被QAHOLD机器，不可转移，请检查！", 0);
                        this.txtCarton.Focus();
                        this.txtCarton.SelectAll();
                        LibHelper.MediasHelper.PlaySoundAsyncByNg();
                        return;
                    }
                }
                #endregion

                if (orgLocationNo.Equals("SOLIDSTORE") || tarLocationNo.Equals("SOLIDSTORE") || tarLocationNo.Equals("TEMPSTORE"))
                {

                    ////加入立库检查逻辑(原储位&立库开关开启时走web api方式)
                    //exeRes = controller.changeLocation(inputData, tarLocationNo);
                    Show_Message("输入原储位和目标储位为立库专属,请切换程功能序!", 0);
                    this.txtCarton.Focus();
                    this.txtCarton.SelectAll();
                    return;
                }

                exeRes = controller.changeLocation(inputData, orgLocationNo, tarLocationNo);

                if (!exeRes.Status)
                {
                    Show_Message(exeRes.Message, 0);
                    this.txtCarton.Focus();
                    this.txtCarton.SelectAll();
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    return;
                }

                //将查到的信息，show到dgv中去
                exeRes = controller.getInformationByInputData(inputData);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    this.dgvCarton.DataSource = dt;
                    this.txtCarton.Focus();
                    this.txtCarton.SelectAll();
                    Show_Message("储位已转移成功，请继续输入！", 1);
                    LibHelper.MediasHelper.PlaySoundAsyncByOk();
                    showAfterChangeLocationInformation(orgLocationNo, tarLocationNo);//将转移后location信息show出
                    return;
                }
                else
                {
                    this.txtCarton.Focus();
                    this.txtCarton.SelectAll();
                    Show_Message(exeRes.Message, 0);
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    return;
                }
            }
        }

        private bool CheckSolidStoreFlag()
        {
            string sql = @"select count(*) as flag from t_basicparameter_info where para_type='TOPSTAR' and enabled='Y'";
            DataTable dt = ClientUtils.ExecuteSQL(sql).Tables[0];
            //string a= dt.Rows[0]["flag"].ToString();
            return dt.Rows[0]["flag"].ToString().Equals("1");
        }

        private void showAfterChangeLocationInformation(string orgLocationNo, string targetLocationNo)
        {
            string locationId = "";
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            exeRes = controller.locationNoTransformLocationIdByLocationNo(targetLocationNo);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                locationId = dt.Rows[0]["location_id"].ToString();
                exeRes = controller.getLocationInformationBylocationId(locationId);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    this.dgvLocationTo.DataSource = dt;
                }
            }
            exeRes = controller.locationNoTransformLocationIdByLocationNo(orgLocationNo);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                locationId = dt.Rows[0]["location_id"].ToString();
                exeRes = controller.getLocationInformationBylocationId(locationId);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    this.dgvLocationFrom.DataSource = dt;
                }
            }
        }


        private void resetInput()//将输入信息清除
        {
            this.txtCarton.Clear();
            this.txtCarton.Focus();
            this.txtCarton.SelectAll();
            this.dgvCarton.DataSource = null;
            Show_Message("重置成功！", 1);
        }

        private string getAllCount(string inputData)
        {
            string countAll = "";
            string sql = @"select count(*)  as qty  from  ppsuser.t_sn_status tss where   tss.pallet_no = :inputData";
            object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inputData", inputData } };
            DataSet ds = ClientUtils.ExecuteSQL(sql, sqlparams);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                countAll = ds.Tables[0].Rows[0]["qty"].ToString();
            }
            else
            {
                countAll = null;
            }
            return countAll;
        }
        /// <summary>
        /// 将A中数据添加到B中
        /// </summary>
        /// <param name="getDataDt">A dataTable</param>
        /// <param name="dgvDt">B dataTable</param>
        /// <returns></returns>
        private DataTable addRowData(DataTable getDataDt, DataTable dgvDt)
        {
            DataTable dt = new DataTable();
            if (getDataDt != null && getDataDt.Rows.Count > 0)//将取到值添加到dgv中
            {
                DataRow drcalc;
                foreach (DataRow dr in getDataDt.Rows)
                {
                    drcalc = dgvDt.NewRow();
                    drcalc.ItemArray = dr.ItemArray;
                    dgvDt.Rows.Add(drcalc);
                }
            }
            dt = dgvDt;
            return dt;
        }


        private DataTable getAllCartonNoBy_SN_cartonNo_PalletNo(string inputData)
        {
            string sql = @"select distinct tss.carton_no
            from ppsuser.t_sn_status tss
            where (tss.serial_number = :inputData or tss.carton_no =:inputData or tss.pallet_no =:inputData)";
            object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inputData", inputData } };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        private DataTable queryDataByCartonNo(string cartonNo)
        {
            string sql = @"select tss.serial_number as  流水号,
                            tss.customer_sn  as  客户料号,
                            tss.pallet_no  as  栈板号,
                            tss.carton_no  as 箱号,
                            tss.location_name as 储位名,
                            tss.pallet_no as 料号,
                            '' as 目标储位
                             from  ppsuser.t_sn_status tss 
                            where   tss.carton_no = :inputData";
            object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inputData", cartonNo } };
            DataSet ds = ClientUtils.ExecuteSQL(sql, sqlparams);
            return ds.Tables[0];
        }

        private void cmbLocationFrom_DropDown(object sender, EventArgs e)
        {
            //获取所有储位信息
            this.cmbLocationFrom.DataSource = null;
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            exeRes = controller.getAllLocationInfo(false);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                if (dt.Rows.Count > 0 && dt != null)
                {
                    this.cmbLocationFrom.DataSource = dt;
                    this.cmbLocationFrom.DisplayMember = "LOCATION_No";
                    this.cmbLocationFrom.ValueMember = "LOCATION_ID";
                }
            }
            else
            {
                Show_Message(exeRes.Message, 0);
                return;
            }
        }

        private void cmbLocationTo_DropDown(object sender, EventArgs e)
        {
            //获取所有储位信息
            this.cmbLocationTo.DataSource = null;//cmbLocationTo
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            exeRes = controller.getAllLocationInfo(true);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                if (dt.Rows.Count > 0 && dt != null)
                {
                    this.cmbLocationTo.DataSource = dt;
                    this.cmbLocationTo.DisplayMember = "LOCATION_No";
                    this.cmbLocationTo.ValueMember = "LOCATION_ID";
                }
            }
            else
            {
                Show_Message(exeRes.Message, 0);
                return;
            }
        }





        private void cmbLocationFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string originalLocationNo = this.cmbLocationFrom.Text.ToUpper().Trim();
                if (string.IsNullOrEmpty(originalLocationNo))
                {
                    Show_Message("储位的信息，不可为空！", 0);
                    return;
                }
                string locationId = "";
                ExecuteResult exeRes = new ExecuteResult();
                DataTable dt = new DataTable();
                exeRes = controller.locationNoTransformLocationIdByLocationNo(originalLocationNo);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    locationId = dt.Rows[0]["location_id"].ToString();
                    exeRes = controller.getLocationInformationBylocationId(locationId);
                    if (exeRes.Status)
                    {
                        dt = (DataTable)exeRes.Anything;
                        this.dgvLocationFrom.DataSource = dt;
                        Show_Message(exeRes.Message, 1);
                    }
                    else
                    {
                        Show_Message(exeRes.Message, 0);
                        return;
                    }
                }
                else
                {
                    Show_Message(exeRes.Message, 0);
                    return;
                }
            }
        }

        private void cmbLocationTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string tarLocationNo = this.cmbLocationTo.Text.ToUpper().Trim();
                if (string.IsNullOrEmpty(tarLocationNo))
                {
                    Show_Message("储位的信息，不可为空！", 0);
                    return;
                }
                string locationId = "";
                ExecuteResult exeRes = new ExecuteResult();
                DataTable dt = new DataTable();
                exeRes = controller.locationNoTransformLocationIdByLocationNo(tarLocationNo);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    locationId = dt.Rows[0]["location_id"].ToString();
                    exeRes = controller.getLocationInformationBylocationId(locationId);
                    if (exeRes.Status)
                    {
                        dt = (DataTable)exeRes.Anything;
                        this.dgvLocationTo.DataSource = dt;
                        Show_Message(exeRes.Message, 1);
                    }
                    else
                    {
                        Show_Message(exeRes.Message, 0);
                        return;
                    }
                }
                else
                {
                    Show_Message(exeRes.Message, 0);
                    return;
                }
            }
        }


        private void cmbLocationFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orglLocationNo = this.cmbLocationFrom.Text.ToUpper().Trim();
            if (string.IsNullOrEmpty(orglLocationNo))
            {
                Show_Message("储位的信息，不可为空！", 0);
                return;
            }
            string locationId = "";
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            exeRes = controller.locationNoTransformLocationIdByLocationNo(orglLocationNo);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                locationId = dt.Rows[0]["LOCATION_ID"].ToString();
                exeRes = controller.getLocationInformationBylocationId(locationId);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    this.dgvLocationFrom.DataSource = dt;
                    Show_Message(exeRes.Message, 1);
                }
                else
                {
                    Show_Message(exeRes.Message, 0);
                    return;
                }
            }
            else
            {
                Show_Message(exeRes.Message, 0);
                return;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            resetAll();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            string orgLocation = this.cmbLocationFrom.Text.ToUpper().Trim();
            string tarLocation = this.cmbLocationTo.Text.ToUpper().Trim();
            if (string.IsNullOrEmpty(orgLocation) || string.IsNullOrEmpty(tarLocation))
            {
                Show_Message("原储位或目标储位没有输入信息！", 0);
                return;
            }
            ExecuteResult exeRes = new ExecuteResult();
            List<string> labelContentList = new List<string>();
            exeRes = controller.getTransferLableContentStr(orgLocation);
            if (exeRes.Status)
            {
                labelContentList.Add((string)exeRes.Anything);
            }
            exeRes = controller.getTransferLableContentStr(tarLocation);
            if (exeRes.Status)
            {
                labelContentList.Add((string)exeRes.Anything);
            }
            exeRes = printLabel.printLableForModifyVersion("Transfer_PalletPartLabel", labelContentList, 1);
            if (!exeRes.Status)
            {
                Show_Message(exeRes.Message, 0);
                return;
            }
            else
            {
                Show_Message("储位转移结束，Label已经打印！", 0);
                return;
            }
        }

        private void chkQHold_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkPrint_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
