using Abp.Domain.Entities;
using ExpenseTracker.Authorization.Users;
using ExpenseTracker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Models
{
    public class UserCategory
    {
        public LimitType LimitType { get; set; }
        public Double LimitAmount { get; set; }
        //foreign keys
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        //nav properties
        public User User { get; set; }
        public Category Category { get; set; }

    }
}
