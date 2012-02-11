using System.Web.Mvc;
using TomHarvey.Website.Models.SearchEngineOptimisation;

namespace TomHarvey.Website.Controllers
{
    using WeBuildStuff.CMS.Business.Pages.Interfaces;
    using WeBuildStuff.CMS.Business.Portfolio.Interfaces;
    using WeBuildStuff.CMS.Business.Services.Interfaces;
    using WeBuildStuff.CMS.Business.Settings.Interfaces;

    public class SearchEngineOptimisationController : BaseController
    {
        private readonly IPageDetailsRepository _pageDetailsRepository;

        private readonly IPortfolioItemsRepository _portfolioItemsRepository;

        private readonly IServiceDetailsRepository _serviceDetailsRepository;

        private readonly ISettingsRepository _settingsRepository;

        public SearchEngineOptimisationController(
            IPageDetailsRepository pageDetailsRepository,
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
            ControllerContext.HttpContext.Response.ContentType = "text/plain";
            return View("robots", new RobotsModel(websiteBaseUrl));
        }

        public ActionResult Sitemap()
        {
            var pages = _pageDetailsRepository.GetAllPagesToDisplayInSearchEngine();
            var portfolioItems = _portfolioItemsRepository.GetAllItems();
            var services = _serviceDetailsRepository.GetAllServiceDetails();
            var websiteBaseUrl = _settingsRepository.WebsiteBaseUrl();
            ControllerContext.HttpContext.Response.ContentType = "text/xml";
            return View("sitemap", new SitemapInformation(websiteBaseUrl, pages, portfolioItems, services));
        }
    }
}