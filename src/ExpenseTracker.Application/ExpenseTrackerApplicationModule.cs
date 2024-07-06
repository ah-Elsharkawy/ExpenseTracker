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
using ExpenseTracker.Models;
using ExpenseTracker.Dto;

namespace ExpenseTracker
{
    [DependsOn(
        typeof(ExpenseTrackerCoreModule), 
        typeof(AbpAutoMapperModule))]
    [DependsOn(typeof(AbpAutoMapperModule))]

    public class ExpenseTrackerApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ExpenseTrackerAuthorizationProvider>();
            Configuration.BackgroundJobs.UseHangfire();
            Configuration.Modules.AbpAutoMapper().Configurators.Add(
             config =>
             {
                 config.CreateMap<UserCategory, UserCategoryDTO>()
                       .ForMember(dto => dto.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                       .ForMember(dto => dto.Amount, opt => opt.MapFrom(src => src.LimitAmount))
                       .ForMember(dto => dto.limitType, opt => opt.MapFrom(src => src.LimitType));

                 config.CreateMap<UserCategory, BudgetDTO>()
                      .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.Category.Name))
                          .ForMember(dto => dto.AmountSpent, opt => opt.MapFrom(src => src.AmountSpent))
                          .ForMember(dto => dto.LimitAmount, opt => opt.MapFrom(src => src.LimitAmount))
                          .ForMember(dto => dto.LimitType, opt => opt.MapFrom(src => src.LimitType))
                          .ForMember(dto => dto.id, opt => opt.MapFrom(src => src.Id)
                 );
             }


     );
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
