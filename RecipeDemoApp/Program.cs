using RecipeLibrary.Application.Services;
using RecipeLibrary.Domain.Entities;
using RecipeLibrary.Infrastructure.Persistence;
using RecipeLibrary.Infrastructure.Repositories;

Console.WriteLine("WORKING DIR: " + Directory.GetCurrentDirectory());
Console.WriteLine("BASE DIR: " + AppContext.BaseDirectory);

var dbContext = new RecipeDbContext();

var recipeRepository = new SqliteRecipeRepository(dbContext);
var ingredientRepository = new SqliteIngredientRepository(dbContext);
var categoryRepository = new SqliteCategoryRepository(dbContext);
var userRepository = new SqliteUserRepository(dbContext);

var recipeService = new RecipeService(recipeRepository);
var ingredientService = new IngredientService(ingredientRepository);
var categoryService = new CategoryService(categoryRepository);
var userService = new UserService(userRepository);

//var user = userService.Register(
//    "Ravi",
//    "ravi@mail.com");

var ingredient1 = ingredientService.CreateIngredient("Salt");
//var ingredient2 = ingredientService.CreateIngredient("Milk");

var category = categoryService.CreateCategory("Dessert");

//var recipe = recipeService.CreateRecipe(
//    name: "Hot Chocolate",
//    userId: user.Id,
//    categoryId: category.Id,
//    ingredientIds: new List<Guid>
//    {
//        ingredient1.Id,
//        ingredient2.Id
//    },
//    steps: new List<string>
//    {
//        "Boil milk",
//        "Add chocolate",
//        "Mix well"
//    });

//Console.WriteLine($"Recipe created: {recipe.Name}");


var saltRecipes = recipeService.GetRecipesByIngredient(ingredient1.Id);
Console.WriteLine($"Recipes with salt: {saltRecipes.Count}");
foreach (var r in saltRecipes)
{
    Console.WriteLine($"- {r.Name}");
}

var italianRecipes =
    recipeService.GetRecipesByCategory(category.Id);
Console.WriteLine($"\ncategory name:{category.Name}");
Console.WriteLine("\nRecipes by category:");

foreach (var r in italianRecipes)
{
    Console.WriteLine(r.Name);
}