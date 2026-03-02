FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY BudgetManager.sln .
COPY src/ ./src/
COPY tests/ ./tests/

RUN dotnet restore

RUN dotnet publish src/BudgetManager.Web/BudgetManager.Web.csproj \
	-c Release \
	-o /app/publish \
	/p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "BudgetManager.Web.dll"]