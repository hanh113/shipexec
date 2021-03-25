using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NPIPickListAC
{
    class NPIPickListACBLL
    {
        public string DelPrefixCartonSN(string insn)
        {
            if (insn.Length == 20 && insn.Substring(0, 2).Equals("00"))
            { insn = insn.Substring(2); }
            else if (insn.StartsWith("3S"))
            { insn = insn.Substring(2); }
            else if (insn.StartsWith("S"))
            { insn = insn.Substring(1); }

            return insn;

        }

        public DataTable GetNPISIDDataTable( string strSID, string strSTATUS, string strStime, string strEtime)
        {
            NPIPickListACDAL wd = new NPIPickListACDAL();
            DataSet dataSet = wd.GetNPISIDDataTableBySQL(strSID, strSTATUS, strStime, strEtime);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public string NPICheckSID(string strSID, string localHostname, out string RetMsg)
        {
            NPIPickListACDAL wd = new NPIPickListACDAL();
            string strRB = wd.NPICheckSIDBySP(strSID, localHostname, out  RetMsg);
            return strRB;
        }


        public DataTable GetSIDLINEINFO(string strSID)
        {
            NPIPickListACDAL wd = new NPIPickListACDAL();
            DataSet dataSet = wd.GetSIDLINEINFOBySQL(strSID);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public DataTable GetSNInfoDataTableBLL(string inputsno)
        {
            if (string.IsNullOrEmpty(inputsno)) { return null; }
            NPIPickListACDAL wd = new NPIPickListACDAL();
            DataSet dataSet = wd.GetSNInfoDataTableDAL(inputsno);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public string NPIPICKInsertCarton( string strPickNOA, out string strPickNO, string strSID, string strCarton, string strUserNo, out string RetMsg, out string strLBL)
        {
            NPIPickListACDAL wd = new NPIPickListACDAL();
            string strRB = wd.NPIPICKInsertCartonBySP( strPickNOA, out  strPickNO,  strSID,  strCarton,  strUserNo, out  RetMsg, out  strLBL);
            return strRB;
        }

        public string NPIPICKUnlockComputer(string strSID, out string RetMsg)
        {
            NPIPickListACDAL wd = new NPIPickListACDAL();
            string strRB = wd.NPIPICKUnlockComputerBySP(strSID, out RetMsg);
            return strRB;
        }

        public DataTable ShowStockInfo(string strPartNo)
        {
            if (string.IsNullOrEmpty(strPartNo)) { return null; }
            NPIPickListACDAL wd = new NPIPickListACDAL();
            DataSet dataSet = wd.GetStockInfoBySQL(strPartNo);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }



        public string AfterEdiHttpPostWebService(string serverip, string url, string ShipmentID,out string strResultOut)
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
            catch (Exception e)
            {
                strResultOut = e.ToString();
                return "NG";
            }

            writer.Write(bytes, 0, bytes.Length);       //把参数数据写入请求数据流
            writer.Close();

            try
            {
                response = (HttpWebResponse)request.GetResponse();      //获得响应
            }
            catch (WebException e1)
            {
                strResultOut = e1.ToString();
                return "NG";
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

            NPIPickListACDAL ud = new NPIPickListACDAL();
            string strRB = ud.PPSInsertWebServieByProcedure(ShipmentID, serverip, url, result, out errmsg);

            strResultOut = "INSERTWEBSERVICE:" + errmsg + "#WEBSERVICE_RESPONSE:" + result;

            return "OK";
        }

        public string PPSCheckWEBLOG(string insn, out string tturl, out string errmsg)
        {

            NPIPickListACDAL ud = new NPIPickListACDAL();
            string strRB = ud.PPSCheckWebServieByProcedure(insn, out tturl, out errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }
        public string NPIPPSCheckWEBLOG(string insn, out string tturl, out string errmsg)
        {

            NPIPickListACDAL ud = new NPIPickListACDAL();
            string strRB = ud.NPIPPSCheckWebServieByProcedure(insn, out tturl, out errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }

        public DataTable GetPalletPrintInfo(string strPartNo)
        {
            if (string.IsNullOrEmpty(strPartNo)) { return null; }
            NPIPickListACDAL wd = new NPIPickListACDAL();
            DataSet dataSet = wd.GetPalletPrintInfoBySQL(strPartNo);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
    }
}
