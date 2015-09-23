using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite
{
    public class ExceptionLogAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            ErrorLog.Write(filterContext);
            //base.OnException(filterContext);
            filterContext.HttpContext.Response.Redirect("/Error/Index");
        }
    }
}