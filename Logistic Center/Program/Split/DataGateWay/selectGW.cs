using Oracle.ManagedDataAccess.Client;
using Split.Beans;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Split.DataGateWay
{
    class selectGW
    {
        internal DataTable QueryAllLine()
        {
            string sql = @"select  distinct Line_Name from mesuser.t_line_info";
            DataTable dt = ClientUtils.ExecuteSQL(sql).Tables[0];
            return dt;
        }

        internal DataTable QueryStationByLine(string strLine)
        {
            string sql = @"select distinct station_name from mesuser.t_station_config  where line_name =:LineName and station_name like 'SPLIT%'";
            object[][] sqlParams = new object[1][];
            sqlParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "LineName", strLine };
            DataTable dt = ClientUtils.ExecuteSQL(sql, sqlParams).Tables[0];
            return dt;
        }

        internal DataSet GetMoInfo(string strMo)
        {
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varMo", strMo };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varStation", SetupInfo.strStation };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "varRes", "" };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "varKeyPart", "" };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.RefCursor, "curBom", null };
            return ClientUtils.ExecuteProc("PPSUSER.SP_CHECK_MO", procParams);
        }

        internal DataSet ExecuteSP(baseinfo bean)
        {
            object[][] procParams = new object[8][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varMo", bean.strMo };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varStation", SetupInfo.strStation };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varLine", SetupInfo.strLine };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varEmp", SetupInfo.strUser };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varSN", bean.strSN };
            procParams[5] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "varRes", "" };
            procParams[6] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "varSplitQty", "" };
            procParams[7] = new object[] { ParameterDirection.Output, OracleDbType.RefCursor, "curDetail", "" };
            return ClientUtils.ExecuteProc("PPSUSER.SP_SPLIT_SN", procParams);
        }
    }
}
