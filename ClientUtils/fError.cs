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
            this.button1 = new Button();
            this.button2 = new Button();
            this.panel1 = new Panel();
            this.label1 = new Label();
            this.txtMessage = new TextBox();
            this.label2 = new Label();
            this.txtstackTrace = new TextBox();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.button1.ForeColor = SystemColors.ControlText;
            this.button1.Location = new Point(428, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.button2.ForeColor = SystemColors.ControlText;
            this.button2.Location = new Point(347, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Continue";
            this.button2.UseVisualStyleBackColor = true;
            this.panel1.BackColor = Color.Transparent;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 240);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(515, 28);
            this.panel1.TabIndex = 6;
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.Transparent;
            this.label1.Dock = DockStyle.Top;
            this.label1.Location = new Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Message";
            this.txtMessage.AcceptsReturn = true;
            this.txtMessage.BackColor = Color.White;
            this.txtMessage.Dock = DockStyle.Top;
            this.txtMessage.Location = new Point(0, 15);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = ScrollBars.Both;
            this.txtMessage.Size = new System.Drawing.Size(515, 76);
            this.txtMessage.TabIndex = 1;
            this.txtMessage.TabStop = false;
            this.label2.AutoSize = true;
            this.label2.BackColor = Color.Transparent;
            this.label2.Dock = DockStyle.Top;
            this.label2.Location = new Point(0, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Stack Trace";
            this.txtstackTrace.AcceptsReturn = true;
            this.txtstackTrace.AcceptsTab = true;
            this.txtstackTrace.BackColor = Color.White;
            this.txtstackTrace.Dock = DockStyle.Fill;
            this.txtstackTrace.Font = new System.Drawing.Font("新細明體", 9f, FontStyle.Regular, GraphicsUnit.Point, 136);
            this.txtstackTrace.Location = new Point(0, 106);
            this.txtstackTrace.Multiline = true;
            this.txtstackTrace.Name = "txtstackTrace";
            this.txtstackTrace.ReadOnly = true;
            this.txtstackTrace.ScrollBars = ScrollBars.Both;
            this.txtstackTrace.Size = new System.Drawing.Size(515, 134);
            this.txtstackTrace.TabIndex = 9;
            this.txtstackTrace.TabStop = false;
            this.BackColor = SystemColors.Control;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            base.ClientSize = new System.Drawing.Size(515, 268);
            base.Controls.Add(this.txtstackTrace);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtMessage);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("新細明體", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 136);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            base.Name = "fError";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Exception";
            base.Load += new EventHandler(this.fError_Load);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}