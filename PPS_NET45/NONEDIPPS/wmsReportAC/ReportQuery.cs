using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wmsReportAC.Core;
using wmsReportAC.Utils;
namespace wmsReportAC
{
    public partial class ReportQuery : Form
    {
        private Controller controller;
        private CreateExcel createExcel;
        private WMSBLL wmsbll;
        public ReportQuery()
        {
            InitializeComponent();
            controller = new Controller();
            createExcel = new CreateExcel();
            wmsbll = new WMSBLL();
        }

        private void ReportQuery_Load(object sender, EventArgs e)
        {

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
        //private void shipmentId_TB_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        string shipmentId = this.shipmentId_TB.Text.ToUpper().Trim();
        //        if (string.IsNullOrEmpty(shipmentId))
        //        {
        //            Show_Message("没有输入集货单号！",0);
        //            return;
        //        }
        //        ExecuteResult exeRes = new ExecuteResult();
        //        DataTable dt = new DataTable();
        //        exeRes = controller.queryShipmentInfoByShipmentId(shipmentId,QAHold_CBox.Checked);
        //        if (exeRes.Status)
        //        {
        //           this.information_DGV.DataSource   =  (DataTable)exeRes.Anything;
        //           this.cartonNo_TB.Enabled = true;
        //            Show_Message("集货单号信息查询成功！", 1);
        //            this.shipmentId_TB.Focus();
        //            this.shipmentId_TB.SelectAll();
        //        }
        //        else
        //        {
        //            this.shipmentId_TB.Focus();
        //            this.shipmentId_TB.SelectAll();
        //            Show_Message(exeRes.Message,0);
        //            return;
        //        }
        //    }
        //}

        //private void cartonNo_TB_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        string cartonNo = this.cartonNo_TB.Text.ToUpper().Trim();
        //        if (string.IsNullOrEmpty(cartonNo))
        //        {
        //            Show_Message("没有输入箱号！",0);
        //            return;
        //        }
        //        ExecuteResult exeRes = new ExecuteResult();
        //        DataTable dt = new DataTable();
        //        exeRes = controller.queryCartonInfoByCartonNo(cartonNo);
        //        if (exeRes.Status)
        //        {
        //            this.information_DGV.DataSource = (DataTable)exeRes.Anything;
        //            this.shipmentId_TB.Enabled = true;
        //            Show_Message("箱号信息查询成功！",1);
        //            this.cartonNo_TB.Focus();
        //            this.cartonNo_TB.SelectAll();
        //        }
        //        else
        //        {
        //            this.cartonNo_TB.Focus();
        //            this.cartonNo_TB.SelectAll();
        //            Show_Message(exeRes.Message, 0);
        //            return;
        //        }
        //    }
        //}



        //private void palletNo_TB_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        string palletNo = this.palletNo_TB.Text.ToUpper().Trim();
        //        if (string.IsNullOrEmpty(palletNo))
        //        {
        //            Show_Message("没有输入栈板号！",0);
        //            return;
        //        }
        //        ExecuteResult exeRes = new ExecuteResult();
        //        DataTable dt = new DataTable();
        //        exeRes = controller.queryPalletNoInfoByPackPalletNo(palletNo);
        //        if (exeRes.Status)
        //        {
        //            this.information_DGV.DataSource = (DataTable)exeRes.Anything;
        //            this.shipmentId_TB.Enabled = true;
        //            Show_Message("栈板信息查询成功！", 1);
        //            this.palletNo_TB.Focus();
        //            this.palletNo_TB.SelectAll();
        //        }
        //        else
        //        {
        //            this.cartonNo_TB.Focus();
        //            this.cartonNo_TB.SelectAll();
        //            Show_Message(exeRes.Message, 0);
        //            return;
        //        }
        //    }
        //}

        private void export_BTN_Click(object sender, EventArgs e)
        {
            if (moreDetail_search_DGV.DataSource != null)
            {
                saveFile_SFD.Filter = "excel|*.xls|excel|*.xlsx";
                saveFile_SFD.FileName = "Report查询_" + DateTime.Now.ToString("yyyyMMddhhmmss");
                saveFile_SFD.AddExtension = true;
                if (saveFile_SFD.ShowDialog() == DialogResult.OK)
                {
                    string saveFileName = createExcel.DataToExcel((DataTable)moreDetail_search_DGV.DataSource, saveFile_SFD.FileName);
                    Show_Message("保存文件成功-" + saveFileName, 1);
                }
            }
        }



        private void queryShipInfo_BTN_Click(object sender, EventArgs e)
        {            
            this.information_DGV.Visible = false;
            this.moreDetail_search_DGV.Visible = true;
            string pps_pallet = this.pps_pallet_TB.Text.ToUpper().Trim();
            string pps_Pick_pallet = this.pps_pick_palletNo_TB.Text.ToUpper().Trim();
            string delivery_no = this.more_deliveryNo_CB.Text.ToUpper().Trim();
            string location_no = this.locationNo_TB.Text.ToUpper().Trim();
            string MES_pallet = this.MES_palletNO_TB.Text.ToUpper().Trim();
            string Ict_pn = this.ICTPN_TB.Text.ToUpper().Trim();
            string carton_no = wmsbll.DelPrefixCartonSN(this.cartonNo_TB.Text.ToString().Trim());
            string sscc_code = this.sscc_code_TB.Text.ToUpper().Trim();
            string shipmentId = this.detail_shipmentid_TB.Text.ToUpper().Trim();
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            exeRes = controller.queryMoreInfoByMultiOption(MES_pallet,pps_pallet,pps_Pick_pallet,delivery_no,location_no,Ict_pn, carton_no,sscc_code, shipmentId);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                this.moreDetail_search_DGV.DataSource = dt;               
                Show_Message(exeRes.Message, 1);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["序号"].ToString().Equals("1"))
                    {
                        this.moreDetail_search_DGV.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    }
                }
                //将此shipmentId 对应的deliveryNo show到moreDeliveryNo_cb上
                if (!(string.IsNullOrEmpty(shipmentId)) &&string.IsNullOrEmpty(delivery_no))
                {
                    this.more_deliveryNo_CB.Items.Clear();
                    this.dn_qty_LB.Text = "0";
                    exeRes = controller.queryDnInfoByShipmentId(shipmentId);
                    if(exeRes.Status)
                    {
                        dt = (DataTable)exeRes.Anything;
                        for (int i = 0;i<dt.Rows.Count;++i)
                        {
                            this.more_deliveryNo_CB.Items.Add(dt.Rows[i]["DELIVERY_NO"].ToString());
                        }
                        this.dn_qty_LB.Text = dt.Rows.Count.ToString();
                    }
                }
                return;
            }
            else
            {
                Show_Message(exeRes.Message,0);
                return;
            }
        }

        private void reset_BTN_Click(object sender, EventArgs e)
        {
            this.pps_pallet_TB.Clear();
            this.pps_pick_palletNo_TB.Clear();
            this.MES_palletNO_TB.Clear();
            this.more_deliveryNo_CB.Items.Clear();
            this.more_deliveryNo_CB.Text="";
            this.locationNo_TB.Clear();
            this.cartonNo_TB.Clear();
            this.sscc_code_TB.Clear();
            this.ICTPN_TB.Clear();
            this.detail_shipmentid_TB.Clear();
            this.moreDetail_search_DGV.DataSource = null;
            this.information_DGV.DataSource = null;
            this.carton_detail_DGV.DataSource = null;
            this.dn_qty_LB.Text = "0";
        }

        private void query_for_shipmentId_BTN_Click(object sender, EventArgs e)
        {
            this.information_DGV.DataSource = null;
            this.information_DGV.Visible = true;
            this.moreDetail_search_DGV.Visible = false;
            string startTime =    this.start_DTP.Value.ToString("yyyyMMdd");
            string endTime =      this.end_DTP.Value.ToString("yyyyMMdd");
            string shipMentId =   this.shipmentId_TB.Text.ToUpper().Trim();
            string shipmemtType = this.shipMentType_CB.Text.ToUpper().Trim();
            string region =       this.region_CB.Text.ToUpper().Trim();
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            exeRes = controller.queryShipmentIdInfoByShipmentTime(startTime, endTime, shipMentId, shipmemtType, region);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                this.information_DGV.DataSource = dt;
                Show_Message(exeRes.Message, 1);
            }
            else
            {
                //this.information_DGV.Visible = false;
                Show_Message(exeRes.Message, 0);
                return;
            }
        }



        private void reset_for_shipmentId_BTN_Click(object sender, EventArgs e)
        {
            this.shipmentId_TB.Clear();
            this.shipmentId_TB.Focus();
            this.shipmentId_TB.SelectAll();
            this.shipMentType_CB.SelectedIndex = -1;
            this.region_CB.SelectedIndex = -1;
            this.moreDetail_search_DGV.DataSource = null;
            this.information_DGV.DataSource = null;
            this.carton_detail_DGV.DataSource = null;
        }

        private void Import_for_shipmentId_BTN_Click(object sender, EventArgs e)
        {
            if (information_DGV.DataSource != null)
            {
                saveFile_SFD.Filter = "excel|*.xls|excel|*.xlsx";
                saveFile_SFD.FileName = "Report查询_" + DateTime.Now.ToString("yyyyMMddhhmmss");
                saveFile_SFD.AddExtension = true;
                if (saveFile_SFD.ShowDialog() == DialogResult.OK)
                {
                    string saveFileName = createExcel.DataToExcel((DataTable)information_DGV.DataSource, saveFile_SFD.FileName);
                    Show_Message("保存文件成功-" + saveFileName, 1);
                }
            }
        }



        private void moreDetail_search_DGV_DoubleClick(object sender, EventArgs e)
        {
            if (this.moreDetail_search_DGV.DataSource != null)
            {
                string cartonNo = this.moreDetail_search_DGV.Rows[moreDetail_search_DGV.CurrentRow.Index].Cells[4].Value.ToString();
                ShowPassStationLog passLog = new ShowPassStationLog(cartonNo);
                passLog.Show();
            }
        }

        private void information_DGV_DoubleClick(object sender, EventArgs e)
        {
            this.carton_detail_DGV.DataSource = null;
            string shipmentId = information_DGV.Rows[information_DGV.CurrentRow.Index].Cells[0].Value.ToString();
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            exeRes = controller.queryQAholdCartonsByShipmentId(shipmentId);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                carton_detail_DGV.DataSource = dt;
            }
        }
    }
}
