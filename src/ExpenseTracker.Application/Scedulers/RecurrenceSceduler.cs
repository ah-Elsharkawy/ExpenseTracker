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

namespace ExpenseTracker.Scedulers
{
   
    public class RecurrenceSceduler: ITransientDependency
    {

        private readonly IRepository<Recurrence> recurrence;
        private readonly IRepository<Transaction> transaction;
        private readonly IIocResolver _iocResolver;
        public RecurrenceSceduler(IRepository<Transaction> transaction, IRepository<Recurrence> recurrence, IIocResolver iocResolver)
        {
            this.transaction = transaction;
            this.recurrence = recurrence;
            _iocResolver = iocResolver;
        }


        [UnitOfWork]
        public void ScheduleRecurringTransactions(Recurrence recurrnce)
        {
            var lastTransaction = transaction.GetAllList().FindLast(t => t.UserId == recurrnce.UserId);
            if(lastTransaction == null)
            {
                lastTransaction = new Transaction
                {
                    Id = 0
                };
            }
            var transactionModel = ConvertRecurrenceToTransaction(recurrnce , lastTransaction);
            try
            {
                RecurringJob.AddOrUpdate(
              recurrnce.Id.ToString(),
              () => ExecuteTransactionJob(recurrnce),
              "0 0 1 * *" // Cron expression for scheduling
          );

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public void RemoveRecurringTransaction(int r)
        {
             RecurringJob.RemoveIfExists(r.ToString());
        }
        public void ExecuteTransactionJob(Recurrence recurrence)
        {
            using (var transactionRepository = _iocResolver.ResolveAsDisposable<IRepository<Transaction>>())
            {



                var lastTransaction = transactionRepository.Object.GetAllList().FindLast(t => t.UserId == recurrence.UserId);
                if (lastTransaction == null)
                {
                    lastTransaction = new Transaction { Id = 0 };
                }

                var transaction = ConvertRecurrenceToTransaction(recurrence, lastTransaction);
                transactionRepository.Object.Insert(transaction);

                //                scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>().Current.SaveChanges();
            }
        }
        private Transaction ConvertRecurrenceToTransaction(Recurrence recurrnce , Transaction last)
        {
            return new Transaction
            {
                Amount = recurrnce.Amount,
                CategoryId = recurrnce.CategoryId,
                Date = recurrnce.Date,
                Description = recurrnce.Description,
                Type = recurrnce.Type,
                UserId = (int)recurrnce.UserId,
                Id = last.Id + 1,
                
            };
        }

        
    }
}
