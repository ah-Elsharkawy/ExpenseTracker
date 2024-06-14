using Abp.Application.Services;
using ExpenseTracker.Dto;
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
    }
}
