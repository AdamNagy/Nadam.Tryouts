using System.Web.Optimization;

namespace Scraper
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {        
            // <bootstrap>
            bundles.Add(new ScriptBundle("~/scripts/bootstrap").Include(
                      "~/web-app-v1/bootstrap/bootstrap.min.js",
                      "~/web-app-v1/bootstrap/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/styles/bootstrap").Include(
                      "~/web-app-v1/bootstrap/bootstrap.min.css",
                      "~/web-app-v1/bootstrap/bootstrap-grid.min.css",
                      "~/web-app-v1/bootstrap/bootstrap-reboot.min.css"));
            // </bootstrap>

            // <jquery>
            bundles.Add(new ScriptBundle("~/scripts/jquery").Include(
                        "~/web-app-v1/jquery/jquery-v3.4.0.js",
                        "~/web-app-v1/jquery/jquery-ui.min.js"));

            bundles.Add(new StyleBundle("~/styles/jquery").Include(
                    "~/web-app-v1/jquery/jquery-ui.min.css",
                    "~/web-app-v1/jquery/jquery-ui.structure.min.css",
                    "~/web-app-v1/jquery/jquery-ui.theme.min.css"));
            // </jquery>

            // <nadam>
            bundles.Add(new ScriptBundle("~/scripts/nadam").Include(
                        "~/web-app-v1/nadam/nadam.dom-filter.js",
                        "~/web-app-v1/nadam/nadam.http.js",
                        "~/web-app-v1/nadam/nadam.side-pager.component.js",
                        "~/web-app-v1/nadam/nadam.removable.element.js"));

            bundles.Add(new StyleBundle("~/styles/nadam").Include(
                        "~/web-app-v1/nadam/nadam.side-pager.component.css"));
            // </nadam>

            // <app>
            bundles.Add(new StyleBundle("~/styles/app").Include(
                        "~/web-app-v1/app/Styles/app.css",
                        "~/web-app-v1/app/Styles/webhack.*"));

            bundles.Add(new ScriptBundle("~/scripts/app").Include(
                        "~/web-app-v1/app/Scripts/app.js",
                        "~/web-app-v1/app/Scripts/webhack.*"));
            // </app>

            // <v2>
            bundles.Add(new ScriptBundle("~/scripts/v2").Include(
                    "~/web-app-v2/dist/index.bundle.js"));
            // </v2>
        }
    }
}
