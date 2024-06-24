using Abp.AutoMapper;
using Abp.Configuration;
using Hangfire;
using Abp.Modules;
using Abp.Net.Mail;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using ExpenseTracker.Authorization;
using System;
using Abp.Hangfire.Configuration;

namespace ExpenseTracker
{
    [DependsOn(
        typeof(ExpenseTrackerCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ExpenseTrackerApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ExpenseTrackerAuthorizationProvider>();
            Configuration.BackgroundJobs.UseHangfire();
        }
        public override void PostInitialize()
        {
            var settingManager = IocManager.Resolve<ISettingManager>();
            settingManager.ChangeSettingForApplicationAsync(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin, "true");
            
            base.PostInitialize();
        }
        public override void Initialize()
        {
            var thisAssembly = typeof(ExpenseTrackerApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
