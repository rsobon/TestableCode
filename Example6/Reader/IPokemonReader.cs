using Example6.Model;

namespace Example6.Reader;

public interface IPokemonReader
{
    Task<IList<Pokemon>> ReadPokemon(Stream stream);
}