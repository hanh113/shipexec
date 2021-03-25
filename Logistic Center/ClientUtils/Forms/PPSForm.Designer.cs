namespace ClientUtilsDll.Forms
{
    partial class PPSForm
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
        //private void InitializeComponent()
        //{
        //    this.components = new System.ComponentModel.Container();
        //    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        //    this.ClientSize = new System.Drawing.Size(800, 450);
        //    this.Text = "PPSForm";
        //}
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.prgBanner = new System.Windows.Forms.Panel();
            this.prgTitle = new System.Windows.Forms.Label();
            this.prgMain = new System.Windows.Forms.GroupBox();
            this.prgFooter = new System.Windows.Forms.Panel();
            this.prgMSG = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.prgBanner.SuspendLayout();
            this.prgFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // prgBanner
            // 
            this.prgBanner.BackColor = System.Drawing.Color.RoyalBlue;
            this.prgBanner.Controls.Add(this.prgTitle);
            this.prgBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.prgBanner.Location = new System.Drawing.Point(0, 0);
            this.prgBanner.Name = "prgBanner";
            this.prgBanner.Size = new System.Drawing.Size(931, 79);
            this.prgBanner.TabIndex = 0;
            // 
            // prgTitle
            // 
            this.prgTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prgTitle.Font = new System.Drawing.Font("Verdana", 24F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prgTitle.ForeColor = System.Drawing.Color.White;
            this.prgTitle.Location = new System.Drawing.Point(0, 0);
            this.prgTitle.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.prgTitle.Name = "prgTitle";
            this.prgTitle.Size = new System.Drawing.Size(931, 79);
            this.prgTitle.TabIndex = 1;
            this.prgTitle.Text = "Title";
            this.prgTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.prgTitle.MouseHover += new System.EventHandler(this.prgTitle_MouseHover);
            // 
            // prgMain
            // 
            this.prgMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prgMain.Location = new System.Drawing.Point(0, 79);
            this.prgMain.Name = "prgMain";
            this.prgMain.Size = new System.Drawing.Size(931, 388);
            this.prgMain.TabIndex = 2;
            this.prgMain.TabStop = false;
            // 
            // prgFooter
            // 
            this.prgFooter.Controls.Add(this.prgMSG);
            this.prgFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.prgFooter.Location = new System.Drawing.Point(0, 467);
            this.prgFooter.Name = "prgFooter";
            this.prgFooter.Size = new System.Drawing.Size(931, 72);
            this.prgFooter.TabIndex = 3;
            // 
            // prgMSG
            // 
            this.prgMSG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prgMSG.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prgMSG.ForeColor = System.Drawing.Color.Blue;
            this.prgMSG.Location = new System.Drawing.Point(0, 0);
            this.prgMSG.Margin = new System.Windows.Forms.Padding(4, 3, 4, 0);
            this.prgMSG.Name = "prgMSG";
            this.prgMSG.Size = new System.Drawing.Size(931, 72);
            this.prgMSG.TabIndex = 1;
            this.prgMSG.Text = "Message";
            this.prgMSG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.prgMSG.MouseHover += new System.EventHandler(this.prgMSG_MouseHover);
            // 
            // PPSForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 539);
            this.Controls.Add(this.prgMain);
            this.Controls.Add(this.prgBanner);
            this.Controls.Add(this.prgFooter);
            this.Name = "PPSForm";
            this.Text = "PPSForm";
            this.prgBanner.ResumeLayout(false);
            this.prgFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
        public System.Windows.Forms.Panel prgBanner;
        public System.Windows.Forms.GroupBox prgMain;
        public System.Windows.Forms.Label prgTitle;
        public System.Windows.Forms.Panel prgFooter;
        public System.Windows.Forms.Label prgMSG;
        private System.Windows.Forms.ToolTip toolTip;
    }
}