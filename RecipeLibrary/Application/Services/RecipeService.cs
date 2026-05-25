using System;
using System.Collections.Generic;
using System.Text;

using RecipeLibrary.Domain.Entities;
using RecipeLibrary.Infrastructure.Repositories;

namespace RecipeLibrary.Application.Services
{
    public class RecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public Recipe CreateRecipe(
            string name,
            Guid userId,
            Guid categoryId,
            List<Guid> ingredientIds,
            List<string> steps)
        {
            ValidateRecipe(name, ingredientIds, steps);

            var recipe = new Recipe
            {
                Name = name,
                UserId = userId,
                CategoryId = categoryId
            };

            foreach (var ingredientId in ingredientIds)
            {
                recipe.RecipeIngredients.Add(new RecipeIngredient
                {
                    RecipeId = recipe.Id,
                    IngredientId = ingredientId
                });
            }

            int stepNumber = 1;

            foreach (var step in steps)
            {
                recipe.RecipeSteps.Add(new RecipeStep
                {
                    RecipeId = recipe.Id,
                    StepNumber = stepNumber,
                    Description = step
                });

                stepNumber++;
            }

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

        public Recipe DeleteRecipe(Guid recipeId)
        {
            var recipe = _recipeRepository.GetByRecipeId(recipeId);
            if (recipe == null)
            {
                throw new Exception("Recipe not found");
            }
            _recipeRepository.Delete(recipe);
            return recipe;
        }

        public List<Recipe> GetRecipesByCategory(Guid categoryId)
        {
            return _recipeRepository.GetByCategoryId(categoryId);
        }

        public List<Recipe> GetRecipesByIngredient(Guid ingredientId)
        {
            return _recipeRepository.GetByIngredientId(ingredientId);
        }

        private void ValidateRecipe(
            string name,
            List<Guid> ingredientIds,
            List<string> steps)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Recipe name is required");
            }

            if (ingredientIds.Count == 0)
            {
                throw new Exception("At least one ingredient is required");
            }

            if (steps.Count == 0)
            {
                throw new Exception("At least one step is required");
            }

            var existingRecipe = _recipeRepository
                .GetAll()
                .FirstOrDefault(r => r.Name.ToLower() == name.ToLower());

            if (existingRecipe != null)
            {
                throw new Exception("Recipe name already exists");
            }
        }
    }
}