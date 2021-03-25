namespace PickList
{
    partial class PickListLabel
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
            this.TextMsg = new System.Windows.Forms.Label();
            this.Shipment_T = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbPallet = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.cmbSmid = new System.Windows.Forms.ComboBox();
            this.labShipmentID = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvPalletInfo = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPalletInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // TextMsg
            // 
            this.TextMsg.AutoEllipsis = true;
            this.TextMsg.BackColor = System.Drawing.Color.Blue;
            this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 368);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(714, 40);
            this.TextMsg.TabIndex = 71;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Shipment_T
            // 
            this.Shipment_T.BackColor = System.Drawing.SystemColors.HotTrack;
            this.Shipment_T.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Shipment_T.Dock = System.Windows.Forms.DockStyle.Top;
            this.Shipment_T.Font = new System.Drawing.Font("SimSun", 29F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Shipment_T.ForeColor = System.Drawing.SystemColors.Info;
            this.Shipment_T.Location = new System.Drawing.Point(0, 0);
            this.Shipment_T.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Shipment_T.Multiline = true;
            this.Shipment_T.Name = "Shipment_T";
            this.Shipment_T.ReadOnly = true;
            this.Shipment_T.Size = new System.Drawing.Size(714, 40);
            this.Shipment_T.TabIndex = 70;
            this.Shipment_T.Text = "PickList_Print";
            this.Shipment_T.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbPallet);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.cmbSmid);
            this.panel1.Controls.Add(this.labShipmentID);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(714, 45);
            this.panel1.TabIndex = 113;
            // 
            // cmbPallet
            // 
            this.cmbPallet.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPallet.FormattingEnabled = true;
            this.cmbPallet.ItemHeight = 16;
            this.cmbPallet.Location = new System.Drawing.Point(406, 9);
            this.cmbPallet.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.cmbPallet.Name = "cmbPallet";
            this.cmbPallet.Size = new System.Drawing.Size(194, 24);
            this.cmbPallet.TabIndex = 117;
            this.cmbPallet.SelectedIndexChanged += new System.EventHandler(this.cmbPallet_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(313, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 116;
            this.label1.Text = "栈板号:";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(620, 5);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 32);
            this.btnPrint.TabIndex = 115;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // cmbSmid
            // 
            this.cmbSmid.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbSmid.FormattingEnabled = true;
            this.cmbSmid.ItemHeight = 16;
            this.cmbSmid.Location = new System.Drawing.Point(105, 9);
            this.cmbSmid.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.cmbSmid.Name = "cmbSmid";
            this.cmbSmid.Size = new System.Drawing.Size(194, 24);
            this.cmbSmid.TabIndex = 114;
            this.cmbSmid.SelectedIndexChanged += new System.EventHandler(this.cmbSmid_SelectedIndexChanged);
            // 
            // labShipmentID
            // 
            this.labShipmentID.AutoSize = true;
            this.labShipmentID.Location = new System.Drawing.Point(14, 13);
            this.labShipmentID.Name = "labShipmentID";
            this.labShipmentID.Size = new System.Drawing.Size(59, 12);
            this.labShipmentID.TabIndex = 113;
            this.labShipmentID.Text = "集货单号:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvPalletInfo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(714, 283);
            this.groupBox1.TabIndex = 114;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "栈板信息";
            // 
            // dgvPalletInfo
            // 
            this.dgvPalletInfo.AllowUserToAddRows = false;
            this.dgvPalletInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPalletInfo.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvPalletInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPalletInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPalletInfo.Location = new System.Drawing.Point(3, 17);
            this.dgvPalletInfo.Name = "dgvPalletInfo";
            this.dgvPalletInfo.ReadOnly = true;
            this.dgvPalletInfo.RowHeadersWidth = 62;
            this.dgvPalletInfo.RowTemplate.Height = 23;
            this.dgvPalletInfo.Size = new System.Drawing.Size(708, 263);
            this.dgvPalletInfo.TabIndex = 0;
            // 
            // PickListLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 408);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TextMsg);
            this.Controls.Add(this.Shipment_T);
            this.Name = "PickListLabel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PickListLabel";
            this.Load += new System.EventHandler(this.PickListLabel_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPalletInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.TextBox Shipment_T;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbPallet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ComboBox cmbSmid;
        private System.Windows.Forms.Label labShipmentID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvPalletInfo;
    }
}