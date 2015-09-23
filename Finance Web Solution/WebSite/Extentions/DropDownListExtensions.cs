using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace WebSite
{
    public static class DropDownListExtensions
    {
        /// <summary>
        /// 生成下拉列表
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="SelectListName">下拉列表的Name值</param>
        /// <param name="SelectItems">数据源</param>
        /// <param name="SelectedValue">选中值</param>
        /// <param name="Attributes">附加属性值，比如onchange=""之类</param>
        /// <returns></returns>
        public static string DropDownList(this HtmlHelper helper, string SelectListName, IEnumerable<SelectListItem> SelectItems, string SelectedValue, string Attributes)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<select");

            if (SelectListName.Trim() != "")
            {
                sb.Append(" name=\"" + SelectListName + "\"");
            }
            else
            {
                return "";
            }

            if (Attributes.Trim() != "")
            {
                sb.Append(" " + Attributes.Trim());
            }


            sb.Append(">");

            foreach (SelectListItem item in SelectItems)
            {
                if (item.Value == SelectedValue)
                {
                    sb.Append("<option value=\"" + item.Value + "\" selected=\"selected\">" + item.Text + "</option>");
                }
                else
                {
                    sb.Append("<option value=\"" + item.Value + "\">" + item.Text + "</option>");
                }
            }

            sb.Append("</select>");

            return sb.ToString();

        }

    }
}