using System;
using System.Threading.Tasks;
using Example1.Command;

namespace Example1;

internal static class Program
{
    static async Task Main()
    {
        var command = new ImportPokemonCommand();

        await command.ImportPokemon(@"App_Data\data.json");

        Console.ReadKey();
    }
}