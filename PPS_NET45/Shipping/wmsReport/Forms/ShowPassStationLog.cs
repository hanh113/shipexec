using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wmsReport.Core;

namespace wmsReport
{
    public partial class ShowPassStationLog : Form
    {
        private string cartonNo;
        private Controller controller;
        public ShowPassStationLog(string cartonNo)
        {
            InitializeComponent();
            controller = new Controller();
            this.cartonNo = cartonNo;
            ClientUtils.runBackground((Form)this);
        }

        private void ShowPassStationLog_Load(object sender, EventArgs e)
        {
            ExecuteResult exeRes = new ExecuteResult();
            exeRes = controller.getPassStationLogByCarton(cartonNo);
            if (exeRes.Status)
            {
                this.show_data_log_DGV.DataSource = (DataTable)exeRes.Anything;
                this.Message_LB.Text = exeRes.Message;
                this.Message_LB.ForeColor = Color.Blue;
                return;
            }
            else
            {
                this.Message_LB.Text = exeRes.Message;
                this.Message_LB.ForeColor = Color.Red;
                return;
            }
        }
    }
}
