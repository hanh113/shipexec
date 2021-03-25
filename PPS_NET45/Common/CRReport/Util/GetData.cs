using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace CRReport.CRfrom
{
    class GetData
    {
        private static string connString = "User ID=SHIPPINGDOC;Password=SHIPPINGDOC;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 10.12.72.55)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = dgedi)))";

        private static string ppsConnString = "User ID=ppstemspro;Password=sproppstem;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = ppsscan.luxshare.com.cn)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = ksppsa)))";

        public static DataTable getDatatable(string strSql)
        {
            DataTable action = new DataTable();
            OracleConnection conn = new OracleConnection(connString);
            OracleCommand cmd = new OracleCommand();
            try
            {
                conn.Open();
                OracleDataAdapter adapter = new OracleDataAdapter(strSql, conn);
                adapter.Fill(action);
                return action;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return action;
        }

        public static DataTable getPPSDatatable(string strSql)
        {
            DataTable action = new DataTable();
            OracleConnection conn = new OracleConnection(ppsConnString);
            OracleCommand cmd = new OracleCommand();
            try
            {
                conn.Open();
                OracleDataAdapter adapter = new OracleDataAdapter(strSql, conn);
                adapter.Fill(action);
                return action;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return action;
        }
    }
}
