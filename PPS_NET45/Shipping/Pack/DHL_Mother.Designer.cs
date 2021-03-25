namespace Packingparcel
{
    partial class DHL_Mother
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
            this.shipmentId_CB = new System.Windows.Forms.ComboBox();
            this.shipTime_DTP = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.createDHLMotherFile_BTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Message_LB = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.shipmentId_CB);
            this.panel1.Controls.Add(this.shipTime_DTP);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.createDHLMotherFile_BTN);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Message_LB);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(711, 436);
            this.panel1.TabIndex = 0;
            // 
            // shipmentId_CB
            // 
            this.shipmentId_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shipmentId_CB.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.shipmentId_CB.FormattingEnabled = true;
            this.shipmentId_CB.Location = new System.Drawing.Point(177, 162);
            this.shipmentId_CB.Name = "shipmentId_CB";
            this.shipmentId_CB.Size = new System.Drawing.Size(210, 28);
            this.shipmentId_CB.TabIndex = 23;
            this.shipmentId_CB.DropDown += new System.EventHandler(this.shipmentId_CB_DropDown);
            // 
            // shipTime_DTP
            // 
            this.shipTime_DTP.CalendarFont = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.shipTime_DTP.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.shipTime_DTP.Location = new System.Drawing.Point(177, 107);
            this.shipTime_DTP.Name = "shipTime_DTP";
            this.shipTime_DTP.Size = new System.Drawing.Size(210, 30);
            this.shipTime_DTP.TabIndex = 22;
            this.shipTime_DTP.ValueChanged += new System.EventHandler(this.shipTime_DTP_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(62, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "集货单号:";
            // 
            // createDHLMotherFile_BTN
            // 
            this.createDHLMotherFile_BTN.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.createDHLMotherFile_BTN.Location = new System.Drawing.Point(518, 126);
            this.createDHLMotherFile_BTN.Name = "createDHLMotherFile_BTN";
            this.createDHLMotherFile_BTN.Size = new System.Drawing.Size(125, 42);
            this.createDHLMotherFile_BTN.TabIndex = 19;
            this.createDHLMotherFile_BTN.Text = "生成";
            this.createDHLMotherFile_BTN.UseVisualStyleBackColor = true;
            this.createDHLMotherFile_BTN.Click += new System.EventHandler(this.createDHLMotherFile_BTN_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(62, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "出货日期:";
            // 
            // Message_LB
            // 
            this.Message_LB.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Message_LB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Message_LB.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Message_LB.Location = new System.Drawing.Point(0, 389);
            this.Message_LB.Name = "Message_LB";
            this.Message_LB.Size = new System.Drawing.Size(711, 47);
            this.Message_LB.TabIndex = 16;
            this.Message_LB.Text = "Message";
            this.Message_LB.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DHL_Mother
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 436);
            this.Controls.Add(this.panel1);
            this.Name = "DHL_Mother";
            this.Text = "DHL_Mother";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Message_LB;
        private System.Windows.Forms.Button createDHLMotherFile_BTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker shipTime_DTP;
        private System.Windows.Forms.ComboBox shipmentId_CB;
    }
}