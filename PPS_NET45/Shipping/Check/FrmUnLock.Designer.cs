namespace Check
{
    partial class FrmUnLock
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
            this.labPW = new System.Windows.Forms.Label();
            this.txtPW = new System.Windows.Forms.TextBox();
            this.labinfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labPW
            // 
            this.labPW.AutoSize = true;
            this.labPW.Location = new System.Drawing.Point(63, 44);
            this.labPW.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labPW.Name = "labPW";
            this.labPW.Size = new System.Drawing.Size(89, 18);
            this.labPW.TabIndex = 0;
            this.labPW.Text = "解锁条码:";
            // 
            // txtPW
            // 
            this.txtPW.Location = new System.Drawing.Point(164, 38);
            this.txtPW.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPW.Name = "txtPW";
            this.txtPW.Size = new System.Drawing.Size(362, 28);
            this.txtPW.TabIndex = 1;
            this.txtPW.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPW_KeyDown);
            // 
            // labinfo
            // 
            this.labinfo.AutoSize = true;
            this.labinfo.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labinfo.Location = new System.Drawing.Point(160, 108);
            this.labinfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labinfo.Name = "labinfo";
            this.labinfo.Size = new System.Drawing.Size(0, 29);
            this.labinfo.TabIndex = 2;
            // 
            // FrmUnLock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 164);
            this.ControlBox = false;
            this.Controls.Add(this.labinfo);
            this.Controls.Add(this.txtPW);
            this.Controls.Add(this.labPW);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUnLock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scan pwd pls.";
            this.Load += new System.EventHandler(this.FrmUnLock_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labPW;
        private System.Windows.Forms.TextBox txtPW;
        private System.Windows.Forms.Label labinfo;
    }
}