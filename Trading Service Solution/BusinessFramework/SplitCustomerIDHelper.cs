using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HyBy.Trading.BusinessFramework
{
    public class SplitCustomerIDHelper
    {
        /// <summary>
        /// 获得用户Id
        /// </summary>
        /// <param name="identityName"></param>
        /// <returns></returns>
        public static long? GetCustomerID(string identityName)
        {
            if (string.IsNullOrEmpty(identityName.Split('$')[0]))
                return null;
            return long.Parse(identityName.Split('$')[0]);
        }

        /// <summary>
        /// 获得用户类型
        /// </summary>
        /// <param name="identityName"></param>
        /// <returns></returns>
        public static long? GetCustomerType(string identityName)
        {
            if (string.IsNullOrEmpty(identityName.Split('$')[1]))
                return null;
            return long.Parse(identityName.Split('$')[1]);
        }

        /// <summary>
        /// 获得用户姓名
        /// </summary>
        /// <param name="identityName"></param>
        /// <returns></returns>
        public static string GetCustomerName(string identityName)
        {
            return identityName.Split('$')[2];
        }

        /// <summary>
        /// 获得用户昵称
        /// </summary>
        /// <param name="identityName"></param>
        /// <returns></returns>
        public static string GetNickName(string identityName)
        {
            return identityName.Split('$')[3];
        }

        /// <summary>
        /// 获得客户分类
        /// </summary>
        /// <param name="identityName"></param>
        /// <returns></returns>
        public static long? GetCustomerCatalog(string identityName)
        {
            if (string.IsNullOrEmpty(identityName.Split('$')[4]))
                return null;
            return long.Parse(identityName.Split('$')[4]);
        }


        /// <summary>
        /// 获取客户性别
        /// </summary>
        /// <param name="identitiName"></param>
        /// <returns></returns>
        public static long? GetCustomerGender(string identityName)
        {
            if (string.IsNullOrEmpty(identityName.Split('$')[5]))
                return null;
            return long.Parse(identityName.Split('$')[5]);
        }

        /// <summary>
        /// 获取客户头像
        /// </summary>
        /// <param name="identitiName"></param>
        /// <returns></returns>
        public static string GetCustomerImgPath(string identityName)
        {
            return identityName.Split('$')[6];
        }

        /// <summary>
        /// 获取会员等级
        /// </summary>
        /// <param name="identitiName"></param>
        /// <returns></returns>
        public static string GetMemberLevel(string identityName)
        {
            return identityName.Split('$')[7];
        }

        /// <summary>
        /// 获取居住地
        /// </summary>
        /// <param name="identitiName"></param>
        /// <returns></returns>
        public static string GetLocation(string identityName)
        {
            return identityName.Split('$')[8];
        }

        /// <summary>
        /// 获取积分
        /// </summary>
        /// <param name="identitiName"></param>
        /// <returns></returns>
        public static int? GetBonus(string identityName)
        {
            if (string.IsNullOrEmpty(identityName.Split('$')[9]))
                return null;
            return int.Parse(identityName.Split('$')[9]);
        }

        /// <summary>
        /// 获取客户状态
        /// </summary>
        /// <param name="identitiName"></param>
        /// <returns></returns>
        public static long? GetCustomerStatus(string identityName)
        {
            if (string.IsNullOrEmpty(identityName.Split('$')[10]))
                return null;
            return long.Parse(identityName.Split('$')[10]);
        }

        public static string GetCustomerCardNo(string identityName)
        {
            return identityName.Split('$')[11];
        }

        public static string GetU_IDO(string identityName)
        {
            return identityName.Split('$')[12];
        }
    }
}
