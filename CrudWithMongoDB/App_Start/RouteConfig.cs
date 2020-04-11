using System.Web.Mvc;
using System.Web.Routing;

namespace CrudWithMongoDB
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { action = "GetAllEmployee", id = UrlParameter.Optional }
            );
        }
    }
}

