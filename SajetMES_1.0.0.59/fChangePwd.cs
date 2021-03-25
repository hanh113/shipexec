using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using ClientUtilsDll;

namespace EDIPPS
{
    public partial class fChangePwd : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        
        public fChangePwd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPwd.Text))
            {
                ClientUtils.ShowMessage("Password Empty!", 0, "", Program.g_sFileName);
                textBoxPwd.Focus();
                textBoxPwd.SelectAll();
                return;
            }
            else if (string.IsNullOrEmpty(textBoxNew.Text))
            {
                ClientUtils.ShowMessage("New Password Empty!", 0, "", Program.g_sFileName);
                textBoxNew.Focus();
                textBoxNew.SelectAll();
                return;
            }
            else if (textBoxNew.Text != textBoxConfirm.Text)
            {
                ClientUtils.ShowMessage("New Password not equal to Confirm Password!", 0, "", Program.g_sFileName);
                textBoxNew.Focus();
                textBoxNew.SelectAll();
                return;
            }
            try
            {
                DataSet dsTemp = ClientUtils.ExecuteSQL("Select Trim(SAJET.password.decrypt(PASSWD)) PASSWD "
                    + "From sajet.SYS_EMP Where EMP_ID = " + ClientUtils.UserPara1 + " and rownum = 1");
                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    if (textBoxPwd.Text != dsTemp.Tables[0].Rows[0]["PASSWD"].ToString())
                    {
                        ClientUtils.ShowMessage("PasswordError", 0, "", Program.g_sFileName);
                        textBoxPwd.Focus();
                        textBoxPwd.SelectAll();
                    }
                    else
                    {
                        ClientUtils.ExecuteSQL("Update SAJET.SYS_EMP Set PASSWD = SAJET.password.encrypt('" + textBoxNew.Text + "') "
                            + ",CHANGE_PW_TIME = sysdate Where EMP_ID = " + ClientUtils.UserPara1 + " and rownum = 1");
                        ClientUtils.ShowMessage("ChangeSucceed", 3, "", Program.g_sFileName);
                        this.Close();
                    }
                }
                else
                    ClientUtils.ShowMessage("UserNotFound", 0,"", Program.g_sFileName);
            }
            catch (Exception ex)
            {
                ClientUtils.ShowMessage(ex.Message, 0, "", Program.g_sFileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tableLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void formChangePwd_Load(object sender, EventArgs e)
        {
            ClientUtils.SetLanguage(this, "");
            if (File.Exists(Program.skinPath + Program.skinName + @"\Login.jpg"))
                this.BackgroundImage = Image.FromFile(Program.skinPath + Program.skinName + @"\Login.jpg");
        }
    }
}