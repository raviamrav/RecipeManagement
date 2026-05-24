using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RecipeLibrary.Domain.Entities;

namespace RecipeLibrary.Infrastructure.Persistence
{
    public class RecipeDbContext : DbContext
    {
        public DbSet<User> Users {  get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase("RecipeLibraryDb");
            var dbPath = Path.Combine(AppContext.BaseDirectory, "recipes.db");
            Console.WriteLine($"DB: {dbPath}");
            //var dbDirectory = Path.GetDirectoryName(dbPath);
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}
