namespace CRoute
{
    partial class fCopyRoute
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fCopyRoute));
            this.panel1 = new System.Windows.Forms.Panel();
            this.editNew = new System.Windows.Forms.TextBox();
            this.combCopyRoute = new System.Windows.Forms.ComboBox();
            this.LabName = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.LabCopy = new System.Windows.Forms.Label();
            this.TVRoute = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.editNew);
            this.panel1.Controls.Add(this.combCopyRoute);
            this.panel1.Controls.Add(this.LabName);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.LabCopy);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // editNew
            // 
            resources.ApplyResources(this.editNew, "editNew");
            this.editNew.Name = "editNew";
            // 
            // combCopyRoute
            // 
            this.combCopyRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combCopyRoute.FormattingEnabled = true;
            resources.ApplyResources(this.combCopyRoute, "combCopyRoute");
            this.combCopyRoute.Name = "combCopyRoute";
            this.combCopyRoute.SelectedIndexChanged += new System.EventHandler(this.combCopyRoute_SelectedIndexChanged);
            // 
            // LabName
            // 
            resources.ApplyResources(this.LabName, "LabName");
            this.LabName.Name = "LabName";
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // LabCopy
            // 
            resources.ApplyResources(this.LabCopy, "LabCopy");
            this.LabCopy.Name = "LabCopy";
            // 
            // TVRoute
            // 
            this.TVRoute.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.TVRoute, "TVRoute");
            this.TVRoute.ImageList = this.imageList1;
            this.TVRoute.Name = "TVRoute";
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
            // fCopyRoute
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TVRoute);
            this.Controls.Add(this.panel1);
            this.Name = "fCopyRoute";
            this.Load += new System.EventHandler(this.fCopyRoute_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label LabCopy;
        private System.Windows.Forms.TreeView TVRoute;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label LabName;
        private System.Windows.Forms.TextBox editNew;
        private System.Windows.Forms.ComboBox combCopyRoute;
    }
}