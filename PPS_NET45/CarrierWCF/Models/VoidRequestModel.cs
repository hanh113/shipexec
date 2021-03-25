using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierWCF.Models
{
    public class VoidRequestModel : Credentials
    {
        public int[] GlobalMsns { get; set; }
    }
}
