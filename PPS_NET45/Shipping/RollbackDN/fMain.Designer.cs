namespace RollbackDN
{
    partial class fMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDNmodel = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.dgvDN = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.grpCartonZCOK = new System.Windows.Forms.GroupBox();
            this.dgvCartonZCOK = new System.Windows.Forms.DataGridView();
            this.OK_CARTON_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpPackCarton = new System.Windows.Forms.GroupBox();
            this.dgvPackCarton = new System.Windows.Forms.DataGridView();
            this.PALLET_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DELIVERY_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CARTON_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtPackCarton = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDNSelect = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSmId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtGroup = new System.Windows.Forms.TextBox();
            this.btnEnd = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvPick = new System.Windows.Forms.DataGridView();
            this.PALLET_NO2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ICTPN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZCCARTONCOUNT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancelSID = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.dt_start = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dt_end = new System.Windows.Forms.DateTimePicker();
            this.lblStart = new System.Windows.Forms.Label();
            this.TextMsg = new System.Windows.Forms.Label();
            this.labShow = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDN)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.grpCartonZCOK.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartonZCOK)).BeginInit();
            this.grpPackCarton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPackCarton)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPick)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDNmodel
            // 
            this.lblDNmodel.AutoSize = true;
            this.lblDNmodel.Location = new System.Drawing.Point(918, 40);
            this.lblDNmodel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDNmodel.Name = "lblDNmodel";
            this.lblDNmodel.Size = new System.Drawing.Size(0, 18);
            this.lblDNmodel.TabIndex = 109;
            // 
            // btnSearch
            // 
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSearch.Location = new System.Drawing.Point(716, 18);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(112, 38);
            this.btnSearch.TabIndex = 107;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnStart
            // 
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnStart.Location = new System.Drawing.Point(12, 66);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(112, 48);
            this.btnStart.TabIndex = 105;
            this.btnStart.Text = "开始作业";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // dgvDN
            // 
            this.dgvDN.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvDN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDN.Location = new System.Drawing.Point(4, 25);
            this.dgvDN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvDN.Name = "dgvDN";
            this.dgvDN.RowHeadersWidth = 62;
            this.dgvDN.RowTemplate.Height = 23;
            this.dgvDN.Size = new System.Drawing.Size(1312, 148);
            this.dgvDN.TabIndex = 0;
            this.dgvDN.SelectionChanged += new System.EventHandler(this.dgvDN_SelectionChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvDN);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 68);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(1320, 177);
            this.groupBox2.TabIndex = 108;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DN LIST:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.grpCartonZCOK);
            this.panel4.Controls.Add(this.grpPackCarton);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(4, 154);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1312, 192);
            this.panel4.TabIndex = 86;
            // 
            // grpCartonZCOK
            // 
            this.grpCartonZCOK.Controls.Add(this.dgvCartonZCOK);
            this.grpCartonZCOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCartonZCOK.Location = new System.Drawing.Point(867, 0);
            this.grpCartonZCOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpCartonZCOK.Name = "grpCartonZCOK";
            this.grpCartonZCOK.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpCartonZCOK.Size = new System.Drawing.Size(445, 192);
            this.grpCartonZCOK.TabIndex = 85;
            this.grpCartonZCOK.TabStop = false;
            this.grpCartonZCOK.Text = "已经ZC箱号";
            // 
            // dgvCartonZCOK
            // 
            this.dgvCartonZCOK.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvCartonZCOK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCartonZCOK.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OK_CARTON_NO});
            this.dgvCartonZCOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCartonZCOK.Location = new System.Drawing.Point(4, 25);
            this.dgvCartonZCOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvCartonZCOK.Name = "dgvCartonZCOK";
            this.dgvCartonZCOK.RowHeadersWidth = 62;
            this.dgvCartonZCOK.RowTemplate.Height = 23;
            this.dgvCartonZCOK.Size = new System.Drawing.Size(437, 163);
            this.dgvCartonZCOK.TabIndex = 83;
            // 
            // OK_CARTON_NO
            // 
            this.OK_CARTON_NO.HeaderText = "OK_CARTON_NO";
            this.OK_CARTON_NO.MinimumWidth = 8;
            this.OK_CARTON_NO.Name = "OK_CARTON_NO";
            this.OK_CARTON_NO.Width = 200;
            // 
            // grpPackCarton
            // 
            this.grpPackCarton.Controls.Add(this.dgvPackCarton);
            this.grpPackCarton.Dock = System.Windows.Forms.DockStyle.Left;
            this.grpPackCarton.Location = new System.Drawing.Point(0, 0);
            this.grpPackCarton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpPackCarton.Name = "grpPackCarton";
            this.grpPackCarton.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpPackCarton.Size = new System.Drawing.Size(867, 192);
            this.grpPackCarton.TabIndex = 84;
            this.grpPackCarton.TabStop = false;
            this.grpPackCarton.Text = "需ZC箱号";
            // 
            // dgvPackCarton
            // 
            this.dgvPackCarton.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvPackCarton.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPackCarton.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PALLET_NO,
            this.DELIVERY_NO,
            this.CARTON_NO});
            this.dgvPackCarton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPackCarton.Location = new System.Drawing.Point(4, 25);
            this.dgvPackCarton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvPackCarton.Name = "dgvPackCarton";
            this.dgvPackCarton.RowHeadersWidth = 62;
            this.dgvPackCarton.RowTemplate.Height = 23;
            this.dgvPackCarton.Size = new System.Drawing.Size(859, 163);
            this.dgvPackCarton.TabIndex = 82;
            // 
            // PALLET_NO
            // 
            this.PALLET_NO.HeaderText = "PALLET_NO";
            this.PALLET_NO.MinimumWidth = 8;
            this.PALLET_NO.Name = "PALLET_NO";
            this.PALLET_NO.Width = 180;
            // 
            // DELIVERY_NO
            // 
            this.DELIVERY_NO.HeaderText = "DELIVERY_NO";
            this.DELIVERY_NO.MinimumWidth = 8;
            this.DELIVERY_NO.Name = "DELIVERY_NO";
            this.DELIVERY_NO.Width = 120;
            // 
            // CARTON_NO
            // 
            this.CARTON_NO.HeaderText = "CARTON_NO";
            this.CARTON_NO.MinimumWidth = 8;
            this.CARTON_NO.Name = "CARTON_NO";
            this.CARTON_NO.Width = 220;
            // 
            // txtPackCarton
            // 
            this.txtPackCarton.BackColor = System.Drawing.Color.White;
            this.txtPackCarton.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPackCarton.Enabled = false;
            this.txtPackCarton.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPackCarton.ForeColor = System.Drawing.Color.Blue;
            this.txtPackCarton.Location = new System.Drawing.Point(396, 70);
            this.txtPackCarton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtPackCarton.Name = "txtPackCarton";
            this.txtPackCarton.Size = new System.Drawing.Size(464, 44);
            this.txtPackCarton.TabIndex = 81;
            this.txtPackCarton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPackCarton_KeyDown);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.txtDNSelect);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.txtSmId);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.txtGroup);
            this.panel3.Controls.Add(this.txtPackCarton);
            this.panel3.Controls.Add(this.btnStart);
            this.panel3.Controls.Add(this.btnEnd);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(4, 25);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1312, 129);
            this.panel3.TabIndex = 85;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(800, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 21);
            this.label1.TabIndex = 111;
            this.label1.Text = "DN:";
            // 
            // txtDNSelect
            // 
            this.txtDNSelect.BackColor = System.Drawing.SystemColors.Control;
            this.txtDNSelect.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDNSelect.ForeColor = System.Drawing.Color.Green;
            this.txtDNSelect.Location = new System.Drawing.Point(852, 10);
            this.txtDNSelect.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtDNSelect.Name = "txtDNSelect";
            this.txtDNSelect.Size = new System.Drawing.Size(205, 35);
            this.txtDNSelect.TabIndex = 112;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(8, 24);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 21);
            this.label4.TabIndex = 107;
            this.label4.Text = "集货单号:";
            // 
            // txtSmId
            // 
            this.txtSmId.BackColor = System.Drawing.SystemColors.Control;
            this.txtSmId.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSmId.ForeColor = System.Drawing.Color.Green;
            this.txtSmId.Location = new System.Drawing.Point(168, 16);
            this.txtSmId.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtSmId.Name = "txtSmId";
            this.txtSmId.Size = new System.Drawing.Size(205, 35);
            this.txtSmId.TabIndex = 108;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(392, 22);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 21);
            this.label7.TabIndex = 109;
            this.label7.Text = "ZC组号:";
            // 
            // txtGroup
            // 
            this.txtGroup.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtGroup.ForeColor = System.Drawing.Color.Green;
            this.txtGroup.Location = new System.Drawing.Point(506, 14);
            this.txtGroup.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtGroup.Name = "txtGroup";
            this.txtGroup.Size = new System.Drawing.Size(260, 35);
            this.txtGroup.TabIndex = 110;
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(168, 66);
            this.btnEnd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(112, 48);
            this.btnEnd.TabIndex = 106;
            this.btnEnd.Text = "结束作业";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel4);
            this.groupBox3.Controls.Add(this.panel3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 245);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(1320, 350);
            this.groupBox3.TabIndex = 109;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pack SN/Carton:";
            // 
            // dgvPick
            // 
            this.dgvPick.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvPick.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPick.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PALLET_NO2,
            this.ICTPN,
            this.ZCCARTONCOUNT});
            this.dgvPick.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPick.Location = new System.Drawing.Point(4, 25);
            this.dgvPick.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvPick.Name = "dgvPick";
            this.dgvPick.RowHeadersWidth = 62;
            this.dgvPick.RowTemplate.Height = 23;
            this.dgvPick.Size = new System.Drawing.Size(1312, 136);
            this.dgvPick.TabIndex = 0;
            // 
            // PALLET_NO2
            // 
            this.PALLET_NO2.HeaderText = "PALLET_NO2";
            this.PALLET_NO2.MinimumWidth = 8;
            this.PALLET_NO2.Name = "PALLET_NO2";
            this.PALLET_NO2.Width = 180;
            // 
            // ICTPN
            // 
            this.ICTPN.HeaderText = "ICTPN";
            this.ICTPN.MinimumWidth = 8;
            this.ICTPN.Name = "ICTPN";
            this.ICTPN.Width = 240;
            // 
            // ZCCARTONCOUNT
            // 
            this.ZCCARTONCOUNT.HeaderText = "ZCCARTONCOUNT";
            this.ZCCARTONCOUNT.MinimumWidth = 8;
            this.ZCCARTONCOUNT.Name = "ZCCARTONCOUNT";
            this.ZCCARTONCOUNT.Width = 150;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgvPick);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 595);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(1320, 165);
            this.groupBox4.TabIndex = 110;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "PICK 多余箱号";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 57);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1320, 760);
            this.panel1.TabIndex = 71;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancelSID);
            this.groupBox1.Controls.Add(this.btnTest);
            this.groupBox1.Controls.Add(this.dt_start);
            this.groupBox1.Controls.Add(this.lblEnd);
            this.groupBox1.Controls.Add(this.dt_end);
            this.groupBox1.Controls.Add(this.lblStart);
            this.groupBox1.Controls.Add(this.lblDNmodel);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(1320, 68);
            this.groupBox1.TabIndex = 107;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "筛选:";
            // 
            // btnCancelSID
            // 
            this.btnCancelSID.Location = new System.Drawing.Point(1052, 18);
            this.btnCancelSID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelSID.Name = "btnCancelSID";
            this.btnCancelSID.Size = new System.Drawing.Size(112, 38);
            this.btnCancelSID.TabIndex = 134;
            this.btnCancelSID.Text = "取消集货单";
            this.btnCancelSID.UseVisualStyleBackColor = true;
            this.btnCancelSID.Click += new System.EventHandler(this.btnCancelSID_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(875, 18);
            this.btnTest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(112, 38);
            this.btnTest.TabIndex = 133;
            this.btnTest.Text = "测试还原";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // dt_start
            // 
            this.dt_start.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_start.CustomFormat = "yyyy-MM-dd";
            this.dt_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_start.Location = new System.Drawing.Point(145, 24);
            this.dt_start.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dt_start.Name = "dt_start";
            this.dt_start.Size = new System.Drawing.Size(150, 28);
            this.dt_start.TabIndex = 132;
            this.dt_start.Value = new System.DateTime(2019, 7, 12, 0, 0, 0, 0);
            this.dt_start.ValueChanged += new System.EventHandler(this.dt_start_ValueChanged);
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(319, 28);
            this.lblEnd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(89, 18);
            this.lblEnd.TabIndex = 131;
            this.lblEnd.Text = "结束日期:";
            // 
            // dt_end
            // 
            this.dt_end.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_end.CustomFormat = "yyyy-MM-dd";
            this.dt_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_end.Location = new System.Drawing.Point(447, 24);
            this.dt_end.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dt_end.Name = "dt_end";
            this.dt_end.Size = new System.Drawing.Size(164, 28);
            this.dt_end.TabIndex = 130;
            this.dt_end.ValueChanged += new System.EventHandler(this.dt_end_ValueChanged);
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(13, 28);
            this.lblStart.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(89, 18);
            this.lblStart.TabIndex = 129;
            this.lblStart.Text = "开始日期:";
            // 
            // TextMsg
            // 
            this.TextMsg.AutoEllipsis = true;
            this.TextMsg.BackColor = System.Drawing.Color.Blue;
            this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 817);
            this.TextMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1320, 62);
            this.TextMsg.TabIndex = 70;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.SystemColors.Info;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labShow.Name = "labShow";
            this.labShow.ReadOnly = true;
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(1320, 57);
            this.labShow.TabIndex = 69;
            this.labShow.Text = "RBDN";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1320, 879);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TextMsg);
            this.Controls.Add(this.labShow);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "fMain";
            this.Text = "fMainNew";
            this.Load += new System.EventHandler(this.fMainNew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDN)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.grpCartonZCOK.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartonZCOK)).EndInit();
            this.grpPackCarton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPackCarton)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPick)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDNmodel;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.DataGridView dgvDN;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dgvPackCarton;
        private System.Windows.Forms.TextBox txtPackCarton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvPick;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.DateTimePicker dt_start;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dt_end;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSmId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDNSelect;
        private System.Windows.Forms.GroupBox grpCartonZCOK;
        private System.Windows.Forms.DataGridView dgvCartonZCOK;
        private System.Windows.Forms.GroupBox grpPackCarton;
        private System.Windows.Forms.DataGridViewTextBoxColumn OK_CARTON_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PALLET_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DELIVERY_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CARTON_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PALLET_NO2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ICTPN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZCCARTONCOUNT;
        private System.Windows.Forms.Button btnCancelSID;
        private System.Windows.Forms.Button btnTest;
    }
}