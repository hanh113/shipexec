using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIP.DataGateway
{
    class selectGW
    {
        internal DataTable GetMoByDate(DateTime dtStart, DateTime dtEnd) => ClientUtils.ExecuteSQL(@"selecT mo_no,
       mo_type,
       case mo_flag
         when 0 then
          'Not Online'
         when 1 then
          'Open'
         when 2 then
          'Open'
         when 3 then
          'Close'
         else
          'NG'
       end as mo_flag,
       target_qty,
       input_qty,
       output_qty,
       keypart,
       route_name,
       create_time,
       carton_rule,
       pallet_rule
  from mesuser.t_mo_basic_info
 where create_time between
       :dtStart and
       :dtEnd ", new object[2][] { new object[] { ParameterDirection.Input, OracleDbType.Date, "dtStart", dtStart }, new object[] { ParameterDirection.Input, OracleDbType.Date, "dtEnd", dtEnd } }).Tables[0];

        internal DataTable GetBomByMo(string strMo) => ClientUtils.ExecuteSQL(@"
select  mo_no,key_part_no,part_desc,item_used_qty,cust_part_no,used_station,req_qty,kp_seq
 from mesuser.t_bom_keypart where mo_no=:varMo", new object[1][] { new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varMo", strMo } }).Tables[0];

        internal DataSet GetSnInfo(string strScanCode)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varScanCode", strScanCode };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "varRes", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.RefCursor, "curSNInfo", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_GetSNReport", procParams);
            return ds;
        }
    }
}
