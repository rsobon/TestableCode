using System;
using System.Threading.Tasks;
using Example3.Command;
using Example3.Db;
using Example3.Logging;
using Example3.Reader;
using Example3.Wrappers;

namespace Example3
{
    /*
     * Programming to interfaces
     */
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var command = new ImportPokemonCommand(
                new Logger(),
                new PokemonStore(),
                new PokemonReader(
                    new DateTimeWrapper(),
                    new Logger()),
                new FileSystemWrapper());

            await command.ImportPokemon(@"App_Data\data.json");

            Console.ReadKey();
        }
    }
}
