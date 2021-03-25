namespace RollbackDN
{
    partial class CancelSID
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
            this.btnRollback = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvSID = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dt_start = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dt_end = new System.Windows.Forms.DateTimePicker();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblDNmodel = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSmId = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSID)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel3.SuspendLayout();
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
            this.TextMsg.Location = new System.Drawing.Point(0, 524);
            this.TextMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(934, 60);
            this.TextMsg.TabIndex = 114;
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
            this.Shipment_T.Multiline = true;
            this.Shipment_T.Name = "Shipment_T";
            this.Shipment_T.ReadOnly = true;
            this.Shipment_T.Size = new System.Drawing.Size(934, 60);
            this.Shipment_T.TabIndex = 113;
            this.Shipment_T.Text = "取消集货单";
            this.Shipment_T.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnRollback
            // 
            this.btnRollback.Location = new System.Drawing.Point(476, 18);
            this.btnRollback.Margin = new System.Windows.Forms.Padding(4);
            this.btnRollback.Name = "btnRollback";
            this.btnRollback.Size = new System.Drawing.Size(112, 38);
            this.btnRollback.TabIndex = 112;
            this.btnRollback.Text = "取消资料";
            this.btnRollback.UseVisualStyleBackColor = true;
            this.btnRollback.Click += new System.EventHandler(this.btnRollback_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvSID);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 128);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(934, 279);
            this.groupBox2.TabIndex = 116;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "集货单 LIST:";
            // 
            // dgvSID
            // 
            this.dgvSID.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvSID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSID.Location = new System.Drawing.Point(4, 25);
            this.dgvSID.Margin = new System.Windows.Forms.Padding(4);
            this.dgvSID.Name = "dgvSID";
            this.dgvSID.RowHeadersWidth = 62;
            this.dgvSID.RowTemplate.Height = 23;
            this.dgvSID.Size = new System.Drawing.Size(926, 250);
            this.dgvSID.TabIndex = 0;
            this.dgvSID.SelectionChanged += new System.EventHandler(this.dgvSID_SelectionChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dt_start);
            this.groupBox1.Controls.Add(this.lblEnd);
            this.groupBox1.Controls.Add(this.dt_end);
            this.groupBox1.Controls.Add(this.lblStart);
            this.groupBox1.Controls.Add(this.lblDNmodel);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 60);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(934, 68);
            this.groupBox1.TabIndex = 115;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "筛选:";
            // 
            // dt_start
            // 
            this.dt_start.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_start.CustomFormat = "yyyy-MM-dd";
            this.dt_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_start.Location = new System.Drawing.Point(146, 21);
            this.dt_start.Margin = new System.Windows.Forms.Padding(4);
            this.dt_start.Name = "dt_start";
            this.dt_start.Size = new System.Drawing.Size(150, 28);
            this.dt_start.TabIndex = 132;
            this.dt_start.Value = new System.DateTime(2019, 7, 12, 0, 0, 0, 0);
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(318, 32);
            this.lblEnd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(89, 18);
            this.lblEnd.TabIndex = 131;
            this.lblEnd.Text = "结束日期:";
            // 
            // dt_end
            // 
            this.dt_end.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_end.CustomFormat = "yyyy-MM-dd";
            this.dt_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_end.Location = new System.Drawing.Point(440, 21);
            this.dt_end.Margin = new System.Windows.Forms.Padding(4);
            this.dt_end.Name = "dt_end";
            this.dt_end.Size = new System.Drawing.Size(164, 28);
            this.dt_end.TabIndex = 130;
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(27, 33);
            this.lblStart.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(89, 18);
            this.lblStart.TabIndex = 129;
            this.lblStart.Text = "开始日期:";
            // 
            // lblDNmodel
            // 
            this.lblDNmodel.AutoSize = true;
            this.lblDNmodel.Location = new System.Drawing.Point(918, 40);
            this.lblDNmodel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDNmodel.Name = "lblDNmodel";
            this.lblDNmodel.Size = new System.Drawing.Size(0, 18);
            this.lblDNmodel.TabIndex = 109;
            // 
            // btnSearch
            // 
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSearch.Location = new System.Drawing.Point(705, 21);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(112, 38);
            this.btnSearch.TabIndex = 107;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 407);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(934, 117);
            this.groupBox3.TabIndex = 117;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pack SN/Carton:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.txtSmId);
            this.panel3.Controls.Add(this.btnRollback);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(4, 25);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(926, 88);
            this.panel3.TabIndex = 85;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(26, 18);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 21);
            this.label4.TabIndex = 107;
            this.label4.Text = "集货单号:";
            // 
            // txtSmId
            // 
            this.txtSmId.BackColor = System.Drawing.SystemColors.Control;
            this.txtSmId.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSmId.ForeColor = System.Drawing.Color.Green;
            this.txtSmId.Location = new System.Drawing.Point(141, 14);
            this.txtSmId.Margin = new System.Windows.Forms.Padding(6);
            this.txtSmId.Name = "txtSmId";
            this.txtSmId.Size = new System.Drawing.Size(205, 35);
            this.txtSmId.TabIndex = 108;
            // 
            // CancelSID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 584);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TextMsg);
            this.Controls.Add(this.Shipment_T);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CancelSID";
            this.Text = "CancelSID";
            this.Load += new System.EventHandler(this.CancelSID_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSID)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.TextBox Shipment_T;
        private System.Windows.Forms.Button btnRollback;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvSID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dt_start;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dt_end;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblDNmodel;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSmId;
    }
}