using ExportExcel;
using MESGridView;
using SajetClass;
using SajetTable;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CRole
{
	public class fMain : Form
	{
		private Cache memoryCache;

		private string sSQL;

		public static string g_sUserID;

		public string g_sProgram;

		public string g_sFunction;

		public string g_sOrderField;

		private string g_sDataSQL;

		private IContainer components = null;

		private DataGridView gvData;

		private BindingNavigator bindingNavigator1;

		private ToolStripButton btnAppend;

		private ToolStripLabel bindingNavigatorCountItem;

		private ToolStripButton btnDelete;

		private ToolStripButton bindingNavigatorMoveFirstItem;

		private ToolStripButton bindingNavigatorMovePreviousItem;

		private ToolStripSeparator bindingNavigatorSeparator;

		private ToolStripTextBox bindingNavigatorPositionItem;

		private ToolStripSeparator bindingNavigatorSeparator1;

		private ToolStripButton bindingNavigatorMoveNextItem;

		private ToolStripButton bindingNavigatorMoveLastItem;

		private ToolStripSeparator bindingNavigatorSeparator2;

		private ToolStripButton btnModify;

		private ToolStripButton btnDisabled;

		private ToolStripButton btnEnabled;

		private ToolStripComboBox combShow;

		private Panel panel1;

		private TextBox editFilter;

		private ComboBox combFilter;

		private ComboBox combFilterField;

		private ToolStripButton btnExport;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem MenuHistory;

		private SaveFileDialog saveFileDialog1;

		private Label LabFilter;

		private ToolStripMenuItem MenuModule;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton btnModule;

		private CheckBox cbAutoResize;

		public fMain()
		{
			InitializeComponent();
		}

		private void Initial_Form()
		{
			g_sUserID = ClientUtils.UserPara1;
			g_sProgram = ClientUtils.fProgramName;
			g_sFunction = ClientUtils.fFunctionName;
			g_sOrderField = TableDefine.gsDef_OrderField;
			SajetCommon.SetLanguageControl(this);
			panel1.BackgroundImage = ClientUtils.LoadImage("ImgFilter.jpg");
			panel1.BackgroundImageLayout = ImageLayout.Stretch;
		}

		private void fMain_Load(object sender, EventArgs e)
		{
            this.WindowState = FormWindowState.Maximized;
            TableDefine.Initial_Table();
			Initial_Form();
			combShow.SelectedIndex = 0;
			Text = Text + "(" + SajetCommon.g_sFileVersion + ")";
			combFilter.Items.Clear();
			combFilterField.Items.Clear();
			for (int i = 0; i <= TableDefine.tGridField.Length - 1; i++)
			{
				combFilter.Items.Add(TableDefine.tGridField[i].sCaption);
				combFilterField.Items.Add(TableDefine.tGridField[i].sFieldName);
			}
			if (combFilter.Items.Count > 0)
			{
				combFilter.SelectedIndex = 0;
			}
			Check_Privilege();
		}

		private void Check_Privilege()
		{
			string sPrivilege = ClientUtils.GetPrivilege(g_sUserID, g_sFunction, g_sProgram).ToString();
			btnAppend.Enabled = SajetCommon.CheckEnabled("INSERT", sPrivilege);
			btnModify.Enabled = SajetCommon.CheckEnabled("UPDATE", sPrivilege);
			btnEnabled.Enabled = SajetCommon.CheckEnabled("ENABLED", sPrivilege);
			btnDisabled.Enabled = SajetCommon.CheckEnabled("DISABLED", sPrivilege);
			btnDelete.Enabled = SajetCommon.CheckEnabled("DELETE", sPrivilege);
		}

		private void combShow_SelectedIndexChanged(object sender, EventArgs e)
		{
			btnDelete.Visible = (combShow.SelectedIndex == 1);
			btnDisabled.Visible = (combShow.SelectedIndex == 0);
			btnEnabled.Visible = (combShow.SelectedIndex == 1);
			ShowData();
			SetSelectRow(gvData, "", TableDefine.gsDef_KeyField);
		}

		public void ShowData()
		{
			sSQL = "Select * from " + TableDefine.gsDef_Table + " ";
			if (combShow.SelectedIndex == 0)
			{
				sSQL += " where Enabled = 'Y' ";
			}
			else if (combShow.SelectedIndex == 1)
			{
				sSQL += " where Enabled = 'N' ";
			}
			if (combFilter.SelectedIndex > -1 && editFilter.Text.Trim() != "")
			{
				string text = combFilterField.Items[combFilter.SelectedIndex].ToString();
				if (combShow.SelectedIndex <= 1)
				{
					sSQL += " and ";
				}
				else
				{
					sSQL += " where ";
				}
				sSQL = sSQL + text + " like '" + editFilter.Text.Trim() + "%'";
			}
			sSQL = sSQL + " order by " + g_sOrderField;
			g_sDataSQL = sSQL;
            //new DisplayGridView().GetGridView(gvData, sSQL, out memoryCache);
            gvData.DataSource = ClientUtils.ExecuteSQL(g_sDataSQL).Tables[0];
            for (int i = 0; i <= gvData.Columns.Count - 1; i++)
			{
				gvData.Columns[i].Visible = false;
			}
			for (int i = 0; i <= TableDefine.tGridField.Length - 1; i++)
			{
				string sFieldName = TableDefine.tGridField[i].sFieldName;
				if (gvData.Columns.Contains(sFieldName))
				{
					gvData.Columns[sFieldName].HeaderText = TableDefine.tGridField[i].sCaption;
					gvData.Columns[sFieldName].DisplayIndex = i;
					gvData.Columns[sFieldName].Visible = true;
				}
			}
			gvData.Focus();
		}

		private void btnDisable_Click(object sender, EventArgs e)
		{
			if (gvData.Rows.Count != 0 && gvData.CurrentRow != null)
			{
				string text = gvData.CurrentRow.Cells[TableDefine.gsDef_KeyField].Value.ToString();
				string str = "";
				string text2 = "";
				if (sender == btnDisabled)
				{
					str = btnDisabled.Text;
					text2 = "N";
				}
				else if (sender == btnEnabled)
				{
					str = btnEnabled.Text;
					text2 = "Y";
				}
				string str2 = gvData.Columns[TableDefine.gsDef_KeyData].HeaderText + " : " + gvData.CurrentRow.Cells[TableDefine.gsDef_KeyData].Value.ToString();
				string sKeyMsg = str + " ?" + Environment.NewLine + str2;
				if (SajetCommon.Show_Message(sKeyMsg, 2) == DialogResult.Yes)
				{
					sSQL = " Update " + TableDefine.gsDef_Table + "  set Enabled = '" + text2 + "'      ,UPDATE_USERID = '" + g_sUserID + "'      ,UPDATE_TIME = SYSDATE   where " + TableDefine.gsDef_KeyField + " = '" + text + "'";
					ClientUtils.ExecuteSQL(sSQL);
					CopyToHistory(text);
					ShowData();
					SetSelectRow(gvData, "", TableDefine.gsDef_KeyField);
				}
			}
		}

		private void editFilter_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				ShowData();
				SetSelectRow(gvData, "", TableDefine.gsDef_KeyField);
				editFilter.Focus();
			}
		}

		private void btnAppend_Click(object sender, EventArgs e)
		{
			fData fData = new fData(this);
			try
			{
				fData.g_sUpdateType = "APPEND";
				fData.g_sformText = btnAppend.Text;
				if (fData.ShowDialog() == DialogResult.OK)
				{
					ShowData();
					SetSelectRow(gvData, "", TableDefine.gsDef_KeyField);
				}
			}
			finally
			{
				fData.Dispose();
			}
		}

		private void btnModify_Click(object sender, EventArgs e)
		{
			if (gvData.Rows.Count != 0 && gvData.CurrentRow != null)
			{
				fData fData = new fData();
				try
				{
					fData.g_sUpdateType = "MODIFY";
					fData.g_sformText = btnModify.Text;
					fData.dataCurrentRow = gvData.CurrentRow;
					string sPrimaryKey = gvData.CurrentRow.Cells[TableDefine.gsDef_KeyField].Value.ToString();
					if (fData.ShowDialog() == DialogResult.OK)
					{
						ShowData();
						SetSelectRow(gvData, sPrimaryKey, TableDefine.gsDef_KeyField);
					}
				}
				finally
				{
					fData.Dispose();
				}
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (gvData.Rows.Count != 0 && gvData.CurrentRow != null)
			{
				string text = gvData.CurrentRow.Cells[TableDefine.gsDef_KeyField].Value.ToString();
				string str = gvData.Columns[TableDefine.gsDef_KeyData].HeaderText + " : " + gvData.CurrentRow.Cells[TableDefine.gsDef_KeyData].Value.ToString();
				string sKeyMsg = btnDelete.Text + " ?" + Environment.NewLine + str;
				if (SajetCommon.Show_Message(sKeyMsg, 2) == DialogResult.Yes)
				{
					sSQL = " Update " + TableDefine.gsDef_Table + "  set Enabled = 'Drop'      ,UPDATE_USERID = '" + g_sUserID + "'      ,UPDATE_TIME = SYSDATE   where " + TableDefine.gsDef_KeyField + " = '" + text + "'";
					ClientUtils.ExecuteSQL(sSQL);
					CopyToHistory(text);
					sSQL = " Delete " + TableDefine.gsDef_Table + "  where " + TableDefine.gsDef_KeyField + " = '" + text + "'";
					ClientUtils.ExecuteSQL(sSQL);
					ShowData();
					SetSelectRow(gvData, "", TableDefine.gsDef_KeyField);
				}
			}
		}

		public static void CopyToHistory(string sID)
		{
			string text = " Insert into " + TableDefine.gsDef_HTTable + "  Select * from " + TableDefine.gsDef_Table + "  where " + TableDefine.gsDef_KeyField + " = '" + sID + "' ";
			DataSet dataSet = ClientUtils.ExecuteSQL(text);
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			saveFileDialog1.DefaultExt = "xls";
			saveFileDialog1.Filter = "All Files(*.xls)|*.xls";
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				string fileName = saveFileDialog1.FileName;
				CreateExcel createExcel = new CreateExcel(fileName);
				createExcel.ExportToExcel(gvData);
			}
		}

		private void SetSelectRow(DataGridView GridData, string sPrimaryKey, string sField)
		{
			if (GridData.Rows.Count > 0)
			{
				int index = 0;
				string name = GridData.Columns[0].Name;
				for (int i = 0; i <= GridData.Columns.Count - 1; i++)
				{
					if (GridData.Columns[i].Visible)
					{
						name = GridData.Columns[i].Name;
						break;
					}
				}
				if (!string.IsNullOrEmpty(sPrimaryKey))
				{
					string text = "";
					string[] array = sField.Split(',');
					string[] array2 = sPrimaryKey.Split(',');
					for (int j = 0; j <= array.Length - 1; j++)
					{
						text = ((j != 0) ? (text + " and " + array[j].ToString() + "='" + array2[j].ToString() + "' ") : (" Where " + array[j].ToString() + "='" + array2[j].ToString() + "' "));
					}
					string text2 = "select idx from ( Select aa.*,rownum-1 idx from (" + g_sDataSQL + " ) aa ) " + text;
					DataSet dataSet = ClientUtils.ExecuteSQL(text2);
					if (dataSet.Tables[0].Rows.Count > 0)
					{
						index = Convert.ToInt32(dataSet.Tables[0].Rows[0]["idx"].ToString());
					}
				}
				GridData.Focus();
				GridData.CurrentCell = GridData.Rows[index].Cells[name];
				GridData.Rows[index].Selected = true;
			}
		}

		private void gvData_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
		{
			e.Value = memoryCache.RetrieveElement(e.RowIndex, e.ColumnIndex);
		}

		private void MenuHistory_Click(object sender, EventArgs e)
		{
			if (gvData.Rows.Count != 0 && gvData.CurrentRow != null)
			{
				string sID = gvData.CurrentRow.Cells[TableDefine.gsDef_KeyField].Value.ToString();
				string text = TableDefine.History_SQL(sID);
				DataSet dataSet = ClientUtils.ExecuteSQL(text);
				fHistory fHistory = new fHistory();
				fHistory.dgvHistory.DataSource = dataSet;
				fHistory.dgvHistory.DataMember = dataSet.Tables[0].ToString();
				for (int i = 0; i <= fHistory.dgvHistory.Columns.Count - 1; i++)
				{
					string headerText = fHistory.dgvHistory.Columns[i].HeaderText;
					string text2 = "";
					for (int j = 0; j <= gvData.Columns.Count - 1; j++)
					{
						text2 = gvData.Columns[j].Name;
						if (headerText == text2)
						{
							fHistory.dgvHistory.Columns[i].HeaderText = gvData.Columns[j].HeaderText;
							break;
						}
					}
				}
				fHistory.ShowDialog();
				fHistory.Dispose();
			}
		}

		private void gvData_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1 && e.ColumnIndex > -1)
			{
				g_sOrderField = gvData.Columns[e.ColumnIndex].Name;
				ShowData();
				SetSelectRow(gvData, "", TableDefine.gsDef_KeyField);
			}
		}

		private void MenuModule_Click(object sender, EventArgs e)
		{
			if (gvData.Rows.Count != 0 && gvData.CurrentRow != null)
			{
				string g_sRoleID = gvData.CurrentRow.Cells["ROLE_ID"].Value.ToString();
				string g_sRoleName = gvData.CurrentRow.Cells["ROLE_NAME"].Value.ToString();
				fModule fModule = new fModule();
				fModule.g_sRoleName = g_sRoleName;
				fModule.g_sRoleID = g_sRoleID;
				fModule.ShowDialog();
				fModule.Dispose();
			}
		}

		private void cbAutoResize_CheckedChanged(object sender, EventArgs e)
		{
			if (cbAutoResize.Checked)
			{
				gvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			}
			else
			{
				gvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
			}
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));
            this.gvData = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuModule = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.combShow = new System.Windows.Forms.ToolStripComboBox();
            this.btnAppend = new System.Windows.Forms.ToolStripButton();
            this.btnModify = new System.Windows.Forms.ToolStripButton();
            this.btnEnabled = new System.Windows.Forms.ToolStripButton();
            this.btnDisabled = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.btnModule = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LabFilter = new System.Windows.Forms.Label();
            this.combFilterField = new System.Windows.Forms.ComboBox();
            this.editFilter = new System.Windows.Forms.TextBox();
            this.combFilter = new System.Windows.Forms.ComboBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.cbAutoResize = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gvData
            // 
            this.gvData.AllowUserToAddRows = false;
            this.gvData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Info;
            this.gvData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gvData.BackgroundColor = System.Drawing.Color.White;
            this.gvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvData.ContextMenuStrip = this.contextMenuStrip1;
            this.gvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvData.Location = new System.Drawing.Point(0, 112);
            this.gvData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gvData.MultiSelect = false;
            this.gvData.Name = "gvData";
            this.gvData.ReadOnly = true;
            this.gvData.RowHeadersWidth = 25;
            this.gvData.RowTemplate.Height = 24;
            this.gvData.Size = new System.Drawing.Size(1048, 562);
            this.gvData.TabIndex = 1;
            this.gvData.VirtualMode = true;
            this.gvData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvData_CellClick);
            this.gvData.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.gvData_CellValueNeeded);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuModule,
            this.toolStripSeparator1,
            this.MenuHistory});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(180, 70);
            // 
            // MenuModule
            // 
            this.MenuModule.Name = "MenuModule";
            this.MenuModule.Size = new System.Drawing.Size(179, 30);
            this.MenuModule.Text = "Module";
            this.MenuModule.Click += new System.EventHandler(this.MenuModule_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(176, 6);
            // 
            // MenuHistory
            // 
            this.MenuHistory.Name = "MenuHistory";
            this.MenuHistory.Size = new System.Drawing.Size(179, 30);
            this.MenuHistory.Text = "History Log";
            this.MenuHistory.Click += new System.EventHandler(this.MenuHistory_Click);
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.CountItem = null;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.bindingNavigator1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.combShow,
            this.btnAppend,
            this.btnModify,
            this.btnEnabled,
            this.btnDisabled,
            this.btnDelete,
            this.bindingNavigatorSeparator2,
            this.btnExport,
            this.btnModule,
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.bindingNavigator1.PositionItem = null;
            this.bindingNavigator1.Size = new System.Drawing.Size(1048, 58);
            this.bindingNavigator1.TabIndex = 2;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // combShow
            // 
            this.combShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.combShow.Items.AddRange(new object[] {
            "Enabled",
            "Disabled",
            "All"});
            this.combShow.Name = "combShow";
            this.combShow.Size = new System.Drawing.Size(118, 58);
            this.combShow.SelectedIndexChanged += new System.EventHandler(this.combShow_SelectedIndexChanged);
            // 
            // btnAppend
            // 
            this.btnAppend.Image = ((System.Drawing.Image)(resources.GetObject("btnAppend.Image")));
            this.btnAppend.Name = "btnAppend";
            this.btnAppend.RightToLeftAutoMirrorImage = true;
            this.btnAppend.Size = new System.Drawing.Size(85, 53);
            this.btnAppend.Text = "Append";
            this.btnAppend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAppend.Click += new System.EventHandler(this.btnAppend_Click);
            // 
            // btnModify
            // 
            this.btnModify.Image = ((System.Drawing.Image)(resources.GetObject("btnModify.Image")));
            this.btnModify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(74, 53);
            this.btnModify.Text = "Modify";
            this.btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnEnabled
            // 
            this.btnEnabled.Image = ((System.Drawing.Image)(resources.GetObject("btnEnabled.Image")));
            this.btnEnabled.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEnabled.Name = "btnEnabled";
            this.btnEnabled.Size = new System.Drawing.Size(88, 53);
            this.btnEnabled.Text = "Enabled";
            this.btnEnabled.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEnabled.Click += new System.EventHandler(this.btnDisable_Click);
            // 
            // btnDisabled
            // 
            this.btnDisabled.Image = ((System.Drawing.Image)(resources.GetObject("btnDisabled.Image")));
            this.btnDisabled.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDisabled.Name = "btnDisabled";
            this.btnDisabled.Size = new System.Drawing.Size(92, 53);
            this.btnDisabled.Text = "Disabled";
            this.btnDisabled.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDisabled.Click += new System.EventHandler(this.btnDisable_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.RightToLeftAutoMirrorImage = true;
            this.btnDelete.Size = new System.Drawing.Size(72, 53);
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 58);
            // 
            // btnExport
            // 
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(72, 53);
            this.btnExport.Text = "Export";
            this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnModule
            // 
            this.btnModule.Image = ((System.Drawing.Image)(resources.GetObject("btnModule.Image")));
            this.btnModule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModule.Name = "btnModule";
            this.btnModule.Size = new System.Drawing.Size(81, 53);
            this.btnModule.Text = "Module";
            this.btnModule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnModule.Click += new System.EventHandler(this.MenuModule_Click);
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(34, 53);
            this.bindingNavigatorMoveFirstItem.Text = "移到最前面";
            this.bindingNavigatorMoveFirstItem.Visible = false;
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(34, 53);
            this.bindingNavigatorMovePreviousItem.Text = "移到上一個";
            this.bindingNavigatorMovePreviousItem.Visible = false;
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 58);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "位置";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(40, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "目前的位置";
            this.bindingNavigatorPositionItem.Visible = false;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(43, 53);
            this.bindingNavigatorCountItem.Text = "/{0}";
            this.bindingNavigatorCountItem.ToolTipText = "項目總數";
            this.bindingNavigatorCountItem.Visible = false;
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 58);
            this.bindingNavigatorSeparator1.Visible = false;
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(34, 53);
            this.bindingNavigatorMoveNextItem.Text = "移到下一個";
            this.bindingNavigatorMoveNextItem.Visible = false;
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(34, 53);
            this.bindingNavigatorMoveLastItem.Text = "移到最後面";
            this.bindingNavigatorMoveLastItem.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.LabFilter);
            this.panel1.Controls.Add(this.combFilterField);
            this.panel1.Controls.Add(this.editFilter);
            this.panel1.Controls.Add(this.combFilter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 58);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1048, 54);
            this.panel1.TabIndex = 5;
            // 
            // LabFilter
            // 
            this.LabFilter.AutoSize = true;
            this.LabFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.LabFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.LabFilter.Location = new System.Drawing.Point(18, 16);
            this.LabFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabFilter.Name = "LabFilter";
            this.LabFilter.Size = new System.Drawing.Size(54, 25);
            this.LabFilter.TabIndex = 3;
            this.LabFilter.Text = "Filter";
            // 
            // combFilterField
            // 
            this.combFilterField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combFilterField.FormattingEnabled = true;
            this.combFilterField.Location = new System.Drawing.Point(560, 12);
            this.combFilterField.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.combFilterField.Name = "combFilterField";
            this.combFilterField.Size = new System.Drawing.Size(180, 26);
            this.combFilterField.TabIndex = 2;
            this.combFilterField.Visible = false;
            // 
            // editFilter
            // 
            this.editFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.editFilter.Location = new System.Drawing.Point(328, 9);
            this.editFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.editFilter.Name = "editFilter";
            this.editFilter.Size = new System.Drawing.Size(220, 30);
            this.editFilter.TabIndex = 1;
            this.editFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editFilter_KeyPress);
            // 
            // combFilter
            // 
            this.combFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.combFilter.FormattingEnabled = true;
            this.combFilter.Location = new System.Drawing.Point(138, 10);
            this.combFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.combFilter.Name = "combFilter";
            this.combFilter.Size = new System.Drawing.Size(180, 33);
            this.combFilter.TabIndex = 0;
            // 
            // cbAutoResize
            // 
            this.cbAutoResize.AutoSize = true;
            this.cbAutoResize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbAutoResize.Location = new System.Drawing.Point(819, 18);
            this.cbAutoResize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbAutoResize.Name = "cbAutoResize";
            this.cbAutoResize.Size = new System.Drawing.Size(133, 22);
            this.cbAutoResize.TabIndex = 7;
            this.cbAutoResize.Text = "Auto Resize";
            this.cbAutoResize.UseVisualStyleBackColor = true;
            this.cbAutoResize.CheckedChanged += new System.EventHandler(this.cbAutoResize_CheckedChanged);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 674);
            this.Controls.Add(this.cbAutoResize);
            this.Controls.Add(this.gvData);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bindingNavigator1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "fMain";
            this.Text = "fMain";
            this.Load += new System.EventHandler(this.fMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

      
    }
}
