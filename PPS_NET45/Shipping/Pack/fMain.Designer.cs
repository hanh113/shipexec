namespace Packingparcel
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
            this.main_panel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpCartonNo = new System.Windows.Forms.GroupBox();
            this.dgvShowBoxStatus = new System.Windows.Forms.DataGridView();
            this.CartonNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ictPn__ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.lblNeedPackBoxQty = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.grpOrderInfo = new System.Windows.Forms.GroupBox();
            this.dgvOrderInfo = new System.Windows.Forms.DataGridView();
            this.DeliveryNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ictPn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalBoxQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planBoxQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packBoxQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pack_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labFinish = new System.Windows.Forms.Label();
            this.btnPrinter = new System.Windows.Forms.Button();
            this.lstCreateTXT = new System.Windows.Forms.ListBox();
            this.ChkInputCartonNo = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.responsetime_LB = new System.Windows.Forms.Label();
            this.lblDNSumQty = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDeliveryNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCartonno = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.grpShipmentInfo = new System.Windows.Forms.GroupBox();
            this.dgvShipmentInfo = new System.Windows.Forms.DataGridView();
            this.shipmentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Carrier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.POE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.region = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.palletNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.palletType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ictPn_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cartonQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alreadyPickCartonQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alreadyPackCartonQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpPICKPalletInfo = new System.Windows.Forms.GroupBox();
            this.lblSecurity = new System.Windows.Forms.Label();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtShipmentId = new System.Windows.Forms.TextBox();
            this.btnResetStatus = new System.Windows.Forms.Button();
            this.lblShipmentType = new System.Windows.Forms.Label();
            this.btnCreateDHLFile = new System.Windows.Forms.Button();
            this.lblisMix = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReprint = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPOE = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPickPalletNo = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.lblCarrierName = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTitel = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.main_panel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpCartonNo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowBoxStatus)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpOrderInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderInfo)).BeginInit();
            this.panel2.SuspendLayout();
            this.grpShipmentInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShipmentInfo)).BeginInit();
            this.grpPICKPalletInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // main_panel
            // 
            this.main_panel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.main_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.main_panel.Controls.Add(this.panel1);
            this.main_panel.Controls.Add(this.txtTitel);
            this.main_panel.Controls.Add(this.lblMessage);
            this.main_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main_panel.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.main_panel.Location = new System.Drawing.Point(0, 0);
            this.main_panel.Name = "main_panel";
            this.main_panel.Size = new System.Drawing.Size(1412, 795);
            this.main_panel.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grpCartonNo);
            this.panel1.Controls.Add(this.grpOrderInfo);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.grpShipmentInfo);
            this.panel1.Controls.Add(this.grpPICKPalletInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1408, 705);
            this.panel1.TabIndex = 58;
            // 
            // grpCartonNo
            // 
            this.grpCartonNo.Controls.Add(this.dgvShowBoxStatus);
            this.grpCartonNo.Controls.Add(this.panel3);
            this.grpCartonNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCartonNo.Location = new System.Drawing.Point(0, 395);
            this.grpCartonNo.Name = "grpCartonNo";
            this.grpCartonNo.Size = new System.Drawing.Size(1408, 310);
            this.grpCartonNo.TabIndex = 69;
            this.grpCartonNo.TabStop = false;
            // 
            // dgvShowBoxStatus
            // 
            this.dgvShowBoxStatus.AllowUserToAddRows = false;
            this.dgvShowBoxStatus.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvShowBoxStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShowBoxStatus.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CartonNo,
            this.ictPn__,
            this.qty});
            this.dgvShowBoxStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShowBoxStatus.Location = new System.Drawing.Point(3, 22);
            this.dgvShowBoxStatus.Name = "dgvShowBoxStatus";
            this.dgvShowBoxStatus.RowHeadersVisible = false;
            this.dgvShowBoxStatus.RowHeadersWidth = 62;
            this.dgvShowBoxStatus.RowTemplate.Height = 27;
            this.dgvShowBoxStatus.Size = new System.Drawing.Size(1117, 285);
            this.dgvShowBoxStatus.TabIndex = 60;
            // 
            // CartonNo
            // 
            this.CartonNo.HeaderText = "箱号";
            this.CartonNo.MinimumWidth = 8;
            this.CartonNo.Name = "CartonNo";
            this.CartonNo.Width = 200;
            // 
            // ictPn__
            // 
            this.ictPn__.HeaderText = "料号";
            this.ictPn__.MinimumWidth = 8;
            this.ictPn__.Name = "ictPn__";
            this.ictPn__.Width = 200;
            // 
            // qty
            // 
            this.qty.HeaderText = "数量";
            this.qty.MinimumWidth = 8;
            this.qty.Name = "qty";
            this.qty.Width = 150;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(1120, 22);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(285, 285);
            this.panel3.TabIndex = 59;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.lblNeedPackBoxQty);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 285);
            this.groupBox1.TabIndex = 58;
            this.groupBox1.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label13.Location = new System.Drawing.Point(-3, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(109, 20);
            this.label13.TabIndex = 57;
            this.label13.Text = "已pack箱数";
            // 
            // lblNeedPackBoxQty
            // 
            this.lblNeedPackBoxQty.AutoSize = true;
            this.lblNeedPackBoxQty.Font = new System.Drawing.Font("SimSun", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNeedPackBoxQty.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblNeedPackBoxQty.Location = new System.Drawing.Point(93, 50);
            this.lblNeedPackBoxQty.Name = "lblNeedPackBoxQty";
            this.lblNeedPackBoxQty.Size = new System.Drawing.Size(92, 48);
            this.lblNeedPackBoxQty.TabIndex = 42;
            this.lblNeedPackBoxQty.Text = "0/0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label10.Location = new System.Drawing.Point(172, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(129, 20);
            this.label10.TabIndex = 45;
            this.label10.Text = "需要pack箱数";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(153, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 20);
            this.label8.TabIndex = 43;
            this.label8.Text = "/";
            // 
            // grpOrderInfo
            // 
            this.grpOrderInfo.Controls.Add(this.dgvOrderInfo);
            this.grpOrderInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpOrderInfo.Location = new System.Drawing.Point(0, 289);
            this.grpOrderInfo.Name = "grpOrderInfo";
            this.grpOrderInfo.Size = new System.Drawing.Size(1408, 106);
            this.grpOrderInfo.TabIndex = 68;
            this.grpOrderInfo.TabStop = false;
            // 
            // dgvOrderInfo
            // 
            this.dgvOrderInfo.AllowUserToAddRows = false;
            this.dgvOrderInfo.AllowUserToDeleteRows = false;
            this.dgvOrderInfo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvOrderInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DeliveryNo,
            this.ictPn,
            this.totalBoxQty,
            this.totalQty,
            this.planBoxQty,
            this.planQty,
            this.packQty,
            this.packBoxQty,
            this.pack_status});
            this.dgvOrderInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrderInfo.Location = new System.Drawing.Point(3, 22);
            this.dgvOrderInfo.Name = "dgvOrderInfo";
            this.dgvOrderInfo.ReadOnly = true;
            this.dgvOrderInfo.RowHeadersVisible = false;
            this.dgvOrderInfo.RowHeadersWidth = 62;
            this.dgvOrderInfo.RowTemplate.Height = 27;
            this.dgvOrderInfo.Size = new System.Drawing.Size(1402, 81);
            this.dgvOrderInfo.TabIndex = 68;
            // 
            // DeliveryNo
            // 
            this.DeliveryNo.FillWeight = 92.51269F;
            this.DeliveryNo.HeaderText = "DeliveryNo";
            this.DeliveryNo.MinimumWidth = 8;
            this.DeliveryNo.Name = "DeliveryNo";
            this.DeliveryNo.ReadOnly = true;
            this.DeliveryNo.Width = 120;
            // 
            // ictPn
            // 
            this.ictPn.FillWeight = 159.8985F;
            this.ictPn.HeaderText = "料号";
            this.ictPn.MinimumWidth = 8;
            this.ictPn.Name = "ictPn";
            this.ictPn.ReadOnly = true;
            this.ictPn.Width = 180;
            // 
            // totalBoxQty
            // 
            this.totalBoxQty.FillWeight = 92.51269F;
            this.totalBoxQty.HeaderText = "总箱数";
            this.totalBoxQty.MinimumWidth = 8;
            this.totalBoxQty.Name = "totalBoxQty";
            this.totalBoxQty.ReadOnly = true;
            this.totalBoxQty.Width = 150;
            // 
            // totalQty
            // 
            this.totalQty.FillWeight = 92.51269F;
            this.totalQty.HeaderText = "总数量";
            this.totalQty.MinimumWidth = 8;
            this.totalQty.Name = "totalQty";
            this.totalQty.ReadOnly = true;
            this.totalQty.Width = 150;
            // 
            // planBoxQty
            // 
            this.planBoxQty.FillWeight = 92.51269F;
            this.planBoxQty.HeaderText = "计划箱数";
            this.planBoxQty.MinimumWidth = 8;
            this.planBoxQty.Name = "planBoxQty";
            this.planBoxQty.ReadOnly = true;
            this.planBoxQty.Width = 150;
            // 
            // planQty
            // 
            this.planQty.FillWeight = 92.51269F;
            this.planQty.HeaderText = "计划数量";
            this.planQty.MinimumWidth = 8;
            this.planQty.Name = "planQty";
            this.planQty.ReadOnly = true;
            this.planQty.Width = 150;
            // 
            // packQty
            // 
            this.packQty.FillWeight = 92.51269F;
            this.packQty.HeaderText = "pack数量";
            this.packQty.MinimumWidth = 8;
            this.packQty.Name = "packQty";
            this.packQty.ReadOnly = true;
            this.packQty.Width = 150;
            // 
            // packBoxQty
            // 
            this.packBoxQty.FillWeight = 92.51269F;
            this.packBoxQty.HeaderText = "pack箱数";
            this.packBoxQty.MinimumWidth = 8;
            this.packBoxQty.Name = "packBoxQty";
            this.packBoxQty.ReadOnly = true;
            this.packBoxQty.Width = 150;
            // 
            // pack_status
            // 
            this.pack_status.FillWeight = 92.51269F;
            this.pack_status.HeaderText = "状态";
            this.pack_status.MinimumWidth = 8;
            this.pack_status.Name = "pack_status";
            this.pack_status.ReadOnly = true;
            this.pack_status.Width = 150;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labFinish);
            this.panel2.Controls.Add(this.lstCreateTXT);
            this.panel2.Controls.Add(this.ChkInputCartonNo);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.responsetime_LB);
            this.panel2.Controls.Add(this.lblDNSumQty);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtDeliveryNo);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtCartonno);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnEnd);
            this.panel2.Controls.Add(this.btnStart);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 193);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1408, 96);
            this.panel2.TabIndex = 67;
            // 
            // labFinish
            // 
            this.labFinish.AutoSize = true;
            this.labFinish.Font = new System.Drawing.Font("SimSun", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labFinish.ForeColor = System.Drawing.Color.Red;
            this.labFinish.Location = new System.Drawing.Point(1079, 15);
            this.labFinish.Name = "labFinish";
            this.labFinish.Size = new System.Drawing.Size(123, 35);
            this.labFinish.TabIndex = 82;
            this.labFinish.Text = "需结单";
            this.labFinish.Visible = false;
            // 
            // btnPrinter
            // 
            this.btnPrinter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrinter.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPrinter.Font = new System.Drawing.Font("Microsoft YaHei", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrinter.Location = new System.Drawing.Point(1277, 44);
            this.btnPrinter.Name = "btnPrinter";
            this.btnPrinter.Size = new System.Drawing.Size(66, 35);
            this.btnPrinter.TabIndex = 81;
            this.btnPrinter.Text = "Printer";
            this.btnPrinter.UseVisualStyleBackColor = false;
            this.btnPrinter.Click += new System.EventHandler(this.btnPrinter_Click);
            // 
            // lstCreateTXT
            // 
            this.lstCreateTXT.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstCreateTXT.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstCreateTXT.FormattingEnabled = true;
            this.lstCreateTXT.HorizontalScrollbar = true;
            this.lstCreateTXT.ItemHeight = 20;
            this.lstCreateTXT.Location = new System.Drawing.Point(623, 3);
            this.lstCreateTXT.Name = "lstCreateTXT";
            this.lstCreateTXT.ScrollAlwaysVisible = true;
            this.lstCreateTXT.Size = new System.Drawing.Size(300, 84);
            this.lstCreateTXT.TabIndex = 64;
            this.lstCreateTXT.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstCreateTXT_DrawItem);
            // 
            // ChkInputCartonNo
            // 
            this.ChkInputCartonNo.AutoSize = true;
            this.ChkInputCartonNo.Checked = true;
            this.ChkInputCartonNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkInputCartonNo.Location = new System.Drawing.Point(464, 7);
            this.ChkInputCartonNo.Name = "ChkInputCartonNo";
            this.ChkInputCartonNo.Size = new System.Drawing.Size(59, 20);
            this.ChkInputCartonNo.TabIndex = 63;
            this.ChkInputCartonNo.Text = "箱号";
            this.ChkInputCartonNo.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1017, 68);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(40, 16);
            this.label14.TabIndex = 62;
            this.label14.Text = "S/秒";
            // 
            // responsetime_LB
            // 
            this.responsetime_LB.AutoSize = true;
            this.responsetime_LB.ForeColor = System.Drawing.Color.Red;
            this.responsetime_LB.Location = new System.Drawing.Point(972, 68);
            this.responsetime_LB.Name = "responsetime_LB";
            this.responsetime_LB.Size = new System.Drawing.Size(40, 16);
            this.responsetime_LB.TabIndex = 61;
            this.responsetime_LB.Text = "——";
            // 
            // lblDNSumQty
            // 
            this.lblDNSumQty.AutoSize = true;
            this.lblDNSumQty.Font = new System.Drawing.Font("SimSun", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDNSumQty.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblDNSumQty.Location = new System.Drawing.Point(454, 49);
            this.lblDNSumQty.Name = "lblDNSumQty";
            this.lblDNSumQty.Size = new System.Drawing.Size(69, 35);
            this.lblDNSumQty.TabIndex = 59;
            this.lblDNSumQty.Text = "0/0";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(542, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 14);
            this.label5.TabIndex = 56;
            this.label5.Text = "文件产出:";
            // 
            // txtDeliveryNo
            // 
            this.txtDeliveryNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDeliveryNo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtDeliveryNo.Font = new System.Drawing.Font("SimSun", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDeliveryNo.Location = new System.Drawing.Point(196, 46);
            this.txtDeliveryNo.Name = "txtDeliveryNo";
            this.txtDeliveryNo.ReadOnly = true;
            this.txtDeliveryNo.Size = new System.Drawing.Size(202, 38);
            this.txtDeliveryNo.TabIndex = 55;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(95, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 14);
            this.label4.TabIndex = 54;
            this.label4.Text = "DeliveryNo:";
            // 
            // txtCartonno
            // 
            this.txtCartonno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCartonno.Enabled = false;
            this.txtCartonno.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCartonno.Location = new System.Drawing.Point(196, 5);
            this.txtCartonno.Name = "txtCartonno";
            this.txtCartonno.Size = new System.Drawing.Size(252, 29);
            this.txtCartonno.TabIndex = 53;
            this.txtCartonno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCartonno_KeyDown);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(18, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(172, 14);
            this.label3.TabIndex = 52;
            this.label3.Text = "SN/Carton/Pick栈板号:";
            // 
            // btnEnd
            // 
            this.btnEnd.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEnd.Location = new System.Drawing.Point(969, 35);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(92, 27);
            this.btnEnd.TabIndex = 51;
            this.btnEnd.Text = "暂停作业";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnStart
            // 
            this.btnStart.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnStart.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.Location = new System.Drawing.Point(968, 7);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(92, 27);
            this.btnStart.TabIndex = 50;
            this.btnStart.Text = "继续作业";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // grpShipmentInfo
            // 
            this.grpShipmentInfo.Controls.Add(this.dgvShipmentInfo);
            this.grpShipmentInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpShipmentInfo.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpShipmentInfo.Location = new System.Drawing.Point(0, 89);
            this.grpShipmentInfo.Name = "grpShipmentInfo";
            this.grpShipmentInfo.Size = new System.Drawing.Size(1408, 104);
            this.grpShipmentInfo.TabIndex = 66;
            this.grpShipmentInfo.TabStop = false;
            this.grpShipmentInfo.Text = "集货单号列表";
            // 
            // dgvShipmentInfo
            // 
            this.dgvShipmentInfo.AllowUserToAddRows = false;
            this.dgvShipmentInfo.AllowUserToDeleteRows = false;
            this.dgvShipmentInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvShipmentInfo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvShipmentInfo.ColumnHeadersHeight = 30;
            this.dgvShipmentInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvShipmentInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.shipmentId,
            this.Carrier,
            this.POE,
            this.region,
            this.palletNo,
            this.palletType,
            this.ictPn_,
            this.cartonQty,
            this.alreadyPickCartonQty,
            this.alreadyPackCartonQty,
            this.status});
            this.dgvShipmentInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShipmentInfo.Location = new System.Drawing.Point(3, 18);
            this.dgvShipmentInfo.Name = "dgvShipmentInfo";
            this.dgvShipmentInfo.ReadOnly = true;
            this.dgvShipmentInfo.RowHeadersVisible = false;
            this.dgvShipmentInfo.RowHeadersWidth = 62;
            this.dgvShipmentInfo.RowTemplate.Height = 27;
            this.dgvShipmentInfo.Size = new System.Drawing.Size(1402, 83);
            this.dgvShipmentInfo.TabIndex = 0;
            // 
            // shipmentId
            // 
            this.shipmentId.FillWeight = 84.38466F;
            this.shipmentId.HeaderText = "集货单号";
            this.shipmentId.MinimumWidth = 8;
            this.shipmentId.Name = "shipmentId";
            this.shipmentId.ReadOnly = true;
            // 
            // Carrier
            // 
            this.Carrier.FillWeight = 84.38466F;
            this.Carrier.HeaderText = "Carrier";
            this.Carrier.MinimumWidth = 8;
            this.Carrier.Name = "Carrier";
            this.Carrier.ReadOnly = true;
            // 
            // POE
            // 
            this.POE.FillWeight = 84.38466F;
            this.POE.HeaderText = "POE";
            this.POE.MinimumWidth = 8;
            this.POE.Name = "POE";
            this.POE.ReadOnly = true;
            // 
            // region
            // 
            this.region.FillWeight = 84.38466F;
            this.region.HeaderText = "地区";
            this.region.MinimumWidth = 8;
            this.region.Name = "region";
            this.region.ReadOnly = true;
            // 
            // palletNo
            // 
            this.palletNo.FillWeight = 111.6751F;
            this.palletNo.HeaderText = "栈板号";
            this.palletNo.MinimumWidth = 8;
            this.palletNo.Name = "palletNo";
            this.palletNo.ReadOnly = true;
            // 
            // palletType
            // 
            this.palletType.FillWeight = 84.38466F;
            this.palletType.HeaderText = "栈板类型";
            this.palletType.MinimumWidth = 8;
            this.palletType.Name = "palletType";
            this.palletType.ReadOnly = true;
            // 
            // ictPn_
            // 
            this.ictPn_.FillWeight = 182.3125F;
            this.ictPn_.HeaderText = "料号";
            this.ictPn_.MinimumWidth = 8;
            this.ictPn_.Name = "ictPn_";
            this.ictPn_.ReadOnly = true;
            // 
            // cartonQty
            // 
            this.cartonQty.FillWeight = 84.38466F;
            this.cartonQty.HeaderText = "箱数";
            this.cartonQty.MinimumWidth = 8;
            this.cartonQty.Name = "cartonQty";
            this.cartonQty.ReadOnly = true;
            // 
            // alreadyPickCartonQty
            // 
            this.alreadyPickCartonQty.FillWeight = 106.4152F;
            this.alreadyPickCartonQty.HeaderText = "已Pick箱数";
            this.alreadyPickCartonQty.MinimumWidth = 8;
            this.alreadyPickCartonQty.Name = "alreadyPickCartonQty";
            this.alreadyPickCartonQty.ReadOnly = true;
            // 
            // alreadyPackCartonQty
            // 
            this.alreadyPackCartonQty.FillWeight = 108.9045F;
            this.alreadyPackCartonQty.HeaderText = "已Pack箱数";
            this.alreadyPackCartonQty.MinimumWidth = 8;
            this.alreadyPackCartonQty.Name = "alreadyPackCartonQty";
            this.alreadyPackCartonQty.ReadOnly = true;
            // 
            // status
            // 
            this.status.FillWeight = 84.38466F;
            this.status.HeaderText = "状态";
            this.status.MinimumWidth = 8;
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // grpPICKPalletInfo
            // 
            this.grpPICKPalletInfo.Controls.Add(this.lblSecurity);
            this.grpPICKPalletInfo.Controls.Add(this.btnPrinter);
            this.grpPICKPalletInfo.Controls.Add(this.lblRemark);
            this.grpPICKPalletInfo.Controls.Add(this.txtShipmentId);
            this.grpPICKPalletInfo.Controls.Add(this.btnResetStatus);
            this.grpPICKPalletInfo.Controls.Add(this.lblShipmentType);
            this.grpPICKPalletInfo.Controls.Add(this.btnCreateDHLFile);
            this.grpPICKPalletInfo.Controls.Add(this.lblisMix);
            this.grpPICKPalletInfo.Controls.Add(this.label1);
            this.grpPICKPalletInfo.Controls.Add(this.btnReprint);
            this.grpPICKPalletInfo.Controls.Add(this.label2);
            this.grpPICKPalletInfo.Controls.Add(this.lblPOE);
            this.grpPICKPalletInfo.Controls.Add(this.label6);
            this.grpPICKPalletInfo.Controls.Add(this.txtPickPalletNo);
            this.grpPICKPalletInfo.Controls.Add(this.lblType);
            this.grpPICKPalletInfo.Controls.Add(this.lblCarrierName);
            this.grpPICKPalletInfo.Controls.Add(this.label7);
            this.grpPICKPalletInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpPICKPalletInfo.Location = new System.Drawing.Point(0, 0);
            this.grpPICKPalletInfo.Name = "grpPICKPalletInfo";
            this.grpPICKPalletInfo.Size = new System.Drawing.Size(1408, 89);
            this.grpPICKPalletInfo.TabIndex = 65;
            this.grpPICKPalletInfo.TabStop = false;
            // 
            // lblSecurity
            // 
            this.lblSecurity.AutoSize = true;
            this.lblSecurity.Font = new System.Drawing.Font("SimSun", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSecurity.ForeColor = System.Drawing.Color.Black;
            this.lblSecurity.Location = new System.Drawing.Point(644, 50);
            this.lblSecurity.Name = "lblSecurity";
            this.lblSecurity.Size = new System.Drawing.Size(54, 22);
            this.lblSecurity.TabIndex = 80;
            this.lblSecurity.Text = "SECU";
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Font = new System.Drawing.Font("SimSun", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRemark.ForeColor = System.Drawing.Color.Black;
            this.lblRemark.Location = new System.Drawing.Point(1080, 18);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(96, 27);
            this.lblRemark.TabIndex = 79;
            this.lblRemark.Text = "REMARK";
            // 
            // txtShipmentId
            // 
            this.txtShipmentId.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtShipmentId.Location = new System.Drawing.Point(183, 18);
            this.txtShipmentId.Name = "txtShipmentId";
            this.txtShipmentId.Size = new System.Drawing.Size(220, 26);
            this.txtShipmentId.TabIndex = 66;
            this.txtShipmentId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtShipmentId_KeyDown);
            // 
            // btnResetStatus
            // 
            this.btnResetStatus.Location = new System.Drawing.Point(914, 50);
            this.btnResetStatus.Name = "btnResetStatus";
            this.btnResetStatus.Size = new System.Drawing.Size(91, 29);
            this.btnResetStatus.TabIndex = 78;
            this.btnResetStatus.Text = "重置";
            this.btnResetStatus.UseVisualStyleBackColor = true;
            this.btnResetStatus.Click += new System.EventHandler(this.btnResetStatus_Click);
            // 
            // lblShipmentType
            // 
            this.lblShipmentType.AutoSize = true;
            this.lblShipmentType.Font = new System.Drawing.Font("SimSun", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblShipmentType.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblShipmentType.Location = new System.Drawing.Point(762, 18);
            this.lblShipmentType.Name = "lblShipmentType";
            this.lblShipmentType.Size = new System.Drawing.Size(96, 27);
            this.lblShipmentType.TabIndex = 77;
            this.lblShipmentType.Text = "DIRECT";
            // 
            // btnCreateDHLFile
            // 
            this.btnCreateDHLFile.Location = new System.Drawing.Point(1036, 50);
            this.btnCreateDHLFile.Name = "btnCreateDHLFile";
            this.btnCreateDHLFile.Size = new System.Drawing.Size(153, 29);
            this.btnCreateDHLFile.TabIndex = 70;
            this.btnCreateDHLFile.Text = "CreateDHLFile";
            this.btnCreateDHLFile.UseVisualStyleBackColor = true;
            this.btnCreateDHLFile.Visible = false;
            this.btnCreateDHLFile.Click += new System.EventHandler(this.btnCreateDHLFile_Click);
            // 
            // lblisMix
            // 
            this.lblisMix.AutoSize = true;
            this.lblisMix.Font = new System.Drawing.Font("SimSun", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblisMix.ForeColor = System.Drawing.Color.Red;
            this.lblisMix.Location = new System.Drawing.Point(443, 52);
            this.lblisMix.Name = "lblisMix";
            this.lblisMix.Size = new System.Drawing.Size(82, 27);
            this.lblisMix.TabIndex = 76;
            this.lblisMix.Text = "-----";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(8, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 65;
            this.label1.Text = "集货单号:";
            // 
            // btnReprint
            // 
            this.btnReprint.Location = new System.Drawing.Point(791, 50);
            this.btnReprint.Name = "btnReprint";
            this.btnReprint.Size = new System.Drawing.Size(92, 29);
            this.btnReprint.TabIndex = 69;
            this.btnReprint.Text = "补列印";
            this.btnReprint.UseVisualStyleBackColor = true;
            this.btnReprint.Click += new System.EventHandler(this.btnReprint_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(8, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 67;
            this.label2.Text = "Pick栈板号:";
            // 
            // lblPOE
            // 
            this.lblPOE.AutoSize = true;
            this.lblPOE.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPOE.Location = new System.Drawing.Point(653, 25);
            this.lblPOE.Name = "lblPOE";
            this.lblPOE.Size = new System.Drawing.Size(39, 14);
            this.lblPOE.TabIndex = 74;
            this.lblPOE.Text = "____";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(419, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 14);
            this.label6.TabIndex = 71;
            this.label6.Text = "货代:";
            // 
            // txtPickPalletNo
            // 
            this.txtPickPalletNo.Enabled = false;
            this.txtPickPalletNo.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPickPalletNo.Location = new System.Drawing.Point(183, 50);
            this.txtPickPalletNo.Name = "txtPickPalletNo";
            this.txtPickPalletNo.Size = new System.Drawing.Size(220, 26);
            this.txtPickPalletNo.TabIndex = 68;
            this.txtPickPalletNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPickPalletNo_KeyDown);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("SimSun", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblType.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblType.Location = new System.Drawing.Point(927, 18);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(96, 27);
            this.lblType.TabIndex = 75;
            this.lblType.Text = "PARCEL";
            // 
            // lblCarrierName
            // 
            this.lblCarrierName.AutoSize = true;
            this.lblCarrierName.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCarrierName.Location = new System.Drawing.Point(489, 26);
            this.lblCarrierName.Name = "lblCarrierName";
            this.lblCarrierName.Size = new System.Drawing.Size(39, 14);
            this.lblCarrierName.TabIndex = 73;
            this.lblCarrierName.Text = "____";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(584, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 14);
            this.label7.TabIndex = 72;
            this.label7.Text = "港口:";
            // 
            // txtTitel
            // 
            this.txtTitel.BackColor = System.Drawing.SystemColors.HotTrack;
            this.txtTitel.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTitel.Font = new System.Drawing.Font("Microsoft YaHei", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTitel.ForeColor = System.Drawing.Color.White;
            this.txtTitel.Location = new System.Drawing.Point(0, 0);
            this.txtTitel.Name = "txtTitel";
            this.txtTitel.ReadOnly = true;
            this.txtTitel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTitel.Size = new System.Drawing.Size(1408, 46);
            this.txtTitel.TabIndex = 62;
            this.txtTitel.Text = "PACK";
            this.txtTitel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMessage.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMessage.Location = new System.Drawing.Point(0, 751);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(1408, 40);
            this.lblMessage.TabIndex = 14;
            this.lblMessage.Text = "Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // fMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1412, 795);
            this.Controls.Add(this.main_panel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.Name = "fMain";
            this.Text = "Ver:1.0.0.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fMain_Load);
            this.SizeChanged += new System.EventHandler(this.fMain_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.fMain_Paint);
            this.main_panel.ResumeLayout(false);
            this.main_panel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.grpCartonNo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowBoxStatus)).EndInit();
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpOrderInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderInfo)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.grpShipmentInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShipmentInfo)).EndInit();
            this.grpPICKPalletInfo.ResumeLayout(false);
            this.grpPICKPalletInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel main_panel;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblNeedPackBoxQty;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtTitel;
        private System.Windows.Forms.GroupBox grpShipmentInfo;
        private System.Windows.Forms.DataGridView dgvShipmentInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn shipmentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Carrier;
        private System.Windows.Forms.DataGridViewTextBoxColumn POE;
        private System.Windows.Forms.DataGridViewTextBoxColumn region;
        private System.Windows.Forms.DataGridViewTextBoxColumn palletNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn palletType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ictPn_;
        private System.Windows.Forms.DataGridViewTextBoxColumn cartonQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn alreadyPickCartonQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn alreadyPackCartonQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.GroupBox grpPICKPalletInfo;
        private System.Windows.Forms.Label lblSecurity;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtShipmentId;
        private System.Windows.Forms.Button btnResetStatus;
        private System.Windows.Forms.Label lblShipmentType;
        private System.Windows.Forms.Button btnCreateDHLFile;
        private System.Windows.Forms.Label lblisMix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReprint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPOE;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPickPalletNo;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblCarrierName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox grpCartonNo;
        private System.Windows.Forms.DataGridView dgvShowBoxStatus;
        private System.Windows.Forms.GroupBox grpOrderInfo;
        private System.Windows.Forms.DataGridView dgvOrderInfo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox lstCreateTXT;
        private System.Windows.Forms.CheckBox ChkInputCartonNo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label responsetime_LB;
        private System.Windows.Forms.Label lblDNSumQty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDeliveryNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCartonno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn CartonNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ictPn__;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeliveryNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ictPn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalBoxQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn planBoxQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn planQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn packQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn packBoxQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn pack_status;
        private System.Windows.Forms.Button btnPrinter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labFinish;
    }
}

