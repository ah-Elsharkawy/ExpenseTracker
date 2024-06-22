using System;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Net.Mail;
using Abp.Zero.Configuration;
using ExpenseTracker.Authorization.Accounts.Dto;
using ExpenseTracker.Authorization.Users;

namespace ExpenseTracker.Authorization.Accounts
{
    public class AccountAppService : ExpenseTrackerAppServiceBase, IAccountAppService
    {
        // from: http://regexlib.com/REDetails.aspx?regexp_id=1923
        public const string PasswordRegex = "(?=^.{8,}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s)[0-9a-zA-Z!@#$%^&*()]*$";

        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly IEmailSender _emailSender;
        private readonly UserManager _userManager;

        public AccountAppService(
            UserRegistrationManager userRegistrationManager, IEmailSender emailSender, UserManager userManager)
        {
            _userRegistrationManager = userRegistrationManager;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        public async Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input)
        {
            var tenant = await TenantManager.FindByTenancyNameAsync(input.TenancyName);
            if (tenant == null)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.NotFound);
            }

            if (!tenant.IsActive)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.InActive);
            }

            return new IsTenantAvailableOutput(TenantAvailabilityState.Available, tenant.Id);
        }

        public async Task<RegisterOutput> Register(RegisterInput input)
        {
            var username = input.Email.Split('@')[0] + input.Email.Split('@')[1].Split(".")[0] + input.Email.Split('@')[1].Split(".")[1];
            var user = await _userRegistrationManager.RegisterAsync(
                input.Name,
                "",
                input.Email,
                username,
                input.Password,
                false 
            );

            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var tokenBytes = System.Text.Encoding.UTF8.GetBytes(token);
            var base64Token = Convert.ToBase64String(tokenBytes);
            user.EmailConfirmationCode = token;
            // Construct the callback URL
            var callbackUrl = $"http://localhost:4200/verifyEmail?Email={user.EmailAddress}&token={base64Token}";
            try
            {
                await _emailSender.SendAsync(user.EmailAddress, "Click at the following link to Confirm your email", callbackUrl);

            }catch(Exception e)
            {

            }
            return new RegisterOutput
            {
                CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
            };
        }
    }
}
