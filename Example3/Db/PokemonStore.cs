using System;
using System.Threading.Tasks;
using Example3.Model;

namespace Example3.Db;

public class PokemonStore : IPokemonStore
{
    public async Task SavePokemon(Pokemon pokemon)
    {
        Console.WriteLine("Saving to database: " + pokemon.Name);

        await Task.Delay(0);
    }
}