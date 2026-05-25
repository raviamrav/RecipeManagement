using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RecipeLibrary.Domain.Entities;

namespace RecipeLibrary.Infrastructure.Persistence
{
    public class RecipeDbContext : DbContext
    {
        // ===== TABLES =====

        public DbSet<User> Users { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        public DbSet<RecipeStep> RecipeSteps { get; set; }


        // ===== DATABASE CONFIG =====

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=recipes.db");
            //var dbPath = Path.Combine(AppContext.BaseDirectory, "recipes.db");
            //var dbPath = Path.Combine(
            //    Directory.GetCurrentDirectory(),
            //    "recipes.db"
            //);
            //Console.WriteLine($"DB: {dbPath}");
            ////var dbDirectory = Path.GetDirectoryName(dbPath);
            //optionsBuilder.UseSqlite($"Data Source={dbPath}");

            var solutionRoot = Path.GetFullPath(
                Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..")
            );

            var dbPath = Path.Combine(solutionRoot, "recipes.db");

            Console.WriteLine($"DB PATH: {dbPath}");

            optionsBuilder.UseSqlite($"Data Source={dbPath}");

        }


        // ===== RELATIONSHIP CONFIG =====

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite Primary Key
            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => new { ri.RecipeId, ri.IngredientId });


            // Unique Recipe Name
            modelBuilder.Entity<Recipe>()
                .HasIndex(r => r.Name)
                .IsUnique();


            // Unique Category Name
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();


            // Unique Ingredient Name
            modelBuilder.Entity<Ingredient>()
                .HasIndex(i => i.Name)
                .IsUnique();


            // Unique User Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}