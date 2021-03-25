using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Write_ReaderTxt
{
    public class WriteFile
    {
        public void writeFile(string path, string  fileName,string data)
        {
            string path_fileName = path + fileName;
            if (!File.Exists(path_fileName))
            {
                FileStream fs1 = new FileStream(path_fileName, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(data);//开始写入值
                sw.Close();
                fs1.Close();
            }
            //else
            //{
            //    FileStream fs = new FileStream(path_fileName, FileMode.Open, FileAccess.Write);
            //    StreamWriter sr = new StreamWriter(fs);
            //    sr.WriteLine(data);//开始写入值
            //    sr.Close();
            //    fs.Close();
            //}
            //if (!File.Exists("F:\\TestTxt.txt"))
            //{
            //    FileStream fs1 = new FileStream("D:\\TestTxt.txt", FileMode.Create, FileAccess.Write);//创建写入文件 
            //    StreamWriter sw = new StreamWriter(fs1);
            //    sw.WriteLine("123");//开始写入值
            //    sw.Close();
            //    fs1.Close();
            //}
            //else
            //{
            //    FileStream fs = new FileStream("D:\\TestTxt.txt", FileMode.Open, FileAccess.Write);
            //    StreamWriter sr = new StreamWriter(fs);
            //    sr.WriteLine("789");//开始写入值
            //    sr.Close();
            //    fs.Close();
            //}
        }
    }
}