using MESModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreateSN
{
    public partial class ManagerForm : Form
    {
        //2020/07/23 create sn managerment (MPart, PPart, Apple care) by wenxing
        const string M_PART = "CreatePPSData.dll";
        const string P_PART = "CreatePPSDataPpart.dll";
        const string APPLE_CARE = "CreatePPSDataAC.dll";
        const string MSG_OLD_DATE = "This version is old date, please update!";
        public ManagerForm()
        {
            ClientUtils.ServerUrl = string.Format("http://{0}:8090/WCF_RemoteObject", ConfigurationManager.AppSettings["AP"]);
            InitializeComponent();
            WindowState = FormWindowState.Maximized;

            //check if version of dll different then automatic download from WCF
            //AutoDownloadNewVersion();
        }

        public void AutoDownloadNewVersion()
        {
            List<FileObject> list = new List<FileObject>();
            string path = Application.StartupPath + Path.DirectorySeparatorChar.ToString();
            list = ClientUtils.GetFileLists("CreateSN");
            Parallel.ForEach<FileObject>(list, (item) =>
            {
                string text = path + item.fileName;
                bool flagDownLoad = CheckVersion(item.fileName);
                if (!flagDownLoad)
                    DownloadFile("CreateSN", item.fileName, text);
            });
        }

        internal static void DownloadFile(string program, string fromFile, string destFile)
        {
            string directoryName = Path.GetDirectoryName(destFile);
            bool flag = !Directory.Exists(directoryName);
            if (flag)
            {
                Directory.CreateDirectory(directoryName);
            }
            bool flag2 = File.Exists(destFile);
            if (flag2)
            {
                File.Delete(destFile);
            }
            byte[] array = ClientUtils.DownloadFileByte(program, fromFile);
            FileStream fileStream = new FileStream(destFile, FileMode.Create, FileAccess.Write);
            fileStream.Write(array, 0, array.Length);
            fileStream.Close();
        }

        private void btnMPart_Click(object sender, EventArgs e)
        {
            var assembly = System.Reflection.Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "/" + M_PART);
            Type type = assembly.GetType("CreatePPSData.MainForm");
            bool flag10 = type != null;
            if (flag10)
            {
                try
                {
                    object obj = Activator.CreateInstance(type);
                    ShowForm(obj as Form, btnMPart, M_PART);
                }
                catch { }
            }
        }

        private void btnPPart_Click(object sender, EventArgs e)
        {
            var assembly = System.Reflection.Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "/" + P_PART);
            Type type = assembly.GetType("CreatePPSDataPpart.MainForm");
            bool flag10 = type != null;
            if (flag10)
            {
                try
                {
                    object obj = Activator.CreateInstance(type);
                    ShowForm(obj as Form, btnPPart, P_PART);
                }
                catch { }
            }
        }

        private void btnAppleCare_Click(object sender, EventArgs e)
        {
            var assembly = System.Reflection.Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "/" + APPLE_CARE);
            Type type = assembly.GetType("CreatePPSDataAC.MainForm3");
            bool flag10 = type != null;
            if (flag10)
            {
                try
                {
                    object obj = Activator.CreateInstance(type);
                    ShowForm(obj as Form, btnAppleCare, APPLE_CARE);
                }
                catch { }
            }
        }
        public void ShowForm(Form f, ToolStripButton tool, string fileName)
        {
            if (!CheckVersion(fileName))
            {
                MessageBox.Show(MSG_OLD_DATE);
                return;
            }

            btnMPart.BackColor = SystemColors.Control;
            btnPPart.BackColor = SystemColors.Control;
            btnAppleCare.BackColor = SystemColors.Control;
            tool.BackColor = Color.Gold;

            Form[] mdiChildren = base.MdiChildren;
            foreach (Form form in mdiChildren)
            {
                var check = form.Tag.ToString() == f.GetType().ToString();
                if (check)
                {
                    form.BringToFront();
                    return;
                }
            }
            f.MdiParent = this;
            f.Tag = f.GetType().ToString();
            f.Show();
        }
        public bool CheckVersion(string fileName = "")
        {
            return true;
            bool res;
            try
            {
                string version;
                string qryGetDBVersion = "select PARA_VALUE from PPSUSER.T_BASICPARAMETER_INFO where  ENABLED = 'Y' ";
                if (String.IsNullOrWhiteSpace(fileName))
                {
                    version = this.ProductVersion;
                    qryGetDBVersion += " and PARA_TYPE = 'CreateSNVersion'";
                }
                else
                {
                    string pathFile = AppDomain.CurrentDomain.BaseDirectory;
                    version = FileVersionInfo.GetVersionInfo(pathFile + @"\" + fileName).FileVersion;
                    switch (fileName)
                    {
                        case M_PART:
                            qryGetDBVersion += " and PARA_TYPE = 'CreateMPartVersion'";
                            break;
                        case P_PART:
                            qryGetDBVersion += " and PARA_TYPE = 'CreatePPartVersion'";
                            break;
                        case APPLE_CARE:
                            qryGetDBVersion += " and PARA_TYPE = 'CreateAppleCareVersion'";
                            break;
                        default:
                            return false;
                    }
                }

                DataTable db = ClientUtils.ExecuteSQL(qryGetDBVersion).Tables[0];
                var dbAppVersion = db.Rows[0]["PARA_VALUE"].ToString();
                res = version == dbAppVersion;
            }
            catch
            {
                res = false;
            }
            return res;
        }
    }
}