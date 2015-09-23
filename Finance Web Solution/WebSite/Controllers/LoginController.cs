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
using HyBy.FrameWork.DAService.ExCommon;

namespace WebSite.Controllers
{
    public class LoginController : BaseController
    {
        //
        // GET: /Login/
        public ActionResult Index(string id = "0")
        {
            if (id != "0")
            {
                return WPSIndex(id);
            }
            return View();
        }

        /// <summary>
        /// 用于WPS免登陆
        /// </summary>
        /// <param name="loginCardNo">07979042</param>
        /// <returns>ActionResult</returns>
        public ActionResult WPSIndex(string loginCardNo = "0")
        {
            if (loginCardNo != "0")
            {
                UserLoginViewModel userViewModel = new UserLoginViewModel();
                userViewModel.LoginCardNo = Convert.ToString(loginCardNo);
                userViewModel.Password = Convert.ToString(loginCardNo);
                userViewModel.IsLoginByWPS = true;
                return Index(userViewModel);
            }

            return View("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserLoginViewModel userViewModel)
        {
            //验证卡号
            if (string.IsNullOrEmpty(userViewModel.LoginCardNo))
            {
                ModelState.AddModelError("LoginCardNo", "请输入卡号!");
                return Index();
            }
            //验证密码
            if (string.IsNullOrEmpty(userViewModel.Password))
            {
                ModelState.AddModelError("Password", "请输入密码!");
                return Index();
            }

            //登陆信息
            CM_UserListEntity userListModel = GetUserInfoByCardNo(userViewModel.LoginCardNo);
            if (userListModel == null || userListModel.ZhuangTai == 0 || userListModel.ZhuangTai == 5)
            {
                ModelState.AddModelError("Password", "用户不存在或离职!");
                return Index();
            }
            //角色信息
            //rsgl_jueseEntity jueSeEntity = GetRoleById(Convert.ToString(userListModel.u_jibie));
            //wps 登陆则不验证密码
            if (!userViewModel.IsLoginByWPS)
            {
                //加密输入的密码
                var pwd = CommHelper.GetMd5Hash(userViewModel.Password);
                //比较查询与输入的密码
                if (pwd != userListModel.Pwd)
                {
                    ModelState.AddModelError("Password", "用户密码错误!");
                    return Index();
                }
            }
            //MessageHelper.WriteLog("1111" + Convert.ToString(userListModel.u_jibie));


            SetLoginCookie(userListModel);
            //如果地区是41 并且级别是54 或者55  为资金部财务登陆
            if (userListModel.DiquID == 41 && (userListModel.JiBie == 55 || userListModel.JiBie == 54)) //集团出纳
            {
                return RedirectToAction("Index", "Home", new { area = "Fund" }); ;
            }
            else if (userListModel.isGroup == 0 && userListModel.JiBie == 54)//会计
            {
                return RedirectToAction("Index", "Home", new { area = "Invest" });
            }
            else if (userListModel.isGroup == 0 && userListModel.JiBie == 55) //出纳
            {
                return RedirectToAction("CashierIndex", "Home");
            }
            else if ((userListModel.isGroup == 1 && userListModel.JiBie == 167) || userListModel.JiBie > 1000) //集团会计
            {
                return RedirectToAction("Index", "Home", new { area = "Group" }); ;
            }
            else if ((userListModel.isGroup == 1 && userListModel.JiBie == 55) || userListModel.JiBie > 1000) //集团出纳
            {
                return RedirectToAction("Index", "Home", new { area = "Group" }); ;
            }
            else
            {
                ModelState.AddModelError("Password", "非财务角色!");
                return Index();
            }

        }


        #region 设置登录后的cookie
        /// <summary>
        /// 设置登录后的cookie
        /// </summary>
        /// <param name="userListModel"></param>
        private void SetLoginCookie(CM_UserListEntity userListModel)
        {
            if (HttpContext.Request.Cookies["RSUser"] != null)
            {
                HttpContext.Request.Cookies["RSUser"].Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies.Add(HttpContext.Request.Cookies["RSUser"]);
            }

            HttpCookie myCookie = new HttpCookie("RSUser");

            //WriteLog.LogResult(dt.Rows[0]["u_kahao"].ToString(), dt.Rows[0]["u_diqu"].ToString());//日志
            myCookie.Values["u_id"] = Convert.ToString(userListModel.UserID);
            myCookie.Values["u_kahao"] = userListModel.UserKahao;
            //myCookie.Values["u_xingming"] = HttpUtility.UrlEncode(userListModel.UserName);
            myCookie.Values["u_xingming"] = HttpUtility.UrlEncode(userListModel.UserName);
            string dq = Convert.ToString(userListModel.DiquID);
            myCookie.Values["u_diqu"] = dq;
            myCookie.Values["u_zhiwu"] = Convert.ToString(userListModel.ZhiWu);
            myCookie.Values["u_bumen"] = Convert.ToString(userListModel.BuMen);
            myCookie.Values["u_jibie"] = Convert.ToString(userListModel.JiBie);
            myCookie.Values["u_zhuangtai"] = Convert.ToString(userListModel.ZhuangTai);
            //myCookie.Values["u_gzJinCheng"] = Convert.ToString(userListModel.u_gzJinCheng);
            //rsgl_jueseEntity jueSeEntity = GetRoleById(Convert.ToString(userListModel.u_jibie));
            myCookie.Values["js_IsGroup"] = Convert.ToString(userListModel.isGroup);
            myCookie.Expires = DateTime.Now.AddDays(1);

            //添加cookie 
            HttpContext.Response.Cookies.Add(myCookie);
        }
        #endregion


        public CM_UserListEntity GetUserInfoByCardNo(string cardNo)
        {
            CM_UserListEntity userListModel = new CM_UserListEntity();
            userListModel.UserKahao = cardNo;
            Dictionary<string, object> args = new Dictionary<string, object>();

            List<CM_UserListEntity> listUser = null;
            using (ICM_UserListService userlist = new CM_UserListService(new DataAccess("defaultRead")))
            {
                listUser = userlist.GetCM_UserListCollection(args);
            }


            using (ICM_UserListService userlist = new CM_UserListService())
            {
                userListModel = listUser[10];
                userListModel.UserKahao = "01020102";
                var list = userlist.InsertCM_UserListEntity(userListModel);
            }



            SearchEntity entity = new SearchEntity("GetCM_UserListEntityByKahao", System.Data.CommandType.StoredProcedure);
            entity["UserKahao"] = cardNo;
            using (IDbExecuter service = new ExCommonService())
            {
                var uList = service.ExecuteSelect<CM_UserListEntity>(entity).ToList();
                userListModel = uList.Count > 0 ? uList[0] : null;
            }






            return userListModel;

        }
    }
}