using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check.Beans
{
    public class baseinfo
    {
        public string strMo { get; set; }
        public string strSN { get; set; }
        public string strKeyPart { get; set; }
        public string strCarton { get; set; }
        public DataTable dtSnInfo { get; set; }
        public double dcmSTDWeight { get; set; }
        /// <summary>
        /// 标准重量上限
        /// </summary>
        public double dblUpperWeight { get => dcmSTDWeight * 1.03; }
        /// <summary>
        /// 标准重量下限
        /// </summary>
        public double dblLowerWeight { get => dcmSTDWeight * 0.97; }
        /// <summary>
        /// 实际重量
        /// </summary>
        public double dblRealWeight { get; set; }
    }
}
