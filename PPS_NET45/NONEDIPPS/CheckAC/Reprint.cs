using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CheckAC.Core;
using CheckAC.Utils;
using CheckAC.Entitys;

namespace CheckAC
{
    public partial class Reprint : Form
    {
        private Controller controller;
        private PrintLabel printLable;
        private DataTable dtTempCartonInfo = null;
        public Reprint()
        {
            InitializeComponent();
            controller = new Controller();
            printLable = new PrintLabel();
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
            //exeRes = controller.getPickPalletInfoByPickPalletNoAndShipmentId(pickPalletNo,shipmentId);
            //if (exeRes.Status)
            //{
            //    dt = (DataTable)exeRes.Anything;
            //    string secu = dt.Rows[0]["SECURITY"].ToString();
            //    List<string> listStr = new List<string>();
            //    listStr.Add(shipmentId + @"|" + pickPalletNo.Substring(2) + @"|" + secu + @"|");
            //    exeRes = printLable.printLableForModifyVersion("CarrierTEMPLable", listStr, 1);
            //    if (!exeRes.Status)
            //    {
            //        Message_LB.Text = exeRes.Message;
            //        Message_LB.ForeColor = Color.Red;
            //        return;
            //    }
            //    this.reprint_BTN.Enabled = false;
            //}
            if (dtTempCartonInfo != null)
            {
                List<string> listStr = new List<string>();
                List<string> listStrPallet = new List<string>();
                string strSUPPLIER_NAME1 = dtTempCartonInfo.Rows[0]["SUPPLIER_NAME1"].ToString().Trim();
                string strSUPPLIER_NAME2 = dtTempCartonInfo.Rows[0]["SUPPLIER_NAME2"].ToString().Trim();
                string strSUPPLIER_NAME3 = dtTempCartonInfo.Rows[0]["SUPPLIER_NAME3"].ToString().Trim();
                string strSUPPLIER_ADDRESS1 = dtTempCartonInfo.Rows[0]["SHIPTONAME1"].ToString().Trim();
                string strSUPPLIER_ADDRESS2 = dtTempCartonInfo.Rows[0]["SHIPTOADDRESS1"].ToString().Trim();
                string strSHIPINFO = dtTempCartonInfo.Rows[0]["SHIPTOCITY"].ToString().Trim();
                string strSHIPTOSTATE = dtTempCartonInfo.Rows[0]["SHIPTOSTATE"].ToString().Trim();
                string strSHIPTOZIP = dtTempCartonInfo.Rows[0]["SHIPTOZIP"].ToString().Trim();
                string strSHIPTOCOUNTRY = dtTempCartonInfo.Rows[0]["SHIPTOCOUNTRY"].ToString().Trim();

                string strSHIPINFO_pallet = strSHIPINFO + ", " + strSHIPTOSTATE + " " + strSHIPTOZIP + " " + strSHIPTOCOUNTRY;


                if (!string.IsNullOrEmpty(strSHIPTOSTATE))
                {
                    strSHIPINFO = strSHIPINFO + "," + strSHIPTOSTATE;
                }
                if (!string.IsNullOrEmpty(strSHIPTOZIP))
                {
                    strSHIPINFO = strSHIPINFO + "," + strSHIPTOZIP;
                }
                if (!string.IsNullOrEmpty(strSHIPTOCOUNTRY))
                {
                    strSHIPINFO = strSHIPINFO + "," + strSHIPTOCOUNTRY;
                }
                string CONSIGNEE = strSUPPLIER_ADDRESS1 + " " + strSUPPLIER_ADDRESS2 + " " + strSHIPINFO_pallet;
                listStr.Add("\"" + strSUPPLIER_NAME1 + "\"," + "\"" + strSUPPLIER_NAME2 + "\"," + "\"" + strSUPPLIER_NAME3 + "\"," + "\"" + strSUPPLIER_ADDRESS1 + "\"," + "\"" + strSUPPLIER_ADDRESS2 + "\"," + "\"" + strSHIPINFO + "\"");
                listStrPallet.Add(CONSIGNEE);
                if(cmbLabel.Text== "Pallet Address Label")
                {
                    exeRes = printLable.printLableForModifyVersion("PalletAddressAC.btw", listStrPallet, 1);
                }
                else if(cmbLabel.Text == "Carton Address Label")
                    {
                    exeRes = printLable.printLableForModifyVersion("CartonAddress.btw", listStr, 1);
                }
               
                if (!exeRes.Status)
                {
                    Message_LB.Text = exeRes.Message;
                    Message_LB.ForeColor = Color.Red;
                    return;
                }
                this.reprint_BTN.Enabled = false;
            }
        }

