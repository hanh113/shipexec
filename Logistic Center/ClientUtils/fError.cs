using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ClientUtilsDll
{
    public class fError : Form
    {
        private IContainer components = null;

        private Button button1;

        private Button button2;

        private Panel panel1;

        private Label label1;

        private TextBox txtMessage;

        private Label label2;

        private TextBox txtstackTrace;

        public fError(string errMessage, string stackTrace)
        {
            this.InitializeComponent();
            this.txtMessage.Text = errMessage;
            this.txtstackTrace.Text = stackTrace;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ClientUtils.Logout();
            }
            catch
            {
            }
            Application.Exit();
        }

        protected override void Dispose(bool disposing)
        {
            if ((!disposing ? false : this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void fError_Load(object sender, EventArgs e)
        {
            string value = "Default";
            //if (Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Chroma", "Skin", "Default") != null)
            //{
            //    value = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Chroma", "Skin", "Default");
            //}
            object[] startupPath = new object[] { Application.StartupPath, Path.DirectorySeparatorChar, "skin", Path.DirectorySeparatorChar, value, "\\Login.jpg" };
            string str = string.Concat(startupPath);
            if (File.Exists(str))
            {
                this.BackgroundImage = Image.FromFile(str);
            }
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtstackTrace = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(428, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.button2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button2.Location = new System.Drawing.Point(347, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Continue";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 240);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(515, 28);
            this.panel1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Message";
            // 
            // txtMessage
            // 
            this.txtMessage.AcceptsReturn = true;
            this.txtMessage.BackColor = System.Drawing.Color.White;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtMessage.Location = new System.Drawing.Point(0, 29);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessage.Size = new System.Drawing.Size(515, 76);
            this.txtMessage.TabIndex = 1;
            this.txtMessage.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 29);
            this.label2.TabIndex = 8;
            this.label2.Text = "Stack Trace";
            // 
            // txtstackTrace
            // 
            this.txtstackTrace.AcceptsReturn = true;
            this.txtstackTrace.AcceptsTab = true;
            this.txtstackTrace.BackColor = System.Drawing.Color.White;
            this.txtstackTrace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtstackTrace.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtstackTrace.Location = new System.Drawing.Point(0, 134);
            this.txtstackTrace.Multiline = true;
            this.txtstackTrace.Name = "txtstackTrace";
            this.txtstackTrace.ReadOnly = true;
            this.txtstackTrace.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtstackTrace.Size = new System.Drawing.Size(515, 106);
            this.txtstackTrace.TabIndex = 9;
            this.txtstackTrace.TabStop = false;
            // 
            // fError
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(515, 268);
            this.Controls.Add(this.txtstackTrace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "fError";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exception";
            this.Load += new System.EventHandler(this.fError_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}