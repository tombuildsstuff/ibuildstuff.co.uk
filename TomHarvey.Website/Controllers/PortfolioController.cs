using System.Linq;
using System.Web.Mvc;
using TomHarvey.Admin.Business.Interfaces;
using TomHarvey.Website.Models.Portfolio;

namespace TomHarvey.Website.Controllers
{
    public class PortfolioController : BaseController
    {
        private readonly IPortfolioItemsRepository _portfolioItemsRepository;
        private readonly IPortfolioImagesRepository _portfolioImagesRepository;
        public PortfolioController(IPortfolioItemsRepository portfolioItemsRepository, IPortfolioImagesRepository portfolioImagesRepository)
        {
            _portfolioItemsRepository = portfolioItemsRepository;
            _portfolioImagesRepository = portfolioImagesRepository;
        }

        public ViewResult Index()
        {
            return View("Portfolio", _portfolioItemsRepository.GetAllItems()
                                        .Select(pi => new PortfolioDetails(pi, _portfolioImagesRepository.GetAllForPortfolioItem(pi.Id)))
                                        .ToList());
        }

        public ActionResult Details(string name)
        {
            return Content(string.Format("portfolio for {0}", name));
        }
    }
}