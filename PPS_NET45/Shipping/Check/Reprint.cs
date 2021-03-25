using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Check.Core;
using Check.Utils;
using Check.Entitys;
using LibHelper;

namespace Check
{
    public partial class Reprint : Form
    {
        private Controller controller;
        private PrintLabel printLable;
        public Reprint()
        {
            InitializeComponent();
            controller = new Controller();
            printLable = new PrintLabel();
            ClientUtils.runBackground((Form)this);
        }

        private void reprint_BTN_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.shipmentId_TB.Text.Trim()) || string.IsNullOrEmpty(this.pickPalletNo_TB.Text.Trim()))
            {
                Message_LB.Text = "集货单号和PICK栈板号不能为空";
                return;
            }
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            string shipmentId = this.shipmentId_TB.Text.Trim().ToUpper();
            string pickPalletNo = this.pickPalletNo_TB.Text.Trim().ToUpper();
            //exeRes = controller.getShipMentInfoByshipmentId(shipmentId);
            exeRes = controller.getPickPalletInfoByPickPalletNoAndShipmentId(pickPalletNo, shipmentId);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                string secu = dt.Rows[0]["SECURITY"].ToString();
                string strEmptyCarton = dt.Rows[0]["empty_carton"].ToString().Trim();
                List<string> listStr = new List<string>();
                listStr.Add(shipmentId + @"|" + pickPalletNo.Substring(2) + @"|" + secu + @"|" + strEmptyCarton + @"|");
                exeRes = printLable.printLableForModifyVersion("SH_PalletIDLabel", listStr, 1);
                
                if (!exeRes.Status)
                {
                    Message_LB.Text = exeRes.Message;
                    Message_LB.ForeColor = Color.Red;
                    return;
                }
                else
                {
                    ClientUtils.WritePrintLabelLog("Check Pallet Label", "", pickPalletNo, LocalHelper.getMacAddr_Local());
                }
                this.reprint_BTN.Enabled = false;
            }

        }

        private void cartonNo_TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cartonNo = this.cartonNo_TB.Text.ToUpper().Trim();
                this.reprint_BTN.Enabled = true;
                if (string.IsNullOrEmpty(cartonNo))
                {
                    Message_LB.Text = "没有输入箱号！";
                    Message_LB.ForeColor = Color.Red;
                    return;
                }
                if (cartonNo.Length == 20 && cartonNo.Substring(0, 2).Equals("00"))//sscc_cartonNo
                {
                    cartonNo = cartonNo.Substring(2, 18);
                }
                if (cartonNo.StartsWith("3S"))
                {
                    cartonNo = cartonNo.Substring(2);
                }
                if (cartonNo.StartsWith("S"))
                {
                    cartonNo = cartonNo.Substring(1);
                }
                if (!controller.isFinishWorkByCartonNo(cartonNo))
                {
                    Message_LB.Text = "站别异常或输入信息不正确，不可以补印！";
                    Message_LB.ForeColor = Color.Red;
                    this.reprint_BTN.Enabled = false;
                    this.cartonNo_TB.Focus();
                    this.cartonNo_TB.SelectAll();
                    return;
                }
                ExecuteResult exeRes = new ExecuteResult();
                DataTable dt = new DataTable();
                exeRes = controller.getReprintInfoByCartonNo(cartonNo);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    this.shipmentId_TB.Text = dt.Rows[0]["SHIPMENT_ID"].ToString();
                    this.pickPalletNo_TB.Text = dt.Rows[0]["PICK_PALLET_NO"].ToString();
                    Message_LB.Text = "OK,请点击补印！";
                    Message_LB.ForeColor = Color.Blue;
                }
                else
                {
                    Message_LB.Text = exeRes.Message;
                    Message_LB.ForeColor = Color.Red;
                    return;
                }
            }
        }

        private void Reprint_Load(object sender, EventArgs e)
        {

        }

        private void Reprint_Paint(object sender, PaintEventArgs e)
        {
            this.cartonNo_TB.Focus();
            this.cartonNo_TB.SelectAll();
        }
    }
}
