using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cWHConfig.Bean;
using Oracle.ManagedDataAccess.Client;
// 疑问：如果store失效掉 下属所挂的库区&库区下属所有的储位是否需要全部失效？
namespace cWHConfig.DataGatWay
{
    public class dataGateWay
    {
        internal DataSet GetDataForStore(baseinfo bean)
        {
            DataSet ds = new DataSet();
            string sql = @" SELECT a.store_no, 
                                        a.store_name,
                                        a.update_time
                                     FROM PPSUSER.T_Store a  where 1=1 ";
            if (bean.Enabled == 0)
                sql += " and a.Enabled = 'Y' ";
            else if (bean.Enabled == 1)
                sql += " and  a.Enabled = 'N' ";
            if (!string.IsNullOrEmpty(bean.CombFilter))
                sql += " and a." + bean.CombFilter + " like '%" + bean.FilterText + "%'";
            return ds = ClientUtils.ExecuteSQL(sql);
        }

        internal DataSet GetDataForLocation(baseinfo bean)
        {
            DataSet ds = new DataSet();
            string sql = @"  select
                                        b.location_no,
                                        b.location_name,
                                        b.update_time,
                                        b.location_type,
                                        a.warehouse_no
                                        from ppsuser.wms_location b ,PPSUSER.T_WAREHOUSE a 
                                        where a.warehouse_id=b.warehouse_id";
            if (bean.Enabled == 0)
                sql += " and b.Enabled = 'Y'";
            else if (bean.Enabled == 1)
                sql += " and b.Enabled = 'N'";
            if (!string.IsNullOrEmpty(bean.CombFilter))
                sql += " and " + bean.CombFilter + " like '%" + bean.FilterText + "%'";
            return ds = ClientUtils.ExecuteSQL(sql);
        }

        internal DataTable ExecuteInsertProc(string mainCombType, string storeNo, string warehouseNo, string location)
        {
            object[][] param = new object[6][];
            param[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "vType", mainCombType };
            param[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "vStoreNo", storeNo };
            param[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "vWarehouseNo", warehouseNo };
            param[3] = new object[] { ParameterDirection.Input, OracleType.VarChar, "vEmpid", ClientUtils.UserPara1 };
            param[4] = new object[] { ParameterDirection.Input, OracleType.VarChar, "vLocation", location };
            param[5] = new object[] { ParameterDirection.Output, OracleType.VarChar, "ReturnMsg", "" };
            string procedurename = @"ppsuser.SP_WAREHOUSE_CFG_INSERT";
            DataSet ds = ClientUtils.ExecuteProc(procedurename, param);
            return ds.Tables[0];
        }

        internal DataTable ExecuteUpdateProc(string mainCombType, string storeNo, string warehouseNo, string rename, string originalData, string userPara1, string Flag)
        {
            object[][] param = new object[8][];
            param[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "vType", mainCombType };
            param[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "vStoreNo", string.IsNullOrEmpty(storeNo) ? "" : storeNo };
            param[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "vWarehouseNo", string.IsNullOrEmpty(warehouseNo) ? "" : warehouseNo };
            param[3] = new object[] { ParameterDirection.Input, OracleType.VarChar, "vEmpid", string.IsNullOrEmpty(userPara1) ? "" : userPara1 };
            param[4] = new object[] { ParameterDirection.Input, OracleType.VarChar, "vRename", string.IsNullOrEmpty(rename) ? "" : rename };
            param[5] = new object[] { ParameterDirection.Input, OracleType.VarChar, "vOriginalData", string.IsNullOrEmpty(originalData) ? "" : originalData };
            param[6] = new object[] { ParameterDirection.Input, OracleType.VarChar, "vFlag", string.IsNullOrEmpty(Flag) ? "" : Flag };
            param[7] = new object[] { ParameterDirection.Output, OracleType.VarChar, "ReturnMsg", "" };
            string procedurename = @"ppsuser.SP_WAREHOUSE_CONFIG";
            DataSet ds = ClientUtils.ExecuteProc(procedurename, param);
            return ds.Tables[0];
        }

        internal DataSet GetWHbyStore(baseinfo bean)
        {
            DataSet ds = new DataSet();
            string sql = @"select
                                      b.warehouse_no,
                                      b.warehouse_name, 
                                      b.update_time,
                                      b.warehouse_type,
                                      c.store_no                                  
                                     from ppsuser.T_WAREHOUSE b ,ppsuser.t_store c
                                      where b.store_id =c.store_id and c.store_no='" + bean.sStoreNo + "'";
            if (bean.Enabled == 0)
                sql += " and b.Enabled = 'Y'";
            else if (bean.Enabled == 1)
                sql += " and b.Enabled = 'N'";
            return ds = ClientUtils.ExecuteSQL(sql);
        }
        internal DataSet GetDataForWarehouse(baseinfo bean)
        {
            DataSet ds = new DataSet();
            string sql = @"  select
                                      b.warehouse_no,
                                      b.warehouse_name, 
                                      b.update_time,
                                      b.warehouse_type,
                                      c.store_no                                  
                                     from ppsuser.T_WAREHOUSE b ,ppsuser.t_store c
                                      where b.store_id =c.store_id";
            if (bean.Enabled == 0)
                sql += " and b.Enabled = 'Y'";
            else if (bean.Enabled == 1)
                sql += " and b.Enabled = 'N'";
            if (!string.IsNullOrEmpty(bean.CombFilter))
                sql += " and " + bean.CombFilter + " like '%" + bean.FilterText + "%'";
            return ds = ClientUtils.ExecuteSQL(sql);
        }
    }
}
