using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PTTWebServices.Models
{
     public class DM_Ship
    {     

        /// <summary>
        /// 出货单号
        /// </summary>
        public string oga01 { get; set; }
        /// <summary>
        /// 出货批
        /// </summary>
        public List<U_OgbFile> OgbFiles { get; set; }
    }
}
