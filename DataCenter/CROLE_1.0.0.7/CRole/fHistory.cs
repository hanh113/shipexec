using SajetClass;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CRole
{
	public class fHistory : Form
	{
		private IContainer components = null;

		public DataGridView dgvHistory;

		public fHistory()
		{
			InitializeComponent();
		}

		private void fHistory_Load(object sender, EventArgs e)
		{
			SajetCommon.SetLanguageControl(this);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
			dgvHistory = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
			SuspendLayout();
			dgvHistory.AllowUserToAddRows = false;
			dgvHistory.AllowUserToDeleteRows = false;
			dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			dgvHistory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			dgvHistory.BackgroundColor = System.Drawing.Color.White;
			dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
			dgvHistory.Location = new System.Drawing.Point(0, 0);
			dgvHistory.Name = "dgvHistory";
			dgvHistory.ReadOnly = true;
			dgvHistory.RowTemplate.Height = 24;
			dgvHistory.Size = new System.Drawing.Size(692, 462);
			dgvHistory.TabIndex = 0;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(692, 462);
			base.Controls.Add(dgvHistory);
			base.Name = "fHistory";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "History";
			base.Load += new System.EventHandler(fHistory_Load);
			((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
			ResumeLayout(false);
		}
	}
}
