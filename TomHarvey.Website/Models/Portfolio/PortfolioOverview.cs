using System.Collections.Generic;
using TomHarvey.Admin.Business.Portfolio;
using WeBuildStuff.PageManagement.Business;

namespace TomHarvey.Website.Models.Portfolio
{
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