namespace TomHarvey.Website.Domain.GetInTouch
{
    using FluentValidation;

    public class ContactForm
    {
        public string Name { get; set; }

        public string ContactDetails { get; set; }

        public string Message { get; set; }

        public class ContactFormValidator : AbstractValidator<ContactForm>
        {
            public ContactFormValidator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter your name");
                RuleFor(x => x.ContactDetails).NotEmpty().WithMessage("Please enter your contact details");
                RuleFor(x => x.Message).NotEmpty().WithMessage("Please enter a brief message");
            }
        }
    }
}