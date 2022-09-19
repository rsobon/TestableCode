# Example6

- Triangulation
- TDD
- InMemory database
- Use appsettings to set input file path instead of hard coded
- Singleton to load PokemonConfiguration
- DI lifetimes
- Service locators and factories

## `Program`
- Used `Worker` pattern
- Used `Microsoft.Extensions.Logging.ILogger`
- User Entity Framework with SQL Server provider

## `Worker`
Three different types of instantiating:
- Service Locator
- Transient lifetime
- Factory pattern

## `DependencyInjectionExtensions`
- Auto-factory pattern
- Transient registration of `PokemonDbContext` without interface?!

## `ImportPokemonCommnad`
- Maybe instead of `ImportingStatus` there should be more complex result object saved to DB. 
- This way we can assert it in the integration test with all possible results (validation, parsing errors etc)

## Development tools
Starting up local db:
```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:latest
```

Package Manager Console commands:
```
Add-Migration Init
Update-Database
```