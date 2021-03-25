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
			InitializeComponent();
			txtMessage.Text = errMessage;
			txtstackTrace.Text = stackTrace;
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
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void fError_Load(object sender, EventArgs e)
		{
			string text = "Default";
			if (Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Chroma", "Skin", "Default") != null)
			{
				text = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Chroma", "Skin", "Default");
			}
			object[] args = new object[6]
			{
				Application.StartupPath,
				Path.DirectorySeparatorChar,
				"skin",
				Path.DirectorySeparatorChar,
				text,
				"\\Login.jpg"
			};
			string text2 = string.Concat(args);
			if (File.Exists(text2))
			{
				BackgroundImage = Image.FromFile(text2);
			}
		}

		private void InitializeComponent()
		{
			button1 = new System.Windows.Forms.Button();
			button2 = new System.Windows.Forms.Button();
			panel1 = new System.Windows.Forms.Panel();
			label1 = new System.Windows.Forms.Label();
			txtMessage = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			txtstackTrace = new System.Windows.Forms.TextBox();
			panel1.SuspendLayout();
			SuspendLayout();
			button1.ForeColor = System.Drawing.SystemColors.ControlText;
			button1.Location = new System.Drawing.Point(428, 3);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(75, 23);
			button1.TabIndex = 2;
			button1.Text = "Exit";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			button2.DialogResult = System.Windows.Forms.DialogResult.Abort;
			button2.ForeColor = System.Drawing.SystemColors.ControlText;
			button2.Location = new System.Drawing.Point(347, 3);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(75, 23);
			button2.TabIndex = 0;
			button2.Text = "Continue";
			button2.UseVisualStyleBackColor = true;
			panel1.BackColor = System.Drawing.Color.Transparent;
			panel1.Controls.Add(button2);
			panel1.Controls.Add(button1);
			panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			panel1.Location = new System.Drawing.Point(0, 240);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(515, 28);
			panel1.TabIndex = 6;
			label1.AutoSize = true;
			label1.BackColor = System.Drawing.Color.Transparent;
			label1.Dock = System.Windows.Forms.DockStyle.Top;
			label1.Location = new System.Drawing.Point(0, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(55, 15);
			label1.TabIndex = 3;
			label1.Text = "Message";
			txtMessage.AcceptsReturn = true;
			txtMessage.BackColor = System.Drawing.Color.White;
			txtMessage.Dock = System.Windows.Forms.DockStyle.Top;
			txtMessage.Location = new System.Drawing.Point(0, 15);
			txtMessage.Multiline = true;
			txtMessage.Name = "txtMessage";
			txtMessage.ReadOnly = true;
			txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			txtMessage.Size = new System.Drawing.Size(515, 76);
			txtMessage.TabIndex = 1;
			txtMessage.TabStop = false;
			label2.AutoSize = true;
			label2.BackColor = System.Drawing.Color.Transparent;
			label2.Dock = System.Windows.Forms.DockStyle.Top;
			label2.Location = new System.Drawing.Point(0, 91);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(74, 15);
			label2.TabIndex = 8;
			label2.Text = "Stack Trace";
			txtstackTrace.AcceptsReturn = true;
			txtstackTrace.AcceptsTab = true;
			txtstackTrace.BackColor = System.Drawing.Color.White;
			txtstackTrace.Dock = System.Windows.Forms.DockStyle.Fill;
			txtstackTrace.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			txtstackTrace.Location = new System.Drawing.Point(0, 106);
			txtstackTrace.Multiline = true;
			txtstackTrace.Name = "txtstackTrace";
			txtstackTrace.ReadOnly = true;
			txtstackTrace.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			txtstackTrace.Size = new System.Drawing.Size(515, 134);
			txtstackTrace.TabIndex = 9;
			txtstackTrace.TabStop = false;
			BackColor = System.Drawing.SystemColors.Control;
			BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			base.ClientSize = new System.Drawing.Size(515, 268);
			base.Controls.Add(txtstackTrace);
			base.Controls.Add(label2);
			base.Controls.Add(txtMessage);
			base.Controls.Add(label1);
			base.Controls.Add(panel1);
			Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "fError";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "Exception";
			base.Load += new System.EventHandler(fError_Load);
			panel1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
