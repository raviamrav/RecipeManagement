using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RecipeLibrary.Domain.Entities;

namespace RecipeLibrary.Infrastructure.Persistence
{
    public class RecipeDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<RecipeStep> RecipeSteps { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath =
                Path.Combine(
                    AppContext.BaseDirectory,
                    "recipes.db");

            optionsBuilder.UseSqlite(
                $"Data Source={dbPath}");
        }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            // =========================
            // UNIQUE CONSTRAINTS
            // =========================

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Recipe>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Ingredient>()
                .HasIndex(i => i.Name)
                .IsUnique();

            // =========================
            // RECIPE INGREDIENT (MANY-TO-MANY)
            // =========================

            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => ri.Id);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany()
                .HasForeignKey(ri => ri.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // RECIPE STEP (1:N)
            // =========================

            modelBuilder.Entity<RecipeStep>()
                .HasKey(rs => rs.Id);

            modelBuilder.Entity<RecipeStep>()
                .HasOne(rs => rs.Recipe)
                .WithMany(r => r.RecipeSteps)
                .HasForeignKey(rs => rs.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}