using Example6.Command;
using Example6.Db;
using Example6.Reader;
using Example6.Validation;
using Example6.Wrappers;
using Microsoft.Extensions.DependencyInjection;

namespace Example6;

public static class DependencyInjectionExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<IPokemonStore, PokemonStore>();
        services.AddTransient<IPokemonReader, PokemonReader>();
        services.AddTransient<IDateTimeWrapper, DateTimeWrapper>();
        services.AddTransient<IFileSystemWrapper, FileSystemWrapper>();
        services.AddTransient<IImportPokemonCommand, ImportPokemonCommand>();
        services.AddTransient<IPokemonValidationService, PokemonValidationService>();
    }
}