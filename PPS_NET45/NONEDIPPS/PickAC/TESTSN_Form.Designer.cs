namespace PickListAC
{
    partial class TESTSN_Form
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
            this.cmbPartno = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnS = new System.Windows.Forms.Button();
            this.btnP = new System.Windows.Forms.Button();
            this.dgvPC = new System.Windows.Forms.DataGridView();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mudList = new System.Windows.Forms.NumericUpDown();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.radSingle = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mudList)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbPartno
            // 
            this.cmbPartno.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPartno.FormattingEnabled = true;
            this.cmbPartno.Items.AddRange(new object[] {
            "L1SWC013-CS-H-AME*MMEF2AM/A",
            "L1SWC013-CS-H-BES*MMEF2BE/A",
            "L1SWC013-CS-H-CAN*MMEF2C/A",
            "L1SWC013-CS-H-CHN*MMEF2CH/A",
            "L1SWC013-CS-H-HIN*MMEF2HN/A",
            "L1SWC013-CS-H-IND*MMEF2ID/A",
            "L1SWC013-CS-H-JPN*MMEF2J/A",
            "L1SWC013-CS-H-KOR*MMEF2KH/A",
            "L1SWC013-CS-H-TUR*MMEF2TU/A",
            "L1SWC013-CS-H-TWN*MMEF2TA/A",
            "L1SWC013-CS-H-ZEE*MMEF2ZE/A",
            "L1SWC013-CS-H-ZML*MMEF2ZM/A",
            "L1SWC013-CS-H-ITS*MMEF2ZA/A",
            "L1SEB001-CS-H-AME*MMEF2AM/A",
            "L1SEB001-CS-H-BES*MMEF2BE/A",
            "L1SEB001-CS-H-CAN*MMEF2C/A",
            "L1SEB001-CS-H-CHN*MMEF2CH/A",
            "L1SEB001-CS-H-HIN*MMEF2HN/A",
            "L1SEB001-CS-H-IND*MMEF2ID/A",
            "L1SEB001-CS-H-JPN*MMEF2J/A",
            "L1SEB001-CS-H-KOR*MMEF2KH/A",
            "L1SEB001-CS-H-TUR*MMEF2TU/A",
            "L1SEB001-CS-H-TWN*MMEF2TA/A",
            "L1SEB001-CS-H-ZEE*MMEF2ZE/A",
            "L1SEB001-CS-H-ZML*MMEF2ZM/A",
            "L1SEB001-CS-H-ITS*MMEF2ZA/A"});
            this.cmbPartno.Location = new System.Drawing.Point(85, 6);
            this.cmbPartno.Name = "cmbPartno";
            this.cmbPartno.Size = new System.Drawing.Size(265, 23);
            this.cmbPartno.TabIndex = 49;
            this.cmbPartno.Text = "L1SEB001-CS-H-ITS*MMEF2ZA/A";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 29);
            this.label1.TabIndex = 48;
            this.label1.Text = "料号：";
            // 
            // btnS
            // 
            this.btnS.Location = new System.Drawing.Point(208, 109);
            this.btnS.Name = "btnS";
            this.btnS.Size = new System.Drawing.Size(75, 23);
            this.btnS.TabIndex = 50;
            this.btnS.Text = "查询";
            this.btnS.UseVisualStyleBackColor = true;
            this.btnS.Click += new System.EventHandler(this.btnS_Click);
            // 
            // btnP
            // 
            this.btnP.Location = new System.Drawing.Point(359, 109);
            this.btnP.Name = "btnP";
            this.btnP.Size = new System.Drawing.Size(75, 23);
            this.btnP.TabIndex = 51;
            this.btnP.Text = "打印";
            this.btnP.UseVisualStyleBackColor = true;
            this.btnP.Click += new System.EventHandler(this.btnP_Click);
            // 
            // dgvPC
            // 
            this.dgvPC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPC.Location = new System.Drawing.Point(11, 152);
            this.dgvPC.Name = "dgvPC";
            this.dgvPC.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvPC.RowTemplate.Height = 23;
            this.dgvPC.Size = new System.Drawing.Size(423, 226);
            this.dgvPC.TabIndex = 52;
            this.dgvPC.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvPC_RowStateChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 38);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(107, 16);
            this.radioButton1.TabIndex = 53;
            this.radioButton1.Text = "满栈板序号打印";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(125, 38);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(71, 16);
            this.radioButton2.TabIndex = 54;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "箱号打印";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(125, 109);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1050,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(54, 21);
            this.numericUpDown1.TabIndex = 55;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
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
            this.label4.Location = new System.Drawing.Point(10, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 29);
            this.label4.TabIndex = 56;
            this.label4.Text = "箱 数:";
            // 
            // txtPage
            // 
            this.txtPage.Location = new System.Drawing.Point(384, 77);
            this.txtPage.Name = "txtPage";
            this.txtPage.Size = new System.Drawing.Size(63, 21);
            this.txtPage.TabIndex = 57;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(325, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 58;
            this.label2.Text = "第几张";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(296, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 60;
            this.label3.Text = "1行或者3行";
            // 
            // mudList
            // 
            this.mudList.Location = new System.Drawing.Point(384, 43);
            this.mudList.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.mudList.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.mudList.Name = "mudList";
            this.mudList.Size = new System.Drawing.Size(54, 21);
            this.mudList.TabIndex = 61;
            this.mudList.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtLocation
            // 
            this.txtLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocation.Location = new System.Drawing.Point(72, 77);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(183, 21);
            this.txtLocation.TabIndex = 62;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(13, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 16);
            this.label5.TabIndex = 63;
            this.label5.Text = "库位：";
            // 
            // radSingle
            // 
            this.radSingle.AutoSize = true;
            this.radSingle.Location = new System.Drawing.Point(208, 38);
            this.radSingle.Name = "radSingle";
            this.radSingle.Size = new System.Drawing.Size(71, 16);
            this.radSingle.TabIndex = 64;
            this.radSingle.Text = "单包打印";
            this.radSingle.UseVisualStyleBackColor = true;
            this.radSingle.CheckedChanged += new System.EventHandler(this.radSingle_CheckedChanged);
            // 
            // TESTSN_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 390);
            this.Controls.Add(this.radSingle);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.mudList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPage);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.dgvPC);
            this.Controls.Add(this.btnP);
            this.Controls.Add(this.btnS);
            this.Controls.Add(this.cmbPartno);
            this.Controls.Add(this.label1);
            this.Name = "TESTSN_Form";
            this.Text = "TESTSN_Form";
            this.Load += new System.EventHandler(this.TESTSN_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mudList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPartno;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnS;
        private System.Windows.Forms.Button btnP;
        private System.Windows.Forms.DataGridView dgvPC;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown mudList;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radSingle;
    }
}