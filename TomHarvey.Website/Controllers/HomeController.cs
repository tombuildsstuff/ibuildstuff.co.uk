﻿using System.Web.Mvc;

namespace TomHarvey.Website.Controllers
{
    public class HomeController : BaseController
    {
        public ViewResult Index()
        {
            return View("Template");
        }

        public ViewResult Test()
        {
            return View("test");
        }
    }
}