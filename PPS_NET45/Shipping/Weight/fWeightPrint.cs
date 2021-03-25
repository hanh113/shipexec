using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Drawing;
using System.Windows.Forms;
using LibHelper;

namespace Weight
{
    public partial class fWeightPrint : Form
    {
        /// <summary>
        /// 是否用水晶报表打印
        /// </summary>
        private bool isPrintCry = false;

        public fWeightPrint()
        {
            InitializeComponent();
            ClientUtils.runBackground((Form)this);
        }
        /// <summary>
        /// 查询需要打印的标签信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btFind_Click(object sender, EventArgs e)
        {
            if (GetWeightinfo())
            {
                this.ShowMsg("查询成功！", 1);
            }
            else
            {
                this.ShowMsg("查询数据不存在，请确认！", 0);
            }
        }
        /// <summary>
        /// 获取当前称重信息
        /// </summary>
        /// <returns></returns>
        private bool GetWeightinfo()
        {
            object[][] Param;
            string sSQL = "select a.* from ppsuser.g_ds_weight_t a where pallet_no=:pallet_no";
            Param = new object[1][];
            Param[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "pallet_no", txtFindNo.Text };
            DataSet sDataSet = ClientUtils.ExecuteSQL(sSQL, Param);

            if (sDataSet.Tables[0].Rows.Count > 0)
            {
                //txtPosition.Text = sDataSet.Tables[0].Rows[0]["wharf_location"].ToString();
                //txtPalletNo.Text = sDataSet.Tables[0].Rows[0]["pallet_no"].ToString();
                txtStandard.Text = sDataSet.Tables[0].Rows[0]["standard_weight"].ToString();
                txtWeight.Text = sDataSet.Tables[0].Rows[0]["weight"].ToString();
                txtDeviation.Text = sDataSet.Tables[0].Rows[0]["deviation"].ToString();
                return true;
            }
            else
            {
                return false;
            }

        }

        private void fWeightPrint_Load(object sender, EventArgs e)
        {
            txtFindNo.SelectAll();
            txtFindNo.Focus();
            cobPage.Enabled = false;
            btPrint.Enabled = false;
            txtWeight.Enabled = false;
            txtStandard.Enabled = false;
            txtDeviation.Enabled = false;
        }
        public DialogResult ShowMsg(string sText, int iType)
        {
            TextMsg.Text = sText;
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
        /// <summary>
        /// 打印标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btPrint_Click(object sender, EventArgs e)
        {
            //PrintStart(txtPalletNo.Text.ToString());

            DSPalletSheetlabel_multi rePrintWeightLabel = new DSPalletSheetlabel_multi();
            string strpalletno = txtFindNo.Text.Trim();
            strpalletno = changerealpallettopallet(strpalletno);

            if (isPrintCry)
            {
                CRReport.CRMain cr = new CRReport.CRMain();
                cr.PalletLoadingSheet(strpalletno, true, false, "");
            }
            else
            {
                if (rePrintWeightLabel.PrintPalletLabel(strpalletno, 8, cobPage.Text))
                {
                    TextMsg.Text = "打印OK";
                    TextMsg.BackColor = Color.Blue;
                    ClientUtils.WritePrintLabelLog("Pallet Loading Sheet", "", strpalletno, LocalHelper.getMacAddr_Local());
                }
                else
                {
                    TextMsg.Text = "打印连接出了问题";
                    TextMsg.BackColor = Color.Yellow;
                }
            }



            txtFindNo.Enabled = true;
            txtFindNo.Text = "";
            txtFindNo.Select();
            cobPage.Enabled = false;
            btPrint.Enabled = false;
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


                string checkWeightSelect = string.Format("select a.customer_sn ,a.wc wc,c.* "
                                                          + "from ppsuser.t_sn_status a "
                                                          + "join ppsuser.t_shipment_pallet b on a.pack_pallet_no = b.pallet_no "
                                                          + "join (select * from (select pallet_no, weight, standard_weight, per_devweight from ppsuser.t_pallet_weight_log where pallet_no= '{0}' AND PASS = '1'  order by cdt desc) where rownum = 1) c on b.pallet_no = c.pallet_no "
                                                          + "where b.pallet_no='{1}'", strpalletno, strpalletno);
                DataTable dt_checkWeight = new DataTable();
                try
                {
                    dt_checkWeight = ClientUtils.ExecuteSQL(checkWeightSelect).Tables[0];
                }
                catch (Exception  ex)
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

        private string changerealpallettopallet(string realpalletno)
        {
            if (realpalletno.Trim().Length == 20)
            {
                string changsntopallet = string.Format("Select pallet_no from ppsuser.t_shipment_pallet where pallet_no='{0}' or real_pallet_no='{1}'", realpalletno, realpalletno);
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

        private void UpdatePrintFlag(string pallet_no)//更新print flag
        {
            try
            {
                object[][] Param;
                string sSQL = @"UPDATE PPSUSER.XXX  A
                                SET A.PRINT_FLAG = 'Y'
                                WHERE a.PALLET_NO =:PALLET_NO";
                Param = new object[1][];
                Param[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PALLET_NO", pallet_no };
                DataSet sDataSet = ClientUtils.ExecuteSQL(sSQL, Param);
            }
            catch (Exception ex)
            {
                this.ShowMsg(ex.ToString(), 0);
            }

        }
        /// <summary>
        /// 打印标签
        /// </summary>
        public void PrintStart(string pallet_No)
        {
            if (string.IsNullOrEmpty(pallet_No))
            {
                ShowMsg("Pallet No 不能为空",0);
                return;
            }
            if (Math.Abs(Convert.ToDouble(txtDeviation.Text)) < 3)
            {
                //if (new Utility().Print_Label(pallet_No))
                //{
                //    this.ShowMsg("打印成功",1);
                //} 

            }
            else
            {
                //根据需求文档修改提示信息  盛家恒 2018-07-13 
                //---B---
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("实际重量＞理论重量差异值！", 0);
                //MessageBoxButtons mess = MessageBoxButtons.OKCancel;
                //DialogResult dr = MessageBox.Show("实际偏差超出允许范围，确定打印吗？", "提示", mess);
                //if (dr == DialogResult.OK)
                //{
                //    new Utility().Print_Label(pallet_No);
                //}
                //else
                //{
                //    return;
                //}
                //---END---
            }
        }

      
    }
}


