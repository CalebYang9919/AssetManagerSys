using System.Web;
using System.Web.Optimization;

namespace MvcUI
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/layui").Include(
                        "~/layui/layui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/Menu.js",
                         "~/Scripts/core.js",
                          "~/Scripts/jquery.dialog.js",
                           "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/nav.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/Login").Include(
                        "~/Scripts/jquery.select.js"));

            // 使用 Modernizr 的开发版本进行开发和了解信息。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/themes/css").Include(
                        "~/Content/themes/css/style.css",
                        "~/Content/themes/css/main.css",
                        "~/Content/themes/css/nav.css",
                        "~/Content/themes/css/jquery.dialog.css"));

            bundles.Add(new StyleBundle("~/Content/themes/css/Login").Include(
                "~/Content/themes/css/style.css",
             "~/Content/themes/css/login.css"));

            bundles.Add(new StyleBundle("~/Content/themes/css/Partial").Include(
                "~/Content/themes/css/Partial.css"));

            bundles.Add(new StyleBundle("~/Content/themes/css/layui").Include(
                                "~/layui/css/layui.css"));
        }
    }
}