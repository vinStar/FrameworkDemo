namespace HyBy.FrameWork.DAService.ExCommon
{
    using HyBy.FrameWork.Common;
    using HyBy.FrameWork.DAService;
    using System;

    public class ExCommonService : BusinessObject, IDbExecuter, IDisposable
    {
        private Random rd;

        public ExCommonService()
        {
            this.rd = new Random();
        }

        public ExCommonService(DataAccess dac)
        {
            if (dac == null)
            {
                base.dac = new DataAccess("");
            }
            else
            {
                base.dac = dac;
            }
        }

        public CommonResult Execute(DbExecuteEntity entity)
        {
            CommonResult result2;
            try
            {
                CommonResult result = null;
                if (base.dac.Scope == null)
                {
                    using (CommonScope scope = new CommonScope(base.dac))
                    {
                        result = new ExCommonDBO(base.dac).Execute(entity);
                        scope.Complete();
                    }
                }
                else
                {
                    result = new ExCommonDBO(base.dac).Execute(entity);
                }
                result2 = result;
            }
            catch (Exception exception)
            {
                throw new CommonException(exception, CommonDeclare.EnumExceptionLevel.ERROR);
            }
            return result2;
        }

        public T ExecuteMultipleSelect<T>(DbExecuteEntity entity)
        {
            T local2;
            try
            {
                T local = default(T);
                if (base.dac.Scope == null)
                {
                    using (CommonScope scope = new CommonScope(base.dac))
                    {
                        local = new ExCommonDBO(base.dac).ExecuteMultipleSelect<T>(entity);
                        scope.Complete();
                    }
                }
                else
                {
                    local = new ExCommonDBO(base.dac).ExecuteMultipleSelect<T>(entity);
                }
                local2 = local;
            }
            catch (Exception exception)
            {
                throw new CommonException(exception, CommonDeclare.EnumExceptionLevel.ERROR);
            }
            return local2;
        }

        public CommonResult<T> ExecuteSelect<T>(SearchEntity search)
        {
            CommonResult<T> result2;
            try
            {
                CommonResult<T> result = null;
                if (base.dac.Scope == null)
                {
                    using (CommonScope scope = new CommonScope(base.dac))
                    {
                        result = new ExCommonDBO(base.dac).ExecuteSelect<T>(search);
                        scope.Complete();
                    }
                }
                else
                {
                    result = new ExCommonDBO(base.dac).ExecuteSelect<T>(search);
                }
                result2 = result;
            }
            catch (Exception exception)
            {
                throw new CommonException(exception, CommonDeclare.EnumExceptionLevel.ERROR);
            }
            return result2;
        }
    }
}

