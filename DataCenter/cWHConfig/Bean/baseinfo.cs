using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cWHConfig.Bean
{
    public class baseinfo
    {
        public string CombType { get; set; }
        public string CombFilter { get; set; }
        public string FilterText { get; set; }
        public int Enabled { get; set; }

        public string g_sUserID { get => ClientUtils.UserPara1; }
        public string g_sProgram { get => ClientUtils.fProgramName; }
        public string g_sFunction { get => ClientUtils.fFunctionName; }
        public string g_sVersion { get => (new fMain()).ProductVersion; }

        public string sStoreNo { get; set; }
        public string sWareHouse { get; set; }
        public string sLocation { get; set; }
        public string sFlag { get; set; }



    }
}
