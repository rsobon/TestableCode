using Example6.Model;

namespace Example6.Db;

public class PokemonStore : IPokemonStore
{
    public async Task SavePokemon(Pokemon pokemon)
    {
        Console.WriteLine("Saving to database: " + pokemon.Name);

        await Task.Delay(0);
    }
}