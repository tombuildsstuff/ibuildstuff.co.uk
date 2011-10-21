using System.Collections.Generic;
using WeBuildStuff.PageManagement.Business;
using WeBuildStuff.Services.Business;

namespace TomHarvey.Website.Models.Services
{
    public class ServicesOverview
    {
        public PageRevision Revision { get; set; }

        public List<ServiceDetail> Services { get; set; }

        public ServicesOverview(PageRevision revision, List<ServiceDetail> services)
        {
            Revision = revision;
            Services = services;
        }
    }
}