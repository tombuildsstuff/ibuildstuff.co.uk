using System.Web.Mvc;
using TomHarvey.Admin.Business.Portfolio.Interfaces;
using TomHarvey.Website.Models.SearchEngineOptimisation;
using WeBuildStuff.PageManagement.Business.Interfaces;
using WeBuildStuff.Services.Business.Interfaces;
using WeBuildStuff.Shared.Settings;

namespace TomHarvey.Website.Controllers
{
    public class SearchEngineOptimisationController : BaseController
    {
        private readonly IPageDetailsRepository _pageDetailsRepository;
        private readonly IPortfolioItemsRepository _portfolioItemsRepository;
        private readonly IServiceDetailsRepository _serviceDetailsRepository;
        private readonly ISettingsRepository _settingsRepository;

        public SearchEngineOptimisationController(IPageDetailsRepository pageDetailsRepository,
                                                  IPortfolioItemsRepository portfolioItemsRepository,
                                                  IServiceDetailsRepository serviceDetailsRepository,
                                                  ISettingsRepository settingsRepository)
        {
            _pageDetailsRepository = pageDetailsRepository;
            _portfolioItemsRepository = portfolioItemsRepository;
            _serviceDetailsRepository = serviceDetailsRepository;
            _settingsRepository = settingsRepository;
        }

        public ActionResult Robots()
        {
            var websiteBaseUrl = _settingsRepository.WebsiteBaseUrl();
            return XmlView("robots", new RobotsModel(websiteBaseUrl));
        }

        public ActionResult Sitemap()
        {
            var pages = _pageDetailsRepository.GetAllPagesToDisplayInSearchEngine();
            var portfolioItems = _portfolioItemsRepository.GetAllItems();
            var services = _serviceDetailsRepository.GetAllServiceDetails();
            var websiteBaseUrl = _settingsRepository.WebsiteBaseUrl();
            return XmlView("sitemap", new SitemapInformation(websiteBaseUrl, pages, portfolioItems, services));
        }

        private ActionResult XmlView(string view, object model)
        {
            return new XmlViewResult().XmlView(view, model);
        }
    }
}