namespace CLine
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
            this.editStage = new System.Windows.Forms.TextBox();
            this.STAGE = new System.Windows.Forms.Label();
            this.editSection = new System.Windows.Forms.TextBox();
            this.editDepatrment = new System.Windows.Forms.TextBox();
            this.Depatrment = new System.Windows.Forms.Label();
            this.SECTION = new System.Windows.Forms.Label();
            this.combShift = new System.Windows.Forms.ComboBox();
            this.LabShift = new System.Windows.Forms.Label();
            this.LabFactoryName = new System.Windows.Forms.Label();
            this.LabFac = new System.Windows.Forms.Label();
            this.editDesc2 = new System.Windows.Forms.TextBox();
            this.editDesc = new System.Windows.Forms.TextBox();
            this.editName = new System.Windows.Forms.TextBox();
            this.LabDesc = new System.Windows.Forms.Label();
            this.LabName = new System.Windows.Forms.Label();
            this.LabDesc2 = new System.Windows.Forms.Label();
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
            this.panelControl.Controls.Add(this.editStage);
            this.panelControl.Controls.Add(this.STAGE);
            this.panelControl.Controls.Add(this.editSection);
            this.panelControl.Controls.Add(this.editDepatrment);
            this.panelControl.Controls.Add(this.Depatrment);
            this.panelControl.Controls.Add(this.SECTION);
            this.panelControl.Controls.Add(this.combShift);
            this.panelControl.Controls.Add(this.LabShift);
            this.panelControl.Controls.Add(this.LabFactoryName);
            this.panelControl.Controls.Add(this.LabFac);
            this.panelControl.Controls.Add(this.editDesc2);
            this.panelControl.Controls.Add(this.editDesc);
            this.panelControl.Controls.Add(this.editName);
            this.panelControl.Controls.Add(this.LabDesc);
            this.panelControl.Controls.Add(this.LabName);
            this.panelControl.Controls.Add(this.LabDesc2);
            this.panelControl.Name = "panelControl";
            // 
            // editStage
            // 
            resources.ApplyResources(this.editStage, "editStage");
            this.editStage.Name = "editStage";
            // 
            // STAGE
            // 
            resources.ApplyResources(this.STAGE, "STAGE");
            this.STAGE.BackColor = System.Drawing.Color.Transparent;
            this.STAGE.Name = "STAGE";
            // 
            // editSection
            // 
            resources.ApplyResources(this.editSection, "editSection");
            this.editSection.Name = "editSection";
            // 
            // editDepatrment
            // 
            resources.ApplyResources(this.editDepatrment, "editDepatrment");
            this.editDepatrment.Name = "editDepatrment";
            // 
            // Depatrment
            // 
            resources.ApplyResources(this.Depatrment, "Depatrment");
            this.Depatrment.BackColor = System.Drawing.Color.Transparent;
            this.Depatrment.Name = "Depatrment";
            // 
            // SECTION
            // 
            resources.ApplyResources(this.SECTION, "SECTION");
            this.SECTION.BackColor = System.Drawing.Color.Transparent;
            this.SECTION.Name = "SECTION";
            // 
            // combShift
            // 
            this.combShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.combShift, "combShift");
            this.combShift.FormattingEnabled = true;
            this.combShift.Name = "combShift";
            this.combShift.SelectedIndexChanged += new System.EventHandler(this.combShift_SelectedIndexChanged);
            // 
            // LabShift
            // 
            resources.ApplyResources(this.LabShift, "LabShift");
            this.LabShift.BackColor = System.Drawing.Color.Transparent;
            this.LabShift.Name = "LabShift";
            // 
            // LabFactoryName
            // 
            resources.ApplyResources(this.LabFactoryName, "LabFactoryName");
            this.LabFactoryName.BackColor = System.Drawing.Color.Transparent;
            this.LabFactoryName.ForeColor = System.Drawing.Color.Maroon;
            this.LabFactoryName.Name = "LabFactoryName";
            this.LabFactoryName.Click += new System.EventHandler(this.LabFactoryName_Click);
            // 
            // LabFac
            // 
            resources.ApplyResources(this.LabFac, "LabFac");
            this.LabFac.BackColor = System.Drawing.Color.Transparent;
            this.LabFac.Name = "LabFac";
            // 
            // editDesc2
            // 
            resources.ApplyResources(this.editDesc2, "editDesc2");
            this.editDesc2.Name = "editDesc2";
            this.editDesc2.TextChanged += new System.EventHandler(this.editDesc2_TextChanged);
            // 
            // editDesc
            // 
            resources.ApplyResources(this.editDesc, "editDesc");
            this.editDesc.Name = "editDesc";
            this.editDesc.TextChanged += new System.EventHandler(this.editDesc_TextChanged);
            // 
            // editName
            // 
            this.editName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            resources.ApplyResources(this.editName, "editName");
            this.editName.Name = "editName";
            this.editName.TextChanged += new System.EventHandler(this.editName_TextChanged);
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
            // LabDesc2
            // 
            resources.ApplyResources(this.LabDesc2, "LabDesc2");
            this.LabDesc2.BackColor = System.Drawing.Color.Transparent;
            this.LabDesc2.Name = "LabDesc2";
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
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
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
        private System.Windows.Forms.TextBox editDesc2;
        private System.Windows.Forms.Label LabDesc2;
        private System.Windows.Forms.Label LabFac;
        private System.Windows.Forms.Label LabFactoryName;
        private System.Windows.Forms.ComboBox combShift;
        private System.Windows.Forms.Label LabShift;
        private System.Windows.Forms.TextBox editStage;
        private System.Windows.Forms.Label STAGE;
        private System.Windows.Forms.TextBox editSection;
        private System.Windows.Forms.TextBox editDepatrment;
        private System.Windows.Forms.Label Depatrment;
        private System.Windows.Forms.Label SECTION;
    }
}