using Abp.Domain.Entities;
using ExpenseTracker.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Models
{
    public class Category : Entity<int>
    {
        [Required,MinLength(3),MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Transaction type must be between 0 and 1")]
        public TransactionType Type { get; set; }
        //nav properties
        public List<Transaction> Transactions { get; set; }
        public List<Recurrence> Recurrences { get; set; }
        public List<UserCategory> userCategories { get; set; }
    }
}
