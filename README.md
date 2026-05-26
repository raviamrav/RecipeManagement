# Recipe Management System

A reusable .NET class library for managing users, recipes, ingredients, and categories, with persistent SQLite storage and a small console demo project.

The project is intentionally structured as a library-first solution: business rules live in `RecipeLibrary`, while `RecipeDemoApp` only demonstrates the public API. This keeps the sample application thin and makes the library reusable from a desktop app, Web API, worker service, or another .NET application.

## Overview

This solution implements a recipe management domain using C#, Entity Framework Core, and SQLite. It focuses on clear responsibilities, explicit validation, persistent storage, and a simple public API that hides infrastructure details from consumers.

The main entry point for consumers is:

```csharp
using var recipes = new RecipeManagement();
```

Applications can then register users, create ingredients and categories, create/update/delete recipes, and query recipes without directly creating repositories or accessing `DbContext`.

## Business Requirements Implemented

| Requirement | Status | Where handled |
| --- | --- | --- |
| .NET class library | Implemented | `RecipeLibrary` |
| Sample project | Implemented | `RecipeDemoApp` |
| Persistent storage | Implemented | SQLite via EF Core |
| Registered users required for recipes | Implemented | `RecipeService` validates `userId` |
| User management | Implemented | `UserService`, `RecipeManagement` |
| Create/edit/delete recipes | Implemented | `RecipeService`, repositories |
| Globally unique recipe names | Implemented | service validation + unique EF index |
| Recipe requires at least one step | Implemented | `RecipeService` validation |
| Recipe requires at least one ingredient | Implemented | `RecipeService` validation |
| Recipe assigned to a category | Implemented | `RecipeService` validates category |
| Global ingredient list | Implemented | `IngredientService`, `Ingredients` table |
| Add new ingredients | Implemented | `CreateIngredient` |
| Create/edit/delete categories | Implemented | `CategoryService` |
| Unique category names | Implemented | service validation + unique EF index |
| Query recipes by user | Implemented | `GetRecipesByUser` |
| Query recipes by category | Implemented | `GetRecipesByCategory` |
| Query recipes by ingredient | Implemented | `GetRecipesByIngredient` |
| Favorites | Not implemented | Optional requirement |

## High-Level Design

```text
RecipeDemoApp
    |
    | uses public library API only
    v
RecipeLibrary.RecipeManagement
    |
    | coordinates application services
    v
Application Services
    |
    | validate business rules through repository interfaces
    v
Infrastructure Repositories
    |
    | persist and query data
    v
SQLite Database
```

## Architecture

The codebase follows a layered structure:

- `Domain`: core entities such as `User`, `Recipe`, `Ingredient`, `Category`, `RecipeIngredient`, and `RecipeStep`.
- `Application`: service classes and repository interfaces. This layer owns business validation and use-case orchestration.
- `Infrastructure`: Entity Framework Core `DbContext` and SQLite repository implementations.
- `RecipeManagement`: public facade that wires the internal services and persistence components.
- `RecipeDemoApp`: console sample that calls the library API and demonstrates typical use cases.

The sample project does not create repositories or access the database context directly. This is deliberate: the demo behaves like an external consumer of the library.

## Database Design

SQLite is used as a lightweight persistent store. Entity Framework Core maps the domain entities to relational tables.

Main tables:

- `Users`
- `Recipes`
- `Ingredients`
- `Categories`
- `RecipeIngredients`
- `RecipeSteps`

Important relationships:

- One user can own many recipes.
- A recipe belongs to one category.
- A recipe has many preparation steps.
- A recipe can use many ingredients through `RecipeIngredients`.
- Ingredients are global and not tied to a specific user.

Important constraints:

- User email is unique.
- Recipe name is unique.
- Category name is unique.
- Ingredient name is unique.

## Technology Stack

- C#
- .NET 10
- Entity Framework Core
- SQLite
- LINQ
- Console application for demonstration

## How to Use This Library

Add a reference to `RecipeLibrary` from another .NET project:

```powershell
dotnet add reference ../RecipeLibrary/RecipeLibrary.csproj
```

