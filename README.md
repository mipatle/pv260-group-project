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
Project uses .NET User Secrets to store sensitive configuration for database connection strings.

### Set DB secrets
```
dotnet user-secrets init - if not setup yet
dotnet user-secrets set "ConnectionStrings:Default" "Host=localhost;Port=5432;Database=arkfundsDB;Username=postgres;Password=*your_password*"
```

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

Set runtime environment with `ASPNETCORE_ENVIRONMENT` (`Development`, `Production`, ...).

### Run locally
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
