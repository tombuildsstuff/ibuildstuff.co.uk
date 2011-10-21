using System.Collections.Generic;
using WeBuildStuff.PageManagement.Business;

namespace TomHarvey.Website.Models.Portfolio
{
    public class PortfolioOverview
    {
        public PageRevision Revision { get; set; }
        public List<PortfolioItemDetails> Items { get; set; }

        public PortfolioOverview(PageRevision revision, List<PortfolioItemDetails> items)
        {
            Revision = revision;
            Items = items;
        }
    }
}