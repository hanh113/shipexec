using ClientUtilsDll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Check
{
    public partial class FrmUnLock : Form
    {
        private string strUnLock = "";
        public FrmUnLock(string strUnLockValue)
        {
            InitializeComponent();
            ClientUtils.runBackground((Form)this);
            strUnLock = strUnLockValue;
        }

        private void Show_Message(string msg, int type)
        {
            this.labinfo.Text = msg;
            switch (type)
            {
                case 0: //error
                    this.labinfo.ForeColor = Color.Red;
                    this.labinfo.BackColor = Color.Yellow;
                    break;
                case 1:
                    this.labinfo.ForeColor = Color.Blue;
                    this.labinfo.BackColor = Color.White;
                    break;
                default:
                    this.labinfo.ForeColor = Color.Black;
                    this.labinfo.BackColor = Color.White;
                    break;
            }
        }

        private void FrmUnLock_Load(object sender, EventArgs e)
        {
            Show_Message("", -1);
        }

        private void txtPW_KeyDown(object sender, KeyEventArgs e)
        {
            Show_Message("", -1);
            if (string.IsNullOrEmpty(this.txtPW.Text.Trim()))
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtPW.Text.Trim().ToUpper() == this.strUnLock.ToUpper())
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    Show_Message("解锁密码错误，请检查!".TL(), 0);
                    this.txtPW.Focus();
                    return;
                }
            }
        }
    }
}