Use the public facade:

```csharp
using RecipeLibrary;

using var recipes = new RecipeManagement();

var user = recipes.RegisterUser("Ravi", "ravi@mail.com");
var salt = recipes.CreateIngredient("Salt");
var category = recipes.CreateCategory("Italian");

var recipe = recipes.CreateRecipe(
    name: "Pasta Carbonara",
    userId: user.Id,
    ingredientIds: new List<Guid> { salt.Id },
    categoryId: category.Id,
    steps: new List<string>
    {
        "Boil pasta",
        "Season with salt"
    });
```

The consuming application does not need to manually instantiate repositories or `RecipeDbContext`.

## Running the Demo Application

From the repository root:

```powershell
dotnet restore
dotnet build
dotnet run --project RecipeDemoApp
```

Run it a second time to see the persistent database behavior:

```powershell
dotnet run --project RecipeDemoApp
```

The SQLite database is created automatically by the library when the demo runs. Do not run `dotnet ef database update`; this project currently uses `EnsureCreated()` for simple local setup instead of migrations.

## Demo Flow

On the first run, the demo creates sample data and executes the full workflow:

```text
=== USER CREATION ===
User created: Ravi

=== INGREDIENT CREATION ===
Ingredient created: Salt
Ingredient created: Milk
Ingredient created: Chocolate

=== CATEGORY CREATION ===
Category created: Italian

=== RECIPE CREATION ===
Recipe created: Pasta Carbonara

=== RECIPE UPDATE ===
Recipe updated successfully

=== RECIPES BY USER ===
- Updated Pasta Carbonara

=== RECIPES BY CATEGORY ===
- Updated Pasta Carbonara

=== RECIPES BY INGREDIENT ===
- Updated Pasta Carbonara

=== CATEGORY UPDATE ===
Category updated successfully

=== DELETE RECIPE ===
Temporary recipe deleted successfully

=== DELETE CATEGORY ===
Temporary category deleted successfully

=== DEMO COMPLETE ===
```

On the second run, the database is reused. Existing setup data is loaded instead of recreated:

```text
=== USER CREATION ===
Existing user loaded: Ravi

=== INGREDIENT CREATION ===
Ingredient 'Salt' already exists
Ingredient 'Milk' already exists
Ingredient 'Chocolate' already exists

=== CATEGORY CREATION ===
Category 'Italian' already exists

=== RECIPE CREATION ===
Recipe name already exists
```

The update, query, and delete demonstrations still run after that.

## Key Design Decisions

- `RecipeManagement` facade: gives consumers one simple API and keeps infrastructure wiring out of the demo project.
- Service layer validation: business rules are checked before persistence operations.
- Repository interfaces: application services depend on abstractions, making storage replaceable.
- EF Core + SQLite: provides real persistence with low setup cost for a portfolio/interview project.
- `EnsureCreated()` for demo setup: keeps fresh-clone execution simple without requiring migration commands.
- Idempotent demo flow: the demo can be run multiple times and still demonstrates persistent data behavior.

## Known Limitations

- No Web API or UI is included; the sample is a console application.
- Favorites are not implemented because they are optional in the assignment.
- Authentication is not implemented; user registration is modeled, but login/session handling is outside the scope.
- The current setup uses `EnsureCreated()` instead of a migration workflow, which is suitable for a simple demo but not ideal for long-term schema evolution.
- There is no separate automated test project yet; the console demo acts as an executable integration demonstration.

## Future Improvements

- Add an xUnit test project for service validation and repository behavior.
- Add optional favorites support.
- Expose the library through an ASP.NET Core Web API.
- Replace `EnsureCreated()` with migrations for production-style schema evolution.
- Add dependency injection configuration for host applications.
- Add structured logging and more specific custom exception types.

## Resetting Demo Data

To reset the demo to first-run behavior, delete the generated SQLite database:

```powershell
Remove-Item .\RecipeDemoApp\bin\Debug\net10.0\recipes.db
```

Then run:

```powershell
dotnet run --project RecipeDemoApp
```
