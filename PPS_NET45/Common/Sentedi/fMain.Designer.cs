namespace Sentedi
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelDNDetail = new System.Windows.Forms.Panel();
            this.btsentedi = new System.Windows.Forms.Button();
            this.btupdate = new System.Windows.Forms.Button();
            this.tbdn = new System.Windows.Forms.TextBox();
            this.btselect = new System.Windows.Forms.Button();
            this.LabDN = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TextMsg = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnUploadTT = new System.Windows.Forms.Button();
            this.lboperation = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbprofit = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panelDNDetail.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelDNDetail
            // 
            this.panelDNDetail.BackColor = System.Drawing.Color.LightSeaGreen;
            this.panelDNDetail.Controls.Add(this.lboperation);
            this.panelDNDetail.Controls.Add(this.label14);
            this.panelDNDetail.Controls.Add(this.lbprofit);
            this.panelDNDetail.Controls.Add(this.label12);
            this.panelDNDetail.Controls.Add(this.btnUploadTT);
            this.panelDNDetail.Controls.Add(this.btsentedi);
            this.panelDNDetail.Controls.Add(this.btupdate);
            this.panelDNDetail.Controls.Add(this.tbdn);
            this.panelDNDetail.Controls.Add(this.btselect);
            this.panelDNDetail.Controls.Add(this.LabDN);
            this.panelDNDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDNDetail.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.panelDNDetail.Location = new System.Drawing.Point(3, 17);
            this.panelDNDetail.Name = "panelDNDetail";
            this.panelDNDetail.Size = new System.Drawing.Size(949, 106);
            this.panelDNDetail.TabIndex = 28;
            // 
            // btsentedi
            // 
            this.btsentedi.Location = new System.Drawing.Point(554, 61);
            this.btsentedi.Name = "btsentedi";
            this.btsentedi.Size = new System.Drawing.Size(75, 33);
            this.btsentedi.TabIndex = 30;
            this.btsentedi.Text = "上传EDI";
            this.btsentedi.UseVisualStyleBackColor = true;
            this.btsentedi.Click += new System.EventHandler(this.btsentedi_Click);
            // 
            // btupdate
            // 
            this.btupdate.Location = new System.Drawing.Point(237, 61);
            this.btupdate.Name = "btupdate";
            this.btupdate.Size = new System.Drawing.Size(75, 33);
            this.btupdate.TabIndex = 29;
            this.btupdate.Text = "保存";
            this.btupdate.UseVisualStyleBackColor = true;
            this.btupdate.Click += new System.EventHandler(this.btupdate_Click);
            // 
            // tbdn
            // 
            this.tbdn.Font = new System.Drawing.Font("新宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbdn.Location = new System.Drawing.Point(98, 14);
            this.tbdn.Name = "tbdn";
            this.tbdn.Size = new System.Drawing.Size(214, 29);
            this.tbdn.TabIndex = 28;
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
            this.LabDN.Location = new System.Drawing.Point(3, 17);
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle4;
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
            // btnUploadTT
            // 
            this.btnUploadTT.Location = new System.Drawing.Point(439, 61);
            this.btnUploadTT.Name = "btnUploadTT";
            this.btnUploadTT.Size = new System.Drawing.Size(75, 33);
            this.btnUploadTT.TabIndex = 31;
            this.btnUploadTT.Text = "上传TT";
            this.btnUploadTT.UseVisualStyleBackColor = true;
            this.btnUploadTT.Click += new System.EventHandler(this.btnUploadTT_Click);
            // 
            // lboperation
            // 
            this.lboperation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lboperation.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.lboperation.ForeColor = System.Drawing.Color.Maroon;
            this.lboperation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lboperation.Location = new System.Drawing.Point(720, 23);
            this.lboperation.Name = "lboperation";
            this.lboperation.Size = new System.Drawing.Size(193, 20);
            this.lboperation.TabIndex = 35;
            this.lboperation.Text = "N/A";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(642, 27);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 14);
            this.label14.TabIndex = 34;
            this.label14.Text = "Operation";
            // 
            // lbprofit
            // 
            this.lbprofit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbprofit.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.lbprofit.ForeColor = System.Drawing.Color.Maroon;
            this.lbprofit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbprofit.Location = new System.Drawing.Point(413, 23);
            this.lbprofit.Name = "lbprofit";
            this.lbprofit.Size = new System.Drawing.Size(193, 20);
            this.lbprofit.TabIndex = 33;
            this.lbprofit.Text = "N/A";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(358, 27);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 14);
            this.label12.TabIndex = 32;
            this.label12.Text = "Profit";
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
        private System.Windows.Forms.Button btupdate;
        private System.Windows.Forms.TextBox tbdn;
        private System.Windows.Forms.Button btnUploadTT;
        private System.Windows.Forms.Label lboperation;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbprofit;
        private System.Windows.Forms.Label label12;
    }
}