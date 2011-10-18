using System;
using System.Web.Mvc;

namespace TomHarvey.Website.Controllers
{
    public class PortfolioController : BaseController
    {
        public ViewResult Index()
        {
            throw new NotImplementedException();
        }

        public ActionResult Details(string name)
        {
            return Content(string.Format("portfolio for {0}", name));
        }
    }
}