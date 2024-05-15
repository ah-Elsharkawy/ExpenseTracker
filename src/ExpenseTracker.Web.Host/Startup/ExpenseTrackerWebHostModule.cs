using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ExpenseTracker.Configuration;

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

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ExpenseTrackerWebHostModule).GetAssembly());
        }
    }
}
