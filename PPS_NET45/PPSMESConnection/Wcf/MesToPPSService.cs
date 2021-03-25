using Newtonsoft.Json;
using OperationWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPSMESConnection.Wcf
{
    public class MesToPPSService : HttpHosting, IMesToPPSService
    {
        //
        private const string logFile = "MesToPPSService.txt";
        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public string QHoldUnHold(string data)
        {
            WriteLog(logFile, "QHoldUnHold : "+data);
            return JsonConvert.SerializeObject(new PPSResultModel {
                Result = true,
                Msg = "Success"
            });
        }
    }
}
