using System.Collections.Generic;
using TomHarvey.Admin.Business.OpenSource;
using WeBuildStuff.PageManagement.Business;

namespace TomHarvey.Website.Models.OpenSource
{
    public class OpenSourceProjectsOverview
    {
        public PageRevision Revision { get; set; }
        public IEnumerable<OpenSourceProjectDetail> Projects { get; set; }

        public OpenSourceProjectsOverview(PageRevision revision, IEnumerable<OpenSourceProjectDetail> projects)
        {
            Revision = revision;
            Projects = projects;
        }
    }
}