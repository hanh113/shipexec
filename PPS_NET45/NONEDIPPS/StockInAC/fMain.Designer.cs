namespace StockInAC
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
            this.gbScan = new System.Windows.Forms.GroupBox();
            this.chkCarton = new System.Windows.Forms.CheckBox();
            this.txtPre = new System.Windows.Forms.TextBox();
            this.radNoSN = new System.Windows.Forms.RadioButton();
            this.radSN = new System.Windows.Forms.RadioButton();
            this.labPart = new System.Windows.Forms.Label();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.labLocation = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCarton = new System.Windows.Forms.TextBox();
            this.txtMsg = new System.Windows.Forms.Label();
            this.labShow = new System.Windows.Forms.TextBox();
            this.gbDNInfo = new System.Windows.Forms.GroupBox();
            this.dtgrvStockin = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnReset = new System.Windows.Forms.Button();
            this.gbScan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrvStockin)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbScan
            // 
            this.gbScan.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gbScan.Controls.Add(this.btnReset);
            this.gbScan.Controls.Add(this.panel1);
            this.gbScan.Controls.Add(this.chkCarton);
            this.gbScan.Controls.Add(this.txtPre);
            this.gbScan.Controls.Add(this.radNoSN);
            this.gbScan.Controls.Add(this.radSN);
            this.gbScan.Controls.Add(this.labPart);
            this.gbScan.Controls.Add(this.txtPart);
            this.gbScan.Controls.Add(this.labLocation);
            this.gbScan.Controls.Add(this.txtLocation);
            this.gbScan.Controls.Add(this.label3);
            this.gbScan.Controls.Add(this.txtCarton);
            this.gbScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbScan.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbScan.Location = new System.Drawing.Point(0, 49);
            this.gbScan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbScan.Name = "gbScan";
            this.gbScan.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbScan.Size = new System.Drawing.Size(1461, 642);
            this.gbScan.TabIndex = 74;
            this.gbScan.TabStop = false;
            this.gbScan.Text = "信息扫入";
            // 
            // chkCarton
            // 
            this.chkCarton.AutoSize = true;
            this.chkCarton.Checked = true;
            this.chkCarton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCarton.Location = new System.Drawing.Point(893, 52);
            this.chkCarton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkCarton.Name = "chkCarton";
            this.chkCarton.Size = new System.Drawing.Size(164, 23);
            this.chkCarton.TabIndex = 132;
            this.chkCarton.Text = "检查箱号前缀：";
            this.chkCarton.UseVisualStyleBackColor = true;
            this.chkCarton.Visible = false;
            // 
            // txtPre
            // 
            this.txtPre.BackColor = System.Drawing.Color.White;
            this.txtPre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPre.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPre.ForeColor = System.Drawing.Color.Blue;
            this.txtPre.Location = new System.Drawing.Point(1125, 44);
            this.txtPre.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtPre.Name = "txtPre";
            this.txtPre.Size = new System.Drawing.Size(299, 38);
            this.txtPre.TabIndex = 131;
            this.txtPre.Text = "CTN";
            this.txtPre.Visible = false;
            // 
            // radNoSN
            // 
            this.radNoSN.AutoSize = true;
            this.radNoSN.Location = new System.Drawing.Point(707, 51);
            this.radNoSN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radNoSN.Name = "radNoSN";
            this.radNoSN.Size = new System.Drawing.Size(69, 23);
            this.radNoSN.TabIndex = 129;
            this.radNoSN.Text = "无SN";
            this.radNoSN.UseVisualStyleBackColor = true;
            this.radNoSN.CheckedChanged += new System.EventHandler(this.radSN_CheckedChanged);
            // 
            // radSN
            // 
            this.radSN.AutoSize = true;
            this.radSN.Checked = true;
            this.radSN.Location = new System.Drawing.Point(403, 52);
            this.radSN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radSN.Name = "radSN";
            this.radSN.Size = new System.Drawing.Size(88, 23);
            this.radSN.TabIndex = 128;
            this.radSN.TabStop = true;
            this.radSN.Text = "有SN号";
            this.radSN.UseVisualStyleBackColor = true;
            this.radSN.CheckedChanged += new System.EventHandler(this.radSN_CheckedChanged);
            // 
            // labPart
            // 
            this.labPart.AutoSize = true;
            this.labPart.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.labPart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labPart.Location = new System.Drawing.Point(1012, 131);
            this.labPart.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labPart.Name = "labPart";
            this.labPart.Size = new System.Drawing.Size(53, 18);
            this.labPart.TabIndex = 126;
            this.labPart.Text = "料号:";
            this.labPart.Visible = false;
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.White;
            this.txtPart.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPart.Enabled = false;
            this.txtPart.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPart.ForeColor = System.Drawing.Color.Blue;
            this.txtPart.Location = new System.Drawing.Point(1125, 120);
            this.txtPart.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new System.Drawing.Size(299, 38);
            this.txtPart.TabIndex = 127;
            this.txtPart.Visible = false;
            this.txtPart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPart_KeyDown);
            // 
            // labLocation
            // 
            this.labLocation.AutoSize = true;
            this.labLocation.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.labLocation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labLocation.Location = new System.Drawing.Point(45, 131);
            this.labLocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labLocation.Name = "labLocation";
            this.labLocation.Size = new System.Drawing.Size(71, 18);
            this.labLocation.TabIndex = 124;
            this.labLocation.Text = "储位号:";
            // 
            // txtLocation
            // 
            this.txtLocation.BackColor = System.Drawing.Color.White;
            this.txtLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocation.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLocation.ForeColor = System.Drawing.Color.Blue;
            this.txtLocation.Location = new System.Drawing.Point(125, 120);
            this.txtLocation.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(299, 38);
            this.txtLocation.TabIndex = 125;
            this.txtLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocation_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(531, 131);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 18);
            this.label3.TabIndex = 122;
            this.label3.Text = "中箱号:";
            // 
            // txtCarton
            // 
            this.txtCarton.BackColor = System.Drawing.Color.White;
            this.txtCarton.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCarton.Enabled = false;
            this.txtCarton.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCarton.ForeColor = System.Drawing.Color.Blue;
            this.txtCarton.Location = new System.Drawing.Point(611, 120);
            this.txtCarton.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtCarton.Name = "txtCarton";
            this.txtCarton.Size = new System.Drawing.Size(299, 38);
            this.txtCarton.TabIndex = 123;
            this.txtCarton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCarton_KeyDown);
            // 
            // txtMsg
            // 
            this.txtMsg.AutoEllipsis = true;
            this.txtMsg.BackColor = System.Drawing.Color.Blue;
            this.txtMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.txtMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtMsg.Location = new System.Drawing.Point(0, 691);
            this.txtMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(1461, 51);
            this.txtMsg.TabIndex = 73;
            this.txtMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Enabled = false;
            this.labShow.Font = new System.Drawing.Font("SimSun", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.Color.White;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labShow.Name = "labShow";
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(1461, 49);
            this.labShow.TabIndex = 72;
            this.labShow.Text = "AppleCare StockIn";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gbDNInfo
            // 
            this.gbDNInfo.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gbDNInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDNInfo.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbDNInfo.Location = new System.Drawing.Point(0, 0);
            this.gbDNInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDNInfo.Name = "gbDNInfo";
            this.gbDNInfo.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDNInfo.Size = new System.Drawing.Size(1461, 742);
            this.gbDNInfo.TabIndex = 75;
            this.gbDNInfo.TabStop = false;
            this.gbDNInfo.Text = "DN信息";
            // 
            // dtgrvStockin
            // 
            this.dtgrvStockin.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtgrvStockin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgrvStockin.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dtgrvStockin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgrvStockin.Location = new System.Drawing.Point(0, 0);
            this.dtgrvStockin.Name = "dtgrvStockin";
            this.dtgrvStockin.RowHeadersWidth = 51;
            this.dtgrvStockin.RowTemplate.Height = 27;
            this.dtgrvStockin.Size = new System.Drawing.Size(1396, 361);
            this.dtgrvStockin.TabIndex = 133;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtgrvStockin);
            this.panel1.Location = new System.Drawing.Point(28, 208);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1396, 361);
            this.panel1.TabIndex = 134;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "location_no";
            this.Column1.HeaderText = "储位";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 76;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "pallet_no";
            this.Column2.HeaderText = "栈板号";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 95;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "carton_no";
            this.Column3.HeaderText = "箱号";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 76;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "part_no";
            this.Column4.HeaderText = "料号";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.Width = 76;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "cdt";
            this.Column5.HeaderText = "出库时间";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.Width = 114;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "qty";
            this.Column6.HeaderText = "数量";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.Width = 76;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(1128, 166);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(142, 32);
            this.btnReset.TabIndex = 135;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1461, 742);
            this.Controls.Add(this.gbScan);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.labShow);
            this.Controls.Add(this.gbDNInfo);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "fMain";
            this.Text = "fMain";
            this.Load += new System.EventHandler(this.fMain_Load);
            this.gbScan.ResumeLayout(false);
            this.gbScan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrvStockin)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbScan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCarton;
        private System.Windows.Forms.Label txtMsg;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.GroupBox gbDNInfo;
        private System.Windows.Forms.Label labLocation;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label labPart;
        private System.Windows.Forms.TextBox txtPart;
        private System.Windows.Forms.RadioButton radNoSN;
        private System.Windows.Forms.RadioButton radSN;
        private System.Windows.Forms.CheckBox chkCarton;
        private System.Windows.Forms.TextBox txtPre;
        private System.Windows.Forms.DataGridView dtgrvStockin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.Button btnReset;
    }
}

