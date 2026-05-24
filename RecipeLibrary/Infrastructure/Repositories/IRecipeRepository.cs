using System;
using System.Collections.Generic;
using System.Text;
using RecipeLibrary.Domain.Entities;

namespace RecipeLibrary.Infrastructure.Repositories
{
    public interface IRecipeRepository
    {
        void Add(Recipe recipe);
        List<Recipe> GetAll();

        List<Recipe> GetByUserId(Guid userId);
    }
}
