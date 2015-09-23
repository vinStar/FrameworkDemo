using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace WebSite
{
    public static class StringExtensions
    {
        #region HtmlHelper extensions

        /// <summary>
        /// 截取字符串长度
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="length">截取长度</param>
        /// <returns></returns>
        public static string SubString(this HtmlHelper htmlHelper, string str, int length)
        {
            if (string.IsNullOrEmpty(str)) { return ""; };
            if (str.Length > length)
            {
                str = str.Substring(0, length - 2) + "...";
            }
            return str;
        }

        /// <summary>
        /// 替换字符串中指定字符
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="str">源字符串</param>
        /// <param name="newChar">替换字符</param>
        /// <param name="startIndex">开始索引</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string ReplaceString(this HtmlHelper htmlHelper, string str, char newChar, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(str) || (startIndex + length - 1) > str.Length) { return ""; };
            StringBuilder sb = new StringBuilder();
            if (startIndex > 0)
            {
                sb.Append(str.Substring(0, startIndex));
            }
            for (int i = 0; i < length; i++)
            {
                sb.Append(newChar);
            }
            sb.Append(str.Substring(startIndex + length));

            return sb.ToString();
        }
        #endregion
    }
}