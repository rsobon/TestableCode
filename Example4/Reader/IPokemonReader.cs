using System.IO;
using System.Threading.Tasks;
using Example4.Model;

namespace Example4.Reader;

public interface IPokemonReader
{
    Task<Pokemon> ReadPokemon(Stream stream);
}