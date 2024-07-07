using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ExpenseTracker.Enums;
using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Dto
{
    [AutoMapFrom(typeof(Notifications))]
    public class NewNotificationDTO : EntityDto<int>
    {
        [MinLength(5)]
        public string Message { get; set; }
        [Required]
        public NotificationType Type { get; set; }

        public int UserId { get; set; }
    }
}
