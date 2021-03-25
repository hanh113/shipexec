using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Packingparcel.Entitys;
using System.Net;


namespace Packingparcel.Utils
{
    class WriteAndReadUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileBackPath">A path</param>
        /// <param name="filePath">B path</param>
        /// <param name="fileContent"> 文件内容</param>
        /// <returns></returns>
        public static ExecuteResult writeToByFilePathAndFileContent(string fileBackUpPath, string filePath, string fileContent)
        {
            ExecuteResult exeRes = new ExecuteResult();
            try
            {
                if (File.Exists(fileBackUpPath))
                {
                    File.Delete(fileBackUpPath);
                }
                FileStream fs = new FileStream(fileBackUpPath, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                byte[] mybyte = Encoding.UTF8.GetBytes(fileContent);
                fileContent = Encoding.UTF8.GetString(mybyte);
                sw.Write(fileContent);
                sw.Close();
                File.Copy(fileBackUpPath, filePath, true);
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            return exeRes;
        }

        /// <summary>
        /// 备份并抛转文件
        /// </summary>
        /// <param name="fileBackUpPath">备份路径</param>
        /// <param name="lisfilePath">目的路径集合</param>
        /// <param name="fileContent">文件内容</param>
        /// <returns>执行结果</returns>
        public static ExecuteResult writeToByFilePathAndFileContent(string fileBackUpPath, List<string> lisfilePath, string fileContent)
        {
            
            ExecuteResult exeRes = new ExecuteResult();
            try
            {
                if (File.Exists(fileBackUpPath))
                {
                    File.Delete(fileBackUpPath);
                }
                FileStream fs = new FileStream(fileBackUpPath, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                byte[] mybyte = Encoding.UTF8.GetBytes(fileContent);
                fileContent = Encoding.UTF8.GetString(mybyte);
                sw.Write(fileContent);
                sw.Close();
                //遍历目的路径并写入文件
                int check = lisfilePath.Count;
                foreach (string filePath in lisfilePath)
                {
                    //File.Copy(fileBackUpPath, filePath, true);
                    try
                    {
                        File.Copy(fileBackUpPath, filePath, true);
                    }

                    catch (Exception e)
                    {
                        check--;
                        if (check == 0)
                        {
                            throw;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            return exeRes;
        }
        /// <summary>
        /// send trans-in file using FTP protocol by wenxing 2021-2-23
        /// </summary>
        /// <param name="fileBackUpPath"></param>
        /// <param name="lisfilePath"></param>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        public static ExecuteResult writeToByFilePathAndFileContentFTP(string fileBackUpPath, List<string> lisfilePath, string fileContent)
        {
            ExecuteResult exeRes = new ExecuteResult();
            string fileName = fileBackUpPath.Substring(fileBackUpPath.LastIndexOf(@"\") + 1);
            try
            {
                if (File.Exists(fileBackUpPath))
                {
                    File.Delete(fileBackUpPath);
                }
                FileStream fs = new FileStream(fileBackUpPath, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                byte[] mybyte = Encoding.UTF8.GetBytes(fileContent);
                fileContent = Encoding.UTF8.GetString(mybyte);
                sw.Write(fileContent);
                sw.Close();
                int check = lisfilePath.Count;
                string ftpAccPwd = String.Empty;
                string outMsg = String.Empty;
                var res = new Dao.SelectData().GetDBTypeBySP("FTP_ACC_PWD", out ftpAccPwd, out outMsg);
                if (res.IndexOf("NG") >= 0 || ftpAccPwd.Split('#').Length != 2)
                {
                    exeRes.Status = false;
                    exeRes.Message = "FTP 账号还没配置！";
                    return exeRes;
                }

                string acc = ftpAccPwd.Split('#')[0];
                string pass = ftpAccPwd.Split('#')[1];

                foreach (string filePath in lisfilePath)
                {
                    //string ftpURI = "ftp://" + filePath;
                    string ftpURI = filePath;

                    try
                    {
                        var request = (FtpWebRequest)WebRequest.Create(ftpURI + "/" + fileName);
                        try
                        {
                            request.Credentials = new NetworkCredential(acc, pass);
                            request.Method = WebRequestMethods.Ftp.GetFileSize;
                            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                            //delete
                            request = (FtpWebRequest)WebRequest.Create(ftpURI + "/" + fileName);
                            request.Credentials = new NetworkCredential(acc, pass);
                            request.Method = WebRequestMethods.Ftp.DeleteFile;
                            response = (FtpWebResponse)request.GetResponse();
                            response.Close();
                        }
                        catch { }
                        finally
                        {
                            request = (FtpWebRequest)WebRequest.Create(ftpURI + "/" + fileName);
                            request.Credentials = new NetworkCredential(acc, pass);
                            request.Method = WebRequestMethods.Ftp.UploadFile;
                            using (FileStream fstream = File.OpenRead(fileBackUpPath))
                            {
                                byte[] buffer = new byte[fstream.Length];
                                fstream.Read(buffer, 0, buffer.Length);
                                fstream.Close();
                                Stream requestStream = request.GetRequestStream();
                                requestStream.Write(buffer, 0, buffer.Length);
                                requestStream.Flush();
                                requestStream.Close();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        check--;
                        if (check == 0)
                        {
                            throw;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }

            return exeRes;
        }
    }
}
