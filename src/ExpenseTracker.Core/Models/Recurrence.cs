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
    public class Recurrence : Entity<int>
    {
        [Required,MinLength(3),MaxLength(20)]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public TransactionType Type { get; set; }
        [Required]
        public int Duration { get; set; }
        //foreign keys
        public int CategoryId { get; set; }
        [ForeignKey("User")]
        public long UserId { get; set; }
        //nav properties

        public User User { get; set; }
        public Category Category { get; set; }

    }
}
