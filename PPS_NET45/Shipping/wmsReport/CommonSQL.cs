using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wmsReport
{
    //copy from KS source by wenxing
    public class CommonSQL
    {
        public DataTable GetLocationInfo(string strLocationNo)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "LOCATION_NO", strLocationNo };
            return ClientUtils.ExecuteSQL(@"SELECT A.LOCATION_ID,A.LOCATION_NO FROM SAJET.WMS_LOCATION A WHERE A.ENABLED='Y' AND A.LOCATION_NO=:LOCATION_NO ", sqlparams).Tables[0];
        }

        public DataTable GetTLocation(string strLocationNo)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "LOCATION_NO", strLocationNo };
            return ClientUtils.ExecuteSQL(@"SELECT a.LOCATION_NO FROM ppsuser.T_LOCATION a WHERE a.LOCATION_NO=:LOCATION_NO ", sqlparams).Tables[0];
        }

        public DataTable GetPPSStockInfo(string strTroNo)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TROLLEY_NO", strTroNo };
            return ClientUtils.ExecuteSQL(@"SELECT a.TROLLEY_NO FROM ppsuser.T_TROLLEY_SN_STATUS a WHERE a.TROLLEY_NO=:TROLLEY_NO ", sqlparams).Tables[0];
        }

        public DataTable GetMESStockInfo(string strTroNo)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TROLLEY_NO", strTroNo };
            return ClientUtils.ExecuteSQL(@"SELECT a.PALLET_NO FROM sajet.g_TROLLEY_SN_STATUS a WHERE a.TROLLEY_NO=:TROLLEY_NO ", sqlparams).Tables[0];
        }

        public string TroStockInLocation(string strPalletNo, string strLocationID)
        {
            object[][] procParams = new object[6][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "varPalletNo", strPalletNo };
            procParams[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "varCartonNo", "NA" };
            procParams[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "varLocationid", strLocationID };
            procParams[3] = new object[] { ParameterDirection.Input, OracleType.Int32, "varEMPid", 10086 };
            procParams[4] = new object[] { ParameterDirection.Output, OracleType.Int32, "varRetCode", 0 };
            procParams[5] = new object[] { ParameterDirection.Output, OracleType.VarChar, "varRetMsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.T_WMS_IN_V4", procParams);
            if (ds.Tables[0].Rows[0]["varRetCode"].ToString().Trim() == "0")
            {
                return "OK";
            }
            else
            {
                return "NG:" + ds.Tables[0].Rows[0]["varRetMsg"].ToString().Trim();
            }
        }

        public string WMSOutByCarton(string strCartonNo)
        {
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "varPalletNo", "NA" };
            procParams[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "varCartonNo", strCartonNo };
            procParams[2] = new object[] { ParameterDirection.Input, OracleType.Int32, "varEMPid", 10086 };
            procParams[3] = new object[] { ParameterDirection.Output, OracleType.Int32, "varRetCode", 0 };
            procParams[4] = new object[] { ParameterDirection.Output, OracleType.VarChar, "varRetMsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.T_WMS_OUT_BAK", procParams);
            if (ds.Tables[0].Rows[0]["varRetCode"].ToString().Trim() == "0")
            {
                return "OK";
            }
            else
            {
                return "NG:" + ds.Tables[0].Rows[0]["varRetMsg"].ToString().Trim();
            }
        }

        public DataTable GetPPSCartonInfo(string strCartonNo)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "CARTON_NO", strCartonNo };
            return ClientUtils.ExecuteSQL(@"SELECT distinct a.CARTON_NO FROM ppsuser.T_SN_STATUS a WHERE a.CARTON_NO=:CARTON_NO ", sqlparams).Tables[0];
        }

        public DataTable GetMESCartonInfo(string strCartonNo)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "CARTON_NO", strCartonNo };
            return ClientUtils.ExecuteSQL(@"SELECT distinct a.CARTON_NO FROM sajet.g_SN_STATUS a WHERE a.CARTON_NO=:CARTON_NO ", sqlparams).Tables[0];
        }

        public string CartonStockInLocation(string strCartonNo, string strLocationID)
        {
            object[][] procParams = new object[6][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "varPalletNo", "NA" };
            procParams[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "varCartonNo", strCartonNo };
            procParams[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "varLocationid", strLocationID };
            procParams[3] = new object[] { ParameterDirection.Input, OracleType.Int32, "varEMPid", 10086 };
            procParams[4] = new object[] { ParameterDirection.Output, OracleType.Int32, "varRetCode", 0 };
            procParams[5] = new object[] { ParameterDirection.Output, OracleType.VarChar, "varRetMsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.T_WMS_IN_V4", procParams);
            if (ds.Tables[0].Rows[0]["varRetCode"].ToString().Trim() == "0")
            {
                return "OK";
            }
            else
            {
                return "NG:" + ds.Tables[0].Rows[0]["varRetMsg"].ToString().Trim();
            }
        }
    }
}
