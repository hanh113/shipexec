namespace CRoute
{
    partial class fHistory
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該公開 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fHistory));
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkbName = new System.Windows.Forms.CheckBox();
            this.chkbID = new System.Windows.Forms.CheckBox();
            this.combTime = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.TV = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.chkbName);
            this.panel1.Controls.Add(this.chkbID);
            this.panel1.Controls.Add(this.combTime);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtName);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // chkbName
            // 
            resources.ApplyResources(this.chkbName, "chkbName");
            this.chkbName.Name = "chkbName";
            this.chkbName.UseVisualStyleBackColor = true;
            this.chkbName.Click += new System.EventHandler(this.chkbName_Click);
            // 
            // chkbID
            // 
            resources.ApplyResources(this.chkbID, "chkbID");
            this.chkbID.Checked = true;
            this.chkbID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbID.Name = "chkbID";
            this.chkbID.UseVisualStyleBackColor = true;
            this.chkbID.Click += new System.EventHandler(this.chkbID_Click);
            // 
            // combTime
            // 
            this.combTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combTime.FormattingEnabled = true;
            resources.ApplyResources(this.combTime, "combTime");
            this.combTime.Name = "combTime";
            this.combTime.SelectedIndexChanged += new System.EventHandler(this.combTime_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // TV
            // 
            this.TV.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.TV, "TV");
            this.TV.ImageList = this.imageList1;
            this.TV.Name = "TV";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "RouteStage.bmp");
            this.imageList1.Images.SetKeyName(1, "RouteProcess.bmp");
            this.imageList1.Images.SetKeyName(2, "RouteRepair.bmp");
            this.imageList1.Images.SetKeyName(3, "RouteUnnecessary.bmp");
            this.imageList1.Images.SetKeyName(4, "RouteDisabled.bmp");
            this.imageList1.Images.SetKeyName(5, "RouteDisabledRepair.bmp");
            // 
            // fHistory
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TV);
            this.Controls.Add(this.panel1);
            this.Name = "fHistory";
            this.Load += new System.EventHandler(this.fHistory_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox combTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView TV;
        private System.Windows.Forms.CheckBox chkbName;
        private System.Windows.Forms.CheckBox chkbID;
        private System.Windows.Forms.ImageList imageList1;

    }
}