using System.Threading.Tasks;
using Example5.Model;

namespace Example5.Db
{
    public interface IPokemonStore
    {
        Task SavePokemon(Pokemon pokemon);

        bool IsValidationEnabled();
    }
}