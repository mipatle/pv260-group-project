# PV260 group project
Repository containing group project of team-1.

# Git hooks
Use the repository hooks for commit message validation, linting, and pre-push tests.

```sh
git config core.hooksPath .githooks
```

Commit subject must match:
- `<command>(us[0-9]*): text`
- allowed commands: `feat`, `fix`, `refactor`, `docs`, `style`, `test`, `chore`, `ci`, `build`, `perf`, `revert`
- example: `feat(us3): add CI workflow`

Pre-commit hook behavior:
- runs `dotnet format PV260.ArkFundsTracker.sln --verify-no-changes --no-restore`
- blocks commit if lint fails

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

### Vertical Slice structure
- `Slices/Home/` - Home feature (controller, view model, and views)
- `Slices/Common/` - shared contracts/views (error model + error view)
- `Infrastructure/DependencyInjection/` - startup registration extensions
- `Infrastructure/Configuration/` - strongly typed options

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
dotnet run --project .\src\PV260.ArkFundsTracker.Web\PV260.ArkFundsTracker.Web.csproj
```

```sh
dotnet restore ./PV260.ArkFundsTracker.sln
dotnet build ./PV260.ArkFundsTracker.sln
dotnet run --project ./src/PV260.ArkFundsTracker.Web/PV260.ArkFundsTracker.Web.csproj
```
