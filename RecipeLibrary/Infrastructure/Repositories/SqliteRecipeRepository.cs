using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RecipeLibrary.Domain.Entities;
using RecipeLibrary.Infrastructure.Persistence;
using RecipeLibrary.Application.Interfaces;

namespace RecipeLibrary.Infrastructure.Repositories
{
    public class SqliteRecipeRepository : IRecipeRepository
    {
        private readonly RecipeDbContext _dbContext;

        public SqliteRecipeRepository(
            RecipeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Recipe recipe)
        {
            _dbContext.Recipes.Add(recipe);
            _dbContext.SaveChanges();
        }

        public void Update(
            Guid recipeId,
            string name,
            Guid categoryId,
            List<Guid> ingredientIds,
            List<string> steps)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            _dbContext.ChangeTracker.Clear();

            var recipe = _dbContext.Recipes
                .Include(r => r.RecipeIngredients)
                .Include(r => r.RecipeSteps)
                .First(r => r.Id == recipeId);

            recipe.Name = name;
            recipe.CategoryId = categoryId;

            // Refresh child relations due to SQLite constraint batching limitations
            _dbContext.RecipeIngredients.RemoveRange(recipe.RecipeIngredients);
            _dbContext.RecipeSteps.RemoveRange(recipe.RecipeSteps);
            _dbContext.SaveChanges();
            _dbContext.ChangeTracker.Clear();

            var newIngredients = ingredientIds
                .Select(id => new RecipeIngredient
                {
                    RecipeId = recipeId,
                    IngredientId = id
                })
                .ToList();

            var newSteps = steps
                .Select((step, index) => new RecipeStep
                {
                    RecipeId = recipeId,
                    StepNumber = index + 1,
                    Description = step
                })
                .ToList();

            _dbContext.RecipeIngredients.AddRange(newIngredients);
            _dbContext.RecipeSteps.AddRange(newSteps);

            _dbContext.SaveChanges();
            transaction.Commit();
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
                .AsNoTracking()
                .Include(r => r.RecipeIngredients)
                .Include(r => r.RecipeSteps)
                .ToList();
        }

        public List<Recipe> GetByUserId(Guid id)
        {
            return _dbContext.Recipes
                .AsNoTracking()
                .Where(r => r.UserId == id)
                .Include(r => r.RecipeIngredients)
                .Include(r => r.RecipeSteps)
                .ToList();
        }

        public List<Recipe> GetByCategoryId(Guid id)
        {
            return _dbContext.Recipes
                .AsNoTracking()
                .Where(r => r.CategoryId == id)
                .Include(r => r.RecipeIngredients)
                .Include(r => r.RecipeSteps)
                .ToList();
        }

        public List<Recipe> GetByIngredientId(Guid ingredientId)
        {
            return _dbContext.Recipes
                .AsNoTracking()
                .Include(r => r.RecipeIngredients)
                .Include(r => r.RecipeSteps)
                .Where(r =>
                    r.RecipeIngredients.Any(ri =>
                        ri.IngredientId == ingredientId))
                .ToList();
        }
    }
}
