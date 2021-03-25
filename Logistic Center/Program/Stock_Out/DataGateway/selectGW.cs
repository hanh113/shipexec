using ClientUtilsDll;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Out.DataGateway
{
    class selectGW
    {
        ExecutionResult exeRes;
        internal ExecutionResult GetSnInfo(string strSN)
        {
            exeRes = new ExecutionResult();
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varSN", strSN };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varEmp", ClientUtils.fLoginUser };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "varRes", "" };
            exeRes.Anything = ClientUtils.ExecuteProc("PPSUSER.SP_WMS_STOCKOUT", procParams);
            return exeRes;
        }

        internal object RefreshSnInfo(string strSN)
        {
            string sql = @"select   customer_sn,
                                              MO_NO,
                                               carton_no,
                                               pallet_no,
                                               part_no,
                                               location_no,
                                              case station_name when '0' then 'STOCK_OUT' else station_name end as STATION_NAME 
                                          from mesuser.t_sn_status
                                         where carton_no =:varSN
                                        union
                                        select 
                                               customer_sn,
                                                MO_NO,
                                               carton_no,
                                               pallet_no,
                                               part_no,
                                               location_no,
                                               case station_name when '0' then 'STOCK_OUT' else station_name end as STATION_NAME
                                          from mesuser.t_sn_status
                                         where pallet_no =:varSN";
            object[][] sqlParams = new object[1][];
            sqlParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varSN", strSN };
            DataTable dt = ClientUtils.ExecuteSQL(sql, sqlParams).Tables[0];
            return dt;
        }

        internal DataTable GetLocationInfo(string strLocation)
        {
            string sql = @"
            SELECT tl.pallet_no, tl.part_no, vmi.MPN, sum(tl.qty) AS  PER_QTY
              FROM PPSUSER.T_LOCATION TL, pptest.vw_mpn_info vmi
             WHERE tl.part_no = vmi.ICTPARTNO
              AND TL.Location_no=:Location_no
             GROUP BY tl.pallet_no, tl.part_no, vmi.MPN ";
            object[][] sqlParams = new object[1][];
            sqlParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Location_no", strLocation };
            DataTable dt = ClientUtils.ExecuteSQL(sql, sqlParams).Tables[0];
            return dt;
        }
    }
}
