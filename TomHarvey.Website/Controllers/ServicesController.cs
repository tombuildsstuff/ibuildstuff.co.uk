namespace TomHarvey.Website.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using TomHarvey.Website.Models.Services;

    using WeBuildStuff.CMS.Business.Pages.Interfaces;
    using WeBuildStuff.CMS.Business.Services.Interfaces;

    public class ServicesController : BaseController
    {
        private readonly IPageDetailsRepository _pageDetailsRepository;
        private readonly IPageRevisionsRepository _pageRevisionsRepository;
        private readonly IServiceDetailsRepository _serviceDetailsRepository;
        private readonly IServicePhotosRepository _servicePhotosRepository;

        public ServicesController(IPageDetailsRepository pageDetailsRepository,
                                  IPageRevisionsRepository pageRevisionsRepository,
                                  IServiceDetailsRepository serviceDetailsRepository,
                                  IServicePhotosRepository servicePhotosRepository)
        {
            _pageDetailsRepository = pageDetailsRepository;
            _pageRevisionsRepository = pageRevisionsRepository;
            _serviceDetailsRepository = serviceDetailsRepository;
            _servicePhotosRepository = servicePhotosRepository;
        }

        public ViewResult Index()
        {
            var page = _pageDetailsRepository.GetByName("Services");
            var content = _pageRevisionsRepository.GetLatestRevision(page.Id);
            var services = _serviceDetailsRepository.GetAll();
            return View("Index", new ServicesOverview(content, services));
        }

        public ActionResult Details(string name)
        {
            var service = string.IsNullOrWhiteSpace(name) ? null : _serviceDetailsRepository.GetByUrl(name);
            if (service == null)
                return new HttpNotFoundResult();


            var otherServices = _serviceDetailsRepository.GetAll().Where(s => s.Id != service.Id).ToList();
            var photos = _servicePhotosRepository.GetAllForService(service.Id);
            return View("Details", new ServiceInformation(service, photos, otherServices));
        }
    }
}