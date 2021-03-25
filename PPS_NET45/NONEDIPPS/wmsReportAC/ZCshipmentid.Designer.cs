namespace wmsReportAC
{
    partial class ZCshipmentid
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.dt_start = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dt_end = new System.Windows.Forms.DateTimePicker();
            this.lblStart = new System.Windows.Forms.Label();
            this.TextMsg = new System.Windows.Forms.Label();
            this.Shipment_T = new System.Windows.Forms.TextBox();
            this.btnRollback = new System.Windows.Forms.Button();
            this.cmbSmid = new System.Windows.Forms.ComboBox();
            this.labShipmentID = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(311, 58);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 32);
            this.btnSearch.TabIndex = 147;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dt_start
            // 
            this.dt_start.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_start.CustomFormat = "yyyy-MM-dd";
            this.dt_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_start.Location = new System.Drawing.Point(155, 50);
            this.dt_start.Name = "dt_start";
            this.dt_start.Size = new System.Drawing.Size(111, 21);
            this.dt_start.TabIndex = 146;
            this.dt_start.Value = new System.DateTime(2019, 7, 12, 0, 0, 0, 0);
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(74, 84);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(59, 12);
            this.lblEnd.TabIndex = 145;
            this.lblEnd.Text = "结束日期:";
            // 
            // dt_end
            // 
            this.dt_end.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_end.CustomFormat = "yyyy-MM-dd";
            this.dt_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_end.Location = new System.Drawing.Point(155, 77);
            this.dt_end.Name = "dt_end";
            this.dt_end.Size = new System.Drawing.Size(111, 21);
            this.dt_end.TabIndex = 144;
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(76, 58);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(59, 12);
            this.lblStart.TabIndex = 143;
            this.lblStart.Text = "开始日期:";
            // 
            // TextMsg
            // 
            this.TextMsg.AutoEllipsis = true;
            this.TextMsg.BackColor = System.Drawing.Color.Blue;
            this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 221);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(456, 40);
            this.TextMsg.TabIndex = 142;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Shipment_T
            // 
            this.Shipment_T.BackColor = System.Drawing.SystemColors.HotTrack;
            this.Shipment_T.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Shipment_T.Dock = System.Windows.Forms.DockStyle.Top;
            this.Shipment_T.Font = new System.Drawing.Font("宋体", 29F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Shipment_T.ForeColor = System.Drawing.SystemColors.Info;
            this.Shipment_T.Location = new System.Drawing.Point(0, 0);
            this.Shipment_T.Margin = new System.Windows.Forms.Padding(2);
            this.Shipment_T.Multiline = true;
            this.Shipment_T.Name = "Shipment_T";
            this.Shipment_T.ReadOnly = true;
            this.Shipment_T.Size = new System.Drawing.Size(456, 40);
            this.Shipment_T.TabIndex = 141;
            this.Shipment_T.Text = "还原资料";
            this.Shipment_T.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnRollback
            // 
            this.btnRollback.Location = new System.Drawing.Point(155, 161);
            this.btnRollback.Name = "btnRollback";
            this.btnRollback.Size = new System.Drawing.Size(75, 32);
            this.btnRollback.TabIndex = 140;
            this.btnRollback.Text = "还原资料";
            this.btnRollback.UseVisualStyleBackColor = true;
            this.btnRollback.Click += new System.EventHandler(this.btnRollback_Click);
            // 
            // cmbSmid
            // 
            this.cmbSmid.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbSmid.FormattingEnabled = true;
            this.cmbSmid.ItemHeight = 16;
            this.cmbSmid.Items.AddRange(new object[] {
            "FK1811000001",
            "FK1811000002",
            "FK1811000003",
            "FK1811000004",
            "FK1811000005",
            "FK1812000001",
            "FK1812000099",
            "FK1812000100",
            "FK1812000101",
            "FK1849000014",
            "FK1849000019",
            "FK1849000020",
            "FK1850000022"});
            this.cmbSmid.Location = new System.Drawing.Point(155, 118);
            this.cmbSmid.Margin = new System.Windows.Forms.Padding(1);
            this.cmbSmid.Name = "cmbSmid";
            this.cmbSmid.Size = new System.Drawing.Size(194, 24);
            this.cmbSmid.TabIndex = 139;
            this.cmbSmid.SelectedIndexChanged += new System.EventHandler(this.cmbSmid_SelectedIndexChanged);
            // 
            // labShipmentID
            // 
            this.labShipmentID.AutoSize = true;
            this.labShipmentID.Location = new System.Drawing.Point(74, 118);
            this.labShipmentID.Name = "labShipmentID";
            this.labShipmentID.Size = new System.Drawing.Size(59, 12);
            this.labShipmentID.TabIndex = 138;
            this.labShipmentID.Text = "集货单号:";
            // 
            // ZCshipmentid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 261);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dt_start);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.dt_end);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.TextMsg);
            this.Controls.Add(this.Shipment_T);
            this.Controls.Add(this.btnRollback);
            this.Controls.Add(this.cmbSmid);
            this.Controls.Add(this.labShipmentID);
            this.Name = "ZCshipmentid";
            this.Text = "还原ShipmentID资料";
            this.Load += new System.EventHandler(this.ZCshipmentid_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dt_start;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dt_end;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.TextBox Shipment_T;
        private System.Windows.Forms.Button btnRollback;
        private System.Windows.Forms.ComboBox cmbSmid;
        private System.Windows.Forms.Label labShipmentID;
    }
}