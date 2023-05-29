using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Optimization;

namespace AspMvcAuth
{
    [ExcludeFromCodeCoverage]
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bcj/js").Include(
                "~/js/jquery.js",
                "~/js/bootstrap.min.js",
                "~/js/jquery.scrollUp.min.js",
                "~/js/price-range.js",
                "~/js/jquery.prettyPhoto.js"
                ));


            //bundles.Add(new ScriptBundle("~/bcj/tweetable").Include(
            //           "~/Scripts/tweetable.jquery.js",
            //           "~/Scripts/jquery.timeago.js",
            //           "~/Scripts/jquery.form.js"
            //    ));

            //bundles.Add(new ScriptBundle("~/bcj/loaded").Include(
            //           "~/Scripts/imagesloaded.js",
            //           "~/Scripts/custom.js"
            //       ));


            bundles.Add(new StyleBundle("~/bcj/css").Include(
                      "~/Content/css/jquery-ui.css",
                      "~/Content/css/style.css",
                      "~/Content/css/icomoon.css",
                      "~/Content/css/superfish.css",
                      "~/Content/commonStyle.css",
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/prettyPhoto.css",
                      "~/Content/css/price-range.css",
                      "~/Content/animate.css",
                      "~/Content/css/main.css",
                      "~/Content/css/responsive.css"
                  ));



        }
    }
}
