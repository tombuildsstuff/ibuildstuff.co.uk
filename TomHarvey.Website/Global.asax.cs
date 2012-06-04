namespace TomHarvey.Website
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using FluentValidation.Attributes;
    using FluentValidation.Mvc;

    using MvcBlog.Repositories;
    using MvcBlog.Repositories.SimpleData;

    using OpenFileSystem.IO;
    using OpenFileSystem.IO.FileSystems.Local;

    using WeBuildStuff.CMS.Business.OpenSource.Interfaces;
    using WeBuildStuff.CMS.Business.Pages.Interfaces;
    using WeBuildStuff.CMS.Business.Portfolio.Interfaces;
    using WeBuildStuff.CMS.Business.Services.Interfaces;
    using WeBuildStuff.CMS.Business.Settings.Interfaces;
    using WeBuildStuff.CMS.Data.Simple.OpenSource;
    using WeBuildStuff.CMS.Data.Simple.Pages;
    using WeBuildStuff.CMS.Data.Simple.Portfolio;
    using WeBuildStuff.CMS.Data.Simple.Services;
    using WeBuildStuff.CMS.DependencyInjection;
    using WeBuildStuff.CMS.DependencyInjection.CastleWindsor;
    using WeBuildStuff.CMS.Domain.Messaging.Email.Implementations;
    using WeBuildStuff.CMS.Domain.Messaging.Email.Interfaces;
    using WeBuildStuff.CMS.Domain.Settings;
    using WeBuildStuff.CMS.Mvc.ControllerFactories;

    public class MvcApplication : HttpApplication
    {
        private static IDependencyManager _dependencyManager;

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

            if (_dependencyManager == null)
            {
                _dependencyManager = new CastleWindsorDependencyManager();
                _dependencyManager.RegisterAllImplementationsInDirectory<IController>(HttpRuntime.BinDirectory);
                _dependencyManager.RegisterImplementation<IPageDetailsRepository, PageDetailsRepository>();
                _dependencyManager.RegisterImplementation<IPageRevisionsRepository, PageRevisionsRepository>();
                _dependencyManager.RegisterImplementation<IServiceDetailsRepository, ServiceDetailsRepository>();
                _dependencyManager.RegisterImplementation<IServicePhotosRepository, ServicePhotosRepository>();
                _dependencyManager.RegisterImplementation<ISettingsRepository, ConfigurationBasedSettingsRepository>();
                _dependencyManager.RegisterImplementation<IEmailService, ImmediateEmailService>();
                _dependencyManager.RegisterImplementation<IPortfolioItemsRepository, PortfolioItemsRepository>();
                _dependencyManager.RegisterImplementation<IPortfolioImagesRepository, PortfolioImagesRepository>();
                _dependencyManager.RegisterImplementation<IOpenSourceProjectDetailsRepository, OpenSourceProjectDetailsRepository>();
                _dependencyManager.RegisterImplementation<IOpenSourceProjectLinksRepository, OpenSourceProjectLinksRepository>();
                _dependencyManager.RegisterImplementation<IPostsRepository, PostsRepository>();
                _dependencyManager.RegisterInstance<IFileSystem>(LocalFileSystem.Instance);
            }

            AreaRegistration.RegisterAllAreas();

            ControllerBuilder.Current.SetControllerFactory(new DependencyManagerControllerFactory(_dependencyManager));
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}