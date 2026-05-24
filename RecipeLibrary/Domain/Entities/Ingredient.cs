using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeLibrary.Domain.Entities
{
    public class Ingredient
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
    }
}
