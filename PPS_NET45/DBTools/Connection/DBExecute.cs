using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace DBTools.Connection
{
    public class DBExecute
    {
        private MESDataAdapter DataAdp;
        private IDbConnection iConn;

        public DBExecute(string connectionString)
        {
            iConn = (new DBConn(connectionString)).GetConnection();
            DataAdp = new MESDataAdapter();
        }
        #region dml

        public ExecutionResult ExecuteQueryDS(string sqlCommandText)
        {
            ExecutionResult result = new ExecutionResult();
            DataSet vDataSet = new DataSet();
            try
            {
                if (iConn.State != ConnectionState.Open)
                {
                    //开启连线
                    iConn.Open();
                }
                IDbCommand iCmd = iConn.CreateCommand();
                iCmd.CommandText = sqlCommandText;
                iCmd.CommandType = CommandType.Text;//cmdType;
                DataAdp.GetAdapater(iCmd).Fill(vDataSet);
                result.Status = true;
                result.Message = "OK";
                result.Anything = vDataSet;
            }
            catch (Exception exception)
            {
                result.Status = false;
                result.Message = exception.Message;
            }
            finally
            {
                if ((iConn != null) && (iConn.State != ConnectionState.Closed))
                {
                    iConn.Close();
                }
            }
            return result;
        }


        public ExecutionResult ExecuteQueryDSHt(string sqlCommandText, Hashtable oraParams)
        {
            ExecutionResult result = new ExecutionResult();
            DataSet vDataSet = new DataSet();
            try
            {
                if (iConn.State != ConnectionState.Open)
                {
                    //开启连线
                    iConn.Open();
                }
                IDbCommand iCmd = iConn.CreateCommand();
                iCmd.CommandText = sqlCommandText;
                iCmd.CommandType = CommandType.Text;//cmdType;
                iCmd = this.IDbCommandAddOracleParams(iCmd, oraParams);
                DataAdp.GetAdapater(iCmd).Fill(vDataSet);
                result.Status = true;
                result.Message = "OK";
                result.Anything = vDataSet;
            }
            catch (Exception exception)
            {
                result.Status = false;
                result.Message = exception.Message;
            }
            finally
            {
                if ((iConn != null) && (iConn.State != ConnectionState.Closed))
                {
                    iConn.Close();
                }
            }
            return result;
        }

        public int ExecuteQueryInt(string sqlCommandText)
        {
            int num2 = 0;
            int oracleNumber = 0;
            try
            {
                if (iConn.State != ConnectionState.Open)
                {
                    iConn.Open();
                }
                IDbCommand iCmd = iConn.CreateCommand();
                iCmd.CommandType = CommandType.Text;
                iCmd.CommandText = sqlCommandText;
                IDataReader reader = iCmd.ExecuteReader();
                if (reader.Read() && !reader.IsDBNull(0))
                {
                    oracleNumber = reader.GetInt32(0);
                }
                num2 = oracleNumber;
                reader.Dispose();
            }
            catch { }
            finally
            {
                if ((iConn != null) && (iConn.State != ConnectionState.Closed))
                {
                    iConn.Close();
                }
            }
            return num2;
        }

        public int ExecuteQueryInt(string sqlCommandText, List<OracleParameter> oraParams)
        {
            int num2 = 0;
            int oracleNumber = 0;
            try
            {
                if (iConn.State != ConnectionState.Open)
                {
                    iConn.Open();
                }
                IDbCommand command = iConn.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sqlCommandText;
                foreach (OracleParameter parameter in oraParams)
                {
                    command.Parameters.Add(parameter);
                }
                IDataReader reader = command.ExecuteReader();
                if (reader.Read() && !reader.IsDBNull(0))
                {
                    oracleNumber = reader.GetInt32(0);
                }
                num2 = oracleNumber;
                reader.Dispose();
            }
            catch
            {
            }
            finally
            {
                if ((iConn != null) && (iConn.State != ConnectionState.Closed))
                {
                    iConn.Close();
                }
            }
            return num2;
        }

        public int ExecuteQueryIntHt(string sqlCommandText, Hashtable oraParams)
        {
            int num2 = 0;
            int oracleNumber = 0;
            try
            {
                if (iConn.State != ConnectionState.Open)
                {
                    iConn.Open();
                }
                IDbCommand cmd = iConn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlCommandText;
                IDataReader reader = this.IDbCommandAddOracleParams(cmd, oraParams).ExecuteReader();
                if (reader.Read() && !reader.IsDBNull(0))
                {
                    //oracleNumber = (int) reader.GetOracleNumber(0);
                    oracleNumber = reader.GetInt32(0);
                }
                num2 = oracleNumber;
                reader.Dispose();
            }
            catch
            {
            }
            finally
            {
                if ((iConn != null) && (iConn.State != ConnectionState.Closed))
                {
                    iConn.Close();
                }
            }
            return num2;
        }

        public string ExecuteQueryStr(string sqlCommandText)
        {
            string oracleString = null;
            string str2 = null;
            try
            {
                if (iConn.State != ConnectionState.Open)
                {
                    iConn.Open();
                }
                IDbCommand command = iConn.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sqlCommandText;
                IDataReader reader = command.ExecuteReader();
                if (reader.Read() && !reader.IsDBNull(0))
                {
                    oracleString = (string)reader.GetString(0);
                }
                str2 = oracleString;
                reader.Dispose();
            }
            catch { }
            finally
            {
                if ((iConn != null) && (iConn.State != ConnectionState.Closed))
                {
                    iConn.Close();
                }
            }
            return str2;
        }

        public string ExecuteQueryStr(string sqlCommandText, List<OracleParameter> oraParams)
        {
            string oracleString = null;
            string str2 = null;
            try
            {
                if (iConn.State != ConnectionState.Open)
                {
                    iConn.Open();
                }
                IDbCommand command = iConn.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sqlCommandText;
                foreach (OracleParameter parameter in oraParams)
                {
                    command.Parameters.Add(parameter);
                }
                IDataReader reader = command.ExecuteReader();
                if (reader.Read() && !reader.IsDBNull(0))
                {
                    oracleString = (string)reader.GetString(0);
                }
                str2 = oracleString;
                reader.Dispose();
            }
            catch { }
            finally
            {
                if ((iConn != null) && (iConn.State != ConnectionState.Closed))
                {
                    iConn.Close();
                }
            }
            return str2;
        }

        public string ExecuteQueryStrHt(string sqlCommandText, Hashtable oraParams)
        {
            string str = null;
            string str2 = null;
            try
            {
                if (iConn.State != ConnectionState.Open)
                {
                    iConn.Open();
                }
                IDbCommand cmd = iConn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlCommandText;
                IDataReader reader = this.IDbCommandAddOracleParams(cmd, oraParams).ExecuteReader();
                if (reader.Read() && !reader.IsDBNull(0))
                {
                    str = reader.GetString(0).ToString();
                }
                str2 = str;
                reader.Dispose();
            }
            catch { }
            finally
            {
                if ((iConn != null) && (iConn.State != ConnectionState.Closed))
                {
                    iConn.Close();
                }
            }
            return str2;
        }

        public ExecutionResult ExecuteSP(string spName, List<OracleParameter> oraParams)
        {
            ExecutionResult result = new ExecutionResult();
            OracleParameter parameter = null;
            try
            {
                if (iConn.State != ConnectionState.Open)
                {
                    iConn.Open();
                }
                IDbCommand command = iConn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = spName;
                foreach (OracleParameter parameter2 in oraParams)
                {
                    command.Parameters.Add(parameter2);
                    if (parameter2.Direction.Equals(ParameterDirection.Output))
                    {
                        parameter = parameter2;
                    }
                }
                command.ExecuteNonQuery();
                if (parameter == null)
                {
                    result.Message = "OK";
                    result.Anything = "OK";
                }
                else
                {
                    result.Message = "OK";
                    result.Anything = parameter.Value.ToString();
                }
                object[] objArray = new object[oraParams.Count];
                int index = 0;
                foreach (OracleParameter parameter3 in command.Parameters)
                {
                    if (parameter3.OracleType.Equals(OracleType.Cursor))
                    {
                        OracleDataReader reader = (OracleDataReader)parameter3.Value;
                        DataTable objDataTable = new DataTable();
                        int intFieldCount = reader.FieldCount;
                        for (int intCounter = 0; intCounter < intFieldCount; ++intCounter)
                        {
                            objDataTable.Columns.Add(reader.GetName(intCounter), reader.GetFieldType(intCounter));
                        }

                        objDataTable.BeginLoadData();

                        object[] objValues = new object[intFieldCount];
                        while (reader.Read())
                        {
                            reader.GetValues(objValues);
                            objDataTable.LoadDataRow(objValues, true);
                        }
                        reader.Close();
                        objDataTable.EndLoadData();
                        objArray[index] = objDataTable;
                    }
                    else
                    {
                        objArray[index] = parameter3.Value;
                    }
                  //  objArray[index] = parameter3.Value;
                    index++;
                }
                result.Arges = objArray;
                result.Status = true;
            }
            catch (Exception exception)
            {
                result.Message = "DBTools:ExecuteSP," + exception.Message;
                result.Anything = "DBTools:ExecuteSP," + exception.Message;
                result.Status = false;
            }
            finally
            {
                if ((iConn != null) && (iConn.State != ConnectionState.Closed))
                {
                    iConn.Close();
                }
            }
            return result;
        }

        public ExecutionResult ExecuteUpdate(string sqlCommandText, List<OracleParameter> oraParams)
        {
            ExecutionResult result = new ExecutionResult();
            try
            {
                if (iConn.State != ConnectionState.Open)
                {
                    iConn.Open();
                }
                IDbCommand command = iConn.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sqlCommandText;
                foreach (OracleParameter parameter in oraParams)
                {
                    command.Parameters.Add(parameter);
                }
                command.ExecuteNonQuery();
                result.Status = true;
                result.Message = "OK";
            }
            catch (Exception exception)
            {
                result.Status = false;
                result.Message = exception.Message;
            }
            finally
            {
                if ((iConn != null) && (iConn.State != ConnectionState.Closed))
                {
                    iConn.Close();
                }
            }
            return result;
        }

        public ExecutionResult ExecuteUpdateHt(string sqlCommandText, Hashtable oraParams)
        {
            ExecutionResult result = new ExecutionResult();
            try
            {
                if (iConn.State != ConnectionState.Open)
                {
                    iConn.Open();
                }
                IDbCommand cmd = iConn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlCommandText;

                this.IDbCommandAddOracleParams(cmd, oraParams).ExecuteNonQuery();
                result.Status = true;
                result.Message = "OK";
            }
            catch (Exception exception)
            {
                result.Status = false;
                result.Message = exception.Message;
            }
            finally
            {
                if ((iConn != null) && (iConn.State != ConnectionState.Closed))
                {
                    iConn.Close();
                }
            }
            return result;
        }

        private IDbCommand IDbCommandAddOracleParams(IDbCommand cmd, Hashtable htParams)
        {
            OracleParameter parameter = null;
            foreach (DictionaryEntry entry in htParams)
            {
                parameter = new OracleParameter
                {
                    ParameterName = entry.Key.ToString()
                };
                parameter = new OracleParameter();
                parameter.ParameterName = entry.Key.ToString();
                switch (entry.Value.GetType().Name)
                {
                    case "Double":
                        parameter.OracleType = OracleType.Double;
                        break;
                    case "Int32":
                        parameter.OracleType = OracleType.Int32;
                        break;
                    case "Decimal":
                        parameter.OracleType = OracleType.Number;
                        break;
                    case "String":
                        parameter.OracleType = OracleType.VarChar;
                        break;
                    case "DateTime":
                        parameter.OracleType = OracleType.DateTime;
                        break;
                    case "Object":
                        parameter.OracleType = OracleType.Clob;
                        break;
                }
                parameter.Value = entry.Value;
                cmd.Parameters.Add(parameter);
            }
            return cmd;
        }

        #endregion
    }
}
