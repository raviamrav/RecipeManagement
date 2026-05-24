using RecipeLibrary.Application.Services;
//using RecipeLibrary.Domain.Entities;
using RecipeLibrary.Infrastructure.Repositories;
using RecipeLibrary.Infrastructure.Persistence;

var userService = new UserService();
//var recipeRepository = new InMemoryRecipeRepository();
var dbContext = new RecipeDbContext();
var recipeRepository = new SqliteRecipeRepository(dbContext);
var recipeService = new RecipeService(recipeRepository);

var user = userService.Registration(
    "Ravivarma", 
    "ravi@mail.com"
    );

Console.WriteLine($"User created: {user.Name} - {user.Email}");

var recipe = recipeService.CreateRecipe(
    name: "Pasta Carbonara",
    userId: user.Id
    //ingredientIds: new List<Guid> { Guid.NewGuid() },
    //steps: new List<string> { "Boil pasta", "Cook bacon", "Mix with sauce" },
    //categoryIds: new List<Guid> { Guid.NewGuid() }
    );

Console.WriteLine($"Recipe created: {recipe.Name} by User ID: {recipe.UserId}");