using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;
using HyBy.FrameWork.DAService;
using HyBy.Trading.BusinessEntity;
using HyBy.Trading.BusinessInterface;
using HyBy.Trading.BusinessImplement;
using System.Configuration;

namespace WebSite.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        //出纳页面
        public ActionResult CashierIndex(string type)
        {
            ViewBag.type = type;
            return View();
        }
    }
}