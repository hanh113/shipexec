namespace wmsReport
{
    partial class ShowPassStationLog
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
            this.show_data_log_DGV = new System.Windows.Forms.DataGridView();
            this.Message_LB = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.show_data_log_DGV)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Message_LB);
            this.panel1.Controls.Add(this.show_data_log_DGV);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(617, 330);
            this.panel1.TabIndex = 0;
            // 
            // show_data_log_DGV
            // 
            this.show_data_log_DGV.AllowUserToAddRows = false;
            this.show_data_log_DGV.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.show_data_log_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.show_data_log_DGV.Location = new System.Drawing.Point(3, 3);
            this.show_data_log_DGV.Name = "show_data_log_DGV";
            this.show_data_log_DGV.ReadOnly = true;
            this.show_data_log_DGV.RowHeadersVisible = false;
            this.show_data_log_DGV.RowTemplate.Height = 27;
            this.show_data_log_DGV.Size = new System.Drawing.Size(611, 285);
            this.show_data_log_DGV.TabIndex = 0;
            // 
            // Message_LB
            // 
            this.Message_LB.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Message_LB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Message_LB.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Message_LB.Location = new System.Drawing.Point(0, 291);
            this.Message_LB.Name = "Message_LB";
            this.Message_LB.Size = new System.Drawing.Size(617, 39);
            this.Message_LB.TabIndex = 15;
            this.Message_LB.Text = "Message";
            this.Message_LB.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ShowPassStationLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 330);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowPassStationLog";
            this.Text = "ShowPassStationLog";
            this.Load += new System.EventHandler(this.ShowPassStationLog_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.show_data_log_DGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView show_data_log_DGV;
        private System.Windows.Forms.Label Message_LB;
    }
}