using System.Web.Mvc;

namespace TomHarvey.Website
{
    public class XmlViewResult : ViewResult
    {
        public ActionResult XmlView(string view, object model)
        {
            if (model != null)
                ViewData.Model = model;

            return new ViewResult { ViewName = view };
        }

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);
            context.HttpContext.Response.ContentType = "text/xml";
        }
    }
}