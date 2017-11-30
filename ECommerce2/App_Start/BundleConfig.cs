using System.Web;
using System.Web.Optimization;

namespace ECommerce2
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/moment.js",
                      "~/Scripts/fileupload.js",
                      "~/Scripts/ecommerce.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-responsive.css",
                      "~/Content/font-awesome-ie7.css",
                      "~/Content/font-awesome.css",
                      "~/Content/animate.css",
                      "~/Content/main.css",
                      "~/Content/prettyPhoto.css",
                      "~/Content/price-range.css",
                      "~/Content/bootstrap-datepicker.css",
                      "~/Content/responsive.css",
                      "~/Content/site.css"));
        }
    }
}
