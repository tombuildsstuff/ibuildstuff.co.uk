using System.Web.Mvc;
using WeBuildStuff.PageManagement.Business.Interfaces;

namespace TomHarvey.Website.Controllers
{
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