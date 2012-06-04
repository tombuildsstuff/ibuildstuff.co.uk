﻿namespace TomHarvey.Website.Models.Services
{
    using System.Collections.Generic;

    using WeBuildStuff.CMS.Business.Pages;
    using WeBuildStuff.CMS.Business.Services;

    public class ServicesOverview
    {
        public PageRevision Revision { get; set; }

        public IEnumerable<ServiceDetail> Services { get; set; }

        public ServicesOverview(PageRevision revision, IEnumerable<ServiceDetail> services)
        {
            Revision = revision;
            Services = services;
        }
    }
}