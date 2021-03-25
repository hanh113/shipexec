using SajetClass;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRole
{
	public class fModule : Form
	{
		private IContainer components = null;

		private Panel panel1;

		private Label label1;

		private Panel panel2;

		private Button bbtnCancel;

		private Button bbtnOK;

		private SplitContainer splitContainer1;

		private TreeView TreeViewAll;

		private TreeView TreeViewSelect;

		private ImageList imageList1;

		private ListView LVData;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private Panel panel4;

		private ComboBox combAuth;

		private Label label3;

		private ContextMenuStrip popMenu2;

		private ToolStripMenuItem deleteToolStripMenuItem;

		private ToolStripMenuItem collapseToolStripMenuItem;

		private ToolStripMenuItem expandToolStripMenuItem;

		private ToolStripComboBox menuitemCombAuth;

		private ToolStripMenuItem AlltoolStripMenuItem;

		private ToolStripMenuItem minToolStripMenuItem;

		private ToolStripMenuItem maxToolStripMenuItem;

		private Label LabRoleName;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripSeparator toolStripSeparator1;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader6;

		private GroupBox groupBox1;

		private GroupBox groupBox2;

		public string g_sRoleID;

		public string g_sRoleName;

		public string sSQL;

		private StringCollection sListAuthEng = new StringCollection();

		private StringCollection sListAuthCht = new StringCollection();

		private string g_sField;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fModule));
            this.panel1 = new System.Windows.Forms.Panel();
            this.LabRoleName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bbtnCancel = new System.Windows.Forms.Button();
            this.bbtnOK = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LVData = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TreeViewAll = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TreeViewSelect = new System.Windows.Forms.TreeView();
            this.popMenu2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AlltoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemCombAuth = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.collapseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.combAuth = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.popMenu2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.LabRoleName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(692, 34);
            this.panel1.TabIndex = 0;
            // 
            // LabRoleName
            // 
            this.LabRoleName.AutoSize = true;
            this.LabRoleName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.LabRoleName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.LabRoleName.Location = new System.Drawing.Point(83, 13);
            this.LabRoleName.Name = "LabRoleName";
            this.LabRoleName.Size = new System.Drawing.Size(37, 16);
            this.LabRoleName.TabIndex = 1;
            this.LabRoleName.Text = "Role";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Role Name";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bbtnCancel);
            this.panel2.Controls.Add(this.bbtnOK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 477);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(692, 31);
            this.panel2.TabIndex = 1;
            // 
            // bbtnCancel
            // 
            this.bbtnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.bbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bbtnCancel.Location = new System.Drawing.Point(443, 3);
            this.bbtnCancel.Name = "bbtnCancel";
            this.bbtnCancel.Size = new System.Drawing.Size(75, 25);
            this.bbtnCancel.TabIndex = 1;
            this.bbtnCancel.Text = "Cancel";
            this.bbtnCancel.UseVisualStyleBackColor = false;
            // 
            // bbtnOK
            // 
            this.bbtnOK.BackColor = System.Drawing.SystemColors.Control;
            this.bbtnOK.Location = new System.Drawing.Point(362, 3);
            this.bbtnOK.Name = "bbtnOK";
            this.bbtnOK.Size = new System.Drawing.Size(75, 25);
            this.bbtnOK.TabIndex = 0;
            this.bbtnOK.Text = "Apply";
            this.bbtnOK.UseVisualStyleBackColor = false;
            this.bbtnOK.Click += new System.EventHandler(this.bbtnOK_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 34);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Size = new System.Drawing.Size(692, 443);
            this.splitContainer1.SplitterDistance = 278;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.LVData);
            this.groupBox1.Controls.Add(this.TreeViewAll);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 443);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "All Item";
            // 
            // LVData
            // 
            this.LVData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.LVData.Location = new System.Drawing.Point(32, 103);
            this.LVData.Name = "LVData";
            this.LVData.Size = new System.Drawing.Size(222, 169);
            this.LVData.TabIndex = 3;
            this.LVData.UseCompatibleStateImageBehavior = false;
            this.LVData.View = System.Windows.Forms.View.Details;
            this.LVData.Visible = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Program";
            this.columnHeader1.Width = 126;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Function";
            this.columnHeader2.Width = 144;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Auth";
            this.columnHeader3.Width = 123;
            // 
            // TreeViewAll
            // 
            this.TreeViewAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeViewAll.ImageIndex = 0;
            this.TreeViewAll.ImageList = this.imageList1;
            this.TreeViewAll.Location = new System.Drawing.Point(3, 17);
            this.TreeViewAll.Name = "TreeViewAll";
            this.TreeViewAll.SelectedImageIndex = 0;
            this.TreeViewAll.ShowNodeToolTips = true;
            this.TreeViewAll.Size = new System.Drawing.Size(272, 423);
            this.TreeViewAll.TabIndex = 0;
            this.TreeViewAll.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeView_ItemDrag);
            this.TreeViewAll.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeView_DragEnter);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1.bmp");
            this.imageList1.Images.SetKeyName(1, "2.bmp");
            this.imageList1.Images.SetKeyName(2, "role3.bmp");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TreeViewSelect);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(410, 410);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Choose Item";
            // 
            // TreeViewSelect
            // 
            this.TreeViewSelect.AllowDrop = true;
            this.TreeViewSelect.ContextMenuStrip = this.popMenu2;
            this.TreeViewSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeViewSelect.HideSelection = false;
            this.TreeViewSelect.ImageIndex = 0;
            this.TreeViewSelect.ImageList = this.imageList1;
            this.TreeViewSelect.Location = new System.Drawing.Point(3, 17);
            this.TreeViewSelect.Name = "TreeViewSelect";
            this.TreeViewSelect.SelectedImageIndex = 0;
            this.TreeViewSelect.ShowNodeToolTips = true;
            this.TreeViewSelect.Size = new System.Drawing.Size(404, 390);
            this.TreeViewSelect.TabIndex = 1;
            this.TreeViewSelect.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeView_ItemDrag);
            this.TreeViewSelect.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewSelect_AfterSelect);
            this.TreeViewSelect.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeViewSelect_DragDrop);
            this.TreeViewSelect.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeView_DragEnter);
            // 
            // popMenu2
            // 
            this.popMenu2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AlltoolStripMenuItem,
            this.menuitemCombAuth,
            this.toolStripSeparator2,
            this.collapseToolStripMenuItem,
            this.expandToolStripMenuItem,
            this.toolStripSeparator1,
            this.deleteToolStripMenuItem});
            this.popMenu2.Name = "popMenu2";
            this.popMenu2.Size = new System.Drawing.Size(216, 133);
            this.popMenu2.Opening += new System.ComponentModel.CancelEventHandler(this.popMenu2_Opening);
            // 
            // AlltoolStripMenuItem
            // 
            this.AlltoolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.minToolStripMenuItem,
            this.maxToolStripMenuItem});
            this.AlltoolStripMenuItem.Name = "AlltoolStripMenuItem";
            this.AlltoolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.AlltoolStripMenuItem.Text = "Change all Authority to..";
            // 
            // minToolStripMenuItem
            // 
            this.minToolStripMenuItem.Name = "minToolStripMenuItem";
            this.minToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.minToolStripMenuItem.Text = "Min Level";
            this.minToolStripMenuItem.Click += new System.EventHandler(this.minToolStripMenuItem_Click);
            // 
            // maxToolStripMenuItem
            // 
            this.maxToolStripMenuItem.Name = "maxToolStripMenuItem";
            this.maxToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.maxToolStripMenuItem.Text = "Max Level";
            this.maxToolStripMenuItem.Click += new System.EventHandler(this.maxToolStripMenuItem_Click);
            // 
            // menuitemCombAuth
            // 
            this.menuitemCombAuth.BackColor = System.Drawing.Color.White;
            this.menuitemCombAuth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.menuitemCombAuth.DropDownWidth = 200;
            this.menuitemCombAuth.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.menuitemCombAuth.Name = "menuitemCombAuth";
            this.menuitemCombAuth.Size = new System.Drawing.Size(150, 25);
            this.menuitemCombAuth.SelectedIndexChanged += new System.EventHandler(this.menuitemCombAuth_SelectedIndexChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(212, 6);
            // 
            // collapseToolStripMenuItem
            // 
            this.collapseToolStripMenuItem.Name = "collapseToolStripMenuItem";
            this.collapseToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.collapseToolStripMenuItem.Text = "Collapse";
            this.collapseToolStripMenuItem.Click += new System.EventHandler(this.collapseToolStripMenuItem_Click);
            // 
            // expandToolStripMenuItem
            // 
            this.expandToolStripMenuItem.Name = "expandToolStripMenuItem";
            this.expandToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.expandToolStripMenuItem.Text = "Expand";
            this.expandToolStripMenuItem.Click += new System.EventHandler(this.expandToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(212, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.combAuth);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 410);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(410, 33);
            this.panel4.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Authority";
            // 
            // combAuth
            // 
            this.combAuth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combAuth.FormattingEnabled = true;
            this.combAuth.Location = new System.Drawing.Point(78, 6);
            this.combAuth.Name = "combAuth";
            this.combAuth.Size = new System.Drawing.Size(182, 20);
            this.combAuth.TabIndex = 0;
            this.combAuth.SelectedIndexChanged += new System.EventHandler(this.combAuth_SelectedIndexChanged);
            // 
            // fModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 508);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "fModule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Module";
            this.Load += new System.EventHandler(this.fModule_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.popMenu2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

		}

		public fModule()
		{
			InitializeComponent();
		}

		private void bbtnOK_Click(object sender, EventArgs e)
		{
			sSQL = " Delete SAJET.SYS_ROLE_PRIVILEGE  Where ROLE_ID = '" + g_sRoleID + "' ";
			DataSet dataSet = ClientUtils.ExecuteSQL(sSQL);
			for (int i = 0; i <= TreeViewSelect.Nodes.Count - 1; i++)
			{
				string name = TreeViewSelect.Nodes[i].Name;
				for (int j = 0; j <= TreeViewSelect.Nodes[i].Nodes.Count - 1; j++)
				{
					string name2 = TreeViewSelect.Nodes[i].Nodes[j].Name;
					string text = TreeViewSelect.Nodes[i].Nodes[j].Nodes[0].Tag.ToString();
					sSQL = " Insert Into SAJET.SYS_ROLE_PRIVILEGE  (ROLE_ID,PROGRAM,FUNCTION,AUTHORITYS,UPDATE_USERID)  Values  ('" + g_sRoleID + "','" + name + "','" + name2 + "','" + text + "','" + fMain.g_sUserID + "') ";
					dataSet = ClientUtils.ExecuteSQL(sSQL);
				}
			}
			MessageBox.Show(SajetCommon.SetLanguage("Privilege Apply OK", 1));
			base.DialogResult = DialogResult.OK;
		}

		public void Show_Module_List()
		{
			TreeViewAll.Nodes.Clear();
			LVData.Items.Clear();
			string text = "FUN_ENG";
			string text2 = "FUN_DESC_ENG";
			string text3 = "PROGRAM";
			string text4 = "";
			if (!string.IsNullOrEmpty(g_sField))
			{
				text = "FUN_" + g_sField;
				text2 = "FUN_DESC_" + g_sField;
				text3 = "PROGRAM_" + g_sField;
				text4 = " ,b." + text3 + " ,a." + text + ",a." + text2 + " ";
			}
			string a = "";
			string a2 = "";
			string text5 = "SELECT PARAM_VALUE FROM SAJET.SYS_BASE_PARAM WHERE PROGRAM = 'Data Center' AND PARAM_NAME = 'Role' AND PARAM_TYPE = 'List' AND ROWNUM = 1";
			DataSet dataSet = ClientUtils.ExecuteSQL(text5);
			text5 = ((dataSet.Tables[0].Rows.Count <= 0) ? ("Select a.PROGRAM,a.FUNCTION,a.AUTH_SEQ,a.AUTHORITYS  ,a.FUN_ENG,a.FUN_DESC_ENG " + text4 + " from SAJET.SYS_PROGRAM_FUN a,sajet.sys_program_name b  where a.program = b.program  and a.ENABLED = 'Y'  and b.ENABLED = 'Y'  Group By a.PROGRAM,a.FUNCTION,a.AUTH_SEQ,a.AUTHORITYS,a.FUN_ENG,a.FUN_DESC_ENG " + text4 + " Order by a.PROGRAM,a.FUNCTION,a.AUTH_SEQ,a.AUTHORITYS ") : dataSet.Tables[0].Rows[0][0].ToString().Replace("@", text4));
			dataSet = ClientUtils.ExecuteSQL(text5);
			for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
			{
				string text6 = dataSet.Tables[0].Rows[i]["PROGRAM"].ToString();
				string text7 = dataSet.Tables[0].Rows[i]["FUNCTION"].ToString();
				string text8 = dataSet.Tables[0].Rows[i]["AUTHORITYS"].ToString();
				string text9 = dataSet.Tables[0].Rows[i]["AUTH_SEQ"].ToString();
				string text10 = dataSet.Tables[0].Rows[i][text3].ToString();
				if (string.IsNullOrEmpty(text10.Trim()))
				{
					text10 = dataSet.Tables[0].Rows[i]["PROGRAM"].ToString();
				}
				string text11 = dataSet.Tables[0].Rows[i][text].ToString();
				if (string.IsNullOrEmpty(text11.Trim()))
				{
					text11 = dataSet.Tables[0].Rows[i]["FUNCTION"].ToString();
				}
				string text12 = dataSet.Tables[0].Rows[i][text2].ToString();
				if (string.IsNullOrEmpty(text12.Trim()))
				{
					text12 = dataSet.Tables[0].Rows[i]["FUN_DESC_ENG"].ToString();
				}
				if (a != text6)
				{
					TreeNode treeNode = new TreeNode();
					treeNode.Text = text10;
					treeNode.Name = text6;
					treeNode.ImageIndex = 0;
					treeNode.SelectedImageIndex = treeNode.ImageIndex;
					TreeViewAll.Nodes.Add(treeNode);
					a2 = "";
				}
				if (a2 != text7)
				{
					TreeNode treeNode = new TreeNode();
					treeNode.Text = text11;
					treeNode.Name = text7;
					treeNode.ImageIndex = 1;
					treeNode.SelectedImageIndex = treeNode.ImageIndex;
					treeNode.ToolTipText = text12;
					TreeViewAll.Nodes[TreeViewAll.Nodes.Count - 1].Nodes.Add(treeNode);
				}
				a = text6;
				a2 = text7;
				LVData.Items.Add(text6);
				LVData.Items[LVData.Items.Count - 1].SubItems.Add(text7);
				LVData.Items[LVData.Items.Count - 1].SubItems.Add(text9);
				LVData.Items[LVData.Items.Count - 1].SubItems.Add(SajetCommon.SetLanguage(text8, 1));
				LVData.Items[LVData.Items.Count - 1].SubItems.Add(text8);
				LVData.Items[LVData.Items.Count - 1].SubItems.Add(text12);
			}
		}

		public void Show_Module_Pri()
		{
			TreeViewSelect.Nodes.Clear();
			string text = "FUN_ENG";
			string text2 = "FUN_DESC_ENG";
			string text3 = "PROGRAM";
			string text4 = "";
			if (!string.IsNullOrEmpty(g_sField))
			{
				text = "FUN_" + g_sField;
				text2 = "FUN_DESC_" + g_sField;
				text3 = "PROGRAM_" + g_sField;
				text4 = " ,c." + text3 + " ,b." + text + ",b." + text2 + " ";
			}
			string a = "";
			string a2 = "";
			string text5 = "SELECT PARAM_VALUE FROM SAJET.SYS_BASE_PARAM WHERE PROGRAM = 'Data Center' AND PARAM_NAME = 'Role' AND PARAM_TYPE = 'Role' AND ROWNUM = 1";
			DataSet dataSet = ClientUtils.ExecuteSQL(text5);
			object[][] array = new object[1][]
			{
				new object[4]
				{
					ParameterDirection.Input,
					OracleType.VarChar,
					"ROLE_ID",
					g_sRoleID
				}
			};
			text5 = ((dataSet.Tables[0].Rows.Count <= 0) ? (" Select a.PROGRAM,a.FUNCTION,a.AUTHORITYS ROLE_AUTHORITYS  ,b.FUN_ENG,b.FUN_DESC_ENG " + text4 + " from SAJET.SYS_ROLE_PRIVILEGE a      ,sajet.sys_program_fun b      ,sajet.sys_program_name c  Where a.Role_id = '" + g_sRoleID + "'  and a.program = b.program  and a.function = b.function  and a.program = c.program  and b.ENABLED = 'Y'  and c.ENABLED = 'Y'  Group By a.PROGRAM,a.FUNCTION,a.AUTHORITYS,b.FUN_ENG,b.FUN_DESC_ENG " + text4 + " Order by a.PROGRAM,a.FUNCTION,a.AUTHORITYS ") : dataSet.Tables[0].Rows[0][0].ToString().Replace("@", text4));
			dataSet = ClientUtils.ExecuteSQL(text5);
			for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
			{
				string text6 = dataSet.Tables[0].Rows[i]["PROGRAM"].ToString();
				string text7 = dataSet.Tables[0].Rows[i]["FUNCTION"].ToString();
				string text8 = dataSet.Tables[0].Rows[i]["ROLE_AUTHORITYS"].ToString();
				string text9 = dataSet.Tables[0].Rows[i][text3].ToString();
				if (string.IsNullOrEmpty(text9.Trim()))
				{
					text9 = dataSet.Tables[0].Rows[i]["PROGRAM"].ToString();
				}
				string text10 = dataSet.Tables[0].Rows[i][text].ToString();
				if (string.IsNullOrEmpty(text10.Trim()))
				{
					text10 = dataSet.Tables[0].Rows[i]["FUNCTION"].ToString();
				}
				string text11 = dataSet.Tables[0].Rows[i][text2].ToString();
				if (string.IsNullOrEmpty(text11.Trim()))
				{
					text11 = dataSet.Tables[0].Rows[i]["FUN_DESC_ENG"].ToString();
				}
				if (a != text6)
				{
					TreeNode treeNode = new TreeNode();
					treeNode.Text = text9;
					treeNode.Name = text6;
					treeNode.ImageIndex = 0;
					treeNode.SelectedImageIndex = treeNode.ImageIndex;
					TreeViewSelect.Nodes.Add(treeNode);
					a2 = "";
				}
				if (a2 != text7)
				{
					TreeNode treeNode = new TreeNode();
					treeNode.Text = text10;
					treeNode.Name = text7;
					treeNode.ImageIndex = 1;
					treeNode.ToolTipText = text11;
					treeNode.SelectedImageIndex = treeNode.ImageIndex;
					TreeViewSelect.Nodes[TreeViewSelect.Nodes.Count - 1].Nodes.Add(treeNode);
				}
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Tag = text8;
				treeNode2.Text = SajetCommon.SetLanguage(text8, 1);
				treeNode2.Name = treeNode2.Text;
				treeNode2.ImageIndex = 2;
				treeNode2.SelectedImageIndex = treeNode2.ImageIndex;
				TreeViewSelect.Nodes[TreeViewSelect.Nodes.Count - 1].LastNode.Nodes.Add(treeNode2);
				a = text6;
				a2 = text7;
			}
		}

		private void fModule_Load(object sender, EventArgs e)
		{
			SajetCommon.SetLanguageControl(this);
			panel2.BackgroundImage = ClientUtils.LoadImage("ImgButton.jpg");
			panel2.BackgroundImageLayout = ImageLayout.Stretch;
			BackgroundImage = ClientUtils.LoadImage("ImgMain.jpg");
			BackgroundImageLayout = ImageLayout.Stretch;
			sSQL = "Select distinct authoritys from sajet.sys_program_fun where authoritys is not null";
			DataSet dataSet = ClientUtils.ExecuteSQL(sSQL);
			for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
			{
				string text = dataSet.Tables[0].Rows[i]["authoritys"].ToString();
				sListAuthEng.Add(text);
				sListAuthCht.Add(SajetCommon.SetLanguage(text, 1));
			}
			string sErrorMsg = "";
			g_sField = SajetCommon.GetSysBaseData("ALL", ClientUtils.fClientLang, ref sErrorMsg);
			LabRoleName.Text = g_sRoleName;
			Show_Module_List();
			Show_Module_Pri();
		}

		private void TreeView_ItemDrag(object sender, ItemDragEventArgs e)
		{
			DoDragDrop(e.Item, DragDropEffects.Move);
		}

		private void TreeView_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
		}

		private void TreeViewSelect_DragDrop(object sender, DragEventArgs e)
		{
			TreeNode treeNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
			TreeViewSelect.Focus();
			if (treeNode.Level == 0)
			{
				TreeNode[] array = TreeViewSelect.Nodes.Find(treeNode.Name, false);
				if (array.Length > 0)
				{
					TreeViewSelect.SelectedNode = array[0];
					return;
				}
				TreeViewSelect.Nodes.Add((TreeNode)treeNode.Clone());
				for (int i = 0; i <= TreeViewSelect.Nodes[TreeViewSelect.Nodes.Count - 1].Nodes.Count - 1; i++)
				{
					string name = treeNode.Name;
					string name2 = treeNode.Nodes[i].Name;
					string text = Get_Default_Auth(name, name2);
					TreeNode treeNode2 = new TreeNode();
					treeNode2.Text = SajetCommon.SetLanguage(text, 1);
					treeNode2.Tag = text;
					treeNode2.Name = treeNode2.Text;
					treeNode2.ImageIndex = 2;
					treeNode2.SelectedImageIndex = treeNode2.ImageIndex;
					TreeViewSelect.Nodes[TreeViewSelect.Nodes.Count - 1].Nodes[i].Nodes.Add(treeNode2);
				}
				TreeViewSelect.SelectedNode = TreeViewSelect.Nodes[TreeViewSelect.Nodes.Count - 1];
			}
			if (treeNode.Level == 1)
			{
				string name = treeNode.Parent.Name;
				string text2 = treeNode.Parent.Text;
				string name2 = treeNode.Name;
				string text3 = treeNode.Text;
				TreeNode[] array2 = TreeViewSelect.Nodes.Find(name, false);
				TreeNode treeNode4;
				if (array2.Length == 0)
				{
					TreeNode treeNode3 = new TreeNode();
					treeNode3.Text = text2;
					treeNode3.Name = name;
					treeNode3.ImageIndex = 0;
					treeNode3.SelectedImageIndex = treeNode3.ImageIndex;
					TreeViewSelect.Nodes.Add(treeNode3);
					treeNode4 = TreeViewSelect.Nodes[TreeViewSelect.Nodes.Count - 1];
				}
				else
				{
					treeNode4 = array2[0];
				}
				TreeViewSelect.SelectedNode = treeNode4;
				if (treeNode4.Nodes.Find(name2, false).Length <= 0)
				{
					TreeNode treeNode5 = new TreeNode();
					treeNode5.Text = text3;
					treeNode5.Name = name2;
					treeNode5.ImageIndex = 1;
					treeNode5.SelectedImageIndex = treeNode5.ImageIndex;
					treeNode4.Nodes.Add(treeNode5);
					string text = Get_Default_Auth(name, name2);
					TreeNode treeNode2 = new TreeNode();
					treeNode2.Text = SajetCommon.SetLanguage(text, 1);
					treeNode2.Tag = text;
					treeNode2.Name = treeNode2.Text;
					treeNode2.ImageIndex = 2;
					treeNode2.SelectedImageIndex = treeNode2.ImageIndex;
					treeNode4.LastNode.Nodes.Add(treeNode2);
				}
			}
		}

		public string Get_Default_Auth(string sPrg, string sFun)
		{
			for (int i = 0; i <= LVData.Items.Count - 1; i++)
			{
				if (LVData.Items[i].Text == sPrg && LVData.Items[i].SubItems[1].Text == sFun)
				{
					return LVData.Items[i].SubItems[4].Text;
				}
			}
			return "";
		}

		private void TreeViewSelect_AfterSelect(object sender, TreeViewEventArgs e)
		{
			combAuth.Items.Clear();
			combAuth.Text = "";
			string text = "";
			string text2 = "";
			string text3 = "";
			switch (TreeViewSelect.SelectedNode.Level)
			{
			default:
				return;
			case 0:
				return;
			case 1:
				text = TreeViewSelect.SelectedNode.Parent.Name;
				text2 = TreeViewSelect.SelectedNode.Name;
				text3 = TreeViewSelect.SelectedNode.Nodes[0].Text;
				break;
			case 2:
				text = TreeViewSelect.SelectedNode.Parent.Parent.Name;
				text2 = TreeViewSelect.SelectedNode.Parent.Name;
				text3 = TreeViewSelect.SelectedNode.Text;
				break;
			}
			menuitemCombAuth.Items.Clear();
			menuitemCombAuth.Text = "";
			for (int i = 0; i <= LVData.Items.Count - 1; i++)
			{
				if (LVData.Items[i].Text == text && LVData.Items[i].SubItems[1].Text == text2)
				{
					combAuth.Items.Add(LVData.Items[i].SubItems[3].Text);
					menuitemCombAuth.Items.Add(LVData.Items[i].SubItems[3].Text);
					if (LVData.Items[i].Text != text || LVData.Items[i].SubItems[1].Text != text2)
					{
						return;
					}
				}
			}
			combAuth.SelectedIndex = combAuth.FindString(text3);
			menuitemCombAuth.SelectedIndex = menuitemCombAuth.FindString(text3);
		}

		private void combAuth_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (TreeViewSelect.SelectedNode != null)
			{
				switch (TreeViewSelect.SelectedNode.Level)
				{
				case 0:
					break;
				case 1:
					TreeViewSelect.SelectedNode.Nodes[0].Text = combAuth.Text;
					TreeViewSelect.SelectedNode.Nodes[0].Tag = sListAuthEng[sListAuthCht.IndexOf(combAuth.Text)].ToString();
					break;
				case 2:
					TreeViewSelect.SelectedNode.Text = combAuth.Text;
					TreeViewSelect.SelectedNode.Tag = sListAuthEng[sListAuthCht.IndexOf(combAuth.Text)].ToString();
					break;
				}
			}
		}

		private void collapseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeViewSelect.CollapseAll();
		}

		private void expandToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeViewSelect.ExpandAll();
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{

			switch (TreeViewSelect.SelectedNode.Level)
			{
			case 0:
				TreeViewSelect.SelectedNode.Remove();
				break;
			case 1:
				if (TreeViewSelect.SelectedNode.Parent.Nodes.Count <= 1)
				{
					TreeViewSelect.SelectedNode.Parent.Remove();
				}
				else
				{
					TreeViewSelect.SelectedNode.Remove();
				}
				break;
			case 2:
				if (TreeViewSelect.SelectedNode.Parent.Parent.Nodes.Count <= 1)
				{
					TreeViewSelect.SelectedNode.Parent.Parent.Remove();
				}
				else
				{
					TreeViewSelect.SelectedNode.Parent.Remove();
				}
				break;
			}
		}

		private void menuitemCombAuth_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (TreeViewSelect.SelectedNode != null)
			{
				switch (TreeViewSelect.SelectedNode.Level)
				{
				case 0:
					return;
				case 1:
					TreeViewSelect.SelectedNode.Nodes[0].Text = menuitemCombAuth.Text;
					TreeViewSelect.SelectedNode.Nodes[0].Tag = sListAuthEng[sListAuthCht.IndexOf(menuitemCombAuth.Text)].ToString();
					break;
				case 2:
					TreeViewSelect.SelectedNode.Text = menuitemCombAuth.Text;
					TreeViewSelect.SelectedNode.Tag = sListAuthEng[sListAuthCht.IndexOf(menuitemCombAuth.Text)].ToString();
					break;
				}
				combAuth.SelectedIndex = combAuth.FindString(menuitemCombAuth.Text);
				popMenu2.Close();
			}
		}

		private void popMenu2_Opening(object sender, CancelEventArgs e)
		{
			if (TreeViewSelect.SelectedNode != null)
			{
				if (TreeViewSelect.SelectedNode.Level == 0)
				{
					menuitemCombAuth.Visible = false;
					AlltoolStripMenuItem.Visible = true;
				}
				else
				{
					menuitemCombAuth.Visible = true;
					AlltoolStripMenuItem.Visible = false;
				}
			}
		}

		private void minToolStripMenuItem_Click(object sender, EventArgs e)
		{
			for (int i = 0; i <= TreeViewSelect.SelectedNode.GetNodeCount(false) - 1; i++)
			{
				string name = TreeViewSelect.SelectedNode.Name;
				string name2 = TreeViewSelect.SelectedNode.Nodes[i].Name;
				string text = TreeViewSelect.SelectedNode.Nodes[i].Nodes[0].Text;
				for (int j = 0; j <= LVData.Items.Count - 1; j++)
				{
					if (LVData.Items[j].Text == name && LVData.Items[j].SubItems[1].Text == name2)
					{
						TreeViewSelect.SelectedNode.Nodes[i].Nodes[0].Text = LVData.Items[j].SubItems[3].Text;
						TreeViewSelect.SelectedNode.Nodes[i].Nodes[0].Tag = LVData.Items[j].SubItems[4].Text;
						break;
					}
				}
			}
		}

		private void maxToolStripMenuItem_Click(object sender, EventArgs e)
		{
			for (int i = 0; i <= TreeViewSelect.SelectedNode.GetNodeCount(false) - 1; i++)
			{
				string name = TreeViewSelect.SelectedNode.Name;
				string name2 = TreeViewSelect.SelectedNode.Nodes[i].Name;
				string text = TreeViewSelect.SelectedNode.Nodes[i].Nodes[0].Text;
				for (int num = LVData.Items.Count - 1; num >= 0; num--)
				{
					if (LVData.Items[num].Text == name && LVData.Items[num].SubItems[1].Text == name2)
					{
						TreeViewSelect.SelectedNode.Nodes[i].Nodes[0].Text = LVData.Items[num].SubItems[3].Text;
						TreeViewSelect.SelectedNode.Nodes[i].Nodes[0].Tag = LVData.Items[num].SubItems[4].Text;
						break;
					}
				}
			}
		}
	}
}
