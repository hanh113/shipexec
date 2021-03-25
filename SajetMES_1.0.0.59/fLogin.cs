using System;
using System.Xml;
using System.IO;
using System.Data;
using System.Text;
using System.Net;
using System.Drawing;
using Microsoft.Win32;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using Microsoft.VisualBasic.Devices;
using System.Threading;
using System.Runtime.Remoting.Channels;
using System.Data.OracleClient;
using System.Collections.Generic;
using MESModel;
using System.Threading.Tasks;
using ClientUtilsDll;

namespace EDIPPS
{
    public partial class fLogin : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        bool g_bCheckClient = true;
        int g_iTimeout = 120, g_iPortTimeout = 5;

        public fLogin()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(fMain.OnErrorOccur);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(fMain.CurrentDomain_UnhandledException);
            InitializeComponent();
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            lablVersion.Text = string.Format(ClientUtils.SetLanguage("Version: {0}", "", Program.g_sFileName), fMain.AssemblyVersion);
            lablCopyright.Text = ClientUtils.SetLanguage(fMain.AssemblyCopyright, "", Program.g_sFileName);
            if (File.Exists(Program.skinPath + Program.skinName + @"\Login.jpg"))
                this.BackgroundImage = Image.FromFile(Program.skinPath + Program.skinName + @"\Login.jpg");
            else
                this.BackColor = SystemColors.Control;
            if (File.Exists(Program.skinPath + Program.skinName + Path.DirectorySeparatorChar + "btnClose.jpg"))
                btnClose.Image = Image.FromFile(Program.skinPath + Program.skinName + Path.DirectorySeparatorChar + "btnClose.jpg");
            else
                btnClose.Image = global::EDIPPS.Properties.Resources.btnClose;
            cmbLang.Visible = Program.visible;
            cmbLang.Items.Clear();
            combValue.Items.Clear();
            DataSet dsCulture = new DataSet();
            dsCulture.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + "PPS.xml");
            g_iTimeout = int.Parse(fMain.GetValue("Setting", "Timeout", "120"));
            g_iPortTimeout = int.Parse(fMain.GetValue("Setting", "PortTimeout", "5"));
            try
            {
                fMain.g_download = dsCulture.Tables["Download"].Rows[0]["Type"].ToString();
                g_bCheckClient = (dsCulture.Tables["Download"].Rows[0]["CheckClient"].ToString() == "1");
            }
            catch { }
            for (int i = 0; i < dsCulture.Tables["Culture"].Rows.Count; i++)
            {
                cmbLang.Items.Add(Convert.ToString(dsCulture.Tables["Culture"].Rows[i]["Name"]));
                combValue.Items.Add(Convert.ToString(dsCulture.Tables["Culture"].Rows[i]["Value"]).ToUpper());
            }
            cmbLang.SelectedIndex = combValue.Items.IndexOf(ClientUtils.fClientLang.ToUpper());
            dsCulture.Dispose();
            string[] MyFiles = System.IO.Directory.GetFiles(Application.StartupPath + Path.DirectorySeparatorChar + "Host");
            foreach (string MyFile in MyFiles)
            {
                DataSet ds = new DataSet();
                ds.ReadXml(MyFile);
                for (int i = 0; i < ds.Tables["APServer"].Rows.Count; i++)
                {
                    string url = Convert.ToString(ds.Tables["APServer"].Rows[i]["ref"]) + "://" + Convert.ToString(ds.Tables["APServer"].Rows[i]["IP"]) + ":" + Convert.ToString(ds.Tables["APServer"].Rows[i]["Port"]);
                    if (TestConnect(Convert.ToString(ds.Tables["APServer"].Rows[i]["IP"])))
                    {
                        if (IsServerConnectable(Convert.ToString(ds.Tables["APServer"].Rows[i]["IP"]), Convert.ToInt16(ds.Tables["APServer"].Rows[i]["Port"])))
                        {
                            cmbServer.Items.Add(Path.GetFileNameWithoutExtension(MyFile));
                            break;
                        }
                    }
                }
                ds.Dispose();
            }
            if (cmbServer.Items.Count != 0)
            {
                string sHost = fMain.GetValue("Setting", "Host", null);
                if (!string.IsNullOrEmpty(sHost))
                    cmbServer.SelectedIndex = cmbServer.Items.IndexOf(sHost);
                if (cmbServer.SelectedIndex == -1)
                    cmbServer.SelectedIndex = 0;
                //cmbServer_SelectedIndexChanged(null, null);
            }
            else
            {
                ClientUtils.ShowMessage("APServerFail", 0, "", Program.g_sFileName);
                Application.Exit();
            }
            textBoxEmp.Focus();
            if (!string.IsNullOrEmpty(Program.g_sUserID))
            {
                try
                {
                    object[][] Params = new object[1][];
                    Params[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "EMP_ID", Program.g_sUserID };
                    DataSet ds = ClientUtils.ExecuteSQL("SELECT EMP_NO, Trim(SAJET.PASSWORD.decrypt(PASSWD)) FROM SAJET.SYS_EMP "
                        + "WHERE EMP_ID = :EMP_ID AND ROWNUM = 1", Params);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        textBoxEmp.Text = ds.Tables[0].Rows[0][0].ToString();
                        textBoxPwd.Text = ds.Tables[0].Rows[0][1].ToString();
                        buttonLogin_Click(sender, e);
                    }
                    ds.Dispose();
                }
                catch { }
            }
            //string sMsg = "";
            //if (!License.CheckLicense(out sMsg))
            //{
            //    ClientUtils.ShowMessage(sMsg, 0, "", Program.g_sFileName);
            //    Application.Exit();
            //}
            //else if (sMsg != "Chroma License")
            //{
            //    ClientUtils.ShowMessage("請勿非法盜用!", 0, "", Program.g_sFileName);
            //    Application.Exit();
            //}
        }
        private bool IsServerConnectable(string host, int port)
        {
            return true;
            TcpClient tcp = new TcpClient();
            DateTime t = DateTime.Now;
            try
            {
                IAsyncResult ar = tcp.BeginConnect(host, port, null, null);
                while (!ar.IsCompleted)
                {
                    if (DateTime.Now > t.AddSeconds(g_iPortTimeout))
                    {
                        throw new Exception("Connection timeout!");
                    }
                    System.Threading.Thread.Sleep(100);
                }
                tcp.EndConnect(ar);
                tcp.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool TestConnect(string sIP)
        {
            try
            {
                Ping pingSender = new Ping();
                int i = 0;
                while (i < 5)
                {
                    PingReply reply = pingSender.Send(sIP, g_iTimeout);
                    if (reply.Status == IPStatus.Success)
                    {
                        return true;
                    }
                    Thread.Sleep(1000);
                    i++;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
        private void CheckFile(string program)
        {
            List<FileObject> filelist = new List<FileObject>();
            string version;
            string fileName;
            string path = Application.StartupPath + Path.DirectorySeparatorChar + program + Path.DirectorySeparatorChar;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            filelist = ClientUtils.GetFileLists(program);
            if (filelist != null)
            {
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
                                fMain.DownloadFile(program, fileInfo.fileName, fileName);
                        }
                        catch
                        {
                            DateTime fileAge = File.GetLastWriteTime(fileName);
                            if (fileAge < fileInfo.fileAge)
                                fMain.DownloadFile(program, fileInfo.fileName, fileName);
                        }
                    }
                    else
                        fMain.DownloadFile(program, fileInfo.fileName, fileName);
                }
            }
        }
        private bool CheckLoginByLDAP(string _user, string _password)
        {
            bool sResult = false;
            com.luxshare_ict.dcs.LdapAd sLdapAd = new com.luxshare_ict.dcs.LdapAd();
            com.luxshare_ict.dcs.CheckUserModel sCheckUserModel = new com.luxshare_ict.dcs.CheckUserModel();
            sCheckUserModel = sLdapAd.CheckUserLogin(_user, _password);
            if (sCheckUserModel.IsSuccess)
                sResult = true;
            else
                return sResult;
            return sResult;
        }
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            Computer ComputerName = new Computer();
            try
            {
                // if (CheckLoginByLDAP(textBoxEmp.Text, textBoxPwd.Text))
                //     str = (ClientUtils.CheckUser(textBoxEmp.Text, textBoxPwd.Text, ComputerName.Name, fMain.g_APServer, out ClientUtils.UserPara1, out ClientUtils.fUserName));
                // else
                str = (ClientUtils.CheckUser(textBoxEmp.Text, textBoxPwd.Text, ComputerName.Name, fMain.g_APServer, out ClientUtils.UserPara1, out ClientUtils.fUserName));
                //str = "帐号或密码错吴";
            }
            catch
            {
                str = (ClientUtils.CheckUser(textBoxEmp.Text, textBoxPwd.Text, ComputerName.Name, fMain.g_APServer, out ClientUtils.UserPara1, out ClientUtils.fUserName));
            }
            if (str != "OK")
            {
                bool fail = true;
                string sSQL = "Select param_value From SAJET.SYS_BASE_PARAM "
                    + "Where PARAM_NAME = 'SAJET_SYS_DBA' ";
                DataSet ds = ClientUtils.ExecuteSQL(sSQL);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    string[] login = ds.Tables[0].Rows[0][0].ToString().Split('/');
                    fail = (login[0] != textBoxEmp.Text || login[1] != textBoxPwd.Text);
                }
                if (fail)
                {
                    ClientUtils.ShowMessage(str, 0, "", Program.g_sFileName);
                    textBoxPwd.Text = "";
                    textBoxEmp.Select();
                    return;
                }
                else
                {
                    if (fMain.g_download == "0")
                    {
                        DataSet dsTemp = ClientUtils.GetProgram("N");
                        CheckFile("Data Center");
                        fMain.g_download = "4";
                    }
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(Application.StartupPath + Path.DirectorySeparatorChar + "PPS.xml");
                    XmlNode xmlNode = xmlDoc.SelectSingleNode("//Culture[@value='" + ClientUtils.fClientLang.ToUpper() + "']");
                    ClientUtils.cultureName = xmlNode.Attributes["Name"].Value;
                    ClientUtils.fLoginUser = textBoxEmp.Text;
                    Program.visible = false;
                    this.Close();
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                if (fMain.g_download == "0")
                {
                    DataSet dsTemp = ClientUtils.GetProgram("N");
                    int i = 0;
                    progressBar1.Maximum = dsTemp.Tables[0].Rows.Count;
                    progressBar1.Value = 0;
                    progressBar1.Visible = true;
                    foreach (DataRow row in dsTemp.Tables[0].Rows)
                    {
                        CheckFile(Convert.ToString(row[1].ToString()));
                        progressBar1.Increment(1);
                        i++;
                    }
                    progressBar1.Visible = false;
                    fMain.g_download = "4";
                }
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Application.StartupPath + Path.DirectorySeparatorChar + "PPS.xml");
                XmlNode xmlNode = xmlDoc.SelectSingleNode("//Culture[@value='" + ClientUtils.fClientLang.ToUpper() + "']");
                ClientUtils.cultureName = xmlNode.Attributes["Name"].Value;
                ClientUtils.fLoginUser = textBoxEmp.Text;
                Program.visible = false;
                this.Close();
                this.DialogResult = DialogResult.Yes;
            }
            string sTempPath = Application.StartupPath + Path.DirectorySeparatorChar + "SkinTemp" + Path.DirectorySeparatorChar;
            if (File.Exists(sTempPath + "btnClose.jpg"))
                btnClose.BackgroundImage.Dispose();
            if (File.Exists(sTempPath + "Login.jpg"))
                if (this.BackgroundImage != null)
                    this.BackgroundImage.Dispose();
        }

        private void cmbLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClientUtils.fClientLang = combValue.Items[cmbLang.SelectedIndex].ToString();
            ClientUtils.cultureName = cmbLang.Text;
            fMain.SetValue("Setting", "Culture", ClientUtils.fClientLang);
            lablEmp.Text = ClientUtils.SetLanguage("Account", "", Program.g_sFileName);
            lablEmp.Refresh();
            lablPwd.Text = ClientUtils.SetLanguage("Password", "", Program.g_sFileName);
            lablPwd.Refresh();
            lablHost.Text = ClientUtils.SetLanguage("Host", "", Program.g_sFileName);
            lablHost.Refresh();
            btnLogin.Text = ClientUtils.SetLanguage("Login", "", Program.g_sFileName);
            btnLogin.Refresh();
            btnCancel.Text = ClientUtils.SetLanguage("Close", "", Program.g_sFileName);
            lablCopyright.Text = ClientUtils.SetLanguage(fMain.AssemblyCopyright, "", Program.g_sFileName);
            lablVersion.Text = string.Format(ClientUtils.SetLanguage("Version: {0}", "", Program.g_sFileName), fMain.AssemblyVersion);
        }

        private void textBoxEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
                textBoxPwd.Select();
        }

        private void textBoxPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
                buttonLogin_Click(sender, e);
        }

        private void LoginForm_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cmbServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string APServer = "";
            ds.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + "Host" + Path.DirectorySeparatorChar + cmbServer.Text + ".xml");
            int iCount = 0;
            string txt = "";
            RemoteObject remoteObject;
            for (int i = 0; i < ds.Tables["APServer"].Rows.Count; i++)
            {
                string url = Convert.ToString(ds.Tables["APServer"].Rows[i]["ref"]) + "://" + Convert.ToString(ds.Tables["APServer"].Rows[i]["IP"]) + ":" + Convert.ToString(ds.Tables["APServer"].Rows[i]["Port"]);
                if (TestConnect(ds.Tables["APServer"].Rows[i]["IP"].ToString()))
                {
                    if (IsServerConnectable(Convert.ToString(ds.Tables["APServer"].Rows[i]["IP"]), Convert.ToInt16(ds.Tables["APServer"].Rows[i]["Port"])))
                    {
                        //RemoteObject
                        try
                        {
                            ClientUtils.ServerUrl = string.Format("http://{0}:{1}/WCF_RemoteObject",
                                Convert.ToString(ds.Tables["APServer"].Rows[i]["IP"]), Convert.ToInt16(ds.Tables["APServer"].Rows[i]["Port"]));

                            int iTemp = (ClientUtils.GetCount());
                            if (i == 0 || iCount > iTemp || APServer == "")
                            {
                                iCount = iTemp;
                                APServer = url;
                            }
                            remoteObject = null;
                        }
                        catch (Exception exp)
                        {
                            txt = "Server: " + url + Environment.NewLine + Environment.NewLine + "Message: " + exp.Message;
                        }
                    }
                }
            }
            if (APServer == "")
            {
                btnLogin.Enabled = false;
                string str = "AP Server Fail! " + Environment.NewLine + Environment.NewLine + txt;
                MessageBox.Show(str);
                Application.Exit();
            }
            else
            {
                fMain.SetValue("Setting", "Host", cmbServer.Text);
                ClientUtils.url = APServer;
                fMain.g_APServer = APServer;
                fMain.g_Host = cmbServer.Text;
                if (string.IsNullOrEmpty(Program.g_sUserID))
                {
                    string sTempPath = Application.StartupPath + Path.DirectorySeparatorChar + "SkinTemp";
                    if (Directory.Exists(sTempPath))
                        Directory.Delete(sTempPath, true);
                    Directory.CreateDirectory(sTempPath);
                    if (fMain.g_download == "0")

                        CheckClient("LoadClient", APServer, false);
                    if (g_bCheckClient)
                        CheckClient("EDIPPS", APServer, true);
                }
            }
            ds.Dispose();
        }
        private void CheckClient(string program, string APServer, bool restart)
        {
            List<FileObject> filelist = new List<FileObject>();

            string path = Application.StartupPath + Path.DirectorySeparatorChar;
            filelist = ClientUtils.GetFileLists(program);
            string sTempPath = Application.StartupPath + Path.DirectorySeparatorChar + "SkinTemp";
            if (filelist != null)
            {
                Parallel.ForEach(filelist, item =>
                // foreach (var item in filelist)
                {
                    string version;
                    string fileName;
                    FileObject fileInfo = item;
                    fileName = path + fileInfo.fileName;
                    if (File.Exists(fileName))
                    {
                        if ((new FileInfo(fileName).Length) == 0)
                            File.Delete(fileName);
                        try
                        {
                            version = FileVersionInfo.GetVersionInfo(@fileName).FileVersion.ToString();
                            if (version != fileInfo.version)
                            {
                                if (restart)
                                {
                                    fMain.g_bRestart = true;
                                    DownloadClient(APServer);
                                    return;
                                }
                                else
                                    fMain.DownloadFile(program, fileInfo.fileName, fileName);
                            }
                        }
                        catch
                        {
                            DateTime fileAge = File.GetLastWriteTime(fileName);
                            if (fileAge < fileInfo.fileAge)
                            {
                                if (restart)
                                {
                                    fMain.g_bRestart = true;
                                    DownloadClient(APServer);
                                    return;
                                }
                                else
                                {
                                    if (Path.GetDirectoryName(fileName) == path + "Skin\\" + Program.skinName)
                                    {
                                        fMain.g_bDownLoadImage = true;
                                        fMain.DownloadFile(program, fileInfo.fileName, Path.Combine(sTempPath, Path.GetFileName(fileName)));
                                    }
                                    else
                                        fMain.DownloadFile(program, fileInfo.fileName, fileName);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (restart)
                        {
                            fMain.g_bRestart = true;
                            DownloadClient(APServer);
                            return;
                        }
                        else
                            fMain.DownloadFile(program, fileInfo.fileName, fileName);
                    }
                }
                );
            }
        }
        private void DownloadClient(string APServer)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Application.StartupPath + Path.DirectorySeparatorChar + "LoadClient.exe";
            startInfo.Arguments = APServer + " " + Program.g_sFileName;
            if (File.Exists(startInfo.FileName))
            {
                Process.Start(startInfo);
                Application.Exit();
            }
        }
    }
}