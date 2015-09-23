/**********************************************************************************************************************
 * 
 *  版权所有：(c)2015， 华跃博弈有限公司
 * 
 * ********************************************************************************************************************/

 using System;
 using System.Collections.Generic;

using HyBy.Trading.BusinessInterface;
using HyBy.Trading.BusinessEntity;
using HyBy.FrameWork.Common;
using HyBy.FrameWork.DAService;

using HyBy.Trading.DAService;

  namespace HyBy.Trading.BusinessImplement
{	 
 
	public class CM_UserListServiceBase:BusinessObject, ICM_UserListServiceBase 
    {
		 /// <summary>
        /// 根据ID获取 CM_UserList实体
        /// </summary>
        /// <param name="entityID">CM_UserList的ID</param>
        /// <returns>CM_UserListEntity 对象</returns>
        public virtual CM_UserListEntity GetCM_UserListEntityByID(string entityID)
        {
            CM_UserListEntity item = new CM_UserListEntity();
            item.EntityID = entityID;
            return this.GetCM_UserListEntity(item);
        }

        /// <summary>
        /// 根据Entity获取 CM_UserList实体
        /// </summary>
        /// <param name="item">CM_UserList的实体</param>
        /// <returns>CM_UserListEntity 对象</returns>
        public virtual CM_UserListEntity GetCM_UserListEntity(CM_UserListEntity item)
        {
            try
            {
                new CM_UserListDBO(dac).GetCM_UserListEntity(item);
                return item;
            }
            catch (Exception ex)
            {
                throw new CommonException(ex, CommonDeclare.EnumExceptionLevel.ERROR);
            }
        }
         
        /// <summary>
        /// 插入CM_UserList实体
        /// </summary>
        /// <param name="item">CM_UserList的实体</param>
        /// <returns>返回插入后获取的CM_UserList实体</returns>
        public virtual CM_UserListEntity InsertCM_UserListEntity(CM_UserListEntity item)
        {
            try
            {
                if ((dac.Scope == null))
                {
                    using (CommonScope scope = new CommonScope(dac))
                    {
                        new CM_UserListDBO(dac).InsertCM_UserListEntity(item);
                        scope.Complete();
                        return item;
                    }
                }
                else
                {
                    new CM_UserListDBO(dac).InsertCM_UserListEntity(item);
                    return item;
                }
            }
            catch (System.Exception ex)
            {
                throw new CommonException(ex, CommonDeclare.EnumExceptionLevel.ERROR);
            }
        }

        /// <summary>
        /// 更新CM_UserList实体
        /// </summary>
        /// <param name="item">CM_UserList的实体</param>
        public virtual void UpdateCM_UserListEntity(CM_UserListEntity item)
        {
            try
            {
                if ((dac.Scope == null))
                {
                    using (CommonScope scope = new CommonScope(dac))
                    {
                        new CM_UserListDBO(dac).UpdateCM_UserListEntity(item);
                        scope.Complete();
                    }
                }
                else
                {
                    new CM_UserListDBO(dac).UpdateCM_UserListEntity(item);
                }
            }
            catch (System.Exception ex)
            {
                throw new CommonException(ex, CommonDeclare.EnumExceptionLevel.ERROR);
            }
        }

        /// <summary>
        /// 获取CM_UserList实体集合
        /// </summary>
        /// <param name="item">CM_UserList的实体</param>
        public virtual List<CM_UserListEntity> GetCM_UserListCollection(System.Collections.Generic.Dictionary<string, object> args)
        {
            try
            {
                List<CM_UserListEntity> item = new List<CM_UserListEntity>();
                item = new CM_UserListDBO(dac).GetCM_UserListCollection(args);
                return item;
            }
            catch (System.Exception ex)
            {
                throw new CommonException(ex, CommonDeclare.EnumExceptionLevel.ERROR);
            }
        }
    }
}



