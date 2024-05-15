using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ExpenseTracker.Authorization;

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
