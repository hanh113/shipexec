namespace CTerminal
{
    partial class fMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));
            this.SCMaster = new System.Windows.Forms.SplitContainer();
            this.TreeViewProcess = new System.Windows.Forms.TreeView();
            this.PopMenu2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.collapseToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.expandToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.TreeViewTerminal = new System.Windows.Forms.TreeView();
            this.PopMenu1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemCollapse = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemExpand = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemModify = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemEnable = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDisable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemPC = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemChange = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemShowTerminalID = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemTerminalType = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LabFactoryCode = new System.Windows.Forms.Label();
            this.combFactory = new System.Windows.Forms.ComboBox();
            this.LabFactory = new System.Windows.Forms.Label();
            this.combLine = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.combShow = new System.Windows.Forms.ToolStripComboBox();
            this.btnAppend = new System.Windows.Forms.ToolStripButton();
            this.btnModify = new System.Windows.Forms.ToolStripButton();
            this.btnEnabled = new System.Windows.Forms.ToolStripButton();
            this.btnDisabled = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.SCMaster.Panel1.SuspendLayout();
            this.SCMaster.Panel2.SuspendLayout();
            this.SCMaster.SuspendLayout();
            this.PopMenu2.SuspendLayout();
            this.PopMenu1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SCMaster
            // 
            resources.ApplyResources(this.SCMaster, "SCMaster");
            this.SCMaster.Name = "SCMaster";
            // 
            // SCMaster.Panel1
            // 
            this.SCMaster.Panel1.Controls.Add(this.TreeViewProcess);
            // 
            // SCMaster.Panel2
            // 
            this.SCMaster.Panel2.Controls.Add(this.TreeViewTerminal);
            // 
            // TreeViewProcess
            // 
            this.TreeViewProcess.AllowDrop = true;
            this.TreeViewProcess.BackColor = System.Drawing.Color.White;
            this.TreeViewProcess.ContextMenuStrip = this.PopMenu2;
            resources.ApplyResources(this.TreeViewProcess, "TreeViewProcess");
            this.TreeViewProcess.ImageList = this.imageList1;
            this.TreeViewProcess.Name = "TreeViewProcess";
            this.TreeViewProcess.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeView_ItemDrag);
            this.TreeViewProcess.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewProcess_AfterSelect);
            this.TreeViewProcess.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeView_DragEnter);
            // 
            // PopMenu2
            // 
            this.PopMenu2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.PopMenu2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.collapseToolStripMenuItem1,
            this.expandToolStripMenuItem1});
            this.PopMenu2.Name = "PopMenu2";
            resources.ApplyResources(this.PopMenu2, "PopMenu2");
            // 
            // collapseToolStripMenuItem1
            // 
            this.collapseToolStripMenuItem1.Name = "collapseToolStripMenuItem1";
            resources.ApplyResources(this.collapseToolStripMenuItem1, "collapseToolStripMenuItem1");
            this.collapseToolStripMenuItem1.Click += new System.EventHandler(this.collapseToolStripMenuItem1_Click);
            // 
            // expandToolStripMenuItem1
            // 
            this.expandToolStripMenuItem1.Name = "expandToolStripMenuItem1";
            resources.ApplyResources(this.expandToolStripMenuItem1, "expandToolStripMenuItem1");
            this.expandToolStripMenuItem1.Click += new System.EventHandler(this.expandToolStripMenuItem1_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Line.bmp");
            this.imageList1.Images.SetKeyName(1, "Stage.bmp");
            this.imageList1.Images.SetKeyName(2, "Process.bmp");
            this.imageList1.Images.SetKeyName(3, "Terminal.bmp");
            this.imageList1.Images.SetKeyName(4, "TerminalDCT.bmp");
            this.imageList1.Images.SetKeyName(5, "TerminalDisabled.bmp");
            this.imageList1.Images.SetKeyName(6, "TerminalDCTDisable.bmp");
            this.imageList1.Images.SetKeyName(7, "TerminalATE.png.png");
            this.imageList1.Images.SetKeyName(8, "TerminalSMT.png");
            this.imageList1.Images.SetKeyName(9, "TerminalATEDis.png");
            this.imageList1.Images.SetKeyName(10, "TerminalSMTDis.png");
            // 
            // TreeViewTerminal
            // 
            this.TreeViewTerminal.AllowDrop = true;
            this.TreeViewTerminal.BackColor = System.Drawing.Color.White;
            this.TreeViewTerminal.ContextMenuStrip = this.PopMenu1;
            resources.ApplyResources(this.TreeViewTerminal, "TreeViewTerminal");
            this.TreeViewTerminal.ImageList = this.imageList1;
            this.TreeViewTerminal.Name = "TreeViewTerminal";
            this.TreeViewTerminal.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeView_ItemDrag);
            this.TreeViewTerminal.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewTerminal_AfterSelect);
            this.TreeViewTerminal.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeView_DragDrop);
            this.TreeViewTerminal.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeView_DragEnter);
            // 
            // PopMenu1
            // 
            this.PopMenu1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.PopMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemCollapse,
            this.MenuItemExpand,
            this.MenuItemCollapseAll,
            this.MenuItemExpandAll,
            this.toolStripSeparator3,
            this.MenuItemAdd,
            this.MenuItemModify,
            this.MenuItemEnable,
            this.MenuItemDisable,
            this.toolStripSeparator1,
            this.MenuItemPC,
            this.MenuItemChange,
            this.toolStripSeparator2,
            this.MenuItemShowTerminalID,
            this.MenuItemHistory,
            this.MenuItemTerminalType});
            this.PopMenu1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.PopMenu1, "PopMenu1");
            this.PopMenu1.Opening += new System.ComponentModel.CancelEventHandler(this.PopMenu1_Opening);
            // 
            // MenuItemCollapse
            // 
            this.MenuItemCollapse.Name = "MenuItemCollapse";
            resources.ApplyResources(this.MenuItemCollapse, "MenuItemCollapse");
            this.MenuItemCollapse.Click += new System.EventHandler(this.MenuItemCollapse_Click);
            // 
            // MenuItemExpand
            // 
            this.MenuItemExpand.Name = "MenuItemExpand";
            resources.ApplyResources(this.MenuItemExpand, "MenuItemExpand");
            this.MenuItemExpand.Click += new System.EventHandler(this.MenuItemExpand_Click);
            // 
            // MenuItemCollapseAll
            // 
            this.MenuItemCollapseAll.Name = "MenuItemCollapseAll";
            resources.ApplyResources(this.MenuItemCollapseAll, "MenuItemCollapseAll");
            this.MenuItemCollapseAll.Click += new System.EventHandler(this.MenuItemCollapseAll_Click);
            // 
            // MenuItemExpandAll
            // 
            this.MenuItemExpandAll.Name = "MenuItemExpandAll";
            resources.ApplyResources(this.MenuItemExpandAll, "MenuItemExpandAll");
            this.MenuItemExpandAll.Click += new System.EventHandler(this.MenuItemExpandAll_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // MenuItemAdd
            // 
            this.MenuItemAdd.Name = "MenuItemAdd";
            resources.ApplyResources(this.MenuItemAdd, "MenuItemAdd");
            this.MenuItemAdd.Click += new System.EventHandler(this.MenuItemAdd_Click);
            // 
            // MenuItemModify
            // 
            this.MenuItemModify.Name = "MenuItemModify";
            resources.ApplyResources(this.MenuItemModify, "MenuItemModify");
            this.MenuItemModify.Click += new System.EventHandler(this.MenuItemModify_Click);
            // 
            // MenuItemEnable
            // 
            this.MenuItemEnable.Name = "MenuItemEnable";
            resources.ApplyResources(this.MenuItemEnable, "MenuItemEnable");
            this.MenuItemEnable.Click += new System.EventHandler(this.MenuItemDisable_Click);
            // 
            // MenuItemDisable
            // 
            this.MenuItemDisable.Name = "MenuItemDisable";
            resources.ApplyResources(this.MenuItemDisable, "MenuItemDisable");
            this.MenuItemDisable.Click += new System.EventHandler(this.MenuItemDisable_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // MenuItemPC
            // 
            this.MenuItemPC.Name = "MenuItemPC";
            resources.ApplyResources(this.MenuItemPC, "MenuItemPC");
            this.MenuItemPC.Click += new System.EventHandler(this.MenuItemChange_Click);
            // 
            // MenuItemChange
            // 
            this.MenuItemChange.Name = "MenuItemChange";
            resources.ApplyResources(this.MenuItemChange, "MenuItemChange");
            this.MenuItemChange.Click += new System.EventHandler(this.MenuItemChange_Click);
            this.MenuItemChange.EnabledChanged += new System.EventHandler(this.MenuItemChange_EnabledChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // MenuItemShowTerminalID
            // 
            this.MenuItemShowTerminalID.Name = "MenuItemShowTerminalID";
            resources.ApplyResources(this.MenuItemShowTerminalID, "MenuItemShowTerminalID");
            this.MenuItemShowTerminalID.Click += new System.EventHandler(this.MenuItemShowTerminalID_Click);
            // 
            // MenuItemHistory
            // 
            this.MenuItemHistory.Name = "MenuItemHistory";
            resources.ApplyResources(this.MenuItemHistory, "MenuItemHistory");
            this.MenuItemHistory.Click += new System.EventHandler(this.MenuItemHistory_Click);
            // 
            // MenuItemTerminalType
            // 
            this.MenuItemTerminalType.Name = "MenuItemTerminalType";
            resources.ApplyResources(this.MenuItemTerminalType, "MenuItemTerminalType");
            this.MenuItemTerminalType.Click += new System.EventHandler(this.MenuItemTerminalType_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.LabFactoryCode);
            this.panel1.Controls.Add(this.combFactory);
            this.panel1.Controls.Add(this.LabFactory);
            this.panel1.Controls.Add(this.combLine);
            this.panel1.Controls.Add(this.label1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Name = "panel1";
            // 
            // LabFactoryCode
            // 
            resources.ApplyResources(this.LabFactoryCode, "LabFactoryCode");
            this.LabFactoryCode.BackColor = System.Drawing.Color.Transparent;
            this.LabFactoryCode.ForeColor = System.Drawing.Color.Maroon;
            this.LabFactoryCode.Name = "LabFactoryCode";
            // 
            // combFactory
            // 
            this.combFactory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.combFactory, "combFactory");
            this.combFactory.FormattingEnabled = true;
            this.combFactory.Name = "combFactory";
            this.combFactory.SelectedIndexChanged += new System.EventHandler(this.combFactory_SelectedIndexChanged);
            // 
            // LabFactory
            // 
            resources.ApplyResources(this.LabFactory, "LabFactory");
            this.LabFactory.BackColor = System.Drawing.Color.Transparent;
            this.LabFactory.Name = "LabFactory";
            // 
            // combLine
            // 
            this.combLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.combLine, "combLine");
            this.combLine.FormattingEnabled = true;
            this.combLine.Name = "combLine";
            this.combLine.SelectedIndexChanged += new System.EventHandler(this.combLine_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.CountItem = null;
            this.bindingNavigator1.DeleteItem = null;
            resources.ApplyResources(this.bindingNavigator1, "bindingNavigator1");
            this.bindingNavigator1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.combShow,
            this.btnAppend,
            this.btnModify,
            this.btnEnabled,
            this.btnDisabled,
            this.btnDelete,
            this.bindingNavigatorSeparator2,
            this.btnExport,
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem});
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = null;
            // 
            // combShow
            // 
            this.combShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.combShow, "combShow");
            this.combShow.Items.AddRange(new object[] {
            resources.GetString("combShow.Items"),
            resources.GetString("combShow.Items1"),
            resources.GetString("combShow.Items2")});
            this.combShow.Name = "combShow";
            this.combShow.SelectedIndexChanged += new System.EventHandler(this.combShow_SelectedIndexChanged);
            // 
            // btnAppend
            // 
            resources.ApplyResources(this.btnAppend, "btnAppend");
            this.btnAppend.Name = "btnAppend";
            this.btnAppend.Click += new System.EventHandler(this.MenuItemAdd_Click);
            // 
            // btnModify
            // 
            resources.ApplyResources(this.btnModify, "btnModify");
            this.btnModify.Name = "btnModify";
            this.btnModify.Click += new System.EventHandler(this.MenuItemModify_Click);
            // 
            // btnEnabled
            // 
            resources.ApplyResources(this.btnEnabled, "btnEnabled");
            this.btnEnabled.Name = "btnEnabled";
            this.btnEnabled.Click += new System.EventHandler(this.MenuItemDisable_Click);
            // 
            // btnDisabled
            // 
            resources.ApplyResources(this.btnDisabled, "btnDisabled");
            this.btnDisabled.Name = "btnDisabled";
            this.btnDisabled.Click += new System.EventHandler(this.MenuItemDisable_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            resources.ApplyResources(this.bindingNavigatorSeparator2, "bindingNavigatorSeparator2");
            // 
            // btnExport
            // 
            resources.ApplyResources(this.btnExport, "btnExport");
            this.btnExport.Name = "btnExport";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.bindingNavigatorMoveFirstItem, "bindingNavigatorMoveFirstItem");
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.bindingNavigatorMovePreviousItem, "bindingNavigatorMovePreviousItem");
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            resources.ApplyResources(this.bindingNavigatorSeparator, "bindingNavigatorSeparator");
            // 
            // bindingNavigatorPositionItem
            // 
            resources.ApplyResources(this.bindingNavigatorPositionItem, "bindingNavigatorPositionItem");
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            resources.ApplyResources(this.bindingNavigatorCountItem, "bindingNavigatorCountItem");
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            resources.ApplyResources(this.bindingNavigatorSeparator1, "bindingNavigatorSeparator1");
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.bindingNavigatorMoveNextItem, "bindingNavigatorMoveNextItem");
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.bindingNavigatorMoveLastItem, "bindingNavigatorMoveLastItem");
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            // 
            // fMain
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.SCMaster);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bindingNavigator1);
            this.Name = "fMain";
            this.Load += new System.EventHandler(this.fRoute_Load);
            this.SCMaster.Panel1.ResumeLayout(false);
            this.SCMaster.Panel2.ResumeLayout(false);
            this.SCMaster.ResumeLayout(false);
            this.PopMenu2.ResumeLayout(false);
            this.PopMenu1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer SCMaster;
        private System.Windows.Forms.ContextMenuStrip PopMenu1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemCollapse;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDisable;
        private System.Windows.Forms.ToolStripMenuItem MenuItemChange;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TreeView TreeViewTerminal;
        private System.Windows.Forms.TreeView TreeViewProcess;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MenuItemShowTerminalID;
        private System.Windows.Forms.ComboBox combLine;
        private System.Windows.Forms.ContextMenuStrip PopMenu2;
        private System.Windows.Forms.ToolStripMenuItem collapseToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem expandToolStripMenuItem1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemAdd;
        private System.Windows.Forms.ToolStripMenuItem MenuItemModify;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem MenuItemExpand;
        private System.Windows.Forms.ToolStripMenuItem MenuItemHistory;
        private System.Windows.Forms.ComboBox combFactory;
        private System.Windows.Forms.Label LabFactory;
        private System.Windows.Forms.Label LabFactoryCode;
        private System.Windows.Forms.ToolStripMenuItem MenuItemPC;
        private System.Windows.Forms.ToolStripMenuItem MenuItemEnable;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripComboBox combShow;
        private System.Windows.Forms.ToolStripButton btnAppend;
        private System.Windows.Forms.ToolStripButton btnModify;
        private System.Windows.Forms.ToolStripButton btnEnabled;
        private System.Windows.Forms.ToolStripButton btnDisabled;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton btnExport;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItemCollapseAll;
        private System.Windows.Forms.ToolStripMenuItem MenuItemExpandAll;
        private System.Windows.Forms.ToolStripMenuItem MenuItemTerminalType;
    }
}
