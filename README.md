# PV260 group project
Repository containing group project of team-1.

# Git hooks
Use the repository hooks for commit message validation, linting, and pre-push tests.

```sh
git config core.hooksPath .githooks
```

Commit subject must match:
- `<command>(us[0-9]+): text`
- allowed commands: `feat`, `fix`, `refactor`, `docs`, `style`, `test`, `chore`, `ci`, `build`, `perf`, `revert`
- example: `feat(us3): add CI workflow`

Pre-push hook behavior:
- runs `dotnet test PV260.ArkFundsTracker.sln --configuration Release --nologo`
- blocks push if tests fail

# Milestone 1
All the artifacts required for `milestone-1` are located in `doc/` folder:

- Big Picture Event Storming (`doc/big_picture.png`)
- Process Modeling (`doc/process_modeling.png`)
- User Stories (`doc/user_stories.md`)
- Estimations of User Stories (`doc/estimations.md`)

# Milestone 2

## US3 Project Setup & MVC Infrastructure
The ASP.NET Core MVC solution skeleton is located in `src/PV260.ArkFundsTracker.Web`.

## US4 Database Schema & PostgreSQL Configuration
Project uses PostgreSQL database.
Connection string key used by the app is `ConnectionStrings:Default`.

### Configuration model
- `appsettings*.json` stores `ConnectionStrings:Default` as a template with `${POSTGRES_*}` placeholders.
- Placeholders are resolved at startup from environment variables.
- If any required `POSTGRES_*` variable is missing, app startup fails fast with a clear error.

### Set DB secrets (optional, local `dotnet run` only)
```
dotnet user-secrets init - if not setup yet
dotnet user-secrets set "ConnectionStrings:Default" "Host=localhost;Port=5432;Database=arkfundsDB;Username=postgres;Password=*your_password*"
```

Use this only when you run the app outside Docker and do not want to export `POSTGRES_*` variables manually.

### Configure environment variables via .env
Use the sample file and create your local `.env`:

```powershell
Copy-Item .env.example .env
```

`.env` is used by Docker Compose to configure:
- PostgreSQL credentials and port
- `POSTGRES_*` variables used by `ConnectionStrings:Default` placeholders in appsettings
- `APP_ENVIRONMENT` to switch between `Development` and `Production`

Default PostgreSQL image is pinned to `postgres:17` via `POSTGRES_IMAGE` to avoid `postgres:latest` major-upgrade surprises during local development.

`dotnet run` startup now loads the nearest `.env` file automatically (searching current directory and parent directories).
Already-set environment variables still win over values from `.env`.

### Vertical Slice structure
- `Slices/Home/` - Home feature (controller, view model, and views)
- `Slices/Common/` - shared contracts/views (error model + error view)
- `Slices/FundPosition/` - FundPosition (entity model)
- `Infrastructure/DependencyInjection/` - startup registration extensions
- `Infrastructure/Configuration/` - strongly typed options
- `Infrastructure/Data/` - Database connection (DB context, entities configuration)
- `Migrations/` - Migrations for database 

### Environment configuration
Configuration files:
- `src/PV260.ArkFundsTracker.Web/appsettings.json`
- `src/PV260.ArkFundsTracker.Web/appsettings.Development.json`
- `src/PV260.ArkFundsTracker.Web/appsettings.Production.json`

`Application` options can be overridden by environment variables, e.g.:
- `Application__Name`

Database variables expected by current setup:
- `POSTGRES_HOST`
- `POSTGRES_PORT`
- `POSTGRES_DB`
- `POSTGRES_USER`
- `POSTGRES_PASSWORD`

Set runtime environment with `ASPNETCORE_ENVIRONMENT` (`Development`, `Production`, ...).

### Run locally
Local non-Docker options:
- Create `.env` from `.env.example` (recommended), or
- set `POSTGRES_*` in your shell/IDE environment, or
- set `ConnectionStrings:Default` via `dotnet user-secrets`.

```powershell
dotnet restore .\PV260.ArkFundsTracker.sln
dotnet build .\PV260.ArkFundsTracker.sln
dotnet ef database update --project .\src\PV260.ArkFundsTracker.Web
dotnet run --project .\src\PV260.ArkFundsTracker.Web\PV260.ArkFundsTracker.Web.csproj
```

```sh
dotnet restore ./PV260.ArkFundsTracker.sln
dotnet build ./PV260.ArkFundsTracker.sln
dotnet ef database update --project ./src/PV260.ArkFundsTracker.Web
dotnet run --project ./src/PV260.ArkFundsTracker.Web/PV260.ArkFundsTracker.Web.csproj
```

### Run with Docker Compose
Start app + database in Development mode (`docker-compose.yml`):

```powershell
Copy-Item .env.example .env
docker compose up --build
```

Run in Production mode (same compose file, env-controlled):

```powershell
(Get-Content .env) -replace '^APP_ENVIRONMENT=.*', 'APP_ENVIRONMENT=Production' | Set-Content .env
docker compose up --build
```

Start only PostgreSQL database (`docker-compose.db.yml`):

```powershell
Copy-Item .env.example .env
docker compose -f docker-compose.db.yml up -d
```

