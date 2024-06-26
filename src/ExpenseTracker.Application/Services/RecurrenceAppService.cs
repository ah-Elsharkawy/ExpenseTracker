﻿using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using ExpenseTracker.Dto;
using ExpenseTracker.IServices;
using ExpenseTracker.Models;
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
        }

        public RecurrnceDTO CreateRecurrence(RecurrnceDTO input)
        {
            try
            {
                var recurrence = this.Repository.Insert(new Recurrence
                {
                    Name = input.Name,
                    Date = input.Date,
                    Amount = input.Amount,
                    Type = input.Type,
                    Duration = input.Duration,
                    CategoryId = input.CategoryId,
                    UserId = input.UserId,

                });
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
                            Name = input.Name,
                            Date = input.Date,
                            Amount = input.Amount,
                            Type = input.Type,
                            Duration = input.Duration,
                            CategoryId = input.CategoryId,
                            UserId = input.UserId,
                            
                        });
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
