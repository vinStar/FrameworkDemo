using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;

//using System.Data.SqlClient;

using HyBy.FrameWork.Common;

namespace HyBy.FrameWork.DAService
{
    public class DataAccess
    {
        //数据库连接字符串
        protected static ConnectionStringSettings connStringSetting = ConfigurationHelper.GetConnectionStringSettings("default");
        //抽象工厂，用以支持多种数据库(ProviderName默认System.Data.SqlClient)
        protected static DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connStringSetting.ProviderName);

        //依赖倒置原则
        //A.高层次的模块不应该依赖于低层次的模块，他们都应该依赖于抽象。
        //B.抽象不应该依赖于具体，具体应该依赖于抽象。
        //工厂创建 new SqlCommand();    
        public IDbCommand cmd = dbFactory.CreateCommand();//依赖于抽象接口 IDbCommand  //DbCommand dbcom = dbFactory.CreateCommand();
        public IDbConnection conn = dbFactory.CreateConnection();//依赖于抽象接口 IDbConnection

        //事务TransactionScope
        private CommonScope scope = null;

        public CommonScope Scope
        {
            get
            {
                return scope;
            }
            set
            {
                scope = value;
            }
        }

        //可选和命名参数  4.0语法糖 等同 DataAccess(  string connstr="")
        //DataAccess a = new DataAccess();
        //DataAccess aa = new DataAccess(connstr:"11");
        public DataAccess([Optional, DefaultParameterValue("")] string connstr)
        {
            //连接参数则使用传入的连接字符串
            if (!string.IsNullOrEmpty(connstr))
            {
                connStringSetting = ConfigurationHelper.GetConnectionStringSettings(connstr);
            }
            conn.ConnectionString = connStringSetting.ConnectionString;
            cmd.Connection = conn;
        }

        public void Close()
        {
            try
            {
                conn.Close();
                conn.Dispose();
            }
            catch (Exception exception)
            {
                throw new CommonException("关闭数据库连接->该方法由 Scope中的Dispose()方法调用 出错.", exception, CommonDeclare.EnumExceptionLevel.ERROR);
            }
        }

        public void CloseConnection()
        {
            try
            {
                cmd.Parameters.Clear();
                if (scope == null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new CommonException("未使用事务 关闭数据库连接出错.", exception, CommonDeclare.EnumExceptionLevel.ERROR);
            }
        }

        public void OpenConnection()
        {
            try
            {
                if (((conn == null) || (scope == null)) || (conn.State == ConnectionState.Closed))
                {
                    conn = dbFactory.CreateConnection();
                    conn.ConnectionString = connStringSetting.ConnectionString;
                    cmd.Connection = conn;
                    conn.Open();
                }
            }
            catch (Exception exception)
            {
                throw new CommonException("打开数据库连接出错.", exception, CommonDeclare.EnumExceptionLevel.FAULT);
            }
        }

    }
}

