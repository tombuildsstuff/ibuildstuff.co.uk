using System.Web.Mvc;

namespace TomHarvey.Website.Admin.Controllers
{
    public class HomeController : Controller
    {
        public RedirectToRouteResult Index()
        {
            return RedirectToAction("index", "dashboard");
        }
    }
}