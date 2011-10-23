using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FluentValidation.Attributes;
using FluentValidation.Mvc;
using NHibernate;
using NHibernate.Cfg;
using TomHarvey.Core.CastleWindsor;
using WeBuildStuff.Shared.ComponentRegistration;

namespace TomHarvey.Website
{
    public class MvcApplication : HttpApplication
    {
        private static IWindsorContainer _container;
        private static ISessionFactory _sessionFactory;

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
            routes.MapRoute("SEOSitemap", "sitemap.xml", new { controller = "searchengineoptimisation", action = "sitemap" });
            routes.MapRoute("SEORobots", "robots.txt", new { controller = "searchengineoptimisation", action = "robots" });
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            // fluent validation
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new AttributedValidatorFactory()));

            // windsor
            if (_sessionFactory == null)
                _sessionFactory = new Configuration().Configure().BuildSessionFactory();

            if (_container == null)
            {
                var assemblies = AllTypes.FromAssemblyInDirectory(new AssemblyFilter(HttpRuntime.BinDirectory));
                _container = new WindsorContainer();
                _container.Register(assemblies.BasedOn<IController>().Configure(c => c.LifeStyle.Transient));
                _container.Register(assemblies.BasedOn<ISharedComponentRegistration>().Configure(c => c.LifeStyle.Transient));
                foreach (var component in _container.ResolveAll<ISharedComponentRegistration>())
                    component.RegisterAllComponents(ref _container);
            }
            AreaRegistration.RegisterAllAreas();

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(_container));
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}