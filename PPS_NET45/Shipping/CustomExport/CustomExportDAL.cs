using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace CustomExport
{
    class CustomExportDAL
    {
        public string CheckShipmentWeightStatusSP(string insn, out string strregion, out string errmsg)
        {
            //insn 是PACKPALLETNO
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "packpalletno", insn };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outregion", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_WEIGHT_CHECKSHIPMENTSTATUS2", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            strregion = ds.Tables[0].Rows[0]["outregion"].ToString();
            return errmsg;


        }
    }
}
