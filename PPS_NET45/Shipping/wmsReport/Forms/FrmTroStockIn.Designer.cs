namespace wmsReport
{
    partial class FrmTroStockIn
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
            this.txtMsg = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labPalletNo = new System.Windows.Forms.Label();
            this.txtTroNo = new System.Windows.Forms.TextBox();
            this.labCarton = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
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
            this.labShow.TabIndex = 72;
            this.labShow.Text = "PPS精钢车入库";
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
            this.txtMsg.TabIndex = 75;
            this.txtMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox1.Controls.Add(this.labPalletNo);
            this.groupBox1.Controls.Add(this.txtTroNo);
            this.groupBox1.Controls.Add(this.labCarton);
            this.groupBox1.Controls.Add(this.txtLocation);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 41);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(1035, 101);
            this.groupBox1.TabIndex = 76;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "信息扫入";
            // 
            // labPalletNo
            // 
            this.labPalletNo.AutoSize = true;
            this.labPalletNo.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.labPalletNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labPalletNo.Location = new System.Drawing.Point(519, 46);
            this.labPalletNo.Name = "labPalletNo";
            this.labPalletNo.Size = new System.Drawing.Size(70, 14);
            this.labPalletNo.TabIndex = 124;
            this.labPalletNo.Text = "精钢车号:";
            // 
            // txtTroNo
            // 
            this.txtTroNo.BackColor = System.Drawing.Color.White;
            this.txtTroNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTroNo.Enabled = false;
            this.txtTroNo.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTroNo.ForeColor = System.Drawing.Color.Blue;
            this.txtTroNo.Location = new System.Drawing.Point(592, 37);
            this.txtTroNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtTroNo.Name = "txtTroNo";
            this.txtTroNo.Size = new System.Drawing.Size(256, 32);
            this.txtTroNo.TabIndex = 125;
            this.txtTroNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTroNo_KeyDown);
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
            // FrmTroStockIn
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
            this.Name = "FrmTroStockIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PPS精钢车入库";
            this.Load += new System.EventHandler(this.FrmTroStockIn_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.Label txtMsg;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labPalletNo;
        private System.Windows.Forms.TextBox txtTroNo;
        private System.Windows.Forms.Label labCarton;
        private System.Windows.Forms.TextBox txtLocation;
    }
}