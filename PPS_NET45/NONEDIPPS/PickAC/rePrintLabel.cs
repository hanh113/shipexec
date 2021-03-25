using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PickListAC
{
    public partial class rePrintLabel : Form
    {
        public rePrintLabel()
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
            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                TextMsg.BackColor = Color.Blue;
                TextMsg.Text = "";

                string strPickPalletno = txtBox.Text.Trim();
                PickListBll plb = new PickListBll();
                strPickPalletno = plb.DelPrefixCartonSN(strPickPalletno);

                strPickPalletno = changeSNtoPickPalletno(strPickPalletno);

                if (string.IsNullOrEmpty(strPickPalletno))
                {
                    ShowMsg("查询不到有效的记录", 0);
                    return;
                }
                {
                    PickPalletLabel ppl = new PickPalletLabel();

                    if (ppl.PrintPickPalletLabel_new(strPickPalletno))
                    {
                        ShowMsg("打印OK", -1);


                    }
                    else
                    {
                        ShowMsg("打印连接出了问题", 0);
                    }

                }

                txtBox.SelectAll();
                txtBox.Focus();
            }
      

        }

       
        private string changeSNtoPickPalletno(string SNtoPickPalletno)
        {
            
            string sql = string.Format("Select pick_pallet_no from NONEDIPPS.t_sn_status where customer_sn='{0}' or carton_no='{1}' or pick_pallet_no='{2}'", SNtoPickPalletno, SNtoPickPalletno, SNtoPickPalletno);
            DataTable dt_change = new DataTable();
            try
            {
                dt_change = ClientUtils.ExecuteSQL(sql).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "";
            }


            if (dt_change.Rows.Count > 0)
            {
                //如果输入的时real_pallet_no 或者时print_pallet_no 
                //转换位pallet_no 来处理
                SNtoPickPalletno = dt_change.Rows[0]["pick_pallet_no"].ToString();
                return SNtoPickPalletno;
            }
            else
            {
                return "";
            }

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
                    TextMsg.ForeColor = Color.Black;
                    TextMsg.BackColor = Color.Blue;
                    return DialogResult.None;
            }
        }
    }
}
