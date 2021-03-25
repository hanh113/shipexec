using ClientUtilsDll;
using Repack.Beans;
using RePack.Beans;
using RePack.DataGateway;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RePack.Core.Common
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
        /// 开关栈板
        /// </summary>
        /// <param name="strPallet">栈板号</param>
        /// <param name="strFlag">开关标识</param>
        /// <returns>结果集合</returns>
        public ExecutionResult CheckPalletNo(string strPallet, string strFlag)
        {
            exeRes = new ExecutionResult();
            try
            {
                DataTable dt = sgw.ExecuteByPallet(strPallet, strFlag);
                exeRes = new ExecutionResult { Status = dt.Rows[0]["varRes"].Equals("OK"), Message = dt.Rows[0]["varRes"].ToString() };
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
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
        /// 通过SN查询打印的栈板信息
        /// </summary>
        /// <param name="strPallet">前台录入SN</param>
        /// <param name="strPallet_no">返回栈板号</param>
        /// <param name="strKP">返回料号</param>
        /// <param name="strQty">返回数量</param>
        /// <returns>结果集合</returns>
        public ExecutionResult GetPrintPalletBySN(string strPallet, out string strPallet_no, out string strKP, out string strQty)
        {
            //可以改成exeRes.Arges 传递out 参数
            exeRes = new ExecutionResult();
            strPallet_no = strKP = strQty = string.Empty;
            try
            {
                //去前缀部分可以分离出去
                if (strPallet.Length == 20 && strPallet.Substring(0, 2).Equals("00"))
                {
                    strPallet = strPallet.Substring(2, 18);
                }
                if (strPallet.StartsWith("3S"))
                {
                    strPallet = strPallet.Substring(2);
                }
                if (strPallet.StartsWith("S"))
                {
                    strPallet = strPallet.Substring(1);
                }
                //查询sql
                DataTable dt = sgw.GetPrintPalletBySN(strPallet);
                exeRes = new ExecutionResult
                {
                    Status = dt.Rows.Count > 0,
                    Message = dt.Rows.Count > 0 ? "" : "Invalid SN  ,pls check",
                };
                if (exeRes.Status)
                {
                    strPallet_no = dt.Rows[0]["pallet_no"].ToString();
                    strKP = dt.Rows[0]["part_no"].ToString();
                    strQty = dt.Rows[0]["qty"].ToString();
                }
            }
            catch (Exception ex)
            {
                exeRes = new ExecutionResult { Status = false, Message = ex.Message };
            }
            return exeRes;
        }

        public ExecutionResult GetCartonLabel(string strCarton)
        {
            exeRes = new ExecutionResult();
            //去前缀部分可以分离出去
            if (strCarton.Length == 20 && strCarton.Substring(0, 2).Equals("00"))
            {
                strCarton = strCarton.Substring(2, 18);
            }
            if (strCarton.StartsWith("3S"))
            {
                strCarton = strCarton.Substring(2);
            }
            if (strCarton.StartsWith("S"))
            {
                strCarton = strCarton.Substring(1);
            }
            DataTable dt = sgw.GetCartonLabelInfo(strCarton);
            if (dt != null && dt.Rows.Count > 0)
            {
                exeRes.Status = true;
                exeRes.Anything = dt;
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "Invalid SN  or  Carton No Not Found";
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
            bean.dtMo = null;
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
                bean.dtMo = exeRes.Status ? ds.Tables["curBom"] : null;
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            return exeRes;
        }

        /// <summary>
        /// 检查栈板号
        /// </summary>
        /// <param name="bean">参数集合</param>
        /// <returns>结果集合</returns>
        public ExecutionResult CheckPalletNo(baseinfo bean)
        {
            exeRes = new ExecutionResult();
            bean.strPalletQty = "0";
            try
            {
                DataTable dt = sgw.CheckPalletNo(bean.strPallet, bean.strMo);
                exeRes = new ExecutionResult { Status = dt.Rows[0]["varRes"].Equals("OK"), Message = dt.Rows[0]["varRes"].ToString() };
                bean.strPalletQty = dt.Rows[0]["varPalletQty"].ToString() ?? "0";
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            return exeRes;
        }
        /// <summary>
        /// 检查SN
        /// </summary>
        /// <param name="bean">参数集合</param>
        /// <returns>结果集合</returns>
        public ExecutionResult CheckSN(baseinfo bean)
        {
            try
            {
                exeRes = new ExecutionResult();
                if (string.IsNullOrEmpty(bean.strMo) || string.IsNullOrEmpty(bean.strSN) || (!bean.bAutoFlag && string.IsNullOrEmpty(bean.strPallet)))
                {
                    exeRes.Status = false;
                    exeRes.Message = string.IsNullOrEmpty(bean.strMo) ? "Mo No cannot be empty." : string.IsNullOrEmpty(bean.strSN) ? "Sn cannot be empty" : "Chose Auto Pallet No or Manual Input Pallet No.";
                    return exeRes;
                }
                DataSet ds = sgw.RepackBySN(bean);
                exeRes = new ExecutionResult
                {
                    Status = ds.Tables[0].Rows[0]["varRes"].Equals("OK"),
                    Message = ds.Tables[0].Rows[0]["varRes"].ToString(),
                    Anything = ds
                };
                bean.dtSnDetail = exeRes.Status ? ds.Tables["curDetail"] : null;
                bean.dtCartonLabel = exeRes.Status ? ds.Tables["curCartonInfo"] : null;
                bean.strPallet = exeRes.Status ? ds.Tables[0].Rows[0]["varPalletTemp"].ToString() : "";
            }
            catch (Exception ex)
            {
                exeRes = new ExecutionResult { Status = false, Message = ex.Message };
            }
            return exeRes;
        }
    }
}
