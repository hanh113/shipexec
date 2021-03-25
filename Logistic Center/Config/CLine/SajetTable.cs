using System;
using System.Collections.Generic;
using System.Text;
using SajetClass;

namespace SajetTable
{
    public class TableDefine
    {
        public static String gsDef_KeyField = "LINE_NAME";
        public static String gsDef_Table = "MESUSER.T_LINE_INFO";
        public static String gsDef_HTTable = "MESUSER.T_HIS_LINE_INFO";
        public static String gsDef_OrderField = "LINE_NAME"; //line name
        public static String gsDef_KeyData = "LINE_NAME";  //line name

        public struct TGrid_Field
        {
            public String sFieldName;
            public String sCaption;    //欄位Title          
        }
        public static TGrid_Field[] tGridField;

        public static void Initial_Table()
        {
            //要在Grid顯示出來的欄位及順序
            Array.Resize(ref tGridField, 7);
            tGridField[0].sFieldName = "LINE_NAME";
            tGridField[0].sCaption = "Line Name";
            tGridField[1].sFieldName = "LINE_DESC";
            tGridField[1].sCaption = "Description";
            tGridField[2].sFieldName = "EMP_NO";
            tGridField[2].sCaption = "EMP NO";

            tGridField[3].sFieldName = "FACTORY_CODE";
            tGridField[3].sCaption = "FACTORY CODE";
            tGridField[4].sFieldName = "FACTORY_NAME";
            tGridField[4].sCaption = "FACTORY NAME";
            tGridField[5].sFieldName = "CREATE_DATE";
            tGridField[5].sCaption = "CREATE DATE";
            tGridField[6].sFieldName = "ENABLED";
            tGridField[6].sCaption = "ENABLED";



            //欄位多國語言
            for (int i = 0; i <= tGridField.Length - 1; i++)
            {
                string sText = SajetCommon.SetLanguage(tGridField[i].sCaption, 1);
                tGridField[i].sCaption = sText;
            }
        }

        public static string History_SQL(string sID)
        {
            //string s = " Select c.factory_name,a.pdline_name,a.pdline_Desc,a.pdline_Desc2 "
            //         + "       ,a.ENABLED,b.emp_name,a.UPDATE_TIME "
            //         + " from " + TableDefine.gsDef_HTTable + " a "
            //         + "     ,sajet.sys_emp b "
            //         + "     ,sajet.sys_factory c "
            //         + " Where a." + TableDefine.gsDef_KeyField + " ='" + sID + "' "
            //         + " and a.update_userid = b.emp_id(+) "
            //         + " and a.factory_id = c.factory_id(+) "
            //         + " Order By a.Update_Time ";

            string s = string.Format(@"Select c.factory_name,
                                                       a.Line_Name,
                                                       a.Line_Desc,
                                                       a.ENABLED,
                                                       b.emp_name,
                                                       a.Update_Time
                                                  from {0} a, sajet.sys_emp b, sajet.sys_factory c
                                                 Where a.{1} = '{2}'
                                                   and a.update_userid = b.emp_id(+)
                                                   and a.factory_id = c.factory_id(+)
                                                 Order By a.Update_Time", TableDefine.gsDef_HTTable, TableDefine.gsDef_KeyField, sID);
            return s;
        }
    }
}
