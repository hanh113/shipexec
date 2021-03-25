namespace cWHConfig.Forms
{
    partial class fDetails
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbStoreNo = new System.Windows.Forms.Label();
            this.lbWarehouseNo = new System.Windows.Forms.Label();
            this.lbLocationNo = new System.Windows.Forms.Label();
            this.txtStoreNo = new System.Windows.Forms.TextBox();
            this.txtWareHouseNo = new System.Windows.Forms.TextBox();
            this.txtLocationNo = new System.Windows.Forms.TextBox();
            this.cbStoreNo = new System.Windows.Forms.ComboBox();
            this.cbWareHouseNo = new System.Windows.Forms.ComboBox();
            this.cbLocationNo = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(458, 469);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 378);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(452, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(32, 27);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(135, 51);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(287, 28);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(135, 51);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtStoreNo);
            this.groupBox2.Controls.Add(this.txtLocationNo);
            this.groupBox2.Controls.Add(this.cbLocationNo);
            this.groupBox2.Controls.Add(this.cbStoreNo);
            this.groupBox2.Controls.Add(this.txtWareHouseNo);
            this.groupBox2.Controls.Add(this.lbLocationNo);
            this.groupBox2.Controls.Add(this.lbWarehouseNo);
            this.groupBox2.Controls.Add(this.lbStoreNo);
            this.groupBox2.Controls.Add(this.cbWareHouseNo);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(452, 369);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // lbStoreNo
            // 
            this.lbStoreNo.AutoSize = true;
            this.lbStoreNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbStoreNo.Location = new System.Drawing.Point(67, 62);
            this.lbStoreNo.Name = "lbStoreNo";
            this.lbStoreNo.Size = new System.Drawing.Size(100, 28);
            this.lbStoreNo.TabIndex = 0;
            this.lbStoreNo.Text = "StoreNo:";
            // 
            // lbWarehouseNo
            // 
            this.lbWarehouseNo.AutoSize = true;
            this.lbWarehouseNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbWarehouseNo.Location = new System.Drawing.Point(3, 148);
            this.lbWarehouseNo.Name = "lbWarehouseNo";
            this.lbWarehouseNo.Size = new System.Drawing.Size(164, 28);
            this.lbWarehouseNo.TabIndex = 0;
            this.lbWarehouseNo.Text = "WareHouseNo:";
            // 
            // lbLocationNo
            // 
            this.lbLocationNo.AutoSize = true;
            this.lbLocationNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbLocationNo.Location = new System.Drawing.Point(33, 245);
            this.lbLocationNo.Name = "lbLocationNo";
            this.lbLocationNo.Size = new System.Drawing.Size(134, 28);
            this.lbLocationNo.TabIndex = 0;
            this.lbLocationNo.Text = "LocationNo:";
            // 
            // txtStoreNo
            // 
            this.txtStoreNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStoreNo.Location = new System.Drawing.Point(173, 63);
            this.txtStoreNo.Name = "txtStoreNo";
            this.txtStoreNo.Size = new System.Drawing.Size(209, 35);
            this.txtStoreNo.TabIndex = 1;
            // 
            // txtWareHouseNo
            // 
            this.txtWareHouseNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtWareHouseNo.Location = new System.Drawing.Point(173, 150);
            this.txtWareHouseNo.Name = "txtWareHouseNo";
            this.txtWareHouseNo.Size = new System.Drawing.Size(209, 35);
            this.txtWareHouseNo.TabIndex = 1;
            // 
            // txtLocationNo
            // 
            this.txtLocationNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLocationNo.Location = new System.Drawing.Point(173, 244);
            this.txtLocationNo.Name = "txtLocationNo";
            this.txtLocationNo.Size = new System.Drawing.Size(209, 35);
            this.txtLocationNo.TabIndex = 1;
            // 
            // cbStoreNo
            // 
            this.cbStoreNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStoreNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbStoreNo.FormattingEnabled = true;
            this.cbStoreNo.Location = new System.Drawing.Point(173, 62);
            this.cbStoreNo.Name = "cbStoreNo";
            this.cbStoreNo.Size = new System.Drawing.Size(249, 36);
            this.cbStoreNo.TabIndex = 2;
            this.cbStoreNo.SelectedIndexChanged += new System.EventHandler(this.cbStoreNo_SelectedIndexChanged);
            // 
            // cbWareHouseNo
            // 
            this.cbWareHouseNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWareHouseNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbWareHouseNo.FormattingEnabled = true;
            this.cbWareHouseNo.Location = new System.Drawing.Point(173, 148);
            this.cbWareHouseNo.Name = "cbWareHouseNo";
            this.cbWareHouseNo.Size = new System.Drawing.Size(249, 36);
            this.cbWareHouseNo.TabIndex = 2;
            // 
            // cbLocationNo
            // 
            this.cbLocationNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLocationNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbLocationNo.FormattingEnabled = true;
            this.cbLocationNo.Location = new System.Drawing.Point(173, 242);
            this.cbLocationNo.Name = "cbLocationNo";
            this.cbLocationNo.Size = new System.Drawing.Size(249, 36);
            this.cbLocationNo.TabIndex = 2;
            // 
            // fDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 469);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "fDetails";
            this.Text = "fDetails";
            this.Load += new System.EventHandler(this.fDetails_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbLocationNo;
        private System.Windows.Forms.ComboBox cbWareHouseNo;
        private System.Windows.Forms.ComboBox cbStoreNo;
        private System.Windows.Forms.TextBox txtLocationNo;
        private System.Windows.Forms.TextBox txtWareHouseNo;
        private System.Windows.Forms.TextBox txtStoreNo;
        private System.Windows.Forms.Label lbLocationNo;
        private System.Windows.Forms.Label lbWarehouseNo;
        private System.Windows.Forms.Label lbStoreNo;
    }
}