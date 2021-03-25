using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientUtilsDll;
using cWHConfig.Bean;
using cWHConfig.DataGatWay;

namespace cWHConfig.Core
{
    public class corebridge
    {
        ExecutionResult exeRes;
        dataGateWay dgw;
        public corebridge()
        {
            dgw = new dataGateWay();
            exeRes = new ExecutionResult();
        }
        public DataTable GetDataByType(baseinfo bean)
        {
            DataSet ds = new DataSet();
            switch (bean.CombType)
            {
                case "STORE":
                    ds = dgw.GetDataForStore(bean);
                    break;
                case "WAREHOUSE":
                    ds = dgw.GetDataForWarehouse(bean);
                    break;
                case "LOCATION":
                    ds = dgw.GetDataForLocation(bean);
                    break;
                default:
                    break;
            }
            if (ds.Tables.Count < 1)
                return null;
            return ds.Tables[0];
        }

        internal DataTable GetWHbyStore(baseinfo bean)
        {
            DataSet ds = new DataSet();
            ds = dgw.GetWHbyStore(bean);
            if (ds.Tables.Count < 1)
                return null;
            return ds.Tables[0];
        }

        internal ExecutionResult InsertData(string mainCombType, string StoreNo, string WarehouseNo, string Location)
        {
            try
            {
                DataTable dt = dgw.ExecuteInsertProc(mainCombType, StoreNo, WarehouseNo, Location);
                exeRes.Status = dt.Rows[0]["ReturnMsg"].Equals("OK");
                exeRes.Message = dt.Rows[0]["ReturnMsg"].ToString();
            }
            catch (Exception e)
            {
                exeRes.Message = e.Message;
                exeRes.Status = false;
            }
            return exeRes;
        }

        internal ExecutionResult UpdateData(string mainCombType, string StoreNo, string WarehouseNo, string Rename, string OriginalData, string Flag)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dgw.ExecuteUpdateProc(mainCombType, StoreNo, WarehouseNo, Rename, OriginalData, ClientUtils.UserPara1, Flag);
                if (dt.Rows[0]["ReturnMsg"].Equals("OK"))
                {
                    exeRes.Status = true;
                    exeRes.Message = "OK";
                }
                else
                {
                    exeRes.Message = dt.Rows[0]["ReturnMsg"].ToString();
                    exeRes.Status = false;
                }
            }
            catch (Exception e)
            {
                exeRes.Message = e.Message;
                exeRes.Status = false;
            }
            return exeRes;
        }
    }
}
