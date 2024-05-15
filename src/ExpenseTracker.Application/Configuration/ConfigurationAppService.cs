using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using ExpenseTracker.Configuration.Dto;

namespace ExpenseTracker.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ExpenseTrackerAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
