using System;
using System.Collections.Generic;
using System.Text;
using RecipeLibrary.Domain.Entities;
using RecipeLibrary.Infrastructure.Persistence;

namespace RecipeLibrary.Infrastructure.Repositories
{
    public class SqliteIngredientRepository : IIngredientRepository
    {
        private readonly RecipeDbContext _dbContext;

        public SqliteIngredientRepository(RecipeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Ingredient ingredient)
        {
            _dbContext.Ingredients.Add(ingredient);
            _dbContext.SaveChanges();
        }

        public List<Ingredient> GetAll()
        {
            return _dbContext.Ingredients.ToList();
        }

        public Ingredient? GetById(Guid id)
        {
            return _dbContext.Ingredients.FirstOrDefault(i => i.Id == id);
        }

        public Ingredient? GetByName(string name)
        {
            return _dbContext.Ingredients.FirstOrDefault(i => i.Name.ToLower() == name.ToLower());
        }

        public void Delete(Ingredient ingredient)
        {
            _dbContext.Ingredients.Remove(ingredient);
            _dbContext.SaveChanges();
        }
    }
}
