using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace RollbackDN.UPSCancel
{
    public class ICTConnectionDB
    {
        public string IctUrlFromDB()
        {
            string result = "";
            string sql = @"select para_value from t_basicparameter_info where para_type='ICTSerivce_URL'";
            DataTable dt = new DataTable();
            dt = ClientUtils.ExecuteSQL(sql).Tables[0];
            if(dt.Rows.Count<1)
            {
                result= "NG-No data found for ICT server service URL";
                throw new Exception(result);
            }
            if(dt.Rows.Count>1)
            {
                result= "NG-Find many URL for ICT service";
                throw new Exception(result);
            }
            else
            {
                result = dt.Rows[0]["para_value"].ToString();
            }
            return result;
        }
       
    }
}

