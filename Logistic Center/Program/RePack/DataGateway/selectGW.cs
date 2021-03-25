using Oracle.ManagedDataAccess.Client;
using Repack.Beans;
using RePack.Beans;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RePack.DataGateway
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
            string sql = @"select distinct station_name from mesuser.t_station_config  where line_name =:LineName and station_name like 'REPACK%'";
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

        internal DataSet RepackBySN(baseinfo bean)
        {
            object[][] procParams = new object[11][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varMo", bean.strMo };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "bAutoFlag", bean.bAutoFlag.ToString().ToUpper() };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varPalletNo", bean.strPallet };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varStation", SetupInfo.strStation };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varLine", SetupInfo.strLine };
            procParams[5] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varEmp", SetupInfo.strUser };
            procParams[6] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varSN", bean.strSN };
            procParams[7] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "varRes", "" };
            procParams[8] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "varPalletTemp", "" };
            procParams[9] = new object[] { ParameterDirection.Output, OracleDbType.RefCursor, "curDetail", "" };
            procParams[10] = new object[] { ParameterDirection.Output, OracleDbType.RefCursor, "curCartonInfo", "" };
            return ClientUtils.ExecuteProc("PPSUSER.SP_REPACK_SN", procParams);
        }

        internal DataTable GetCartonLabelInfo(string strCarton)
        {
            string sql = @"select distinct a.upc_code, a.custpart, a.custpartmodel, b.carton_no
                                      from pptest.oms_partmapping a, mesuser.t_sn_status b
                                     where a.part = b.part_no
                                       and b.carton_no in
                                           (select carton_no
                                              from mesuser.t_sn_status
                                             where customer_sn =:varCarton
                                            union
                                            select carton_no
                                              from mesuser.t_sn_status
                                             where carton_no =:varCarton)";
            object[][] sqlParams = new object[1][];
            sqlParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varCarton", strCarton };
            return ClientUtils.ExecuteSQL(sql, sqlParams).Tables[0];
        }
    }
}
