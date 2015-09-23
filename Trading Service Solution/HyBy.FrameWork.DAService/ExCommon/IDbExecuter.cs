namespace HyBy.FrameWork.DAService.ExCommon
{
    using System;
    using System.ServiceModel;

    [ServiceContract(Name="Ex_ExecuteSqlServices", Namespace="http://www.rxjy.com")]
    public interface IDbExecuter : IDisposable
    {
        CommonResult Execute(DbExecuteEntity entity);
        T ExecuteMultipleSelect<T>(DbExecuteEntity entity);
        CommonResult<T> ExecuteSelect<T>(SearchEntity search);
    }
}

