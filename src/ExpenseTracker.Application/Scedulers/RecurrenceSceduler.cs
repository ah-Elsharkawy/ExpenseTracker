using Abp.Application.Services;
using System;
using Hangfire;
using ExpenseTracker.Dto;
using ExpenseTracker.IServices;
using System.Collections.Generic;
using ExpenseTracker.Models;
using ExpenseTracker.Enums;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Abp.Domain.Uow;
using System.Diagnostics;

namespace ExpenseTracker.Scedulers
{
   
    public class RecurrenceSceduler: ITransientDependency
    {

       private readonly IRepository<Recurrence> RecurrenceRepo;
        private readonly IRepository<Transaction> transactionRepository;
        private readonly IUnitOfWorkManager unitOfWorkManager;
        public RecurrenceSceduler(IRepository<Recurrence> Repository, IUnitOfWorkManager unitOfWorkManager, IRepository<Transaction> transactionRepository)
        {
            this.RecurrenceRepo = Repository;
            this.unitOfWorkManager = unitOfWorkManager;
            this.transactionRepository = transactionRepository;
        }

        public void ExecuteTransactionJob(Recurrence recurrence)
        {
            using (var uow = unitOfWorkManager.Begin())
            {
                var transaction = new Transaction
                {
                    Amount = recurrence.Amount,
                    Date = DateTime.Now,
                    Description = recurrence.Description,
                    Type = recurrence.Type,
                    CategoryId = recurrence.CategoryId,
                    UserId = (int)recurrence.UserId
                };
                recurrence.Duration = recurrence.Duration - 1;
                RecurrenceRepo.Update(recurrence);
                transactionRepository.Insert(transaction);
               if(recurrence.Duration == 0)
                {
                    RecurringJob.RemoveIfExists(recurrence.Id.ToString());
                    RecurrenceRepo.Delete(recurrence);
                }
                uow.Complete();
            }
        }

    }
}
