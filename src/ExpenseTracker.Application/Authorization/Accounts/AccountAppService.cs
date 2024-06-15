using System;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Zero.Configuration;
using Castle.Core.Smtp;
using ExpenseTracker.Authorization.Accounts.Dto;
using ExpenseTracker.Authorization.Users;
using ExpenseTracker.Email;
namespace ExpenseTracker.Authorization.Accounts
{
    public class AccountAppService : ExpenseTrackerAppServiceBase, IAccountAppService
    {
        // from: http://regexlib.com/REDetails.aspx?regexp_id=1923
        public const string PasswordRegex = "(?=^.{8,}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s)[0-9a-zA-Z!@#$%^&*()]*$";

        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly UserManager _userManager;
        private readonly ExpenseTracker.Email.IEmailSender _emailSender;

        public AccountAppService( UserRegistrationManager userRegistrationManager,UserManager userManager, ExpenseTracker.Email.IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _userRegistrationManager = userRegistrationManager;
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
                false // Assumed email address is always confirmed. Change this if you want to implement email confirmation.
            );
            // Save user to database
            //await _userManager.CreateAsync(user);

            // Generate email confirmation token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // Manually construct the confirmation link
            var confirmationLink = $"http://localhost:4200/verifyEmail?email={user.EmailAddress}&token={Uri.EscapeDataString(token)}";

            // Send email
            var emailBody = $"Please confirm your email by clicking this link: <a href=\"{confirmationLink}\">Confirm Email</a>";
            await _emailSender.SendEmailAsync(user.EmailAddress, "Confirm your email", emailBody);

            // Check if email confirmation is required for login
            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

            return new RegisterOutput
            {
                CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
            };
        }


    }
}
