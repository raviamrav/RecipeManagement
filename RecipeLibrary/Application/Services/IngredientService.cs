using System;
using System.Collections.Generic;
using System.Text;
using RecipeLibrary.Application.Interfaces;
using RecipeLibrary.Domain.Entities;

namespace RecipeLibrary.Application.Services
{
    public class IngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public Ingredient CreateIngredient(string name)
        {
            var existingIngredient = _ingredientRepository.GetByName(name);
            if (existingIngredient != null)
            {
                throw new InvalidOperationException(
                    $"An ingredient with the name '{name}' already exists."
                );
            }

            var ingredient = new Ingredient
            {
                Name = name
            };
            _ingredientRepository.Add(ingredient);
            return ingredient;
        }

        public List<Ingredient> GetAll()
        {
            return _ingredientRepository.GetAll();
        }
    }
}
