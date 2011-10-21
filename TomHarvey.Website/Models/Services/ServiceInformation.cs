using System.Collections.Generic;
using WeBuildStuff.Services.Business;

namespace TomHarvey.Website.Models.Services
{
    public class ServiceInformation
    {
        public ServiceDetail Service { get; set; }
        public List<ServicePhoto> Photos { get; set; }
        public List<ServiceDetail> Services { get; set; }

        public ServiceInformation(ServiceDetail service, List<ServicePhoto> photos, List<ServiceDetail> services)
        {
            Service = service;
            Photos = photos;
            Services = services;
        }
    }
}