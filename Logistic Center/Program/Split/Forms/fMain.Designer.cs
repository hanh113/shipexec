namespace Split
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbSN = new System.Windows.Forms.TextBox();
            this.tbMo = new System.Windows.Forms.TextBox();
            this.lbSN = new System.Windows.Forms.Label();
            this.lbQty = new System.Windows.Forms.Label();
            this.lbCount = new System.Windows.Forms.Label();
            this.lbPart = new System.Windows.Forms.Label();
            this.lbkp = new System.Windows.Forms.Label();
            this.lbMO = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvBom = new System.Windows.Forms.DataGridView();
            this.mo_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.key_part_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust_part_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.part_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.customer_sn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.part_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.carton_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pallet_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prgBanner.SuspendLayout();
            this.prgMain.SuspendLayout();
            this.prgFooter.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // prgBanner
            // 
            this.prgBanner.Controls.Add(this.menuStrip1);
            this.prgBanner.Size = new System.Drawing.Size(1083, 79);
            this.prgBanner.Controls.SetChildIndex(this.prgTitle, 0);
            this.prgBanner.Controls.SetChildIndex(this.menuStrip1, 0);
            // 
            // prgMain
            // 
            this.prgMain.Controls.Add(this.tableLayoutPanel1);
            this.prgMain.Size = new System.Drawing.Size(1083, 467);
            // 
            // prgTitle
            // 
            this.prgTitle.Size = new System.Drawing.Size(1083, 79);
            // 
            // prgFooter
            // 
            this.prgFooter.Location = new System.Drawing.Point(0, 546);
            this.prgFooter.Size = new System.Drawing.Size(1083, 72);
            // 
            // prgMSG
            // 
            this.prgMSG.Size = new System.Drawing.Size(1083, 72);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setupToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(29, 9);
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
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1077, 440);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.tbSN);
            this.panel1.Controls.Add(this.tbMo);
            this.panel1.Controls.Add(this.lbSN);
            this.panel1.Controls.Add(this.lbQty);
            this.panel1.Controls.Add(this.lbCount);
            this.panel1.Controls.Add(this.lbPart);
            this.panel1.Controls.Add(this.lbkp);
            this.panel1.Controls.Add(this.lbMO);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1071, 126);
            this.panel1.TabIndex = 0;
            // 
            // tbSN
            // 
            this.tbSN.Enabled = false;
            this.tbSN.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSN.Location = new System.Drawing.Point(104, 68);
            this.tbSN.Name = "tbSN";
            this.tbSN.Size = new System.Drawing.Size(354, 44);
            this.tbSN.TabIndex = 1;
            this.tbSN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSN_KeyDown);
            // 
            // tbMo
            // 
            this.tbMo.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMo.Location = new System.Drawing.Point(104, 14);
            this.tbMo.Name = "tbMo";
            this.tbMo.Size = new System.Drawing.Size(354, 44);
            this.tbMo.TabIndex = 1;
            this.tbMo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbMo_KeyDown);
            // 
            // lbSN
            // 
            this.lbSN.AutoSize = true;
            this.lbSN.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSN.Location = new System.Drawing.Point(39, 74);
            this.lbSN.Name = "lbSN";
            this.lbSN.Size = new System.Drawing.Size(61, 36);
            this.lbSN.TabIndex = 0;
            this.lbSN.Text = "SN:";
            // 
            // lbQty
            // 
            this.lbQty.AutoSize = true;
            this.lbQty.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbQty.Location = new System.Drawing.Point(651, 74);
            this.lbQty.Name = "lbQty";
            this.lbQty.Size = new System.Drawing.Size(31, 36);
            this.lbQty.TabIndex = 0;
            this.lbQty.Text = "0";
            // 
            // lbCount
            // 
            this.lbCount.AutoSize = true;
            this.lbCount.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCount.Location = new System.Drawing.Point(553, 73);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(78, 36);
            this.lbCount.TabIndex = 0;
            this.lbCount.Text = "QTY:";
            // 
            // lbPart
            // 
            this.lbPart.AutoSize = true;
            this.lbPart.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPart.Location = new System.Drawing.Point(634, 13);
            this.lbPart.Name = "lbPart";
            this.lbPart.Size = new System.Drawing.Size(70, 36);
            this.lbPart.TabIndex = 0;
            this.lbPart.Text = "N/A";
            // 
            // lbkp
            // 
            this.lbkp.AutoSize = true;
            this.lbkp.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbkp.Location = new System.Drawing.Point(556, 13);
            this.lbkp.Name = "lbkp";
            this.lbkp.Size = new System.Drawing.Size(75, 36);
            this.lbkp.TabIndex = 0;
            this.lbkp.Text = "Part:";
            // 
            // lbMO
            // 
            this.lbMO.AutoSize = true;
            this.lbMO.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMO.Location = new System.Drawing.Point(23, 13);
            this.lbMO.Name = "lbMO";
            this.lbMO.Size = new System.Drawing.Size(93, 36);
            this.lbMO.TabIndex = 0;
            this.lbMO.Text = "MO：";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 135);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvBom);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvDetail);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(1071, 302);
            this.splitContainer1.SplitterDistance = 507;
            this.splitContainer1.TabIndex = 1;
            // 
            // dgvBom
            // 
            this.dgvBom.AllowUserToAddRows = false;
            this.dgvBom.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvBom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBom.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mo_no,
            this.key_part_no,
            this.cust_part_no,
            this.part_desc});
            this.dgvBom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBom.Location = new System.Drawing.Point(0, 0);
            this.dgvBom.Name = "dgvBom";
            this.dgvBom.ReadOnly = true;
            this.dgvBom.RowHeadersVisible = false;
            this.dgvBom.RowHeadersWidth = 62;
            this.dgvBom.RowTemplate.Height = 30;
            this.dgvBom.Size = new System.Drawing.Size(507, 302);
            this.dgvBom.TabIndex = 0;
            // 
            // mo_no
            // 
            this.mo_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.mo_no.DataPropertyName = "mo_no";
            this.mo_no.HeaderText = "Mo_No";
            this.mo_no.MinimumWidth = 8;
            this.mo_no.Name = "mo_no";
            this.mo_no.ReadOnly = true;
            // 
            // key_part_no
            // 
            this.key_part_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.key_part_no.DataPropertyName = "key_part_no";
            this.key_part_no.HeaderText = "Part_No";
            this.key_part_no.MinimumWidth = 8;
            this.key_part_no.Name = "key_part_no";
            this.key_part_no.ReadOnly = true;
            // 
            // cust_part_no
            // 
            this.cust_part_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cust_part_no.DataPropertyName = "cust_part_no";
            this.cust_part_no.HeaderText = "MPN";
            this.cust_part_no.MinimumWidth = 8;
            this.cust_part_no.Name = "cust_part_no";
            this.cust_part_no.ReadOnly = true;
            // 
            // part_desc
            // 
            this.part_desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.part_desc.DataPropertyName = "part_desc";
            this.part_desc.HeaderText = "Part_Desc";
            this.part_desc.MinimumWidth = 8;
            this.part_desc.Name = "part_desc";
            this.part_desc.ReadOnly = true;
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.customer_sn,
            this.part_no,
            this.carton_no,
            this.pallet_no});
            this.dgvDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetail.Location = new System.Drawing.Point(0, 0);
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.RowHeadersVisible = false;
            this.dgvDetail.RowHeadersWidth = 62;
            this.dgvDetail.RowTemplate.Height = 30;
            this.dgvDetail.Size = new System.Drawing.Size(560, 302);
            this.dgvDetail.TabIndex = 0;
            // 
            // customer_sn
            // 
            this.customer_sn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.customer_sn.DataPropertyName = "customer_sn";
            this.customer_sn.HeaderText = "customer_sn";
            this.customer_sn.MinimumWidth = 8;
            this.customer_sn.Name = "customer_sn";
            // 
            // part_no
            // 
            this.part_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.part_no.DataPropertyName = "part_no";
            this.part_no.HeaderText = "part_no";
            this.part_no.MinimumWidth = 8;
            this.part_no.Name = "part_no";
            // 
            // carton_no
            // 
            this.carton_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.carton_no.DataPropertyName = "carton_no";
            this.carton_no.HeaderText = "carton_no";
            this.carton_no.MinimumWidth = 8;
            this.carton_no.Name = "carton_no";
            // 
            // pallet_no
            // 
            this.pallet_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.pallet_no.DataPropertyName = "pallet_no";
            this.pallet_no.HeaderText = "pallet_no";
            this.pallet_no.MinimumWidth = 8;
            this.pallet_no.Name = "pallet_no";
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 618);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "fMain";
            this.Text = "Split";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fMain_Load);
            this.prgBanner.ResumeLayout(false);
            this.prgBanner.PerformLayout();
            this.prgMain.ResumeLayout(false);
            this.prgFooter.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbSN;
        private System.Windows.Forms.TextBox tbMo;
        private System.Windows.Forms.Label lbSN;
        private System.Windows.Forms.Label lbQty;
        private System.Windows.Forms.Label lbCount;
        private System.Windows.Forms.Label lbPart;
        private System.Windows.Forms.Label lbkp;
        private System.Windows.Forms.Label lbMO;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvBom;
        private System.Windows.Forms.DataGridView dgvDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn mo_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn key_part_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_part_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn part_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_sn;
        private System.Windows.Forms.DataGridViewTextBoxColumn part_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn carton_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn pallet_no;
    }
}

