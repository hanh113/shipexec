using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace wmsReportAC
{
    public partial class formTrolleyMove : Form
    {
        public formTrolleyMove()
        {
            InitializeComponent();
        }

        private void formTrolleyMove_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            initCarFrom();
            initLocationTo();
        }
        private void initCarFrom()
        {
            string strSql = @"
                                select distinct a.trolley_no id ,a.trolley_no name 
                                  from NONEDIPPS.t_trolley_sn_status a
                                 where a.trolley_no not in ('ICT-00-00-000', 'ICT-00-00-SYS')
                                 order by a.trolley_no asc";
            WMSBLL wb = new WMSBLL();
            wb.fillCmb(strSql, "", cmbCARFrom);
        }
        private void initLocationTo()
        {
            string strSql = @"
                           select a.location_no id, a.location_no name
                              from NONEDIPPS.wms_location a
                             where a.location_id not in
                                   (select b.location_id from NONEDIPPS.t_location b)
                               and a.warehouse_id in (select c.warehouse_id
                                                        from NONEDIPPS.wms_warehouse c
                                                       where c.enabled = 'Y')
                               and a.warehouse_id in (select d.warehouse_id from NONEDIPPS.t_location d)
                               and a.enabled = 'Y'
                             order by a.location_no asc";
            WMSBLL wb = new WMSBLL();
            wb.fillCmb(strSql, "", cmbLocationTo);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string strResult = string.Empty;
            string strResulterrmsg = string.Empty;
            WMSBLL wb = new WMSBLL();
            strResult = wb.WMSTrolleyMoce(cmbCARFrom.Text, cmbLocationTo.Text, out strResulterrmsg);

            if (strResult.Equals("OK"))
            {
                LibHelperAC.MediasHelper.PlaySoundAsyncByOk();
                ShowMsg("OK", -1);
            }
            else
            {
                LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                ShowMsg(strResulterrmsg, 0);
            }

            showdgvCar();

        }
        public void showdgvCar()
        {
            WMSBLL wb = new WMSBLL();
            wb.ShowCarStockInfo(cmbCARFrom.Text, dgvCar);
        }
        public DialogResult ShowMsg(string strTxt, int strType)
        {
            TextMsg.Text = strTxt;
            switch (strType)
            {
                case 0: //Error                
                    TextMsg.ForeColor = Color.Red;
                    TextMsg.BackColor = Color.Silver;
                    return DialogResult.None;
                case 1: //Warning                        
                    TextMsg.ForeColor = Color.Blue;
                    TextMsg.BackColor = Color.FromArgb(255, 255, 128);
                    return DialogResult.None;
                case 2: //Confirm
                    return MessageBox.Show(strTxt, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    TextMsg.ForeColor = Color.Green;
                    TextMsg.BackColor = Color.White;
                    return DialogResult.None;
            }
        }

        private void cmbCARFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            showdgvCar();
        }
    }
}
