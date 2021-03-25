using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Packingparcel.Entitys;
using Packingparcel.Core;
using ClientUtilsDll;

namespace Packingparcel
{
    public partial class DHL_Mother : Form
    {
        private Controller controller;
        public DHL_Mother()
        {
            InitializeComponent();
            controller = new Controller();
        }

        private void createDHLMotherFile_BTN_Click(object sender, EventArgs e)
        {
            string shipmentId = this.shipmentId_CB.Text.Trim();
            if (string.IsNullOrEmpty(shipmentId))
            {
                Message_LB.Text = "没有获取到集货单号，不可以生成BBXMotherFile！".TL();
                Message_LB.ForeColor = Color.Red;
                return;
            }
            ExecuteResult exeRes = new ExecuteResult();
            exeRes = controller.getShipmentInfoOfRegionByshipmentId(shipmentId);
            if (exeRes.Status)
            {
                string region = (string)exeRes.Anything;
                exeRes =   controller.createBBX_Mother_File(shipmentId,region);
                if (exeRes.Status)
                {
                    Message_LB.Text = "BBXMotherFile  文件创建成功！".TL();
                }
                else
                {
                    Message_LB.Text = exeRes.Message;
                    Message_LB.ForeColor = Color.Red;
                    return;
                }
            }
            else
            {
                Message_LB.Text =exeRes.Message;
                Message_LB.ForeColor = Color.Red;
            }
        }

        private void shipTime_DTP_ValueChanged(object sender, EventArgs e)
        {
            this.shipmentId_CB.DataSource = null;
            DateTime  shippingTime =   shipTime_DTP.Value;
            ExecuteResult exeRes   =   new ExecuteResult();
            List<string> shipmentIds = new List<string>();
            exeRes =   controller.getDHL_MotherFileShipmentIdsByShipmentTime(shippingTime.ToString("yyyyMMdd"));
            if (exeRes.Status)
            {
                shipmentIds = (List<string>)exeRes.Anything;
                this.shipmentId_CB.DataSource = shipmentIds;
            }
            else
            {
                Message_LB.Text = exeRes.Message.TL();
                Message_LB.ForeColor = Color.Red;             
            }

        }

        private void shipmentId_CB_DropDown(object sender, EventArgs e)
        {

        }
    }
}
