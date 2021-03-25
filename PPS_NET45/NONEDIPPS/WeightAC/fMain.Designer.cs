namespace WeightAC
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.chkPAC_HM = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoCOMBluetooth = new System.Windows.Forms.RadioButton();
            this.rdoCOM = new System.Windows.Forms.RadioButton();
            this.rdoBluetooth = new System.Windows.Forms.RadioButton();
            this.rdoWagonBalance = new System.Windows.Forms.RadioButton();
            this.txtDeviation = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.lbl_WeightValue = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtStandard = new System.Windows.Forms.TextBox();
            this.txtUpperWeight = new System.Windows.Forms.TextBox();
            this.lblsxzl = new System.Windows.Forms.Label();
            this.txtLowerWeight = new System.Windows.Forms.TextBox();
            this.lblxxzl = new System.Windows.Forms.Label();
            this.grpPallet = new System.Windows.Forms.GroupBox();
            this.btn_reprintmark = new System.Windows.Forms.Button();
            this.lblSecurity = new System.Windows.Forms.Label();
            this.lblRegion = new System.Windows.Forms.Label();
            this.btn_RePrintHM = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.txtPalletNo = new System.Windows.Forms.TextBox();
            this.btn_RePrint = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.labShow = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.Label();
            this.dgvPalletinfo = new System.Windows.Forms.DataGridView();
            this.customer_sn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.part_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.a_pack_pallet_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.b_pallet_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.b_shipment_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grpShipment = new System.Windows.Forms.GroupBox();
            this.dgvShipment = new System.Windows.Forms.DataGridView();
            this.集货单号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.栈板号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.箱数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.重量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PICK箱数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PACK箱数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHECK结果 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SECURITY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpSN = new System.Windows.Forms.GroupBox();
            this.grpWeight = new System.Windows.Forms.GroupBox();
            this.dgvWeightInfo = new System.Windows.Forms.DataGridView();
            this.ShipMent_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PACK栈板号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.标准重量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.实际重量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.差异值 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.grpInfo.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpPallet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPalletinfo)).BeginInit();
            this.panel2.SuspendLayout();
            this.grpShipment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShipment)).BeginInit();
            this.grpSN.SuspendLayout();
            this.grpWeight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeightInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.grpInfo);
            this.panel1.Controls.Add(this.grpPallet);
            this.panel1.Controls.Add(this.labShow);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(991, 259);
            this.panel1.TabIndex = 38;
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.chkPAC_HM);
            this.grpInfo.Controls.Add(this.groupBox3);
            this.grpInfo.Controls.Add(this.txtDeviation);
            this.grpInfo.Controls.Add(this.label10);
            this.grpInfo.Controls.Add(this.label11);
            this.grpInfo.Controls.Add(this.txtWeight);
            this.grpInfo.Controls.Add(this.lbl_WeightValue);
            this.grpInfo.Controls.Add(this.label6);
            this.grpInfo.Controls.Add(this.txtStandard);
            this.grpInfo.Controls.Add(this.txtUpperWeight);
            this.grpInfo.Controls.Add(this.lblsxzl);
            this.grpInfo.Controls.Add(this.txtLowerWeight);
            this.grpInfo.Controls.Add(this.lblxxzl);
            this.grpInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpInfo.Location = new System.Drawing.Point(0, 99);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(991, 157);
            this.grpInfo.TabIndex = 66;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "信息";
            // 
            // chkPAC_HM
            // 
            this.chkPAC_HM.AutoSize = true;
            this.chkPAC_HM.Checked = true;
            this.chkPAC_HM.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPAC_HM.Location = new System.Drawing.Point(668, 131);
            this.chkPAC_HM.Name = "chkPAC_HM";
            this.chkPAC_HM.Size = new System.Drawing.Size(174, 16);
            this.chkPAC_HM.TabIndex = 39;
            this.chkPAC_HM.Text = "自动打印Handover Manifest";
            this.chkPAC_HM.UseVisualStyleBackColor = true;
            this.chkPAC_HM.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoCOMBluetooth);
            this.groupBox3.Controls.Add(this.rdoCOM);
            this.groupBox3.Controls.Add(this.rdoBluetooth);
            this.groupBox3.Controls.Add(this.rdoWagonBalance);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox3.Location = new System.Drawing.Point(857, 17);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(131, 137);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "秤型号选择";
            // 
            // rdoCOMBluetooth
            // 
            this.rdoCOMBluetooth.AutoSize = true;
            this.rdoCOMBluetooth.Location = new System.Drawing.Point(26, 114);
            this.rdoCOMBluetooth.Name = "rdoCOMBluetooth";
            this.rdoCOMBluetooth.Size = new System.Drawing.Size(83, 16);
            this.rdoCOMBluetooth.TabIndex = 3;
            this.rdoCOMBluetooth.Text = "COM3电子秤";
            this.rdoCOMBluetooth.UseVisualStyleBackColor = true;
            this.rdoCOMBluetooth.CheckedChanged += new System.EventHandler(this.rdoCOMBluetooth_CheckedChanged);
            // 
            // rdoCOM
            // 
            this.rdoCOM.AutoSize = true;
            this.rdoCOM.Location = new System.Drawing.Point(26, 84);
            this.rdoCOM.Name = "rdoCOM";
            this.rdoCOM.Size = new System.Drawing.Size(71, 16);
            this.rdoCOM.TabIndex = 2;
            this.rdoCOM.Text = "COM4地磅";
            this.rdoCOM.UseVisualStyleBackColor = true;
            this.rdoCOM.CheckedChanged += new System.EventHandler(this.rdoCOM_CheckedChanged);
            // 
            // rdoBluetooth
            // 
            this.rdoBluetooth.AutoSize = true;
            this.rdoBluetooth.Location = new System.Drawing.Point(26, 54);
            this.rdoBluetooth.Name = "rdoBluetooth";
            this.rdoBluetooth.Size = new System.Drawing.Size(83, 16);
            this.rdoBluetooth.TabIndex = 1;
            this.rdoBluetooth.Text = "坤宏电子秤";
            this.rdoBluetooth.UseVisualStyleBackColor = true;
            this.rdoBluetooth.CheckedChanged += new System.EventHandler(this.rdoBluetooth_CheckedChanged);
            // 
            // rdoWagonBalance
            // 
            this.rdoWagonBalance.AutoSize = true;
            this.rdoWagonBalance.Checked = true;
            this.rdoWagonBalance.Location = new System.Drawing.Point(26, 24);
            this.rdoWagonBalance.Name = "rdoWagonBalance";
            this.rdoWagonBalance.Size = new System.Drawing.Size(89, 16);
            this.rdoWagonBalance.TabIndex = 0;
            this.rdoWagonBalance.TabStop = true;
            this.rdoWagonBalance.Text = "B6E称重仪表";
            this.rdoWagonBalance.UseVisualStyleBackColor = true;
            this.rdoWagonBalance.CheckedChanged += new System.EventHandler(this.rdoWagonBalance_CheckedChanged);
            // 
            // txtDeviation
            // 
            this.txtDeviation.BackColor = System.Drawing.SystemColors.Control;
            this.txtDeviation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDeviation.Enabled = false;
            this.txtDeviation.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDeviation.ForeColor = System.Drawing.Color.Red;
            this.txtDeviation.Location = new System.Drawing.Point(795, 16);
            this.txtDeviation.Name = "txtDeviation";
            this.txtDeviation.ReadOnly = true;
            this.txtDeviation.Size = new System.Drawing.Size(45, 31);
            this.txtDeviation.TabIndex = 22;
            this.txtDeviation.Text = "3";
            this.txtDeviation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("NSimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(1, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 16);
            this.label10.TabIndex = 23;
            this.label10.Text = "标准重量:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("NSimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(703, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 16);
            this.label11.TabIndex = 23;
            this.label11.Text = "偏差(%):";
            // 
            // txtWeight
            // 
            this.txtWeight.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtWeight.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtWeight.Font = new System.Drawing.Font("NSimSun", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtWeight.Location = new System.Drawing.Point(375, 60);
            this.txtWeight.Multiline = true;
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.ReadOnly = true;
            this.txtWeight.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtWeight.Size = new System.Drawing.Size(283, 87);
            this.txtWeight.TabIndex = 2;
            this.txtWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtWeight.WordWrap = false;
            this.txtWeight.TextChanged += new System.EventHandler(this.txtWeight_TextChanged);
            // 
            // lbl_WeightValue
            // 
            this.lbl_WeightValue.AutoSize = true;
            this.lbl_WeightValue.Font = new System.Drawing.Font("SimSun", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_WeightValue.Location = new System.Drawing.Point(275, 100);
            this.lbl_WeightValue.Name = "lbl_WeightValue";
            this.lbl_WeightValue.Size = new System.Drawing.Size(83, 33);
            this.lbl_WeightValue.TabIndex = 37;
            this.lbl_WeightValue.Text = "0.00";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("SimSun", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(7, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(164, 33);
            this.label6.TabIndex = 22;
            this.label6.Text = "实际重量:";
            // 
            // txtStandard
            // 
            this.txtStandard.BackColor = System.Drawing.Color.Yellow;
            this.txtStandard.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStandard.Enabled = false;
            this.txtStandard.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStandard.Location = new System.Drawing.Point(143, 17);
            this.txtStandard.Name = "txtStandard";
            this.txtStandard.ReadOnly = true;
            this.txtStandard.Size = new System.Drawing.Size(90, 31);
            this.txtStandard.TabIndex = 22;
            this.txtStandard.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtStandard.TextChanged += new System.EventHandler(this.txtStandard_TextChanged);
            // 
            // txtUpperWeight
            // 
            this.txtUpperWeight.BackColor = System.Drawing.SystemColors.Control;
            this.txtUpperWeight.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUpperWeight.Enabled = false;
            this.txtUpperWeight.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUpperWeight.Location = new System.Drawing.Point(353, 16);
            this.txtUpperWeight.Name = "txtUpperWeight";
            this.txtUpperWeight.ReadOnly = true;
            this.txtUpperWeight.Size = new System.Drawing.Size(90, 31);
            this.txtUpperWeight.TabIndex = 36;
            this.txtUpperWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblsxzl
            // 
            this.lblsxzl.AutoSize = true;
            this.lblsxzl.Font = new System.Drawing.Font("NSimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblsxzl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblsxzl.Location = new System.Drawing.Point(231, 24);
            this.lblsxzl.Name = "lblsxzl";
            this.lblsxzl.Size = new System.Drawing.Size(85, 16);
            this.lblsxzl.TabIndex = 32;
            this.lblsxzl.Text = "上限重量:";
            // 
            // txtLowerWeight
            // 
            this.txtLowerWeight.BackColor = System.Drawing.SystemColors.Control;
            this.txtLowerWeight.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLowerWeight.Enabled = false;
            this.txtLowerWeight.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLowerWeight.Location = new System.Drawing.Point(597, 14);
            this.txtLowerWeight.Name = "txtLowerWeight";
            this.txtLowerWeight.ReadOnly = true;
            this.txtLowerWeight.Size = new System.Drawing.Size(90, 31);
            this.txtLowerWeight.TabIndex = 35;
            this.txtLowerWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblxxzl
            // 
            this.lblxxzl.AutoSize = true;
            this.lblxxzl.Font = new System.Drawing.Font("NSimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblxxzl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblxxzl.Location = new System.Drawing.Point(446, 24);
            this.lblxxzl.Name = "lblxxzl";
            this.lblxxzl.Size = new System.Drawing.Size(85, 16);
            this.lblxxzl.TabIndex = 33;
            this.lblxxzl.Text = "下限重量:";
            // 
            // grpPallet
            // 
            this.grpPallet.Controls.Add(this.btn_reprintmark);
            this.grpPallet.Controls.Add(this.lblSecurity);
            this.grpPallet.Controls.Add(this.lblRegion);
            this.grpPallet.Controls.Add(this.btn_RePrintHM);
            this.grpPallet.Controls.Add(this.btnTest);
            this.grpPallet.Controls.Add(this.txtPalletNo);
            this.grpPallet.Controls.Add(this.btn_RePrint);
            this.grpPallet.Controls.Add(this.label4);
            this.grpPallet.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpPallet.Location = new System.Drawing.Point(0, 41);
            this.grpPallet.Name = "grpPallet";
            this.grpPallet.Size = new System.Drawing.Size(991, 58);
            this.grpPallet.TabIndex = 64;
            this.grpPallet.TabStop = false;
            this.grpPallet.Text = "栈板";
            // 
            // btn_reprintmark
            // 
            this.btn_reprintmark.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_reprintmark.Location = new System.Drawing.Point(536, 11);
            this.btn_reprintmark.Name = "btn_reprintmark";
            this.btn_reprintmark.Size = new System.Drawing.Size(144, 41);
            this.btn_reprintmark.TabIndex = 44;
            this.btn_reprintmark.Text = "补列印ShippingMark";
            this.btn_reprintmark.UseVisualStyleBackColor = true;
            this.btn_reprintmark.Click += new System.EventHandler(this.btn_reprintmark_Click);
            // 
            // lblSecurity
            // 
            this.lblSecurity.AutoSize = true;
            this.lblSecurity.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblSecurity.Font = new System.Drawing.Font("NSimSun", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSecurity.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSecurity.Location = new System.Drawing.Point(436, 20);
            this.lblSecurity.Name = "lblSecurity";
            this.lblSecurity.Size = new System.Drawing.Size(0, 20);
            this.lblSecurity.TabIndex = 42;
            // 
            // lblRegion
            // 
            this.lblRegion.AutoSize = true;
            this.lblRegion.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblRegion.Font = new System.Drawing.Font("NSimSun", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRegion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRegion.Location = new System.Drawing.Point(350, 20);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(0, 20);
            this.lblRegion.TabIndex = 41;
            // 
            // btn_RePrintHM
            // 
            this.btn_RePrintHM.Enabled = false;
            this.btn_RePrintHM.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_RePrintHM.Location = new System.Drawing.Point(699, 12);
            this.btn_RePrintHM.Name = "btn_RePrintHM";
            this.btn_RePrintHM.Size = new System.Drawing.Size(177, 41);
            this.btn_RePrintHM.TabIndex = 40;
            this.btn_RePrintHM.Text = "补列印HandoverManifest";
            this.btn_RePrintHM.UseVisualStyleBackColor = true;
            this.btn_RePrintHM.Visible = false;
            this.btn_RePrintHM.Click += new System.EventHandler(this.btn_RePrintHM_Click);
            // 
            // btnTest
            // 
            this.btnTest.Enabled = false;
            this.btnTest.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTest.Location = new System.Drawing.Point(889, 12);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(77, 33);
            this.btnTest.TabIndex = 39;
            this.btnTest.Text = "test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Visible = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtPalletNo
            // 
            this.txtPalletNo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtPalletNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPalletNo.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPalletNo.ForeColor = System.Drawing.Color.Blue;
            this.txtPalletNo.Location = new System.Drawing.Point(98, 15);
            this.txtPalletNo.Name = "txtPalletNo";
            this.txtPalletNo.Size = new System.Drawing.Size(232, 31);
            this.txtPalletNo.TabIndex = 1;
            this.txtPalletNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPalletNo_KeyDown);
            // 
            // btn_RePrint
            // 
            this.btn_RePrint.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_RePrint.Location = new System.Drawing.Point(429, 12);
            this.btn_RePrint.Name = "btn_RePrint";
            this.btn_RePrint.Size = new System.Drawing.Size(86, 41);
            this.btn_RePrint.TabIndex = 38;
            this.btn_RePrint.Text = "补列印";
            this.btn_RePrint.UseVisualStyleBackColor = true;
            this.btn_RePrint.Click += new System.EventHandler(this.btn_RePrint_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("NSimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(6, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 16);
            this.label4.TabIndex = 23;
            this.label4.Text = "栈板号:";
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Enabled = false;
            this.labShow.Font = new System.Drawing.Font("SimSun", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.SystemColors.Info;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Name = "labShow";
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(991, 41);
            this.labShow.TabIndex = 63;
            this.labShow.Text = "Weight";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMessage
            // 
            this.txtMessage.AutoEllipsis = true;
            this.txtMessage.BackColor = System.Drawing.Color.Blue;
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.txtMessage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtMessage.Location = new System.Drawing.Point(0, 568);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(991, 44);
            this.txtMessage.TabIndex = 61;
            this.txtMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvPalletinfo
            // 
            this.dgvPalletinfo.AllowUserToAddRows = false;
            this.dgvPalletinfo.AllowUserToDeleteRows = false;
            this.dgvPalletinfo.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvPalletinfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPalletinfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.customer_sn,
            this.part_no,
            this.wc,
            this.a_pack_pallet_no,
            this.b_pallet_no,
            this.weight,
            this.b_shipment_id});
            this.dgvPalletinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPalletinfo.EnableHeadersVisualStyles = false;
            this.dgvPalletinfo.Location = new System.Drawing.Point(3, 17);
            this.dgvPalletinfo.Name = "dgvPalletinfo";
            this.dgvPalletinfo.ReadOnly = true;
            this.dgvPalletinfo.RowHeadersWidth = 62;
            this.dgvPalletinfo.RowTemplate.Height = 23;
            this.dgvPalletinfo.Size = new System.Drawing.Size(985, 86);
            this.dgvPalletinfo.TabIndex = 62;
            this.dgvPalletinfo.Visible = false;
            // 
            // customer_sn
            // 
            this.customer_sn.HeaderText = "customer_sn";
            this.customer_sn.MinimumWidth = 8;
            this.customer_sn.Name = "customer_sn";
            this.customer_sn.ReadOnly = true;
            this.customer_sn.Width = 150;
            // 
            // part_no
            // 
            this.part_no.HeaderText = "part_no";
            this.part_no.MinimumWidth = 8;
            this.part_no.Name = "part_no";
            this.part_no.ReadOnly = true;
            this.part_no.Width = 200;
            // 
            // wc
            // 
            this.wc.HeaderText = "wc";
            this.wc.MinimumWidth = 8;
            this.wc.Name = "wc";
            this.wc.ReadOnly = true;
            this.wc.Width = 50;
            // 
            // a_pack_pallet_no
            // 
            this.a_pack_pallet_no.HeaderText = "a_pack_pallet_no";
            this.a_pack_pallet_no.MinimumWidth = 8;
            this.a_pack_pallet_no.Name = "a_pack_pallet_no";
            this.a_pack_pallet_no.ReadOnly = true;
            this.a_pack_pallet_no.Width = 120;
            // 
            // b_pallet_no
            // 
            this.b_pallet_no.HeaderText = "b_pallet_no";
            this.b_pallet_no.MinimumWidth = 8;
            this.b_pallet_no.Name = "b_pallet_no";
            this.b_pallet_no.ReadOnly = true;
            this.b_pallet_no.Width = 120;
            // 
            // weight
            // 
            this.weight.HeaderText = "weight";
            this.weight.MinimumWidth = 8;
            this.weight.Name = "weight";
            this.weight.ReadOnly = true;
            this.weight.Width = 150;
            // 
            // b_shipment_id
            // 
            this.b_shipment_id.HeaderText = "b_shipment_id";
            this.b_shipment_id.MinimumWidth = 8;
            this.b_shipment_id.Name = "b_shipment_id";
            this.b_shipment_id.ReadOnly = true;
            this.b_shipment_id.Width = 200;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.grpShipment);
            this.panel2.Controls.Add(this.grpSN);
            this.panel2.Controls.Add(this.grpWeight);
            this.panel2.Controls.Add(this.txtMessage);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(991, 612);
            this.panel2.TabIndex = 39;
            // 
            // grpShipment
            // 
            this.grpShipment.Controls.Add(this.dgvShipment);
            this.grpShipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpShipment.Location = new System.Drawing.Point(0, 348);
            this.grpShipment.Name = "grpShipment";
            this.grpShipment.Size = new System.Drawing.Size(991, 114);
            this.grpShipment.TabIndex = 66;
            this.grpShipment.TabStop = false;
            this.grpShipment.Text = "集货单信息";
            // 
            // dgvShipment
            // 
            this.dgvShipment.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvShipment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShipment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.集货单号,
            this.栈板号,
            this.箱数,
            this.重量,
            this.PICK箱数,
            this.PACK箱数,
            this.CHECK结果,
            this.SECURITY});
            this.dgvShipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShipment.Location = new System.Drawing.Point(3, 17);
            this.dgvShipment.Name = "dgvShipment";
            this.dgvShipment.RowHeadersWidth = 62;
            this.dgvShipment.RowTemplate.Height = 23;
            this.dgvShipment.Size = new System.Drawing.Size(985, 94);
            this.dgvShipment.TabIndex = 0;
            // 
            // 集货单号
            // 
            this.集货单号.HeaderText = "集货单号";
            this.集货单号.MinimumWidth = 8;
            this.集货单号.Name = "集货单号";
            this.集货单号.Width = 150;
            // 
            // 栈板号
            // 
            this.栈板号.HeaderText = "栈板号";
            this.栈板号.MinimumWidth = 8;
            this.栈板号.Name = "栈板号";
            this.栈板号.Width = 150;
            // 
            // 箱数
            // 
            this.箱数.HeaderText = "箱数";
            this.箱数.MinimumWidth = 8;
            this.箱数.Name = "箱数";
            this.箱数.Width = 150;
            // 
            // 重量
            // 
            this.重量.HeaderText = "重量";
            this.重量.MinimumWidth = 8;
            this.重量.Name = "重量";
            this.重量.Width = 150;
            // 
            // PICK箱数
            // 
            this.PICK箱数.HeaderText = "PICK箱数";
            this.PICK箱数.MinimumWidth = 8;
            this.PICK箱数.Name = "PICK箱数";
            this.PICK箱数.Width = 150;
            // 
            // PACK箱数
            // 
            this.PACK箱数.HeaderText = "PACK箱数";
            this.PACK箱数.MinimumWidth = 8;
            this.PACK箱数.Name = "PACK箱数";
            this.PACK箱数.Width = 150;
            // 
            // CHECK结果
            // 
            this.CHECK结果.HeaderText = "CHECK结果";
            this.CHECK结果.MinimumWidth = 8;
            this.CHECK结果.Name = "CHECK结果";
            this.CHECK结果.Width = 150;
            // 
            // SECURITY
            // 
            this.SECURITY.HeaderText = "SECURITY";
            this.SECURITY.MinimumWidth = 8;
            this.SECURITY.Name = "SECURITY";
            this.SECURITY.Width = 150;
            // 
            // grpSN
            // 
            this.grpSN.Controls.Add(this.dgvPalletinfo);
            this.grpSN.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpSN.Location = new System.Drawing.Point(0, 462);
            this.grpSN.Name = "grpSN";
            this.grpSN.Size = new System.Drawing.Size(991, 106);
            this.grpSN.TabIndex = 65;
            this.grpSN.TabStop = false;
            this.grpSN.Text = "站别异常的SN";
            // 
            // grpWeight
            // 
            this.grpWeight.Controls.Add(this.dgvWeightInfo);
            this.grpWeight.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpWeight.Location = new System.Drawing.Point(0, 259);
            this.grpWeight.Name = "grpWeight";
            this.grpWeight.Size = new System.Drawing.Size(991, 89);
            this.grpWeight.TabIndex = 64;
            this.grpWeight.TabStop = false;
            this.grpWeight.Text = "称重重量";
            // 
            // dgvWeightInfo
            // 
            this.dgvWeightInfo.AllowUserToAddRows = false;
            this.dgvWeightInfo.AllowUserToDeleteRows = false;
            this.dgvWeightInfo.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvWeightInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvWeightInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWeightInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ShipMent_id,
            this.PACK栈板号,
            this.标准重量,
            this.实际重量,
            this.差异值});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvWeightInfo.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvWeightInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWeightInfo.EnableHeadersVisualStyles = false;
            this.dgvWeightInfo.Location = new System.Drawing.Point(3, 17);
            this.dgvWeightInfo.Name = "dgvWeightInfo";
            this.dgvWeightInfo.ReadOnly = true;
            this.dgvWeightInfo.RowHeadersWidth = 62;
            this.dgvWeightInfo.RowTemplate.Height = 23;
            this.dgvWeightInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvWeightInfo.Size = new System.Drawing.Size(985, 69);
            this.dgvWeightInfo.TabIndex = 63;
            this.dgvWeightInfo.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvWeightInfo_RowStateChanged);
            // 
            // ShipMent_id
            // 
            this.ShipMent_id.HeaderText = "ShipMent_id";
            this.ShipMent_id.MinimumWidth = 8;
            this.ShipMent_id.Name = "ShipMent_id";
            this.ShipMent_id.ReadOnly = true;
            this.ShipMent_id.Width = 200;
            // 
            // PACK栈板号
            // 
            this.PACK栈板号.HeaderText = "PACK栈板号";
            this.PACK栈板号.MinimumWidth = 8;
            this.PACK栈板号.Name = "PACK栈板号";
            this.PACK栈板号.ReadOnly = true;
            this.PACK栈板号.Width = 200;
            // 
            // 标准重量
            // 
            this.标准重量.HeaderText = "标准重量";
            this.标准重量.MinimumWidth = 8;
            this.标准重量.Name = "标准重量";
            this.标准重量.ReadOnly = true;
            this.标准重量.Width = 150;
            // 
            // 实际重量
            // 
            this.实际重量.HeaderText = "实际重量";
            this.实际重量.MinimumWidth = 8;
            this.实际重量.Name = "实际重量";
            this.实际重量.ReadOnly = true;
            this.实际重量.Width = 150;
            // 
            // 差异值
            // 
            this.差异值.HeaderText = "差异值";
            this.差异值.MinimumWidth = 8;
            this.差异值.Name = "差异值";
            this.差异值.ReadOnly = true;
            this.差异值.Width = 150;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 612);
            this.Controls.Add(this.panel2);
            this.KeyPreview = true;
            this.Name = "fMain";
            this.Text = "Ver:1.0.0.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grpPallet.ResumeLayout(false);
            this.grpPallet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPalletinfo)).EndInit();
            this.panel2.ResumeLayout(false);
            this.grpShipment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShipment)).EndInit();
            this.grpSN.ResumeLayout(false);
            this.grpWeight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeightInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label txtMessage;
        private System.Windows.Forms.DataGridView dgvPalletinfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_sn;
        private System.Windows.Forms.DataGridViewTextBoxColumn part_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn wc;
        private System.Windows.Forms.DataGridViewTextBoxColumn a_pack_pallet_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn b_pallet_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn weight;
        private System.Windows.Forms.DataGridViewTextBoxColumn b_shipment_id;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox grpPallet;
        private System.Windows.Forms.TextBox txtPalletNo;
        private System.Windows.Forms.Button btn_RePrint;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.TextBox txtDeviation;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.Label lbl_WeightValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtStandard;
        private System.Windows.Forms.TextBox txtUpperWeight;
        private System.Windows.Forms.Label lblsxzl;
        private System.Windows.Forms.TextBox txtLowerWeight;
        private System.Windows.Forms.Label lblxxzl;
        private System.Windows.Forms.DataGridView dgvWeightInfo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoBluetooth;
        private System.Windows.Forms.RadioButton rdoWagonBalance;
        private System.Windows.Forms.RadioButton rdoCOM;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.RadioButton rdoCOMBluetooth;
        private System.Windows.Forms.CheckBox chkPAC_HM;
        private System.Windows.Forms.Button btn_RePrintHM;
        private System.Windows.Forms.GroupBox grpShipment;
        private System.Windows.Forms.DataGridView dgvShipment;
        private System.Windows.Forms.GroupBox grpSN;
        private System.Windows.Forms.GroupBox grpWeight;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShipMent_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn PACK栈板号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 标准重量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 实际重量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 差异值;
        private System.Windows.Forms.DataGridViewTextBoxColumn 集货单号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 栈板号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 箱数;
        private System.Windows.Forms.DataGridViewTextBoxColumn 重量;
        private System.Windows.Forms.DataGridViewTextBoxColumn PICK箱数;
        private System.Windows.Forms.DataGridViewTextBoxColumn PACK箱数;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHECK结果;
        private System.Windows.Forms.DataGridViewTextBoxColumn SECURITY;
        private System.Windows.Forms.Label lblSecurity;
        private System.Windows.Forms.Button btn_reprintmark;
    }
}
