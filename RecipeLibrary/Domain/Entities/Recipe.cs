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
        //public List<Guid> IngredientIds { get; set; } = new();
        //public List<string> Steps { get; set; } = new();
        //public List<Guid> CategoryIds { get; set; } = new();
    }
}
