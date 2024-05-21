using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using ExpenseTracker.Dto;
using ExpenseTracker.IServices;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ...




namespace ExpenseTracker.Services
{
    public class CategoryAppService : ApplicationService, ICategoryAppService
    {
        private readonly IRepository<Category> _repository;
        private readonly IObjectMapper _objectMapper;

        public CategoryAppService(IRepository<Category> repository, IObjectMapper objectMapper)
        {
            _repository = repository;
            _objectMapper = objectMapper;
        }

        public CategoryDto CreateCategory(CategoryDto input)
        {
            try
            {
                var category =  _repository.Insert(new Category { Name = input.Name, Type = input.Type });  
                return _objectMapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteCategory(int id)
        {
            try
            {
                _repository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CategoryDto> GetCategories()
        {
            try
            {
                var categories = _repository.GetAll().ToList();
                return _objectMapper.Map<List<CategoryDto>>(categories);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CategoryDto GetCategoryById(int id)
        {
            try
            {
                var c = _repository.Get(id);
                return _objectMapper.Map<CategoryDto>(c);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CategoryDto UpdateCategory(CategoryDto category)
        {
            try
            {
               var c = _repository.Update(new Category() { Id = category.Id, Name = category.Name, Type = category.Type });
               return _objectMapper.Map<CategoryDto>(c);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
