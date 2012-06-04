namespace TomHarvey.Website.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using TomHarvey.Website.Models.OpenSource;

    using WeBuildStuff.CMS.Business.OpenSource.Interfaces;
    using WeBuildStuff.CMS.Business.Pages.Interfaces;

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
            var page = _pageDetailsRepository.GetByName("OpenSource");
            var revision = _pageRevisionsRepository.GetLatestRevision(page.Id);
            var projects = _openSourceProjectDetailsRepository.GetAll();
            return View("index", new OpenSourceProjectsOverview(revision, projects));
        }

        public ActionResult Details(string id)
        {
            var project = _openSourceProjectDetailsRepository.GetByUrl(id);
            if (project == null || project.DeletedByUserId.HasValue || project.DateDeleted.HasValue)
                return HttpNotFound();

            var links = _openSourceProjectLinksRepository.GetAllForProject(project.Id);
            var otherProjects = _openSourceProjectDetailsRepository.GetAll().Where(pd => pd.Id != project.Id).ToList();
            return View("details", new OpenSourceProjectDetails(project, links, otherProjects));
        }
    }
}