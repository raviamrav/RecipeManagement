using System;
using System.Collections.Generic;
using System.Text;
using RecipeLibrary.Domain.Entities;

namespace RecipeLibrary.Infrastructure.Repositories
{
    public class InMemoryRecipeRepository : IRecipeRepository
    {
        private readonly List<Recipe> _recipes = new();

        public void Add(Recipe recipe)
        {
            _recipes.Add(recipe);
        }
        public List<Recipe> GetAll()
        {
            return _recipes;
        }

        public List<Recipe> GetByUserId(Guid userId)
        {
            return _recipes.Where(r => r.UserId == userId).ToList();
        }

    }
}
