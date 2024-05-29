using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using ExpenseTracker.Dto;
using ExpenseTracker.Enums;
using ExpenseTracker.IServices;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
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


        public TransactionAppService(IRepository<Transaction> transactionRepository, IObjectMapper objectMapper)
        {
            _transactionRepository = transactionRepository;
            _objectMapper = objectMapper;
        }
        public TransactionDTO CreateTransaction(TransactionDTO input)
        {
            //add new transaction
            var transaction = _transactionRepository.Insert(new Transaction { UserId = input.UserId, Amount = input.Amount, CategoryId = input.CategoryId, Type = input.Type, Date = input.Date, Description = input.Description });
            return _objectMapper.Map<TransactionDTO>(transaction);
        }
        public List<TransactionDTO> GetTransactions()
        {
            var transactions = _transactionRepository.GetAllList().ToList();
            return _objectMapper.Map<List<TransactionDTO>>(transactions);
        }
        public TransactionDTO GetTransactionById(int id)
        {
            try
            {
                var transaction = _transactionRepository.Get(id);
                return _objectMapper.Map<TransactionDTO>(transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public List<TransactionDTO> GetTransactionByType(TransactionType type)
        {
            var transaction = _transactionRepository.GetAllList().Where(t => t.Type == type).ToList();
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
                var updatedTransaction = _transactionRepository.Update(new Transaction { UserId = transaction.UserId,  Id = transaction.Id, Amount = transaction.Amount, CategoryId = transaction.CategoryId, Type = transaction.Type, Date = transaction.Date, Description = transaction.Description });
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
    }
}

