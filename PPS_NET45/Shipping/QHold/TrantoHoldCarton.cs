using ClientUtilsDll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QHold
{
    public partial class TrantoHoldCarton : Form
    {
        public TrantoHoldCarton()
        {
            InitializeComponent();
            ClientUtils.runBackground((Form)this);
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            ShowMsg("", 0);
            string StrCartonNo = txtBox.Text.Trim();
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            if (string.IsNullOrEmpty(StrCartonNo))
            {
                TextMsg.Text = "";
                TextMsg.BackColor = Color.Blue;
            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                TextMsg.BackColor = Color.Blue;
                TextMsg.Text = "";

                if (StrCartonNo.Length == 20 && StrCartonNo.Substring(0, 2).Equals("00"))
                { StrCartonNo = StrCartonNo.Substring(2); }
                StrCartonNo = changeSNtoCarton(StrCartonNo);

                if (string.IsNullOrEmpty(StrCartonNo))
                {
                    ShowMsg("查询不到有效的记录", 0);
                    return;
                }
                {
                    QHoldBll qh = new QHoldBll();
                    string errorMessage = string.Empty;
                    Int16 holdcount = (Int16)nudCSNcount.Value;
                    string strResult = qh.TOHoldCarton(StrCartonNo, holdcount, out errorMessage);
                    if (!strResult.Contains("OK"))
                    {
                        ShowMsg(errorMessage, 0);
                        return;
                    }
                    else
                    {
                        ShowMsg(errorMessage, 0);
                    }

                }
                txtBox.SelectAll();
                txtBox.Focus();

            }

        }

        private string changeSNtoCarton(string StrCartonNo)
        {

            string sql = string.Format("Select carton_no from ppsuser.t_sn_status where customer_sn='{0}' or carton_no='{1}' ", StrCartonNo, StrCartonNo);
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
                StrCartonNo = dt_change.Rows[0]["carton_no"].ToString();
                return StrCartonNo;
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
