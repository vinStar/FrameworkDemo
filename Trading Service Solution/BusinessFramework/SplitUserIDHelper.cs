using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HyBy.Trading.BusinessFramework
{
    public class SplitUserIDHelper
    {
        
        /// <summary>
        /// 获取登录用户编号
        /// </summary>
        /// <param name="identitiName"></param>
        /// <returns></returns>
        public static long? GetUserID(string identityName)
        {
            if (string.IsNullOrEmpty(identityName.Split('$')[0]))
                return null;
            return long.Parse(identityName.Split('$')[0]);
        }

        /// <summary>
        /// 获取登录人名称
        /// </summary>
        /// <param name="identitiName"></param>
        /// <returns></returns>
        public static string GetUserName(string identityName)
        {
            return identityName.Split('$')[1];
        }

        /// <summary>
        /// 获取登录角色
        /// </summary>
        /// <param name="identitiName"></param>
        /// <returns></returns>
        public static string GetUserRole(string identityName)
        {
            return identityName.Split('$')[2];
        }

        public static string GetUserMenuCode(string identityName)
        {
            return identityName.Split('$')[3];
        }
    }
}
