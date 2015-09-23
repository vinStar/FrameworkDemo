using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class UserLoginViewModel
    {
        [DisplayName("登卡号")]
        [StringLength(50)]
        [Required(ErrorMessage = "请输入卡号")]
        public string LoginCardNo { get; set; }

        [DisplayName("登陆密码")]
        [StringLength(50)]
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }
        /// <summary>
        /// 是否WPS登录 默认false
        /// </summary>
        public bool IsLoginByWPS { get; set; }
    }
}