namespace TomHarvey.Website.Domain.EmailGeneration
{
    using System;
    using System.Text;
    
    using TomHarvey.Website.Domain.GetInTouch;

    using WeBuildStuff.CMS.Business.Messaging.Email;
    using WeBuildStuff.CMS.Business.Settings.Interfaces;

    public static class ContactFormEmail
    {
        public static EmailMessage GenerateEmailMessage(this ContactForm form, string toAlias, string toAddress, string fromAlias, string fromAddress)
        {
            var message = new StringBuilder();
            message.Append("Hi Tom<br /><br />There's a new message from the website enquiry form. Details below:<br /><br />");
            message.AppendFormat("Name: <strong>{0}</strong><br />", form.Name);
            message.AppendFormat("Contact Details: <strong>{0}</strong><br />", form.ContactDetails);
            message.AppendFormat("Message:<br />{0}<br /><br />----------- end message -----------<br /><br />", form.Message);
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