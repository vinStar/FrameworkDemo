namespace HyBy.FrameWork.DAService
{
    using HyBy.FrameWork.Common;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.InteropServices;

    public class DataAccess
    {
        public IDbCommand cmd = dbFactory.CreateCommand();
        public IDbConnection conn = dbFactory.CreateConnection();
        protected static ConnectionStringSettings connStringSetting = ConfigurationHelper.GetConnectionStringSettings("default");
        protected static DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connStringSetting.ProviderName);
        private CommonScope scope = null;

        public DataAccess([Optional, DefaultParameterValue("")] string connstr)
        {
            if (!string.IsNullOrEmpty(connstr))
            {
                connStringSetting = ConfigurationHelper.GetConnectionStringSettings(connstr);
            }
            this.conn.ConnectionString = connStringSetting.ConnectionString;
            this.cmd.Connection = this.conn;
        }

        public void Close()
        {
            try
            {
                this.conn.Close();
                this.conn.Dispose();
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
                this.cmd.Parameters.Clear();
                if (this.scope == null)
                {
                    this.conn.Close();
                    this.conn.Dispose();
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
                if (((this.conn == null) || (this.scope == null)) || (this.conn.State == ConnectionState.Closed))
                {
                    this.conn = dbFactory.CreateConnection();
                    this.conn.ConnectionString = connStringSetting.ConnectionString;
                    this.cmd.Connection = this.conn;
                    this.conn.Open();
                }
            }
            catch (Exception exception)
            {
                throw new CommonException("打开数据库连接出错.", exception, CommonDeclare.EnumExceptionLevel.FAULT);
            }
        }

        public CommonScope Scope
        {
            get
            {
                return this.scope;
            }
            set
            {
                this.scope = value;
            }
        }
    }
}

