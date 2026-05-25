using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeLibrary.Domain.Entities
{
    public class Recipe
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public Guid CategoryId { get; set; }

        public List<RecipeIngredient> RecipeIngredients { get; set; } = new();

        public List<RecipeStep> RecipeSteps { get; set; } = new();
    }
}
