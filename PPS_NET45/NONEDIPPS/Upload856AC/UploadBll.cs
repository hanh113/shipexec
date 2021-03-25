using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace UpLoad856AC
{
    class UploadBll
    {
        public string PPSInsertWorkLog(string insn, string inwc, string macaddress, out string errmsg)
        {

            errmsg = string.Empty;
            UploadDAL ud = new UploadDAL();
            string strRB = ud.PPSInsertWorkLogByProcedure(insn, inwc, macaddress, out errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }



        public string PPSCheckWEBLOG(string insn, out string tturl, out string errmsg)
        {

            UploadDAL ud = new UploadDAL();
            string strRB = ud.PPSCheckWebServieByProcedure(insn, out tturl, out errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }

        public string AfterEdiHttpPostWebService(string serverip, string url, string ShipmentID)
        {
            string result = string.Empty;
            string param = string.Empty;
            byte[] bytes = null;

            Stream writer = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;

            param = HttpUtility.UrlEncode("ShipmentID") + "=" + HttpUtility.UrlEncode(ShipmentID) + "&" + HttpUtility.UrlEncode("wdate") + "=" + HttpUtility.UrlEncode("");
            bytes = Encoding.UTF8.GetBytes(param);

            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;

            try
            {
                writer = request.GetRequestStream();        //获取用于写入请求数据的Stream对象
            }
            catch (Exception )
            {
                return "exception_request";
            }

            writer.Write(bytes, 0, bytes.Length);       //把参数数据写入请求数据流
            writer.Close();

            try
            {
                response = (HttpWebResponse)request.GetResponse();      //获得响应
            }
            catch (WebException )
            {
                return "exception_response";
            }

            #region 这种方式读取到的是一个返回的结果字符串
            //Stream stream = response.GetResponseStream();        //获取响应流
            //XmlTextReader Reader = new XmlTextReader(stream);
            //Reader.MoveToContent();
            //result = Reader.ReadInnerXml();
            #endregion

            #region 这种方式读取到的是一个Xml格式的字符串
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            result = reader.ReadToEnd();
            
            #endregion 
            //response.Dispose();
            response.Close();

            reader.Close();
            reader.Dispose();

            //Reader.Dispose();
            //Reader.Close();

            //stream.Dispose();
            //stream.Close();

            string errmsg = string.Empty;
            
            UploadDAL ud = new UploadDAL();
            string strRB = ud.PPSInsertWebServieByProcedure(ShipmentID, serverip,  url, result, out errmsg);

            result = "INSERTWEBSERVICE:"+errmsg +"#WEBSERVICE_RESPONSE:" +result;

            return result;
        }
    }
    
}

