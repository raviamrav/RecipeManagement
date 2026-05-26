using RecipeLibrary.Application.Services;
using RecipeLibrary.Domain.Entities;
using RecipeLibrary.Infrastructure.Persistence;
using RecipeLibrary.Infrastructure.Repositories;

namespace RecipeLibrary
{
    public sealed class RecipeManagement : IDisposable
    {
        private readonly RecipeDbContext _dbContext;
        private readonly UserService _userService;
        private readonly CategoryService _categoryService;
        private readonly IngredientService _ingredientService;
        private readonly RecipeService _recipeService;
        private readonly SqliteUserRepository _userRepository;
        private readonly SqliteCategoryRepository _categoryRepository;
        private readonly SqliteIngredientRepository _ingredientRepository;

        public RecipeManagement()
        {
            _dbContext = new RecipeDbContext();
            _dbContext.Database.EnsureCreated();

            _userRepository = new SqliteUserRepository(_dbContext);
            _categoryRepository = new SqliteCategoryRepository(_dbContext);
            _ingredientRepository = new SqliteIngredientRepository(_dbContext);
            var recipeRepository = new SqliteRecipeRepository(_dbContext);

            _userService = new UserService(_userRepository);
            _categoryService = new CategoryService(_categoryRepository);
            _ingredientService = new IngredientService(_ingredientRepository);
            _recipeService = new RecipeService(
                recipeRepository,
                _categoryRepository,
                _userRepository,
                _ingredientRepository);
        }

        public User RegisterUser(
            string name,
            string email)
        {
            return _userService.Register(name, email);
        }

        public User? GetUserByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }

        public Ingredient CreateIngredient(string name)
        {
            return _ingredientService.CreateIngredient(name);
        }

        public Ingredient? GetIngredientByName(string name)
        {
            return _ingredientRepository.GetByName(name);
        }

        public Category CreateCategory(string name)
        {
            return _categoryService.CreateCategory(name);
        }

        public List<Category> GetAllCategories()
        {
            return _categoryService.GetAllCategories();
        }

        public void UpdateCategory(
            Guid id,
            string name)
        {
            _categoryService.UpdateCategory(id, name);
        }

        public void DeleteCategory(Guid id)
        {
            _categoryService.DeleteCategory(id);
        }

        public Recipe CreateRecipe(
            string name,
            Guid userId,
            List<Guid> ingredientIds,
            Guid categoryId,
            List<string> steps)
        {
            return _recipeService.CreateRecipe(
                name,
                userId,
                ingredientIds,
                categoryId,
                steps);
        }

        public void UpdateRecipe(
            Guid recipeId,
            string name,
            List<Guid> ingredientIds,
            Guid categoryId,
            List<string> steps)
        {
            _recipeService.UpdateRecipe(
                recipeId,
                name,
                ingredientIds,
                categoryId,
                steps);
        }

        public void DeleteRecipe(Guid recipeId)
        {
            _recipeService.DeleteRecipe(recipeId);
        }

        public List<Recipe> GetAllRecipes()
        {
            return _recipeService.GetAllRecipes();
        }

        public List<Recipe> GetRecipesByUser(Guid userId)
        {
            return _recipeService.GetRecipesByUser(userId);
        }

        public List<Recipe> GetRecipesByCategory(Guid categoryId)
        {
            return _recipeService.GetRecipesByCategory(categoryId);
        }

        public List<Recipe> GetRecipesByIngredient(Guid ingredientId)
        {
            return _recipeService.GetRecipesByIngredient(ingredientId);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
