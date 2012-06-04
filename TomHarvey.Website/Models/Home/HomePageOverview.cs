namespace TomHarvey.Website.Models.Home
{
    using System.Collections.Generic;

    using MvcBlog.Entities;
    
    using WeBuildStuff.CMS.Business.OpenSource;
    using WeBuildStuff.CMS.Business.Pages;
    using WeBuildStuff.CMS.Business.Portfolio;

    public class HomePageOverview
    {
        public PageRevision Revision { get; set; }

        public string BlogBaseUrl { get; set; }

        public IEnumerable<Post> BlogPosts { get; set; } 

        public IEnumerable<PortfolioItem> PortfolioItems { get; set; }

        public IEnumerable<OpenSourceProjectDetail> OpenSourceProjects { get; set; }

        public HomePageOverview(PageRevision revision, string blogBaseUrl, IEnumerable<Post> blogPosts, IEnumerable<PortfolioItem> portfolioItems, IEnumerable<OpenSourceProjectDetail> openSourceProjects)
        {
            Revision = revision;
            BlogBaseUrl = blogBaseUrl;
            BlogPosts = blogPosts;
            PortfolioItems = portfolioItems;
            OpenSourceProjects = openSourceProjects;
        }
    }
}