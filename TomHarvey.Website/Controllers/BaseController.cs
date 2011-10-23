using System.Web.Mvc;

namespace TomHarvey.Website.Controllers
{
    public class BaseController : Controller
    {
        // TODO: selected menu item
        protected string WebsiteBaseUrl
        {
            get { return HttpContext.Request.Url.Host; }
        }
    }
}