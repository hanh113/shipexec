using SajetClass;
using SajetTable;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRole
{
	public class fData : Form
	{
		private IContainer components = null;

		private Panel panelControl;

		private Panel panel2;

		private Button btnCancel;

		private Button btnOK;

		private TextBox editDesc;

		private TextBox editCode;

		private Label LabName;

		private Label LabCode;

		private fMain fMainControl;

		public string g_sUpdateType;

		public string g_sformText;

		public string g_sKeyID;

		public DataGridViewRow dataCurrentRow;

		private string sSQL;

		private DataSet dsTemp;

		private bool bAppendSucess = false;

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
            this.panelControl = new System.Windows.Forms.Panel();
            this.editDesc = new System.Windows.Forms.TextBox();
            this.editCode = new System.Windows.Forms.TextBox();
            this.LabName = new System.Windows.Forms.Label();
            this.LabCode = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.panelControl.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.AutoScroll = true;
            this.panelControl.BackColor = System.Drawing.Color.Transparent;
            this.panelControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelControl.Controls.Add(this.editDesc);
            this.panelControl.Controls.Add(this.editCode);
            this.panelControl.Controls.Add(this.LabName);
            this.panelControl.Controls.Add(this.LabCode);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(415, 165);
            this.panelControl.TabIndex = 0;
            // 
            // editDesc
            // 
            this.editDesc.Location = new System.Drawing.Point(135, 48);
            this.editDesc.Name = "editDesc";
            this.editDesc.Size = new System.Drawing.Size(145, 21);
            this.editDesc.TabIndex = 1;
            // 
            // editCode
            // 
            this.editCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.editCode.Location = new System.Drawing.Point(135, 20);
            this.editCode.Name = "editCode";
            this.editCode.Size = new System.Drawing.Size(145, 21);
            this.editCode.TabIndex = 0;
            // 
            // LabName
            // 
            this.LabName.AutoSize = true;
            this.LabName.BackColor = System.Drawing.Color.Transparent;
            this.LabName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.LabName.Location = new System.Drawing.Point(36, 51);
            this.LabName.Name = "LabName";
            this.LabName.Size = new System.Drawing.Size(76, 16);
            this.LabName.TabIndex = 1;
            this.LabName.Text = "Description";
            // 
            // LabCode
            // 
            this.LabCode.AutoSize = true;
            this.LabCode.BackColor = System.Drawing.Color.Transparent;
            this.LabCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.LabCode.Location = new System.Drawing.Point(36, 23);
            this.LabCode.Name = "LabCode";
            this.LabCode.Size = new System.Drawing.Size(77, 16);
            this.LabCode.TabIndex = 0;
            this.LabCode.Text = "Role Name";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 165);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(415, 33);
            this.panel2.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(253, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.Location = new System.Drawing.Point(172, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // fData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 198);
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.panel2);
            this.Name = "fData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modify";
            this.Load += new System.EventHandler(this.fData_Load);
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		public fData()
		{
			InitializeComponent();
		}

		public fData(fMain f)
		{
			InitializeComponent();
			fMainControl = f;
		}

		private void fData_Load(object sender, EventArgs e)
		{
			SajetCommon.SetLanguageControl(this);
			panel2.BackgroundImage = ClientUtils.LoadImage("ImgButton.jpg");
			panel2.BackgroundImageLayout = ImageLayout.Stretch;
			BackgroundImage = ClientUtils.LoadImage("ImgMain.jpg");
			BackgroundImageLayout = ImageLayout.Stretch;
			Text = g_sformText;
			if (g_sUpdateType == "MODIFY")
			{
				g_sKeyID = dataCurrentRow.Cells[TableDefine.gsDef_KeyField].Value.ToString();
				editCode.Text = dataCurrentRow.Cells["ROLE_NAME"].Value.ToString();
				editDesc.Text = dataCurrentRow.Cells["ROLE_DESC"].Value.ToString();
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			for (int i = 0; i <= panelControl.Controls.Count - 1; i++)
			{
				if (panelControl.Controls[i] is TextBox)
				{
					panelControl.Controls[i].Text = panelControl.Controls[i].Text.Trim();
				}
			}
			if (editCode.Text == "")
			{
				string text = LabCode.Text;
				string sKeyMsg = SajetCommon.SetLanguage("Data is null", 2) + Environment.NewLine + text;
				SajetCommon.Show_Message(sKeyMsg, 0);
				editCode.Focus();
				editCode.SelectAll();
			}
			else
			{
				sSQL = " Select * from " + TableDefine.gsDef_Table + "  Where ROLE_NAME = '" + editCode.Text + "' ";
				if (g_sUpdateType == "MODIFY")
				{
					sSQL = sSQL + " and role_id <> '" + g_sKeyID + "'";
				}
				dsTemp = ClientUtils.ExecuteSQL(sSQL);
				if (dsTemp.Tables[0].Rows.Count > 0)
				{
					string text = LabCode.Text + " : " + editCode.Text;
					string sKeyMsg = SajetCommon.SetLanguage("Data Duplicate", 2) + Environment.NewLine + text;
					SajetCommon.Show_Message(sKeyMsg, 0);
					editCode.Focus();
					editCode.SelectAll();
				}
				else
				{
					try
					{
						if (g_sUpdateType == "APPEND")
						{
							AppendData();
							bAppendSucess = true;
							if (fMainControl != null)
							{
								fMainControl.ShowData();
							}
							string sKeyMsg = SajetCommon.SetLanguage("Data Append OK", 2) + " !" + Environment.NewLine + SajetCommon.SetLanguage("Append Other Data", 2) + " ?";
							if (SajetCommon.Show_Message(sKeyMsg, 2) == DialogResult.Yes)
							{
								ClearData();
								editCode.Focus();
							}
							else
							{
								base.DialogResult = DialogResult.OK;
							}
						}
						else if (g_sUpdateType == "MODIFY")
						{
							ModifyData();
							base.DialogResult = DialogResult.OK;
						}
					}
					catch (Exception ex)
					{
						SajetCommon.Show_Message("Exception : " + ex.Message, 0);
					}
				}
			}
		}

		private void AppendData()
		{
			string maxID = SajetCommon.GetMaxID("SAJET.SYS_ROLE", "ROLE_ID", 8);
			object[][] array = new object[4][];
			sSQL = " Insert into SAJET.SYS_ROLE  (ROLE_ID,ROLE_NAME,ROLE_DESC,ENABLED,UPDATE_USERID,UPDATE_TIME)  Values  (:ROLE_ID,:ROLE_NAME,:ROLE_DESC,'Y',:UPDATE_USERID,SYSDATE) ";
			array[0] = new object[4]
			{
				ParameterDirection.Input,
				OracleType.VarChar,
				"ROLE_ID",
				maxID
			};
			array[1] = new object[4]
			{
				ParameterDirection.Input,
				OracleType.VarChar,
				"ROLE_NAME",
				editCode.Text
			};
			array[2] = new object[4]
			{
				ParameterDirection.Input,
				OracleType.VarChar,
				"ROLE_DESC",
				editDesc.Text
			};
			array[3] = new object[4]
			{
				ParameterDirection.Input,
				OracleType.VarChar,
				"UPDATE_USERID",
				fMain.g_sUserID
			};
			dsTemp = ClientUtils.ExecuteSQL(sSQL, array);
			fMain.CopyToHistory(maxID);
		}

		private void ModifyData()
		{
			object[][] array = new object[4][];
			sSQL = " Update SAJET.SYS_ROLE  set ROLE_NAME = :ROLE_NAME     ,ROLE_DESC = :ROLE_DESC     ,UPDATE_USERID = :UPDATE_USERID     ,UPDATE_TIME = SYSDATE  where ROLE_ID = :ROLE_ID ";
			array[0] = new object[4]
			{
				ParameterDirection.Input,
				OracleType.VarChar,
				"ROLE_NAME",
				editCode.Text
			};
			array[1] = new object[4]
			{
				ParameterDirection.Input,
				OracleType.VarChar,
				"ROLE_DESC",
				editDesc.Text
			};
			array[2] = new object[4]
			{
				ParameterDirection.Input,
				OracleType.VarChar,
				"UPDATE_USERID",
				fMain.g_sUserID
			};
			array[3] = new object[4]
			{
				ParameterDirection.Input,
				OracleType.VarChar,
				"ROLE_ID",
				g_sKeyID
			};
			dsTemp = ClientUtils.ExecuteSQL(sSQL, array);
			fMain.CopyToHistory(g_sKeyID);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (bAppendSucess)
			{
				base.DialogResult = DialogResult.OK;
			}
		}

		private void ClearData()
		{
			for (int i = 0; i <= panelControl.Controls.Count - 1; i++)
			{
				if (panelControl.Controls[i] is TextBox)
				{
					panelControl.Controls[i].Text = "";
				}
				else if (panelControl.Controls[i] is ComboBox)
				{
					((ComboBox)panelControl.Controls[i]).SelectedIndex = -1;
				}
			}
		}
	}
}
