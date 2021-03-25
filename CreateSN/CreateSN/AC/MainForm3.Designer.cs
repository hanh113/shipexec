namespace CreatePPSDataAC
{
  partial class MainForm3
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
    /// 设计器支持所需的方法 - 不要
    /// 使用代码编辑器修改此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm3));
            this.cmbCartonQty = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCartons = new System.Windows.Forms.TextBox();
            this.btnCreateCarton = new System.Windows.Forms.Button();
            this.btnPrintCarton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClearLocation = new System.Windows.Forms.Button();
            this.nudCartonCount = new System.Windows.Forms.NumericUpDown();
            this.cmbPartno = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtGTIN = new System.Windows.Forms.TextBox();
            this.lblModel = new System.Windows.Forms.Label();
            this.lblJAN = new System.Windows.Forms.Label();
            this.lblUPC = new System.Windows.Forms.Label();
            this.txtCartonRePrint = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvPART = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoCSN2 = new System.Windows.Forms.RadioButton();
            this.rdoCSN = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labSCC14 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rdoSCCartonA = new System.Windows.Forms.RadioButton();
            this.rdoSCCarton = new System.Windows.Forms.RadioButton();
            this.rdoNCarton = new System.Windows.Forms.RadioButton();
            this.btnCreateCarton2 = new System.Windows.Forms.Button();
            this.btnADD = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rdoListLocation = new System.Windows.Forms.RadioButton();
            this.rdoOneLocation = new System.Windows.Forms.RadioButton();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.chkGS1Label = new System.Windows.Forms.CheckBox();
            this.chkCSNListLabel = new System.Windows.Forms.CheckBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudCartonCount)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPART)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.grpList.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbCartonQty
            // 
            this.cmbCartonQty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCartonQty.Enabled = false;
            this.cmbCartonQty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCartonQty.FormattingEnabled = true;
            this.cmbCartonQty.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmbCartonQty.Location = new System.Drawing.Point(410, 65);
            this.cmbCartonQty.Name = "cmbCartonQty";
            this.cmbCartonQty.Size = new System.Drawing.Size(58, 20);
            this.cmbCartonQty.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(345, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 36;
            this.label3.Text = "每箱数量:";
            // 
            // txtCartons
            // 
            this.txtCartons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCartons.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCartons.Location = new System.Drawing.Point(3, 17);
            this.txtCartons.Multiline = true;
            this.txtCartons.Name = "txtCartons";
            this.txtCartons.Size = new System.Drawing.Size(201, 109);
            this.txtCartons.TabIndex = 8;
            // 
            // btnCreateCarton
            // 
            this.btnCreateCarton.Location = new System.Drawing.Point(318, 15);
            this.btnCreateCarton.Name = "btnCreateCarton";
            this.btnCreateCarton.Size = new System.Drawing.Size(75, 23);
            this.btnCreateCarton.TabIndex = 5;
            this.btnCreateCarton.Text = "单库位产生";
            this.btnCreateCarton.UseVisualStyleBackColor = true;
            this.btnCreateCarton.Click += new System.EventHandler(this.btnCreateCarton_Click);
            // 
            // btnPrintCarton
            // 
            this.btnPrintCarton.Enabled = false;
            this.btnPrintCarton.Location = new System.Drawing.Point(176, 73);
            this.btnPrintCarton.Name = "btnPrintCarton";
            this.btnPrintCarton.Size = new System.Drawing.Size(62, 23);
            this.btnPrintCarton.TabIndex = 7;
            this.btnPrintCarton.Text = "打印箱号";
            this.btnPrintCarton.UseVisualStyleBackColor = true;
            this.btnPrintCarton.Click += new System.EventHandler(this.btnPrintCarton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(19, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 42;
            this.label4.Text = "需求箱数:";
            // 
            // btnClearLocation
            // 
            this.btnClearLocation.Enabled = false;
            this.btnClearLocation.Location = new System.Drawing.Point(117, 73);
            this.btnClearLocation.Name = "btnClearLocation";
            this.btnClearLocation.Size = new System.Drawing.Size(62, 23);
            this.btnClearLocation.TabIndex = 6;
            this.btnClearLocation.Text = "清空储位";
            this.btnClearLocation.UseVisualStyleBackColor = true;
            this.btnClearLocation.Click += new System.EventHandler(this.btnClearLocation_Click);
            // 
            // nudCartonCount
            // 
            this.nudCartonCount.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudCartonCount.Location = new System.Drawing.Point(121, 15);
            this.nudCartonCount.Maximum = new decimal(new int[] {
            1050,
            0,
            0,
            0});
            this.nudCartonCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCartonCount.Name = "nudCartonCount";
            this.nudCartonCount.Size = new System.Drawing.Size(81, 26);
            this.nudCartonCount.TabIndex = 2;
            this.nudCartonCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbPartno
            // 
            this.cmbPartno.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPartno.FormattingEnabled = true;
            this.cmbPartno.Items.AddRange(new object[] {
            "L1SWC013-CS-H-AME*MMEF2AM/A",
            "L1SWC013-CS-H-BES*MMEF2BE/A",
            "L1SWC013-CS-H-CAN*MMEF2C/A",
            "L1SWC013-CS-H-CHN*MMEF2CH/A",
            "L1SWC013-CS-H-HIN*MMEF2HN/A",
            "L1SWC013-CS-H-IND*MMEF2ID/A",
            "L1SWC013-CS-H-JPN*MMEF2J/A",
            "L1SWC013-CS-H-KOR*MMEF2KH/A",
            "L1SWC013-CS-H-TUR*MMEF2TU/A",
            "L1SWC013-CS-H-TWN*MMEF2TA/A",
            "L1SWC013-CS-H-ZEE*MMEF2ZE/A",
            "L1SWC013-CS-H-ZML*MMEF2ZM/A",
            "L1SWC013-CS-H-ITS*MMEF2ZA/A",
            "L1SEB001-CS-H-AME*MMEF2AM/A",
            "L1SEB001-CS-H-BES*MMEF2BE/A",
            "L1SEB001-CS-H-CAN*MMEF2C/A",
            "L1SEB001-CS-H-CHN*MMEF2CH/A",
            "L1SEB001-CS-H-HIN*MMEF2HN/A",
            "L1SEB001-CS-H-IND*MMEF2ID/A",
            "L1SEB001-CS-H-JPN*MMEF2J/A",
            "L1SEB001-CS-H-KOR*MMEF2KH/A",
            "L1SEB001-CS-H-TUR*MMEF2TU/A",
            "L1SEB001-CS-H-TWN*MMEF2TA/A",
            "L1SEB001-CS-H-ZEE*MMEF2ZE/A",
            "L1SEB001-CS-H-ZML*MMEF2ZM/A",
            "L1SEB001-CS-H-ITS*MMEF2ZA/A"});
            this.cmbPartno.Location = new System.Drawing.Point(78, 13);
            this.cmbPartno.Name = "cmbPartno";
            this.cmbPartno.Size = new System.Drawing.Size(390, 23);
            this.cmbPartno.TabIndex = 47;
            this.cmbPartno.SelectedIndexChanged += new System.EventHandler(this.cmbPartno_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(49, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(62, 23);
            this.button1.TabIndex = 48;
            this.button1.Text = "产生CSN";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbLocation
            // 
            this.cmbLocation.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Items.AddRange(new object[] {
            "L1SWC013-CS-H-AME*MMEF2AM/A",
            "L1SWC013-CS-H-BES*MMEF2BE/A",
            "L1SWC013-CS-H-CAN*MMEF2C/A",
            "L1SWC013-CS-H-CHN*MMEF2CH/A",
            "L1SWC013-CS-H-HIN*MMEF2HN/A",
            "L1SWC013-CS-H-IND*MMEF2ID/A",
            "L1SWC013-CS-H-JPN*MMEF2J/A",
            "L1SWC013-CS-H-KOR*MMEF2KH/A",
            "L1SWC013-CS-H-TUR*MMEF2TU/A",
            "L1SWC013-CS-H-TWN*MMEF2TA/A",
            "L1SWC013-CS-H-ZEE*MMEF2ZE/A",
            "L1SWC013-CS-H-ZML*MMEF2ZM/A",
            "L1SWC013-CS-H-ITS*MMEF2ZA/A",
            "L1SEB001-CS-H-AME*MMEF2AM/A",
            "L1SEB001-CS-H-BES*MMEF2BE/A",
            "L1SEB001-CS-H-CAN*MMEF2C/A",
            "L1SEB001-CS-H-CHN*MMEF2CH/A",
            "L1SEB001-CS-H-HIN*MMEF2HN/A",
            "L1SEB001-CS-H-IND*MMEF2ID/A",
            "L1SEB001-CS-H-JPN*MMEF2J/A",
            "L1SEB001-CS-H-KOR*MMEF2KH/A",
            "L1SEB001-CS-H-TUR*MMEF2TU/A",
            "L1SEB001-CS-H-TWN*MMEF2TA/A",
            "L1SEB001-CS-H-ZEE*MMEF2ZE/A",
            "L1SEB001-CS-H-ZML*MMEF2ZM/A",
            "L1SEB001-CS-H-ITS*MMEF2ZA/A"});
            this.cmbLocation.Location = new System.Drawing.Point(6, 17);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(230, 23);
            this.cmbLocation.TabIndex = 49;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(252, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 53;
            this.label6.Text = "GTIN:";
            // 
            // txtGTIN
            // 
            this.txtGTIN.Enabled = false;
            this.txtGTIN.Location = new System.Drawing.Point(295, 41);
            this.txtGTIN.Name = "txtGTIN";
            this.txtGTIN.Size = new System.Drawing.Size(173, 21);
            this.txtGTIN.TabIndex = 52;
            this.txtGTIN.Text = "0";
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(78, 47);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(11, 12);
            this.lblModel.TabIndex = 55;
            this.lblModel.Text = "0";
            // 
            // lblJAN
            // 
            this.lblJAN.AutoSize = true;
            this.lblJAN.Location = new System.Drawing.Point(230, 71);
            this.lblJAN.Name = "lblJAN";
            this.lblJAN.Size = new System.Drawing.Size(11, 12);
            this.lblJAN.TabIndex = 56;
            this.lblJAN.Text = "0";
            // 
            // lblUPC
            // 
            this.lblUPC.AutoSize = true;
            this.lblUPC.Location = new System.Drawing.Point(81, 71);
            this.lblUPC.Name = "lblUPC";
            this.lblUPC.Size = new System.Drawing.Size(11, 12);
            this.lblUPC.TabIndex = 57;
            this.lblUPC.Text = "0";
            // 
            // txtCartonRePrint
            // 
            this.txtCartonRePrint.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCartonRePrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCartonRePrint.Location = new System.Drawing.Point(6, 23);
            this.txtCartonRePrint.MaxLength = 20;
            this.txtCartonRePrint.Name = "txtCartonRePrint";
            this.txtCartonRePrint.Size = new System.Drawing.Size(248, 26);
            this.txtCartonRePrint.TabIndex = 58;
            this.txtCartonRePrint.TabStop = false;
            this.txtCartonRePrint.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCartonRePrint_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCartonRePrint);
            this.groupBox1.Controls.Add(this.btnPrintCarton);
            this.groupBox1.Controls.Add(this.btnClearLocation);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(222, 439);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 127);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "输入箱号补列印";
            // 
            // dgvPART
            // 
            this.dgvPART.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvPART.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPART.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPART.Enabled = false;
            this.dgvPART.Location = new System.Drawing.Point(3, 17);
            this.dgvPART.Name = "dgvPART";
            this.dgvPART.RowHeadersWidth = 51;
            this.dgvPART.RowTemplate.Height = 23;
            this.dgvPART.Size = new System.Drawing.Size(531, 125);
            this.dgvPART.TabIndex = 60;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoCSN2);
            this.groupBox2.Controls.Add(this.rdoCSN);
            this.groupBox2.Location = new System.Drawing.Point(8, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(152, 36);
            this.groupBox2.TabIndex = 61;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CSN类型";
            // 
            // rdoCSN2
            // 
            this.rdoCSN2.AutoSize = true;
            this.rdoCSN2.Enabled = false;
            this.rdoCSN2.Location = new System.Drawing.Point(83, 12);
            this.rdoCSN2.Name = "rdoCSN2";
            this.rdoCSN2.Size = new System.Drawing.Size(53, 16);
            this.rdoCSN2.TabIndex = 1;
            this.rdoCSN2.Text = "CSN17";
            this.rdoCSN2.UseVisualStyleBackColor = true;
            // 
            // rdoCSN
            // 
            this.rdoCSN.AutoSize = true;
            this.rdoCSN.Checked = true;
            this.rdoCSN.Location = new System.Drawing.Point(21, 12);
            this.rdoCSN.Name = "rdoCSN";
            this.rdoCSN.Size = new System.Drawing.Size(53, 16);
            this.rdoCSN.TabIndex = 0;
            this.rdoCSN.TabStop = true;
            this.rdoCSN.Text = "CSN12";
            this.rdoCSN.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labSCC14);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cmbCartonQty);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.cmbPartno);
            this.groupBox3.Controls.Add(this.txtGTIN);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.lblModel);
            this.groupBox3.Controls.Add(this.lblUPC);
            this.groupBox3.Controls.Add(this.lblJAN);
            this.groupBox3.Location = new System.Drawing.Point(8, 49);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(537, 89);
            this.groupBox3.TabIndex = 62;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "料号";
            // 
            // labSCC14
            // 
            this.labSCC14.AutoSize = true;
            this.labSCC14.Location = new System.Drawing.Point(496, 69);
            this.labSCC14.Name = "labSCC14";
            this.labSCC14.Size = new System.Drawing.Size(11, 12);
            this.labSCC14.TabIndex = 62;
            this.labSCC14.Text = "0";
            this.labSCC14.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(194, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 61;
            this.label9.Text = "JAN:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 60;
            this.label8.Text = "ICTPN:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(45, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 59;
            this.label7.Text = "UPC:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 58;
            this.label1.Text = "CustModel:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmbLocation);
            this.groupBox4.Location = new System.Drawing.Point(240, 145);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(302, 45);
            this.groupBox4.TabIndex = 63;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "库位";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rdoSCCartonA);
            this.groupBox5.Controls.Add(this.rdoSCCarton);
            this.groupBox5.Controls.Add(this.rdoNCarton);
            this.groupBox5.Location = new System.Drawing.Point(166, 8);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(376, 33);
            this.groupBox5.TabIndex = 64;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "箱号类型";
            // 
            // rdoSCCartonA
            // 
            this.rdoSCCartonA.AutoSize = true;
            this.rdoSCCartonA.Location = new System.Drawing.Point(183, 12);
            this.rdoSCCartonA.Name = "rdoSCCartonA";
            this.rdoSCCartonA.Size = new System.Drawing.Size(131, 16);
            this.rdoSCCartonA.TabIndex = 49;
            this.rdoSCCartonA.Text = "客户指定SSCC18箱号";
            this.rdoSCCartonA.UseVisualStyleBackColor = true;
            // 
            // rdoSCCarton
            // 
            this.rdoSCCarton.AutoSize = true;
            this.rdoSCCarton.Location = new System.Drawing.Point(96, 12);
            this.rdoSCCarton.Name = "rdoSCCarton";
            this.rdoSCCarton.Size = new System.Drawing.Size(83, 16);
            this.rdoSCCarton.TabIndex = 48;
            this.rdoSCCarton.Text = "SSCC18箱号";
            this.rdoSCCarton.UseVisualStyleBackColor = true;
            // 
            // rdoNCarton
            // 
            this.rdoNCarton.AutoSize = true;
            this.rdoNCarton.Checked = true;
            this.rdoNCarton.Location = new System.Drawing.Point(6, 12);
            this.rdoNCarton.Name = "rdoNCarton";
            this.rdoNCarton.Size = new System.Drawing.Size(83, 16);
            this.rdoNCarton.TabIndex = 47;
            this.rdoNCarton.TabStop = true;
            this.rdoNCarton.Text = "自定义箱号";
            this.rdoNCarton.UseVisualStyleBackColor = true;
            // 
            // btnCreateCarton2
            // 
            this.btnCreateCarton2.Enabled = false;
            this.btnCreateCarton2.Location = new System.Drawing.Point(377, 52);
            this.btnCreateCarton2.Name = "btnCreateCarton2";
            this.btnCreateCarton2.Size = new System.Drawing.Size(75, 21);
            this.btnCreateCarton2.TabIndex = 66;
            this.btnCreateCarton2.Text = "多库位产生";
            this.btnCreateCarton2.UseVisualStyleBackColor = true;
            this.btnCreateCarton2.Click += new System.EventHandler(this.btnCreateCarton2_Click);
            // 
            // btnADD
            // 
            this.btnADD.Enabled = false;
            this.btnADD.Location = new System.Drawing.Point(254, 52);
            this.btnADD.Name = "btnADD";
            this.btnADD.Size = new System.Drawing.Size(75, 21);
            this.btnADD.TabIndex = 50;
            this.btnADD.Text = "ADD";
            this.btnADD.UseVisualStyleBackColor = true;
            this.btnADD.Click += new System.EventHandler(this.btnADD_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rdoListLocation);
            this.groupBox6.Controls.Add(this.rdoOneLocation);
            this.groupBox6.Location = new System.Drawing.Point(8, 145);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(223, 45);
            this.groupBox6.TabIndex = 67;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "作业分类";
            // 
            // rdoListLocation
            // 
            this.rdoListLocation.AutoSize = true;
            this.rdoListLocation.Enabled = false;
            this.rdoListLocation.Location = new System.Drawing.Point(113, 17);
            this.rdoListLocation.Name = "rdoListLocation";
            this.rdoListLocation.Size = new System.Drawing.Size(83, 16);
            this.rdoListLocation.TabIndex = 1;
            this.rdoListLocation.Text = "多库位作业";
            this.rdoListLocation.UseVisualStyleBackColor = true;
            // 
            // rdoOneLocation
            // 
            this.rdoOneLocation.AutoSize = true;
            this.rdoOneLocation.Checked = true;
            this.rdoOneLocation.Location = new System.Drawing.Point(24, 17);
            this.rdoOneLocation.Name = "rdoOneLocation";
            this.rdoOneLocation.Size = new System.Drawing.Size(83, 16);
            this.rdoOneLocation.TabIndex = 0;
            this.rdoOneLocation.TabStop = true;
            this.rdoOneLocation.Text = "单库位作业";
            this.rdoOneLocation.UseVisualStyleBackColor = true;
            this.rdoOneLocation.CheckedChanged += new System.EventHandler(this.rdoOneLocation_CheckedChanged);
            // 
            // grpList
            // 
            this.grpList.Controls.Add(this.dgvPART);
            this.grpList.Location = new System.Drawing.Point(8, 287);
            this.grpList.Name = "grpList";
            this.grpList.Size = new System.Drawing.Size(537, 145);
            this.grpList.TabIndex = 68;
            this.grpList.TabStop = false;
            this.grpList.Text = "产生序号库位 List0:";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.txtCartons);
            this.groupBox8.Location = new System.Drawing.Point(8, 439);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(207, 129);
            this.groupBox8.TabIndex = 69;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "CartonNO List:";
            // 
            // chkGS1Label
            // 
            this.chkGS1Label.AutoSize = true;
            this.chkGS1Label.Location = new System.Drawing.Point(22, 57);
            this.chkGS1Label.Name = "chkGS1Label";
            this.chkGS1Label.Size = new System.Drawing.Size(72, 16);
            this.chkGS1Label.TabIndex = 70;
            this.chkGS1Label.Text = "GS1Label";
            this.chkGS1Label.UseVisualStyleBackColor = true;
            // 
            // chkCSNListLabel
            // 
            this.chkCSNListLabel.AutoSize = true;
            this.chkCSNListLabel.Location = new System.Drawing.Point(121, 57);
            this.chkCSNListLabel.Name = "chkCSNListLabel";
            this.chkCSNListLabel.Size = new System.Drawing.Size(90, 16);
            this.chkCSNListLabel.TabIndex = 71;
            this.chkCSNListLabel.Text = "SNListLabel";
            this.chkCSNListLabel.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.chkCSNListLabel);
            this.groupBox9.Controls.Add(this.nudCartonCount);
            this.groupBox9.Controls.Add(this.chkGS1Label);
            this.groupBox9.Controls.Add(this.label4);
            this.groupBox9.Controls.Add(this.btnADD);
            this.groupBox9.Controls.Add(this.btnCreateCarton2);
            this.groupBox9.Controls.Add(this.btnCreateCarton);
            this.groupBox9.Location = new System.Drawing.Point(8, 197);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(534, 84);
            this.groupBox9.TabIndex = 72;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "产生箱号";
            // 
            // MainForm3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 639);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.grpList);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm3";
            this.Text = "NONEDIPPS产生一般品序号";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudCartonCount)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPART)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.grpList.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ComboBox cmbCartonQty;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtCartons;
    private System.Windows.Forms.Button btnCreateCarton;
    private System.Windows.Forms.Button btnPrintCarton;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button btnClearLocation;
    private System.Windows.Forms.NumericUpDown nudCartonCount;
        private System.Windows.Forms.ComboBox cmbPartno;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtGTIN;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.Label lblJAN;
        private System.Windows.Forms.Label lblUPC;
        private System.Windows.Forms.TextBox txtCartonRePrint;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvPART;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoCSN2;
        private System.Windows.Forms.RadioButton rdoCSN;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rdoSCCartonA;
        private System.Windows.Forms.RadioButton rdoSCCarton;
        private System.Windows.Forms.RadioButton rdoNCarton;
        private System.Windows.Forms.Button btnCreateCarton2;
        private System.Windows.Forms.Button btnADD;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rdoListLocation;
        private System.Windows.Forms.RadioButton rdoOneLocation;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox chkGS1Label;
        private System.Windows.Forms.CheckBox chkCSNListLabel;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label labSCC14;
    }
}

