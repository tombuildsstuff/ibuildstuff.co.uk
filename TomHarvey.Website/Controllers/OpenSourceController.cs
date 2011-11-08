using System.Web.Mvc;
using TomHarvey.Admin.Business.Interfaces;
using TomHarvey.Website.Models.OpenSource;
using WeBuildStuff.PageManagement.Business.Interfaces;

namespace TomHarvey.Website.Controllers
{
    public class OpenSourceController : BaseController
    {
        private readonly IPageDetailsRepository _pageDetailsRepository;
        private readonly IPageRevisionsRepository _pageRevisionsRepository;
        private readonly IOpenSourceProjectDetailsRepository _openSourceProjectDetailsRepository;
        private readonly IOpenSourceProjectLinksRepository _openSourceProjectLinksRepository;

        public OpenSourceController(IPageDetailsRepository pageDetailsRepository,
                                    IPageRevisionsRepository pageRevisionsRepository,
                                    IOpenSourceProjectDetailsRepository openSourceProjectDetailsRepository,
                                    IOpenSourceProjectLinksRepository openSourceProjectLinksRepository)
        {
            _pageDetailsRepository = pageDetailsRepository;
            _pageRevisionsRepository = pageRevisionsRepository;
            _openSourceProjectDetailsRepository = openSourceProjectDetailsRepository;
            _openSourceProjectLinksRepository = openSourceProjectLinksRepository;
        }

        public ViewResult Index()
        {
            var page = _pageDetailsRepository.GetPageDetailsByName("OpenSource");
            var revision = _pageRevisionsRepository.GetLatestRevisionForPage(page.Id);
            var projects = _openSourceProjectDetailsRepository.GetAll();
            return View("index", new OpenSourceProjectsOverview(revision, projects));
        }

        public ActionResult Details(string id)
        {
            var project = _openSourceProjectDetailsRepository.GetByUrl(id);
            if (project == null)
                return new HttpNotFoundResult();

            var links = _openSourceProjectLinksRepository.GetAllForProject(project.Id);
            return View("details", new OpenSourceProjectDetails(project, links /* images? */));
        }
    }
}