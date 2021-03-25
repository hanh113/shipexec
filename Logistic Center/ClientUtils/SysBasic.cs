using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientUtilsDll
{
    public class SysBasic
    {
        private static object lockObj;
        static SysBasic()
        {
            lockObj = new object();
            if (!Directory.Exists(string.Concat(LocalAppPath, "\\Log")))
            {
                Directory.CreateDirectory(string.Concat(LocalAppPath, "\\Log"));
            }
        }

        public static string LocalAppPath
        {
            get
            {
                return Directory.GetCurrentDirectory();
            }
        }
        public static void WriteLog(string filePath, string fileName, string text)
        {
            Monitor.Enter(lockObj);
            try
            {
                try
                {
                    string[] str = new string[] { filePath, "\\", null, null, null };
                    DateTime now = DateTime.Now;
                    str[2] = now.ToString("yyyyMMdd");
                    str[3] = "_";
                    str[4] = fileName;
                    string str1 = string.Concat(str);
                    now = DateTime.Now;
                    string str2 = string.Concat(now.ToString("yyyy/MM/dd HH:mm:ss"), "\t", text);
                    checkFile(str1);
                    using (StreamWriter streamWriter = File.AppendText(str1))
                    {
                        streamWriter.WriteLine(str2);
                        streamWriter.Flush();
                        streamWriter.Close();
                        streamWriter.Dispose();
                    }
                }
                catch
                {
                }
            }
            finally
            {
                Monitor.Exit(lockObj);
            }
        }

        private static void checkFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                StreamWriter streamWriter = File.CreateText(fileName);
                streamWriter.Close();
                streamWriter.Dispose();
            }
        }

    }
}
