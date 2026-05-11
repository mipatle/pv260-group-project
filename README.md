# PV260 Group Project

This project is a web application built with ASP.NET Core MVC for tracking and managing fund positions.

It allows users to view historical fund data, refresh and store the latest data from external sources, and work with financial records through a structured backend connected to a PostgreSQL database.

The application follows a vertical slice architecture and focuses on clean separation of concerns, testability, and maintainable code structure.

Repository containing the group project for team-1.

> Built with **.NET 10**

---

## Git Hooks

Use the repository hooks for commit message validation, linting, and pre-push tests.

    git config core.hooksPath .githooks

### Commit Message Format

Commit subject must match:

- <type>(us<NUMBER>): message
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

This milestone introduces the core application architecture, database integration, runtime configuration and tests.

### Project Structure

The ASP.NET Core MVC solution is located in:

    src/PV260.ArkFundsTracker.Web

### Database

The project uses PostgreSQL.

Connection string key used by the application:

    ConnectionStrings:Default

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

#### Create `.env` file

##### Windows (PowerShell)
    Copy-Item .env.example .env

##### macOS / Linux
    cp .env.example .env

The `.env` file is used by Docker Compose to configure:

- PostgreSQL credentials and port
- `APP_ENVIRONMENT` (e.g., `Development`, `Production`)

`.env` values apply only to containerized (Docker Compose) runs.  
When running locally via `dotnet run`, the application uses `appsettings.Development.json` or optional user-secrets overrides.

The PostgreSQL image is pinned to `postgres:17` via `POSTGRES_IMAGE` to avoid unexpected upgrades.

For Docker Compose, the web container connects to the database using the `db` service name and sets `ConnectionStrings__Default` internally.

---

### Vertical Slice Structure

- `Slices/Home/` – Home feature (controller, view model, views)
- `Slices/Common/` – shared contracts and views (e.g., error handling)
- `Slices/FundPosition/` – fund position feature (data ingestion, storage, logic)
- `Slices/TimestampNav/` – timestamp navigation and selection logic
- `Slices/CronFetching/` – scheduled data fetching (background tasks)
- `Infrastructure/DependencyInjection/` – service registration
- `Infrastructure/Configuration/` – strongly typed options
- `Infrastructure/Data/` – database context and configuration
- `Infrastructure/Logging/` – structured logging (CSV parsing, fetching, cron jobs)
- `Migrations/` – database migrations

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

### Admin User Seeding

The application does **not create any default admin user automatically**.

An admin user is seeded on startup **only if** the `AdminUser` configuration section is provided.

For local development, configure the admin user using user secrets:

    dotnet user-secrets set "AdminUser:Email" "admin@example.com" --project src/PV260.ArkFundsTracker.Web
    dotnet user-secrets set "AdminUser:Password" "Admin123" --project src/PV260.ArkFundsTracker.Web

After starting the application, the admin user will be created automatically if it does not already exist.

If the configuration is missing, admin seeding is skipped.

Do not store admin credentials in `appsettings.json`.

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

## Continuous Deployment

The repository now uses separate CD workflows in `.github/workflows/`:

- `dev-cd.yml` deploys to the development Azure App Service after **any successful CI run**.
- `prod-cd.yml` deploys to the production Azure App Service on pushes to `main` and `milestone-*`, and on tags matching `release-*`.

For step-by-step deployment instructions, see [`doc/deployment-guide.md`](doc/deployment-guide.md).

### Health check

The app exposes a readiness endpoint at `/health`.

- It returns `200 OK` when the application can connect to the configured PostgreSQL database.
- It returns `503 Service Unavailable` when the database dependency is not reachable.

### Azure environment settings

Use GitHub Environments and repository variables/secrets for each deployment target:

- `AZURE_WEBAPP_NAME_DEV` / `AZURE_WEBAPP_NAME_PROD`
- `AZURE_RESOURCE_GROUP_DEV` / `AZURE_RESOURCE_GROUP_PROD`
- `AZURE_CREDENTIALS_DEV` / `AZURE_CREDENTIALS_PROD`

Terraform now manages `ASPNETCORE_ENVIRONMENT`, `ConnectionStrings__Default`, and `WEBSITE_HEALTHCHECK_PATH=/health` in Azure App Service, so the CD workflows only deploy the package.

## Terraform Infrastructure

Azure deployment infrastructure is defined in `infra/terraform/`.

It provisions one environment per Terraform run, selected by the `environment` value in `tfvars`.

Resources include:

- Azure Resource Group
- Azure App Service Plan
- Azure Linux Web App
- Azure Database for PostgreSQL Flexible Server
- Log Analytics Workspace
- Application Insights

Default Terraform settings are now aligned to the lowest practical tier for the app:

- Azure region: `germanywestcentral`
- App Service plan: `B1`
- PostgreSQL Flexible Server: `B_Standard_B1ms`
- PostgreSQL version: `18`

You can still override these values in the selected `tfvars` file when you need to target a different region or tier.

### Files

- `infra/terraform/main.tf` – root module wiring for the selected environment
- `infra/terraform/modules/webapp-postgres/` – reusable module for one environment
- `infra/terraform/terraform.tfvars.example` – development template with commented overrides
- `infra/terraform/terraform.prod.tfvars.example` – production template with commented overrides

### Example workflow

```powershell
az login

Copy-Item .\terraform.tfvars.example .\terraform.tfvars -Force
terraform init
terraform plan --var-file=terraform.tfvars
terraform apply --var-file=terraform.tfvars
```

### Notes

- Use a unique `name_prefix` so your Azure resource names do not collide with other subscriptions.
- The tfvars templates are intentionally small: set `environment`, then add only the overrides you need.
- The App Service is configured with `ASPNETCORE_ENVIRONMENT`, `ConnectionStrings__Default`, and `WEBSITE_HEALTHCHECK_PATH=/health`.
- Azure PostgreSQL Flexible Server always uses backups; this setup keeps the retention at the minimum supported 7 days.
- The `/health` endpoint is a readiness check that verifies database connectivity before returning `200 OK`.
- Keep one state file per environment; do not let development and production share the same Terraform state.

---

## Testing

Project includes automated tests for ingestion and audit logic.

### Running Tests

    dotnet test