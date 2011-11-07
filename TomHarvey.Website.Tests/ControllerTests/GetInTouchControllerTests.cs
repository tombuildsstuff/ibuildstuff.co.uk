using System.Web.Mvc;
using NUnit.Framework;
using Rhino.Mocks;
using TomHarvey.Core.Communication.Emailing;
using TomHarvey.Website.Controllers;
using TomHarvey.Website.Domain.EmailGeneration;
using TomHarvey.Website.Domain.GetInTouch;
using TomHarvey.Website.Models.GetInTouch;
using WeBuildStuff.PageManagement.Business;
using WeBuildStuff.PageManagement.Business.Interfaces;
using WeBuildStuff.PageManagement.Fakes;
using WeBuildStuff.Shared.Settings;

namespace TomHarvey.Website.Tests.ControllerTests
{
    [TestFixture]
    public class GetInTouchControllerTests
    {
        private readonly IPageDetailsRepository _pageDetailsRepository;
        private readonly IPageRevisionsRepository _pageRevisionsRepository;

        public GetInTouchControllerTests()
        {
            _pageDetailsRepository = new FakePageDetailsRepository(new[] { "GetInTouch", "GetInTouchThanks" });
            _pageRevisionsRepository = new FakePageRevisionsRepository(_pageDetailsRepository.GetAllPageDetails(), new[] { 1 });
        }

        [Test]
        public void Index_DoesReturn_View()
        {
            var controller = new GetInTouchController(null, null, _pageDetailsRepository, _pageRevisionsRepository);
            var action = controller.Index();
            Assert.AreEqual("index", action.ViewName);
        }

        [Test]
        public void Index_DoesReturn_AUsefulModel()
        {
            var controller = new GetInTouchController(null, null, _pageDetailsRepository, _pageRevisionsRepository);
            var action = controller.Index();
            
            Assert.IsNotNull(action.Model);

            var model = (GetInTouchOverview)action.Model;
            Assert.IsInstanceOf<GetInTouchOverview>(model);

            Assert.IsNotNull(model.Form);
            Assert.IsNull(model.ValidationResult); // validation result should be empty on initial load..
            Assert.IsNotNull(model.Revision);
        }

        [Test]
        public void Send_ShouldReturn_ViewWithAnInvalidModel()
        {
            var controller = new GetInTouchController(null, null, _pageDetailsRepository, _pageRevisionsRepository);
            var form = new ContactForm();
            var action = controller.Send(form);

            Assert.IsInstanceOf<ViewResult>(action);
            var result = (ViewResult)action;
            Assert.AreEqual("index", result.ViewName);

            Assert.IsInstanceOf<GetInTouchOverview>(result.Model);
            var model = (GetInTouchOverview)result.Model;
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Form);
            Assert.AreSame(form, model.Form);
            Assert.IsNotNull(model.ValidationResult);
            Assert.IsNotNull(model.Revision);
        }

        [Test]
        public void Send_ShouldSendEmail_WhenHasValidForm()
        {
            var fakeSettingsRepository = MockRepository.GenerateMock<ISettingsRepository>();
            var fakeSettings = new EmailSettings("localhost", 2020, false, "bob@me.com", "jobs21");
            fakeSettingsRepository.Expect(x => x.CurrentEmailSettings()).Return(fakeSettings);
         
            var form = new ContactForm { Name = "Tom", ContactDetails = "010101", Message = "some message" };
            var email = form.GenerateEmailMessage(fakeSettingsRepository);
   
            var fakeEmailService = MockRepository.GenerateMock<IEmailMailerService>();
            fakeEmailService.Expect(x => x.SendEmail(email, fakeSettings));

            var controller = new GetInTouchController(fakeEmailService, fakeSettingsRepository, null, null);
            controller.Send(form);

            fakeSettingsRepository.VerifyAllExpectations();
            fakeEmailService.VerifyAllExpectations();
        }

        [Test]
        public void Send_ShouldRedirectToThanks_WhenHasValidForm()
        {
            var fakeSettingsRepository = MockRepository.GenerateMock<ISettingsRepository>();
            var fakeSettings = new EmailSettings("localhost", 2020, false, "bob@me.com", "jobs21");
            fakeSettingsRepository.Expect(x => x.CurrentEmailSettings()).Return(fakeSettings);

            var form = new ContactForm { Name = "Tom", ContactDetails = "010101", Message = "some message" };
            var fakeEmailService = MockRepository.GenerateMock<IEmailMailerService>();

            var controller = new GetInTouchController(fakeEmailService, fakeSettingsRepository, null, null);
            var action = controller.Send(form);

            Assert.IsInstanceOf<RedirectToRouteResult>(action);
            var result = (RedirectToRouteResult)action;
            Assert.IsNotNull(result);
            Assert.AreEqual("thanks", result.RouteValues["action"]);
        }

        [Test]
        public void Thanks_DoesReturn_View()
        {
            var controller = new GetInTouchController(null, null, _pageDetailsRepository, _pageRevisionsRepository);
            var action = controller.Thanks();
            Assert.AreEqual("thanks", action.ViewName);
        }

        [Test]
        public void Thanks_DoesReturn_AUsefulModel()
        {
            var controller = new GetInTouchController(null, null, _pageDetailsRepository, _pageRevisionsRepository);
            var action = controller.Thanks();

            Assert.IsNotNull(action.Model);
            Assert.IsInstanceOf<PageRevision>(action.Model);
        }
    }
}