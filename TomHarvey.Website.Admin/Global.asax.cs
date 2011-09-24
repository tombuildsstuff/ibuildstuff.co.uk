using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Core.Resource;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using FluentValidation.Attributes;
using FluentValidation.Mvc;
using MvcContrib.UI.InputBuilder;
using NHibernate; 
using OpenFileSystem.IO;
using OpenFileSystem.IO.FileSystems.Local;
using TomHarvey.Core.CastleWindsor;
using WeBuildStuff.Shared.ComponentRegistration;
using Configuration = NHibernate.Cfg.Configuration;

namespace TomHarvey.Website.Admin
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
            _sessionFactory = new Configuration().Configure().BuildSessionFactory();
            _container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));
            _container.Register(Component.For<ISessionFactory>().Instance(_sessionFactory));

            var assemblies = AllTypes.FromAssemblyInDirectory(new AssemblyFilter(HttpRuntime.BinDirectory));
            _container.Register(assemblies.BasedOn<IController>().Configure(c => c.LifeStyle.Transient));
            _container.Register(assemblies.BasedOn<ISharedComponentRegistration>().Configure(c => c.LifeStyle.Transient));
            foreach (var component in _container.ResolveAll<ISharedComponentRegistration>())
                component.RegisterAllComponents(ref _container);

            _container.Register(Component.For<IFileSystem>().Instance(LocalFileSystem.Instance));
            _container.Register(Component.For<IWindsorContainer>().Instance(_container)); // yes, you read that right.

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(_container));

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            InputBuilder.BootStrap();
        }
    }
}