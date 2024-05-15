using Abp.Authorization;
using ExpenseTracker.Authorization.Roles;
using ExpenseTracker.Authorization.Users;

namespace ExpenseTracker.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
