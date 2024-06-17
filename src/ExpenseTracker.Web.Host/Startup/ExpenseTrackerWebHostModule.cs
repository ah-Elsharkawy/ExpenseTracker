using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ExpenseTracker.Configuration;
using Abp.Configuration;

namespace ExpenseTracker.Web.Host.Startup
{
    [DependsOn(
       typeof(ExpenseTrackerWebCoreModule))]


    public class ExpenseTrackerWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ExpenseTrackerWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();


        }
        public override void PreInitialize()
        {
            
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ExpenseTrackerWebHostModule).GetAssembly());
        }
        public override void PostInitialize()
        {
            var settingManager = IocManager.Resolve<ISettingManager>();
            settingManager.ChangeSettingForApplicationAsync(EmailSettingNames.DefaultFromAddress, "ExpenseTracker123@outlook.com").Wait();
            settingManager.ChangeSettingForApplicationAsync(EmailSettingNames.DefaultFromDisplayName, "Expense Tracker").Wait();
            settingManager.ChangeSettingForApplicationAsync(EmailSettingNames.SmtpHost, "smtp-mail.outlook.com").Wait();
            settingManager.ChangeSettingForApplicationAsync(EmailSettingNames.SmtpPort, "587").Wait();
            settingManager.ChangeSettingForApplicationAsync(EmailSettingNames.SmtpUserName, "ExpenseTracker123@outlook.com").Wait();
            settingManager.ChangeSettingForApplicationAsync(EmailSettingNames.SmtpPassword, "svggkqejpjbmekxh").Wait();
            settingManager.ChangeSettingForApplicationAsync(EmailSettingNames.SmtpDomain, "smtp-mail.outlook.com").Wait();
            settingManager.ChangeSettingForApplicationAsync(EmailSettingNames.SmtpEnableSsl, "true").Wait();
            settingManager.ChangeSettingForApplicationAsync("Abp.Net.Mail.Smtp.Domain", "outlook.live.com").Wait();

        }

    }
}
