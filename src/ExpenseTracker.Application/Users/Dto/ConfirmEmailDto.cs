using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Users.Dto
{
    public class ConfirmEmailDto
    {
        public string Email { get; set; }
        public string token { get; set; }
    }
}