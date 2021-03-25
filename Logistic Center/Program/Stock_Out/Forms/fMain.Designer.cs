namespace Stock_Out
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
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbQty = new System.Windows.Forms.Label();
            this.lbcount = new System.Windows.Forms.Label();
            this.tbScan = new System.Windows.Forms.TextBox();
            this.lbScanInfo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvInfo = new System.Windows.Forms.DataGridView();
            this.CSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Carton = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pallet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnReprint = new System.Windows.Forms.Button();
            this.prgBanner.SuspendLayout();
            this.prgMain.SuspendLayout();
            this.prgFooter.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // prgBanner
            // 
            this.prgBanner.Controls.Add(this.menuStrip2);
            this.prgBanner.Size = new System.Drawing.Size(1282, 87);
            this.prgBanner.Controls.SetChildIndex(this.prgTitle, 0);
            this.prgBanner.Controls.SetChildIndex(this.menuStrip2, 0);
            // 
            // prgMain
            // 
            this.prgMain.Controls.Add(this.splitContainer1);
            this.prgMain.Location = new System.Drawing.Point(0, 87);
            this.prgMain.Size = new System.Drawing.Size(1282, 435);
            // 
            // prgTitle
            // 
            this.prgTitle.Size = new System.Drawing.Size(1282, 87);
            // 
            // prgFooter
            // 
            this.prgFooter.Location = new System.Drawing.Point(0, 522);
            this.prgFooter.Size = new System.Drawing.Size(1282, 72);
            // 
            // prgMSG
            // 
            this.prgMSG.Size = new System.Drawing.Size(1282, 72);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip2.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setupToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(30, 9);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(84, 32);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip2";
            this.menuStrip2.Visible = false;
            // 
            // setupToolStripMenuItem
            // 
            this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
            this.setupToolStripMenuItem.Size = new System.Drawing.Size(76, 28);
            this.setupToolStripMenuItem.Text = "Setup";
            this.setupToolStripMenuItem.Click += new System.EventHandler(this.setupToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(1276, 408);
            this.splitContainer1.SplitterDistance = 338;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.btnReprint);
            this.panel1.Controls.Add(this.lbQty);
            this.panel1.Controls.Add(this.lbcount);
            this.panel1.Controls.Add(this.tbScan);
            this.panel1.Controls.Add(this.lbScanInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(338, 408);
            this.panel1.TabIndex = 0;
            // 
            // lbQty
            // 
            this.lbQty.AutoSize = true;
            this.lbQty.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbQty.Location = new System.Drawing.Point(101, 153);
            this.lbQty.Name = "lbQty";
            this.lbQty.Size = new System.Drawing.Size(31, 36);
            this.lbQty.TabIndex = 3;
            this.lbQty.Text = "0";
            // 
            // lbcount
            // 
            this.lbcount.AutoSize = true;
            this.lbcount.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbcount.Location = new System.Drawing.Point(19, 156);
            this.lbcount.Name = "lbcount";
            this.lbcount.Size = new System.Drawing.Size(75, 28);
            this.lbcount.TabIndex = 2;
            this.lbcount.Text = "数量：";
            // 
            // tbScan
            // 
            this.tbScan.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbScan.Location = new System.Drawing.Point(22, 85);
            this.tbScan.Name = "tbScan";
            this.tbScan.Size = new System.Drawing.Size(303, 39);
            this.tbScan.TabIndex = 1;
            this.tbScan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbScan_KeyDown);
            // 
            // lbScanInfo
            // 
            this.lbScanInfo.AutoSize = true;
            this.lbScanInfo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbScanInfo.Location = new System.Drawing.Point(19, 37);
            this.lbScanInfo.Name = "lbScanInfo";
            this.lbScanInfo.Size = new System.Drawing.Size(147, 28);
            this.lbScanInfo.TabIndex = 0;
            this.lbScanInfo.Text = "箱号/栈板号：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvInfo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(934, 408);
            this.panel2.TabIndex = 1;
            // 
            // dgvInfo
            // 
            this.dgvInfo.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgvInfo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvInfo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CSN,
            this.Carton,
            this.Pallet,
            this.PartNo,
            this.Location});
            this.dgvInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInfo.Location = new System.Drawing.Point(0, 0);
            this.dgvInfo.Name = "dgvInfo";
            this.dgvInfo.ReadOnly = true;
            this.dgvInfo.RowHeadersVisible = false;
            this.dgvInfo.RowHeadersWidth = 62;
            this.dgvInfo.RowTemplate.Height = 30;
            this.dgvInfo.Size = new System.Drawing.Size(934, 408);
            this.dgvInfo.TabIndex = 0;
            // 
            // CSN
            // 
            this.CSN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CSN.DataPropertyName = "customer_sn";
            this.CSN.HeaderText = "CSN";
            this.CSN.MinimumWidth = 8;
            this.CSN.Name = "CSN";
            this.CSN.ReadOnly = true;
            // 
            // Carton
            // 
            this.Carton.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Carton.DataPropertyName = "carton_no";
            this.Carton.HeaderText = "Carton";
            this.Carton.MinimumWidth = 8;
            this.Carton.Name = "Carton";
            this.Carton.ReadOnly = true;
            // 
            // Pallet
            // 
            this.Pallet.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Pallet.DataPropertyName = "pallet_no";
            this.Pallet.HeaderText = "Pallet";
            this.Pallet.MinimumWidth = 8;
            this.Pallet.Name = "Pallet";
            this.Pallet.ReadOnly = true;
            // 
            // PartNo
            // 
            this.PartNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PartNo.DataPropertyName = "part_no";
            this.PartNo.HeaderText = "PartNo";
            this.PartNo.MinimumWidth = 8;
            this.PartNo.Name = "PartNo";
            this.PartNo.ReadOnly = true;
            // 
            // Location
            // 
            this.Location.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Location.DataPropertyName = "location_no";
            this.Location.HeaderText = "Location";
            this.Location.MinimumWidth = 8;
            this.Location.Name = "Location";
            this.Location.ReadOnly = true;
            // 
            // btnReprint
            // 
            this.btnReprint.Location = new System.Drawing.Point(22, 346);
            this.btnReprint.Name = "btnReprint";
            this.btnReprint.Size = new System.Drawing.Size(303, 44);
            this.btnReprint.TabIndex = 4;
            this.btnReprint.Text = "Reprint";
            this.btnReprint.UseVisualStyleBackColor = true;
            this.btnReprint.Click += new System.EventHandler(this.btnReprint_Click);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 594);
            this.Name = "fMain";
            this.Text = "Stock_out";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.prgBanner.ResumeLayout(false);
            this.prgBanner.PerformLayout();
            this.prgMain.ResumeLayout(false);
            this.prgFooter.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbQty;
        private System.Windows.Forms.Label lbcount;
        private System.Windows.Forms.TextBox tbScan;
        private System.Windows.Forms.Label lbScanInfo;
        private System.Windows.Forms.DataGridView dgvInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn Carton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pallet;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.Button btnReprint;
    }
}

