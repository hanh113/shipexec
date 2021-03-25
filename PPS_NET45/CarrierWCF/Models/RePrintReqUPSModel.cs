using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierWCF.Models
{
    class RePrintReqUPSModel : Credentials
    {
        public PrintConfiguration PrintConfiguration { set; get; }
    }
    public class PrintConfiguration
    {
        public string GlobalMsn { get; set; }
    }
}
