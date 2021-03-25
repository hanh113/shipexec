using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace wmsReport
{
    public partial class FrmSNSingle : Form
    {
        public string strSnArray
        {
            get { return this._strSnArray; }
        }

        private string _strSnArray = string.Empty;
        public FrmSNSingle()
        {
            InitializeComponent();
            ClientUtils.runBackground((Form)this);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this._strSnArray = this.txtSNList.Text.Trim();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
