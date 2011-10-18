using System.Web.Mvc;
using TomHarvey.Core.Communication.Emailing;
using TomHarvey.Website.Domain.GetInTouch;

namespace TomHarvey.Website.Controllers
{
    public class GetInTouchController : BaseController
    {
        private readonly IEmailMailerService _emailService;
        public GetInTouchController(IEmailMailerService emailService)
        {
            _emailService = emailService;
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