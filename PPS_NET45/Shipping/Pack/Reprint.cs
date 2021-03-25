using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClientUtilsDll;
using Packingparcel.Core;
using Packingparcel.Entitys;

namespace Packingparcel
{
    public partial class Reprint : Form
    {
        private Controller controller;
        private ShipmentInfo shipmentInfo;
        private string strLocalMACADDRESS = string.Empty;
        private string strlocalHostname = "";
        public Reprint()
        {
            InitializeComponent();
            controller = new Controller();
            shipmentInfo = new ShipmentInfo();
            ClientUtils.runBackground((Form)this);
        }

        private void Reprint_Load(object sender, EventArgs e)
        {
            strLocalMACADDRESS = LibHelper.LocalHelper.getMacAddr_Local();
            strlocalHostname = System.Environment.MachineName;
        }
        private void reprint_BTN_Click(object sender, EventArgs e)
        {
            string reprintContent = this.reprintContent_CB.Text; 
            if (string.IsNullOrEmpty(reprintContent))
            {
                Show_Message("请输入需要打印标签类型！", 0);
                return;
            }
            string cartonNo = this.inputData_TB.Text.Trim().ToUpper();
            string isMix = this.isMix_LB.Text.Trim();
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            /*
             GS1_LABEL
PACKINGLIST
             
             */
            if (cartonNo.Length == 20 && cartonNo.Substring(0, 2).Equals("00"))
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

            RePrintUserInfo.RePrintLabelName = this.reprintContent_CB.Text.Trim();
            RePrintUserInfo.LoginEmpNo = ClientUtils.fLoginUser;
            RePrintUserInfo.LoginUserName = ClientUtils.fUserName;

            switch (this.reprintContent_CB.Text.Trim())
            {
                case "LABEL":
                    if (!isMix.Equals("MIX"))
                    {
                        if (!controller.isPrintForNoMix(cartonNo))
                        {
                            Show_Message("NOMIX栈板未完成不可以补印,请检查!", 0);
                            this.inputData_TB.Enabled = true;
                            this.inputData_TB.Focus();
                            this.inputData_TB.SelectAll();
                            this.reprint_BTN.Enabled = false;
                            return;
                        }
                    }
                    exeRes = controller.printAllLabelLogic(isMix.Equals("MIX") ? true : false, cartonNo, shipmentInfo);
                    if (!exeRes.Status)
                    {
                        Show_Message(exeRes.Message, 0);
                        this.inputData_TB.Enabled = true;
                        this.inputData_TB.Focus();
                        this.inputData_TB.SelectAll();
                        return;
                    }
                    break;
                case "GS1_LABEL":
                    if (!controller.isPrintForNoMix(cartonNo))
                    {
                        Show_Message("NOMIX栈板未完成不可以补印,请检查!", 0);
                        this.inputData_TB.Enabled = true;
                        this.inputData_TB.Focus();
                        this.inputData_TB.SelectAll();
                        this.reprint_BTN.Enabled = false;
                        return;
                    }
                    exeRes = controller.printGS1LabelLogic(shipmentInfo, cartonNo);
                    if (!exeRes.Status)
                    {
                        Show_Message(exeRes.Message, 0);
                        this.inputData_TB.Enabled = true;
                        this.inputData_TB.Focus();
                        this.inputData_TB.SelectAll();
                        return;
                    }
                    break;
                case "PACKINGLIST":
                    exeRes = controller.getReprintInfoByCartonNo(cartonNo);
                    if (exeRes.Status)
                    {
                        dt = (DataTable)exeRes.Anything;
                        string deliveryNo = dt.Rows[0]["delivery_no"].ToString();
                        string lineItem = dt.Rows[0]["line_item"].ToString();
                        string packPalletNo = dt.Rows[0]["pack_pallet_no"].ToString();
                        exeRes = controller.printCrystalReportAllLogic(cartonNo, shipmentInfo, deliveryNo, lineItem, true, packPalletNo);
                        if (!exeRes.Status)
                        {
                            Show_Message(exeRes.Message, 0);
                            this.inputData_TB.Enabled = true;
                            this.inputData_TB.Focus();
                            this.inputData_TB.SelectAll();
                            this.reprint_BTN.Enabled = false;
                            return;
                        }
                    }
                    else
                    {
                        Show_Message(exeRes.Message, 0);
                        this.inputData_TB.Enabled = true;
                        this.inputData_TB.Focus();
                        this.inputData_TB.SelectAll();
                        this.reprint_BTN.Enabled = false;
                        return;
                    }
                    break;
                case "CarrierLable":
                    exeRes = controller.printAllCarrierLableLogic(shipmentInfo, cartonNo);
                    if (!exeRes.Status)
                    {
                        Show_Message(exeRes.Message, 0);
                        this.inputData_TB.Enabled = true;
                        this.inputData_TB.Focus();
                        this.inputData_TB.SelectAll();
                        this.reprint_BTN.Enabled = false;
                        return;
                    }
                    break;
                case "DELIVERYNOTE":
                    exeRes = controller.getReprintInfoByCartonNo(cartonNo);
                    if (exeRes.Status)
                    {
                        dt = (DataTable)exeRes.Anything;
                        string deliveryNo = dt.Rows[0]["delivery_no"].ToString();
                        string lineItem = dt.Rows[0]["line_item"].ToString();
                        exeRes = controller.printDeliveryNoteLabel(cartonNo, deliveryNo, lineItem, shipmentInfo, true);
                        if (!exeRes.Status)
                        {
                            Show_Message(exeRes.Message, 0);
                            this.inputData_TB.Enabled = true;
                            this.inputData_TB.Focus();
                            this.inputData_TB.SelectAll();
                            this.reprint_BTN.Enabled = false;
                            return;
                        }
                    }
                    else
                    {
                        Show_Message(exeRes.Message, 0);
                        this.inputData_TB.Enabled = true;
                        this.inputData_TB.Focus();
                        this.inputData_TB.SelectAll();
                        this.reprint_BTN.Enabled = false;
                        return;
                    }

                    break;
                default:
                    break;
            }

            exeRes = controller.PPInsertRePrintLogBySP("PACK", RePrintUserInfo.RePrintLabelName, cartonNo, RePrintUserInfo.LoginEmpNo, RePrintUserInfo.LoginUserName, RePrintUserInfo.RePrintEmpNo, RePrintUserInfo.RePrintUserName, RePrintUserInfo.RePrintLoginTime, strlocalHostname, strLocalMACADDRESS, "");
            if (!exeRes.Status)
            {
                Show_Message(exeRes.Message, 0);
                this.inputData_TB.Enabled = true;
                this.inputData_TB.Focus();
                this.inputData_TB.SelectAll();
                this.reprint_BTN.Enabled = false;
                return;
            }

            this.inputData_TB.Enabled = true;
            this.inputData_TB.Focus();
            this.inputData_TB.SelectAll();
            this.reprint_BTN.Enabled = false;
            Show_Message("补印成功！", 1);
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
        private void query_BTN_Click(object sender, EventArgs e)
        {
            string cartonNo = this.inputData_TB.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(cartonNo))
            {
                Show_Message("请输入箱号！", 0);
                return;
            }
            if (cartonNo.Length == 20 && cartonNo.Substring(0, 2).Equals("00"))
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
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            shipmentInfo = null;
            //if (controller.isReprint(cartonNo)) //copy from js by wenxing 20200930
            exeRes = controller.CheckReprintCartonBySP(RePrintUserInfo.RePrintEmpNo, "PACK", cartonNo);
            if (exeRes.Status)
            {
                exeRes = controller.getReprintLabelFormInfo(cartonNo);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    this.shipmentId_TB.Text = dt.Rows[0]["shipment_id"].ToString();
                    this.pickPalletNo_TB.Text = dt.Rows[0]["pick_pallet_no"].ToString();
                    this.shipMentType_LB.Text = dt.Rows[0]["shipment_type"].ToString();
                    this.type_Lb.Text = dt.Rows[0]["type"].ToString();
                    this.isMix_LB.Text = dt.Rows[0]["PALLETTYPE"].ToString();
                    this.ssccCode_LB.Text = dt.Rows[0]["sscc"].ToString();
                    this.deliveryNo_LB.Text = dt.Rows[0]["delivery_no"].ToString();
                    shipmentInfo = new ShipmentInfo
                    {
                        DeliveryNo = dt.Rows[0]["delivery_no"].ToString(),
                        CarrierName = dt.Rows[0]["carrier_name"].ToString(),
                        Region = dt.Rows[0]["region"].ToString(),
                        ShipmentType = dt.Rows[0]["shipment_type"].ToString(),
                        ShipmentId = dt.Rows[0]["shipment_id"].ToString(),
                        TYPE = dt.Rows[0]["type"].ToString(),
                        CarrierCode = dt.Rows[0]["carrier_code"].ToString(),
                        ServiceLevel = dt.Rows[0]["service_level"].ToString()
                    };
                    Show_Message("查询成功，请选择打印标签类型！", 1);
                    this.reprint_BTN.Enabled = true;
                    this.inputData_TB.Enabled = false;
                }
                else
                {
                    this.inputData_TB.Focus();
                    this.inputData_TB.SelectAll();
                    Show_Message(exeRes.Message, 0);
                    return;
                }
            }
            else
            {
                //Show_Message("此箱：" + cartonNo + " 不可补印，只有当前站别之后机器，方可补印！", 0);
                Show_Message(exeRes.Message, 0);
                return;
            }
        }

    }
}
