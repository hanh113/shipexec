using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using ClientUtilsDll;

namespace EDIPPS
{
    public partial class fError : Form
    {
        public fError(String errMessage, String stackTrace)
        {
            InitializeComponent();
            txtMessage.Text = errMessage;
            txtstackTrace.Text = stackTrace;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                ClientUtils.Logout();
            }
            catch { }
            Application.Exit();
        }

        private void fError_Load(object sender, EventArgs e)
        {
            string fileName = Program.skinPath + Program.skinName + @"\Login.jpg";
            if (File.Exists(fileName))
            {
                this.BackgroundImage = Image.FromFile(fileName);
            }
        }
    }
}