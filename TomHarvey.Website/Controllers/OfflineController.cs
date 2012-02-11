using System.Web.Mvc;

namespace TomHarvey.Website.Controllers
{
    public class OfflineController : Controller
    {
        public ViewResult Index()
        {
            return View("SectionOffline");
        }

        public ViewResult Error()
        {
            return View("Unavailable");
        }
    }
}