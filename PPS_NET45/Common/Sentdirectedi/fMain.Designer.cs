namespace Sentdirectedi
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelDNDetail = new System.Windows.Forms.Panel();
            this.btnUploadTT = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btsentedi = new System.Windows.Forms.Button();
            this.tbshipmentid = new System.Windows.Forms.TextBox();
            this.btselect = new System.Windows.Forms.Button();
            this.LabDN = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TextMsg = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panelDNDetail.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelDNDetail
            // 
            this.panelDNDetail.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panelDNDetail.Controls.Add(this.btnUploadTT);
            this.panelDNDetail.Controls.Add(this.label1);
            this.panelDNDetail.Controls.Add(this.btsentedi);
            this.panelDNDetail.Controls.Add(this.tbshipmentid);
            this.panelDNDetail.Controls.Add(this.btselect);
            this.panelDNDetail.Controls.Add(this.LabDN);
            this.panelDNDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDNDetail.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.panelDNDetail.Location = new System.Drawing.Point(3, 17);
            this.panelDNDetail.Name = "panelDNDetail";
            this.panelDNDetail.Size = new System.Drawing.Size(949, 106);
            this.panelDNDetail.TabIndex = 28;
            // 
            // btnUploadTT
            // 
            this.btnUploadTT.Location = new System.Drawing.Point(214, 61);
            this.btnUploadTT.Name = "btnUploadTT";
            this.btnUploadTT.Size = new System.Drawing.Size(75, 33);
            this.btnUploadTT.TabIndex = 32;
            this.btnUploadTT.Text = "上传TT";
            this.btnUploadTT.UseVisualStyleBackColor = true;
            this.btnUploadTT.Click += new System.EventHandler(this.btnUploadTT_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(637, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 35);
            this.label1.TabIndex = 31;
            this.label1.Text = "DIRECT SHIP";
            // 
            // btsentedi
            // 
            this.btsentedi.Location = new System.Drawing.Point(333, 61);
            this.btsentedi.Name = "btsentedi";
            this.btsentedi.Size = new System.Drawing.Size(75, 33);
            this.btsentedi.TabIndex = 30;
            this.btsentedi.Text = "上传EDI";
            this.btsentedi.UseVisualStyleBackColor = true;
            this.btsentedi.Click += new System.EventHandler(this.btsentedi_Click);
            // 
            // tbshipmentid
            // 
            this.tbshipmentid.Font = new System.Drawing.Font("新宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbshipmentid.Location = new System.Drawing.Point(98, 14);
            this.tbshipmentid.Name = "tbshipmentid";
            this.tbshipmentid.Size = new System.Drawing.Size(310, 29);
            this.tbshipmentid.TabIndex = 28;
            // 
            // btselect
            // 
            this.btselect.Location = new System.Drawing.Point(98, 61);
            this.btselect.Name = "btselect";
            this.btselect.Size = new System.Drawing.Size(75, 33);
            this.btselect.TabIndex = 23;
            this.btselect.Text = "查询";
            this.btselect.UseVisualStyleBackColor = true;
            this.btselect.Click += new System.EventHandler(this.button1_Click);
            // 
            // LabDN
            // 
            this.LabDN.AutoSize = true;
            this.LabDN.Font = new System.Drawing.Font("新宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabDN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabDN.Location = new System.Drawing.Point(3, 19);
            this.LabDN.Name = "LabDN";
            this.LabDN.Size = new System.Drawing.Size(89, 19);
            this.LabDN.TabIndex = 0;
            this.LabDN.Text = "出货单号";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TextMsg);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.panelDNDetail);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(955, 610);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            // 
            // TextMsg
            // 
            this.TextMsg.AutoEllipsis = true;
            this.TextMsg.BackColor = System.Drawing.Color.White;
            this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Top;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(3, 478);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(949, 126);
            this.TextMsg.TabIndex = 60;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(3, 123);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(949, 355);
            this.dataGridView1.TabIndex = 30;
            this.dataGridView1.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridView1_RowStateChanged);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 546);
            this.Controls.Add(this.groupBox1);
            this.Name = "fMain";
            this.Text = "fMain";
            this.Load += new System.EventHandler(this.fMain_Load);
            this.panelDNDetail.ResumeLayout(false);
            this.panelDNDetail.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDNDetail;
        private System.Windows.Forms.Label LabDN;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.Button btselect;
        private System.Windows.Forms.Button btsentedi;
        private System.Windows.Forms.TextBox tbshipmentid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUploadTT;
    }
}