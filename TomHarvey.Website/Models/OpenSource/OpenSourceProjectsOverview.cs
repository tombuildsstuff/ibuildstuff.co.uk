namespace TomHarvey.Website.Models.OpenSource
{
    using System.Collections.Generic;

    using WeBuildStuff.CMS.Business.OpenSource;
    using WeBuildStuff.CMS.Business.Pages;

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