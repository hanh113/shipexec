using ClientUtilsDll;
using RollbackDN.UPSCancel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RollbackDN
{
    public partial class ZCshipmentid : Form
    {
        public ZCshipmentid()
        {
            InitializeComponent();
            initComboBox();
            ClientUtils.runBackground((Form)this);
        }
        private void ZCshipmentid_Load(object sender, EventArgs e)
        {
            DateTime dateTimeNow = DateTime.Now;
            //edit by franky 根据当前日期判断两个月前月初时间为开始时间 2020/1/20
            dt_start.Value = new DateTime(DateTime.Now.AddMonths(-2).Year, DateTime.Now.AddMonths(-2).Month, 1);
            dt_end.Value = dateTimeNow.AddDays(1);
        }

        private void btnRollback_Click(object sender, EventArgs e)
        {
            string msg = "";
            ShowMsg("", -1);
            btnRollback.Enabled = false;
            string strShipmentid = cmbSmid.Text;
            if (string.IsNullOrEmpty(strShipmentid))
            {
                ShowMsg("Shipmentid为空", 0);
                btnRollback.Enabled = true;
                return;
            }

            RollbackBll rb = new RollbackBll();
            string errorMessage = string.Empty;
            string sendUPSCancel = string.Empty;
            CheckUPS_shipment chk = new CheckUPS_shipment();
            string checkUPSshipment = chk.UPSCheck(strShipmentid);
            if (checkUPSshipment.Equals("OK"))
            {
                if (chk.CheckUPSEnable())
                {
                    sendUPSCancel = chk.SendShipmentCancel(strShipmentid);
                    if (sendUPSCancel.Equals("OK"))
                        msg = "Sent cancel request to Sever OK";
                    else
                    {
                        ShowMsg("UPS cancel NG " + sendUPSCancel, 0);
                        btnRollback.Enabled = true;
                        return;
                    }
                }
            }

            string strResult = rb.RBShipmentID(strShipmentid, out errorMessage);
            if (!strResult.Equals("OK"))
            {
                ShowMsg(errorMessage, 0);
                btnRollback.Enabled = true;
                return;
            }
            ShowMsg("还原完成  " + msg, -1);
            btnRollback.Enabled = true;
        }
        private void initComboBox()
        {
            string strStartDay = dt_start.Value.ToString("yyyy-MM-dd");
            string strEndDay = dt_end.Value.ToString("yyyy-MM-dd");

            //SHIPMENTID
            cmbSmid.DataSource = null;
            cmbSmid.Items.Clear();
            //string strSql = @"SELECT distinct shipment_id "
            //                 + "     FROM ppsuser.t_shipment_info "
            //                 + "    where shipment_id not in "
            //                 + "          (select shipment_id "
            //                 + "             from (select shipment_id, sum(pick_carton) as sumcarton "
            //                 + "                     from ppsuser.t_shipment_pallet "
            //                 + "                    group by shipment_id) a "
            //                 + "            where a.sumcarton = 0) "
            //                 + "      and shipment_id not in "
            //                 + "          (select shipment_id "
            //                 + "             from  ppsuser.t_shipment_info "
            //                 + "              where shipment_id like 'FK%' "
            //                 + "      and cdt > to_date('2019-06-06', 'yyyy-mm-dd') ) "
            //                 + "    order by shipment_id asc";

            string strSql = string.Format(@"
                         SELECT distinct shipment_id
                                  FROM ppsuser.t_shipment_info
                                 where shipment_id not in
                                       (select shipment_id
                                          from (select shipment_id, sum(pick_carton) as sumcarton
                                                  from ppsuser.t_shipment_pallet
                                                 group by shipment_id) a
                                         where a.sumcarton = 0)
                                   and status not in ('CP', 'UF', 'WS', 'IN', 'SF')
                                   and cdt >= to_date('{0}', 'yyyy-mm-dd')
                                   and cdt <= to_date('{1}', 'yyyy-mm-dd')
                               ", strStartDay, strEndDay);

            DataSet dts = ClientUtils.ExecuteSQL(strSql);
            if (dts != null && dts.Tables[0].Rows.Count > 0)
            {


                List<string> carrierList = (from d in dts.Tables[0].AsEnumerable()
                                            select d.Field<string>("shipment_id")).ToList();
                carrierList.Sort();
                cmbSmid.DataSource = carrierList;

            }
            else
            {
                cmbSmid.DataSource = null;
            }
        }


        public DialogResult ShowMsg(string sText, int iType)
        {
            TextMsg.Text = sText.TP();
            switch (iType)
            {
                case 0: //Error                
                    TextMsg.ForeColor = Color.Red;
                    TextMsg.BackColor = Color.Yellow;
                    return DialogResult.None;
                case 1: //Warning                        
                    TextMsg.ForeColor = Color.Blue;
                    TextMsg.BackColor = Color.White;
                    return DialogResult.None;
                case 2: //Confirm
                    return MessageBox.Show(sText, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    TextMsg.ForeColor = Color.Black;
                    TextMsg.BackColor = Color.Blue;
                    return DialogResult.None;
            }
        }

        private void cmbSmid_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowMsg("", -1);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            initComboBox();
        }


    }
}
