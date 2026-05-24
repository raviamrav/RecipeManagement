using System;
using System.Collections.Generic;
using System.Text;
using RecipeLibrary.Domain.Entities;
using RecipeLibrary.Infrastructure.Persistence;

namespace RecipeLibrary.Infrastructure.Repositories
{
    public class SqliteRecipeRepository : IRecipeRepository
    {
        private readonly RecipeDbContext _dbContext;

        public SqliteRecipeRepository(RecipeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Recipe recipe)
        {
            _dbContext.Recipes.Add(recipe);
            _dbContext.SaveChanges();
        }

        public List<Recipe> GetAll()
        {
            return _dbContext.Recipes.ToList();
        }

        public List<Recipe> GetByUserId(Guid userId)
        {
            return _dbContext.Recipes.Where(r => r.UserId == userId).ToList();
        }


    }
}
