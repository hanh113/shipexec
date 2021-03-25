
using BarTender;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Weight
{
    public class FileToBarCodePrint
    {
        public static bool TxtWrite(string[] paras)
        {
            try
            {
                //string path = System.IO.Directory.GetCurrentDirectory()+ "\\ppsweight.txt";
                string strVal = "";
                for (var i = 0; i < paras.Length; i++)
                {
                    strVal = strVal + string.Format(@"""{0}"",", paras[i]);
                }
                string strpath = @"C:\pps\ppsweight.txt";
                string strLabel = @"""barcodeSN"",""lableClientUtils"",""lable02"",""lable03"",""lable04"",""lable05"",""lable06"",""lable07"",""lable08"",""lable09"",""lable10""" + "\r\n";
                FileStream fs1 = new FileStream(strpath, FileMode.Create,FileAccess.Write);
                StreamWriter sr1 = new StreamWriter(fs1);
                sr1.WriteLine(strLabel+strVal);//开始写入值
                sr1.Close();
                fs1.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public static bool CodePrint(string fileName)
        {
            try
            {
                BarTender.Application btApp = new BarTender.Application();
                Format btFormat;
                btApp = new BarTender.Application();
                btFormat = btApp.Formats.Open(@"E:\Luxshare\PPS\SO-SOURCE\Weight\bin\Debug\" + fileName, false, "");
                btFormat.PrintSetup.NumberSerializedLabels = 1; //设置打印份数
                btFormat.PrintOut(true, false); //第二个参数设置是否跳出打印属性;
                                                //btFormat.Close(BarTender.BtSaveOptions.btDoNotSaveChanges); //退出时是否保存标签   
                btApp.Quit(BarTender.BtSaveOptions.btSaveChanges);//退出时同步退出bartender进程
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
