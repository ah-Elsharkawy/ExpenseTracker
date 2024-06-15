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

    [AutoMapFrom(typeof(Category))]
    public class CategoryDto : EntityDto<int>
    {
        [Required, MinLength(3), MaxLength(20, ErrorMessage = "Category name must be from 3 to 20 chars long")]
        public string Name { get; set; }

        public string Icon { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Transaction type must be between 0 and 1")]
        public TransactionType Type { get; set; }
    }
}
