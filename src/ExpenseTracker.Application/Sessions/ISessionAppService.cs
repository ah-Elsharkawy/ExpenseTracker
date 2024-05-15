using System.Threading.Tasks;
using Abp.Application.Services;
using ExpenseTracker.Sessions.Dto;

namespace ExpenseTracker.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
