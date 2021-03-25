using ClientUtilsDll;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SajetClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PackLogic
{
    public partial class fMain : Form
    {
        /// <summary>
        /// SQL语句类
        /// </summary>
        private CommonSQL common = new CommonSQL();
        /// <summary>
        /// 栈板数据集
        /// </summary>
        private DataTable dtTempPallet = null;
        /// <summary>
        /// DN数据集
        /// </summary>
        private DataTable dtTempDN = null;

        public fMain()
        {
            InitializeComponent();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " (" + SajetCommon.g_sFileVersion + ")";
            labShow.Text = labShow.Text + " (" + SajetCommon.g_sFileVersion + ")";
            this.WindowState = FormWindowState.Maximized;

            this.dt_Shipment.Value = DateTime.Now;
            InitData();
            QueryShipmentData(this.dt_Shipment.Value.ToString("yyyy-MM-dd"));
        }

        private void InitData()
        {
            dtTempPallet = new DataTable();
            dtTempPallet.Columns.Add("PalletNo");
            dtTempPallet.Columns.Add("ShippingLabel");
            dtTempPallet.Columns.Add("GS1Label");
            dtTempPallet.Columns.Add("UUI");

            dtTempDN = new DataTable();
            dtTempDN.Columns.Add("DeliveryNo");
            dtTempDN.Columns.Add("ComsumerPackingList");
            dtTempDN.Columns.Add("ChannelPackingList");
            dtTempDN.Columns.Add("DeliveryNote");
        }

        private void QueryShipmentData(string strShipmentTime)
        {
            this.cmbSmid.Items.Clear();
            DataTable dtTempShipment = common.GetShipmentInfoByShipTime(strShipmentTime);
            if ((dtTempShipment != null) && (dtTempShipment.Rows.Count > 0))
            {
                foreach (DataRow dr in dtTempShipment.Rows)
                {
                    this.cmbSmid.Items.Add(dr["SHIPMENT_ID"].ToString().Trim());
                }
                this.cmbSmid.SelectedIndex = 0;
            }
            else
            {
                this.cmbSmid.SelectedIndex = -1;
                this.cmbSmid.Text = "";
            }
        }

        private void cmbSmid_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvPallet.DataSource = null;
            dgvDN.DataSource = null;

            this.labType.Text = "N/A";
            this.labFreightForward.Text = "N/A";
            this.labTypeMode.Text = "N/A";
            this.labServiceLevel.Text = "N/A";
            this.labArea.Text = "N/A";
            this.labEntrancePort.Text = "N/A";
            this.labTransport.Text = "N/A";

            ShowMsg("", -1);

           // QueryLabelData(this.cmbSmid.Text.Trim());
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            ShowMsg("", -1);
            this.dgvPallet.DataSource = null;
            this.dgvDN.DataSource = null;
            QueryLabelData(this.cmbSmid.Text.Trim());
            btnSearch.Enabled = true;
        }
        private void cmbSmid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryLabelData(this.cmbSmid.Text.Trim());
            }
        }
        private void dt_Shipment_ValueChanged(object sender, EventArgs e)
        {
            dgvPallet.DataSource = null;
            dgvDN.DataSource = null;

            this.labType.Text = "N/A";
            this.labFreightForward.Text = "N/A";
            this.labTypeMode.Text = "N/A";
            this.labServiceLevel.Text = "N/A";
            this.labArea.Text = "N/A";
            this.labEntrancePort.Text = "N/A";
            this.labTransport.Text = "N/A";
            ShowMsg("", -1);

            QueryShipmentData(this.dt_Shipment.Value.ToString("yyyy-MM-dd"));
        }
        private void QueryLabelData(string strShipmentID)
        {
            if (string.IsNullOrEmpty(strShipmentID))
            {
                ShowMsg("集货单号不能为空", 1);
                return;
            }
            DataTable dtTempLabel = common.GetShipentInfoByShipmentID(strShipmentID);
            if ((dtTempLabel != null) && (dtTempLabel.Rows.Count > 0))
            {
                this.labType.Text = dtTempLabel.Rows[0]["SHIPMENT_TYPE"].ToString().Trim();
                this.labFreightForward.Text = dtTempLabel.Rows[0]["CARRIERSCACCODE"].ToString().Trim();
                this.labTypeMode.Text = dtTempLabel.Rows[0]["TYPE"].ToString().Trim();
                this.labServiceLevel.Text = dtTempLabel.Rows[0]["SERVICE_LEVEL"].ToString().Trim();
                this.labArea.Text = dtTempLabel.Rows[0]["REGION"].ToString().Trim();
                this.labEntrancePort.Text = dtTempLabel.Rows[0]["POE"].ToString().Trim();
                this.labTransport.Text = dtTempLabel.Rows[0]["TRANSPORT"].ToString().Trim();

                dtTempPallet.Rows.Clear();
                dtTempDN.Rows.Clear();
                DataRow[] drPallets = null;
                DataRow[] drDNs = null;

                try
                {
                    foreach (DataRow dr in dtTempLabel.Rows)
                    {
                        drPallets = dtTempPallet.Select(string.Format("PalletNo='{0}'", dr["PALLET_NO"].ToString().Trim()));
                        if ((drPallets == null) || (drPallets.Length <= 0))
                        {
                            ShipmentInfo shipmentinfoPT = new ShipmentInfo()
                            {
                                ShipmentID = dr["SHIPMENT_ID"].ToString().Trim(),
                                PalletNo = dr["PALLET_NO"].ToString().Trim(),
                                ShipmentType = dr["SHIPMENT_TYPE"].ToString().Trim().ToUpper(),
                                Region = dr["REGION"].ToString().Trim().ToUpper(),
                                Type = dr["TYPE"].ToString().Trim().ToUpper(),
                                ICTPN = dr["ICTPN"].ToString().Trim(),
                                IsMix = dr["PALLETTYPE"].ToString().Trim() == "MIX" ? true : false,
                                DeliveryNo = dr["DELIVERY_NO"].ToString().Trim(),
                                LineItem = dr["LINE_ITEM"].ToString().Trim(),
                                ShipPlant = dr["SHIPPLANT"].ToString().Trim().ToUpper(),
                                DSGS1Flag = dr["GS1FLAG"].ToString().Trim(),
                                PackCode = dr["PACK_CODE"].ToString().Trim()
                            };
                            GetPalletLabelStatus(shipmentinfoPT);
                            dtTempPallet.Rows.Add(shipmentinfoPT.PalletNo, shipmentinfoPT.ShippingLabelType, shipmentinfoPT.GS1Label, shipmentinfoPT.UUI);
                        }
                        drDNs = dtTempDN.Select(string.Format("DeliveryNo='{0}'", dr["DELIVERY_NO"].ToString().Trim()));
                        if ((drDNs == null) || (drDNs.Length <= 0))
                        {
                            ShipmentInfo shipmentinfoDN = new ShipmentInfo()
                            {
                                ShipmentID = dr["SHIPMENT_ID"].ToString().Trim(),
                                PalletNo = dr["PALLET_NO"].ToString().Trim(),
                                ShipmentType = dr["SHIPMENT_TYPE"].ToString().Trim().ToUpper(),
                                Region = dr["REGION"].ToString().Trim().ToUpper(),
                                Type = dr["TYPE"].ToString().Trim().ToUpper(),
                                ICTPN = dr["ICTPN"].ToString().Trim(),
                                IsMix = dr["PALLETTYPE"].ToString().Trim() == "MIX" ? true : false,
                                DeliveryNo = dr["DELIVERY_NO"].ToString().Trim(),
                                LineItem = dr["LINE_ITEM"].ToString().Trim(),
                                ShipPlant = dr["SHIPPLANT"].ToString().Trim(),
                                DSGS1Flag = dr["GS1FLAG"].ToString().Trim(),
                                PackCode = dr["PACK_CODE"].ToString().Trim()
                            };
                            GetDNLabelStatus(shipmentinfoDN);
                            dtTempDN.Rows.Add(shipmentinfoDN.DeliveryNo, shipmentinfoDN.ComsumerPackingList, shipmentinfoDN.ChannelPackingList, shipmentinfoDN.DeliveryNote);
                        }
                    }

                    this.dgvPallet.DataSource = this.dtTempPallet;
                    this.dgvDN.DataSource = this.dtTempDN;
                }
                catch (Exception ex)
                {
                    ShowMsg(ex.Message, 0);
                }
            }
            else
            {
                ShowMsg("查询无资料", 0);
            }
        }


        private void GetPalletLabelStatus(ShipmentInfo shipmentinfo)
        {
            shipmentinfo.ShippingLabelType = "None";
            shipmentinfo.GS1Label = "No";
            shipmentinfo.UUI = "No";

            DataTable dtTempGS1 = common.GetICTCountByPalletNo(shipmentinfo.PalletNo);
            string gs1Count = "0";
            if ((dtTempGS1 != null) && (dtTempGS1.Rows.Count > 0))
            {
                gs1Count = dtTempGS1.Rows[0]["ICTPNCOUNT"].ToString().Trim();
            }

            if (shipmentinfo.ShipmentType == "FD")
            {
                if (shipmentinfo.IsMix)
                {
                    shipmentinfo.ShippingLabelType = "Carton";
                }
                else
                {
                    shipmentinfo.ShippingLabelType = "Pallet";
                }
                if ((gs1Count == "1") && (!shipmentinfo.IsMix))
                {
                    shipmentinfo.GS1Label = "Yes";
                }
                else
                {
                    shipmentinfo.GS1Label = "No";
                }
            }
            else
            {
                if (shipmentinfo.Type == "PARCEL")
                {
                    shipmentinfo.ShippingLabelType = "None";
                }
                else
                {
                    if (shipmentinfo.Region == "PAC")
                    {
                        DataTable dtTempPACShippingLabel = common.GetPrintPACShippingLabel(shipmentinfo.ShipmentID, shipmentinfo.Type);
                        if ((dtTempPACShippingLabel != null) && (dtTempPACShippingLabel.Rows.Count > 0))
                        {
                            if (shipmentinfo.IsMix)
                            {
                                shipmentinfo.ShippingLabelType = "Carton";
                            }
                            else
                            {
                                shipmentinfo.ShippingLabelType = "Pallet";
                            }
                        }
                        else
                        {
                            shipmentinfo.ShippingLabelType = "None";
                        }
                    }
                    else
                    {
                        if (shipmentinfo.Region == "EMEIA")
                        {
                            if (shipmentinfo.ShipPlant.Contains("MIT"))
                            {
                                DataTable dtTempPackCode = common.GetICTUnitCountByPackCode(shipmentinfo.PackCode, shipmentinfo.ICTPN);
                                int unitPack = 0;
                                if ((dtTempPackCode != null) && (dtTempPackCode.Rows.Count > 0))
                                {
                                    unitPack = Convert.ToInt32(dtTempPackCode.Rows[0]["PACKUNIT"].ToString().Trim());
                                }
                                else
                                {
                                    throw new Exception(string.Format("未找到PackCode为:{0} 的包规基础资料,请联系IT-PPS!", shipmentinfo.PackCode).TP());
                                }
                                int cartonCount = 0;
                                DataTable dtPalletCartonCount = common.GetPalletCartonCount(shipmentinfo.PalletNo);
                                if ((dtPalletCartonCount != null) && (dtPalletCartonCount.Rows.Count > 0))
                                {
                                    cartonCount = Convert.ToInt32(dtPalletCartonCount.Rows[0]["CARTON_QTY"].ToString().Trim());
                                }
                                else
                                {
                                    throw new Exception(string.Format("未找到栈板号为:{0} 的资料,请联系IT-PPS!", shipmentinfo.PalletNo).TP());
                                }
                                if (shipmentinfo.IsMix)
                                {
                                    if (unitPack <= 1)
                                    {
                                        shipmentinfo.UUI = "No";
                                    }
                                    else
                                    {
                                        shipmentinfo.UUI = "Yes";
                                    }
                                }
                                else
                                {
                                    if (unitPack <= 1)
                                    {
                                        shipmentinfo.UUI = "No";
                                    }
                                    else
                                    {
                                        if (cartonCount == 1)
                                        {
                                            shipmentinfo.UUI = "Yes";
                                        }
                                        else
                                        {
                                            shipmentinfo.UUI = "No";
                                        }
                                    }
                                }
                            }
                        }
                        if (shipmentinfo.IsMix)
                        {
                            shipmentinfo.ShippingLabelType = "Carton";
                        }
                        else
                        {
                            shipmentinfo.ShippingLabelType = "Pallet";
                        }
                    }
                }

                if ((gs1Count == "1") && (shipmentinfo.DSGS1Flag == "001") && (!shipmentinfo.IsMix))
                {
                    shipmentinfo.GS1Label = "Yes";
                }
                else if ((gs1Count == "1") && (shipmentinfo.DSGS1Flag == "004") && (!shipmentinfo.IsMix))
                {
                    shipmentinfo.GS1Label = "Yes";
                }
                else if ((gs1Count == "1") && (shipmentinfo.DSGS1Flag == "009") && (!shipmentinfo.IsMix))
                {
                    shipmentinfo.GS1Label = "Yes";
                }
                else if ((shipmentinfo.DSGS1Flag == "003") && (!shipmentinfo.IsMix))
                {
                    shipmentinfo.GS1Label = "Yes";
                }
                else
                {
                    shipmentinfo.GS1Label = "No";
                }
            }
        }

        private void GetDNLabelStatus(ShipmentInfo shipmentinfo)
        {
            shipmentinfo.ComsumerPackingList = "No";
            shipmentinfo.ChannelPackingList = "No";
            shipmentinfo.DeliveryNote = "No";

            if (shipmentinfo.ShipmentType == "FD")
            {
                if (shipmentinfo.Region == "AMR")
                {
                    shipmentinfo.ChannelPackingList = "Yes";
                }
                else
                {
                    shipmentinfo.ChannelPackingList = "No";
                }
                shipmentinfo.ComsumerPackingList = "No";
            }
            else
            {
                T940UnicodeInfo t940UnicodeInfo = GetT940UnicodeInfoByDeliveryNoAndLineItem(shipmentinfo.DeliveryNo, shipmentinfo.LineItem);
                if (t940UnicodeInfo == null)
                {
                    shipmentinfo.ComsumerPackingList = "No";
                    shipmentinfo.ChannelPackingList = "No";
                }
                else
                {
                    DataTable dtTempPackList = common.JudgeCrystalReportByCondition(t940UnicodeInfo.Region, t940UnicodeInfo.CustomerGroup, t940UnicodeInfo.MsgFlag, t940UnicodeInfo.GpFlag);
                    if ((dtTempPackList != null) && (dtTempPackList.Rows.Count > 0))
                    {
                        string documentName = dtTempPackList.Rows[0]["documentname"].ToString().Trim();
                        if (documentName.Equals("ConsumerPL"))
                        {
                            shipmentinfo.ComsumerPackingList = "Yes";
                            shipmentinfo.ChannelPackingList = "No";
                        }
                        else
                        {
                            if (t940UnicodeInfo.Region == "PAC")
                            {
                                shipmentinfo.ComsumerPackingList = "No";
                                shipmentinfo.ChannelPackingList = "No";
                            }
                            else
                            {
                                shipmentinfo.ComsumerPackingList = "No";
                                shipmentinfo.ChannelPackingList = "Yes";
                            }
                        }
                    }
                    else
                    {
                        shipmentinfo.ComsumerPackingList = "No";
                        shipmentinfo.ChannelPackingList = "No";
                    }
                }
            }

            if (shipmentinfo.Region == "PAC")
            {
                if (shipmentinfo.Type == "PARCEL")
                {
                    DataTable dtTempT90DeliveryNote = common.GetDeliveryNoteT90Info(shipmentinfo.DeliveryNo);
                    if ((dtTempT90DeliveryNote != null) && (dtTempT90DeliveryNote.Rows.Count > 0))
                    {
                        string shipCnCode = dtTempT90DeliveryNote.Rows[0]["SHIPCNTYCODE"].ToString();
                        string custGroup = dtTempT90DeliveryNote.Rows[0]["CUSTOMERGROUP"].ToString();

                        ///取消Ship To Japan 一定要列印DELIVERY NOTE 逻辑

                        if (shipCnCode.Equals("JP"))
                        {
                            //DataTable dtTempPACDeliverNoteLabel1 = common.GetPrintPACDeliveryNoteLabel(shipmentinfo.ShipmentID, shipmentinfo.Type);
                            //if ((dtTempPACDeliverNoteLabel1 != null) && (dtTempPACDeliverNoteLabel1.Rows.Count > 0))
                            //{
                            //    shipmentinfo.DeliveryNote = "Yes";
                            //}
                            //else
                            //{
                                shipmentinfo.DeliveryNote = "No";
                            //}
                        }
                        else
                        {
                            if (custGroup.Equals("IN") || custGroup.Equals("RK") || custGroup.Equals("RW"))
                            {
                                shipmentinfo.DeliveryNote = "No";
                            }
                            else
                            {
                                DataTable dtTempPACDeliverNoteLabel2 = common.GetPrintPACDeliveryNoteLabel(shipmentinfo.ShipmentID, shipmentinfo.Type);
                                if ((dtTempPACDeliverNoteLabel2 != null) && (dtTempPACDeliverNoteLabel2.Rows.Count > 0))
                                {
                                    shipmentinfo.DeliveryNote = "Yes";
                                }
                                else
                                {
                                    shipmentinfo.DeliveryNote = "No";
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("T940Unicode没有获得资料,请联系IT-PPS!".TP());
                    }
                }
                else
                {
                    DataTable dtTempPACDeliverNoteLabel3 = common.GetPrintPACDeliveryNoteLabel(shipmentinfo.ShipmentID, shipmentinfo.Type);
                    if ((dtTempPACDeliverNoteLabel3 != null) && (dtTempPACDeliverNoteLabel3.Rows.Count > 0))
                    {
                        shipmentinfo.DeliveryNote = "Yes";
                    }
                    else
                    {
                        shipmentinfo.DeliveryNote = "No";
                    }
                }
            }
            else
            {
                shipmentinfo.DeliveryNote = "No";
            }
        }

        private T940UnicodeInfo GetT940UnicodeInfoByDeliveryNoAndLineItem(string DeliveryNo, string LineItem)
        {
            DataTable dtT90UniCode = common.GetT940UnicodeInfoByDeliveryNoAndLineItem(DeliveryNo);
            if ((dtT90UniCode != null) && (dtT90UniCode.Rows.Count > 0))
            {
                string customerGroup_D = dtT90UniCode.Rows[0]["customergroup"].ToString().Trim();
                if (!(customerGroup_D.Equals("RK") || customerGroup_D.Equals("RW") || customerGroup_D.Equals("IN") || customerGroup_D.Equals("RR")))
                {
                    customerGroup_D = "OTHER";
                }
                string t9uMsgFlag = "N";
                string t9uGbFlag = "N";
                for (int i = 0; i < dtT90UniCode.Rows.Count; i++)
                {
                    if (!dtT90UniCode.Rows[i]["msgflag"].ToString().Trim().Equals(t9uMsgFlag))
                    {
                        t9uMsgFlag = dtT90UniCode.Rows[i]["msgflag"].ToString().Trim();
                        break;
                    }
                }
                for (int i = 0; i < dtT90UniCode.Rows.Count; i++)
                {
                    if (!dtT90UniCode.Rows[i]["gpflag"].ToString().Trim().Equals(t9uGbFlag))
                    {
                        t9uGbFlag = dtT90UniCode.Rows[i]["gpflag"].ToString().Trim();
                        break;
                    }
                }
                return new T940UnicodeInfo
                {
                    Region = dtT90UniCode.Rows[0]["region"].ToString().Trim(),
                    MsgFlag = t9uMsgFlag,
                    GpFlag = t9uGbFlag,
                    CustomerGroup = customerGroup_D,
                    DeliveryNo = DeliveryNo,
                    LineItem = LineItem,
                    ShipCntyCode = dtT90UniCode.Rows[0]["shipcntycode"].ToString().Trim()
                };
            }
            else
            {
                throw new Exception("T940Unicode没有获得资料,请联系IT-PPS!".TP());
            }
        }



        public DialogResult ShowMsg(string strTxt, int strType)
        {
            TextMsg.Text = strTxt.TP();
            switch (strType)
            {
                case 0: //Error    
                    TextMsg.ForeColor = Color.Red;
                    TextMsg.BackColor = Color.Blue;
                    return DialogResult.None;
                case 1: //Warning                        
                    TextMsg.ForeColor = Color.Blue;
                    TextMsg.BackColor = Color.FromArgb(255, 255, 128);
                    return DialogResult.None;
                case 2: //Confirm
                    return MessageBox.Show(strTxt, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    TextMsg.ForeColor = Color.White;
                    TextMsg.BackColor = Color.Blue;
                    return DialogResult.None;
            }
        }

        private void btnPalletExport_Click(object sender, EventArgs e)
        {
            //导出Excel文件
            try
            {
                if (dgvPallet.Rows.Count > 0)
                {
                    ExportExcel(dgvPallet);
                }
                else
                {
                    this.ShowMsg("确认导出Excel有数据！", 0);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.TP());
            }
        }

        private void btnDNExport_Click(object sender, EventArgs e)
        {
            //导出Excel文件
            try
            {
                if (dgvDN.Rows.Count > 0)
                {
                    ExportExcel(dgvDN);
                }
                else
                {
                    this.ShowMsg("确认导出Excel有数据！", 0);
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.TP());
            }
        }

        public void ExportExcel(DataGridView dt)
        {
            //获取导出路径
            string filePath = string.Empty;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "EXCEL 97-2007 工作簿(*.xls)|*.xls";//设置文件类型

            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            string currdate = currentTime.ToString("yyyy-MM-dd-HH-mm-ss");
            //HH是24小时制,hh是12小时制

            //sfd.FileName = "wmsReport"+cmbTYPE.Text.Trim()+"_"+cmbLocation.Text.Trim()+"_"+ currdate;//设置默认文件名

            sfd.FileName = "_" + currdate;
            sfd.DefaultExt = "xlsx";//设置默认格式（可以不设）
            sfd.AddExtension = true;//设置自动在文件名中添加扩展名
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filePath = sfd.FileName;
            }
            else
            {
                this.ShowMsg("导出Excel失败！", 0);
            }

            IWorkbook workbook;
            string fileExt = Path.GetExtension(filePath).ToLower();
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                workbook = null;
            }
            if (workbook == null)
            {
                return;
            }
            ISheet sheet = string.IsNullOrEmpty("wmsReport") ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet("wmsReport");

            //表头  
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].HeaderText);
            }

            //数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    if (dt.Rows[i].Cells[j].Value != null)
                    {
                        cell.SetCellValue(dt.Rows[i].Cells[j].Value.ToString());
                    }
                    else
                    {
                        cell.SetCellValue("");
                    }

                }
            }

            //转为字节数组  
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();

            //保存为Excel文件  
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
                this.ShowMsg("导出Excel成功！", 5);
            }
        }

    }
}
