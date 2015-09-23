/**********************************************************************************************************************
 * 
 *  版权所有：(c)2015， 华跃博弈有限公司
 * 
 * ********************************************************************************************************************/

using HyBy.Trading.BusinessInterface;
using HyBy.FrameWork.DAService;

namespace HyBy.Trading.BusinessImplement
{

    public class CM_UserListService : CM_UserListServiceBase, ICM_UserListService
    {
        /// <summary>
        /// 如果未传入数据连接及事务控制对象，则使用默认的
        /// </summary>
        public CM_UserListService()
        {
        }

        /// <summary>
        /// 根据传入的数据连接及事务控件对象启用事务控制
        /// </summary>
        /// <param name="dac">数据连接入事务控制对象</param>
        public CM_UserListService(DataAccess dac)
        {
            this.dac = dac;
        }
    }
}



