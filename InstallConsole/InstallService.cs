using MESModel;
using RemoteService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstallConsole
{
    public class InstallService
    {
        private string serverUrl { get; set; }
        private string fixPath = "D:\\MES_CLIENT";
        private IRemoteServiceObject _ws { get; set; }

        public InstallService()
        {
            DataSet ds = new DataSet();
            string APServer = "";
            ds.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + "Host" + Path.DirectorySeparatorChar + "Default.xml");
            serverUrl = string.Format("http://{0}:{1}/WCF_RemoteObject",
                Convert.ToString(ds.Tables["APServer"].Rows[0]["IP"]), Convert.ToInt16(ds.Tables["APServer"].Rows[0]["Port"]));
            _ws = OperationWCF.HttpChannel.Get<IRemoteServiceObject>(serverUrl);
        }
        public InstallService(string Url, string Path)
        {
            fixPath = Path;
            serverUrl = string.Format("http://{0}/WCF_RemoteObject", Url);
            _ws = OperationWCF.HttpChannel.Get<IRemoteServiceObject>(serverUrl);
        }
        public void Install(bool isdelete)
        {
            killEDIPPS();
            if (isdelete && Application.StartupPath.ToUpper() != fixPath)
            {
                deleteMES_Client();
            }
            CheckClient("LoadClient");
            loadMES_Client();
        }

        private void deleteMES_Client()
        {
            try
            {
                Directory.Delete(fixPath, true);
            }
            catch { }
        }

        private void CheckClient(string program)
        {
            List<FileObject> filelist = new List<FileObject>();

            string path = fixPath + Path.DirectorySeparatorChar;
            filelist = getFileLists(program);
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
                                DownloadFile(program, fileInfo.fileName, fileName);
                            }
                        }
                        catch
                        {
                            DateTime fileAge = File.GetLastWriteTime(fileName);
                            if (fileAge < fileInfo.fileAge)
                            {
                                if (Path.GetDirectoryName(fileName) == path + "Skin\\" + "Default")
                                {
                                    DownloadFile(program, fileInfo.fileName, Path.Combine(sTempPath, Path.GetFileName(fileName)));
                                }
                                else
                                    DownloadFile(program, fileInfo.fileName, fileName);
                            }
                        }
                    }
                    else
                    {
                        DownloadFile(program, fileInfo.fileName, fileName);
                    }

                }
               );
            }
        }

        private void killEDIPPS()
        {
            try
            {
                Process[] proc = Process.GetProcessesByName("EDIPPS");
                proc[0].Kill();
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {

            }
        }

        private void loadMES_Client()
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = fixPath + Path.DirectorySeparatorChar + "EDIPPS.exe";
                if (File.Exists(startInfo.FileName))
                {
                    Process.Start(startInfo);
                }
            }
            catch (Exception ex)
            {

            }
        }


        private List<FileObject> getFileLists(string program)
        {
            return _ws.GetFileLists(program);
        }

        private void DownloadFile(string program, string fromFile, string destFile)
        {
            string sDir = Path.GetDirectoryName(destFile);
            if (!Directory.Exists(sDir))
                Directory.CreateDirectory(sDir);
            if (File.Exists(destFile))
                File.Delete(destFile);
            byte[] data = _ws.DownloadFileByte(program, fromFile);
            FileStream localFS = new FileStream(destFile, FileMode.Create, FileAccess.Write);
            localFS.Write(data, 0, data.Length);
            localFS.Close();
        }

    }

}
