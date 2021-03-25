using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Data;

namespace Weight
{
    public class Utility
    {
        [DllImport("kernel32.dll")]
        public static extern int WinExec(string exeName, int operType);

        //public bool Print_Label(string  strEndPalletNo)
        //{
            //string strShipType = @"select b.type 
            //                    from ppsuser.g_ds_pallet_t a,ppsuser.g_ds_shimment_base_t b 
            //                    where a.shipment_id = b.shipment_id 
            //                    and  a.end_palletno = '"+ strEndPalletNo +"'";
            //DataTable dtshiptype = ClientUtils.ExecuteSQL(strShipType).Tables[0];
            //不区分Bulk,Parcel 需求未确认，先打印出来确认一下，很无奈。
            //if (dtshiptype.Rows[0]["type"].ToString() == "Bulk")
            //{
                //DSPalletSheetlabel dspsl = new DSPalletSheetlabel();
                //dspsl.PrintPalletLabel(strEndPalletNo);
                //return true;
            //}
            //else
            //{
            //    System.Windows.Forms.ListBox ListParam = new System.Windows.Forms.ListBox();
            //    System.Windows.Forms.ListBox ListData = new System.Windows.Forms.ListBox();
            //    string strAssyPath = "";
            //    string strFileFix = "";
            //    string strLabelType = "PALLET_LABEL";
            //    string strMessage = string.Empty;
            //    strAssyPath = Assembly.GetExecutingAssembly().Location;

            //    strAssyPath = strAssyPath.Substring(0, strAssyPath.LastIndexOf('\\')) + "\\";
            //    PrintLabel.Setup PrintLabelDll = new PrintLabel.Setup();
            //    PrintLabel_Bitland.Setup stup = new PrintLabel_Bitland.Setup();
            //    string labelName = "palletsheetlabel";
            //    string labelPath = strAssyPath + labelName + ".btw";
            //    try
            //    {
            //        ListParam.Items.Clear();
            //        ListData.Items.Clear();
            //        ListData.Items.Add(strEndPalletNo);
            //        if (!stup.GetPrintData(strLabelType, ref ListParam, ref ListData, out strMessage)) //获得列印数据
            //        {
            //            return false;
            //        }
            //        string sExeName = ClientUtils.fCurrentProject;
            //        PrintLabelDll.Print_Bartender_DataSource_Single(sExeName, strLabelType, strFileFix, labelName, 1, "BARTENDER", "DATASOURCE", ListParam, ListData, out strMessage);
            //        if (strMessage != "OK")
            //        {
            //            return false;
            //        }
            //        //Thread.Sleep(3000);
            //        return true;
            //    }
            //    catch (Exception ex)
            //    {
            //        return false;
            //    }
            //    //DSPalletSheetlabel dspsl = new DSPalletSheetlabel();
            //    //dspsl.PrintPalletLabel(strEndPalletNo);
            //    //return true;
            //}
        //}


        public bool Print_Bartender_DataSource(string strFileName, string strDirectory, string strInputText, int intPrintQty, out string strMessage)
        {
            try
            {
                strMessage = "OK";
                string strStartupPath = System.Windows.Forms.Application.StartupPath;
                string strSampleFile = strDirectory + @"\" + strFileName + ".btw";
                string str7 = strDirectory + strFileName + ".lst";
                string str8 = strDirectory + strFileName + ".dat";
                string sFile = strStartupPath + @"\PrintGo.bat";
                string str11 = strStartupPath + @"\PrintLabel.bat";
                string sData = this.LoadBatFile(str11, ref strMessage);
                string str9 = string.Empty;
                if (!File.Exists(strSampleFile))
                {
                    strMessage = "Sample File Not exists-" + strFileName;
                    return false;
                }
                if (!string.IsNullOrEmpty(strMessage))
                {
                    return false;
                }
                if (File.Exists(str7))
                {
                    File.Delete(str7);
                }
                if (!File.Exists(str8))
                {
                    strMessage = "Label File Not Found (.dat)" + Environment.NewLine + Environment.NewLine + str8 + Environment.NewLine + Environment.NewLine;
                    return false;
                }
                str9 = Readtxt(str8);
                this.WriteToTxt(str7, str9 + "\r\n" + strInputText);
                string path = strSampleFile;
                sData = sData.Replace("@PATH1", '"' + path + '"').Replace("@PATH2", '"' + str7 + '"').Replace("@QTY", intPrintQty.ToString());
                this.WriteToPrintGo(sFile, sData);
                int num4 = WinExec(sFile, 0);
                strMessage = "OK";
                return true;
            }
            catch (Exception ex)
            {
                strMessage = ex.Message;
                return false;
            }
        }

        private string LoadBatFile(string strFile, ref string strMessage)
        {
            strMessage = string.Empty;
            string str = string.Empty;
            if (!File.Exists(strFile))
            {
                strMessage = "File not exist - " + strFile;
                return str;
            }
            StreamReader reader = new StreamReader(strFile);
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
        private void WriteToPrintGo(string strFile, string strData)
        {
            try
            {
                if (File.Exists(strFile))
                {
                    File.Delete(strFile);
                }
                File.AppendAllText(strFile, strData, Encoding.Default);
            }
            finally
            {
            }
        }
        private string Readtxt(string strFile)
        {
            try
            {
                string sData = string.Empty;
                using (StreamReader _sr = new StreamReader(strFile))
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
        private void WriteToTxt(string strFile, string strData)
        {
            StreamWriter writer = null;
            try
            {
                using (writer = new StreamWriter(strFile, false, Encoding.UTF8))
                {
                    writer.WriteLine(strData);
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
