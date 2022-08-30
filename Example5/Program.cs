using System;
using System.Threading.Tasks;
using Example5.Command;
using Microsoft.Extensions.DependencyInjection;

namespace Example5;

internal static class Program
{
    static async Task Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.RegisterServices();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var command = serviceProvider.GetRequiredService<IImportPokemonCommand>();
        await command.ImportPokemon(@"App_Data\data.json");

        Console.ReadKey();
    }
}