using System;
using System.IO;
using System.Web.Mvc;
using OpenFileSystem.IO;
using TomHarvey.Admin.Business.Interfaces;
using TomHarvey.Core.Helpers;
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

        public ImagesController(IPortfolioItemsRepository portfolioItemsRepository,
                                IPortfolioImagesRepository portfolioImagesRepository,
                                IFileSystem fileSystem,
                                ISettingsRepository settingsRepository)
        {
            _portfolioItemsRepository = portfolioItemsRepository;
            _portfolioImagesRepository = portfolioImagesRepository;
            _fileSystem = fileSystem;
            _settingsRepository = settingsRepository;
        }

        public ActionResult Portfolio(int id, int additional)
        {
            var portfolioItem = _portfolioItemsRepository.GetById(id);
            if (portfolioItem == null || portfolioItem.Removed)
                return new HttpNotFoundResult();

            var portfolioImage = _portfolioImagesRepository.GetById(additional);
            if (portfolioImage == null || portfolioImage.Removed)
                return new HttpNotFoundResult();

            try
            {
                var filePath = Path.Combine(_settingsRepository.StorageBaseDirectory(),
                                        Path.DirectorySeparatorChar.ToString(),
                                        portfolioImage.FileName);
                var file = _fileSystem.GetFile(filePath);
                byte[] fileContent;
                using (var fileStream = file.OpenRead())
                    fileContent = fileStream.ToByteArray();

                return File(fileContent, "image/jpeg", portfolioImage.FileName);
            }
            catch (FileNotFoundException)
            {
                return new HttpNotFoundResult();
            }
        }

        public ActionResult Service(int id, int additional)
        {
            throw new NotImplementedException();
        }
    }
}