using RecipeLibrary.Application.Services;
using RecipeLibrary.Domain.Entities;
using RecipeLibrary.Infrastructure.Persistence;
using RecipeLibrary.Infrastructure.Repositories;

// =======================
// DATABASE + REPOSITORIES
// =======================
var dbContext = new RecipeDbContext();

var userRepository = new SqliteUserRepository(dbContext);
var categoryRepository = new SqliteCategoryRepository(dbContext);
var ingredientRepository = new SqliteIngredientRepository(dbContext);
var recipeRepository = new SqliteRecipeRepository(dbContext);

var userService = new UserService(userRepository);
var categoryService = new CategoryService(categoryRepository);
var ingredientService = new IngredientService(ingredientRepository);
var recipeService = new RecipeService(recipeRepository);

// =======================
// 1. USER CREATION
// =======================
Console.WriteLine("\n=== USER CREATION ===");

User? user = null;
try
{
    user = userService.Register(
        "Ravi",
        "ravi@mail.com"
    );

    Console.WriteLine(
        $"User created: {user.Name} ({user.Email})"
    );
}
catch (Exception)
{
    Console.WriteLine(
        $"User already exists. Loading existing user."
    );

    user = userRepository.GetByEmail("ravi@mail.com");

    if (user != null)
    {
        Console.WriteLine(
            $"Existing user loaded: {user!.Name}"
        );
    }
    else
    {
        Console.WriteLine(
            "User could not be loaded."
        );
    }
}

// =======================
// 2. INGREDIENTS
// =======================
Console.WriteLine("\n=== INGREDIENTS ===");

var salt = ingredientService.CreateIngredient("Salt");
var milk = ingredientService.CreateIngredient("Milk");
var chocolate = ingredientService.CreateIngredient("Chocolate");

Console.WriteLine($"Ingredients created: {salt.Name}, {milk.Name}, {chocolate.Name}");

// =======================
// 3. CATEGORIES
// =======================
Console.WriteLine("\n=== CATEGORIES ===");

var italian = categoryService.CreateCategory("Italian");

Console.WriteLine($"Category created: {italian.Name}");

// =======================
// 4. RECIPE CREATION
// =======================
Console.WriteLine("\n=== RECIPE CREATION ===");

var userId = user!.Id;
var categoryId = italian.Id;
var recipe = recipeService.CreateRecipe(
    name: "Pasta Carbonara",
    userId: userId,
    ingredientIds: new List<Guid>
    {
        salt.Id,
        milk.Id,
        chocolate.Id
    },
    categoryId: categoryId,
    steps: new List<string>
    {
        "Boil milk",
        "Add chocolate",
        "Mix well"
    }
);

Console.WriteLine($"Recipe created: {recipe.Name}");

// =======================
// 5. QUERY DEMO
// =======================
Console.WriteLine("\n=== QUERY RESULTS ===");

// By User
var userRecipes = recipeService.GetRecipesByUser(user.Id);

Console.WriteLine("\nRecipes by User:");
foreach (var r in userRecipes)
{
    Console.WriteLine($"- {r.Name}");
}

// By Category
var categoryRecipes = recipeService.GetRecipesByCategory(italian.Id);

Console.WriteLine("\nRecipes by Category:");
foreach (var r in categoryRecipes)
{
    Console.WriteLine($"- {r.Name}");
}

// By Ingredient
var ingredientRecipes = recipeService.GetRecipesByIngredient(salt.Id);

Console.WriteLine("\nRecipes by Ingredient:");
foreach (var r in ingredientRecipes)
{
    Console.WriteLine($"- {r.Name}");
}

// =======================
// END
// =======================
Console.WriteLine("\n=== DEMO COMPLETE ===");