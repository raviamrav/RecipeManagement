using System;
using System.Collections.Generic;
using System.Text;
using RecipeLibrary.Domain.Entities;
using RecipeLibrary.Infrastructure.Repositories;

namespace RecipeLibrary.Application.Services
{
    public class RecipeService
    {
        //private readonly List<Recipe> _recipes = new();
        private readonly IRecipeRepository _recipeRepository;
        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public Recipe CreateRecipe(string name, Guid userId 
            //, List<Guid> ingredientIds, List<string> steps, List<Guid> categoryIds
            )
        {

            //if (_recipes.Any(r => r.Name == name))
            if (_recipeRepository.GetAll().Any(r => r.Name == name))
                throw new Exception("A recipe with the same name already exists.");

            //if (ingredientIds == null || ingredientIds.Count == 0)
            //    throw new Exception("A recipe must have at least one ingredient.");

            //if (steps == null || steps.Count == 0)
            //    throw new Exception("A recipe must have at least one step.");

            //if (categoryIds == null || categoryIds.Count == 0)
            //    throw new Exception("A recipe must belong to at least one category.");

            var recipe = new Recipe
            {
                Name = name,
                UserId = userId,
                //IngredientIds = ingredientIds,
                //Steps = steps,
                //CategoryIds = categoryIds
            };

            //_recipes.Add(recipe);
            _recipeRepository.Add(recipe);

            return recipe;
        }


        public List<Recipe> GetAllRecipes()
        {
            return _recipeRepository.GetAll();
        }

        public List<Recipe> GetRecipesByUser(Guid userId)
        {
            return _recipeRepository.GetByUserId(userId);
        }

    }
}