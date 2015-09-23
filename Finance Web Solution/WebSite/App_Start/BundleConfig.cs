using System.Web;
using System.Web.Optimization;

namespace WebSite
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            //salary
            bundles.Add(new ScriptBundle("~/bundles/base").Include(
                        "~/Scripts/js/jquery-1.8.3.min.js",
                        "~/Scripts/js/rxued.js",
                        "~/Scripts/js/listbase.js"));

            //date97  
            bundles.Add(new ScriptBundle("~/bundles/date").Include(
                        "~/Scripts/My97DatePicker/WdatePicker.js"
                        ));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            bundles.Add(new StyleBundle("~/Content/salary").Include(
                "~/Content/CSS/base.css",
                "~/Content/CSS/sub.css",
                "~/Content/CSS/layout.css",
                "~/Content/CSS/alert.css",
                "~/Content/CSS/listcase.css",
                "~/Content/CSS/color.css"));
            //fund
            bundles.Add(new StyleBundle("~/Content/fund").Include(
               "~/Content/CSS/base.css",
               "~/Content/CSS/sub.css",
               "~/Content/CSS/layout.css",
               "~/Content/CSS/alert.css",
               "~/Content/CSS/lvse/Inside_color.css"));
            
            //Voucher1
            bundles.Add(new StyleBundle("~/Content/Voucher1").Include(
                "~/Content/CSS/common.css",
                "~/Content/CSS/pz.css"
                , "~/Content/CSS/ui.css"
                , "~/Content/CSS/voucher.css"
                ));
            bundles.Add(new ScriptBundle("~/bundles/Voucher1").Include(
            "~/Scripts/js/jquery-1.8.3.min.js",
            "~/Scripts/js/spinbox.js",
            "~/Scripts/js/jquery.combo.js",
            "~/Scripts/js/radixPoint.js"));

        }






    }
}
