using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

using HyBy.FrameWork.Common;

namespace HyBy.FrameWork.DAService
{
    public class BusinessObject
    {
        protected DataAccess dac;

        /// <summary>
        /// 默认构造函数总是执行，除非用:base指定构造函数，启用默认数据库连接
        /// 传入DataAccess da也会执行浪费资源可以改进
        /// </summary>
        protected BusinessObject()
        {
            this.dac = new DataAccess("");
        }

        protected BusinessObject(DataAccess da)
        {
            this.dac = new DataAccess("");
            this.dac = da;
        }

        /// <summary>
        /// 除了这个没啥用
        /// </summary>
        public virtual void Dispose()
        {
            this.dac.Close();
        }

    }
}

