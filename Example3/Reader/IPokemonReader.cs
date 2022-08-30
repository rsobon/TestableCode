using System.IO;
using System.Threading.Tasks;
using Example3.Model;

namespace Example3.Reader;

public interface IPokemonReader
{
    Task<Pokemon> ReadPokemon(Stream stream);
}