using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using HyBy.Trading.BusinessFramework;
using System.Net;
using HyBy.FrameWork.DAService;
using HyBy.Trading.BusinessInterface;
using HyBy.Trading.BusinessEntity;
using HyBy.Trading.BusinessImplement;
using System.IO;
using System.Text;
using System.Configuration;


namespace WebSite
{
    [ExceptionLog]
    [CompressAttribute]
    public class BaseController : Controller
    {
        #region Cookies为NULL ，跳转到登录页面
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            string url = filterContext.HttpContext.Request.Path.ToLower();
            if (filterContext.HttpContext.Request.Cookies["RSUser"] == null && !url.Equals("/") && url.IndexOf("/login") != 0)
            {
                Response.Redirect("/Login/Index");
            }
            else if (!url.Equals("/") && url.IndexOf("/login") != 0 && Request.Cookies["RSUser"]["u_diqu"] == null)
            {
                Response.Redirect("/Login/Index");
            }

            //集团账户只能在集团区域活动 出纳财务同理
            //string Identity = CheckLogIdentity(Request.Cookies["RSUser"]["u_jibie"], Request.Cookies["RSUser"]["js_IsGroup"]);
            //if (url.IndexOf("/identityerror") < 0 &&  url.IndexOf("/login") < 0)
            //{
            //    if (url.IndexOf("/g/") > -1 || url.IndexOf("/group/") > -1 )
            //    {
            //        if (Identity != "Group")
            //        {
            //            //RedirectToAction("Error", "IdentityError", new { area = Identity });
            //          filterContext.HttpContext.Response.Redirect("/Error/IdentityError?area=" + Identity);
            //        }
            //    }
            //    else if (url.IndexOf("/i/") > -1 || url.IndexOf("/invest/") > -1)
            //    {
            //        if (Identity != "Invest")
            //        {
            //            filterContext.HttpContext.Response.Redirect("/Error/IdentityError?area=" + Identity);
            //        }
            //    }
            //    else
            //    {
            //        if (Identity != "Cashier")
            //        {
            //            filterContext.HttpContext.Response.Redirect("/Error/IdentityError?area=" + Identity);
            //        }
            //    }
            //}
        }
        #endregion

        /// <summary>
        /// 根据身份获取可操作的区域
        /// </summary>
        public string CheckLogIdentity(string JiBie, string isGroup)
        {

            if (isGroup == "0" && JiBie == "54")//会计
            {
                return "Invest";
            }
            else if (isGroup == "0" && JiBie == "55") //出纳
            {
                return "Cashier";
            }
            else if ((isGroup == "1" && JiBie == "167") || Convert.ToInt32(JiBie) > 1000) //集团会计
            {
                return "Group";
            }
            else if ((isGroup == "1" && JiBie == "55") || Convert.ToInt32(JiBie) > 1000) //集团出纳
            {
                return "Group";
            }
            else
            {
                return "None";
            }
        }




        /// <summary>
        /// 日志记录 2014/10/22 by lzy
        /// </summary>
        /// <param name="msg">示例; 更新主案人员状态</param>
        /// <param name="dt">记录时间</param>
        /// <param name="cardNum">卡号</param>
        /// <param name="regionId">地区ID</param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="sql"></param>
        /// <param name="result">返回结果</param>
        public void SetLogInfo(string msg, DateTime dt, string cardNum, object regionId, string actionName, string controllerName, string sql, object result)
        {
            MessageHelper.WriteLog(string.Format("--------------------------------------{0}------------------------------------------" +
                                            "\r\n\r\n== Date：{1}\r\n\r\n== CardNum：{2}\r\n\r\n== RegionID：{3}\r\n\r\n== ActionName：{4}\r\n\r\n== ControllerName：{5}\r\n\r\n== SQL：{6}\r\n\r\n== Result：{7}",
                                            msg, dt, cardNum, regionId, actionName, controllerName, sql, result));
        }
        /// <summary>
        /// 获取登陆人的地区ID 2014/10/06 by lzy
        /// </summary>
        ////public string RegionId { get { return BiaoZhun.DiQu; } }


   

        /// <summary>
        /// 是否是集团人事登陆 2014/11/20 by lzy
        /// </summary>
        public bool IsGroup
        {
            get
            {
                return (Request.Cookies["RSUser"] == null) ? false : Request.Cookies["RSUser"]["js_IsGroup"] == "1";
            }
        }

        /// <summary>
        /// 用户类型不正确,跳转到用户注销页
        /// </summary>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        public ActionResult RedirectToLogoutAction(string rawUrl, int? rolel)
        {
            if (rolel.HasValue)
            {
                switch (rolel.Value)
                {
                    case 1:
                        return Redirect("/XX.htm");
                    case 2:
                        return Redirect("/XX.htm");
                    default: break;
                }
            }
            //return Redirect("/Login/Index.html?rawUrl=" + rawUrl);
            return RedirectToAction("Index", "Login", new { area = "UserSystem", rawUrl });
        }

        //获取登录用户的信息
        public string RoleID
        {
            get
            {
                if (Request.Cookies["RSUser"] == null)
                {
                    return "";
                }
                return Convert.ToString(Request.Cookies["RSUser"]["u_jibie"]);
            }
        }

        /// <summary>
        /// 获取登录用户的地区
        /// </summary>
        public string RegionID
        {
            get
            {
                if (Request.Cookies["RSUser"] == null)
                {
                    return "0";
                }
                return Request.Cookies["RSUser"]["u_diqu"];
            }
        }

    }

    #region 记录错误日志2014/10/23 by lzy
    /// <summary>
    /// 记录错误日志2014/10/23 by lzy
    /// </summary>
    public class ErrorLog
    {
        public static void Write(ExceptionContext filterContext)
        {
            string logPath = ConfigurationManager.AppSettings["SysLogSavePath"].ToString();
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            logPath = string.Format("{0}/ERROR{1}.txt", logPath, DateTime.Now.ToString("yyyyMMdd"));
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(logPath, true, Encoding.GetEncoding("gb2312"));
                writer.WriteLine(" -------------------------------------【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "】-------------------------------------------");
                writer.WriteLine("");
                writer.WriteLine("== URL：" + filterContext.HttpContext.Request.Url);
                writer.WriteLine("");
                writer.WriteLine("== ErrorMessage：" + filterContext.Exception.Message);
                writer.WriteLine("");
                writer.WriteLine("== MethodName：" + filterContext.RouteData.Values["action"]);
                writer.WriteLine("");
                writer.WriteLine("== Controller：" + filterContext.Controller);
                writer.WriteLine("");
                writer.WriteLine("== 错误源：" + filterContext.Exception.Source);
                writer.WriteLine("");
                writer.WriteLine("== 堆栈信息：\r\n" + filterContext.Exception.StackTrace);
                writer.WriteLine("");
                writer.WriteLine(" ---------------------------------------------------------------------------------------------------");
                writer.WriteLine("");
            }
            finally
            {
                if (writer != null)
                {
                    writer.Flush();
                    writer.Close();
                }
            }
        }
    }
    #endregion
}