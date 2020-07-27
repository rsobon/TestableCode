using System;
using System.Threading.Tasks;
using Example1.Command;

namespace Example1
{
    /*
     * Tight coupling:
     * - static classes and methods
     * - framework classes and methods
     * - new keyword
     */
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var command = new ImportPokemonCommand();

            await command.ImportPokemon(@"App_Data\data.json");

            Console.ReadKey();
        }
    }
}
