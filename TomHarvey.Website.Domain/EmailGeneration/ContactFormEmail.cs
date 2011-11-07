using TomHarvey.Core.Communication.Emailing;
using TomHarvey.Website.Domain.GetInTouch;
using WeBuildStuff.Shared.Settings;

namespace TomHarvey.Website.Domain.EmailGeneration
{
    public static class ContactFormEmail
    {
        public static EmailMessage GenerateEmailMessage(this ContactForm form, string toAlias, string toAddress, string fromAlias, string fromAddress)
        {
            // TODO: implement me plz
            return null;
        }

        public static EmailMessage GenerateEmailMessage(this ContactForm form, ISettingsRepository settings)
        {
            return form.GenerateEmailMessage(settings.EmailToAlias(), settings.EmailToAddress(), settings.EmailFromAlias(), settings.EmailFromAddress());
        }
    }
}