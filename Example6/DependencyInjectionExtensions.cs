using Example6.Command;
using Example6.Db;
using Example6.Reader;
using Example6.Validation;
using Example6.Wrappers;
using Microsoft.Extensions.DependencyInjection;

namespace Example6;

public static class DependencyInjectionExtensions
{
    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IPokemonStore, PokemonStore>();
        serviceCollection.AddTransient<IPokemonReader, PokemonReader>();
        serviceCollection.AddTransient<IDateTimeWrapper, DateTimeWrapper>();
        serviceCollection.AddTransient<IFileSystemWrapper, FileSystemWrapper>();
        serviceCollection.AddTransient<IImportPokemonCommand, ImportPokemonCommand>();
        serviceCollection.AddSingleton<Func<IImportPokemonCommand>>(provider => provider.GetRequiredService<IImportPokemonCommand>);
        serviceCollection.AddTransient<IPokemonValidationService, PokemonValidationService>();
    }
}