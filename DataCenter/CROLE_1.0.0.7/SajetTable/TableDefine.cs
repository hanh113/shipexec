using SajetClass;
using System;

namespace SajetTable
{
	public class TableDefine
	{
		public struct TGrid_Field
		{
			public string sFieldName;

			public string sCaption;
		}

		public static string gsDef_KeyField = "ROLE_ID";

		public static string gsDef_Table = "SAJET.SYS_ROLE";

		public static string gsDef_HTTable = "SAJET.SYS_HT_ROLE";

		public static string gsDef_OrderField = "ROLE_NAME";

		public static string gsDef_KeyData = "ROLE_NAME";

		public static TGrid_Field[] tGridField;

		public static void Initial_Table()
		{
			Array.Resize(ref tGridField, 2);
			tGridField[0].sFieldName = "ROLE_NAME";
			tGridField[0].sCaption = "Role Name";
			tGridField[1].sFieldName = "ROLE_DESC";
			tGridField[1].sCaption = "Description";
			for (int i = 0; i <= tGridField.Length - 1; i++)
			{
				string sCaption = SajetCommon.SetLanguage(tGridField[i].sCaption, 1);
				tGridField[i].sCaption = sCaption;
			}
		}

		public static string History_SQL(string sID)
		{
			return " Select a.ROle_Name,a.Role_Desc        ,a.ENABLED,b.emp_name,a.UPDATE_TIME  from " + gsDef_HTTable + " a      ,sajet.sys_emp b  Where a." + gsDef_KeyField + " ='" + sID + "'  and a.update_userid = b.emp_id(+)  Order By a.Update_Time ";
		}
	}
}
