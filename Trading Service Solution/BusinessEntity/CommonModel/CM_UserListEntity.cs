/**********************************************************************************************************************
 * 
 *  版权所有：(c)2015， 华跃博弈有限公司
 * 
 * ********************************************************************************************************************/


 using System;

  namespace HyBy.Trading.BusinessEntity
{
	/// 用于自定添加字段
 
	public class CM_UserListEntity:CM_UserListEntityBase
    {
        /// <summary>
        /// 是否是集团角色
        /// </summary>
        public int isGroup
        {
            get;
            set;
        }
		  /// <summary>
         /// 序号
         /// </summary>
        // public virtual long? RN { get; set; }      
    }
}



