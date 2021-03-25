namespace PrintShippingPalletLabel
{
    partial class fMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
      this.txt_PalletId = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.txt_Region = new System.Windows.Forms.TextBox();
      this.txt_Start = new System.Windows.Forms.TextBox();
      this.txt_Carrier = new System.Windows.Forms.TextBox();
      this.txt_TotalCartons = new System.Windows.Forms.TextBox();
      this.txt_Hawb = new System.Windows.Forms.TextBox();
      this.txt_Invoice = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.txt_EmptyCartons = new System.Windows.Forms.TextBox();
      this.btnPrint = new System.Windows.Forms.Button();
      this.label10 = new System.Windows.Forms.Label();
      this.txt_End = new System.Windows.Forms.TextBox();
      this.tblayer = new System.Windows.Forms.TextBox();
      this.label11 = new System.Windows.Forms.Label();
      this.label12 = new System.Windows.Forms.Label();
      this.txtStandardWeitht = new System.Windows.Forms.TextBox();
      this.label13 = new System.Windows.Forms.Label();
      this.txtWeight = new System.Windows.Forms.TextBox();
      this.label14 = new System.Windows.Forms.Label();
      this.txtDeviation = new System.Windows.Forms.TextBox();
      this.label15 = new System.Windows.Forms.Label();
      this.txtUpperlimit = new System.Windows.Forms.TextBox();
      this.txtLowerlimit = new System.Windows.Forms.TextBox();
      this.label16 = new System.Windows.Forms.Label();
      this.panel3 = new System.Windows.Forms.Panel();
      this.TextMsg = new System.Windows.Forms.Label();
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnPrintPDF = new System.Windows.Forms.Button();
      this.btnPrintH = new System.Windows.Forms.Button();
      this.txtLocation = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.txtWeightRecive = new System.Windows.Forms.TextBox();
      this.label17 = new System.Windows.Forms.Label();
      this.cmbWeightType = new System.Windows.Forms.ComboBox();
      this.label18 = new System.Windows.Forms.Label();
      this.panel3.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // txt_PalletId
      // 
      this.txt_PalletId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
      this.txt_PalletId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
      this.txt_PalletId.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.txt_PalletId.ForeColor = System.Drawing.SystemColors.WindowText;
      this.txt_PalletId.ImeMode = System.Windows.Forms.ImeMode.Disable;
      this.txt_PalletId.Location = new System.Drawing.Point(195, 19);
      this.txt_PalletId.Name = "txt_PalletId";
      this.txt_PalletId.Size = new System.Drawing.Size(249, 31);
      this.txt_PalletId.TabIndex = 1;
      this.txt_PalletId.TextChanged += new System.EventHandler(this.txt_PalletId_TextChanged);
      this.txt_PalletId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_PalletId_KeyPress);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.BackColor = System.Drawing.Color.Transparent;
      this.label2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label2.Location = new System.Drawing.Point(60, 23);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(118, 24);
      this.label2.TabIndex = 2;
      this.label2.Text = "PalletNo:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.BackColor = System.Drawing.Color.Transparent;
      this.label3.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label3.Location = new System.Drawing.Point(72, 313);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(106, 24);
      this.label3.TabIndex = 3;
      this.label3.Text = "Carrier:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.BackColor = System.Drawing.Color.Transparent;
      this.label4.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label4.Location = new System.Drawing.Point(84, 197);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(94, 24);
      this.label4.TabIndex = 4;
      this.label4.Text = "Pallet:";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.BackColor = System.Drawing.Color.Transparent;
      this.label5.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label5.Location = new System.Drawing.Point(72, 81);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(106, 24);
      this.label5.TabIndex = 5;
      this.label5.Text = "Invoice:";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.BackColor = System.Drawing.Color.Transparent;
      this.label6.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label6.Location = new System.Drawing.Point(108, 139);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(70, 24);
      this.label6.TabIndex = 6;
      this.label6.Text = "HAWB:";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.BackColor = System.Drawing.Color.Transparent;
      this.label7.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label7.Location = new System.Drawing.Point(84, 255);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(94, 24);
      this.label7.TabIndex = 7;
      this.label7.Text = "Region:";
      // 
      // txt_Region
      // 
      this.txt_Region.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.txt_Region.Location = new System.Drawing.Point(195, 255);
      this.txt_Region.Name = "txt_Region";
      this.txt_Region.Size = new System.Drawing.Size(249, 31);
      this.txt_Region.TabIndex = 9;
      // 
      // txt_Start
      // 
      this.txt_Start.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.txt_Start.Location = new System.Drawing.Point(195, 197);
      this.txt_Start.Name = "txt_Start";
      this.txt_Start.Size = new System.Drawing.Size(90, 31);
      this.txt_Start.TabIndex = 10;
      // 
      // txt_Carrier
      // 
      this.txt_Carrier.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.txt_Carrier.Location = new System.Drawing.Point(195, 313);
      this.txt_Carrier.Name = "txt_Carrier";
      this.txt_Carrier.Size = new System.Drawing.Size(249, 31);
      this.txt_Carrier.TabIndex = 11;
      // 
      // txt_TotalCartons
      // 
      this.txt_TotalCartons.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.txt_TotalCartons.Location = new System.Drawing.Point(195, 371);
      this.txt_TotalCartons.Name = "txt_TotalCartons";
      this.txt_TotalCartons.Size = new System.Drawing.Size(249, 31);
      this.txt_TotalCartons.TabIndex = 12;
      // 
      // txt_Hawb
      // 
      this.txt_Hawb.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.txt_Hawb.Location = new System.Drawing.Point(195, 136);
      this.txt_Hawb.Name = "txt_Hawb";
      this.txt_Hawb.Size = new System.Drawing.Size(249, 31);
      this.txt_Hawb.TabIndex = 13;
      // 
      // txt_Invoice
      // 
      this.txt_Invoice.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.txt_Invoice.Location = new System.Drawing.Point(195, 81);
      this.txt_Invoice.Name = "txt_Invoice";
      this.txt_Invoice.Size = new System.Drawing.Size(249, 31);
      this.txt_Invoice.TabIndex = 14;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.BackColor = System.Drawing.Color.Transparent;
      this.label8.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label8.Location = new System.Drawing.Point(12, 371);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(166, 24);
      this.label8.TabIndex = 15;
      this.label8.Text = "TotalCartons:";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.BackColor = System.Drawing.Color.Transparent;
      this.label9.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label9.Location = new System.Drawing.Point(12, 429);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(166, 24);
      this.label9.TabIndex = 16;
      this.label9.Text = "EmptyCartons:";
      // 
      // txt_EmptyCartons
      // 
      this.txt_EmptyCartons.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.txt_EmptyCartons.Location = new System.Drawing.Point(195, 429);
      this.txt_EmptyCartons.Name = "txt_EmptyCartons";
      this.txt_EmptyCartons.Size = new System.Drawing.Size(249, 31);
      this.txt_EmptyCartons.TabIndex = 17;
      // 
      // btnPrint
      // 
      this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnPrint.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.btnPrint.Location = new System.Drawing.Point(468, 482);
      this.btnPrint.Name = "btnPrint";
      this.btnPrint.Size = new System.Drawing.Size(87, 39);
      this.btnPrint.TabIndex = 18;
      this.btnPrint.Text = "打印";
      this.btnPrint.UseVisualStyleBackColor = true;
      this.btnPrint.Visible = false;
      this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.BackColor = System.Drawing.Color.Transparent;
      this.label10.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label10.Location = new System.Drawing.Point(306, 204);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(29, 19);
      this.label10.TabIndex = 19;
      this.label10.Text = "OF";
      // 
      // txt_End
      // 
      this.txt_End.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.txt_End.Location = new System.Drawing.Point(352, 197);
      this.txt_End.Name = "txt_End";
      this.txt_End.Size = new System.Drawing.Size(90, 31);
      this.txt_End.TabIndex = 20;
      // 
      // tblayer
      // 
      this.tblayer.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.tblayer.Location = new System.Drawing.Point(193, 487);
      this.tblayer.Name = "tblayer";
      this.tblayer.Size = new System.Drawing.Size(249, 31);
      this.tblayer.TabIndex = 21;
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.BackColor = System.Drawing.Color.Transparent;
      this.label11.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label11.Location = new System.Drawing.Point(96, 487);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(82, 24);
      this.label11.TabIndex = 22;
      this.label11.Text = "Layer:";
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.BackColor = System.Drawing.Color.Transparent;
      this.label12.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label12.Location = new System.Drawing.Point(473, 28);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(90, 21);
      this.label12.TabIndex = 23;
      this.label12.Text = "标准重量：";
      // 
      // txtStandardWeitht
      // 
      this.txtStandardWeitht.Enabled = false;
      this.txtStandardWeitht.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.txtStandardWeitht.Location = new System.Drawing.Point(561, 24);
      this.txtStandardWeitht.Name = "txtStandardWeitht";
      this.txtStandardWeitht.Size = new System.Drawing.Size(100, 29);
      this.txtStandardWeitht.TabIndex = 24;
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.BackColor = System.Drawing.Color.Transparent;
      this.label13.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label13.Location = new System.Drawing.Point(485, 323);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(78, 21);
      this.label13.TabIndex = 25;
      this.label13.Text = "实际重量:";
      // 
      // txtWeight
      // 
      this.txtWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
      this.txtWeight.Enabled = false;
      this.txtWeight.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.txtWeight.ImeMode = System.Windows.Forms.ImeMode.Disable;
      this.txtWeight.Location = new System.Drawing.Point(569, 320);
      this.txtWeight.Name = "txtWeight";
      this.txtWeight.Size = new System.Drawing.Size(114, 29);
      this.txtWeight.TabIndex = 26;
      this.txtWeight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWeight_KeyDown);
      // 
      // label14
      // 
      this.label14.AutoSize = true;
      this.label14.BackColor = System.Drawing.Color.Transparent;
      this.label14.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label14.Location = new System.Drawing.Point(501, 82);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(58, 21);
      this.label14.TabIndex = 27;
      this.label14.Text = "偏差：";
      // 
      // txtDeviation
      // 
      this.txtDeviation.Enabled = false;
      this.txtDeviation.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.txtDeviation.Location = new System.Drawing.Point(561, 77);
      this.txtDeviation.Name = "txtDeviation";
      this.txtDeviation.Size = new System.Drawing.Size(100, 29);
      this.txtDeviation.TabIndex = 24;
      // 
      // label15
      // 
      this.label15.AutoSize = true;
      this.label15.BackColor = System.Drawing.Color.Transparent;
      this.label15.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label15.Location = new System.Drawing.Point(485, 143);
      this.label15.Name = "label15";
      this.label15.Size = new System.Drawing.Size(74, 21);
      this.label15.TabIndex = 28;
      this.label15.Text = "上下限：";
      // 
      // txtUpperlimit
      // 
      this.txtUpperlimit.Enabled = false;
      this.txtUpperlimit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.txtUpperlimit.Location = new System.Drawing.Point(650, 140);
      this.txtUpperlimit.Name = "txtUpperlimit";
      this.txtUpperlimit.Size = new System.Drawing.Size(51, 29);
      this.txtUpperlimit.TabIndex = 24;
      // 
      // txtLowerlimit
      // 
      this.txtLowerlimit.Enabled = false;
      this.txtLowerlimit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.txtLowerlimit.Location = new System.Drawing.Point(565, 140);
      this.txtLowerlimit.Name = "txtLowerlimit";
      this.txtLowerlimit.Size = new System.Drawing.Size(51, 29);
      this.txtLowerlimit.TabIndex = 29;
      // 
      // label16
      // 
      this.label16.AutoSize = true;
      this.label16.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label16.Location = new System.Drawing.Point(622, 143);
      this.label16.Name = "label16";
      this.label16.Size = new System.Drawing.Size(22, 21);
      this.label16.TabIndex = 27;
      this.label16.Text = "~";
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.TextMsg);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel3.Location = new System.Drawing.Point(0, 529);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(899, 50);
      this.panel3.TabIndex = 34;
      // 
      // TextMsg
      // 
      this.TextMsg.AutoEllipsis = true;
      this.TextMsg.BackColor = System.Drawing.Color.White;
      this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.TextMsg.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
      this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.TextMsg.Location = new System.Drawing.Point(0, 0);
      this.TextMsg.Name = "TextMsg";
      this.TextMsg.Size = new System.Drawing.Size(899, 50);
      this.TextMsg.TabIndex = 59;
      this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.label18);
      this.panel1.Controls.Add(this.cmbWeightType);
      this.panel1.Controls.Add(this.txtWeightRecive);
      this.panel1.Controls.Add(this.label17);
      this.panel1.Controls.Add(this.btnPrintPDF);
      this.panel1.Controls.Add(this.btnPrintH);
      this.panel1.Controls.Add(this.txtLowerlimit);
      this.panel1.Controls.Add(this.txtLocation);
      this.panel1.Controls.Add(this.label15);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.label16);
      this.panel1.Controls.Add(this.txtStandardWeitht);
      this.panel1.Controls.Add(this.label14);
      this.panel1.Controls.Add(this.label12);
      this.panel1.Controls.Add(this.txtWeight);
      this.panel1.Controls.Add(this.txtUpperlimit);
      this.panel1.Controls.Add(this.label13);
      this.panel1.Controls.Add(this.txtDeviation);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(899, 529);
      this.panel1.TabIndex = 35;
      // 
      // btnPrintPDF
      // 
      this.btnPrintPDF.Enabled = false;
      this.btnPrintPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnPrintPDF.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.btnPrintPDF.Location = new System.Drawing.Point(658, 482);
      this.btnPrintPDF.Name = "btnPrintPDF";
      this.btnPrintPDF.Size = new System.Drawing.Size(87, 39);
      this.btnPrintPDF.TabIndex = 38;
      this.btnPrintPDF.Text = "Print PDF";
      this.btnPrintPDF.UseVisualStyleBackColor = true;
      this.btnPrintPDF.Click += new System.EventHandler(this.btnPrintPDF_Click);
      // 
      // btnPrintH
      // 
      this.btnPrintH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnPrintH.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.btnPrintH.Location = new System.Drawing.Point(565, 482);
      this.btnPrintH.Name = "btnPrintH";
      this.btnPrintH.Size = new System.Drawing.Size(87, 39);
      this.btnPrintH.TabIndex = 38;
      this.btnPrintH.Text = "Print Handover";
      this.btnPrintH.UseVisualStyleBackColor = true;
      this.btnPrintH.Click += new System.EventHandler(this.btnPrintH_Click);
      // 
      // txtLocation
      // 
      this.txtLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
      this.txtLocation.Enabled = false;
      this.txtLocation.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.txtLocation.ImeMode = System.Windows.Forms.ImeMode.Disable;
      this.txtLocation.Location = new System.Drawing.Point(561, 371);
      this.txtLocation.Name = "txtLocation";
      this.txtLocation.Size = new System.Drawing.Size(198, 29);
      this.txtLocation.TabIndex = 37;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.BackColor = System.Drawing.Color.Transparent;
      this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label1.Location = new System.Drawing.Point(477, 374);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(78, 21);
      this.label1.TabIndex = 36;
      this.label1.Text = "出货储位:";
      // 
      // txtWeightRecive
      // 
      this.txtWeightRecive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
      this.txtWeightRecive.Enabled = false;
      this.txtWeightRecive.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.txtWeightRecive.ImeMode = System.Windows.Forms.ImeMode.Disable;
      this.txtWeightRecive.Location = new System.Drawing.Point(565, 197);
      this.txtWeightRecive.Multiline = true;
      this.txtWeightRecive.Name = "txtWeightRecive";
      this.txtWeightRecive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtWeightRecive.Size = new System.Drawing.Size(203, 105);
      this.txtWeightRecive.TabIndex = 40;
      this.txtWeightRecive.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWeightRecive_KeyDown);
      // 
      // label17
      // 
      this.label17.AutoSize = true;
      this.label17.BackColor = System.Drawing.Color.Transparent;
      this.label17.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label17.Location = new System.Drawing.Point(481, 200);
      this.label17.Name = "label17";
      this.label17.Size = new System.Drawing.Size(78, 21);
      this.label17.TabIndex = 39;
      this.label17.Text = "重量数据:";
      // 
      // cmbWeightType
      // 
      this.cmbWeightType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbWeightType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.cmbWeightType.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.cmbWeightType.FormattingEnabled = true;
      this.cmbWeightType.Items.AddRange(new object[] {
            "地磅",
            "蓝牙"});
      this.cmbWeightType.Location = new System.Drawing.Point(783, 23);
      this.cmbWeightType.Name = "cmbWeightType";
      this.cmbWeightType.Size = new System.Drawing.Size(90, 29);
      this.cmbWeightType.TabIndex = 41;
      this.cmbWeightType.SelectedIndexChanged += new System.EventHandler(this.cmbWeightType_SelectedIndexChanged);
      // 
      // label18
      // 
      this.label18.AutoSize = true;
      this.label18.BackColor = System.Drawing.Color.Transparent;
      this.label18.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.label18.Location = new System.Drawing.Point(687, 28);
      this.label18.Name = "label18";
      this.label18.Size = new System.Drawing.Size(90, 21);
      this.label18.TabIndex = 42;
      this.label18.Text = "称重方式：";
      // 
      // fMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(899, 579);
      this.Controls.Add(this.panel3);
      this.Controls.Add(this.label11);
      this.Controls.Add(this.tblayer);
      this.Controls.Add(this.txt_End);
      this.Controls.Add(this.label10);
      this.Controls.Add(this.btnPrint);
      this.Controls.Add(this.txt_EmptyCartons);
      this.Controls.Add(this.label9);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.txt_Invoice);
      this.Controls.Add(this.txt_Hawb);
      this.Controls.Add(this.txt_TotalCartons);
      this.Controls.Add(this.txt_Carrier);
      this.Controls.Add(this.txt_Start);
      this.Controls.Add(this.txt_Region);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txt_PalletId);
      this.Controls.Add(this.panel1);
      this.Name = "fMain";
      this.Text = "fMain";
      this.Load += new System.EventHandler(this.fMain_Load);
      this.panel3.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_PalletId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_Region;
        private System.Windows.Forms.TextBox txt_Start;
        private System.Windows.Forms.TextBox txt_Carrier;
        private System.Windows.Forms.TextBox txt_TotalCartons;
        private System.Windows.Forms.TextBox txt_Hawb;
        private System.Windows.Forms.TextBox txt_Invoice;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_EmptyCartons;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_End;
        private System.Windows.Forms.TextBox tblayer;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtStandardWeitht;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtDeviation;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtUpperlimit;
        private System.Windows.Forms.TextBox txtLowerlimit;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPrintH;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrintPDF;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cmbWeightType;
        private System.Windows.Forms.TextBox txtWeightRecive;
        private System.Windows.Forms.Label label17;
    }
}

