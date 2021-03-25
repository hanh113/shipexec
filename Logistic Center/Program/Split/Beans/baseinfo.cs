using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Split.Beans
{
    public class baseinfo
    {
        public string strMo { get; set; }
        public string strSN { get; set; }
        public string strKeyPart { get; set; }
        public string strSplitQty { get; set; }
        public DataTable dtBom { get; set; }
        public DataTable dtSnDetail { get; set; }
    }
}
