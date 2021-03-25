using MESModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInfo
{
    public class ConnStatics
    {
        public static string DownloadFolder {
            get {
                return ConfigurationManager.AppSettings["DownloadFolder"].ToString();
            }
        } 
        private static Dictionary<string, List<ClientObject>> _clientList { get; set; }
        private static List<FileObject> _fileList { get; set; }

        public static Dictionary<string, List<ClientObject>> ClientList
        {
            get
            {
                if (_clientList == null) {
                    lock (lockObject)
                    {
                        _clientList = new Dictionary<string, List<ClientObject>>();
                    }
                }
                return _clientList;
            }
        }
        public static List<FileObject> FileList
        {
            get
            {
                if (_fileList == null)
                {
                    lock (lockObject2)
                    {
                        _fileList = new List<FileObject>();
                    }
                }
                return _fileList;
            }
        }

        private static object lockObject = new object();
        private static object lockObject2 = new object();


        public static void SaveLogin(string key, ClientObject model) {
            lock (lockObject)
            {
                var isContains = _clientList.ContainsKey(key);
                if (isContains)
                {
                    var models = _clientList[key];
                    var m = models.FirstOrDefault(x => x.computerName == model.computerName);
                    if (m != null)
                        m = model;
                    else
                        models.Add(model);
                }
                else
                {
                    _clientList.Add(key, new List<ClientObject> { model });
                }
            }
        }

        public static void RemoveClientList(string key) {
            lock (lockObject)
            {
                var isContains = _clientList.ContainsKey(key);
                if (isContains)
                {
                    _clientList.Remove(key);
                }
                else
                {
                }
            }
        }


        public static void ResetFiles()
        {
            lock (lockObject2)
            {
                //FileObject
                _fileList = new List<FileObject>();

                var files =  Directory.GetFiles(DownloadFolder, "*", SearchOption.AllDirectories);

                _fileList = files.Select(x =>
                {
                    var info = new FileInfo(x);
                    var version = FileVersionInfo.GetVersionInfo(x);
                    return new FileObject(info.Name, version.FileVersion == null ? "" : version.FileVersion.ToString()
                        , info.LastWriteTime);
                }).ToList();
            }
        }
    }
}
