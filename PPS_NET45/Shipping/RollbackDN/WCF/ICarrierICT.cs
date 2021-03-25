using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CarrierWCF.Wcf
{
    public interface ICarrierToICTService
    {
    }
    [ServiceContract]
    [XmlSerializerFormat]
    public interface IICTToCarrierService
    {
        [OperationContract]
        string Ship(string data);
        [OperationContract]
        string Void(string data);
    }
}
