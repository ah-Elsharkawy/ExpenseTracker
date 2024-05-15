using Abp.Application.Services;
using ExpenseTracker.MultiTenancy.Dto;

namespace ExpenseTracker.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

