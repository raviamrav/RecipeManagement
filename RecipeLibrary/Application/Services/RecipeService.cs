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
        private readonly ICategoryRepository _categoryRepository;

        public RecipeService(
            IRecipeRepository recipeRepository,
            ICategoryRepository categoryRepository)
        {
            _recipeRepository = recipeRepository;
            _categoryRepository = categoryRepository;
        }

        public Recipe CreateRecipe(
            string name,
            Guid userId,
            List<Guid> ingredientIds,
            Guid categoryId,
            List<string> steps)
        {
            ValidateRecipe(
                name,
                ingredientIds,
                categoryId,
                steps
            );

            var recipe = new Recipe
            {
                Name = name,
                UserId = userId,
                CategoryId = categoryId
            };

            int stepNumber = 1;

            foreach (var step in steps)
            {
                recipe.RecipeSteps.Add(
                    new RecipeStep
                    {
                        RecipeId = recipe.Id,
                        StepNumber = stepNumber++,
                        Description = step
                    });
            }

            foreach (var ingredientId in ingredientIds)
            {
                recipe.RecipeIngredients.Add(
                    new RecipeIngredient
                    {
                        RecipeId = recipe.Id,
                        IngredientId = ingredientId
                    });
            }

            _recipeRepository.Add(recipe);

            return recipe;
        }

        public List<Recipe> GetAllRecipes()
        {
            return _recipeRepository.GetAll();
        }

        public Recipe? GetRecipeById(Guid recipeId)
        {
            return _recipeRepository.GetByRecipeId(recipeId);
        }

        public List<Recipe> GetRecipesByUser(Guid userId)
        {
            return _recipeRepository.GetByUserId(userId);
        }

        public List<Recipe> GetRecipesByCategory(Guid categoryId)
        {
            return _recipeRepository.GetByCategoryId(categoryId);
        }

        public List<Recipe> GetRecipesByIngredient(Guid ingredientId)
        {
            return _recipeRepository.GetByIngredientId(ingredientId);
        }

        public void UpdateRecipe(
            Guid recipeId,
            string name,
            List<Guid> ingredientIds,
            Guid categoryId,
            List<string> steps)
        {
            var recipe = _recipeRepository.GetByRecipeId(recipeId);

            if (recipe == null)
            {
                throw new InvalidOperationException(
                    "Recipe not found"
                );
            }

            ValidateRecipe(
                name,
                ingredientIds,
                categoryId,
                steps,
                recipeId
            );

            recipe.Name = name;
            recipe.CategoryId = categoryId;

            recipe.RecipeIngredients.Clear();

            foreach (var ingredientId in ingredientIds)
            {
                recipe.RecipeIngredients.Add(
                    new RecipeIngredient
                    {
                        RecipeId = recipe.Id,
                        IngredientId = ingredientId
                    });
            }

            recipe.RecipeSteps.Clear();

            int stepNumber = 1;

            foreach (var step in steps)
            {
                recipe.RecipeSteps.Add(
                    new RecipeStep
                    {
                        RecipeId = recipe.Id,
                        StepNumber = stepNumber++,
                        Description = step
                    });
            }

            _recipeRepository.Update(recipe);
        }

        public void DeleteRecipe(Guid recipeId)
        {
            var recipe = _recipeRepository.GetByRecipeId(recipeId);

            if (recipe == null)
            {
                throw new InvalidOperationException(
                    "Recipe not found"
                );
            }

            _recipeRepository.Delete(recipe);
        }

        private void ValidateRecipe(
            string name,
            List<Guid> ingredientIds,
            Guid categoryId,
            List<string> steps,
            Guid? existingRecipeId = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(
                    "Recipe name is required"
                );
            }

            if (ingredientIds == null || ingredientIds.Count == 0)
            {
                throw new ArgumentException(
                    "At least one ingredient is required"
                );
            }

            if (steps == null || steps.Count == 0)
            {
                throw new ArgumentException(
                    "At least one step is required"
                );
            }

            var category = _categoryRepository.GetById(categoryId);

            if (category == null)
            {
                throw new InvalidOperationException(
                    "Category does not exist"
                );
            }

            var existingRecipe = _recipeRepository
                .GetAll()
                .FirstOrDefault(r =>
                    r.Name.ToLower() == name.ToLower()
                    && r.Id != existingRecipeId);

            if (existingRecipe != null)
            {
                throw new InvalidOperationException(
                    "Recipe name already exists"
                );
            }
        }
    }
}
