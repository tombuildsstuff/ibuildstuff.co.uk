using TomHarvey.Core.Communication.Emailing;
using TomHarvey.Website.Domain.GetInTouch;

namespace TomHarvey.Website.Domain.EmailGeneration
{
    public static class ContactFormEmail
    {
        public static EmailMessage GenerateEmailMessage(this ContactForm form, string toAlias, string toAddress, string fromAlias, string fromAddress)
        {
            return null;
        }
    }
}