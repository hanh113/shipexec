using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_In.DataGatway
{

    class selectGW
    {
        internal DataTable GetWHInfo() => ClientUtils.ExecuteSQL("select  warehouse_id,warehouse_no from sajet.wms_warehouse where enabled='Y'").Tables[0];

        internal DataTable GetLocationInfo(string strWHID) =>
            ClientUtils.ExecuteSQL(string.Format("select location_id,location_no from sajet.wms_location where enabled='Y' and warehouse_id='{0}'", strWHID)).Tables[0];

        internal DataSet ExecuteStockIn(string strLocation, string strPallet)
        {
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varSN", strPallet };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varLocation", strLocation };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varEmp", ClientUtils.fLoginUser };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "varRes", "" };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.RefCursor, "curPalletInfo", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_WMS_STOCKIN", procParams);
            return ds;
        }

        internal DataTable GetPalletInfo(string strPallet) => ClientUtils.ExecuteSQL(
             @"select serial_number,
           customer_sn,
           carton_no,
           pallet_no,
           part_no,
          '' as  location_no
      from mesuser.t_sn_status
     where pallet_no =:varSN", new object[1][] { new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varSN", strPallet } }).Tables[0];
    }
}
