using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wmsReportAC.Core;
using wmsReportAC.Entitys;

namespace wmsReportAC
{
    public partial class CreateDeliveryNoteForm : Form
    {
        Controller controller;
        public CreateDeliveryNoteForm()
        {
            InitializeComponent();
            controller = new Controller();
        }

        private void createDeliveryNote_BTN_Click(object sender, EventArgs e)
        {
            string closeTime = this.closeTime_DTP.Value.ToString("yyyyMMdd");
            ExecuteResult exeRes = new ExecuteResult();
            exeRes = controller.checkIsPrintDeliveryNote(closeTime);
            if (exeRes.Status)
            {
                Show_Message(exeRes.Message,1);
            }
            else
            {
                Show_Message(exeRes.Message, 0);
            }
        }
        private void Show_Message(string msg, int type)
        {
            Message_LB.Text = msg;
            switch (type)
            {
                case 0: //error
                    Message_LB.ForeColor = Color.Red;
                    Message_LB.BackColor = Color.Yellow;
                    break;
                case 1:
                    Message_LB.ForeColor = Color.Blue;
                    Message_LB.BackColor = Color.White;
                    break;
                default:
                    Message_LB.ForeColor = Color.Black;
                    Message_LB.BackColor = Color.White;
                    break;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
