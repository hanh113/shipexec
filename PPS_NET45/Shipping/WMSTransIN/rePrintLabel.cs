using ClientUtilsDll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WMSTransIN
{
    public partial class rePrintLabel : Form
    {
        public rePrintLabel()
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

                string strCarton = txtBox.Text.Trim();
                WMSTransINBLL wb = new WMSTransINBLL();
                strCarton = wb.DelPrefixCartonSN(strCarton);

                {
                    WHPalletLabel wp = new WHPalletLabel();

                    if (wp.PrintWHPalletLabel(strCarton))
                    {
                        TextMsg.Text = "打印OK".TP();
                    }
                    else
                    {
                        TextMsg.Text = "打印FAIL".TP();
                    }
                }
                txtBox.SelectAll();
                txtBox.Focus();
            }
        }


        private string changeSNtoPickPalletno(string SNtoPickPalletno)
        {

            string sql = string.Format("Select pick_pallet_no from ppsuser.t_sn_status where customer_sn='{0}' or carton_no='{1}' or pick_pallet_no='{2}'", SNtoPickPalletno, SNtoPickPalletno, SNtoPickPalletno);
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
                    TextMsg.ForeColor = Color.Black;
                    TextMsg.BackColor = Color.Blue;
                    return DialogResult.None;
            }
        }
    }
}
