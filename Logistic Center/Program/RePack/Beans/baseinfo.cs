using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RePack.Beans
{
    public class baseinfo
    {
        public string strMo { get; set; }
        public string strSN { get; set; }
        public string strPallet { get; set; }
        public string strPalletQty { get; set; }

        public bool bAutoFlag { get; set; }
        public DataTable dtMo { get; set; }
        public DataTable dtSnDetail { get; set; }
        public DataTable dtCartonLabel { get; set; }
    }
}
