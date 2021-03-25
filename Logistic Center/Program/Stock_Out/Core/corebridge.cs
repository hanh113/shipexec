using ClientUtilsDll;
using Stock_Out.DataGateway;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Out.Core
{

    class corebridge
    {
        ExecutionResult exeRes = new ExecutionResult();
        selectGW sgw = new selectGW();
        internal ExecutionResult Excute(string strSN)
        {
            try
            {
                exeRes = sgw.GetSnInfo(strSN);
                DataTable dt = ((DataSet)exeRes.Anything).Tables[0];
                exeRes.Message = strSN + ":" + dt.Rows[0]["varRes"].ToString();
                exeRes.Status = dt.Rows[0]["varRes"].Equals("OK");
                if (exeRes.Status)
                {
                    exeRes.Anything = sgw.RefreshSnInfo(strSN);
                }
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            return exeRes;
        }

        /// <summary>
        /// 获取PPS库存中栈板信息 用于打印标签
        /// </summary>
        /// <param name="strLocation">储位号</param>
        /// <returns>结果集合</returns>
        public ExecutionResult GetLabelInfo(string strLocation)
        {
            exeRes = new ExecutionResult();
            if (string.IsNullOrEmpty(strLocation))
                return exeRes = new ExecutionResult { Status = false, Message = "Location No cannot be empty." };
            DataTable dt = sgw.GetLocationInfo(strLocation);
            if (dt == null || dt.Rows.Count == 0)
            {
                exeRes = new ExecutionResult { Status = false, Message = "Location not exist or Location is Empty." };
            }
            else
            {
                exeRes = new ExecutionResult { Status = true, Anything = dt, Message = "Query Success." };
            }
            return exeRes;
        }
    }
}
