using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//add by wangdunyang   增加检查程式版本
namespace LibHelper.PPSUtils
{
    class CheckProgramVersion
    {

        public static string checkVersion()
        {
            string versionS = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string dllName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            return null;
        }

    }
}
