namespace wmsReport
{
    partial class FrmEartipsStockIn
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labPalletNo = new System.Windows.Forms.Label();
            this.txtCartonNo = new System.Windows.Forms.TextBox();
            this.labCarton = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.labShow = new System.Windows.Forms.TextBox();
            this.txtMsg = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.labPalletNo);
            this.groupBox1.Controls.Add(this.txtCartonNo);
            this.groupBox1.Controls.Add(this.labCarton);
            this.groupBox1.Controls.Add(this.txtLocation);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 41);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(1035, 101);
            this.groupBox1.TabIndex = 79;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "信息扫入";
            // 
            // labPalletNo
            // 
            this.labPalletNo.AutoSize = true;
            this.labPalletNo.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.labPalletNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labPalletNo.Location = new System.Drawing.Point(533, 46);
            this.labPalletNo.Name = "labPalletNo";
            this.labPalletNo.Size = new System.Drawing.Size(56, 14);
            this.labPalletNo.TabIndex = 124;
            this.labPalletNo.Text = "外箱号:";
            // 
            // txtCartonNo
            // 
            this.txtCartonNo.BackColor = System.Drawing.Color.White;
            this.txtCartonNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCartonNo.Enabled = false;
            this.txtCartonNo.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCartonNo.ForeColor = System.Drawing.Color.Blue;
            this.txtCartonNo.Location = new System.Drawing.Point(592, 37);
            this.txtCartonNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCartonNo.Name = "txtCartonNo";
            this.txtCartonNo.Size = new System.Drawing.Size(256, 32);
            this.txtCartonNo.TabIndex = 125;
            this.txtCartonNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCartonNo_KeyDown);
            // 
            // labCarton
            // 
            this.labCarton.AutoSize = true;
            this.labCarton.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.labCarton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labCarton.Location = new System.Drawing.Point(57, 46);
            this.labCarton.Name = "labCarton";
            this.labCarton.Size = new System.Drawing.Size(56, 14);
            this.labCarton.TabIndex = 122;
            this.labCarton.Text = "储位号:";
            // 
            // txtLocation
            // 
            this.txtLocation.BackColor = System.Drawing.Color.White;
            this.txtLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocation.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLocation.ForeColor = System.Drawing.Color.Blue;
            this.txtLocation.Location = new System.Drawing.Point(116, 37);
            this.txtLocation.Margin = new System.Windows.Forms.Padding(4);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(256, 32);
            this.txtLocation.TabIndex = 123;
            this.txtLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocation_KeyDown);
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Enabled = false;
            this.labShow.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.Color.White;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Name = "labShow";
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(1035, 41);
            this.labShow.TabIndex = 77;
            this.labShow.Text = "Eartips入库";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.txtMsg.TabIndex = 78;
            this.txtMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(932, 37);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 32);
            this.btnReset.TabIndex = 126;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // FrmEartipsStockIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 431);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labShow);
            this.Controls.Add(this.txtMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEartipsStockIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Eartips入库";
            this.Load += new System.EventHandler(this.FrmEartipsStockIn_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labPalletNo;
        private System.Windows.Forms.TextBox txtCartonNo;
        private System.Windows.Forms.Label labCarton;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.Label txtMsg;
        private System.Windows.Forms.Button btnReset;
    }
}