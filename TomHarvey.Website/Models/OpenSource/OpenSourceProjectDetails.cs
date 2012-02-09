using System.Collections.Generic;

namespace TomHarvey.Website.Models.OpenSource
{
    using WeBuildStuff.CMS.Business.OpenSource;

    public class OpenSourceProjectDetails
    {
        public OpenSourceProjectDetail Project { get; set; }

        public IEnumerable<OpenSourceProjectLink> Links { get; set; }

        public IEnumerable<OpenSourceProjectDetail> OtherProjects { get; set; }

        public OpenSourceProjectDetails(OpenSourceProjectDetail project, IEnumerable<OpenSourceProjectLink> links, IEnumerable<OpenSourceProjectDetail> otherProjects)
        {
            Project = project;
            Links = links;
            OtherProjects = otherProjects;
        }
    }
}