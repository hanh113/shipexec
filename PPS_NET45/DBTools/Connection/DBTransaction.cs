using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace DBTools.Connection
{
    public class DBTransaction
    {
        private MESDataAdapter DataAdp;
        private IDbConnection iConn;
        private IDbTransaction iTrans;

        public DBTransaction(string connectionString)
        {
            iConn = (new DBConn(connectionString)).GetConnection();
            DataAdp = new MESDataAdapter();
        }

        #region Transaction
        public void BeginTransaction()
        {
            if (iConn.State != ConnectionState.Open)
            {
                //开启连线
                iConn.Open();
            }
            this.iTrans = iConn.BeginTransaction();
        }

        public void Commit()
        {
            this.iTrans.Commit();
        }
        public void Rollback()
        {
            this.iTrans.Rollback();
        }

        public void EndTransaction()
        {
            if (this.iTrans != null)
            {
                //释放资源
                this.iTrans.Dispose();
            }
            if ((this.iConn != null) && (this.iConn.State != ConnectionState.Closed))
            {
                //断开连线
                this.iConn.Close();
            }
        }

        public ExecutionResult ExecuteQueryDS(string sqlCommandText, List<OracleParameter> sqlParams)
        {
            ExecutionResult result = new ExecutionResult();
            DataSet ds = new DataSet();
            try
            {
                IDbCommand iCmd = iConn.CreateCommand();
                iCmd.CommandText = sqlCommandText;
                iCmd.Transaction = this.iTrans;
                iCmd.CommandType = CommandType.Text;//cmdType;
                foreach (OracleParameter parameter in sqlParams)
                {
                    iCmd.Parameters.Add(parameter);
                }
                DataAdp.GetAdapater(iCmd).Fill(ds);
                iCmd.Parameters.Clear();
                result.Status = true;
                result.Message = "OK";
                result.Anything = ds;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }
            return result;





        }

        public ExecutionResult ExecuteUpdate(string sqlCommandText, List<OracleParameter> sqlParams)
        {
            ExecutionResult result = new ExecutionResult();

            try
            {
                IDbCommand iCmd = iConn.CreateCommand();
                iCmd.CommandText = sqlCommandText;
                iCmd.CommandType = CommandType.Text;//cmdType;
                iCmd.Transaction = this.iTrans;
                foreach (OracleParameter parameter in sqlParams)
                {
                    iCmd.Parameters.Add(parameter);
                }
                iCmd.ExecuteNonQuery();//
                result.Status = true;
                result.Message = "OK";
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public ExecutionResult ExecuteSP(string spName, List<OracleParameter> sqlParams)
        {
            OracleParameter sqlparameter = null;
            ExecutionResult result = new ExecutionResult();
            try
            {
                //创建控制对象
                IDbCommand iCmd = iConn.CreateCommand();
                //定义控制类型，存储过程
                iCmd.CommandType = CommandType.StoredProcedure;
                //定义命令名称，存储过程名称
                iCmd.CommandText = spName;
                //连接事务处理
                iCmd.Transaction = this.iTrans;
                foreach (OracleParameter sqlpara in sqlParams)
                {
                    //循环将参数列中值赋予到参数中
                    iCmd.Parameters.Add(sqlpara);
                    if (sqlpara.Direction.Equals(ParameterDirection.Output))
                    {
                        sqlparameter = sqlpara;
                    }
                }
                //执行命令
                iCmd.ExecuteNonQuery();
                if (sqlparameter == null)
                {
                    result.Anything = "OK";
                    result.Message = "OK";
                }
                else
                {
                    result.Message = "OK";
                    result.Anything = sqlparameter.Value.ToString();
                }
                //循环将参数列赋值到result.arges中    
                object[] objArray = new object[sqlParams.Count];
                int index = 0;
                foreach (OracleParameter parameter3 in iCmd.Parameters)
                {
                    objArray[index] = parameter3.Value;
                    index++;
                }
                result.Arges = objArray;
                result.Status = true;
            }
            catch (Exception ex)
            {
                //捕获异常并返回报错信息
                result.Message = "DBTools:ExecuteSP," + ex.Message;
                result.Anything = "DBTools:ExecuteSP," + ex.Message;
                result.Status = false;
            }
            return result;

        }
        #endregion


    }
}
