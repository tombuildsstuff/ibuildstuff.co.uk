using System.Linq;
using NUnit.Framework;
using TomHarvey.Website.Domain.GetInTouch;

namespace TomHarvey.Website.Tests.ValidationTests
{
    [TestFixture]
    public class ContactFormValidationTests
    {
        private readonly ContactForm.ContactFormValidator _validator;
        public ContactFormValidationTests()
        {
            _validator = new ContactForm.ContactFormValidator();
        }    

        [Test]
        public void Name_CannotBeEmpty()
        {
            var form = new ContactForm { ContactDetails = "some contact details", Message = "some message goes here"};
            
            var result = _validator.Validate(form);

            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Errors.Where(e => e != null).Count());
        }

        [Test]
        public void ContactDetails_CannotBeEmpty()
        {
            var form = new ContactForm { Name = "my name here", Message = "some message goes here" };

            var result = _validator.Validate(form);

            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Errors.Where(e => e != null).Count());
        }

        [Test]
        public void Message_CannotBeEmpty()
        {
            var form = new ContactForm {Name = "my name", ContactDetails = "01202 290101"};
            var result = _validator.Validate(form);

            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Errors.Where(e => e != null).Count());
        }
    }
}