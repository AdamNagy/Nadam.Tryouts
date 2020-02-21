using System.Web.Optimization;

namespace ManifestRepositoryApi.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/scripts/admin-page").Include(
                        "~/Views/Admin/admin.js"));

            bundles.Add(new ScriptBundle("~/scripts/global").Include(
                        "~/Scripts/jquery-v3.4.0.js",
                        "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/styles/global").Include(
                      "~/Styles/bootstrap.min.css",
                      "~/Styles/Site.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}