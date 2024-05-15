using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Controllers
{
    public abstract class ExpenseTrackerControllerBase: AbpController
    {
        protected ExpenseTrackerControllerBase()
        {
            LocalizationSourceName = ExpenseTrackerConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
