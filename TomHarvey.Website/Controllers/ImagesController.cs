using System.IO;
using System.Web.Mvc;
using OpenFileSystem.IO;
using TomHarvey.Admin.Business.Interfaces;
using TomHarvey.Core.Helpers;
using WeBuildStuff.Services.Business.Interfaces;
using WeBuildStuff.Shared.Settings;
using Path = System.IO.Path;

namespace TomHarvey.Website.Controllers
{
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
            if (portfolioItem == null || portfolioItem.Removed)
                return new HttpNotFoundResult();

            var portfolioImage = _portfolioImagesRepository.GetById(additional);
            if (portfolioImage == null || portfolioImage.Removed || portfolioImage.PortfolioItemId != id)
                return new HttpNotFoundResult();

            var bytes = GetImageContents("Portfolio", portfolioImage.FileName);
            if (bytes == null)
                return new HttpNotFoundResult();

            return File(bytes, "image/jpeg", portfolioImage.FileName);
        }

        [ActionName("portfolio-main")]
        public ActionResult PortfolioMain(int id)
        {
            var main = _portfolioImagesRepository.GetMainForPortfolioItem(id);
            return RedirectToActionPermanent("portfolio", new { id, additional = main.Id });
        }

        public ActionResult Service(int id, int additional)
        {
            var service = _serviceDetailsRepository.GetById(id);
            if (service == null || service.Deleted)
                return new HttpNotFoundResult();

            var photo = _servicePhotosRepository.GetServicePhotoById(additional);
            if (photo == null || photo.Deleted || photo.ServiceId != id)
                return new HttpNotFoundResult();

            var bytes = GetImageContents("Services", string.Format("{0}.jpg", photo.Id));
            if (bytes == null)
                return new HttpNotFoundResult();

            return File(bytes, "image/jpeg");
        }

        private byte[] GetImageContents(string folderName, string fileName)
        {
            try
            {
                var filePath = Path.Combine(_settingsRepository.StorageBaseDirectory(),
                                            Path.DirectorySeparatorChar.ToString(),
                                            folderName,
                                            Path.DirectorySeparatorChar.ToString(),
                                            fileName);
                var file = _fileSystem.GetFile(filePath);
                byte[] fileContent;
                using (var fileStream = file.OpenRead())
                    fileContent = fileStream.ToByteArray();

                return fileContent;
            
            } catch (FileNotFoundException) {
                return null;
            }
        }
    }
}