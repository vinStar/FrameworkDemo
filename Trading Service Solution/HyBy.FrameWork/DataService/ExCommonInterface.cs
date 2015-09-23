using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HyBy.FrameWork.DAService.ExCommon
{
    [ServiceContract(Name = "Ex_ExecuteSqlServices", Namespace = "http://www.rxjy.com")]
    public interface IDbExecuter:IDisposable
    {
        /// <summary>
        /// 执行存储过程，常用于执行数据处理的操作
        /// </summary>
        /// <param name="entity">执行参数对象</param>
        /// <returns></returns>
        CommonResult Execute(DbExecuteEntity entity);

        /// <summary>
        /// 可用于执行sql语句和存储过程，常用于执行查询操作
        /// </summary>
        /// <typeparam name="T">返回集合成员类型</typeparam>
        /// <param name="search">执行参数对象</param>
        /// <returns></returns>
        CommonResult<T> ExecuteSelect<T>(SearchEntity search);

        /// <summary>
        /// 执行存储过程，常用于执行多表查询，同时返回多个数据集
        /// </summary>
        /// <typeparam name="T">要返回的ViewModel对象类型</typeparam>
        /// <param name="entity">执行参数对象</param>
        /// <returns>T类型对象</returns>
        T ExecuteMultipleSelect<T>(DbExecuteEntity entity);
    }
}
