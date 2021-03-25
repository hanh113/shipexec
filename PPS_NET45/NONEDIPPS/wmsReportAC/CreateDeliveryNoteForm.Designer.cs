namespace wmsReportAC
{
    partial class CreateDeliveryNoteForm
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
            this.createDeliveryNote_BTN = new System.Windows.Forms.Button();
            this.closeTime_DTP = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.Message_LB = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.createDeliveryNote_BTN);
            this.panel1.Controls.Add(this.closeTime_DTP);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Message_LB);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(718, 395);
            this.panel1.TabIndex = 0;
            // 
            // createDeliveryNote_BTN
            // 
            this.createDeliveryNote_BTN.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.createDeliveryNote_BTN.Location = new System.Drawing.Point(453, 101);
            this.createDeliveryNote_BTN.Name = "createDeliveryNote_BTN";
            this.createDeliveryNote_BTN.Size = new System.Drawing.Size(95, 36);
            this.createDeliveryNote_BTN.TabIndex = 68;
            this.createDeliveryNote_BTN.Text = "生成";
            this.createDeliveryNote_BTN.UseVisualStyleBackColor = true;
            this.createDeliveryNote_BTN.Click += new System.EventHandler(this.createDeliveryNote_BTN_Click);
            // 
            // closeTime_DTP
            // 
            this.closeTime_DTP.CalendarFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.closeTime_DTP.Location = new System.Drawing.Point(196, 106);
            this.closeTime_DTP.Name = "closeTime_DTP";
            this.closeTime_DTP.Size = new System.Drawing.Size(200, 25);
            this.closeTime_DTP.TabIndex = 67;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(67, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 25);
            this.label1.TabIndex = 66;
            this.label1.Text = "生成日期：";
            // 
            // Message_LB
            // 
            this.Message_LB.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Message_LB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Message_LB.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Message_LB.Location = new System.Drawing.Point(0, 363);
            this.Message_LB.Name = "Message_LB";
            this.Message_LB.Size = new System.Drawing.Size(718, 32);
            this.Message_LB.TabIndex = 65;
            this.Message_LB.Text = "Message";
            this.Message_LB.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // CreateDeliveryNoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 395);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "CreateDeliveryNoteForm";
            this.Text = "CreateDeliveryNote";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Message_LB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker closeTime_DTP;
        private System.Windows.Forms.Button createDeliveryNote_BTN;
    }
}