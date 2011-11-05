using System.Web.Mvc;
using NUnit.Framework;
using Rhino.Mocks;
using TomHarvey.Core.Communication.Emailing;
using TomHarvey.Website.Controllers;
using TomHarvey.Website.Domain.GetInTouch;

namespace TomHarvey.Website.Tests.ControllerTests
{
    [TestFixture]
    public class GetInTouchControllerTests
    {
        [Test]
        public void Index_DoesReturn_View()
        {
            var controller = new GetInTouchController(null, null);
            var action = controller.Index();
            Assert.AreEqual(action.ViewName, "index");
        }

        [Test]
        public void Index_DoesReturn_EmptyContactFormAsModel()
        {
            var controller = new GetInTouchController(null, null);
            var action = controller.Index();
            Assert.IsInstanceOf<ContactForm>(action.Model);
        }

        [Test]
        public void ContactForm_WithModelErrors_ReturnsView()
        {
            var controller = new GetInTouchController(null, null);
            var form = new ContactForm();
            var action = controller.Send(form);
            Assert.NotNull(action as ViewResult);
            Assert.AreEqual("index", ((ViewResult)action).ViewName);
            Assert.AreEqual(form, ((ViewResult)action).Model);
        }

        [Test]
        [Ignore("Not Yet Implemented")]
        public void ContactForm_CompletedSuccessfully_SendsMessage()
        {

        }

        [Test]
        public void ContactForm_CompletedSuccessfully_RedirectsToThanks()
        {
            var controller = new GetInTouchController(MockRepository.GenerateStub<ImmediateEmailMailerService>(), null);
            var action = controller.Send(new ContactForm { Name = "some name", ContactDetails = "some details", Message = "some message" });
            var result = ((RedirectToRouteResult) action);

            Assert.NotNull(result);
            Assert.AreEqual("getintouch", result.RouteValues["controller"]);
            Assert.AreEqual("thanks", result.RouteValues["action"]);
        }

        [Test]
        public void Thanks_DoesReturnView()
        {
            var controller = new GetInTouchController(null, null);
            var action = controller.Thanks();
            Assert.AreEqual("thanks", action.ViewName);
        }
    }
}