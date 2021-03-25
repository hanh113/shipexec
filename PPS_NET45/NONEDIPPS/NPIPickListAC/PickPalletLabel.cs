using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace NPIPickListAC
{
    class PickPalletLabel
    {

        [DllImport("kernel32.dll")]
        public static extern int WinExec(string exeName, int operType);

        //HYQ： 如果是9号pick栈板，palletno必须状态是FP的才能打印和补印，.....
        

        public bool PrintPickPalletLabel_new(string strPickpalletno)
        {
            //HYQ： 本来下面这个是一个功能很多的版本，但是现在打印PickPalletLabel 只要几个栏位就好。
            //完整的版类似水晶报表， strHead +strLine=strALL
            //这里只要strHead 就好
            if (string.IsNullOrEmpty(strPickpalletno))
            {
                return false;
            }

            string sMessage = "";
            string strLabelName = @"NPI_PALLETLABEL";
            string strStartupPath = System.Windows.Forms.Application.StartupPath;
            string strLabelPath = strStartupPath +@"\ShippingAC\Label";
            //HYQ： 这部分是写.dat文件。 5个栏位就好
            string LabelParam = @"SHIPMENTID|HAWB|REGION|PALLETNO|PALLETSEQ|SNQTY|CARTONQTY|";

            string strSID = string.Empty;
            string strHAWB = string.Empty;
            string strRegion = string.Empty;
            string strPalletNO = string.Empty;
            string strPalletSeq = string.Empty;
            string strSNQTY = string.Empty;
            string strCARTONQTY = string.Empty;

            //目前 打印的不知道在哪设置 支持|  ， 目前只能用逗号
            LabelParam = LabelParam.Replace("|", @",");

            string strHead = string.Empty;
            NPIPickListACBLL nb = new NPIPickListACBLL();
            DataTable dt=  nb.GetPalletPrintInfo(strPickpalletno);
            if (dt.Rows.Count >0) 
            {
                 strSID = dt.Rows[0]["shipmentid"].ToString();
                 strHAWB = dt.Rows[0]["hawb"].ToString();
                 strRegion = dt.Rows[0]["region"].ToString();
                 strPalletNO = dt.Rows[0]["palletno"].ToString();
                 strPalletSeq = dt.Rows[0]["palletseq"].ToString();
                 strSNQTY = dt.Rows[0]["snqty"].ToString();
                 strCARTONQTY = dt.Rows[0]["cartonqty"].ToString();


            }

            strHead = strSID+","+ strHAWB + "," + strRegion + "," + strPalletNO + ","  + strPalletSeq + "," + strSNQTY + "," + strCARTONQTY + "," ;
            strHead = LabelParam + "\r\n" + strHead;
            
            string strLst = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".lst";
            this.WriteToPrintGo(strLst, strHead);
            using (Process p = new Process())
            {
                string strSampleFile = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".btw";
                if (!File.Exists(strSampleFile))
                {
                    sMessage = "Sample File Not exists-" + strSampleFile;
                    return false;
                }
                p.StartInfo.FileName = "bartend.exe";
                string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + strLst + '"').Replace("@QTY", "1");
                p.StartInfo.Arguments = sArguments;
                p.Start();
                p.WaitForExit();
            }
            return true;
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
        private void WriteToTxt(string sFile, string sData)
        {
            StreamWriter writer = null;
            try
            {
                using (writer = new StreamWriter(sFile, false, Encoding.UTF8))
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
        
        }
    }



