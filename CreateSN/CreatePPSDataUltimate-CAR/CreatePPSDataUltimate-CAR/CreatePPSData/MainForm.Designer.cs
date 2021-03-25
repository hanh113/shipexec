namespace CreatePPSData
{
  partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
            this.btn_search = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cmbCoo = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
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
            this.button2 = new System.Windows.Forms.Button();
            this.btn_GetAll = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lblMaxQty = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblCurrCarqty = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblDNqty = new System.Windows.Forms.Label();
            this.lblDNcaton = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbCAR = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbDN = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
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
            this.groupBox7.SuspendLayout();
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
            this.cmbCartonQty.Location = new System.Drawing.Point(547, 82);
            this.cmbCartonQty.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbCartonQty.Name = "cmbCartonQty";
            this.cmbCartonQty.Size = new System.Drawing.Size(76, 23);
            this.cmbCartonQty.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(460, 87);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 36;
            this.label3.Text = "每箱数量:";
            // 
            // txtCartons
            // 
            this.txtCartons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCartons.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCartons.Location = new System.Drawing.Point(4, 21);
            this.txtCartons.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtCartons.Multiline = true;
            this.txtCartons.Name = "txtCartons";
            this.txtCartons.Size = new System.Drawing.Size(268, 83);
            this.txtCartons.TabIndex = 8;
            // 
            // btnCreateCarton
            // 
            this.btnCreateCarton.Location = new System.Drawing.Point(424, 18);
            this.btnCreateCarton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCreateCarton.Name = "btnCreateCarton";
            this.btnCreateCarton.Size = new System.Drawing.Size(100, 28);
            this.btnCreateCarton.TabIndex = 5;
            this.btnCreateCarton.Text = "单库位产生";
            this.btnCreateCarton.UseVisualStyleBackColor = true;
            this.btnCreateCarton.Click += new System.EventHandler(this.btnCreateCarton_Click);
            // 
            // btnPrintCarton
            // 
            this.btnPrintCarton.Enabled = false;
            this.btnPrintCarton.Location = new System.Drawing.Point(235, 68);
            this.btnPrintCarton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPrintCarton.Name = "btnPrintCarton";
            this.btnPrintCarton.Size = new System.Drawing.Size(83, 28);
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
            this.label4.Location = new System.Drawing.Point(25, 32);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 20);
            this.label4.TabIndex = 42;
            this.label4.Text = "需求箱数:";
            // 
            // btnClearLocation
            // 
            this.btnClearLocation.Enabled = false;
            this.btnClearLocation.Location = new System.Drawing.Point(156, 68);
            this.btnClearLocation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClearLocation.Name = "btnClearLocation";
            this.btnClearLocation.Size = new System.Drawing.Size(83, 28);
            this.btnClearLocation.TabIndex = 6;
            this.btnClearLocation.Text = "清空储位";
            this.btnClearLocation.UseVisualStyleBackColor = true;
            this.btnClearLocation.Click += new System.EventHandler(this.btnClearLocation_Click);
            // 
            // nudCartonCount
            // 
            this.nudCartonCount.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudCartonCount.Location = new System.Drawing.Point(162, 18);
            this.nudCartonCount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
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
            this.nudCartonCount.Size = new System.Drawing.Size(108, 30);
            this.nudCartonCount.TabIndex = 2;
            this.nudCartonCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbPartno
            // 
            this.cmbPartno.Enabled = false;
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
            this.cmbPartno.Location = new System.Drawing.Point(104, 17);
            this.cmbPartno.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbPartno.Name = "cmbPartno";
            this.cmbPartno.Size = new System.Drawing.Size(519, 27);
            this.cmbPartno.TabIndex = 47;
            this.cmbPartno.SelectedIndexChanged += new System.EventHandler(this.cmbPartno_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(66, 68);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 28);
            this.button1.TabIndex = 48;
            this.button1.Text = "产生CSN";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbLocation
            // 
            this.cmbLocation.Enabled = false;
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
            this.cmbLocation.Location = new System.Drawing.Point(8, 22);
            this.cmbLocation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(305, 27);
            this.cmbLocation.TabIndex = 49;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(336, 58);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 15);
            this.label6.TabIndex = 53;
            this.label6.Text = "GTIN:";
            // 
            // txtGTIN
            // 
            this.txtGTIN.Enabled = false;
            this.txtGTIN.Location = new System.Drawing.Point(393, 52);
            this.txtGTIN.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtGTIN.Name = "txtGTIN";
            this.txtGTIN.Size = new System.Drawing.Size(230, 25);
            this.txtGTIN.TabIndex = 52;
            this.txtGTIN.Text = "0";
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(104, 58);
            this.lblModel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(15, 15);
            this.lblModel.TabIndex = 55;
            this.lblModel.Text = "0";
            // 
            // lblJAN
            // 
            this.lblJAN.AutoSize = true;
            this.lblJAN.Location = new System.Drawing.Point(307, 87);
            this.lblJAN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblJAN.Name = "lblJAN";
            this.lblJAN.Size = new System.Drawing.Size(15, 15);
            this.lblJAN.TabIndex = 56;
            this.lblJAN.Text = "0";
            // 
            // lblUPC
            // 
            this.lblUPC.AutoSize = true;
            this.lblUPC.Location = new System.Drawing.Point(108, 87);
            this.lblUPC.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUPC.Name = "lblUPC";
            this.lblUPC.Size = new System.Drawing.Size(15, 15);
            this.lblUPC.TabIndex = 57;
            this.lblUPC.Text = "0";
            // 
            // txtCartonRePrint
            // 
            this.txtCartonRePrint.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCartonRePrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCartonRePrint.Location = new System.Drawing.Point(8, 28);
            this.txtCartonRePrint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtCartonRePrint.MaxLength = 40;
            this.txtCartonRePrint.Name = "txtCartonRePrint";
            this.txtCartonRePrint.Size = new System.Drawing.Size(329, 30);
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
            this.groupBox1.Location = new System.Drawing.Point(296, 675);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(346, 107);
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
            this.dgvPART.Location = new System.Drawing.Point(4, 21);
            this.dgvPART.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvPART.Name = "dgvPART";
            this.dgvPART.RowHeadersWidth = 62;
            this.dgvPART.RowTemplate.Height = 23;
            this.dgvPART.Size = new System.Drawing.Size(956, 113);
            this.dgvPART.TabIndex = 60;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoCSN2);
            this.groupBox2.Controls.Add(this.rdoCSN);
            this.groupBox2.Location = new System.Drawing.Point(11, 172);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(203, 45);
            this.groupBox2.TabIndex = 61;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CSN类型";
            // 
            // rdoCSN2
            // 
            this.rdoCSN2.AutoSize = true;
            this.rdoCSN2.Location = new System.Drawing.Point(110, 15);
            this.rdoCSN2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rdoCSN2.Name = "rdoCSN2";
            this.rdoCSN2.Size = new System.Drawing.Size(68, 19);
            this.rdoCSN2.TabIndex = 1;
            this.rdoCSN2.Text = "CSN17";
            this.rdoCSN2.UseVisualStyleBackColor = true;
            this.rdoCSN2.Visible = false;
            // 
            // rdoCSN
            // 
            this.rdoCSN.AutoSize = true;
            this.rdoCSN.Checked = true;
            this.rdoCSN.Location = new System.Drawing.Point(28, 15);
            this.rdoCSN.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rdoCSN.Name = "rdoCSN";
            this.rdoCSN.Size = new System.Drawing.Size(68, 19);
            this.rdoCSN.TabIndex = 0;
            this.rdoCSN.TabStop = true;
            this.rdoCSN.Text = "CSN12";
            this.rdoCSN.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_search);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.cmbCoo);
            this.groupBox3.Controls.Add(this.label12);
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
            this.groupBox3.Location = new System.Drawing.Point(11, 222);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Size = new System.Drawing.Size(964, 112);
            this.groupBox3.TabIndex = 62;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "料号";
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(884, 75);
            this.btn_search.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(74, 25);
            this.btn_search.TabIndex = 68;
            this.btn_search.Text = "Search";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(699, 54);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(103, 15);
            this.label14.TabIndex = 67;
            this.label14.Text = "Shipment ID:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(689, 77);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(164, 25);
            this.textBox1.TabIndex = 66;
            // 
            // cmbCoo
            // 
            this.cmbCoo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCoo.FormattingEnabled = true;
            this.cmbCoo.Location = new System.Drawing.Point(684, 21);
            this.cmbCoo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbCoo.Name = "cmbCoo";
            this.cmbCoo.Size = new System.Drawing.Size(61, 23);
            this.cmbCoo.TabIndex = 65;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(639, 22);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 15);
            this.label12.TabIndex = 64;
            this.label12.Text = "COO:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(259, 87);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 15);
            this.label9.TabIndex = 61;
            this.label9.Text = "JAN:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(41, 27);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 15);
            this.label8.TabIndex = 60;
            this.label8.Text = "ICTPN:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(60, 87);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 15);
            this.label7.TabIndex = 59;
            this.label7.Text = "UPC:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 15);
            this.label1.TabIndex = 58;
            this.label1.Text = "CustModel:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmbLocation);
            this.groupBox4.Location = new System.Drawing.Point(316, 342);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox4.Size = new System.Drawing.Size(659, 57);
            this.groupBox4.TabIndex = 63;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "库位";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rdoSCCartonA);
            this.groupBox5.Controls.Add(this.rdoSCCarton);
            this.groupBox5.Controls.Add(this.rdoNCarton);
            this.groupBox5.Location = new System.Drawing.Point(221, 172);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox5.Size = new System.Drawing.Size(754, 42);
            this.groupBox5.TabIndex = 64;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "箱号类型";
            // 
            // rdoSCCartonA
            // 
            this.rdoSCCartonA.AutoSize = true;
            this.rdoSCCartonA.Location = new System.Drawing.Point(244, 15);
            this.rdoSCCartonA.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rdoSCCartonA.Name = "rdoSCCartonA";
            this.rdoSCCartonA.Size = new System.Drawing.Size(166, 19);
            this.rdoSCCartonA.TabIndex = 49;
            this.rdoSCCartonA.Text = "客户指定SSCC18箱号";
            this.rdoSCCartonA.UseVisualStyleBackColor = true;
            // 
            // rdoSCCarton
            // 
            this.rdoSCCarton.AutoSize = true;
            this.rdoSCCarton.Location = new System.Drawing.Point(128, 15);
            this.rdoSCCarton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rdoSCCarton.Name = "rdoSCCarton";
            this.rdoSCCarton.Size = new System.Drawing.Size(106, 19);
            this.rdoSCCarton.TabIndex = 48;
            this.rdoSCCarton.Text = "SSCC18箱号";
            this.rdoSCCarton.UseVisualStyleBackColor = true;
            // 
            // rdoNCarton
            // 
            this.rdoNCarton.AutoSize = true;
            this.rdoNCarton.Checked = true;
            this.rdoNCarton.Location = new System.Drawing.Point(8, 15);
            this.rdoNCarton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rdoNCarton.Name = "rdoNCarton";
            this.rdoNCarton.Size = new System.Drawing.Size(103, 19);
            this.rdoNCarton.TabIndex = 47;
            this.rdoNCarton.TabStop = true;
            this.rdoNCarton.Text = "自定义箱号";
            this.rdoNCarton.UseVisualStyleBackColor = true;
            // 
            // btnCreateCarton2
            // 
            this.btnCreateCarton2.Enabled = false;
            this.btnCreateCarton2.Location = new System.Drawing.Point(459, 65);
            this.btnCreateCarton2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCreateCarton2.Name = "btnCreateCarton2";
            this.btnCreateCarton2.Size = new System.Drawing.Size(100, 27);
            this.btnCreateCarton2.TabIndex = 66;
            this.btnCreateCarton2.Text = "多库位产生";
            this.btnCreateCarton2.UseVisualStyleBackColor = true;
            this.btnCreateCarton2.Click += new System.EventHandler(this.btnCreateCarton2_Click);
            // 
            // btnADD
            // 
            this.btnADD.Enabled = false;
            this.btnADD.Location = new System.Drawing.Point(339, 65);
            this.btnADD.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnADD.Name = "btnADD";
            this.btnADD.Size = new System.Drawing.Size(100, 27);
            this.btnADD.TabIndex = 50;
            this.btnADD.Text = "ADD";
            this.btnADD.UseVisualStyleBackColor = true;
            this.btnADD.Click += new System.EventHandler(this.btnADD_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rdoListLocation);
            this.groupBox6.Controls.Add(this.rdoOneLocation);
            this.groupBox6.Location = new System.Drawing.Point(11, 342);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox6.Size = new System.Drawing.Size(297, 57);
            this.groupBox6.TabIndex = 67;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "作业分类";
            // 
            // rdoListLocation
            // 
            this.rdoListLocation.AutoSize = true;
            this.rdoListLocation.Enabled = false;
            this.rdoListLocation.Location = new System.Drawing.Point(151, 22);
            this.rdoListLocation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rdoListLocation.Name = "rdoListLocation";
            this.rdoListLocation.Size = new System.Drawing.Size(103, 19);
            this.rdoListLocation.TabIndex = 1;
            this.rdoListLocation.Text = "多库位作业";
            this.rdoListLocation.UseVisualStyleBackColor = true;
            // 
            // rdoOneLocation
            // 
            this.rdoOneLocation.AutoSize = true;
            this.rdoOneLocation.Checked = true;
            this.rdoOneLocation.Location = new System.Drawing.Point(32, 22);
            this.rdoOneLocation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rdoOneLocation.Name = "rdoOneLocation";
            this.rdoOneLocation.Size = new System.Drawing.Size(103, 19);
            this.rdoOneLocation.TabIndex = 0;
            this.rdoOneLocation.TabStop = true;
            this.rdoOneLocation.Text = "单库位作业";
            this.rdoOneLocation.UseVisualStyleBackColor = true;
            this.rdoOneLocation.CheckedChanged += new System.EventHandler(this.rdoOneLocation_CheckedChanged);
            // 
            // grpList
            // 
            this.grpList.Controls.Add(this.dgvPART);
            this.grpList.Location = new System.Drawing.Point(11, 527);
            this.grpList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpList.Name = "grpList";
            this.grpList.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpList.Size = new System.Drawing.Size(964, 137);
            this.grpList.TabIndex = 68;
            this.grpList.TabStop = false;
            this.grpList.Text = "产生序号库位 List0:";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.txtCartons);
            this.groupBox8.Location = new System.Drawing.Point(11, 675);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox8.Size = new System.Drawing.Size(276, 107);
            this.groupBox8.TabIndex = 69;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "CartonNO List:";
            // 
            // chkGS1Label
            // 
            this.chkGS1Label.AutoSize = true;
            this.chkGS1Label.Checked = true;
            this.chkGS1Label.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGS1Label.Enabled = false;
            this.chkGS1Label.Location = new System.Drawing.Point(29, 72);
            this.chkGS1Label.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkGS1Label.Name = "chkGS1Label";
            this.chkGS1Label.Size = new System.Drawing.Size(93, 19);
            this.chkGS1Label.TabIndex = 70;
            this.chkGS1Label.Text = "GS1Label";
            this.chkGS1Label.UseVisualStyleBackColor = true;
            // 
            // chkCSNListLabel
            // 
            this.chkCSNListLabel.AutoSize = true;
            this.chkCSNListLabel.Checked = true;
            this.chkCSNListLabel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCSNListLabel.Enabled = false;
            this.chkCSNListLabel.Location = new System.Drawing.Point(162, 72);
            this.chkCSNListLabel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkCSNListLabel.Name = "chkCSNListLabel";
            this.chkCSNListLabel.Size = new System.Drawing.Size(117, 19);
            this.chkCSNListLabel.TabIndex = 71;
            this.chkCSNListLabel.Text = "SNListLabel";
            this.chkCSNListLabel.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.button2);
            this.groupBox9.Controls.Add(this.btn_GetAll);
            this.groupBox9.Controls.Add(this.chkCSNListLabel);
            this.groupBox9.Controls.Add(this.nudCartonCount);
            this.groupBox9.Controls.Add(this.chkGS1Label);
            this.groupBox9.Controls.Add(this.label4);
            this.groupBox9.Controls.Add(this.btnADD);
            this.groupBox9.Controls.Add(this.btnCreateCarton2);
            this.groupBox9.Controls.Add(this.btnCreateCarton);
            this.groupBox9.Location = new System.Drawing.Point(11, 412);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox9.Size = new System.Drawing.Size(964, 105);
            this.groupBox9.TabIndex = 72;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "产生箱号";
            this.groupBox9.Enter += new System.EventHandler(this.groupBox9_Enter);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(778, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(179, 48);
            this.button2.TabIndex = 73;
            this.button2.Text = "Generate with  Print";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_GetAll
            // 
            this.btn_GetAll.Location = new System.Drawing.Point(570, 13);
            this.btn_GetAll.Name = "btn_GetAll";
            this.btn_GetAll.Size = new System.Drawing.Size(190, 48);
            this.btn_GetAll.TabIndex = 72;
            this.btn_GetAll.Text = "Generate without Print";
            this.btn_GetAll.UseVisualStyleBackColor = true;
            this.btn_GetAll.Click += new System.EventHandler(this.btn_GetAll_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label11);
            this.groupBox7.Controls.Add(this.lblMaxQty);
            this.groupBox7.Controls.Add(this.label13);
            this.groupBox7.Controls.Add(this.lblCurrCarqty);
            this.groupBox7.Controls.Add(this.label2);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Controls.Add(this.lblDNqty);
            this.groupBox7.Controls.Add(this.lblDNcaton);
            this.groupBox7.Controls.Add(this.label17);
            this.groupBox7.Controls.Add(this.cmbCAR);
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Controls.Add(this.cmbDN);
            this.groupBox7.Location = new System.Drawing.Point(7, 7);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox7.Size = new System.Drawing.Size(968, 140);
            this.groupBox7.TabIndex = 73;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "DN和车行号";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(661, 97);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 15);
            this.label11.TabIndex = 69;
            this.label11.Text = "车行号存储数量:";
            // 
            // lblMaxQty
            // 
            this.lblMaxQty.AutoSize = true;
            this.lblMaxQty.Location = new System.Drawing.Point(853, 97);
            this.lblMaxQty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaxQty.Name = "lblMaxQty";
            this.lblMaxQty.Size = new System.Drawing.Size(15, 15);
            this.lblMaxQty.TabIndex = 68;
            this.lblMaxQty.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(841, 97);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(15, 15);
            this.label13.TabIndex = 68;
            this.label13.Text = "/";
            // 
            // lblCurrCarqty
            // 
            this.lblCurrCarqty.AutoSize = true;
            this.lblCurrCarqty.Location = new System.Drawing.Point(828, 97);
            this.lblCurrCarqty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrCarqty.Name = "lblCurrCarqty";
            this.lblCurrCarqty.Size = new System.Drawing.Size(15, 15);
            this.lblCurrCarqty.TabIndex = 68;
            this.lblCurrCarqty.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(448, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 15);
            this.label2.TabIndex = 67;
            this.label2.Text = "DN需求箱数:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(140, 48);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 15);
            this.label10.TabIndex = 66;
            this.label10.Text = "DN需求数量:";
            // 
            // lblDNqty
            // 
            this.lblDNqty.AutoSize = true;
            this.lblDNqty.Location = new System.Drawing.Point(235, 48);
            this.lblDNqty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDNqty.Name = "lblDNqty";
            this.lblDNqty.Size = new System.Drawing.Size(15, 15);
            this.lblDNqty.TabIndex = 64;
            this.lblDNqty.Text = "0";
            // 
            // lblDNcaton
            // 
            this.lblDNcaton.AutoSize = true;
            this.lblDNcaton.Location = new System.Drawing.Point(548, 48);
            this.lblDNcaton.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDNcaton.Name = "lblDNcaton";
            this.lblDNcaton.Size = new System.Drawing.Size(15, 15);
            this.lblDNcaton.TabIndex = 65;
            this.lblDNcaton.Text = "0";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(9, 97);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(87, 15);
            this.label17.TabIndex = 63;
            this.label17.Text = "CARLINENO:";
            // 
            // cmbCAR
            // 
            this.cmbCAR.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCAR.FormattingEnabled = true;
            this.cmbCAR.Items.AddRange(new object[] {
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
            this.cmbCAR.Location = new System.Drawing.Point(104, 90);
            this.cmbCAR.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbCAR.Name = "cmbCAR";
            this.cmbCAR.Size = new System.Drawing.Size(519, 27);
            this.cmbCAR.TabIndex = 62;
            this.cmbCAR.SelectedIndexChanged += new System.EventHandler(this.cmbCAR_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(66, 30);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 15);
            this.label5.TabIndex = 60;
            this.label5.Text = "DN:";
            // 
            // cmbDN
            // 
            this.cmbDN.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbDN.FormattingEnabled = true;
            this.cmbDN.Items.AddRange(new object[] {
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
            this.cmbDN.Location = new System.Drawing.Point(104, 17);
            this.cmbDN.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbDN.Name = "cmbDN";
            this.cmbDN.Size = new System.Drawing.Size(765, 27);
            this.cmbDN.TabIndex = 47;
            this.cmbDN.SelectedIndexChanged += new System.EventHandler(this.cmbDN_SelectedIndexChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(675, 668);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(296, 161);
            this.richTextBox1.TabIndex = 74;
            this.richTextBox1.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 858);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.grpList);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Text = "序号产生forPPS";
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
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
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
        private System.Windows.Forms.RadioButton rdoCSN;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
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
        private System.Windows.Forms.RadioButton rdoCSN2;
        private System.Windows.Forms.RadioButton rdoSCCartonA;
        private System.Windows.Forms.RadioButton rdoSCCarton;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cmbCAR;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbDN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblDNqty;
        private System.Windows.Forms.Label lblDNcaton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblCurrCarqty;
        private System.Windows.Forms.Label lblMaxQty;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbCoo;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_GetAll;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button2;
    }
}

