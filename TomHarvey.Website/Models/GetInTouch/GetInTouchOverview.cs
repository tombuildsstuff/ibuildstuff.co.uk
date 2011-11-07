using FluentValidation.Results;
using TomHarvey.Website.Domain.GetInTouch;
using WeBuildStuff.PageManagement.Business;

namespace TomHarvey.Website.Models.GetInTouch
{
    public class GetInTouchOverview
    {
        public PageRevision Revision { get; set; }
        
        public ContactForm Form { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public GetInTouchOverview(PageRevision revision, ContactForm form, ValidationResult result)
        {
            Revision = revision;

            Form = form;

            ValidationResult = result;
        }
    }
}