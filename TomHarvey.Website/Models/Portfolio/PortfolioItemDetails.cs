using System.Collections.Generic;

namespace TomHarvey.Website.Models.Portfolio
{
    using WeBuildStuff.CMS.Business.Portfolio;

    public class PortfolioItemDetails
    {
        public PortfolioItem Item { get; set; }

        public IEnumerable<PortfolioImage> Images { get; set; }

        public PortfolioItemDetails(PortfolioItem item, IEnumerable<PortfolioImage> images)
        {
            Item = item;
            Images = images;
        }
    }
}