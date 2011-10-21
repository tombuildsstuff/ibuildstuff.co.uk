using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TomHarvey.Website
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("OpenSourceRoute", "open-source/{action}/{id}", new { controller = "opensource", action = "index", id = UrlParameter.Optional });
            routes.MapRoute("PortfolioElement", "portfolio/{name}", new { controller = "portfolio", action = "details" });
            routes.MapRoute("ServicesElement", "services/{name}", new { controller = "services", action = "details" });
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}