using System;
using System.Collections.Generic;
using System.Text;
using RecipeLibrary.Application.Interfaces;
using RecipeLibrary.Domain.Entities;

namespace RecipeLibrary.Application.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(
            ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Category CreateCategory(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(
                    "Category name is required"
                );
            }

            var existingCategory = _categoryRepository
                .GetAll()
                .FirstOrDefault(c =>
                    c.Name.ToLower() == name.ToLower());

            if (existingCategory != null)
            {
                throw new InvalidOperationException(
                    $"Category '{name}' already exists"
                );
            }

            var category = new Category
            {
                Name = name
            };

            _categoryRepository.Add(category);

            return category;
        }

        public void UpdateCategory(Guid id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(
                    "Category name is required"
                );
            }

            var category = _categoryRepository.GetById(id);

            if (category == null)
            {
                throw new InvalidOperationException(
                    "Category not found"
                );
            }

            var existingCategory = _categoryRepository
                .GetAll()
                .FirstOrDefault(c =>
                    c.Name.ToLower() == name.ToLower()
                    && c.Id != id);

            if (existingCategory != null)
            {
                throw new InvalidOperationException(
                    "Category name already exists"
                );
            }

            category.Name = name;

            _categoryRepository.Update(category);
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll();
        }

        public void DeleteCategory(Guid id)
        {
            var category = _categoryRepository.GetById(id);

            if (category == null)
            {
                throw new InvalidOperationException(
                    "Category not found"
                );
            }

            _categoryRepository.Delete(category);
        }
    }
}
