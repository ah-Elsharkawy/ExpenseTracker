using ExpenseTracker.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using ExpenseTracker.Validations;
using ExpenseTracker.Models;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;

namespace ExpenseTracker.Dto
{
    [AutoMapFrom(typeof(Transaction))]
    public class TransactionDTO 
    {

        [Range(0, int.MaxValue, ErrorMessage = "Amount must be a positive number")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "CategoryId is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Type is required")]
        [ValidateTransactionType(ErrorMessage = "Invalid transaction type")]
        public TransactionType Type { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MinLength(3, ErrorMessage = "Description must be at least 3 characters long")]
        [MaxLength(50, ErrorMessage = "Description cannot exceed 50 characters")]
        public string Description { get; set; }
    }

}
