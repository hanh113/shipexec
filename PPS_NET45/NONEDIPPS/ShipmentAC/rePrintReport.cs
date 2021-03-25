using ClientUtilsDll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShipmentAC
{
    public partial class rePrintReport : Form
    {
        public rePrintReport()
        {
            InitializeComponent();
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
                return;
            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                TextMsg.BackColor = Color.Blue;
                TextMsg.Text = "";


                string strsShipmentID = StrPalletNo = txtBox.Text.Trim();
                string errormsg = string.Empty;
                strsShipmentID = changeSNtoShipmentID(strsShipmentID, out errormsg);
                if (errormsg.Contains("NG"))
                {
                    ShowMsg("刷入序号查询资料异常", 1);
                    return;
                }

                if (!IsCarALLOver(strsShipmentID)&&1!=1)
                {
                    ShowMsg("刷入序号所在车未装车完毕,请检查!", 1);
                    return;
                }

                //HYQ： 打印水晶报表。
                ShipmentDal da = new ShipmentDal();
                //int remainNum = da.GetShipmentRemainNum(strsShipmentID);
                int remainNum = 0;
                if (remainNum == 0)
                {
                    //HYQ: 这里可以打印水晶报表了 handoverManifest

                    try
                    {
                        CRReport.CRMain cr = new CRReport.CRMain();
                        cr.ACHanDoveMan2(strsShipmentID, StrPalletNo, true, false, "", "", "");
                    }
                    catch (Exception ex)
                    {
                        ShowMsg(ex.Message, 1);
                        return;
                    }

                    txtBox.Text = "";
                    txtBox.SelectAll();
                    txtBox.Focus();

                    TextMsg.Text = "打印OK";
                    TextMsg.BackColor = Color.Blue;


                }
                else
                {
                    ShowMsg("shipemntid存在没有装车完的栈板", 1);
                }

            }


        }


        private string changeSNtoShipmentID(string changeSNtoShipmentID, out string errmsg)
        {

            string sql = string.Format(@"Select distinct a.shipment_id 
                                        from NONEDIPPS.t_shipment_pallet a  
                                        join NONEDIPPS.t_shipment_info b 
                                         on a.shipment_id =b.shipment_id
                                        where b.status in('LF','UF')
                                    and  (a.pallet_no='{0}' or a.real_pallet_no='{1}' or a.shipment_id='{2}') "
                                    , changeSNtoShipmentID, changeSNtoShipmentID, changeSNtoShipmentID);

            DataTable dt_change = new DataTable();
            try
            {
                dt_change = ClientUtils.ExecuteSQL(sql).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                errmsg = "NG";
                return "";
            }


            if (dt_change.Rows.Count > 0)
            {
                //如果输入的时real_pallet_no 或者时print_pallet_no 
                //转换位pallet_no 来处理
                changeSNtoShipmentID = dt_change.Rows[0]["shipment_id"].ToString();
                errmsg = "";
                return changeSNtoShipmentID;
            }
            else
            {
                errmsg = "NG";
                return "";
            }

        }

        private bool IsCarALLOver(string strShipment)
        {
            DataTable dtTemp = ClientUtils.ExecuteSQL(string.Format(@"
                SELECT count(*) as PACOUNT
FROM NONEDIOMS.oms_load_car a
WHERE(a.car_no, TRUNC(a.expectedtime)) IN
(
SELECT DISTINCT b.car_no, TRUNC(c.SHIPPING_TIME)
FROM NONEDIOMS.oms_load_car b INNER JOIN NONEDIPPS.T_SHIPMENT_INFO c ON b.SHIPMENT_ID = c.SHIPMENT_ID
INNER JOIN NONEDIPPS.T_SHIPMENT_PALLET c ON b.SHIPMENT_ID = c.SHIPMENT_ID
WHERE c.SHIPMENT_ID = '{0}' OR c.PALLET_NO = '{0}' OR c.REAL_PALLET_NO = '{0}'
)
AND nvl(a.isload, 0) <> 1
                ", strShipment)).Tables[0];
            if ((dtTemp == null) || (dtTemp.Rows.Count <= 0))
            {
                return false;
            }
            if (dtTemp.Rows[0]["PACOUNT"].ToString().Trim() == "0")
            {
                return true;
            }
            return false;
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
