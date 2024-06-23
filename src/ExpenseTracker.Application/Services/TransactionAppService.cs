using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using ExpenseTracker.Authorization.Users;
using ExpenseTracker.Dto;
using ExpenseTracker.Enums;
using ExpenseTracker.IServices;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Services
{
    public class TransactionAppService : ApplicationService, ITransactionAppService
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IObjectMapper _objectMapper;
        public IAbpSession AbpSession { get; set; }


        public TransactionAppService(IRepository<Transaction> transactionRepository, IObjectMapper objectMapper)
        {
            _transactionRepository = transactionRepository;
            _objectMapper = objectMapper;
            AbpSession = NullAbpSession.Instance;
        }
        [Authorize]
        public TransactionDTO CreateTransaction(TransactionDTO input ,int ?userid)
        {
            //add new transaction
            var uId = AbpSession.UserId ?? userid;
            if(uId == null)
                return new TransactionDTO();
            var transaction = _transactionRepository.Insert(new Transaction { UserId =(int)uId, Amount = input.Amount, CategoryId = input.CategoryId, Type = input.Type, Date = input.Date, Description = input.Description });
            Console.WriteLine(uId);
            return _objectMapper.Map<TransactionDTO>(transaction);
        }
   
        public List<TransactionDTO> GetTransactions()
        {
            var uId = AbpSession.UserId;
            if(uId == null)
                return null;
            var transactions = _transactionRepository.GetAllList().Where(t => t.UserId == uId).ToList();
            return _objectMapper.Map<List<TransactionDTO>>(transactions);
        }
        public TransactionDTO GetTransactionById(int id)
        {
            try
            {
                var uId = AbpSession.UserId;
                var transaction = _transactionRepository.Get(id);
                if(transaction.UserId == uId)
                    return _objectMapper.Map<TransactionDTO>(transaction);
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public List<TransactionDTO> GetTransactionByType(TransactionType type, int userId)
        {
            var uId = AbpSession.UserId;
            if (uId == null || userId == null)
                return null;
            
            var transaction = _transactionRepository.GetAllList().Where(t => t.Type == type && t.UserId == userId).ToList();
            return _objectMapper.Map<List<TransactionDTO>>(transaction);
        }
        public List<TransactionDTO> GetTransactionsByUserId(int userId)
        {
            try
            {
                var transactions = _transactionRepository.GetAllList().Where(t => t.UserId == userId).ToList();
                return _objectMapper.Map<List<TransactionDTO>>(transactions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public TransactionDTO UpdateTransaction(TransactionDTO transaction)
        {
            try
            {
                Transaction t = null;
                if(transaction != null)
                {
                     t = _transactionRepository.Get(transaction.Id);
                    t.Amount = transaction.Amount;
                    t.CategoryId = transaction.CategoryId;
                    t.Type = transaction.Type;
                    t.Date = DateTime.Now;
                    t.Description = transaction.Description;
                }

                var uId = AbpSession.UserId;

                if (uId == null || t?.UserId != uId) return null;
                var updatedTransaction = _transactionRepository.Update(t);
                return _objectMapper.Map<TransactionDTO>(updatedTransaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public void DeleteTransaction(int id)
        {
            try
            {
                _transactionRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<TransactionDTO> GetTransactionsOneWeekAgo(int id, TransactionType? type)
        {
            DateTime today = DateTime.Now;
            DateTime oneWeekBefore = today.AddDays(-7);
            //var user = AbpSession.UserId;
            List<Transaction> transaction;
            if (type.HasValue)
            {
                transaction = _transactionRepository.GetAllList().Where(u => u.UserId == id && u.Date >= oneWeekBefore && u.Date <= today && u.Type == type).OrderBy(t => t.Date).ToList();
            }
            else
            {
                transaction = _transactionRepository.GetAllList().Where(u => u.UserId == id && u.Date >= oneWeekBefore && u.Date <= today).OrderBy(t => t.Date).ToList();
            }
            return _objectMapper.Map<List<TransactionDTO>>(transaction);
        }

        public List<TransactionDTO> GetTransactionByDate(int id, DateTime startDate, DateTime endDate, TransactionType? type)
        {
            DateTime startDateOnly = startDate.Date;
            DateTime endDateOnly = endDate.Date.AddDays(1).AddMilliseconds(-1);

            List<Transaction> transaction;
            if (type.HasValue)
            {

                transaction = _transactionRepository.GetAllList().Where(u => u.UserId == id && u.Date >= startDateOnly && u.Date <= endDateOnly && u.Type == type).OrderBy(t => t.Date).ToList();
            }
            else
            {
                transaction = _transactionRepository.GetAllList().Where(u => u.UserId == id && u.Date >= startDateOnly && u.Date <= endDateOnly).OrderBy(t => t.Date).ToList();
            }

            return _objectMapper.Map<List<TransactionDTO>>(transaction);
            //var user = AbpSession.UserId;
        }
    }
}

