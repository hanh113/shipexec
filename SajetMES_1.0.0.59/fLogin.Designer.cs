namespace EDIPPS
{
    partial class fLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fLogin));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.combValue = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lablPwd = new System.Windows.Forms.Label();
            this.cmbServer = new System.Windows.Forms.ComboBox();
            this.textBoxEmp = new System.Windows.Forms.TextBox();
            this.lablEmp = new System.Windows.Forms.Label();
            this.textBoxPwd = new System.Windows.Forms.TextBox();
            this.cmbLang = new System.Windows.Forms.ComboBox();
            this.lablHost = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lablVersion = new System.Windows.Forms.Label();
            this.lablCopyright = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.combValue, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.progressBar1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lablVersion, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lablCopyright, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnClose, 3, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginForm_MouseDown);
            // 
            // combValue
            // 
            this.combValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combValue.FormattingEnabled = true;
            resources.ApplyResources(this.combValue, "combValue");
            this.combValue.Name = "combValue";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.lablPwd, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.cmbServer, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.textBoxEmp, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.lablEmp, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.textBoxPwd, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.cmbLang, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.lablHost, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnCancel, 2, 4);
            this.tableLayoutPanel3.Controls.Add(this.btnLogin, 1, 4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // lablPwd
            // 
            this.lablPwd.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lablPwd, "lablPwd");
            this.lablPwd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(98)))), ((int)(((byte)(159)))));
            this.lablPwd.Name = "lablPwd";
            // 
            // cmbServer
            // 
            resources.ApplyResources(this.cmbServer, "cmbServer");
            this.cmbServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServer.FormattingEnabled = true;
            this.cmbServer.Name = "cmbServer";
            this.cmbServer.SelectedIndexChanged += new System.EventHandler(this.cmbServer_SelectedIndexChanged);
            // 
            // textBoxEmp
            // 
            resources.ApplyResources(this.textBoxEmp, "textBoxEmp");
            this.textBoxEmp.Name = "textBoxEmp";
            this.textBoxEmp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxEmp_KeyPress);
            // 
            // lablEmp
            // 
            this.lablEmp.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lablEmp, "lablEmp");
            this.lablEmp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(98)))), ((int)(((byte)(159)))));
            this.lablEmp.Name = "lablEmp";
            // 
            // textBoxPwd
            // 
            resources.ApplyResources(this.textBoxPwd, "textBoxPwd");
            this.textBoxPwd.Name = "textBoxPwd";
            this.textBoxPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPwd_KeyPress);
            // 
            // cmbLang
            // 
            resources.ApplyResources(this.cmbLang, "cmbLang");
            this.cmbLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLang.FormattingEnabled = true;
            this.cmbLang.Name = "cmbLang";
            this.cmbLang.SelectedIndexChanged += new System.EventHandler(this.cmbLang_SelectedIndexChanged);
            // 
            // lablHost
            // 
            this.lablHost.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lablHost, "lablHost");
            this.lablHost.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(98)))), ((int)(((byte)(159)))));
            this.lablHost.Name = "lablHost";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(98)))), ((int)(((byte)(159)))));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // btnLogin
            // 
            resources.ApplyResources(this.btnLogin, "btnLogin");
            this.btnLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(98)))), ((int)(((byte)(159)))));
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // progressBar1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.progressBar1, 4);
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // lablVersion
            // 
            resources.ApplyResources(this.lablVersion, "lablVersion");
            this.tableLayoutPanel1.SetColumnSpan(this.lablVersion, 4);
            this.lablVersion.Name = "lablVersion";
            // 
            // lablCopyright
            // 
            resources.ApplyResources(this.lablCopyright, "lablCopyright");
            this.tableLayoutPanel1.SetColumnSpan(this.lablCopyright, 4);
            this.lablCopyright.Name = "lablCopyright";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // fLogin
            // 
            this.BackColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fLogin";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginForm_MouseDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lablVersion;
        private System.Windows.Forms.Label lablCopyright;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.ComboBox cmbServer;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox textBoxEmp;
        private System.Windows.Forms.TextBox textBoxPwd;
        private System.Windows.Forms.ComboBox cmbLang;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox combValue;
        private System.Windows.Forms.Label lablPwd;
        private System.Windows.Forms.Label lablEmp;
        private System.Windows.Forms.Label lablHost;
    }
}

