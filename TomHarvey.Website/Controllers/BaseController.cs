namespace TomHarvey.Website.Controllers
{
    using System.Web.Mvc;

    using WeBuildStuff.CMS.Mvc.Attributes;

    [CheckIsUnavailable(OfflineController = "offline", OfflineAction = "index")] // check if this particular section is offline..
    public abstract class BaseController : Controller
    {
    }
}