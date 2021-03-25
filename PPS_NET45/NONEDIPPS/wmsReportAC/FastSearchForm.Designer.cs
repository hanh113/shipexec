namespace wmsReportAC
{
    partial class FastSearchForm
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
            this.confirm_BTN = new System.Windows.Forms.Button();
            this.org_carton_TB = new System.Windows.Forms.TextBox();
            this.orgCarton_LB = new System.Windows.Forms.Label();
            this.countNum_LB = new System.Windows.Forms.Label();
            this.cellInfo_TB = new System.Windows.Forms.TextBox();
            this.cartons_LISTBOX = new System.Windows.Forms.ListBox();
            this.cartonSearch_CB = new System.Windows.Forms.CheckBox();
            this.cellInfo_CB = new System.Windows.Forms.CheckBox();
            this.inputCarton_TB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Message_LB = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.reset_BTN = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Menu;
            this.panel1.Controls.Add(this.reset_BTN);
            this.panel1.Controls.Add(this.confirm_BTN);
            this.panel1.Controls.Add(this.org_carton_TB);
            this.panel1.Controls.Add(this.orgCarton_LB);
            this.panel1.Controls.Add(this.countNum_LB);
            this.panel1.Controls.Add(this.cellInfo_TB);
            this.panel1.Controls.Add(this.cartons_LISTBOX);
            this.panel1.Controls.Add(this.cartonSearch_CB);
            this.panel1.Controls.Add(this.cellInfo_CB);
            this.panel1.Controls.Add(this.inputCarton_TB);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Message_LB);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1181, 637);
            this.panel1.TabIndex = 0;
            // 
            // confirm_BTN
            // 
            this.confirm_BTN.Location = new System.Drawing.Point(738, 60);
            this.confirm_BTN.Name = "confirm_BTN";
            this.confirm_BTN.Size = new System.Drawing.Size(103, 29);
            this.confirm_BTN.TabIndex = 75;
            this.confirm_BTN.Text = "确认箱号";
            this.confirm_BTN.UseVisualStyleBackColor = true;
            this.confirm_BTN.Visible = false;
            this.confirm_BTN.Click += new System.EventHandler(this.confirm_BTN_Click);
            // 
            // org_carton_TB
            // 
            this.org_carton_TB.Enabled = false;
            this.org_carton_TB.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.org_carton_TB.Location = new System.Drawing.Point(286, 55);
            this.org_carton_TB.Name = "org_carton_TB";
            this.org_carton_TB.Size = new System.Drawing.Size(419, 34);
            this.org_carton_TB.TabIndex = 74;
            this.org_carton_TB.Visible = false;
            // 
            // orgCarton_LB
            // 
            this.orgCarton_LB.AutoSize = true;
            this.orgCarton_LB.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.orgCarton_LB.Location = new System.Drawing.Point(187, 60);
            this.orgCarton_LB.Name = "orgCarton_LB";
            this.orgCarton_LB.Size = new System.Drawing.Size(94, 24);
            this.orgCarton_LB.TabIndex = 73;
            this.orgCarton_LB.Text = "原箱号:";
            this.orgCarton_LB.Visible = false;
            // 
            // countNum_LB
            // 
            this.countNum_LB.AutoSize = true;
            this.countNum_LB.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.countNum_LB.Location = new System.Drawing.Point(982, 552);
            this.countNum_LB.Name = "countNum_LB";
            this.countNum_LB.Size = new System.Drawing.Size(22, 24);
            this.countNum_LB.TabIndex = 72;
            this.countNum_LB.Text = "0";
            // 
            // cellInfo_TB
            // 
            this.cellInfo_TB.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cellInfo_TB.Location = new System.Drawing.Point(4, 174);
            this.cellInfo_TB.Multiline = true;
            this.cellInfo_TB.Name = "cellInfo_TB";
            this.cellInfo_TB.ReadOnly = true;
            this.cellInfo_TB.Size = new System.Drawing.Size(468, 106);
            this.cellInfo_TB.TabIndex = 71;
            // 
            // cartons_LISTBOX
            // 
            this.cartons_LISTBOX.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cartons_LISTBOX.FormattingEnabled = true;
            this.cartons_LISTBOX.ItemHeight = 20;
            this.cartons_LISTBOX.Location = new System.Drawing.Point(478, 174);
            this.cartons_LISTBOX.Name = "cartons_LISTBOX";
            this.cartons_LISTBOX.ScrollAlwaysVisible = true;
            this.cartons_LISTBOX.Size = new System.Drawing.Size(498, 404);
            this.cartons_LISTBOX.TabIndex = 70;
            // 
            // cartonSearch_CB
            // 
            this.cartonSearch_CB.AutoSize = true;
            this.cartonSearch_CB.Location = new System.Drawing.Point(738, 137);
            this.cartonSearch_CB.Name = "cartonSearch_CB";
            this.cartonSearch_CB.Size = new System.Drawing.Size(89, 19);
            this.cartonSearch_CB.TabIndex = 69;
            this.cartonSearch_CB.Text = "箱号查找";
            this.cartonSearch_CB.UseVisualStyleBackColor = true;
            this.cartonSearch_CB.CheckedChanged += new System.EventHandler(this.cartonSearch_CB_CheckedChanged);
            // 
            // cellInfo_CB
            // 
            this.cellInfo_CB.AutoSize = true;
            this.cellInfo_CB.Checked = true;
            this.cellInfo_CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cellInfo_CB.Location = new System.Drawing.Point(738, 102);
            this.cellInfo_CB.Name = "cellInfo_CB";
            this.cellInfo_CB.Size = new System.Drawing.Size(149, 19);
            this.cellInfo_CB.TabIndex = 68;
            this.cellInfo_CB.Text = "层别查询（位置）";
            this.cellInfo_CB.UseVisualStyleBackColor = true;
            this.cellInfo_CB.CheckedChanged += new System.EventHandler(this.cellInfo_CB_CheckedChanged);
            // 
            // inputCarton_TB
            // 
            this.inputCarton_TB.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.inputCarton_TB.Location = new System.Drawing.Point(286, 102);
            this.inputCarton_TB.Name = "inputCarton_TB";
            this.inputCarton_TB.Size = new System.Drawing.Size(419, 34);
            this.inputCarton_TB.TabIndex = 67;
            this.inputCarton_TB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputCarton_TB_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(210, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 24);
            this.label1.TabIndex = 66;
            this.label1.Text = "箱号:";
            // 
            // Message_LB
            // 
            this.Message_LB.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Message_LB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Message_LB.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Message_LB.Location = new System.Drawing.Point(0, 587);
            this.Message_LB.Name = "Message_LB";
            this.Message_LB.Size = new System.Drawing.Size(1181, 50);
            this.Message_LB.TabIndex = 65;
            this.Message_LB.Text = "Message";
            this.Message_LB.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox1.Size = new System.Drawing.Size(1181, 49);
            this.textBox1.TabIndex = 64;
            this.textBox1.Text = "快速查找（箱号）";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // reset_BTN
            // 
            this.reset_BTN.Location = new System.Drawing.Point(893, 96);
            this.reset_BTN.Name = "reset_BTN";
            this.reset_BTN.Size = new System.Drawing.Size(103, 29);
            this.reset_BTN.TabIndex = 76;
            this.reset_BTN.Text = "重置";
            this.reset_BTN.UseVisualStyleBackColor = true;
            this.reset_BTN.Click += new System.EventHandler(this.reset_BTN_Click);
            // 
            // FastSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 637);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FastSearchForm";
            this.Text = "FastSearchForm";
            this.Load += new System.EventHandler(this.FastSearchForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label Message_LB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inputCarton_TB;
        private System.Windows.Forms.CheckBox cartonSearch_CB;
        private System.Windows.Forms.CheckBox cellInfo_CB;
        private System.Windows.Forms.ListBox cartons_LISTBOX;
        private System.Windows.Forms.TextBox cellInfo_TB;
        private System.Windows.Forms.Label countNum_LB;
        private System.Windows.Forms.TextBox org_carton_TB;
        private System.Windows.Forms.Label orgCarton_LB;
        private System.Windows.Forms.Button confirm_BTN;
        private System.Windows.Forms.Button reset_BTN;
    }
}