using System.Collections.Generic;
using TomHarvey.Admin.Business.Portfolio;

namespace TomHarvey.Website.Models.Portfolio
{
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