using System.Threading.Tasks;
using Abp.Application.Services;
using ExpenseTracker.Authorization.Accounts.Dto;

namespace ExpenseTracker.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
