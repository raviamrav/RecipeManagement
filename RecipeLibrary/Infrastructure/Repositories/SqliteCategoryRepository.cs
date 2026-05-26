using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
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
            // Re-fetch as fresh tracked entity — GetById uses AsNoTracking
            // so we must load tracked here before saving
            var tracked = _dbContext.Categories
                .First(c => c.Id == category.Id);

            tracked.Name = category.Name;
            _dbContext.SaveChanges();
        }

        public void Delete(Category category)
        {
            // Re-fetch as fresh tracked entity — same reason as Update
            var tracked = _dbContext.Categories
                .First(c => c.Id == category.Id);

            _dbContext.Categories.Remove(tracked);
            _dbContext.SaveChanges();
        }

        public Category? GetById(Guid id)
        {
            // READ-ONLY => AsNoTracking prevents double-tracking conflicts
            return _dbContext.Categories
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);
        }

        public List<Category> GetAll()
        {
            // READ-ONLY => AsNoTracking prevents double-tracking conflicts
            return _dbContext.Categories
                .AsNoTracking()
                .ToList();
        }
    }
}
