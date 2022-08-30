using System.IO;
using System.Threading.Tasks;
using Example5.Model;

namespace Example5.Reader;

public interface IPokemonReader
{
    Task<Pokemon> ReadPokemon(Stream stream);
}