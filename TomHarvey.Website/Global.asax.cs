using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FluentValidation.Attributes;
using FluentValidation.Mvc;
using NHibernate;
using NHibernate.Cfg;
using TomHarvey.Admin.Business.Interfaces;
using TomHarvey.Admin.Fakes;
using TomHarvey.Core.CastleWindsor;
using TomHarvey.Core.Communication.Emailing;
using WeBuildStuff.PageManagement.Business.Interfaces;
using WeBuildStuff.PageManagement.Data.NHibernate;
using WeBuildStuff.Services.Business.Interfaces;
using WeBuildStuff.Services.Data.NHibernate;
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

            routes.MapRoute("GetInTouchRoute", "get-in-touch/{action}/{id}", new { controller = "getintouch", action = "index", id = UrlParameter.Optional });
            routes.MapRoute("OpenSourceRoute", "open-source/{id}", new { controller = "opensource", action = "details" });
            routes.MapRoute("OpenSourceMainPage", "open-source", new { controller = "opensource", action = "index" });
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

                _container.Register(Component.For<ISessionFactory>().Instance(_sessionFactory));
                _container.Register(Component.For<IPageDetailsRepository>().ImplementedBy<PageDetailsRepository>());
                _container.Register(Component.For<IPageRevisionsRepository>().ImplementedBy<PageRevisionsRepository>());
                _container.Register(Component.For<IServiceDetailsRepository>().ImplementedBy<ServiceDetailsRepository>());
                _container.Register(Component.For<IServicePhotosRepository>().ImplementedBy<ServicePhotosRepository>());

                // BUG: TODO: complete PROPER component registration
                _container.Register(Component.For<IEmailMailerService>().ImplementedBy<ImmediateEmailMailerService>());
                _container.Register(Component.For<IPortfolioItemsRepository>().ImplementedBy<FakePortfolioItemsRepository>());
                _container.Register(Component.For<IPortfolioImagesRepository>().ImplementedBy<FakePortfolioImagesRepository>());
                _container.Register(Component.For<IOpenSourceProjectDetailsRepository>().ImplementedBy<FakeOpenSourceProjectDetailsRepository>());
                _container.Register(Component.For<IOpenSourceProjectLinksRepository>().ImplementedBy<FakeOpenSourceProjectLinksRepository>());
            }
            AreaRegistration.RegisterAllAreas();

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(_container));
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}