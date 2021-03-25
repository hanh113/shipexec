using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierWCF.Models
{
    public class GetGroupRequestModel : Credentials
    {
        public string Carrier { get; set; }
        /// <summary>
        /// WorldEaseId 
        /// </summary>
        public string GroupId { get; set; }
        public string GroupType { get; set; }
    }
}
