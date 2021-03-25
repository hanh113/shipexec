using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;

namespace HShippingLabel
{
    class PrintLabel
    {
        [DllImport("kernel32.dll")]
        public static extern int WinExec(string exeName, int operType);
        public PrintLabel()
        {

        }
        public bool Print_Bartender_DataSource(string sFileName, string sDirectory, string sInputText, int iPrintQty, out string sMessage)
        {
            try
            {
                sMessage = "OK";
                string startupPath = System.Windows.Forms.Application.StartupPath;
                string sSampleFile = sDirectory + @"\" + sFileName + ".btw";
                string str7 = sDirectory + sFileName + ".lst";
                string str8 = sDirectory + sFileName + ".dat";
                string sFile = startupPath + @"\PrintGo.bat";
                string str11 = startupPath + @"\PrintLabel.bat";
                string sData = this.LoadBatFile(str11, ref sMessage);
                string str9 = string.Empty;
                if (!File.Exists(sSampleFile))
                {
                    sMessage = "Sample File Not exists-" + sFileName;
                    return false;
                }
                if (!string.IsNullOrEmpty(sMessage))
                {
                    return false;
                }
                if (File.Exists(str7))
                {
                    File.Delete(str7);
                }
                if (!File.Exists(str8))
                {
                    sMessage = "Label File Not Found (.dat)" + Environment.NewLine + Environment.NewLine + str8 + Environment.NewLine + Environment.NewLine;
                    return false;
                }
                str9 = Readtxt(str8);
                this.WriteToTxt(str7, str9 + "\r\n" + sInputText, false);
                string path = sSampleFile;
                sData = sData.Replace("@PATH1", '"' + path + '"').Replace("@PATH2", '"' + str7 + '"').Replace("@QTY", iPrintQty.ToString());
                this.WriteToPrintGo(sFile, sData);
                int num4 = WinExec(sFile, 0);
                sMessage = "OK";
                return true;
            }
            catch (Exception ex)
            {
                sMessage = ex.Message;
                return false;
            }
        }

        public bool Print_Bartender_DataSource(string sFileName, string sDirectory, List<string> sInputText, int iPrintQty, out string sMessage)
        {
            try
            {
                sMessage = "OK";
                string startupPath = System.Windows.Forms.Application.StartupPath;
                string sSampleFile = sDirectory + @"\" + sFileName + ".btw";
                string str7 = sDirectory + sFileName + ".lst";
                string str8 = sDirectory + sFileName + ".dat";
                string sFile = startupPath + @"\PrintGo.bat";
                string str11 = startupPath + @"\PrintLabel.bat";
                string sData = this.LoadBatFile(str11, ref sMessage);
                string str9 = string.Empty;
                if (!File.Exists(sSampleFile))
                {
                    sMessage = "Sample File Not exists-" + sFileName;
                    return false;
                }
                if (!string.IsNullOrEmpty(sMessage))
                {
                    return false;
                }
                if (File.Exists(str7))
                {
                    File.Delete(str7);
                }
                if (!File.Exists(str8))
                {
                    sMessage = "Label File Not Found (.dat)" + Environment.NewLine + Environment.NewLine + str8 + Environment.NewLine + Environment.NewLine;
                    return false;
                }
                str9 = Readtxt(str8);
                this.WriteToTxt(str7, str9, false);
                foreach (string line in sInputText)
                {
                    this.WriteToTxt(str7, Environment.NewLine + line, true);
                }


                string path = sSampleFile;
                sData = sData.Replace("@PATH1", '"' + path + '"').Replace("@PATH2", '"' + str7 + '"').Replace("@QTY", iPrintQty.ToString());
                this.WriteToPrintGo(sFile, sData);
                int num4 = WinExec(sFile, 0);
                sMessage = "OK";
                return true;
            }
            catch (Exception ex)
            {
                sMessage = ex.Message;
                return false;
            }
        }

        private string LoadBatFile(string sFile, ref string sMessage)
        {
            sMessage = string.Empty;
            string str = string.Empty;
            if (!File.Exists(sFile))
            {
                sMessage = "File not exist - " + sFile;
                return str;
            }
            StreamReader reader = new StreamReader(sFile);
            try
            {
                str = reader.ReadLine().Trim();
            }
            finally
            {
                reader.Close();
            }
            return str;
        }
        private void WriteToPrintGo(string sFile, string sData)
        {
            try
            {
                if (File.Exists(sFile))
                {
                    File.Delete(sFile);
                }
                File.AppendAllText(sFile, sData, Encoding.Default);
            }
            finally
            {
            }
        }
        private string Readtxt(string sFile)
        {
            try
            {
                string sData = string.Empty;
                using (StreamReader _sr = new StreamReader(sFile))
                {
                    sData = _sr.ReadLine();
                    return sData;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            { }
        }
        private void WriteToTxt(string sFile, string sData, bool appandFlag)
        {
            StreamWriter writer = null;
            try
            {
                using (writer = new StreamWriter(sFile, appandFlag, Encoding.UTF8))
                {
                    writer.WriteLine(sData);
                    writer.Flush();
                    writer.Close();
                }
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
    }
}
