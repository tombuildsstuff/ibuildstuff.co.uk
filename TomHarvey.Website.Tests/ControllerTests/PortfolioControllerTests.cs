using System.Collections.Generic;
using System.Web.Mvc;
using NUnit.Framework;
using Rhino.Mocks;
using TomHarvey.Admin.Business.Interfaces;
using TomHarvey.Admin.Business.Portfolio;
using TomHarvey.Website.Controllers;
using TomHarvey.Website.Models.Portfolio;

namespace TomHarvey.Website.Tests.ControllerTests
{
    [TestFixture]
    public class PortfolioControllerTests
    {
        [Test]
        public void Index_DoesReturnView()
        {
            var portfolioItemsRepository = MockRepository.GenerateStub<IPortfolioItemsRepository>();
            Expect.Call(portfolioItemsRepository.GetAllItems()).Return(new List<PortfolioItem>());

            var controller = new PortfolioController(portfolioItemsRepository, null);
            var action = controller.Index();

            Assert.IsInstanceOf<ViewResult>(action);
            Assert.AreEqual("portfolio", action.ViewName);
            Assert.IsInstanceOf<IEnumerable<PortfolioDetails>>(action.Model);
        }
    }
}