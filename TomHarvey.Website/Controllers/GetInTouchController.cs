using System.Web.Mvc;
using TomHarvey.Core.Communication.Emailing;
using TomHarvey.Website.Domain.GetInTouch;
using WeBuildStuff.Shared.Settings;

namespace TomHarvey.Website.Controllers
{
    public class GetInTouchController : BaseController
    {
        private readonly IEmailMailerService _emailService;
        private readonly ISettingsRepository _settingsRepository;

        public GetInTouchController(IEmailMailerService emailService, ISettingsRepository settingsRepository)
        {
            _emailService = emailService;
            _settingsRepository = settingsRepository;
        }

        public ViewResult Index()
        {
            return View("index", new ContactForm());
        }

        public ActionResult Send(ContactForm form)
        {
            var validator = new ContactForm.ContactFormValidator();
            var result = validator.Validate(form);
            if (result.IsValid)
            {
                // _emailService.SendEmail(form.ToEmailMessage);
                // TODO: send an email
                return RedirectToAction("thanks", "getintouch");
            }
            return View("index", form);
        }

        public ViewResult Thanks()
        {
            return View("thanks");
        }
    }
}