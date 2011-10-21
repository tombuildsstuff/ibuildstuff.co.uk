using System.Collections.Generic;
using TomHarvey.Admin.Business.Portfolio;

namespace TomHarvey.Website.Models.Portfolio
{
    public class PortfolioDetails
    {
        public PortfolioItem Item { get; set; }

        public IEnumerable<PortfolioImage> Images { get; set; }

        public PortfolioDetails(PortfolioItem item, IEnumerable<PortfolioImage> images)
        {
            Item = item;
            Images = images;
        }
    }
}