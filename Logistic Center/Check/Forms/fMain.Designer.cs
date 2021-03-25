using System.IO.Ports;

namespace Check
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
            this.components = new System.ComponentModel.Container();
            this.ComPort = new System.IO.Ports.SerialPort(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvSnInfo = new System.Windows.Forms.DataGridView();
            this.mo_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_sn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.part_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.station_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.carton_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pallet_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbSTDWeight = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbWeight = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbWeight = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbManual = new System.Windows.Forms.CheckBox();
            this.lbPart = new System.Windows.Forms.Label();
            this.lbkp = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tbCarton = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSN = new System.Windows.Forms.TextBox();
            this.tbMo = new System.Windows.Forms.TextBox();
            this.lbCarton = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvSnInfo)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // prgBanner
            // 
            this.prgBanner.Controls.Add(this.menuStrip1);
            this.prgBanner.Size = new System.Drawing.Size(1241, 79);
            this.prgBanner.Controls.SetChildIndex(this.prgTitle, 0);
            this.prgBanner.Controls.SetChildIndex(this.menuStrip1, 0);
            // 
            // prgMain
            // 
            this.prgMain.Controls.Add(this.tableLayoutPanel1);
            this.prgMain.Size = new System.Drawing.Size(1241, 432);
            // 
            // prgTitle
            // 
            this.prgTitle.Size = new System.Drawing.Size(1241, 79);
            // 
            // prgFooter
            // 
            this.prgFooter.Location = new System.Drawing.Point(0, 511);
            this.prgFooter.Size = new System.Drawing.Size(1241, 72);
            // 
            // prgMSG
            // 
            this.prgMSG.Size = new System.Drawing.Size(1241, 72);
            // 
            // ComPort
            // 
            this.ComPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.ComPort_DataReceived);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setupToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(237, 9);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(84, 32);
            this.menuStrip1.TabIndex = 3;
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.82716F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.17284F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1235, 405);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 139);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.splitContainer1.Panel1.Controls.Add(this.dgvSnInfo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1229, 263);
            this.splitContainer1.SplitterDistance = 545;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvSnInfo
            // 
            this.dgvSnInfo.AllowUserToAddRows = false;
            this.dgvSnInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSnInfo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvSnInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSnInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mo_no,
            this.customer_sn,
            this.part_no,
            this.station_name,
            this.carton_no,
            this.pallet_no});
            this.dgvSnInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSnInfo.Location = new System.Drawing.Point(0, 0);
            this.dgvSnInfo.MultiSelect = false;
            this.dgvSnInfo.Name = "dgvSnInfo";
            this.dgvSnInfo.ReadOnly = true;
            this.dgvSnInfo.RowHeadersVisible = false;
            this.dgvSnInfo.RowHeadersWidth = 62;
            this.dgvSnInfo.RowTemplate.Height = 30;
            this.dgvSnInfo.Size = new System.Drawing.Size(545, 263);
            this.dgvSnInfo.TabIndex = 0;
            // 
            // mo_no
            // 
            this.mo_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.mo_no.DataPropertyName = "mo_no";
            this.mo_no.HeaderText = "工单";
            this.mo_no.MinimumWidth = 8;
            this.mo_no.Name = "mo_no";
            this.mo_no.ReadOnly = true;
            this.mo_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // customer_sn
            // 
            this.customer_sn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.customer_sn.DataPropertyName = "customer_sn";
            this.customer_sn.HeaderText = "客户序号";
            this.customer_sn.MinimumWidth = 8;
            this.customer_sn.Name = "customer_sn";
            this.customer_sn.ReadOnly = true;
            this.customer_sn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.customer_sn.Width = 86;
            // 
            // part_no
            // 
            this.part_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.part_no.DataPropertyName = "part_no";
            this.part_no.HeaderText = "料号";
            this.part_no.MinimumWidth = 8;
            this.part_no.Name = "part_no";
            this.part_no.ReadOnly = true;
            this.part_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.part_no.Width = 45;
            // 
            // station_name
            // 
            this.station_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.station_name.DataPropertyName = "station_name";
            this.station_name.HeaderText = "站别";
            this.station_name.MinimumWidth = 8;
            this.station_name.Name = "station_name";
            this.station_name.ReadOnly = true;
            this.station_name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // carton_no
            // 
            this.carton_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.carton_no.DataPropertyName = "carton_no";
            this.carton_no.HeaderText = "箱号";
            this.carton_no.MinimumWidth = 8;
            this.carton_no.Name = "carton_no";
            this.carton_no.ReadOnly = true;
            this.carton_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.carton_no.Width = 45;
            // 
            // pallet_no
            // 
            this.pallet_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.pallet_no.DataPropertyName = "pallet_no";
            this.pallet_no.HeaderText = "栈板号";
            this.pallet_no.MinimumWidth = 8;
            this.pallet_no.Name = "pallet_no";
            this.pallet_no.ReadOnly = true;
            this.pallet_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbSTDWeight);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lbWeight);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbWeight);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(680, 263);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lbSTDWeight
            // 
            this.lbSTDWeight.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbSTDWeight.AutoSize = true;
            this.lbSTDWeight.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSTDWeight.Location = new System.Drawing.Point(263, 24);
            this.lbSTDWeight.Name = "lbSTDWeight";
            this.lbSTDWeight.Size = new System.Drawing.Size(70, 36);
            this.lbSTDWeight.TabIndex = 5;
            this.lbSTDWeight.Text = "N/A";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(58, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 36);
            this.label3.TabIndex = 5;
            this.label3.Text = "STD Weight:";
            // 
            // lbWeight
            // 
            this.lbWeight.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbWeight.AutoSize = true;
            this.lbWeight.Font = new System.Drawing.Font("微软雅黑", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbWeight.Location = new System.Drawing.Point(244, 60);
            this.lbWeight.Name = "lbWeight";
            this.lbWeight.Size = new System.Drawing.Size(283, 188);
            this.lbWeight.TabIndex = 2;
            this.lbWeight.Text = "0.0";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(58, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 36);
            this.label1.TabIndex = 3;
            this.label1.Text = "Weight:";
            // 
            // tbWeight
            // 
            this.tbWeight.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbWeight.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbWeight.Location = new System.Drawing.Point(207, 94);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.Size = new System.Drawing.Size(242, 103);
            this.tbWeight.TabIndex = 7;
            this.tbWeight.Visible = false;
            this.tbWeight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbWeight_KeyDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.cbManual);
            this.panel1.Controls.Add(this.lbPart);
            this.panel1.Controls.Add(this.lbkp);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.tbCarton);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbSN);
            this.panel1.Controls.Add(this.tbMo);
            this.panel1.Controls.Add(this.lbCarton);
            this.panel1.Controls.Add(this.lbSN);
            this.panel1.Controls.Add(this.lbMO);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1229, 130);
            this.panel1.TabIndex = 1;
            // 
            // cbManual
            // 
            this.cbManual.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cbManual.AutoSize = true;
            this.cbManual.Location = new System.Drawing.Point(1045, 12);
            this.cbManual.Name = "cbManual";
            this.cbManual.Size = new System.Drawing.Size(151, 22);
            this.cbManual.TabIndex = 6;
            this.cbManual.Text = "Manual Weight";
            this.cbManual.UseVisualStyleBackColor = true;
            this.cbManual.CheckedChanged += new System.EventHandler(this.cbManual_CheckedChanged);
            // 
            // lbPart
            // 
            this.lbPart.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbPart.AutoSize = true;
            this.lbPart.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPart.Location = new System.Drawing.Point(123, 82);
            this.lbPart.Name = "lbPart";
            this.lbPart.Size = new System.Drawing.Size(70, 36);
            this.lbPart.TabIndex = 2;
            this.lbPart.Text = "N/A";
            // 
            // lbkp
            // 
            this.lbkp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbkp.AutoSize = true;
            this.lbkp.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbkp.Location = new System.Drawing.Point(39, 79);
            this.lbkp.Name = "lbkp";
            this.lbkp.Size = new System.Drawing.Size(75, 36);
            this.lbkp.TabIndex = 3;
            this.lbkp.Text = "Part:";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(1045, 79);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(151, 39);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.Visible = false;
            // 
            // tbCarton
            // 
            this.tbCarton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbCarton.Enabled = false;
            this.tbCarton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCarton.Location = new System.Drawing.Point(542, 21);
            this.tbCarton.Name = "tbCarton";
            this.tbCarton.Size = new System.Drawing.Size(411, 39);
            this.tbCarton.TabIndex = 1;
            this.tbCarton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCarton_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(1063, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 36);
            this.label2.TabIndex = 3;
            this.label2.Text = "COM:";
            this.label2.Visible = false;
            // 
            // tbSN
            // 
            this.tbSN.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbSN.Enabled = false;
            this.tbSN.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSN.Location = new System.Drawing.Point(542, 83);
            this.tbSN.Name = "tbSN";
            this.tbSN.Size = new System.Drawing.Size(411, 39);
            this.tbSN.TabIndex = 1;
            this.tbSN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSN_KeyDown);
            // 
            // tbMo
            // 
            this.tbMo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbMo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMo.Location = new System.Drawing.Point(126, 24);
            this.tbMo.Name = "tbMo";
            this.tbMo.Size = new System.Drawing.Size(269, 39);
            this.tbMo.TabIndex = 1;
            this.tbMo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbMo_KeyDown);
            // 
            // lbCarton
            // 
            this.lbCarton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbCarton.AutoSize = true;
            this.lbCarton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCarton.Location = new System.Drawing.Point(439, 27);
            this.lbCarton.Name = "lbCarton";
            this.lbCarton.Size = new System.Drawing.Size(97, 31);
            this.lbCarton.TabIndex = 0;
            this.lbCarton.Text = "Carton:";
            // 
            // lbSN
            // 
            this.lbSN.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbSN.AutoSize = true;
            this.lbSN.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSN.Location = new System.Drawing.Point(482, 87);
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
            this.lbMO.Location = new System.Drawing.Point(51, 24);
            this.lbMO.Name = "lbMO";
            this.lbMO.Size = new System.Drawing.Size(63, 31);
            this.lbMO.TabIndex = 0;
            this.lbMO.Text = "MO:";
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1241, 583);
            this.Name = "fMain";
            this.Text = "Check";
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvSnInfo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvSnInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbCarton;
        private System.Windows.Forms.TextBox tbSN;
        private System.Windows.Forms.TextBox tbMo;
        private System.Windows.Forms.Label lbCarton;
        private System.Windows.Forms.Label lbSN;
        private System.Windows.Forms.Label lbMO;
        private System.Windows.Forms.Label lbWeight;
        private System.Windows.Forms.Label lbPart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbkp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbSTDWeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn mo_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_sn;
        private System.Windows.Forms.DataGridViewTextBoxColumn part_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn station_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn carton_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn pallet_no;
        private System.IO.Ports.SerialPort ComPort;
        private System.Windows.Forms.CheckBox cbManual;
        private System.Windows.Forms.TextBox tbWeight;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
    }
}

