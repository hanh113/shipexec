using ClientUtilsDll;
using Split.Beans;
using Split.DataGateWay;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Split.Core.Common
{
    /// <summary>
    /// 公共逻辑
    /// </summary>
    class CommonProcessExcute
    {
        ExecutionResult exeRes = new ExecutionResult();
        selectGW sgw = new selectGW();
        baseinfo bean = new baseinfo();
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
        /// <param name="strMo">传入工单号</param>
        /// <param name="dtBom">返回BOM中当站所用材料</param>
        /// <param name="strKP">返回工单成品料号</param>
        /// <returns>执行结果集</returns>
        public ExecutionResult GetMoInfo(baseinfo bean)
        {
            bean.dtBom = null;
            bean.strKeyPart = string.Empty;
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
                bean.dtBom = exeRes.Status ? ds.Tables["curBom"] : null;
                bean.strKeyPart = exeRes.Status ? ds.Tables[0].Rows[0]["varKeyPart"].ToString() : string.Empty;
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            return exeRes;
        }

        /// <summary>
        /// 检查执行SN输入逻辑
        /// </summary>
        /// <param name="strMo">工单号</param>
        /// <param name="strSN">录入序列号</param>
        /// <param name="dtSNDetail">返回刷入SN信息</param>
        /// <param name="strSplitQty">返回当前工单总拆分数</param>
        /// <returns>执行结果集</returns>
        public ExecutionResult ExecuteBySN(baseinfo bean)
        {
            bean.dtSnDetail = null; bean.strSplitQty = string.Empty;
            exeRes = new ExecutionResult();
            try
            {
                //站别不为空时，线别值不会为空 多余检查
                if (string.IsNullOrEmpty(bean.strMo) || string.IsNullOrEmpty(SetupInfo.strStation) || string.IsNullOrEmpty(SetupInfo.strLine) || string.IsNullOrEmpty(bean.strSN))
                {
                    return exeRes = new ExecutionResult
                    {
                        Status = false,
                        Message = string.IsNullOrEmpty(bean.strMo) ? "Mo number cannot be empty." :
                        string.IsNullOrEmpty(SetupInfo.strStation) ? "Setup Station first pls." :
                        string.IsNullOrEmpty(SetupInfo.strLine) ? "Setup Line first pls." : " Invalid SN"
                    };
                };
                //检查&过站写入同一procedure  后续有需求再拆分
                DataSet ds = sgw.ExecuteSP(bean);
                exeRes = new ExecutionResult
                {
                    Status = ds.Tables[0].Rows[0]["varRes"].Equals("OK"),
                    Message = ds.Tables[0].Rows[0]["varRes"].ToString(),
                    Anything = ds
                };
                bean.dtSnDetail = exeRes.Status ? ds.Tables["curDetail"] : null;
                bean.strSplitQty = exeRes.Status ? ds.Tables[0].Rows[0]["varSplitQty"].ToString() : string.Empty;
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            return exeRes;
        }
    }
}
