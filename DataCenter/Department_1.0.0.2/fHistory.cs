using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SajetClass;

namespace CDept
{
    public partial class fHistory : Form
    {
        public fHistory()
        {
            InitializeComponent();
        }

        private void fHistory_Load(object sender, EventArgs e)
        {
            SajetCommon.SetLanguageControl(this);
        }
    }
}