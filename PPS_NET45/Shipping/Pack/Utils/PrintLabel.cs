using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using Packingparcel.Entitys;
using System.Data;
//using System.Windows.Forms;

namespace Packingparcel.Utils
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
        /// <summary>
        /// 新的打印方法：默认btw文件，参数格式已经写好
        /// </summary>
        /// <param name="labelName">label名称</param>
        /// <param name="labelContentList">label内容</param>
        /// <param name="printQty">打印数量</param>
        /// <returns>
        /// exeRes.status  返回是否打印成功
        /// exeRes.Message  返回打印信息
        /// </returns>
        public ExecuteResult printLableForModifyVersion(string labelName, List<string> labelContentList, int printQty)
        {
            /*
             PrintLable.bat
             printGo.bat 
             放于启动文件夹下  D:\MES_CLIENT\                        
             启动 bartender.exe 文件的  传入三个参数   path= .btw   path= .lst    printQty
             */
            deleteOnTimeFile();//定时删除           
            ExecuteResult exeRes = new ExecuteResult();
            exeRes = labelVersionCheck(labelName);
            if (!exeRes.Status)
            {
                return exeRes;
            }
            try
            {
                using (Process p = new Process())
                {
                    string startPathDir = Application.StartupPath;
                    string secDir = startPathDir + @"\Shipping\";
                    string labelPath_btw = (string)exeRes.Anything;//默认 label放于启动文件夹二级文件夹下  \Shipping
                    string labelPath_dat = labelPath_btw.Split('.')[0] + ".dat";
                    string printLabelBat = startPathDir + @"\PrintLabel.bat";
                    if (!File.Exists(labelPath_btw))
                    {
                        exeRes.Message = "需要打印的label不存在，请检查！-" + labelPath_btw;
                        exeRes.Status = false;
                        return exeRes;
                    }
                    //string sData = Readtxt(printLabelBat);
                    string dateStr = DateTime.Now.ToString("yyyy-MM-dd");
                    string lstDir = secDir + @"Lst\" + dateStr + @"\";//用于存放Lst文件   做定时删除
                    if (!Directory.Exists(lstDir))
                    {
                        Directory.CreateDirectory(lstDir);
                    }
                    string labelPath_lst = lstDir + createUniqueLabelLstName() + ".lst";
                    //将btw文件中  数据格式读出  
                    string headParaStr = Readtxt(labelPath_dat);
                    string lstContentStr = "";
                    foreach (string perLstContent in labelContentList)
                    {
                        lstContentStr += perLstContent + Environment.NewLine;
                    }
                    lstContentStr = headParaStr + "\r\n" + lstContentStr;
                    this.WriteToPrintGo(labelPath_lst, lstContentStr);
                    string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                    sArguments = sArguments.Replace("@PATH1", '"' + labelPath_btw + '"').Replace("@PATH2", '"' + labelPath_lst + '"').Replace("@QTY", printQty.ToString());
                    p.StartInfo.Arguments = sArguments;
                    p.StartInfo.FileName = "bartend.exe";
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                    exeRes.Message = "打印成功--" + labelPath_btw;
                }
            }
            catch (Exception e)
            {
                exeRes.Status = false;
                exeRes.Message = e.Message;
            }
            return exeRes;
        }

        public ExecuteResult getLableVersionInfoByLabelName(string labelName)
        {
            ExecuteResult exeRes = new ExecuteResult();
            try
            {
                string sql = string.Format(@"   SELECT TLV.* FROM PPSUSER.T_LABEL_VERSION TLV
                                            WHERE TLV.LABEL_NAME = '{0}'", labelName);
                DataTable dt = new DataTable();
                dt = ClientUtils.ExecuteSQL(sql).Tables[0];
                if (dt.Rows.Count > 0 && dt != null)
                {
                    exeRes.Anything = dt;
                }
                else
                {
                    exeRes.Status = false;
                    exeRes.Message = "此 " + labelName + " 数据库中无资料，请联系 IT-PPS Team！";
                }
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            return exeRes;
        }
        public ExecuteResult labelVersionCheck(string labelName)
        {
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            string shippingPath = Application.StartupPath + @"\Shipping\";
            exeRes = getLableVersionInfoByLabelName(labelName);
            if (!exeRes.Status)
            {
                return exeRes;
            }
            dt = (DataTable)exeRes.Anything;
            string serverVersion = dt.Rows[0]["Label_Version"].ToString();
            bool isCorrect = false;
            string[] locLabelPath = Directory.GetFiles(shippingPath, labelName + "#*.btw", SearchOption.TopDirectoryOnly);
            if (locLabelPath.Length > 0)
            {
                foreach (string path in locLabelPath)
                {
                    string lastPart = path.Split('#')[1];
                    lastPart = lastPart.Substring(0, lastPart.IndexOf('.'));
                    if (serverVersion.Equals(lastPart))
                    {
                        isCorrect = true;
                        exeRes.Anything = path;
                        break;
                    }
                }
                if (!isCorrect)
                {
                    exeRes.Status = false;
                    exeRes.Message = "此label最新版本应为" + labelName + "#" + serverVersion + ".btw,请联系IT-PPS 运维人员处理！";
                }
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "本地无此Label: " + labelName + " ，请联系IT-PPS 运维人员处理！";
            }
            return exeRes;
        }
        /// <summary>
        /// 删除除了今天 lst文件
        /// </summary>
        public void deleteOnTimeFile()
        {
            try
            {
                string LstDir = Application.StartupPath + @"\Shipping\Lst";
                if (Directory.Exists(LstDir))
                {
                    string[] rootDir = Directory.GetDirectories(LstDir);
                    foreach (string dirName in rootDir)
                    {
                        string[] fileArray = dirName.Split('\\');
                        if (!DateTime.Parse(fileArray[4]).ToString("yyyyMMdd").Equals(DateTime.Now.ToString("yyyyMMdd")))
                        {
                            Directory.Delete(dirName, true);
                        }
                    }
                }

            }
            catch
            {
            }
        }



        /// <summary>
        /// 创建一个唯一lst文件名
        /// </summary>
        /// <returns></returns>
        public string createUniqueLabelLstName()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
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
                File.AppendAllText(sFile, sData, Encoding.Unicode);
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
            {

            }
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
