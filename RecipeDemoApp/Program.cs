using RecipeLibrary.Application.Services;
using RecipeLibrary.Domain.Entities;
using RecipeLibrary.Infrastructure.Persistence;
using RecipeLibrary.Infrastructure.Repositories;

// =======================
// DATABASE + REPOSITORIES
// =======================

var dbContext = new RecipeDbContext();

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
// VARIABLES
// =======================

User? user = null;

Ingredient? salt = null;
Ingredient? milk = null;
Ingredient? chocolate = null;

Category? italian = null;

Recipe? recipe = null;

// =======================
// USER CREATION
// =======================

Console.WriteLine("\n=== USER CREATION ===");

try
{
    user = userService.Register(
        "Ravi",
        "ravi@mail.com"
    );

    Console.WriteLine(
        $"User created: {user.Name}"
    );
}
catch (Exception)
{
    user = userRepository
        .GetByEmail("ravi@mail.com");

    Console.WriteLine(
        $"Existing user loaded: {user?.Name}"
    );
}

// =======================
// INGREDIENT CREATION
// =======================

Console.WriteLine("\n=== INGREDIENT CREATION ===");

try
{
    salt = ingredientService
        .CreateIngredient("Salt");

    Console.WriteLine(
        $"Ingredient created: {salt.Name}"
    );
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);

    salt = ingredientRepository
        .GetAll()
        .FirstOrDefault(i =>
            i.Name == "Salt");
}

try
{
    milk = ingredientService
        .CreateIngredient("Milk");

    Console.WriteLine(
        $"Ingredient created: {milk.Name}"
    );
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);

    milk = ingredientRepository
        .GetAll()
        .FirstOrDefault(i =>
            i.Name == "Milk");
}

try
{
    chocolate = ingredientService
        .CreateIngredient("Chocolate");

    Console.WriteLine(
        $"Ingredient created: {chocolate.Name}"
    );
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);

    chocolate = ingredientRepository
        .GetAll()
        .FirstOrDefault(i =>
            i.Name == "Chocolate");
}

// =======================
// CATEGORY CREATION
// =======================

Console.WriteLine("\n=== CATEGORY CREATION ===");

try
{
    italian = categoryService
        .CreateCategory("Italian");

    Console.WriteLine(
        $"Category created: {italian.Name}"
    );
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);

    italian = categoryRepository
        .GetAll()
        .FirstOrDefault(c =>
            c.Name == "Italian");
}

// =======================
// RECIPE CREATION
// =======================

Console.WriteLine("\n=== RECIPE CREATION ===");

if (
    user != null &&
    salt != null &&
    milk != null &&
    chocolate != null &&
    italian != null
)
{
    try
    {
        recipe = recipeService.CreateRecipe(
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

        Console.WriteLine(
            $"Recipe created: {recipe.Name}"
        );
    }
    catch (Exception ex)
    {
        Console.WriteLine(
            $"Recipe creation failed: {ex.Message}"
        );
    }
}

// =======================
// UPDATE RECIPE
// =======================

Console.WriteLine("\n=== RECIPE UPDATE ===");

if (
    recipe != null &&
    salt != null &&
    italian != null
)
{
    try
    {
        recipeService.UpdateRecipe(
            recipe.Id,

            "Updated Pasta Carbonara",

            new List<Guid>
            {
                salt.Id
            },

            italian.Id,

            new List<string>
            {
                "Updated Step 1",
                "Updated Step 2"
            }
        );

        Console.WriteLine(
            "Recipe updated successfully"
        );
    }
    catch (Exception ex)
    {
        Console.WriteLine(
            $"Recipe update failed: {ex.Message}"
        );
    }
}

// =======================
// QUERY BY USER
// =======================

Console.WriteLine("\n=== RECIPES BY USER ===");

if (user != null)
{
    var recipesByUser =
        recipeService.GetRecipesByUser(
            user.Id
        );

    foreach (var r in recipesByUser)
    {
        Console.WriteLine(
            $"- {r.Name}"
        );
    }
}

// =======================
// QUERY BY CATEGORY
// =======================

Console.WriteLine("\n=== RECIPES BY CATEGORY ===");

if (italian != null)
{
    var recipesByCategory =
        recipeService.GetRecipesByCategory(
            italian.Id
        );

    foreach (var r in recipesByCategory)
    {
        Console.WriteLine(
            $"- {r.Name}"
        );
    }
}

// =======================
// QUERY BY INGREDIENT
// =======================

Console.WriteLine("\n=== RECIPES BY INGREDIENT ===");

if (salt != null)
{
    var recipesByIngredient =
        recipeService.GetRecipesByIngredient(
            salt.Id
        );

    foreach (var r in recipesByIngredient)
    {
        Console.WriteLine(
            $"- {r.Name}"
        );
    }
}

// =======================
// CATEGORY UPDATE
// =======================

Console.WriteLine("\n=== CATEGORY UPDATE ===");

if (italian != null)
{
    try
    {
        categoryService.UpdateCategory(
            italian.Id,
            "Updated Italian"
        );

        Console.WriteLine(
            "Category updated successfully"
        );
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

// =======================
// DEMO COMPLETE
// =======================

Console.WriteLine(
    "\n=== DEMO COMPLETE ==="
);