using System.Threading.Tasks;
using Example4.Model;

namespace Example4.Db;

public interface IPokemonStore
{
    Task SavePokemon(Pokemon pokemon);
}