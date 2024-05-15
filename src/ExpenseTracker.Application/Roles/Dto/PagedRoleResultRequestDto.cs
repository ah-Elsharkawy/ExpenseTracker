using Abp.Application.Services.Dto;

namespace ExpenseTracker.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

