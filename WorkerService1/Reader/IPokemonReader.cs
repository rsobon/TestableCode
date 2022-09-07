using Example6.Model;

namespace Example6.Reader;

public interface IPokemonReader
{
    Task<Pokemon> ReadPokemon(Stream stream);
}