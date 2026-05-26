# Programming Assignment: Recipe Management as a .NET Library

## Objective
Develop a .NET class library in C# that provides recipe, category, and user management. 

The solution should behave like production-ready code and demonstrate how you analyze and implement real-world requirements.

## Key Focus Areas
Place particular emphasis on:

- Clear structure and well-defined responsibilities  
- Transparent architecture and design decisions  
- Code quality (understandable, maintainable, extensible, testable, easily analyzable)  
- Application of common software development principles  

## Specifications
- Language/Framework: C# in the .NET ecosystem  
- Architecture, tooling, and external libraries: freely selectable, appropriate for the task and your knowledge  

## Expected Results
- Link to a public Git repository 
- A short README with installation/startup instructions and an architecture overview
- A .NET class library
- A sample project to demonstrate the library

### The .NET class library

- Encapsulates the logic for implementing the business requirements
- Provides the required functionalities via a simple API
- Can be used in any application (desktop app, website, WebAPI)
- Enables data to be stored permanently (technology and structure of your choice)


### The Sample Project for Demonstration
Create a small supplementary project that uses the library in typical use cases. No complex UI is required.

Important: The demo project itself must not contain any business logic. It merely demonstrates the developed library via the API provided for that purpose (i.e., no direct access to persistence or the library’s internal components) 


## Business Requirements

### User Management
- Only registered users may manage recipes  
- User management is required  

### Recipe Management
- Users can create, edit, and delete recipes  
- Constraints:
  - Recipe names are globally unique  
  - A recipe contains at least one preparation step  
  - A recipe contains at least one ingredient  
  - A recipe is assigned to at least one category  
- Ingredients:
  - come from a global, user-independent ingredient list  
  - new ingredients can be added  

### Category Management
- Categories can be created, edited, and deleted  
- Category names are unique  

### Queries & Favorites
- Display recipes from a specific user  
- Display recipes by category  
- Display recipes by ingredient  
- Optional: Mark/unmark other users' recipes as favorites  
- Optional: Display a user's favorites

