using System.Web.Mvc;
using TomHarvey.Core.Communication.Emailing;
using TomHarvey.Website.Domain.EmailGeneration;
using TomHarvey.Website.Domain.GetInTouch;
using TomHarvey.Website.Models.GetInTouch;
using WeBuildStuff.PageManagement.Business.Interfaces;
using WeBuildStuff.Shared.Settings;

namespace TomHarvey.Website.Controllers
{
    public class GetInTouchController : BaseController
    {
        public readonly IEmailMailerService EmailService;
        private readonly ISettingsRepository _settingsRepository;
        private readonly IPageDetailsRepository _pageDetailsRepository;
        private readonly IPageRevisionsRepository _pageRevisionsRepository;

        public GetInTouchController(IEmailMailerService emailService,
                                    ISettingsRepository settingsRepository,
                                    IPageDetailsRepository pageDetailsRepository,
                                    IPageRevisionsRepository pageRevisionsRepository)
        {
            EmailService = emailService;
            _settingsRepository = settingsRepository;
            _pageDetailsRepository = pageDetailsRepository;
            _pageRevisionsRepository = pageRevisionsRepository;
        }

        public ViewResult Index()
        {
            var page = _pageDetailsRepository.GetPageDetailsByName("GetInTouch");
            var revision = _pageRevisionsRepository.GetLatestRevisionForPage(page.Id);
            var form = new ContactForm();
            return View("index", new GetInTouchOverview(revision, form, null));
        }

        public ActionResult Send(ContactForm form)
        {
            var validator = new ContactForm.ContactFormValidator();
            var result = validator.Validate(form);

            if (result.IsValid)
            {
                var email = form.GenerateEmailMessage(_settingsRepository);
                 EmailService.SendEmail(email, _settingsRepository.CurrentEmailSettings());
                return RedirectToAction("thanks", "getintouch");
            }

            var page = _pageDetailsRepository.GetPageDetailsByName("GetInTouch");
            var revision = _pageRevisionsRepository.GetLatestRevisionForPage(page.Id);
            return View("index", new GetInTouchOverview(revision, form, result));
        }

        public ViewResult Thanks()
        {
            var page = _pageDetailsRepository.GetPageDetailsByName("GetInTouchThanks");
            var revision = _pageRevisionsRepository.GetLatestRevisionForPage(page.Id);
            return View("thanks", revision);
        }
    }
}