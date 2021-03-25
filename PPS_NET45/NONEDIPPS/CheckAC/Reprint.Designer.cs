namespace CheckAC
{
    partial class Reprint
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cartonNo_TB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.reprint_BTN = new System.Windows.Forms.Button();
            this.pickPalletNo_TB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.shipmentId_TB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Message_LB = new System.Windows.Forms.Label();
            this.cmbLabel = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbLabel);
            this.panel1.Controls.Add(this.cartonNo_TB);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.reprint_BTN);
            this.panel1.Controls.Add(this.pickPalletNo_TB);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.shipmentId_TB);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.Message_LB);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(637, 360);
            this.panel1.TabIndex = 0;
            // 
            // cartonNo_TB
            // 
            this.cartonNo_TB.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cartonNo_TB.Location = new System.Drawing.Point(171, 82);
            this.cartonNo_TB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cartonNo_TB.Name = "cartonNo_TB";
            this.cartonNo_TB.Size = new System.Drawing.Size(259, 30);
            this.cartonNo_TB.TabIndex = 23;
            this.cartonNo_TB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cartonNo_TB_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(95, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 22;
            this.label1.Text = "箱号:";
            // 
            // reprint_BTN
            // 
            this.reprint_BTN.Enabled = false;
            this.reprint_BTN.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.reprint_BTN.Location = new System.Drawing.Point(477, 120);
            this.reprint_BTN.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.reprint_BTN.Name = "reprint_BTN";
            this.reprint_BTN.Size = new System.Drawing.Size(108, 49);
            this.reprint_BTN.TabIndex = 21;
            this.reprint_BTN.Text = "补列印";
            this.reprint_BTN.UseVisualStyleBackColor = true;
            this.reprint_BTN.Click += new System.EventHandler(this.reprint_BTN_Click);
            // 
            // pickPalletNo_TB
            // 
            this.pickPalletNo_TB.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pickPalletNo_TB.Location = new System.Drawing.Point(171, 189);
            this.pickPalletNo_TB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pickPalletNo_TB.Name = "pickPalletNo_TB";
            this.pickPalletNo_TB.ReadOnly = true;
            this.pickPalletNo_TB.Size = new System.Drawing.Size(259, 30);
            this.pickPalletNo_TB.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(35, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 20);
            this.label2.TabIndex = 19;
            this.label2.Text = "Pick栈板号:";
            // 
            // shipmentId_TB
            // 
            this.shipmentId_TB.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.shipmentId_TB.Location = new System.Drawing.Point(171, 136);
            this.shipmentId_TB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.shipmentId_TB.Name = "shipmentId_TB";
            this.shipmentId_TB.ReadOnly = true;
            this.shipmentId_TB.Size = new System.Drawing.Size(259, 30);
            this.shipmentId_TB.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(55, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "集货单号:";
            // 
            // Message_LB
            // 
            this.Message_LB.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Message_LB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Message_LB.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Message_LB.Location = new System.Drawing.Point(0, 312);
            this.Message_LB.Name = "Message_LB";
            this.Message_LB.Size = new System.Drawing.Size(637, 48);
            this.Message_LB.TabIndex = 16;
            this.Message_LB.Text = "Message";
            this.Message_LB.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmbLabel
            // 
            this.cmbLabel.DisplayMember = "Pallet Address Label";
            this.cmbLabel.FormattingEnabled = true;
            this.cmbLabel.Items.AddRange(new object[] {
            "Pallet Address Label",
            "Carton Address Label"});
            this.cmbLabel.Location = new System.Drawing.Point(173, 250);
            this.cmbLabel.Name = "cmbLabel";
            this.cmbLabel.Size = new System.Drawing.Size(256, 23);
            this.cmbLabel.TabIndex = 24;
            this.cmbLabel.Text = "Pallet Address Label";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 15);
            this.label4.TabIndex = 25;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(55, 253);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 20);
            this.label5.TabIndex = 26;
            this.label5.Text = "选择标签：";
            // 
            // Reprint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 360);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Reprint";
            this.Text = "Reprint";
            this.Load += new System.EventHandler(this.Reprint_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Reprint_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Message_LB;
        private System.Windows.Forms.TextBox pickPalletNo_TB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox shipmentId_TB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button reprint_BTN;
        private System.Windows.Forms.TextBox cartonNo_TB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}