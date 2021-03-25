namespace Check
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
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
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
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(718, 432);
            this.panel1.TabIndex = 0;
            // 
            // cartonNo_TB
            // 
            this.cartonNo_TB.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cartonNo_TB.Location = new System.Drawing.Point(191, 98);
            this.cartonNo_TB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cartonNo_TB.Name = "cartonNo_TB";
            this.cartonNo_TB.Size = new System.Drawing.Size(291, 35);
            this.cartonNo_TB.TabIndex = 23;
            this.cartonNo_TB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cartonNo_TB_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 24);
            this.label1.TabIndex = 22;
            this.label1.Text = "箱号:";
            // 
            // reprint_BTN
            // 
            this.reprint_BTN.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.reprint_BTN.Location = new System.Drawing.Point(538, 144);
            this.reprint_BTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.reprint_BTN.Name = "reprint_BTN";
            this.reprint_BTN.Size = new System.Drawing.Size(122, 59);
            this.reprint_BTN.TabIndex = 21;
            this.reprint_BTN.Text = "补列印";
            this.reprint_BTN.UseVisualStyleBackColor = true;
            this.reprint_BTN.Click += new System.EventHandler(this.reprint_BTN_Click);
            // 
            // pickPalletNo_TB
            // 
            this.pickPalletNo_TB.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pickPalletNo_TB.Location = new System.Drawing.Point(191, 227);
            this.pickPalletNo_TB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pickPalletNo_TB.Name = "pickPalletNo_TB";
            this.pickPalletNo_TB.ReadOnly = true;
            this.pickPalletNo_TB.Size = new System.Drawing.Size(291, 35);
            this.pickPalletNo_TB.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(12, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 24);
            this.label2.TabIndex = 19;
            this.label2.Text = "Pick栈板号:";
            // 
            // shipmentId_TB
            // 
            this.shipmentId_TB.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.shipmentId_TB.Location = new System.Drawing.Point(191, 163);
            this.shipmentId_TB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.shipmentId_TB.Name = "shipmentId_TB";
            this.shipmentId_TB.ReadOnly = true;
            this.shipmentId_TB.Size = new System.Drawing.Size(291, 35);
            this.shipmentId_TB.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(12, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 24);
            this.label3.TabIndex = 17;
            this.label3.Text = "集货单号:";
            // 
            // Message_LB
            // 
            this.Message_LB.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Message_LB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Message_LB.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Message_LB.Location = new System.Drawing.Point(0, 376);
            this.Message_LB.Name = "Message_LB";
            this.Message_LB.Size = new System.Drawing.Size(718, 56);
            this.Message_LB.TabIndex = 16;
            this.Message_LB.Text = "Message";
            this.Message_LB.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Reprint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 432);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
    }
}