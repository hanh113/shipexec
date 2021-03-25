namespace wmsReport
{
    partial class fWMSQuery
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.客户序号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.箱号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.料号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.储位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.栈板号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.精钢车行号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.位置点 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.TextMsg = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnEXCEL3 = new System.Windows.Forms.Button();
            this.lblSN = new System.Windows.Forms.Label();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.panelDNDetail = new System.Windows.Forms.Panel();
            this.labShow = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSN = new System.Windows.Forms.Button();
            this.btnHOLD = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.panel8.SuspendLayout();
            this.panelDNDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.panelDNDetail);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1190, 468);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1184, 408);
            this.panel1.TabIndex = 65;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.TextMsg);
            this.panel3.Controls.Add(this.btnExport);
            this.panel3.Controls.Add(this.panel8);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1184, 408);
            this.panel3.TabIndex = 103;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvDetail);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 77);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1184, 289);
            this.panel2.TabIndex = 96;
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.AllowUserToDeleteRows = false;
            this.dgvDetail.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvDetail.ColumnHeadersHeight = 30;
            this.dgvDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.客户序号,
            this.箱号,
            this.料号,
            this.储位,
            this.栈板号,
            this.精钢车行号,
            this.位置点});
            this.dgvDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetail.Location = new System.Drawing.Point(0, 5);
            this.dgvDetail.Margin = new System.Windows.Forms.Padding(2);
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
            this.dgvDetail.Size = new System.Drawing.Size(1184, 284);
            this.dgvDetail.TabIndex = 96;
            // 
            // 客户序号
            // 
            this.客户序号.HeaderText = "客户序号";
            this.客户序号.Name = "客户序号";
            this.客户序号.ReadOnly = true;
            this.客户序号.Width = 150;
            // 
            // 箱号
            // 
            this.箱号.HeaderText = "箱号";
            this.箱号.Name = "箱号";
            this.箱号.ReadOnly = true;
            this.箱号.Width = 170;
            // 
            // 料号
            // 
            this.料号.HeaderText = "料号";
            this.料号.Name = "料号";
            this.料号.ReadOnly = true;
            this.料号.Width = 140;
            // 
            // 储位
            // 
            this.储位.HeaderText = "储位";
            this.储位.Name = "储位";
            this.储位.ReadOnly = true;
            this.储位.Width = 150;
            // 
            // 栈板号
            // 
            this.栈板号.HeaderText = "栈板号";
            this.栈板号.Name = "栈板号";
            this.栈板号.ReadOnly = true;
            this.栈板号.Width = 200;
            // 
            // 精钢车行号
            // 
            this.精钢车行号.HeaderText = "精钢车行号";
            this.精钢车行号.Name = "精钢车行号";
            this.精钢车行号.ReadOnly = true;
            this.精钢车行号.Width = 200;
            // 
            // 位置点
            // 
            this.位置点.HeaderText = "位置点";
            this.位置点.Name = "位置点";
            this.位置点.ReadOnly = true;
            this.位置点.Width = 60;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1184, 5);
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
            this.TextMsg.Location = new System.Drawing.Point(0, 366);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1184, 42);
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
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel8.Controls.Add(this.btnHOLD);
            this.panel8.Controls.Add(this.btnSN);
            this.panel8.Controls.Add(this.btnEXCEL3);
            this.panel8.Controls.Add(this.lblSN);
            this.panel8.Controls.Add(this.txtSN);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1184, 77);
            this.panel8.TabIndex = 90;
            // 
            // btnEXCEL3
            // 
            this.btnEXCEL3.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnEXCEL3.Location = new System.Drawing.Point(679, 26);
            this.btnEXCEL3.Name = "btnEXCEL3";
            this.btnEXCEL3.Size = new System.Drawing.Size(87, 29);
            this.btnEXCEL3.TabIndex = 137;
            this.btnEXCEL3.Text = "导出EXCEL";
            this.btnEXCEL3.UseVisualStyleBackColor = true;
            this.btnEXCEL3.Click += new System.EventHandler(this.btnEXCEL3_Click);
            // 
            // lblSN
            // 
            this.lblSN.AutoSize = true;
            this.lblSN.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSN.Location = new System.Drawing.Point(63, 31);
            this.lblSN.Name = "lblSN";
            this.lblSN.Size = new System.Drawing.Size(206, 16);
            this.lblSN.TabIndex = 109;
            this.lblSN.Text = "CSN/CartonNO/PALLETNO:";
            // 
            // txtSN
            // 
            this.txtSN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSN.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSN.ForeColor = System.Drawing.Color.Blue;
            this.txtSN.Location = new System.Drawing.Point(276, 4);
            this.txtSN.Margin = new System.Windows.Forms.Padding(4);
            this.txtSN.Multiline = true;
            this.txtSN.Name = "txtSN";
            this.txtSN.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSN.Size = new System.Drawing.Size(292, 67);
            this.txtSN.TabIndex = 85;
            this.txtSN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSN_KeyDown);
            // 
            // panelDNDetail
            // 
            this.panelDNDetail.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelDNDetail.Controls.Add(this.labShow);
            this.panelDNDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDNDetail.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.panelDNDetail.Location = new System.Drawing.Point(3, 17);
            this.panelDNDetail.Name = "panelDNDetail";
            this.panelDNDetail.Size = new System.Drawing.Size(1184, 40);
            this.panelDNDetail.TabIndex = 28;
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Enabled = false;
            this.labShow.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.SystemColors.Info;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Name = "labShow";
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(1184, 41);
            this.labShow.TabIndex = 62;
            this.labShow.Text = "储位查询";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(152, 58);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 31);
            this.button1.TabIndex = 35;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnSN
            // 
            this.btnSN.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSN.Location = new System.Drawing.Point(598, 26);
            this.btnSN.Name = "btnSN";
            this.btnSN.Size = new System.Drawing.Size(58, 29);
            this.btnSN.TabIndex = 138;
            this.btnSN.Text = "...";
            this.btnSN.UseVisualStyleBackColor = true;
            this.btnSN.Click += new System.EventHandler(this.btnSN_Click);
            // 
            // btnHOLD
            // 
            this.btnHOLD.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnHOLD.Location = new System.Drawing.Point(877, 27);
            this.btnHOLD.Name = "btnHOLD";
            this.btnHOLD.Size = new System.Drawing.Size(153, 29);
            this.btnHOLD.TabIndex = 139;
            this.btnHOLD.Text = "PPS中未出货HOLD数据";
            this.btnHOLD.UseVisualStyleBackColor = true;
            this.btnHOLD.Visible = false;
            this.btnHOLD.Click += new System.EventHandler(this.btnHOLD_Click);
            // 
            // fWMSQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1190, 468);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Name = "fWMSQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "fWMSQuery";
            this.Load += new System.EventHandler(this.fWMSQuery_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panelDNDetail.ResumeLayout(false);
            this.panelDNDetail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvDetail;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lblSN;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.Panel panelDNDetail;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnEXCEL3;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客户序号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 箱号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 料号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 储位;
        private System.Windows.Forms.DataGridViewTextBoxColumn 栈板号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 精钢车行号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 位置点;
        private System.Windows.Forms.Button btnSN;
        private System.Windows.Forms.Button btnHOLD;
    }
}