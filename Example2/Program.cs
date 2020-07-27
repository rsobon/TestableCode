using System;
using System.Threading.Tasks;
using Example2.Command;

namespace Example2
{
    /*
     * Created new class EntityReader for isolation
     * Still tightly coupled
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
