using Abp.Application.Services;
using ExpenseTracker.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.IServices
{
    public interface IBudgetAppService: IApplicationService
    {
        public List<CategoryDto> getAllAvailableCategories();
        public List<BudgetDTO> getBudgets();
        public bool EditBudget(UserCategoryDTO budget);
        public bool DeleteBudget(int id);
        public bool CreateBudget(UserCategoryDTO budget);

    }
}
