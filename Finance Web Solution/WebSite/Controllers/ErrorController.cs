using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Controllers
{
    public class ErrorController : BaseController
    {
        //
        // GET: /Error/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IdentityError(string area)
        {
            string msg = string.Empty;
            switch (area)
            {
                case "Group":
                    msg = "您的账号不是集团会计或集团出纳";
                    break;
                case "Invest":
                    msg = "您的账号不是会计角色";
                    break;

                case "Cashier":
                    msg = "您的账号不是出纳角色";
                    break;
                case "None":
                    msg = "您的账号不是财务角色";
                    break;
            }
            ViewBag.ErrorMsg = msg;
            return View();
        }
    }
}