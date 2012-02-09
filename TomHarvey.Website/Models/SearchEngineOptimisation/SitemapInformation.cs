using System.Collections.Generic;

namespace TomHarvey.Website.Models.SearchEngineOptimisation
{
    using WeBuildStuff.CMS.Business.Pages;
    using WeBuildStuff.CMS.Business.Portfolio;
    using WeBuildStuff.CMS.Business.Services;

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