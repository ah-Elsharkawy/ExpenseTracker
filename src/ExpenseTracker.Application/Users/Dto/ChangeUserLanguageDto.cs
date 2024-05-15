using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}