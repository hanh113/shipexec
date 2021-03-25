namespace wmsReport.Forms
{
    partial class StockTransfer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvStorage = new System.Windows.Forms.DataGridView();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.location_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.part_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Coo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pallet_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.Control = new System.Windows.Forms.GroupBox();
            this.lbqtyTotal = new System.Windows.Forms.Label();
            this.lbQty = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lbCoo = new System.Windows.Forms.Label();
            this.lbKeyPart = new System.Windows.Forms.Label();
            this.cmbCoo = new System.Windows.Forms.ComboBox();
            this.cmbKeyPart = new System.Windows.Forms.ComboBox();
            this.btnAllStockOut = new System.Windows.Forms.Button();
            this.prgBanner.SuspendLayout();
            this.prgMain.SuspendLayout();
            this.prgFooter.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStorage)).BeginInit();
            this.Control.SuspendLayout();
            this.SuspendLayout();
            // 
            // prgBanner
            // 
            this.prgBanner.Size = new System.Drawing.Size(1231, 65);
            // 
            // prgMain
            // 
            this.prgMain.Controls.Add(this.tableLayoutPanel1);
            this.prgMain.Size = new System.Drawing.Size(1231, 392);
            // 
            // prgTitle
            // 
            this.prgTitle.Size = new System.Drawing.Size(1231, 65);
            this.prgTitle.Text = "Intelligent storage Transfer";
            // 
            // prgFooter
            // 
            this.prgFooter.Location = new System.Drawing.Point(0, 457);
            this.prgFooter.Size = new System.Drawing.Size(1231, 60);
            // 
            // prgMSG
            // 
            this.prgMSG.Size = new System.Drawing.Size(1231, 60);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Control, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 20);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.67568F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 74.32433F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1225, 370);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 97);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvStorage);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1219, 271);
            this.splitContainer1.SplitterDistance = 552;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvStorage
            // 
            this.dgvStorage.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgvStorage.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvStorage.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvStorage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStorage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check,
            this.location_no,
            this.part_no,
            this.Coo,
            this.pallet_no,
            this.qty});
            this.dgvStorage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStorage.GridColor = System.Drawing.SystemColors.ControlText;
            this.dgvStorage.Location = new System.Drawing.Point(0, 0);
            this.dgvStorage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvStorage.Name = "dgvStorage";
            this.dgvStorage.RowHeadersVisible = false;
            this.dgvStorage.RowHeadersWidth = 62;
            this.dgvStorage.RowTemplate.Height = 30;
            this.dgvStorage.Size = new System.Drawing.Size(552, 271);
            this.dgvStorage.TabIndex = 0;
            this.dgvStorage.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStorage_CellClick);
            // 
            // Check
            // 
            this.Check.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Check.HeaderText = "Check";
            this.Check.MinimumWidth = 8;
            this.Check.Name = "Check";
            this.Check.ReadOnly = true;
            this.Check.Width = 53;
            // 
            // location_no
            // 
            this.location_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.location_no.DataPropertyName = "location_no";
            this.location_no.HeaderText = "储位";
            this.location_no.MinimumWidth = 8;
            this.location_no.Name = "location_no";
            this.location_no.ReadOnly = true;
            // 
            // part_no
            // 
            this.part_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.part_no.DataPropertyName = "part_no";
            this.part_no.HeaderText = "料号";
            this.part_no.MinimumWidth = 8;
            this.part_no.Name = "part_no";
            this.part_no.ReadOnly = true;
            // 
            // Coo
            // 
            this.Coo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Coo.DataPropertyName = "coo";
            this.Coo.HeaderText = "Coo";
            this.Coo.MinimumWidth = 8;
            this.Coo.Name = "Coo";
            this.Coo.ReadOnly = true;
            this.Coo.Width = 60;
            // 
            // pallet_no
            // 
            this.pallet_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.pallet_no.DataPropertyName = "pallet_no";
            this.pallet_no.HeaderText = "栈板号";
            this.pallet_no.MinimumWidth = 8;
            this.pallet_no.Name = "pallet_no";
            this.pallet_no.ReadOnly = true;
            // 
            // qty
            // 
            this.qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.qty.DataPropertyName = "qty";
            this.qty.HeaderText = "数量";
            this.qty.MinimumWidth = 8;
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            this.qty.Width = 66;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(661, 271);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // Control
            // 
            this.Control.Controls.Add(this.btnAllStockOut);
            this.Control.Controls.Add(this.lbqtyTotal);
            this.Control.Controls.Add(this.lbQty);
            this.Control.Controls.Add(this.btnQuery);
            this.Control.Controls.Add(this.btnStart);
            this.Control.Controls.Add(this.lbCoo);
            this.Control.Controls.Add(this.lbKeyPart);
            this.Control.Controls.Add(this.cmbCoo);
            this.Control.Controls.Add(this.cmbKeyPart);
            this.Control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Control.Location = new System.Drawing.Point(3, 2);
            this.Control.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Control.Name = "Control";
            this.Control.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Control.Size = new System.Drawing.Size(1219, 91);
            this.Control.TabIndex = 1;
            this.Control.TabStop = false;
            this.Control.Text = "Control";
            // 
            // lbqtyTotal
            // 
            this.lbqtyTotal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbqtyTotal.AutoSize = true;
            this.lbqtyTotal.Font = new System.Drawing.Font("Microsoft YaHei", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbqtyTotal.ForeColor = System.Drawing.Color.Blue;
            this.lbqtyTotal.Location = new System.Drawing.Point(1179, 47);
            this.lbqtyTotal.Name = "lbqtyTotal";
            this.lbqtyTotal.Size = new System.Drawing.Size(28, 31);
            this.lbqtyTotal.TabIndex = 4;
            this.lbqtyTotal.Text = "0";
            // 
            // lbQty
            // 
            this.lbQty.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbQty.AutoSize = true;
            this.lbQty.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbQty.Location = new System.Drawing.Point(1157, 20);
            this.lbQty.Name = "lbQty";
            this.lbQty.Size = new System.Drawing.Size(44, 18);
            this.lbQty.TabIndex = 3;
            this.lbQty.Text = "数量";
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnQuery.Location = new System.Drawing.Point(755, 26);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(105, 49);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnStart.Location = new System.Drawing.Point(931, 26);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(105, 49);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lbCoo
            // 
            this.lbCoo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbCoo.AutoSize = true;
            this.lbCoo.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCoo.Location = new System.Drawing.Point(396, 38);
            this.lbCoo.Name = "lbCoo";
            this.lbCoo.Size = new System.Drawing.Size(35, 18);
            this.lbCoo.TabIndex = 1;
            this.lbCoo.Text = "Coo";
            // 
            // lbKeyPart
            // 
            this.lbKeyPart.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbKeyPart.AutoSize = true;
            this.lbKeyPart.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbKeyPart.Location = new System.Drawing.Point(7, 39);
            this.lbKeyPart.Name = "lbKeyPart";
            this.lbKeyPart.Size = new System.Drawing.Size(44, 18);
            this.lbKeyPart.TabIndex = 1;
            this.lbKeyPart.Text = "料号";
            // 
            // cmbCoo
            // 
            this.cmbCoo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbCoo.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCoo.FormattingEnabled = true;
            this.cmbCoo.Location = new System.Drawing.Point(485, 35);
            this.cmbCoo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbCoo.Name = "cmbCoo";
            this.cmbCoo.Size = new System.Drawing.Size(238, 28);
            this.cmbCoo.TabIndex = 0;
            // 
            // cmbKeyPart
            // 
            this.cmbKeyPart.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbKeyPart.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbKeyPart.FormattingEnabled = true;
            this.cmbKeyPart.Location = new System.Drawing.Point(105, 35);
            this.cmbKeyPart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbKeyPart.Name = "cmbKeyPart";
            this.cmbKeyPart.Size = new System.Drawing.Size(238, 28);
            this.cmbKeyPart.TabIndex = 0;
            // 
            // btnAllStockOut
            // 
            this.btnAllStockOut.Location = new System.Drawing.Point(1059, 27);
            this.btnAllStockOut.Name = "btnAllStockOut";
            this.btnAllStockOut.Size = new System.Drawing.Size(92, 44);
            this.btnAllStockOut.TabIndex = 5;
            this.btnAllStockOut.Text = "StockOut";
            this.btnAllStockOut.UseVisualStyleBackColor = true;
            this.btnAllStockOut.Click += new System.EventHandler(this.btnAllStockOut_Click);
            // 
            // StockTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1231, 517);
            this.Name = "StockTransfer";
            this.Text = "StockTransfer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.StockTransfer_Load);
            this.prgBanner.ResumeLayout(false);
            this.prgMain.ResumeLayout(false);
            this.prgFooter.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStorage)).EndInit();
            this.Control.ResumeLayout(false);
            this.Control.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox Control;
        private System.Windows.Forms.DataGridView dgvStorage;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lbCoo;
        private System.Windows.Forms.Label lbKeyPart;
        private System.Windows.Forms.ComboBox cmbCoo;
        private System.Windows.Forms.ComboBox cmbKeyPart;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label lbqtyTotal;
        private System.Windows.Forms.Label lbQty;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private System.Windows.Forms.DataGridViewTextBoxColumn location_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn part_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn Coo;
        private System.Windows.Forms.DataGridViewTextBoxColumn pallet_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.Button btnAllStockOut;
    }
}