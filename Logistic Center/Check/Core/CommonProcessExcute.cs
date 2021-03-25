using ClientUtilsDll;
using Check.Beans;
using Check.Beans;
using Check.DataGateway;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check.Core.Common
{
    class CommonProcessExcute
    {
        ExecutionResult exeRes = new ExecutionResult();
        selectGW sgw = new selectGW();
        baseinfo bean = new baseinfo();
        /// <summary>
        /// 获取线别信息
        /// </summary>
        /// <returns>结果集合</returns>
        public ExecutionResult GetLineInfo()
        {
            try
            {
                DataTable dt = sgw.QueryAllLine();
                exeRes = new ExecutionResult
                {
                    Anything = dt,
                    Status = dt.Rows.Count > 0,
                    Message = dt.Rows.Count > 0 ? "OK" : "Not Found Any Line Info."
                };
            }
            catch (Exception ex)
            {
                exeRes = new ExecutionResult { Status = false, Message = ex.Message };
            }
            return exeRes;
        }


        /// <summary>
        /// 根据线别查询站别
        /// </summary>
        /// <param name="strLine">线别名</param>
        /// <returns>结果集合</returns>
        public ExecutionResult GetStationByLine(string strLine)
        {
            try
            {
                DataTable dt = sgw.QueryStationByLine(strLine);
                exeRes = new ExecutionResult
                {
                    Anything = dt,
                    Status = dt.Rows.Count > 0,
                    Message = dt.Rows.Count > 0 ? "OK" : string.Format("Not Found Any Station By this Line:{0}.", strLine)
                };
            }
            catch (Exception ex)
            {
                exeRes = new ExecutionResult { Status = false, Message = ex.Message };
            }
            return exeRes;
        }


        /// <summary>
        /// 获取工单信息
        /// </summary>
        /// <param name="bean">集合参数 key:mo & station</param>
        /// <returns>结果集合</returns>
        public ExecutionResult GetMoInfo(baseinfo bean)
        {
            try
            {
                //保证关键值不为空
                if (string.IsNullOrEmpty(bean.strMo) || string.IsNullOrEmpty(SetupInfo.strStation))
                {
                    return exeRes = new ExecutionResult
                    {
                        Status = false,
                        Message = string.IsNullOrEmpty(bean.strMo) ? "Mo number cannot be empty." : "Setup Station first pls."
                    };
                };
                //检查&获取工单信息
                DataSet ds = sgw.GetMoInfo(bean.strMo);
                exeRes = new ExecutionResult
                {
                    Status = ds.Tables[0].Rows[0]["varRes"].Equals("OK"),
                    Message = ds.Tables[0].Rows[0]["varRes"].ToString(),
                    Anything = ds
                };
                bean.strKeyPart = exeRes.Status ? ds.Tables[0].Rows[0]["varKeyPart"].ToString() : "N/A";
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            return exeRes;
        }

        /// <summary>
        /// 获取并检查箱信息
        /// </summary>
        /// <param name="bean">参数集合</param>
        /// <returns>结果集合</returns>
        public ExecutionResult GetCartonInfo(baseinfo bean)
        {
            exeRes = new ExecutionResult();
            try
            {
                if (string.IsNullOrEmpty(bean.strMo) || string.IsNullOrEmpty(bean.strCarton))
                {
                    return exeRes = new ExecutionResult { Status = false, Message = string.IsNullOrEmpty(bean.strMo) ? "Mo No cannot be empty." : "Carton No cannot be empty." };
                }
                DataSet ds = sgw.GetCartonInfo(bean);
                exeRes = new ExecutionResult
                {
                    Status = ds.Tables[0].Rows[0]["varRes"].Equals("OK"),
                    Message = ds.Tables[0].Rows[0]["varRes"].ToString(),
                    Anything = ds
                };
                bean.dtSnInfo = exeRes.Status ? ds.Tables["curSNList"] : null;
                bean.dcmSTDWeight = exeRes.Status ? Convert.ToDouble(ds.Tables[0].Rows[0]["numSTDWeight"].ToString()) : 0;
            }
            catch (Exception ex)
            {
                exeRes = new ExecutionResult { Status = false, Message = ex.Message };
            }
            return exeRes;
        }

        /// <summary>
        /// 过站逻辑
        /// </summary>
        /// <param name="bean">参数集合</param>
        /// <param name="strWeight">前台获取重量</param>
        /// <returns>结果集合</returns>
        internal ExecutionResult PassStation(baseinfo bean, string strWeight)
        {
            exeRes = new ExecutionResult();
            if (!CheckNum(strWeight))
            {
                return exeRes = new ExecutionResult { Status = false, Message = "Get Weight Error." };
            }
            bean.dblRealWeight = Convert.ToDouble(strWeight);
            if (bean.dblLowerWeight > bean.dblRealWeight)
            {
                return exeRes = new ExecutionResult { Status = false, Message = string.Format("Weight {0} cannot be   less than {1}", bean.dblRealWeight, bean.dblLowerWeight) };
            }
            else if (bean.dblRealWeight > bean.dblUpperWeight)
            {
                return exeRes = new ExecutionResult { Status = false, Message = string.Format("Weight {0} cannot be  more than {1}", bean.dblRealWeight, bean.dblUpperWeight) };
            }
            DataTable dt = sgw.PassStation(bean);
            exeRes = new ExecutionResult
            {
                Status = dt.Rows[0]["varRes"].Equals("OK"),
                Message = dt.Rows[0]["varRes"].ToString()
            };
            return exeRes;
        }

        public bool CheckNum(string temp)
        {
            bool bStatus = false;
            double i = 0.0;
            try
            {
                i = Convert.ToDouble(temp);
                bStatus = true;
            }
            catch
            {
            }

            return bStatus;
        }
    }
}
