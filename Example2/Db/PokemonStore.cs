using System;
using System.Threading.Tasks;
using Example2.Model;

namespace Example2.Db;

public class PokemonStore
{
    public async Task SavePokemon(Pokemon pokemon)
    {
        Console.WriteLine("Saving to database: " + pokemon.Name);

        await Task.Delay(0);
    }
}