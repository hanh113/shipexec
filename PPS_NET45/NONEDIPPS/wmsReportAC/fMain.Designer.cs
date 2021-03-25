namespace wmsReportAC
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
            this.dgvNo = new System.Windows.Forms.DataGridView();
            this.labShow = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCancelshipment = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.reportS_BTN = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.radDetail = new System.Windows.Forms.RadioButton();
            this.radSummary = new System.Windows.Forms.RadioButton();
            this.cmbTYPE = new System.Windows.Forms.ComboBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.labType = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnPPSOUT = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblstrFile = new System.Windows.Forms.Label();
            this.cmbICTNO = new System.Windows.Forms.ComboBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dt_start = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.dt_end = new System.Windows.Forms.DateTimePicker();
            this.lblStart = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbWHID = new System.Windows.Forms.ComboBox();
            this.labWHID = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.TextMsg = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNo)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvNo
            // 
            this.dgvNo.AllowUserToAddRows = false;
            this.dgvNo.AllowUserToDeleteRows = false;
            this.dgvNo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvNo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNo.ColumnHeadersHeight = 34;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNo.Location = new System.Drawing.Point(3, 16);
            this.dgvNo.Margin = new System.Windows.Forms.Padding(2);
            this.dgvNo.MultiSelect = false;
            this.dgvNo.Name = "dgvNo";
            this.dgvNo.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNo.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvNo.RowHeadersWidth = 50;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.dgvNo.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvNo.RowTemplate.Height = 27;
            this.dgvNo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvNo.Size = new System.Drawing.Size(1170, 318);
            this.dgvNo.TabIndex = 94;
            this.dgvNo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNo_CellContentClick);
            this.dgvNo.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvNo_RowsAdded);
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labShow.Enabled = false;
            this.labShow.Font = new System.Drawing.Font("SimSun", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.SystemColors.Info;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Name = "labShow";
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(1176, 41);
            this.labShow.TabIndex = 61;
            this.labShow.Text = "WMS Report";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labShow);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1176, 47);
            this.panel2.TabIndex = 118;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox2.Controls.Add(this.btnCancelshipment);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.btnQuery);
            this.groupBox2.Controls.Add(this.reportS_BTN);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.radDetail);
            this.groupBox2.Controls.Add(this.radSummary);
            this.groupBox2.Controls.Add(this.cmbTYPE);
            this.groupBox2.Controls.Add(this.btnCheck);
            this.groupBox2.Controls.Add(this.labType);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.btnPPSOUT);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 47);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(1176, 85);
            this.groupBox2.TabIndex = 119;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "报表";
            // 
            // btnCancelshipment
            // 
            this.btnCancelshipment.Location = new System.Drawing.Point(774, 50);
            this.btnCancelshipment.Name = "btnCancelshipment";
            this.btnCancelshipment.Size = new System.Drawing.Size(139, 25);
            this.btnCancelshipment.TabIndex = 124;
            this.btnCancelshipment.Text = "AppleCare取消集货单";
            this.btnCancelshipment.UseVisualStyleBackColor = true;
            this.btnCancelshipment.Click += new System.EventHandler(this.btnCancelshipment_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(599, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 25);
            this.button1.TabIndex = 122;
            this.button1.Text = "AppleCare集货单还原";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(506, 13);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 25);
            this.btnQuery.TabIndex = 121;
            this.btnQuery.Text = "储位查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // reportS_BTN
            // 
            this.reportS_BTN.Location = new System.Drawing.Point(506, 50);
            this.reportS_BTN.Name = "reportS_BTN";
            this.reportS_BTN.Size = new System.Drawing.Size(75, 25);
            this.reportS_BTN.TabIndex = 120;
            this.reportS_BTN.Text = "Report查询";
            this.reportS_BTN.UseVisualStyleBackColor = true;
            this.reportS_BTN.Click += new System.EventHandler(this.reportS_BTN_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(223, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 119;
            this.label2.Text = "样式：";
            // 
            // radDetail
            // 
            this.radDetail.AutoSize = true;
            this.radDetail.Location = new System.Drawing.Point(274, 53);
            this.radDetail.Margin = new System.Windows.Forms.Padding(2);
            this.radDetail.Name = "radDetail";
            this.radDetail.Size = new System.Drawing.Size(87, 17);
            this.radDetail.TabIndex = 118;
            this.radDetail.Text = "Detail Report";
            this.radDetail.UseVisualStyleBackColor = true;
            // 
            // radSummary
            // 
            this.radSummary.AutoSize = true;
            this.radSummary.Checked = true;
            this.radSummary.Location = new System.Drawing.Point(274, 21);
            this.radSummary.Margin = new System.Windows.Forms.Padding(2);
            this.radSummary.Name = "radSummary";
            this.radSummary.Size = new System.Drawing.Size(103, 17);
            this.radSummary.TabIndex = 117;
            this.radSummary.TabStop = true;
            this.radSummary.Text = "Summary Report";
            this.radSummary.UseVisualStyleBackColor = true;
            this.radSummary.CheckedChanged += new System.EventHandler(this.radSummary_CheckedChanged);
            // 
            // cmbTYPE
            // 
            this.cmbTYPE.FormattingEnabled = true;
            this.cmbTYPE.Items.AddRange(new object[] {
            "库存报表",
            "储位检查报表"});
            this.cmbTYPE.Location = new System.Drawing.Point(64, 18);
            this.cmbTYPE.Name = "cmbTYPE";
            this.cmbTYPE.Size = new System.Drawing.Size(126, 21);
            this.cmbTYPE.TabIndex = 116;
            this.cmbTYPE.Text = "库存报表";
            this.cmbTYPE.SelectedIndexChanged += new System.EventHandler(this.cmbTYPE_SelectedIndexChanged);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(425, 50);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(75, 25);
            this.btnCheck.TabIndex = 116;
            this.btnCheck.Text = "储位盘点";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // labType
            // 
            this.labType.AutoSize = true;
            this.labType.Location = new System.Drawing.Point(10, 23);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(43, 13);
            this.labType.TabIndex = 115;
            this.labType.Text = "类型：";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(425, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 113;
            this.button2.Text = "储位移转";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnPPSOUT
            // 
            this.btnPPSOUT.Location = new System.Drawing.Point(599, 13);
            this.btnPPSOUT.Name = "btnPPSOUT";
            this.btnPPSOUT.Size = new System.Drawing.Size(139, 25);
            this.btnPPSOUT.TabIndex = 116;
            this.btnPPSOUT.Text = "AppleCare出库";
            this.btnPPSOUT.UseVisualStyleBackColor = true;
            this.btnPPSOUT.Click += new System.EventHandler(this.btnPPSOUT_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox3.Controls.Add(this.lblstrFile);
            this.groupBox3.Controls.Add(this.cmbICTNO);
            this.groupBox3.Controls.Add(this.btnExport);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.dt_start);
            this.groupBox3.Controls.Add(this.lblEnd);
            this.groupBox3.Controls.Add(this.cmbLocation);
            this.groupBox3.Controls.Add(this.dt_end);
            this.groupBox3.Controls.Add(this.lblStart);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.cmbWHID);
            this.groupBox3.Controls.Add(this.labWHID);
            this.groupBox3.Controls.Add(this.btnSearch);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 132);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(1176, 89);
            this.groupBox3.TabIndex = 120;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查询条件";
            // 
            // lblstrFile
            // 
            this.lblstrFile.AutoSize = true;
            this.lblstrFile.Location = new System.Drawing.Point(772, 64);
            this.lblstrFile.Name = "lblstrFile";
            this.lblstrFile.Size = new System.Drawing.Size(0, 13);
            this.lblstrFile.TabIndex = 111;
            this.lblstrFile.Visible = false;
            // 
            // cmbICTNO
            // 
            this.cmbICTNO.FormattingEnabled = true;
            this.cmbICTNO.Location = new System.Drawing.Point(326, 59);
            this.cmbICTNO.Margin = new System.Windows.Forms.Padding(2);
            this.cmbICTNO.Name = "cmbICTNO";
            this.cmbICTNO.Size = new System.Drawing.Size(174, 21);
            this.cmbICTNO.TabIndex = 110;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(658, 59);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(80, 25);
            this.btnExport.TabIndex = 90;
            this.btnExport.Text = "导出Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(273, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 109;
            this.label4.Text = "料号：";
            // 
            // dt_start
            // 
            this.dt_start.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_start.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dt_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_start.Location = new System.Drawing.Point(300, 22);
            this.dt_start.Name = "dt_start";
            this.dt_start.Size = new System.Drawing.Size(174, 20);
            this.dt_start.TabIndex = 93;
            this.dt_start.Visible = false;
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(484, 25);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(67, 13);
            this.lblEnd.TabIndex = 92;
            this.lblEnd.Text = "结束时间：";
            this.lblEnd.Visible = false;
            // 
            // cmbLocation
            // 
            this.cmbLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(62, 59);
            this.cmbLocation.Margin = new System.Windows.Forms.Padding(2);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(174, 21);
            this.cmbLocation.TabIndex = 108;
            // 
            // dt_end
            // 
            this.dt_end.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_end.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dt_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_end.Location = new System.Drawing.Point(564, 22);
            this.dt_end.Name = "dt_end";
            this.dt_end.Size = new System.Drawing.Size(174, 20);
            this.dt_end.TabIndex = 10;
            this.dt_end.Visible = false;
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(219, 25);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(67, 13);
            this.lblStart.TabIndex = 9;
            this.lblStart.Text = "开始时间：";
            this.lblStart.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 107;
            this.label3.Text = "储位：";
            // 
            // cmbWHID
            // 
            this.cmbWHID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWHID.FormattingEnabled = true;
            this.cmbWHID.Location = new System.Drawing.Point(62, 21);
            this.cmbWHID.Margin = new System.Windows.Forms.Padding(2);
            this.cmbWHID.Name = "cmbWHID";
            this.cmbWHID.Size = new System.Drawing.Size(128, 21);
            this.cmbWHID.TabIndex = 106;
            this.cmbWHID.SelectedIndexChanged += new System.EventHandler(this.cmbWHID_SelectedIndexChanged);
            // 
            // labWHID
            // 
            this.labWHID.AutoSize = true;
            this.labWHID.Location = new System.Drawing.Point(10, 25);
            this.labWHID.Name = "labWHID";
            this.labWHID.Size = new System.Drawing.Size(43, 13);
            this.labWHID.TabIndex = 105;
            this.labWHID.Text = "仓库：";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(564, 59);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 25);
            this.btnSearch.TabIndex = 82;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // TextMsg
            // 
            this.TextMsg.AutoEllipsis = true;
            this.TextMsg.BackColor = System.Drawing.Color.Blue;
            this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1176, 48);
            this.TextMsg.TabIndex = 122;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TextMsg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 558);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1176, 48);
            this.panel1.TabIndex = 123;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvNo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 221);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1176, 337);
            this.groupBox1.TabIndex = 124;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "库存报表";
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1176, 606);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "fMain";
            this.Text = "Ver:1.0.0.0";
            this.Load += new System.EventHandler(this.fMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNo)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.DataGridView dgvNo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radDetail;
        private System.Windows.Forms.RadioButton radSummary;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.ComboBox cmbTYPE;
        private System.Windows.Forms.Label labType;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblstrFile;
        private System.Windows.Forms.ComboBox cmbICTNO;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dt_start;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.DateTimePicker dt_end;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbWHID;
        private System.Windows.Forms.Label labWHID;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button reportS_BTN;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnPPSOUT;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnCancelshipment;
    }
}

