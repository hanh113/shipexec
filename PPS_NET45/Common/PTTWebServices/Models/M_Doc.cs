using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PTTWebServices.Models
{
     public class M_Doc 
    {
        public User_Account User { get; set; }
        public string ASNtype { get; set; }

        /// <summary>
        /// 仓储批
        /// </summary>
        public List<M_Ship> M_Ships { get; set; }

    }
 
}
