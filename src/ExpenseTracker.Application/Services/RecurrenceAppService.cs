using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using ExpenseTracker.Dto;
using ExpenseTracker.Enums;
using ExpenseTracker.IServices;
using ExpenseTracker.Models;
using ExpenseTracker.Scedulers;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Services
{
    public class RecurrenceAppService : ApplicationService, IRecurrenceAppService
    {
        private readonly IObjectMapper objectMapper;
        private readonly IRepository<Recurrence> Repository;


        public RecurrenceAppService(IObjectMapper objectMapper, IRepository<Recurrence> Repository)
        {
            this.objectMapper = objectMapper;
            this.Repository = Repository;
         //   this.recurrenceSceduler = recurrenceSceduler;
        }

      
        public RecurrnceDTO CreateRecurrence(RecurrnceDTO input)
        {
            var uId = AbpSession.UserId;
            if (uId == null)
                return null;
            try
            {
                var recurrence = this.Repository.Insert(new Recurrence
                {
                    Description = input.Description,
                    Date = input.Date,
                    Amount = input.Amount,
                    Type = input.Type,
                    Duration = input.Duration,
                    CategoryId = input.CategoryId,
                    UserId = (int)uId,
                    Id = input.Id

                });
                RecurringJob.AddOrUpdate<RecurrenceSceduler>(recurrence.Id.ToString(), x => x.ScheduleRecurringTransactions(recurrence), "0 0 1 * *");
                return objectMapper.Map<RecurrnceDTO>(recurrence);

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public void DeleteRecurrence(int id)
        {
            try
            {
               var recurrence =   this.Repository.FirstOrDefault(x => x.Id == id);
                if (recurrence != null)
                {
                    this.Repository.Delete(recurrence);
                }
               

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<RecurrnceDTO> GetRecurrencesForUser(int UserId)
        {
            try
            {

                var UserRecurrences = this.Repository.GetAllList(x => x.UserId == UserId);
                return objectMapper.Map<List<RecurrnceDTO>>(UserRecurrences);
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public List<RecurrnceDTO> GetRecurrenceByType(TransactionType type)
        {
            try
            {
                var Recurrences = this.Repository.GetAllList().Where(x => x.Type == type).ToList();
                return objectMapper.Map<List<RecurrnceDTO>>(Recurrences);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public RecurrnceDTO GetRecurrnceDetails(int id)
        {
            try
            {
               
                var Recurrence = this.Repository.FirstOrDefault(id);
                if (Recurrence != null)
                {
                    return objectMapper.Map<RecurrnceDTO>(Recurrence);

                }
                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void UpdateRecurrence(int id,RecurrnceDTO input)
        {
            try
            {

             
                
                    this.Repository.Update(
                        new Recurrence()
                        {
                            Id = id,
                            Description = input.Description,
                            Date = input.Date,
                            Amount = input.Amount,
                            Type = input.Type,
                            Duration = input.Duration,
                            CategoryId = input.CategoryId,
                            //UserId = input.UserId,
                            
                        });
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
