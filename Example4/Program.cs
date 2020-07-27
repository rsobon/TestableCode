using System;
using System.Threading.Tasks;
using Example4.Command;
using Microsoft.Extensions.DependencyInjection;

namespace Example4
{
    /*
     * Dependency injection via IoC
     */
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.RegisterServices();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var command = serviceProvider.GetService<IImportPokemonCommand>();
            await command.ImportPokemon(@"App_Data\data.json");

            Console.ReadKey();
        }
    }
}
