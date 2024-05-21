using Abp.Application.Services;
using ExpenseTracker.Dto;
using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.IServices
{
    public interface ICategoryAppService : IApplicationService
    {
        CategoryDto CreateCategory(CategoryDto input);
        List<CategoryDto> GetCategories();
        CategoryDto GetCategoryById(int id);
        CategoryDto UpdateCategory(CategoryDto category);
        void DeleteCategory(int id);
    }
}
