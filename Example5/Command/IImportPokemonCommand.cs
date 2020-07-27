using System.Threading.Tasks;
using Example5.Enums;

namespace Example5.Command
{
    public interface IImportPokemonCommand
    {
        Task<ImportingStatus> ImportPokemon(string filePath);
    }
}