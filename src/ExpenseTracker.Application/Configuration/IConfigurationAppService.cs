using System.Threading.Tasks;
using ExpenseTracker.Configuration.Dto;

namespace ExpenseTracker.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
