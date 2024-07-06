using ExpenseTracker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Dto
{

    public class BudgetDTO
    {
        public string Name { get; set; }
        public double AmountSpent { get; set; }
        public double LimitAmount { get; set; }
        public LimitType LimitType { get; set; }
        public int id { set; get; }
    }
}
