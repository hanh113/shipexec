namespace ShipmentAC
{
    partial class fMain
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dgvShipmentInfo = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.dgvShipmentInfoAlready = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtMessage = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Del_SelectRow_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Del_AllRow_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labShow = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.chkAllRecord = new System.Windows.Forms.CheckBox();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.lbldate = new System.Windows.Forms.Label();
            this.lblchepai = new System.Windows.Forms.Label();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.cboCarNo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRemainNum = new System.Windows.Forms.TextBox();
            this.txtAlreadyNum = new System.Windows.Forms.TextBox();
            this.txtShallNum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblqianbanhao = new System.Windows.Forms.Label();
            this.txtPalletNo = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.panelDNDetail = new System.Windows.Forms.Panel();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.labInfo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShipmentInfo)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShipmentInfoAlready)).BeginInit();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panelDNDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 291);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(998, 271);
            this.panel1.TabIndex = 65;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(505, 271);
            this.panel4.TabIndex = 5;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dgvShipmentInfo);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(0, 0);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2, 2, 5, 2);
            this.groupBox6.Size = new System.Drawing.Size(505, 271);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "未装车";
            // 
            // dgvShipmentInfo
            // 
            this.dgvShipmentInfo.AllowUserToAddRows = false;
            this.dgvShipmentInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvShipmentInfo.BackgroundColor = System.Drawing.Color.White;
            this.dgvShipmentInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvShipmentInfo.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvShipmentInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShipmentInfo.Location = new System.Drawing.Point(2, 16);
            this.dgvShipmentInfo.Name = "dgvShipmentInfo";
            this.dgvShipmentInfo.ReadOnly = true;
            this.dgvShipmentInfo.RowTemplate.Height = 30;
            this.dgvShipmentInfo.Size = new System.Drawing.Size(498, 253);
            this.dgvShipmentInfo.TabIndex = 77;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(505, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(493, 271);
            this.panel3.TabIndex = 4;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.dgvShipmentInfoAlready);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(0, 0);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.groupBox7.Size = new System.Drawing.Size(493, 271);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "已装车";
            // 
            // dgvShipmentInfoAlready
            // 
            this.dgvShipmentInfoAlready.AllowUserToAddRows = false;
            this.dgvShipmentInfoAlready.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvShipmentInfoAlready.BackgroundColor = System.Drawing.Color.White;
            this.dgvShipmentInfoAlready.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvShipmentInfoAlready.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvShipmentInfoAlready.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShipmentInfoAlready.Location = new System.Drawing.Point(5, 16);
            this.dgvShipmentInfoAlready.Name = "dgvShipmentInfoAlready";
            this.dgvShipmentInfoAlready.ReadOnly = true;
            this.dgvShipmentInfoAlready.RowTemplate.Height = 30;
            this.dgvShipmentInfoAlready.Size = new System.Drawing.Size(486, 253);
            this.dgvShipmentInfoAlready.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtMessage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 562);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(998, 44);
            this.panel2.TabIndex = 1;
            // 
            // txtMessage
            // 
            this.txtMessage.AutoEllipsis = true;
            this.txtMessage.BackColor = System.Drawing.Color.Blue;
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.txtMessage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtMessage.Location = new System.Drawing.Point(0, 0);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(998, 44);
            this.txtMessage.TabIndex = 62;
            this.txtMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Del_SelectRow_ToolStripMenuItem,
            this.Del_AllRow_ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(140, 48);
            // 
            // Del_SelectRow_ToolStripMenuItem
            // 
            this.Del_SelectRow_ToolStripMenuItem.Name = "Del_SelectRow_ToolStripMenuItem";
            this.Del_SelectRow_ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.Del_SelectRow_ToolStripMenuItem.Text = "删除选中行";
            // 
            // Del_AllRow_ToolStripMenuItem
            // 
            this.Del_AllRow_ToolStripMenuItem.Name = "Del_AllRow_ToolStripMenuItem";
            this.Del_AllRow_ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.Del_AllRow_ToolStripMenuItem.Text = "删除所有行";
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Enabled = false;
            this.labShow.Font = new System.Drawing.Font("SimSun", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.Color.White;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Name = "labShow";
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(998, 41);
            this.labShow.TabIndex = 62;
            this.labShow.Text = "ShipMent";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnSearch);
            this.groupBox3.Controls.Add(this.chkAllRecord);
            this.groupBox3.Controls.Add(this.dtpStartTime);
            this.groupBox3.Controls.Add(this.lbldate);
            this.groupBox3.Controls.Add(this.lblchepai);
            this.groupBox3.Controls.Add(this.dtpEndTime);
            this.groupBox3.Controls.Add(this.cboCarNo);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(8, 4);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(650, 77);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "筛选";
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("NSimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.Location = new System.Drawing.Point(513, 21);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(94, 35);
            this.btnSearch.TabIndex = 103;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // chkAllRecord
            // 
            this.chkAllRecord.AutoSize = true;
            this.chkAllRecord.Location = new System.Drawing.Point(415, 16);
            this.chkAllRecord.Name = "chkAllRecord";
            this.chkAllRecord.Size = new System.Drawing.Size(75, 18);
            this.chkAllRecord.TabIndex = 99;
            this.chkAllRecord.Text = "ALL记录";
            this.chkAllRecord.UseVisualStyleBackColor = true;
            this.chkAllRecord.CheckedChanged += new System.EventHandler(this.chkAllRecord_CheckedChanged);
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dtpStartTime.CustomFormat = "yyyy-MM-dd ";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(110, 16);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(114, 23);
            this.dtpStartTime.TabIndex = 86;
            this.dtpStartTime.ValueChanged += new System.EventHandler(this.dtpStartTime_ValueChanged);
            this.dtpStartTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpStartTime_KeyPress);
            // 
            // lbldate
            // 
            this.lbldate.AutoSize = true;
            this.lbldate.Location = new System.Drawing.Point(55, 19);
            this.lbldate.Name = "lbldate";
            this.lbldate.Size = new System.Drawing.Size(49, 14);
            this.lbldate.TabIndex = 69;
            this.lbldate.Text = "日 期:";
            // 
            // lblchepai
            // 
            this.lblchepai.AutoSize = true;
            this.lblchepai.Location = new System.Drawing.Point(48, 51);
            this.lblchepai.Name = "lblchepai";
            this.lblchepai.Size = new System.Drawing.Size(56, 14);
            this.lblchepai.TabIndex = 72;
            this.lblchepai.Text = "车牌号:";
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dtpEndTime.CustomFormat = "yyyy-MM-dd";
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(250, 16);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(114, 23);
            this.dtpEndTime.TabIndex = 98;
            this.dtpEndTime.ValueChanged += new System.EventHandler(this.dtpEndTime_ValueChanged);
            this.dtpEndTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpEndTime_KeyPress);
            // 
            // cboCarNo
            // 
            this.cboCarNo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cboCarNo.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboCarNo.FormattingEnabled = true;
            this.cboCarNo.Items.AddRange(new object[] {
            "卡车车牌号码"});
            this.cboCarNo.Location = new System.Drawing.Point(110, 45);
            this.cboCarNo.Name = "cboCarNo";
            this.cboCarNo.Size = new System.Drawing.Size(254, 24);
            this.cboCarNo.TabIndex = 0;
            this.cboCarNo.SelectedIndexChanged += new System.EventHandler(this.cboCarNo_SelectedIndexChanged);
            this.cboCarNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboCarNo_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(230, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 14);
            this.label4.TabIndex = 97;
            this.label4.Text = "-";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.labInfo);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.txtRemainNum);
            this.groupBox4.Controls.Add(this.txtAlreadyNum);
            this.groupBox4.Controls.Add(this.txtShallNum);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(8, 85);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(650, 99);
            this.groupBox4.TabIndex = 88;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "信息";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(443, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 14);
            this.label2.TabIndex = 90;
            this.label2.Text = "已装车栈板数:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 14);
            this.label1.TabIndex = 88;
            this.label1.Text = "应装栈板数:";
            // 
            // txtRemainNum
            // 
            this.txtRemainNum.BackColor = System.Drawing.Color.Yellow;
            this.txtRemainNum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRemainNum.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRemainNum.Location = new System.Drawing.Point(369, 17);
            this.txtRemainNum.Name = "txtRemainNum";
            this.txtRemainNum.ReadOnly = true;
            this.txtRemainNum.Size = new System.Drawing.Size(58, 31);
            this.txtRemainNum.TabIndex = 93;
            // 
            // txtAlreadyNum
            // 
            this.txtAlreadyNum.BackColor = System.Drawing.Color.Green;
            this.txtAlreadyNum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAlreadyNum.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAlreadyNum.ForeColor = System.Drawing.Color.Gold;
            this.txtAlreadyNum.Location = new System.Drawing.Point(584, 17);
            this.txtAlreadyNum.Name = "txtAlreadyNum";
            this.txtAlreadyNum.ReadOnly = true;
            this.txtAlreadyNum.Size = new System.Drawing.Size(58, 31);
            this.txtAlreadyNum.TabIndex = 91;
            // 
            // txtShallNum
            // 
            this.txtShallNum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtShallNum.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtShallNum.Location = new System.Drawing.Point(110, 20);
            this.txtShallNum.Name = "txtShallNum";
            this.txtShallNum.ReadOnly = true;
            this.txtShallNum.Size = new System.Drawing.Size(58, 31);
            this.txtShallNum.TabIndex = 89;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(230, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 14);
            this.label3.TabIndex = 92;
            this.label3.Text = "未装车栈板数:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblqianbanhao);
            this.groupBox5.Controls.Add(this.txtPalletNo);
            this.groupBox5.Location = new System.Drawing.Point(8, 188);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(650, 54);
            this.groupBox5.TabIndex = 88;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "栈板";
            // 
            // lblqianbanhao
            // 
            this.lblqianbanhao.AutoSize = true;
            this.lblqianbanhao.Location = new System.Drawing.Point(48, 23);
            this.lblqianbanhao.Name = "lblqianbanhao";
            this.lblqianbanhao.Size = new System.Drawing.Size(56, 14);
            this.lblqianbanhao.TabIndex = 74;
            this.lblqianbanhao.Text = "栈板号:";
            this.lblqianbanhao.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtPalletNo
            // 
            this.txtPalletNo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtPalletNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPalletNo.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPalletNo.ForeColor = System.Drawing.Color.Blue;
            this.txtPalletNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtPalletNo.Location = new System.Drawing.Point(126, 17);
            this.txtPalletNo.Name = "txtPalletNo";
            this.txtPalletNo.Size = new System.Drawing.Size(213, 31);
            this.txtPalletNo.TabIndex = 1;
            this.txtPalletNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPalletNo_KeyDown);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("NSimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(693, 25);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 35);
            this.button3.TabIndex = 102;
            this.button3.Text = "补列印";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panelDNDetail
            // 
            this.panelDNDetail.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelDNDetail.Controls.Add(this.btnConfirm);
            this.panelDNDetail.Controls.Add(this.button3);
            this.panelDNDetail.Controls.Add(this.groupBox5);
            this.panelDNDetail.Controls.Add(this.groupBox4);
            this.panelDNDetail.Controls.Add(this.groupBox3);
            this.panelDNDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDNDetail.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.panelDNDetail.Location = new System.Drawing.Point(0, 41);
            this.panelDNDetail.Name = "panelDNDetail";
            this.panelDNDetail.Size = new System.Drawing.Size(998, 250);
            this.panelDNDetail.TabIndex = 28;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Enabled = false;
            this.btnConfirm.Font = new System.Drawing.Font("NSimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.Location = new System.Drawing.Point(758, 124);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(94, 35);
            this.btnConfirm.TabIndex = 103;
            this.btnConfirm.Text = "装车确认";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Visible = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // labInfo
            // 
            this.labInfo.AutoSize = true;
            this.labInfo.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labInfo.ForeColor = System.Drawing.Color.Red;
            this.labInfo.Location = new System.Drawing.Point(22, 63);
            this.labInfo.Name = "labInfo";
            this.labInfo.Size = new System.Drawing.Size(475, 24);
            this.labInfo.TabIndex = 96;
            this.labInfo.Text = "此车同时包含EDI和AC的货，请注意装车！";
            this.labInfo.Visible = false;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(998, 606);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelDNDetail);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.labShow);
            this.Name = "fMain";
            this.Text = "Ver:1.0.0.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShipmentInfo)).EndInit();
            this.panel3.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShipmentInfoAlready)).EndInit();
            this.panel2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panelDNDetail.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Del_SelectRow_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Del_AllRow_ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvShipmentInfo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label txtMessage;
        private System.Windows.Forms.DataGridView dgvShipmentInfoAlready;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox chkAllRecord;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.Label lbldate;
        private System.Windows.Forms.Label lblchepai;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.ComboBox cboCarNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtShallNum;
        private System.Windows.Forms.TextBox txtRemainNum;
        private System.Windows.Forms.TextBox txtAlreadyNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblqianbanhao;
        private System.Windows.Forms.TextBox txtPalletNo;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panelDNDetail;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label labInfo;
    }
}