# PV260 group project
Repository containing group project of team-1.

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
- `Slices/Shared/` - shared contracts/views (error model + error view)
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
