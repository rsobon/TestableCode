using Example4.Command;
using Example4.Db;
using Example4.Logging;
using Example4.Reader;
using Example4.Wrappers;
using Microsoft.Extensions.DependencyInjection;

namespace Example4
{
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
        }
    }
}
