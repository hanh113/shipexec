namespace CreateSN
{
    partial class ManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagerForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnMPart = new System.Windows.Forms.ToolStripButton();
            this.btnAppleCare = new System.Windows.Forms.ToolStripButton();
            this.btnPPart = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMPart,
            this.btnPPart,
            this.btnAppleCare});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnMPart
            // 
            this.btnMPart.Image = ((System.Drawing.Image)(resources.GetObject("btnMPart.Image")));
            this.btnMPart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMPart.Name = "btnMPart";
            this.btnMPart.Size = new System.Drawing.Size(62, 22);
            this.btnMPart.Text = "M Part";
            this.btnMPart.Click += new System.EventHandler(this.btnMPart_Click);
            // 
            // btnAppleCare
            // 
            this.btnAppleCare.Image = ((System.Drawing.Image)(resources.GetObject("btnAppleCare.Image")));
            this.btnAppleCare.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAppleCare.Name = "btnAppleCare";
            this.btnAppleCare.Size = new System.Drawing.Size(83, 22);
            this.btnAppleCare.Text = "Apple care";
            this.btnAppleCare.Click += new System.EventHandler(this.btnAppleCare_Click);
            // 
            // btnPPart
            // 
            this.btnPPart.BackColor = System.Drawing.SystemColors.Control;
            this.btnPPart.Image = ((System.Drawing.Image)(resources.GetObject("btnPPart.Image")));
            this.btnPPart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPPart.Name = "btnPPart";
            this.btnPPart.Size = new System.Drawing.Size(58, 22);
            this.btnPPart.Text = "P Part";
            this.btnPPart.Click += new System.EventHandler(this.btnPPart_Click);
            // 
            // ManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "ManagerForm";
            this.Text = "ManagerForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnMPart;
        private System.Windows.Forms.ToolStripButton btnPPart;
        private System.Windows.Forms.ToolStripButton btnAppleCare;
    }
}