namespace CRoute
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
            this.TreeViewRoute = new System.Windows.Forms.TreeView();
            this.PopMenu1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.collapseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.unNecessaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NecessaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.changeRouteNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyRoueFromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemInProcess = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.combRoute = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.LabRouteName = new System.Windows.Forms.Label();
            this.grpbDesc = new System.Windows.Forms.GroupBox();
            this.editRouteDesc = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.label1 = new System.Windows.Forms.Label();
            this.routeDesc = new System.Windows.Forms.TextBox();
            this.SCMaster.Panel1.SuspendLayout();
            this.SCMaster.Panel2.SuspendLayout();
            this.SCMaster.SuspendLayout();
            this.PopMenu2.SuspendLayout();
            this.PopMenu1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpbDesc.SuspendLayout();
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
            this.SCMaster.Panel2.Controls.Add(this.TreeViewRoute);
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
            this.TreeViewProcess.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeView_DragEnter);
            // 
            // PopMenu2
            // 
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
            this.imageList1.Images.SetKeyName(0, "RouteStage.bmp");
            this.imageList1.Images.SetKeyName(1, "RouteProcess.bmp");
            this.imageList1.Images.SetKeyName(2, "RouteRepair.bmp");
            this.imageList1.Images.SetKeyName(3, "RouteUnnecessary.bmp");
            this.imageList1.Images.SetKeyName(4, "RouteDisabled.bmp");
            this.imageList1.Images.SetKeyName(5, "RouteDisabledRepair.bmp");
            // 
            // TreeViewRoute
            // 
            this.TreeViewRoute.AllowDrop = true;
            this.TreeViewRoute.BackColor = System.Drawing.Color.White;
            this.TreeViewRoute.ContextMenuStrip = this.PopMenu1;
            resources.ApplyResources(this.TreeViewRoute, "TreeViewRoute");
            this.TreeViewRoute.ImageList = this.imageList1;
            this.TreeViewRoute.Name = "TreeViewRoute";
            this.TreeViewRoute.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeView_ItemDrag);
            this.TreeViewRoute.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeView_DragDrop);
            this.TreeViewRoute.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeView_DragEnter);
            this.TreeViewRoute.DragOver += new System.Windows.Forms.DragEventHandler(this.TreeViewRoute_DragOver);
            this.TreeViewRoute.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeViewRoute_MouseDown);
            // 
            // PopMenu1
            // 
            this.PopMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.collapseToolStripMenuItem,
            this.expandToolStripMenuItem,
            this.toolStripMenuItem1,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.unNecessaryToolStripMenuItem,
            this.NecessaryToolStripMenuItem,
            this.toolStripSeparator2,
            this.changeRouteNameToolStripMenuItem,
            this.copyRoueFromToolStripMenuItem,
            this.historyLogToolStripMenuItem,
            this.MenuItemInProcess});
            this.PopMenu1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.PopMenu1, "PopMenu1");
            this.PopMenu1.Opening += new System.ComponentModel.CancelEventHandler(this.PopMenu1_Opening);
            // 
            // collapseToolStripMenuItem
            // 
            this.collapseToolStripMenuItem.Name = "collapseToolStripMenuItem";
            resources.ApplyResources(this.collapseToolStripMenuItem, "collapseToolStripMenuItem");
            this.collapseToolStripMenuItem.Click += new System.EventHandler(this.collapseToolStripMenuItem_Click);
            // 
            // expandToolStripMenuItem
            // 
            this.expandToolStripMenuItem.Name = "expandToolStripMenuItem";
            resources.ApplyResources(this.expandToolStripMenuItem, "expandToolStripMenuItem");
            this.expandToolStripMenuItem.Click += new System.EventHandler(this.expandToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // unNecessaryToolStripMenuItem
            // 
            this.unNecessaryToolStripMenuItem.Name = "unNecessaryToolStripMenuItem";
            resources.ApplyResources(this.unNecessaryToolStripMenuItem, "unNecessaryToolStripMenuItem");
            this.unNecessaryToolStripMenuItem.Click += new System.EventHandler(this.necessaryToolStripMenuItem_Click);
            // 
            // NecessaryToolStripMenuItem
            // 
            this.NecessaryToolStripMenuItem.Name = "NecessaryToolStripMenuItem";
            resources.ApplyResources(this.NecessaryToolStripMenuItem, "NecessaryToolStripMenuItem");
            this.NecessaryToolStripMenuItem.Click += new System.EventHandler(this.necessaryToolStripMenuItem_Click);
            this.NecessaryToolStripMenuItem.EnabledChanged += new System.EventHandler(this.NecessaryToolStripMenuItem_EnabledChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // changeRouteNameToolStripMenuItem
            // 
            this.changeRouteNameToolStripMenuItem.Name = "changeRouteNameToolStripMenuItem";
            resources.ApplyResources(this.changeRouteNameToolStripMenuItem, "changeRouteNameToolStripMenuItem");
            this.changeRouteNameToolStripMenuItem.Click += new System.EventHandler(this.changeRouteNameToolStripMenuItem_Click);
            // 
            // copyRoueFromToolStripMenuItem
            // 
            this.copyRoueFromToolStripMenuItem.Name = "copyRoueFromToolStripMenuItem";
            resources.ApplyResources(this.copyRoueFromToolStripMenuItem, "copyRoueFromToolStripMenuItem");
            this.copyRoueFromToolStripMenuItem.Click += new System.EventHandler(this.copyRoueFromToolStripMenuItem_Click);
            // 
            // historyLogToolStripMenuItem
            // 
            this.historyLogToolStripMenuItem.Name = "historyLogToolStripMenuItem";
            resources.ApplyResources(this.historyLogToolStripMenuItem, "historyLogToolStripMenuItem");
            this.historyLogToolStripMenuItem.Click += new System.EventHandler(this.historyLogToolStripMenuItem_Click);
            // 
            // MenuItemInProcess
            // 
            this.MenuItemInProcess.Name = "MenuItemInProcess";
            resources.ApplyResources(this.MenuItemInProcess, "MenuItemInProcess");
            this.MenuItemInProcess.Click += new System.EventHandler(this.MenuItemInProcess_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.routeDesc);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.combRoute);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.LabRouteName);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // combRoute
            // 
            this.combRoute.FormattingEnabled = true;
            resources.ApplyResources(this.combRoute, "combRoute");
            this.combRoute.Name = "combRoute";
            this.combRoute.TextChanged += new System.EventHandler(this.combRoute_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // LabRouteName
            // 
            resources.ApplyResources(this.LabRouteName, "LabRouteName");
            this.LabRouteName.Name = "LabRouteName";
            // 
            // grpbDesc
            // 
            this.grpbDesc.BackColor = System.Drawing.Color.Transparent;
            this.grpbDesc.Controls.Add(this.editRouteDesc);
            resources.ApplyResources(this.grpbDesc, "grpbDesc");
            this.grpbDesc.Name = "grpbDesc";
            this.grpbDesc.TabStop = false;
            // 
            // editRouteDesc
            // 
            resources.ApplyResources(this.editRouteDesc, "editRouteDesc");
            this.editRouteDesc.Name = "editRouteDesc";
            this.editRouteDesc.ReadOnly = true;
            // 
            // splitter1
            // 
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // routeDesc
            // 
            resources.ApplyResources(this.routeDesc, "routeDesc");
            this.routeDesc.Name = "routeDesc";
            // 
            // fMain
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.SCMaster);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.grpbDesc);
            this.Controls.Add(this.panel1);
            this.Name = "fMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fMain_FormClosing);
            this.Load += new System.EventHandler(this.fRoute_Load);
            this.SCMaster.Panel1.ResumeLayout(false);
            this.SCMaster.Panel2.ResumeLayout(false);
            this.SCMaster.ResumeLayout(false);
            this.PopMenu2.ResumeLayout(false);
            this.PopMenu1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpbDesc.ResumeLayout(false);
            this.grpbDesc.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
          
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LabRouteName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.SplitContainer SCMaster;
        private System.Windows.Forms.ContextMenuStrip PopMenu1;
        private System.Windows.Forms.ToolStripMenuItem collapseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NecessaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TreeView TreeViewRoute;
        private System.Windows.Forms.TreeView TreeViewProcess;
        private System.Windows.Forms.ToolStripMenuItem historyLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem changeRouteNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyRoueFromToolStripMenuItem;
        private System.Windows.Forms.ComboBox combRoute;
        private System.Windows.Forms.ContextMenuStrip PopMenu2;
        private System.Windows.Forms.ToolStripMenuItem collapseToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem expandToolStripMenuItem1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem unNecessaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItemInProcess;
        private System.Windows.Forms.GroupBox grpbDesc;
        private System.Windows.Forms.TextBox editRouteDesc;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TextBox routeDesc;
        private System.Windows.Forms.Label label1;
    }
}
