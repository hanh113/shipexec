namespace NPIPickListAC
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvPickNO = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.txtPalletNO = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCarton = new System.Windows.Forms.TextBox();
            this.dgvStock = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvNo = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.labqty = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dt_end = new System.Windows.Forms.DateTimePicker();
            this.btnClsFace = new System.Windows.Forms.Button();
            this.cmbSID = new System.Windows.Forms.ComboBox();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dt_start = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnTEST = new System.Windows.Forms.Button();
            this.cmbSTATUS = new System.Windows.Forms.ComboBox();
            this.lblStart = new System.Windows.Forms.Label();
            this.labShipmentID = new System.Windows.Forms.Label();
            this.labStatus = new System.Windows.Forms.Label();
            this.labShow = new System.Windows.Forms.TextBox();
            this.TextMsg = new System.Windows.Forms.Label();
            this.shipment_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delivery_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.line_item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mpn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ictpn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.carton_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pack_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pack_carton_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnReprint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPickNO)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNo)).BeginInit();
            this.panel5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPickNO
            // 
            this.dgvPickNO.AllowUserToAddRows = false;
            this.dgvPickNO.AllowUserToDeleteRows = false;
            this.dgvPickNO.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPickNO.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvPickNO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPickNO.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.shipment_id,
            this.delivery_no,
            this.line_item,
            this.mpn,
            this.ictpn,
            this.status,
            this.qty,
            this.carton_qty,
            this.pack_qty,
            this.pack_carton_qty});
            this.dgvPickNO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPickNO.GridColor = System.Drawing.SystemColors.ControlText;
            this.dgvPickNO.Location = new System.Drawing.Point(0, 0);
            this.dgvPickNO.Margin = new System.Windows.Forms.Padding(5);
            this.dgvPickNO.MultiSelect = false;
            this.dgvPickNO.Name = "dgvPickNO";
            this.dgvPickNO.ReadOnly = true;
            this.dgvPickNO.RowHeadersWidth = 30;
            this.dgvPickNO.RowTemplate.Height = 23;
            this.dgvPickNO.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvPickNO.Size = new System.Drawing.Size(1036, 281);
            this.dgvPickNO.TabIndex = 125;
            this.dgvPickNO.SelectionChanged += new System.EventHandler(this.dgvPickNO_SelectionChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvPickNO);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 113);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1036, 281);
            this.panel1.TabIndex = 118;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 369);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1036, 394);
            this.panel3.TabIndex = 133;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel7.Controls.Add(this.btnExport);
            this.panel7.Controls.Add(this.btnEnd);
            this.panel7.Controls.Add(this.btnStart);
            this.panel7.Controls.Add(this.label16);
            this.panel7.Controls.Add(this.txtPalletNO);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.txtSID);
            this.panel7.Controls.Add(this.label3);
            this.panel7.Controls.Add(this.txtCarton);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(4);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1036, 113);
            this.panel7.TabIndex = 127;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnExport.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExport.Location = new System.Drawing.Point(929, 4);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(99, 32);
            this.btnExport.TabIndex = 105;
            this.btnExport.Text = "导出excel";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // btnEnd
            // 
            this.btnEnd.Enabled = false;
            this.btnEnd.Location = new System.Drawing.Point(650, 57);
            this.btnEnd.Margin = new System.Windows.Forms.Padding(4);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(100, 35);
            this.btnEnd.TabIndex = 105;
            this.btnEnd.Text = "结束作业";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnStart
            // 
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnStart.Location = new System.Drawing.Point(509, 58);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 34);
            this.btnStart.TabIndex = 104;
            this.btnStart.Text = "开始作业";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(451, 22);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(71, 18);
            this.label16.TabIndex = 102;
            this.label16.Text = "栈板号:";
            // 
            // txtPalletNO
            // 
            this.txtPalletNO.Enabled = false;
            this.txtPalletNO.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPalletNO.ForeColor = System.Drawing.Color.Blue;
            this.txtPalletNO.Location = new System.Drawing.Point(531, 11);
            this.txtPalletNO.Margin = new System.Windows.Forms.Padding(5);
            this.txtPalletNO.Name = "txtPalletNO";
            this.txtPalletNO.ReadOnly = true;
            this.txtPalletNO.Size = new System.Drawing.Size(293, 38);
            this.txtPalletNO.TabIndex = 103;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(59, 23);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 18);
            this.label4.TabIndex = 72;
            this.label4.Text = "NPI出货单号:";
            // 
            // txtSID
            // 
            this.txtSID.BackColor = System.Drawing.SystemColors.Control;
            this.txtSID.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSID.ForeColor = System.Drawing.Color.Green;
            this.txtSID.Location = new System.Drawing.Point(181, 18);
            this.txtSID.Margin = new System.Windows.Forms.Padding(5);
            this.txtSID.Name = "txtSID";
            this.txtSID.Size = new System.Drawing.Size(224, 30);
            this.txtSID.TabIndex = 73;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(77, 70);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 18);
            this.label3.TabIndex = 77;
            this.label3.Text = "序号/箱号:";
            // 
            // txtCarton
            // 
            this.txtCarton.BackColor = System.Drawing.Color.White;
            this.txtCarton.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCarton.Enabled = false;
            this.txtCarton.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCarton.ForeColor = System.Drawing.Color.Blue;
            this.txtCarton.Location = new System.Drawing.Point(181, 58);
            this.txtCarton.Margin = new System.Windows.Forms.Padding(5);
            this.txtCarton.Name = "txtCarton";
            this.txtCarton.Size = new System.Drawing.Size(280, 38);
            this.txtCarton.TabIndex = 78;
            this.txtCarton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCarton_KeyDown);
            // 
            // dgvStock
            // 
            this.dgvStock.AllowUserToAddRows = false;
            this.dgvStock.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvStock.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvStock.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvStock.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvStock.ColumnHeadersHeight = 29;
            this.dgvStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStock.Location = new System.Drawing.Point(4, 26);
            this.dgvStock.Margin = new System.Windows.Forms.Padding(1);
            this.dgvStock.MultiSelect = false;
            this.dgvStock.Name = "dgvStock";
            this.dgvStock.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("新宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStock.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvStock.RowHeadersWidth = 30;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            this.dgvStock.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvStock.RowTemplate.Height = 27;
            this.dgvStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvStock.Size = new System.Drawing.Size(591, 364);
            this.dgvStock.TabIndex = 98;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox2.Controls.Add(this.dgvStock);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Font = new System.Drawing.Font("新宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(1036, 369);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(599, 394);
            this.groupBox2.TabIndex = 132;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "库位信息";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 158);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1635, 211);
            this.panel2.TabIndex = 131;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox5.Controls.Add(this.dgvNo);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Font = new System.Drawing.Font("新宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Size = new System.Drawing.Size(1635, 211);
            this.groupBox5.TabIndex = 114;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "NPI出货单号列表";
            // 
            // dgvNo
            // 
            this.dgvNo.AllowUserToAddRows = false;
            this.dgvNo.AllowUserToDeleteRows = false;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9F);
            this.dgvNo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvNo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNo.ColumnHeadersHeight = 29;
            this.dgvNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNo.Location = new System.Drawing.Point(3, 24);
            this.dgvNo.Margin = new System.Windows.Forms.Padding(1);
            this.dgvNo.MultiSelect = false;
            this.dgvNo.Name = "dgvNo";
            this.dgvNo.ReadOnly = true;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("新宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNo.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvNo.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            this.dgvNo.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvNo.RowTemplate.Height = 27;
            this.dgvNo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNo.Size = new System.Drawing.Size(1629, 185);
            this.dgvNo.TabIndex = 97;
            this.dgvNo.SelectionChanged += new System.EventHandler(this.dgvNo_SelectionChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.labqty);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(1237, 20);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(395, 87);
            this.panel5.TabIndex = 118;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 16F);
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(85, 6);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(242, 27);
            this.label12.TabIndex = 89;
            this.label12.Text = "已刷箱数/需要箱数";
            // 
            // labqty
            // 
            this.labqty.Font = new System.Drawing.Font("宋体", 32F, System.Drawing.FontStyle.Bold);
            this.labqty.ForeColor = System.Drawing.Color.Blue;
            this.labqty.Location = new System.Drawing.Point(69, 33);
            this.labqty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labqty.Name = "labqty";
            this.labqty.Size = new System.Drawing.Size(268, 54);
            this.labqty.TabIndex = 90;
            this.labqty.Text = "00/00";
            this.labqty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(749, 20);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(125, 34);
            this.btnSearch.TabIndex = 82;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dt_end
            // 
            this.dt_end.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_end.CustomFormat = "yyyy-MM-dd";
            this.dt_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_end.Location = new System.Drawing.Point(319, 51);
            this.dt_end.Margin = new System.Windows.Forms.Padding(4);
            this.dt_end.Name = "dt_end";
            this.dt_end.Size = new System.Drawing.Size(126, 25);
            this.dt_end.TabIndex = 10;
            // 
            // btnClsFace
            // 
            this.btnClsFace.Location = new System.Drawing.Point(749, 59);
            this.btnClsFace.Margin = new System.Windows.Forms.Padding(4);
            this.btnClsFace.Name = "btnClsFace";
            this.btnClsFace.Size = new System.Drawing.Size(125, 34);
            this.btnClsFace.TabIndex = 85;
            this.btnClsFace.Text = "重置";
            this.btnClsFace.UseVisualStyleBackColor = true;
            this.btnClsFace.Click += new System.EventHandler(this.btnClsFace_Click);
            // 
            // cmbSID
            // 
            this.cmbSID.FormattingEnabled = true;
            this.cmbSID.Location = new System.Drawing.Point(120, 16);
            this.cmbSID.Margin = new System.Windows.Forms.Padding(1);
            this.cmbSID.Name = "cmbSID";
            this.cmbSID.Size = new System.Drawing.Size(257, 23);
            this.cmbSID.TabIndex = 104;
            this.cmbSID.Text = "ALL";
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(274, 58);
            this.lblEnd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(30, 15);
            this.lblEnd.TabIndex = 92;
            this.lblEnd.Text = "至:";
            // 
            // dt_start
            // 
            this.dt_start.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_start.CustomFormat = "yyyy-MM-dd";
            this.dt_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_start.Location = new System.Drawing.Point(120, 51);
            this.dt_start.Margin = new System.Windows.Forms.Padding(4);
            this.dt_start.Name = "dt_start";
            this.dt_start.Size = new System.Drawing.Size(135, 25);
            this.dt_start.TabIndex = 93;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox3.Controls.Add(this.btnReprint);
            this.groupBox3.Controls.Add(this.btnTEST);
            this.groupBox3.Controls.Add(this.panel5);
            this.groupBox3.Controls.Add(this.btnSearch);
            this.groupBox3.Controls.Add(this.dt_end);
            this.groupBox3.Controls.Add(this.btnClsFace);
            this.groupBox3.Controls.Add(this.cmbSID);
            this.groupBox3.Controls.Add(this.lblEnd);
            this.groupBox3.Controls.Add(this.dt_start);
            this.groupBox3.Controls.Add(this.cmbSTATUS);
            this.groupBox3.Controls.Add(this.lblStart);
            this.groupBox3.Controls.Add(this.labShipmentID);
            this.groupBox3.Controls.Add(this.labStatus);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 49);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(1635, 109);
            this.groupBox3.TabIndex = 129;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "筛选";
            // 
            // btnTEST
            // 
            this.btnTEST.Location = new System.Drawing.Point(911, 59);
            this.btnTEST.Name = "btnTEST";
            this.btnTEST.Size = new System.Drawing.Size(125, 34);
            this.btnTEST.TabIndex = 122;
            this.btnTEST.Text = "TEST";
            this.btnTEST.UseVisualStyleBackColor = true;
            this.btnTEST.Click += new System.EventHandler(this.btnTEST_Click);
            // 
            // cmbSTATUS
            // 
            this.cmbSTATUS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSTATUS.FormattingEnabled = true;
            this.cmbSTATUS.Items.AddRange(new object[] {
            "ALL",
            "WP-未开始",
            "IP-作业中",
            "FP-已完成",
            "CP-CANCEL",
            "HO-HOLD"});
            this.cmbSTATUS.Location = new System.Drawing.Point(502, 16);
            this.cmbSTATUS.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSTATUS.Name = "cmbSTATUS";
            this.cmbSTATUS.Size = new System.Drawing.Size(115, 23);
            this.cmbSTATUS.TabIndex = 101;
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(40, 56);
            this.lblStart.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(75, 15);
            this.lblStart.TabIndex = 9;
            this.lblStart.Text = "开始日期:";
            // 
            // labShipmentID
            // 
            this.labShipmentID.AutoSize = true;
            this.labShipmentID.Location = new System.Drawing.Point(16, 19);
            this.labShipmentID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labShipmentID.Name = "labShipmentID";
            this.labShipmentID.Size = new System.Drawing.Size(99, 15);
            this.labShipmentID.TabIndex = 13;
            this.labShipmentID.Text = "NPI出货单号:";
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.Location = new System.Drawing.Point(419, 20);
            this.labStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(75, 15);
            this.labStatus.TabIndex = 11;
            this.labStatus.Text = "单号状态:";
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Enabled = false;
            this.labShow.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.SystemColors.Info;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Margin = new System.Windows.Forms.Padding(4);
            this.labShow.Name = "labShow";
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(1635, 49);
            this.labShow.TabIndex = 128;
            this.labShow.Text = "NPI出货";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TextMsg
            // 
            this.TextMsg.AutoEllipsis = true;
            this.TextMsg.BackColor = System.Drawing.Color.Blue;
            this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 763);
            this.TextMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1635, 55);
            this.TextMsg.TabIndex = 130;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // shipment_id
            // 
            this.shipment_id.HeaderText = "shipment_id";
            this.shipment_id.MinimumWidth = 6;
            this.shipment_id.Name = "shipment_id";
            this.shipment_id.ReadOnly = true;
            this.shipment_id.Width = 124;
            // 
            // delivery_no
            // 
            this.delivery_no.HeaderText = "delivery_no";
            this.delivery_no.MinimumWidth = 6;
            this.delivery_no.Name = "delivery_no";
            this.delivery_no.ReadOnly = true;
            this.delivery_no.Width = 124;
            // 
            // line_item
            // 
            this.line_item.HeaderText = "line_item";
            this.line_item.MinimumWidth = 6;
            this.line_item.Name = "line_item";
            this.line_item.ReadOnly = true;
            this.line_item.Width = 108;
            // 
            // mpn
            // 
            this.mpn.HeaderText = "mpn";
            this.mpn.MinimumWidth = 6;
            this.mpn.Name = "mpn";
            this.mpn.ReadOnly = true;
            this.mpn.Width = 60;
            // 
            // ictpn
            // 
            this.ictpn.HeaderText = "ictpn";
            this.ictpn.MinimumWidth = 6;
            this.ictpn.Name = "ictpn";
            this.ictpn.ReadOnly = true;
            this.ictpn.Width = 76;
            // 
            // status
            // 
            this.status.HeaderText = "status";
            this.status.MinimumWidth = 6;
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 84;
            // 
            // qty
            // 
            this.qty.HeaderText = "qty";
            this.qty.MinimumWidth = 6;
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            this.qty.Width = 60;
            // 
            // carton_qty
            // 
            this.carton_qty.HeaderText = "carton_qty";
            this.carton_qty.MinimumWidth = 6;
            this.carton_qty.Name = "carton_qty";
            this.carton_qty.ReadOnly = true;
            this.carton_qty.Width = 116;
            // 
            // pack_qty
            // 
            this.pack_qty.HeaderText = "pack_qty";
            this.pack_qty.MinimumWidth = 6;
            this.pack_qty.Name = "pack_qty";
            this.pack_qty.ReadOnly = true;
            // 
            // pack_carton_qty
            // 
            this.pack_carton_qty.HeaderText = "pack_carton_qty";
            this.pack_carton_qty.MinimumWidth = 6;
            this.pack_carton_qty.Name = "pack_carton_qty";
            this.pack_carton_qty.ReadOnly = true;
            this.pack_carton_qty.Width = 156;
            // 
            // btnReprint
            // 
            this.btnReprint.Location = new System.Drawing.Point(911, 19);
            this.btnReprint.Name = "btnReprint";
            this.btnReprint.Size = new System.Drawing.Size(125, 34);
            this.btnReprint.TabIndex = 123;
            this.btnReprint.Text = "补列印";
            this.btnReprint.UseVisualStyleBackColor = true;
            this.btnReprint.Click += new System.EventHandler(this.btnReprint_Click);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1635, 818);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.labShow);
            this.Controls.Add(this.TextMsg);
            this.Name = "fMain";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPickNO)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNo)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvPickNO;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtPalletNO;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCarton;
        private System.Windows.Forms.DataGridView dgvStock;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgvNo;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labqty;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dt_end;
        private System.Windows.Forms.Button btnClsFace;
        private System.Windows.Forms.ComboBox cmbSID;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dt_start;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbSTATUS;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label labShipmentID;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.Button btnTEST;
        private System.Windows.Forms.DataGridViewTextBoxColumn shipment_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn delivery_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn line_item;
        private System.Windows.Forms.DataGridViewTextBoxColumn mpn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ictpn;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn carton_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn pack_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn pack_carton_qty;
        private System.Windows.Forms.Button btnReprint;
    }
}

