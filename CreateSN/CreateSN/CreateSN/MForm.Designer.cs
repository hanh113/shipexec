namespace CreateSN
{
    partial class MForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MForm));
            this.btnMpart = new System.Windows.Forms.Button();
            this.btnPpart = new System.Windows.Forms.Button();
            this.btnAC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMpart
            // 
            this.btnMpart.Location = new System.Drawing.Point(74, 72);
            this.btnMpart.Name = "btnMpart";
            this.btnMpart.Size = new System.Drawing.Size(221, 62);
            this.btnMpart.TabIndex = 0;
            this.btnMpart.Text = "M Part";
            this.btnMpart.UseVisualStyleBackColor = true;
            this.btnMpart.Click += new System.EventHandler(this.btnMpart_Click);
            // 
            // btnPpart
            // 
            this.btnPpart.Location = new System.Drawing.Point(74, 196);
            this.btnPpart.Name = "btnPpart";
            this.btnPpart.Size = new System.Drawing.Size(221, 62);
            this.btnPpart.TabIndex = 0;
            this.btnPpart.Text = "P Part";
            this.btnPpart.UseVisualStyleBackColor = true;
            this.btnPpart.Click += new System.EventHandler(this.btnPpart_Click);
            // 
            // btnAC
            // 
            this.btnAC.Location = new System.Drawing.Point(74, 332);
            this.btnAC.Name = "btnAC";
            this.btnAC.Size = new System.Drawing.Size(221, 62);
            this.btnAC.TabIndex = 0;
            this.btnAC.Text = "Apple Care";
            this.btnAC.UseVisualStyleBackColor = true;
            this.btnAC.Click += new System.EventHandler(this.btnAC_Click);
            // 
            // MForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 495);
            this.Controls.Add(this.btnAC);
            this.Controls.Add(this.btnPpart);
            this.Controls.Add(this.btnMpart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MForm";
            this.Text = "测试造序号";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMpart;
        private System.Windows.Forms.Button btnPpart;
        private System.Windows.Forms.Button btnAC;
    }
}

