namespace CTerminal
{
    partial class fMachineLink
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearchStage = new System.Windows.Forms.Button();
            this.btnSearchProcess = new System.Windows.Forms.Button();
            this.editProcess = new System.Windows.Forms.TextBox();
            this.editStage = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labLine = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.combTerminalType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.gvData = new System.Windows.Forms.DataGridView();
            this.STAGE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PROCESS_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TERMINAL_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TERMINAL_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SMT_SIDE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MACHINE_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearchStage);
            this.panel1.Controls.Add(this.btnSearchProcess);
            this.panel1.Controls.Add(this.editProcess);
            this.panel1.Controls.Add(this.editStage);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.labLine);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.combTerminalType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(651, 93);
            this.panel1.TabIndex = 0;
            // 
            // btnSearchStage
            // 
            this.btnSearchStage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearchStage.Location = new System.Drawing.Point(238, 34);
            this.btnSearchStage.Name = "btnSearchStage";
            this.btnSearchStage.Size = new System.Drawing.Size(26, 23);
            this.btnSearchStage.TabIndex = 17;
            this.btnSearchStage.Text = "..";
            this.btnSearchStage.UseVisualStyleBackColor = true;
            this.btnSearchStage.Click += new System.EventHandler(this.btnSearchStage_Click);
            // 
            // btnSearchProcess
            // 
            this.btnSearchProcess.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearchProcess.Location = new System.Drawing.Point(482, 34);
            this.btnSearchProcess.Name = "btnSearchProcess";
            this.btnSearchProcess.Size = new System.Drawing.Size(26, 23);
            this.btnSearchProcess.TabIndex = 16;
            this.btnSearchProcess.Text = "..";
            this.btnSearchProcess.UseVisualStyleBackColor = true;
            this.btnSearchProcess.Click += new System.EventHandler(this.btnSearchProcess_Click);
            // 
            // editProcess
            // 
            this.editProcess.Location = new System.Drawing.Point(358, 35);
            this.editProcess.Name = "editProcess";
            this.editProcess.Size = new System.Drawing.Size(121, 21);
            this.editProcess.TabIndex = 8;
            // 
            // editStage
            // 
            this.editStage.Location = new System.Drawing.Point(115, 35);
            this.editStage.Name = "editStage";
            this.editStage.Size = new System.Drawing.Size(121, 21);
            this.editStage.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(273, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Process Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Stage Name";
            // 
            // labLine
            // 
            this.labLine.AutoSize = true;
            this.labLine.ForeColor = System.Drawing.Color.Maroon;
            this.labLine.Location = new System.Drawing.Point(115, 9);
            this.labLine.Name = "labLine";
            this.labLine.Size = new System.Drawing.Size(23, 12);
            this.labLine.TabIndex = 4;
            this.labLine.Text = "N/A";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Production Line";
            // 
            // combTerminalType
            // 
            this.combTerminalType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combTerminalType.FormattingEnabled = true;
            this.combTerminalType.Location = new System.Drawing.Point(115, 66);
            this.combTerminalType.Name = "combTerminalType";
            this.combTerminalType.Size = new System.Drawing.Size(121, 20);
            this.combTerminalType.TabIndex = 2;
            this.combTerminalType.SelectedIndexChanged += new System.EventHandler(this.combTerminalType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Terminal Type";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(358, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Query";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gvData
            // 
            this.gvData.AllowUserToAddRows = false;
            this.gvData.AllowUserToDeleteRows = false;
            this.gvData.BackgroundColor = System.Drawing.Color.White;
            this.gvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STAGE_NAME,
            this.PROCESS_NAME,
            this.TERMINAL_NAME,
            this.TERMINAL_TYPE,
            this.SMT_SIDE,
            this.MACHINE_CODE});
            this.gvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvData.Location = new System.Drawing.Point(0, 93);
            this.gvData.Name = "gvData";
            this.gvData.ReadOnly = true;
            this.gvData.RowHeadersWidth = 25;
            this.gvData.RowTemplate.Height = 24;
            this.gvData.Size = new System.Drawing.Size(651, 346);
            this.gvData.TabIndex = 2;
            // 
            // STAGE_NAME
            // 
            this.STAGE_NAME.HeaderText = "Stage Name";
            this.STAGE_NAME.Name = "STAGE_NAME";
            this.STAGE_NAME.ReadOnly = true;
            // 
            // PROCESS_NAME
            // 
            this.PROCESS_NAME.HeaderText = "Process Name";
            this.PROCESS_NAME.Name = "PROCESS_NAME";
            this.PROCESS_NAME.ReadOnly = true;
            // 
            // TERMINAL_NAME
            // 
            this.TERMINAL_NAME.HeaderText = "Terminal Name";
            this.TERMINAL_NAME.Name = "TERMINAL_NAME";
            this.TERMINAL_NAME.ReadOnly = true;
            // 
            // TERMINAL_TYPE
            // 
            this.TERMINAL_TYPE.HeaderText = "Terminal Type";
            this.TERMINAL_TYPE.Name = "TERMINAL_TYPE";
            this.TERMINAL_TYPE.ReadOnly = true;
            // 
            // SMT_SIDE
            // 
            this.SMT_SIDE.HeaderText = "SMT Side";
            this.SMT_SIDE.Name = "SMT_SIDE";
            this.SMT_SIDE.ReadOnly = true;
            // 
            // MACHINE_CODE
            // 
            this.MACHINE_CODE.HeaderText = "Machine";
            this.MACHINE_CODE.Name = "MACHINE_CODE";
            this.MACHINE_CODE.ReadOnly = true;
            // 
            // fMachineLink
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 439);
            this.Controls.Add(this.gvData);
            this.Controls.Add(this.panel1);
            this.Name = "fMachineLink";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Terminal Type";
            this.Load += new System.EventHandler(this.fMachineLink_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gvData;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox combTerminalType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labLine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox editProcess;
        private System.Windows.Forms.TextBox editStage;
        private System.Windows.Forms.Button btnSearchStage;
        private System.Windows.Forms.Button btnSearchProcess;
        private System.Windows.Forms.DataGridViewTextBoxColumn STAGE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn PROCESS_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn TERMINAL_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn TERMINAL_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SMT_SIDE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MACHINE_CODE;
    }
}