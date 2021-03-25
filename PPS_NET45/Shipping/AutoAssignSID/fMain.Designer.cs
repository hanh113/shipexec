namespace AutoAssignSID
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
            this.TextMsg = new System.Windows.Forms.Label();
            this.labShow = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvAssign = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkShowPallet = new System.Windows.Forms.CheckBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNewSID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLine = new System.Windows.Forms.TextBox();
            this.btnEXCEL3 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSID = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnResult = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvLine = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnEXCEL2 = new System.Windows.Forms.Button();
            this.btnSearchLine = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvSID = new System.Windows.Forms.DataGridView();
            this.SHIPMENT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FD_DS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRIORITY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REGION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CARTON_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PACK_CARTON = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnEXCEL1 = new System.Windows.Forms.Button();
            this.btnSearchSID = new System.Windows.Forms.Button();
            this.dtSID = new System.Windows.Forms.DateTimePicker();
            this.lblStart = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssign)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLine)).BeginInit();
            this.panel5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSID)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextMsg
            // 
            this.TextMsg.AutoEllipsis = true;
            this.TextMsg.BackColor = System.Drawing.Color.Blue;
            this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 667);
            this.TextMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1324, 62);
            this.TextMsg.TabIndex = 73;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.SystemColors.Info;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labShow.Name = "labShow";
            this.labShow.ReadOnly = true;
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(1324, 57);
            this.labShow.TabIndex = 72;
            this.labShow.Text = "AutoAssignSID";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 57);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1324, 610);
            this.panel1.TabIndex = 75;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvAssign);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 255);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(1324, 355);
            this.groupBox1.TabIndex = 112;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "分单";
            // 
            // dgvAssign
            // 
            this.dgvAssign.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAssign.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvAssign.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAssign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAssign.Location = new System.Drawing.Point(4, 124);
            this.dgvAssign.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvAssign.Name = "dgvAssign";
            this.dgvAssign.RowHeadersWidth = 62;
            this.dgvAssign.RowTemplate.Height = 23;
            this.dgvAssign.Size = new System.Drawing.Size(1316, 227);
            this.dgvAssign.TabIndex = 112;
            this.dgvAssign.SelectionChanged += new System.EventHandler(this.dgvAssign_SelectionChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkShowPallet);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.btnResult);
            this.panel3.Controls.Add(this.btnStart);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(4, 25);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1316, 99);
            this.panel3.TabIndex = 87;
            // 
            // chkShowPallet
            // 
            this.chkShowPallet.AutoSize = true;
            this.chkShowPallet.Location = new System.Drawing.Point(240, 60);
            this.chkShowPallet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkShowPallet.Name = "chkShowPallet";
            this.chkShowPallet.Size = new System.Drawing.Size(106, 22);
            this.chkShowPallet.TabIndex = 111;
            this.chkShowPallet.Text = "栈板信息";
            this.chkShowPallet.UseVisualStyleBackColor = true;
            this.chkShowPallet.CheckedChanged += new System.EventHandler(this.chkShowPallet_CheckedChanged);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.txtNewSID);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.txtLine);
            this.panel6.Controls.Add(this.btnEXCEL3);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.txtSID);
            this.panel6.Controls.Add(this.btnUpdate);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(419, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(897, 99);
            this.panel6.TabIndex = 110;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(405, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 21);
            this.label2.TabIndex = 140;
            this.label2.Text = "指定集货单号:";
            // 
            // txtNewSID
            // 
            this.txtNewSID.BackColor = System.Drawing.SystemColors.Control;
            this.txtNewSID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNewSID.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNewSID.ForeColor = System.Drawing.Color.Green;
            this.txtNewSID.Location = new System.Drawing.Point(562, 10);
            this.txtNewSID.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtNewSID.Name = "txtNewSID";
            this.txtNewSID.Size = new System.Drawing.Size(310, 35);
            this.txtNewSID.TabIndex = 139;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(58, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 21);
            this.label1.TabIndex = 137;
            this.label1.Text = "Line:";
            // 
            // txtLine
            // 
            this.txtLine.BackColor = System.Drawing.SystemColors.Control;
            this.txtLine.Enabled = false;
            this.txtLine.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLine.ForeColor = System.Drawing.Color.Green;
            this.txtLine.Location = new System.Drawing.Point(163, 10);
            this.txtLine.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtLine.Name = "txtLine";
            this.txtLine.Size = new System.Drawing.Size(222, 35);
            this.txtLine.TabIndex = 138;
            // 
            // btnEXCEL3
            // 
            this.btnEXCEL3.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnEXCEL3.Location = new System.Drawing.Point(820, 64);
            this.btnEXCEL3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEXCEL3.Name = "btnEXCEL3";
            this.btnEXCEL3.Size = new System.Drawing.Size(72, 28);
            this.btnEXCEL3.TabIndex = 136;
            this.btnEXCEL3.Text = "EXCEL";
            this.btnEXCEL3.UseVisualStyleBackColor = true;
            this.btnEXCEL3.Click += new System.EventHandler(this.btnEXCEL3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(16, 63);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 21);
            this.label4.TabIndex = 107;
            this.label4.Text = "集货单号:";
            // 
            // txtSID
            // 
            this.txtSID.BackColor = System.Drawing.SystemColors.Control;
            this.txtSID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSID.Enabled = false;
            this.txtSID.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSID.ForeColor = System.Drawing.Color.Green;
            this.txtSID.Location = new System.Drawing.Point(163, 54);
            this.txtSID.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtSID.Name = "txtSID";
            this.txtSID.Size = new System.Drawing.Size(222, 35);
            this.txtSID.TabIndex = 108;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(562, 56);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(112, 38);
            this.btnUpdate.TabIndex = 106;
            this.btnUpdate.Text = "执行更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnResult
            // 
            this.btnResult.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnResult.Location = new System.Drawing.Point(44, 10);
            this.btnResult.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnResult.Name = "btnResult";
            this.btnResult.Size = new System.Drawing.Size(112, 38);
            this.btnResult.TabIndex = 109;
            this.btnResult.Text = "查询";
            this.btnResult.UseVisualStyleBackColor = true;
            this.btnResult.Click += new System.EventHandler(this.btnResult_Click);
            // 
            // btnStart
            // 
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnStart.Location = new System.Drawing.Point(44, 52);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(112, 38);
            this.btnStart.TabIndex = 105;
            this.btnStart.Text = "执行分单";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.groupBox3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1324, 255);
            this.panel4.TabIndex = 111;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvLine);
            this.groupBox2.Controls.Add(this.panel5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(860, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(464, 255);
            this.groupBox2.TabIndex = 110;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Line信息:";
            // 
            // dgvLine
            // 
            this.dgvLine.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvLine.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvLine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLine.Location = new System.Drawing.Point(4, 71);
            this.dgvLine.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvLine.Name = "dgvLine";
            this.dgvLine.RowHeadersWidth = 62;
            this.dgvLine.RowTemplate.Height = 23;
            this.dgvLine.Size = new System.Drawing.Size(456, 180);
            this.dgvLine.TabIndex = 112;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnEXCEL2);
            this.panel5.Controls.Add(this.btnSearchLine);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(4, 25);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(456, 46);
            this.panel5.TabIndex = 111;
            // 
            // btnEXCEL2
            // 
            this.btnEXCEL2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEXCEL2.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnEXCEL2.Location = new System.Drawing.Point(380, 16);
            this.btnEXCEL2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEXCEL2.Name = "btnEXCEL2";
            this.btnEXCEL2.Size = new System.Drawing.Size(72, 28);
            this.btnEXCEL2.TabIndex = 135;
            this.btnEXCEL2.Text = "EXCEL";
            this.btnEXCEL2.UseVisualStyleBackColor = true;
            this.btnEXCEL2.Click += new System.EventHandler(this.btnEXCEL2_Click);
            // 
            // btnSearchLine
            // 
            this.btnSearchLine.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSearchLine.Location = new System.Drawing.Point(58, 4);
            this.btnSearchLine.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearchLine.Name = "btnSearchLine";
            this.btnSearchLine.Size = new System.Drawing.Size(112, 38);
            this.btnSearchLine.TabIndex = 107;
            this.btnSearchLine.Text = "查询";
            this.btnSearchLine.UseVisualStyleBackColor = true;
            this.btnSearchLine.Click += new System.EventHandler(this.btnSearchLine_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvSID);
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(860, 255);
            this.groupBox3.TabIndex = 109;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "集货单信息";
            // 
            // dgvSID
            // 
            this.dgvSID.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvSID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSID.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SHIPMENT_ID,
            this.FD_DS,
            this.TYPE,
            this.PRIORITY,
            this.REGION,
            this.CNAME,
            this.CARTON_QTY,
            this.PACK_CARTON});
            this.dgvSID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSID.Location = new System.Drawing.Point(4, 71);
            this.dgvSID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvSID.Name = "dgvSID";
            this.dgvSID.RowHeadersWidth = 62;
            this.dgvSID.RowTemplate.Height = 23;
            this.dgvSID.Size = new System.Drawing.Size(852, 180);
            this.dgvSID.TabIndex = 111;
            // 
            // SHIPMENT_ID
            // 
            this.SHIPMENT_ID.HeaderText = "SHIPMENT_ID";
            this.SHIPMENT_ID.MinimumWidth = 8;
            this.SHIPMENT_ID.Name = "SHIPMENT_ID";
            this.SHIPMENT_ID.Width = 150;
            // 
            // FD_DS
            // 
            this.FD_DS.HeaderText = "FD_DS";
            this.FD_DS.MinimumWidth = 8;
            this.FD_DS.Name = "FD_DS";
            this.FD_DS.Width = 45;
            // 
            // TYPE
            // 
            this.TYPE.HeaderText = "TYPE";
            this.TYPE.MinimumWidth = 8;
            this.TYPE.Name = "TYPE";
            this.TYPE.Width = 60;
            // 
            // PRIORITY
            // 
            this.PRIORITY.HeaderText = "PRIORITY";
            this.PRIORITY.MinimumWidth = 8;
            this.PRIORITY.Name = "PRIORITY";
            this.PRIORITY.Width = 50;
            // 
            // REGION
            // 
            this.REGION.HeaderText = "REGION";
            this.REGION.MinimumWidth = 8;
            this.REGION.Name = "REGION";
            this.REGION.Width = 60;
            // 
            // CNAME
            // 
            this.CNAME.HeaderText = "CNAME";
            this.CNAME.MinimumWidth = 8;
            this.CNAME.Name = "CNAME";
            this.CNAME.Width = 80;
            // 
            // CARTON_QTY
            // 
            this.CARTON_QTY.HeaderText = "CARTON_QTY";
            this.CARTON_QTY.MinimumWidth = 8;
            this.CARTON_QTY.Name = "CARTON_QTY";
            this.CARTON_QTY.Width = 70;
            // 
            // PACK_CARTON
            // 
            this.PACK_CARTON.HeaderText = "PACK_CARTON";
            this.PACK_CARTON.MinimumWidth = 8;
            this.PACK_CARTON.Name = "PACK_CARTON";
            this.PACK_CARTON.Width = 75;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnEXCEL1);
            this.panel2.Controls.Add(this.btnSearchSID);
            this.panel2.Controls.Add(this.dtSID);
            this.panel2.Controls.Add(this.lblStart);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(4, 25);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 46);
            this.panel2.TabIndex = 110;
            // 
            // btnEXCEL1
            // 
            this.btnEXCEL1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEXCEL1.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnEXCEL1.Location = new System.Drawing.Point(776, 16);
            this.btnEXCEL1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEXCEL1.Name = "btnEXCEL1";
            this.btnEXCEL1.Size = new System.Drawing.Size(72, 28);
            this.btnEXCEL1.TabIndex = 134;
            this.btnEXCEL1.Text = "EXCEL";
            this.btnEXCEL1.UseVisualStyleBackColor = true;
            this.btnEXCEL1.Click += new System.EventHandler(this.btnEXCEL1_Click);
            // 
            // btnSearchSID
            // 
            this.btnSearchSID.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSearchSID.Location = new System.Drawing.Point(346, 4);
            this.btnSearchSID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearchSID.Name = "btnSearchSID";
            this.btnSearchSID.Size = new System.Drawing.Size(112, 38);
            this.btnSearchSID.TabIndex = 133;
            this.btnSearchSID.Text = "查询";
            this.btnSearchSID.UseVisualStyleBackColor = true;
            this.btnSearchSID.Click += new System.EventHandler(this.btnSearchSID_Click);
            // 
            // dtSID
            // 
            this.dtSID.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dtSID.CustomFormat = "yyyy-MM-dd";
            this.dtSID.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtSID.Location = new System.Drawing.Point(165, 8);
            this.dtSID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtSID.Name = "dtSID";
            this.dtSID.Size = new System.Drawing.Size(150, 28);
            this.dtSID.TabIndex = 132;
            this.dtSID.Value = new System.DateTime(2019, 7, 12, 0, 0, 0, 0);
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(14, 14);
            this.lblStart.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(143, 18);
            this.lblStart.TabIndex = 129;
            this.lblStart.Text = "集货单出货日期:";
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1324, 729);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TextMsg);
            this.Controls.Add(this.labShow);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "fMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.fMain_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssign)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLine)).EndInit();
            this.panel5.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSID)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvAssign;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLine;
        private System.Windows.Forms.Button btnEXCEL3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSID;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnResult;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvLine;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnEXCEL2;
        private System.Windows.Forms.Button btnSearchLine;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvSID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SHIPMENT_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FD_DS;
        private System.Windows.Forms.DataGridViewTextBoxColumn TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRIORITY;
        private System.Windows.Forms.DataGridViewTextBoxColumn REGION;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn CARTON_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn PACK_CARTON;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnEXCEL1;
        private System.Windows.Forms.Button btnSearchSID;
        private System.Windows.Forms.DateTimePicker dtSID;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNewSID;
        private System.Windows.Forms.CheckBox chkShowPallet;
    }
}

