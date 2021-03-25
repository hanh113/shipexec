using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Data.OracleClient;
using System.Xml;
using System.Security.Cryptography;
using System.Data.SqlClient;
namespace PickListAC
{
    class SajetCommon
    {

        public static string g_sAESKeyChroma = "akdirutjdla;3ofkfiAfdaf392-1dkaf";
        public static string g_sFileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.ToString();  //檔案版本 
        public static string g_sFileName = Path.GetFileName(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileName); //檔案名稱          
        public static string g_sExeName = ClientUtils.fCurrentProject;
        //public static string g_sConnection;
        public static DialogResult Show_Message(string sKeyMsg, int iType)
        {
            string sXMLFile = Path.GetFileNameWithoutExtension(g_sFileName);
            return ClientUtils.ShowMessage(sKeyMsg, iType, g_sExeName, sXMLFile);
        }

        public static string SetLanguage(string sSearchText, string sDefaultTxt, int iTransType)
        {
            string sText = SetLanguage(sSearchText, iTransType);
            if (sText != sSearchText)
                return sText;
            else
                return sDefaultTxt;
        }
        public static string SetLanguage(string sSearchText)
        {
            string sXMLFile = "";
            string sText = "";

            sXMLFile = Path.GetFileNameWithoutExtension(g_sFileName); //Dll.xml
            sText = ClientUtils.SetLanguage(sSearchText, g_sExeName, sXMLFile);
            if (sSearchText == sText)
            {
                sXMLFile = g_sExeName; //Program.xml
                sText = ClientUtils.SetLanguage(sSearchText, g_sExeName, sXMLFile);
            }
            return sText;
        }
        public static string SetLanguage(string sSearchText, int iTransType)
        {
            string sXMLFile = "";
            switch (iTransType)
            {
                case 1:  //Dll.xml
                    sXMLFile = Path.GetFileNameWithoutExtension(g_sFileName);
                    break;
                case 2:  //Program.xml
                    sXMLFile = g_sExeName;
                    break;
            }
            string sText = ClientUtils.SetLanguage(sSearchText, g_sExeName, sXMLFile);
            return sText;
        }
        public static void SetLanguageControl(Control c)
        {
            //轉換元件Txt的多國語言
            string sXMLFile = Path.GetFileNameWithoutExtension(g_sFileName);
            ClientUtils.SetLanguage(c, g_sExeName, sXMLFile);
            ClientUtils.SetLanguage(c, g_sExeName, g_sExeName);
        }

        public static string GetSysBaseData(string sProgram, string sParamName, ref string sErrorMsg)
        {
            //讀取SYS_BASE設定值
            string sSQL = "";
            sSQL = " SELECT PARAM_VALUE "
                 + "   FROM SAJET.SYS_BASE "
                 + "  WHERE Upper(PROGRAM) = '" + sProgram.ToUpper() + "' "
                 + "    and Upper(PARAM_NAME) = '" + sParamName.ToUpper() + "' ";
            DataSet dsTemp = ClientUtils.ExecuteSQL(sSQL);
            if (dsTemp.Tables[0].Rows.Count > 0)
                return dsTemp.Tables[0].Rows[0]["PARAM_VALUE"].ToString();
            else
            {
                sErrorMsg = sErrorMsg + sParamName + Environment.NewLine;
                return "";
            }
        }

