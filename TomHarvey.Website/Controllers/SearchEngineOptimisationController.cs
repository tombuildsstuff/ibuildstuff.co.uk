using System.Web.Mvc;
using TomHarvey.Admin.Business.Interfaces;
using TomHarvey.Website.Models.SearchEngineOptimisation;
using WeBuildStuff.PageManagement.Business.Interfaces;
using WeBuildStuff.Services.Business.Interfaces;

namespace TomHarvey.Website.Controllers
{
    public class SearchEngineOptimisationController : BaseController
    {
        private readonly IPageDetailsRepository _pageDetailsRepository;
        private readonly IPortfolioItemsRepository _portfolioItemsRepository;
        private readonly IServiceDetailsRepository _serviceDetailsRepository;

        public SearchEngineOptimisationController(IPageDetailsRepository pageDetailsRepository, IPortfolioItemsRepository portfolioItemsRepository, IServiceDetailsRepository serviceDetailsRepository)
        {
            _pageDetailsRepository = pageDetailsRepository;
            _portfolioItemsRepository = portfolioItemsRepository;
            _serviceDetailsRepository = serviceDetailsRepository;
        }

        public ActionResult Robots()
        {
            return View("robots", new RobotsModel(WebsiteBaseUrl));
        }

        public ActionResult Sitemap()
        {
            var pages = _pageDetailsRepository.GetAllPagesToDisplayInSearchEngine();
            var portfolioItems = _portfolioItemsRepository.GetAllItems();
            var services = _serviceDetailsRepository.GetAllServiceDetails();

            return View("sitemap", new SitemapInformation(WebsiteBaseUrl, pages, portfolioItems, services));
        }
    }
}