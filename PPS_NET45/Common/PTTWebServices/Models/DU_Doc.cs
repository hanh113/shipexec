using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PTTWebServices.Models
{
     public class DU_Doc
    {
      
        public User_Account User { get; set; }
        public string ASNtype { get; set; }
        public string EDItype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SHIPMENT_ID { get; set; }
        /// <summary>
        /// 出货单
        /// </summary>
        public List<U_OgaFile> OgaFiles { get; set; }
        /// <summary>
        /// 出货批
        /// </summary>
        public List<U_OgbFile> OgbFiles { get; set; }

    }
}
