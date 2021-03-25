using ClientUtilsDll;
using Stock_In.DataGatway;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_In.Core
{
    class corebridge
    {
        selectGW sgw = new selectGW();
        ExecutionResult exeRes = new ExecutionResult();
        public ExecutionResult GetWarehouseInfo()
        {
            exeRes = new ExecutionResult();
            try
            {
                DataTable dt = sgw.GetWHInfo();
                exeRes = new ExecutionResult
                {
                    Anything = dt,
                    Status = dt.Rows.Count > 0,
                    Message = dt.Rows.Count > 0 ? "OK" : "Not Found Any Warehouse Info."
                };
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            return exeRes;
        }

        public ExecutionResult GetLocatioinByWH(string strWHID)
        {
            exeRes = new ExecutionResult();
            try
            {
                DataTable dt = sgw.GetLocationInfo(strWHID);
                exeRes = new ExecutionResult
                {
                    Anything = dt,
                    Status = dt.Rows.Count > 0,
                    Message = dt.Rows.Count > 0 ? "OK" : "Not Found Any Location Info."
                };
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            return exeRes;
        }

        public ExecutionResult Stock_IN(string strLocation, string strPallet)
        {
            exeRes = new ExecutionResult();
            try
            {
                DataSet ds = sgw.ExecuteStockIn(strLocation, strPallet);
                exeRes = new ExecutionResult
                {
                    Status = ds.Tables[0].Rows[0]["varRes"].Equals("OK"),
                    Message = ds.Tables[0].Rows[0]["varRes"].ToString()
                };
                exeRes.Anything = exeRes.Status ? ds.Tables["curPalletInfo"] : null;
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            return exeRes;
        }

        internal ExecutionResult GetPalletInfo(string strPallet)
        {
            exeRes = new ExecutionResult();
            try
            {
                DataTable dt = sgw.GetPalletInfo(strPallet);
                exeRes = new ExecutionResult
                {
                    Status = dt.Rows.Count > 0,
                    Anything = dt.Rows.Count > 0 ? dt : null,
                    Message = dt.Rows.Count > 0 ? "OK" : "No Data Found",
                };
            }
            catch (Exception ex)
            {
                exeRes = new ExecutionResult { Status = false, Message = ex.Message };
            }
            return exeRes;
        }
    }
}
