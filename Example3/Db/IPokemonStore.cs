using System.Threading.Tasks;
using Example3.Model;

namespace Example3.Db;

public interface IPokemonStore
{
    Task SavePokemon(Pokemon pokemon);
}