namespace InPaShippingLabel
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_shipingNO = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_Hawb = new System.Windows.Forms.TextBox();
            this.txt_Invoice = new System.Windows.Forms.TextBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.txt_MSG = new System.Windows.Forms.TextBox();
            this.cmb_type = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_InvoiceLableName = new System.Windows.Forms.TextBox();
            this.txt_PLLableName = new System.Windows.Forms.TextBox();
            this.btnPrintPacklist = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(227, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "出货单号:";
            // 
            // txt_shipingNO
            // 
            this.txt_shipingNO.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_shipingNO.Location = new System.Drawing.Point(421, 57);
            this.txt_shipingNO.Margin = new System.Windows.Forms.Padding(4);
            this.txt_shipingNO.Name = "txt_shipingNO";
            this.txt_shipingNO.Size = new System.Drawing.Size(332, 37);
            this.txt_shipingNO.TabIndex = 1;
            this.txt_shipingNO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_sscc_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(122, 290);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(253, 30);
            this.label2.TabIndex = 2;
            this.label2.Text = "Invoice模板名称:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(242, 115);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 30);
            this.label5.TabIndex = 5;
            this.label5.Text = "Invoice:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(287, 177);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 30);
            this.label6.TabIndex = 6;
            this.label6.Text = "HAWB:";
            // 
            // txt_Hawb
            // 
            this.txt_Hawb.Enabled = false;
            this.txt_Hawb.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Hawb.Location = new System.Drawing.Point(421, 170);
            this.txt_Hawb.Margin = new System.Windows.Forms.Padding(4);
            this.txt_Hawb.Name = "txt_Hawb";
            this.txt_Hawb.Size = new System.Drawing.Size(332, 37);
            this.txt_Hawb.TabIndex = 13;
            // 
            // txt_Invoice
            // 
            this.txt_Invoice.Enabled = false;
            this.txt_Invoice.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Invoice.Location = new System.Drawing.Point(421, 113);
            this.txt_Invoice.Margin = new System.Windows.Forms.Padding(4);
            this.txt_Invoice.Name = "txt_Invoice";
            this.txt_Invoice.Size = new System.Drawing.Size(332, 37);
            this.txt_Invoice.TabIndex = 14;
            // 
            // btnPrint
            // 
            this.btnPrint.Enabled = false;
            this.btnPrint.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrint.Location = new System.Drawing.Point(349, 399);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(191, 49);
            this.btnPrint.TabIndex = 18;
            this.btnPrint.Text = "打印Invoice";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // txt_MSG
            // 
            this.txt_MSG.BackColor = System.Drawing.SystemColors.Window;
            this.txt_MSG.Font = new System.Drawing.Font("宋体", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_MSG.Location = new System.Drawing.Point(25, 467);
            this.txt_MSG.Multiline = true;
            this.txt_MSG.Name = "txt_MSG";
            this.txt_MSG.Size = new System.Drawing.Size(777, 54);
            this.txt_MSG.TabIndex = 19;
            this.txt_MSG.Text = "N/A";
            this.txt_MSG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmb_type
            // 
            this.cmb_type.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_type.FormattingEnabled = true;
            this.cmb_type.Items.AddRange(new object[] {
            "AMR",
            "APAC",
            "EMEA"});
            this.cmb_type.Location = new System.Drawing.Point(421, 232);
            this.cmb_type.Name = "cmb_type";
            this.cmb_type.Size = new System.Drawing.Size(332, 38);
            this.cmb_type.TabIndex = 22;
            this.cmb_type.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(227, 234);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 30);
            this.label3.TabIndex = 21;
            this.label3.Text = "打印类型:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(47, 339);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(328, 30);
            this.label4.TabIndex = 23;
            this.label4.Text = "PACKING LIST模板名称:";
            // 
            // txt_InvoiceLableName
            // 
            this.txt_InvoiceLableName.Enabled = false;
            this.txt_InvoiceLableName.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_InvoiceLableName.Location = new System.Drawing.Point(421, 287);
            this.txt_InvoiceLableName.Margin = new System.Windows.Forms.Padding(4);
            this.txt_InvoiceLableName.Name = "txt_InvoiceLableName";
            this.txt_InvoiceLableName.Size = new System.Drawing.Size(332, 37);
            this.txt_InvoiceLableName.TabIndex = 24;
            // 
            // txt_PLLableName
            // 
            this.txt_PLLableName.Enabled = false;
            this.txt_PLLableName.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_PLLableName.Location = new System.Drawing.Point(421, 335);
            this.txt_PLLableName.Margin = new System.Windows.Forms.Padding(4);
            this.txt_PLLableName.Name = "txt_PLLableName";
            this.txt_PLLableName.Size = new System.Drawing.Size(332, 37);
            this.txt_PLLableName.TabIndex = 25;
            // 
            // btnPrintPacklist
            // 
            this.btnPrintPacklist.Enabled = false;
            this.btnPrintPacklist.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrintPacklist.Location = new System.Drawing.Point(597, 399);
            this.btnPrintPacklist.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrintPacklist.Name = "btnPrintPacklist";
            this.btnPrintPacklist.Size = new System.Drawing.Size(195, 49);
            this.btnPrintPacklist.TabIndex = 26;
            this.btnPrintPacklist.Text = "打印Packlist";
            this.btnPrintPacklist.UseVisualStyleBackColor = true;
            this.btnPrintPacklist.Click += new System.EventHandler(this.btnPrintPacklist_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(902, 13);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 49);
            this.button1.TabIndex = 27;
            this.button1.Text = "其他测试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 518);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnPrintPacklist);
            this.Controls.Add(this.txt_PLLableName);
            this.Controls.Add(this.txt_InvoiceLableName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmb_type);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_MSG);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.txt_Invoice);
            this.Controls.Add(this.txt_Hawb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_shipingNO);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "fMain";
            this.Text = "fMain";
            this.Load += new System.EventHandler(this.fMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_shipingNO;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_Hawb;
        private System.Windows.Forms.TextBox txt_Invoice;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TextBox txt_MSG;
        private System.Windows.Forms.ComboBox cmb_type;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_InvoiceLableName;
        private System.Windows.Forms.TextBox txt_PLLableName;
        private System.Windows.Forms.Button btnPrintPacklist;
        private System.Windows.Forms.Button button1;
    }
}

