using System;
using System.Linq;
using System.Web.Mvc;
using TomHarvey.Website.Models.Services;
using WeBuildStuff.PageManagement.Business.Interfaces;
using WeBuildStuff.Services.Business.Interfaces;

namespace TomHarvey.Website.Controllers
{
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
            var page = _pageDetailsRepository.GetPageDetailsByName("Services");
            var content = _pageRevisionsRepository.GetLatestRevisionForPage(page.Id);
            var services = _serviceDetailsRepository.GetAllServiceDetails();
            return View("Index", new ServicesOverview(content, services));
        }

        public ActionResult Details(string name)
        {
            var service = string.IsNullOrWhiteSpace(name) ? null : _serviceDetailsRepository.GetByUrl(name);
            if (service == null || service.Deleted)
                return RedirectToAction("not-found");

            var otherServices = _serviceDetailsRepository.GetAllServiceDetails().Where(s => s.Id != service.Id).ToList();
            var photos = _servicePhotosRepository.GetAllPhotosForService(service.Id);
            return View("Details", new ServiceInformation(service, photos, otherServices));
        }

        [ActionName("not-found")]
        public ViewResult NotFound()
        {
            // TODO: status code should be 404..
            throw new NotImplementedException();
        }
        // TODO: images for a portfolio item..
    }
}