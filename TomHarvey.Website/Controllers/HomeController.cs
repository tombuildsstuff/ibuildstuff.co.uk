using System.Web.Mvc;

namespace TomHarvey.Website.Controllers
{
    using WeBuildStuff.CMS.Business.Pages.Interfaces;

    public class HomeController : BaseController
    {
        private readonly IPageDetailsRepository _pageDetailsRepository;
        private readonly IPageRevisionsRepository _pageRevisionsRepository;
        public HomeController(IPageDetailsRepository pageDetailsRepository, IPageRevisionsRepository pageRevisionsRepository)
        {
            _pageDetailsRepository = pageDetailsRepository;
            _pageRevisionsRepository = pageRevisionsRepository;
        }

        public ViewResult Index()
        {
            var page = _pageDetailsRepository.GetPageDetailsByName("Home");
            var revision = _pageRevisionsRepository.GetLatestRevisionForPage(page.Id);
            return View("Index", revision);
        }
    }
}