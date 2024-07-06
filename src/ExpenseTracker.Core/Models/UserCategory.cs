using Abp.Domain.Entities;
using ExpenseTracker.Authorization.Users;
using ExpenseTracker.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Models
{
    public class UserCategory: Entity<int>
    {
        public LimitType LimitType { get; set; }
        public double LimitAmount { get; set; }
        //foreign keys
        [ForeignKey("User")]

        public long UserId { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        //nav properties
        public User User { get; set; }
        public Category Category { get; set; }
        public double AmountSpent { get; set; } 

    }
}
