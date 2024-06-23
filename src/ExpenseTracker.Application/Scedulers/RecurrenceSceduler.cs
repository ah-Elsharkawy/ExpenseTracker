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
        private readonly ITransactionAppService transactionAppService;
        public RecurrenceSceduler(IRepository<Recurrence> Repository, IUnitOfWorkManager unitOfWorkManager, IRepository<Transaction> transactionRepository, ITransactionAppService transactionAppService)
        {
            this.RecurrenceRepo = Repository;
            this.unitOfWorkManager = unitOfWorkManager;
            this.transactionRepository = transactionRepository;
            this.transactionAppService = transactionAppService;
        }

        public void ExecuteTransactionJob(Recurrence recurrence)
        {
            using (var uow = unitOfWorkManager.Begin())
            {
               transactionAppService.CreateTransaction(new TransactionDTO
               {
                   Amount = recurrence.Amount,
                   CategoryId = recurrence.CategoryId,
                   Date = DateTime.Now,
                   Description = recurrence.Description,
                   Type = recurrence.Type,
                                      
                   
               });

                recurrence.Duration = recurrence.Duration - 1;
                RecurrenceRepo.Update(recurrence);
               
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
