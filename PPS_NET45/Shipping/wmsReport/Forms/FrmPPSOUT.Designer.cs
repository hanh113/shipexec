namespace wmsReport
{
    partial class FrmPPSOUT
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
            this.labShow = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labCarton = new System.Windows.Forms.Label();
            this.txtCarton = new System.Windows.Forms.TextBox();
            this.txtMsg = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.labOutNum = new System.Windows.Forms.Label();
            this.labOutDesc = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Enabled = false;
            this.labShow.Font = new System.Drawing.Font("SimSun", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.Color.White;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Name = "labShow";
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(1035, 41);
            this.labShow.TabIndex = 66;
            this.labShow.Text = "PPS按箱号出库";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.labOutNum);
            this.groupBox1.Controls.Add(this.labOutDesc);
            this.groupBox1.Controls.Add(this.labCarton);
            this.groupBox1.Controls.Add(this.txtCarton);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 41);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(1035, 101);
            this.groupBox1.TabIndex = 70;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "信息扫入";
            // 
            // labCarton
            // 
            this.labCarton.AutoSize = true;
            this.labCarton.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.labCarton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labCarton.Location = new System.Drawing.Point(57, 46);
            this.labCarton.Name = "labCarton";
            this.labCarton.Size = new System.Drawing.Size(56, 14);
            this.labCarton.TabIndex = 122;
            this.labCarton.Text = "外箱号:";
            // 
            // txtCarton
            // 
            this.txtCarton.BackColor = System.Drawing.Color.White;
            this.txtCarton.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCarton.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCarton.ForeColor = System.Drawing.Color.Blue;
            this.txtCarton.Location = new System.Drawing.Point(116, 37);
            this.txtCarton.Margin = new System.Windows.Forms.Padding(4);
            this.txtCarton.Name = "txtCarton";
            this.txtCarton.Size = new System.Drawing.Size(256, 32);
            this.txtCarton.TabIndex = 123;
            this.txtCarton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCarton_KeyDown);
            // 
            // txtMsg
            // 
            this.txtMsg.AutoEllipsis = true;
            this.txtMsg.BackColor = System.Drawing.Color.Blue;
            this.txtMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.txtMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtMsg.Location = new System.Drawing.Point(0, 381);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(1035, 50);
            this.txtMsg.TabIndex = 71;
            this.txtMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(690, 37);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 32);
            this.btnReset.TabIndex = 129;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // labOutNum
            // 
            this.labOutNum.AutoSize = true;
            this.labOutNum.Font = new System.Drawing.Font("NSimSun", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labOutNum.ForeColor = System.Drawing.SystemColors.MenuText;
            this.labOutNum.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labOutNum.Location = new System.Drawing.Point(589, 43);
            this.labOutNum.Name = "labOutNum";
            this.labOutNum.Size = new System.Drawing.Size(20, 19);
            this.labOutNum.TabIndex = 128;
            this.labOutNum.Text = "0";
            // 
            // labOutDesc
            // 
            this.labOutDesc.AutoSize = true;
            this.labOutDesc.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.labOutDesc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labOutDesc.Location = new System.Drawing.Point(471, 45);
            this.labOutDesc.Name = "labOutDesc";
            this.labOutDesc.Size = new System.Drawing.Size(112, 14);
            this.labOutDesc.TabIndex = 127;
            this.labOutDesc.Text = "出库总箱数累积:";
            // 
            // FrmPPSOUT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 431);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labShow);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPPSOUT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PPS按箱号出库";
            this.Load += new System.EventHandler(this.FrmPPSOUT_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labCarton;
        private System.Windows.Forms.TextBox txtCarton;
        private System.Windows.Forms.Label txtMsg;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label labOutNum;
        private System.Windows.Forms.Label labOutDesc;
    }
}