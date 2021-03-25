using ClientUtilsDll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shipment
{
    public partial class fShipmentConfirm : Form
    {
        public fShipmentConfirm()
        {
            InitializeComponent();
            ClientUtils.runBackground((Form)this);
        }

        private void fShipmentConfirm_Load(object sender, EventArgs e)
        {
            this.dtpStartTime.ValueChanged -= new System.EventHandler(this.dtpStartTime_ValueChanged);
            this.dtpEndTime.ValueChanged -= new System.EventHandler(this.dtpEndTime_ValueChanged);

            DateTime dateTimeNow = DateTime.Now;
            dtpStartTime.Value = DateTime.Now.AddMonths(-1);
            dtpEndTime.Value = DateTime.Now.AddDays(1);


            //关掉触发
            this.cmbCarNo.SelectedIndexChanged -= new System.EventHandler(this.cmbCarNo_SelectedIndexChanged);


            getTruckNo(dtpStartTime.Text, dtpEndTime.Text);


            this.dtpStartTime.ValueChanged += new System.EventHandler(this.dtpStartTime_ValueChanged);
            this.dtpEndTime.ValueChanged += new System.EventHandler(this.dtpEndTime_ValueChanged);

            //开启触发
            this.cmbCarNo.SelectedIndexChanged += new System.EventHandler(this.cmbCarNo_SelectedIndexChanged);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void dtpEndTime_ValueChanged(object sender, EventArgs e)
        {
            getTruckNo(dtpStartTime.Text, dtpEndTime.Text);
        }

        private void dtpStartTime_ValueChanged(object sender, EventArgs e)
        {
            getTruckNo(dtpStartTime.Text, dtpEndTime.Text);
        }
        private void cmbCarNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowAllDatabyTimeandTruck(dtpStartTime.Text, dtpEndTime.Text,cmbCarNo.Text);
        }
        private void getTruckNo(string starttime,string endtime)
        {
            string strSql = string.Format(@"select distinct b.car_no id, b.car_no name
                                              FROM ppsuser.t_shipment_pallet a
                                              join pptest.oms_load_car b
                                                on a.shipment_id = b.shipment_id
                                               --and isload = 1
                                               and (b.active = 0 or b.active is null)
                                             WHERE (to_date(a.cdt) >= to_date('{0}', 'YYYY-MM-DD') AND
                                                   to_date(a.cdt) <= to_date('{1}', 'YYYY-MM-DD'))
                                             order by b.car_no asc", starttime, endtime) ;
            ShipmentBll sb = new ShipmentBll();
            sb.fillCmb(strSql, "", cmbCarNo);
        }

        private void ShowAllDatabyTimeandTruck(string starttime, string endtime,string truckno)
        {

            txtWHStatus.Text = "";
            txtSecurityStatus.Text = "";

            //清空dgvPartInfo
            ShipmentBll shipmentBll = new ShipmentBll();
            
            DataTable dtShipMentDataTalbe = (DataTable)dgvPartInfo.DataSource;
            if (dtShipMentDataTalbe != null)
            {
                dtShipMentDataTalbe.Rows.Clear();
                dgvPartInfo.DataSource = dtShipMentDataTalbe;
            }
          
            //重新获取数据 
            DataTable dataTable = shipmentBll.GetCarInfoDataTable( starttime,  endtime,  truckno);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {

                dgvPartInfo.DataSource = dataTable;

                if (!string.IsNullOrEmpty(dataTable.Rows[0]["仓库确认人"].ToString()))
                {
                    txtWHStatus.Text = "仓库确认OK";
                    txtWHStatus.BackColor = Color.Green;
                }
                else
                {
                    txtWHStatus.Text = "待仓库确认";
                    txtWHStatus.BackColor = Color.Yellow;
                }

                if (!string.IsNullOrEmpty(dataTable.Rows[0]["安保确认人"].ToString()))
                {
                    txtSecurityStatus.Text = "安保确认OK";
                    txtSecurityStatus.BackColor = Color.Green;
                }
                else
                {
                    txtSecurityStatus.Text = "待安保确认";
                    txtSecurityStatus.BackColor = Color.Yellow;
                }


            }
            string strallreturnlist = string.Empty;
            string strerrmsg = string.Empty;
            bool isResultOK = shipmentBll.getPalletsQtyByTruck( starttime,  endtime,  truckno, out strallreturnlist, out strerrmsg);
            if (!isResultOK)
            {
                ShowMsg(strerrmsg, 0);
                txtName.Text = "";
                txtID.Text = "";
                txtTruck.Text = "";
                txtPalletQty.Text = "0";
                txtCartonQty.Text = "0";
                txtQty.Text = "0";
            
                return;
            }
            string[] CarInfo = strallreturnlist.Split('*');
            
            txtName.Text = CarInfo[0];
            txtID.Text = CarInfo[1];
            txtTruck.Text = CarInfo[2];
            txtPalletQty.Text = CarInfo[3];
            txtCartonQty.Text = CarInfo[4];
            txtQty.Text = CarInfo[5];
           
            ShowMsg(strerrmsg, -1);
        }


        private void btnWH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                ShowMsg("司机名称信息没有维护", 0);
            }
            
            string strerrmsg = string.Empty;
            ShipmentBll sb = new ShipmentBll();
            bool isResultOK = sb.InertTruckConfirm(dtpStartTime.Text, dtpEndTime.Text, cmbCarNo.Text, "WH",txtWH.Text, out strerrmsg);
            if (!isResultOK)
            {
                ShowMsg(strerrmsg, 0);
                return;
            }
            ShowAllDatabyTimeandTruck(dtpStartTime.Text, dtpEndTime.Text, cmbCarNo.Text);
               
        }

        private void btnSecurity_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                ShowMsg("司机名称信息没有维护", 0);
            }
            string strerrmsg = string.Empty;
            ShipmentBll sb = new ShipmentBll();
            bool isResultOK = sb.InertTruckConfirm(dtpStartTime.Text, dtpEndTime.Text, cmbCarNo.Text, "SECURITY", txtSecurity.Text, out strerrmsg);
            if (!isResultOK)
            {
                ShowMsg(strerrmsg, 0);
                return;
            }
            ShowAllDatabyTimeandTruck(dtpStartTime.Text, dtpEndTime.Text, cmbCarNo.Text);
            
        }

        public DialogResult ShowMsg(string strTxt, int strType)
        {
            TextMsg.Text = strTxt.TP();
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

    }
}
