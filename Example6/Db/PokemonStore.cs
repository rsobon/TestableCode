using Example6.Model;

namespace Example6.Db;

public class PokemonStore : IPokemonStore
{
    public async Task SavePokemon(IList<Pokemon> pokemon)
    {
        await Task.Delay(0);
    }
}