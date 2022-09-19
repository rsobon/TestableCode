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
- Factory pattern (Worker)
- Transient lifetime (Worker2)
- Service Locator (Worker3)

## `DependencyInjectionExtensions`
- Transient registration of `PokemonDbContext` without interface?!

## `ImportPokemonCommnad`
- Maybe instead of `ImportingStatus` there should be more complex result object saved to DB. 
- This way we can assert it in the integration test with all possible results (validation, parsing errors etc)

## Example6_Tests
- Valuable integration tests
- Test simulate "real" business event - copying the file to Inbox directory
- We can do proper TDD - method body of tested `ImportPokemonCommnad` can be deleted and test still compile (we only need input and output to write the test)
- We use IoC registrations from main app (`RegisterServices()`) but substitue some registrations for the purpose of testing (`AddDbContext()`)
- No need to have interface for everything (like `PokemonDbContext`)
- Timestamp cannot be mocked (another subsitute in IoC registration)
- More triangulation (more invalid test data)

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