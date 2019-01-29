using System.Web;
using System.Web.Optimization;

namespace MyRLGProject
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/myscripts").Include(
                       "~/Scripts/jquery-3.3.1.min.js", "~/Scripts/bootstrap.min.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css", "~/Content/MyStyles.css"));
        }
    }
}
