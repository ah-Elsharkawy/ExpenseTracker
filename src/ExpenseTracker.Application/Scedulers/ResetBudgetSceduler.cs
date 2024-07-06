using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Scedulers
{
    public class ResetBudgetSceduler
    {
        private readonly IRepository<UserCategory> repository;
        private readonly IUnitOfWorkManager unitOfWorkManager;


        public ResetBudgetSceduler(IRepository<UserCategory> repository, IUnitOfWorkManager unitOfWorkManager)
        {
            this.repository = repository;
            this.unitOfWorkManager = unitOfWorkManager;
        }
        public void ResetAllMonthlyBudgets()
        {
            using (var uow = unitOfWorkManager.Begin())
            {
                var userCategories = repository.GetAllList(lt => lt.LimitType == Enums.LimitType.monthly);
                foreach (var userCategory in userCategories)
                {
                    userCategory.AmountSpent = 0;
                    repository.Update(userCategory);
                }
                uow.Complete();
            }
          
        }
        public void ResetAllWeeklyBudgets()
        {
            using (var uow = unitOfWorkManager.Begin())
            {
                var userCategories = repository.GetAllList(lt => lt.LimitType == Enums.LimitType.weekly);
                foreach (var userCategory in userCategories)
                {
                    userCategory.AmountSpent = 0;
                    repository.Update(userCategory);
                }
                uow.Complete();
            }
        }
        //public void ResetAllDailyBudgets()
        //{
        //    using (var uow = unitOfWorkManager.Begin())
        //    {
        //        var userCategories = repository.GetAllList(lt => lt.LimitType == Enums.LimitType.daily);
        //        foreach (var userCategory in userCategories)
        //        {
        //            userCategory.AmountSpent = 0;
        //            repository.Update(userCategory);
        //        }
        //        uow.Complete();
        //    }
        //}
    }
}
