using MESModel;
using OperationWCF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDIPPS.Service
{
    public class ClientHosting : HttpHosting, IClientHost
    {
        public ClientHosting()
        {
            string xx = "";
        }
        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public void ProcessMsg(string msg)
        {
            if (msg == "ReDownload")
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = Application.StartupPath + Path.DirectorySeparatorChar + "InstallConsole.exe";
                if (File.Exists(startInfo.FileName))
                {
                    Process.Start(startInfo);
                    Application.Exit();
                }
            }
        }

        public List<FileObject> GetFileLists()
        {
            List<FileObject> fileList = new List<FileObject>();

            string str;
            string str1 = Application.StartupPath;
            fileList.Clear();
            if (Directory.Exists(str1))
            {
                var files = Directory.GetFiles(str1, "*", SearchOption.AllDirectories);
                string rootPath = (str1 + "/").Replace("\\", "/");
                fileList = files.Select(x =>
                {

                    var info = new FileInfo(x);
                    var version = FileVersionInfo.GetVersionInfo(x);
                    //string name =
                    //       Regex.Replace(info.FullName.Replace("\\", "/"), rootPath, "", RegexOptions.IgnoreCase);
                    string name = Path.GetFileName(info.FullName.Replace("\\", "/"));
                    name = name.Replace("/", "\\");
                    return new FileObject(name, version.FileVersion == null ? "" : version.FileVersion.ToString()
                        , info.LastWriteTime);
                }).ToList();
            }
            return fileList;
        }
    }
}
