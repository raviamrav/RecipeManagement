using System;
using System.Collections.Generic;
using System.Text;
using RecipeLibrary.Domain.Entities;
using RecipeLibrary.Infrastructure.Persistence;

namespace RecipeLibrary.Infrastructure.Repositories
{
    public class SqliteCategoryRepository : ICategoryRepository
    {
        private readonly RecipeDbContext _dbContext;

        public SqliteCategoryRepository(
            RecipeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
        }

        public void Update(Category category)
        {
            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();
        }

        public Category? GetById(Guid id)
        {
            return _dbContext.Categories
                .FirstOrDefault(c => c.Id == id);
        }

        public List<Category> GetAll()
        {
            return _dbContext.Categories.ToList();
        }

        public void Delete(Category category)
        {
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
        }
    }
}
