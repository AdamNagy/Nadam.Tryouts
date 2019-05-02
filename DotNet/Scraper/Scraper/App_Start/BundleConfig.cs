using System.Web;
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
                      "~/web_modules/bootstrap/bootstrap.min.js",
                      "~/web_modules/bootstrap/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/styles/bootstrap").Include(
                      "~/web_modules/bootstrap/bootstrap.min.css",
                      "~/web_modules/bootstrap/bootstrap-grid.min.css",
                      "~/web_modules/bootstrap/bootstrap-reboot.min.css"));
            // </bootstrap>

            // <jquery>
            bundles.Add(new ScriptBundle("~/scripts/jquery").Include(
                        "~/web_modules/jquery/jquery-v3.4.0.js",
                        "~/web_modules/jquery/jquery-ui.min.js"));

            bundles.Add(new StyleBundle("~/styles/jquery").Include(
                    "~/web_modules/jquery/jquery-ui.min.css",
                    "~/web_modules/jquery/jquery-ui.structure.min.css",
                    "~/web_modules/jquery/jquery-ui.theme.min.css"));
            // </jquery>

            // <nadam>
            bundles.Add(new ScriptBundle("~/scripts/nadam").Include(
                        "~/web_modules/nadam/nadam.dom-filter.js",
                        "~/web_modules/nadam/nadam.http.js",
                        "~/web_modules/nadam/nadam.side-pager.component.js"));

            bundles.Add(new StyleBundle("~/styles/nadam").Include(
                        "~/web_modules/nadam/nadam.side-pager.component.css"));
            // </nadam>

            // <app>
            bundles.Add(new StyleBundle("~/styles/app").Include(
                    "~/web_modules/app/app.css"));

            bundles.Add(new ScriptBundle("~/scripts/app").Include(
                    "~/web_modules/app/app.js"));
            // </app>
        }
    }
}
