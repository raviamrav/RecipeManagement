# 🍽️ Recipe Management System

## .NET Class Library | Entity Framework Core | SQLite | Clean Architecture

## 📌 Overview
The **Recipe Management System** is a backend system built as a reusable .NET Class Library using C#, Entity Framework Core, and SQLite. 

The project demonstrates a real-world implementation of Clean Architecture principles with a strict separation between the **Domain**, **Application**, and **Infrastructure** layers. 

The core business logic resides entirely inside a reusable Class Library (`RecipeLibrary`), while a separate Console Application (`RecipeDemoApp`) serves exclusively for demonstration, integration testing, and evaluation purposes.

The system manages users, recipes, ingredients, and categories, enforcing real-world business constraints through a highly structured, service-driven architecture.

---

## 🎯 Business Requirements (Implemented)

### 👤 User Management
* Users can register in the system.
* Duplicate email registration is prevented at the service layer.
* Each recipe belongs to a registered user.

### 🍲 Recipe Management
* Users can create, update, and delete recipes.
* **Enforced Business Rules:**
  * Recipe names must be globally unique.
  * Each recipe must contain at least one ingredient.
  * Each recipe must contain at least one preparation step.
  * Each recipe must belong to a valid category.

### 🧂 Ingredient Management
* Ingredients are global and user-independent.
* New ingredients can be added dynamically to the system.
* Duplicate ingredient names are strictly prevented.

### 🏷️ Category Management
* Categories can be created, updated, and deleted.
* Category names must be unique.

### 🔎 Query Features
The system supports advanced querying and data fetching:
* Get recipes by user
* Get recipes by category
* Get recipes by ingredient

---
```
┌──────────────────────────────────────────────────────────────┐
│                    RecipeDemoApp (Console)                   │
│--------------------------------------------------------------│
│ - Demonstrates application workflow                          │
│ - Creates services and executes operations                   │
│ - Used only for testing / demo purposes                      │
└──────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌──────────────────────────────────────────────────────────────┐
│                     Application Layer                        │
│--------------------------------------------------------------│
│ Services:                                                    │
│ - RecipeService                                              │
│ - UserService                                                │
│ - CategoryService                                            │
│ - IngredientService                                          │
│                                                              │
│ Responsibilities:                                            │
│ - Business logic                                             │
│ - Validation rules                                           │
│ - Orchestration                                              │
│ - Repository contracts (interfaces)                          │
└──────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌──────────────────────────────────────────────────────────────┐
│                        Domain Layer                          │
│--------------------------------------------------------------│
│ Entities:                                                    │
│ - User                                                       │
│ - Recipe                                                     │
│ - Ingredient                                                 │
│ - Category                                                   │
│ - RecipeIngredient                                           │
│ - RecipeStep                                                 │
│                                                              │
│ Responsibilities:                                            │
│ - Core business models                                       │
│ - Relationships                                              │
│ - Domain structure                                           │
└──────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌──────────────────────────────────────────────────────────────┐
│                    Infrastructure Layer                      │
│--------------------------------------------------------------│
│ Persistence & External Concerns:                             │
│ - RecipeDbContext                                            │
│ - EF Core Configuration                                      │
│ - SQLite Database                                            │
│ - Repository Implementations                                 │
│   - SqliteRecipeRepository                                   │
│   - SqliteUserRepository                                     │
│   - SqliteCategoryRepository                                 │
│   - SqliteIngredientRepository                               │
└──────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌──────────────────────────────────────────────────────────────┐
│                         SQLite DB                            │
│--------------------------------------------------------------│
│ Tables:                                                      │
│ - Users                                                      │
│ - Recipes                                                    │
│ - Ingredients                                                │
│ - Categories                                                 │
│ - RecipeIngredients                                          │
│ - RecipeSteps                                                │
└──────────────────────────────────────────────────────────────┘

```
---
## 🏗️ Architecture
The project strictly isolates infrastructure and framework specifics away from the core software design patterns.

### 📦 Domain Layer
Contains core business entities, completely free of infrastructure or persistence frameworks:
* `User`
* `Recipe`
* `Ingredient`
* `Category`
* `RecipeIngredient` (Junction table tracking quantitative recipe needs)
* `RecipeStep` (One-to-Many sequential instructions)

### ⚙️ Application Layer
Houses the application services, validation rules, orchestration, and repository abstractions:
* **Main Services:** `RecipeService`, `UserService`, `CategoryService`, `IngredientService`
* Defines interfaces (`IRecipeRepository`, etc.) to uphold dependency inversion.

### 🗄️ Infrastructure Layer
Manages the application's physical boundaries, external frameworks, and storage persistence:
* Entity Framework Core integration and DbContext details
* SQLite configuration and local state mapping
* Concrete repository implementations (e.g., `SqliteRecipeRepository`)

---

## 🗄️ Database Design
The relational system is powered by SQLite and mapped cleanly via the Entity Framework Core ORM layer.

### Tables
* `Users`
* `Recipes`
* `Ingredients`
* `Categories`
* `RecipeIngredients`
* `RecipeSteps`

