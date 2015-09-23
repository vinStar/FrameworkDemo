using HyBy.FrameWork.Common;
using HyBy.FrameWork.DAService;
using HyBy.Trading.BusinessEntity;
using HyBy.Trading.BusinessInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HyBy.Trading.BusinessImplement
{
    public class ExCommonService : BusinessObject, IDbExecuter
    {
        Random rd;
        /// <summary>
        /// 如果未传入数据连接及事务控制对象，则使用默认的
        /// </summary>
        public ExCommonService()
        {
            dac = new DataAccess("default");
            rd = new Random();
        }

        /// <summary>
        /// 如果传入的对象为null，则创建一个
        /// </summary>
        /// <param name="dac">数据连接入事务控制对象</param>
        public ExCommonService(DataAccess dac)
        {
            if ((dac == null))
            {
                this.dac = new DataAccess();
            }
            else
            {
                this.dac = dac;
            }
        }

        /// <summary>
        /// 执行存储过程，常用于执行数据处理的操作
        /// </summary>
        /// <param name="entity">执行参数对象</param>
        /// <returns></returns>
        public CommonResult Execute(DbExecuteEntity entity)
        {
            return Execute(entity, false);
        }
        /// <summary>
        /// 执行存储过程，常用于执行数据处理的操作
        /// </summary>
        /// <param name="entity">执行参数对象</param>
        /// <param name="throwException">是否抛出异常</param>
        /// <returns></returns>
        public CommonResult Execute(DbExecuteEntity entity,bool throwException)
        {
            try
            {
                CommonResult item = null;
                if ((dac.Scope == null) && Transaction.Current == null)
                {
                    using (CommonScope scope = new CommonScope(dac))
                    {
                        item = new ExCommonDBO(dac).Execute(entity, throwException);
                        scope.Complete();
                    }
                }
                else
                {
                    item = new ExCommonDBO(dac).Execute(entity, throwException);
                }
                return item;
            }
            catch (System.Exception ex)
            {
                throw new CommonException(ex, CommonDeclare.EnumExceptionLevel.ERROR);
            }
        }

        /// <summary>
        /// 可用于执行sql语句和存储过程，常用于执行查询操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="search"></param>
        /// <returns></returns>
        public CommonResult<T> ExecuteSelect<T>(SearchEntity search)
        {
            try
            {
                CommonResult<T> item = null;
                item = new ExCommonDBO(dac).ExecuteSelect<T>(search);
                //if ((dac.Scope == null) && Transaction.Current == null)
                //{
                //    using (CommonScope scope = new CommonScope(dac))
                //    {
                //        item = new ExCommonDBO(dac).ExecuteSelect<T>(search);
                //        scope.Complete();
                //    }
                //}
                //else
                //{
                //    item = new ExCommonDBO(dac).ExecuteSelect<T>(search);
                //}
                return item;
            }
            catch (System.Exception ex)
            {
                
                throw new CommonException(ex, CommonDeclare.EnumExceptionLevel.ERROR);
            }
        }


        /// <summary>
        /// 执行存储过程，常用于执行多表查询，同时返回多个数据集
        /// </summary>
        /// <typeparam name="T">要返回的ViewModel对象类型</typeparam>
        /// <param name="entity">执行参数对象</param>
        /// <returns>T类型对象</returns>
        public T ExecuteMultipleSelect<T>(DbExecuteEntity entity)
        {
            try
            {
                T item = default(T);
                item = new ExCommonDBO(dac).ExecuteMultipleSelect<T>(entity);
                //if ((dac.Scope == null) && Transaction.Current == null)
                //{
                //    using (CommonScope scope = new CommonScope(dac))
                //    {
                //        item = new ExCommonDBO(dac).ExecuteMultipleSelect<T>(entity);
                //        scope.Complete();
                //    }
                //}
                //else
                //{
                //    item = new ExCommonDBO(dac).ExecuteMultipleSelect<T>(entity);
                //}
                return item;
            }
            catch (System.Exception ex)
            {
                throw new CommonException(ex, CommonDeclare.EnumExceptionLevel.ERROR);
            }
        }
    }
}
