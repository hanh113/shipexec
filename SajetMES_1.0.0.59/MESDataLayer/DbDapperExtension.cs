using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniDb;

namespace MESDataLayer
{
    public interface IDbOperation
    {
        IEnumerable<T> Query<T>(string sql, object model);
        int Add<T>(T model);
        int Add<T>(IEnumerable<T> model);
        int InsertUpdate<T>(T model, List<string> keyfields);
        int InsertUpdate<T>(IEnumerable<T> model, List<string> keyfields);
        int DoTransaction(Dictionary<string, object> data);
        int DoTransaction(string sql, object data);
    }

    public class DbDapperExtension : OracleDb, IDbOperation
    {
        public DbDapperExtension(string conn)
            : base(conn)
        {
        }

        public IEnumerable<T> Query<T>(string sql, object model = null)
        {
            try
            {
                DbCon.Open();
                return DbCon.Query<T>(sql, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DbCon.Close();
            }
        }

        public int Add<T>(T model)
        {
            string sql = generateInsertSQL<T>();
            return Add<T>(new List<T> { model });
        }
        public int Add<T>(IEnumerable<T> model)
        {
            int i = 0;
            string sql = generateInsertSQL<T>();
            try
            {
                DbCon.Open();
                using (var tra = DbCon.BeginTransaction())
                {
                    try
                    {
                        i = DbCon.Execute(sql, model, tra);
                        tra.Commit();
                    }
                    catch (Exception ex)
                    {
                        tra.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DbCon.Close();
            }
            return i;
        }


        /// <summary>
        /// Dictionary<string,object>  sql,param
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public int DoTransaction(Dictionary<string, object> data)
        {
            int i = 0;
            try
            {
                DbCon.Open();
                using (var tra = DbCon.BeginTransaction())
                {
                    try
                    {
                        foreach (KeyValuePair<string, object> item in data)
                        {
                            if (isList(item.Value))
                            {
                                foreach (var it in (item.Value as IList))
                                {
                                    i = DbCon.Execute(item.Key, it, tra);
                                }
                            }
                            else
                            {
                                i = DbCon.Execute(item.Key, item.Value, tra);
                            }
                        }
                        tra.Commit();
                    }
                    catch (Exception ex)
                    {
                        tra.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DbCon.Close();
            }
            return i;
        }

        public int DoTransaction(string sql, object data)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add(sql, data);
            return DoTransaction(dict);
        }

        public int InsertUpdate<T>(T model, List<string> keyfields = null)
        {
            string sql = generateInsertUpdate<T>(keyfields);
            return DoTransaction(sql, model);
        }
        public int InsertUpdate<T>(IEnumerable<T> model, List<string> keyfields = null)
        {
            string sql = generateInsertUpdate<T>(keyfields);
            return DoTransaction(sql, model);
        }



        private string generateInsertSQL<T>()
        {
            string table = getTableName<T>();
            var properties = getProperties<T>();
            string sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                table, string.Join(",", properties),
                string.Join(",", properties.Select(x => ":" + x)));
            return sql;
        }
        private string generateInsertUpdate<T>(List<string> keyfields)
        {
            string tablename = getTableName<T>();
            var pks = keyfields == null || keyfields.Count() < 1 ?
                this.GetTablePK(tablename).Tables[0].AsEnumerable().Select(x => x[0].ToString()).ToList() : keyfields;
            var fileds = getProperties<T>();

            var upfields = fileds.Except(pks, StringComparer.CurrentCultureIgnoreCase);

            string restrict = string.Join("AND", pks.Select(x => string.Format("{0} =: {0}", x)));
            string update = "update set " + string.Join(" ,", upfields.Select(x => string.Format("{0} =: {0}", x)));
            string insert = string.Format("insert ({0}) values ({1})",
                  string.Join(" ,", fileds),
                    string.Join(" ,", fileds.Select(x => ":" + x))
                );


            string sql = string.Format(@"
                     Merge into {0} a using dual on ({1})
                     when matched then {2}
                     when not matched then {3}
                    ", tablename, restrict, update, insert);

            return sql;

        }

        private string getTableName<T>()
        {
            //Type type = typeof(T);
            //var tableNameAtt = type.GetCustomAttributes(typeof(TableNameAttribute), false);
            //if (tableNameAtt != null && tableNameAtt.Length > 0)
            //{
            //    return ((TableNameAttribute)tableNameAtt[0]).Value;
            //}
            return "";
        }

        private IEnumerable<string> getProperties(Type t)
        {
            var properties = t.GetProperties().Where(x => !x.Name.StartsWith("_") && !x.Name.StartsWith("x_"));
            return properties.Select(x => x.Name);
        }
        private IEnumerable<string> getProperties<T>()
        {
            var properties = typeof(T).GetProperties().Where(x => !x.Name.StartsWith("_") && !x.Name.StartsWith("x_"));
            return properties.Select(x => x.Name);
        }

        private bool isList(object o)
        {
            if (o == null) return false;
            return o is IList &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }
    }
}
