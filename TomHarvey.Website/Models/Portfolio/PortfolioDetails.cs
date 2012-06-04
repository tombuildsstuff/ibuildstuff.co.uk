using System.Collections.Generic;

namespace TomHarvey.Website.Models.Portfolio
{
    using WeBuildStuff.CMS.Business.Portfolio;

    public class PortfolioDetails
    {
        public PortfolioItemDetails PortfolioItemDetails { get; set; }
        public List<PortfolioItem> PortfolioItems { get; set; }

        public PortfolioDetails(PortfolioItemDetails portfolioItemDetails, List<PortfolioItem> portfolioItems)
        {
            PortfolioItemDetails = portfolioItemDetails;
            PortfolioItems = portfolioItems;
        }
    }
}