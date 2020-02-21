using System.Web.Mvc;
using System.Web.Routing;

namespace ManifestRepositoryApi.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "admin",
                url: "admin",
                defaults: new { controller = "Admin", action = "Index" }
            );

            routes.MapRoute(
                name: "home",
                url: "home",
                defaults: new { controller = "Gallery", action = "Thumbnails" }
            );

            routes.MapRoute(
                name: "thumbnail-search",
                url: "home/{title}",
                defaults: new { controller = "Gallery", action = "ThumbnailsForTitle" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Gallery", action = "Thumbnails" }
            );
        }
    }
}