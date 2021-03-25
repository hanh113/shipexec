namespace CDept
{
    partial class fData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fData));
            this.panelControl = new System.Windows.Forms.Panel();
            this.LabFactoryName = new System.Windows.Forms.Label();
            this.LabFac = new System.Windows.Forms.Label();
            this.editDesc = new System.Windows.Forms.TextBox();
            this.editName = new System.Windows.Forms.TextBox();
            this.LabDesc = new System.Windows.Forms.Label();
            this.LabName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.panelControl.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            resources.ApplyResources(this.panelControl, "panelControl");
            this.panelControl.BackColor = System.Drawing.Color.Transparent;
            this.panelControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelControl.Controls.Add(this.LabFactoryName);
            this.panelControl.Controls.Add(this.LabFac);
            this.panelControl.Controls.Add(this.editDesc);
            this.panelControl.Controls.Add(this.editName);
            this.panelControl.Controls.Add(this.LabDesc);
            this.panelControl.Controls.Add(this.LabName);
            this.panelControl.Name = "panelControl";
            // 
            // LabFactoryName
            // 
            resources.ApplyResources(this.LabFactoryName, "LabFactoryName");
            this.LabFactoryName.BackColor = System.Drawing.Color.Transparent;
            this.LabFactoryName.ForeColor = System.Drawing.Color.Maroon;
            this.LabFactoryName.Name = "LabFactoryName";
            // 
            // LabFac
            // 
            resources.ApplyResources(this.LabFac, "LabFac");
            this.LabFac.BackColor = System.Drawing.Color.Transparent;
            this.LabFac.Name = "LabFac";
            // 
            // editDesc
            // 
            resources.ApplyResources(this.editDesc, "editDesc");
            this.editDesc.Name = "editDesc";
            // 
            // editName
            // 
            this.editName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            resources.ApplyResources(this.editName, "editName");
            this.editName.Name = "editName";
            // 
            // LabDesc
            // 
            resources.ApplyResources(this.LabDesc, "LabDesc");
            this.LabDesc.BackColor = System.Drawing.Color.Transparent;
            this.LabDesc.Name = "LabDesc";
            // 
            // LabName
            // 
            resources.ApplyResources(this.LabName, "LabName");
            this.LabName.BackColor = System.Drawing.Color.Transparent;
            this.LabName.Name = "LabName";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOK);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // fData
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.panel2);
            this.Name = "fData";
            this.Load += new System.EventHandler(this.fData_Load);
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox editDesc;
        private System.Windows.Forms.TextBox editName;
        private System.Windows.Forms.Label LabDesc;
        private System.Windows.Forms.Label LabName;
        private System.Windows.Forms.Label LabFac;
        private System.Windows.Forms.Label LabFactoryName;
    }
}