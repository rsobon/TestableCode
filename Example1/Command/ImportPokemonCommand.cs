using System;
using System.IO;
using System.Threading.Tasks;
using Example1.Db;
using Example1.Enums;
using Example1.Model;
using Newtonsoft.Json;

namespace Example1.Command
{
    public class ImportPokemonCommand
    {
        public async Task<ImportingStatus> ImportPokemon(string filePath)
        {
            try
            {
                Console.WriteLine($"Received pokemon to import: {filePath}..."); // usage of static method

                var fileContent = File.ReadAllText(filePath);
                Pokemon pokemon = JsonConvert.DeserializeObject<Pokemon>(fileContent); // usage of framework method
                pokemon.Timestamp = DateTime.Now;

                var database = new PokemonStore(); // new keyword
                await database.SavePokemon(pokemon);

                Console.WriteLine($"Pokemon saved. Id: {pokemon.Id}, Name: {pokemon.Name}, Type: {pokemon.Type}, Timestamp: {pokemon.Timestamp}");

                return ImportingStatus.Success;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error! " + e.Message);
                return ImportingStatus.Error;
            }
        }
    }
}
