using ClientUtilsDll;
using LibHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Weight
{
    public partial class rePrintReport : Form
    {
        public rePrintReport()
        {
            InitializeComponent();
            ClientUtils.runBackground((Form)this);
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            string StrPalletNo = txtBox.Text.Trim();
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            if (string.IsNullOrEmpty(StrPalletNo))
            {
                TextMsg.Text = "";
                TextMsg.BackColor = Color.Blue;
            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                TextMsg.BackColor = Color.Blue;
                TextMsg.Text = "";


                string strsShipmentID = txtBox.Text.Trim();

                WeightBll pb1 = new WeightBll();
                //如果此集货单的所有的栈板都称重完成，且是PAC的，就打印Handover manifest

                string strResultShipment = string.Empty;
                string strResultRegion = string.Empty;
                pb1.CheckShipmentWeightStatus(StrPalletNo, out strResultRegion, out strResultShipment);

                if (strResultShipment.Equals("OK"))
                {
                    //OK  说明集货单所有栈板都称重完成，且是PAC
                    string errormsg2 = string.Empty;

                    strsShipmentID = pb1.changeSNtoShipmentID(StrPalletNo, out errormsg2);
                    if (errormsg2.Equals("OK"))
                    {
                        CRReport.CRMain cr = new CRReport.CRMain();
                        if (strResultRegion.Equals("PAC"))
                        {
                            cr.HanDoveMan(strsShipmentID, true, false);
                        }
                        else
                        {
                            cr.HanDoveMan2(strsShipmentID, true, false,"WEIGHT");
                        }
                        txtBox.Text = "";
                        txtBox.SelectAll();
                        txtBox.Focus();
                        TextMsg.Text = "打印OK";
                        TextMsg.BackColor = Color.Blue;
                        ClientUtils.WritePrintLabelLog("Handover Manifest", "", StrPalletNo, LocalHelper.getMacAddr_Local());
                    }
                    else
                    {
                        ShowMsg("刷入序号查询资料异常", 1);
                        return;
                    }
                }
                else
                {
                    ShowMsg(strResultShipment, 1);
                    return;
                }
            }
        }
        public DialogResult ShowMsg(string sText, int iType)
        {
            TextMsg.Text = sText.TP();
            switch (iType)
            {
                case 0: //Error                
                    TextMsg.ForeColor = Color.Red;
                    TextMsg.BackColor = Color.Yellow;
                    return DialogResult.None;
                case 1: //Warning                        
                    TextMsg.ForeColor = Color.Blue;
                    TextMsg.BackColor = Color.White;
                    return DialogResult.None;
                case 2: //Confirm
                    return MessageBox.Show(sText, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    TextMsg.ForeColor = Color.Green;
                    TextMsg.BackColor = Color.White;
                    return DialogResult.None;
            }
        }
    }
}
