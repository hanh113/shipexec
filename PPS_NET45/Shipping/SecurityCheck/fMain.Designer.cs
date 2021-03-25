namespace SecurityCheck
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
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.lbldate = new System.Windows.Forms.Label();
            this.lblchepai = new System.Windows.Forms.Label();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbCarNo = new System.Windows.Forms.ComboBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTruck = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCartonQty = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPalletQty = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvPartInfo = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSecurityStatus = new System.Windows.Forms.TextBox();
            this.txtWHStatus = new System.Windows.Forms.TextBox();
            this.txtSecurity = new System.Windows.Forms.TextBox();
            this.txtWH = new System.Windows.Forms.TextBox();
            this.btnSecurity = new System.Windows.Forms.Button();
            this.btnWH = new System.Windows.Forms.Button();
            this.TextMsg = new System.Windows.Forms.Label();
            this.labShow = new System.Windows.Forms.TextBox();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartInfo)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dtpStartTime.CustomFormat = "yyyy-MM-dd ";
            this.dtpStartTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(159, 24);
            this.dtpStartTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(199, 35);
            this.dtpStartTime.TabIndex = 86;
            this.dtpStartTime.ValueChanged += new System.EventHandler(this.dtpStartTime_ValueChanged);
            // 
            // lbldate
            // 
            this.lbldate.AutoSize = true;
            this.lbldate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbldate.Location = new System.Drawing.Point(32, 29);
            this.lbldate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbldate.Name = "lbldate";
            this.lbldate.Size = new System.Drawing.Size(86, 24);
            this.lbldate.TabIndex = 69;
            this.lbldate.Text = "日 期:";
            // 
            // lblchepai
            // 
            this.lblchepai.AutoSize = true;
            this.lblchepai.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblchepai.Location = new System.Drawing.Point(723, 32);
            this.lblchepai.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblchepai.Name = "lblchepai";
            this.lblchepai.Size = new System.Drawing.Size(98, 24);
            this.lblchepai.TabIndex = 72;
            this.lblchepai.Text = "车牌号:";
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dtpEndTime.CustomFormat = "yyyy-MM-dd";
            this.dtpEndTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(416, 25);
            this.dtpEndTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(199, 35);
            this.dtpEndTime.TabIndex = 98;
            this.dtpEndTime.ValueChanged += new System.EventHandler(this.dtpEndTime_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(371, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 24);
            this.label4.TabIndex = 97;
            this.label4.Text = "-";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox3.Controls.Add(this.dtpStartTime);
            this.groupBox3.Controls.Add(this.lbldate);
            this.groupBox3.Controls.Add(this.lblchepai);
            this.groupBox3.Controls.Add(this.dtpEndTime);
            this.groupBox3.Controls.Add(this.cmbCarNo);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1178, 84);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "筛选";
            // 
            // cmbCarNo
            // 
            this.cmbCarNo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbCarNo.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCarNo.FormattingEnabled = true;
            this.cmbCarNo.Items.AddRange(new object[] {
            "卡车车牌号码"});
            this.cmbCarNo.Location = new System.Drawing.Point(849, 24);
            this.cmbCarNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbCarNo.Name = "cmbCarNo";
            this.cmbCarNo.Size = new System.Drawing.Size(325, 37);
            this.cmbCarNo.TabIndex = 0;
            this.cmbCarNo.SelectedIndexChanged += new System.EventHandler(this.cmbCarNo_SelectedIndexChanged);
            // 
            // txtID
            // 
            this.txtID.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtID.Location = new System.Drawing.Point(507, 32);
            this.txtID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(337, 40);
            this.txtID.TabIndex = 109;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(320, 35);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 29);
            this.label3.TabIndex = 108;
            this.label3.Text = "身份证号码:";
            // 
            // txtTruck
            // 
            this.txtTruck.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTruck.Location = new System.Drawing.Point(1025, 29);
            this.txtTruck.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTruck.Name = "txtTruck";
            this.txtTruck.ReadOnly = true;
            this.txtTruck.Size = new System.Drawing.Size(140, 40);
            this.txtTruck.TabIndex = 107;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(893, 35);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 29);
            this.label2.TabIndex = 106;
            this.label2.Text = "车型:";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.Location = new System.Drawing.Point(125, 32);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(160, 40);
            this.txtName.TabIndex = 105;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(8, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 29);
            this.label1.TabIndex = 104;
            this.label1.Text = "姓名:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtID);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTruck);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 84);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(1178, 92);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "司机信息";
            // 
            // txtQty
            // 
            this.txtQty.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtQty.Location = new System.Drawing.Point(195, 134);
            this.txtQty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(160, 40);
            this.txtQty.TabIndex = 111;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(8, 141);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 29);
            this.label7.TabIndex = 110;
            this.label7.Text = "总数:";
            // 
            // txtCartonQty
            // 
            this.txtCartonQty.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCartonQty.Location = new System.Drawing.Point(195, 81);
            this.txtCartonQty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCartonQty.Name = "txtCartonQty";
            this.txtCartonQty.ReadOnly = true;
            this.txtCartonQty.Size = new System.Drawing.Size(160, 40);
            this.txtCartonQty.TabIndex = 109;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(8, 88);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 29);
            this.label6.TabIndex = 108;
            this.label6.Text = "总箱数:";
            // 
            // txtPalletQty
            // 
            this.txtPalletQty.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPalletQty.Location = new System.Drawing.Point(195, 30);
            this.txtPalletQty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPalletQty.Name = "txtPalletQty";
            this.txtPalletQty.ReadOnly = true;
            this.txtPalletQty.Size = new System.Drawing.Size(160, 40);
            this.txtPalletQty.TabIndex = 107;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(8, 36);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 29);
            this.label5.TabIndex = 106;
            this.label5.Text = "总栈板数:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtQty);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txtCartonQty);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.txtPalletQty);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox4.Location = new System.Drawing.Point(762, 323);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(416, 167);
            this.groupBox4.TabIndex = 106;
            this.groupBox4.TabStop = false;
            // 
            // dgvPartInfo
            // 
            this.dgvPartInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPartInfo.BackgroundColor = System.Drawing.Color.White;
            this.dgvPartInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPartInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPartInfo.Location = new System.Drawing.Point(4, 25);
            this.dgvPartInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvPartInfo.Name = "dgvPartInfo";
            this.dgvPartInfo.RowHeadersWidth = 62;
            this.dgvPartInfo.RowTemplate.Height = 23;
            this.dgvPartInfo.Size = new System.Drawing.Size(754, 138);
            this.dgvPartInfo.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgvPartInfo);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 323);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox5.Size = new System.Drawing.Size(762, 167);
            this.groupBox5.TabIndex = 107;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "货物料号信息";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 56);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1178, 490);
            this.panel1.TabIndex = 75;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSecurityStatus);
            this.groupBox2.Controls.Add(this.txtWHStatus);
            this.groupBox2.Controls.Add(this.txtSecurity);
            this.groupBox2.Controls.Add(this.txtWH);
            this.groupBox2.Controls.Add(this.btnSecurity);
            this.groupBox2.Controls.Add(this.btnWH);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 176);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(1178, 147);
            this.groupBox2.TabIndex = 105;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "确认状态";
            // 
            // txtSecurityStatus
            // 
            this.txtSecurityStatus.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSecurityStatus.Location = new System.Drawing.Point(526, 30);
            this.txtSecurityStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSecurityStatus.Name = "txtSecurityStatus";
            this.txtSecurityStatus.ReadOnly = true;
            this.txtSecurityStatus.Size = new System.Drawing.Size(283, 40);
            this.txtSecurityStatus.TabIndex = 121;
            this.txtSecurityStatus.Text = "安保确认状态";
            this.txtSecurityStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtWHStatus
            // 
            this.txtWHStatus.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtWHStatus.Location = new System.Drawing.Point(134, 30);
            this.txtWHStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtWHStatus.Name = "txtWHStatus";
            this.txtWHStatus.ReadOnly = true;
            this.txtWHStatus.Size = new System.Drawing.Size(283, 40);
            this.txtWHStatus.TabIndex = 120;
            this.txtWHStatus.Text = "仓库确认状态";
            this.txtWHStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtWHStatus.Visible = false;
            // 
            // txtSecurity
            // 
            this.txtSecurity.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSecurity.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSecurity.Location = new System.Drawing.Point(465, 82);
            this.txtSecurity.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSecurity.Name = "txtSecurity";
            this.txtSecurity.PasswordChar = '*';
            this.txtSecurity.Size = new System.Drawing.Size(246, 40);
            this.txtSecurity.TabIndex = 119;
            this.txtSecurity.Text = "888888888";
            // 
            // txtWH
            // 
            this.txtWH.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtWH.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtWH.Location = new System.Drawing.Point(39, 82);
            this.txtWH.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtWH.Name = "txtWH";
            this.txtWH.PasswordChar = '*';
            this.txtWH.Size = new System.Drawing.Size(246, 40);
            this.txtWH.TabIndex = 118;
            this.txtWH.Text = "888888888";
            this.txtWH.Visible = false;
            // 
            // btnSecurity
            // 
            this.btnSecurity.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSecurity.Location = new System.Drawing.Point(734, 76);
            this.btnSecurity.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSecurity.Name = "btnSecurity";
            this.btnSecurity.Size = new System.Drawing.Size(141, 52);
            this.btnSecurity.TabIndex = 117;
            this.btnSecurity.Text = "安保确认";
            this.btnSecurity.UseVisualStyleBackColor = true;
            this.btnSecurity.Click += new System.EventHandler(this.btnSecurity_Click);
            // 
            // btnWH
            // 
            this.btnWH.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnWH.Location = new System.Drawing.Point(310, 76);
            this.btnWH.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnWH.Name = "btnWH";
            this.btnWH.Size = new System.Drawing.Size(141, 52);
            this.btnWH.TabIndex = 116;
            this.btnWH.Text = "仓库确认";
            this.btnWH.UseVisualStyleBackColor = true;
            this.btnWH.Visible = false;
            this.btnWH.Click += new System.EventHandler(this.btnWH_Click);
            // 
            // TextMsg
            // 
            this.TextMsg.AutoEllipsis = true;
            this.TextMsg.BackColor = System.Drawing.Color.Blue;
            this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 546);
            this.TextMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1178, 60);
            this.TextMsg.TabIndex = 74;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Italic);
            this.labShow.ForeColor = System.Drawing.SystemColors.Info;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Multiline = true;
            this.labShow.Name = "labShow";
            this.labShow.ReadOnly = true;
            this.labShow.Size = new System.Drawing.Size(1178, 56);
            this.labShow.TabIndex = 73;
            this.labShow.Text = "SecurityCheck";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 606);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TextMsg);
            this.Controls.Add(this.labShow);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "fMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.fMain_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartInfo)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.Label lbldate;
        private System.Windows.Forms.Label lblchepai;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbCarNo;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTruck;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCartonQty;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPalletQty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgvPartInfo;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.TextBox txtSecurityStatus;
        private System.Windows.Forms.TextBox txtWHStatus;
        private System.Windows.Forms.TextBox txtSecurity;
        private System.Windows.Forms.TextBox txtWH;
        private System.Windows.Forms.Button btnSecurity;
        private System.Windows.Forms.Button btnWH;
        private System.Windows.Forms.TextBox labShow;
    }
}

