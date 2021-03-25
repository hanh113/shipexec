using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Runtime.Remoting.Channels;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using System.Reflection;
using System.Xml;
using System.Runtime.Remoting;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Data.OracleClient;
using System.Runtime.InteropServices;
using MESModel;
using System.Threading.Tasks;
using ClientUtilsDll;

namespace EDIPPS
{
    public partial class fMain : Form
    {

        ButtonArray buttonArray;
        internal static string g_download = "0";  // 不為0~2, 則不下載
        internal static bool g_bDownLoadImage = false, g_bRestart = false;
        object[][] downloadList;
        bool g_bManualSelect = false, g_bFunctionHide = false;
        Form activeForm = null;
        internal static string g_APServer = null;
        internal static string g_Host = null;
        string g_sFileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.ToString();
        internal static int g_iModuleStyle = 2, g_iModuleSelect = 0;
        int formHeight = 0, formWidth = 0, formLeft, formTop;
        internal static Font fFont, fLargeFont, fSmallFont;  // 預設字型, 大字型, 小字型

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        [DllImport("user32.dll")]
        public static extern void SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int X, int Y, int width, int height, uint flags);

        public static void SetWinFullScreen(IntPtr hwnd)
        {
            Screen[] screens = Screen.AllScreens;
            Screen screen = screens[0];
            SetWindowPos(hwnd, IntPtr.Zero, 0, 0, screen.WorkingArea.Width, screen.WorkingArea.Height, 64);
        }
        private Font SetFont(string[] sFont)
        {
            string[] sFontStyle = sFont[2].Split(',');
            FontStyle fontStyle = new FontStyle();
            foreach (string style in sFontStyle)
                switch (style.Trim())
                {
                    case "Bold":
                        fontStyle |= FontStyle.Bold;
                        break;
                    case "Italic":
                        fontStyle |= FontStyle.Italic;
                        break;
                    case "Strikeout":
                        fontStyle |= FontStyle.Strikeout;
                        break;
                    case "Underline":
                        fontStyle |= FontStyle.Underline;
                        break;
                }
            return new Font(sFont[0].Trim(), float.Parse(sFont[1].Trim()), fontStyle);
        }
        public fMain()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(OnErrorOccur);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            ClientUtils.fClientLang = GetValue("Setting", "Culture", CultureInfo.CurrentCulture.Name);
            if (ClientUtils.fClientLang == null)
                ClientUtils.fClientLang = CultureInfo.CurrentCulture.Name;
            InitializeComponent();
            buttonArray = new ButtonArray(panelList);
            string[] sFont = GetValue("Font", "Default", "Arial; 10;").Split(';');
            fFont = SetFont(sFont);
            ClientUtils.fModuleFont = fFont;
            sFont = GetValue("Font", "Large", "Arial; 12;").Split(';');
            fLargeFont = SetFont(sFont);
            sFont = GetValue("Font", "Small", "Arial; 8;").Split(';');
            fSmallFont = SetFont(sFont);
            ClientUtils.bChangeFont = bool.Parse(GetValue("Font", "ChangeFlag", "False"));
            ClientUtils.bLoadImage = bool.Parse(GetValue("Module", "LoadImage", "False"));
            btnLargeFont.Visible = ClientUtils.bChangeFont;
            btnFont.Visible = ClientUtils.bChangeFont;
            btnSmallFont.Visible = ClientUtils.bChangeFont;
            string sStyle = GetValue("Module", "Style", "ComboBox");
            switch (sStyle)
            {
                case "Button":
                    g_iModuleStyle = 0;
                    pnlTreeView.Padding = new System.Windows.Forms.Padding(0);
                    lablFunction.Visible = false;
                    break;
                case "ComboBox":
                    g_iModuleStyle = 1;
                    pnlModule.Visible = true;
                    pnlTreeView.Visible = true;
                    break;
                default:
                    g_iModuleStyle = 2;
                    pnlModule.Visible = true;
                    pnlFunType.Visible = true;
                    pnlTreeView.Visible = true;
                    break;
            }
            g_iModuleSelect = int.Parse(GetValue("Module", "SelectMode", "0"));
            tsHideBottom.Checked = bool.Parse(GetValue("Setting", "BottomHide", "False"));
            pnlBottom.Visible = !tsHideBottom.Checked;
            tsHideLabel.Checked = bool.Parse(GetValue("Setting", "LabelHide", "False"));
            tsShowHost.Checked = bool.Parse(GetValue("Setting", "ShowHost", "False"));
            lablMES.Visible = !tsHideLabel.Checked;
            g_bFunctionHide = bool.Parse(GetValue("Setting", "FunctionHide", "False"));
            functionHide();
            int iPort = 8086;
            int.TryParse(GetValue("Setting", "BroadCast", "8086"), out iPort);
        }
        private void functionHide()
        {
            if (g_bFunctionHide)
                button1.ImageIndex = 4;
            else
                button1.ImageIndex = 3;
            panelFunction.Visible = !g_bFunctionHide;
            toolStrip1.Visible = g_bFunctionHide;
        }
        private void formMain_Load(object sender, EventArgs e)
        {



            classChromaLog.deleteOldLog();
            Program.skinName = GetValue("Setting", "Skin", "Default");
            Login();
            lablCopyRight.Text = ClientUtils.SetLanguage(AssemblyCopyright, "", Program.g_sFileName);
            lablTitle.Tag = ClientUtils.SetLanguage(AssemblyTitle, "", Program.g_sFileName) + " (" + g_sFileVersion + ")";
            lablTitle.Text = lablTitle.Tag.ToString();
            if (tsShowHost.Checked)
                lablTitle.Text = lablTitle.Text + " - [ " + g_Host + " ] - [ " + ClientUtils.url + " ]";
            if (pnlMenu.Height == 26)
                lablTitle.Text = lablTitle.Text + " - [ " + ClientUtils.fUserName + " ]";
            this.Tag = lablTitle.Text;
        }
        internal static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }
        internal static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                    return "";
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }
        internal static string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        internal static string GetValue(string section, string field, string sValue)
        {
            string fileName = Application.StartupPath + Path.DirectorySeparatorChar + "PPS.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            string sFilter;
            sFilter = "//" + section;
            XmlNode xmlNode = xmlDoc.SelectSingleNode(sFilter);
            if (xmlNode == null)
                return sValue;
            else
                if (xmlNode.Attributes[field] == null)
                return sValue;
            else
                return xmlNode.Attributes[field].Value;
        }
        internal static void SetValue(string Node, string sField, string Value)
        {
            string xmlFile = Application.StartupPath + Path.DirectorySeparatorChar + "PPS.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFile);
            XmlNode xElmntRoot = xmlDoc.SelectSingleNode("//" + Node);
            if (xElmntRoot == null)
            {
                xElmntRoot = xmlDoc.DocumentElement;
                XmlElement elem = xmlDoc.CreateElement(Node);
                elem.SetAttribute(sField, Value);
                xElmntRoot.AppendChild(elem);

            }
            else
            {
                if (xElmntRoot.Attributes[sField] == null)
                {
                    XmlAttribute attr = xElmntRoot.Attributes[sField];
                    attr = xElmntRoot.OwnerDocument.CreateAttribute(sField);
                    attr.Value = Value;
                    xElmntRoot.Attributes.Append(attr);
                }
                else
                    xElmntRoot.Attributes[sField].Value = Value;
            }
            xmlDoc.Save(xmlFile);
        }
        internal static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            if (ex is TargetInvocationException && ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            fError frm = new fError(ex.Message, ex.StackTrace);
            frm.ShowDialog();
            frm.Dispose();
            SaveLog(LogType.Error, ex.Message, ex.StackTrace);
        }
        internal protected static void OnErrorOccur(object sender, ThreadExceptionEventArgs ex)
        {
            Exception e = (Exception)ex.Exception;
            if (e is TargetInvocationException && e.InnerException != null)
            {
                e = e.InnerException;
            }
            fError frm = new fError(e.Message, e.StackTrace);
            frm.ShowDialog();
            frm.Dispose();
            SaveLog(LogType.Error, e.Message, e.StackTrace);
        }
        private static void SaveLog(LogType f_LogType, string f_sFunction, string f_sMessage)
        {
            classChromaLog.addLog(f_LogType, f_sFunction, f_sMessage);
        }
        private void Login()
        {
            fLogin Form2 = new fLogin();
            DialogResult dr = Form2.ShowDialog();
            if (dr == DialogResult.OK || dr == DialogResult.Yes)
            {
                StartHost();
                loginToolStripMenuItem.Visible = false;
                changePasswordToolStripMenuItem.Visible = true;
                switch (dr)
                {
                    case DialogResult.Yes:
                        functionHide();


                        DataSet dsTemp1 = ClientUtils.GetProgram("N");
                        DataSet dsTemp = new DataSet();
                        DataTable dtt = new DataTable();

                        dsTemp.Tables.Add(new DataTable());
                        dtt.Columns.Add("SHOWNAME");
                        dtt.Columns.Add("EXE_FILENAME");
                        dtt.Columns.Add("PROGRAM");
                        dtt.Columns.Add("FUN_TYPE_IDX");
                        for (int n = 0; n < dsTemp1.Tables[0].Rows.Count; n++)
                        {
                            //if (dsTemp1.Tables[0].Rows[n][0].ToString() == "P09 出货作业")
                            //{
                            DataRow row = dtt.NewRow();
                            row["SHOWNAME"] = dsTemp1.Tables[0].Rows[n][0].ToString();

                            row["EXE_FILENAME"] = dsTemp1.Tables[0].Rows[n][1].ToString();

                            row["PROGRAM"] = dsTemp1.Tables[0].Rows[n][2].ToString();

                            row["FUN_TYPE_IDX"] = dsTemp1.Tables[0].Rows[n][3].ToString();
                            dtt.Rows.Add(row);



                            // }
                        }
                        dsTemp.Tables.Add(dtt);

                        int i = 0;
                        downloadList = new object[dsTemp.Tables[1].Rows.Count][];
                        if (g_iModuleStyle != 0)
                        {
                            bsModule.DataSource = dsTemp;
                            bsModule.DataMember = dsTemp.Tables[1].ToString();
                            combModule.DisplayMember = "SHOWNAME";
                            combModule.ValueMember = "EXE_FILENAME";
                            pnlTreeView.Visible = true;
                        }
                        else
                            pnlTreeView.Visible = false;
                        if (dsTemp.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow row in dsTemp.Tables[1].Rows)
                            {
                                downloadList[i] = new object[] { row[2].ToString(), true };
                                if (g_iModuleStyle == 0)
                                {
                                    buttonArray.AddNewButton(row[0].ToString(), row[1].ToString());
                                    buttonArray[i].Click += new EventHandler(this.functionButton_Click);
                                }
                                i++;
                            }
                        }
                        break;
                    case DialogResult.OK:
                        downloadList = new object[1][];
                        downloadList[0] = new object[] { "Data Center", true };
                        if (g_iModuleStyle == 0)
                        {
                            buttonArray.AddNewButton("Data Center", "DataCenter");
                            buttonArray[0].Click += new EventHandler(this.functionButton_Click);
                            buttonArray[0].BackColor = Color.Transparent;
                            buttonArray[0].FlatStyle = FlatStyle.Flat;
                        }
                        else
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.Add("SHOWNAME");
                            dt.Columns.Add("EXE_FILENAME");
                            dt.Rows.Add();
                            dt.Rows[0]["SHOWNAME"] = "Data Center";
                            dt.Rows[0]["EXE_FILENAME"] = "DataCenter";
                            dsTemp = new DataSet();
                            dsTemp.Tables.Add(dt);
                            bsModule.DataSource = dsTemp;
                            bsModule.DataMember = dsTemp.Tables[1].ToString();
                            combModule.DisplayMember = "SHOWNAME";
                            combModule.ValueMember = "EXE_FILENAME";
                        }
                        break;
                }
                Form2.Dispose();
                this.WindowState = FormWindowState.Maximized;
                SetWinFullScreen(this.Handle);
                ClientUtils.SetLanguage(this, "");
                lablLogin.Text = ClientUtils.SetLanguage(lablLogin.Tag.ToString(), "", Program.g_sFileName) + ClientUtils.fUserName;
                lablLogin.Left = pnlMenu2.Width - lablLogin.Width - 5;
                lablLogin.Visible = true;
                if (g_iModuleStyle != 0 && combModule.Items.Count > 0)
                {
                    combModule.SelectedIndex = g_iModuleSelect;
                    ModuleChange();
                    lablModule.Tag = combModule.Text;
                }
                if (g_bDownLoadImage && !g_bRestart)
                    ReplaceFile();
                ChangeBackground();
                if (string.IsNullOrEmpty(Program.g_sUserID))
                {
                    logoutToolStripMenuItem.Visible = true;
                    ClientStatus.RegisterService();
                }
            }
            string dir = Application.StartupPath + Path.DirectorySeparatorChar + "SkinTemp";
            if (Directory.Exists(dir))
                Directory.Delete(dir, true);
        }
        private void StartHost()
        {
            try
            {
                Service.WcfHostingService.StartWcfHost();
            }
            catch (Exception ex)
            {
            }
        }

        private void ReplaceFile()
        {
            string dir = Application.StartupPath + Path.DirectorySeparatorChar + "SkinTemp";
            string sPath = Program.skinPath + Program.skinName;
            if (Directory.Exists(dir))
            {
                string[] localFiles = Directory.GetFiles(dir);
                foreach (string localFile in localFiles)
                {
                    string sFileName = Path.GetFileName(localFile).ToUpper();
                    File.Copy(localFile, Path.Combine(sPath, Path.GetFileName(localFile)), true);
                }
            }
        }
        private void ShowList(string program, string exeName, TreeView treeView, string sfunType)
        {
            DataSet dsTemp = ClientUtils.GetFunction(program, "N");
            treeView.Tag = exeName;
            treeView.Text = program;
            treeView.Nodes.Clear();
            string sfun = "", stemp = "";
            int j = 1;
            ListBox slfun = new ListBox();
            foreach (DataRow row in dsTemp.Tables[0].Rows)
            {
                if (row[0].ToString() != stemp)
                {
                    slfun.Items.Add("0");
                    j = 0;
                }
                j++;
                slfun.Items[slfun.Items.Count - 1] = j.ToString();
                stemp = row[0].ToString();
            }
            int iIndex = 0;
            if (string.IsNullOrEmpty(sfunType))
            {
                j = 0;
                stemp = "";
                foreach (DataRow row in dsTemp.Tables[0].Rows)
                {
                    // Function Type
                    sfun = row[0].ToString();
                    if (stemp != sfun)
                    {
                        if (slfun.Items[iIndex].ToString() == "1")
                        {
                            treeView.Nodes.Add(row[1].ToString());
                            // Dll File Name, Function
                            treeView.Nodes[iIndex].Name = row[2].ToString() + "," + row[5].ToString();
                            // Parameter
                            treeView.Nodes[iIndex].Tag = row[3].ToString();
                            treeView.Nodes[iIndex].ImageIndex = 1;
                            treeView.Nodes[iIndex].SelectedImageIndex = 1;
                            // Form Name
                            treeView.Nodes[iIndex].ToolTipText = row[4].ToString();
                        }
                        else
                        {
                            treeView.Nodes.Add(sfun);
                        }
                        iIndex++;
                        j = 0;
                    }
                    if (slfun.Items[iIndex - 1].ToString() != "1")
                    {
                        // Function Name
                        treeView.Nodes[iIndex - 1].Nodes.Add(row[1].ToString());
                        treeView.Nodes[iIndex - 1].StateImageIndex = 2;
                        // Dll File Name
                        treeView.Nodes[iIndex - 1].Nodes[j].Name = row[2].ToString() + "," + row[5].ToString();
                        // Parameter
                        treeView.Nodes[iIndex - 1].Nodes[j].Tag = row[3].ToString();
                        treeView.Nodes[iIndex - 1].Nodes[j].ImageIndex = 1;
                        treeView.Nodes[iIndex - 1].Nodes[j].SelectedImageIndex = 1;
                        // Form Name
                        treeView.Nodes[iIndex - 1].Nodes[j].ToolTipText = row[4].ToString();
                        j++;
                    }
                    stemp = sfun;
                }
            }
            else
            {
                foreach (DataRow row in dsTemp.Tables[0].Rows)
                {
                    sfun = row[0].ToString();
                    if (sfun == sfunType)
                    {
                        iIndex = slfun.Items.IndexOf(sfunType);
                        // Function Name
                        treeView.Nodes.Add(row[1].ToString());
                        treeView.Nodes[treeView.Nodes.Count - 1].StateImageIndex = 2;
                        // Dll File Name
                        treeView.Nodes[treeView.Nodes.Count - 1].Name = row[2].ToString() + "," + row[5].ToString();
                        // Parameter
                        treeView.Nodes[treeView.Nodes.Count - 1].Tag = row[3].ToString();
                        treeView.Nodes[treeView.Nodes.Count - 1].ImageIndex = 1;
                        treeView.Nodes[treeView.Nodes.Count - 1].SelectedImageIndex = 1;
                        // Form Name
                        treeView.Nodes[treeView.Nodes.Count - 1].ToolTipText = row[4].ToString();
                    }
                }
            }
            treeView.Visible = true;
        }

        private void functionButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string exeName = (String)btn.Tag;
            int isender = int.Parse(btn.Name.Substring("Button".Length)) - 1;
            string program = downloadList[isender][0].ToString();
            lablModule.Tag = btn.Text;
            pnlTreeView.Visible = true;
            ShowList(program, btn.Tag.ToString(), treeView1, "");
            bool downloadFile = false;
            if (((g_download == "1") && ((bool)downloadList[isender][1])) || (g_download == "2"))
                downloadFile = true;
            if (downloadFile)
                CheckFile(exeName);
            if (g_download == "1")
                downloadList[isender][1] = false;
            panelList.Visible = false;
            for (int i = isender; i >= 0; i--)
            {
                buttonArray[i].SendToBack();
                buttonArray[i].Dock = DockStyle.Top;
            }
            for (int i = isender + 1; i <= buttonArray.Count - 1; i++)
            {
                buttonArray[i].SendToBack();
                buttonArray[i].Dock = DockStyle.Bottom;
            }
            panelList.Visible = true;
            ShowMainForm(program);
        }

        private void CheckFile(string program)
        {
            List<FileObject> filelist = new List<FileObject>();
            string version;
            string fileName;
            string path = Application.StartupPath + Path.DirectorySeparatorChar + program + Path.DirectorySeparatorChar;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            filelist = ClientUtils.GetFileLists(@"\LoadClient\" + program);
            if (filelist != null)
            {
                progressBar1.Maximum = filelist.Count;
                progressBar1.Value = 0;
                progressBar1.Visible = true;
                for (int i = 0; i <= filelist.Count - 1; i++)
                {
                    if (filelist[i] == null) continue;
                    FileObject fileInfo = (FileObject)filelist[i];
                    fileName = path + fileInfo.fileName;
                    if (File.Exists(fileName))
                    {
                        try
                        {
                            version = FileVersionInfo.GetVersionInfo(@fileName).FileVersion.ToString();
                            if (version != fileInfo.version)
                                DownloadFile(@"\LoadClient\" + program, fileInfo.fileName, fileName);
                        }
                        catch
                        {
                            DateTime fileAge = File.GetLastWriteTime(fileName);
                            if (fileAge < fileInfo.fileAge)
                                DownloadFile(@"\LoadClient\" + program, fileInfo.fileName, fileName);
                        }
                    }
                    else
                        DownloadFile(@"\LoadClient\" + program, fileInfo.fileName, fileName);
                    progressBar1.Increment(1);
                }
                progressBar1.Visible = false;
            }
        }
        internal static void DownloadFile(string program, string fromFile, string destFile)
        {
            string sDir = Path.GetDirectoryName(destFile);
            if (!Directory.Exists(sDir))
                Directory.CreateDirectory(sDir);
            if (File.Exists(destFile))
                File.Delete(destFile);
            byte[] data = ClientUtils.DownloadFileByte(program, fromFile);
            FileStream localFS = new FileStream(destFile, FileMode.Create, FileAccess.Write);
            localFS.Write(data, 0, data.Length);
            localFS.Close();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fChangePwd Form2 = new fChangePwd();
            Form2.ShowDialog();
        }

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult dr = ClientUtils.ShowMessage("Exit", 2, "", Program.g_sFileName);
                if (dr == DialogResult.Yes)
                {

                    try
                    {
                        if (!string.IsNullOrEmpty(Program.g_sUserID))
                        {
                            Computer ComputerName = new Computer();
                            ClientUtils.Logout(ComputerName.Name);
                        }
                        //Program.mutex.ReleaseMutex();
                    }
                    catch
                    {
                    }
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void tileVerticallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void minimizeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] mdiChild = this.MdiChildren;

            foreach (Form childForm in mdiChild)
                childForm.WindowState = FormWindowState.Minimized;
        }

        private void showAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] mdiChild = this.MdiChildren;

            foreach (Form childForm in mdiChild)
                childForm.WindowState = FormWindowState.Normal;
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] mdiChild = this.MdiChildren;

            foreach (Form childForm in mdiChild)
                childForm.Close();
        }

        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = ClientUtils.ShowMessage("Logout?", 2, "", Program.g_sFileName);
            lablTitle.Text = lablTitle.Tag.ToString();
            if (dr == DialogResult.Yes)
            {
                panelFunction.Visible = false;
                Form[] mdiChild = this.MdiChildren;
                foreach (Form childForm in mdiChild)
                    childForm.Close();
                for (int i = buttonArray.Count - 1; i >= 0; i--)
                    buttonArray.Remove();
                treeView1.Nodes.Clear();
                toolStrip1.Visible = false;
                changePasswordToolStripMenuItem.Visible = false;
                Computer ComputerName = new Computer();
                ClientUtils.Logout(ComputerName.Name);
                ClientStatus.UnregisterService();
                loginToolStripMenuItem.Visible = true;
                logoutToolStripMenuItem.Visible = false;
                lablLogin.Visible = false;
                Login();
            }
        }

        private void skinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fSkin Form2 = new fSkin();
            DialogResult dr = Form2.ShowDialog();
            if (dr == DialogResult.Yes)
            {
                if (Form2.g_sSkinName != Program.skinName)
                {
                    Program.skinName = Form2.g_sSkinName;
                    this.WindowState = FormWindowState.Minimized;
                    this.WindowState = FormWindowState.Maximized;
                    ChangeBackground();
                }
                btnSmallFont.Visible = ClientUtils.bChangeFont;
                btnFont.Visible = ClientUtils.bChangeFont;
                btnLargeFont.Visible = ClientUtils.bChangeFont;
            }
        }
        internal void ChangeBackground()
        {
            string sPath = Program.skinPath + Program.skinName + Path.DirectorySeparatorChar;
            if (File.Exists(sPath + "button.jpg"))
                for (int i = buttonArray.Count - 1; i >= 0; i--)
                    buttonArray[i].BackgroundImage = Image.FromFile(sPath + "button.jpg");
            else
                for (int i = buttonArray.Count - 1; i >= 0; i--)
                    buttonArray[i].BackgroundImage = null;
            if (File.Exists(sPath + "Title.jpg"))
                pnlTitle.BackgroundImage = Image.FromFile(sPath + "Title.jpg");
            else
                pnlTitle.BackgroundImage = global::EDIPPS.Properties.Resources.Title;
            if (File.Exists(sPath + "MinBar.jpg"))
                pnlMin.BackgroundImage = Image.FromFile(sPath + "MinBar.jpg");
            else
                pnlMin.BackgroundImage = global::EDIPPS.Properties.Resources.MinBar;
            if (File.Exists(sPath + "MN01.jpg"))
            {
                pnlMenu1.BackgroundImage = Image.FromFile(sPath + "MN01.jpg");
                if (File.Exists(sPath + "MN02.jpg"))
                    pnlMenu2.BackgroundImage = Image.FromFile(sPath + "MN02.jpg");
                else
                    pnlMenu2.BackgroundImage = null;
                menuStrip1.Parent = pnlMenuStrip;
                menuStrip1.BackgroundImage = null;
                menuStrip1.BackColor = Color.Transparent;
                pnlMenu.Height = 71;
                tsHideLabel.Visible = true;
            }
            else
            {
                pnlMenu1.BackgroundImage = null;
                pnlMenu2.BackgroundImage = null;
                pnlMenu.Height = 26;
                menuStrip1.Parent = pnlMenu;
                menuStrip1.BackColor = SystemColors.Control;
                if (File.Exists(sPath + "MenuStrip.jpg"))
                    menuStrip1.BackgroundImage = Image.FromFile(sPath + "MenuStrip.jpg");
                else
                    menuStrip1.BackgroundImage = null;
                tsHideLabel.Visible = false;
            }
            if (File.Exists(sPath + "ToolStrip.jpg"))
                toolStrip1.BackgroundImage = Image.FromFile(sPath + "ToolStrip.jpg");
            else
                toolStrip1.BackgroundImage = null;
            if (File.Exists(sPath + "function.jpg"))
                panelFunction.BackgroundImage = Image.FromFile(sPath + "function.jpg");
            else
                panelFunction.BackgroundImage = null;
            if (File.Exists(sPath + "Bottom.jpg"))
                pnlBottom.BackgroundImage = Image.FromFile(sPath + "Bottom.jpg");
            else
                pnlBottom.BackgroundImage = null;
            if (File.Exists(sPath + "Module.jpg"))
                pnlModule.BackgroundImage = Image.FromFile(sPath + "Module.jpg");
            else
                pnlModule.BackgroundImage = null;
            pnlFunType.BackgroundImage = pnlModule.BackgroundImage;
            pnlTreeView.BackgroundImage = pnlModule.BackgroundImage;
            if (File.Exists(sPath + "btnClose.jpg"))
                btnClose.Image = Image.FromFile(sPath + "btnClose.jpg");
            else
                btnClose.Image = global::EDIPPS.Properties.Resources.btnClose;
            if (this.WindowState == FormWindowState.Normal)
            {
                if (File.Exists(sPath + "btnMax.jpg"))
                    btnResize.Image = Image.FromFile(sPath + "btnMax.jpg");
                else
                    btnResize.Image = global::EDIPPS.Properties.Resources.btnMax;
            }
            else
            {
                if (File.Exists(sPath + "btnResize.jpg"))
                    btnResize.Image = Image.FromFile(sPath + "btnResize.jpg");
                else
                    btnResize.Image = global::EDIPPS.Properties.Resources.btnResize;
            }
            if (File.Exists(sPath + "btnMin.jpg"))
                btnMin.Image = Image.FromFile(sPath + "btnMin.jpg");
            else
                btnMin.Image = global::EDIPPS.Properties.Resources.btnMin;
            if (File.Exists(sPath + "CopyRight.jpg"))
            {
                pnlCopyRight.BackgroundImage = Image.FromFile(sPath + "CopyRight.jpg");
                lablCopyRight.Visible = false;
                pnlCopyRight.Visible = true;
                pnlBottom.Height = 37;
            }
            else
            {
                pnlCopyRight.BackgroundImage = null;
                pnlCopyRight.Visible = false;
                lablCopyRight.Visible = true;
                pnlBottom.Height = 16;
            }
            if (File.Exists(sPath + "folder.bmp"))
                imageList1.Images[0] = Image.FromFile(sPath + "folder.bmp");
            else
                imageList1.Images[0] = global::EDIPPS.Properties.Resources.folder;
            toolStripButton1.Image = imageList1.Images[0];
            if (File.Exists(sPath + "file.bmp"))
                imageList1.Images[1] = Image.FromFile(sPath + "file.bmp");
            else
                imageList1.Images[1] = global::EDIPPS.Properties.Resources.file;
            if (File.Exists(sPath + "folderopen.bmp"))
                imageList1.Images[2] = Image.FromFile(sPath + "folderopen.bmp");
            else
                imageList1.Images[2] = global::EDIPPS.Properties.Resources.folderopen;
            if (File.Exists(sPath + "background.jpg"))
                this.BackgroundImage = Image.FromFile(sPath + "background.jpg");
            else
                this.BackgroundImage = null;
            lablLogin.Text = ClientUtils.SetLanguage(lablLogin.Tag.ToString(), "", Program.g_sFileName) + ClientUtils.fUserName;
            lablLogin.Left = pnlMenu2.Width - lablLogin.Width - 5;
            if (tsShowHost.Checked)
                lablTitle.Text = lablTitle.Text + " - [ " + g_Host + " ] - [ " + ClientUtils.url + " ]";
            if (pnlMenu.Height == 26)
                lablTitle.Text = lablTitle.Text + " - " + ClientUtils.fUserName;
        }

        private bool CheckDllFileVer(string dllname)
        {
            try
            {
                string sqlstr = "";
                object[][] sqlparams = new object[][] { };
                DataTable dt = ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];
                return true;
            }
            catch (Exception ex)
            {
                ClientUtils.ShowMessage("CheckDllFileVer Error", 0, "", dllname + ":" + ex.Message);
                return false;
            }

        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAbout Form2 = new fAbout();
            Form2.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.ImageIndex == 3)
            {
                g_bFunctionHide = true;
                button1.ImageIndex = 4;
            }
            else
            {
                g_bFunctionHide = false;
                button1.ImageIndex = 3;
            }
            toolStrip1.Visible = g_bFunctionHide;
            panelFunction.Visible = !g_bFunctionHide;
            SetValue("Setting", "FunctionHide", g_bFunctionHide.ToString());
        }
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeView treeView = (TreeView)sender;

            if (treeView.SelectedNode != null)
            {
                switch (treeView.SelectedNode.Nodes.Count)
                {
                    case 0:
                        if (g_bManualSelect)
                        {
                            string[] sfunction = treeView.SelectedNode.Name.Split(',');
                            string formText = lablModule.Tag.ToString() + "-" + treeView.SelectedNode.Text;
                            string formTag = lablModule.Tag.ToString() + "\\" + treeView.SelectedNode.Text;
                            string fileName = Application.StartupPath + Path.DirectorySeparatorChar + treeView.Tag + Path.DirectorySeparatorChar + sfunction[0];
                            string dllName = Path.GetFileNameWithoutExtension(fileName);
                            string formName = treeView.SelectedNode.ToolTipText.ToString();
                            if (File.Exists(fileName))
                            {
                                if (Path.GetExtension(fileName).ToUpper() == ".DLL")
                                {
                                    Form[] mdiChild = this.MdiChildren;
                                    foreach (Form childForm in mdiChild)
                                    {
                                        if (childForm.Tag.ToString() == formTag)
                                        {
                                            if (childForm.WindowState == FormWindowState.Minimized)
                                                childForm.WindowState = FormWindowState.Normal;
                                            childForm.BringToFront();
                                            g_bManualSelect = false;
                                            activeForm = childForm;
                                            if (toolStrip1.Visible)
                                                panelFunction.Visible = false;
                                            treeView.SelectedNode = null;
                                            return;
                                        }
                                    }
                                    Assembly assemblyLoad = Assembly.LoadFrom(fileName);
                                    string[] Name = assemblyLoad.FullName.ToString().Split(',');
                                    Type myType = assemblyLoad.GetType(Name[0] + "." + formName);
                                    if (myType != null)
                                    {
                                        try
                                        {
                                            object objectDll = Activator.CreateInstance(myType);
                                            ClientUtils.fCurrentProject = treeView.Tag.ToString();
                                            ClientUtils.fProgramName = treeView.Text;
                                            ClientUtils.fFunctionName = sfunction[1];
                                            ClientUtils.fParameter = treeView.SelectedNode.Tag.ToString();
                                            ClientUtils.fModuleFont = GetFont(formTag);
                                            Form formChild = (Form)objectDll;
                                            formChild.MdiParent = this;
                                            formChild.Text = formText;
                                            formChild.Tag = formTag;
                                            formChild.Show();
                                            runBackground(formChild);
                                            activeForm = formChild;
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message.ToString());
                                        }
                                    }
                                    else
                                    {
                                        ClientUtils.ShowMessage(string.Format("Form Error: {0} . {1}", dllName, formName), 0, "", Program.g_sFileName);
                                    }
                                }
                                else
                                {
                                    ProcessStartInfo startInfo = new ProcessStartInfo();
                                    startInfo.FileName = fileName;
                                    startInfo.Arguments = ClientUtils.UserPara1;
                                    if (File.Exists(startInfo.FileName))
                                        Process.Start(startInfo);
                                }
                            }
                            else
                            {
                                ClientUtils.ShowMessage("FileNotFound", 0, "", Program.g_sFileName);
                            }
                            g_bManualSelect = false;
                            treeView.SelectedNode = null;
                            return;
                        }
                        else
                        {
                            treeView.SelectedNode = null;
                            if (activeForm != null)
                                activeForm.Focus();
                            return;
                        }
                    default:
                        if (g_bManualSelect)
                        {
                            if (e.Node.IsExpanded)
                            {
                                e.Node.Collapse();
                                e.Node.ImageIndex = 0;
                            }
                            else
                            {
                                e.Node.ImageIndex = 2;
                                e.Node.ExpandAll();
                            }
                            g_bManualSelect = false;
                            treeView.SelectedNode = null;
                        }
                        else
                        {
                            if (!toolStrip1.Visible)
                                treeView.SelectedNode = null;
                            if (activeForm != null)
                                activeForm.Focus();
                        }
                        break;
                }
            }
        }

        private void runBackground(Form form)
        {
            BackgroundWorker _bw = new BackgroundWorker();
            _bw.DoWork += new DoWorkEventHandler(runChangeIcon);
            _bw.RunWorkerAsync(form);
        }

        private void runChangeIcon(object sender, DoWorkEventArgs e)
        {

            var form = e.Argument as Form;
            ClientUtils.ClearLanguage();
            var ctrls = form.GetAllControls();
            // foreach(var c in ctrls)
            Parallel.ForEach(ctrls, c =>
            {
                try
                {
                    if (c.GetType() == typeof(Label))
                        setValue((Label)c, ((Label)c).Text.TL());
                    else if (c.GetType() == typeof(GroupBox))
                        setValue((GroupBox)c, ((GroupBox)c).Text.TL());
                    else if (c.GetType() == typeof(RadioButton))
                        setValue((RadioButton)c, ((RadioButton)c).Text.TL());
                    else if (c.GetType() == typeof(Button))
                        setValue((Button)c, ((Button)c).Text.TL());
                    else if (c.GetType() == typeof(CheckBox))
                        setValue((CheckBox)c, ((CheckBox)c).Text.TL());
                    else if (c.GetType() == typeof(DataGridView))
                    {
                        var grid = (DataGridView)c;
                        foreach (DataGridViewColumn col in grid.Columns)
                        {
                            setGridViewValue(col, grid, col.HeaderText.TL());
                        }
                    }
                }
                catch (Exception ex)
                {
                }


            }
            );
        }

        private void setValue(Control c, string value)
        {
            Action action = () =>
            {
                //Franky
                c.Text = value;
                c.Font = new Font(fSmallFont.Name, c.Font.Size);
            };
            if (c.InvokeRequired)
            {
                c.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void setGridViewValue(DataGridViewColumn c, DataGridView grid, string value)
        {
            Action action = () =>
            {
                c.HeaderText = value;
            };
            if (grid.InvokeRequired)
            {
                grid.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            g_bManualSelect = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            treeView1.SelectedNode = null;
            panelFunction.Visible = true;
        }

        private void panelList_Leave(object sender, EventArgs e)
        {
            if (toolStrip1.Visible)
                panelFunction.Visible = false;
        }

        private void combModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModuleChange();
        }

        private void ModuleChange()
        {
            if (combModule.SelectedIndex == -1 || downloadList[0] == null) return;
            int isender = combModule.SelectedIndex;
            string exeName = combModule.SelectedValue.ToString();
            string program = downloadList[isender][0].ToString();
            lablModule.Tag = combModule.Text;
            if (g_iModuleStyle == 1)
                ShowList(program, exeName, treeView1, "");
            else
                ShowFunType(program);
            bool downloadFile = false;
            if (((g_download == "1") && ((bool)downloadList[isender][1])) || (g_download == "2"))
                downloadFile = true;
            if (downloadFile)
                CheckFile(exeName);
            if (g_download == "1")
                downloadList[isender][1] = false;
            ShowMainForm(program);
        }

        private void ShowMainForm(string program)
        {
            string sSQL = "select param_value from sajet.sys_base_param "
             + "where program = 'ALL' and upper(param_name) = '" + ClientUtils.fClientLang + "'";
            DataSet ds = ClientUtils.ExecuteSQL(sSQL);
            string sFunType = "FUN_TYPE";
            string sFunction = "FUNCTION";
            if (ds.Tables[0].Rows.Count > 0)
            {
                string fieldName = ds.Tables[0].Rows[0][0].ToString();
                sFunType = "nvl(FUN_TYPE_" + fieldName + ",FUN_TYPE)";
                sFunction = "nvl(FUN_" + fieldName + ",FUNCTION)";
            }
            sSQL = "select " + sFunType + ", " + sFunction
                + " from sajet.sys_program_fun_name "
                + " where program = '" + program + "' and show_flag = '1' and rownum = 1";
            ds = ClientUtils.ExecuteSQL(sSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                sFunction = ds.Tables[0].Rows[0][1].ToString();
                switch (g_iModuleStyle)
                {
                    case 2:
                        int iIndex = combFunType.Items.IndexOf(ds.Tables[0].Rows[0][0].ToString());
                        if (iIndex == -1) return;
                        combFunType.SelectedIndex = iIndex;
                        break;
                }
                g_bManualSelect = true;
                treeView1.SelectedNode = TreeNodeFindText(treeView1.Nodes, sFunction);
            }
        }
        public static TreeNode TreeNodeFindText(TreeNodeCollection TopNode, string Text)
        {
            foreach (TreeNode node in TopNode)
            {
                if (node.Nodes.Count == 0)
                {
                    if (node.Text == Text) return node;
                }
                else
                {
                    TreeNode nodeChild = TreeNodeFindText(node.Nodes, Text);
                    if (nodeChild != null) return nodeChild;
                }
            }
            return null;
        }

        private void ShowFunType(string program)
        {
            combFunType.Items.Clear();
            DataSet dsTemp = ClientUtils.GetFunction(program, "N");
            foreach (DataRow row in dsTemp.Tables[0].Rows)
                if (combFunType.Items.IndexOf(row[0].ToString()) == -1)
                    combFunType.Items.Add(row[0].ToString());
            if (combFunType.Items.Count > 0)
                combFunType.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void combFunType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ComboBoxItem myItem = (ComboBoxItem)combModule.SelectedItem; //combModule.SelectedValue.ToString()
            ShowList(downloadList[combModule.SelectedIndex][0].ToString(), combModule.SelectedValue.ToString(), treeView1, combFunType.Text);
        }
        private void tsHint_Click(object sender, EventArgs e)
        {
            pnlBottom.Visible = !tsHideBottom.Checked;
            ChangeProgressBar();
            SetValue("Setting", "BottomHide", tsHideBottom.Checked.ToString());
        }

        private void ChangeProgressBar()
        {
            if (pnlBottom.Visible)
            {
                progressBar1.Parent = pnlBottom;
                progressBar1.Width = pnlBottom.Width - pnlCopyRight.Width - 3;
                progressBar1.Top = 3;
                progressBar1.Left = 3;
            }
            else if (pnlMenu.Height == 26)
            {
                progressBar1.Parent = pnlTitle;
                progressBar1.Left = lablTitle.Width + 10;
                progressBar1.Width = btnMin.Left - progressBar1.Left - 10;
                progressBar1.Top = 12;
            }
            else
            {
                progressBar1.Parent = pnlMenu;
                progressBar1.Left = 3;
                progressBar1.Width = pnlMenu.Width - 8;
                progressBar1.Top = 3;
            }
            progressBar1.BringToFront();
        }

        private void formMain_SizeChanged(object sender, EventArgs e)
        {
            btnClose.Left = pnlTitle.Width - btnClose.Width - 8;
            btnResize.Left = btnClose.Left - 25;
            btnMin.Left = btnResize.Left - 20;
            ChangeProgressBar();
            if (this.WindowState == FormWindowState.Normal)
            {
                formHeight = this.Height;
                formWidth = this.Width;
                formLeft = this.Location.X;
                formTop = this.Location.Y;
            }
        }

        private void tsHideLabel_Click(object sender, EventArgs e)
        {
            lablMES.Visible = !tsHideLabel.Checked;
            SetValue("Setting", "LabelHide", tsHideLabel.Checked.ToString());
        }

        private void tsShowHost_Click(object sender, EventArgs e)
        {
            lablTitle.Text = lablTitle.Tag.ToString();
            if (tsShowHost.Checked)
                lablTitle.Text = lablTitle.Text + " - [ " + g_Host + " ]";
            if (pnlMenu.Height == 26)
                lablTitle.Text = lablTitle.Text + " - " + ClientUtils.fUserName;
            SetValue("Setting", "ShowHost", tsShowHost.Checked.ToString());
        }

        private void pnlTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                if (e.Clicks == 1)
                {
                    ReleaseCapture();
                    SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
                    formLeft = this.Left;
                    formTop = this.Top;
                }
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            string sPath = Program.skinPath + Program.skinName + Path.DirectorySeparatorChar;
            if (this.WindowState == FormWindowState.Maximized)
            {
                if (File.Exists(sPath + "btnMax.jpg"))
                    btnResize.Image = Image.FromFile(sPath + "btnMax.jpg");
                else
                    btnResize.Image = global::EDIPPS.Properties.Resources.btnMax;
                this.WindowState = FormWindowState.Normal;
                if (formWidth != 0)
                {
                    this.Width = formWidth;
                    this.Height = formHeight;
                    this.Location = new Point(formLeft, formTop);
                }
            }
            else
            {
                if (File.Exists(sPath + "btnResize.jpg"))
                    btnResize.Image = Image.FromFile(sPath + "btnResize.jpg");
                else
                    btnResize.Image = global::EDIPPS.Properties.Resources.btnResize;
                this.WindowState = FormWindowState.Maximized;
                SetWinFullScreen(this.Handle);
            }
        }
        private bool flagMove = false;
        private int g_iMoveType;
        private void splitter_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Splitter splitter = (Splitter)sender;
            if (this.WindowState == FormWindowState.Maximized)
                splitter.Cursor = Cursors.Default;
            else
            {
                flagMove = true;
                ChangeCursor(splitter, e);
                int iPosition = 20;
                switch (splitter.Dock)
                {
                    case DockStyle.Left:
                        if (e.Y < splitter.Top + iPosition)
                            g_iMoveType = 1;
                        else if (e.Y > splitter.Top + splitter.Height - iPosition)
                            g_iMoveType = 6;
                        else
                            g_iMoveType = 4;
                        break;
                    case DockStyle.Right:
                        if (e.Y < splitter.Top + iPosition)
                            g_iMoveType = 3;
                        else if (e.Y > splitter.Top + splitter.Height - iPosition)
                            g_iMoveType = 8;
                        else
                            g_iMoveType = 5;
                        break;
                    case DockStyle.Top:
                        if (e.X > splitter.Left + splitter.Width - iPosition)
                            g_iMoveType = 3;
                        else if (e.X < splitter.Left + iPosition)
                            g_iMoveType = 1;
                        else
                            g_iMoveType = 2;
                        break;
                    case DockStyle.Bottom:
                        if (e.X > splitter.Left + splitter.Width - iPosition)
                            g_iMoveType = 8;
                        else if (e.X < splitter.Left + iPosition)
                            g_iMoveType = 6;
                        else
                            g_iMoveType = 7;
                        break;
                }
            }
        }
        private void ChangeCursor(Splitter splitter, System.Windows.Forms.MouseEventArgs e)
        {
            int iPosition = 20;
            switch (splitter.Dock)
            {
                case DockStyle.Left:
                    if (e.Y < splitter.Top + iPosition)
                        splitter.Cursor = Cursors.SizeNWSE;
                    else if (e.Y > splitter.Top + splitter.Height - iPosition)
                        splitter.Cursor = Cursors.SizeNESW;
                    else
                        splitter.Cursor = Cursors.SizeWE;
                    break;
                case DockStyle.Right:
                    if (e.Y < splitter.Top + iPosition)
                        splitter.Cursor = Cursors.SizeNESW;
                    else if (e.Y > splitter.Top + splitter.Height - iPosition)
                        splitter.Cursor = Cursors.SizeNWSE;
                    else
                        splitter.Cursor = Cursors.SizeWE;
                    break;
                case DockStyle.Top:
                    if (e.X > splitter.Left + splitter.Width - iPosition)
                        splitter.Cursor = Cursors.SizeNESW;
                    else if (e.X < splitter.Left + iPosition)
                        splitter.Cursor = Cursors.SizeNWSE;
                    else
                        splitter.Cursor = Cursors.SizeNS;
                    break;
                case DockStyle.Bottom:
                    if (e.X > splitter.Left + splitter.Width - iPosition)
                        splitter.Cursor = Cursors.SizeNWSE;
                    else if (e.X < splitter.Left + iPosition)
                        splitter.Cursor = Cursors.SizeNESW;
                    else
                        splitter.Cursor = Cursors.SizeNS;
                    break;
            }
        }
        private void splitter_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Splitter splitter = (Splitter)sender;
            if (this.WindowState == FormWindowState.Maximized)
                splitter.Cursor = Cursors.Default;
            else
            {
                int iWidth = 300, iHeight = 300;
                ChangeCursor(splitter, e);
                if (this.flagMove)
                {
                    switch (g_iMoveType)
                    {
                        case 1:
                            changeFromTop(iHeight);
                            changeFromLeft(iWidth);
                            break;
                        case 2:
                            changeFromTop(iHeight);
                            break;
                        case 3:
                            changeFromTop(iHeight);
                            changeFromRight(iWidth);
                            break;
                        case 4:
                            changeFromLeft(iWidth);
                            break;
                        case 5:
                            changeFromRight(iWidth);
                            break;
                        case 6:
                            changeFromLeft(iWidth);
                            changeFromBottom(iHeight);
                            break;
                        case 7:
                            changeFromBottom(iHeight);
                            break;
                        case 8:
                            changeFromRight(iWidth);
                            changeFromBottom(iHeight);
                            break;
                    }
                }
            }
        }
        private void changeFromTop(int minHeight)
        {
            int YY = MousePosition.Y - this.Top;
            int iHeight;
            if (YY < 0)
                iHeight = this.Height + (0 - YY);
            else
                iHeight = this.Height - YY;
            if (iHeight > minHeight)
            {
                this.Top = MousePosition.Y;
                this.Height = iHeight;
                formTop = this.Top;
                formHeight = this.Height;
            }
        }
        private void changeFromBottom(int minHeight)
        {
            int iHeight = MousePosition.Y - this.Top;
            if (iHeight > minHeight)
            {
                this.Height = iHeight;
                formHeight = this.Height;
            }
        }
        private void changeFromLeft(int minWidth)
        {
            int XX = MousePosition.X - this.Left;
            int iWidth;
            if (XX < 0)
                iWidth = this.Width + (0 - XX);
            else
                iWidth = this.Width - XX;
            if (iWidth > minWidth)
            {
                this.Left = MousePosition.X;
                this.Width = iWidth;
                formLeft = this.Left;
                formWidth = this.Width;
            }
        }
        private void changeFromRight(int minWidth)
        {
            int iWidth = MousePosition.X - this.Left;
            if (iWidth > minWidth)
            {
                this.Width = iWidth;
                formWidth = this.Width;
            }
        }
        private void splitter_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            flagMove = false;
        }
        private void fMain_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
                tsFunction.Text = "";
            else
            {
                tsFunction.Text = this.ActiveMdiChild.Tag.ToString();
                ClientUtils.fModuleFont = GetFont(tsFunction.Text);
            }
        }
        private Font GetFont(string formTag)
        {
            SajetInifile iniFile = new SajetInifile();
            int iFontSize = 1;
            int.TryParse(iniFile.ReadIniFile(Application.StartupPath + Path.DirectorySeparatorChar + "Font.ini", ClientUtils.cultureName, formTag, "1"), out iFontSize);
            iniFile.Dispose();
            switch (iFontSize)
            {
                case 0: return fSmallFont;
                case 2: return fLargeFont;
                default: return fFont;
            }
        }
        private void SetFont(string formTag, string sFontSize)
        {
            SajetInifile iniFile = new SajetInifile();
            iniFile.WriteIniFile(Application.StartupPath + Path.DirectorySeparatorChar + "Font.ini", ClientUtils.cultureName, formTag, sFontSize);
            iniFile.Dispose();
        }
        private void btnFont_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                Button btn = (Button)sender;
                switch (btn.Name)
                {
                    case "btnLargeFont":
                        ClientUtils.fModuleFont = fLargeFont;
                        break;
                    case "btnFont":
                        ClientUtils.fModuleFont = fFont;
                        break;
                    default:
                        ClientUtils.fModuleFont = fSmallFont;
                        break;
                }
                SetFont(this.ActiveMdiChild.Tag.ToString(), btn.Tag.ToString());
                ClientUtils.SetFont(this.ActiveMdiChild);
            }
        }
    }

}