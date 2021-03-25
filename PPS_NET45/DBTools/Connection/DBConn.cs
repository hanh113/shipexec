using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace DBTools.Connection
{
    public class DBConn
    {
        public string dbConnectionString;
        public DBConn(string ConnectionString)
        {
            dbConnectionString = ConnectionString;
        }

        public IDbConnection GetConnection()
        {
            return new OracleConnection(this.dbConnectionString);
        }
        public IDbCommand GetCommand()
        {
            return new OracleCommand();
        }
        public IDbCommand GetCommand(string Sql, IDbConnection iConn)
        {
            return new OracleCommand(Sql, (OracleConnection)iConn);
        }

    }

    public class MESDataAdapter
    {
        public IDataAdapter GetAdapater(string Sql, IDbConnection iConn)
        {
            return new OracleDataAdapter(Sql, (OracleConnection)iConn);
        }
        public IDataAdapter GetAdapater()
        {
            return new OracleDataAdapter();
        }
        public IDataAdapter GetAdapater(IDbCommand iCmd)
        {
            return new OracleDataAdapter((OracleCommand)iCmd);
        }
    }
}
