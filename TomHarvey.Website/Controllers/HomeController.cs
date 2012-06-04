namespace TomHarvey.Website.Controllers
{
    using System.Configuration;
    using System.Linq;
    using System.Web.Mvc;

    using MvcBlog.Repositories;

    using TomHarvey.Website.Models.Home;

    using WeBuildStuff.CMS.Business.OpenSource.Interfaces;
    using WeBuildStuff.CMS.Business.Pages.Interfaces;
    using WeBuildStuff.CMS.Business.Portfolio.Interfaces;

    public class HomeController : BaseController
    {
        private readonly IPageDetailsRepository _pageDetailsRepository;
        private readonly IPageRevisionsRepository _pageRevisionsRepository;
        private readonly IPostsRepository _postsRepository;
        private readonly IPortfolioItemsRepository _portfolioItemsRepository;
        private readonly IOpenSourceProjectDetailsRepository _openSourceProjectDetailsRepository;

        public HomeController(IPageDetailsRepository pageDetailsRepository,
                              IPageRevisionsRepository pageRevisionsRepository,
                              IPostsRepository postsRepository,
                              IPortfolioItemsRepository portfolioItemsRepository,
                              IOpenSourceProjectDetailsRepository openSourceProjectDetailsRepository)
        {
            _pageDetailsRepository = pageDetailsRepository;
            _pageRevisionsRepository = pageRevisionsRepository;
            _postsRepository = postsRepository;
            _portfolioItemsRepository = portfolioItemsRepository;
            _openSourceProjectDetailsRepository = openSourceProjectDetailsRepository;
        }

        public ViewResult Index()
        {
            var page = _pageDetailsRepository.GetByName("Home");
            var revision = _pageRevisionsRepository.GetLatestRevision(page.Id);
            var blogBaseUrl = ConfigurationManager.AppSettings["BlogBaseUrl"];
            var blogPosts = _postsRepository.GetAllPublishedPosts().OrderByDescending(p => p.PublishDate.Value).Take(10).ToList();
            var portfolioItems = _portfolioItemsRepository.GetAll().OrderBy(p => p.Title).ToList();
            var openSourceProjects = _openSourceProjectDetailsRepository.GetAll().OrderBy(o => o.Title).ToList();
            var model = new HomePageOverview(revision, blogBaseUrl, blogPosts, portfolioItems, openSourceProjects);
            return View("Index", model);
        }
    }
}