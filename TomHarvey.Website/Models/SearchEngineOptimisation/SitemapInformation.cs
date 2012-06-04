namespace TomHarvey.Website.Models.SearchEngineOptimisation
{
    using System.Collections.Generic;

    using WeBuildStuff.CMS.Business.Pages;
    using WeBuildStuff.CMS.Business.Portfolio;
    using WeBuildStuff.CMS.Business.Services;

    public class SitemapInformation : AbstractSearchEngineModel
    {
        public IEnumerable<PageDetail> Pages { get; set; }
        
        public IEnumerable<PortfolioItem> PortfolioItems { get; set; }
        
        public IEnumerable<ServiceDetail> Services { get; set; }

        public SitemapInformation(string websiteBaseUriStem, IEnumerable<PageDetail> pages, IEnumerable<PortfolioItem> portfolioItems, IEnumerable<ServiceDetail> services)
        : base(websiteBaseUriStem)
        {
            Pages = pages;
            PortfolioItems = portfolioItems;
            Services = services;
        }
    }
}