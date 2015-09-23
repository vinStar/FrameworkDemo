namespace HyBy.FrameWork.DAService.ExCommon
{
    using HyBy.FrameWork.DAService;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.OracleClient;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ExCommonDBO : DBO
    {
        private SearchOption option;
        private DbParameter outparam;
        private DbParameter[] parameters;

        public ExCommonDBO()
        {
            this.option = null;
            this.parameters = null;
            this.outparam = null;
        }

        public ExCommonDBO(DataAccess dac) : this()
        {
            base.dac = dac;
        }

        private List<T> CastToList<T>(IDataReader reader)
        {
            List<T> list = new List<T>();
            if (reader != null)
            {
                try
                {
                    while (reader.Read())
                    {
                        T local = Activator.CreateInstance<T>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            object obj2 = reader[i];
                            if (obj2 != DBNull.Value)
                            {
                                string name = reader.GetName(i);
                                PropertyInfo property = typeof(T).GetProperty(name);
                                if (property != null)
                                {
                                    property.SetValue(local, obj2);
                                }
                            }
                        }
                        list.Add(local);
                    }
                }
                catch
                {
                }
                finally
                {
                    if (base.dac.conn.State == ConnectionState.Open)
                    {
                        base.dac.conn.Close();
                    }
                }
            }
            return list;
        }

        private object CastToObject(DataTable table, Type objType)
        {
            object obj2 = Activator.CreateInstance(objType);
            if ((table != null) && (table.Rows.Count > 0))
            {
                Type type = objType;
                if (obj2 is IList)
                {
                    type = objType.GenericTypeArguments[0];
                }
                string[] strArray = new string[table.Columns.Count];
                for (int i = 0; i < strArray.Length; i++)
                {
                    strArray[i] = table.Columns[i].ColumnName;
                }
                foreach (DataRow row in table.Rows)
                {
                    object obj3 = Activator.CreateInstance(type);
                    foreach (string str in strArray)
                    {
                        object obj4 = row[str];
                        if (obj4 != DBNull.Value)
                        {
                            PropertyInfo property = type.GetProperty(str);
                            if (property != null)
                            {
                                property.SetValue(obj3, obj4);
                            }
                        }
                    }
                    if (!type.Equals(objType))
                    {
                        (obj2 as IList).Add(obj3);
                    }
                    else
                    {
                        return obj3;
                    }
                }
            }
            return obj2;
        }

        private Dictionary<string, DataTable> ExecteMultipleTable(SearchOption option, DbParameter[] parameters)
        {
            IDbCommand command = this.InitCommand(option, parameters);
            if (command != null)
            {
                DataAdapter adapter = null;
                DataSet dataSet = new DataSet();
                try
                {
                    if (option.DbType == DatabaseType.SqlServer)
                    {
                        adapter = new SqlDataAdapter(command as SqlCommand);
                    }
                    else
                    {
                        adapter = new OracleDataAdapter(command as OracleCommand);
                    }
                    adapter.Fill(dataSet);
                    Dictionary<string, DataTable> dictionary = new Dictionary<string, DataTable>();
                    int num = dataSet.Tables.Count - 1;
                    for (int i = 0; i < num; i++)
                    {
                        dictionary.Add(dataSet.Tables[num].Rows[0][i].ToString(), dataSet.Tables[i]);
                    }
                    return dictionary;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    command.Dispose();
                    adapter.Dispose();
                    dataSet.Dispose();
                }
            }
            return null;
        }

        public CommonResult Execute(DbExecuteEntity entity)
        {
            this.InitExecute(entity, out this.option, out this.parameters, out this.outparam);
            int num = 1;
            string message = string.Empty;
            try
            {
                List<CommonResult> list = this.CastToList<CommonResult>(this.ExecuteReader(this.option, this.parameters.ToArray<DbParameter>()));
                if (list.Count > 0)
                {
                    return list[0];
                }
            }
            catch (Exception exception)
            {
                num = 0;
                message = exception.Message;
            }
            return new CommonResult { ResultID = num, Message = message };
        }

        public T ExecuteMultipleSelect<T>(DbExecuteEntity entity)
        {
            this.InitExecute(entity, out this.option, out this.parameters, out this.outparam);
            T local = Activator.CreateInstance<T>();
            Dictionary<string, DataTable> dictionary = this.ExecteMultipleTable(this.option, this.parameters);
            if ((dictionary != null) || (dictionary.Count > 0))
            {
                foreach (string str in dictionary.Keys)
                {
                    PropertyInfo property = typeof(T).GetProperty(str);
                    if (property != null)
                    {
                        property.SetValue(local, this.CastToObject(dictionary[str], property.PropertyType));
                    }
                }
            }
            return local;
        }

        private IDataReader ExecuteReader(SearchOption option, DbParameter[] parameters)
        {
            IDbCommand command = this.InitCommand(option, parameters);
            if (command != null)
            {
                try
                {
                    return base.dac.cmd.ExecuteReader();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    command.Dispose();
                }
            }
            return null;
        }

        public CommonResult<T> ExecuteSelect<T>(SearchEntity search)
        {
            this.InitExecute(search, out this.option, out this.parameters, out this.outparam);
            CommonResult<T> result = new CommonResult<T> {
                Datas = this.CastToList<T>(this.ExecuteReader(this.option, this.parameters.ToArray<DbParameter>()))
            };
            if (this.outparam != null)
            {
                result.PageCount = (int) Math.Ceiling((double) (Convert.ToDouble(this.outparam.Value) / ((double) search.PageSize)));
                result.RecordCount = Convert.ToInt32(this.outparam.Value);
            }
            return result;
        }

        private IDbCommand InitCommand(SearchOption option, DbParameter[] parameters)
        {
            try
            {
                base.dac.OpenConnection();
                if (option.DbType == DatabaseType.SqlServer)
                {
                    base.dac.cmd = new SqlCommand(option.SearchID, base.dac.conn as SqlConnection);
                }
                else
                {
                    base.dac.cmd = new OracleCommand(option.SearchID, base.dac.conn as OracleConnection);
                }
                base.dac.cmd.CommandType = option.DbCommandType;
                base.dac.cmd.Parameters.Clear();
                foreach (DbParameter parameter in parameters)
                {
                    base.dac.cmd.Parameters.Add(parameter);
                }
                return base.dac.cmd;
            }
            catch
            {
                if (base.dac.conn.State == ConnectionState.Open)
                {
                    base.dac.conn.Close();
                }
                base.dac.cmd.Dispose();
            }
            return null;
        }

        private void InitExecute(DbExecuteEntity entity, out SearchOption option, out DbParameter[] parameters, out DbParameter outparam)
        {
            object obj2;
            SearchEntity entity2;
            option = new SearchOption();
            List<DbParameter> list = new List<DbParameter>();
            outparam = null;
            option.DbCommandType = entity.CommandType;
            if (base.dac.conn.GetType().IsAssignableFrom(typeof(OracleConnection)))
            {
                option.DbType = DatabaseType.Oracle;
                option.SearchID = entity.ToSearchString(option.DbIdentify);
                foreach (string str in entity.ConditionKeys)
                {
                    obj2 = entity[str];
                    list.Add(new OracleParameter(option.DbIdentify + str, (obj2 == null) ? DBNull.Value : obj2));
                }
                entity2 = entity as SearchEntity;
                if ((entity2 != null) && (entity2.PageIndex > 0))
                {
                    list.Add(new OracleParameter("startIndex", (entity2.PageIndex - 1) * entity2.PageSize));
                    list.Add(new OracleParameter("endIndex", entity2.PageIndex * entity2.PageSize));
                    OracleParameter parameter = new OracleParameter(option.DbIdentify + "outcount", (OracleType) 0) {
                        Direction = ParameterDirection.Output
                    };
                    outparam = parameter;
                    list.Add(outparam);
                }
            }
            else
            {
                option.DbType = DatabaseType.SqlServer;
                option.SearchID = entity.ToSearchString(option.DbIdentify);
                foreach (string str in entity.ConditionKeys)
                {
                    obj2 = entity[str];
                    list.Add(new SqlParameter(option.DbIdentify + str, (obj2 == null) ? DBNull.Value : obj2));
                }
                entity2 = entity as SearchEntity;
                if ((entity2 != null) && (entity2.PageIndex > 0))
                {
                    list.Add(new SqlParameter("startIndex", (entity2.PageIndex - 1) * entity2.PageSize));
                    list.Add(new SqlParameter("endIndex", entity2.PageIndex * entity2.PageSize));
                    SqlParameter parameter2 = new SqlParameter(option.DbIdentify + "outcount", SqlDbType.BigInt) {
                        Direction = ParameterDirection.Output
                    };
                    outparam = parameter2;
                    list.Add(outparam);
                }
            }
            parameters = list.ToArray();
        }

        private enum DatabaseType
        {
            SqlServer,
            Oracle
        }

        private class SearchOption
        {
            public CommandType DbCommandType { get; set; }

            public string DbIdentify
            {
                get
                {
                    if (this.DbType == ExCommonDBO.DatabaseType.SqlServer)
                    {
                        return "@";
                    }
                    if (this.DbType == ExCommonDBO.DatabaseType.Oracle)
                    {
                        return ":";
                    }
                    return "";
                }
            }

            public ExCommonDBO.DatabaseType DbType { get; set; }

            public string SearchID { get; set; }
        }
    }
}

