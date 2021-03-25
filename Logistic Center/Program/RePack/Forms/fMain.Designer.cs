namespace RePack
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvMoInfo = new System.Windows.Forms.DataGridView();
            this.dgvPalletInfo = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lbQty = new System.Windows.Forms.Label();
            this.lbPackedQty = new System.Windows.Forms.Label();
            this.btnClosePallet = new System.Windows.Forms.Button();
            this.cbAuto = new System.Windows.Forms.CheckBox();
            this.tbSN = new System.Windows.Forms.TextBox();
            this.tbPallet = new System.Windows.Forms.TextBox();
            this.tbMo = new System.Windows.Forms.TextBox();
            this.lbPallet = new System.Windows.Forms.Label();
            this.lbSN = new System.Windows.Forms.Label();
            this.lbMO = new System.Windows.Forms.Label();
            this.prgBanner.SuspendLayout();
            this.prgMain.SuspendLayout();
            this.prgFooter.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMoInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPalletInfo)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // prgBanner
            // 
            this.prgBanner.Controls.Add(this.menuStrip1);
            this.prgBanner.Size = new System.Drawing.Size(1389, 79);
            this.prgBanner.Controls.SetChildIndex(this.prgTitle, 0);
            this.prgBanner.Controls.SetChildIndex(this.menuStrip1, 0);
            // 
            // prgMain
            // 
            this.prgMain.Controls.Add(this.tableLayoutPanel1);
            this.prgMain.Size = new System.Drawing.Size(1389, 429);
            // 
            // prgTitle
            // 
            this.prgTitle.Size = new System.Drawing.Size(1389, 79);
            // 
            // prgFooter
            // 
            this.prgFooter.Location = new System.Drawing.Point(0, 508);
            this.prgFooter.Size = new System.Drawing.Size(1389, 72);
            // 
            // prgMSG
            // 
            this.prgMSG.Size = new System.Drawing.Size(1389, 72);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setupToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(144, 13);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(84, 32);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // setupToolStripMenuItem
            // 
            this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
            this.setupToolStripMenuItem.Size = new System.Drawing.Size(76, 28);
            this.setupToolStripMenuItem.Text = "Setup";
            this.setupToolStripMenuItem.Click += new System.EventHandler(this.setupToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1383, 402);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 103);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.splitContainer1.Panel1.Controls.Add(this.dgvMoInfo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.splitContainer1.Panel2.Controls.Add(this.dgvPalletInfo);
            this.splitContainer1.Size = new System.Drawing.Size(1377, 296);
            this.splitContainer1.SplitterDistance = 363;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvMoInfo
            // 
            this.dgvMoInfo.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgvMoInfo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMoInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMoInfo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvMoInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMoInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMoInfo.Location = new System.Drawing.Point(0, 0);
            this.dgvMoInfo.Name = "dgvMoInfo";
            this.dgvMoInfo.RowHeadersVisible = false;
            this.dgvMoInfo.RowHeadersWidth = 62;
            this.dgvMoInfo.RowTemplate.Height = 30;
            this.dgvMoInfo.Size = new System.Drawing.Size(363, 296);
            this.dgvMoInfo.TabIndex = 0;
            // 
            // dgvPalletInfo
            // 
            this.dgvPalletInfo.AllowUserToAddRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgvPalletInfo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPalletInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPalletInfo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvPalletInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPalletInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPalletInfo.Location = new System.Drawing.Point(0, 0);
            this.dgvPalletInfo.Name = "dgvPalletInfo";
            this.dgvPalletInfo.RowHeadersVisible = false;
            this.dgvPalletInfo.RowHeadersWidth = 62;
            this.dgvPalletInfo.RowTemplate.Height = 30;
            this.dgvPalletInfo.Size = new System.Drawing.Size(1010, 296);
            this.dgvPalletInfo.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.lbQty);
            this.panel1.Controls.Add(this.lbPackedQty);
            this.panel1.Controls.Add(this.btnClosePallet);
            this.panel1.Controls.Add(this.cbAuto);
            this.panel1.Controls.Add(this.tbSN);
            this.panel1.Controls.Add(this.tbPallet);
            this.panel1.Controls.Add(this.tbMo);
            this.panel1.Controls.Add(this.lbPallet);
            this.panel1.Controls.Add(this.lbSN);
            this.panel1.Controls.Add(this.lbMO);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1377, 94);
            this.panel1.TabIndex = 1;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnPrint.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrint.Location = new System.Drawing.Point(1254, 6);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(117, 84);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "Print Label";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lbQty
            // 
            this.lbQty.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbQty.AutoSize = true;
            this.lbQty.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbQty.Location = new System.Drawing.Point(1088, 56);
            this.lbQty.Name = "lbQty";
            this.lbQty.Size = new System.Drawing.Size(28, 31);
            this.lbQty.TabIndex = 4;
            this.lbQty.Text = "0";
            // 
            // lbPackedQty
            // 
            this.lbPackedQty.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbPackedQty.AutoSize = true;
            this.lbPackedQty.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPackedQty.Location = new System.Drawing.Point(1029, 8);
            this.lbPackedQty.Name = "lbPackedQty";
            this.lbPackedQty.Size = new System.Drawing.Size(145, 31);
            this.lbPackedQty.TabIndex = 4;
            this.lbPackedQty.Text = "Packed Qty";
            // 
            // btnClosePallet
            // 
            this.btnClosePallet.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnClosePallet.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClosePallet.Location = new System.Drawing.Point(868, 6);
            this.btnClosePallet.Name = "btnClosePallet";
            this.btnClosePallet.Size = new System.Drawing.Size(117, 84);
            this.btnClosePallet.TabIndex = 3;
            this.btnClosePallet.Text = "Close Pallet";
            this.btnClosePallet.UseVisualStyleBackColor = true;
            this.btnClosePallet.Click += new System.EventHandler(this.btnClosePallet_Click);
            // 
            // cbAuto
            // 
            this.cbAuto.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbAuto.AutoSize = true;
            this.cbAuto.Enabled = false;
            this.cbAuto.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbAuto.Location = new System.Drawing.Point(551, 55);
            this.cbAuto.Name = "cbAuto";
            this.cbAuto.Size = new System.Drawing.Size(208, 35);
            this.cbAuto.TabIndex = 2;
            this.cbAuto.Text = "Auto Pallet No";
            this.cbAuto.UseVisualStyleBackColor = true;
            this.cbAuto.CheckedChanged += new System.EventHandler(this.cbAuto_CheckedChanged);
            // 
            // tbSN
            // 
            this.tbSN.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbSN.Enabled = false;
            this.tbSN.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSN.Location = new System.Drawing.Point(108, 53);
            this.tbSN.Name = "tbSN";
            this.tbSN.Size = new System.Drawing.Size(269, 39);
            this.tbSN.TabIndex = 1;
            this.tbSN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSN_KeyDown);
            // 
            // tbPallet
            // 
            this.tbPallet.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbPallet.Enabled = false;
            this.tbPallet.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPallet.Location = new System.Drawing.Point(551, 8);
            this.tbPallet.Name = "tbPallet";
            this.tbPallet.Size = new System.Drawing.Size(269, 39);
            this.tbPallet.TabIndex = 1;
            this.tbPallet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPallet_KeyDown);
            // 
            // tbMo
            // 
            this.tbMo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbMo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMo.Location = new System.Drawing.Point(108, 6);
            this.tbMo.Name = "tbMo";
            this.tbMo.Size = new System.Drawing.Size(269, 39);
            this.tbMo.TabIndex = 1;
            this.tbMo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbMo_KeyDown);
            // 
            // lbPallet
            // 
            this.lbPallet.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbPallet.AutoSize = true;
            this.lbPallet.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPallet.Location = new System.Drawing.Point(420, 11);
            this.lbPallet.Name = "lbPallet";
            this.lbPallet.Size = new System.Drawing.Size(125, 31);
            this.lbPallet.TabIndex = 0;
            this.lbPallet.Text = "Pallet No:";
            // 
            // lbSN
            // 
            this.lbSN.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbSN.AutoSize = true;
            this.lbSN.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSN.Location = new System.Drawing.Point(39, 59);
            this.lbSN.Name = "lbSN";
            this.lbSN.Size = new System.Drawing.Size(54, 31);
            this.lbSN.TabIndex = 0;
            this.lbSN.Text = "SN:";
            // 
            // lbMO
            // 
            this.lbMO.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbMO.AutoSize = true;
            this.lbMO.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMO.Location = new System.Drawing.Point(30, 11);
            this.lbMO.Name = "lbMO";
            this.lbMO.Size = new System.Drawing.Size(63, 31);
            this.lbMO.TabIndex = 0;
            this.lbMO.Text = "MO:";
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1389, 580);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "fMain";
            this.Text = "Repack";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fMain_Load);
            this.prgBanner.ResumeLayout(false);
            this.prgBanner.PerformLayout();
            this.prgMain.ResumeLayout(false);
            this.prgFooter.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMoInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPalletInfo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvMoInfo;
        private System.Windows.Forms.DataGridView dgvPalletInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbQty;
        private System.Windows.Forms.Label lbPackedQty;
        private System.Windows.Forms.Button btnClosePallet;
        private System.Windows.Forms.CheckBox cbAuto;
        private System.Windows.Forms.TextBox tbSN;
        private System.Windows.Forms.TextBox tbPallet;
        private System.Windows.Forms.TextBox tbMo;
        private System.Windows.Forms.Label lbPallet;
        private System.Windows.Forms.Label lbSN;
        private System.Windows.Forms.Label lbMO;
        private System.Windows.Forms.Button btnPrint;
    }
}