### Relationships
* **User $\rightarrow$ Recipe** $\rightarrow$ One-to-Many
* **Recipe $\leftrightarrow$ Ingredients** $\rightarrow$ Many-to-Many
* **Recipe $\rightarrow$ Steps** $\rightarrow$ One-to-Many
* **Recipe $\rightarrow$ Category** $\rightarrow$ Many-to-One

---

## ⚙️ Technology Stack
* **Runtime / Compiler:** .NET 10
* **Language:** C#
* **ORM:** Entity Framework Core
* **Database Engine:** SQLite
* **Query Language:** LINQ (Language Integrated Query)
* **Architectural Design Patterns:** Repository Pattern, Service Pattern, Clean Architecture

---

## 📦 How to Use This Library
Because the core system is packaged cleanly as a reusable .NET backend library, it can easily be wired into different frontend systems.

### 1. Add Reference to the Library
```bash
dotnet add reference ../RecipeLibrary/RecipeLibrary.csproj
```

### 2. Configure Database Context
```csharp
var dbContext = new RecipeDbContext();
```

### 3. Create Repository Instances
```csharp
var recipeRepository = new SqliteRecipeRepository(dbContext);
var categoryRepository = new SqliteCategoryRepository(dbContext);
```

### 4. Create Service Instances
```csharp
var recipeService = new RecipeService(recipeRepository, categoryRepository);
```

### 5. Create Recipes
```csharp
var recipe = recipeService.CreateRecipe(
    name: "Pasta Carbonara",
    userId: user.Id,
    ingredientIds: ingredientIds,
    categoryId: category.Id,
    steps: new List<string>
    {
        "Boil pasta",
        "Prepare sauce",
        "Mix ingredients"
    }
);
```

---

## ▶️ Running the Demo Application

### 1. Restore Dependencies
```bash
dotnet restore
```

### 2. Build the Solution
```bash
dotnet build
```

### 3. Apply Database Migrations
```bash
dotnet ef database update --project RecipeLibrary --startup-project RecipeDemoApp
```

### 4. Run the Console Demo
```bash
dotnet run --project RecipeDemoApp
```

---

## 🧪 Demo Flow
When executed, the `RecipeDemoApp` console engine takes you through an end-to-end sandbox lifecycle:
1. **User Scope Setup:** Handles creating a new profile or loading active configurations.
2. **Ingredient Seeding:** Instantiates essential ingredient types.
3. **Category Configuration:** Generates logical group categories.
4. **Recipe Assembly:** Demonstrates relational grouping, building a robust recipe with steps and cross-references.
5. **Recipe Updates:** Modifies active state and updates components on-the-fly.
6. **Query Execution:** Runs filtering searches to isolate data across users, categories, and ingredients.
7. **Delete Operations:** Performs safe, controlled teardowns of data dependencies.

---

## 🧠 Key Design Decisions

* **Clean Architecture:** Guarantees decoupling between business workflows, storage mechanisms, and infrastructure boundaries.
* **Repository Pattern:** Completely wraps persistence querying, abstracting physical data logic and boosting software testability.
* **Entity Framework Core:** Used as an intuitive tool managing mappings, automated relational change tracking, and smooth migration updates.
* **SQLite Database:** Selected for its simple, zero-maintenance cross-platform utility, enabling zero-configuration development loops.
* **Service Layer Validation:** All business rules, format invariants, uniqueness checks, and referential safeguards are checked explicitly inside the service layer before reaching database persistence.

---

## ⚠️ Known Limitations
* Console application interface only (No HTTP API endpoint or graphical layout built).
* No native authentication or authorization filters.
* No decoupled, comprehensive unit test coverage library suite.
* Simple, high-level structural try-catch runtime exception handling.

---

## 🚀 Future Improvements
The following system developments are intended to transform this setup into an enterprise, production-ready environment:
* Transition the class library into an **ASP.NET Core Web API**.
* Implement **JWT-based authentication** (JSON Web Tokens) and role-based policies.
* Integrate automated unit testing frameworks leveraging **xUnit** and **Moq**.
* Incorporate robust structural telemetry using **Serilog** alongside the built-in Microsoft logging abstraction (`ILogger`).
* Build **Docker** container configurations for multi-environment packaging.
* Construct an interface client layer utilizing **React** or **Blazor**.
* Evolve structural layers to deep **Domain-Driven Design (DDD)** elements by configuring proper Aggregate Roots and Value Objects.

---

## 👨‍💻 Author
This repository represents an integration portfolio demonstrating critical backend design concepts:
* Clean Architecture code organization.
* Advanced Entity Framework Core relational schemas.
* Modular Service and Repository software designs.
* Strict validation-first data engineering.
* Modern reusable .NET components.

### 📌 Notes
This software architecture was implemented using an **iterative learning approach**, placing massive importance on addressing concrete EF Core mechanics, keeping domain code separate from infrastructure boundaries, resolving database null safety anomalies, and crafting maintainable service dependencies. It highlights production-focused choices scale-adapted for modern business logic layers.
