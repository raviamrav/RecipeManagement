using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        public void Update(Recipe recipe)
        {
            _dbContext.Recipes.Update(recipe);
            _dbContext.SaveChanges();
        }

        public void Delete(Recipe recipe)
        {
            _dbContext.Recipes.Remove(recipe);
            _dbContext.SaveChanges();
        }

        public Recipe? GetByRecipeId(Guid id)
        {
            return _dbContext.Recipes
                .Include(r => r.RecipeIngredients)
                .Include(r => r.RecipeSteps)
                .FirstOrDefault(r => r.Id == id);
        }

        public List<Recipe> GetAll()
        {
            return _dbContext.Recipes
                .Include(r => r.RecipeIngredients)
                .Include(r => r.RecipeSteps)
                .ToList();
        }

        public List<Recipe> GetByUserId(Guid id)
        {
            return _dbContext.Recipes
                .Where(r => r.UserId == id)
                .ToList();
        }

        public List<Recipe> GetByCategoryId(Guid categoryId)
        {
            return _dbContext.Recipes
                .Where(r => r.CategoryId == categoryId)
                .ToList();
        }

        public List<Recipe> GetByIngredientId(Guid ingredientId)
        {
            return _dbContext.Recipes
                .Where(r => r.RecipeIngredients.Any(ri => ri.IngredientId == ingredientId))
                .ToList();
        }
    }
}