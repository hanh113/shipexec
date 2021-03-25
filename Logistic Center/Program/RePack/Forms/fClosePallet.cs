using ClientUtilsDll;
using ClientUtilsDll.Forms;
using RePack.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RePack.Forms
{
    public partial class fClosePallet : PPSForm
    {
        private string _strPallet;
        private string _strType;
        private ExecutionResult exeRes;
        private CommonProcessExcute core;
        public fClosePallet()
        {
            exeRes = new ExecutionResult();
            core = new CommonProcessExcute();
            InitializeComponent();
            
        }
        public fClosePallet(string strPallet, string strType = "")
        {
            _strPallet = strPallet;
            strType = _strType;
            exeRes = new ExecutionResult();
            core = new CommonProcessExcute();
            InitializeComponent();
        }

        private void btnOpenPallet_Click(object sender, EventArgs e)
        {
            _strType = "OPEN";
            exeRes = core.CheckPalletNo(tbPallet.Text.Trim().ToUpper(), _strType);
            ShowMSG(_strType  +" "+ exeRes.Message, exeRes.Status ? 3 : 0);
        }

        private void btnClosePallet_Click(object sender, EventArgs e)
        {
            _strType = "CLOSE";
            exeRes = core.CheckPalletNo(tbPallet.Text.Trim().ToUpper(), _strType);
            ShowMSG(_strType+" "+ exeRes.Message, exeRes.Status ? 3 : 0);
            fMain.bType = exeRes.Status;
            if (exeRes.Status)
            {
                fReprint fReprint = new fReprint(tbPallet.Text.Trim().ToUpper());
                fReprint.ShowDialog();
                this.Hide();
            }
        }

        private void tbPallet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            btnClosePallet_Click(sender, e);
        }

        private void fClosePallet_Load(object sender, EventArgs e)
        {
            tbPallet.Text = string.IsNullOrEmpty(_strPallet) ? "" : _strPallet;
            tbPallet.SelectAll();
            tbPallet.Focus();
        }

        private void prgMSG_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(prgMSG, prgMSG.Text);
        }
    }
}
