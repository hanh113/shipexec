//using CRReport.Reports.Consumer.P1_ConsumerPackingList__China;
using CRReport.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRReport
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //fP1_ConsumerPackingList_China frm = new fP1_ConsumerPackingList_China();
            //frm.Show();
            //MixServices service = new MixServices();
            //service.ExecuteSqlTran();

            CRReport.CRfrom.FDHubGlobalLayoutForm fdc = new CRfrom.FDHubGlobalLayoutForm("CASE4918641", "1501834004", @"D:\", true);
        }

        private void fMain_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
