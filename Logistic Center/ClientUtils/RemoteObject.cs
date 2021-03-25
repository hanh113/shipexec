using RemoteService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientUtilsDll
{
    public class RemoteObject
    {
        public RemoteObject()
        {
        }
        public IRemoteServiceObject New()
        {
            return OperationWCF.HttpChannel.Get<IRemoteServiceObject>(ClientUtils.ServerUrl);
        }
    }
}

