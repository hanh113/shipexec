namespace Stock_In
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbLocation = new System.Windows.Forms.ComboBox();
            this.cbWarehouse = new System.Windows.Forms.ComboBox();
            this.lbWarehouse = new System.Windows.Forms.Label();
            this.lbLocation = new System.Windows.Forms.Label();
            this.lbQty = new System.Windows.Forms.Label();
            this.lbcount = new System.Windows.Forms.Label();
            this.tbPallet = new System.Windows.Forms.TextBox();
            this.lbScanInfo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvInfo = new System.Windows.Forms.DataGridView();
            this.sn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Carton = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pallet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnStock = new System.Windows.Forms.Button();
            this.prgBanner.SuspendLayout();
            this.prgMain.SuspendLayout();
            this.prgFooter.SuspendLayout();
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
            this.prgBanner.Size = new System.Drawing.Size(1257, 79);
            // 
            // prgMain
            // 
            this.prgMain.Controls.Add(this.splitContainer1);
            this.prgMain.Size = new System.Drawing.Size(1257, 427);
            // 
            // prgTitle
            // 
            this.prgTitle.Size = new System.Drawing.Size(1257, 79);
            // 
            // prgFooter
            // 
            this.prgFooter.Location = new System.Drawing.Point(0, 506);
            this.prgFooter.Size = new System.Drawing.Size(1257, 72);
            // 
            // prgMSG
            // 
            this.prgMSG.Size = new System.Drawing.Size(1257, 72);
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
            this.splitContainer1.Size = new System.Drawing.Size(1251, 400);
            this.splitContainer1.SplitterDistance = 231;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.btnStock);
            this.panel1.Controls.Add(this.cbLocation);
            this.panel1.Controls.Add(this.cbWarehouse);
            this.panel1.Controls.Add(this.lbWarehouse);
            this.panel1.Controls.Add(this.lbLocation);
            this.panel1.Controls.Add(this.lbQty);
            this.panel1.Controls.Add(this.lbcount);
            this.panel1.Controls.Add(this.tbPallet);
            this.panel1.Controls.Add(this.lbScanInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(231, 400);
            this.panel1.TabIndex = 0;
            // 
            // cbLocation
            // 
            this.cbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLocation.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbLocation.FormattingEnabled = true;
            this.cbLocation.Location = new System.Drawing.Point(24, 165);
            this.cbLocation.Name = "cbLocation";
            this.cbLocation.Size = new System.Drawing.Size(297, 39);
            this.cbLocation.TabIndex = 5;
            // 
            // cbWarehouse
            // 
            this.cbWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWarehouse.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbWarehouse.FormattingEnabled = true;
            this.cbWarehouse.Location = new System.Drawing.Point(24, 66);
            this.cbWarehouse.Name = "cbWarehouse";
            this.cbWarehouse.Size = new System.Drawing.Size(297, 39);
            this.cbWarehouse.TabIndex = 5;
            this.cbWarehouse.SelectedIndexChanged += new System.EventHandler(this.cbWarehouse_SelectedIndexChanged);
            // 
            // lbWarehouse
            // 
            this.lbWarehouse.AutoSize = true;
            this.lbWarehouse.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbWarehouse.Location = new System.Drawing.Point(19, 23);
            this.lbWarehouse.Name = "lbWarehouse";
            this.lbWarehouse.Size = new System.Drawing.Size(150, 28);
            this.lbWarehouse.TabIndex = 4;
            this.lbWarehouse.Text = "WareHouse：";
            // 
            // lbLocation
            // 
            this.lbLocation.AutoSize = true;
            this.lbLocation.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbLocation.Location = new System.Drawing.Point(19, 125);
            this.lbLocation.Name = "lbLocation";
            this.lbLocation.Size = new System.Drawing.Size(120, 28);
            this.lbLocation.TabIndex = 4;
            this.lbLocation.Text = "Location：";
            // 
            // lbQty
            // 
            this.lbQty.AutoSize = true;
            this.lbQty.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbQty.Location = new System.Drawing.Point(101, 336);
            this.lbQty.Name = "lbQty";
            this.lbQty.Size = new System.Drawing.Size(31, 36);
            this.lbQty.TabIndex = 3;
            this.lbQty.Text = "0";
            // 
            // lbcount
            // 
            this.lbcount.AutoSize = true;
            this.lbcount.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbcount.Location = new System.Drawing.Point(19, 339);
            this.lbcount.Name = "lbcount";
            this.lbcount.Size = new System.Drawing.Size(75, 28);
            this.lbcount.TabIndex = 2;
            this.lbcount.Text = "数量：";
            // 
            // tbPallet
            // 
            this.tbPallet.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPallet.Location = new System.Drawing.Point(24, 268);
            this.tbPallet.Name = "tbPallet";
            this.tbPallet.Size = new System.Drawing.Size(297, 39);
            this.tbPallet.TabIndex = 1;
            this.tbPallet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPallet_KeyDown);
            // 
            // lbScanInfo
            // 
            this.lbScanInfo.AutoSize = true;
            this.lbScanInfo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbScanInfo.Location = new System.Drawing.Point(15, 220);
            this.lbScanInfo.Name = "lbScanInfo";
            this.lbScanInfo.Size = new System.Drawing.Size(126, 28);
            this.lbScanInfo.TabIndex = 0;
            this.lbScanInfo.Text = "Pallet No：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvInfo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1016, 400);
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
            this.sn,
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
            this.dgvInfo.Size = new System.Drawing.Size(1016, 400);
            this.dgvInfo.TabIndex = 0;
            // 
            // sn
            // 
            this.sn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sn.DataPropertyName = "serial_number";
            this.sn.HeaderText = "SN";
            this.sn.MinimumWidth = 8;
            this.sn.Name = "sn";
            this.sn.ReadOnly = true;
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
            // btnStock
            // 
            this.btnStock.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStock.Location = new System.Drawing.Point(173, 327);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(117, 55);
            this.btnStock.TabIndex = 6;
            this.btnStock.Text = "StockIN";
            this.btnStock.UseVisualStyleBackColor = true;
            this.btnStock.Click += new System.EventHandler(this.btnStock_Click);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 578);
            this.Name = "fMain";
            this.Text = "Stock_IN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fMain_Load);
            this.prgBanner.ResumeLayout(false);
            this.prgMain.ResumeLayout(false);
            this.prgFooter.ResumeLayout(false);
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

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbQty;
        private System.Windows.Forms.Label lbcount;
        private System.Windows.Forms.TextBox tbPallet;
        private System.Windows.Forms.Label lbScanInfo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn sn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn Carton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pallet;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.ComboBox cbLocation;
        private System.Windows.Forms.ComboBox cbWarehouse;
        private System.Windows.Forms.Label lbWarehouse;
        private System.Windows.Forms.Label lbLocation;
        private System.Windows.Forms.Button btnStock;
    }
}

