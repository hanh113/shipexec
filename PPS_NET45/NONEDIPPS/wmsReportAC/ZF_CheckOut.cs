using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wmsReportAC.Core;
using wmsReportAC.Utils;

namespace wmsReportAC
{
    public partial class ZF_CheckOut : Form
    {
        private Controller controller;

        public ZF_CheckOut()
        {
            InitializeComponent();
            controller = new Controller();
        }

        private void tt_orderInfo_DGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void Show_Message(string msg, int type)
        {
            Message_LB.Text = msg;
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
        private void commit_BTN_Click(object sender, EventArgs e)
        {
            int c_totalQty = 0;
            string ttOrder = this.ttOrder_TB.Text.ToUpper().Trim();
            string ictPn = this.ictPn_TB.Text.ToUpper().Trim();
            string totalQty = this.totalQty_TB.Text.ToUpper().Trim();
            string empNo = this.createEmp_TB.Text.ToUpper().Trim();
            string remark = this.remark_TB.Text.ToUpper().Trim();
            if (string.IsNullOrEmpty(ttOrder))
            {
                Show_Message("TT_订单号没有输入，不可以提交！",0);
                return;
            }
            if (string.IsNullOrEmpty(ictPn))
            {
                Show_Message("料号没有输入，不可以提交！", 0);
                return;
            }
            if (string.IsNullOrEmpty(totalQty))
            {
                Show_Message("数量没有输入，不可以提交！", 0);
                return;
            }
            try
            {
                c_totalQty = Convert.ToInt32(totalQty);
            }
            catch (Exception)
            {
                Show_Message("数量输入的是非数字，不可以提交!",0);
                return;
            }
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            exeRes = controller.insertTTOrderInfo(ttOrder,ictPn,c_totalQty,remark,empNo);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                dt.Columns.RemoveAt(0);
                this.tt_orderInfo_DGV.DataSource = dt;
                this.ttOrder_TB.Clear();
                this.ttOrder_TB.Focus();
                this.ictPn_TB.Clear();
                this.totalQty_TB.Clear();
                this.createEmp_TB.Clear();
                this.remark_TB.Clear();
                Show_Message(exeRes.Message,1);
            }
            else
            {
                Show_Message(exeRes.Message,0);
                return;
            }
        }

        private void s_query_BTN_Click(object sender, EventArgs e)
        {
            string s_tt_order = this.s_tt_order_TB.Text.ToUpper().Trim();
            string s_ictPn = this.s_ictPn_TB.Text.ToUpper().Trim();
            string start_time = this.start_time_DTP.Value.ToString("yyyyMMdd");
            string end_time = this.end_time_DTP.Value.ToString("yyyyMMdd");
            ExecuteResult exeRes = new ExecuteResult();
            exeRes = controller.getTTOrderInfoByConditions(s_tt_order,s_ictPn, start_time,end_time);
            if (exeRes.Status)
            {
                this.s_tt_order_DGV.DataSource = (DataTable)exeRes.Anything;
                sec_msg_LB.Text = exeRes.Message;
                sec_msg_LB.ForeColor = Color.Blue;
                sec_msg_LB.BackColor = Color.White;
            }
            else
            {
                sec_msg_LB.Text = exeRes.Message;
                sec_msg_LB.ForeColor = Color.Red;
                sec_msg_LB.BackColor = Color.Yellow;
            }
        }

        private void s_tt_order_DGV_DoubleClick(object sender, EventArgs e)
        {
            if (this.s_tt_order_DGV.DataSource != null)
            {
                string tt_orderNo = this.s_tt_order_DGV.Rows[s_tt_order_DGV.CurrentRow.Index].Cells[1].Value.ToString();
                string outQty = this.s_tt_order_DGV.Rows[s_tt_order_DGV.CurrentRow.Index].Cells[4].Value.ToString();
                string totalQty = this.s_tt_order_DGV.Rows[s_tt_order_DGV.CurrentRow.Index].Cells[3].Value.ToString();
                this.tt_on_process_ictpn_TB.Text = this.s_tt_order_DGV.Rows[s_tt_order_DGV.CurrentRow.Index].Cells[2].Value.ToString();
                this.tt_id.Text = this.s_tt_order_DGV.Rows[s_tt_order_DGV.CurrentRow.Index].Cells[0].Value.ToString();
                this.tt_on_process_ttOrder_TB.Text = tt_orderNo;
                this.account_LB.Text = outQty + "/" + totalQty;
                this.cartons_listBox.Items.Clear();
            }
        }


