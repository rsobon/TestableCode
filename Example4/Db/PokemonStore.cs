using System;
using System.Threading.Tasks;
using Example4.Model;

namespace Example4.Db;

public class PokemonStore : IPokemonStore
{
    public async Task SavePokemon(Pokemon pokemon)
    {
        Console.WriteLine("Saving to database: " + pokemon.Name);

        await Task.Delay(0);
    }
}