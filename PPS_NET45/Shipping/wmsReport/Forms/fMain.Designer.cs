namespace wmsReport
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
            this.dgvNo = new System.Windows.Forms.DataGridView();
            this.labShow = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnStockTransfer = new System.Windows.Forms.Button();
            this.btnTrolleyMove = new System.Windows.Forms.Button();
            this.Fast_search_BTN = new System.Windows.Forms.Button();
            this.btnCheckTranFile = new System.Windows.Forms.Button();
            this.createDnPdf_BTN = new System.Windows.Forms.Button();
            this.btnPpartTrans = new System.Windows.Forms.Button();
            this.ZF_BTN = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.reportS_BTN = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.radDetail = new System.Windows.Forms.RadioButton();
            this.radSummary = new System.Windows.Forms.RadioButton();
            this.cmbTYPE = new System.Windows.Forms.ComboBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.labType = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnEartips = new System.Windows.Forms.Button();
            this.btnPPSOUT = new System.Windows.Forms.Button();
            this.btnTroStockIn = new System.Windows.Forms.Button();
            this.btnCarInfo = new System.Windows.Forms.Button();
            this.btnPpartCheck = new System.Windows.Forms.Button();
            this.lblstrFile = new System.Windows.Forms.Label();
            this.cmbICTNO = new System.Windows.Forms.ComboBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dt_start = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.dt_end = new System.Windows.Forms.DateTimePicker();
            this.lblStart = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbWHID = new System.Windows.Forms.ComboBox();
            this.labWHID = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.TextMsg = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNo)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvNo
            // 
            this.dgvNo.AllowUserToAddRows = false;
            this.dgvNo.AllowUserToDeleteRows = false;
            this.dgvNo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvNo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNo.ColumnHeadersHeight = 34;
            this.dgvNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNo.Location = new System.Drawing.Point(3, 16);
            this.dgvNo.Margin = new System.Windows.Forms.Padding(2);
            this.dgvNo.MultiSelect = false;
            this.dgvNo.Name = "dgvNo";
            this.dgvNo.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNo.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNo.RowHeadersWidth = 50;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.dgvNo.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvNo.RowTemplate.Height = 27;
            this.dgvNo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvNo.Size = new System.Drawing.Size(1170, 320);
            this.dgvNo.TabIndex = 94;
            this.dgvNo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNo_CellContentClick);
            this.dgvNo.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvNo_RowsAdded);
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labShow.Enabled = false;
            this.labShow.Font = new System.Drawing.Font("SimSun", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.SystemColors.Info;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labShow.Name = "labShow";
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(1176, 41);
            this.labShow.TabIndex = 61;
            this.labShow.Text = "WMS Report";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labShow);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1176, 46);
            this.panel2.TabIndex = 118;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox2.Controls.Add(this.btnStockTransfer);
            this.groupBox2.Controls.Add(this.btnTrolleyMove);
            this.groupBox2.Controls.Add(this.Fast_search_BTN);
            this.groupBox2.Controls.Add(this.btnCheckTranFile);
            this.groupBox2.Controls.Add(this.createDnPdf_BTN);
            this.groupBox2.Controls.Add(this.btnPpartTrans);
            this.groupBox2.Controls.Add(this.ZF_BTN);
            this.groupBox2.Controls.Add(this.btnQuery);
            this.groupBox2.Controls.Add(this.reportS_BTN);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.radDetail);
            this.groupBox2.Controls.Add(this.radSummary);
            this.groupBox2.Controls.Add(this.cmbTYPE);
            this.groupBox2.Controls.Add(this.btnCheck);
            this.groupBox2.Controls.Add(this.labType);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 46);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(1176, 85);
            this.groupBox2.TabIndex = 119;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "报表";
            // 
            // btnStockTransfer
            // 
            this.btnStockTransfer.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStockTransfer.Location = new System.Drawing.Point(736, 15);
            this.btnStockTransfer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStockTransfer.Name = "btnStockTransfer";
            this.btnStockTransfer.Size = new System.Drawing.Size(104, 24);
            this.btnStockTransfer.TabIndex = 127;
            this.btnStockTransfer.Text = "储位移转(立库)";
            this.btnStockTransfer.UseVisualStyleBackColor = true;
            this.btnStockTransfer.Click += new System.EventHandler(this.btnStockTransfer_Click);
            // 
            // btnTrolleyMove
            // 
            this.btnTrolleyMove.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrolleyMove.Location = new System.Drawing.Point(864, 50);
            this.btnTrolleyMove.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTrolleyMove.Name = "btnTrolleyMove";
            this.btnTrolleyMove.Size = new System.Drawing.Size(184, 24);
            this.btnTrolleyMove.TabIndex = 126;
            this.btnTrolleyMove.Text = "金刚车整车移储位";
            this.btnTrolleyMove.UseVisualStyleBackColor = true;
            this.btnTrolleyMove.Click += new System.EventHandler(this.btnTrolleyMove_Click);
            // 
            // Fast_search_BTN
            // 
            this.Fast_search_BTN.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fast_search_BTN.Location = new System.Drawing.Point(736, 50);
            this.Fast_search_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.Fast_search_BTN.Name = "Fast_search_BTN";
            this.Fast_search_BTN.Size = new System.Drawing.Size(104, 24);
            this.Fast_search_BTN.TabIndex = 125;
            this.Fast_search_BTN.Text = "快速查找";
            this.Fast_search_BTN.UseVisualStyleBackColor = true;
            this.Fast_search_BTN.Click += new System.EventHandler(this.Fast_search_BTN_Click);
            // 
            // btnCheckTranFile
            // 
            this.btnCheckTranFile.Enabled = false;
            this.btnCheckTranFile.Location = new System.Drawing.Point(1073, 49);
            this.btnCheckTranFile.Name = "btnCheckTranFile";
            this.btnCheckTranFile.Size = new System.Drawing.Size(91, 25);
            this.btnCheckTranFile.TabIndex = 127;
            this.btnCheckTranFile.Text = "检查Tran.in File";
            this.btnCheckTranFile.UseVisualStyleBackColor = true;
            this.btnCheckTranFile.Visible = false;
            this.btnCheckTranFile.Click += new System.EventHandler(this.btnCheckTranFile_Click);
            // 
            // createDnPdf_BTN
            // 
            this.createDnPdf_BTN.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createDnPdf_BTN.Location = new System.Drawing.Point(615, 50);
            this.createDnPdf_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.createDnPdf_BTN.Name = "createDnPdf_BTN";
            this.createDnPdf_BTN.Size = new System.Drawing.Size(99, 24);
            this.createDnPdf_BTN.TabIndex = 124;
            this.createDnPdf_BTN.Text = "DN_PDF生成";
            this.createDnPdf_BTN.UseVisualStyleBackColor = true;
            this.createDnPdf_BTN.Click += new System.EventHandler(this.createDnPdf_BTN_Click);
            // 
            // btnPpartTrans
            // 
            this.btnPpartTrans.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPpartTrans.Location = new System.Drawing.Point(864, 15);
            this.btnPpartTrans.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPpartTrans.Name = "btnPpartTrans";
            this.btnPpartTrans.Size = new System.Drawing.Size(184, 24);
            this.btnPpartTrans.TabIndex = 123;
            this.btnPpartTrans.Text = "P-Part 储位移转";
            this.btnPpartTrans.UseVisualStyleBackColor = true;
            this.btnPpartTrans.Click += new System.EventHandler(this.btnPpartTrans_Click);
            // 
            // ZF_BTN
            // 
            this.ZF_BTN.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZF_BTN.Location = new System.Drawing.Point(615, 13);
            this.ZF_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.ZF_BTN.Name = "ZF_BTN";
            this.ZF_BTN.Size = new System.Drawing.Size(99, 24);
            this.ZF_BTN.TabIndex = 122;
            this.ZF_BTN.Text = "杂发出货";
            this.ZF_BTN.UseVisualStyleBackColor = true;
            this.ZF_BTN.Click += new System.EventHandler(this.ZF_BTN_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuery.Location = new System.Drawing.Point(510, 13);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(99, 24);
            this.btnQuery.TabIndex = 121;
            this.btnQuery.Text = "储位查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // reportS_BTN
            // 
            this.reportS_BTN.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportS_BTN.Location = new System.Drawing.Point(510, 50);
            this.reportS_BTN.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.reportS_BTN.Name = "reportS_BTN";
            this.reportS_BTN.Size = new System.Drawing.Size(99, 24);
            this.reportS_BTN.TabIndex = 120;
            this.reportS_BTN.Text = "Report查询";
            this.reportS_BTN.UseVisualStyleBackColor = true;
            this.reportS_BTN.Click += new System.EventHandler(this.reportS_BTN_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(194, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 119;
            this.label2.Text = "样式：";
            // 
            // radDetail
            // 
            this.radDetail.AutoSize = true;
            this.radDetail.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radDetail.Location = new System.Drawing.Point(274, 54);
            this.radDetail.Margin = new System.Windows.Forms.Padding(2);
            this.radDetail.Name = "radDetail";
            this.radDetail.Size = new System.Drawing.Size(79, 20);
            this.radDetail.TabIndex = 118;
            this.radDetail.Text = "Detail Report";
            this.radDetail.UseVisualStyleBackColor = true;
            // 
            // radSummary
            // 
            this.radSummary.AutoSize = true;
            this.radSummary.Checked = true;
            this.radSummary.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radSummary.Location = new System.Drawing.Point(274, 20);
            this.radSummary.Margin = new System.Windows.Forms.Padding(2);
            this.radSummary.Name = "radSummary";
            this.radSummary.Size = new System.Drawing.Size(98, 20);
            this.radSummary.TabIndex = 117;
            this.radSummary.TabStop = true;
            this.radSummary.Text = "Summary Report";
            this.radSummary.UseVisualStyleBackColor = true;
            this.radSummary.CheckedChanged += new System.EventHandler(this.radSummary_CheckedChanged);
            // 
            // cmbTYPE
            // 
            this.cmbTYPE.FormattingEnabled = true;
            this.cmbTYPE.Items.AddRange(new object[] {
            "库存报表",
            "储位检查报表",
            "PPart车位报表",
            "p-part盘点报表"});
            this.cmbTYPE.Location = new System.Drawing.Point(64, 20);
            this.cmbTYPE.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbTYPE.Name = "cmbTYPE";
            this.cmbTYPE.Size = new System.Drawing.Size(126, 24);
            this.cmbTYPE.TabIndex = 116;
            this.cmbTYPE.Text = "库存报表";
            this.cmbTYPE.SelectedIndexChanged += new System.EventHandler(this.cmbTYPE_SelectedIndexChanged);
            // 
            // btnCheck
            // 
            this.btnCheck.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.Location = new System.Drawing.Point(394, 50);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(104, 24);
            this.btnCheck.TabIndex = 116;
            this.btnCheck.Text = "储位盘点";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // labType
            // 
            this.labType.AutoSize = true;
            this.labType.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labType.Location = new System.Drawing.Point(4, 24);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(44, 16);
            this.labType.TabIndex = 115;
            this.labType.Text = "类型：";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(394, 14);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 24);
            this.button2.TabIndex = 113;
            this.button2.Text = "储位移转";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox3.Controls.Add(this.txtQty);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.btnEartips);
            this.groupBox3.Controls.Add(this.btnPPSOUT);
            this.groupBox3.Controls.Add(this.btnTroStockIn);
            this.groupBox3.Controls.Add(this.btnCarInfo);
            this.groupBox3.Controls.Add(this.btnPpartCheck);
            this.groupBox3.Controls.Add(this.lblstrFile);
            this.groupBox3.Controls.Add(this.cmbICTNO);
            this.groupBox3.Controls.Add(this.btnExport);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.dt_start);
            this.groupBox3.Controls.Add(this.lblEnd);
            this.groupBox3.Controls.Add(this.cmbLocation);
            this.groupBox3.Controls.Add(this.dt_end);
            this.groupBox3.Controls.Add(this.lblStart);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.cmbWHID);
            this.groupBox3.Controls.Add(this.labWHID);
            this.groupBox3.Controls.Add(this.btnSearch);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(0, 131);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(1176, 89);
            this.groupBox3.TabIndex = 120;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查询条件";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(501, 60);
            this.txtQty.Margin = new System.Windows.Forms.Padding(2);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(57, 21);
            this.txtQty.TabIndex = 131;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(455, 65);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 16);
            this.label5.TabIndex = 130;
            this.label5.Text = "数量:";
            // 
            // btnEartips
            // 
            this.btnEartips.Location = new System.Drawing.Point(940, 57);
            this.btnEartips.Name = "btnEartips";
            this.btnEartips.Size = new System.Drawing.Size(104, 25);
            this.btnEartips.TabIndex = 129;
            this.btnEartips.Text = "Eartips入库";
            this.btnEartips.UseVisualStyleBackColor = true;
            this.btnEartips.Visible = false;
            this.btnEartips.Click += new System.EventHandler(this.btnEartips_Click);
            // 
            // btnPPSOUT
            // 
            this.btnPPSOUT.Location = new System.Drawing.Point(1052, 56);
            this.btnPPSOUT.Name = "btnPPSOUT";
            this.btnPPSOUT.Size = new System.Drawing.Size(104, 25);
            this.btnPPSOUT.TabIndex = 117;
            this.btnPPSOUT.Text = "PPS按箱出库";
            this.btnPPSOUT.UseVisualStyleBackColor = true;
            this.btnPPSOUT.Visible = false;
            this.btnPPSOUT.Click += new System.EventHandler(this.btnPPSOUT_Click);
            // 
            // btnTroStockIn
            // 
            this.btnTroStockIn.Location = new System.Drawing.Point(940, 14);
            this.btnTroStockIn.Name = "btnTroStockIn";
            this.btnTroStockIn.Size = new System.Drawing.Size(104, 25);
            this.btnTroStockIn.TabIndex = 128;
            this.btnTroStockIn.Text = "PPS精钢车入库";
            this.btnTroStockIn.UseVisualStyleBackColor = true;
            this.btnTroStockIn.Visible = false;
            this.btnTroStockIn.Click += new System.EventHandler(this.btnTroStockIn_Click);
            // 
            // btnCarInfo
            // 
            this.btnCarInfo.Location = new System.Drawing.Point(775, 15);
            this.btnCarInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCarInfo.Name = "btnCarInfo";
            this.btnCarInfo.Size = new System.Drawing.Size(118, 24);
            this.btnCarInfo.TabIndex = 126;
            this.btnCarInfo.Text = "装车明细导出";
            this.btnCarInfo.UseVisualStyleBackColor = true;
            this.btnCarInfo.Click += new System.EventHandler(this.btnCarInfo_Click);
            // 
            // btnPpartCheck
            // 
            this.btnPpartCheck.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPpartCheck.Location = new System.Drawing.Point(800, 59);
            this.btnPpartCheck.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPpartCheck.Name = "btnPpartCheck";
            this.btnPpartCheck.Size = new System.Drawing.Size(91, 24);
            this.btnPpartCheck.TabIndex = 115;
            this.btnPpartCheck.Text = "P-Part 储位盘点";
            this.btnPpartCheck.UseVisualStyleBackColor = true;
            this.btnPpartCheck.Visible = false;
            this.btnPpartCheck.Click += new System.EventHandler(this.btnPpartCheck_Click);
            // 
            // lblstrFile
            // 
            this.lblstrFile.AutoSize = true;
            this.lblstrFile.Location = new System.Drawing.Point(772, 63);
            this.lblstrFile.Name = "lblstrFile";
            this.lblstrFile.Size = new System.Drawing.Size(0, 16);
            this.lblstrFile.TabIndex = 111;
            this.lblstrFile.Visible = false;
            // 
            // cmbICTNO
            // 
            this.cmbICTNO.FormattingEnabled = true;
            this.cmbICTNO.Location = new System.Drawing.Point(261, 59);
            this.cmbICTNO.Margin = new System.Windows.Forms.Padding(2);
            this.cmbICTNO.Name = "cmbICTNO";
            this.cmbICTNO.Size = new System.Drawing.Size(133, 24);
            this.cmbICTNO.TabIndex = 110;
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(692, 60);
            this.btnExport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(80, 24);
            this.btnExport.TabIndex = 90;
            this.btnExport.Text = "导出Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(208, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 109;
            this.label4.Text = "料号：";
            // 
            // dt_start
            // 
            this.dt_start.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_start.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dt_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_start.Location = new System.Drawing.Point(300, 22);
            this.dt_start.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dt_start.Name = "dt_start";
            this.dt_start.Size = new System.Drawing.Size(174, 21);
            this.dt_start.TabIndex = 93;
            this.dt_start.Visible = false;
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnd.Location = new System.Drawing.Point(484, 24);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(68, 16);
            this.lblEnd.TabIndex = 92;
            this.lblEnd.Text = "结束时间：";
            this.lblEnd.Visible = false;
            // 
            // cmbLocation
            // 
            this.cmbLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(64, 59);
            this.cmbLocation.Margin = new System.Windows.Forms.Padding(2);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(128, 24);
            this.cmbLocation.TabIndex = 108;
            // 
            // dt_end
            // 
            this.dt_end.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_end.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dt_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_end.Location = new System.Drawing.Point(585, 17);
            this.dt_end.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dt_end.Name = "dt_end";
            this.dt_end.Size = new System.Drawing.Size(174, 21);
            this.dt_end.TabIndex = 10;
            this.dt_end.Visible = false;
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStart.Location = new System.Drawing.Point(203, 24);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(68, 16);
            this.lblStart.TabIndex = 9;
            this.lblStart.Text = "开始时间：";
            this.lblStart.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 16);
            this.label3.TabIndex = 107;
            this.label3.Text = "储位：";
            // 
            // cmbWHID
            // 
            this.cmbWHID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWHID.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWHID.FormattingEnabled = true;
            this.cmbWHID.Location = new System.Drawing.Point(64, 24);
            this.cmbWHID.Margin = new System.Windows.Forms.Padding(2);
            this.cmbWHID.Name = "cmbWHID";
            this.cmbWHID.Size = new System.Drawing.Size(128, 25);
            this.cmbWHID.TabIndex = 106;
            this.cmbWHID.SelectedIndexChanged += new System.EventHandler(this.cmbWHID_SelectedIndexChanged);
            // 
            // labWHID
            // 
            this.labWHID.AutoSize = true;
            this.labWHID.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labWHID.Location = new System.Drawing.Point(4, 24);
            this.labWHID.Name = "labWHID";
            this.labWHID.Size = new System.Drawing.Size(44, 16);
            this.labWHID.TabIndex = 105;
            this.labWHID.Text = "仓库：";
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(603, 60);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 24);
            this.btnSearch.TabIndex = 82;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // TextMsg
            // 
            this.TextMsg.AutoEllipsis = true;
            this.TextMsg.BackColor = System.Drawing.Color.Blue;
            this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1176, 48);
            this.TextMsg.TabIndex = 122;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TextMsg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 558);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1176, 48);
            this.panel1.TabIndex = 123;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvNo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 220);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(1176, 338);
            this.groupBox1.TabIndex = 124;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "库存报表";
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1176, 606);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "fMain";
            this.Text = "Ver:1.0.0.0";
            this.Load += new System.EventHandler(this.fMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNo)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.DataGridView dgvNo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radDetail;
        private System.Windows.Forms.RadioButton radSummary;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.ComboBox cmbTYPE;
        private System.Windows.Forms.Label labType;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblstrFile;
        private System.Windows.Forms.ComboBox cmbICTNO;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dt_start;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.DateTimePicker dt_end;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbWHID;
        private System.Windows.Forms.Label labWHID;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button reportS_BTN;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button ZF_BTN;
        private System.Windows.Forms.Button btnPpartCheck;
        private System.Windows.Forms.Button btnPpartTrans;
        private System.Windows.Forms.Button createDnPdf_BTN;
        private System.Windows.Forms.Button Fast_search_BTN;
        private System.Windows.Forms.Button btnTrolleyMove;
        private System.Windows.Forms.Button btnStockTransfer;
        private System.Windows.Forms.Button btnCarInfo;
        private System.Windows.Forms.Button btnPPSOUT;
        private System.Windows.Forms.Button btnCheckTranFile;
        private System.Windows.Forms.Button btnTroStockIn;
        private System.Windows.Forms.Button btnEartips;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label5;
    }
}

