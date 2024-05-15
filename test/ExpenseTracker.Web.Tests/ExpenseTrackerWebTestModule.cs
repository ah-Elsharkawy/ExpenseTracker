using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ExpenseTracker.EntityFrameworkCore;
using ExpenseTracker.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace ExpenseTracker.Web.Tests
{
    [DependsOn(
        typeof(ExpenseTrackerWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class ExpenseTrackerWebTestModule : AbpModule
    {
        public ExpenseTrackerWebTestModule(ExpenseTrackerEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ExpenseTrackerWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(ExpenseTrackerWebMvcModule).Assembly);
        }
    }
}