using System.Web.Mvc;
using TomHarvey.Core.Mvc.Attributes;

namespace TomHarvey.Website.Controllers
{
    [CheckIsUnavailable(OfflineController = "offline", OfflineAction = "index")] // check if this particular section is offline..
    public abstract class BaseController : Controller
    {
    }
}