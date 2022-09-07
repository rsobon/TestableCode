using Example6.Enums;

namespace Example6.Command;

public interface IImportPokemonCommand
{
    Task<ImportingStatus> ImportPokemon(string filePath);
}