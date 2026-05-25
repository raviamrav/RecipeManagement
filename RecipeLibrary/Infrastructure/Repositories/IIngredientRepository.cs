using System;
using System.Collections.Generic;
using System.Text;
using RecipeLibrary.Domain.Entities;

namespace RecipeLibrary.Infrastructure.Repositories
{
    public interface IIngredientRepository
    {
        void Add(Ingredient ingredient);
        List<Ingredient> GetAll();
        Ingredient? GetById(Guid id);
        Ingredient? GetByName(string name);
        void Delete(Ingredient ingredient);
    }
}
