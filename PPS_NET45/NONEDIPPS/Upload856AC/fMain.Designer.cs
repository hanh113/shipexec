namespace UpLoad856AC
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labShow = new System.Windows.Forms.TextBox();
            this.TextMsg = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvDN = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvNo = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnInit = new System.Windows.Forms.Button();
            this.dt_start = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dt_end = new System.Windows.Forms.DateTimePicker();
            this.lblStart = new System.Windows.Forms.Label();
            this.btnUploadTT = new System.Windows.Forms.Button();
            this.cmbPOE = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.radFD = new System.Windows.Forms.RadioButton();
            this.radDS = new System.Windows.Forms.RadioButton();
            this.cmbSmid = new System.Windows.Forms.ComboBox();
            this.cmbSTATUS = new System.Windows.Forms.ComboBox();
            this.cmbCarrier = new System.Windows.Forms.ComboBox();
            this.cmbREGION = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.labShipmentID = new System.Windows.Forms.Label();
            this.labFreightForward = new System.Windows.Forms.Label();
            this.labStatus = new System.Windows.Forms.Label();
            this.labEntrancePort = new System.Windows.Forms.Label();
            this.labArea = new System.Windows.Forms.Label();
            this.btsSentEDI = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDN)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNo)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.labShow);
            this.panel1.Controls.Add(this.TextMsg);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1036, 648);
            this.panel1.TabIndex = 0;
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Enabled = false;
            this.labShow.Font = new System.Drawing.Font("SimSun", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.Color.White;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Name = "labShow";
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(1036, 41);
            this.labShow.TabIndex = 64;
            this.labShow.Text = "Upload";
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
            this.TextMsg.Location = new System.Drawing.Point(0, 605);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1036, 43);
            this.TextMsg.TabIndex = 63;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox3.Controls.Add(this.dgvDN);
            this.groupBox3.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(4, 393);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(1032, 212);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DN信息";
            // 
            // dgvDN
            // 
            this.dgvDN.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDN.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvDN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDN.Location = new System.Drawing.Point(2, 19);
            this.dgvDN.Margin = new System.Windows.Forms.Padding(2);
            this.dgvDN.Name = "dgvDN";
            this.dgvDN.RowTemplate.Height = 27;
            this.dgvDN.Size = new System.Drawing.Size(1028, 191);
            this.dgvDN.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox2.Controls.Add(this.dgvNo);
            this.groupBox2.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(2, 182);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(1032, 207);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "集货单号";
            // 
            // dgvNo
            // 
            this.dgvNo.AllowUserToAddRows = false;
            this.dgvNo.AllowUserToDeleteRows = false;
            this.dgvNo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvNo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNo.Location = new System.Drawing.Point(2, 19);
            this.dgvNo.Margin = new System.Windows.Forms.Padding(1);
            this.dgvNo.MultiSelect = false;
            this.dgvNo.Name = "dgvNo";
            this.dgvNo.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNo.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvNo.RowHeadersWidth = 50;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.dgvNo.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvNo.RowTemplate.Height = 27;
            this.dgvNo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvNo.Size = new System.Drawing.Size(1028, 186);
            this.dgvNo.TabIndex = 98;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnInit);
            this.groupBox1.Controls.Add(this.dt_start);
            this.groupBox1.Controls.Add(this.lblEnd);
            this.groupBox1.Controls.Add(this.dt_end);
            this.groupBox1.Controls.Add(this.lblStart);
            this.groupBox1.Controls.Add(this.btnUploadTT);
            this.groupBox1.Controls.Add(this.cmbPOE);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.radFD);
            this.groupBox1.Controls.Add(this.radDS);
            this.groupBox1.Controls.Add(this.cmbSmid);
            this.groupBox1.Controls.Add(this.cmbSTATUS);
            this.groupBox1.Controls.Add(this.cmbCarrier);
            this.groupBox1.Controls.Add(this.cmbREGION);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.labShipmentID);
            this.groupBox1.Controls.Add(this.labFreightForward);
            this.groupBox1.Controls.Add(this.labStatus);
            this.groupBox1.Controls.Add(this.labEntrancePort);
            this.groupBox1.Controls.Add(this.labArea);
            this.groupBox1.Controls.Add(this.btsSentEDI);
            this.groupBox1.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(2, 46);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(1032, 132);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(947, 100);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 130;
            this.button1.Text = "btnTest";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(840, 66);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(130, 30);
            this.btnInit.TabIndex = 129;
            this.btnInit.Text = "重 置";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // dt_start
            // 
            this.dt_start.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_start.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dt_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_start.Location = new System.Drawing.Point(440, 101);
            this.dt_start.Name = "dt_start";
            this.dt_start.Size = new System.Drawing.Size(195, 24);
            this.dt_start.TabIndex = 128;
            this.dt_start.Visible = false;
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(646, 109);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(75, 15);
            this.lblEnd.TabIndex = 127;
            this.lblEnd.Text = "结束时间:";
            this.lblEnd.Visible = false;
            // 
            // dt_end
            // 
            this.dt_end.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_end.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dt_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_end.Location = new System.Drawing.Point(727, 102);
            this.dt_end.Name = "dt_end";
            this.dt_end.Size = new System.Drawing.Size(195, 24);
            this.dt_end.TabIndex = 126;
            this.dt_end.Visible = false;
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(335, 110);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(75, 15);
            this.lblStart.TabIndex = 125;
            this.lblStart.Text = "开始时间:";
            this.lblStart.Visible = false;
            // 
            // btnUploadTT
            // 
            this.btnUploadTT.Location = new System.Drawing.Point(840, 17);
            this.btnUploadTT.Name = "btnUploadTT";
            this.btnUploadTT.Size = new System.Drawing.Size(150, 30);
            this.btnUploadTT.TabIndex = 124;
            this.btnUploadTT.Text = "2.SAP扣账";
            this.btnUploadTT.UseVisualStyleBackColor = true;
            this.btnUploadTT.Click += new System.EventHandler(this.btnUploadTT_Click);
            // 
            // cmbPOE
            // 
            this.cmbPOE.FormattingEnabled = true;
            this.cmbPOE.Location = new System.Drawing.Point(123, 72);
            this.cmbPOE.Name = "cmbPOE";
            this.cmbPOE.Size = new System.Drawing.Size(195, 23);
            this.cmbPOE.Sorted = true;
            this.cmbPOE.TabIndex = 123;
            this.cmbPOE.Text = "-ALL-";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(-3, 20);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(75, 15);
            this.label14.TabIndex = 122;
            this.label14.Text = "出货类型:";
            // 
            // radFD
            // 
            this.radFD.AutoSize = true;
            this.radFD.Checked = true;
            this.radFD.Location = new System.Drawing.Point(200, 18);
            this.radFD.Margin = new System.Windows.Forms.Padding(1);
            this.radFD.Name = "radFD";
            this.radFD.Size = new System.Drawing.Size(41, 19);
            this.radFD.TabIndex = 121;
            this.radFD.TabStop = true;
            this.radFD.Text = "FD";
            this.radFD.UseVisualStyleBackColor = true;
            this.radFD.CheckedChanged += new System.EventHandler(this.radFD_CheckedChanged);
            // 
            // radDS
            // 
            this.radDS.AutoSize = true;
            this.radDS.Enabled = false;
            this.radDS.Location = new System.Drawing.Point(123, 17);
            this.radDS.Margin = new System.Windows.Forms.Padding(1);
            this.radDS.Name = "radDS";
            this.radDS.Size = new System.Drawing.Size(41, 19);
            this.radDS.TabIndex = 120;
            this.radDS.Text = "DS";
            this.radDS.UseVisualStyleBackColor = true;
            this.radDS.CheckedChanged += new System.EventHandler(this.radDS_CheckedChanged);
            // 
            // cmbSmid
            // 
            this.cmbSmid.FormattingEnabled = true;
            this.cmbSmid.Items.AddRange(new object[] {
            "-ALL-"});
            this.cmbSmid.Location = new System.Drawing.Point(123, 43);
            this.cmbSmid.Margin = new System.Windows.Forms.Padding(1);
            this.cmbSmid.Name = "cmbSmid";
            this.cmbSmid.Size = new System.Drawing.Size(194, 23);
            this.cmbSmid.TabIndex = 119;
            this.cmbSmid.Text = "-ALL-";
            this.cmbSmid.SelectedIndexChanged += new System.EventHandler(this.cmbSmid_SelectedIndexChanged);
            // 
            // cmbSTATUS
            // 
            this.cmbSTATUS.FormattingEnabled = true;
            this.cmbSTATUS.Items.AddRange(new object[] {
            "-ALL-",
            "WP-未PACK",
            "IP-PACK中",
            "FP-已PACK",
            "LF-已LOADCAR",
            "CP-CANCEL",
            "HO-HOLD"});
            this.cmbSTATUS.Location = new System.Drawing.Point(123, 101);
            this.cmbSTATUS.Name = "cmbSTATUS";
            this.cmbSTATUS.Size = new System.Drawing.Size(195, 23);
            this.cmbSTATUS.TabIndex = 118;
            this.cmbSTATUS.Text = "LF-已LOADCAR";
            this.cmbSTATUS.SelectedIndexChanged += new System.EventHandler(this.cmbSTATUS_SelectedIndexChanged);
            // 
            // cmbCarrier
            // 
            this.cmbCarrier.FormattingEnabled = true;
            this.cmbCarrier.Location = new System.Drawing.Point(440, 72);
            this.cmbCarrier.Name = "cmbCarrier";
            this.cmbCarrier.Size = new System.Drawing.Size(197, 23);
            this.cmbCarrier.Sorted = true;
            this.cmbCarrier.TabIndex = 117;
            this.cmbCarrier.Text = "-ALL-";
            // 
            // cmbREGION
            // 
            this.cmbREGION.FormattingEnabled = true;
            this.cmbREGION.Items.AddRange(new object[] {
            "-ALL-",
            "AMR",
            "APAC",
            "EMEIA"});
            this.cmbREGION.Location = new System.Drawing.Point(440, 38);
            this.cmbREGION.Name = "cmbREGION";
            this.cmbREGION.Size = new System.Drawing.Size(197, 23);
            this.cmbREGION.Sorted = true;
            this.cmbREGION.TabIndex = 116;
            this.cmbREGION.Text = "-ALL-";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(682, 69);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(130, 27);
            this.btnSearch.TabIndex = 115;
            this.btnSearch.Text = "查 询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labShipmentID
            // 
            this.labShipmentID.AutoSize = true;
            this.labShipmentID.Location = new System.Drawing.Point(-3, 51);
            this.labShipmentID.Name = "labShipmentID";
            this.labShipmentID.Size = new System.Drawing.Size(75, 15);
            this.labShipmentID.TabIndex = 114;
            this.labShipmentID.Text = "集货单号:";
            // 
            // labFreightForward
            // 
            this.labFreightForward.AutoSize = true;
            this.labFreightForward.Location = new System.Drawing.Point(365, 77);
            this.labFreightForward.Name = "labFreightForward";
            this.labFreightForward.Size = new System.Drawing.Size(45, 15);
            this.labFreightForward.TabIndex = 111;
            this.labFreightForward.Text = "货代:";
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.Location = new System.Drawing.Point(-4, 109);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(90, 15);
            this.labStatus.TabIndex = 113;
            this.labStatus.Text = "集货单状态:";
            // 
            // labEntrancePort
            // 
            this.labEntrancePort.AutoSize = true;
            this.labEntrancePort.Location = new System.Drawing.Point(0, 80);
            this.labEntrancePort.Name = "labEntrancePort";
            this.labEntrancePort.Size = new System.Drawing.Size(45, 15);
            this.labEntrancePort.TabIndex = 112;
            this.labEntrancePort.Text = "港口:";
            // 
            // labArea
            // 
            this.labArea.AutoSize = true;
            this.labArea.Location = new System.Drawing.Point(335, 43);
            this.labArea.Name = "labArea";
            this.labArea.Size = new System.Drawing.Size(75, 15);
            this.labArea.TabIndex = 110;
            this.labArea.Text = "出货区域:";
            // 
            // btsSentEDI
            // 
            this.btsSentEDI.Enabled = false;
            this.btsSentEDI.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btsSentEDI.Location = new System.Drawing.Point(682, 17);
            this.btsSentEDI.Margin = new System.Windows.Forms.Padding(2);
            this.btsSentEDI.Name = "btsSentEDI";
            this.btsSentEDI.Size = new System.Drawing.Size(130, 30);
            this.btsSentEDI.TabIndex = 31;
            this.btsSentEDI.Text = "1.UPLOAD EDI";
            this.btsSentEDI.UseVisualStyleBackColor = true;
            this.btsSentEDI.Visible = false;
            this.btsSentEDI.Click += new System.EventHandler(this.btsSentEDI_Click);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 648);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "fMain";
            this.Text = "Ver:1.0.0.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDN)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvDN;
        private System.Windows.Forms.Button btsSentEDI;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.ComboBox cmbPOE;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RadioButton radFD;
        private System.Windows.Forms.RadioButton radDS;
        private System.Windows.Forms.ComboBox cmbSmid;
        private System.Windows.Forms.ComboBox cmbSTATUS;
        private System.Windows.Forms.ComboBox cmbCarrier;
        private System.Windows.Forms.ComboBox cmbREGION;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label labShipmentID;
        private System.Windows.Forms.Label labFreightForward;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.Label labEntrancePort;
        private System.Windows.Forms.Label labArea;
        private System.Windows.Forms.DataGridView dgvNo;
        private System.Windows.Forms.Button btnUploadTT;
        private System.Windows.Forms.DateTimePicker dt_start;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dt_end;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.Button button1;
    }
}

