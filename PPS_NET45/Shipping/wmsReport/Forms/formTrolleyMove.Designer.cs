namespace wmsReport
{
    partial class formTrolleyMove
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
            this.labShow = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvCar = new System.Windows.Forms.DataGridView();
            this.TROLLEY_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TROLLEY_LINE_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.POINTNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PALLET_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CARTON_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUSTOM_SN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DELIVERY_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LINE_ITEM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOCATION_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbLocationTo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCARFrom = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCar)).BeginInit();
            this.panel3.SuspendLayout();
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
            this.TextMsg.Location = new System.Drawing.Point(0, 589);
            this.TextMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1251, 62);
            this.TextMsg.TabIndex = 73;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.labShow.Size = new System.Drawing.Size(1251, 57);
            this.labShow.TabIndex = 72;
            this.labShow.Text = "整车移转储位";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 57);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1251, 532);
            this.panel1.TabIndex = 75;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Controls.Add(this.panel3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(1251, 532);
            this.groupBox3.TabIndex = 110;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "筛选:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(4, 175);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1243, 353);
            this.panel2.TabIndex = 86;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvCar);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1243, 353);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Trolley info";
            // 
            // dgvCar
            // 
            this.dgvCar.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvCar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TROLLEY_NO,
            this.TROLLEY_LINE_NO,
            this.POINTNO,
            this.PALLET_NO,
            this.CARTON_NO,
            this.CUSTOM_SN,
            this.DELIVERY_NO,
            this.LINE_ITEM,
            this.LOCATION_NO});
            this.dgvCar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCar.Location = new System.Drawing.Point(4, 25);
            this.dgvCar.Margin = new System.Windows.Forms.Padding(4);
            this.dgvCar.Name = "dgvCar";
            this.dgvCar.RowHeadersWidth = 60;
            this.dgvCar.RowTemplate.Height = 23;
            this.dgvCar.Size = new System.Drawing.Size(1235, 324);
            this.dgvCar.TabIndex = 0;
            // 
            // TROLLEY_NO
            // 
            this.TROLLEY_NO.HeaderText = "TROLLEY_NO";
            this.TROLLEY_NO.MinimumWidth = 8;
            this.TROLLEY_NO.Name = "TROLLEY_NO";
            this.TROLLEY_NO.Width = 150;
            // 
            // TROLLEY_LINE_NO
            // 
            this.TROLLEY_LINE_NO.HeaderText = "TROLLEY_LINE_NO";
            this.TROLLEY_LINE_NO.MinimumWidth = 8;
            this.TROLLEY_LINE_NO.Name = "TROLLEY_LINE_NO";
            this.TROLLEY_LINE_NO.Width = 120;
            // 
            // POINTNO
            // 
            this.POINTNO.HeaderText = "POINTNO";
            this.POINTNO.MinimumWidth = 8;
            this.POINTNO.Name = "POINTNO";
            this.POINTNO.Width = 60;
            // 
            // PALLET_NO
            // 
            this.PALLET_NO.HeaderText = "PALLET_NO";
            this.PALLET_NO.MinimumWidth = 8;
            this.PALLET_NO.Name = "PALLET_NO";
            this.PALLET_NO.Width = 120;
            // 
            // CARTON_NO
            // 
            this.CARTON_NO.HeaderText = "CARTON_NO";
            this.CARTON_NO.MinimumWidth = 8;
            this.CARTON_NO.Name = "CARTON_NO";
            this.CARTON_NO.Width = 200;
            // 
            // CUSTOM_SN
            // 
            this.CUSTOM_SN.HeaderText = "CUSTOM_SN";
            this.CUSTOM_SN.MinimumWidth = 8;
            this.CUSTOM_SN.Name = "CUSTOM_SN";
            this.CUSTOM_SN.Width = 150;
            // 
            // DELIVERY_NO
            // 
            this.DELIVERY_NO.HeaderText = "DELIVERY_NO";
            this.DELIVERY_NO.MinimumWidth = 8;
            this.DELIVERY_NO.Name = "DELIVERY_NO";
            this.DELIVERY_NO.Width = 150;
            // 
            // LINE_ITEM
            // 
            this.LINE_ITEM.HeaderText = "LINE_ITEM";
            this.LINE_ITEM.MinimumWidth = 8;
            this.LINE_ITEM.Name = "LINE_ITEM";
            this.LINE_ITEM.Width = 150;
            // 
            // LOCATION_NO
            // 
            this.LOCATION_NO.HeaderText = "LOCATION_NO";
            this.LOCATION_NO.MinimumWidth = 8;
            this.LOCATION_NO.Name = "LOCATION_NO";
            this.LOCATION_NO.Width = 200;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cmbLocationTo);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.cmbCARFrom);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.btnStart);
            this.panel3.Controls.Add(this.btnEnd);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(4, 25);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1243, 150);
            this.panel3.TabIndex = 85;
            // 
            // cmbLocationTo
            // 
            this.cmbLocationTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbLocationTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbLocationTo.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbLocationTo.FormattingEnabled = true;
            this.cmbLocationTo.Location = new System.Drawing.Point(642, 50);
            this.cmbLocationTo.Name = "cmbLocationTo";
            this.cmbLocationTo.Size = new System.Drawing.Size(385, 37);
            this.cmbLocationTo.TabIndex = 114;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(639, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 22);
            this.label1.TabIndex = 113;
            this.label1.Text = "目的储位:";
            // 
            // cmbCARFrom
            // 
            this.cmbCARFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCARFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCARFrom.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCARFrom.FormattingEnabled = true;
            this.cmbCARFrom.Location = new System.Drawing.Point(32, 50);
            this.cmbCARFrom.Name = "cmbCARFrom";
            this.cmbCARFrom.Size = new System.Drawing.Size(385, 37);
            this.cmbCARFrom.TabIndex = 112;
            this.cmbCARFrom.SelectedIndexChanged += new System.EventHandler(this.cmbCARFrom_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(29, 22);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 22);
            this.label3.TabIndex = 111;
            this.label3.Text = "车号:";
            // 
            // btnStart
            // 
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnStart.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(468, 94);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(112, 48);
            this.btnStart.TabIndex = 105;
            this.btnStart.Text = "开始转移";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Enabled = false;
            this.btnEnd.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnd.Location = new System.Drawing.Point(1113, 94);
            this.btnEnd.Margin = new System.Windows.Forms.Padding(4);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(112, 48);
            this.btnEnd.TabIndex = 106;
            this.btnEnd.Text = "结束作业";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Visible = false;
            // 
            // formTrolleyMove
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1251, 651);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TextMsg);
            this.Controls.Add(this.labShow);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "formTrolleyMove";
            this.Text = "formTrolleyMove";
            this.Load += new System.EventHandler(this.formTrolleyMove_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCar)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvCar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cmbLocationTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCARFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn TROLLEY_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TROLLEY_LINE_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn POINTNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PALLET_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CARTON_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUSTOM_SN;
        private System.Windows.Forms.DataGridViewTextBoxColumn DELIVERY_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn LINE_ITEM;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOCATION_NO;
    }
}