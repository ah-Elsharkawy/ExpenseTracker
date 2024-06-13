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
    public interface IRecurrenceAppService: IApplicationService
    {
        List<RecurrnceDTO> GetRecurrencesForUser(int UserId);
        RecurrnceDTO GetRecurrnceDetails(int id);
        RecurrnceDTO CreateRecurrence(RecurrnceDTO input);
        void UpdateRecurrence(int id ,RecurrnceDTO input);
        void DeleteRecurrence(int id);
        List<RecurrnceDTO> GetRecurrenceByType(TransactionType type);



    }
}
