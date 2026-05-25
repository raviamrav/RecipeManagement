using System;
using System.Collections.Generic;
using System.Text;
using RecipeLibrary.Domain.Entities;
using RecipeLibrary.Infrastructure.Repositories;

namespace RecipeLibrary.Application.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Category CreateCategory(string name)
        {
            var existingCategory = _categoryRepository.GetAll().FirstOrDefault(c => c.Name.ToLower() == name.ToLower());
            if (existingCategory != null)
            {
                //throw new InvalidOperationException($"A category with the name '{name}' already exists.");
                return existingCategory;
            }

            var category = new Category
            {
                Name = name
            };
            _categoryRepository.Add(category);
            return category;
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll();
        }

        public Category? GetCategoryById(Guid id)
        {
            return _categoryRepository.GetById(id);
        }
    }
}
