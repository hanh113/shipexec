namespace WIP
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvBom = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn = new System.Windows.Forms.Button();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lbEnd = new System.Windows.Forms.Label();
            this.lbStart = new System.Windows.Forms.Label();
            this.dgvMo = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnQueryAll = new System.Windows.Forms.Button();
            this.btnSNExl = new System.Windows.Forms.Button();
            this.lbQty = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbScan = new System.Windows.Forms.TextBox();
            this.lbScan = new System.Windows.Forms.Label();
            this.dgvSN = new System.Windows.Forms.DataGridView();
            this.prgBanner.SuspendLayout();
            this.prgMain.SuspendLayout();
            this.prgFooter.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBom)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMo)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSN)).BeginInit();
            this.SuspendLayout();
            // 
            // prgBanner
            // 
            this.prgBanner.Size = new System.Drawing.Size(1259, 79);
            // 
            // prgMain
            // 
            this.prgMain.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.prgMain.Controls.Add(this.tabControl1);
            this.prgMain.Size = new System.Drawing.Size(1259, 483);
            // 
            // prgTitle
            // 
            this.prgTitle.Size = new System.Drawing.Size(1259, 79);
            this.prgTitle.Text = "WIP";
            // 
            // prgFooter
            // 
            this.prgFooter.Location = new System.Drawing.Point(0, 562);
            this.prgFooter.Size = new System.Drawing.Size(1259, 72);
            // 
            // prgMSG
            // 
            this.prgMSG.Size = new System.Drawing.Size(1259, 72);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1253, 456);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1245, 424);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Mo Info";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvBom, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvMo, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1239, 418);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dgvBom
            // 
            this.dgvBom.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgvBom.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBom.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBom.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvBom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBom.Location = new System.Drawing.Point(3, 295);
            this.dgvBom.Name = "dgvBom";
            this.dgvBom.RowHeadersVisible = false;
            this.dgvBom.RowHeadersWidth = 62;
            this.dgvBom.RowTemplate.Height = 30;
            this.dgvBom.Size = new System.Drawing.Size(1233, 120);
            this.dgvBom.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn);
            this.panel1.Controls.Add(this.dtpEnd);
            this.panel1.Controls.Add(this.dtpStart);
            this.panel1.Controls.Add(this.lbEnd);
            this.panel1.Controls.Add(this.lbStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1233, 77);
            this.panel1.TabIndex = 0;
            // 
            // btn
            // 
            this.btn.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn.Location = new System.Drawing.Point(558, 9);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(134, 58);
            this.btn.TabIndex = 2;
            this.btn.Text = "Query";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(283, 28);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(200, 28);
            this.dtpEnd.TabIndex = 1;
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(32, 28);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(200, 28);
            this.dtpStart.TabIndex = 1;
            // 
            // lbEnd
            // 
            this.lbEnd.AutoSize = true;
            this.lbEnd.Location = new System.Drawing.Point(344, 7);
            this.lbEnd.Name = "lbEnd";
            this.lbEnd.Size = new System.Drawing.Size(71, 18);
            this.lbEnd.TabIndex = 0;
            this.lbEnd.Text = "EndTime";
            // 
            // lbStart
            // 
            this.lbStart.AutoSize = true;
            this.lbStart.Location = new System.Drawing.Point(78, 7);
            this.lbStart.Name = "lbStart";
            this.lbStart.Size = new System.Drawing.Size(89, 18);
            this.lbStart.TabIndex = 0;
            this.lbStart.Text = "StartTime";
            // 
            // dgvMo
            // 
            this.dgvMo.AllowUserToAddRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgvMo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvMo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMo.Location = new System.Drawing.Point(3, 86);
            this.dgvMo.Name = "dgvMo";
            this.dgvMo.RowHeadersVisible = false;
            this.dgvMo.RowHeadersWidth = 62;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            this.dgvMo.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMo.RowTemplate.Height = 30;
            this.dgvMo.Size = new System.Drawing.Size(1233, 203);
            this.dgvMo.TabIndex = 1;
            this.dgvMo.SelectionChanged += new System.EventHandler(this.dgvMo_SelectionChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1245, 424);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Sn Info";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dgvSN, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.35407F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.64594F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1239, 418);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnQueryAll);
            this.panel2.Controls.Add(this.btnSNExl);
            this.panel2.Controls.Add(this.lbQty);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.tbScan);
            this.panel2.Controls.Add(this.lbScan);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1233, 54);
            this.panel2.TabIndex = 0;
            // 
            // btnQueryAll
            // 
            this.btnQueryAll.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnQueryAll.Location = new System.Drawing.Point(876, 6);
            this.btnQueryAll.Name = "btnQueryAll";
            this.btnQueryAll.Size = new System.Drawing.Size(203, 42);
            this.btnQueryAll.TabIndex = 3;
            this.btnQueryAll.Text = "Query All Stock Out";
            this.btnQueryAll.UseVisualStyleBackColor = true;
            this.btnQueryAll.Click += new System.EventHandler(this.btnQueryAll_Click);
            // 
            // btnSNExl
            // 
            this.btnSNExl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSNExl.Location = new System.Drawing.Point(1110, 6);
            this.btnSNExl.Name = "btnSNExl";
            this.btnSNExl.Size = new System.Drawing.Size(85, 42);
            this.btnSNExl.TabIndex = 3;
            this.btnSNExl.Text = "Excel";
            this.btnSNExl.UseVisualStyleBackColor = true;
            this.btnSNExl.Click += new System.EventHandler(this.btnSNExl_Click);
            // 
            // lbQty
            // 
            this.lbQty.AutoSize = true;
            this.lbQty.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbQty.Location = new System.Drawing.Point(831, 13);
            this.lbQty.Name = "lbQty";
            this.lbQty.Size = new System.Drawing.Size(24, 28);
            this.lbQty.TabIndex = 2;
            this.lbQty.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(759, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "QTY:";
            // 
            // tbScan
            // 
            this.tbScan.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbScan.Location = new System.Drawing.Point(289, 10);
            this.tbScan.Name = "tbScan";
            this.tbScan.Size = new System.Drawing.Size(331, 35);
            this.tbScan.TabIndex = 1;
            this.tbScan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbScan_KeyDown);
            // 
            // lbScan
            // 
            this.lbScan.AutoSize = true;
            this.lbScan.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbScan.Location = new System.Drawing.Point(48, 13);
            this.lbScan.Name = "lbScan";
            this.lbScan.Size = new System.Drawing.Size(232, 28);
            this.lbScan.TabIndex = 0;
            this.lbScan.Text = "Mo/CARTON/PALLET:";
            // 
            // dgvSN
            // 
            this.dgvSN.AllowUserToAddRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgvSN.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSN.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvSN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSN.Location = new System.Drawing.Point(3, 63);
            this.dgvSN.Name = "dgvSN";
            this.dgvSN.RowHeadersVisible = false;
            this.dgvSN.RowHeadersWidth = 62;
            this.dgvSN.RowTemplate.Height = 30;
            this.dgvSN.Size = new System.Drawing.Size(1233, 352);
            this.dgvSN.TabIndex = 1;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 634);
            this.Name = "fMain";
            this.Text = "Wip";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.prgBanner.ResumeLayout(false);
            this.prgMain.ResumeLayout(false);
            this.prgFooter.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBom)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMo)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSN)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvBom;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lbEnd;
        private System.Windows.Forms.Label lbStart;
        private System.Windows.Forms.DataGridView dgvMo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tbScan;
        private System.Windows.Forms.Label lbScan;
        private System.Windows.Forms.DataGridView dgvSN;
        private System.Windows.Forms.Label lbQty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSNExl;
        private System.Windows.Forms.Button btnQueryAll;
    }
}