        public static string GetMaxID(string sTable, string sField, int iIDLength)
        {
            string sMaxID = "0";
            try
            {
                object[][] Params = new object[5][];
                Params[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TFIELD", sField };
                Params[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TTABLE", sTable };
                Params[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TNUM", iIDLength.ToString() };
                Params[3] = new object[] { ParameterDirection.Output, OracleType.VarChar, "TRES", "" };
                Params[4] = new object[] { ParameterDirection.Output, OracleType.VarChar, "T_MAXID", "" };
                DataSet dsTemp = ClientUtils.ExecuteProc("SAJET.SJ_GET_MAXID", Params);

                string sRes = dsTemp.Tables[0].Rows[0]["TRES"].ToString();
                sMaxID = dsTemp.Tables[0].Rows[0]["T_MAXID"].ToString();

                if (sRes != "OK")
                {
                    SajetCommon.Show_Message(sRes, 0);
                    return "0";
                }
            }
            catch (Exception ex)
            {
                SajetCommon.Show_Message("SAJET.SJ_GET_MAXID" + Environment.NewLine + ex.Message, 0);
                return "0";
            }
            return sMaxID;
        }

        public static string GetID(string sTable, string sFieldID, string sFieldName, string sValue)
        {
            return GetID(sTable, sFieldID, sFieldName, sValue, "");
        }
        public static string GetID(string sTable, string sFieldID, string sFieldName, string sValue, string sEnabled)
        {
            //找欄位ID值
            if (string.IsNullOrEmpty(sValue))
                return "0";
            string sSQL = "select " + sFieldID + " from " + sTable + " "
                        + "where " + sFieldName + " = '" + sValue + "' ";
            if (!string.IsNullOrEmpty(sEnabled))
                sSQL = sSQL + " and ENABLED = '" + sEnabled + "' ";
            sSQL = sSQL + " and Rownum = 1 ";

            DataSet dsTemp = ClientUtils.ExecuteSQL(sSQL);
            if (dsTemp.Tables[0].Rows.Count > 0)
                return dsTemp.Tables[0].Rows[0][sFieldID].ToString();
            else
                return "0";
        }
        public static string AESEncoder(string Source, string Key)
        {

            try
            {
                if (Source == null || Source.Length <= 0)
                    throw new ArgumentNullException("Original Text");
                if (Key == null || Key.Length <= 0)
                    throw new ArgumentNullException("Key");
                byte[] encrypted;
                using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
                {
                    aesAlg.Key = Encoding.ASCII.GetBytes(Key);
                    aesAlg.IV = new byte[16]; //設定對稱演算法的初始化向量(16字元)，目前設定為預設值：16 個 0
                    //aesAlg.IV = new Byte[16] { 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                    using (MemoryStream msEncrypt = new MemoryStream())
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            swEncrypt.Write(Source);
                        encrypted = msEncrypt.ToArray();
                    }
                }
                return Convert.ToBase64String(encrypted);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + Source + "," + ex.Message);
                return string.Empty;
            }
        }
        public static string AESDecoder(string Ciphertext, string Key)
        {
            try
            {
                if (Ciphertext == null || Ciphertext.Length <= 0)
                    throw new ArgumentNullException("Encrypted Words");
                if (Key == null || Key.Length <= 0)
                    throw new ArgumentNullException("Key");
                string plaintext = null;
                byte[] CipherText = Convert.FromBase64String(Ciphertext);
                using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
                {
                    aesAlg.Key = Encoding.ASCII.GetBytes(Key);
                    aesAlg.IV = new byte[16]; //設定對稱演算法的初始化向量(16字元)，目前設定為預設值：16 個 0
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    try
                    {
                        using (MemoryStream msDecrypt = new MemoryStream(CipherText))
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            plaintext = srDecrypt.ReadToEnd();
                    }
                    catch (CryptographicException)
                    {
                        throw new CryptographicException("金鑰錯誤");
                    }
                }
                return plaintext;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message);
                return string.Empty;
            }
        }       
        //public static DataSet ExecuteSQL(string sSQL)
        //{
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        SqlDataAdapter adapter = new SqlDataAdapter(sSQL, g_sConnection);
        //        try
        //        {
        //            adapter.Fill(ds);
        //            return ds;
        //        }
        //        finally
        //        {
        //            adapter.Dispose();
        //        }
        //    }
        //    finally
        //    {
        //    }
        //}
        //public static DataSet ExecuteSQL(string sSQL, object[][] Params)
        //{
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        SqlDataAdapter adapter = new SqlDataAdapter(sSQL, g_sConnection);

        //        for (int i = 0; i <= Params.Length - 1; i++)
        //        {

        //            SqlParameter oraclePar = new SqlParameter();
        //            oraclePar.Direction = ParameterDirection.Input;
        //            try
        //            {
        //                oraclePar.DbType = (DbType)Params[i][1];
        //            }
        //            catch
        //            {
        //                switch (Params[i][1].ToString())
        //                {
        //                    case "1": oraclePar.DbType = DbType.String; break;
        //                    case "2": oraclePar.DbType = DbType.Int32; break;
        //                    case "3": oraclePar.DbType = DbType.DateTime; break;
        //                    default: oraclePar.DbType = DbType.String; break;
        //                }
        //            }

        //            oraclePar.ParameterName = Params[i][2].ToString().ToUpper();
        //            oraclePar.Size = Params[i][3].ToString().Length;
        //            oraclePar.Value = Params[i][3].ToString();
        //            adapter.SelectCommand.Parameters.Add(oraclePar);
        //        }
        //        try
        //        {
        //            adapter.Fill(ds);
        //            return ds;
        //        }
        //        finally
        //        {
        //            adapter.Dispose();
        //        }
        //    }
        //    finally
        //    {
        //    }
        //}       
    
}
}
