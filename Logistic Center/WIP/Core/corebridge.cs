using ClientUtilsDll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIP.DataGateway;

namespace WIP.Core
{
    class corebridge
    {
        selectGW sgw = new selectGW();
        ExecutionResult exeRes = new ExecutionResult();
        public ExecutionResult GetAllMoInfo(DateTime dtStart, DateTime dtEnd)
        {
            exeRes = new ExecutionResult();
            try
            {
                if (dtEnd < dtStart.AddDays(-1))
                {
                    return exeRes = new ExecutionResult { Status = false, Message = "开始时间不能大于结束时间" };
                }
                DataTable dt = sgw.GetMoByDate(dtStart, dtEnd);
                exeRes = new ExecutionResult { Status = dt.Rows.Count > 0, Message = dt.Rows.Count > 0 ? "Query Success,Mo count:" + dt.Rows.Count.ToString() : "No data found", Anything = dt.Rows.Count > 0 ? dt : null };
            }
            catch (Exception ex)
            {
                exeRes = new ExecutionResult { Status = false, Message = ex.Message };
            }
            return exeRes;
        }

        public ExecutionResult GetBomByMo(string strMo)
        {
            exeRes = new ExecutionResult();
            try
            {
                if (string.IsNullOrEmpty(strMo))
                    return exeRes = new ExecutionResult { Status = false, Message = "Mo No cannot be empty." };
                DataTable dt = sgw.GetBomByMo(strMo);
                exeRes = new ExecutionResult { Status = dt.Rows.Count > 0, Message = dt.Rows.Count > 0 ? "Query Success,Bom count:" + dt.Rows.Count.ToString() : "No data found", Anything = dt.Rows.Count > 0 ? dt : null };
            }
            catch (Exception ex)
            {
                exeRes = new ExecutionResult { Status = false, Message = ex.Message };
            }
            return exeRes;
        }

        internal ExecutionResult GetSnInfo(string strScanCode)
        {
            exeRes = new ExecutionResult();
            try
            {
                DataSet ds = sgw.GetSnInfo(strScanCode);
                exeRes = new ExecutionResult
                {
                    Status = ds.Tables[0].Rows[0]["varRes"].Equals("OK"),
                    Message = ds.Tables[0].Rows[0]["varRes"].ToString()
                };
                exeRes.Anything = exeRes.Status ? ds.Tables["curSNInfo"] : null;
            }
            catch (Exception ex)
            {
                exeRes = new ExecutionResult { Status = false, Message = ex.Message };
            }
            return exeRes;
        }
    }
}
