namespace TomHarvey.Website.Controllers
{
    using System.IO;
    using System.Web.Mvc;

    using OpenFileSystem.IO;

    using TomHarvey.Core.Helpers;

    using WeBuildStuff.CMS.Business.Portfolio.Interfaces;
    using WeBuildStuff.CMS.Business.Services.Interfaces;
    using WeBuildStuff.CMS.Business.Settings.Interfaces;

    using Path = System.IO.Path;

    public class ImagesController : BaseController
    {
        private readonly IPortfolioItemsRepository _portfolioItemsRepository;
        private readonly IPortfolioImagesRepository _portfolioImagesRepository;
        private readonly IFileSystem _fileSystem;
        private readonly ISettingsRepository _settingsRepository;
        private readonly IServiceDetailsRepository _serviceDetailsRepository;
        private readonly IServicePhotosRepository _servicePhotosRepository;

        public ImagesController(IPortfolioItemsRepository portfolioItemsRepository,
                                IPortfolioImagesRepository portfolioImagesRepository,
                                IFileSystem fileSystem,
                                ISettingsRepository settingsRepository,
                                IServiceDetailsRepository serviceDetailsRepository,
                                IServicePhotosRepository servicePhotosRepository)
        {
            _portfolioItemsRepository = portfolioItemsRepository;
            _portfolioImagesRepository = portfolioImagesRepository;
            _fileSystem = fileSystem;
            _settingsRepository = settingsRepository;
            _serviceDetailsRepository = serviceDetailsRepository;
            _servicePhotosRepository = servicePhotosRepository;
        }

        public ActionResult Portfolio(int id, int additional)
        {
            var portfolioItem = _portfolioItemsRepository.GetById(id);
            if (portfolioItem == null)
                return new HttpNotFoundResult();

            var portfolioImage = _portfolioImagesRepository.GetById(additional);
            if (portfolioImage == null || portfolioImage.PortfolioId != id)
                return new HttpNotFoundResult();

            var bytes = GetImageContents("Portfolio", string.Format("{0}-Original.jpg", portfolioImage.Id));
            if (bytes == null)
                return new HttpNotFoundResult();

            return File(bytes, "image/jpeg", portfolioImage.FileName);
        }

        [ActionName("portfolio-medium")]
        public ActionResult PortfolioMedium(int id, int additional)
        {
            var portfolioItem = _portfolioItemsRepository.GetById(id);
            if (portfolioItem == null)
                return new HttpNotFoundResult();

            var portfolioImage = _portfolioImagesRepository.GetById(additional);
            if (portfolioImage == null || portfolioImage.PortfolioId != id)
                return new HttpNotFoundResult();

            var bytes = GetImageContents("Portfolio", string.Format("{0}-Medium.jpg", portfolioImage.Id));
            if (bytes == null)
                return new HttpNotFoundResult();

            return File(bytes, "image/jpeg", portfolioImage.FileName);
        }

        [ActionName("portfolio-main")]
        public ActionResult PortfolioMain(int id)
        {
            var main = _portfolioImagesRepository.GetMainForPortfolio(id);
            return RedirectToActionPermanent("portfolio-medium", new { id, additional = main.Id });
        }

        public ActionResult Service(int id, int additional)
        {
            var service = _serviceDetailsRepository.GetById(id);
            if (service == null)
                return new HttpNotFoundResult();

            var photo = _servicePhotosRepository.GetById(additional);
            if (photo == null || photo.ServiceId != id)
                return new HttpNotFoundResult();

            var bytes = GetImageContents("Services", string.Format("{0}-Original.jpg", photo.Id));
            if (bytes == null)
                return new HttpNotFoundResult();

            return File(bytes, "image/jpeg");
        }

        private byte[] GetImageContents(string folderName, string fileName)
        {
            try
            {
                var filePath = Path.Combine(_settingsRepository.StorageBaseDirectory(),
                                            folderName,
                                            fileName);
                var file = _fileSystem.GetFile(filePath);

                byte[] fileContent;

                using (var fileStream = file.OpenRead())
                    fileContent = fileStream.ToByteArray();

                return fileContent;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }
    }
}