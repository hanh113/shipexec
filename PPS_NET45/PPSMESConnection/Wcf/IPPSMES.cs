using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPSMESConnection.Wcf
{
    public interface IPPSToMESService
    {
    }

    [ServiceContract]
    public interface IMesToPPSService
    {
        /// <summary>
        /// QHold
        /// </summary>
        /// <param name="PalletId"></param>
        /// <returns></returns>
        [OperationContract]
        string QHoldUnHold(string data);
    }

    #region

    public class PPSResultModel
    {
        public bool Result { get; set; }
        public string Msg { get; set; }
    }

    #endregion
}
