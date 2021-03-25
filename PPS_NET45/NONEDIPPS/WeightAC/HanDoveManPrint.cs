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
    public partial class HanDoveManPrint : Form
    {
        public HanDoveManPrint()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CRReport.CRMain cr = new CRReport.CRMain();
            //cr.HanDoveMan(textBox1.Text.Trim(),true);
            //ShowMsg("打印成功！", 1);
        }
        public DialogResult ShowMsg(string sText, int iType)
        {
            txtMsg.Text = sText;
            switch (iType)
            {
                case 0: //Error                
                    txtMsg.ForeColor = Color.Red;
                    txtMsg.BackColor = Color.Yellow;
                    return DialogResult.None;
                case 1: //Warning                        
                    txtMsg.ForeColor = Color.Blue;
                    txtMsg.BackColor = Color.White;
                    return DialogResult.None;
                case 2: //Confirm
                    return MessageBox.Show(sText, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    txtMsg.ForeColor = Color.Green;
                    txtMsg.BackColor = Color.White;
                    return DialogResult.None;
            }
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            txtMsg.Text = "";
            txtMsg.BackColor = Color.White;
            try
            {
                if (e.KeyCode != Keys.Enter)
                {
                    return;
                }
                if (string.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    ShowMsg("SN不能为空", 0);
                    return;
                }
            }
            catch
            { }
        }
    }
}
