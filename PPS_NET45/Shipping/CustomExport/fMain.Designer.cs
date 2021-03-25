namespace CustomExport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labShow = new System.Windows.Forms.TextBox();
            this.TextMsg = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvNo = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.chkFD = new System.Windows.Forms.CheckBox();
            this.cmbPDF = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkDS = new System.Windows.Forms.CheckBox();
            this.cmbPOE = new System.Windows.Forms.ComboBox();
            this.cmbCarrier = new System.Windows.Forms.ComboBox();
            this.labFreightForward = new System.Windows.Forms.Label();
            this.labEntrancePort = new System.Windows.Forms.Label();
            this.btnInit = new System.Windows.Forms.Button();
            this.dt_close = new System.Windows.Forms.DateTimePicker();
            this.lblClose = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbSmid = new System.Windows.Forms.ComboBox();
            this.cmbREGION = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.labShipmentID = new System.Windows.Forms.Label();
            this.labArea = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNo)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.Color.White;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labShow.Name = "labShow";
            this.labShow.ReadOnly = true;
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(1328, 57);
            this.labShow.TabIndex = 64;
            this.labShow.Text = "Logistic Export";
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
            this.TextMsg.Location = new System.Drawing.Point(0, 920);
            this.TextMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1328, 64);
            this.TextMsg.TabIndex = 63;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.labShow);
            this.panel1.Controls.Add(this.TextMsg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1328, 984);
            this.panel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox2.Controls.Add(this.dgvNo);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 323);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1328, 597);
            this.groupBox2.TabIndex = 68;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "集货单号";
            // 
            // dgvNo
            // 
            this.dgvNo.AllowUserToAddRows = false;
            this.dgvNo.AllowUserToDeleteRows = false;
            this.dgvNo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvNo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvNo.ColumnHeadersHeight = 34;
            this.dgvNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNo.Location = new System.Drawing.Point(3, 29);
            this.dgvNo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvNo.MultiSelect = false;
            this.dgvNo.Name = "dgvNo";
            this.dgvNo.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNo.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvNo.RowHeadersWidth = 50;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            this.dgvNo.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvNo.RowTemplate.Height = 27;
            this.dgvNo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvNo.Size = new System.Drawing.Size(1322, 565);
            this.dgvNo.TabIndex = 98;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox1.Controls.Add(this.btnCreate);
            this.groupBox1.Controls.Add(this.chkFD);
            this.groupBox1.Controls.Add(this.cmbPDF);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkDS);
            this.groupBox1.Controls.Add(this.cmbPOE);
            this.groupBox1.Controls.Add(this.cmbCarrier);
            this.groupBox1.Controls.Add(this.labFreightForward);
            this.groupBox1.Controls.Add(this.labEntrancePort);
            this.groupBox1.Controls.Add(this.btnInit);
            this.groupBox1.Controls.Add(this.dt_close);
            this.groupBox1.Controls.Add(this.lblClose);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.cmbSmid);
            this.groupBox1.Controls.Add(this.cmbREGION);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.labShipmentID);
            this.groupBox1.Controls.Add(this.labArea);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1328, 266);
            this.groupBox1.TabIndex = 65;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(1121, 33);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(141, 64);
            this.btnCreate.TabIndex = 121;
            this.btnCreate.Text = "产生PDF";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // chkFD
            // 
            this.chkFD.AutoSize = true;
            this.chkFD.Checked = true;
            this.chkFD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFD.Location = new System.Drawing.Point(292, 81);
            this.chkFD.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkFD.Name = "chkFD";
            this.chkFD.Size = new System.Drawing.Size(58, 26);
            this.chkFD.TabIndex = 136;
            this.chkFD.Text = "FD";
            this.chkFD.UseVisualStyleBackColor = true;
            this.chkFD.CheckedChanged += new System.EventHandler(this.chkFD_CheckedChanged);
            // 
            // cmbPDF
            // 
            this.cmbPDF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPDF.FormattingEnabled = true;
            this.cmbPDF.Items.AddRange(new object[] {
            "DELIVERYNOTE",
            "PACKINGLIST",
            "PALLETLOADINGSHEET",
            "HANDOVERMANIFEST"});
            this.cmbPDF.Location = new System.Drawing.Point(183, 33);
            this.cmbPDF.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbPDF.Name = "cmbPDF";
            this.cmbPDF.Size = new System.Drawing.Size(290, 30);
            this.cmbPDF.TabIndex = 120;
            this.cmbPDF.SelectedIndexChanged += new System.EventHandler(this.cmbPDF_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 22);
            this.label1.TabIndex = 119;
            this.label1.Text = "PDF类型:";
            // 
            // chkDS
            // 
            this.chkDS.AutoSize = true;
            this.chkDS.Checked = true;
            this.chkDS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDS.Location = new System.Drawing.Point(198, 81);
            this.chkDS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkDS.Name = "chkDS";
            this.chkDS.Size = new System.Drawing.Size(58, 26);
            this.chkDS.TabIndex = 135;
            this.chkDS.Text = "DS";
            this.chkDS.UseVisualStyleBackColor = true;
            this.chkDS.CheckedChanged += new System.EventHandler(this.chkDS_CheckedChanged);
            // 
            // cmbPOE
            // 
            this.cmbPOE.FormattingEnabled = true;
            this.cmbPOE.Items.AddRange(new object[] {
            "-ALL-"});
            this.cmbPOE.Location = new System.Drawing.Point(184, 171);
            this.cmbPOE.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbPOE.Name = "cmbPOE";
            this.cmbPOE.Size = new System.Drawing.Size(290, 30);
            this.cmbPOE.Sorted = true;
            this.cmbPOE.TabIndex = 134;
            this.cmbPOE.Text = "-ALL-";
            this.cmbPOE.SelectedIndexChanged += new System.EventHandler(this.cmbPOE_SelectedIndexChanged);
            // 
            // cmbCarrier
            // 
            this.cmbCarrier.FormattingEnabled = true;
            this.cmbCarrier.Items.AddRange(new object[] {
            "-ALL-"});
            this.cmbCarrier.Location = new System.Drawing.Point(736, 171);
            this.cmbCarrier.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbCarrier.Name = "cmbCarrier";
            this.cmbCarrier.Size = new System.Drawing.Size(294, 30);
            this.cmbCarrier.Sorted = true;
            this.cmbCarrier.TabIndex = 133;
            this.cmbCarrier.Text = "-ALL-";
            this.cmbCarrier.SelectedIndexChanged += new System.EventHandler(this.cmbCarrier_SelectedIndexChanged);
            // 
            // labFreightForward
            // 
            this.labFreightForward.AutoSize = true;
            this.labFreightForward.Location = new System.Drawing.Point(544, 174);
            this.labFreightForward.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labFreightForward.Name = "labFreightForward";
            this.labFreightForward.Size = new System.Drawing.Size(65, 22);
            this.labFreightForward.TabIndex = 131;
            this.labFreightForward.Text = "货代:";
            // 
            // labEntrancePort
            // 
            this.labEntrancePort.AutoSize = true;
            this.labEntrancePort.Location = new System.Drawing.Point(19, 175);
            this.labEntrancePort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labEntrancePort.Name = "labEntrancePort";
            this.labEntrancePort.Size = new System.Drawing.Size(65, 22);
            this.labEntrancePort.TabIndex = 132;
            this.labEntrancePort.Text = "港口:";
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(1121, 120);
            this.btnInit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(141, 64);
            this.btnInit.TabIndex = 129;
            this.btnInit.Text = "重 置";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Visible = false;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // dt_close
            // 
            this.dt_close.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_close.CustomFormat = "yyyy-MM-dd";
            this.dt_close.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_close.Location = new System.Drawing.Point(184, 214);
            this.dt_close.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dt_close.Name = "dt_close";
            this.dt_close.Size = new System.Drawing.Size(169, 33);
            this.dt_close.TabIndex = 128;
            this.dt_close.ValueChanged += new System.EventHandler(this.dt_close_ValueChanged);
            // 
            // lblClose
            // 
            this.lblClose.AutoSize = true;
            this.lblClose.Location = new System.Drawing.Point(19, 217);
            this.lblClose.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(109, 22);
            this.lblClose.TabIndex = 125;
            this.lblClose.Text = "关闭日期:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(18, 81);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(109, 22);
            this.label14.TabIndex = 122;
            this.label14.Text = "出货类型:";
            // 
            // cmbSmid
            // 
            this.cmbSmid.FormattingEnabled = true;
            this.cmbSmid.Items.AddRange(new object[] {
            "-ALL-"});
            this.cmbSmid.Location = new System.Drawing.Point(184, 123);
            this.cmbSmid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbSmid.Name = "cmbSmid";
            this.cmbSmid.Size = new System.Drawing.Size(289, 30);
            this.cmbSmid.TabIndex = 119;
            this.cmbSmid.Text = "-ALL-";
            this.cmbSmid.SelectedIndexChanged += new System.EventHandler(this.cmbSmid_SelectedIndexChanged);
            // 
            // cmbREGION
            // 
            this.cmbREGION.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbREGION.FormattingEnabled = true;
            this.cmbREGION.Items.AddRange(new object[] {
            "-ALL-",
            "AMR",
            "EMEIA",
            "PAC"});
            this.cmbREGION.Location = new System.Drawing.Point(736, 123);
            this.cmbREGION.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbREGION.Name = "cmbREGION";
            this.cmbREGION.Size = new System.Drawing.Size(294, 30);
            this.cmbREGION.Sorted = true;
            this.cmbREGION.TabIndex = 116;
            this.cmbREGION.SelectedIndexChanged += new System.EventHandler(this.cmbREGION_SelectedIndexChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(766, 33);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(141, 64);
            this.btnSearch.TabIndex = 115;
            this.btnSearch.Text = "查 询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labShipmentID
            // 
            this.labShipmentID.AutoSize = true;
            this.labShipmentID.Location = new System.Drawing.Point(19, 129);
            this.labShipmentID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labShipmentID.Name = "labShipmentID";
            this.labShipmentID.Size = new System.Drawing.Size(109, 22);
            this.labShipmentID.TabIndex = 114;
            this.labShipmentID.Text = "集货单号:";
            // 
            // labArea
            // 
            this.labArea.AutoSize = true;
            this.labArea.Location = new System.Drawing.Point(544, 129);
            this.labArea.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labArea.Name = "labArea";
            this.labArea.Size = new System.Drawing.Size(109, 22);
            this.labArea.TabIndex = 110;
            this.labArea.Text = "出货区域:";
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1328, 984);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "fMain";
            this.Text = "产生集货单PDF";
            this.Load += new System.EventHandler(this.fMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.DateTimePicker dt_close;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbSmid;
        private System.Windows.Forms.ComboBox cmbREGION;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label labShipmentID;
        private System.Windows.Forms.Label labArea;
        private System.Windows.Forms.CheckBox chkFD;
        private System.Windows.Forms.CheckBox chkDS;
        private System.Windows.Forms.ComboBox cmbPOE;
        private System.Windows.Forms.ComboBox cmbCarrier;
        private System.Windows.Forms.Label labFreightForward;
        private System.Windows.Forms.Label labEntrancePort;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.ComboBox cmbPDF;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvNo;
    }
}

