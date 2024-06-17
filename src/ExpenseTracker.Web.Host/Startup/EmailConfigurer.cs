using Abp.Configuration;
namespace ExpenseTracker.Web.Host.Startup
{
    public static class EmailSettingNames
    {
        public const string DefaultFromAddress = "Abp.Net.Mail.DefaultFromAddress";
        public const string DefaultFromDisplayName = "Abp.Net.Mail.DefaultFromDisplayName";
        public const string SmtpHost = "Abp.Net.Mail.Smtp.Host";
        public const string SmtpPort = "Abp.Net.Mail.Smtp.Port";
        public const string SmtpUserName = "Abp.Net.Mail.Smtp.UserName";
        public const string SmtpPassword = "Abp.Net.Mail.Smtp.Password";
        public const string SmtpDomain = "Abp.Net.Mail.Smtp.Domain";
        public const string SmtpEnableSsl = "Abp.Net.Mail.Smtp.EnableSsl";
        public const string SmtpUseDefaultCredentials = "Abp.Net.Mail.Smtp.UseDefaultCredentials";
    }

    public static class EmailConfigurer
    {

    }
}
