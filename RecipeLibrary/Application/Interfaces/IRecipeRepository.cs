using System;
using System.Collections.Generic;
using System.Text;
using RecipeLibrary.Domain.Entities;

namespace RecipeLibrary.Application.Interfaces
{
    public interface IRecipeRepository
    {
        void Add(Recipe recipe);
        void Update(Guid recipeId, string name, Guid categoryId, List<Guid> ingredientIds, List<string> steps);
        void Delete(Recipe recipe);
        Recipe? GetByRecipeId(Guid Id);
        List<Recipe> GetAll();
        List<Recipe> GetByUserId(Guid Id);
        List<Recipe> GetByCategoryId(Guid CategoryId);
        List<Recipe> GetByIngredientId(Guid IngredientId);
    }
}
