using System;
using System.Collections.Generic;
using System.Linq;
using RecipeLibrary.Application.Interfaces;
using RecipeLibrary.Domain.Entities;

namespace RecipeLibrary.Application.Services
{
    public class RecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IIngredientRepository _ingredientRepository;

        public RecipeService(
            IRecipeRepository recipeRepository,
            ICategoryRepository categoryRepository,
            IUserRepository userRepository,
            IIngredientRepository ingredientRepository)
        {
            _recipeRepository = recipeRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _ingredientRepository = ingredientRepository;
        }

        // =========================
        // CREATE
        // =========================

        public Recipe CreateRecipe(
            string name,
            Guid userId,
            List<Guid> ingredientIds,
            Guid categoryId,
            List<string> steps)
        {
            ValidateRecipe(name, userId, ingredientIds, steps, categoryId);

            var recipe = new Recipe
            {
                Name = name,
                UserId = userId,
                CategoryId = categoryId
            };

            recipe.RecipeIngredients = ingredientIds
                .Select(id => new RecipeIngredient
                {
                    RecipeId = recipe.Id,
                    IngredientId = id
                })
                .ToList();

            recipe.RecipeSteps = steps
                .Select((step, index) => new RecipeStep
                {
                    RecipeId = recipe.Id,
                    StepNumber = index + 1,
                    Description = step
                })
                .ToList();

            _recipeRepository.Add(recipe);

            return recipe;
        }

        // =========================
        // READ
        // =========================

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

        // =========================
        // UPDATE
        // =========================

        public void UpdateRecipe(
            Guid recipeId,
            string name,
            List<Guid> ingredientIds,
            Guid categoryId,
            List<string> steps)
        {
            var existingRecipe = _recipeRepository.GetByRecipeId(recipeId);

            if (existingRecipe == null)
            {
                throw new InvalidOperationException("Recipe not found");
            }

            ValidateRecipe(
                name,
                existingRecipe.UserId,
                ingredientIds,
                steps,
                categoryId,
                recipeId);

            // All EF work (load, remove children, add new children, save)
            // is done inside the repository in one clean transaction
            _recipeRepository.Update(recipeId, name, categoryId, ingredientIds, steps);
        }

        // =========================
        // DELETE
        // =========================

        public void DeleteRecipe(Guid recipeId)
        {
            var recipe = _recipeRepository.GetByRecipeId(recipeId);

            if (recipe == null)
            {
                throw new InvalidOperationException("Recipe not found");
            }

            _recipeRepository.Delete(recipe);
        }

        // =========================
        // VALIDATION
        // =========================

        private void ValidateRecipe(
            string name,
            Guid userId,
            List<Guid> ingredientIds,
            List<string> steps,
            Guid categoryId,
            Guid? existingRecipeId = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Recipe name is required");

            if (ingredientIds == null || ingredientIds.Count == 0)
                throw new ArgumentException("At least one ingredient is required");

            if (steps == null || steps.Count == 0)
                throw new ArgumentException("At least one step is required");

            var user = _userRepository.GetById(userId);

            if (user == null)
                throw new InvalidOperationException("User not found");

            var category = _categoryRepository.GetById(categoryId);

            if (category == null)
                throw new InvalidOperationException("Category not found");

            var missingIngredient = ingredientIds
                .FirstOrDefault(id => _ingredientRepository.GetById(id) == null);

            if (missingIngredient != Guid.Empty)
                throw new InvalidOperationException("Ingredient not found");

            var existingRecipe = _recipeRepository
                .GetAll()
                .FirstOrDefault(r =>
                    r.Name.ToLower() == name.ToLower()
                    && r.Id != existingRecipeId);

            if (existingRecipe != null)
                throw new InvalidOperationException("Recipe name already exists");
        }
    }
}
