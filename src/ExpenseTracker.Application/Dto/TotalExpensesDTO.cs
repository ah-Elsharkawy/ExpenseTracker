using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ExpenseTracker.Dto
{
    [AutoMapFrom(typeof(Transaction))]
    public class TotalExpensesDTO 
    {
        public double TotalExpense { get; set; }
    }
}
