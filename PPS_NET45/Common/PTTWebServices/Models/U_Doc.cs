using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PTTWebServices.Models
{
     public class U_Doc
    {
      
        public User_Account User { get; set; }
        public string oea01 { get; set;}
        public string ASNtype { get; set; }

        public List<U_OgaFile> U_OgaFiles { get; set; }

        /// <summary>
        /// 仓储批
        /// </summary>
        public List<U_OgcFile> U_OgcFiles { get; set; }
    }
}
