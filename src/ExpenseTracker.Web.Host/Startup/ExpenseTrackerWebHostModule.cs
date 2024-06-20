using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ExpenseTracker.Configuration;
using Abp.Hangfire.Configuration;
using Abp.Hangfire;

namespace ExpenseTracker.Web.Host.Startup
{
    [DependsOn(
       typeof(ExpenseTrackerWebCoreModule))]
    
        [DependsOn(
            typeof(AbpHangfireAspNetCoreModule))
        ]
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
            Configuration.BackgroundJobs.UseHangfire();

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ExpenseTrackerWebHostModule).GetAssembly());
        }
    }
}
