using System.Web.Mvc;
using System.Web.Routing;

namespace Scraper
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "V2",
                url: "v2/{action}/{id}",
                defaults: new { controller = "v2", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "v1", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
