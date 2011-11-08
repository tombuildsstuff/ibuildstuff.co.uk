using System;
using System.Linq;
using System.Web.Mvc;
using TomHarvey.Admin.Business.Interfaces;
using TomHarvey.Website.Models.Portfolio;
using WeBuildStuff.PageManagement.Business.Interfaces;

namespace TomHarvey.Website.Controllers
{
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
            var page = _pageDetailsRepository.GetPageDetailsByName("Portfolio");
            var revision = _pageRevisionsRepository.GetLatestRevisionForPage(page.Id);
            var portfolioItems = _portfolioItemsRepository.GetAllItems();
            return View("Index", new PortfolioOverview(revision, portfolioItems));
        }

        public ActionResult Details(string name)
        {
            var item = string.IsNullOrWhiteSpace(name) ? null : _portfolioItemsRepository.GetByUrl(name);
            if (item == null || item.Removed)
                return new HttpNotFoundResult();

            var images = _portfolioImagesRepository.GetAllForPortfolioItem(item.Id);
            var otherPortfolioItems = _portfolioItemsRepository.GetAllItems().Where(pi => pi.Id != item.Id).ToList();
            return View("Details", new PortfolioDetails(new PortfolioItemDetails(item, images), otherPortfolioItems));
        }

        public ActionResult Image(int id, int additional)
        {
            throw new NotImplementedException();
        }

        // TODO: images for a portfolio item..
    }
}