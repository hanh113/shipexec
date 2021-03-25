namespace CEmp
{
    partial class fRoleAssign
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fRoleAssign));
            this.SC1 = new System.Windows.Forms.SplitContainer();
            this.LVAll = new System.Windows.Forms.ListView();
            this.tvCol1 = new System.Windows.Forms.ColumnHeader();
            this.tvCol2 = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.LabEmp = new System.Windows.Forms.Label();
            this.bbtnSelectAll = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.bbtnSelectNone = new System.Windows.Forms.Button();
            this.LVChoose = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.panel4 = new System.Windows.Forms.Panel();
            this.bbtnSelectNone1 = new System.Windows.Forms.Button();
            this.bbtnSelectAll1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bbtnChoose = new System.Windows.Forms.Button();
            this.bbtnRemove = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bbtnCancel = new System.Windows.Forms.Button();
            this.bbtnSave = new System.Windows.Forms.Button();
            this.SC1.Panel1.SuspendLayout();
            this.SC1.Panel2.SuspendLayout();
            this.SC1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SC1
            // 
            resources.ApplyResources(this.SC1, "SC1");
            this.SC1.Name = "SC1";
            // 
            // SC1.Panel1
            // 
            this.SC1.Panel1.Controls.Add(this.LVAll);
            this.SC1.Panel1.Controls.Add(this.panel3);
            // 
            // SC1.Panel2
            // 
            this.SC1.Panel2.Controls.Add(this.LVChoose);
            this.SC1.Panel2.Controls.Add(this.panel4);
            this.SC1.Panel2.Controls.Add(this.panel2);
            // 
            // LVAll
            // 
            this.LVAll.BackColor = System.Drawing.Color.White;
            this.LVAll.CheckBoxes = true;
            this.LVAll.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.tvCol1,
            this.tvCol2});
            resources.ApplyResources(this.LVAll, "LVAll");
            this.LVAll.FullRowSelect = true;
            this.LVAll.Name = "LVAll";
            this.LVAll.SmallImageList = this.imageList1;
            this.LVAll.UseCompatibleStateImageBehavior = false;
            this.LVAll.View = System.Windows.Forms.View.Details;
            // 
            // tvCol1
            // 
            resources.ApplyResources(this.tvCol1, "tvCol1");
            // 
            // tvCol2
            // 
            resources.ApplyResources(this.tvCol2, "tvCol2");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "role.bmp");
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.LabEmp);
            this.panel3.Controls.Add(this.bbtnSelectAll);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.bbtnSelectNone);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // LabEmp
            // 
            resources.ApplyResources(this.LabEmp, "LabEmp");
            this.LabEmp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.LabEmp.Name = "LabEmp";
            // 
            // bbtnSelectAll
            // 
            resources.ApplyResources(this.bbtnSelectAll, "bbtnSelectAll");
            this.bbtnSelectAll.Name = "bbtnSelectAll";
            this.bbtnSelectAll.UseVisualStyleBackColor = true;
            this.bbtnSelectAll.Click += new System.EventHandler(this.bbtnSelectAll_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // bbtnSelectNone
            // 
            resources.ApplyResources(this.bbtnSelectNone, "bbtnSelectNone");
            this.bbtnSelectNone.Name = "bbtnSelectNone";
            this.bbtnSelectNone.UseVisualStyleBackColor = true;
            this.bbtnSelectNone.Click += new System.EventHandler(this.bbtnSelectNone_Click);
            // 
            // LVChoose
            // 
            this.LVChoose.BackColor = System.Drawing.Color.White;
            this.LVChoose.CheckBoxes = true;
            this.LVChoose.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            resources.ApplyResources(this.LVChoose, "LVChoose");
            this.LVChoose.Name = "LVChoose";
            this.LVChoose.SmallImageList = this.imageList1;
            this.LVChoose.UseCompatibleStateImageBehavior = false;
            this.LVChoose.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.bbtnSelectNone1);
            this.panel4.Controls.Add(this.bbtnSelectAll1);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // bbtnSelectNone1
            // 
            resources.ApplyResources(this.bbtnSelectNone1, "bbtnSelectNone1");
            this.bbtnSelectNone1.Name = "bbtnSelectNone1";
            this.bbtnSelectNone1.UseVisualStyleBackColor = true;
            this.bbtnSelectNone1.Click += new System.EventHandler(this.bbtnSelectNone1_Click);
            // 
            // bbtnSelectAll1
            // 
            resources.ApplyResources(this.bbtnSelectAll1, "bbtnSelectAll1");
            this.bbtnSelectAll1.Name = "bbtnSelectAll1";
            this.bbtnSelectAll1.UseVisualStyleBackColor = true;
            this.bbtnSelectAll1.Click += new System.EventHandler(this.bbtnSelectAll1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.bbtnChoose);
            this.panel2.Controls.Add(this.bbtnRemove);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // bbtnChoose
            // 
            resources.ApplyResources(this.bbtnChoose, "bbtnChoose");
            this.bbtnChoose.Name = "bbtnChoose";
            this.bbtnChoose.UseVisualStyleBackColor = true;
            this.bbtnChoose.Click += new System.EventHandler(this.bbtnChoose_Click);
            // 
            // bbtnRemove
            // 
            resources.ApplyResources(this.bbtnRemove, "bbtnRemove");
            this.bbtnRemove.Name = "bbtnRemove";
            this.bbtnRemove.UseVisualStyleBackColor = true;
            this.bbtnRemove.Click += new System.EventHandler(this.bbtnRemove_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bbtnCancel);
            this.panel1.Controls.Add(this.bbtnSave);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // bbtnCancel
            // 
            this.bbtnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.bbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.bbtnCancel, "bbtnCancel");
            this.bbtnCancel.Name = "bbtnCancel";
            this.bbtnCancel.UseVisualStyleBackColor = false;
            // 
            // bbtnSave
            // 
            this.bbtnSave.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.bbtnSave, "bbtnSave");
            this.bbtnSave.Name = "bbtnSave";
            this.bbtnSave.UseVisualStyleBackColor = false;
            this.bbtnSave.Click += new System.EventHandler(this.bbtnSave_Click);
            // 
            // fRoleAssign
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SC1);
            this.Controls.Add(this.panel1);
            this.Name = "fRoleAssign";
            this.Load += new System.EventHandler(this.fRoleAssign_Load);
            this.SC1.Panel1.ResumeLayout(false);
            this.SC1.Panel2.ResumeLayout(false);
            this.SC1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer SC1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView LVAll;
        private System.Windows.Forms.ColumnHeader tvCol1;
        private System.Windows.Forms.ColumnHeader tvCol2;
        private System.Windows.Forms.Button bbtnSelectNone;
        private System.Windows.Forms.Button bbtnChoose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bbtnCancel;
        private System.Windows.Forms.Button bbtnSave;
        private System.Windows.Forms.ListView LVChoose;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button bbtnSelectAll1;
        private System.Windows.Forms.Button bbtnRemove;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button bbtnSelectNone1;
        public System.Windows.Forms.Label LabEmp;
        private System.Windows.Forms.ImageList imageList1;        
        private System.Windows.Forms.Button bbtnSelectAll;
    }
}