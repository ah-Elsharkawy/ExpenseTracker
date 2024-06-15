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
    [AutoMapFrom(typeof(Recurrence))]
    public class RecurrnceDTO : EntityDto<int>
    {
        [Required(AllowEmptyStrings =false, ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public int Amount { get; set; }

        //[Required(ErrorMessage = "User is required")]
        //public int UserId { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Duration must be greater than 0")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public  TransactionType Type{ get; set; }

    }
}
