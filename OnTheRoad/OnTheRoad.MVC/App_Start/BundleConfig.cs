using System.Web;
using System.Web.Optimization;

namespace OnTheRoad.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/Content/Scripts/js").Include(
                      "~/Content/Scripts/SiteNavigation.js"));

            bundles.Add(new StyleBundle("~/Content/Styles/css").Include(
                      "~/Content/Styles/bootstrap.css",
                      "~/Content/Styles/site.css",
                      "~/Content/Styles/AddTrip.css",
                      "~/Content/Styles/Admin.css",
                      "~/Content/Styles/DataPager.css",
                      "~/Content/Styles/Home.css",
                      "~/Content/Styles/LoginRegister.css",
                      "~/Content/Styles/SiteNavigation.css",
                      "~/Content/Styles/Trips.css",
                      "~/Content/Styles/TripsContainer.css",
                      "~/Content/Styles/UserProfile.css"));
        }
    }
}
