namespace HyBy.FrameWork.DAService
{
    using HyBy.FrameWork.Common;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using System.Data.OracleClient;
    using System.Data.SqlClient;
    using System.Reflection;
    using System.Text;

    public class DBO : IDisposable
    {
        protected static ConnectionStringSettings connStringSetting = ConfigurationHelper.GetConnectionStringSettings("default");
        protected DataAccess dac = new DataAccess("");
        protected static DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connStringSetting.ProviderName);
        public DBMappingCollection[] DBMapping = new DBMappingCollection[3];
        public DBMappingCollection[] MySqlDBMapping = new DBMappingCollection[3];
        public DBMappingCollection[] OracleDBMapping = new DBMappingCollection[3];
        public const int PKDBMappingIndex = 2;
        public const int SelectDBMappingIndex = 0;
        public DBMappingCollection[] SqlDBMapping = new DBMappingCollection[3];
        public const int UpdateDBMappingIndex = 1;

        public void Dispose()
        {
            this.dac.Close();
        }

        public void ExcuteCommand(string spName, Hashtable args)
        {
            try
            {
                this.dac.cmd.Parameters.Clear();
                this.dac.cmd.CommandText = spName;
                this.dac.cmd.CommandType = CommandType.StoredProcedure;
                this.dac.cmd.Connection = this.dac.conn;
                if (this.dac.conn is SqlConnection)
                {
                    foreach (string str in args.Keys)
                    {
                        SqlParameter parameter = new SqlParameter
                        {
                            ParameterName = (str.IndexOf("@") == 0) ? str : string.Format("@{0}", str),
                            Value = args[str]
                        };
                        this.dac.cmd.Parameters.Add(parameter);
                    }
                }
                if (this.dac.conn is OracleConnection)
                {
                    foreach (string str in args.Keys)
                    {
                        OracleParameter parameter3 = new OracleParameter
                        {
                            ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, str),
                            Value = args[str]
                        };
                        switch (Type.GetTypeCode(args[str].GetType()))
                        {
                            case TypeCode.Int32:
                            case TypeCode.Int64:
                            case TypeCode.Single:
                            case TypeCode.Double:
                            case TypeCode.Decimal:
                                parameter3.OracleType = OracleType.Number;
                                break;

                            case TypeCode.DateTime:
                                parameter3.Value = (DateTime)args[str];
                                parameter3.OracleType = OracleType.DateTime;
                                break;

                            case TypeCode.Boolean:
                                parameter3.OracleType = OracleType.Char;
                                if (bool.Parse(args[str].ToString()))
                                {
                                    parameter3.Value = "Y";
                                }
                                else
                                {
                                    parameter3.Value = "N";
                                }
                                break;

                            default:
                                parameter3.OracleType = OracleType.VarChar;
                                parameter3.Size = 0xfa0;
                                break;
                        }
                        this.dac.cmd.Parameters.Add(parameter3);
                    }
                }
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw new CommonException(exception, CommonDeclare.EnumExceptionLevel.ERROR);
            }
        }

        public static DataSet ExcuteSql(string sql)
        {
            DataSet set2;
            try
            {
                using (DbConnection connection = dbFactory.CreateConnection())
                {
                    connection.ConnectionString = connStringSetting.ConnectionString;
                    connection.Open();
                    DataSet dataSet = new DataSet();
                    DbDataAdapter adapter = dbFactory.CreateDataAdapter();
                    DbCommand command = dbFactory.CreateCommand();
                    command.Connection = connection;
                    command.CommandText = sql;
                    adapter.SelectCommand = command;
                    adapter.Fill(dataSet);
                    set2 = dataSet;
                }
            }
            catch (Exception exception)
            {
                throw new CommonException(exception, CommonDeclare.EnumExceptionLevel.ERROR);
            }
            return set2;
        }

        public static void ExecuteCommand(DbCommand cmd)
        {
            try
            {
                using (DbConnection connection = dbFactory.CreateConnection())
                {
                    connection.ConnectionString = connStringSetting.ConnectionString;
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new CommonException(exception, CommonDeclare.EnumExceptionLevel.ERROR);
            }
        }

        public object ExecuteScalar(string spName, Dictionary<string, object> args)
        {
            object obj3;
            try
            {
                object obj2;
                this.dac.cmd.Parameters.Clear();
                this.dac.cmd.CommandText = spName;
                this.dac.cmd.CommandType = CommandType.StoredProcedure;
                this.dac.cmd.Connection = this.dac.conn;
                if (this.dac.conn is SqlConnection)
                {
                    foreach (string str in args.Keys)
                    {
                        SqlParameter parameter = new SqlParameter
                        {
                            ParameterName = (str.IndexOf("@") == 0) ? str : string.Format("@{0}", str),
                            Value = args[str]
                        };
                        this.dac.cmd.Parameters.Add(parameter);
                    }
                }
                if (this.dac.conn is OracleConnection)
                {
                    foreach (string str in args.Keys)
                    {
                        OracleParameter parameter3 = new OracleParameter
                        {
                            ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, str),
                            Value = args[str]
                        };
                        switch (Type.GetTypeCode(args[str].GetType()))
                        {
                            case TypeCode.Int32:
                            case TypeCode.Int64:
                            case TypeCode.Single:
                            case TypeCode.Double:
                            case TypeCode.Decimal:
                                parameter3.OracleType = OracleType.Number;
                                break;

                            case TypeCode.DateTime:
                                parameter3.Value = (DateTime)args[str];
                                parameter3.OracleType = OracleType.DateTime;
                                break;

                            case TypeCode.Boolean:
                                parameter3.OracleType = OracleType.Char;
                                if (bool.Parse(args[str].ToString()))
                                {
                                    parameter3.Value = "Y";
                                }
                                else
                                {
                                    parameter3.Value = "N";
                                }
                                break;

                            default:
                                parameter3.OracleType = OracleType.VarChar;
                                parameter3.Size = 0xfa0;
                                break;
                        }
                        this.dac.cmd.Parameters.Add(parameter3);
                        OracleParameter parameter4 = new OracleParameter
                        {
                            ParameterName = "p_rc",
                            OracleType = OracleType.Cursor,
                            Direction = ParameterDirection.Output
                        };
                        this.dac.cmd.Parameters.Add(parameter4);
                    }
                }
                this.dac.OpenConnection();
                IDataReader reader = this.dac.cmd.ExecuteReader();
                if (reader.Read())
                {
                    obj2 = reader.GetValue(0);
                }
                else
                {
                    obj2 = null;
                }
                reader.Close();
                obj3 = obj2;
            }
            catch (Exception exception)
            {
                throw new CommonException(exception, CommonDeclare.EnumExceptionLevel.ERROR);
            }
            return obj3;
        }

        private static IList GenerateListT<T>(T item)
        {
            return (Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[] { item.GetType() })) as IList);
        }

        private void GenereateSqlCommand(string spName)
        {
            try
            {
                this.dac.cmd.Parameters.Clear();
                this.dac.cmd.CommandText = spName;
                this.dac.cmd.CommandType = CommandType.StoredProcedure;
            }
            catch (Exception exception)
            {
                throw new CommonException(exception, CommonDeclare.EnumExceptionLevel.ERROR);
            }
        }

        public IList GetAllBySP<T>(string spName, T item)
        {
            IList list = GenerateListT<T>(item);
            this.GenereateSqlCommand(spName);
            if (this.dac.conn is SqlConnection)
            {
                this.dac.OpenConnection();
                IDataReader reader = this.dac.cmd.ExecuteReader();
                while (reader.Read())
                {
                    item = ReaderToObjectForCollection<T>(item, reader);
                    list.Add(item);
                }
                reader.Close();
            }
            if (this.dac.conn is OracleConnection)
            {
                OracleParameter parameter = new OracleParameter
                {
                    ParameterName = "p_rc",
                    OracleType = OracleType.Cursor,
                    Direction = ParameterDirection.Output
                };
                this.dac.cmd.Parameters.Add(parameter);
                this.dac.OpenConnection();
                IDataReader reader2 = this.dac.cmd.ExecuteReader();
                while (reader2.Read())
                {
                    item = ReaderToObjectForCollection<T>(item, reader2);
                    list.Add(item);
                }
                reader2.Close();
            }
            return list;
        }

        public void GetAllBySP(string spName, DataTable items)
        {
            this.dac.cmd.Parameters.Clear();
            this.dac.cmd.CommandText = spName;
            this.dac.cmd.CommandType = CommandType.StoredProcedure;
            this.dac.cmd.Connection = this.dac.conn;
            if (this.dac.conn is OracleConnection)
            {
                OracleParameter parameter = new OracleParameter
                {
                    ParameterName = "p_rc",
                    OracleType = OracleType.Cursor,
                    Direction = ParameterDirection.Output
                };
                this.dac.cmd.Parameters.Add(parameter);
            }
            this.dac.OpenConnection();
            IDataReader reader = this.dac.cmd.ExecuteReader();
            while (reader.Read())
            {
                int num;
                DataRow row = items.NewRow();
                if (this.dac.conn is SqlConnection)
                {
                    foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[0])
                    {
                        num = 0;
                        while (num < reader.FieldCount)
                        {
                            if (reader.GetName(num).ToLower() == mapping.First.ToLower())
                            {
                                row[mapping.Second] = reader.GetValue(num);
                            }
                            num++;
                        }
                    }
                }
                if (this.dac.conn is OracleConnection)
                {
                    foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[0])
                    {
                        for (num = 0; num < reader.FieldCount; num++)
                        {
                            if (reader.GetName(num).ToLower() == mapping2.First.ToLower())
                            {
                                row[mapping2.Second] = reader.GetValue(num);
                            }
                        }
                    }
                }
                items.Rows.Add(row);
            }
            reader.Close();
        }

        public IList GetAllBySP<T>(string spName, T item, Dictionary<string, object> args)
        {
            IList list = GenerateListT<T>(item);
            this.GenereateSqlCommand(spName);
            if (this.dac.conn is SqlConnection)
            {
                foreach (string str in args.Keys)
                {
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = (str.IndexOf("@") == 0) ? str : string.Format("@{0}", str),
                        Value = (args[str] == null) ? DBNull.Value : args[str]
                    };
                    this.dac.cmd.Parameters.Add(parameter);
                }
                this.dac.OpenConnection();
                IDataReader reader = this.dac.cmd.ExecuteReader();
                while (reader.Read())
                {
                    item = ReaderToObjectForCollection<T>(item, reader);
                    list.Add(item);
                }
                reader.Close();
            }
            if (this.dac.conn is OracleConnection)
            {
                foreach (string str in args.Keys)
                {
                    OracleParameter parameter3 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, str),
                        Value = args[str]
                    };
                    switch (Type.GetTypeCode(args[str].GetType()))
                    {
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            parameter3.OracleType = OracleType.Number;
                            break;

                        case TypeCode.DateTime:
                            parameter3.Value = ((DateTime)args[str]).ToString("yyyy-MM-dd hh:mm:ss");
                            parameter3.OracleType = OracleType.VarChar;
                            break;

                        case TypeCode.Boolean:
                            parameter3.OracleType = OracleType.Char;
                            if (bool.Parse(args[str].ToString()))
                            {
                                parameter3.Value = "Y";
                            }
                            else
                            {
                                parameter3.Value = "N";
                            }
                            break;

                        default:
                            parameter3.OracleType = OracleType.VarChar;
                            parameter3.Size = 0xfa0;
                            break;
                    }
                    this.dac.cmd.Parameters.Add(parameter3);
                }
                OracleParameter parameter5 = new OracleParameter
                {
                    ParameterName = "p_rc",
                    OracleType = OracleType.Cursor,
                    Direction = ParameterDirection.Output
                };
                this.dac.cmd.Parameters.Add(parameter5);
                this.dac.OpenConnection();
                IDataReader reader2 = this.dac.cmd.ExecuteReader();
                while (reader2.Read())
                {
                    item = ReaderToObjectForCollection<T>(item, reader2);
                    list.Add(item);
                }
                reader2.Close();
            }
            return list;
        }

        public void GetAllBySP(string spName, DataTable items, Dictionary<string, object> args)
        {
            DataRow row;
            int num;
            this.dac.cmd.Parameters.Clear();
            items.Rows.Clear();
            this.dac.cmd.CommandText = spName;
            this.dac.cmd.CommandType = CommandType.StoredProcedure;
            this.dac.cmd.Connection = this.dac.conn;
            if (this.dac.conn is SqlConnection)
            {
                foreach (string str in args.Keys)
                {
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = (str.IndexOf("@") == 0) ? str : string.Format("@{0}", str),
                        Value = args[str]
                    };
                    this.dac.cmd.Parameters.Add(parameter);
                }
                this.dac.OpenConnection();
                IDataReader reader = this.dac.cmd.ExecuteReader();
                while (reader.Read())
                {
                    row = items.NewRow();
                    foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[0])
                    {
                        num = 0;
                        while (num < reader.FieldCount)
                        {
                            if (reader.GetName(num).ToLower() == mapping.First.ToLower())
                            {
                                row[mapping.Second] = reader.GetValue(num);
                            }
                            num++;
                        }
                    }
                    items.Rows.Add(row);
                }
                reader.Close();
            }
            if (this.dac.conn is OracleConnection)
            {
                foreach (string str in args.Keys)
                {
                    OracleParameter parameter3 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, str),
                        Value = args[str]
                    };
                    switch (Type.GetTypeCode(args[str].GetType()))
                    {
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            parameter3.OracleType = OracleType.Number;
                            break;

                        case TypeCode.DateTime:
                            parameter3.Value = ((DateTime)args[str]).ToString("yyyy-MM-dd hh:mm:ss");
                            parameter3.OracleType = OracleType.VarChar;
                            break;

                        case TypeCode.Boolean:
                            parameter3.OracleType = OracleType.Char;
                            if (bool.Parse(args[str].ToString()))
                            {
                                parameter3.Value = "Y";
                            }
                            else
                            {
                                parameter3.Value = "N";
                            }
                            break;

                        default:
                            parameter3.OracleType = OracleType.VarChar;
                            parameter3.Size = 0xfa0;
                            break;
                    }
                    this.dac.cmd.Parameters.Add(parameter3);
                }
                OracleParameter parameter5 = new OracleParameter
                {
                    ParameterName = "p_rc",
                    OracleType = OracleType.Cursor,
                    Direction = ParameterDirection.Output
                };
                this.dac.cmd.Parameters.Add(parameter5);
                this.dac.OpenConnection();
                IDataReader reader2 = this.dac.cmd.ExecuteReader();
                while (reader2.Read())
                {
                    row = items.NewRow();
                    foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[0])
                    {
                        for (num = 0; num < reader2.FieldCount; num++)
                        {
                            if (reader2.GetName(num).ToLower() == mapping2.First.ToLower())
                            {
                                if (items.Columns[mapping2.Second].DataType == typeof(bool))
                                {
                                    if (reader2.GetValue(num).ToString().ToUpper() == "Y")
                                    {
                                        row[mapping2.Second] = true;
                                    }
                                    else
                                    {
                                        row[mapping2.Second] = false;
                                    }
                                }
                                else
                                {
                                    row[mapping2.Second] = reader2.GetValue(num);
                                }
                            }
                        }
                    }
                    items.Rows.Add(row);
                }
                reader2.Close();
            }
        }

        public IList GetAllBySP<T>(string spName, T item, Dictionary<string, object> args, ref Dictionary<string, object> outputArgs)
        {
            IList list = GenerateListT<T>(item);
            this.GenereateSqlCommand(spName);
            if (this.dac.conn is SqlConnection)
            {
                SqlParameter parameter;
                foreach (string str in args.Keys)
                {
                    if (args[str] == null)
                    {
                        parameter = new SqlParameter
                        {
                            ParameterName = (str.IndexOf("@") == 0) ? str : string.Format("@{0}", str),
                            Value = string.Empty
                        };
                    }
                    else
                    {
                        parameter = new SqlParameter
                        {
                            ParameterName = (str.IndexOf("@") == 0) ? str : string.Format("@{0}", str),
                            Value = args[str]
                        };
                    }
                    this.dac.cmd.Parameters.Add(parameter);
                }
                foreach (string str in outputArgs.Keys)
                {
                    parameter = new SqlParameter
                    {
                        ParameterName = (str.IndexOf("@") == 0) ? str : string.Format("@{0}", str),
                        Value = 0,
                        Direction = ParameterDirection.Output,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 0x100
                    };
                    this.dac.cmd.Parameters.Add(parameter);
                }
                this.dac.OpenConnection();
                IDataReader reader = this.dac.cmd.ExecuteReader();
                while (reader.Read())
                {
                    item = ReaderToObjectForCollection<T>(item, reader);
                    list.Add(item);
                }
                reader.Close();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                foreach (string str in outputArgs.Keys)
                {
                    dictionary.Add(str, (this.dac.cmd as SqlCommand).Parameters[(str.IndexOf("@") == 0) ? str : string.Format("@{0}", str)].Value);
                }
                outputArgs = dictionary;
            }
            if (this.dac.conn is OracleConnection)
            {
                foreach (string str in args.Keys)
                {
                    OracleParameter parameter5 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, str),
                        Value = args[str]
                    };
                    switch (Type.GetTypeCode(args[str].GetType()))
                    {
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            parameter5.OracleType = OracleType.Number;
                            break;

                        case TypeCode.DateTime:
                            parameter5.Value = ((DateTime)args[str]).ToString("yyyy-MM-dd hh:mm:ss");
                            parameter5.OracleType = OracleType.VarChar;
                            break;

                        case TypeCode.Boolean:
                            parameter5.OracleType = OracleType.Char;
                            if (bool.Parse(args[str].ToString()))
                            {
                                parameter5.Value = "Y";
                            }
                            else
                            {
                                parameter5.Value = "N";
                            }
                            break;

                        default:
                            parameter5.OracleType = OracleType.VarChar;
                            parameter5.Size = 0xfa0;
                            break;
                    }
                    this.dac.cmd.Parameters.Add(parameter5);
                }
                OracleParameter parameter7 = new OracleParameter
                {
                    ParameterName = "p_rc",
                    OracleType = OracleType.Cursor,
                    Direction = ParameterDirection.Output
                };
                this.dac.cmd.Parameters.Add(parameter7);
                this.dac.OpenConnection();
                IDataReader reader2 = this.dac.cmd.ExecuteReader();
                while (reader2.Read())
                {
                    item = ReaderToObjectForCollection<T>(item, reader2);
                    list.Add(item);
                }
                reader2.Close();
            }
            return list;
        }

        public void GetAllBySP(string spName, DataTable items, Dictionary<string, object> args, ref Dictionary<string, object> outputArgs)
        {
            DataRow row;
            int num;
            this.dac.cmd.Parameters.Clear();
            items.Clear();
            this.dac.cmd.CommandText = spName;
            this.dac.cmd.CommandType = CommandType.StoredProcedure;
            this.dac.cmd.Connection = this.dac.conn;
            if (this.dac.conn is SqlConnection)
            {
                SqlParameter parameter;
                foreach (string str in args.Keys)
                {
                    if (args[str] == null)
                    {
                        parameter = new SqlParameter
                        {
                            ParameterName = (str.IndexOf("@") == 0) ? str : string.Format("@{0}", str),
                            Value = string.Empty
                        };
                    }
                    else
                    {
                        parameter = new SqlParameter
                        {
                            ParameterName = (str.IndexOf("@") == 0) ? str : string.Format("@{0}", str),
                            Value = args[str]
                        };
                    }
                    this.dac.cmd.Parameters.Add(parameter);
                }
                foreach (string str in outputArgs.Keys)
                {
                    parameter = new SqlParameter
                    {
                        ParameterName = (str.IndexOf("@") == 0) ? str : string.Format("@{0}", str),
                        Value = 0,
                        Direction = ParameterDirection.Output,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 0x100
                    };
                    this.dac.cmd.Parameters.Add(parameter);
                }
                this.dac.OpenConnection();
                IDataReader reader = this.dac.cmd.ExecuteReader();
                while (reader.Read())
                {
                    row = items.NewRow();
                    foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[0])
                    {
                        num = 0;
                        while (num < reader.FieldCount)
                        {
                            if (reader.GetName(num).ToLower() == mapping.First.ToLower())
                            {
                                row[mapping.Second] = reader.GetValue(num);
                            }
                            num++;
                        }
                    }
                    items.Rows.Add(row);
                }
                reader.Close();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                foreach (string str in outputArgs.Keys)
                {
                    dictionary.Add(str, (this.dac.cmd as SqlCommand).Parameters[(str.IndexOf("@") == 0) ? str : string.Format("@{0}", str)].Value);
                }
                outputArgs = dictionary;
            }
            if (this.dac.conn is OracleConnection)
            {
                foreach (string str in args.Keys)
                {
                    OracleParameter parameter5 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, str),
                        Value = args[str]
                    };
                    switch (Type.GetTypeCode(args[str].GetType()))
                    {
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            parameter5.OracleType = OracleType.Number;
                            break;

                        case TypeCode.DateTime:
                            parameter5.Value = ((DateTime)args[str]).ToString("yyyy-MM-dd hh:mm:ss");
                            parameter5.OracleType = OracleType.VarChar;
                            break;

                        case TypeCode.Boolean:
                            parameter5.OracleType = OracleType.Char;
                            if (bool.Parse(args[str].ToString()))
                            {
                                parameter5.Value = "Y";
                            }
                            else
                            {
                                parameter5.Value = "N";
                            }
                            break;

                        default:
                            parameter5.OracleType = OracleType.VarChar;
                            parameter5.Size = 0xfa0;
                            break;
                    }
                    this.dac.cmd.Parameters.Add(parameter5);
                }
                OracleParameter parameter7 = new OracleParameter
                {
                    ParameterName = "p_rc",
                    OracleType = OracleType.Cursor,
                    Direction = ParameterDirection.Output
                };
                this.dac.cmd.Parameters.Add(parameter7);
                this.dac.OpenConnection();
                IDataReader reader2 = this.dac.cmd.ExecuteReader();
                while (reader2.Read())
                {
                    row = items.NewRow();
                    foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[0])
                    {
                        for (num = 0; num < reader2.FieldCount; num++)
                        {
                            if (reader2.GetName(num).ToLower() == mapping2.First.ToLower())
                            {
                                if (items.Columns[mapping2.Second].DataType == typeof(bool))
                                {
                                    if (reader2.GetValue(num).ToString().ToUpper() == "Y")
                                    {
                                        row[mapping2.Second] = true;
                                    }
                                    else
                                    {
                                        row[mapping2.Second] = false;
                                    }
                                }
                                else
                                {
                                    row[mapping2.Second] = reader2.GetValue(num);
                                }
                            }
                        }
                    }
                    items.Rows.Add(row);
                }
                reader2.Close();
            }
        }

        public void GetByField(string spName, DataTable item)
        {
            IDataReader reader;
            DataRow row;
            int num;
            this.dac.cmd.Parameters.Clear();
            this.dac.cmd.CommandText = spName;
            this.dac.cmd.CommandType = CommandType.StoredProcedure;
            if (this.dac.conn is SqlConnection)
            {
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[1])
                {
                    if (item.Rows[0][mapping.Second] != DBNull.Value)
                    {
                        SqlParameter parameter = new SqlParameter
                        {
                            ParameterName = mapping.First,
                            Value = item.Rows[0][mapping.Second],
                            Size = mapping.Size
                        };
                        this.dac.cmd.Parameters.Add(parameter);
                    }
                }
                this.dac.OpenConnection();
                reader = this.dac.cmd.ExecuteReader();
                item.Rows.Clear();
                while (reader.Read())
                {
                    row = item.NewRow();
                    foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[0])
                    {
                        num = 0;
                        while (num < reader.FieldCount)
                        {
                            if (reader.GetName(num).ToLower() == mapping.First.ToLower())
                            {
                                row[mapping.Second] = reader.GetValue(num);
                            }
                            num++;
                        }
                    }
                    item.Rows.Add(row);
                }
                reader.Close();
            }
            if (this.dac.conn is OracleConnection)
            {
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[1])
                {
                    DateTime time;
                    if (item.Rows[0][mapping2.Second] == DBNull.Value)
                    {
                        continue;
                    }
                    OracleParameter parameter3 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First)
                    };
                    TypeCode typeCode = Type.GetTypeCode(item.Columns[mapping2.Second].DataType);
                    if (typeCode != TypeCode.Boolean)
                    {
                        if (typeCode == TypeCode.DateTime)
                        {
                            goto Label_033B;
                        }
                        goto Label_0370;
                    }
                    if ((bool)item.Rows[0][mapping2.Second])
                    {
                        parameter3.Value = "Y";
                    }
                    else
                    {
                        parameter3.Value = "N";
                    }
                    goto Label_0392;
                Label_033B:
                    time = (DateTime)item.Rows[0][mapping2.Second];
                    parameter3.Value = time.ToString("yyyy/MM/dd hh:mm:ss");
                    goto Label_0392;
                Label_0370:
                    parameter3.Value = item.Rows[0][mapping2.Second];
                Label_0392:
                    parameter3.OracleType = mapping2.OracleType;
                    this.dac.cmd.Parameters.Add(parameter3);
                }
                OracleParameter parameter5 = new OracleParameter
                {
                    ParameterName = "p_rc",
                    OracleType = OracleType.Cursor,
                    Direction = ParameterDirection.Output
                };
                this.dac.cmd.Parameters.Add(parameter5);
                this.dac.OpenConnection();
                reader = this.dac.cmd.ExecuteReader();
                item.Rows.Clear();
                while (reader.Read())
                {
                    row = item.NewRow();
                    foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[0])
                    {
                        for (num = 0; num < reader.FieldCount; num++)
                        {
                            if (reader.GetName(num).ToLower() == mapping2.First.ToLower())
                            {
                                if (item.Columns[mapping2.Second].DataType == typeof(bool))
                                {
                                    if (reader.GetValue(num).ToString().ToUpper() == "Y")
                                    {
                                        row[mapping2.Second] = true;
                                    }
                                    else
                                    {
                                        row[mapping2.Second] = false;
                                    }
                                }
                                else
                                {
                                    row[mapping2.Second] = reader.GetValue(num);
                                }
                            }
                        }
                    }
                    item.Rows.Add(row);
                }
                reader.Close();
            }
            if (item.Rows.Count == 0)
            {
                item.Rows.Add(item.NewRow());
            }
        }

        public void GetByField<T>(string spName, T item)
        {
            PropertyInfo property;
            object obj2;
            IDataReader reader;
            this.GenereateSqlCommand(spName);
            if (this.dac.conn is SqlConnection)
            {
                SqlParameter parameter;
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    property = item.GetType().GetProperty(mapping.Second);
                    obj2 = property.GetValue(item, null);
                    if ((property != null) && (obj2 != null))
                    {
                        parameter = new SqlParameter
                        {
                            ParameterName = mapping.First,
                            Value = obj2
                        };
                        this.dac.cmd.Parameters.Add(parameter);
                    }
                }
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[1])
                {
                    property = item.GetType().GetProperty(mapping.Second);
                    obj2 = property.GetValue(item, null);
                    if ((property != null) && (obj2 != null))
                    {
                        parameter = new SqlParameter
                        {
                            ParameterName = mapping.First,
                            Value = obj2
                        };
                        this.dac.cmd.Parameters.Add(parameter);
                    }
                }
                this.dac.OpenConnection();
                reader = this.dac.cmd.ExecuteReader();
                while (reader.Read())
                {
                    ReaderToObject<T>(item, reader);
                }
                reader.Close();
            }
            if (this.dac.conn is OracleConnection)
            {
                OracleParameter parameter4;
                TypeCode typeCode;
                DateTime time;
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[1])
                {
                    property = item.GetType().GetProperty(mapping2.Second);
                    obj2 = property.GetValue(item, null);
                    if ((property == null) || (obj2 == null))
                    {
                        continue;
                    }
                    parameter4 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First)
                    };
                    typeCode = Type.GetTypeCode(property.GetType());
                    if (typeCode != TypeCode.Boolean)
                    {
                        if (typeCode == TypeCode.DateTime)
                        {
                            goto Label_02FF;
                        }
                        goto Label_031D;
                    }
                    if ((bool)obj2)
                    {
                        parameter4.Value = "Y";
                    }
                    else
                    {
                        parameter4.Value = "N";
                    }
                    goto Label_0328;
                Label_02FF:
                    time = (DateTime)obj2;
                    parameter4.Value = time.ToString("yyyy/MM/dd hh:mm:ss");
                    goto Label_0328;
                Label_031D:
                    parameter4.Value = obj2;
                Label_0328:
                    parameter4.OracleType = mapping2.OracleType;
                    this.dac.cmd.Parameters.Add(parameter4);
                }
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[0])
                {
                    property = item.GetType().GetProperty(mapping2.Second);
                    obj2 = property.GetValue(item, null);
                    if ((property == null) || (obj2 == null))
                    {
                        continue;
                    }
                    parameter4 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First)
                    };
                    typeCode = Type.GetTypeCode(property.GetType());
                    if (typeCode != TypeCode.Boolean)
                    {
                        if (typeCode == TypeCode.DateTime)
                        {
                            goto Label_045E;
                        }
                        goto Label_047C;
                    }
                    if ((bool)obj2)
                    {
                        parameter4.Value = "Y";
                    }
                    else
                    {
                        parameter4.Value = "N";
                    }
                    goto Label_0487;
                Label_045E:
                    time = (DateTime)obj2;
                    parameter4.Value = time.ToString("yyyy/MM/dd hh:mm:ss");
                    goto Label_0487;
                Label_047C:
                    parameter4.Value = obj2;
                Label_0487:
                    parameter4.OracleType = mapping2.OracleType;
                    this.dac.cmd.Parameters.Add(parameter4);
                }
                OracleParameter parameter7 = new OracleParameter
                {
                    ParameterName = "p_rc",
                    OracleType = OracleType.Cursor,
                    Direction = ParameterDirection.Output
                };
                this.dac.cmd.Parameters.Add(parameter7);
                this.dac.OpenConnection();
                reader = this.dac.cmd.ExecuteReader();
                if (reader.Read())
                {
                    ReaderToObject<T>(item, reader);
                }
                reader.Close();
            }
        }

        public void GetByKey(string spName, DataTable item)
        {
            IDataReader reader;
            DataRow row;
            int num;
            this.dac.cmd.Parameters.Clear();
            this.dac.cmd.CommandText = spName;
            this.dac.cmd.CommandType = CommandType.StoredProcedure;
            this.dac.cmd.Connection = this.dac.conn;
            if (this.dac.conn is SqlConnection)
            {
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    if (item.Rows[0][mapping.Second] != DBNull.Value)
                    {
                        SqlParameter parameter = new SqlParameter
                        {
                            ParameterName = mapping.First,
                            Value = item.Rows[0][mapping.Second]
                        };
                        this.dac.cmd.Parameters.Add(parameter);
                    }
                }
                this.dac.OpenConnection();
                reader = this.dac.cmd.ExecuteReader();
                item.Rows.Clear();
                while (reader.Read())
                {
                    row = item.NewRow();
                    foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[0])
                    {
                        num = 0;
                        while (num < reader.FieldCount)
                        {
                            if (reader.GetName(num).ToLower() == mapping.First.ToLower())
                            {
                                row[mapping.Second] = reader.GetValue(num);
                            }
                            num++;
                        }
                    }
                    item.Rows.Add(row);
                }
                reader.Close();
            }
            if (this.dac.conn is OracleConnection)
            {
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    if (item.Rows[0][mapping2.Second] != DBNull.Value)
                    {
                        OracleParameter parameter3 = new OracleParameter
                        {
                            ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First),
                            Value = item.Rows[0][mapping2.Second]
                        };
                        this.dac.cmd.Parameters.Add(parameter3);
                    }
                }
                OracleParameter parameter5 = new OracleParameter
                {
                    ParameterName = "p_rc",
                    OracleType = OracleType.Cursor,
                    Direction = ParameterDirection.Output
                };
                this.dac.cmd.Parameters.Add(parameter5);
                this.dac.OpenConnection();
                reader = this.dac.cmd.ExecuteReader();
                item.Rows.Clear();
                while (reader.Read())
                {
                    row = item.NewRow();
                    foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[0])
                    {
                        for (num = 0; num < reader.FieldCount; num++)
                        {
                            if (reader.GetName(num).ToLower() == mapping2.First.ToLower())
                            {
                                if (item.Columns[mapping2.Second].DataType == typeof(bool))
                                {
                                    if (reader.GetValue(num).ToString().ToUpper() == "Y")
                                    {
                                        row[mapping2.Second] = true;
                                    }
                                    else
                                    {
                                        row[mapping2.Second] = false;
                                    }
                                }
                                else
                                {
                                    row[mapping2.Second] = reader.GetValue(num);
                                }
                            }
                        }
                    }
                    item.Rows.Add(row);
                }
                reader.Close();
            }
            if (item.Rows.Count == 0)
            {
                item.Rows.Add(item.NewRow());
            }
        }

        public void GetByKey<T>(string spName, T item)
        {
            PropertyInfo property;
            object obj2;
            IDataReader reader;
            this.GenereateSqlCommand(spName);
            if (this.dac.conn is SqlConnection)
            {
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    property = item.GetType().GetProperty(mapping.Second);
                    obj2 = property.GetValue(item, null);
                    if ((property != null) && (obj2 != null))
                    {
                        SqlParameter parameter = new SqlParameter
                        {
                            ParameterName = mapping.First,
                            Value = obj2
                        };
                        this.dac.cmd.Parameters.Add(parameter);
                    }
                }
                this.dac.OpenConnection();
                reader = this.dac.cmd.ExecuteReader();
                while (reader.Read())
                {
                    ReaderToObject<T>(item, reader);
                }
                reader.Close();
            }
            if (this.dac.conn is OracleConnection)
            {
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    property = item.GetType().GetProperty(mapping2.Second);
                    obj2 = property.GetValue(item, null);
                    if ((property != null) && (obj2 != null))
                    {
                        OracleParameter parameter3 = new OracleParameter
                        {
                            ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First),
                            Value = obj2
                        };
                        this.dac.cmd.Parameters.Add(parameter3);
                    }
                }
                OracleParameter parameter5 = new OracleParameter
                {
                    ParameterName = "p_rc",
                    OracleType = OracleType.Cursor,
                    Direction = ParameterDirection.Output
                };
                this.dac.cmd.Parameters.Add(parameter5);
                this.dac.OpenConnection();
                reader = this.dac.cmd.ExecuteReader();
                while (reader.Read())
                {
                    ReaderToObject<T>(item, reader);
                }
                reader.Close();
            }
        }

        public static DataTable GetSchema(string schema)
        {
            DataTable table2;
            try
            {
                using (DbConnection connection = dbFactory.CreateConnection())
                {
                    connection.ConnectionString = connStringSetting.ConnectionString;
                    connection.Open();
                    table2 = connection.GetSchema(schema);
                }
            }
            catch (Exception exception)
            {
                throw new CommonException(exception, CommonDeclare.EnumExceptionLevel.ERROR);
            }
            return table2;
        }

        public void InsertEntity(string spName, DataTable item)
        {
            Dictionary<string, object> dictionary;
            string str2;
            this.dac.cmd.Parameters.Clear();
            string pkField = string.Empty;
            this.dac.cmd.CommandText = spName;
            this.dac.cmd.CommandType = CommandType.StoredProcedure;
            if (this.dac.conn is SqlConnection)
            {
                SqlParameter parameter;
                dictionary = new Dictionary<string, object>();
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    pkField = mapping.First.Substring(1);
                    parameter = new SqlParameter
                    {
                        ParameterName = mapping.First,
                        Value = item.Rows[0][mapping.Second],
                        Direction = ParameterDirection.Output,
                        Size = mapping.Size
                    };
                    parameter.SqlDbType = mapping.SqlType;
                    this.dac.cmd.Parameters.Add(parameter);
                }
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[1])
                {
                    parameter = new SqlParameter
                    {
                        ParameterName = mapping.First
                    };
                    if (mapping.SqlType != SqlDbType.VarBinary)
                    {
                        parameter.Value = item.Rows[0][mapping.Second].Equals(string.Empty) ? DBNull.Value : item.Rows[0][mapping.Second];
                        parameter.Size = mapping.Size;
                        parameter.SqlDbType = mapping.SqlType;
                    }
                    else
                    {
                        if (item.Rows[0][mapping.Second].Equals(string.Empty))
                        {
                            parameter.Value = item.Rows[0][mapping.Second];
                        }
                        else
                        {
                            parameter.Value = new byte[0];
                        }
                        parameter.SqlDbType = mapping.SqlType;
                        dictionary.Add(mapping.First.Substring(1), item.Rows[0][mapping.Second]);
                    }
                    this.dac.cmd.Parameters.Add(parameter);
                }
                SqlParameter parameter4 = new SqlParameter
                {
                    ParameterName = "@LoginAccount",
                    Value = "admin",
                    Direction = ParameterDirection.Input,
                    Size = 0x80
                };
                this.dac.cmd.Parameters.Add(parameter4);
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
                str2 = string.Empty;
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    item.Rows[0][mapping.Second] = str2 = (this.dac.cmd as SqlCommand).Parameters[mapping.First].Value.ToString();
                }
                if (dictionary.Count > 0)
                {
                    foreach (string str3 in dictionary.Keys)
                    {
                        this.UpdateSqlLobField(item.TableName.Substring(0, item.TableName.Length - 6), pkField, str2, str3, dictionary[str3]);
                    }
                }
            }
            if (this.dac.conn is OracleConnection)
            {
                OracleParameter parameter6;
                dictionary = new Dictionary<string, object>();
                OracleCommand cmd = this.dac.cmd as OracleCommand;
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    parameter6 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(cmd.CommandType, mapping2.First),
                        Value = item.Rows[0][mapping2.Second],
                        OracleType = mapping2.OracleType,
                        Size = mapping2.Size,
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(parameter6);
                    pkField = OracleParameterHelper.FormatParameter(CommandType.Text, mapping2.First);
                }
                Dictionary<string, OracleType> dictionary2 = new Dictionary<string, OracleType>();
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[1])
                {
                    parameter6 = dbFactory.CreateParameter() as OracleParameter;
                    parameter6.ParameterName = OracleParameterHelper.FormatParameter(cmd.CommandType, mapping2.First);
                    parameter6.OracleType = mapping2.OracleType;
                    parameter6.Size = mapping2.Size;
                    if (item.Columns[mapping2.Second].DataType == typeof(bool))
                    {
                        if ((item.Rows[0][mapping2.Second] != DBNull.Value) && ((bool)item.Rows[0][mapping2.Second]))
                        {
                            parameter6.Value = "Y";
                        }
                        else
                        {
                            parameter6.Value = "N";
                        }
                    }
                    else if (mapping2.Size < 0x100000)
                    {
                        parameter6.Value = item.Rows[0][mapping2.Second];
                    }
                    else
                    {
                        dictionary.Add(OracleParameterHelper.FormatParameter(CommandType.Text, mapping2.First), item.Rows[0][mapping2.Second]);
                        dictionary2.Add(OracleParameterHelper.FormatParameter(CommandType.Text, mapping2.First), mapping2.OracleType);
                    }
                    cmd.Parameters.Add(parameter6);
                }
                this.dac.OpenConnection();
                cmd.ExecuteNonQuery();
                str2 = string.Empty;
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    item.Rows[0][mapping2.Second] = str2 = cmd.Parameters[mapping2.First].Value.ToString();
                }
                if (dictionary.Count > 0)
                {
                    foreach (string str3 in dictionary.Keys)
                    {
                        this.UpdateOracleLobField(item.TableName.Substring(0, item.TableName.Length - 6), pkField, str2, dictionary2[str3], str3, dictionary[str3]);
                    }
                }
            }
        }

        public void InsertEntity<T>(string spName, T item)
        {
            Dictionary<string, object> dictionary;
            PropertyInfo property;
            object obj2;
            PropertyInfo info2;
            this.GenereateSqlCommand(spName);
            string pkField = string.Empty;
            if (this.dac.conn is SqlConnection)
            {
                SqlParameter parameter;
                dictionary = new Dictionary<string, object>();
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    property = item.GetType().GetProperty(mapping.Second);
                    pkField = mapping.First.Substring(1);
                    parameter = new SqlParameter
                    {
                        ParameterName = mapping.First,
                        Direction = ParameterDirection.Output,
                        Size = mapping.Size,
                        Value = DBNull.Value
                    };
                    parameter.SqlDbType = mapping.SqlType;
                    this.dac.cmd.Parameters.Add(parameter);
                }
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[1])
                {
                    obj2 = item.GetType().GetProperty(mapping.Second).GetValue(item, null);
                    parameter = new SqlParameter
                    {
                        ParameterName = mapping.First
                    };
                    if (mapping.SqlType != SqlDbType.VarBinary)
                    {
                        parameter.Value = (obj2 == null) ? DBNull.Value : obj2;
                        parameter.Size = mapping.Size;
                        parameter.SqlDbType = mapping.SqlType;
                    }
                    else
                    {
                        parameter.SqlDbType = mapping.SqlType;
                        if (obj2 == null)
                        {
                            parameter.Value = DBNull.Value;
                        }
                        else
                        {
                            parameter.Value = new byte[0];
                            dictionary.Add(mapping.First.Substring(1), obj2);
                        }
                    }
                    this.dac.cmd.Parameters.Add(parameter);
                }
                SqlParameter parameter4 = new SqlParameter
                {
                    ParameterName = "@LoginAccount",
                    Value = "admin",
                    Direction = ParameterDirection.Input,
                    Size = 0x80
                };
                this.dac.cmd.Parameters.Add(parameter4);
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
                object obj3 = null;
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    property = item.GetType().GetProperty(mapping.Second);
                    obj3 = (this.dac.cmd as SqlCommand).Parameters[mapping.First].Value;
                    property.SetValue(item, obj3, null);
                }
                if (dictionary.Count > 0)
                {
                    foreach (string str2 in dictionary.Keys)
                    {
                        info2 = typeof(T).GetProperty("EntityName");
                        this.UpdateSqlLobField(info2.GetValue(item, null).ToString(), pkField, obj3, str2, dictionary[str2]);
                    }
                }
            }
            if (this.dac.conn is OracleConnection)
            {
                OracleParameter parameter6;
                dictionary = new Dictionary<string, object>();
                OracleCommand cmd = this.dac.cmd as OracleCommand;
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    parameter6 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(cmd.CommandType, mapping2.First),
                        OracleType = mapping2.OracleType,
                        Size = mapping2.Size,
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(parameter6);
                    pkField = OracleParameterHelper.FormatParameter(CommandType.Text, mapping2.First);
                }
                Dictionary<string, OracleType> dictionary2 = new Dictionary<string, OracleType>();
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[1])
                {
                    property = item.GetType().GetProperty(mapping2.Second);
                    obj2 = property.GetValue(item, null);
                    parameter6 = dbFactory.CreateParameter() as OracleParameter;
                    parameter6.ParameterName = OracleParameterHelper.FormatParameter(cmd.CommandType, mapping2.First);
                    parameter6.OracleType = mapping2.OracleType;
                    parameter6.Size = mapping2.Size;
                    if (property.GetType().FullName == typeof(bool).FullName)
                    {
                        if ((obj2 != null) && ((bool)obj2))
                        {
                            parameter6.Value = "Y";
                        }
                        else
                        {
                            parameter6.Value = "N";
                        }
                    }
                    else if (mapping2.Size < 0x100000)
                    {
                        parameter6.Value = obj2;
                    }
                    else
                    {
                        dictionary.Add(OracleParameterHelper.FormatParameter(CommandType.Text, mapping2.First), obj2);
                        dictionary2.Add(OracleParameterHelper.FormatParameter(CommandType.Text, mapping2.First), mapping2.OracleType);
                    }
                    cmd.Parameters.Add(parameter6);
                }
                this.dac.OpenConnection();
                cmd.ExecuteNonQuery();
                string str3 = string.Empty;
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    property = item.GetType().GetProperty(mapping2.Second);
                    str3 = cmd.Parameters[mapping2.First].Value.ToString();
                    property.SetValue(item, str3, null);
                }
                if (dictionary.Count > 0)
                {
                    foreach (string str2 in dictionary.Keys)
                    {
                        info2 = typeof(T).GetProperty("EntityName");
                        this.UpdateOracleLobField(info2.GetValue(item, null).ToString(), pkField, str3, dictionary2[str2], str2, dictionary[str2]);
                    }
                }
            }
        }

        public void InsertEntity(string spName, DataTable item, bool useExtendArgs)
        {
            Dictionary<string, object> dictionary;
            string str3;
            this.dac.cmd.Parameters.Clear();
            string pkField = string.Empty;
            this.dac.cmd.CommandText = spName;
            this.dac.cmd.CommandType = CommandType.StoredProcedure;
            if (this.dac.conn is SqlConnection)
            {
                SqlParameter parameter;
                dictionary = new Dictionary<string, object>();
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    pkField = mapping.First.Substring(1);
                    parameter = new SqlParameter
                    {
                        ParameterName = mapping.First,
                        Value = item.Rows[0][mapping.Second],
                        Direction = ParameterDirection.Output,
                        Size = mapping.Size
                    };
                    parameter.SqlDbType = mapping.SqlType;
                    this.dac.cmd.Parameters.Add(parameter);
                }
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[1])
                {
                    parameter = new SqlParameter
                    {
                        ParameterName = mapping.First
                    };
                    if (mapping.SqlType != SqlDbType.VarBinary)
                    {
                        parameter.Value = item.Rows[0][mapping.Second].Equals(string.Empty) ? DBNull.Value : item.Rows[0][mapping.Second];
                        parameter.Size = mapping.Size;
                        parameter.SqlDbType = mapping.SqlType;
                    }
                    else
                    {
                        if (item.Rows[0][mapping.Second].Equals(string.Empty))
                        {
                            parameter.Value = item.Rows[0][mapping.Second];
                        }
                        else
                        {
                            parameter.Value = new byte[0];
                        }
                        parameter.SqlDbType = mapping.SqlType;
                        dictionary.Add(mapping.First.Substring(1), item.Rows[0][mapping.Second]);
                    }
                    this.dac.cmd.Parameters.Add(parameter);
                }
                SqlParameter parameter4 = new SqlParameter
                {
                    ParameterName = "@LoginAccount",
                    Value = "admin",
                    Direction = ParameterDirection.Input,
                    Size = 0x80
                };
                this.dac.cmd.Parameters.Add(parameter4);
                if (useExtendArgs)
                {
                    foreach (string str2 in item.ExtendedProperties.Keys)
                    {
                        SqlParameter parameter5 = new SqlParameter
                        {
                            ParameterName = "@" + str2,
                            Value = item.ExtendedProperties[str2] ?? string.Empty,
                            Direction = ParameterDirection.Input
                        };
                        this.dac.cmd.Parameters.Add(parameter5);
                    }
                }
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
                str3 = string.Empty;
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    item.Rows[0][mapping.Second] = str3 = (this.dac.cmd as SqlCommand).Parameters[mapping.First].Value.ToString();
                }
                if (dictionary.Count > 0)
                {
                    foreach (string str2 in dictionary.Keys)
                    {
                        this.UpdateSqlLobField(item.TableName.Substring(0, item.TableName.Length - 6), pkField, str3, str2, dictionary[str2]);
                    }
                }
            }
            if (this.dac.conn is OracleConnection)
            {
                OracleParameter parameter8;
                dictionary = new Dictionary<string, object>();
                OracleCommand cmd = this.dac.cmd as OracleCommand;
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    parameter8 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(cmd.CommandType, mapping2.First),
                        Value = item.Rows[0][mapping2.Second],
                        OracleType = mapping2.OracleType,
                        Size = mapping2.Size,
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(parameter8);
                    pkField = OracleParameterHelper.FormatParameter(CommandType.Text, mapping2.First);
                }
                Dictionary<string, OracleType> dictionary2 = new Dictionary<string, OracleType>();
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[1])
                {
                    parameter8 = dbFactory.CreateParameter() as OracleParameter;
                    parameter8.ParameterName = OracleParameterHelper.FormatParameter(cmd.CommandType, mapping2.First);
                    parameter8.OracleType = mapping2.OracleType;
                    parameter8.Size = mapping2.Size;
                    if (item.Columns[mapping2.Second].DataType == typeof(bool))
                    {
                        if ((item.Rows[0][mapping2.Second] != DBNull.Value) && ((bool)item.Rows[0][mapping2.Second]))
                        {
                            parameter8.Value = "Y";
                        }
                        else
                        {
                            parameter8.Value = "N";
                        }
                    }
                    else if (mapping2.Size < 0x100000)
                    {
                        parameter8.Value = item.Rows[0][mapping2.Second];
                    }
                    else
                    {
                        dictionary.Add(OracleParameterHelper.FormatParameter(CommandType.Text, mapping2.First), item.Rows[0][mapping2.Second]);
                        dictionary2.Add(OracleParameterHelper.FormatParameter(CommandType.Text, mapping2.First), mapping2.OracleType);
                    }
                    cmd.Parameters.Add(parameter8);
                }
                this.dac.OpenConnection();
                cmd.ExecuteNonQuery();
                str3 = string.Empty;
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    item.Rows[0][mapping2.Second] = str3 = cmd.Parameters[mapping2.First].Value.ToString();
                }
                if (dictionary.Count > 0)
                {
                    foreach (string str2 in dictionary.Keys)
                    {
                        this.UpdateOracleLobField(item.TableName.Substring(0, item.TableName.Length - 6), pkField, str3, dictionary2[str2], str2, dictionary[str2]);
                    }
                }
            }
        }

        public void InsertEntity(string spName, DataTable item, Dictionary<string, object> args)
        {
            Dictionary<string, object> dictionary;
            string str3;
            this.dac.cmd.Parameters.Clear();
            string pkField = string.Empty;
            this.dac.cmd.CommandText = spName;
            this.dac.cmd.CommandType = CommandType.StoredProcedure;
            if (this.dac.conn is SqlConnection)
            {
                SqlParameter parameter;
                dictionary = new Dictionary<string, object>();
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    pkField = mapping.First.Substring(1);
                    parameter = new SqlParameter
                    {
                        ParameterName = mapping.First,
                        Value = item.Rows[0][mapping.Second],
                        Direction = ParameterDirection.Output,
                        Size = mapping.Size
                    };
                    parameter.SqlDbType = mapping.SqlType;
                    this.dac.cmd.Parameters.Add(parameter);
                }
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[1])
                {
                    parameter = new SqlParameter
                    {
                        ParameterName = mapping.First
                    };
                    if (mapping.SqlType != SqlDbType.VarBinary)
                    {
                        parameter.Value = item.Rows[0][mapping.Second].Equals(string.Empty) ? DBNull.Value : item.Rows[0][mapping.Second];
                        parameter.Size = mapping.Size;
                        parameter.SqlDbType = mapping.SqlType;
                    }
                    else
                    {
                        if (item.Rows[0][mapping.Second].Equals(string.Empty))
                        {
                            parameter.Value = item.Rows[0][mapping.Second];
                        }
                        else
                        {
                            parameter.Value = new byte[0];
                        }
                        parameter.SqlDbType = mapping.SqlType;
                        dictionary.Add(mapping.First.Substring(1), item.Rows[0][mapping.Second]);
                    }
                    this.dac.cmd.Parameters.Add(parameter);
                }
                foreach (string str2 in args.Keys)
                {
                    SqlParameter parameter4 = new SqlParameter
                    {
                        ParameterName = "@" + str2,
                        Value = args[str2],
                        Direction = ParameterDirection.Input
                    };
                    this.dac.cmd.Parameters.Add(parameter4);
                }
                SqlParameter parameter6 = new SqlParameter
                {
                    ParameterName = "@LoginAccount",
                    Value = "admin",
                    Direction = ParameterDirection.Input,
                    Size = 0x80
                };
                this.dac.cmd.Parameters.Add(parameter6);
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
                str3 = string.Empty;
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    item.Rows[0][mapping.Second] = str3 = (this.dac.cmd as SqlCommand).Parameters[mapping.First].Value.ToString();
                }
                if (dictionary.Count > 0)
                {
                    foreach (string str2 in dictionary.Keys)
                    {
                        this.UpdateSqlLobField(item.TableName.Substring(0, item.TableName.Length - 6), pkField, str3, str2, dictionary[str2]);
                    }
                }
            }
            if (this.dac.conn is OracleConnection)
            {
                OracleParameter parameter8;
                dictionary = new Dictionary<string, object>();
                OracleCommand cmd = this.dac.cmd as OracleCommand;
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    parameter8 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(cmd.CommandType, mapping2.First),
                        Value = item.Rows[0][mapping2.Second],
                        OracleType = mapping2.OracleType,
                        Size = mapping2.Size,
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(parameter8);
                    pkField = OracleParameterHelper.FormatParameter(CommandType.Text, mapping2.First);
                }
                Dictionary<string, OracleType> dictionary2 = new Dictionary<string, OracleType>();
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[1])
                {
                    parameter8 = dbFactory.CreateParameter() as OracleParameter;
                    parameter8.ParameterName = OracleParameterHelper.FormatParameter(cmd.CommandType, mapping2.First);
                    parameter8.OracleType = mapping2.OracleType;
                    parameter8.Size = mapping2.Size;
                    if (item.Columns[mapping2.Second].DataType == typeof(bool))
                    {
                        if ((item.Rows[0][mapping2.Second] != DBNull.Value) && ((bool)item.Rows[0][mapping2.Second]))
                        {
                            parameter8.Value = "Y";
                        }
                        else
                        {
                            parameter8.Value = "N";
                        }
                    }
                    else if (mapping2.Size < 0x100000)
                    {
                        parameter8.Value = item.Rows[0][mapping2.Second];
                    }
                    else
                    {
                        dictionary.Add(OracleParameterHelper.FormatParameter(CommandType.Text, mapping2.First), item.Rows[0][mapping2.Second]);
                        dictionary2.Add(OracleParameterHelper.FormatParameter(CommandType.Text, mapping2.First), mapping2.OracleType);
                    }
                    cmd.Parameters.Add(parameter8);
                }
                this.dac.OpenConnection();
                cmd.ExecuteNonQuery();
                str3 = string.Empty;
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    item.Rows[0][mapping2.Second] = str3 = cmd.Parameters[mapping2.First].Value.ToString();
                }
                if (dictionary.Count > 0)
                {
                    foreach (string str2 in dictionary.Keys)
                    {
                        this.UpdateOracleLobField(item.TableName.Substring(0, item.TableName.Length - 6), pkField, str3, dictionary2[str2], str2, dictionary[str2]);
                    }
                }
            }
        }

        public void InsertEntity<T>(string spName, T item, Dictionary<string, object> args)
        {
            Dictionary<string, object> dictionary;
            PropertyInfo property;
            object obj2;
            string str3;
            PropertyInfo info2;
            string pkField = string.Empty;
            this.GenereateSqlCommand(spName);
            if (this.dac.conn is SqlConnection)
            {
                SqlParameter parameter;
                dictionary = new Dictionary<string, object>();
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    pkField = mapping.First.Substring(1);
                    parameter = new SqlParameter
                    {
                        ParameterName = mapping.First,
                        Direction = ParameterDirection.Output,
                        Size = mapping.Size
                    };
                    parameter.SqlDbType = mapping.SqlType;
                    this.dac.cmd.Parameters.Add(parameter);
                }
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[1])
                {
                    obj2 = item.GetType().GetProperty(mapping.Second).GetValue(item, null);
                    parameter = new SqlParameter
                    {
                        ParameterName = mapping.First
                    };
                    if (mapping.SqlType != SqlDbType.VarBinary)
                    {
                        parameter.Value = (obj2 == null) ? DBNull.Value : obj2;
                        parameter.Size = mapping.Size;
                        parameter.SqlDbType = mapping.SqlType;
                    }
                    else
                    {
                        parameter.SqlDbType = mapping.SqlType;
                        if (obj2 == null)
                        {
                            parameter.Value = DBNull.Value;
                        }
                        else
                        {
                            parameter.Value = new byte[0];
                            dictionary.Add(mapping.First.Substring(1), obj2);
                        }
                    }
                    this.dac.cmd.Parameters.Add(parameter);
                }
                foreach (string str2 in args.Keys)
                {
                    SqlParameter parameter4 = new SqlParameter
                    {
                        ParameterName = "@" + str2,
                        Value = args[str2],
                        Direction = ParameterDirection.Input
                    };
                    this.dac.cmd.Parameters.Add(parameter4);
                }
                SqlParameter parameter6 = new SqlParameter
                {
                    ParameterName = "@LoginAccount",
                    Value = "admin",
                    Direction = ParameterDirection.Input,
                    Size = 0x80
                };
                this.dac.cmd.Parameters.Add(parameter6);
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
                str3 = string.Empty;
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    property = item.GetType().GetProperty(mapping.Second);
                    str3 = (this.dac.cmd as SqlCommand).Parameters[mapping.First].Value.ToString();
                    property.SetValue(item, str3, null);
                }
                if (dictionary.Count > 0)
                {
                    foreach (string str2 in dictionary.Keys)
                    {
                        info2 = typeof(T).GetProperty("EntityName");
                        this.UpdateSqlLobField(info2.GetValue(item, null).ToString(), pkField, str3, str2, dictionary[str2]);
                    }
                }
            }
            if (this.dac.conn is OracleConnection)
            {
                OracleParameter parameter8;
                dictionary = new Dictionary<string, object>();
                OracleCommand cmd = this.dac.cmd as OracleCommand;
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    parameter8 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(cmd.CommandType, mapping2.First),
                        OracleType = mapping2.OracleType,
                        Size = mapping2.Size,
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(parameter8);
                    pkField = OracleParameterHelper.FormatParameter(CommandType.Text, mapping2.First);
                }
                Dictionary<string, OracleType> dictionary2 = new Dictionary<string, OracleType>();
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[1])
                {
                    property = item.GetType().GetProperty(mapping2.Second);
                    obj2 = property.GetValue(item, null);
                    parameter8 = dbFactory.CreateParameter() as OracleParameter;
                    parameter8.ParameterName = OracleParameterHelper.FormatParameter(cmd.CommandType, mapping2.First);
                    parameter8.OracleType = mapping2.OracleType;
                    parameter8.Size = mapping2.Size;
                    if (property.GetType().FullName == typeof(bool).FullName)
                    {
                        if ((obj2 != null) && ((bool)obj2))
                        {
                            parameter8.Value = "Y";
                        }
                        else
                        {
                            parameter8.Value = "N";
                        }
                    }
                    else if (mapping2.Size < 0x100000)
                    {
                        parameter8.Value = obj2;
                    }
                    else
                    {
                        dictionary.Add(OracleParameterHelper.FormatParameter(CommandType.Text, mapping2.First), obj2);
                        dictionary2.Add(OracleParameterHelper.FormatParameter(CommandType.Text, mapping2.First), mapping2.OracleType);
                    }
                    cmd.Parameters.Add(parameter8);
                }
                this.dac.OpenConnection();
                cmd.ExecuteNonQuery();
                str3 = string.Empty;
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    property = item.GetType().GetProperty(mapping2.Second);
                    str3 = cmd.Parameters[mapping2.First].Value.ToString();
                    property.SetValue(item, str3, null);
                }
                if (dictionary.Count > 0)
                {
                    foreach (string str2 in dictionary.Keys)
                    {
                        info2 = typeof(T).GetProperty("EntityName");
                        this.UpdateOracleLobField(info2.GetValue(item, null).ToString(), pkField, str3, dictionary2[str2], str2, dictionary[str2]);
                    }
                }
            }
        }

        public void InsertEntity<T>(string spName, T item, Dictionary<string, object> args, Dictionary<string, object> outputArgs)
        {
            Dictionary<string, object> dictionary;
            PropertyInfo property;
            object obj2;
            PropertyInfo info2;
            IEnumerator enumerator;

            this.GenereateSqlCommand(spName);
            string pkField = string.Empty;
            if (this.dac.conn is SqlConnection)
            {
                HyBy.FrameWork.DAService.SqlDBMapping current;
                SqlParameter parameter;
                dictionary = new Dictionary<string, object>();
                enumerator = this.SqlDBMapping[2].GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        current = (HyBy.FrameWork.DAService.SqlDBMapping)enumerator.Current;
                        property = item.GetType().GetProperty(current.Second);
                        pkField = current.First.Substring(1);
                        parameter = new SqlParameter
                        {
                            ParameterName = current.First,
                            Direction = ParameterDirection.Output,
                            Size = current.Size,
                            Value = DBNull.Value
                        };
                        parameter.SqlDbType = current.SqlType;
                        this.dac.cmd.Parameters.Add(parameter);
                    }
                }
                catch (Exception e) { throw e; }
                finally
                {
                    enumerator = null;
                }
                foreach (string str3 in outputArgs.Keys)
                {
                    current = (HyBy.FrameWork.DAService.SqlDBMapping)this.SqlDBMapping[1].GetBySecond(str3);
                    SqlParameter parameter3 = new SqlParameter
                    {
                        ParameterName = (str3.IndexOf("@") == 0) ? str3 : string.Format("@{0}", str3),
                        Value = DBNull.Value,
                        Direction = ParameterDirection.Output,
                        SqlDbType = current.SqlType,
                        Size = current.Size
                    };
                    this.dac.cmd.Parameters.Add(parameter3);
                }
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[1])
                {
                    if (!outputArgs.ContainsKey(mapping.Second))
                    {
                        obj2 = item.GetType().GetProperty(mapping.Second).GetValue(item, null);
                        parameter = new SqlParameter
                        {
                            ParameterName = mapping.First
                        };
                        if (mapping.SqlType != SqlDbType.VarBinary)
                        {
                            parameter.Value = (obj2 == null) ? DBNull.Value : obj2;
                            parameter.Size = mapping.Size;
                            parameter.SqlDbType = mapping.SqlType;
                        }
                        else
                        {
                            parameter.SqlDbType = mapping.SqlType;
                            if (obj2 == null)
                            {
                                parameter.Value = DBNull.Value;
                            }
                            else
                            {
                                parameter.Value = new byte[0];
                                dictionary.Add(mapping.First.Substring(1), obj2);
                            }
                        }
                        this.dac.cmd.Parameters.Add(parameter);
                    }
                }
                SqlParameter parameter6 = new SqlParameter
                {
                    ParameterName = "@LoginAccount",
                    Value = "admin",
                    Direction = ParameterDirection.Input,
                    Size = 0x80
                };
                this.dac.cmd.Parameters.Add(parameter6);
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
                object obj3 = null;
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    property = item.GetType().GetProperty(mapping.Second);
                    obj3 = (this.dac.cmd as SqlCommand).Parameters[mapping.First].Value;
                    property.SetValue(item, obj3, null);
                }
                foreach (string str3 in outputArgs.Keys)
                {
                    property = item.GetType().GetProperty(str3);
                    obj3 = (this.dac.cmd as SqlCommand).Parameters["@" + str3].Value;
                    property.SetValue(item, obj3, null);
                }
                if (dictionary.Count > 0)
                {
                    foreach (string str3 in dictionary.Keys)
                    {
                        info2 = typeof(T).GetProperty("EntityName");
                        this.UpdateSqlLobField(info2.GetValue(item, null).ToString(), pkField, obj3, str3, dictionary[str3]);
                    }
                }
            }
            if (this.dac.conn is OracleConnection)
            {
                HyBy.FrameWork.DAService.OracleDBMapping bySecond;
                OracleParameter parameter8;
                dictionary = new Dictionary<string, object>();
                OracleCommand cmd = this.dac.cmd as OracleCommand;
                enumerator = this.OracleDBMapping[2].GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        bySecond = (HyBy.FrameWork.DAService.OracleDBMapping)enumerator.Current;
                        parameter8 = new OracleParameter
                        {
                            ParameterName = OracleParameterHelper.FormatParameter(cmd.CommandType, bySecond.First),
                            OracleType = bySecond.OracleType,
                            Size = bySecond.Size,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(parameter8);
                        pkField = OracleParameterHelper.FormatParameter(CommandType.Text, bySecond.First);
                    }
                }
                catch (Exception e) { throw e; }
                finally
                {
                    enumerator = null;
                }

                foreach (string str3 in outputArgs.Keys)
                {
                    bySecond = (HyBy.FrameWork.DAService.OracleDBMapping)this.SqlDBMapping[1].GetBySecond(str3);
                    parameter8 = new OracleParameter
                    {
                        ParameterName = (str3.IndexOf("@") == 0) ? str3 : string.Format("@{0}", str3),
                        Value = DBNull.Value,
                        Direction = ParameterDirection.Output,
                        OracleType = bySecond.OracleType,
                        Size = bySecond.Size
                    };
                    this.dac.cmd.Parameters.Add(parameter8);
                }
                Dictionary<string, OracleType> dictionary2 = new Dictionary<string, OracleType>();
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[1])
                {
                    if (!outputArgs.ContainsKey(mapping2.Second))
                    {
                        property = item.GetType().GetProperty(mapping2.Second);
                        obj2 = property.GetValue(item, null);
                        parameter8 = dbFactory.CreateParameter() as OracleParameter;
                        parameter8.ParameterName = OracleParameterHelper.FormatParameter(cmd.CommandType, mapping2.First);
                        parameter8.OracleType = mapping2.OracleType;
                        parameter8.Size = mapping2.Size;
                        if (property.GetType().FullName == typeof(bool).FullName)
                        {
                            if ((obj2 != null) && ((bool)obj2))
                            {
                                parameter8.Value = "Y";
                            }
                            else
                            {
                                parameter8.Value = "N";
                            }
                        }
                        else if (mapping2.Size < 0x100000)
                        {
                            parameter8.Value = obj2;
                        }
                        else
                        {
                            dictionary.Add(OracleParameterHelper.FormatParameter(CommandType.Text, mapping2.First), obj2);
                            dictionary2.Add(OracleParameterHelper.FormatParameter(CommandType.Text, mapping2.First), mapping2.OracleType);
                        }
                        cmd.Parameters.Add(parameter8);
                    }
                }
                this.dac.OpenConnection();
                cmd.ExecuteNonQuery();
                string str4 = string.Empty;
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    property = item.GetType().GetProperty(mapping2.Second);
                    str4 = cmd.Parameters[mapping2.First].Value.ToString();
                    property.SetValue(item, str4, null);
                }
                foreach (string str3 in outputArgs.Keys)
                {
                    property = item.GetType().GetProperty(str3);
                    str4 = cmd.Parameters["@" + str3].Value.ToString();
                    property.SetValue(item, str4, null);
                }
                if (dictionary.Count > 0)
                {
                    foreach (string str3 in dictionary.Keys)
                    {
                        info2 = typeof(T).GetProperty("EntityName");
                        this.UpdateOracleLobField(info2.GetValue(item, null).ToString(), pkField, str4, dictionary2[str3], str3, dictionary[str3]);
                    }
                }
            }
        }

        private static void ReaderToObject<T>(T item, IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                PropertyInfo property = item.GetType().GetProperty(reader.GetName(i));
                if ((property != null) && (reader.GetValue(i) != DBNull.Value))
                {
                    if (property.PropertyType.IsEnum)
                    {
                        property.SetValue(item, Enum.ToObject(property.PropertyType, reader.GetValue(i)), null);
                    }
                    else if (property.PropertyType.FullName.IndexOf("Boolean") > -1)
                    {
                        property.SetValue(item, reader.GetBoolean(i), null);
                    }
                    else
                    {
                        property.SetValue(item, reader.GetValue(i), null);
                    }
                }
            }
        }

        private static T ReaderToObjectForCollection<T>(T item, IDataReader reader)
        {
            item = Activator.CreateInstance<T>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                PropertyInfo property = item.GetType().GetProperty(reader.GetName(i));
                if ((property != null) && (reader.GetValue(i) != DBNull.Value))
                {
                    if (property.PropertyType.IsEnum)
                    {
                        property.SetValue(item, Enum.ToObject(property.PropertyType, reader.GetValue(i)), null);
                    }
                    else if (property.PropertyType.FullName.IndexOf("Boolean") > -1)
                    {
                        property.SetValue(item, reader.GetBoolean(i), null);
                    }
                    else
                    {
                        property.SetValue(item, reader.GetValue(i), null);
                    }
                }
            }
            return item;
        }

        public void RemoveByField(string spName, DataTable item)
        {
            this.dac.cmd.Parameters.Clear();
            this.dac.cmd.CommandText = spName;
            this.dac.cmd.CommandType = CommandType.StoredProcedure;
            this.dac.cmd.Connection = this.dac.conn;
            this.dac.OpenConnection();
            if (this.dac.conn is SqlConnection)
            {
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[1])
                {
                    if (item.Rows[0][mapping.Second] != DBNull.Value)
                    {
                        SqlParameter parameter = new SqlParameter
                        {
                            ParameterName = mapping.First,
                            Value = item.Rows[0][mapping.Second],
                            Size = mapping.Size
                        };
                        this.dac.cmd.Parameters.Add(parameter);
                    }
                }
            }
            if (this.dac.conn is OracleConnection)
            {
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[1])
                {
                    DateTime time;
                    if (item.Rows[0][mapping2.Second] == DBNull.Value)
                    {
                        continue;
                    }
                    OracleParameter parameter3 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First)
                    };
                    TypeCode typeCode = Type.GetTypeCode(item.Columns[mapping2.Second].DataType);
                    if (typeCode != TypeCode.Boolean)
                    {
                        if (typeCode == TypeCode.DateTime)
                        {
                            goto Label_025B;
                        }
                        goto Label_028F;
                    }
                    if ((bool)item.Rows[0][mapping2.Second])
                    {
                        parameter3.Value = "Y";
                    }
                    else
                    {
                        parameter3.Value = "N";
                    }
                    goto Label_02B0;
                Label_025B:
                    time = (DateTime)item.Rows[0][mapping2.Second];
                    parameter3.Value = time.ToString("yyyy/MM/dd hh:mm:ss");
                    goto Label_02B0;
                Label_028F:
                    parameter3.Value = item.Rows[0][mapping2.Second];
                Label_02B0:
                    parameter3.OracleType = mapping2.OracleType;
                    this.dac.cmd.Parameters.Add(parameter3);
                }
            }
            this.dac.cmd.ExecuteNonQuery();
        }

        public void RemoveByField<T>(string spName, T item)
        {
            object obj2;
            this.GenereateSqlCommand(spName);
            if (this.dac.conn is SqlConnection)
            {
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[1])
                {
                    obj2 = item.GetType().GetProperty(mapping.Second).GetValue(item, null);
                    if (obj2 != null)
                    {
                        SqlParameter parameter = new SqlParameter
                        {
                            ParameterName = mapping.First,
                            Value = obj2,
                            Size = mapping.Size
                        };
                        this.dac.cmd.Parameters.Add(parameter);
                    }
                }
            }
            if (this.dac.conn is OracleConnection)
            {
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[1])
                {
                    DateTime time;
                    PropertyInfo property = item.GetType().GetProperty(mapping2.Second);
                    obj2 = property.GetValue(item, null);
                    if (obj2 == null)
                    {
                        continue;
                    }
                    OracleParameter parameter3 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First)
                    };
                    TypeCode typeCode = Type.GetTypeCode(property.GetType());
                    if (typeCode != TypeCode.Boolean)
                    {
                        if (typeCode == TypeCode.DateTime)
                        {
                            goto Label_01E7;
                        }
                        goto Label_0205;
                    }
                    if ((bool)obj2)
                    {
                        parameter3.Value = "Y";
                    }
                    else
                    {
                        parameter3.Value = "N";
                    }
                    goto Label_0210;
                Label_01E7:
                    time = (DateTime)obj2;
                    parameter3.Value = time.ToString("yyyy/MM/dd hh:mm:ss");
                    goto Label_0210;
                Label_0205:
                    parameter3.Value = obj2;
                Label_0210:
                    parameter3.OracleType = mapping2.OracleType;
                    this.dac.cmd.Parameters.Add(parameter3);
                }
            }
            this.dac.OpenConnection();
            this.dac.cmd.ExecuteNonQuery();
        }

        public void RemoveByKey(string spName, DataTable item)
        {
            this.dac.cmd.Parameters.Clear();
            this.dac.cmd.CommandText = spName;
            this.dac.cmd.CommandType = CommandType.StoredProcedure;
            this.dac.cmd.Connection = this.dac.conn;
            this.dac.OpenConnection();
            if (this.dac.conn is SqlConnection)
            {
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = mapping.First,
                        Value = item.Rows[0][mapping.Second]
                    };
                    this.dac.cmd.Parameters.Add(parameter);
                }
            }
            if (this.dac.conn is OracleConnection)
            {
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    OracleParameter parameter3 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First),
                        OracleType = mapping2.OracleType,
                        Value = item.Rows[0][mapping2.Second]
                    };
                    this.dac.cmd.Parameters.Add(parameter3);
                }
            }
            this.dac.cmd.ExecuteNonQuery();
        }

        public void RemoveByKey<T>(string spName, T item)
        {
            object obj2;
            this.GenereateSqlCommand(spName);
            if (this.dac.conn is SqlConnection)
            {
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    obj2 = item.GetType().GetProperty(mapping.Second).GetValue(item, null);
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = mapping.First,
                        Value = obj2
                    };
                    this.dac.cmd.Parameters.Add(parameter);
                }
            }
            if (this.dac.conn is OracleConnection)
            {
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    obj2 = item.GetType().GetProperty(mapping2.Second).GetValue(item, null);
                    OracleParameter parameter3 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First),
                        OracleType = mapping2.OracleType,
                        Value = obj2
                    };
                    this.dac.cmd.Parameters.Add(parameter3);
                }
            }
            this.dac.OpenConnection();
            this.dac.cmd.ExecuteNonQuery();
        }

        public void UpdateByField(string spName, DataTable item)
        {
            Dictionary<string, object> dictionary;
            string str2;
            this.dac.cmd.Parameters.Clear();
            this.dac.cmd.CommandText = spName;
            this.dac.cmd.CommandType = CommandType.StoredProcedure;
            this.dac.cmd.Connection = this.dac.conn;
            string pkField = string.Empty;
            if (this.dac.conn is SqlConnection)
            {
                SqlParameter parameter;
                dictionary = new Dictionary<string, object>();
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[1])
                {
                    if (item.Rows[0][mapping.Second] != DBNull.Value)
                    {
                        parameter = new SqlParameter
                        {
                            ParameterName = mapping.First
                        };
                        if (mapping.SqlType != SqlDbType.VarBinary)
                        {
                            parameter.Value = item.Rows[0][mapping.Second].Equals(string.Empty) ? DBNull.Value : item.Rows[0][mapping.Second];
                            if (mapping.Size > 0)
                            {
                                parameter.Size = mapping.Size;
                            }
                        }
                        else
                        {
                            if (item.Rows[0][mapping.Second].Equals(string.Empty))
                            {
                                parameter.Value = item.Rows[0][mapping.Second];
                            }
                            else
                            {
                                parameter.Value = new byte[0];
                            }
                            if (mapping.Size > 0)
                            {
                                parameter.Size = mapping.Size;
                            }
                            parameter.SqlDbType = mapping.SqlType;
                            dictionary.Add(mapping.First.Substring(1), item.Rows[0][mapping.Second]);
                        }
                        this.dac.cmd.Parameters.Add(parameter);
                    }
                }
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    pkField = mapping.First.Substring(1);
                    parameter = new SqlParameter
                    {
                        ParameterName = mapping.First,
                        Value = item.Rows[0][mapping.Second].Equals(string.Empty) ? DBNull.Value : item.Rows[0][mapping.Second],
                        Size = mapping.Size
                    };
                    this.dac.cmd.Parameters.Add(parameter);
                }
                if ((spName == "UpdateE_CustomerLoginInfoEntityForFailed") || (spName == "UpdateE_CustomerLoginInfoEntityForSuccess"))
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (SqlParameter param in this.dac.cmd.Parameters)
                    {
                        builder.AppendFormat("{0}={1}\r\n", param.ParameterName, param.Value);
                    }
                    ExceptionToMessageHelper.WriteLog(builder.ToString());
                }
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
                if (dictionary.Count > 0)
                {
                    str2 = string.Empty;
                    foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                    {
                        item.Rows[0][mapping.Second] = str2 = (this.dac.cmd as SqlCommand).Parameters[mapping.First].Value.ToString();
                    }
                    foreach (string str3 in dictionary.Keys)
                    {
                        this.UpdateSqlLobField(item.TableName.Substring(0, item.TableName.Length - 6), pkField, str2, str3, dictionary[str3]);
                    }
                }
            }
            if (this.dac.conn is OracleConnection)
            {
                OracleParameter parameter4;
                dictionary = new Dictionary<string, object>();
                Dictionary<string, OracleType> dictionary2 = new Dictionary<string, OracleType>();
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[1])
                {
                    if (item.Rows[0][mapping2.Second] != DBNull.Value)
                    {
                        parameter4 = new OracleParameter
                        {
                            ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First),
                            OracleType = mapping2.OracleType
                        };
                        if (item.Columns[mapping2.Second].DataType == typeof(bool))
                        {
                            if ((item.Rows[0][mapping2.Second] != DBNull.Value) && ((bool)item.Rows[0][mapping2.Second]))
                            {
                                parameter4.Value = "Y";
                            }
                            else
                            {
                                parameter4.Value = "N";
                            }
                        }
                        else if (parameter4.Size > -1)
                        {
                            parameter4.Value = item.Rows[0][mapping2.Second];
                        }
                        else
                        {
                            dictionary.Add(OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First), item.Rows[0][mapping2.Second]);
                            dictionary2.Add(OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First), mapping2.OracleType);
                        }
                        this.dac.cmd.Parameters.Add(parameter4);
                    }
                }
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    parameter4 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First),
                        OracleType = mapping2.OracleType,
                        Value = item.Rows[0][mapping2.Second]
                    };
                    this.dac.cmd.Parameters.Add(parameter4);
                }
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
                if (dictionary.Count > 0)
                {
                    str2 = string.Empty;
                    foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                    {
                        item.Rows[0][mapping2.Second] = str2 = (this.dac.cmd as OracleCommand).Parameters[mapping2.First].Value.ToString();
                    }
                    foreach (string str3 in dictionary.Keys)
                    {
                        this.UpdateOracleLobField(item.TableName.Substring(0, item.TableName.Length - 6), pkField, str2, dictionary2[str3], str3, dictionary[str3]);
                    }
                }
            }
        }

        public void UpdateByField<T>(string spName, T item)
        {
            Dictionary<string, object> dictionary;
            PropertyInfo property;
            object obj2;
            string str2;
            PropertyInfo info2;
            string pkField = string.Empty;
            this.GenereateSqlCommand(spName);
            if (this.dac.conn is SqlConnection)
            {
                SqlParameter parameter;
                dictionary = new Dictionary<string, object>();
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[1])
                {
                    obj2 = item.GetType().GetProperty(mapping.Second).GetValue(item, null);
                    if (obj2 != null)
                    {
                        parameter = new SqlParameter
                        {
                            ParameterName = mapping.First
                        };
                        if (mapping.SqlType != SqlDbType.VarBinary)
                        {
                            parameter.Value = (obj2 == null) ? DBNull.Value : obj2;
                            parameter.Size = mapping.Size;
                        }
                        else
                        {
                            if (obj2 == null)
                            {
                                parameter.Value = DBNull.Value;
                            }
                            else
                            {
                                parameter.Value = new byte[0];
                            }
                            parameter.Size = mapping.Size;
                            parameter.SqlDbType = mapping.SqlType;
                            dictionary.Add(mapping.First.Substring(1), obj2);
                        }
                        this.dac.cmd.Parameters.Add(parameter);
                    }
                }
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    obj2 = item.GetType().GetProperty(mapping.Second).GetValue(item, null);
                    if (obj2 != null)
                    {
                        pkField = mapping.First.Substring(1);
                        parameter = new SqlParameter
                        {
                            ParameterName = mapping.First,
                            Value = (obj2 == null) ? DBNull.Value : obj2,
                            Size = mapping.Size
                        };
                        this.dac.cmd.Parameters.Add(parameter);
                    }
                }
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
                if (dictionary.Count > 0)
                {
                    str2 = string.Empty;
                    foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                    {
                        property = item.GetType().GetProperty(mapping.Second);
                        str2 = (this.dac.cmd as SqlCommand).Parameters[mapping.First].Value.ToString();
                        property.SetValue(item, str2, null);
                    }
                    foreach (string str3 in dictionary.Keys)
                    {
                        info2 = typeof(T).GetProperty("EntityName");
                        this.UpdateSqlLobField(info2.GetValue(item, null).ToString(), pkField, str2, str3, dictionary[str3]);
                    }
                }
            }
            if (this.dac.conn is OracleConnection)
            {
                OracleParameter parameter4;
                dictionary = new Dictionary<string, object>();
                Dictionary<string, OracleType> dictionary2 = new Dictionary<string, OracleType>();
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[1])
                {
                    property = item.GetType().GetProperty(mapping2.Second);
                    obj2 = property.GetValue(item, null);
                    if (obj2 != null)
                    {
                        parameter4 = new OracleParameter
                        {
                            ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First),
                            OracleType = mapping2.OracleType
                        };
                        if (property.GetType().FullName == typeof(bool).FullName)
                        {
                            if ((obj2 != null) && ((bool)obj2))
                            {
                                parameter4.Value = "Y";
                            }
                            else
                            {
                                parameter4.Value = "N";
                            }
                        }
                        else if (parameter4.Size > -1)
                        {
                            parameter4.Value = obj2;
                        }
                        else
                        {
                            dictionary.Add(OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First), obj2);
                            dictionary2.Add(OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First), mapping2.OracleType);
                        }
                        this.dac.cmd.Parameters.Add(parameter4);
                    }
                }
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    obj2 = item.GetType().GetProperty(mapping2.Second).GetValue(item, null);
                    parameter4 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First),
                        OracleType = mapping2.OracleType,
                        Value = obj2
                    };
                    this.dac.cmd.Parameters.Add(parameter4);
                }
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
                if (dictionary.Count > 0)
                {
                    str2 = string.Empty;
                    foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                    {
                        property = item.GetType().GetProperty(mapping2.Second);
                        str2 = (this.dac.cmd as OracleCommand).Parameters[mapping2.First].Value.ToString();
                        property.SetValue(item, str2, null);
                    }
                    foreach (string str3 in dictionary.Keys)
                    {
                        info2 = typeof(T).GetProperty("EntityName");
                        this.UpdateOracleLobField(info2.GetValue(item, null).ToString(), pkField, str2, dictionary2[str3], str3, dictionary[str3]);
                    }
                }
            }
        }

        public void UpdateByField(string spName, DataTable item, bool useExtendArgs)
        {
            Dictionary<string, object> dictionary;
            string str3;
            this.dac.cmd.Parameters.Clear();
            this.dac.cmd.CommandText = spName;
            this.dac.cmd.CommandType = CommandType.StoredProcedure;
            this.dac.cmd.Connection = this.dac.conn;
            string pkField = string.Empty;
            if (this.dac.conn is SqlConnection)
            {
                SqlParameter parameter;
                dictionary = new Dictionary<string, object>();
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[1])
                {
                    if (item.Rows[0][mapping.Second] != DBNull.Value)
                    {
                        parameter = new SqlParameter
                        {
                            ParameterName = mapping.First
                        };
                        if (mapping.SqlType != SqlDbType.VarBinary)
                        {
                            parameter.Value = item.Rows[0][mapping.Second].Equals(string.Empty) ? DBNull.Value : item.Rows[0][mapping.Second];
                            parameter.Size = mapping.Size;
                        }
                        else
                        {
                            if (item.Rows[0][mapping.Second].Equals(string.Empty))
                            {
                                parameter.Value = item.Rows[0][mapping.Second];
                            }
                            else
                            {
                                parameter.Value = new byte[0];
                            }
                            parameter.Size = mapping.Size;
                            parameter.SqlDbType = mapping.SqlType;
                            dictionary.Add(mapping.First.Substring(1), item.Rows[0][mapping.Second]);
                        }
                        this.dac.cmd.Parameters.Add(parameter);
                    }
                }
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    pkField = mapping.First.Substring(1);
                    parameter = new SqlParameter
                    {
                        ParameterName = mapping.First,
                        Value = item.Rows[0][mapping.Second].Equals(string.Empty) ? DBNull.Value : item.Rows[0][mapping.Second],
                        Size = mapping.Size
                    };
                    this.dac.cmd.Parameters.Add(parameter);
                }
                if (useExtendArgs)
                {
                    foreach (string str2 in item.ExtendedProperties.Keys)
                    {
                        SqlParameter parameter4 = new SqlParameter
                        {
                            ParameterName = "@" + str2,
                            Value = item.ExtendedProperties[str2] ?? string.Empty,
                            Direction = ParameterDirection.Input
                        };
                        this.dac.cmd.Parameters.Add(parameter4);
                    }
                }
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
                if (dictionary.Count > 0)
                {
                    str3 = string.Empty;
                    foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                    {
                        item.Rows[0][mapping.Second] = str3 = (this.dac.cmd as SqlCommand).Parameters[mapping.First].Value.ToString();
                    }
                    foreach (string str2 in dictionary.Keys)
                    {
                        this.UpdateSqlLobField(item.TableName.Substring(0, item.TableName.Length - 6), pkField, str3, str2, dictionary[str2]);
                    }
                }
            }
            if (this.dac.conn is OracleConnection)
            {
                OracleParameter parameter6;
                dictionary = new Dictionary<string, object>();
                Dictionary<string, OracleType> dictionary2 = new Dictionary<string, OracleType>();
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[1])
                {
                    if (item.Rows[0][mapping2.Second] != DBNull.Value)
                    {
                        parameter6 = new OracleParameter
                        {
                            ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First),
                            OracleType = mapping2.OracleType
                        };
                        if (item.Columns[mapping2.Second].DataType == typeof(bool))
                        {
                            if ((item.Rows[0][mapping2.Second] != DBNull.Value) && ((bool)item.Rows[0][mapping2.Second]))
                            {
                                parameter6.Value = "Y";
                            }
                            else
                            {
                                parameter6.Value = "N";
                            }
                        }
                        else if (parameter6.Size > -1)
                        {
                            parameter6.Value = item.Rows[0][mapping2.Second];
                        }
                        else
                        {
                            dictionary.Add(OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First), item.Rows[0][mapping2.Second]);
                            dictionary2.Add(OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First), mapping2.OracleType);
                        }
                        this.dac.cmd.Parameters.Add(parameter6);
                    }
                }
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    parameter6 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First),
                        OracleType = mapping2.OracleType,
                        Value = item.Rows[0][mapping2.Second]
                    };
                    this.dac.cmd.Parameters.Add(parameter6);
                }
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
                if (dictionary.Count > 0)
                {
                    str3 = string.Empty;
                    foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                    {
                        item.Rows[0][mapping2.Second] = str3 = (this.dac.cmd as OracleCommand).Parameters[mapping2.First].Value.ToString();
                    }
                    foreach (string str2 in dictionary.Keys)
                    {
                        this.UpdateOracleLobField(item.TableName.Substring(0, item.TableName.Length - 6), pkField, str3, dictionary2[str2], str2, dictionary[str2]);
                    }
                }
            }
        }

        public void UpdateByKey(string spName, DataTable item)
        {
            Dictionary<string, object> dictionary;
            this.dac.cmd.Parameters.Clear();
            this.dac.cmd.CommandText = spName;
            this.dac.cmd.CommandType = CommandType.StoredProcedure;
            this.dac.cmd.Connection = this.dac.conn;
            string pkField = string.Empty;
            if (this.dac.conn is SqlConnection)
            {
                SqlParameter parameter;
                dictionary = new Dictionary<string, object>();
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[1])
                {
                    parameter = new SqlParameter
                    {
                        ParameterName = mapping.First
                    };
                    if (mapping.SqlType != SqlDbType.VarBinary)
                    {
                        parameter.Value = item.Rows[0][mapping.Second].Equals(string.Empty) ? DBNull.Value : item.Rows[0][mapping.Second];
                        parameter.Size = mapping.Size;
                    }
                    else
                    {
                        if (item.Rows[0][mapping.Second].Equals(string.Empty))
                        {
                            parameter.Value = item.Rows[0][mapping.Second];
                        }
                        else
                        {
                            parameter.Value = new byte[0];
                        }
                        parameter.Size = mapping.Size;
                        parameter.SqlDbType = mapping.SqlType;
                        dictionary.Add(mapping.First.Substring(1), item.Rows[0][mapping.Second]);
                    }
                    this.dac.cmd.Parameters.Add(parameter);
                }
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    pkField = mapping.First.Substring(1);
                    parameter = new SqlParameter
                    {
                        ParameterName = mapping.First,
                        Value = item.Rows[0][mapping.Second].Equals(string.Empty) ? DBNull.Value : item.Rows[0][mapping.Second],
                        Size = mapping.Size
                    };
                    this.dac.cmd.Parameters.Add(parameter);
                }
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
                if (dictionary.Count > 0)
                {
                    string pkValue = string.Empty;
                    foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                    {
                        item.Rows[0][mapping.Second] = pkValue = (this.dac.cmd as SqlCommand).Parameters[mapping.First].Value.ToString();
                    }
                    foreach (string str3 in dictionary.Keys)
                    {
                        this.UpdateSqlLobField(item.TableName.Substring(0, item.TableName.Length - 6), pkField, pkValue, str3, dictionary[str3]);
                    }
                }
            }
            if (this.dac.conn is OracleConnection)
            {
                OracleParameter parameter4;
                dictionary = new Dictionary<string, object>();
                Dictionary<string, OracleType> dictionary2 = new Dictionary<string, OracleType>();
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[1])
                {
                    parameter4 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First),
                        OracleType = mapping2.OracleType
                    };
                    if (item.Columns[mapping2.Second].DataType == typeof(bool))
                    {
                        if ((item.Rows[0][mapping2.Second] != DBNull.Value) && ((bool)item.Rows[0][mapping2.Second]))
                        {
                            parameter4.Value = "Y";
                        }
                        else
                        {
                            parameter4.Value = "N";
                        }
                    }
                    else if (parameter4.Size > -1)
                    {
                        parameter4.Value = item.Rows[0][mapping2.Second];
                    }
                    else
                    {
                        dictionary.Add(OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First), item.Rows[0][mapping2.Second]);
                        dictionary2.Add(OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First), mapping2.OracleType);
                    }
                    this.dac.cmd.Parameters.Add(parameter4);
                }
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    parameter4 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First),
                        OracleType = mapping2.OracleType,
                        Value = item.Rows[0][mapping2.Second]
                    };
                    this.dac.cmd.Parameters.Add(parameter4);
                }
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
                if (dictionary.Count > 0)
                {
                    object obj2 = null;
                    foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                    {
                        item.Rows[0][mapping2.Second] = obj2 = (this.dac.cmd as OracleCommand).Parameters[mapping2.First].Value;
                    }
                    foreach (string str3 in dictionary.Keys)
                    {
                        this.UpdateOracleLobField(item.TableName.Substring(0, item.TableName.Length - 6), pkField, obj2, dictionary2[str3], str3, dictionary[str3]);
                    }
                }
            }
        }

        public void UpdateByKey<T>(string spName, T item)
        {
            Dictionary<string, object> dictionary;
            PropertyInfo property;
            object obj2;
            PropertyInfo info2;
            string pkField = string.Empty;
            this.GenereateSqlCommand(spName);
            if (this.dac.conn is SqlConnection)
            {
                SqlParameter parameter;
                dictionary = new Dictionary<string, object>();
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[1])
                {
                    obj2 = item.GetType().GetProperty(mapping.Second).GetValue(item, null);
                    parameter = new SqlParameter
                    {
                        ParameterName = mapping.First
                    };
                    if (mapping.SqlType != SqlDbType.VarBinary)
                    {
                        parameter.Value = (obj2 == null) ? DBNull.Value : obj2;
                        parameter.Size = mapping.Size;
                    }
                    else
                    {
                        parameter.Size = mapping.Size;
                        parameter.SqlDbType = mapping.SqlType;
                        if (obj2 == null)
                        {
                            parameter.Value = DBNull.Value;
                        }
                        else
                        {
                            parameter.Value = new byte[0];
                            dictionary.Add(mapping.First.Substring(1), obj2);
                        }
                    }
                    this.dac.cmd.Parameters.Add(parameter);
                }
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    obj2 = item.GetType().GetProperty(mapping.Second).GetValue(item, null);
                    pkField = mapping.First.Substring(1);
                    parameter = new SqlParameter
                    {
                        ParameterName = mapping.First,
                        Value = (obj2 == null) ? DBNull.Value : obj2,
                        Size = mapping.Size
                    };
                    this.dac.cmd.Parameters.Add(parameter);
                }
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
                if (dictionary.Count > 0)
                {
                    object obj3 = null;
                    foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                    {
                        property = item.GetType().GetProperty(mapping.Second);
                        obj3 = (this.dac.cmd as SqlCommand).Parameters[mapping.First].Value;
                        property.SetValue(item, obj3, null);
                    }
                    foreach (string str2 in dictionary.Keys)
                    {
                        info2 = typeof(T).GetProperty("EntityName");
                        this.UpdateSqlLobField(info2.GetValue(item, null).ToString(), pkField, obj3, str2, dictionary[str2]);
                    }
                }
            }
            if (this.dac.conn is OracleConnection)
            {
                OracleParameter parameter4;
                dictionary = new Dictionary<string, object>();
                Dictionary<string, OracleType> dictionary2 = new Dictionary<string, OracleType>();
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[1])
                {
                    property = item.GetType().GetProperty(mapping2.Second);
                    obj2 = property.GetValue(item, null);
                    parameter4 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First),
                        OracleType = mapping2.OracleType
                    };
                    if (property.GetType().FullName == typeof(bool).FullName)
                    {
                        if ((obj2 != null) && ((bool)obj2))
                        {
                            parameter4.Value = "Y";
                        }
                        else
                        {
                            parameter4.Value = "N";
                        }
                    }
                    else if (parameter4.Size > -1)
                    {
                        parameter4.Value = obj2;
                    }
                    else
                    {
                        dictionary.Add(OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First), obj2);
                        dictionary2.Add(OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First), mapping2.OracleType);
                    }
                    this.dac.cmd.Parameters.Add(parameter4);
                }
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    obj2 = item.GetType().GetProperty(mapping2.Second).GetValue(item, null);
                    parameter4 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First),
                        OracleType = mapping2.OracleType,
                        Value = obj2
                    };
                    this.dac.cmd.Parameters.Add(parameter4);
                }
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
                if (dictionary.Count > 0)
                {
                    string str3 = string.Empty;
                    foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                    {
                        property = item.GetType().GetProperty(mapping2.Second);
                        str3 = (this.dac.cmd as OracleCommand).Parameters[mapping2.First].Value.ToString();
                        property.SetValue(item, str3, null);
                    }
                    foreach (string str2 in dictionary.Keys)
                    {
                        info2 = typeof(T).GetProperty("EntityName");
                        this.UpdateOracleLobField(info2.GetValue(item, null).ToString(), pkField, str3, dictionary2[str2], str2, dictionary[str2]);
                    }
                }
            }
        }

        public void UpdateByKey(string spName, DataTable item, Dictionary<string, object> args)
        {
            Dictionary<string, object> dictionary;
            this.dac.cmd.Parameters.Clear();
            this.dac.cmd.CommandText = spName;
            this.dac.cmd.CommandType = CommandType.StoredProcedure;
            this.dac.cmd.Connection = this.dac.conn;
            string pkField = string.Empty;
            if (this.dac.conn is SqlConnection)
            {
                SqlParameter parameter;
                dictionary = new Dictionary<string, object>();
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[1])
                {
                    parameter = new SqlParameter
                    {
                        ParameterName = mapping.First
                    };
                    if (mapping.SqlType != SqlDbType.VarBinary)
                    {
                        parameter.Value = item.Rows[0][mapping.Second].Equals(string.Empty) ? DBNull.Value : item.Rows[0][mapping.Second];
                        parameter.Size = mapping.Size;
                    }
                    else
                    {
                        if (item.Rows[0][mapping.Second].Equals(string.Empty))
                        {
                            parameter.Value = item.Rows[0][mapping.Second];
                        }
                        else
                        {
                            parameter.Value = new byte[0];
                        }
                        parameter.Size = mapping.Size;
                        parameter.SqlDbType = mapping.SqlType;
                        dictionary.Add(mapping.First.Substring(1), item.Rows[0][mapping.Second]);
                    }
                    this.dac.cmd.Parameters.Add(parameter);
                }
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    pkField = mapping.First.Substring(1);
                    parameter = new SqlParameter
                    {
                        ParameterName = mapping.First,
                        Value = item.Rows[0][mapping.Second].Equals(string.Empty) ? DBNull.Value : item.Rows[0][mapping.Second],
                        Size = mapping.Size
                    };
                    this.dac.cmd.Parameters.Add(parameter);
                }
                foreach (string str2 in args.Keys)
                {
                    SqlParameter parameter4 = new SqlParameter
                    {
                        ParameterName = "@" + str2,
                        Value = args[str2],
                        Direction = ParameterDirection.Input
                    };
                    this.dac.cmd.Parameters.Add(parameter4);
                }
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
                if (dictionary.Count > 0)
                {
                    object pkValue = null;
                    foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                    {
                        item.Rows[0][mapping.Second] = pkValue = (this.dac.cmd as SqlCommand).Parameters[mapping.First].Value;
                    }
                    foreach (string str2 in dictionary.Keys)
                    {
                        this.UpdateSqlLobField(item.TableName.Substring(0, item.TableName.Length - 6), pkField, pkValue, str2, dictionary[str2]);
                    }
                }
            }
            if (this.dac.conn is OracleConnection)
            {
                OracleParameter parameter6;
                dictionary = new Dictionary<string, object>();
                Dictionary<string, OracleType> dictionary2 = new Dictionary<string, OracleType>();
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[1])
                {
                    parameter6 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First),
                        OracleType = mapping2.OracleType
                    };
                    if (item.Columns[mapping2.Second].DataType == typeof(bool))
                    {
                        if ((item.Rows[0][mapping2.Second] != DBNull.Value) && ((bool)item.Rows[0][mapping2.Second]))
                        {
                            parameter6.Value = "Y";
                        }
                        else
                        {
                            parameter6.Value = "N";
                        }
                    }
                    else if (parameter6.Size > -1)
                    {
                        parameter6.Value = item.Rows[0][mapping2.Second];
                    }
                    else
                    {
                        dictionary.Add(OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First), item.Rows[0][mapping2.Second]);
                        dictionary2.Add(OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First), mapping2.OracleType);
                    }
                    this.dac.cmd.Parameters.Add(parameter6);
                }
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    parameter6 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First),
                        OracleType = mapping2.OracleType,
                        Value = item.Rows[0][mapping2.Second]
                    };
                    this.dac.cmd.Parameters.Add(parameter6);
                }
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
                if (dictionary.Count > 0)
                {
                    string str3 = string.Empty;
                    foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                    {
                        item.Rows[0][mapping2.Second] = str3 = (this.dac.cmd as OracleCommand).Parameters[mapping2.First].Value.ToString();
                    }
                    foreach (string str2 in dictionary.Keys)
                    {
                        this.UpdateOracleLobField(item.TableName.Substring(0, item.TableName.Length - 6), pkField, str3, dictionary2[str2], str2, dictionary[str2]);
                    }
                }
            }
        }

        public void UpdateByKey<T>(string spName, T item, Dictionary<string, object> args)
        {
            Dictionary<string, object> dictionary;
            PropertyInfo property;
            object obj2;
            PropertyInfo info2;
            string pkField = string.Empty;
            this.GenereateSqlCommand(spName);
            if (this.dac.conn is SqlConnection)
            {
                SqlParameter parameter;
                dictionary = new Dictionary<string, object>();
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[1])
                {
                    obj2 = item.GetType().GetProperty(mapping.Second).GetValue(item, null);
                    parameter = new SqlParameter
                    {
                        ParameterName = mapping.First
                    };
                    if (mapping.SqlType != SqlDbType.VarBinary)
                    {
                        parameter.Value = obj2 ?? DBNull.Value;
                        parameter.Size = mapping.Size;
                    }
                    else
                    {
                        if (obj2 == null)
                        {
                            parameter.Value = obj2;
                        }
                        else
                        {
                            parameter.Value = new byte[0];
                        }
                        parameter.Size = mapping.Size;
                        parameter.SqlDbType = mapping.SqlType;
                        dictionary.Add(mapping.First.Substring(1), obj2);
                    }
                    this.dac.cmd.Parameters.Add(parameter);
                }
                foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                {
                    obj2 = item.GetType().GetProperty(mapping.Second).GetValue(item, null);
                    pkField = mapping.First.Substring(1);
                    parameter = new SqlParameter
                    {
                        ParameterName = mapping.First,
                        Value = obj2 ?? DBNull.Value,
                        Size = mapping.Size
                    };
                    this.dac.cmd.Parameters.Add(parameter);
                }
                foreach (string str2 in args.Keys)
                {
                    SqlParameter parameter4 = new SqlParameter
                    {
                        ParameterName = "@" + str2,
                        Value = args[str2],
                        Direction = ParameterDirection.Input
                    };
                    this.dac.cmd.Parameters.Add(parameter4);
                }
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
                if (dictionary.Count > 0)
                {
                    object obj3 = null;
                    foreach (HyBy.FrameWork.DAService.SqlDBMapping mapping in this.SqlDBMapping[2])
                    {
                        property = item.GetType().GetProperty(mapping.Second);
                        obj3 = (this.dac.cmd as SqlCommand).Parameters[mapping.First].Value;
                        property.SetValue(item, obj3, null);
                    }
                    foreach (string str2 in dictionary.Keys)
                    {
                        info2 = typeof(T).GetProperty("EntityName");
                        this.UpdateSqlLobField(info2.GetValue(item, null).ToString(), pkField, obj3, str2, dictionary[str2]);
                    }
                }
            }
            if (this.dac.conn is OracleConnection)
            {
                OracleParameter parameter6;
                dictionary = new Dictionary<string, object>();
                Dictionary<string, OracleType> dictionary2 = new Dictionary<string, OracleType>();
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[1])
                {
                    property = item.GetType().GetProperty(mapping2.Second);
                    obj2 = property.GetValue(item, null);
                    parameter6 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First),
                        OracleType = mapping2.OracleType
                    };
                    if (property.GetType().FullName == typeof(bool).FullName)
                    {
                        if ((obj2 != null) && ((bool)obj2))
                        {
                            parameter6.Value = "Y";
                        }
                        else
                        {
                            parameter6.Value = "N";
                        }
                    }
                    else if (parameter6.Size > -1)
                    {
                        parameter6.Value = obj2;
                    }
                    else
                    {
                        dictionary.Add(OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First), obj2);
                        dictionary2.Add(OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First), mapping2.OracleType);
                    }
                    this.dac.cmd.Parameters.Add(parameter6);
                }
                foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                {
                    obj2 = item.GetType().GetProperty(mapping2.Second).GetValue(item, null);
                    parameter6 = new OracleParameter
                    {
                        ParameterName = OracleParameterHelper.FormatParameter(this.dac.cmd.CommandType, mapping2.First),
                        OracleType = mapping2.OracleType,
                        Value = obj2
                    };
                    this.dac.cmd.Parameters.Add(parameter6);
                }
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
                if (dictionary.Count > 0)
                {
                    string str3 = string.Empty;
                    foreach (HyBy.FrameWork.DAService.OracleDBMapping mapping2 in this.OracleDBMapping[2])
                    {
                        property = item.GetType().GetProperty(mapping2.Second);
                        str3 = (this.dac.cmd as OracleCommand).Parameters[mapping2.First].Value.ToString();
                        property.SetValue(item, str3, null);
                    }
                    foreach (string str2 in dictionary.Keys)
                    {
                        info2 = typeof(T).GetProperty("EntityName");
                        this.UpdateOracleLobField(info2.GetValue(item, null).ToString(), pkField, str3, dictionary2[str2], str2, dictionary[str2]);
                    }
                }
            }
        }

        public void UpdateOracleLobField(string tableName, string pkFiled, object pkValue, OracleType dbType, string lobField, object lobValue)
        {
            try
            {
                this.dac.cmd.Parameters.Clear();
                string str = string.Format("Update {0} SET {1} = :LOB_DATA WHERE {2} = {3}", new object[] { tableName, lobField, pkFiled, pkValue });
                this.dac.cmd.CommandText = str;
                this.dac.cmd.CommandType = CommandType.Text;
                OracleParameter parameter = new OracleParameter
                {
                    ParameterName = ":LOB_DATA",
                    OracleType = dbType,
                    Value = lobValue
                };
                this.dac.cmd.Parameters.Add(parameter);
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw new CommonException(exception, CommonDeclare.EnumExceptionLevel.ERROR);
            }
        }

        public void UpdateSqlLobField(string tableName, string pkField, object pkValue, string lobField, object lobValue)
        {
            try
            {
                this.dac.cmd.Parameters.Clear();
                string str = string.Format("Update {0} set {1} = @LobData where {2} = '{3}'", new object[] { tableName, lobField, pkField, pkValue });
                this.dac.cmd.CommandText = str;
                this.dac.cmd.CommandType = CommandType.Text;
                SqlParameter parameter = new SqlParameter
                {
                    ParameterName = "@LobData",
                    Value = lobValue,
                    SqlDbType = SqlDbType.VarBinary
                };
                this.dac.cmd.Parameters.Add(parameter);
                this.dac.OpenConnection();
                this.dac.cmd.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw new CommonException(exception, CommonDeclare.EnumExceptionLevel.ERROR);
            }
        }
    }
}

