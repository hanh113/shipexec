namespace Stock_Out.Forms
{
    partial class fReprint
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbLocation = new System.Windows.Forms.TextBox();
            this.prgBanner.SuspendLayout();
            this.prgMain.SuspendLayout();
            this.prgFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // prgBanner
            // 
            this.prgBanner.Size = new System.Drawing.Size(800, 79);
            // 
            // prgMain
            // 
            this.prgMain.Controls.Add(this.tbLocation);
            this.prgMain.Controls.Add(this.label1);
            this.prgMain.Size = new System.Drawing.Size(800, 299);
            // 
            // prgTitle
            // 
            this.prgTitle.Size = new System.Drawing.Size(800, 79);
            this.prgTitle.Text = "Reprint";
            // 
            // prgFooter
            // 
            this.prgFooter.Location = new System.Drawing.Point(0, 378);
            this.prgFooter.Size = new System.Drawing.Size(800, 72);
            // 
            // prgMSG
            // 
            this.prgMSG.Size = new System.Drawing.Size(800, 72);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(162, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "PPS Location:";
            // 
            // tbLocation
            // 
            this.tbLocation.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbLocation.Location = new System.Drawing.Point(164, 137);
            this.tbLocation.Name = "tbLocation";
            this.tbLocation.Size = new System.Drawing.Size(467, 39);
            this.tbLocation.TabIndex = 1;
            this.tbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbLocation_KeyDown);
            // 
            // fReprint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "fReprint";
            this.Text = "fReprint";
            this.prgBanner.ResumeLayout(false);
            this.prgMain.ResumeLayout(false);
            this.prgMain.PerformLayout();
            this.prgFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbLocation;
        private System.Windows.Forms.Label label1;
    }
}