        private void cartonNo_TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cartonNo = this.cartonNo_TB.Text.ToUpper().Trim();
                
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
                //if (cartonNo.StartsWith("3S"))
                //{
                //    cartonNo = cartonNo.Substring(2);
                //}
                //if (cartonNo.StartsWith("S"))
                //{
                //    cartonNo = cartonNo.Substring(1);
                //}
                //if (!controller.isFinishWorkByCartonNo(cartonNo))
                //{
                //    Message_LB.Text = "站别异常或输入信息不正确，不可以补印！";
                //    Message_LB.ForeColor = Color.Red;
                //    this.reprint_BTN.Enabled = false;
                //    this.cartonNo_TB.Focus();
                //    this.cartonNo_TB.SelectAll();
                //    return;
                //}
                ExecuteResult exeRes = new ExecuteResult();
                DataTable dt = new DataTable();
                exeRes = controller.getReprintInfoByCartonNo(cartonNo);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    this.shipmentId_TB.Text = dt.Rows[0]["SHIPMENT_ID"].ToString().Trim();
                    this.pickPalletNo_TB.Text = dt.Rows[0]["PICK_PALLET_NO"].ToString().Trim();
                    string strCheck = dt.Rows[0]["CHECK_TIME"].ToString().Trim();
                    if (string.IsNullOrEmpty(this.pickPalletNo_TB.Text) || string.IsNullOrEmpty(strCheck))
                    {
                        Message_LB.Text = "该箱号还未CHECK,不可以补印！";
                        Message_LB.ForeColor = Color.Red;
                        this.reprint_BTN.Enabled = false;
                        this.cartonNo_TB.Focus();
                        this.cartonNo_TB.SelectAll();
                        return;
                    }
                    //增加CUSTMODEL为CASE的时候才需要打印这个LABEL
                    dtTempCartonInfo = controller.GetModelType(cartonNo);
                    if ((dtTempCartonInfo == null) || (dtTempCartonInfo.Rows.Count <= 0))
                    {
                        throw new Exception("未找到料号包规信息,请联系IT-PPS处理!");
                    }
                    if (dtTempCartonInfo.Rows[0]["CUSTMODEL"].ToString().Trim().ToUpper() == "CASE")
                    {
                        dtTempCartonInfo = controller.GetAddressInfo(cartonNo);
                        if ((dtTempCartonInfo == null) || (dtTempCartonInfo.Rows.Count <= 0))
                        {
                            Message_LB.Text = "未找到PO相关信息,请联系IT-PPS处理!";
                            Message_LB.ForeColor = Color.Red;
                            this.reprint_BTN.Enabled = false;
                            this.cartonNo_TB.Focus();
                            this.cartonNo_TB.SelectAll();
                            return;
                        }
                        if (dtTempCartonInfo.Rows.Count > 1)
                        {
                            Message_LB.Text = "同一箱号对应PO出货地址不一致,请联系IT-PPS处理!";
                            Message_LB.ForeColor = Color.Red;
                            this.reprint_BTN.Enabled = false;
                            this.cartonNo_TB.Focus();
                            this.cartonNo_TB.SelectAll();
                            return;
                        }
                        Message_LB.Text = "OK,请点击补印！";
                        Message_LB.ForeColor = Color.Blue;
                        this.reprint_BTN.Enabled = true;
                    }
                    else
                    {
                        Message_LB.Text = "该箱号不需要打印,请检查！";
                        Message_LB.ForeColor = Color.Red;
                        this.cartonNo_TB.Focus();
                        this.cartonNo_TB.SelectAll();
                        this.reprint_BTN.Enabled = false;
                        return;
                    }
                }
                else
                {
                    Message_LB.Text = "该箱号不存在,请检查！";
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
