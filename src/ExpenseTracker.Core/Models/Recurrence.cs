using Abp.Domain.Entities;
using ExpenseTracker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Models
{
    public class Recurrence : Entity<int>
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public TransactionType Type { get; set; }
        public int Duration { get; set; }
        //foreign keys
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}
