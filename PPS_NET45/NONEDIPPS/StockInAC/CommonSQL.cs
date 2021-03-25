using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.Threading;
using System.Net;
using System.Net.Mail;

namespace StockInAC
{
    public class CommonSQL
    {
        public DataTable GetLocationInfo(string strLocationNo)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "LOCATION_NO", strLocationNo };
            return ClientUtils.ExecuteSQL(@"SELECT A.LOCATION_ID,A.LOCATION_NO FROM SAJET.WMS_LOCATION A WHERE A.ENABLED='Y' AND A.LOCATION_NO=:LOCATION_NO ", sqlparams).Tables[0];
        }

        public DataTable StockInByPallet(string strLocationID, string strPallet_no)
        {
            object[][] procParams = new object[6][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varPalletNo", strPallet_no };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varCartonNo", "NA" };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varLocationid", strLocationID };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Int32, "varEMPid", 10086 };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Int32, "varRetCode", 0 };
            procParams[5] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "varRetMsg", "" };
            return ClientUtils.ExecuteProc("NONEDIPPS.T_WMS_IN_BYPALLET_V4", procParams).Tables[0];
        }

        public DataTable StockInByCartonAndPart(string strLocationID, string strLocationNo, string strCartonNo, string strPartNo)
        {
            object[][] procParams = new object[7][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varLocationid", strLocationID };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varLocationno", strLocationNo };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varCartonNo", strCartonNo };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varPartNo", strPartNo };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Int32, "varEMPid", 20086 };
            procParams[5] = new object[] { ParameterDirection.Output, OracleDbType.Int32, "varRetCode", 0 };
            procParams[6] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "varRetMsg", "" };
            return ClientUtils.ExecuteProc("NONEDIPPS.T_WMS_IN_BY_CARTON_V4", procParams).Tables[0];
        }
    }
}
