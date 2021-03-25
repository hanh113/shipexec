using System;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace SajetClass
{
	internal class SajetCommon
	{
		public static string g_sFileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.ToString();

		public static string g_sFileName = Path.GetFileName(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileName);

		public static string g_sExeName = ClientUtils.fCurrentProject;

		public static DialogResult Show_Message(string sKeyMsg, int iType)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(g_sFileName);
			return ClientUtils.ShowMessage(sKeyMsg, iType, g_sExeName, fileNameWithoutExtension);
		}

		public static string SetLanguage(string sSearchText, string sDefaultTxt, int iTransType)
		{
			string text = SetLanguage(sSearchText, iTransType);
			if (text != sSearchText)
			{
				return text;
			}
			return sDefaultTxt;
		}

		public static string SetLanguage(string sSearchText)
		{
			string text = "";
			string text2 = "";
			text = Path.GetFileNameWithoutExtension(g_sFileName);
			text2 = ClientUtils.SetLanguage(sSearchText, g_sExeName, text);
			if (sSearchText == text2)
			{
				text = g_sExeName;
				text2 = ClientUtils.SetLanguage(sSearchText, g_sExeName, text);
			}
			return text2;
		}

		public static string SetLanguage(string sSearchText, int iTransType)
		{
			string xmlFileName = "";
			switch (iTransType)
			{
			case 1:
				xmlFileName = Path.GetFileNameWithoutExtension(g_sFileName);
				break;
			case 2:
				xmlFileName = g_sExeName;
				break;
			}
			return ClientUtils.SetLanguage(sSearchText, g_sExeName, xmlFileName);
		}

		public static void SetLanguageControl(Control c)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(g_sFileName);
			ClientUtils.SetLanguage(c, g_sExeName, fileNameWithoutExtension);
			ClientUtils.SetLanguage(c, g_sExeName, g_sExeName);
		}

		public static string GetSysBaseData(string sProgram, string sParamName, ref string sErrorMsg)
		{
			string text = "";
			text = " SELECT PARAM_VALUE    FROM SAJET.SYS_BASE   WHERE Upper(PROGRAM) = '" + sProgram.ToUpper() + "'     and Upper(PARAM_NAME) = '" + sParamName.ToUpper() + "' ";
			DataSet dataSet = ClientUtils.ExecuteSQL(text);
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				return dataSet.Tables[0].Rows[0]["PARAM_VALUE"].ToString();
			}
			sErrorMsg = sErrorMsg + sParamName + Environment.NewLine;
			return "";
		}

		public static string GetMaxID(string sTable, string sField, int iIDLength)
		{
			string text = "0";
			try
			{
				DataSet dataSet = ClientUtils.ExecuteProc("SAJET.SJ_GET_MAXID", new object[5][]
				{
					new object[4]
					{
						ParameterDirection.Input,
						OracleType.VarChar,
						"TFIELD",
						sField
					},
					new object[4]
					{
						ParameterDirection.Input,
						OracleType.VarChar,
						"TTABLE",
						sTable
					},
					new object[4]
					{
						ParameterDirection.Input,
						OracleType.VarChar,
						"TNUM",
						iIDLength.ToString()
					},
					new object[4]
					{
						ParameterDirection.Output,
						OracleType.VarChar,
						"TRES",
						""
					},
					new object[4]
					{
						ParameterDirection.Output,
						OracleType.VarChar,
						"T_MAXID",
						""
					}
				});
				string text2 = dataSet.Tables[0].Rows[0]["TRES"].ToString();
				text = dataSet.Tables[0].Rows[0]["T_MAXID"].ToString();
				if (text2 != "OK")
				{
					Show_Message(text2, 0);
					return "0";
				}
			}
			catch (Exception ex)
			{
				Show_Message("SAJET.SJ_GET_MAXID" + Environment.NewLine + ex.Message, 0);
				return "0";
			}
			return text;
		}

		public static string GetID(string sTable, string sFieldID, string sFieldName, string sValue)
		{
			return GetID(sTable, sFieldID, sFieldName, sValue, "");
		}

		public static string GetID(string sTable, string sFieldID, string sFieldName, string sValue, string sEnabled)
		{
			if (string.IsNullOrEmpty(sValue))
			{
				return "0";
			}
			string str = "select " + sFieldID + " from " + sTable + " where " + sFieldName + " = '" + sValue + "' ";
			if (!string.IsNullOrEmpty(sEnabled))
			{
				str = str + " and ENABLED = '" + sEnabled + "' ";
			}
			str += " and Rownum = 1 ";
			DataSet dataSet = ClientUtils.ExecuteSQL(str);
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				return dataSet.Tables[0].Rows[0][sFieldID].ToString();
			}
			return "0";
		}

		public static bool CheckEnabled(string sType, string sPrivilege)
		{
			try
			{
				string sSQL = " SELECT SAJET.SJ_PRIVILEGE_DEFINE('" + sType + "','" + sPrivilege + "') TENABLED from DUAL ";
				DataSet dataSet = ClientUtils.ExecuteSQL(sSQL);
				string a = dataSet.Tables[0].Rows[0]["TENABLED"].ToString();
				return a == "Y";
			}
			catch (Exception ex)
			{
				Show_Message("Function:SAJET.SJ_PRIVILEGE_DEFINE" + Environment.NewLine + ex.Message, 0);
				return false;
			}
		}
	}
}
