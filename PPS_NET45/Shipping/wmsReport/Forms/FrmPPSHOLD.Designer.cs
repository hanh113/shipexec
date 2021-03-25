namespace wmsReport
{
    partial class FrmPPSHOLD
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panCondition = new System.Windows.Forms.Panel();
            this.panData = new System.Windows.Forms.Panel();
            this.btnEXCEL3 = new System.Windows.Forms.Button();
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.panCondition.SuspendLayout();
            this.panData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // panCondition
            // 
            this.panCondition.Controls.Add(this.btnEXCEL3);
            this.panCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.panCondition.Location = new System.Drawing.Point(0, 0);
            this.panCondition.Name = "panCondition";
            this.panCondition.Size = new System.Drawing.Size(1009, 58);
            this.panCondition.TabIndex = 0;
            // 
            // panData
            // 
            this.panData.Controls.Add(this.dgvDetail);
            this.panData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panData.Location = new System.Drawing.Point(0, 58);
            this.panData.Name = "panData";
            this.panData.Size = new System.Drawing.Size(1009, 370);
            this.panData.TabIndex = 1;
            // 
            // btnEXCEL3
            // 
            this.btnEXCEL3.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnEXCEL3.Location = new System.Drawing.Point(85, 14);
            this.btnEXCEL3.Name = "btnEXCEL3";
            this.btnEXCEL3.Size = new System.Drawing.Size(87, 29);
            this.btnEXCEL3.TabIndex = 138;
            this.btnEXCEL3.Text = "导出EXCEL";
            this.btnEXCEL3.UseVisualStyleBackColor = true;
            this.btnEXCEL3.Click += new System.EventHandler(this.btnEXCEL3_Click);
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.AllowUserToDeleteRows = false;
            this.dgvDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvDetail.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvDetail.ColumnHeadersHeight = 30;
            this.dgvDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetail.Location = new System.Drawing.Point(0, 0);
            this.dgvDetail.Margin = new System.Windows.Forms.Padding(2);
            this.dgvDetail.MultiSelect = false;
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetail.RowHeadersWidth = 55;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.dgvDetail.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDetail.RowTemplate.Height = 27;
            this.dgvDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetail.Size = new System.Drawing.Size(1009, 370);
            this.dgvDetail.TabIndex = 97;
            // 
            // FrmPPSHOLD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 428);
            this.Controls.Add(this.panData);
            this.Controls.Add(this.panCondition);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPPSHOLD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PPS未出货被HOLD数据";
            this.Load += new System.EventHandler(this.FrmPPSHOLD_Load);
            this.panCondition.ResumeLayout(false);
            this.panData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panCondition;
        private System.Windows.Forms.Panel panData;
        private System.Windows.Forms.Button btnEXCEL3;
        private System.Windows.Forms.DataGridView dgvDetail;
    }
}