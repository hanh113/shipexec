using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PTTWebServices.Models
{
     public class M_Ship
    {     
        
        /// <summary>
        /// 出货单号
        /// </summary>
        public string oga01 { get; set; }

        /// <summary>
        /// 仓储批
        /// </summary>
        public List<U_OgcFile> OgcFiles { get; set; }
    }
}
