using ClientUtilsDll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace WMSTransIN
{
    class WHPalletLabel
    {
        [DllImport("kernel32.dll")]
        public static extern int WinExec(string exeName, int operType);
        public bool PrintWHPalletLabel(string strSN)
        {
            //这里只要strHead 就好
            if (string.IsNullOrEmpty(strSN))
            {
                return false;
            }

            string sMessage = "";
            string strLabelName = @"LocationPalletLabel";
            string strStartupPath = System.Windows.Forms.Application.StartupPath;
            string strLabelPath = strStartupPath + @"\Shipping\Label";
            //HYQ： 这部分是写.dat文件。 5个栏位就好
            //PALLETID,MPN,ICTPN,LOCATIONID,QTY,CARTONQTY,
            string LabelParam = @"PALLETID,MPN,ICTPN,LOCATIONID,QTY,CARTONQTY,";

            //目前 打印的不知道在哪设置 支持|  ， 目前只能用逗号
            LabelParam = LabelParam.Replace("|", @",");

            string SQL = string.Empty;
            SQL = string.Format(@"
                      select a.location_no LOCATIONID,
                     a.pallet_no   PALLETID,
                     a.part_no     ICTPN,
                     a.qty,
                     a.cartonqty
                from ppsuser.t_location a
               where a.pallet_no in (SELECT distinct b.newpalletno
                                       FROM PPSUSER.t_other_locate_sn b
                                      where b.carton_no = '{0}'
                                         or b.palletno = '{1}'
                                         or b.customer_sn = '{2}')
                    or a.pallet_no ='{3}'    
                ", strSN, strSN, strSN, strSN);

            DataTable dt = new DataTable();

            try
            {
                dt = ClientUtils.ExecuteSQL(SQL).Tables[0];
            }

            catch (Exception ex)
            {
                MessageBox.Show("strSQL执行异常".TP() + ex.ToString());
                return false;
            }
            string strHead = "";
            string strpackcodedesc = string.Empty;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string str1 = dt.Rows[0]["PALLETID"].ToString();
                    string str2 = "";
                    string str3 = dt.Rows[0]["ICTPN"].ToString();
                    string str4 = dt.Rows[0]["LOCATIONID"].ToString();
                    //if (Convert.ToInt32(str4) == 0) { return false; }
                    string str5 = dt.Rows[0]["QTY"].ToString();
                    string str6 = dt.Rows[0]["CARTONQTY"].ToString();
                    strHead = str1 + "," + str2 + "," + str3 + "," + str4 + "," + str5 + "," + str6 + ",\r\n";
                }
            }
            else
            {
                return false;
            }

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

