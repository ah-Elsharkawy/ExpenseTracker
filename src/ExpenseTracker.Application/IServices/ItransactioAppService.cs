using Abp.Application.Services;
using ExpenseTracker.Dto;
using ExpenseTracker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.IServices
{
    public interface ITransactionAppService : IApplicationService
    {
        //return type is TransactionDTO,//method name is CreateTransaction,//parameter is TransactionDTO
        //create transaction
        TransactionDTO CreateTransaction(TransactionDTO input);
        List<TransactionDTO> GetTransactions();
        TransactionDTO GetTransactionById(int id);
        TransactionDTO UpdateTransaction(TransactionDTO transaction);
        void DeleteTransaction(int id);
        List<TransactionDTO> GetTransactionByType(TransactionType type, int userId);
        List<TransactionDTO> GetTransactionsByUserId(int userId);

        List<TransactionDTO> GetTransactionsOneWeekAgo(int id, TransactionType? type);
        List<TransactionDTO> GetTransactionByDate(int id, DateTime startDate, DateTime endDate, TransactionType? type);
    }
}
