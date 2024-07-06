using ExpenseTracker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Dto
{
    
    public class UserCategoryDTO
    {
        public int CategoryId { get; set; }
        public double Amount { get; set; }
        public LimitType limitType { get; set; }

    }
}
