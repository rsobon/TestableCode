using System;
using System.IO;
using System.Threading.Tasks;
using Example2.Db;
using Example2.Enums;
using Example2.Reader;

namespace Example2.Command;

public class ImportPokemonCommand
{
    public async Task<ImportingStatus> ImportPokemon(string filePath)
    {
        try
        {
            Console.WriteLine($"Received pokemon to import: {filePath}...");

            var reader = new PokemonReader();
            var fileContent = await File.ReadAllTextAsync(filePath);
            var pokemon = reader.ReadPokemon(fileContent); // dependency isolated in the PokemonReader service class

            var database = new PokemonStore();
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