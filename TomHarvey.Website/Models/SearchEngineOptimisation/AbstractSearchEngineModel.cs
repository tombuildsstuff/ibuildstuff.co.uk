namespace TomHarvey.Website.Models.SearchEngineOptimisation
{
    public abstract class AbstractSearchEngineModel
    {
        public string WebsiteBaseUriStem { get; set; }

        protected AbstractSearchEngineModel(string websiteBaseUrl)
        {
            WebsiteBaseUriStem = websiteBaseUrl;
        }
    }
}