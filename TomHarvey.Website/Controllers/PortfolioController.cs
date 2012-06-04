namespace TomHarvey.Website.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using TomHarvey.Website.Models.Portfolio;

    using WeBuildStuff.CMS.Business.Pages.Interfaces;
    using WeBuildStuff.CMS.Business.Portfolio.Interfaces;

    public class PortfolioController : BaseController
    {
        private readonly IPageDetailsRepository _pageDetailsRepository;
        private readonly IPageRevisionsRepository _pageRevisionsRepository;
        private readonly IPortfolioItemsRepository _portfolioItemsRepository;
        private readonly IPortfolioImagesRepository _portfolioImagesRepository;
        public PortfolioController(IPageDetailsRepository pageDetailsRepository,
                                   IPageRevisionsRepository pageRevisionsRepository,
                                   IPortfolioItemsRepository portfolioItemsRepository,
                                   IPortfolioImagesRepository portfolioImagesRepository)
        {
            _pageDetailsRepository = pageDetailsRepository;
            _pageRevisionsRepository = pageRevisionsRepository;
            _portfolioItemsRepository = portfolioItemsRepository;
            _portfolioImagesRepository = portfolioImagesRepository;
        }

        public ViewResult Index()
        {
            var page = _pageDetailsRepository.GetByName("Portfolio");
            var revision = _pageRevisionsRepository.GetLatestRevision(page.Id);
            var portfolioItems = _portfolioItemsRepository.GetAll();
            return View("Index", new PortfolioOverview(revision, portfolioItems));
        }

        public ActionResult Details(string name)
        {
            var item = string.IsNullOrWhiteSpace(name) ? null : _portfolioItemsRepository.GetByUrl(name);
            if (item == null)
                return new HttpNotFoundResult();

            var images = _portfolioImagesRepository.GetAllForPortfolio(item.Id);
            var otherPortfolioItems = _portfolioItemsRepository.GetAll().Where(pi => pi.Id != item.Id).ToList();
            return View("Details", new PortfolioDetails(new PortfolioItemDetails(item, images), otherPortfolioItems));
        }
    }
}