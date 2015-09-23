using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebSite
{
    public static class RadioButtonListHelper
    {

        public static string RadioButtonList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> items)
        {
            return RadioButtonList(helper, name, items, null, null, "", int.MaxValue);
        }

        public static string RadioButtonList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> items, IDictionary<string, object> htmlAttributes)
        {
            return RadioButtonList(helper, name, items, htmlAttributes, null, "", int.MaxValue);
        }

        public static string RadioButtonList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> items, IDictionary<string, object> htmlAttributes, string selectedValue, string defaultValue)
        {
            return RadioButtonList(helper, name, items, htmlAttributes, selectedValue, defaultValue, int.MaxValue);
        }
        public static string RadioButtonList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> items, IDictionary<string, object> htmlAttributes, string selectedValue, string defaultValue, int maxAmount)
        {

            IList<string> itemValues = items.Select(p => p.Value).ToList();
            StringBuilder output = new StringBuilder();
            if (string.IsNullOrEmpty(selectedValue))
            {
                selectedValue = defaultValue;
            }

            if (!itemValues.Contains(selectedValue))
            {
                selectedValue = defaultValue;
            }
            int index = 0;
            foreach (var item in items)
            {
                index++;
                var rbValue = item.Value ?? item.Text;
                var rbId = name + "_" + rbValue;

                var radioButton = new TagBuilder("input");
                radioButton.MergeAttribute("id", rbId);
                radioButton.MergeAttribute("name", name);
                radioButton.MergeAttribute("type", "radio");
                radioButton.MergeAttribute("value", rbValue);
                if (!string.IsNullOrEmpty(selectedValue) && selectedValue == item.Value)
                {
                    radioButton.MergeAttribute("checked", "checked");
                }

                if (htmlAttributes != null)
                {
                    radioButton.MergeAttributes(htmlAttributes);
                }
                radioButton.SetInnerText(item.Text);

                var labFor = new TagBuilder("label ");
                labFor.MergeAttribute("for", rbId);
                labFor.MergeAttribute("id", rbId + "_Label");
                labFor.SetInnerText(item.Text);

                output.Append(radioButton.ToString(TagRenderMode.SelfClosing));
                output.Append(labFor);

                if (index >= maxAmount)
                {
                    index = 0;
                    output.Append("<br/>");

                }
            }
            return output.ToString();
        }
    }
}