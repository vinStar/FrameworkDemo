using HyBy.FrameWork.DAService;
using HyBy.Trading.BusinessEntity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HyBy.Trading.BusinessImplement
{
    public class ExCommonDBO : DBO
    {
        enum DatabaseType
        {
            SqlServer,
            Oracle
        }

        class SearchOption
        {
            public string SearchID { get; set; }

            public DatabaseType DbType { get; set; }

            public CommandType DbCommandType { get; set; }

            public string DbIdentify
            {
                get
                {
                    if (DbType == DatabaseType.SqlServer)
                    {
                        return "@";
                    }
                    else if (DbType == DatabaseType.Oracle)
                    {
                        return ":";
                    }
                    return "";
                }
            }
        }

        SearchOption option = null;
        DbParameter[] parameters = null;
        DbParameter outparam = null;

        /// <summary>
        /// 如果未传入数据连接及事务控制对象，则使用默认的
        /// </summary>
        public ExCommonDBO()
        {
        }

        /// <summary>
        /// 根据传入的数据连接及事务控件对象启用事务控制
        /// </summary>
        /// <param name="dac">数据连接入事务控制对象</param>
        public ExCommonDBO(DataAccess dac)
            : this()
        {
            this.dac = dac;
        }

        #region 私有方法，本类中调用

        #region 转换数据集到实体对象/集合

        /// <summary>
        /// 转换数据集到集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="reader">数据集</param>
        /// <returns></returns>
        List<T> CastToList<T>(IDataReader reader)
        {
            List<T> rlist = new List<T>();
            if (reader == null)
            {
                return rlist;
            }

            try
            {
                while (reader.Read())
                {
                    T obj = Activator.CreateInstance<T>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        object value = reader[i];
                        if (value != DBNull.Value)
                        {
                            string rh = reader.GetName(i);
                            PropertyInfo pi = typeof(T).GetProperty(rh);
                            if (pi != null)
                            {
                                pi.SetValue(obj, value);
                            }
                        }
                    }

                    rlist.Add(obj);
                }
            }
            catch { }
            finally
            {
                if (dac.conn.State == ConnectionState.Open)
                {
                    dac.conn.Close();
                }
            }
            return rlist;
        }

        /// <summary>
        /// 转换DataTable到指定类型的对象
        /// </summary>
        /// <param name="table">数据源DataTable</param>
        /// <param name="objType">指定要转换到的类型</param>
        /// <returns>指定objType类型的对象</returns>
        object CastToObject(DataTable table, Type objType)
        {
            object instance = Activator.CreateInstance(objType);
            if (table == null || table.Rows.Count <= 0)
            {
                return instance;
            }

            Type memType = objType;
            if (instance is IList)
            {
                memType = objType.GenericTypeArguments[0];
            }

            string[] headers = new string[table.Columns.Count];
            for (int i = 0; i < headers.Length; i++)
            {
                headers[i] = table.Columns[i].ColumnName;
            }

            foreach (DataRow dr in table.Rows)
            {
                object obj = Activator.CreateInstance(memType);
                foreach (string header in headers)
                {
                    var value = dr[header];
                    if (value != DBNull.Value)
                    {
                        PropertyInfo pi = memType.GetProperty(header);
                        if (pi != null)
                        {
                            pi.SetValue(obj, value);
                        }
                    }
                }
                if (!memType.Equals(objType))
                {
                    (instance as System.Collections.IList).Add(obj);
                }
                else
                {
                    return obj;
                }
            }

            return instance;
        }

        #endregion

        #region 执行初始化

        /// <summary>
        /// 初始化查询
        /// </summary>
        /// <param name="entity">执行参数对象</param>
        /// <param name="option">要初始化的内容：查询选项</param>
        /// <param name="parameters">要初始化的内容：查询参数</param>
        void InitExecute(DbExecuteEntity entity, out SearchOption option, out DbParameter[] parameters, out DbParameter outparam)
        {
            option = new SearchOption();
            List<DbParameter> tempParams = new List<DbParameter>();
            outparam = null;

            option.DbCommandType = entity.CommandType;
            if (dac.conn.GetType().IsAssignableFrom(typeof(OracleConnection)))
            {
                option.DbType = DatabaseType.Oracle;
                option.SearchID = entity.ToSearchString(option.DbIdentify);

                foreach (string key in entity.ConditionKeys)
                {
                    var pv = entity[key];
                    tempParams.Add(new OracleParameter(option.DbIdentify + key, pv == null ? DBNull.Value : pv));
                }

                SearchEntity se = entity as SearchEntity;
                if (se != null && se.PageIndex > 0)
                {
                    tempParams.Add(new OracleParameter("startIndex", (se.PageIndex - 1) * se.PageSize));
                    tempParams.Add(new OracleParameter("endIndex", se.PageIndex * se.PageSize));
                    outparam = new OracleParameter(option.DbIdentify + "outcount", 0) { Direction = ParameterDirection.Output };
                    tempParams.Add(outparam);
                }
            }
            else
            {
                option.DbType = DatabaseType.SqlServer;
                option.SearchID = entity.ToSearchString(option.DbIdentify);

                foreach (string key in entity.ConditionKeys)
                {
                    var pv = entity[key];
                    tempParams.Add(new SqlParameter(option.DbIdentify + key, pv == null ? DBNull.Value : pv));
                }

                SearchEntity se=entity as SearchEntity;
                if (se!=null&&se.PageIndex>0)
                {
                    tempParams.Add(new SqlParameter("startIndex", (se.PageIndex - 1) * se.PageSize));
                    tempParams.Add(new SqlParameter("endIndex", se.PageIndex * se.PageSize));
                    outparam = new SqlParameter(option.DbIdentify + "outcount", 0) { Direction = ParameterDirection.Output };
                    tempParams.Add(outparam);
                }
            }

            parameters = tempParams.ToArray();
        }

        /// <summary>
        /// 初始化Command对象
        /// </summary>
        /// <param name="option"></param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        IDbCommand InitCommand(SearchOption option, DbParameter[] parameters)
        {
            try
            {
                dac.OpenConnection();
                if (option.DbType == DatabaseType.SqlServer)
                {
                    dac.cmd = new SqlCommand(option.SearchID, dac.conn as SqlConnection);
                }
                else
                {
                    dac.cmd = new OracleCommand(option.SearchID, dac.conn as OracleConnection);
                }
                dac.cmd.CommandType = option.DbCommandType;

                dac.cmd.Parameters.Clear();
                foreach (var p in parameters)
                {
                    dac.cmd.Parameters.Add(p);
                }

                return dac.cmd;
            }
            catch
            {
                if (dac.conn.State == ConnectionState.Open)
                {
                    dac.conn.Close();
                }
                dac.cmd.Dispose();
            }

            return null;
        }

        #endregion

        #region 执行查询

        /// <summary>
        /// 执行ExecuteReader
        /// </summary>
        /// <param name="option">查询条件项</param>
        /// <param name="parameters">参数</param>
        /// <returns>IDataReader</returns>
        IDataReader ExecuteReader(SearchOption option, DbParameter[] parameters)
        {
            var cmd = InitCommand(option, parameters);
            if (cmd != null)
            {
                try
                {
                    return dac.cmd.ExecuteReader();
                }
                catch { throw; }
                finally
                {
                    cmd.Dispose();
                }
            }
            return null;
        }

        /// <summary>
        /// 执行多个查询，一般用于在存储过程中查询出多个Table
        /// </summary>
        /// <param name="option">查询条件项</param>
        /// <param name="parameters">参数</param>
        /// <returns>Dictionary<string,DataTable> string:数据表对应的Model属性名；DataTable:数据表</returns>
        Dictionary<string, DataTable> ExecteMultipleTable(SearchOption option, DbParameter[] parameters)
        {
            var cmd = InitCommand(option, parameters);
            if (cmd != null)
            {
                DataAdapter adapter = null;
                DataSet ds = new DataSet();
                try
                {
                    if (option.DbType == DatabaseType.SqlServer)
                    {
                        adapter = new SqlDataAdapter(cmd as SqlCommand);
                    }
                    else
                    {
                        adapter = new OracleDataAdapter(cmd as OracleCommand);
                    }
                    adapter.Fill(ds);

                    Dictionary<string, DataTable> dic = new Dictionary<string, DataTable>();
                    int identifyIndex = ds.Tables.Count - 1;
                    for (int i = 0; i < identifyIndex; i++)
                    {
                        dic.Add(ds.Tables[identifyIndex].Rows[0][i].ToString(), ds.Tables[i]);
                    }
                    return dic;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    cmd.Dispose();
                    adapter.Dispose();
                    ds.Dispose();
                }
            }
            return null;
        }

        #endregion

        #endregion

        public CommonResult Execute(DbExecuteEntity entity)
        {
            InitExecute(entity, out option, out parameters, out outparam);

            int rid = 1;
            string message = string.Empty;
            try
            {
                var list = CastToList<CommonResult>(ExecuteReader(option, parameters.ToArray()));
                if (list.Count > 0)
                {
                    return list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new CommonResult() { ResultID = rid, Message = message };
        }

        public CommonResult Execute(DbExecuteEntity entity,bool throwException)
        {
            try
            {
                return Execute(entity);
            }
            catch(Exception err)
            {
                if(throwException)
                {
                    throw err;
                }
                else
                {
                    return new CommonResult() { ResultID = 0, Message = err.Message };
                }
            }
        }

        public CommonResult<T> ExecuteSelect<T>(SearchEntity search)
        {
            InitExecute(search, out option, out parameters, out outparam);

            CommonResult<T> result = new CommonResult<T>();
            result.Datas = CastToList<T>(ExecuteReader(option, parameters.ToArray()));
            if (outparam != null)
            {
                result.PageCount = (int)Math.Ceiling(Convert.ToDouble(outparam.Value) / search.PageSize);
                result.RecordCount = Convert.ToInt32(outparam.Value);                
            }
            return result;
        }

        public T ExecuteMultipleSelect<T>(DbExecuteEntity entity)
        {
            InitExecute(entity, out option, out parameters, out outparam);

            T obj = Activator.CreateInstance<T>();
            var dic = ExecteMultipleTable(option, parameters);
            if (dic != null || dic.Count > 0)
            {
                foreach (string d in dic.Keys)
                {
                    PropertyInfo pi = typeof(T).GetProperty(d);
                    if (pi != null)
                    {
                        pi.SetValue(obj, CastToObject(dic[d], pi.PropertyType));
                    }
                }
            }

            return obj;
        }
    }
}
