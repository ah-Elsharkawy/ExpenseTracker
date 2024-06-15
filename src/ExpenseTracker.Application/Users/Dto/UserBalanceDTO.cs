using Abp.AutoMapper;
using ExpenseTracker.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserBalanceDTO
    {
        public double Balance { get; set; }
    }
}
