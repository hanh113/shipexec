namespace RePack.Forms
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
            this.lbPallet = new System.Windows.Forms.Label();
            this.tbPallet = new System.Windows.Forms.TextBox();
            this.cbType = new System.Windows.Forms.ComboBox();
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
            this.prgMain.Controls.Add(this.cbType);
            this.prgMain.Controls.Add(this.tbPallet);
            this.prgMain.Controls.Add(this.lbPallet);
            this.prgMain.Size = new System.Drawing.Size(800, 299);
            // 
            // prgTitle
            // 
            this.prgTitle.Size = new System.Drawing.Size(800, 79);
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
            // lbPallet
            // 
            this.lbPallet.AutoSize = true;
            this.lbPallet.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPallet.Location = new System.Drawing.Point(374, 54);
            this.lbPallet.Name = "lbPallet";
            this.lbPallet.Size = new System.Drawing.Size(22, 36);
            this.lbPallet.TabIndex = 0;
            this.lbPallet.Text = ":";
            // 
            // tbPallet
            // 
            this.tbPallet.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPallet.Location = new System.Drawing.Point(166, 125);
            this.tbPallet.Name = "tbPallet";
            this.tbPallet.Size = new System.Drawing.Size(450, 39);
            this.tbPallet.TabIndex = 1;
            this.tbPallet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPallet_KeyDown);
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "CARTON",
            "PALLET"});
            this.cbType.Location = new System.Drawing.Point(166, 50);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(202, 44);
            this.cbType.TabIndex = 2;
            // 
            // fReprint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "fReprint";
            this.Text = "fReprint";
            this.Load += new System.EventHandler(this.fReprint_Load);
            this.prgBanner.ResumeLayout(false);
            this.prgMain.ResumeLayout(false);
            this.prgMain.PerformLayout();
            this.prgFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbPallet;
        private System.Windows.Forms.Label lbPallet;
        private System.Windows.Forms.ComboBox cbType;
    }
}