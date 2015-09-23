using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HyBy.FrameWork.Common
{
    /// <summary>
    /// 版权所有: 版权所有(C) 2013，华跃博弈
    /// 内容摘要: Cookie操作类
    /// 完成日期：2014-5-16
    /// 版    本：V1.0
    /// 作    者：段帅峰
    /// </summary>
    public class CookieHelper
    {
        #region 判断是否有这个Cookie名称
        /// <summary>
        /// 判断是否有这个Cookie名称
        /// </summary>
        /// <param name="name">Cookie名称</param>
        /// <returns>Cookie名称存在返回True，不存在返回False</returns>
        public static bool CheckCookie(string name)
        {
            ///修改读取Cookie方法
            if (System.Web.HttpContext.Current.Request.Cookies[name] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region 清除Cookie缓存
        /// <summary>
        /// 清除Cookie缓存
        /// </summary>
        /// <param name="cookiename">Cookie名称</param>
        public static void ClearCookie(string cookiename)
        {

            HttpCookie cookie = new HttpCookie(cookiename);
            if (cookie != null)
            {
                //cookie.Domain= null;
                cookie.Path = "/";
                cookie.Expires = DateTime.Now.AddYears(-1);
                cookie.Values.Clear();

                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            }

        }
        #endregion

        #region 读取Cookie
        /// <summary>
        /// 读取Cookie  - subCookie 值
        /// </summary>
        /// <param name="name">Cookie名称</param>
        /// <param name="subname">子Cookie名称</param>
        /// <returns>对应Cookie值</returns>
        public static string GetCookie(string name, string subname = null)
        {
            string _cstr = "";

            if ((name != null) && (subname != null))
            {
                if (System.Web.HttpContext.Current.Request.Cookies[name] == null
                    || System.Web.HttpContext.Current.Request.Cookies[name][subname]==null)
                {
                    return null;
                }
                else
                {
                    _cstr = System.Web.HttpContext.Current.Request.Cookies[name][subname].ToString();
                    return System.Web.HttpUtility.UrlDecode(_cstr, Encoding.GetEncoding("UTF-8"));
                }
            }
            else if ((name != null))
            {
                if (System.Web.HttpContext.Current.Request.Cookies[name] == null)
                {
                    return null;
                }
                else
                {
                    _cstr = System.Web.HttpContext.Current.Request.Cookies[name].Value.ToString();
                    return System.Web.HttpUtility.UrlDecode(_cstr, Encoding.GetEncoding("UTF-8"));
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 创建新Cookie
        /// <summary>
        /// 添加Cookie
        /// </summary>
        /// <param name="CookieName">Cookie的名字</param>
        /// <param name="CookieValue">Cookie的值</param>
        /// <param name="iDays">有效时间-天</param>
        /// <param name="iHours">有效时间-时</param>
        /// <param name="iMinute">有效时间-分</param>
        /// <param name="iSecond">有效时间-秒</param>
        public static bool AddCookie(string CookieName, string CookieValue, int iDays = 1, int iHours = 0, int iMinute = 0, int iSecond = 0)
        {
            try
            {
                System.Web.HttpCookie addCookie = new HttpCookie(CookieName, CookieValue);
                System.TimeSpan newTimeSpan = new TimeSpan(iDays, iHours, iMinute, iSecond);
                addCookie.Expires = DateTime.Now.Add(newTimeSpan);
                System.Web.HttpContext.Current.Response.Cookies.Add(addCookie);
            }
            catch (Exception e)
            {
                ExceptionToMessageHelper.WriteLog(e);
                return false;
            }
            return true;
        }
        #endregion

        #region 创建新子Cookie
        /// <summary>
        /// 创建新子Cookie
        /// </summary>
        /// <param name="strname">Cookie名称</param>
        /// <param name="strsubname">子Cookie名称</param>
        /// <param name="strvalue">Cookie值</param>
        /// <returns>成功返回True，失败返回False</returns>
        public static bool SetSubCookie(string strname, string strsubname, string strvalue)
        {
            try
            {
                if (System.Web.HttpContext.Current.Request.Cookies[strname] != null)
                {
                    System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[strname];
                    cookie.Values.Set(strsubname, strvalue);
                    System.Web.HttpContext.Current.Request.Cookies.Set(cookie);
                    System.Web.HttpContext.Current.Response.AppendCookie(cookie);
                }
            }
            catch (Exception e)
            {
                ExceptionToMessageHelper.WriteLog(e);
                return false;
            }
            return true;
        }
        #endregion

        #region 删除Cookie
        /// <summary>
        /// 删除Cookie
        /// </summary>
        /// <param name="strname">Cookie名称</param>
        public static void DelCookie(string strname)
        {
            if (System.Web.HttpContext.Current.Request.Cookies[strname] != null)
            {
                System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[strname];
                cookie.Expires = DateTime.Now.AddDays(-1);
                System.Web.HttpContext.Current.Response.AppendCookie(cookie);
            }
        }
        #endregion

    }
}