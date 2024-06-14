using Abp.Domain.Entities;
using ExpenseTracker.Authorization.Users;
using ExpenseTracker.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Models
{
    public class Notifications : Entity<int>
    {
        [MinLength(5)]
        public string Message { get; set; }
        [Required]
        public NotificationType Type { get; set; }
        public bool IsRead { get; set; } = false;
        //foreign keys
        [ForeignKey("User")]
        public long UserId { get; set; }
        //nav properties
        public User User { get; set; }
    }
}
