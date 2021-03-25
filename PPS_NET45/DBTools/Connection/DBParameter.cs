using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace DBTools.Connection
{
    public class DBParameter
    {
        private List<OracleParameter> sqlParams = new List<OracleParameter>();

        public void Add(string ParaName, OracleType DataType)
        {
            OracleParameter item = new OracleParameter
            {
                ParameterName = ParaName,
                OracleType = DataType,
                Size = 200,
                Direction = ParameterDirection.Output,
            };
            this.sqlParams.Add(item);
        }

        public void Add(string ParaName, OracleType DataType, Object ParaValue)
        {
            OracleParameter item = new OracleParameter
            {
                ParameterName = ParaName,
                OracleType = DataType,
                Value = ParaValue
            };
            this.sqlParams.Add(item);
        }

        public void Clear()
        {
            this.sqlParams.Clear();
        }

        public List<OracleParameter> GetParameters()
        {
            return this.sqlParams;
        }
    }


}
