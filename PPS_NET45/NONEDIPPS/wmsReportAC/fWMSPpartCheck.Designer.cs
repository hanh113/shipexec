namespace wmsReportAC
{
    partial class fWMSPpartCheck
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
            this.labShow = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.TextMsg = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lblSN = new System.Windows.Forms.Label();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvFindResult = new System.Windows.Forms.DataGridView();
            this.CUSTOM_SN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TROLLEY_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PALLET_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SIDES_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LEVEL_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SEQ_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.POINTNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel8.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFindResult)).BeginInit();
            this.SuspendLayout();
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labShow.Enabled = false;
            this.labShow.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.SystemColors.Info;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Name = "labShow";
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(815, 41);
            this.labShow.TabIndex = 61;
            this.labShow.Text = "储位盘点";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labShow);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(815, 39);
            this.panel2.TabIndex = 62;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 475);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(815, 45);
            this.panel1.TabIndex = 66;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.TextMsg);
            this.panel3.Controls.Add(this.btnExport);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(815, 44);
            this.panel3.TabIndex = 103;
            // 
            // TextMsg
            // 
            this.TextMsg.AutoEllipsis = true;
            this.TextMsg.BackColor = System.Drawing.Color.White;
            this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(815, 44);
            this.TextMsg.TabIndex = 66;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(4, 446);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(105, 21);
            this.btnExport.TabIndex = 90;
            this.btnExport.Text = "导出Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox3.Controls.Add(this.cmbLocation);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btnSearch);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(0, 39);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(815, 63);
            this.groupBox3.TabIndex = 120;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查询条件";
            // 
            // cmbLocation
            // 
            this.cmbLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbLocation.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(137, 23);
            this.cmbLocation.Margin = new System.Windows.Forms.Padding(2);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(258, 27);
            this.cmbLocation.TabIndex = 108;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 107;
            this.label3.Text = "储位:";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(469, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(92, 37);
            this.btnSearch.TabIndex = 82;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel8.Controls.Add(this.lblSN);
            this.panel8.Controls.Add(this.txtSN);
            this.panel8.Controls.Add(this.btnEnd);
            this.panel8.Controls.Add(this.btnStart);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel8.Location = new System.Drawing.Point(0, 102);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(815, 60);
            this.panel8.TabIndex = 122;
            // 
            // lblSN
            // 
            this.lblSN.AutoSize = true;
            this.lblSN.Location = new System.Drawing.Point(25, 18);
            this.lblSN.Name = "lblSN";
            this.lblSN.Size = new System.Drawing.Size(107, 16);
            this.lblSN.TabIndex = 109;
            this.lblSN.Text = "CustomerSN:";
            // 
            // txtSN
            // 
            this.txtSN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSN.Enabled = false;
            this.txtSN.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSN.ForeColor = System.Drawing.Color.Blue;
            this.txtSN.Location = new System.Drawing.Point(137, 12);
            this.txtSN.Margin = new System.Windows.Forms.Padding(4);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(258, 30);
            this.txtSN.TabIndex = 85;
            this.txtSN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSN_KeyDown);
            // 
            // btnEnd
            // 
            this.btnEnd.Enabled = false;
            this.btnEnd.Location = new System.Drawing.Point(580, 12);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(92, 38);
            this.btnEnd.TabIndex = 84;
            this.btnEnd.Text = "结束作业";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(469, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(92, 38);
            this.btnStart.TabIndex = 83;
            this.btnStart.Text = "开始作业";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvFindResult);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 162);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(815, 313);
            this.groupBox1.TabIndex = 123;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "储位信息";
            // 
            // dgvFindResult
            // 
            this.dgvFindResult.AllowUserToAddRows = false;
            this.dgvFindResult.AllowUserToDeleteRows = false;
            this.dgvFindResult.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvFindResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvFindResult.ColumnHeadersHeight = 30;
            this.dgvFindResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CUSTOM_SN,
            this.TROLLEY_NO,
            this.PALLET_NO,
            this.SIDES_NO,
            this.LEVEL_NO,
            this.SEQ_NO,
            this.POINTNO});
            this.dgvFindResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFindResult.Location = new System.Drawing.Point(3, 17);
            this.dgvFindResult.Margin = new System.Windows.Forms.Padding(2);
            this.dgvFindResult.MultiSelect = false;
            this.dgvFindResult.Name = "dgvFindResult";
            this.dgvFindResult.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.dgvFindResult.Size = new System.Drawing.Size(809, 293);
            this.dgvFindResult.TabIndex = 119;
            // 
            // CUSTOM_SN
            // 
            this.CUSTOM_SN.HeaderText = "CUSTOM_SN";
            this.CUSTOM_SN.Name = "CUSTOM_SN";
            this.CUSTOM_SN.ReadOnly = true;
            // 
            // TROLLEY_NO
            // 
            this.TROLLEY_NO.HeaderText = "TROLLEY_NO";
            this.TROLLEY_NO.Name = "TROLLEY_NO";
            this.TROLLEY_NO.ReadOnly = true;
            // 
            // PALLET_NO
            // 
            this.PALLET_NO.HeaderText = "PALLET_NO";
            this.PALLET_NO.Name = "PALLET_NO";
            this.PALLET_NO.ReadOnly = true;
            // 
            // SIDES_NO
            // 
            this.SIDES_NO.HeaderText = "SIDES_NO";
            this.SIDES_NO.Name = "SIDES_NO";
            this.SIDES_NO.ReadOnly = true;
            // 
            // LEVEL_NO
            // 
            this.LEVEL_NO.HeaderText = "LEVEL_NO";
            this.LEVEL_NO.Name = "LEVEL_NO";
            this.LEVEL_NO.ReadOnly = true;
            // 
            // SEQ_NO
            // 
            this.SEQ_NO.HeaderText = "SEQ_NO";
            this.SEQ_NO.Name = "SEQ_NO";
            this.SEQ_NO.ReadOnly = true;
            // 
            // POINTNO
            // 
            this.POINTNO.HeaderText = "POINTNO";
            this.POINTNO.Name = "POINTNO";
            this.POINTNO.ReadOnly = true;
            // 
            // fWMSPpartCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 520);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "fWMSPpartCheck";
            this.Text = "fWMSPpartCheck";
            this.Load += new System.EventHandler(this.fWMSPpartCheck_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFindResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lblSN;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvFindResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUSTOM_SN;
        private System.Windows.Forms.DataGridViewTextBoxColumn TROLLEY_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PALLET_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SIDES_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn LEVEL_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SEQ_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn POINTNO;
    }
}