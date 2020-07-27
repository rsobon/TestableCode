using System;
using System.Threading.Tasks;
using Example5.Command;
using Microsoft.Extensions.DependencyInjection;

namespace Example5
{
    /*
     * Logic in constructor of PokemonReader
     * - requires to do more setup for tests
     * - if database fails, constructor fails and the rest of the application will fail
     * - cannot test the IsValid method because it's private
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
