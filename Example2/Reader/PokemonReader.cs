using System;
using System.Text.Json;
using Example2.Model;

namespace Example2.Reader;

public class PokemonReader
{
    public Pokemon ReadPokemon(string fileContent)
    {
        var pokemon = JsonSerializer.Deserialize<Pokemon>(fileContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        }); 

        pokemon.Timestamp = DateTime.Now;
        Console.WriteLine($"Pokemon deserialized. Id: {pokemon.Id}, Name: {pokemon.Name}, Type: {pokemon.Type}, Timestamp: {pokemon.Timestamp}");
        return pokemon;
    }
}