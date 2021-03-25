namespace CheckAC
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpResult = new System.Windows.Forms.GroupBox();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.Pallet_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrackingNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sn_carton_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dn_packing_list = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpCheckItem = new System.Windows.Forms.GroupBox();
            this.lblTotalTime = new System.Windows.Forms.Label();
            this.txtDNDN = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTrackingNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCartonNo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblNeedCheckBoxQty = new System.Windows.Forms.Label();
            this.txtDNPO = new System.Windows.Forms.TextBox();
            this.grpPalletInfo = new System.Windows.Forms.GroupBox();
            this.dgvPalletInfo = new System.Windows.Forms.DataGridView();
            this.palletNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalBoxQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alreadyPackQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alreadyPackBoxQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alreadyCheckedBoxQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpPICKPalletInfo = new System.Windows.Forms.GroupBox();
            this.lblPalletSize = new System.Windows.Forms.Label();
            this.lblSecurity = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPickPalletNo = new System.Windows.Forms.TextBox();
            this.txtShipmentId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblCarrierName = new System.Windows.Forms.Label();
            this.lblPOE = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblisMix = new System.Windows.Forms.Label();
            this.lblShipmentType = new System.Windows.Forms.Label();
            this.btnReprint = new System.Windows.Forms.Button();
            this.lblTitel = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.grpResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.grpCheckItem.SuspendLayout();
            this.grpPalletInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPalletInfo)).BeginInit();
            this.grpPICKPalletInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.grpResult);
            this.panel1.Controls.Add(this.grpCheckItem);
            this.panel1.Controls.Add(this.grpPalletInfo);
            this.panel1.Controls.Add(this.grpPICKPalletInfo);
            this.panel1.Controls.Add(this.lblTitel);
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1064, 599);
            this.panel1.TabIndex = 0;
            // 
            // grpResult
            // 
            this.grpResult.Controls.Add(this.dgvResult);
            this.grpResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpResult.Location = new System.Drawing.Point(0, 429);
            this.grpResult.Name = "grpResult";
            this.grpResult.Size = new System.Drawing.Size(1064, 130);
            this.grpResult.TabIndex = 92;
            this.grpResult.TabStop = false;
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Pallet_ID,
            this.TrackingNo,
            this.sn_carton_no,
            this.dn_packing_list,
            this.dataGridViewTextBoxColumn1});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvResult.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(3, 17);
            this.dgvResult.Margin = new System.Windows.Forms.Padding(2);
            this.dgvResult.Name = "dgvResult";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResult.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.RowTemplate.Height = 27;
            this.dgvResult.Size = new System.Drawing.Size(1058, 110);
            this.dgvResult.TabIndex = 75;
            // 
            // Pallet_ID
            // 
            this.Pallet_ID.FillWeight = 94.46596F;
            this.Pallet_ID.HeaderText = "PalletID";
            this.Pallet_ID.Name = "Pallet_ID";
            this.Pallet_ID.Width = 175;
            // 
            // TrackingNo
            // 
            this.TrackingNo.FillWeight = 102.3889F;
            this.TrackingNo.HeaderText = "TrackingNo";
            this.TrackingNo.Name = "TrackingNo";
            this.TrackingNo.Width = 190;
            // 
            // sn_carton_no
            // 
            this.sn_carton_no.FillWeight = 114.2132F;
            this.sn_carton_no.HeaderText = "PickPalletNo/Carton No";
            this.sn_carton_no.Name = "sn_carton_no";
            this.sn_carton_no.Width = 250;
            // 
            // dn_packing_list
            // 
            this.dn_packing_list.FillWeight = 94.46596F;
            this.dn_packing_list.HeaderText = "DN Packing List";
            this.dn_packing_list.Name = "dn_packing_list";
            this.dn_packing_list.Width = 176;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 94.46596F;
            this.dataGridViewTextBoxColumn1.HeaderText = "状态";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 176;
            // 
            // grpCheckItem
            // 
            this.grpCheckItem.Controls.Add(this.lblTotalTime);
            this.grpCheckItem.Controls.Add(this.txtDNDN);
            this.grpCheckItem.Controls.Add(this.label3);
            this.grpCheckItem.Controls.Add(this.txtTrackingNo);
            this.grpCheckItem.Controls.Add(this.label4);
            this.grpCheckItem.Controls.Add(this.txtCartonNo);
            this.grpCheckItem.Controls.Add(this.label10);
            this.grpCheckItem.Controls.Add(this.label9);
            this.grpCheckItem.Controls.Add(this.label11);
            this.grpCheckItem.Controls.Add(this.label8);
            this.grpCheckItem.Controls.Add(this.label5);
            this.grpCheckItem.Controls.Add(this.lblNeedCheckBoxQty);
            this.grpCheckItem.Controls.Add(this.txtDNPO);
            this.grpCheckItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpCheckItem.Location = new System.Drawing.Point(0, 288);
            this.grpCheckItem.Name = "grpCheckItem";
            this.grpCheckItem.Size = new System.Drawing.Size(1064, 141);
            this.grpCheckItem.TabIndex = 90;
            this.grpCheckItem.TabStop = false;
            // 
            // lblTotalTime
            // 
            this.lblTotalTime.Location = new System.Drawing.Point(521, 50);
            this.lblTotalTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalTime.Name = "lblTotalTime";
            this.lblTotalTime.Size = new System.Drawing.Size(69, 19);
            this.lblTotalTime.TabIndex = 86;
            this.lblTotalTime.Text = "0-秒";
            // 
            // txtDNDN
            // 
            this.txtDNDN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDNDN.Enabled = false;
            this.txtDNDN.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDNDN.Location = new System.Drawing.Point(201, 110);
            this.txtDNDN.Margin = new System.Windows.Forms.Padding(2);
            this.txtDNDN.Name = "txtDNDN";
            this.txtDNDN.Size = new System.Drawing.Size(236, 23);
            this.txtDNDN.TabIndex = 84;
            this.txtDNDN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDNDN_KeyDown);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(94, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 14);
            this.label3.TabIndex = 69;
            this.label3.Text = "TrackingNo:";
            // 
            // txtTrackingNo
            // 
            this.txtTrackingNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTrackingNo.Enabled = false;
            this.txtTrackingNo.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTrackingNo.Location = new System.Drawing.Point(201, 14);
            this.txtTrackingNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtTrackingNo.Name = "txtTrackingNo";
            this.txtTrackingNo.Size = new System.Drawing.Size(236, 23);
            this.txtTrackingNo.TabIndex = 70;
            this.txtTrackingNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTrackingNo_KeyDown);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(109, 49);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 14);
            this.label4.TabIndex = 71;
            this.label4.Text = "cartonNo:";
            // 
            // txtCartonNo
            // 
            this.txtCartonNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCartonNo.Enabled = false;
            this.txtCartonNo.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCartonNo.Location = new System.Drawing.Point(201, 46);
            this.txtCartonNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtCartonNo.Name = "txtCartonNo";
            this.txtCartonNo.Size = new System.Drawing.Size(236, 23);
            this.txtCartonNo.TabIndex = 72;
            this.txtCartonNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCartonNo_KeyDown);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label10.Location = new System.Drawing.Point(855, 17);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(139, 20);
            this.label10.TabIndex = 80;
            this.label10.Text = "需要Check箱数";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(21, 114);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(167, 14);
            this.label9.TabIndex = 83;
            this.label9.Text = "DN/PO(DeliveryNote):";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(689, 17);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(139, 20);
            this.label11.TabIndex = 79;
            this.label11.Text = "已Checked箱数";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(832, 17);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 20);
            this.label8.TabIndex = 78;
            this.label8.Text = "/";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(21, 80);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(167, 14);
            this.label5.TabIndex = 73;
            this.label5.Text = "DN/PO(Packing List):";
            // 
            // lblNeedCheckBoxQty
            // 
            this.lblNeedCheckBoxQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNeedCheckBoxQty.AutoSize = true;
            this.lblNeedCheckBoxQty.Font = new System.Drawing.Font("SimSun", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNeedCheckBoxQty.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblNeedCheckBoxQty.Location = new System.Drawing.Point(718, 49);
            this.lblNeedCheckBoxQty.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNeedCheckBoxQty.Name = "lblNeedCheckBoxQty";
            this.lblNeedCheckBoxQty.Size = new System.Drawing.Size(124, 64);
            this.lblNeedCheckBoxQty.TabIndex = 77;
            this.lblNeedCheckBoxQty.Text = "0/0";
            // 
            // txtDNPO
            // 
            this.txtDNPO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDNPO.Enabled = false;
            this.txtDNPO.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDNPO.Location = new System.Drawing.Point(201, 77);
            this.txtDNPO.Margin = new System.Windows.Forms.Padding(2);
            this.txtDNPO.Name = "txtDNPO";
            this.txtDNPO.Size = new System.Drawing.Size(236, 23);
            this.txtDNPO.TabIndex = 74;
            this.txtDNPO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDNPO_KeyDown);
            // 
            // grpPalletInfo
            // 
            this.grpPalletInfo.Controls.Add(this.dgvPalletInfo);
            this.grpPalletInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpPalletInfo.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpPalletInfo.Location = new System.Drawing.Point(0, 136);
            this.grpPalletInfo.Margin = new System.Windows.Forms.Padding(2);
            this.grpPalletInfo.Name = "grpPalletInfo";
            this.grpPalletInfo.Padding = new System.Windows.Forms.Padding(2);
            this.grpPalletInfo.Size = new System.Drawing.Size(1064, 152);
            this.grpPalletInfo.TabIndex = 89;
            this.grpPalletInfo.TabStop = false;
            this.grpPalletInfo.Text = "栈板信息";
            // 
            // dgvPalletInfo
            // 
            this.dgvPalletInfo.AllowUserToAddRows = false;
            this.dgvPalletInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPalletInfo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPalletInfo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvPalletInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPalletInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.palletNo,
            this.partNo,
            this.totalBoxQty,
            this.totalQty,
            this.alreadyPackQty,
            this.alreadyPackBoxQty,
            this.alreadyCheckedBoxQty,
            this.status});
            this.dgvPalletInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPalletInfo.Location = new System.Drawing.Point(2, 21);
            this.dgvPalletInfo.Margin = new System.Windows.Forms.Padding(2);
            this.dgvPalletInfo.Name = "dgvPalletInfo";
            this.dgvPalletInfo.RowHeadersVisible = false;
            this.dgvPalletInfo.RowTemplate.Height = 27;
            this.dgvPalletInfo.Size = new System.Drawing.Size(1060, 129);
            this.dgvPalletInfo.TabIndex = 0;
            // 
            // palletNo
            // 
            this.palletNo.FillWeight = 91.35555F;
            this.palletNo.HeaderText = "栈板号";
            this.palletNo.Name = "palletNo";
            // 
            // partNo
            // 
            this.partNo.FillWeight = 138.1611F;
            this.partNo.HeaderText = "料号";
            this.partNo.Name = "partNo";
            // 
            // totalBoxQty
            // 
            this.totalBoxQty.FillWeight = 91.35555F;
            this.totalBoxQty.HeaderText = "总箱数";
            this.totalBoxQty.Name = "totalBoxQty";
            // 
            // totalQty
            // 
            this.totalQty.FillWeight = 91.35555F;
            this.totalQty.HeaderText = "总数量";
            this.totalQty.Name = "totalQty";
            // 
            // alreadyPackQty
            // 
            this.alreadyPackQty.FillWeight = 91.35555F;
            this.alreadyPackQty.HeaderText = "已Pack数量";
            this.alreadyPackQty.Name = "alreadyPackQty";
            // 
            // alreadyPackBoxQty
            // 
            this.alreadyPackBoxQty.FillWeight = 91.35555F;
            this.alreadyPackBoxQty.HeaderText = "已Pack箱数";
            this.alreadyPackBoxQty.Name = "alreadyPackBoxQty";
            // 
            // alreadyCheckedBoxQty
            // 
            this.alreadyCheckedBoxQty.FillWeight = 113.7056F;
            this.alreadyCheckedBoxQty.HeaderText = "已Checked箱数";
            this.alreadyCheckedBoxQty.Name = "alreadyCheckedBoxQty";
            // 
            // status
            // 
            this.status.FillWeight = 91.35555F;
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            // 
            // grpPICKPalletInfo
            // 
            this.grpPICKPalletInfo.Controls.Add(this.lblPalletSize);
            this.grpPICKPalletInfo.Controls.Add(this.lblSecurity);
            this.grpPICKPalletInfo.Controls.Add(this.label1);
            this.grpPICKPalletInfo.Controls.Add(this.label2);
            this.grpPICKPalletInfo.Controls.Add(this.txtPickPalletNo);
            this.grpPICKPalletInfo.Controls.Add(this.txtShipmentId);
            this.grpPICKPalletInfo.Controls.Add(this.label6);
            this.grpPICKPalletInfo.Controls.Add(this.label7);
            this.grpPICKPalletInfo.Controls.Add(this.btnReset);
            this.grpPICKPalletInfo.Controls.Add(this.lblCarrierName);
            this.grpPICKPalletInfo.Controls.Add(this.lblPOE);
            this.grpPICKPalletInfo.Controls.Add(this.lblType);
            this.grpPICKPalletInfo.Controls.Add(this.lblisMix);
            this.grpPICKPalletInfo.Controls.Add(this.lblShipmentType);
            this.grpPICKPalletInfo.Controls.Add(this.btnReprint);
            this.grpPICKPalletInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpPICKPalletInfo.Location = new System.Drawing.Point(0, 41);
            this.grpPICKPalletInfo.Name = "grpPICKPalletInfo";
            this.grpPICKPalletInfo.Size = new System.Drawing.Size(1064, 95);
            this.grpPICKPalletInfo.TabIndex = 88;
            this.grpPICKPalletInfo.TabStop = false;
            // 
            // lblPalletSize
            // 
            this.lblPalletSize.Font = new System.Drawing.Font("SimSun", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPalletSize.ForeColor = System.Drawing.Color.Black;
            this.lblPalletSize.Location = new System.Drawing.Point(486, 49);
            this.lblPalletSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPalletSize.Name = "lblPalletSize";
            this.lblPalletSize.Size = new System.Drawing.Size(277, 38);
            this.lblPalletSize.TabIndex = 86;
            this.lblPalletSize.Text = "-----";
            // 
            // lblSecurity
            // 
            this.lblSecurity.AutoSize = true;
            this.lblSecurity.Font = new System.Drawing.Font("SimSun", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSecurity.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblSecurity.Location = new System.Drawing.Point(858, 19);
            this.lblSecurity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSecurity.Name = "lblSecurity";
            this.lblSecurity.Size = new System.Drawing.Size(95, 33);
            this.lblSecurity.TabIndex = 87;
            this.lblSecurity.Text = "BASIC";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(5, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 56;
            this.label1.Text = "集货单号:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(5, 57);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 58;
            this.label2.Text = "Pick栈板号:";
            // 
            // txtPickPalletNo
            // 
            this.txtPickPalletNo.Enabled = false;
            this.txtPickPalletNo.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPickPalletNo.Location = new System.Drawing.Point(117, 57);
            this.txtPickPalletNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtPickPalletNo.Name = "txtPickPalletNo";
            this.txtPickPalletNo.Size = new System.Drawing.Size(220, 26);
            this.txtPickPalletNo.TabIndex = 59;
            this.txtPickPalletNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPickPalletNo_KeyDown);
            // 
            // txtShipmentId
            // 
            this.txtShipmentId.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtShipmentId.Location = new System.Drawing.Point(117, 19);
            this.txtShipmentId.Margin = new System.Windows.Forms.Padding(2);
            this.txtShipmentId.Name = "txtShipmentId";
            this.txtShipmentId.Size = new System.Drawing.Size(220, 26);
            this.txtShipmentId.TabIndex = 57;
            this.txtShipmentId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtShipmentId_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(341, 22);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 14);
            this.label6.TabIndex = 60;
            this.label6.Text = "货代:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(480, 22);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 14);
            this.label7.TabIndex = 61;
            this.label7.Text = "港口:";
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReset.Location = new System.Drawing.Point(953, 52);
            this.btnReset.Margin = new System.Windows.Forms.Padding(2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 32);
            this.btnReset.TabIndex = 81;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblCarrierName
            // 
            this.lblCarrierName.AutoSize = true;
            this.lblCarrierName.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCarrierName.Location = new System.Drawing.Point(385, 22);
            this.lblCarrierName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCarrierName.Name = "lblCarrierName";
            this.lblCarrierName.Size = new System.Drawing.Size(39, 14);
            this.lblCarrierName.TabIndex = 62;
            this.lblCarrierName.Text = "____";
            // 
            // lblPOE
            // 
            this.lblPOE.AutoSize = true;
            this.lblPOE.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPOE.Location = new System.Drawing.Point(523, 22);
            this.lblPOE.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPOE.Name = "lblPOE";
            this.lblPOE.Size = new System.Drawing.Size(39, 14);
            this.lblPOE.TabIndex = 63;
            this.lblPOE.Text = "____";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("SimSun", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblType.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblType.Location = new System.Drawing.Point(731, 19);
            this.lblType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(111, 33);
            this.lblType.TabIndex = 64;
            this.lblType.Text = "PARCEL";
            // 
            // lblisMix
            // 
            this.lblisMix.AutoSize = true;
            this.lblisMix.Font = new System.Drawing.Font("SimSun", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblisMix.ForeColor = System.Drawing.Color.Red;
            this.lblisMix.Location = new System.Drawing.Point(346, 49);
            this.lblisMix.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblisMix.Name = "lblisMix";
            this.lblisMix.Size = new System.Drawing.Size(112, 37);
            this.lblisMix.TabIndex = 65;
            this.lblisMix.Text = "-----";
            // 
            // lblShipmentType
            // 
            this.lblShipmentType.AutoSize = true;
            this.lblShipmentType.Font = new System.Drawing.Font("SimSun", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblShipmentType.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblShipmentType.Location = new System.Drawing.Point(611, 19);
            this.lblShipmentType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblShipmentType.Name = "lblShipmentType";
            this.lblShipmentType.Size = new System.Drawing.Size(111, 33);
            this.lblShipmentType.TabIndex = 66;
            this.lblShipmentType.Text = "DIRECT";
            // 
            // btnReprint
            // 
            this.btnReprint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReprint.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReprint.Location = new System.Drawing.Point(953, 14);
            this.btnReprint.Margin = new System.Windows.Forms.Padding(2);
            this.btnReprint.Name = "btnReprint";
            this.btnReprint.Size = new System.Drawing.Size(100, 32);
            this.btnReprint.TabIndex = 67;
            this.btnReprint.Text = "补列印";
            this.btnReprint.UseVisualStyleBackColor = true;
            this.btnReprint.Click += new System.EventHandler(this.btnReprint_Click);
            // 
            // lblTitel
            // 
            this.lblTitel.BackColor = System.Drawing.SystemColors.HotTrack;
            this.lblTitel.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitel.Enabled = false;
            this.lblTitel.Font = new System.Drawing.Font("SimSun", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitel.ForeColor = System.Drawing.Color.White;
            this.lblTitel.Location = new System.Drawing.Point(0, 0);
            this.lblTitel.Name = "lblTitel";
            this.lblTitel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTitel.Size = new System.Drawing.Size(1064, 41);
            this.lblTitel.TabIndex = 82;
            this.lblTitel.Text = "CHECK";
            this.lblTitel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMessage.Font = new System.Drawing.Font("SimSun", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMessage.Location = new System.Drawing.Point(0, 559);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(1064, 40);
            this.lblMessage.TabIndex = 15;
            this.lblMessage.Text = "Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 599);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "fMain";
            this.Text = "Ver:1.0.0.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.grpCheckItem.ResumeLayout(false);
            this.grpCheckItem.PerformLayout();
            this.grpPalletInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPalletInfo)).EndInit();
            this.grpPICKPalletInfo.ResumeLayout(false);
            this.grpPICKPalletInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblShipmentType;
        private System.Windows.Forms.Label lblisMix;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblPOE;
        private System.Windows.Forms.Label lblCarrierName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtShipmentId;
        private System.Windows.Forms.TextBox txtPickPalletNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReprint;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTrackingNo;
        private System.Windows.Forms.TextBox txtCartonNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDNPO;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblNeedCheckBoxQty;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TextBox lblTitel;
        private System.Windows.Forms.TextBox txtDNDN;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblPalletSize;
        private System.Windows.Forms.Label lblSecurity;
        private System.Windows.Forms.GroupBox grpCheckItem;
        private System.Windows.Forms.GroupBox grpPalletInfo;
        private System.Windows.Forms.DataGridView dgvPalletInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn palletNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn partNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalBoxQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn alreadyPackQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn alreadyPackBoxQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn alreadyCheckedBoxQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.GroupBox grpPICKPalletInfo;
        private System.Windows.Forms.GroupBox grpResult;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pallet_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrackingNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn sn_carton_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn dn_packing_list;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.Label lblTotalTime;
    }
}

