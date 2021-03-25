using ClientUtilsDll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wmsReport.Core;

namespace wmsReport
{
    public partial class FastSearchForm : Form
    {
        private Controller controller;
        private int countNum = 0;
        public FastSearchForm()
        {
            controller = new Controller();
            InitializeComponent();
            ClientUtils.runBackground((Form)this);
        }

        private void FastSearchForm_Load(object sender, EventArgs e)
        {

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

        private void cellInfo_CB_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cellInfo_CB.Checked)
            {
                this.cartonSearch_CB.Checked = false;
                this.cellInfo_CB.Checked = true;
                this.org_carton_TB.Visible = false;
                this.orgCarton_LB.Visible = false;
                this.org_carton_TB.Enabled = false;
                this.confirm_BTN.Visible = false;
            }
        }

        private void cartonSearch_CB_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cartonSearch_CB.Checked)
            {
                this.cellInfo_CB.Checked = false;
                this.cartonSearch_CB.Checked = true;
                this.org_carton_TB.Visible = true;
                this.orgCarton_LB.Visible = true;
                this.org_carton_TB.Enabled = true;
                this.confirm_BTN.Visible = true;
            }
        }

        private void inputCarton_TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cartonNo = this.inputCarton_TB.Text.ToUpper().Trim();
                if (string.IsNullOrEmpty(cartonNo))
                {
                    Show_Message("请输入箱号！", 0);
                    return;
                }
                ExecuteResult exeRes = new ExecuteResult();
                DataTable dt = new DataTable();
                if (cellInfo_CB.Checked)
                {
                    exeRes = controller.fastSearchByCartonNo(cartonNo);
                    if (exeRes.Status)
                    {
                        dt = (DataTable)exeRes.Anything;
                        this.cellInfo_TB.Text = dt.Rows[0]["RET_TIER"].ToString();
                        this.inputCarton_TB.Focus();
                        this.inputCarton_TB.SelectAll();
                    }
                    else
                    {
                        this.inputCarton_TB.Focus();
                        this.inputCarton_TB.SelectAll();
                        Show_Message(exeRes.Message, 0);
                        return;
                    }
                }
                else if (cartonSearch_CB.Checked)
                {
                    if (this.org_carton_TB.Enabled)
                    {
                        Show_Message("箱号未经确认，无法查找，请点击确认箱号！", 0);
                        return;
                    }
                    string orgCartonNo = this.org_carton_TB.Text.ToUpper().Trim();
                    string inputCartonNo = this.inputCarton_TB.Text.ToUpper().Trim();
                    if (this.cartons_LISTBOX.Items.Count > 0)
                    {
                        for (int i = 0; i < this.cartons_LISTBOX.Items.Count; i++)
                        {
                            if (inputCartonNo.Equals(this.cartons_LISTBOX.Items[i]))
                            {
                                Show_Message("箱号已经重复刷入，请刷下一箱！", 0);
                                return;
                            }
                        }
                    }
                    this.cartons_LISTBOX.Items.Add(inputCartonNo);
                    countNum++;
                    this.countNum_LB.Text = countNum.ToString();
                    this.inputCarton_TB.Focus();
                    this.inputCarton_TB.SelectAll();
                    if (orgCartonNo.Equals(inputCartonNo))
                    {
                        LibHelper.MediasHelper.PlaySoundAsyncByOk();
                        Show_Message("箱号已经找到了！", 1);
                        return;
                    }
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    Show_Message("未找到，请刷下一箱！", 0);
                }
            }
        }

        private void confirm_BTN_Click(object sender, EventArgs e)
        {
            string orgCartonNo = this.org_carton_TB.Text.ToUpper().Trim();
            if (string.IsNullOrEmpty(orgCartonNo))
            {
                Show_Message("请刷入需要查找的箱号！", 0);
                return;
            }
            this.org_carton_TB.Enabled = false;
            Show_Message("需要查找箱号确认完毕，请刷入箱号！", 1);
        }

        private void reset_BTN_Click(object sender, EventArgs e)
        {
            this.cellInfo_CB.Checked = true;
            this.inputCarton_TB.Enabled = true;
            this.org_carton_TB.Clear();
            this.inputCarton_TB.Focus();
            this.inputCarton_TB.SelectAll();
            this.cellInfo_TB.Clear();
            this.cartons_LISTBOX.Items.Clear();
            this.Message_LB.Text = "";
            this.countNum = 0;
            this.countNum_LB.Text = "0";
        }
    }
}
