using System.Collections.Generic;
using TomHarvey.Admin.Business.Portfolio;
using WeBuildStuff.PageManagement.Business;
using WeBuildStuff.Services.Business;

namespace TomHarvey.Website.Models.SearchEngineOptimisation
{
    public class SitemapInformation : AbstractSearchEngineModel
    {
        public List<PageDetail> Pages { get; set; }
        public IEnumerable<PortfolioItem> PortfolioItems { get; set; }
        public List<ServiceDetail> Services { get; set; }

        public SitemapInformation(string websiteBaseUriStem, List<PageDetail> pages, IEnumerable<PortfolioItem> portfolioItems, List<ServiceDetail> services) : base(websiteBaseUriStem)
        {
            Pages = pages;
            PortfolioItems = portfolioItems;
            Services = services;
        }
    }
}