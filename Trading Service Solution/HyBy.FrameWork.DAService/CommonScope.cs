namespace HyBy.FrameWork.DAService
{
    using HyBy.FrameWork.Common;
    using System;
    using System.Transactions;

    public sealed class CommonScope : IDisposable
    {
        private DataAccess dac = null;
        private TransactionScope scope = null;

        public CommonScope(DataAccess dac)
        {
            try
            {
                this.dac = dac;
                if (this.scope == null)
                {
                    dac.Scope = this;
                    this.scope = new TransactionScope();
                    dac.conn.Open();
                }
            }
            catch (Exception exception)
            {
                throw new CommonException("事务中打开数据库连接出错. 连接串：" + dac.conn.ConnectionString, exception, CommonDeclare.EnumExceptionLevel.FAULT);
            }
        }

        public void Complete()
        {
            this.scope.Complete();
        }

        void IDisposable.Dispose()
        {
            try
            {
                this.dac.Close();
            }
            catch (Exception exception)
            {
                throw new CommonException("Scope中关闭数据库连接出错.", exception, CommonDeclare.EnumExceptionLevel.ERROR);
            }
            finally
            {
                this.scope.Dispose();
            }
        }
    }
}

