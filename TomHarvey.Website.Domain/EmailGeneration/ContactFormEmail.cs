namespace TomHarvey.Website.Domain.EmailGeneration
{
    using System;
    using System.Text;
    
    using TomHarvey.Core.Communication.Emailing;
    using TomHarvey.Website.Domain.GetInTouch;
    
    using WeBuildStuff.CMS.Business.Settings.Interfaces;

    public static class ContactFormEmail
    {
        public static EmailMessage GenerateEmailMessage(this ContactForm form, string toAlias, string toAddress, string fromAlias, string fromAddress)
        {
            var message = new StringBuilder();
            message.Append("Hi Tom\n\nThere's a new message from the website enquiry form. Details below:\n\n");
            message.AppendFormat("Name: <strong>{0}</strong>", form.Name);
            message.AppendFormat("Contact Details: <strong>{0}</strong>", form.ContactDetails);
            message.AppendFormat("Message:\n\n{0}\n\n----------- end message -----------\n\n", form.Message);
            message.AppendFormat("Send on {0} at {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());

            return new EmailMessage(string.IsNullOrWhiteSpace(fromAlias) ? fromAddress : fromAlias,
                                    fromAddress,
                                    string.IsNullOrWhiteSpace(toAlias) ? toAddress : toAlias,
                                    toAddress,
                                    "New Website Contact Form Enquiry",
                                    message.ToString(),
                                    false);
        }

        public static EmailMessage GenerateEmailMessage(this ContactForm form, ISettingsRepository settings)
        {
            return form.GenerateEmailMessage(settings.EmailToAlias(), settings.EmailToAddress(), settings.EmailFromAlias(), settings.EmailFromAddress());
        }
    }
}