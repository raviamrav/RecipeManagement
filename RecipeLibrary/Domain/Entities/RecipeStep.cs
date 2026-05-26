using System;

namespace RecipeLibrary.Domain.Entities
{
    public class RecipeStep
    {
        public Guid Id { get; set; } =
            Guid.NewGuid();

        public Guid RecipeId { get; set; }

        public Recipe Recipe { get; set; } = null!;

        public int StepNumber { get; set; }

        public string Description { get; set; } =
            string.Empty;
    }
}