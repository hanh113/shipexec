using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;

namespace UpLoad856AC
{
    class UploadDAL
    {
        public string PPSInsertWorkLogByProcedure(string insn, string inwc, string macaddress, out string errmsg)
        {
            object[][] procParams = new object[4][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", insn };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inwc", inwc };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "macaddress", macaddress };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_PPS_INSERTWORKLOG", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }

        
        public string PPSInsertWebServieByProcedure(string insn, string serverip,string  url,string result, out string errmsg)
        {
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", insn };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "strserverip", serverip };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "strurl", url };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "strresult", result };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_UPLOAD_INSERTWEBSERVICELOG", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }

        public string PPSCheckWebServieByProcedure(string insn, out string tturl, out string errmsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", insn };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "tturl", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_UPLOAD_CHECKWEBSERVICELOG", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            tturl = ds.Tables[0].Rows[0]["tturl"].ToString();
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }
    }
}
