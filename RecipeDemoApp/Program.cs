using RecipeLibrary.Application.Services;
using RecipeLibrary.Domain.Entities;
using RecipeLibrary.Infrastructure.Persistence;
using RecipeLibrary.Infrastructure.Repositories;

// =======================
// DATABASE + REPOSITORIES
// =======================

var dbContext = new RecipeDbContext();

// Always start with a fresh database so the demo
// runs cleanly every time with no stale data
dbContext.Database.EnsureDeleted();
dbContext.Database.EnsureCreated();

var userRepository =
    new SqliteUserRepository(dbContext);

var categoryRepository =
    new SqliteCategoryRepository(dbContext);

var ingredientRepository =
    new SqliteIngredientRepository(dbContext);

var recipeRepository =
    new SqliteRecipeRepository(dbContext);

// =======================
// SERVICES
// =======================

var userService =
    new UserService(userRepository);

var categoryService =
    new CategoryService(categoryRepository);

var ingredientService =
    new IngredientService(ingredientRepository);

var recipeService =
    new RecipeService(
        recipeRepository,
        categoryRepository
    );

// =======================
// 1. USER CREATION
// =======================

Console.WriteLine("\n=== USER CREATION ===");

var user = userService.Register(
    "Ravi",
    "ravi@mail.com"
);

Console.WriteLine($"User created: {user.Name}");

// =======================
// 2. INGREDIENT CREATION
// =======================

Console.WriteLine("\n=== INGREDIENT CREATION ===");

var salt = ingredientService.CreateIngredient("Salt");
var milk = ingredientService.CreateIngredient("Milk");
var chocolate = ingredientService.CreateIngredient("Chocolate");

Console.WriteLine($"Ingredient created: {salt.Name}");
Console.WriteLine($"Ingredient created: {milk.Name}");
Console.WriteLine($"Ingredient created: {chocolate.Name}");

// =======================
// 3. CATEGORY CREATION
// =======================

Console.WriteLine("\n=== CATEGORY CREATION ===");

var italian = categoryService.CreateCategory("Italian");

Console.WriteLine($"Category created: {italian.Name}");

// =======================
// 4. RECIPE CREATION
// =======================

Console.WriteLine("\n=== RECIPE CREATION ===");

var recipe = recipeService.CreateRecipe(
    name: "Pasta Carbonara",
    userId: user.Id,
    ingredientIds: new List<Guid>
    {
        salt.Id,
        milk.Id,
        chocolate.Id
    },
    categoryId: italian.Id,
    steps: new List<string>
    {
        "Boil milk",
        "Add chocolate",
        "Mix well"
    }
);

Console.WriteLine($"Recipe created: {recipe.Name}");

// =======================
// 5. RECIPE UPDATE
// =======================

Console.WriteLine("\n=== RECIPE UPDATE ===");

recipeService.UpdateRecipe(
    recipe.Id,
    "Updated Pasta Carbonara",
    new List<Guid> { salt.Id },
    italian.Id,
    new List<string>
    {
        "Updated Step 1",
        "Updated Step 2"
    }
);

Console.WriteLine("Recipe updated successfully");

// =======================
// 6. RECIPES BY USER
// =======================

Console.WriteLine("\n=== RECIPES BY USER ===");

var recipesByUser = recipeService.GetRecipesByUser(user.Id);

foreach (var r in recipesByUser)
{
    Console.WriteLine($"- {r.Name}");
}

// =======================
// 7. RECIPES BY CATEGORY
// =======================

Console.WriteLine("\n=== RECIPES BY CATEGORY ===");

var recipesByCategory =
    recipeService.GetRecipesByCategory(italian.Id);

foreach (var r in recipesByCategory)
{
    Console.WriteLine($"- {r.Name}");
}

// =======================
// 8. RECIPES BY INGREDIENT
// =======================

Console.WriteLine("\n=== RECIPES BY INGREDIENT ===");

var recipesByIngredient =
    recipeService.GetRecipesByIngredient(salt.Id);

foreach (var r in recipesByIngredient)
{
    Console.WriteLine($"- {r.Name}");
}

// =======================
// 9. CATEGORY UPDATE
// =======================

Console.WriteLine("\n=== CATEGORY UPDATE ===");

categoryService.UpdateCategory(
    italian.Id,
    "Updated Italian"
);

Console.WriteLine("Category updated successfully");

// =======================
// 10. DELETE RECIPE
// =======================

Console.WriteLine("\n=== DELETE RECIPE ===");

var tempRecipe = recipeService.CreateRecipe(
    "Temporary Recipe",
    user.Id,
    new List<Guid> { salt.Id },
    italian.Id,
    new List<string> { "Temporary step" }
);

recipeService.DeleteRecipe(tempRecipe.Id);

Console.WriteLine("Temporary recipe deleted successfully");

// =======================
// 11. DELETE CATEGORY
// =======================

Console.WriteLine("\n=== DELETE CATEGORY ===");

var tempCategory =
    categoryService.CreateCategory("Temporary Category");

categoryService.DeleteCategory(tempCategory.Id);

Console.WriteLine("Temporary category deleted successfully");

// =======================
// DEMO COMPLETE
// =======================

Console.WriteLine("\n=== DEMO COMPLETE ===");
