# .NET Core API Project

## Overview
This repository contains a .NET Core API project built using the Clean Architecture principles. The application implements a generic repository and unit of work pattern to abstract data access and integrates MediatR for handling application layer requests and commands.

## Features
- **Clean Architecture**: Ensures separation of concerns and scalability.
- **Generic Repository**: Simplifies data access and promotes code reuse.
- **Unit of Work**: Manages database transactions for consistent operations.
- **MediatR**: Implements CQRS (Command Query Responsibility Segregation) pattern for request/response handling.
- **Dependency Injection**: Follows .NET Core's built-in DI container.
- **Extensible Design**: Easy to add or modify features without affecting other layers.

## Project Structure
The project follows the Clean Architecture structure:

```
- src
  - Application
    - DTOs
    - Interfaces
    - Queries
    - Commands
  - Domain
    - Entities
    - Enums
  - Infrastructure
    - Data
    - Repositories
    - UnitOfWork
  - API
    - Controllers
    - Middlewares
    - Startup.cs
```

### Key Layers
1. **Domain**
   - Contains core business logic and domain entities.
2. **Application**
   - Includes interfaces, DTOs, and CQRS handlers (commands and queries).
3. **Infrastructure**
   - Implements data access logic, including repositories and unit of work.
4. **API**
   - Exposes endpoints and integrates all layers.

## Prerequisites
- .NET 8 SDK
- SQL Server (or any supported database)
- Visual Studio or VS Code

## Getting Started
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/your-repo.git
   cd your-repo
   ```

2. Set up the database connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Data Source=.;Initial Catalog=ASAPTaskDB2;Integrated Security=True;Trust Server Certificate=True"
   }
   ```

3. Apply database migrations:
   ```bash
   dotnet ef database update
   ```

4. Run the application:
   ```bash
   dotnet run --project src/API
   ```

## Usage
### Generic Repository
Example usage of the generic repository:
```csharp
var userRepo = unitOfWork.Repository<User>();
var users = await userRepo.GetAllAsync();
```

### Unit of Work
Just run the project it will auto migration and seed user and role
```

## Acknowledgements
- [MediatR](https://github.com/jbogard/MediatR)
- [EF Core](https://learn.microsoft.com/en-us/ef/core/)

