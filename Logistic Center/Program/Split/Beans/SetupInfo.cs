using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Split.Beans
{
    /// <summary>
    /// 线体站别
    /// </summary>
    public class SetupInfo
    {
        public static string strUser { get => ClientUtils.fLoginUser; }
        public static string strLine { get; set; }
        public static string strStation { get; set; }
    }
}
