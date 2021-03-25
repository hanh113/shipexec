namespace CTerminal
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LabTerminal = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.LabLine = new System.Windows.Forms.Label();
            this.LabProcess = new System.Windows.Forms.Label();
            this.editTerminal = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.combSide = new System.Windows.Forms.ComboBox();
            this.editSMTMachine = new System.Windows.Forms.TextBox();
            this.btnSearchSMT = new System.Windows.Forms.Button();
            this.rdbtnSMT = new System.Windows.Forms.RadioButton();
            this.rdbtnPC = new System.Windows.Forms.RadioButton();
            this.btnSearchMachine = new System.Windows.Forms.Button();
            this.rdbtnDCN = new System.Windows.Forms.RadioButton();
            this.editMachine = new System.Windows.Forms.TextBox();
            this.rdbtnMachine = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Name = "label2";
            // 
            // LabTerminal
            // 
            resources.ApplyResources(this.LabTerminal, "LabTerminal");
            this.LabTerminal.ForeColor = System.Drawing.Color.Black;
            this.LabTerminal.Name = "LabTerminal";
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // LabLine
            // 
            resources.ApplyResources(this.LabLine, "LabLine");
            this.LabLine.ForeColor = System.Drawing.Color.Maroon;
            this.LabLine.Name = "LabLine";
            // 
            // LabProcess
            // 
            resources.ApplyResources(this.LabProcess, "LabProcess");
            this.LabProcess.ForeColor = System.Drawing.Color.Maroon;
            this.LabProcess.Name = "LabProcess";
            // 
            // editTerminal
            // 
            this.editTerminal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            resources.ApplyResources(this.editTerminal, "editTerminal");
            this.editTerminal.Name = "editTerminal";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.editTerminal);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.LabTerminal);
            this.panel1.Controls.Add(this.LabProcess);
            this.panel1.Controls.Add(this.LabLine);
            this.panel1.Name = "panel1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.combSide);
            this.groupBox1.Controls.Add(this.editSMTMachine);
            this.groupBox1.Controls.Add(this.btnSearchSMT);
            this.groupBox1.Controls.Add(this.rdbtnSMT);
            this.groupBox1.Controls.Add(this.rdbtnPC);
            this.groupBox1.Controls.Add(this.btnSearchMachine);
            this.groupBox1.Controls.Add(this.rdbtnDCN);
            this.groupBox1.Controls.Add(this.editMachine);
            this.groupBox1.Controls.Add(this.rdbtnMachine);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // combSide
            // 
            this.combSide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.combSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combSide.FormattingEnabled = true;
            resources.ApplyResources(this.combSide, "combSide");
            this.combSide.Name = "combSide";
            // 
            // editSMTMachine
            // 
            this.editSMTMachine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            resources.ApplyResources(this.editSMTMachine, "editSMTMachine");
            this.editSMTMachine.Name = "editSMTMachine";
            this.editSMTMachine.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editSMTMachine_KeyPress);
            this.editSMTMachine.EnabledChanged += new System.EventHandler(this.editSMTMachine_EnabledChanged);
            // 
            // btnSearchSMT
            // 
            resources.ApplyResources(this.btnSearchSMT, "btnSearchSMT");
            this.btnSearchSMT.Name = "btnSearchSMT";
            this.btnSearchSMT.UseVisualStyleBackColor = true;
            this.btnSearchSMT.Click += new System.EventHandler(this.btnSearchSMT_Click);
            // 
            // rdbtnSMT
            // 
            resources.ApplyResources(this.rdbtnSMT, "rdbtnSMT");
            this.rdbtnSMT.Name = "rdbtnSMT";
            this.rdbtnSMT.TabStop = true;
            this.rdbtnSMT.UseVisualStyleBackColor = true;
            this.rdbtnSMT.CheckedChanged += new System.EventHandler(this.rdbtnSMT_CheckedChanged);
            // 
            // rdbtnPC
            // 
            resources.ApplyResources(this.rdbtnPC, "rdbtnPC");
            this.rdbtnPC.Name = "rdbtnPC";
            this.rdbtnPC.TabStop = true;
            this.rdbtnPC.UseVisualStyleBackColor = true;
            // 
            // btnSearchMachine
            // 
            resources.ApplyResources(this.btnSearchMachine, "btnSearchMachine");
            this.btnSearchMachine.Name = "btnSearchMachine";
            this.btnSearchMachine.UseVisualStyleBackColor = true;
            this.btnSearchMachine.Click += new System.EventHandler(this.btnSearchMachine_Click);
            // 
            // rdbtnDCN
            // 
            resources.ApplyResources(this.rdbtnDCN, "rdbtnDCN");
            this.rdbtnDCN.Name = "rdbtnDCN";
            this.rdbtnDCN.TabStop = true;
            this.rdbtnDCN.UseVisualStyleBackColor = true;
            // 
            // editMachine
            // 
            this.editMachine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            resources.ApplyResources(this.editMachine, "editMachine");
            this.editMachine.Name = "editMachine";
            this.editMachine.EnabledChanged += new System.EventHandler(this.editMachine_EnabledChanged);
            // 
            // rdbtnMachine
            // 
            resources.ApplyResources(this.rdbtnMachine, "rdbtnMachine");
            this.rdbtnMachine.Name = "rdbtnMachine";
            this.rdbtnMachine.TabStop = true;
            this.rdbtnMachine.UseVisualStyleBackColor = true;
            this.rdbtnMachine.CheckedChanged += new System.EventHandler(this.rdbtnMachine_CheckedChanged);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Name = "panel2";
            // 
            // fData
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "fData";
            this.Load += new System.EventHandler(this.fData_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Label LabLine;
        public System.Windows.Forms.Label LabProcess;
        public System.Windows.Forms.TextBox editTerminal;
        private System.Windows.Forms.Label LabTerminal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdbtnMachine;
        private System.Windows.Forms.RadioButton rdbtnDCN;
        private System.Windows.Forms.RadioButton rdbtnPC;
        private System.Windows.Forms.Button btnSearchMachine;
        public System.Windows.Forms.TextBox editMachine;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbtnSMT;
        private System.Windows.Forms.Button btnSearchSMT;
        public System.Windows.Forms.TextBox editSMTMachine;
        private System.Windows.Forms.ComboBox combSide;
    }
}