using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using AutoMapper;
using ExpenseTracker.Authorization.Users;
using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Dto
{
    [AutoMapFrom(typeof(Transaction))]
    public class TotalIncomesDTO : EntityDto<int>
    {
        public double TotalIncome { get; set; }
    }
}
