﻿using Abp.Domain.Entities;
using ExpenseTracker.Authorization.Users;
using ExpenseTracker.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Models
{
    public class Transaction : Entity<int>
    {
        [Required,MinLength(3),MaxLength(50)]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public TransactionType Type { get; set; }
        //foreign keys
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        //nav properties
        public Category Category { get; set; }
        public User User { get; set; }

    }
}
