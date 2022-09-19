using Example6.Model;

namespace Example6.Db;

public interface IPokemonStore
{
    Task SavePokemon(IList<Pokemon> pokemon, CancellationToken token);
}