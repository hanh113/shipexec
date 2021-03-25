namespace cWHConfig.Forms
{
    partial class Modify
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbtype1 = new System.Windows.Forms.Label();
            this.lbtype2 = new System.Windows.Forms.Label();
            this.txtRename = new System.Windows.Forms.TextBox();
            this.cbStoreNo = new System.Windows.Forms.ComboBox();
            this.lbWarehouseNo = new System.Windows.Forms.Label();
            this.lbStoreNo = new System.Windows.Forms.Label();
            this.cbWareHouseNo = new System.Windows.Forms.ComboBox();
            this.lbRename = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(499, 450);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 362);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 85);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(311, 24);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(135, 51);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(68, 24);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(135, 51);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbtype2);
            this.groupBox2.Controls.Add(this.lbtype1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(493, 61);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtRename);
            this.groupBox3.Controls.Add(this.cbStoreNo);
            this.groupBox3.Controls.Add(this.lbRename);
            this.groupBox3.Controls.Add(this.lbWarehouseNo);
            this.groupBox3.Controls.Add(this.lbStoreNo);
            this.groupBox3.Controls.Add(this.cbWareHouseNo);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 70);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(493, 286);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // lbtype1
            // 
            this.lbtype1.AutoSize = true;
            this.lbtype1.Location = new System.Drawing.Point(119, 24);
            this.lbtype1.Name = "lbtype1";
            this.lbtype1.Size = new System.Drawing.Size(53, 18);
            this.lbtype1.TabIndex = 0;
            this.lbtype1.Text = "TYPE:";
            // 
            // lbtype2
            // 
            this.lbtype2.AutoSize = true;
            this.lbtype2.Location = new System.Drawing.Point(232, 24);
            this.lbtype2.Name = "lbtype2";
            this.lbtype2.Size = new System.Drawing.Size(62, 18);
            this.lbtype2.TabIndex = 0;
            this.lbtype2.Text = "TYPE2:";
            // 
            // txtRename
            // 
            this.txtRename.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRename.Location = new System.Drawing.Point(178, 199);
            this.txtRename.Name = "txtRename";
            this.txtRename.Size = new System.Drawing.Size(249, 35);
            this.txtRename.TabIndex = 7;
            // 
            // cbStoreNo
            // 
            this.cbStoreNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStoreNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbStoreNo.FormattingEnabled = true;
            this.cbStoreNo.Location = new System.Drawing.Point(178, 35);
            this.cbStoreNo.Name = "cbStoreNo";
            this.cbStoreNo.Size = new System.Drawing.Size(249, 36);
            this.cbStoreNo.TabIndex = 10;
            this.cbStoreNo.SelectedIndexChanged += new System.EventHandler(this.cbStoreNo_SelectedIndexChanged);
            // 
            // lbWarehouseNo
            // 
            this.lbWarehouseNo.AutoSize = true;
            this.lbWarehouseNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbWarehouseNo.Location = new System.Drawing.Point(8, 118);
            this.lbWarehouseNo.Name = "lbWarehouseNo";
            this.lbWarehouseNo.Size = new System.Drawing.Size(164, 28);
            this.lbWarehouseNo.TabIndex = 4;
            this.lbWarehouseNo.Text = "WareHouseNo:";
            // 
            // lbStoreNo
            // 
            this.lbStoreNo.AutoSize = true;
            this.lbStoreNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbStoreNo.Location = new System.Drawing.Point(72, 35);
            this.lbStoreNo.Name = "lbStoreNo";
            this.lbStoreNo.Size = new System.Drawing.Size(100, 28);
            this.lbStoreNo.TabIndex = 5;
            this.lbStoreNo.Text = "StoreNo:";
            // 
            // cbWareHouseNo
            // 
            this.cbWareHouseNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWareHouseNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbWareHouseNo.FormattingEnabled = true;
            this.cbWareHouseNo.Location = new System.Drawing.Point(178, 115);
            this.cbWareHouseNo.Name = "cbWareHouseNo";
            this.cbWareHouseNo.Size = new System.Drawing.Size(249, 36);
            this.cbWareHouseNo.TabIndex = 11;
            // 
            // lbRename
            // 
            this.lbRename.AutoSize = true;
            this.lbRename.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRename.Location = new System.Drawing.Point(72, 202);
            this.lbRename.Name = "lbRename";
            this.lbRename.Size = new System.Drawing.Size(100, 28);
            this.lbRename.TabIndex = 3;
            this.lbRename.Text = "Rename:";
            // 
            // Modify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Modify";
            this.Text = "Modify";
            this.Load += new System.EventHandler(this.Modify_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbtype2;
        private System.Windows.Forms.Label lbtype1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtRename;
        private System.Windows.Forms.ComboBox cbStoreNo;
        private System.Windows.Forms.Label lbRename;
        private System.Windows.Forms.Label lbWarehouseNo;
        private System.Windows.Forms.Label lbStoreNo;
        private System.Windows.Forms.ComboBox cbWareHouseNo;
    }
}