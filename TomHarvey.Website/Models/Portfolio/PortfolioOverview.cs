using System.Collections.Generic;

namespace TomHarvey.Website.Models.Portfolio
{
    using WeBuildStuff.CMS.Business.Pages;
    using WeBuildStuff.CMS.Business.Portfolio;

    public class PortfolioOverview
    {
        public PageRevision Revision { get; set; }
        public IEnumerable<PortfolioItem> Items { get; set; }

        public PortfolioOverview(PageRevision revision, IEnumerable<PortfolioItem> items)
        {
            Revision = revision;
            Items = items;
        }
    }
}