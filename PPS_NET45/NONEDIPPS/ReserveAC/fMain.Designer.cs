namespace ReverseAC
{
    partial class fMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtUnHold = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BtUnShip = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtCartonNo = new System.Windows.Forms.TextBox();
            this.txtShipmentId = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtMessage = new System.Windows.Forms.Label();
            this.DgvInfo = new System.Windows.Forms.DataGridView();
            this.Sel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RowIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShipmentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hawb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PalletNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaterialNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CartonNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TabReverse = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DgvInfoTran = new System.Windows.Forms.DataGridView();
            this.tbcRowNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbcPartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbcCartonNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbcPcNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnTran = new System.Windows.Forms.Button();
            this.BtnClear = new System.Windows.Forms.Button();
            this.TxtCarton = new System.Windows.Forms.TextBox();
            this.TxtMaterial = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtLocation = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvInfo)).BeginInit();
            this.TabReverse.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvInfoTran)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Margin = new System.Windows.Forms.Padding(6);
            this.textBox1.Name = "textBox1";
            this.textBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox1.Size = new System.Drawing.Size(1505, 74);
            this.textBox1.TabIndex = 62;
            this.textBox1.Text = "Reverse";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1483, 149);
            this.panel1.TabIndex = 63;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtUnHold);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.BtUnShip);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TxtCartonNo);
            this.groupBox1.Controls.Add(this.txtShipmentId);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1483, 149);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "搜索条件";
            // 
            // BtUnHold
            // 
            this.BtUnHold.Location = new System.Drawing.Point(660, 30);
            this.BtUnHold.Name = "BtUnHold";
            this.BtUnHold.Size = new System.Drawing.Size(193, 43);
            this.BtUnHold.TabIndex = 3;
            this.BtUnHold.Text = "解除Hold";
            this.BtUnHold.UseVisualStyleBackColor = true;
            this.BtUnHold.Click += new System.EventHandler(this.BtUnHold_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "集货单号";
            // 
            // BtUnShip
            // 
            this.BtUnShip.Location = new System.Drawing.Point(660, 86);
            this.BtUnShip.Name = "BtUnShip";
            this.BtUnShip.Size = new System.Drawing.Size(193, 43);
            this.BtUnShip.TabIndex = 3;
            this.BtUnShip.Text = "取消出货";
            this.BtUnShip.UseVisualStyleBackColor = true;
            this.BtUnShip.Click += new System.EventHandler(this.UnShip_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "箱号";
            // 
            // TxtCartonNo
            // 
            this.TxtCartonNo.Location = new System.Drawing.Point(209, 92);
            this.TxtCartonNo.Name = "TxtCartonNo";
            this.TxtCartonNo.Size = new System.Drawing.Size(348, 35);
            this.TxtCartonNo.TabIndex = 2;
            this.TxtCartonNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCartonNo_KeyPress);
            // 
            // txtShipmentId
            // 
            this.txtShipmentId.Location = new System.Drawing.Point(209, 36);
            this.txtShipmentId.Name = "txtShipmentId";
            this.txtShipmentId.Size = new System.Drawing.Size(348, 35);
            this.txtShipmentId.TabIndex = 1;
            this.txtShipmentId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtShipmentId_KeyPress);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtMessage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 957);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1505, 128);
            this.panel2.TabIndex = 64;
            // 
            // txtMessage
            // 
            this.txtMessage.AutoEllipsis = true;
            this.txtMessage.BackColor = System.Drawing.Color.White;
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.txtMessage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtMessage.Location = new System.Drawing.Point(0, 0);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(1505, 128);
            this.txtMessage.TabIndex = 63;
            this.txtMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DgvInfo
            // 
            this.DgvInfo.AllowUserToAddRows = false;
            this.DgvInfo.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.DgvInfo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.DgvInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sel,
            this.RowIndex,
            this.ShipmentId,
            this.Hawb,
            this.PalletNo,
            this.MaterialNo,
            this.CartonNo,
            this.Status});
            this.DgvInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvInfo.Location = new System.Drawing.Point(3, 152);
            this.DgvInfo.Name = "DgvInfo";
            this.DgvInfo.RowTemplate.Height = 30;
            this.DgvInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvInfo.Size = new System.Drawing.Size(1483, 681);
            this.DgvInfo.TabIndex = 2;
            this.DgvInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvInfo_CellClick);
            // 
            // Sel
            // 
            this.Sel.DataPropertyName = "SEL";
            this.Sel.FalseValue = "False";
            this.Sel.HeaderText = "选择";
            this.Sel.Name = "Sel";
            this.Sel.TrueValue = "True";
            // 
            // RowIndex
            // 
            this.RowIndex.DataPropertyName = "ROWNUM";
            this.RowIndex.HeaderText = "项次";
            this.RowIndex.Name = "RowIndex";
            this.RowIndex.ReadOnly = true;
            // 
            // ShipmentId
            // 
            this.ShipmentId.DataPropertyName = "SHIPMENT_ID";
            this.ShipmentId.HeaderText = "集货单号";
            this.ShipmentId.Name = "ShipmentId";
            this.ShipmentId.ReadOnly = true;
            // 
            // Hawb
            // 
            this.Hawb.DataPropertyName = "HAWB";
            this.Hawb.HeaderText = "HAWB";
            this.Hawb.Name = "Hawb";
            this.Hawb.ReadOnly = true;
            // 
            // PalletNo
            // 
            this.PalletNo.DataPropertyName = "Pallet_No";
            this.PalletNo.HeaderText = "栈板号";
            this.PalletNo.Name = "PalletNo";
            this.PalletNo.ReadOnly = true;
            // 
            // MaterialNo
            // 
            this.MaterialNo.DataPropertyName = "MaterialNo";
            this.MaterialNo.HeaderText = "料号";
            this.MaterialNo.Name = "MaterialNo";
            this.MaterialNo.ReadOnly = true;
            // 
            // CartonNo
            // 
            this.CartonNo.DataPropertyName = "Carton_No";
            this.CartonNo.HeaderText = "箱号";
            this.CartonNo.Name = "CartonNo";
            this.CartonNo.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "状态";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // TabReverse
            // 
            this.TabReverse.Controls.Add(this.tabPage1);
            this.TabReverse.Controls.Add(this.tabPage2);
            this.TabReverse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabReverse.Location = new System.Drawing.Point(0, 74);
            this.TabReverse.Name = "TabReverse";
            this.TabReverse.SelectedIndex = 0;
            this.TabReverse.Size = new System.Drawing.Size(1505, 883);
            this.TabReverse.TabIndex = 65;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DgvInfo);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1489, 836);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "取消出货";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DgvInfoTran);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1489, 836);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "储位转移";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DgvInfoTran
            // 
            this.DgvInfoTran.AllowUserToAddRows = false;
            this.DgvInfoTran.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.DgvInfoTran.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.DgvInfoTran.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvInfoTran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvInfoTran.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tbcRowNum,
            this.tbcPartNo,
            this.tbcCartonNo,
            this.tbcPcNo});
            this.DgvInfoTran.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvInfoTran.Location = new System.Drawing.Point(3, 315);
            this.DgvInfoTran.Name = "DgvInfoTran";
            this.DgvInfoTran.RowTemplate.Height = 30;
            this.DgvInfoTran.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvInfoTran.Size = new System.Drawing.Size(1483, 518);
            this.DgvInfoTran.TabIndex = 3;
            // 
            // tbcRowNum
            // 
            this.tbcRowNum.DataPropertyName = "ROWNUM";
            this.tbcRowNum.HeaderText = "项次";
            this.tbcRowNum.Name = "tbcRowNum";
            this.tbcRowNum.ReadOnly = true;
            // 
            // tbcPartNo
            // 
            this.tbcPartNo.DataPropertyName = "PART_NO";
            this.tbcPartNo.HeaderText = "料号";
            this.tbcPartNo.Name = "tbcPartNo";
            this.tbcPartNo.ReadOnly = true;
            // 
            // tbcCartonNo
            // 
            this.tbcCartonNo.DataPropertyName = "Carton_No";
            this.tbcCartonNo.HeaderText = "箱号";
            this.tbcCartonNo.Name = "tbcCartonNo";
            this.tbcCartonNo.ReadOnly = true;
            // 
            // tbcPcNo
            // 
            this.tbcPcNo.DataPropertyName = "RC_NO";
            this.tbcPcNo.HeaderText = "储位";
            this.tbcPcNo.Name = "tbcPcNo";
            this.tbcPcNo.ReadOnly = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1483, 312);
            this.panel3.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BtnTran);
            this.groupBox2.Controls.Add(this.BtnClear);
            this.groupBox2.Controls.Add(this.TxtCarton);
            this.groupBox2.Controls.Add(this.TxtMaterial);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 117);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1483, 185);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "筛选条件";
            // 
            // BtnTran
            // 
            this.BtnTran.Location = new System.Drawing.Point(596, 49);
            this.BtnTran.Name = "BtnTran";
            this.BtnTran.Size = new System.Drawing.Size(134, 45);
            this.BtnTran.TabIndex = 3;
            this.BtnTran.Text = "转移储位";
            this.BtnTran.UseVisualStyleBackColor = true;
            this.BtnTran.Click += new System.EventHandler(this.BtnTran_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.Location = new System.Drawing.Point(596, 114);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(134, 45);
            this.BtnClear.TabIndex = 4;
            this.BtnClear.Text = "清除箱号";
            this.BtnClear.UseVisualStyleBackColor = true;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // TxtCarton
            // 
            this.TxtCarton.Location = new System.Drawing.Point(181, 121);
            this.TxtCarton.Name = "TxtCarton";
            this.TxtCarton.Size = new System.Drawing.Size(337, 35);
            this.TxtCarton.TabIndex = 1;
            this.TxtCarton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCarton_KeyPress);
            // 
            // TxtMaterial
            // 
            this.TxtMaterial.Location = new System.Drawing.Point(181, 56);
            this.TxtMaterial.Name = "TxtMaterial";
            this.TxtMaterial.Size = new System.Drawing.Size(337, 35);
            this.TxtMaterial.TabIndex = 2;
            this.TxtMaterial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtMaterial_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(75, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 24);
            this.label6.TabIndex = 0;
            this.label6.Text = "箱号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(75, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 24);
            this.label4.TabIndex = 0;
            this.label4.Text = "料号：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.TxtLocation);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1483, 117);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "移入储位";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(75, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 24);
            this.label7.TabIndex = 0;
            this.label7.Text = "储位：";
            // 
            // TxtLocation
            // 
            this.TxtLocation.Location = new System.Drawing.Point(181, 47);
            this.TxtLocation.Name = "TxtLocation";
            this.TxtLocation.Size = new System.Drawing.Size(337, 35);
            this.TxtLocation.TabIndex = 0;
            this.TxtLocation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtLocation_KeyPress);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1505, 1085);
            this.Controls.Add(this.TabReverse);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.textBox1);
            this.Name = "fMain";
            this.Text = "fMain";
            this.Load += new System.EventHandler(this.fMain_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvInfo)).EndInit();
            this.TabReverse.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvInfoTran)).EndInit();
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtUnShip;
        private System.Windows.Forms.TextBox TxtCartonNo;
        private System.Windows.Forms.TextBox txtShipmentId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label txtMessage;
        private System.Windows.Forms.DataGridView DgvInfo;
        private System.Windows.Forms.Button BtUnHold;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Sel;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShipmentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hawb;
        private System.Windows.Forms.DataGridViewTextBoxColumn PalletNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaterialNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CartonNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.TabControl TabReverse;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView DgvInfoTran;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbcRowNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbcPartNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbcCartonNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbcPcNo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BtnClear;
        private System.Windows.Forms.TextBox TxtCarton;
        private System.Windows.Forms.TextBox TxtMaterial;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnTran;
    }
}