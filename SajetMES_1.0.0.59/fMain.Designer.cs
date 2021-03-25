namespace EDIPPS
{
    partial class fMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));
            this.panelList = new System.Windows.Forms.Panel();
            this.pnlTreeView = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lablFunction = new System.Windows.Forms.Label();
            this.pnlFunType = new System.Windows.Forms.Panel();
            this.combFunType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlModule = new System.Windows.Forms.Panel();
            this.combModule = new System.Windows.Forms.ComboBox();
            this.bsModule = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSmallFont = new System.Windows.Forms.Button();
            this.btnFont = new System.Windows.Forms.Button();
            this.btnLargeFont = new System.Windows.Forms.Button();
            this.lablModule = new System.Windows.Forms.Label();
            this.pnlMin = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.securityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.skinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsHideBottom = new System.Windows.Forms.ToolStripMenuItem();
            this.tsHideLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsShowHost = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.minimizeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsVisible = new System.Windows.Forms.ToolStripMenuItem();
            this.tsFunction = new System.Windows.Forms.ToolStripTextBox();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelFunction = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.pnlMenu2 = new System.Windows.Forms.Panel();
            this.lablLogin = new System.Windows.Forms.Label();
            this.pnlMenuStrip = new System.Windows.Forms.Panel();
            this.lablMES = new System.Windows.Forms.Label();
            this.pnlMenu1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMin = new System.Windows.Forms.Button();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.btnResize = new System.Windows.Forms.Button();
            this.lablTitle = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lablCopyRight = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pnlCopyRight = new System.Windows.Forms.Panel();
            this.splitterRight = new System.Windows.Forms.Splitter();
            this.splitterBottom = new System.Windows.Forms.Splitter();
            this.splitterLeft = new System.Windows.Forms.Splitter();
            this.splitterTop = new System.Windows.Forms.Splitter();
            this.panelList.SuspendLayout();
            this.pnlTreeView.SuspendLayout();
            this.pnlFunType.SuspendLayout();
            this.pnlModule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsModule)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlMin.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panelFunction.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            this.pnlMenu2.SuspendLayout();
            this.pnlMenuStrip.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelList
            // 
            this.panelList.BackColor = System.Drawing.Color.Transparent;
            this.panelList.Controls.Add(this.pnlTreeView);
            this.panelList.Controls.Add(this.pnlFunType);
            this.panelList.Controls.Add(this.pnlModule);
            resources.ApplyResources(this.panelList, "panelList");
            this.panelList.Name = "panelList";
            this.panelList.Leave += new System.EventHandler(this.panelList_Leave);
            // 
            // pnlTreeView
            // 
            resources.ApplyResources(this.pnlTreeView, "pnlTreeView");
            this.pnlTreeView.Controls.Add(this.treeView1);
            this.pnlTreeView.Controls.Add(this.lablFunction);
            this.pnlTreeView.Name = "pnlTreeView";
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.treeView1, "treeView1");
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowLines = false;
            this.treeView1.ShowPlusMinus = false;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView1.Click += new System.EventHandler(this.treeView1_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Olive;
            this.imageList1.Images.SetKeyName(0, "folder.bmp");
            this.imageList1.Images.SetKeyName(1, "file.bmp");
            this.imageList1.Images.SetKeyName(2, "folderopen.bmp");
            this.imageList1.Images.SetKeyName(3, "pin.bmp");
            this.imageList1.Images.SetKeyName(4, "pin1.bmp");
            // 
            // lablFunction
            // 
            resources.ApplyResources(this.lablFunction, "lablFunction");
            this.lablFunction.Name = "lablFunction";
            // 
            // pnlFunType
            // 
            resources.ApplyResources(this.pnlFunType, "pnlFunType");
            this.pnlFunType.Controls.Add(this.combFunType);
            this.pnlFunType.Controls.Add(this.label3);
            this.pnlFunType.Name = "pnlFunType";
            // 
            // combFunType
            // 
            resources.ApplyResources(this.combFunType, "combFunType");
            this.combFunType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combFunType.FormattingEnabled = true;
            this.combFunType.Name = "combFunType";
            this.combFunType.SelectedIndexChanged += new System.EventHandler(this.combFunType_SelectedIndexChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // pnlModule
            // 
            resources.ApplyResources(this.pnlModule, "pnlModule");
            this.pnlModule.Controls.Add(this.combModule);
            this.pnlModule.Controls.Add(this.panel1);
            this.pnlModule.Name = "pnlModule";
            // 
            // combModule
            // 
            this.combModule.BackColor = System.Drawing.SystemColors.Window;
            this.combModule.DataSource = this.bsModule;
            resources.ApplyResources(this.combModule, "combModule");
            this.combModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combModule.FormattingEnabled = true;
            this.combModule.Name = "combModule";
            this.combModule.SelectedIndexChanged += new System.EventHandler(this.combModule_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSmallFont);
            this.panel1.Controls.Add(this.btnFont);
            this.panel1.Controls.Add(this.btnLargeFont);
            this.panel1.Controls.Add(this.lablModule);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnSmallFont
            // 
            this.btnSmallFont.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnSmallFont, "btnSmallFont");
            this.btnSmallFont.FlatAppearance.BorderSize = 0;
            this.btnSmallFont.Name = "btnSmallFont";
            this.btnSmallFont.Tag = "0";
            this.btnSmallFont.UseVisualStyleBackColor = false;
            this.btnSmallFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // btnFont
            // 
            this.btnFont.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnFont, "btnFont");
            this.btnFont.FlatAppearance.BorderSize = 0;
            this.btnFont.Name = "btnFont";
            this.btnFont.Tag = "1";
            this.btnFont.UseVisualStyleBackColor = false;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // btnLargeFont
            // 
            this.btnLargeFont.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnLargeFont, "btnLargeFont");
            this.btnLargeFont.FlatAppearance.BorderSize = 0;
            this.btnLargeFont.Name = "btnLargeFont";
            this.btnLargeFont.Tag = "2";
            this.btnLargeFont.UseVisualStyleBackColor = false;
            this.btnLargeFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // lablModule
            // 
            resources.ApplyResources(this.lablModule, "lablModule");
            this.lablModule.Name = "lablModule";
            // 
            // pnlMin
            // 
            this.pnlMin.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pnlMin, "pnlMin");
            this.pnlMin.Controls.Add(this.button1);
            this.pnlMin.Name = "pnlMin";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.button1, "button1");
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.ImageList = this.imageList1;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.securityToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.tsVisible,
            this.tsFunction,
            this.helpToolStripMenuItem});
            this.menuStrip1.MdiWindowListItem = this.windowToolStripMenuItem;
            this.menuStrip1.Name = "menuStrip1";
            // 
            // securityToolStripMenuItem
            // 
            this.securityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.logoutToolStripMenuItem,
            this.changePasswordToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.toolStripSeparator2,
            this.skinToolStripMenuItem,
            this.toolStripSeparator3,
            this.tsHideBottom,
            this.tsHideLabel,
            this.tsShowHost});
            this.securityToolStripMenuItem.Name = "securityToolStripMenuItem";
            resources.ApplyResources(this.securityToolStripMenuItem, "securityToolStripMenuItem");
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            resources.ApplyResources(this.loginToolStripMenuItem, "loginToolStripMenuItem");
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            resources.ApplyResources(this.logoutToolStripMenuItem, "logoutToolStripMenuItem");
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            resources.ApplyResources(this.changePasswordToolStripMenuItem, "changePasswordToolStripMenuItem");
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // skinToolStripMenuItem
            // 
            this.skinToolStripMenuItem.Name = "skinToolStripMenuItem";
            resources.ApplyResources(this.skinToolStripMenuItem, "skinToolStripMenuItem");
            this.skinToolStripMenuItem.Click += new System.EventHandler(this.skinToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // tsHideBottom
            // 
            this.tsHideBottom.CheckOnClick = true;
            this.tsHideBottom.Name = "tsHideBottom";
            resources.ApplyResources(this.tsHideBottom, "tsHideBottom");
            this.tsHideBottom.Click += new System.EventHandler(this.tsHint_Click);
            // 
            // tsHideLabel
            // 
            this.tsHideLabel.CheckOnClick = true;
            this.tsHideLabel.Name = "tsHideLabel";
            resources.ApplyResources(this.tsHideLabel, "tsHideLabel");
            this.tsHideLabel.Click += new System.EventHandler(this.tsHideLabel_Click);
            // 
            // tsShowHost
            // 
            this.tsShowHost.CheckOnClick = true;
            this.tsShowHost.Name = "tsShowHost";
            resources.ApplyResources(this.tsShowHost, "tsShowHost");
            this.tsShowHost.Click += new System.EventHandler(this.tsShowHost_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cascadeToolStripMenuItem,
            this.tileHorizontalToolStripMenuItem,
            this.tileVerticallyToolStripMenuItem,
            this.toolStripSeparator1,
            this.minimizeAllToolStripMenuItem,
            this.showAllToolStripMenuItem,
            this.closeAllToolStripMenuItem});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            resources.ApplyResources(this.windowToolStripMenuItem, "windowToolStripMenuItem");
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            resources.ApplyResources(this.cascadeToolStripMenuItem, "cascadeToolStripMenuItem");
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.cascadeToolStripMenuItem_Click);
            // 
            // tileHorizontalToolStripMenuItem
            // 
            this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
            resources.ApplyResources(this.tileHorizontalToolStripMenuItem, "tileHorizontalToolStripMenuItem");
            this.tileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.tileHorizontalToolStripMenuItem_Click);
            // 
            // tileVerticallyToolStripMenuItem
            // 
            this.tileVerticallyToolStripMenuItem.Name = "tileVerticallyToolStripMenuItem";
            resources.ApplyResources(this.tileVerticallyToolStripMenuItem, "tileVerticallyToolStripMenuItem");
            this.tileVerticallyToolStripMenuItem.Click += new System.EventHandler(this.tileVerticallyToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // minimizeAllToolStripMenuItem
            // 
            this.minimizeAllToolStripMenuItem.Name = "minimizeAllToolStripMenuItem";
            resources.ApplyResources(this.minimizeAllToolStripMenuItem, "minimizeAllToolStripMenuItem");
            this.minimizeAllToolStripMenuItem.Click += new System.EventHandler(this.minimizeAllToolStripMenuItem_Click);
            // 
            // showAllToolStripMenuItem
            // 
            this.showAllToolStripMenuItem.Name = "showAllToolStripMenuItem";
            resources.ApplyResources(this.showAllToolStripMenuItem, "showAllToolStripMenuItem");
            this.showAllToolStripMenuItem.Click += new System.EventHandler(this.showAllToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            resources.ApplyResources(this.closeAllToolStripMenuItem, "closeAllToolStripMenuItem");
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.closeAllToolStripMenuItem_Click);
            // 
            // tsVisible
            // 
            this.tsVisible.Name = "tsVisible";
            resources.ApplyResources(this.tsVisible, "tsVisible");
            // 
            // tsFunction
            // 
            resources.ApplyResources(this.tsFunction, "tsFunction");
            this.tsFunction.BackColor = System.Drawing.Color.White;
            this.tsFunction.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tsFunction.Name = "tsFunction";
            this.tsFunction.ReadOnly = true;
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // panelFunction
            // 
            this.panelFunction.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.panelFunction, "panelFunction");
            this.panelFunction.Controls.Add(this.panelList);
            this.panelFunction.Controls.Add(this.pnlMin);
            this.panelFunction.Name = "panelFunction";
            this.panelFunction.Leave += new System.EventHandler(this.panelList_Leave);
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            // 
            // toolStripButton1
            // 
            resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // pnlMenu
            // 
            this.pnlMenu.Controls.Add(this.pnlMenu2);
            this.pnlMenu.Controls.Add(this.pnlMenu1);
            resources.ApplyResources(this.pnlMenu, "pnlMenu");
            this.pnlMenu.Name = "pnlMenu";
            // 
            // pnlMenu2
            // 
            resources.ApplyResources(this.pnlMenu2, "pnlMenu2");
            this.pnlMenu2.Controls.Add(this.lablLogin);
            this.pnlMenu2.Controls.Add(this.pnlMenuStrip);
            this.pnlMenu2.Controls.Add(this.lablMES);
            this.pnlMenu2.Name = "pnlMenu2";
            // 
            // lablLogin
            // 
            resources.ApplyResources(this.lablLogin, "lablLogin");
            this.lablLogin.BackColor = System.Drawing.Color.Transparent;
            this.lablLogin.Name = "lablLogin";
            this.lablLogin.Tag = "User Name: ";
            // 
            // pnlMenuStrip
            // 
            this.pnlMenuStrip.BackColor = System.Drawing.Color.Transparent;
            this.pnlMenuStrip.Controls.Add(this.menuStrip1);
            resources.ApplyResources(this.pnlMenuStrip, "pnlMenuStrip");
            this.pnlMenuStrip.Name = "pnlMenuStrip";
            // 
            // lablMES
            // 
            this.lablMES.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lablMES, "lablMES");
            this.lablMES.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(98)))), ((int)(((byte)(159)))));
            this.lablMES.Name = "lablMES";
            // 
            // pnlMenu1
            // 
            resources.ApplyResources(this.pnlMenu1, "pnlMenu1");
            this.pnlMenu1.Name = "pnlMenu1";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.ForeColor = System.Drawing.Color.Transparent;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMin
            // 
            this.btnMin.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnMin, "btnMin");
            this.btnMin.ForeColor = System.Drawing.Color.Transparent;
            this.btnMin.Name = "btnMin";
            this.btnMin.UseVisualStyleBackColor = false;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            // 
            // pnlTitle
            // 
            resources.ApplyResources(this.pnlTitle, "pnlTitle");
            this.pnlTitle.Controls.Add(this.btnResize);
            this.pnlTitle.Controls.Add(this.lablTitle);
            this.pnlTitle.Controls.Add(this.btnMin);
            this.pnlTitle.Controls.Add(this.btnClose);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.DoubleClick += new System.EventHandler(this.btnResize_Click);
            this.pnlTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseDown);
            // 
            // btnResize
            // 
            this.btnResize.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnResize, "btnResize");
            this.btnResize.ForeColor = System.Drawing.Color.Transparent;
            this.btnResize.Name = "btnResize";
            this.btnResize.Tag = "1";
            this.btnResize.UseVisualStyleBackColor = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // lablTitle
            // 
            resources.ApplyResources(this.lablTitle, "lablTitle");
            this.lablTitle.BackColor = System.Drawing.Color.Transparent;
            this.lablTitle.Name = "lablTitle";
            this.lablTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseDown);
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.pnlBottom, "pnlBottom");
            this.pnlBottom.Controls.Add(this.lablCopyRight);
            this.pnlBottom.Controls.Add(this.progressBar1);
            this.pnlBottom.Controls.Add(this.pnlCopyRight);
            this.pnlBottom.Name = "pnlBottom";
            // 
            // lablCopyRight
            // 
            resources.ApplyResources(this.lablCopyRight, "lablCopyRight");
            this.lablCopyRight.BackColor = System.Drawing.Color.Transparent;
            this.lablCopyRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(98)))), ((int)(((byte)(159)))));
            this.lablCopyRight.Name = "lablCopyRight";
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // pnlCopyRight
            // 
            resources.ApplyResources(this.pnlCopyRight, "pnlCopyRight");
            this.pnlCopyRight.Name = "pnlCopyRight";
            // 
            // splitterRight
            // 
            this.splitterRight.BackColor = System.Drawing.Color.DimGray;
            this.splitterRight.Cursor = System.Windows.Forms.Cursors.SizeWE;
            resources.ApplyResources(this.splitterRight, "splitterRight");
            this.splitterRight.Name = "splitterRight";
            this.splitterRight.TabStop = false;
            this.splitterRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.splitter_MouseDown);
            this.splitterRight.MouseMove += new System.Windows.Forms.MouseEventHandler(this.splitter_MouseMove);
            this.splitterRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.splitter_MouseUp);
            // 
            // splitterBottom
            // 
            this.splitterBottom.BackColor = System.Drawing.Color.DimGray;
            this.splitterBottom.Cursor = System.Windows.Forms.Cursors.SizeNS;
            resources.ApplyResources(this.splitterBottom, "splitterBottom");
            this.splitterBottom.Name = "splitterBottom";
            this.splitterBottom.TabStop = false;
            this.splitterBottom.MouseDown += new System.Windows.Forms.MouseEventHandler(this.splitter_MouseDown);
            this.splitterBottom.MouseMove += new System.Windows.Forms.MouseEventHandler(this.splitter_MouseMove);
            this.splitterBottom.MouseUp += new System.Windows.Forms.MouseEventHandler(this.splitter_MouseUp);
            // 
            // splitterLeft
            // 
            this.splitterLeft.BackColor = System.Drawing.Color.DimGray;
            this.splitterLeft.Cursor = System.Windows.Forms.Cursors.SizeWE;
            resources.ApplyResources(this.splitterLeft, "splitterLeft");
            this.splitterLeft.Name = "splitterLeft";
            this.splitterLeft.TabStop = false;
            this.splitterLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.splitter_MouseDown);
            this.splitterLeft.MouseMove += new System.Windows.Forms.MouseEventHandler(this.splitter_MouseMove);
            this.splitterLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.splitter_MouseUp);
            // 
            // splitterTop
            // 
            this.splitterTop.BackColor = System.Drawing.Color.DimGray;
            this.splitterTop.Cursor = System.Windows.Forms.Cursors.SizeNS;
            resources.ApplyResources(this.splitterTop, "splitterTop");
            this.splitterTop.Name = "splitterTop";
            this.splitterTop.TabStop = false;
            this.splitterTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.splitter_MouseDown);
            this.splitterTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.splitter_MouseMove);
            this.splitterTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.splitter_MouseUp);
            // 
            // fMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panelFunction);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlMenu);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.splitterRight);
            this.Controls.Add(this.splitterBottom);
            this.Controls.Add(this.splitterLeft);
            this.Controls.Add(this.splitterTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "fMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formMain_FormClosing);
            this.Load += new System.EventHandler(this.formMain_Load);
            this.MdiChildActivate += new System.EventHandler(this.fMain_MdiChildActivate);
            this.SizeChanged += new System.EventHandler(this.formMain_SizeChanged);
            this.panelList.ResumeLayout(false);
            this.pnlTreeView.ResumeLayout(false);
            this.pnlTreeView.PerformLayout();
            this.pnlFunType.ResumeLayout(false);
            this.pnlFunType.PerformLayout();
            this.pnlModule.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsModule)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlMin.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelFunction.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlMenu.ResumeLayout(false);
            this.pnlMenu2.ResumeLayout(false);
            this.pnlMenu2.PerformLayout();
            this.pnlMenuStrip.ResumeLayout(false);
            this.pnlMenuStrip.PerformLayout();
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem securityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVerticallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem minimizeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem skinToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel panelList;
        private System.Windows.Forms.Panel pnlMin;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelFunction;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Panel pnlModule;
        private System.Windows.Forms.BindingSource bsModule;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Panel pnlMenu1;
        private System.Windows.Forms.Label lablMES;
        private System.Windows.Forms.Panel pnlMenu2;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Panel pnlFunType;
        private System.Windows.Forms.ComboBox combFunType;
        private System.Windows.Forms.Label lablFunction;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lablModule;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlCopyRight;
        private System.Windows.Forms.ToolStripMenuItem tsVisible;
        private System.Windows.Forms.ToolStripTextBox tsFunction;
        private System.Windows.Forms.Panel pnlMenuStrip;
        private System.Windows.Forms.Label lablCopyRight;
        private System.Windows.Forms.Panel pnlTreeView;
        private System.Windows.Forms.ToolStripMenuItem tsHideBottom;
        private System.Windows.Forms.ToolStripMenuItem tsHideLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Label lablLogin;
        private System.Windows.Forms.Label lablTitle;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripMenuItem tsShowHost;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Splitter splitterRight;
        private System.Windows.Forms.Splitter splitterBottom;
        private System.Windows.Forms.Splitter splitterLeft;
        private System.Windows.Forms.Splitter splitterTop;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ComboBox combModule;
        private System.Windows.Forms.Button btnLargeFont;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFont;
        private System.Windows.Forms.Button btnSmallFont;

    }
}

