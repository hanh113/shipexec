using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MESModel
{
    [ServiceContract]
    public interface IClientHost
    {
        [OperationContract]
        void ProcessMsg(string msg);
        [OperationContract]
        List<FileObject> GetFileLists();
    }
}
