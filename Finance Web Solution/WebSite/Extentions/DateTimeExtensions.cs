using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace WebSite
{
    public static class DateTimeExtensions
    {
        #region HtmlHelper extensions
        /// <summary>
        /// Format: yyyy年M月d日
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatDate(this HtmlHelper htmlHelper, DateTime date)
        {
            return string.Format("{0:yyyy年M月d日}", date);
        }
        /// <summary>
        /// Format: yyyy年M月d日 HH:mm
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatDateTime(this HtmlHelper htmlHelper, DateTime date)
        {
            return string.Format("{0:yyyy年M月d日 HH:mm}", date);
        }
        /// <summary>
        /// Format: yyyy-MM-dd
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatDateUSAL(this HtmlHelper htmlHelper, DateTime date)
        {
            return string.Format("{0:yyyy-MM-dd}", date);
        }
        /// <summary>
        /// Format: yyyy/MM/dd
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatDateUSA(this HtmlHelper htmlHelper, DateTime date)
        {
            return string.Format("{0:yyyy/MM/dd}", date);
        }

        /// <summary>
        /// Crtaet Span Tag
        /// </summary>
        /// <param name="helper">Html.Span("Id","Content","CssStyle")</param>
        /// <param name="strId"></param>
        /// <param name="strcontent"></param>
        /// <param name="strClass"></param>
        /// <returns></returns>
        public static string Span(this HtmlHelper helper, string strId, string strcontent, string strClass)
        {
            TagBuilder builder = new TagBuilder("span");

            builder.GenerateId(strId);
            builder.SetInnerText(strcontent);
            builder.AddCssClass(strClass);
            builder.MergeAttribute("title", "span!");
            return builder.ToString();
        }

        #endregion
    }
}