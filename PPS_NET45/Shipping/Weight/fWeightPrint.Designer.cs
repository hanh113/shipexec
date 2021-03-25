namespace Weight
{
    partial class fWeightPrint
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
            this.txtFindNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStandard = new System.Windows.Forms.TextBox();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.btPrint = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDeviation = new System.Windows.Forms.TextBox();
            this.TextMsg = new System.Windows.Forms.Label();
            this.cobPage = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labShow = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFindNo
            // 
            this.txtFindNo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtFindNo.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.txtFindNo.Location = new System.Drawing.Point(268, 114);
            this.txtFindNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFindNo.Name = "txtFindNo";
            this.txtFindNo.Size = new System.Drawing.Size(288, 31);
            this.txtFindNo.TabIndex = 1;
            this.txtFindNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFindNo_KeyDown);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(35, 241);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(225, 30);
            this.label2.TabIndex = 25;
            this.label2.Text = "标准重量:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtStandard
            // 
            this.txtStandard.BackColor = System.Drawing.SystemColors.Control;
            this.txtStandard.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.txtStandard.Location = new System.Drawing.Point(268, 240);
            this.txtStandard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtStandard.Name = "txtStandard";
            this.txtStandard.ReadOnly = true;
            this.txtStandard.Size = new System.Drawing.Size(288, 31);
            this.txtStandard.TabIndex = 26;
            // 
            // txtWeight
            // 
            this.txtWeight.BackColor = System.Drawing.SystemColors.Control;
            this.txtWeight.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.txtWeight.Location = new System.Drawing.Point(268, 177);
            this.txtWeight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.ReadOnly = true;
            this.txtWeight.Size = new System.Drawing.Size(288, 31);
            this.txtWeight.TabIndex = 26;
            // 
            // btPrint
            // 
            this.btPrint.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btPrint.Location = new System.Drawing.Point(268, 438);
            this.btPrint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(182, 58);
            this.btPrint.TabIndex = 39;
            this.btPrint.Text = "打印[&P]";
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(35, 300);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(225, 30);
            this.label5.TabIndex = 25;
            this.label5.Text = "偏差(%):";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDeviation
            // 
            this.txtDeviation.BackColor = System.Drawing.SystemColors.Control;
            this.txtDeviation.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.txtDeviation.Location = new System.Drawing.Point(268, 303);
            this.txtDeviation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDeviation.Name = "txtDeviation";
            this.txtDeviation.ReadOnly = true;
            this.txtDeviation.Size = new System.Drawing.Size(288, 31);
            this.txtDeviation.TabIndex = 26;
            // 
            // TextMsg
            // 
            this.TextMsg.AutoEllipsis = true;
            this.TextMsg.BackColor = System.Drawing.Color.Blue;
            this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 590);
            this.TextMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(750, 60);
            this.TextMsg.TabIndex = 62;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cobPage
            // 
            this.cobPage.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cobPage.FormattingEnabled = true;
            this.cobPage.ItemHeight = 24;
            this.cobPage.Items.AddRange(new object[] {
            "ALL",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cobPage.Location = new System.Drawing.Point(268, 366);
            this.cobPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cobPage.Name = "cobPage";
            this.cobPage.Size = new System.Drawing.Size(180, 32);
            this.cobPage.TabIndex = 66;
            this.cobPage.Text = "ALL";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(35, 374);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 24);
            this.label6.TabIndex = 67;
            this.label6.Text = "请选择页号:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labShow);
            this.panel1.Controls.Add(this.txtFindNo);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cobPage);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtStandard);
            this.panel1.Controls.Add(this.btPrint);
            this.panel1.Controls.Add(this.txtWeight);
            this.panel1.Controls.Add(this.txtDeviation);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(750, 590);
            this.panel1.TabIndex = 68;
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Enabled = false;
            this.labShow.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.SystemColors.Info;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labShow.Name = "labShow";
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(750, 57);
            this.labShow.TabIndex = 68;
            this.labShow.Text = "Weight PrintLabel";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(35, 178);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 30);
            this.label1.TabIndex = 25;
            this.label1.Text = "称重重量:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(35, 115);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(225, 30);
            this.label3.TabIndex = 25;
            this.label3.Text = "栈板号:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fWeightPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(750, 650);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TextMsg);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "fWeightPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reprint Pallet Label";
            this.Load += new System.EventHandler(this.fWeightPrint_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtFindNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStandard;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDeviation;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.ComboBox cobPage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
    }
}