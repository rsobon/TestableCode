using System.Threading.Tasks;
using Example4.Enums;

namespace Example4.Command
{
    public interface IImportPokemonCommand
    {
        Task<ImportingStatus> ImportPokemon(string filePath);
    }
}