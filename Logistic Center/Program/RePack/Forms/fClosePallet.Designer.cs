namespace RePack.Forms
{
    partial class fClosePallet
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
            this.components = new System.ComponentModel.Container();
            this.tbPallet = new System.Windows.Forms.TextBox();
            this.lbPallet = new System.Windows.Forms.Label();
            this.btnOpenPallet = new System.Windows.Forms.Button();
            this.btnClosePallet = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
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
            this.prgMain.Controls.Add(this.btnClosePallet);
            this.prgMain.Controls.Add(this.btnOpenPallet);
            this.prgMain.Controls.Add(this.tbPallet);
            this.prgMain.Controls.Add(this.lbPallet);
            this.prgMain.Size = new System.Drawing.Size(800, 299);
            // 
            // prgTitle
            // 
            this.prgTitle.Size = new System.Drawing.Size(800, 79);
            this.prgTitle.Text = "Close Pallet";
            // 
            // prgFooter
            // 
            this.prgFooter.Location = new System.Drawing.Point(0, 378);
            this.prgFooter.Size = new System.Drawing.Size(800, 72);
            // 
            // prgMSG
            // 
            this.prgMSG.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prgMSG.Size = new System.Drawing.Size(800, 72);
            this.prgMSG.MouseHover += new System.EventHandler(this.prgMSG_MouseHover);
            // 
            // tbPallet
            // 
            this.tbPallet.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPallet.Location = new System.Drawing.Point(235, 94);
            this.tbPallet.Name = "tbPallet";
            this.tbPallet.Size = new System.Drawing.Size(434, 39);
            this.tbPallet.TabIndex = 3;
            this.tbPallet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPallet_KeyDown);
            // 
            // lbPallet
            // 
            this.lbPallet.AutoSize = true;
            this.lbPallet.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPallet.Location = new System.Drawing.Point(75, 94);
            this.lbPallet.Name = "lbPallet";
            this.lbPallet.Size = new System.Drawing.Size(118, 36);
            this.lbPallet.TabIndex = 2;
            this.lbPallet.Text = "PALLET:";
            // 
            // btnOpenPallet
            // 
            this.btnOpenPallet.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpenPallet.Location = new System.Drawing.Point(124, 217);
            this.btnOpenPallet.Name = "btnOpenPallet";
            this.btnOpenPallet.Size = new System.Drawing.Size(154, 45);
            this.btnOpenPallet.TabIndex = 4;
            this.btnOpenPallet.Text = "Open Pallet";
            this.btnOpenPallet.UseVisualStyleBackColor = true;
            this.btnOpenPallet.Click += new System.EventHandler(this.btnOpenPallet_Click);
            // 
            // btnClosePallet
            // 
            this.btnClosePallet.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClosePallet.Location = new System.Drawing.Point(469, 217);
            this.btnClosePallet.Name = "btnClosePallet";
            this.btnClosePallet.Size = new System.Drawing.Size(154, 45);
            this.btnClosePallet.TabIndex = 4;
            this.btnClosePallet.Text = "Close Pallet";
            this.btnClosePallet.UseVisualStyleBackColor = true;
            this.btnClosePallet.Click += new System.EventHandler(this.btnClosePallet_Click);
            // 
            // fClosePallet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "fClosePallet";
            this.Text = "fClosePallet";
            this.Load += new System.EventHandler(this.fClosePallet_Load);
            this.prgBanner.ResumeLayout(false);
            this.prgMain.ResumeLayout(false);
            this.prgMain.PerformLayout();
            this.prgFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClosePallet;
        private System.Windows.Forms.Button btnOpenPallet;
        private System.Windows.Forms.TextBox tbPallet;
        private System.Windows.Forms.Label lbPallet;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}