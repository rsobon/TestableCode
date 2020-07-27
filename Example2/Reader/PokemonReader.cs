using System;
using Example2.Model;
using Newtonsoft.Json;

namespace Example2.Reader
{
    public class PokemonReader
    {
        public Pokemon ReadPokemon(string fileContent)
        {
            Pokemon pokemon = JsonConvert.DeserializeObject<Pokemon>(fileContent);
            pokemon.Timestamp = DateTime.Now;
            Console.WriteLine($"Pokemon deserialized. Id: {pokemon.Id}, Name: {pokemon.Name}, Type: {pokemon.Type}, Timestamp: {pokemon.Timestamp}");
            return pokemon;
        }
    }
}
