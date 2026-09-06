# Todo List API (.NET 8)

ASP.NET Core Web API for user registration, login, and managing personal todo items. The API uses JWT bearer tokens for authentication, ASP.NET Core Identity for user accounts, Entity Framework Core with SQL Server for persistence, AutoMapper for DTO mapping, and Swagger for local API exploration.

GitHub repository: <https://github.com/abdelrahmanelsaeed208-glitch/Todo-List-API-.NET-8>

## Project Status

The main API issues found during review have been fixed:

- JWT authentication middleware is enabled before authorization.
- Todo endpoints are protected with `[Authorize]`.
- The todo list route is `GET /api/todos` instead of the incorrect `GET /api/todos/{id}` route.
- Pagination now returns real todo data, total count, page size, and total pages.
- Request DTOs use validation attributes so invalid requests return automatic 400 responses.
- The unused custom `User` model was removed because the app uses ASP.NET Core Identity.
- Unused package references were removed from the project file.
- `.gitignore` now excludes Visual Studio and build output folders.

Remaining recommended work:

- Add automated tests for register, login, todo ownership, pagination, and validation.
- Move production connection strings and JWT keys to a secure secret store.
- Consider returning `404 NotFound` for missing todos instead of `400 BadRequest`.

## Tech Stack

- .NET 8
- ASP.NET Core Web API controllers
- ASP.NET Core Identity
- JWT bearer authentication
- Entity Framework Core
- SQL Server LocalDB
- AutoMapper
- Swagger / Swashbuckle

## Project Structure

```text
ToDoListAPI/
  Controllers/       API endpoints for authentication and todos
  DTOs/              Request and response contracts
  Models/            Entity models
  Data/              EF Core DbContext
  Repositories/      Data access layer
  Services/          Business logic layer
  Helpers/           JWT helper
  Mapping/           AutoMapper profile
  Migrations/        EF Core migrations
```

## Requirements

- .NET 8 SDK
- SQL Server LocalDB or another SQL Server instance
- Visual Studio, Visual Studio Code, or another C# editor

## Configuration

The default connection string is in `ToDoListAPI/appsettings.json`:

```json
"ConnectionStrings": {
  "My Connection": "Server=(localdb)\\mssqllocaldb;Database=ToDoListDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

JWT settings are configured in `appsettings.json`, while the signing key is stored in the local `.env` file. The `.env` file is ignored by Git:

```json
"JWT": {
  "Issuer": "https://localhost:7125",
  "Audience": "https://localhost:5001",
  "DurationInDays": 30
}
```

Create `/.env` from `.env.example` and set a long random value for `JWT__KEY`:

```powershell
Copy-Item .env.example .env
```

## Run Locally

Restore packages:

```powershell
dotnet restore
```

Apply database migrations:

```powershell
dotnet ef database update --project ToDoListAPI
```

Run the API:

```powershell
dotnet run --project ToDoListAPI
```

Open Swagger:

```text
https://localhost:7125/swagger
```

The exact local URL may vary depending on `ToDoListAPI/Properties/launchSettings.json`.

## API Endpoints

### Authentication

| Method | Endpoint | Description |
| --- | --- | --- |
| POST | `/api/auth/register` | Register a new user and return a JWT |
| POST | `/api/auth/login` | Login and return a JWT |

Example register request:

```json
{
  "name": "Abdelrahman",
  "email": "user@example.com",
  "password": "Password123!"
}
```

Example login request:

```json
{
  "email": "user@example.com",
  "password": "Password123!"
}
```

### Todos

Todo endpoints require a JWT bearer token.

| Method | Endpoint | Description |
| --- | --- | --- |
| POST | `/api/todos` | Create a todo |
| GET | `/api/todos` | Get paged todos for the current user |
| PUT | `/api/todos/{id}` | Update a todo |
| DELETE | `/api/todos/{id}` | Delete a todo |

Example create request:

```json
{
  "title": "Learn ASP.NET Core",
  "description": "Build a clean todo API"
}
```

Example update request:

```json
{
  "title": "Learn ASP.NET Core",
  "description": "Finish API cleanup",
  "isCompleted": true
}
```

Example query:

```text
GET /api/todos?page=1&limit=10&search=api&isCompleted=false
```

## Authentication Usage

After login or registration, copy the returned token and send it with todo requests:

```text
Authorization: Bearer <token>
```

Swagger is configured with a Bearer security scheme, so you can use the Authorize button in Swagger UI.

## Build

```powershell
dotnet build ToDoListAPI.sln
```

Current verification result:

```text
Build succeeded.
0 warnings.
0 errors.
```