        private void tt_on_process_locationNo_TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.tt_t_location_DGV.DataSource = null;
                string ttOnProcessLocationNo = this.tt_on_process_locationNo_TB.Text.Trim().ToUpper();
                if (string.IsNullOrEmpty(ttOnProcessLocationNo))
                {
                    sec_msg_LB.Text = "请输入储位号！";
                    sec_msg_LB.ForeColor = Color.Red;
                    sec_msg_LB.BackColor = Color.Yellow;
                    return;
                }
                string ttOnProcessIctPn = this.tt_on_process_ictpn_TB.Text.Trim().ToUpper();
                ExecuteResult exeRes = new ExecuteResult();
                DataTable dt = new DataTable();
                exeRes = controller.checkIsExistIctPnOnLocationByLocationNoAndIctPn(ttOnProcessIctPn,ttOnProcessLocationNo);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    this.tt_t_location_DGV.DataSource = TransferDataTable.RowsToCol(dt);
                    this.tt_on_process_locationNo_TB.Enabled = false;
                    this.tt_on_process_cartonNo_TB.Focus();
                    this.tt_on_process_cartonNo_TB.SelectAll();
                    sec_msg_LB.Text = exeRes.Message;
                    sec_msg_LB.ForeColor = Color.Blue;
                    sec_msg_LB.BackColor = Color.White;
                }
                else
                {
                    sec_msg_LB.Text = exeRes.Message;
                    sec_msg_LB.ForeColor = Color.Red;
                    sec_msg_LB.BackColor = Color.Yellow;
                    return;
                }
            }
        }

        private void tt_on_process_cartonNo_TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cartonNo = this.tt_on_process_cartonNo_TB.Text.Trim().ToUpper();
                if (string.IsNullOrEmpty(cartonNo))
                {
                    sec_msg_LB.Text ="请输入箱号！";
                    this.tt_on_process_cartonNo_TB.Focus();
                    this.tt_on_process_cartonNo_TB.SelectAll();
                    sec_msg_LB.ForeColor = Color.Red;
                    sec_msg_LB.BackColor = Color.Yellow;
                    return;
                }
                string locationNo = this.tt_on_process_locationNo_TB.Text.Trim().ToUpper();
                string ictPn = this.tt_on_process_ictpn_TB.Text.Trim().ToUpper();
                ExecuteResult exeRes = new ExecuteResult();
                exeRes = controller.checkIsExistCartonNoIsLinkIctPn(cartonNo,ictPn);
                if (!exeRes.Status)
                {
                    this.tt_on_process_cartonNo_TB.Focus();
                    this.tt_on_process_cartonNo_TB.SelectAll();
                    sec_msg_LB.Text = exeRes.Message;
                    sec_msg_LB.ForeColor = Color.Red;
                    sec_msg_LB.BackColor = Color.Yellow;
                    return;
                }
                exeRes = controller.checkIsExistCartonNoIsLinkLocationNO(cartonNo,locationNo);
                if (!exeRes.Status)
                {
                    this.tt_on_process_cartonNo_TB.Focus();
                    this.tt_on_process_cartonNo_TB.SelectAll();
                    sec_msg_LB.Text = exeRes.Message;
                    sec_msg_LB.ForeColor = Color.Red;
                    sec_msg_LB.BackColor = Color.Yellow;
                    return;
                }
                string tt_order = this.s_tt_order_TB.Text.Trim().ToUpper();
                string tt_id =  this.tt_id.Text.Trim().ToUpper();
                DataTable dt = new DataTable();
                exeRes = controller.ZFPassStation(tt_id,tt_order,ictPn,locationNo,cartonNo);
                if (exeRes.Status)
                {
                    cartons_listBox.Items.Add(cartonNo);
                    dt = (DataTable)exeRes.Anything;
                    this.account_LB.Text = dt.Rows[0]["C_MSG"].ToString();
                    this.tt_on_process_cartonNo_TB.Focus();
                    this.tt_on_process_cartonNo_TB.SelectAll();
                    sec_msg_LB.Text = "此箱杂发出货成功，请刷下一箱！";
                    sec_msg_LB.ForeColor = Color.Blue;
                    sec_msg_LB.BackColor = Color.White;
                }
                else
                {
                    this.tt_on_process_cartonNo_TB.Focus();
                    this.tt_on_process_cartonNo_TB.SelectAll();
                    sec_msg_LB.Text = exeRes.Message;
                    sec_msg_LB.ForeColor = Color.Red;
                    sec_msg_LB.BackColor = Color.Yellow;
                    return;
                }
            }
        }
    }
}
