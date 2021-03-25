using Oracle.ManagedDataAccess.Client;
using Check.Beans;
using Check.Beans;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check.DataGateway
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
            string sql = @"select distinct station_name from mesuser.t_station_config  where line_name =:LineName and station_name like 'CHECK%'";
            object[][] sqlParams = new object[1][];
            sqlParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "LineName", strLine };
            DataTable dt = ClientUtils.ExecuteSQL(sql, sqlParams).Tables[0];
            return dt;
        }

        internal DataSet GetMoInfo(string strMo)
        {
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varMo", strMo };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varStation", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "varRes", "" };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "varKeyPart", "" };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.RefCursor, "curBom", null };
            return ClientUtils.ExecuteProc("PPSUSER.SP_CHECK_MO", procParams);
        }

        internal DataTable ExecuteByPallet(string strPallet, string strFlag)
        {
            object[][] procParams = new object[4][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varPallet", strPallet };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varCarton", "N/A" };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varFlag", strFlag };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "varRes", "" };
            return ClientUtils.ExecuteProc("PPSUSER.SP_ExecuteCloseOpen", procParams).Tables[0];
        }

        internal DataTable CheckPalletNo(string strPallet, string strMo)
        {
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varMo", strMo };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varPallet", strPallet };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varStation", SetupInfo.strStation };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "varRes", "" };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "varPalletQty", "" };
            return ClientUtils.ExecuteProc("PPSUSER.SP_Check_PalletNo", procParams).Tables[0];
        }

        internal DataTable GetPrintPalletBySN(string strPallet)
        {
            string sql = @"select pallet_no, part_no, count(*) as qty
                                          from mesuser.t_sn_status
                                         where pallet_no =:varSN
                                         group by pallet_no, part_no
                                        union
                                        select pallet_no, part_no, count(*) as qty
                                          from mesuser.t_sn_status
                                         where carton_no =:varSN
                                         group by pallet_no, part_no
                                        union
                                        select pallet_no, part_no, count(*) as qty
                                          from mesuser.t_sn_status
                                         where customer_sn =:varSN
                                         group by pallet_no, part_no";
            object[][] Params = new object[1][];
            Params[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varSN", strPallet };
            return ClientUtils.ExecuteSQL(sql, Params).Tables[0];
        }

        internal DataSet GetCartonInfo(baseinfo bean)
        {
            object[][] procParams = new object[6][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varMo", bean.strMo };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varStation", SetupInfo.strStation };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varSN", bean.strCarton };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Double, "numSTDWeight", 0 };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "varRes", "" };
            procParams[5] = new object[] { ParameterDirection.Output, OracleDbType.RefCursor, "curSNList", "" };
            return ClientUtils.ExecuteProc("PPSUSER.SP_GetWeightCarton", procParams);
        }

        internal DataTable PassStation(baseinfo bean)
        {
            object[][] procParams = new object[7][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varMo", bean.strMo };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varStation", SetupInfo.strStation };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varSN", bean.strCarton };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varLine", SetupInfo.strLine };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varEmp", SetupInfo.strUser };
            procParams[5] = new object[] { ParameterDirection.Input, OracleDbType.Double, "numRealWeight", bean.dblRealWeight };
            procParams[6] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "varRes", "" };
            return ClientUtils.ExecuteProc("PPSUSER.SP_WeightPassStationByCarton", procParams).Tables[0];
        }
    }
}
