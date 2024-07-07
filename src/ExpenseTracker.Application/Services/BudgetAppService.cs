using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using ExpenseTracker.Dto;
using ExpenseTracker.IServices;
using ExpenseTracker.Models;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Services
{
    public class BudgetAppService : ApplicationService, IBudgetAppService
    {
        private readonly IRepository<UserCategory> _repository;
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<Category> _categoryRepository;


        public BudgetAppService(IRepository<UserCategory> repository, IObjectMapper objectMapper, IRepository<Category> categoryRepository)
        {
            _repository = repository;
            _objectMapper = objectMapper;
            _categoryRepository = categoryRepository;
        }

        public bool CreateBudget(UserCategoryDTO UCategory)
        {
            var user = AbpSession.UserId;
            if (user == null)
                return false;
            var userCategory = new UserCategory
            {
                UserId = (int)user,
                CategoryId = UCategory.CategoryId,
                AmountSpent = 0,
                LimitAmount = UCategory.Amount,
                LimitType = UCategory.limitType

            };

            _repository.Insert(userCategory);
            return true;
        }

        public bool DeleteBudget(int id)
        {
            var user = (int)AbpSession.UserId;
            if (user == null)
                return false;
           var userBudget =  _repository.FirstOrDefault(x => x.Id == id && x.UserId == user);
            _repository.Delete(userBudget);
            return true;
        }
        public UserCategoryDTO GetUserCategory(int id)
        {

            var userCategory = _repository.FirstOrDefault(x => x.Id == id);
            return _objectMapper.Map<UserCategoryDTO>(userCategory);
        }

        public bool EditBudget(UserCategoryDTO budget)
        {
            var user = AbpSession.UserId;
            if (user == null)
                return false;
            var userBudget = _repository.FirstOrDefault(x => x.CategoryId == budget.CategoryId && x.UserId == user.Value);

            userBudget.LimitAmount = budget.Amount;
            _repository.Update(userBudget);
            return true;
        }

        public List<CategoryDto> getAllAvailableCategories()
        {
            // i need to get all categories not in the UserCategory table
            var user = AbpSession.UserId;
            if (user == null)
                return null;
            var userCategories = _repository.GetAllList(x => x.UserId == user.Value);
            var categories = _categoryRepository.GetAllList();
            var NotInUserCategories = categories.FindAll(x => !userCategories.Exists(y => y.CategoryId == x.Id) && x.Type == Enums.TransactionType.Expense);
            return _objectMapper.Map<List<CategoryDto>>(NotInUserCategories);
        }

        public List<BudgetDTO> getBudgets()
        {


            var userBudgets = _repository.GetAllIncluding(x => x.Category).ToList()
                .Where(x => x.UserId == AbpSession.UserId);
            return _objectMapper.Map<List<BudgetDTO>>(userBudgets);

        }
    }
}
