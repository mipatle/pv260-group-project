# PV260 Group Project

This project is a web application built with ASP.NET Core MVC for tracking and managing fund positions.

It allows users to view historical fund data, refresh and store the latest data from external sources, and work with financial records through a structured backend connected to a PostgreSQL database.

The application follows a vertical slice architecture and focuses on clean separation of concerns, testability, and maintainable code structure.

Repository containing group project of team-1.

> Built with **.NET 10**

---

## Git Hooks

Use the repository hooks for commit message validation, linting, and pre-push tests.

    git config core.hooksPath .githooks

### Commit Message Format

Commit subject must match:

- `<command>(us[0-9]+): text`
- allowed commands: `feat`, `fix`, `refactor`, `docs`, `style`, `test`, `chore`, `ci`, `build`, `perf`, `revert`
- example: `feat(us3): add CI workflow`

### Pre-push Hook Behavior

- runs `dotnet test PV260.ArkFundsTracker.sln --configuration Release --nologo`
- blocks push if tests fail

---

## Milestone 1

All the artifacts required for `milestone-1` are located in `doc/` folder:

- Big Picture Event Storming (`doc/big_picture.png`)
- Process Modeling (`doc/process_modeling.png`)
- User Stories (`doc/user_stories.md`)
- Estimations of User Stories (`doc/estimations.md`)

---

## Milestone 2

### US3 Project Setup & MVC Infrastructure

The ASP.NET Core MVC solution skeleton is located in:

    src/PV260.ArkFundsTracker.Web

---

### US4 Database Schema & PostgreSQL Configuration

Project uses PostgreSQL database.  
Connection string key used by the app is:

    ConnectionStrings:Default

---

### Configuration Model

- `appsettings.json` and `appsettings.Production.json` keep `ConnectionStrings:Default` empty by default
- `appsettings.Development.json` contains the local host-run development connection string
- Docker Compose uses `.env` for container startup values and sets `ConnectionStrings__Default` for the web container

---

### Development Connection String

`src/PV260.ArkFundsTracker.Web/appsettings.Development.json` currently uses:

    {
      "ConnectionStrings": {
        "Default": "Host=localhost;Port=5434;Database=arkfundsDB;Username=postgres;Password=password123"
      }
    }

If needed, you can still override this value using `ConnectionStrings__Default` environment variable or `dotnet user-secrets`.

---

### Configure Environment Variables via `.env`

Use the sample file and create your local `.env`:

#### Windows (PowerShell)
    Copy-Item .env.example .env

`.env` is used by Docker Compose to configure:

- PostgreSQL credentials and port
- `APP_ENVIRONMENT` to switch between `Development` and `Production`

`.env` values are for Compose-run containers.  
Local host-run (`dotnet run`) uses `appsettings.Development.json` or optional user-secrets overrides.

Default PostgreSQL image is pinned to `postgres:17` via `POSTGRES_IMAGE` to avoid `postgres:latest` major-upgrade surprises during local development.

`dotnet run` uses the connection string from `appsettings.Development.json` by default.  
For Docker Compose, the web container uses the `db` service name internally and sets `ConnectionStrings__Default` itself.

---

### Vertical Slice Structure

- `Slices/Home/` – Home feature (controller, view model, and views)
- `Slices/Common/` – shared contracts/views (error model + error view)
- `Slices/FundPosition/` – FundPosition (entity model)
- `Infrastructure/DependencyInjection/` – startup registration extensions
- `Infrastructure/Configuration/` – strongly typed options
- `Infrastructure/Data/` – Database connection (DB context, entities configuration)
- `Migrations/` – Migrations for database

---

### Environment Configuration

Configuration files:

- `src/PV260.ArkFundsTracker.Web/appsettings.json`
- `src/PV260.ArkFundsTracker.Web/appsettings.Development.json`
- `src/PV260.ArkFundsTracker.Web/appsettings.Production.json`

`Application` options can be overridden by environment variables, e.g.:

- `Application__Name`

Database variables expected for Docker Compose setup:

- `POSTGRES_PORT`
- `POSTGRES_DB`
- `POSTGRES_USER`
- `POSTGRES_PASSWORD`

Set runtime environment with:

    ASPNETCORE_ENVIRONMENT

(e.g. `Development`, `Production`, ...)

---

### Run Locally

Local non-Docker options:

- Use `ConnectionStrings:Default` from `appsettings.Development.json` (default), or
- override with `ConnectionStrings__Default` in your shell/IDE environment

Current local default connection string:

    {
      "ConnectionStrings": {
        "Default": "Host=localhost;Port=5434;Database=arkfundsDB;Username=postgres;Password=password123"
      }
    }

#### Windows

    dotnet restore .\PV260.ArkFundsTracker.sln
    dotnet build .\PV260.ArkFundsTracker.sln
    dotnet ef database update --project .\src\PV260.ArkFundsTracker.Web
    dotnet run --project .\src\PV260.ArkFundsTracker.Web\PV260.ArkFundsTracker.Web.csproj

#### macOS / Linux

    dotnet restore ./PV260.ArkFundsTracker.sln
    dotnet build ./PV260.ArkFundsTracker.sln
    dotnet ef database update --project ./src/PV260.ArkFundsTracker.Web
    dotnet run --project ./src/PV260.ArkFundsTracker.Web/PV260.ArkFundsTracker.Web.csproj

---

### Run with Docker Compose

#### Start app + database (Development)

    Copy-Item .env.example .env
    docker compose up --build

Compose-run web app connects to PostgreSQL using:

    Host=db;Port=5432

inside the Docker network.

---

#### Run in Production mode

    (Get-Content .env) -replace '^APP_ENVIRONMENT=.*', 'APP_ENVIRONMENT=Production' | Set-Content .env
    docker compose up --build

---

#### Start only PostgreSQL database

    Copy-Item .env.example .env
    docker compose -f docker-compose.db.yml up -d

---

## Testing

Project includes automated tests for ingestion and audit logic.

### Running Tests

    dotnet test