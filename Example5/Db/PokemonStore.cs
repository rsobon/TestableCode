using System;
using System.Threading.Tasks;
using Example5.Model;

namespace Example5.Db;

public class PokemonStore : IPokemonStore
{
    public async Task SavePokemon(Pokemon pokemon)
    {
        Console.WriteLine("Saving to database: " + pokemon.Name);

        await Task.Delay(0);
    }
}