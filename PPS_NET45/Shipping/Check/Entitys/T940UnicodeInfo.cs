using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Check.Entitys
{
    /// <summary>
    /// t_940_unicode  key-->deliveryNo,lineItem
    /// </summary>
    class T940UnicodeInfo
    {
        public string region { get; set; }
        public string customerGroup { get; set; }
        public string msgFlag { get; set; }
        public string gpFlag { get; set; }
        public string deliveryNo { get; set; }
        public string lineItem { get; set; }
        public string ShipCntyCode { get; set; }
    }
}
