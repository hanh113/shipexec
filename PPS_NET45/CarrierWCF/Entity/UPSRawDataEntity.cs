using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierWCF.Entity
{
    public class UPSRawDataEntity
    {
        public string CARTON_NO { set; get; }
        public string TRACKING_NO { set; get; }
        public string GLOBALMSN { set; get; }
        public string RAWDATA { set; get; }
        public string DELIVERY_NO { set; get; }
    }
}
