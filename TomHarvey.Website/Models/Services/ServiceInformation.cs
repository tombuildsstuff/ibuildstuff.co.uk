namespace TomHarvey.Website.Models.Services
{
    using System.Collections.Generic;
    
    using WeBuildStuff.CMS.Business.Services;

    public class ServiceInformation
    {
        public ServiceDetail Service { get; set; }
        
        public IEnumerable<ServicePhoto> Photos { get; set; }

        public IEnumerable<ServiceDetail> Services { get; set; }

        public ServiceInformation(ServiceDetail service, IEnumerable<ServicePhoto> photos, IEnumerable<ServiceDetail> services)
        {
            Service = service;
            Photos = photos;
            Services = services;
        }
    }
}