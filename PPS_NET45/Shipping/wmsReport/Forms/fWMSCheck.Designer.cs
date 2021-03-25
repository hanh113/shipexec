namespace wmsReport
{
    partial class fWMSCheck
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button1 = new System.Windows.Forms.Button();
            this.panelDNDetail = new System.Windows.Forms.Panel();
            this.dgvFindResult = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbWHID = new System.Windows.Forms.ComboBox();
            this.labShipmentID = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.labShow = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.sn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cartonid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.palletno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SERIAL_NUMBER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.TextMsg = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.dgvCheckSum = new System.Windows.Forms.DataGridView();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lalFirst = new System.Windows.Forms.Label();
            this.lblSN = new System.Windows.Forms.Label();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.butEnd = new System.Windows.Forms.Button();
            this.butStart = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelDNDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFindResult)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheckSum)).BeginInit();
            this.panel8.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(228, 87);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(51, 46);
            this.button1.TabIndex = 33;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panelDNDetail
            // 
            this.panelDNDetail.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelDNDetail.Controls.Add(this.dgvFindResult);
            this.panelDNDetail.Controls.Add(this.groupBox3);
            this.panelDNDetail.Controls.Add(this.labShow);
            this.panelDNDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDNDetail.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.panelDNDetail.Location = new System.Drawing.Point(4, 25);
            this.panelDNDetail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelDNDetail.Name = "panelDNDetail";
            this.panelDNDetail.Size = new System.Drawing.Size(1534, 318);
            this.panelDNDetail.TabIndex = 28;
            // 
            // dgvFindResult
            // 
            this.dgvFindResult.AllowUserToAddRows = false;
            this.dgvFindResult.AllowUserToDeleteRows = false;
            this.dgvFindResult.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvFindResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvFindResult.ColumnHeadersHeight = 30;
            this.dgvFindResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFindResult.Location = new System.Drawing.Point(0, 131);
            this.dgvFindResult.MultiSelect = false;
            this.dgvFindResult.Name = "dgvFindResult";
            this.dgvFindResult.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("新宋体", 10.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFindResult.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFindResult.RowHeadersWidth = 55;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.dgvFindResult.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvFindResult.RowTemplate.Height = 27;
            this.dgvFindResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvFindResult.Size = new System.Drawing.Size(1534, 187);
            this.dgvFindResult.TabIndex = 119;
            this.dgvFindResult.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvFindResult_RowsAdded);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbWHID);
            this.groupBox3.Controls.Add(this.labShipmentID);
            this.groupBox3.Controls.Add(this.cmbLocation);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btnSearch);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 57);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1534, 74);
            this.groupBox3.TabIndex = 118;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查询条件";
            // 
            // cmbWHID
            // 
            this.cmbWHID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWHID.FormattingEnabled = true;
            this.cmbWHID.Location = new System.Drawing.Point(160, 31);
            this.cmbWHID.Name = "cmbWHID";
            this.cmbWHID.Size = new System.Drawing.Size(190, 29);
            this.cmbWHID.TabIndex = 110;
            this.cmbWHID.SelectedIndexChanged += new System.EventHandler(this.cmbWHID_SelectedIndexChanged);
            // 
            // labShipmentID
            // 
            this.labShipmentID.AutoSize = true;
            this.labShipmentID.Location = new System.Drawing.Point(22, 35);
            this.labShipmentID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labShipmentID.Name = "labShipmentID";
            this.labShipmentID.Size = new System.Drawing.Size(76, 21);
            this.labShipmentID.TabIndex = 109;
            this.labShipmentID.Text = "仓库：";
            // 
            // cmbLocation
            // 
            this.cmbLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(487, 31);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(259, 29);
            this.cmbLocation.TabIndex = 108;
            this.cmbLocation.SelectedIndexChanged += new System.EventHandler(this.cmbLocation_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(380, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 21);
            this.label3.TabIndex = 107;
            this.label3.Text = "储位：";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(778, 26);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(112, 34);
            this.btnSearch.TabIndex = 82;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Enabled = false;
            this.labShow.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.SystemColors.Info;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labShow.Name = "labShow";
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(1534, 57);
            this.labShow.TabIndex = 61;
            this.labShow.Text = "储位盘点";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 343);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1534, 481);
            this.panel1.TabIndex = 65;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.TextMsg);
            this.panel3.Controls.Add(this.btnExport);
            this.panel3.Controls.Add(this.dgvCheckSum);
            this.panel3.Controls.Add(this.panel8);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1534, 481);
            this.panel3.TabIndex = 103;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvDetail);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 248);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1534, 170);
            this.panel2.TabIndex = 96;
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.AllowUserToDeleteRows = false;
            this.dgvDetail.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvDetail.ColumnHeadersHeight = 30;
            this.dgvDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sn,
            this.cartonid,
            this.partno,
            this.location,
            this.palletno,
            this.SERIAL_NUMBER});
            this.dgvDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetail.Location = new System.Drawing.Point(0, 8);
            this.dgvDetail.MultiSelect = false;
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDetail.RowHeadersWidth = 55;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.dgvDetail.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDetail.RowTemplate.Height = 27;
            this.dgvDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetail.Size = new System.Drawing.Size(1534, 162);
            this.dgvDetail.TabIndex = 96;
            this.dgvDetail.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvDetail_RowsAdded);
            // 
            // sn
            // 
            this.sn.HeaderText = "Customer SN";
            this.sn.MinimumWidth = 8;
            this.sn.Name = "sn";
            this.sn.ReadOnly = true;
            this.sn.Width = 150;
            // 
            // cartonid
            // 
            this.cartonid.HeaderText = "Carton ID";
            this.cartonid.MinimumWidth = 8;
            this.cartonid.Name = "cartonid";
            this.cartonid.ReadOnly = true;
            this.cartonid.Width = 150;
            // 
            // partno
            // 
            this.partno.HeaderText = "料号";
            this.partno.MinimumWidth = 8;
            this.partno.Name = "partno";
            this.partno.ReadOnly = true;
            this.partno.Width = 150;
            // 
            // location
            // 
            this.location.HeaderText = "储位";
            this.location.MinimumWidth = 8;
            this.location.Name = "location";
            this.location.ReadOnly = true;
            this.location.Width = 150;
            // 
            // palletno
            // 
            this.palletno.HeaderText = "Pallet NO";
            this.palletno.MinimumWidth = 8;
            this.palletno.Name = "palletno";
            this.palletno.ReadOnly = true;
            this.palletno.Width = 150;
            // 
            // SERIAL_NUMBER
            // 
            this.SERIAL_NUMBER.HeaderText = "Serial Number";
            this.SERIAL_NUMBER.MinimumWidth = 8;
            this.SERIAL_NUMBER.Name = "SERIAL_NUMBER";
            this.SERIAL_NUMBER.ReadOnly = true;
            this.SERIAL_NUMBER.Width = 150;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1534, 8);
            this.panel4.TabIndex = 0;
            // 
            // TextMsg
            // 
            this.TextMsg.AutoEllipsis = true;
            this.TextMsg.BackColor = System.Drawing.Color.White;
            this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 418);
            this.TextMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1534, 63);
            this.TextMsg.TabIndex = 66;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(6, 669);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(158, 32);
            this.btnExport.TabIndex = 90;
            this.btnExport.Text = "导出Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // dgvCheckSum
            // 
            this.dgvCheckSum.AllowUserToAddRows = false;
            this.dgvCheckSum.AllowUserToDeleteRows = false;
            this.dgvCheckSum.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCheckSum.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvCheckSum.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvCheckSum.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCheckSum.ColumnHeadersHeight = 30;
            this.dgvCheckSum.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvCheckSum.Location = new System.Drawing.Point(0, 66);
            this.dgvCheckSum.MultiSelect = false;
            this.dgvCheckSum.Name = "dgvCheckSum";
            this.dgvCheckSum.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCheckSum.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvCheckSum.RowHeadersWidth = 55;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            this.dgvCheckSum.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvCheckSum.RowTemplate.Height = 27;
            this.dgvCheckSum.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCheckSum.Size = new System.Drawing.Size(1534, 182);
            this.dgvCheckSum.TabIndex = 94;
            this.dgvCheckSum.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvCheckSum_RowsAdded);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel8.Controls.Add(this.lalFirst);
            this.panel8.Controls.Add(this.lblSN);
            this.panel8.Controls.Add(this.txtSN);
            this.panel8.Controls.Add(this.butEnd);
            this.panel8.Controls.Add(this.butStart);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1534, 66);
            this.panel8.TabIndex = 90;
            // 
            // lalFirst
            // 
            this.lalFirst.AutoSize = true;
            this.lalFirst.Location = new System.Drawing.Point(1090, 28);
            this.lalFirst.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lalFirst.Name = "lalFirst";
            this.lalFirst.Size = new System.Drawing.Size(17, 18);
            this.lalFirst.TabIndex = 110;
            this.lalFirst.Text = "N";
            this.lalFirst.Visible = false;
            // 
            // lblSN
            // 
            this.lblSN.AutoSize = true;
            this.lblSN.Location = new System.Drawing.Point(358, 30);
            this.lblSN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSN.Name = "lblSN";
            this.lblSN.Size = new System.Drawing.Size(206, 18);
            this.lblSN.TabIndex = 109;
            this.lblSN.Text = "Customer SN/Carton ID:";
            // 
            // txtSN
            // 
            this.txtSN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSN.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSN.ForeColor = System.Drawing.Color.Blue;
            this.txtSN.Location = new System.Drawing.Point(576, 9);
            this.txtSN.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtSN.Name = "txtSN";
            this.txtSN.ReadOnly = true;
            this.txtSN.Size = new System.Drawing.Size(436, 49);
            this.txtSN.TabIndex = 85;
            this.txtSN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSN_KeyDown);
            // 
            // butEnd
            // 
            this.butEnd.Enabled = false;
            this.butEnd.Location = new System.Drawing.Point(171, 15);
            this.butEnd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butEnd.Name = "butEnd";
            this.butEnd.Size = new System.Drawing.Size(135, 34);
            this.butEnd.TabIndex = 84;
            this.butEnd.Text = "结束作业";
            this.butEnd.UseVisualStyleBackColor = true;
            this.butEnd.Click += new System.EventHandler(this.butEnd_Click);
            // 
            // butStart
            // 
            this.butStart.Enabled = false;
            this.butStart.Location = new System.Drawing.Point(14, 15);
            this.butStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butStart.Name = "butStart";
            this.butStart.Size = new System.Drawing.Size(135, 34);
            this.butStart.TabIndex = 83;
            this.butStart.Text = "开始作业";
            this.butStart.UseVisualStyleBackColor = true;
            this.butStart.Click += new System.EventHandler(this.butStart_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.panelDNDetail);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(1542, 828);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            // 
            // fWMSCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1542, 828);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Name = "fWMSCheck";
            this.Text = "fWMSCheck";
            this.Load += new System.EventHandler(this.fWMSCheck_Load);
            this.panelDNDetail.ResumeLayout(false);
            this.panelDNDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFindResult)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheckSum)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelDNDetail;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DataGridView dgvCheckSum;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvFindResult;
        private System.Windows.Forms.Button butEnd;
        private System.Windows.Forms.Button butStart;
        private System.Windows.Forms.Label lblSN;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.ComboBox cmbWHID;
        private System.Windows.Forms.Label labShipmentID;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvDetail;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridViewTextBoxColumn sn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cartonid;
        private System.Windows.Forms.DataGridViewTextBoxColumn partno;
        private System.Windows.Forms.DataGridViewTextBoxColumn location;
        private System.Windows.Forms.DataGridViewTextBoxColumn palletno;
        private System.Windows.Forms.DataGridViewTextBoxColumn SERIAL_NUMBER;
        private System.Windows.Forms.Label lalFirst;
    }
}