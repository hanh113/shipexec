using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PTTWebServices.Models
{
     public class DM_Doc 
    {
        public User_Account User { get; set; }
        public string ASNtype { get; set; }
        public string EDItype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SHIPMENT_ID { get; set; }

        /// <summary>
        /// 仓储批
        /// </summary>
        public List<DM_Ship> DM_Ships { get; set; }
        //public DM_Doc(string OASNtype = "post", string EDItype = "940", string SHIPMENT_ID = "940")
        //{
        //    this.ASNtype = ASNtype;
        //    this.EDItype = EDItype;
        //    this.SHIPMENT_ID = SHIPMENT_ID;
        //}

    }
 
}
