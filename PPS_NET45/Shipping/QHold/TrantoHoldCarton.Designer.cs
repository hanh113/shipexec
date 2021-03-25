namespace QHold
{
    partial class TrantoHoldCarton
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
            this.txtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudCSNcount = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudCSNcount)).BeginInit();
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
            this.TextMsg.Location = new System.Drawing.Point(0, 382);
            this.TextMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(660, 60);
            this.TextMsg.TabIndex = 71;
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
            this.Shipment_T.Multiline = true;
            this.Shipment_T.Name = "Shipment_T";
            this.Shipment_T.ReadOnly = true;
            this.Shipment_T.Size = new System.Drawing.Size(660, 60);
            this.Shipment_T.TabIndex = 70;
            this.Shipment_T.Text = "Hold  Change";
            this.Shipment_T.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBox
            // 
            this.txtBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox.Location = new System.Drawing.Point(144, 220);
            this.txtBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBox.Name = "txtBox";
            this.txtBox.Size = new System.Drawing.Size(316, 35);
            this.txtBox.TabIndex = 69;
            this.txtBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(140, 176);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(343, 24);
            this.label1.TabIndex = 68;
            this.label1.Text = "CARTONNO转为Hold和解Hold：";
            // 
            // nudCSNcount
            // 
            this.nudCSNcount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudCSNcount.Location = new System.Drawing.Point(392, 92);
            this.nudCSNcount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudCSNcount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudCSNcount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCSNcount.Name = "nudCSNcount";
            this.nudCSNcount.Size = new System.Drawing.Size(81, 35);
            this.nudCSNcount.TabIndex = 72;
            this.nudCSNcount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(99, 87);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(197, 40);
            this.label4.TabIndex = 73;
            this.label4.Text = "转换数量：";
            // 
            // TrantoHoldCarton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 442);
            this.Controls.Add(this.nudCSNcount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TextMsg);
            this.Controls.Add(this.Shipment_T);
            this.Controls.Add(this.txtBox);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TrantoHoldCarton";
            this.Text = "TrantoHoldCarton";
            ((System.ComponentModel.ISupportInitialize)(this.nudCSNcount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.TextBox Shipment_T;
        private System.Windows.Forms.TextBox txtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudCSNcount;
        private System.Windows.Forms.Label label4;
    }
}