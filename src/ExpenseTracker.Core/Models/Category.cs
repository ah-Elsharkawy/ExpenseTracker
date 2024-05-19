using Abp.Domain.Entities;
using ExpenseTracker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Models
{
    public class Category : Entity<int>
    {
        public string Name { get; set; }
        public TransactionType Type { get; set; }
    }
}
