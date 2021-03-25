using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WeightAC
{
    public partial class fMarkPrint : Form
    {

        /// <summary>
        /// 是否用水晶报表打印
        /// </summary>
        private bool isPrintCry = false;

        public fMarkPrint()
        {
            InitializeComponent();
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            //PrintStart(txtPalletNo.Text.ToString());
            string msg = "";
            DSPalletSheetlabel_multi rePrintWeightLabel = new DSPalletSheetlabel_multi();
            string strpalletno = txtFindNo.Text.Trim();
            strpalletno = changerealpallettopallet(strpalletno);

            if (isPrintCry)
            {
                //CRReport.CRMain cr = new CRReport.CRMain();
                //cr.PalletLoadingSheet(strpalletno, true, false, "");
            }
            else
            {
                if (rePrintWeightLabel.PrintShippingMarkLabel(strpalletno,8,cobPage.Text,out msg))
                { 
                    Show_Message("打印OK", 1);
                }  
                else
                {
                    if (!string.IsNullOrEmpty(msg))
                    {
                        Show_Message(msg, 0);
                    }
                    else
                    {
                        Show_Message("打印连接出了问题", 0);
                    }
                }
            }
            txtFindNo.Enabled = true;
            txtFindNo.Text = "";
            txtFindNo.Select();
            cobPage.Enabled = false;
            btPrint.Enabled = false;
        }

        private string changerealpallettopallet(string realpalletno)
        {
            if (realpalletno.Trim().Length == 20)
            {
                string changsntopallet = string.Format("Select pallet_no from NONEDIPPS.t_shipment_pallet where pallet_no='{0}' or real_pallet_no='{1}'", realpalletno, realpalletno);
                DataTable dt_change = new DataTable();
                try
                {
                    dt_change = ClientUtils.ExecuteSQL(changsntopallet).Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return realpalletno;
                }


                if (dt_change.Rows.Count > 0)
                {
                    //如果输入的时real_pallet_no 或者时print_pallet_no 
                    //转换位pallet_no 来处理
                    realpalletno = dt_change.Rows[0]["pallet_no"].ToString();
                    return realpalletno;
                }
                else
                {
                    return realpalletno;
                }
            }
            else
            {
                return realpalletno;
            }
        }

        private void txtFindNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                TextMsg.Text = "";
                TextMsg.BackColor = Color.Blue;



                string errorMessage = string.Empty;
                string strpalletno = txtFindNo.Text.Trim();
                if (string.IsNullOrEmpty(strpalletno))
                {
                    TextMsg.Text = "";
                    TextMsg.BackColor = Color.Blue;
                    return;
                }
                //如果是realpallet_no 转换下
                strpalletno = changerealpallettopallet(strpalletno);

                //换成PICK栈板号
                if ((strpalletno.Length > 2) && (strpalletno.Substring(0, 1).ToUpper() == "P"))
                {
                    strpalletno = strpalletno.Substring(2);
                    this.txtFindNo.Text = strpalletno;
                }

                string checkWeightSelect = string.Format("select a.customer_sn ,a.wc wc,c.* "
                                                          + "from NONEDIPPS.t_sn_status a "
                                                          + "join NONEDIPPS.t_shipment_pallet b on a.pack_pallet_no = b.pallet_no "
                                                          + "join (select * from (select pallet_no, weight, standard_weight, per_devweight from NONEDIPPS.t_pallet_weight_log where pallet_no= '{0}' AND PASS = '1'  order by cdt desc) where rownum = 1) c on b.pallet_no = c.pallet_no "
                                                          + "where b.pallet_no='{1}'", strpalletno, strpalletno);
                DataTable dt_checkWeight = new DataTable();
                try
                {
                    dt_checkWeight = ClientUtils.ExecuteSQL(checkWeightSelect).Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }


                if (dt_checkWeight.Rows.Count > 0)
                {
                    string weightStationCheck = "";
                    for (int i = 0; i < dt_checkWeight.Rows.Count; i++)
                    {
                        string weightStation = dt_checkWeight.Rows[i]["wc"].ToString();
                        weightStation = weightStation.Trim();



                        if (!(weightStation.Equals("W4") || weightStation.Equals("W5")))
                        {
                            weightStationCheck = "存在异常站别";
                            break;
                        }
                        if (weightStationCheck.Equals("存在异常站别"))
                        {
                            string errmessage = "输入的栈板号: " + strpalletno + "中存在序号站别异常,不能做补列印作业";
                            TextMsg.Text = errmessage;
                            TextMsg.BackColor = Color.Yellow;
                            txtFindNo.Text = "";
                            return;
                        }

                        if (i == 0)
                        {
                            txtWeight.Text = dt_checkWeight.Rows[i]["weight"].ToString();
                            txtStandard.Text = dt_checkWeight.Rows[i]["standard_weight"].ToString();
                            txtDeviation.Text = dt_checkWeight.Rows[i]["per_devweight"].ToString();
                        }

                        txtFindNo.Enabled = false;

                        cobPage.Enabled = true;
                        btPrint.Enabled = true;

                    }


                }


                else
                {
                    TextMsg.Text = "查无资料";
                    txtFindNo.Enabled = true;
                    cobPage.Enabled = false;
                    btPrint.Enabled = false;
                }
            }




        }

        /// <summary>
        /// 输入后，弹出窗口状态信息
        /// </summary>
        private void Show_Message(string msg, int type)
        {
            TextMsg.Text = msg;
            switch (type)
            {
                case 0: 
                    TextMsg.BackColor = Color.Yellow;
                    break;
                case 1:
                    TextMsg.BackColor = Color.Blue;
                    break;
                default:
                    TextMsg.BackColor = Color.White;
                    break;
            }
        }

    }
}
