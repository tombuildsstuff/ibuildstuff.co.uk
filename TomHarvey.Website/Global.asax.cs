namespace TomHarvey.Website
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    using FluentValidation.Attributes;
    using FluentValidation.Mvc;

    using OpenFileSystem.IO;
    using OpenFileSystem.IO.FileSystems.Local;

    using TomHarvey.Core.CastleWindsor;
    using TomHarvey.Core.Communication.Emailing;

    using WeBuildStuff.CMS.Business.OpenSource.Interfaces;
    using WeBuildStuff.CMS.Business.Pages.Interfaces;
    using WeBuildStuff.CMS.Business.Portfolio.Interfaces;
    using WeBuildStuff.CMS.Business.Services.Interfaces;
    using WeBuildStuff.CMS.Business.Settings.Interfaces;
    using WeBuildStuff.CMS.Data.Simple.OpenSource;
    using WeBuildStuff.CMS.Data.Simple.Pages;
    using WeBuildStuff.CMS.Data.Simple.Portfolio;
    using WeBuildStuff.CMS.Data.Simple.Services;
    using WeBuildStuff.CMS.Domain.Settings;

    public class MvcApplication : HttpApplication
    {
        private static IWindsorContainer _container;

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
            routes.MapRoute("PortfolioMainPage", "portfolio", new { controller = "portfolio", action = "index" });
            routes.MapRoute("ServicesElement", "services/{name}", new { controller = "services", action = "details" });
            routes.MapRoute("SEOSitemap", "sitemap.xml", new { controller = "searchengineoptimisation", action = "sitemap" });
            routes.MapRoute("SEORobots", "robots.txt", new { controller = "searchengineoptimisation", action = "robots" });

            routes.MapRoute("DefaultWithAdditional", "{controller}/{action}/{id}/{additional}");
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        protected void Application_Start()
        {
            // fluent validation
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new AttributedValidatorFactory()));

            if (_container == null)
            {
                var assemblies = AllTypes.FromAssemblyInDirectory(new AssemblyFilter(HttpRuntime.BinDirectory));
                _container = new WindsorContainer();
                _container.Register(assemblies.BasedOn<IController>().Configure(c => c.LifeStyle.Transient));

                _container.Register(Component.For<IPageDetailsRepository>().ImplementedBy<PageDetailsRepository>());
                _container.Register(Component.For<IPageRevisionsRepository>().ImplementedBy<PageRevisionsRepository>());
                _container.Register(Component.For<IServiceDetailsRepository>().ImplementedBy<ServiceDetailsRepository>());
                _container.Register(Component.For<IServicePhotosRepository>().ImplementedBy<ServicePhotosRepository>());
                _container.Register(Component.For<ISettingsRepository>().ImplementedBy<ConfigurationBasedSettingsRepository>());
                _container.Register(Component.For<IFileSystem>().Instance(LocalFileSystem.Instance));
                _container.Register(Component.For<IEmailMailerService>().ImplementedBy<ImmediateEmailMailerService>());
                _container.Register(Component.For<IPortfolioItemsRepository>().ImplementedBy<PortfolioItemsRepository>());
                _container.Register(Component.For<IPortfolioImagesRepository>().ImplementedBy<PortfolioImagesRepository>());
                _container.Register(Component.For<IOpenSourceProjectDetailsRepository>().ImplementedBy<OpenSourceProjectDetailsRepository>());
                _container.Register(Component.For<IOpenSourceProjectLinksRepository>().ImplementedBy<OpenSourceProjectLinksRepository>());
            }

            AreaRegistration.RegisterAllAreas();

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(_container));
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}