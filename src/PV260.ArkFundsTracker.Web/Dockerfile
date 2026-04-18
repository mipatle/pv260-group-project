FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/PV260.ArkFundsTracker.Web/PV260.ArkFundsTracker.Web.csproj", "src/PV260.ArkFundsTracker.Web/"]
RUN dotnet restore "src/PV260.ArkFundsTracker.Web/PV260.ArkFundsTracker.Web.csproj"

COPY . .
WORKDIR /src/src/PV260.ArkFundsTracker.Web
RUN dotnet publish "PV260.ArkFundsTracker.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "PV260.ArkFundsTracker.Web.dll"]

