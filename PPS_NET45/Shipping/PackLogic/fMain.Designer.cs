namespace PackLogic
{
    partial class fMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbDN = new System.Windows.Forms.GroupBox();
            this.dgvDN = new System.Windows.Forms.DataGridView();
            this.DeliveryNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComsumerPackingList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChannelPackingList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeliveryNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbPallet = new System.Windows.Forms.GroupBox();
            this.dgvPallet = new System.Windows.Forms.DataGridView();
            this.PalletNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShippingLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GS1Label = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UUI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbSelect = new System.Windows.Forms.GroupBox();
            this.btnDNExport = new System.Windows.Forms.Button();
            this.btnPalletExport = new System.Windows.Forms.Button();
            this.labServiceLevel = new System.Windows.Forms.Label();
            this.labServiceLevelInfo = new System.Windows.Forms.Label();
            this.labTransport = new System.Windows.Forms.Label();
            this.labTransportInfo = new System.Windows.Forms.Label();
            this.labTypeMode = new System.Windows.Forms.Label();
            this.labTypeModeInfo = new System.Windows.Forms.Label();
            this.labEntrancePort = new System.Windows.Forms.Label();
            this.labFreightForward = new System.Windows.Forms.Label();
            this.labArea = new System.Windows.Forms.Label();
            this.labType = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.labTypeDesc = new System.Windows.Forms.Label();
            this.cmbSmid = new System.Windows.Forms.ComboBox();
            this.dt_Shipment = new System.Windows.Forms.DateTimePicker();
            this.labAreaInfo = new System.Windows.Forms.Label();
            this.labFreightForwardInfo = new System.Windows.Forms.Label();
            this.lblDtShipmentInfo = new System.Windows.Forms.Label();
            this.labShipmentID = new System.Windows.Forms.Label();
            this.labEntrancePortInfo = new System.Windows.Forms.Label();
            this.labShow = new System.Windows.Forms.TextBox();
            this.TextMsg = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.gbDN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDN)).BeginInit();
            this.gbPallet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPallet)).BeginInit();
            this.gbSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.gbDN);
            this.panel1.Controls.Add(this.gbPallet);
            this.panel1.Controls.Add(this.gbSelect);
            this.panel1.Controls.Add(this.labShow);
            this.panel1.Controls.Add(this.TextMsg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1475, 693);
            this.panel1.TabIndex = 2;
            // 
            // gbDN
            // 
            this.gbDN.Controls.Add(this.dgvDN);
            this.gbDN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDN.Font = new System.Drawing.Font("NSimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbDN.Location = new System.Drawing.Point(0, 408);
            this.gbDN.Name = "gbDN";
            this.gbDN.Size = new System.Drawing.Size(1475, 221);
            this.gbDN.TabIndex = 126;
            this.gbDN.TabStop = false;
            this.gbDN.Text = "Delivery Level";
            // 
            // dgvDN
            // 
            this.dgvDN.AllowUserToAddRows = false;
            this.dgvDN.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvDN.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDN.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDN.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvDN.ColumnHeadersHeight = 34;
            this.dgvDN.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DeliveryNo,
            this.ComsumerPackingList,
            this.ChannelPackingList,
            this.DeliveryNote});
            this.dgvDN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDN.Location = new System.Drawing.Point(3, 29);
            this.dgvDN.Margin = new System.Windows.Forms.Padding(2);
            this.dgvDN.MultiSelect = false;
            this.dgvDN.Name = "dgvDN";
            this.dgvDN.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("NSimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDN.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDN.RowHeadersWidth = 50;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.dgvDN.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDN.RowTemplate.Height = 27;
            this.dgvDN.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDN.Size = new System.Drawing.Size(1469, 189);
            this.dgvDN.TabIndex = 98;
            // 
            // DeliveryNo
            // 
            this.DeliveryNo.DataPropertyName = "DeliveryNo";
            this.DeliveryNo.HeaderText = "DeliveryNo";
            this.DeliveryNo.MinimumWidth = 8;
            this.DeliveryNo.Name = "DeliveryNo";
            this.DeliveryNo.ReadOnly = true;
            this.DeliveryNo.Width = 166;
            // 
            // ComsumerPackingList
            // 
            this.ComsumerPackingList.DataPropertyName = "ComsumerPackingList";
            this.ComsumerPackingList.HeaderText = "Comsumer Packing List";
            this.ComsumerPackingList.MinimumWidth = 8;
            this.ComsumerPackingList.Name = "ComsumerPackingList";
            this.ComsumerPackingList.ReadOnly = true;
            this.ComsumerPackingList.Width = 298;
            // 
            // ChannelPackingList
            // 
            this.ChannelPackingList.DataPropertyName = "ChannelPackingList";
            this.ChannelPackingList.HeaderText = "Channel Packing List";
            this.ChannelPackingList.MinimumWidth = 8;
            this.ChannelPackingList.Name = "ChannelPackingList";
            this.ChannelPackingList.ReadOnly = true;
            this.ChannelPackingList.Width = 286;
            // 
            // DeliveryNote
            // 
            this.DeliveryNote.DataPropertyName = "DeliveryNote";
            this.DeliveryNote.HeaderText = "DeliveryNote";
            this.DeliveryNote.MinimumWidth = 8;
            this.DeliveryNote.Name = "DeliveryNote";
            this.DeliveryNote.ReadOnly = true;
            this.DeliveryNote.Width = 190;
            // 
            // gbPallet
            // 
            this.gbPallet.Controls.Add(this.dgvPallet);
            this.gbPallet.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbPallet.Font = new System.Drawing.Font("NSimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbPallet.Location = new System.Drawing.Point(0, 219);
            this.gbPallet.Name = "gbPallet";
            this.gbPallet.Size = new System.Drawing.Size(1475, 189);
            this.gbPallet.TabIndex = 125;
            this.gbPallet.TabStop = false;
            this.gbPallet.Text = "Pallet Level";
            // 
            // dgvPallet
            // 
            this.dgvPallet.AllowUserToAddRows = false;
            this.dgvPallet.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvPallet.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPallet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPallet.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvPallet.ColumnHeadersHeight = 34;
            this.dgvPallet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PalletNo,
            this.ShippingLabel,
            this.GS1Label,
            this.UUI});
            this.dgvPallet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPallet.Location = new System.Drawing.Point(3, 29);
            this.dgvPallet.Margin = new System.Windows.Forms.Padding(2);
            this.dgvPallet.MultiSelect = false;
            this.dgvPallet.Name = "dgvPallet";
            this.dgvPallet.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("NSimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPallet.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvPallet.RowHeadersWidth = 50;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            this.dgvPallet.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvPallet.RowTemplate.Height = 27;
            this.dgvPallet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPallet.Size = new System.Drawing.Size(1469, 157);
            this.dgvPallet.TabIndex = 98;
            // 
            // PalletNo
            // 
            this.PalletNo.DataPropertyName = "PalletNo";
            this.PalletNo.HeaderText = "PalletNo";
            this.PalletNo.MinimumWidth = 8;
            this.PalletNo.Name = "PalletNo";
            this.PalletNo.ReadOnly = true;
            this.PalletNo.Width = 142;
            // 
            // ShippingLabel
            // 
            this.ShippingLabel.DataPropertyName = "ShippingLabel";
            this.ShippingLabel.HeaderText = "Shipping Label";
            this.ShippingLabel.MinimumWidth = 8;
            this.ShippingLabel.Name = "ShippingLabel";
            this.ShippingLabel.ReadOnly = true;
            this.ShippingLabel.Width = 214;
            // 
            // GS1Label
            // 
            this.GS1Label.DataPropertyName = "GS1Label";
            this.GS1Label.HeaderText = "GS1 Label";
            this.GS1Label.MinimumWidth = 8;
            this.GS1Label.Name = "GS1Label";
            this.GS1Label.ReadOnly = true;
            this.GS1Label.Width = 154;
            // 
            // UUI
            // 
            this.UUI.DataPropertyName = "UUI";
            this.UUI.HeaderText = "UUI";
            this.UUI.MinimumWidth = 8;
            this.UUI.Name = "UUI";
            this.UUI.ReadOnly = true;
            this.UUI.Width = 82;
            // 
            // gbSelect
            // 
            this.gbSelect.Controls.Add(this.btnDNExport);
            this.gbSelect.Controls.Add(this.btnPalletExport);
            this.gbSelect.Controls.Add(this.labServiceLevel);
            this.gbSelect.Controls.Add(this.labServiceLevelInfo);
            this.gbSelect.Controls.Add(this.labTransport);
            this.gbSelect.Controls.Add(this.labTransportInfo);
            this.gbSelect.Controls.Add(this.labTypeMode);
            this.gbSelect.Controls.Add(this.labTypeModeInfo);
            this.gbSelect.Controls.Add(this.labEntrancePort);
            this.gbSelect.Controls.Add(this.labFreightForward);
            this.gbSelect.Controls.Add(this.labArea);
            this.gbSelect.Controls.Add(this.labType);
            this.gbSelect.Controls.Add(this.btnSearch);
            this.gbSelect.Controls.Add(this.labTypeDesc);
            this.gbSelect.Controls.Add(this.cmbSmid);
            this.gbSelect.Controls.Add(this.dt_Shipment);
            this.gbSelect.Controls.Add(this.labAreaInfo);
            this.gbSelect.Controls.Add(this.labFreightForwardInfo);
            this.gbSelect.Controls.Add(this.lblDtShipmentInfo);
            this.gbSelect.Controls.Add(this.labShipmentID);
            this.gbSelect.Controls.Add(this.labEntrancePortInfo);
            this.gbSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbSelect.Location = new System.Drawing.Point(0, 57);
            this.gbSelect.Name = "gbSelect";
            this.gbSelect.Size = new System.Drawing.Size(1475, 162);
            this.gbSelect.TabIndex = 124;
            this.gbSelect.TabStop = false;
            this.gbSelect.Text = "筛选";
            // 
            // btnDNExport
            // 
            this.btnDNExport.Location = new System.Drawing.Point(1296, 91);
            this.btnDNExport.Margin = new System.Windows.Forms.Padding(4);
            this.btnDNExport.Name = "btnDNExport";
            this.btnDNExport.Size = new System.Drawing.Size(148, 40);
            this.btnDNExport.TabIndex = 119;
            this.btnDNExport.Text = "DN资料导出";
            this.btnDNExport.UseVisualStyleBackColor = true;
            this.btnDNExport.Click += new System.EventHandler(this.btnDNExport_Click);
            // 
            // btnPalletExport
            // 
            this.btnPalletExport.Location = new System.Drawing.Point(1296, 40);
            this.btnPalletExport.Margin = new System.Windows.Forms.Padding(4);
            this.btnPalletExport.Name = "btnPalletExport";
            this.btnPalletExport.Size = new System.Drawing.Size(148, 40);
            this.btnPalletExport.TabIndex = 118;
            this.btnPalletExport.Text = "栈板资料导出";
            this.btnPalletExport.UseVisualStyleBackColor = true;
            this.btnPalletExport.Click += new System.EventHandler(this.btnPalletExport_Click);
            // 
            // labServiceLevel
            // 
            this.labServiceLevel.AutoSize = true;
            this.labServiceLevel.Location = new System.Drawing.Point(1128, 47);
            this.labServiceLevel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labServiceLevel.Name = "labServiceLevel";
            this.labServiceLevel.Size = new System.Drawing.Size(35, 18);
            this.labServiceLevel.TabIndex = 117;
            this.labServiceLevel.Text = "N/A";
            // 
            // labServiceLevelInfo
            // 
            this.labServiceLevelInfo.AutoSize = true;
            this.labServiceLevelInfo.Location = new System.Drawing.Point(1031, 47);
            this.labServiceLevelInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labServiceLevelInfo.Name = "labServiceLevelInfo";
            this.labServiceLevelInfo.Size = new System.Drawing.Size(89, 18);
            this.labServiceLevelInfo.TabIndex = 116;
            this.labServiceLevelInfo.Text = "服务等级:";
            // 
            // labTransport
            // 
            this.labTransport.AutoSize = true;
            this.labTransport.Location = new System.Drawing.Point(915, 111);
            this.labTransport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labTransport.Name = "labTransport";
            this.labTransport.Size = new System.Drawing.Size(35, 18);
            this.labTransport.TabIndex = 115;
            this.labTransport.Text = "N/A";
            // 
            // labTransportInfo
            // 
            this.labTransportInfo.AutoSize = true;
            this.labTransportInfo.Location = new System.Drawing.Point(797, 111);
            this.labTransportInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labTransportInfo.Name = "labTransportInfo";
            this.labTransportInfo.Size = new System.Drawing.Size(89, 18);
            this.labTransportInfo.TabIndex = 114;
            this.labTransportInfo.Text = "运输方式:";
            // 
            // labTypeMode
            // 
            this.labTypeMode.AutoSize = true;
            this.labTypeMode.Location = new System.Drawing.Point(915, 47);
            this.labTypeMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labTypeMode.Name = "labTypeMode";
            this.labTypeMode.Size = new System.Drawing.Size(35, 18);
            this.labTypeMode.TabIndex = 113;
            this.labTypeMode.Text = "N/A";
            // 
            // labTypeModeInfo
            // 
            this.labTypeModeInfo.AutoSize = true;
            this.labTypeModeInfo.Location = new System.Drawing.Point(797, 47);
            this.labTypeModeInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labTypeModeInfo.Name = "labTypeModeInfo";
            this.labTypeModeInfo.Size = new System.Drawing.Size(89, 18);
            this.labTypeModeInfo.TabIndex = 112;
            this.labTypeModeInfo.Text = "出货模式:";
            // 
            // labEntrancePort
            // 
            this.labEntrancePort.AutoSize = true;
            this.labEntrancePort.Location = new System.Drawing.Point(687, 111);
            this.labEntrancePort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labEntrancePort.Name = "labEntrancePort";
            this.labEntrancePort.Size = new System.Drawing.Size(35, 18);
            this.labEntrancePort.TabIndex = 111;
            this.labEntrancePort.Text = "N/A";
            // 
            // labFreightForward
            // 
            this.labFreightForward.AutoSize = true;
            this.labFreightForward.Location = new System.Drawing.Point(687, 47);
            this.labFreightForward.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labFreightForward.Name = "labFreightForward";
            this.labFreightForward.Size = new System.Drawing.Size(35, 18);
            this.labFreightForward.TabIndex = 110;
            this.labFreightForward.Text = "N/A";
            // 
            // labArea
            // 
            this.labArea.AutoSize = true;
            this.labArea.Location = new System.Drawing.Point(512, 111);
            this.labArea.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labArea.Name = "labArea";
            this.labArea.Size = new System.Drawing.Size(35, 18);
            this.labArea.TabIndex = 109;
            this.labArea.Text = "N/A";
            // 
            // labType
            // 
            this.labType.AutoSize = true;
            this.labType.Location = new System.Drawing.Point(512, 47);
            this.labType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(35, 18);
            this.labType.TabIndex = 108;
            this.labType.Text = "N/A";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1033, 91);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(112, 40);
            this.btnSearch.TabIndex = 82;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labTypeDesc
            // 
            this.labTypeDesc.AutoSize = true;
            this.labTypeDesc.Location = new System.Drawing.Point(407, 47);
            this.labTypeDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labTypeDesc.Name = "labTypeDesc";
            this.labTypeDesc.Size = new System.Drawing.Size(89, 18);
            this.labTypeDesc.TabIndex = 107;
            this.labTypeDesc.Text = "出货类型:";
            // 
            // cmbSmid
            // 
            this.cmbSmid.FormattingEnabled = true;
            this.cmbSmid.Location = new System.Drawing.Point(166, 107);
            this.cmbSmid.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSmid.Name = "cmbSmid";
            this.cmbSmid.Size = new System.Drawing.Size(211, 26);
            this.cmbSmid.TabIndex = 104;
            this.cmbSmid.SelectedIndexChanged += new System.EventHandler(this.cmbSmid_SelectedIndexChanged);
            this.cmbSmid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSmid_KeyDown);
            // 
            // dt_Shipment
            // 
            this.dt_Shipment.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_Shipment.CustomFormat = "yyyy-MM-dd";
            this.dt_Shipment.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_Shipment.Location = new System.Drawing.Point(166, 37);
            this.dt_Shipment.Margin = new System.Windows.Forms.Padding(4);
            this.dt_Shipment.Name = "dt_Shipment";
            this.dt_Shipment.Size = new System.Drawing.Size(211, 28);
            this.dt_Shipment.TabIndex = 93;
            this.dt_Shipment.ValueChanged += new System.EventHandler(this.dt_Shipment_ValueChanged);
            // 
            // labAreaInfo
            // 
            this.labAreaInfo.AutoSize = true;
            this.labAreaInfo.Location = new System.Drawing.Point(407, 111);
            this.labAreaInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labAreaInfo.Name = "labAreaInfo";
            this.labAreaInfo.Size = new System.Drawing.Size(89, 18);
            this.labAreaInfo.TabIndex = 1;
            this.labAreaInfo.Text = "出货区域:";
            // 
            // labFreightForwardInfo
            // 
            this.labFreightForwardInfo.AutoSize = true;
            this.labFreightForwardInfo.Location = new System.Drawing.Point(609, 47);
            this.labFreightForwardInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labFreightForwardInfo.Name = "labFreightForwardInfo";
            this.labFreightForwardInfo.Size = new System.Drawing.Size(53, 18);
            this.labFreightForwardInfo.TabIndex = 3;
            this.labFreightForwardInfo.Text = "货代:";
            // 
            // lblDtShipmentInfo
            // 
            this.lblDtShipmentInfo.AutoSize = true;
            this.lblDtShipmentInfo.Location = new System.Drawing.Point(12, 42);
            this.lblDtShipmentInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDtShipmentInfo.Name = "lblDtShipmentInfo";
            this.lblDtShipmentInfo.Size = new System.Drawing.Size(89, 18);
            this.lblDtShipmentInfo.TabIndex = 9;
            this.lblDtShipmentInfo.Text = "出货日期:";
            // 
            // labShipmentID
            // 
            this.labShipmentID.AutoSize = true;
            this.labShipmentID.Location = new System.Drawing.Point(12, 111);
            this.labShipmentID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labShipmentID.Name = "labShipmentID";
            this.labShipmentID.Size = new System.Drawing.Size(89, 18);
            this.labShipmentID.TabIndex = 13;
            this.labShipmentID.Text = "集货单号:";
            // 
            // labEntrancePortInfo
            // 
            this.labEntrancePortInfo.AutoSize = true;
            this.labEntrancePortInfo.Location = new System.Drawing.Point(609, 111);
            this.labEntrancePortInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labEntrancePortInfo.Name = "labEntrancePortInfo";
            this.labEntrancePortInfo.Size = new System.Drawing.Size(53, 18);
            this.labEntrancePortInfo.TabIndex = 7;
            this.labEntrancePortInfo.Text = "港口:";
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Font = new System.Drawing.Font("SimSun", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.Color.White;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Margin = new System.Windows.Forms.Padding(4);
            this.labShow.Name = "labShow";
            this.labShow.ReadOnly = true;
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(1475, 57);
            this.labShow.TabIndex = 64;
            this.labShow.Text = "PackLogic";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TextMsg
            // 
            this.TextMsg.AutoEllipsis = true;
            this.TextMsg.BackColor = System.Drawing.Color.Blue;
            this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 629);
            this.TextMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1475, 64);
            this.TextMsg.TabIndex = 63;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1475, 693);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "fMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.fMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbDN.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDN)).EndInit();
            this.gbPallet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPallet)).EndInit();
            this.gbSelect.ResumeLayout(false);
            this.gbSelect.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.GroupBox gbSelect;
        private System.Windows.Forms.Label labServiceLevel;
        private System.Windows.Forms.Label labServiceLevelInfo;
        private System.Windows.Forms.Label labTransport;
        private System.Windows.Forms.Label labTransportInfo;
        private System.Windows.Forms.Label labTypeMode;
        private System.Windows.Forms.Label labTypeModeInfo;
        private System.Windows.Forms.Label labEntrancePort;
        private System.Windows.Forms.Label labFreightForward;
        private System.Windows.Forms.Label labArea;
        private System.Windows.Forms.Label labType;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label labTypeDesc;
        private System.Windows.Forms.ComboBox cmbSmid;
        private System.Windows.Forms.DateTimePicker dt_Shipment;
        private System.Windows.Forms.Label labAreaInfo;
        private System.Windows.Forms.Label labFreightForwardInfo;
        private System.Windows.Forms.Label lblDtShipmentInfo;
        private System.Windows.Forms.Label labShipmentID;
        private System.Windows.Forms.Label labEntrancePortInfo;
        private System.Windows.Forms.GroupBox gbDN;
        private System.Windows.Forms.DataGridView dgvDN;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeliveryNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComsumerPackingList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChannelPackingList;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeliveryNote;
        private System.Windows.Forms.GroupBox gbPallet;
        private System.Windows.Forms.DataGridView dgvPallet;
        private System.Windows.Forms.DataGridViewTextBoxColumn PalletNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShippingLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn GS1Label;
        private System.Windows.Forms.DataGridViewTextBoxColumn UUI;
        private System.Windows.Forms.Button btnDNExport;
        private System.Windows.Forms.Button btnPalletExport;
    }
}

