using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using ExpenseTracker.Authorization.Users;
using ExpenseTracker.Dto;
using ExpenseTracker.Enums;
using ExpenseTracker.IServices;
using ExpenseTracker.Models;
using ExpenseTracker.Scedulers;
using Hangfire;
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
        private readonly UserManager _userManager;
        private readonly IRepository<UserCategory> userCategory;
        private readonly IRepository<Category> _categoryRepository;
        private readonly INotificationAppService _notificationAppService;

        public IAbpSession AbpSession { get; set; }


        public TransactionAppService(IRepository<Transaction> transactionRepository, IObjectMapper objectMapper, UserManager userManager, IRepository<Category> categoryRepository, INotificationAppService notificationAppService, IRepository<UserCategory> UserCategory)
        {
            _transactionRepository = transactionRepository;
            _objectMapper = objectMapper;
            AbpSession = NullAbpSession.Instance;
            _userManager = userManager;
            userCategory = UserCategory;
            RecurringJob.AddOrUpdate<ResetBudgetSceduler>("weekly", (x) => x.ResetAllWeeklyBudgets(), "@weekly");
            RecurringJob.AddOrUpdate<ResetBudgetSceduler>("monthly", (x) => x.ResetAllMonthlyBudgets(), "@monthly");
            _categoryRepository = categoryRepository;
            _notificationAppService = notificationAppService;

        }
        [Authorize]
        public TransactionDTO CreateTransaction(TransactionDTO input, int? userid)
        {
            try
            {
                //add new transaction
                var uId = AbpSession.UserId ?? userid;
                if (uId == null)
                    return new TransactionDTO();
                var userCat = userCategory.GetAllIncluding(c=>c.Category).FirstOrDefault(x => x.CategoryId == input.CategoryId && x.UserId == uId);
                var transaction = _transactionRepository.Insert(new Transaction { UserId = (int)uId, Amount = input.Amount, CategoryId = input.CategoryId, Type = input.Type, Date = input.Date, Description = input.Description });
                var user = _userManager.GetUserById((int)uId);

                if (transaction.Type == TransactionType.Income)
                {
                    user.Balance += transaction.Amount;

                    var categoryName = _categoryRepository.Get(transaction.CategoryId).Name;

                    _notificationAppService.CreateNotification(new NewNotificationDTO
                    {
                        Message = $"A new income which is {categoryName} that worth ${transaction.Amount} has been added to your balance",
                        Type = NotificationType.reminder,
                        UserId = (int)uId
                    });
                }
                else
                {
                    if ((user.Balance - transaction.Amount) < 0)
                        throw new Exception("Not enough balance");
                    user.Balance -= transaction.Amount;
                    if(userCat != null)
                    {
                        userCat.AmountSpent += transaction.Amount;

                        userCategory.Update(userCat);

                        if (userCat.LimitAmount < userCat.AmountSpent)
                        {
                            _notificationAppService.CreateNotification(new NewNotificationDTO
                            {
                                Message = $"this category's {userCat.Category.Name} budget limit exceeded",
                                Type = NotificationType.reminder,
                                UserId = (int)uId
                            });
                        }
                    }


                    var categoryName = _categoryRepository.Get(transaction.CategoryId).Name;

                    _notificationAppService.CreateNotification(new NewNotificationDTO
                    {
                        Message = $"A new expense which is {categoryName} that worth ${transaction.Amount} has been removed from your balance",
                        Type = NotificationType.reminder,
                        UserId = (int)uId
                    });
                }


                Console.WriteLine(uId);
                return _objectMapper.Map<TransactionDTO>(transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<TransactionDTO> GetTransactions()
        {
            var uId = AbpSession.UserId;
            if (uId == null)
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
                if (transaction.UserId == uId)
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
                var uId = AbpSession.UserId;
                var user = _userManager.GetUserById((int)uId);
                var userCat = userCategory.FirstOrDefault(x => x.CategoryId == transaction.CategoryId && x.UserId == user.Id);

                if (transaction != null)
                {
                    t = _transactionRepository.Get(transaction.Id);

                    t.CategoryId = transaction.CategoryId;
                    if (t.Amount != transaction.Amount)
                    {
                        user.Balance -= t.Amount;
                        if (transaction.Type == TransactionType.Income)
                            user.Balance += transaction.Amount;
                        else
                        {
                            if (user.Balance - transaction.Amount < 0)
                                throw new Exception("Not enough balance");
                            user.Balance -= transaction.Amount;
                        }

                    }
                    t.Amount = transaction.Amount;
                    t.Type = transaction.Type;
                    t.Date = DateTime.Now;
                    t.Description = transaction.Description;
                    if(userCategory != null)
                    {
                        userCat.AmountSpent += t.Amount;
                        userCat.AmountSpent -= transaction.Amount;
                        userCategory.Update(userCat);
                    }
                }



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
                Transaction t = null;
                t = _transactionRepository.Get(id);
                var uId = AbpSession.UserId;
                var user = _userManager.GetUserById((int)uId);
                var userCat = userCategory.FirstOrDefault(x => x.CategoryId == t.CategoryId && x.UserId == user.Id);

                if (t != null && user != null)
                {
                    if ((user.Balance - t.Amount) < 0)
                        throw new Exception("Not enough balance");
                    user.Balance -= t.Amount;
                }
                if(userCategory != null)
                {
                    userCat.AmountSpent -= t.Amount;
                    userCategory.Update(userCat);
                }

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
        public TotalIncomesDTO GetTotalIncomeByMonth(int id)
        {
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate - TimeSpan.FromDays(30);

            double totalIncome = _transactionRepository
                .GetAllList()
                .Where(u => u.UserId == id && u.Date <= endDate && u.Date >= startDate)
                .Where(t => t.Type == TransactionType.Income)
                .Sum(t => t.Amount);

            var totalIncomesDTO = new TotalIncomesDTO
            {
                TotalIncome = totalIncome
            };
            return totalIncomesDTO;
        }

        public TotalExpensesDTO GetTotalExpenseByMonth(int id)
        {
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate - TimeSpan.FromDays(30);

            double totalExpense = _transactionRepository
                .GetAllList()
                .Where(u => u.UserId == id && u.Date <= endDate && u.Date >= startDate)
                .Where(t => t.Type == TransactionType.Expense)
                .Sum(t => t.Amount);

            var totalExpensesDTO = new TotalExpensesDTO
            {
                TotalExpense = totalExpense
            };

            return totalExpensesDTO;
        }

        public BalanceDTO GetBalance(int id)
        {
            var user = _userManager.GetUserById(id);
            var balance = new BalanceDTO
            {
                Balance = user.Balance
            };
            return balance;
        }

        public List<CategoryExpenseDto> GetCategoryExpenses(int _Month)
        {
            var uId = AbpSession.UserId;
            if (uId == null)
                return new List<CategoryExpenseDto>(); // or handle the null case appropriately

            var month = _Month;
            var categoryExpenses = _transactionRepository.GetAllList()
                .Where(t => t.Date.Month == month && t.UserId == uId)
                .Join(_categoryRepository.GetAllList().Where(c => c.Type == (TransactionType)1),
                      t => t.CategoryId,
                      c => c.Id,
                      (t, c) => new { t.Amount, c.Name, t.CategoryId })
                .GroupBy(tc => new { tc.CategoryId, tc.Name })
                .Select(g => new CategoryExpenseDto
                {
                    TotalExpenses = g.Sum(tc => tc.Amount),
                    CategoryName = g.Key.Name
                })
                .ToList();

            return categoryExpenses;
        }
    }

        


    
}


