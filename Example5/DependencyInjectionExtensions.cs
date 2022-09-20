using Example5.Command;
using Example5.Db;
using Example5.Logging;
using Example5.Reader;
using Example5.Validation;
using Example5.Wrappers;
using Microsoft.Extensions.DependencyInjection;

namespace Example5;

public static class DependencyInjectionExtensions
{
    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IPokemonStore, PokemonStore>();
        serviceCollection.AddTransient<IPokemonReader, PokemonReader>();
        serviceCollection.AddTransient<IDateTimeWrapper, DateTimeWrapper>();
        serviceCollection.AddTransient<ILogger, Logger>();
        serviceCollection.AddTransient<IFileSystemWrapper, FileSystemWrapper>();
        serviceCollection.AddTransient<IImportPokemonCommand, ImportPokemonCommand>();
        serviceCollection.AddTransient<IPokemonValidationService, PokemonValidationService>();
    }
}