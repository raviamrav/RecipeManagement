using RecipeLibrary;
using RecipeLibrary.Domain.Entities;

using var recipeManagement = new RecipeManagement();

// =======================
// 1. USER CREATION
// =======================

Console.WriteLine("\n=== USER CREATION ===");

var user = GetOrCreateUser(
    "Ravi",
    "ravi@mail.com");

// =======================
// 2. INGREDIENT CREATION
// =======================

Console.WriteLine("\n=== INGREDIENT CREATION ===");

var salt = GetOrCreateIngredient("Salt");
var milk = GetOrCreateIngredient("Milk");
var chocolate = GetOrCreateIngredient("Chocolate");

// =======================
// 3. CATEGORY CREATION
// =======================

Console.WriteLine("\n=== CATEGORY CREATION ===");

var italian = GetOrCreateCategory(
    originalName: "Italian",
    updatedName: "Updated Italian");

// =======================
// 4. RECIPE CREATION
// =======================

Console.WriteLine("\n=== RECIPE CREATION ===");

var recipe = GetOrCreateRecipe(
    originalName: "Pasta Carbonara",
    updatedName: "Updated Pasta Carbonara",
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
    });

// =======================
// 5. RECIPE UPDATE
// =======================

Console.WriteLine("\n=== RECIPE UPDATE ===");

recipeManagement.UpdateRecipe(
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

var recipesByUser = recipeManagement.GetRecipesByUser(user.Id);

foreach (var r in recipesByUser)
{
    Console.WriteLine($"- {r.Name}");
}

// =======================
// 7. RECIPES BY CATEGORY
// =======================

Console.WriteLine("\n=== RECIPES BY CATEGORY ===");

var recipesByCategory =
    recipeManagement.GetRecipesByCategory(italian.Id);

foreach (var r in recipesByCategory)
{
    Console.WriteLine($"- {r.Name}");
}

// =======================
// 8. RECIPES BY INGREDIENT
// =======================

Console.WriteLine("\n=== RECIPES BY INGREDIENT ===");

var recipesByIngredient =
    recipeManagement.GetRecipesByIngredient(salt.Id);

foreach (var r in recipesByIngredient)
{
    Console.WriteLine($"- {r.Name}");
}

// =======================
// 9. CATEGORY UPDATE
// =======================

Console.WriteLine("\n=== CATEGORY UPDATE ===");

recipeManagement.UpdateCategory(
    italian.Id,
    "Updated Italian"
);

Console.WriteLine("Category updated successfully");

// =======================
// 10. DELETE RECIPE
// =======================

Console.WriteLine("\n=== DELETE RECIPE ===");

var tempRecipe = CreateTemporaryRecipe(
    user.Id,
    salt.Id,
    italian.Id);

recipeManagement.DeleteRecipe(tempRecipe.Id);

Console.WriteLine("Temporary recipe deleted successfully");

// =======================
// 11. DELETE CATEGORY
// =======================

Console.WriteLine("\n=== DELETE CATEGORY ===");

var tempCategory =
    CreateTemporaryCategory();

recipeManagement.DeleteCategory(tempCategory.Id);

Console.WriteLine("Temporary category deleted successfully");

// =======================
// DEMO COMPLETE
// =======================

Console.WriteLine("\n=== DEMO COMPLETE ===");

User GetOrCreateUser(
    string name,
    string email)
{
    var existingUser = recipeManagement.GetUserByEmail(email);

    if (existingUser != null)
    {
        Console.WriteLine($"Existing user loaded: {existingUser.Name}");
        return existingUser;
    }

    var user = recipeManagement.RegisterUser(name, email);

    Console.WriteLine($"User created: {user.Name}");
    return user;
}

Ingredient GetOrCreateIngredient(string name)
{
    var existingIngredient = recipeManagement.GetIngredientByName(name);

    if (existingIngredient != null)
    {
        Console.WriteLine($"Ingredient '{name}' already exists");
        return existingIngredient;
    }

    var ingredient = recipeManagement.CreateIngredient(name);

    Console.WriteLine($"Ingredient created: {ingredient.Name}");
    return ingredient;
}

Category GetOrCreateCategory(
    string originalName,
    string updatedName)
{
    var existingCategory = recipeManagement
        .GetAllCategories()
        .FirstOrDefault(c =>
            c.Name.Equals(originalName, StringComparison.OrdinalIgnoreCase)
            || c.Name.Equals(updatedName, StringComparison.OrdinalIgnoreCase));

    if (existingCategory != null)
    {
        Console.WriteLine($"Category '{originalName}' already exists");
        return existingCategory;
    }

    var category = recipeManagement.CreateCategory(originalName);

    Console.WriteLine($"Category created: {category.Name}");
    return category;
}

Recipe GetOrCreateRecipe(
    string originalName,
    string updatedName,
    Guid userId,
    List<Guid> ingredientIds,
    Guid categoryId,
    List<string> steps)
{
    var existingRecipe = recipeManagement
        .GetAllRecipes()
        .FirstOrDefault(r =>
            r.Name.Equals(originalName, StringComparison.OrdinalIgnoreCase)
            || r.Name.Equals(updatedName, StringComparison.OrdinalIgnoreCase));

    if (existingRecipe != null)
    {
        Console.WriteLine("Recipe name already exists");
        return existingRecipe;
    }

    var recipe = recipeManagement.CreateRecipe(
        name: originalName,
        userId: userId,
        ingredientIds: ingredientIds,
        categoryId: categoryId,
        steps: steps);

    Console.WriteLine($"Recipe created: {recipe.Name}");
    return recipe;
}

Recipe CreateTemporaryRecipe(
    Guid userId,
    Guid ingredientId,
    Guid categoryId)
{
    var existingTemporaryRecipe = recipeManagement
        .GetAllRecipes()
        .FirstOrDefault(r =>
            r.Name.Equals(
                "Temporary Recipe",
                StringComparison.OrdinalIgnoreCase));

    if (existingTemporaryRecipe != null)
    {
        recipeManagement.DeleteRecipe(existingTemporaryRecipe.Id);
    }

    return recipeManagement.CreateRecipe(
        "Temporary Recipe",
        userId,
        new List<Guid> { ingredientId },
        categoryId,
        new List<string> { "Temporary step" });
}

Category CreateTemporaryCategory()
{
    var existingTemporaryCategory = recipeManagement
        .GetAllCategories()
        .FirstOrDefault(c =>
            c.Name.Equals(
                "Temporary Category",
                StringComparison.OrdinalIgnoreCase));

    if (existingTemporaryCategory != null)
    {
        recipeManagement.DeleteCategory(existingTemporaryCategory.Id);
    }

    return recipeManagement.CreateCategory("Temporary Category");
}
