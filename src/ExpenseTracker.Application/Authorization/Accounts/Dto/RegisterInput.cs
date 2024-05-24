using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Extensions;
using ExpenseTracker.Validation;

namespace ExpenseTracker.Authorization.Accounts.Dto
{
    public class RegisterInput
    {
        private const string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        //[Required]
        //[StringLength(AbpUserBase.MaxSurnameLength)]
        //public string Surname { get; set; }

        //[Required]
        //[StringLength(AbpUserBase.MaxUserNameLength)]
        //public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string Email { get; set; }

        [Required]
        [RegularExpression(PasswordPattern, ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        
        public string Password { get; set; }

        //[DisableAuditing]
        //public string CaptchaResponse { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (!UserName.IsNullOrEmpty())
        //    {
        //        if (!UserName.Equals(Email) && ValidationHelper.IsEmail(UserName))
        //        {
        //            yield return new ValidationResult("Username cannot be an email address unless it's the same as your email address!");
        //        }
        //    }
        //}
    }
}
