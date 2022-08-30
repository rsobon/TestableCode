using System;
using System.Threading.Tasks;
using Example2.Command;

namespace Example2;

internal static class Program
{
    static async Task Main()
    {
        var command = new ImportPokemonCommand();

        await command.ImportPokemon(@"App_Data\data.json");

        Console.ReadKey();
    }
}