using Abp.AutoMapper;
using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Dto
{
    [AutoMapFrom(typeof(Transaction))]
    public class BalanceDTO
    {
        public double Balance { get; set; }
    }
}
