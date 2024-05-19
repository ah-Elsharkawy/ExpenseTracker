using Abp.Domain.Entities;
using ExpenseTracker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Models
{
    public class UserCategory : IEntity
    {
        public LimitType LimitType { get; set; }
        public Double LimitAmount { get; set; }
        //foreign keys
        public int UserId { get; set; }
        public int CategoryId { get; set; }

        // IEntity implementation
        public virtual object[] GetKeys()
        {
            return new object[] { UserId, CategoryId };
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is UserCategory))
                return false;

            var other = (UserCategory)obj;
            return UserId == other.UserId && CategoryId == other.CategoryId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, CategoryId);
        }

    }
}
